using Application.EventHandlers.Model;
using System;

namespace Application.EventHandlers.Proxies
{
    public class DittoEventProxy : IEventProxy
    {
        public void Publish(EventData @event)
        {
            throw new NotImplementedException();
        }
    }
}
