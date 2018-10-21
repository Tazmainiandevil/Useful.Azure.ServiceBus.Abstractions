using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Useful.Azure.ServiceBus.Abstractions.sender
{
    public interface ISender<T>
    {
        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        Task SendAsJsonAsync(T data);

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued. Message enqueuing time does not mean that the message will be sent at the same time</param>
        Task SendAsJsonAsync(T data, DateTime scheduledEnqueueTimeUtc);

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="scheduleIn">How far in the future to enqueue the message</param>
        Task SendAsJsonAsync(T data, TimeSpan scheduleIn);

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        Task SendAsJsonAsync(IList<T> dataList);

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued. Message enqueuing time does not mean that the message will be sent at the same time</param>
        Task SendAsJsonAsync(IList<T> dataList, DateTime scheduledEnqueueTimeUtc);

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        /// <param name="scheduleIn">How far in the future to enqueue the messages</param>
        Task SendAsJsonAsync(IList<T> dataList, TimeSpan scheduleIn);
    }
}