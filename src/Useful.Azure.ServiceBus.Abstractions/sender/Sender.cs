namespace Useful.Azure.ServiceBus.Abstractions.sender;

public class Sender<T> : ISender<T> where T : class
{
    private ServiceBusSender _client;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="client">The sender client</param>
    internal Sender(ServiceBusSender client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data, CancellationToken cancellationToken = default) =>
        SendAsJsonAsync(data, TimeSpan.Zero, DateTime.MinValue, cancellationToken);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default) =>
        SendAsJsonAsync(data, TimeSpan.Zero, scheduledEnqueueTimeUtc, cancellationToken);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data, TimeSpan timeToLive, CancellationToken cancellationToken = default) =>
        SendAsJsonAsync(data, timeToLive, DateTime.MinValue, cancellationToken);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default)
    {
        var message = CreateMessage(data, timeToLive, scheduledEnqueueTimeUtc);

        return _client.SendMessageAsync(message, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList, CancellationToken cancellationToken = default) => SendAsJsonAsync(dataList, TimeSpan.Zero, DateTime.MinValue, cancellationToken);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default)
    => SendAsJsonAsync(dataList, TimeSpan.Zero, scheduledEnqueueTimeUtc, cancellationToken);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive, CancellationToken cancellationToken = default)
    => SendAsJsonAsync(dataList, timeToLive, DateTime.MinValue, cancellationToken);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default)
    {
        var messages = (from message in dataList
                        select CreateMessage(message, timeToLive, scheduledEnqueueTimeUtc));

        return _client.SendMessagesAsync(messages, cancellationToken);
    }

    /// <summary>
    /// Create a Service Bus message
    /// </summary>
    /// <param name="data">The data to send</param>
    /// <param name="timeToLive">How far in the future should the message expire</param>
    /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be en-queued. Message en-queuing time does not mean that the message will be sent at the same time</param>
    /// <returns></returns>
    private static ServiceBusMessage CreateMessage(T data, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc)
    {
        var dataString = JsonSerializer.Serialize(data);
        var message = new ServiceBusMessage(dataString);

        if (scheduledEnqueueTimeUtc > DateTime.MinValue)
        {
            message.ScheduledEnqueueTime = scheduledEnqueueTimeUtc;
        }

        if (timeToLive > TimeSpan.Zero)
        {
            message.TimeToLive = timeToLive;
        }

        return message;
    }

    /// <summary>
    /// Dispose of resources
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;

        (_client as IDisposable)?.Dispose();
        _client = null;
    }

    /// <summary>
    /// Dispose of resources asynchronously
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_client is not null)
        {
            await _client.DisposeAsync().ConfigureAwait(false);
        }

        _client = null;
    }
}