using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful.Azure.ServiceBus.Abstractions.sender
{
    public class Sender<T> : ISender<T> where T : class
    {
        private readonly ISenderClient _client;
        protected internal string JsonContentType;
        protected internal string DataNullExceptionMessage;
        protected internal string DataListNullOrEmptyExceptionMessage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">The sender client</param>
        public Sender(ISenderClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        public async Task SendAsJsonAsync(T data)
        {
            await SendAsJsonAsync(data, TimeSpan.Zero, DateTime.MinValue).ConfigureAwait(false);
        }

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued. Message enqueuing time does not mean that the message will be sent at the same time</param>
        public async Task SendAsJsonAsync(T data, DateTime scheduledEnqueueTimeUtc)
        {
            await SendAsJsonAsync(data, TimeSpan.Zero, scheduledEnqueueTimeUtc).ConfigureAwait(false);
        }

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="timeToLive">How far in the future should the message expire</param>
        public async Task SendAsJsonAsync(T data, TimeSpan timeToLive)
        {
            await SendAsJsonAsync(data, timeToLive, DateTime.MinValue).ConfigureAwait(false);
        }

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="timeToLive">How far in the future should the message expire</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued. Message enqueuing time does not mean that the message will be sent at the same time</param>
        public async Task SendAsJsonAsync(T data, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc)
        {
            var message = CreateMessage(data, timeToLive, scheduledEnqueueTimeUtc);

            await _client.SendAsync(message).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        public async Task SendAsJsonAsync(IList<T> dataList)
        {
            await SendAsJsonAsync(dataList, TimeSpan.Zero, DateTime.MinValue).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued</param>
        public async Task SendAsJsonAsync(IList<T> dataList, DateTime scheduledEnqueueTimeUtc)
        {
            await SendAsJsonAsync(dataList, TimeSpan.Zero, scheduledEnqueueTimeUtc).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        /// <param name="timeToLive">How far in the future should the message expire</param>
        public async Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive)
        {
            await SendAsJsonAsync(dataList, timeToLive, DateTime.MinValue).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        /// <param name="timeToLive">How far in the future should the message expire</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued. Message enqueuing time does not mean that the message will be sent at the same time</param>
        public async Task SendAsJsonAsync(IList<T> dataList, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc)
        {
            if (dataList == null || !dataList.Any())
            {
                throw new ArgumentException(DataListNullOrEmptyExceptionMessage, nameof(dataList));
            }

            var messages = (from message in dataList
                            select CreateMessage(message, timeToLive, scheduledEnqueueTimeUtc))
                            .ToList();

            await _client.SendAsync(messages).ConfigureAwait(false);
        }

        #region Helpers

        private byte[] GetDataAsBytes(T data)
        {
            var json = JsonConvert.SerializeObject(data ?? throw new ArgumentException(DataNullExceptionMessage, nameof(data)));
            var bytes = Encoding.UTF8.GetBytes(json);
            return bytes;
        }

        private Message CreateMessage(T data, TimeSpan timeToLive, DateTime scheduledEnqueueTimeUtc)
        {
            var bytes = GetDataAsBytes(data);
            var message = new Message(bytes) { ContentType = JsonContentType };

            if (timeToLive > TimeSpan.Zero)
            {
                message.TimeToLive = timeToLive;
            }

            if (scheduledEnqueueTimeUtc > DateTime.MinValue)
            {
                message.ScheduledEnqueueTimeUtc = scheduledEnqueueTimeUtc;
            }

            return message;
        }

        #endregion Helpers
    }
}