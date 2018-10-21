# Useful.Azure.ServiceBus.Abstractions

Azure Service Bus Wrapper in C# to help manage and use the Service Bus for sending messages as JSON. Uses the .NET Standard 2.0 version of the service bus libraries.

<a href="https://badge.fury.io/nu/Useful.Azure.ServiceBus.Abstractions"><img src="https://badge.fury.io/nu/Useful.Azure.ServiceBus.Abstractions.svg" alt="NuGet version" height="18"></a>

## Usage

To create a topic/queue sender or receiver, create an instance of the factory and then create a sender or receiver from that.

Each of the factory methods support creation of the queue or topic as well as transport type, receive mode and retry policy.

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

#### AMPQ Websocket Topic Sender

```csharp
var builder = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);
builder.EntityPath = "mytopic";
builder.TransportType = TransportType.AmqpWebSockets;

var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(builder.SasKeyName, builder.SasKey);

var factory = new ServiceBusFactory();
var sender = await factory.CreateTopicSenderAsync<MyMessage>(builder, tokenProvider).ConfigureAwait(false);

await sender.SendAsJsonAsync(mymessage).ConfigureAwait(false);
```

#### AMPQ Websocket Topic Receiver

```csharp
var builder = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);
builder.EntityPath = "mytopic";
builder.TransportType = TransportType.AmqpWebSockets;

var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(builder.SasKeyName, builder.SasKey);

var factory = new ServiceBusFactory();
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(builder, tokenProvider, "mySubscription").ConfigureAwait(false);

// The receive method takes an exception func<> to provide feedback and returns an IObservable to get messages
var observer = receiver.Receive(exception =>
{
    Console.WriteLine(exception.Exception.Message);
    return Task.CompletedTask;
});

observer.Subscribe(x => Console.WriteLine($"From Topic {x}"));
```

### Queues

#### Basic Queue Sender

```csharp
var factory = new ServiceBusFactory();
var sender = await factory.CreateQueueSenderAsync<string>(ServiceBusConnectionString, "myQueue").ConfigureAwait(false);

await sender.SendAsJsonAsync(mymessage).ConfigureAwait(false);
```

#### Basic Queue Receiver

```csharp
var factory = new ServiceBusFactory();
var receiver = await factory.CreateQueueReceiverAsync<string>(ServiceBusConnectionString, "myQueue").ConfigureAwait(false);

// The receive method takes an exception func<> to provide feedback and returns an IObservable to get messages
var observer = receiver.Receive(exception =>
{
    Console.WriteLine(exception.Exception.Message);
    return Task.CompletedTask;
});

observer.Subscribe(x => Console.WriteLine($"From Queue {x}"));
```

### Sender

The sender has a few methods to send the messages as JSON and supports:

- Scheduled Enqueue Time UTC
- Time To Live
- Sending lists of messages

#### Sending with Time to Live

```csharp
var factory = new ServiceBusFactory();
var sender = await factory.CreateTopicSenderAsync<MyMessage>(ServiceBusConnectionString, "myTopic").ConfigureAwait(false);

await sender.SendAsJsonAsync(mymessage, TimeSpan.FromMinutes(10)).ConfigureAwait(false);
```

#### Sending with Scheduled Enqueue Time UTC

```csharp
var factory = new ServiceBusFactory();
var sender = await factory.CreateTopicSenderAsync<MyMessage>(ServiceBusConnectionString, "myTopic").ConfigureAwait(false);

var enqueue = DateTime.UtcNow + TimeSpan.FromDays(1);
await sender.SendAsJsonAsync(mymessage, enqueue).ConfigureAwait(false);
```

### Receiver

The receiver has a single receive method that takes an exception handler func and can override the maxConcurrentCalls (default is 1).

```csharp
var factory = new ServiceBusFactory();
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(ServiceBusConnectionString, "myTopic", "mySubscription").ConfigureAwait(false);

var maxConcurrentCalls = 10;
// The receive method takes an exception func<> to provide feedback and returns an IObservable to get messages
var observer = receiver.Receive(exception =>
{
    Console.WriteLine(exception.Exception.Message);
    return Task.CompletedTask;
}, maxConcurrentCalls);

observer.Subscribe(x => Console.WriteLine($"From Topic {x}"));
```