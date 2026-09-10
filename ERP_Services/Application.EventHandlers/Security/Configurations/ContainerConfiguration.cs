using Application.EventHandlers.Security.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Application.EventHandlers.Security.Configurations
{
    internal class ContainerConfiguration : EntityTypeConfiguration<Container>
    {
        public ContainerConfiguration()
        {
            ToTable("Containers", "Security");
        }
    }
}
