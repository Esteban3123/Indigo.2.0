using Application.EventHandlers.Security.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Application.EventHandlers.Security.Configurations
{
    internal class EventConfigurationConfiguration : EntityTypeConfiguration<EventConfiguration>
    {
        public EventConfigurationConfiguration()
        {
            ToTable("EventsConfiguration", "Security");
        }
    }
}
