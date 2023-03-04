using Azure;
using Azure.Core;
using Azure.Messaging.ServiceBus.Administration;
using Useful.Azure.ServiceBus.Abstractions.receiver;
using Useful.Azure.ServiceBus.Abstractions.sender;

namespace Useful.Azure.ServiceBus.Abstractions.factory;

public class ServiceBusFactory : IServiceBusFactory
{
    #region Receiver ConnectionString

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName) where T : class
        => CreateTopicReceiverAsync<T>(connectionString, topicName, subscriptionName, new ReceiverOptions());

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName,
        ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        ArgumentNullException.ThrowIfNull(topicName);
        ArgumentNullException.ThrowIfNull(subscriptionName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(connectionString);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(connectionString) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions);
    }

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName) where T : class
        => CreateQueueReceiverAsync<T>(connectionString, queueName, new ReceiverOptions());

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(connectionString);
        ArgumentNullException.ThrowIfNull(queueName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(connectionString);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(connectionString) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions);
    }

    #endregion Receiver ConnectionString

    #region Receiver AzureNamedKeyCredential

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName,
        string subscriptionName) where T : class =>
        CreateTopicReceiverAsync<T>(fullyQualifiedNamespace, credential, topicName, subscriptionName, new ReceiverOptions());

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName,
        string subscriptionName, ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace);
        ArgumentNullException.ThrowIfNull(credential);
        ArgumentNullException.ThrowIfNull(topicName);
        ArgumentNullException.ThrowIfNull(subscriptionName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions);
    }

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName) where T : class
        => CreateQueueReceiverAsync<T>(fullyQualifiedNamespace, credential, queueName, new ReceiverOptions());

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName, ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace);
        ArgumentNullException.ThrowIfNull(credential);
        ArgumentNullException.ThrowIfNull(queueName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions);
    }

    #endregion Receiver AzureNamedKeyCredential

    #region Receiver TokenCredential

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName,
        string subscriptionName) where T : class =>
        CreateTopicReceiverAsync<T>(fullyQualifiedNamespace, credential, topicName, subscriptionName, new ReceiverOptions());

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName,
        string subscriptionName, ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace);
        ArgumentNullException.ThrowIfNull(credential);
        ArgumentNullException.ThrowIfNull(topicName);
        ArgumentNullException.ThrowIfNull(subscriptionName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions);
    }

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName) where T : class
    => CreateQueueReceiverAsync<T>(fullyQualifiedNamespace, credential, queueName, new ReceiverOptions());

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName, ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace);
        ArgumentNullException.ThrowIfNull(credential);
        ArgumentNullException.ThrowIfNull(queueName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions);
    }

    #endregion Receiver TokenCredential

    #region Receiver AzureSasCredential

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName,
        string subscriptionName) where T : class
    {
        return CreateTopicReceiverAsync<T>(fullyQualifiedNamespace, credential, topicName, subscriptionName, new ReceiverOptions());
    }

    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName,
        string subscriptionName, ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace);
        ArgumentNullException.ThrowIfNull(credential);
        ArgumentNullException.ThrowIfNull(topicName);
        ArgumentNullException.ThrowIfNull(subscriptionName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions);
    }

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName) where T : class
        => CreateQueueReceiverAsync<T>(fullyQualifiedNamespace, credential, queueName, new ReceiverOptions());

    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName, ReceiverOptions receiverOptions) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace);
        ArgumentNullException.ThrowIfNull(credential);
        ArgumentNullException.ThrowIfNull(queueName);

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions);
    }

    #endregion Receiver AzureSasCredential

    #region Sender ConnectionString

    public ISender<T> CreateTopicSenderAsync<T>(string connectionString, string topicName) where T : class
    {
        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender(topicName);

        return new Sender<T>(sender);
    }

    #endregion Sender ConnectionString

    #region Sender AzureNamedKeyCredential

    public ISender<T> CreateTopicSenderAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName) where T : class
    {
        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var sender = client.CreateSender(topicName);

        return new Sender<T>(sender);
    }

    #endregion Sender AzureNamedKeyCredential

    #region Sender TokenCredential

    public ISender<T> CreateTopicSenderAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName) where T : class
    {
        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var sender = client.CreateSender(topicName);

        return new Sender<T>(sender);
    }

    #endregion Sender TokenCredential

    #region Sender AzureSasCredential

    public ISender<T> CreateTopicSenderAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName) where T : class
    {
        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var sender = client.CreateSender(topicName);

        return new Sender<T>(sender);
    }

    #endregion Sender AzureSasCredential

    #region Create Receivers

    private static async Task<IReceiver<T>> CreateTopicReceiverAsync<T>(ServiceBusAdministrationClient adminClient, ServiceBusClient client, string topicName, string subscriptionName, ReceiverOptions receiverOptions) where T : class
    {
        await ConfigureTopicAsync(adminClient, topicName, subscriptionName);
        var receiver = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = receiverOptions.MaxConcurrentCalls,
            ReceiveMode = receiverOptions.ReceiveMode
        });

        return new Receiver<T>(receiver);
    }

    private static async Task<IReceiver<T>> CreateQueueReceiverAsync<T>(ServiceBusAdministrationClient adminClient, ServiceBusClient client, string queueName, ReceiverOptions receiverOptions) where T : class
    {
        await ConfigureQueueAsync(adminClient, queueName);
        var receiver = client.CreateProcessor(queueName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = receiverOptions.MaxConcurrentCalls,
            ReceiveMode = receiverOptions.ReceiveMode
        });

        return new Receiver<T>(receiver);
    }

    #endregion Create Receivers

    #region Configure Topic/Queues

    private static async Task ConfigureTopicAsync(ServiceBusAdministrationClient adminClient, string topicName, string subscriptionName)
    {
        if (adminClient == null)
        {
            return;
        }

        var options = new CreateTopicOptions(topicName)
        {
            EnableBatchedOperations = true,
            EnablePartitioning = true
        };

        if (!await adminClient.TopicExistsAsync(topicName).ConfigureAwait(false))
        {
            await adminClient.CreateTopicAsync(options);
        }

        if (!await adminClient.SubscriptionExistsAsync(topicName, subscriptionName))
        {
            await adminClient.CreateSubscriptionAsync(topicName, subscriptionName);
        }
    }

    private static async Task ConfigureQueueAsync(ServiceBusAdministrationClient adminClient, string queueName)
    {
        if (adminClient == null)
        {
            return;
        }

        var options = new CreateQueueOptions(queueName)
        {
            EnableBatchedOperations = true,
            EnablePartitioning = true
        };

        if (!await adminClient.QueueExistsAsync(queueName).ConfigureAwait(false))
        {
            await adminClient.CreateQueueAsync(options);
        }
    }

    #endregion Configure Topic/Queues
}