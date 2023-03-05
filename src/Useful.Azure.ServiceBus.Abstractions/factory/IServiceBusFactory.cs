using Azure;
using Azure.Core;
using Useful.Azure.ServiceBus.Abstractions.receiver;
using Useful.Azure.ServiceBus.Abstractions.sender;

namespace Useful.Azure.ServiceBus.Abstractions.factory;

public interface IServiceBusFactory
{
    #region Receiver ConnectionString

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="connectionString">The connection string for connecting to the service bus</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="connectionString">The connection string for connecting to the service bus</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="receiverOptions">The options to configure the receiver</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="connectionString">The connection string for connecting to the service bus</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="connectionString">The connection string for connecting to the service bus</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="receiverOptions">The options to configure the receiver</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Receiver ConnectionString

    #region Receiver AzureNamedKeyCredential

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureNamedKeyCredential to use for authorization.  Access controls may be specified by the Service Bus namespace.</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName, string subscriptionName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureNamedKeyCredential to use for authorization.  Access controls may be specified by the Service Bus namespace.</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="receiverOptions"></param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName, string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureNamedKeyCredential to use for authorization.  Access controls may be specified by the Service Bus namespace.</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureNamedKeyCredential to use for authorization.  Access controls may be specified by the Service Bus namespace.</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="receiverOptions">The options to configure the receiver</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Receiver AzureNamedKeyCredential

    #region Receiver TokenCredential

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The Azure managed identity credential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName, string subscriptionName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The Azure managed identity credential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="receiverOptions"></param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName, string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The Azure managed identity credential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The Azure managed identity credential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="receiverOptions">The options to configure the receiver</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Receiver TokenCredential

    #region Receiver AzureSasCredential

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureSasCredential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName, string subscriptionName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Topic Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureSasCredential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="topicName">The name of the topic</param>
    /// <param name="subscriptionName">The name of the subscription</param>
    /// <param name="receiverOptions"></param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName, string subscriptionName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureSasCredential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue Receiver
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureSasCredential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueName">The name of the queue</param>
    /// <param name="receiverOptions">The options to configure the receiver</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Receiver</returns>
    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName, ReceiverOptions receiverOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Receiver AzureSasCredential

    #region Sender ConnectionString

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="connectionString">The connection string for connecting to the service bus</param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string connectionString, string queueOrTopicName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the expected response</typeparam>
    /// <param name="connectionString"></param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="senderOptions"></param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string connectionString, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Sender ConnectionString

    #region Sender AzureNamedKeyCredential

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the message to send</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureNamedKeyCredential to use for authorization.  Access controls may be specified by the Service Bus namespace.</param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueOrTopicName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the message to send</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureNamedKeyCredential to use for authorization.  Access controls may be specified by the Service Bus namespace.</param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="senderOptions"></param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Sender AzureNamedKeyCredential

    #region Sender TokenCredential

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the message to send</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The Azure managed identity credential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueOrTopicName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the message to send</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The Azure managed identity credential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="senderOptions"></param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Sender TokenCredential

    #region Sender AzureSasCredential

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the message to send</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureSasCredential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueOrTopicName, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Create a Queue/Topic Sender
    /// </summary>
    /// <typeparam name="T">The class structure of the message to send</typeparam>
    /// <param name="fullyQualifiedNamespace">The fully qualified Service Bus namespace to connect to</param>
    /// <param name="credential">The AzureSasCredential to use for authorization. Access controls may be specified by the Service Bus namespace</param>
    /// <param name="queueOrTopicName">The queue or topic name</param>
    /// <param name="senderOptions">The options for the Sender</param>
    /// <param name="cancellationToken">Cancellation Token instance to signal the request to cancel the operation</param>
    /// <returns>An instance of the Sender</returns>
    Task<ISender<T>> CreateSenderAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueOrTopicName, SenderOptions senderOptions, CancellationToken cancellationToken = default) where T : class;

    #endregion Sender AzureSasCredential
}