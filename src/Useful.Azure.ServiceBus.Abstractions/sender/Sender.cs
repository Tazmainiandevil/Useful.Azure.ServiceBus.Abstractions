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
            var bytes = GetDataAsBytes(data);
            
            var message = new Message(bytes)
            {
                ContentType = JsonContentType
            };

            await _client.SendAsync(message).ConfigureAwait(false);
        }

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued. Message enqueuing time does not mean that the message will be sent at the same time</param>
        public async Task SendAsJsonAsync(T data, DateTime scheduledEnqueueTimeUtc)
        {
            var bytes = GetDataAsBytes(data);

            var message = new Message(bytes)
            {
                ContentType = JsonContentType,
                ScheduledEnqueueTimeUtc = scheduledEnqueueTimeUtc
            };

            await _client.SendAsync(message).ConfigureAwait(false);
        }

        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        /// <param name="scheduleIn">How far in the future to enqueue the message</param>
        public async Task SendAsJsonAsync(T data, TimeSpan scheduleIn)
        {
            if (scheduleIn > TimeSpan.Zero)
            {
                var scheduledEnqueueTimeUtc = DateTime.UtcNow + scheduleIn;
                await SendAsJsonAsync(data, scheduledEnqueueTimeUtc).ConfigureAwait(false);
            }
            else
            {
                await SendAsJsonAsync(data).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        public async Task SendAsJsonAsync(IList<T> dataList)
        {
            if (dataList == null || !dataList.Any())
            {
                DataListNullOrEmptyExceptionMessage = "The data list to send cannot be null or empty";
                throw new ArgumentException(DataListNullOrEmptyExceptionMessage, nameof(dataList));
            }

            var messages = (from message in dataList
                            select GetDataAsBytes(message) into bytes
                            select new Message(bytes) { ContentType = JsonContentType }).ToList();

            await _client.SendAsync(messages).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        /// <param name="scheduledEnqueueTimeUtc">Gets or sets the date and time in UTC at which the message will be enqueued</param>
        public async Task SendAsJsonAsync(IList<T> dataList, DateTime scheduledEnqueueTimeUtc)
        {
            if (dataList == null || !dataList.Any())
            {
                throw new ArgumentException(DataListNullOrEmptyExceptionMessage, nameof(dataList));
            }

            var messages = (from message in dataList
                            select GetDataAsBytes(message) into bytes
                            select new Message(bytes) { ContentType = JsonContentType, ScheduledEnqueueTimeUtc = scheduledEnqueueTimeUtc })
                            .ToList();

            await _client.SendAsync(messages).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        /// <param name="scheduleIn">How far in the future to enqueue the messages</param>
        public async Task SendAsJsonAsync(IList<T> dataList, TimeSpan scheduleIn)
        {
            if (scheduleIn > TimeSpan.Zero)
            {
                var scheduledEnqueueTimeUtc = DateTime.UtcNow + scheduleIn;
                await SendAsJsonAsync(dataList, scheduledEnqueueTimeUtc).ConfigureAwait(false);
            }
            else
            {
                await SendAsJsonAsync(dataList).ConfigureAwait(false);
            }
        }        

        private byte[] GetDataAsBytes(T data)
        {
            var json = JsonConvert.SerializeObject(data ?? throw new ArgumentException(DataNullExceptionMessage, nameof(data)));
            var bytes = Encoding.UTF8.GetBytes(json);
            return bytes;
        }
    }
}