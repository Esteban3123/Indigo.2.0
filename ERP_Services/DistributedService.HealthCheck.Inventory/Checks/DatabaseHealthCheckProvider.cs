using Application.HealthCheck.Base;
using Application.HealthCheck.Base.Models;
using Infrastructure.CrossCutting.Base;
using Infrastructure.Data.ModelRepository;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedService.HealthCheck.Inventory.Health.Checks
{
    public class DatabaseHealthCheckProvider : IHealthCheckProvider
    {
        public int SortOrder => 1;

        public Task<HealthCheckItemResult> GetHealthCheckAsync()
        {
            var res = new HealthCheckItemResult(nameof(DatabaseHealthCheckProvider), SortOrder, "Checks the database", "Checks whether the main database can be accessed.");

            try
            {
                var database = ConfigurationManager.AppSettings.Get("Database");
                var context = new GlobalModelUnitOfWork(database);
                context.Warehouse.FirstOrDefault();
                res.HealthState = HealthState.pass;
            }
            catch (System.Exception ex)
            {
                res.HealthState = HealthState.fail;
                res.Message = ex.ToDetailString();
            }

            return Task.FromResult(res);
        }
    }
}