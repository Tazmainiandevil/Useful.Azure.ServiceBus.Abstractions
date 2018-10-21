using Microsoft.Azure.ServiceBus;
using System;
using System.Threading.Tasks;

namespace Useful.Azure.ServiceBus.Abstractions.receiver
{
    public interface IReceiver<out T>
    {
        /// <summary>
        /// Receive messages
        /// </summary>
        /// <param name="exceptionHandler">The exception handler for exposing any exceptions that happen during receive</param>
        /// <param name="maxConcurrentCalls">Gets or sets the maximum number of concurrent calls to the callback the message pump should initiate. Default is 1</param>
        /// <returns>An observable of type T</returns>
        IObservable<T> Receive(Func<ExceptionReceivedEventArgs, Task> exceptionHandler, int maxConcurrentCalls = 1);
    }
}