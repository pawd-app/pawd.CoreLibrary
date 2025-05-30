using Microsoft.Extensions.Logging;
using pawd.CoreLibrary.JobRabbitMqBridge.Exceptions;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMqMessagePublisher : IRabbitMqMessagePublisher, IAsyncDisposable
{
    private readonly IConnection _connection;
    private IChannel? _channel;
    private readonly ILogger<RabbitMqMessagePublisher> _logger;
    private readonly SemaphoreSlim _channelLock = new(1, 1);
    private bool _disposed;

    public RabbitMqMessagePublisher(
        IConnection connection,
        ILogger<RabbitMqMessagePublisher> logger)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private async ValueTask<IChannel> GetChannelAsync()
    {
        if (_channel?.IsOpen == true)
            return _channel;

        await _channelLock.WaitAsync();
        try
        {
            if (_channel?.IsOpen != true)
            {
                if (_channel != null)
                {
                    await _channel.DisposeAsync();
                }

                _channel = await _connection.CreateChannelAsync();
                await ConfigureChannelAsync(_channel);
            }
            return _channel;
        }
        finally
        {
            _channelLock.Release();
        }
    }

    private async ValueTask ConfigureChannelAsync(IChannel channel)
    {
        channel.ChannelShutdownAsync += async (sender, args) =>
        {
            if (args.Initiator != ShutdownInitiator.Application)
            {
                _logger.LogWarning("Channel shutdown: {ReplyText}", args.ReplyText);
                await TryRecoverChannelAsync();
            }
        };

        await channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false);
    }

    private async Task TryRecoverChannelAsync()
    {
        try
        {
            await _channelLock.WaitAsync();
            if (_channel?.IsOpen == false)
            {
                await _channel.DisposeAsync();
                _channel = await _connection.CreateChannelAsync();
                await ConfigureChannelAsync(_channel);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to recover channel");
        }
        finally
        {
            _channelLock.Release();
        }
    }

    public async Task PublishAsync<TMessage>(
        string exchange,
        string routingKey,
        TMessage message,
        string? messageType = null,
        IDictionary<string, object>? headers = null,
        CancellationToken ct = default)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(RabbitMqMessagePublisher));

        var channel = await GetChannelAsync();

        try
        {
            var properties = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json",
                Type = messageType ?? typeof(TMessage).Name,
                MessageId = Guid.NewGuid().ToString(),
                Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                Headers = headers
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: properties,
                body: body,
                cancellationToken: ct);

            _logger.LogDebug("Published {MessageType} to {Exchange}/{RoutingKey}",
                properties.Type, exchange, routingKey);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Failed to publish to {Exchange}/{RoutingKey}", exchange, routingKey);
            throw new MessagePublishException("Failed to publish message", ex);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        try
        {
            await _channelLock.WaitAsync();
            if (_channel != null)
            {
                await _channel.DisposeAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during disposal");
        }
        finally
        {
            _channelLock.Release();
            _channelLock.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}