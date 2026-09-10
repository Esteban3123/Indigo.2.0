using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Domain.Billing.POCO
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    [Serializable()]
    [KnownType(typeof(InvoicePartialDetailMasterAccount))]
    public class InvoicePartialDetailMasterAccount
    {
        [DataMember()]
        public string Id { get; set; }

        [DataMember()]
        public int RevenueControlDetailId { get; set; }

        [DataMember()]
        public int ServiceOrderDetailDistributionId { get; set; }

        [DataMember()]
        public int ServiceOrderDetailId { get; set; }

        [DataMember()]
        public string BillingGroup { get; set; }

        [DataMember()]
        public string Code { get; set; }

        [DataMember()]
        public string CUPSCode { get; set; }

        [DataMember()]
        public string RIPSCode { get; set; }

        [DataMember()]
        public string CodeAlternative { get; set; }

        [DataMember()]
        public string CodeAlternativeTwo { get; set; }

        [DataMember()]
        public string CodeCUM { get; set; }

        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public string CUPSName { get; set; }

        [DataMember()]
        public string RIPSName { get; set; }

        [DataMember()]
        public string ContractDescriptionCode { get; set; }

        [DataMember()]
        public string ContractDescriptionName { get; set; }

        [DataMember()]
        public DateTime ServiceDate { get; set; }

        [DataMember()]
        public string AuthorizationNumber { get; set; }

        [DataMember()]
        public byte RecordType { get; set; }

        [DataMember()]
        public byte Presentation { get; set; }

        [DataMember()]
        public int InvoicedQuantity { get; set; }

        [DataMember()]
        public decimal TotalSalesPrice { get; set; }

        [DataMember()]
        public decimal ThirdPartyDiscount { get; set; }

        [DataMember()]
        public decimal SubTotalPatientSalesPrice { get; set; }

        [DataMember()]
        public decimal ThirdPartySalesPrice { get; set; }

        [DataMember()]
        public int? SurgicalId { get; set; }

        [DataMember()]
        public string CodeSurgical { get; set; }

        [DataMember()]
        public string NameSurgical { get; set; }

        [DataMember()]
        public int? QuantitySurgical { get; set; }

        [DataMember()]
        public decimal? TotalSalesPriceSurgical { get; set; }

        [DataMember()]
        public string IvaPercentage { get; set; }

        [DataMember()]
        public decimal IvaTotalValue { get; set; }

        [DataMember()]
        public decimal GrossValue { get; set; }

        [DataMember()]
        public decimal SubTotalSalesPrice { get; set; }

        [DataMember()]
        public decimal NetWorth { get; set; }

        [DataMember()]
        public decimal NetUnitValue { get; set; }

        [DataMember()]
        public decimal GrandTotalSalesPrice { get; set; }

        [DataMember()]
        public decimal GrandTotalDiscount { get; set; }

        [DataMember()]
        public decimal TaxPercentage { get; set; }
    }
}
