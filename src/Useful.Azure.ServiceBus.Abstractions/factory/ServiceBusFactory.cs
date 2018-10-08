﻿using Microsoft.Azure.ServiceBus;
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
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateTopicSenderAsync<T>(string connectionString, string topicName, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(connectionString, topicName, canCreateTopic).ConfigureAwait(false);

            var topicClient = new TopicClient(connectionString, topicName);

            return new Sender<T>(topicClient);
        }

        /// <summary>
        /// Create a message topic sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="topicName">The name of the topic</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateTopicSenderAsync<T>(string connectionString, string topicName, TransportType transportType, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(connectionString, topicName, canCreateTopic).ConfigureAwait(false);

            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            var topicClient = new TopicClient(builder.Endpoint, topicName, tokenProvider, transportType);

            return new Sender<T>(topicClient);
        }

        #endregion Topic Senders

        #region Queue Senders

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="canCreateQueue">A boolean denoting if queue should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateQueueSenderAsync<T>(string connectionString, string queueName, bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var queueClient = new QueueClient(connectionString, queueName);

            return new Sender<T>(queueClient);
        }

        /// <summary>
        /// Create a message queue sender
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus sender</returns>
        public async Task<ISender<T>> CreateQueueSenderAsync<T>(string connectionString, string queueName, TransportType transportType, bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            var queueClient = new QueueClient(builder.Endpoint, queueName, tokenProvider, transportType);

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
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        public async Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName,
            ReceiveMode receiveMode = ReceiveMode.PeekLock, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(connectionString, topicName, canCreateTopic).ConfigureAwait(false);

            await ConfigureSubscriptionAsync(connectionString, topicName, subscriptionName).ConfigureAwait(false);

            var subscriptionClient = new SubscriptionClient(connectionString, topicName, subscriptionName, receiveMode);

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
        /// <param name="canCreateTopic">A boolean denoting if topic should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        public async Task<IReceiver<T>> CreateTopicReceiverAsync<T>(string connectionString, string topicName, string subscriptionName,
            TransportType transportType, ReceiveMode receiveMode = ReceiveMode.PeekLock, bool canCreateTopic = false) where T : class
        {
            await ConfigureTopicAsync(connectionString, topicName, canCreateTopic).ConfigureAwait(false);

            await ConfigureSubscriptionAsync(connectionString, topicName, subscriptionName).ConfigureAwait(false);

            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            var subscriptionClient = new SubscriptionClient(builder.Endpoint, topicName, subscriptionName, tokenProvider, transportType, receiveMode);

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
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        public async Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName,
            ReceiveMode receiveMode = ReceiveMode.PeekLock, bool canCreateQueue = false) where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var queueClient = new QueueClient(connectionString, queueName, receiveMode);

            return new Receiver<T>(queueClient);
        }

        /// <summary>
        /// Create a message queue receiver
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="queueName">The name of the queue</param>
        /// <param name="transportType">The transport type e.g. AMQP, AMQP WebSockets</param>
        /// <param name="receiveMode">The mode to receive messages default is PeekLock</param>
        /// <param name="canCreateQueue">A boolean denoting if queueName should be created if it does not exist. NOTE: Manage rights required</param>
        /// <returns>A service bus receiver</returns>
        public async Task<IReceiver<T>> CreateQueueReceiverAsync<T>(string connectionString, string queueName,
            TransportType transportType, ReceiveMode receiveMode = ReceiveMode.PeekLock, bool canCreateQueue = false)
            where T : class
        {
            await ConfigureQueueAsync(connectionString, queueName, canCreateQueue).ConfigureAwait(false);

            var builder = new ServiceBusConnectionStringBuilder(connectionString);
            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                builder.SasKeyName,
                builder.SasKey);

            var queueClient = new QueueClient(builder.Endpoint, queueName, tokenProvider, transportType, receiveMode);

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