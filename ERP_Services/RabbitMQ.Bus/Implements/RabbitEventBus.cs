using Infrastructure.CrossCutting.Exceptions;
using Newtonsoft.Json;
using RabbitMQ.Bus.BusRabbit;
using RabbitMQ.Bus.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;


namespace RabbitMQ.Bus.Implements
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;        

        public RabbitEventBus()
        {
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }        
        public void Publish<Object>(Object evento, string UrlQueue) where Object : Event
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(UrlQueue)
                };
                factory.RequestedHeartbeat = TimeSpan.FromTicks(60);
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var eventName = evento.GetType().Name;
                    channel.QueueDeclare(eventName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    var message = JsonConvert.SerializeObject(evento);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", eventName, null, body);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }            
        }       
    }
}

