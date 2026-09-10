using Application.EventHandlers.Model;
using Application.EventHandlers.Security;
using Azure.Messaging.ServiceBus;
using Infrastructure.CrossCutting.Base;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Application.EventHandlers.Proxies
{
    public class AzureServiceBusProxy : IEventProxy
    {
        /// <summary>
        /// the client that owns the connection and can be used to create senders and receivers
        /// </summary>
        private ServiceBusClient client;
        /// <summary>
        /// the sender used to publish messages to the queue
        /// </summary>
        private ServiceBusSender sender;

        /// <summary>
        /// Publish event
        /// </summary>
        /// <param name="event"></param>
        public void Publish(EventData @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            if (@event.data == null) throw new ArgumentNullException(nameof(@event.data));

            var Th = new System.Threading.Thread(async eventObj =>
                {
                    try
                    {
                        var queue = GetUrlQueue(@event.db, @event.source.ToString());

                        if (string.IsNullOrEmpty(queue.database) || string.IsNullOrEmpty(queue.urlQueue) || string.IsNullOrEmpty(queue.topic)) 
                            throw new Exception("Event has not configurated");

                        @event.db = queue.database;
                        client = new ServiceBusClient(queue.urlQueue);
                        sender = client.CreateSender(queue.topic);

                        using (ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync())
                        {
                            var serializedServiceBusMessage  =  new ServiceBusMessage(JsonConvert.SerializeObject(@event));
                            serializedServiceBusMessage.ApplicationProperties.Add("transmitter", "Vie_Cloud_Platform");
                            serializedServiceBusMessage.ApplicationProperties.Add("ContainerDB", @event.db);
                            serializedServiceBusMessage.ApplicationProperties.Add("SourceEvent", @event.source);

                            if (!messageBatch.TryAddMessage(serializedServiceBusMessage))
                            {
                                Debug.WriteLine($"The message XXX is too large to fit in the batch.");
                                throw new Exception($"The message XXX is too large to fit in the batch.");
                            }

                            try
                            {
                                await sender.SendMessagesAsync(messageBatch);
                                Debug.WriteLine($"A batch of XXXX messages has been published to the queue.");
                            }
                            finally
                            {
                                await sender.DisposeAsync();
                                await client.DisposeAsync();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                });
            Th.Start();
        }

        private (string urlQueue, string topic, string database) GetUrlQueue(
            string company,
            string eventCode)
        {
            var securityContainer = ConfigurationManager.AppSettings.Get("containerSecurity");
            using (var context = new SecurityContext(
                Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, string.Empty, securityContainer, true)
            ))
            {
                var @event = context.EventsConfiguration.AsNoTracking()
                    .Include(m => m.Container)
                    .SingleOrDefault(m => m.Container.Code == company && m.Code == eventCode);

                return (@event?.UrlQueue, @event?.Topic, @event?.Container?.TransactionalContainer);
            }
        }
    }
}
