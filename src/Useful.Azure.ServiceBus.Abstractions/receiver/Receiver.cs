using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Useful.Azure.ServiceBus.Abstractions.receiver
{
    public class Receiver<T> : IReceiver<T> where T : class
    {
        private ServiceBusProcessor _client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">The receiver client</param>
        internal Receiver(ServiceBusProcessor client)
        {
            _client = client;
        }

        /// <inheritdoc/>
        public IObservable<T> Receive(Func<ProcessErrorEventArgs, Task> exceptionHandler, CancellationToken cancellationToken = default)
        {
            var ob = Observable.Create<T>(async o =>
            {
                _client.ProcessMessageAsync += async args =>
                {
                    var data = JsonSerializer.Deserialize<T>(args.Message.Body.ToString());
                    o.OnNext(data);
                    await args.CompleteMessageAsync(args.Message, cancellationToken).ConfigureAwait(false);
                };

                _client.ProcessErrorAsync += exceptionHandler;

                await _client.StartProcessingAsync(cancellationToken).ConfigureAwait(false);

                return Disposable.Empty;
            });

            return ob;
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
}