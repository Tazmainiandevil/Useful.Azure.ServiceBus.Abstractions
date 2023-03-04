# Useful.Azure.ServiceBus.Abstractions

The purpose of this project is to to provide a wrapper for Azure Service Bus to send messages as JSON and abstract some of the configuration away. This also aids in unit testing by providing interfaces for the Sender and Receiver.

Supported .NET Frameworks - .NET 6 and .NET 7

<a href="https://badge.fury.io/nu/Useful.Azure.ServiceBus.Abstractions"><img src="https://badge.fury.io/nu/Useful.Azure.ServiceBus.Abstractions.svg" alt="NuGet version" height="18"></a>

## Usage

__NOTE__ : The default configuration expects the connection has manage rights to create the queue or topic.

### Topics

#### Basic Topic Sender

```csharp
var factory = new ServiceBusFactory();
var sender = await factory.CreateTopicSenderAsync<MyMessage>(ServiceBusConnectionString, "myTopic").ConfigureAwait(false);

await sender.SendAsJsonAsync(mymessage).ConfigureAwait(false);
```

#### Basic Topic Receiver

```csharp
var factory = new ServiceBusFactory();
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(ServiceBusConnectionString, "myTopic", "mySubscription").ConfigureAwait(false);

// The receive method takes an exception func<> to provide feedback and returns an IObservable to get messages
var observer = receiver.Receive(exception =>
{
    Console.WriteLine(exception.Exception.Message);
    return Task.CompletedTask;
});

observer.Subscribe(x => Console.WriteLine($"From Topic {x}"));
```

#### Basic Topic Receiver With options

```csharp
var factory = new ServiceBusFactory();
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(ServiceBusConnectionString, "myTopic", "mySubscription", new ReceiverOptions {
    ConnectionCanCreateTopicOrQueue = false,
    MaxConcurrentCalls = 10,
    ServiceBusReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
}).ConfigureAwait(false);

// The receive method takes an exception func<> to provide feedback and returns an IObservable to get messages
var observer = receiver.Receive(exception =>
{
    Console.WriteLine(exception.Exception.Message);
    return Task.CompletedTask;
});

observer.Subscribe(x => Console.WriteLine($"From Topic {x}"));
```

