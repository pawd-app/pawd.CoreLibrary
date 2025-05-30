using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text.Json;

namespace pawd.CoreLibrary.JobRabbitMqBridge
{
    public class JobEventHandlers : IAsyncDisposable
    {
        private readonly IConnection _connection;
        private readonly ILogger<JobEventHandlers> _logger;
        private IChannel? _channel;
        private readonly SemaphoreSlim _channelLock = new(1, 1);
        private bool _disposed;

        public JobEventHandlers(
            IConnection connection,
            ILogger<JobEventHandlers> logger)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleJobCreatedAsync<TJob>(
            TJob job,
            string exchange = "job.events",
            string routingKey = "job.created",
            CancellationToken ct = default) where TJob : class
        {
            if (_disposed) throw new ObjectDisposedException(nameof(JobEventHandlers));

            try
            {
                var channel = await GetChannelAsync(ct);
                var properties = CreateBasicProperties();
                var body = SerializeJobEvent(job);

                await channel.BasicPublishAsync(
                    exchange: exchange,
                    routingKey: routingKey,
                    mandatory: true,
                    basicProperties: properties,
                    body: body,
                    cancellationToken: ct);

                _logger.LogInformation("Published job created event: {JobId}",
                    GetJobId(job));
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Job event publishing was cancelled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish job created event");
                throw new JobEventPublishException("Failed to publish job event", ex);
            }
        }

        private async ValueTask<IChannel> GetChannelAsync(CancellationToken ct)
        {
            if (_channel?.IsOpen == true) return _channel;

            await _channelLock.WaitAsync(ct);
            try
            {
                if (_channel?.IsOpen != true)
                {
                    if (_channel != null)
                        await _channel.DisposeAsync();

                    _channel = await _connection.CreateChannelAsync();
                    await ConfigureChannelAsync(_channel, ct);
                }
                return _channel;
            }
            finally
            {
                _channelLock.Release();
            }
        }

        private async Task ConfigureChannelAsync(IChannel channel, CancellationToken ct)
        {
            await channel.BasicQosAsync(
                prefetchSize: 0,
                prefetchCount: 1,
                global: false,
                cancellationToken: ct);

            channel.ChannelShutdownAsync += async (sender, args) =>
            {
                if (args.Initiator != ShutdownInitiator.Application)
                {
                    _logger.LogWarning("Channel shutdown detected: {ReplyText}", args.ReplyText);
                }
            };
        }

        private static BasicProperties CreateBasicProperties()
        {
            return new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json",
                MessageId = Guid.NewGuid().ToString(),
                Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                Type = "JobCreated"
            };
        }

        private static byte[] SerializeJobEvent<TJob>(TJob job)
        {
            return JsonSerializer.SerializeToUtf8Bytes(new
            {
                EventId = Guid.NewGuid(),
                EventType = "JobCreated",
                Timestamp = DateTime.UtcNow,
                Data = job
            }, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });
        }

        private static string GetJobId<TJob>(TJob job)
        {
            // Implement your job ID access logic
            // Example: return job.Id.ToString();
            return "unknown"; // Replace with actual ID access
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;
            _disposed = true;

            await _channelLock.WaitAsync();
            try
            {
                if (_channel != null)
                    await _channel.DisposeAsync();
            }
            finally
            {
                _channelLock.Release();
                _channelLock.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }

    public class JobEventPublishException : Exception
    {
        public JobEventPublishException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
