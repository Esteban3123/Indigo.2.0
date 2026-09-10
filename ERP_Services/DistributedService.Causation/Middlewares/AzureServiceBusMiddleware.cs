using Azure.Messaging.ServiceBus;
using DistributedService.Causation.Extensions;
using DistributedService.Causation.Models;
using DistributedService.Causation.Services;
using Infrastructure.CrossCutting.Exceptions;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedService.Causation.Middlewares
{
    public class AzureServiceBusMiddleware : OwinMiddleware
    {
        private string connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("queue-url");
        private string topicName = System.Configuration.ConfigurationManager.AppSettings.Get("topic-name");
        private string subscriptionName = System.Configuration.ConfigurationManager.AppSettings.Get("subscription-name");
        private ServiceBusClient client;
        private ServiceBusProcessor processor;
        private ICausationService _causationService;

        public AzureServiceBusMiddleware(OwinMiddleware next)
            : base(next)
        {
            _causationService = new CausationService();
            InstanceServiceBus();
        }

        private async void InstanceServiceBus()
        {
            client = new ServiceBusClient(connectionString);
            processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());

            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;
                await processor.StartProcessingAsync().ConfigureAwait(false);
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                //await processor.DisposeAsync();
                //await client.DisposeAsync();
            }
        }

        //[Transaction]
        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            await args.CompleteMessageAsync(args.Message);            
            string body = args.Message.Body.ToString();
            var @event = JsonConvert.DeserializeObject<EventData>(body);

            await Task.Factory.StartNew(async () =>
            {
                if (@event.Action == Enums.Enums.EventAction.Insert)
                    await _causationService.CausateInvoicesAsync(@event.Data.MapTo<List<InvoiceEvent>>(), @event.Database, @event.UserCode);
                else if (@event.Action == Enums.Enums.EventAction.Annulate)
                    await _causationService.RemoveInvoiceFromPendingAsync(@event.Data.MapTo<List<InvoiceEvent>>(), @event.Database, @event.UserCode);
            });
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            IndigoManagementExceptions.HandleException(args.Exception, "ApplicationPolicy");
            return Task.CompletedTask;
        }

        public override async Task Invoke(IOwinContext request)
        {
            await Next.Invoke(request);
        }
    }
}