using Application.HealthCheck.Base;
using Application.HealthCheck.Base.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace DistributedService.HealthCheck.MixingStation.Controllers
{
    [RoutePrefix("api/healthCheck")]
    public class HealthController : ApiController
    {
        [HttpGet]
        [Route("ms")]
        public async Task<IHttpActionResult> GetHealthInfo()
        {
            var result = new HealthCheckResult
            {
                ServiceId = "healthCheck:ms",
                Description = "health of mixing station service",
                MachineName = Environment.MachineName
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var items = new List<HealthCheckItemResult>();
            var instances = new List<IHealthCheckProvider>();

            foreach (var provider in GetAllProviders())
            {
                var instance = (IHealthCheckProvider)Activator.CreateInstance(provider);
                instances.Add(instance);
            }

            await Task.WhenAll(instances.Select(async x => items.Add(await x.GetHealthCheckAsync())));
            stopwatch.Stop();
            result.UpTime = stopwatch.Elapsed.TotalSeconds;
            result.HealthChecks.AddRange(items.OrderBy(x => x.SortOrder));

            return ResponseMessage(Request.CreateResponse(getStatusResult(result.Status), result));
        }

        private HttpStatusCode getStatusResult(string status)
        {
            if (status == HealthState.pass.ToString())
                return HttpStatusCode.OK;
            else if (status == HealthState.warn.ToString())
                return HttpStatusCode.MultipleChoices;
            else
                return HttpStatusCode.BadRequest;
        }

        private List<Type> GetAllProviders()
        {
            return GetTypesDeriving<IHealthCheckProvider>();
        }

        private static List<Type> GetTypesDeriving<T>()
        {
            var assemblies = new List<Assembly> { Assembly.Load(typeof(HealthController).Assembly.FullName) };
            //return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
            //        from assemblyType in domainAssembly.GetTypes()
            //        where typeof(T).IsAssignableFrom(assemblyType) && !assemblyType.IsAbstract
            //        select assemblyType).ToList();
            return (from domainAssembly in assemblies
                    from assemblyType in domainAssembly.GetTypes()
                    where typeof(T).IsAssignableFrom(assemblyType) && !assemblyType.IsAbstract
                    select assemblyType).ToList();
        }
    }
}
