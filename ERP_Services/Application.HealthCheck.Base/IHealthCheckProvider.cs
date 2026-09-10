using Application.HealthCheck.Base.Models;
using System.Threading.Tasks;

namespace Application.HealthCheck.Base
{
    public interface IHealthCheckProvider
    {
        Task<HealthCheckItemResult> GetHealthCheckAsync();

        int SortOrder { get; }
    }
}
