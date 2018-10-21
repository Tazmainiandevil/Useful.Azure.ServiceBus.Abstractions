using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Primitives;
using System.Threading.Tasks;
using Useful.Azure.ServiceBus.Abstractions.receiver;
using Useful.Azure.ServiceBus.Abstractions.sender;

namespace Useful.Azure.ServiceBus.Abstractions.factory
{
    public interface IServiceBusFactory
    {
        #region Senders

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="topicName">The name of the topic</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateTopicSenderAsync<T>(string connectionString, string topicName, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="topicName">The name of the topic</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateTopicSenderAsync<T>(string connectionString, string topicName, TransportType transportType, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateTopicSenderAsync<T>(ServiceBusConnectionStringBuilder builder, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="tokenProvider">The token provider</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateTopicSenderAsync<T>(ServiceBusConnectionStringBuilder builder, ITokenProvider tokenProvider, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if queue should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateQueueSenderAsync<T>(string connectionString, string queueName, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateQueueSenderAsync<T>(string connectionString, string queueName, TransportType transportType, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="retryPolicy"></param>
        /// <param name="canCreateQueue"></param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateQueueSenderAsync<T>(ServiceBusConnectionStringBuilder builder, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="tokenProvider">The token provider</param>
        /// <param name="retryPolicy"></param>
        /// <param name="canCreateQueue"></param>
        /// <returns>A service bus sender</returns>
        Task<ISender<T>> CreateQueueSenderAsync<T>(ServiceBusConnectionStringBuilder builder, ITokenProvider tokenProvider, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        #endregion Senders

        #region Receivers

        /// <summary>
        /// Create a message topic receiver
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="topicName">The name of the topic</param>
        /// <param name="subscriptionName">The name of the subscription</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;

        /// <summary>
        /// Create a message topic receiver
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="topicName">The name of the topic</param>
        /// <param name="subscriptionName">The name of the subscription</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName, TransportType transportType, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;

        /// <summary>
        /// Create a message topic receiver
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="subscriptionName">The name of the subscription</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateTopicReceiverAsync<T>(ServiceBusConnectionStringBuilder builder, string subscriptionName, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;
        
        /// <summary>
        /// Create a message topic receiver
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="tokenProvider"></param>
        /// <param name="subscriptionName">The name of the subscription</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateTopicReceiverAsync<T>(ServiceBusConnectionStringBuilder builder, ITokenProvider tokenProvider, string subscriptionName, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class;

        /// <summary>
        /// Create a message queue receiver
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        /// <summary>
        /// Create a message queue receiver
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName, TransportType transportType, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        /// <summary>
        /// Create a message queue receiver
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="subscriptionName">The name of the subscription</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateQueueReceiverAsync<T>(ServiceBusConnectionStringBuilder builder, string subscriptionName, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        /// <summary>
        /// Create a message queue receiver
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="tokenProvider"></param>
        /// <param name="subscriptionName">The name of the subscription</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        Task<IReceiver<T>> CreateQueueReceiverAsync<T>(ServiceBusConnectionStringBuilder builder, ITokenProvider tokenProvider, string subscriptionName, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class;

        #endregion Receivers
    }
}