using System.Collections.Generic;

namespace Application.EventHandlers.Security.Entities
{
    public class Container
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FoundationalContainer { get; set; }
        public string TransactionalContainer { get; set; }
        public string DocumentalContainer { get; set; }
        public byte CompanyType { get; set; }
        public string CompanyNit { get; set; }
        public bool State { get; set; }

        public virtual ICollection<EventConfiguration> EventConfigurations { get; set; } = new HashSet<EventConfiguration>();
    }
}
