using Azure;
using Azure.Core;
using Azure.Messaging.ServiceBus.Administration;
using Useful.Azure.ServiceBus.Abstractions.receiver;
using Useful.Azure.ServiceBus.Abstractions.sender;

namespace Useful.Azure.ServiceBus.Abstractions.factory;

public class ServiceBusFactory : IServiceBusFactory
{
    #region Receiver ConnectionString

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName, CancellationToken cancellationToken = default) where T : class
        => CreateTopicReceiverAsync<T>(connectionString, topicName, subscriptionName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName,
        ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));
        ArgumentNullException.ThrowIfNull(topicName, nameof(topicName));
        ArgumentNullException.ThrowIfNull(subscriptionName, nameof(subscriptionName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(connectionString);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(connectionString) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, CancellationToken cancellationToken = default) where T : class
        => CreateQueueReceiverAsync<T>(connectionString, queueName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));
        ArgumentNullException.ThrowIfNull(queueName, nameof(queueName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(connectionString);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(connectionString) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions, cancellationToken);
    }

    #endregion Receiver ConnectionString

    #region Receiver AzureNamedKeyCredential

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName,
        string subscriptionName, CancellationToken cancellationToken = default) where T : class =>
        CreateTopicReceiverAsync<T>(fullyQualifiedNamespace, credential, topicName, subscriptionName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName,
        string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(topicName, nameof(topicName));
        ArgumentNullException.ThrowIfNull(subscriptionName, nameof(subscriptionName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName, CancellationToken cancellationToken = default) where T : class
        => CreateQueueReceiverAsync<T>(fullyQualifiedNamespace, credential, queueName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(queueName, nameof(queueName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions, cancellationToken);
    }

    #endregion Receiver AzureNamedKeyCredential

    #region Receiver TokenCredential

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName,
        string subscriptionName, CancellationToken cancellationToken = default) where T : class =>
        CreateTopicReceiverAsync<T>(fullyQualifiedNamespace, credential, topicName, subscriptionName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName,
        string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(topicName, nameof(topicName));
        ArgumentNullException.ThrowIfNull(subscriptionName, nameof(subscriptionName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName, CancellationToken cancellationToken = default) where T : class
    => CreateQueueReceiverAsync<T>(fullyQualifiedNamespace, credential, queueName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(queueName, nameof(queueName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions, cancellationToken);
    }

    #endregion Receiver TokenCredential

    #region Receiver AzureSasCredential

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName,
        string subscriptionName, CancellationToken cancellationToken = default) where T : class =>
        CreateTopicReceiverAsync<T>(fullyQualifiedNamespace, credential, topicName, subscriptionName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName,
        string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(topicName, nameof(topicName));
        ArgumentNullException.ThrowIfNull(subscriptionName, nameof(subscriptionName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateTopicReceiverAsync<T>(adminClient, client, topicName, subscriptionName, receiverOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName, CancellationToken cancellationToken = default) where T : class
        => CreateQueueReceiverAsync<T>(fullyQualifiedNamespace, credential, queueName, new ReceiverOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(queueName, nameof(queueName));

        receiverOptions ??= new ReceiverOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential);
        var adminClient = receiverOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueReceiverAsync<T>(adminClient, client, queueName, receiverOptions, cancellationToken);
    }

    #endregion Receiver AzureSasCredential

    #region Sender ConnectionString

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string connectionString, string queueOrTopicName, CancellationToken cancellationToken = default) where T : class =>
        CreateSenderAsync<T>(connectionString, queueOrTopicName, new SenderOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string connectionString, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));
        ArgumentNullException.ThrowIfNull(queueOrTopicName, nameof(queueOrTopicName));

        senderOptions ??= new SenderOptions();

        var client = new ServiceBusClient(connectionString, CreateServiceBusClientOptions(senderOptions));

        var adminClient = senderOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(connectionString) : null;

        return CreateQueueOrTopicSenderAsync<T>(adminClient, client, queueOrTopicName, cancellationToken);
    }

    #endregion Sender ConnectionString

    #region Sender AzureNamedKeyCredential

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential,
        string queueOrTopicName, CancellationToken cancellationToken = default) where T : class =>
        CreateSenderAsync<T>(fullyQualifiedNamespace, credential, queueOrTopicName, new SenderOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(queueOrTopicName, nameof(queueOrTopicName));

        senderOptions ??= new SenderOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential, CreateServiceBusClientOptions(senderOptions));

        var adminClient = senderOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueOrTopicSenderAsync<T>(adminClient, client, queueOrTopicName, cancellationToken);
    }

    #endregion Sender AzureNamedKeyCredential

    #region Sender TokenCredential

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, TokenCredential credential,
        string queueOrTopicName, CancellationToken cancellationToken = default) where T : class =>
        CreateSenderAsync<T>(fullyQualifiedNamespace, credential, queueOrTopicName, new SenderOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(queueOrTopicName, nameof(queueOrTopicName));

        senderOptions ??= new SenderOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential, CreateServiceBusClientOptions(senderOptions));

        var adminClient = senderOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueOrTopicSenderAsync<T>(adminClient, client, queueOrTopicName, cancellationToken);
    }

    #endregion Sender TokenCredential

    #region Sender AzureSasCredential

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential,
        string queueOrTopicName, CancellationToken cancellationToken = default) where T : class =>
        CreateSenderAsync<T>(fullyQualifiedNamespace, credential, queueOrTopicName, new SenderOptions(), cancellationToken);

    /// <inheritdoc/>
    public Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(fullyQualifiedNamespace, nameof(fullyQualifiedNamespace));
        ArgumentNullException.ThrowIfNull(credential, nameof(credential));
        ArgumentNullException.ThrowIfNull(queueOrTopicName, nameof(queueOrTopicName));

        senderOptions ??= new SenderOptions();

        var client = new ServiceBusClient(fullyQualifiedNamespace, credential, CreateServiceBusClientOptions(senderOptions));

        var adminClient = senderOptions.ConnectionCanCreateTopicOrQueue ? new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential) : null;

        return CreateQueueOrTopicSenderAsync<T>(adminClient, client, queueOrTopicName, cancellationToken);
    }

    #endregion Sender AzureSasCredential

    /// <summary>
    /// Create Service Bus Client Options based on configured sender options
    /// </summary>
    /// <param name="senderOptions">The sender options</param>
    /// <returns>A ServiceBusClientOptions object</returns>
    private static ServiceBusClientOptions CreateServiceBusClientOptions(SenderOptions senderOptions)
    {
        return new ServiceBusClientOptions
        {
            TransportType = senderOptions.ServiceBusTransportType,
            RetryOptions = new ServiceBusRetryOptions
            {
                Mode = senderOptions.Mode,
                Delay = senderOptions.Delay,
                MaxRetries = senderOptions.MaxRetries,
                MaxDelay = senderOptions.MaxDelay,
            }
        };
    }

    #region Create Senders

    /// <summary>
    /// Create a Queue/Topic sender
    /// </summary>
    /// <typeparam name="T">The class structure of the message to send</typeparam>
    /// <param name="adminClient">The service bus administration client object</param>
    /// <param name="client">Teh service bus client object</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    private static async Task<ISender<T>> CreateQueueOrTopicSenderAsync<T>(ServiceBusAdministrationClient adminClient, ServiceBusClient client, string topicName, CancellationToken cancellationToken) where T : class
    {
        await ConfigureTopicAsync(adminClient, topicName, cancellationToken).ConfigureAwait(false);
        var sender = client.CreateSender(topicName);

        return new Sender<T>(sender);
    }

    #endregion Create Senders

    #region Create Receivers

    /// <summary>
    /// Create a Topic receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="adminClient">The service bus administration client object</param>
    /// <param name="client">Teh service bus client object</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="receiverOptions">The options for the receiver</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    private static async Task<IReceiver<T>> CreateTopicReceiverAsync<T>(ServiceBusAdministrationClient adminClient, ServiceBusClient client, string topicName, string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken) where T : class
    {
        await ConfigureTopicAsync(adminClient, topicName, cancellationToken).ConfigureAwait(false);
        await ConfigureSubscriptionAsync(adminClient, topicName, subscriptionName, cancellationToken).ConfigureAwait(false);
        var receiver = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = receiverOptions.MaxConcurrentCalls,
            ReceiveMode = receiverOptions.ReceiveMode
        });

        return new Receiver<T>(receiver);
    }

    /// <summary>
    /// Create a Queue receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="adminClient">The service bus administration client object</param>
    /// <param name="client">Teh service bus client object</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="receiverOptions">The options for the receiver</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    private static async Task<IReceiver<T>> CreateQueueReceiverAsync<T>(ServiceBusAdministrationClient adminClient, ServiceBusClient client, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken) where T : class
    {
        await ConfigureQueueAsync(adminClient, queueName, cancellationToken).ConfigureAwait(false);
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

    /// <summary>
    /// Configure the creation of a topic
    /// </summary>
    /// <param name="adminClient">The service bus administration client object</param>
    /// <param name="topicName">The name of the topic to subscribe to</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    private static async Task ConfigureTopicAsync(ServiceBusAdministrationClient adminClient, string topicName, CancellationToken cancellationToken)
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

        if (!await adminClient.TopicExistsAsync(topicName, cancellationToken).ConfigureAwait(false))
        {
            await adminClient.CreateTopicAsync(options, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Configure creation of the Subscription
    /// </summary>
    /// <param name="adminClient">The service bus administration client object</param>
    /// <param name="topicName">The name of the topic to subscribe to</param>
    /// <param name="subscriptionName">The name of the subscription to create</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    private static async Task ConfigureSubscriptionAsync(ServiceBusAdministrationClient adminClient, string topicName, string subscriptionName, CancellationToken cancellationToken)
    {
        if (adminClient == null)
        {
            return;
        }

        if (!await adminClient.SubscriptionExistsAsync(topicName, subscriptionName, cancellationToken).ConfigureAwait(false))
        {
            await adminClient.CreateSubscriptionAsync(topicName, subscriptionName, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Configure creation of a Queue
    /// </summary>
    /// <param name="adminClient">The service bus administration client object</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    private static async Task ConfigureQueueAsync(ServiceBusAdministrationClient adminClient, string queueName, CancellationToken cancellationToken)
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

        if (!await adminClient.QueueExistsAsync(queueName, cancellationToken).ConfigureAwait(false))
        {
            await adminClient.CreateQueueAsync(options, cancellationToken).ConfigureAwait(false);
        }
    }

    #endregion Configure Topic/Queues
}