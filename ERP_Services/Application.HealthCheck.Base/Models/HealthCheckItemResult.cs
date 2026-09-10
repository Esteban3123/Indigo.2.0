using Newtonsoft.Json;

namespace Application.HealthCheck.Base.Models
{
    public class HealthCheckItemResult
    {
        public HealthCheckItemResult(string resourceName, int sortOrder, string friendlyName, string description = null)
        {
            ResourceName = resourceName;
            SortOrder = sortOrder;
            FriendlyName = friendlyName;
            Description = description;
        }

        [JsonProperty(PropertyName = "id", Order = -70)]
        public string ResourceName { get; }

        [JsonProperty(PropertyName = "name", Order = -60)]
        public string FriendlyName { get; }

        [JsonIgnore]
        [JsonProperty(Order = -50)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual HealthState HealthState { get; set; }

        [JsonProperty(PropertyName = "status", Order = -40)]
        public virtual string State => HealthState.ToString();

        [JsonProperty(PropertyName = "output", Order = 1000)]
        public string Message { get; set; } = "";

        [JsonIgnore]
        public int SortOrder { get; }
    }

    public enum HealthState
    {
        pass,
        warn,
        fail
    }
}