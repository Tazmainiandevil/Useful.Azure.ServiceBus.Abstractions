using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Useful.Azure.ServiceBus.Abstractions.receiver
{
    public class Receiver<T> : IAsyncDisposable, IReceiver<T> where T : class
    {
        private ServiceBusProcessor _client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">The receiver client</param>
        public Receiver(ServiceBusProcessor client)
        {
            _client = client;
        }

        /// <inheritdoc/>
        public IObservable<T> Receive(Func<ProcessErrorEventArgs, Task> exceptionHandler)
        {
            var ob = Observable.Create<T>(async (o, cancellationToken) =>
            {
                _client.ProcessMessageAsync += async args =>
                {
                    var data = JsonSerializer.Deserialize<T>(args.Message.Body.ToString());
                    o.OnNext(data);
                    await args.CompleteMessageAsync(args.Message, cancellationToken);
                };

                _client.ProcessErrorAsync += exceptionHandler;

                await _client.StartProcessingAsync(cancellationToken);

                return Disposable.Empty;
            });

            return ob;
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
}