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
    public class PatientInfo
    {
        [DataMember()]
        public int Code { get; set; }

        [DataMember()]
        public int BornDays { get; set; }

        [DataMember()]
        public int DocumentType { get; set; }

        [DataMember()]
        public string DocumentNumber { get; set; }
    }
}
