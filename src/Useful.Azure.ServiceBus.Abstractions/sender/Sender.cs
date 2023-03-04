using System.Collections.Generic;

namespace Useful.Azure.ServiceBus.Abstractions.sender;

public class Sender<T> : IAsyncDisposable, ISender<T> where T : class
{
    private ServiceBusSender _client;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="client">The sender client</param>
    public Sender(ServiceBusSender client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data) => SendAsJsonAsync(data, TimeSpan.Zero, DateTime.MinValue);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data, DateTime scheduledEnqueueTimeUtc) => SendAsJsonAsync(data, TimeSpan.Zero, scheduledEnqueueTimeUtc);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data, TimeSpan timeToLive) => SendAsJsonAsync(data, timeToLive, DateTime.MinValue);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(T data, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc)
    {
        var message = CreateMessage(data, timeToLive, scheduledEnqueueTimeUtc);

        return _client.SendMessageAsync(message);
    }

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList) => SendAsJsonAsync(dataList, TimeSpan.Zero, DateTime.MinValue);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList, DateTime scheduledEnqueueTimeUtc)
    => SendAsJsonAsync(dataList, TimeSpan.Zero, scheduledEnqueueTimeUtc);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive)
    => SendAsJsonAsync(dataList, timeToLive, DateTime.MinValue);

    /// <inheritdoc/>
    public Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc)
    {
        var messages = (from message in dataList
                        select CreateMessage(message, timeToLive, scheduledEnqueueTimeUtc));

        return _client.SendMessagesAsync(messages);
    }

    private static ServiceBusMessage CreateMessage(T data, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc)
    {
        var dataString = JsonSerializer.Serialize(data);
        var message = new ServiceBusMessage(dataString)
        {
            ScheduledEnqueueTime = scheduledEnqueueTimeUtc,
            TimeToLive = timeToLive
        };

        return message;
    }

    public async ValueTask DisposeAsync()
    {
        if (_client is not null)
        {
            await _client.DisposeAsync().ConfigureAwait(false);
        }

        _client = null;
    }
}