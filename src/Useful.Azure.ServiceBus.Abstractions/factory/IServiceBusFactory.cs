using Azure;
using Azure.Core;
using Useful.Azure.ServiceBus.Abstractions.receiver;
using Useful.Azure.ServiceBus.Abstractions.sender;

namespace Useful.Azure.ServiceBus.Abstractions.factory;

public interface IServiceBusFactory
{
    #region Receiver ConnectionString

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName) where T : class;

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName, ReceiverOptions receiverOptions) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, ReceiverOptions receiverOptions) where T : class;

    #endregion Receiver ConnectionString

    #region Receiver AzureNamedKeyCredential

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName, string subscriptionName) where T : class;

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string topicName, string subscriptionName, ReceiverOptions receiverOptions) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureNamedKeyCredential credential, string queueName, ReceiverOptions receiverOptions) where T : class;

    #endregion Receiver AzureNamedKeyCredential

    #region Receiver TokenCredential

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName, string subscriptionName) where T : class;

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string topicName, string subscriptionName, ReceiverOptions receiverOptions) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, TokenCredential credential, string queueName, ReceiverOptions receiverOptions) where T : class;

    #endregion Receiver TokenCredential

    #region Receiver AzureSasCredential

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName, string subscriptionName) where T : class;

    Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string topicName, string subscriptionName, ReceiverOptions receiverOptions) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName) where T : class;

    Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string fullyQualifiedNamespace, AzureSasCredential credential, string queueName, ReceiverOptions receiverOptions) where T : class;

    #endregion Receiver AzureSasCredential

    ISender<T> CreateTopicSenderAsync<T>(string connectionString, string topicName) where T : class;
}