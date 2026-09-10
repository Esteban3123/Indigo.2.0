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
    public class TaxDevolution
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public int RevenueControlDetailId { get; set; }

        [DataMember()]
        public decimal TaxValue { get; set; }

        [DataMember()]
        public decimal ValueWithTaxDevolution { get; set; }

        [DataMember()]
        public byte PaymentMethodType { get; set; }

        [DataMember()]
        public string PaymentMethodTypeName { get; set; }

        [DataMember()]
        public int CurrencyId { get; set; }

        [DataMember()]
        public string CurrencyAbbreviation { get; set; }

        [DataMember()]
        public string CurrencyName { get; set; }
    }
}
