namespace Application.EventHandlers.Security.Entities
{
    public class EventConfiguration
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ContainerId { get; set; }
        public string UrlQueue { get; set; }
        public string Topic { get; set; }
        public bool Status { get; set; }

        public virtual Container Container { get; set; }        
    }
}
