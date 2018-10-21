using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Useful.Azure.ServiceBus.Abstractions.receiver
{
    public class Receiver<T> : IReceiver<T> where T : class
    {
        private readonly IReceiverClient _client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">The receiver client</param>
        public Receiver(IReceiverClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Receive messages
        /// </summary>
        /// <param name="exceptionHandler">The exception handler for exposing any exceptions that happen during receive</param>
        /// <param name="maxConcurrentCalls">Gets or sets the maximum number of concurrent calls to the callback the message pump should initiate. Default is 1</param>
        /// <returns>An observable of type T</returns>
        public IObservable<T> Receive(Func<ExceptionReceivedEventArgs, Task> exceptionHandler, int maxConcurrentCalls = 1)
        {
            var ob = Observable.Create<T>(o =>
            {
                _client.RegisterMessageHandler(async (message, token) =>
                {
                    var data = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
                    o.OnNext(data);

                    await _client.CompleteAsync(message.SystemProperties.LockToken).ConfigureAwait(false);
                }, new MessageHandlerOptions(exceptionHandler) { AutoComplete = false, MaxConcurrentCalls = maxConcurrentCalls });

                return Disposable.Empty;
            });

            return ob;
        }        
    }
}