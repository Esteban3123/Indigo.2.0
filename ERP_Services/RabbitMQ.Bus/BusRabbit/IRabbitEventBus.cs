using RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
        void Publish<T>(T @evento, String UrlQueue) where T : Event;        
    
    }
}
