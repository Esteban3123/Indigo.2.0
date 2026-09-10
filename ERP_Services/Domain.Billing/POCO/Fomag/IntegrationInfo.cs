using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Billing.POCO
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    [Serializable()]
    [KnownType(typeof(TaxDevolution))]
    public class IntegrationInfo
    {
        [DataMember()]
        public string CodeIndigo { get; set; }

        [DataMember()]
        public string CodeIntegration { get; set; }

        [DataMember()]
        public string Description { get; set; }

        [DataMember()]
        public string URLAz { get; set; }
    }
}
