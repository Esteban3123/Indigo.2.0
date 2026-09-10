using Application.EventHandlers.Security.Configurations;
using Application.EventHandlers.Security.Entities;
using System.Data.Entity;

namespace Application.EventHandlers.Security
{
    public class SecurityContext : DbContext
    {
        public SecurityContext(string nameOrConnectionString)
            : base(nameOrConnectionString: nameOrConnectionString.Replace("\"", ""))
        {

        }

        public DbSet<Container> Containers { get; set; }
        public DbSet<EventConfiguration> EventsConfiguration { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.Configuration.LazyLoadingEnabled = false;
            base.Configuration.ProxyCreationEnabled = false;

            modelBuilder.Configurations.Add(new ContainerConfiguration());
            modelBuilder.Configurations.Add(new EventConfigurationConfiguration());
        }
    }
}
