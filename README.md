# Useful.Azure.ServiceBus.Abstractions

Azure Service Bus Wrapper in C# to help manage and use the Service Bus for sending messages as JSON. Uses the .NET Standard version of the service bus libraries and supporting .NETStandard 2.0

## Basic Usage

Create an instance of the factory and then create a sender or receiver from there.

```csharp
var factory = new ServiceBusFactory();
var sender = await factory.CreateTopicSenderAsync<MyMessage>(ServiceBusConnectionString, "myTopic").ConfigureAwait(false);

await sender.SendAsJsonAsync(mymessage).ConfigureAwait(false);
```

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
