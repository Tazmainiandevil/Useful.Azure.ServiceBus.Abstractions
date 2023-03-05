namespace Useful.Azure.ServiceBus.Abstractions;

public record ReceiverOptions
{
    public bool ConnectionCanCreateTopicOrQueue { get; set; } = false;
    public int MaxConcurrentCalls { get; set; } = 1;
    public ServiceBusReceiveMode ReceiveMode { get; set; } = ServiceBusReceiveMode.PeekLock;
}