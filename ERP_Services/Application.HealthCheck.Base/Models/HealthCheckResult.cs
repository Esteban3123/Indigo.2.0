using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Application.HealthCheck.Base.Models
{
    public class HealthCheckResult
    {
        [JsonProperty("serviceID")]
        public string ServiceId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("status")]
        public string Status
            => HealthChecks.Any(x => x.HealthState == HealthState.fail)
            ? HealthState.fail.ToString()
            : HealthChecks.Any(m => m.HealthState != HealthState.pass) ? HealthState.warn.ToString() : HealthState.pass.ToString();

        [JsonIgnore]
        public bool HasFailures
        {
            get { return HealthChecks.Any(x => x.HealthState != HealthState.pass); }
        }

        [JsonIgnore]
        public string MachineName { get; set; }

        [JsonProperty("uptime")]
        public double UpTime { get; set; }

        [JsonProperty("details")]
        public List<HealthCheckItemResult> HealthChecks { get; set; } = new List<HealthCheckItemResult>();
    }
}