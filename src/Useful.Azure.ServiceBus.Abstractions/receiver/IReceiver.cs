﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Useful.Azure.ServiceBus.Abstractions.receiver
{
    public interface IReceiver<out T>
    {
        /// <summary>
        /// Receive messages
        /// </summary>
        /// <param name="exceptionHandler">The exception handler for exposing any exceptions that happen during receive</param>
        /// <returns>An observable of type T</returns>
        IObservable<T> Receive(Func<ExceptionReceivedEventArgs, Task> exceptionHandler);        
    }
}