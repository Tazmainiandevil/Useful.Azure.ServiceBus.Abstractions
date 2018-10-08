using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Useful.Azure.ServiceBus.Abstractions.sender
{
    public class Sender<T> : ISender<T> where T : class
    {
        private readonly ISenderClient _client;

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
            var json = JsonConvert.SerializeObject(data ?? throw new ArgumentException("data to send cannot be null", nameof(data)));
            var bytes = Encoding.UTF8.GetBytes(json);

            var message = new Message(bytes)
            {
                ContentType = "application/json"
            };

            await _client.SendAsync(message).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        public async Task SendAsJsonAsync(IList<T> dataList)
        {
            if (dataList == null || !dataList.Any())
            {
                throw new ArgumentException("The data list to send cannot be null or empty", nameof(dataList));
            }

            var messages = (from message in dataList
                            select JsonConvert.SerializeObject(message) into json
                            select Encoding.UTF8.GetBytes(json) into bytes
                            select new Message(bytes) { ContentType = "application/json" }).ToList();

            await _client.SendAsync(messages).ConfigureAwait(false);
        }
    }
}