# Useful.Azure.ServiceBus.Abstractions

The purpose of this project is to to provide a wrapper for Azure Service Bus to send messages as JSON and abstract some of the configuration away. This also aids in unit testing by providing interfaces for the Sender and Receiver.

Supported .NET Frameworks - .NET 6 and .NET 7

<a href="https://badge.fury.io/nu/Useful.Azure.ServiceBus.Abstractions"><img src="https://badge.fury.io/nu/Useful.Azure.ServiceBus.Abstractions.svg" alt="NuGet version" height="18"></a>

## Configuration

Add the packages for your app

__NOTE__: Azure.Identity and Microsoft.Extensions.Azure are used for Managed Identity connections

```PowerShell
dotnet add package Azure.Identity
dotnet add package Microsoft.Extensions.Azure
dotnet add package Useful.Azure.ServiceBus.Abstractions
```

## Basic Console App Usage


### Sending

Create a message structure e.g.

```csharp
public record MyMessage
{
    public string Name { get; init; }
}
```

Create an instance of the ServiceBusFactory e.g.

```csharp
var factory = new ServiceBusFactory();
```

Create a Sender with a connection string for the Service Bus e.g.

```csharp
var sender = await factory.CreateSenderAsync<MyMessage>(ServiceBusConnectionString, "myTopic");
```

or using Managed Identity

```csharp
const string fullyQualifiedNamespace = "<your namespace>.servicebus.windows.net";
var sender = await factory.CreateSenderAsync<MyMessage>(fullyQualifiedNamespace, new DefaultAzureCredential(), "myTopic");
```

Then use the SendAsJsonAsync method to send to the Service Bus

```csharp
await sender.SendAsJsonAsync(new MyMessage { Name = "Bilbo Baggins" });
```

When creating the Sender there is also a number of options that can be configured e.g.

```csharp
var sender = await factory.CreateSenderAsync<MyMessage>(fullyQualifiedNamespace, new DefaultAzureCredential(), "myTopic", new SenderOptions { ConnectionCanCreateTopicOrQueue = true } );
```

The defaults for the SenderOptions are:

```csharp
public record SenderOptions
{
    public bool ConnectionCanCreateTopicOrQueue { get; set; } = false;
    public ServiceBusTransportType ServiceBusTransportType { get; set; } = ServiceBusTransportType.AmqpTcp;
    public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(0.8);
    public TimeSpan MaxDelay { get; set; } = TimeSpan.FromMinutes(1);
    public int MaxRetries { get; set; } = 3;
    public ServiceBusRetryMode Mode { get; set; } = ServiceBusRetryMode.Exponential;
}
```

### Receiving

Create a Receiver with a connection string for the Service Bus e.g.

```csharp
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(ServiceBusConnectionString, "myTopic", "mySub");
```

or using Managed Identity

```csharp
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(fullyQualifiedNamespace, new DefaultAzureCredential(), "myTopic", "mySub");
```

Then to listen for incoming messages e.g.

__NOTE__ : The receive method takes an exception func<> to provide feedback and returns an IObservable to get messages

```csharp
var observer = receiver.Receive(args =>
{
    Console.WriteLine(args.Exception.Message);
    return Task.CompletedTask;
});
```

And finally subscribe to receive the messages e.g.

```csharp
observer.Subscribe(x => Console.WriteLine($"From Topic {x}"));
```

__TIP__: Running in a console app you'll need to keep it open, so add a ReadKey at the bottom

```csharp
Console.ReadKey();
```

When creating the Receiver there is also a number of options that can be configured e.g.

```csharp
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(fullyQualifiedNamespace, new DefaultAzureCredential(), "myTopic", "mySub", new ReceiverOptions { ConnectionCanCreateTopicOrQueue = true });
```

The defaults for the ReceiverOptions are:

```csharp
public record ReceiverOptions
{
    public bool ConnectionCanCreateTopicOrQueue { get; set; } = false;
    public int MaxConcurrentCalls { get; set; } = 10;
    public ServiceBusReceiveMode ReceiveMode { get; set; } = ServiceBusReceiveMode.PeekLock;
}
```

## Basic Injection Usage

Inject the Factory in program.cs e.g.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IServiceBusFactory, ServiceBusFactory>();

```

Example

```csharp
await using var sender = await _serviceBusFactory.CreateSenderAsync<MyMessage>(SendConnectionString, "myTopic");
await sender.SendAsJsonAsync(new MyMessage { Name = "Bilbo Baggins" });
```

or a Sender e.g.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var factory = new ServiceBusFactory();
var sender = await factory.CreateSenderAsync<MyMessage>(builder.Configuration["ServiceBusSendConnectionString"], "myTopic");
builder.Services.AddSingleton(sender);
```

Example

```csharp
await _sender.SendAsJsonAsync(new MyMessage { Name = "Bilbo Baggins" });
```

or a Receiver

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var factory = new ServiceBusFactory();
var receiver = await factory.CreateTopicReceiverAsync<MyMessage>(builder.Configuration["ServiceBusReceiveConnectionString"], "myTopic", "mySub");
builder.Services.AddSingleton(receiver);
```

Example

```csharp
var observer = receiver.Receive(args =>
{
    Console.WriteLine(args.Exception.Message);
    return Task.CompletedTask;
});

observer.Subscribe(x => Console.WriteLine($"From Topic {x.Name}"));
```

Example with Methods

```csharp
var observer = receiver.Receive(ExceptionHandler);
observer.Subscribe(IncomingMessage);

private void IncomingMessage(MyMessage message)
{
    Console.WriteLine($"From Topic {message.Name}")
}

private Task ExceptionHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.Message);
    return Task.CompletedTask;
}
```