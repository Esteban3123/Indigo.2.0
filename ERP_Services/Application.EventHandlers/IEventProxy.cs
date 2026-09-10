using Application.EventHandlers.Model;

namespace Application.EventHandlers
{
    public interface IEventProxy
    {
        void Publish(EventData @event);
    }
}
