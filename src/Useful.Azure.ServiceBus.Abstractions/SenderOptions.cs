namespace Useful.Azure.ServiceBus.Abstractions;
public record SenderOptions
{
    public bool ConnectionCanCreateTopicOrQueue { get; set; } = false;

    public ServiceBusTransportType ServiceBusTransportType { get; set; } = ServiceBusTransportType.AmqpTcp;
    public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(0.8);
    public TimeSpan MaxDelay { get; set; } = TimeSpan.FromMinutes(1);
    public int MaxRetries { get; set; } = 10;
    public ServiceBusRetryMode Mode { get; set; } = ServiceBusRetryMode.Exponential;
}