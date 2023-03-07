namespace Useful.Azure.ServiceBus.Abstractions;

public record ReceiverOptions
{
    public bool ConnectionCanCreateTopicOrQueue { get; set; } = false;
    public int MaxConcurrentCalls { get; set; } = 10;
    public ServiceBusReceiveMode ReceiveMode { get; set; } = ServiceBusReceiveMode.PeekLock;
    public ServiceBusTransportType ServiceBusTransportType { get; set; } = ServiceBusTransportType.AmqpTcp;
    public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(0.8);
    public TimeSpan MaxDelay { get; set; } = TimeSpan.FromMinutes(1);
    public int MaxRetries { get; set; } = 3;
    public ServiceBusRetryMode Mode { get; set; } = ServiceBusRetryMode.Exponential;
}