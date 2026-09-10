using Newtonsoft.Json;

namespace Application.HealthCheck.Base.Models
{
    public class MessageContainer
    {
        public MessageContainer(string message)
        {
            Message = message;
        }

        public MessageContainer(string message, object additionalInfo)
        {
            Message = message;
            AdditionalInfo = additionalInfo;
        }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object AdditionalInfo { get; set; }
    }
}