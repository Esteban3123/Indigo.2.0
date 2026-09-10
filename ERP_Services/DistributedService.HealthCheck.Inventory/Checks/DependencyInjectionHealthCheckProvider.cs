using Application.HealthCheck.Base;
using Application.HealthCheck.Base.Models;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DistributedService.HealthCheck.Inventory.Health.Checks
{
    public class DependencyInjectionHealthCheckProvider : IHealthCheckProvider
    {
        public int SortOrder => 2;

        public Task<HealthCheckItemResult> GetHealthCheckAsync()
        {
            var res = new HealthCheckItemResult(nameof(DependencyInjectionHealthCheckProvider), SortOrder, "Checks dependency injection", "Checks if the inventory module can be instantiated correctly.");

            try
            {
                var types = GetTypesDeriving();

                foreach (var type in types)
                {
                    Container.Current.Resolve(type);
                }

                res.HealthState = HealthState.pass;
            }
            catch (Exception ex)
            {
                res.HealthState = HealthState.fail;
                res.Message = ex.ToDetailString();
            }

            return Task.FromResult(res);
        }

        private static List<Type> GetTypesDeriving()
        {
            var assemblies = new List<Assembly> { Assembly.Load("Application.Inventory") };
            return (from domainAssembly in assemblies
                    from assemblyType in domainAssembly.GetTypes()
                    where assemblyType.IsInterface
                    select assemblyType).ToList();
        }
    }
}