using System;


namespace Application.Events.Models
{
    public class DittoQueue  : RabbitMQ.Bus.Events.Event
    {

        public Object data { get; set; }
        public string source { get; set; }
        public string action { get; set; }
        public string db { get; set; }
        public string user_code { get; set; }

    }
}
