namespace Useful.Azure.ServiceBus.Abstractions.receiver
{
    public interface IReceiver<out T>
    {
        /// <summary>
        /// Receive messages
        /// </summary>
        /// <typeparam name="T">The class structure of the expected response</typeparam>
        /// <param name="exceptionHandler">The exception handler for exposing any exceptions that happen during receive</param>
        /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
        /// <returns>An observable of type T</returns>
        IObservable<T> Receive(Func<ProcessErrorEventArgs, Task> exceptionHandler, CancellationToken cancellationToken = default);
    }
}