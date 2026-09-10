using Newtonsoft.Json;

namespace DistributedService.Causation.Extensions
{
    public static class MapperExtensions
    {
        public static T MapTo<T>(this object value)
            => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value));
    }
}