namespace Useful.Azure.ServiceBus.Abstractions.sender;

public interface ISender<T>
{
    /// <summary>
    /// Send message of type T as a json string
    /// </summary>
    /// <param name="data">The data to send</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(T data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send message of type T as a json string
    /// </summary>
    /// <param name="data">The data to send</param>
    /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be en-queued. Message en-queuing time does not mean that the message will be sent at the same time</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(T data, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send message of type T as a json string
    /// </summary>
    /// <param name="data">The data to send</param>
    /// <param name="timeToLive">How far in the future should the message expire</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(T data, TimeSpan timeToLive, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send message of type T as a json string
    /// </summary>
    /// <param name="data">The data to send</param>
    /// <param name="timeToLive">How far in the future should the message expire</param>
    /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be en-queued. Message en-queuing time does not mean that the message will be sent at the same time</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(T data, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send a list of messages of type T as json strings
    /// </summary>
    /// <param name="dataList">The data to send</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(IList<T> dataList, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send a list of messages of type T as json strings
    /// </summary>
    /// <param name="dataList">The data to send</param>
    /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be en-queued. Message en-queuing time does not mean that the message will be sent at the same time</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(IList<T> dataList, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send a list of messages of type T as json strings
    /// </summary>
    /// <param name="dataList">The data to send</param>
    /// <param name="timeToLive">How far in the future should the message expire</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send a list of messages of type T as json strings
    /// </summary>
    /// <param name="dataList">The data to send</param>
    /// <param name="timeToLive">How far in the future should the message expire</param>
    /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be en-queued. Message en-queuing time does not mean that the message will be sent at the same time</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc, CancellationToken cancellationToken = default);
}