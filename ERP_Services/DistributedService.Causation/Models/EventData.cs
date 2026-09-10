using static DistributedService.Causation.Enums.Enums;

namespace DistributedService.Causation.Models
{
    public class EventData
    {
        public object Data { get; set; }
        public EventType EventType { get; set; }
        public EventAction Action { get; set; }
        public string Database { get; set; }
        public string UserCode { get; set; }
    }
}
