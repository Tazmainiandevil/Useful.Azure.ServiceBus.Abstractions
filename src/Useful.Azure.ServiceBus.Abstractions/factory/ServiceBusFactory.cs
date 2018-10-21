using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Azure.ServiceBus.Primitives;
using System.Threading.Tasks;
using Useful.Azure.ServiceBus.Abstractions.receiver;
using Useful.Azure.ServiceBus.Abstractions.sender;

namespace Useful.Azure.ServiceBus.Abstractions.factory
{
    public class ServiceBusFactory : IServiceBusFactory
    {
        #region Topic Senders

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="topicName">The name of the topic</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateTopicSenderAsync<T>(string connectionString, string topicName, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(connectionString, topicName, canCreateTopic).ConfigureAwait(false);

            var topicClient = new TopicClient(connectionString, topicName, retryPolicy);

            return new Sender<T>(topicClient);
        }

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="topicName">The name of the topic</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateTopicSenderAsync<T>(string connectionString, string topicName, TransportType transportType, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class
        {
            var builder = new ServiceBusConnectionStringBuilder(connectionString) { EntityPath = topicName };
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            await ConfigureTopicAsync(builder, canCreateTopic).ConfigureAwait(false);

            var topicClient = new TopicClient(builder.Endpoint, topicName, tokenProvider, transportType);

            return new Sender<T>(topicClient);
        }

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateTopicSenderAsync<T>(ServiceBusConnectionStringBuilder builder, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(builder, canCreateTopic).ConfigureAwait(false);

            var topicClient = new TopicClient(builder, retryPolicy);
            return new Sender<T>(topicClient);
        }

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="tokenProvider">The token provider</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateTopicSenderAsync<T>(ServiceBusConnectionStringBuilder builder, ITokenProvider tokenProvider, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(builder, canCreateTopic).ConfigureAwait(false);

            var topicClient = new TopicClient(builder.Endpoint, builder.EntityPath, tokenProvider, builder.TransportType, retryPolicy);
            return new Sender<T>(topicClient);
        }

        #endregion Topic Senders

        #region Queue Senders

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if queue should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateQueueSenderAsync<T>(string connectionString, string queueName, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var queueClient = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock, retryPolicy);

            return new Sender<T>(queueClient);
        }

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateQueueSenderAsync<T>(string connectionString, string queueName, TransportType transportType, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            var queueClient = new QueueClient(builder.Endpoint, queueName, tokenProvider, transportType, ReceiveMode.PeekLock, retryPolicy);

            return new Sender<T>(queueClient);
        }

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="retryPolicy"></param>
        /// <param name="canCreateQueue"></param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateQueueSenderAsync<T>(ServiceBusConnectionStringBuilder builder, RetryPolicy retryPolicy = null,
            bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(builder, canCreateQueue).ConfigureAwait(false);

            var queueClient = new QueueClient(builder.Endpoint, builder.EntityPath, ReceiveMode.PeekLock, retryPolicy);

            return new Sender<T>(queueClient);
        }

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="builder">The connection string builder</param>
        /// <param name="tokenProvider">The token provider</param>
        /// <param name="retryPolicy"></param>
        /// <param name="canCreateQueue"></param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateQueueSenderAsync<T>(ServiceBusConnectionStringBuilder builder, ITokenProvider tokenProvider,
            RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(builder, canCreateQueue).ConfigureAwait(false);

            var queueClient = new QueueClient(builder.Endpoint, builder.EntityPath, tokenProvider, builder.TransportType, ReceiveMode.PeekLock, retryPolicy);

            return new Sender<T>(queueClient);
        }

        #endregion Queue Senders

        #region Topic Receivers

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
        public async Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName,
            ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(connectionString, topicName, canCreateTopic).ConfigureAwait(false);

            await ConfigureSubscriptionAsync(connectionString, topicName, subscriptionName).ConfigureAwait(false);

            var subscriptionClient = new SubscriptionClient(connectionString, topicName, subscriptionName, receiveMode, retryPolicy);

            return new Receiver<T>(subscriptionClient);
        }

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
        public async Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName,
            TransportType transportType, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(connectionString, topicName, canCreateTopic).ConfigureAwait(false);

            await ConfigureSubscriptionAsync(connectionString, topicName, subscriptionName).ConfigureAwait(false);

            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            var subscriptionClient = new SubscriptionClient(builder.Endpoint, topicName, subscriptionName, tokenProvider, transportType, receiveMode, retryPolicy);

            return new Receiver<T>(subscriptionClient);
        }

        #endregion Topic Receivers

        #region Queue Receivers

        /// <summary>
        /// Create a message queue receiver
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="retryPolicy">The retry policy</param>
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        public async Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName,
            ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var queueClient = new QueueClient(connectionString, queueName, receiveMode, retryPolicy);

            return new Receiver<T>(queueClient);
        }

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
        public async Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName,
            TransportType transportType, ReceiveMode receiveMode = ReceiveMode.PeekLock, RetryPolicy retryPolicy = null, bool canCreateQueue = false)
            where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            var queueClient = new QueueClient(builder.Endpoint, queueName, tokenProvider, transportType, receiveMode, retryPolicy);

            return new Receiver<T>(queueClient);
        }

        #endregion Queue Receivers

        #region Configure Topics, Queues and Subscriptions

        private static async Task ConfigureTopicAsync(string connectionString, string topic, bool canCreateTopic)
        {
            if (canCreateTopic)
            {
                var topicDescription = new TopicDescription(topic)
                {
                    EnableBatchedOperations = true,
                    EnablePartitioning = true
                };

                var client = new ManagementClient(connectionString);
                if (!await client.TopicExistsAsync(topic).ConfigureAwait(false))
                {
                    await client.CreateTopicAsync(topicDescription).ConfigureAwait(false);
                }
            }
        }

        private static async Task ConfigureTopicAsync(ServiceBusConnectionStringBuilder builder, bool canCreateTopic)
        {
            if (canCreateTopic)
            {
                var topicDescription = new TopicDescription(builder.EntityPath)
                {
                    EnableBatchedOperations = true,
                    EnablePartitioning = true
                };

                var client = new ManagementClient(builder.GetNamespaceConnectionString());
                if (!await client.TopicExistsAsync(builder.EntityPath).ConfigureAwait(false))
                {
                    await client.CreateTopicAsync(topicDescription).ConfigureAwait(false);
                }
            }
        }

        private static async Task ConfigureQueueAsync(string connectionString, string queue, bool canCreateQueue)
        {
            if (canCreateQueue)
            {
                var queueDescription = new QueueDescription(queue)
                {
                    EnableBatchedOperations = true,
                    EnablePartitioning = true
                };

                var client = new ManagementClient(connectionString);
                if (!await client.QueueExistsAsync(queue).ConfigureAwait(false))
                {
                    await client.CreateQueueAsync(queueDescription).ConfigureAwait(false);
                }
            }
        }

        private static async Task ConfigureQueueAsync(ServiceBusConnectionStringBuilder builder, bool canCreateQueue)
        {
            if (canCreateQueue)
            {
                var queueDescription = new QueueDescription(builder.EntityPath)
                {
                    EnableBatchedOperations = true,
                    EnablePartitioning = true
                };

                var client = new ManagementClient(builder.GetNamespaceConnectionString());
                if (!await client.QueueExistsAsync(builder.EntityPath).ConfigureAwait(false))
                {
                    await client.CreateQueueAsync(queueDescription).ConfigureAwait(false);
                }
            }
        }

        private static async Task ConfigureSubscriptionAsync(string connectionString, string topicName, string subscriptionName)
        {
            var subscriptionDescription = new SubscriptionDescription(topicName, subscriptionName);

            var client = new ManagementClient(connectionString);
            if (!await client.SubscriptionExistsAsync(topicName, subscriptionName).ConfigureAwait(false))
            {
                await client.CreateSubscriptionAsync(subscriptionDescription).ConfigureAwait(false);
            }
        }

        #endregion Configure Topics, Queues and Subscriptions
    }
}