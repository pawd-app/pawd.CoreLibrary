
public interface IRabbitMqMessagePublisher
{
    ValueTask DisposeAsync();
    Task PublishAsync<TMessage>(string exchange, string routingKey, TMessage message, string? messageType = null, IDictionary<string, object>? headers = null, CancellationToken ct = default);
}