using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Bus.Events
{
    public class Event
    {
    
        public long timestamp { get; set; }
        protected Event()
        {
            timestamp = DateTime.Now.ToFileTime();
        }
    }
}
