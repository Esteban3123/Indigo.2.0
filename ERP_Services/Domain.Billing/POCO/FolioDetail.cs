using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Domain.Billing.POCO
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    [Serializable()]
    [KnownType(typeof(FolioDetail))]
    public class FolioDetail
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public int ServiceOrderId { get; set; }

        [DataMember()]
        public string ServiceOrderCode { get; set; }

        [DataMember()]
        public int ServiceOrderDetailId { get; set; }

        [DataMember()]
        public byte SurgeryNumber { get; set; }

        [DataMember()]
        public string PerformsFunctionalUnitCodeName { get; set; }

        [DataMember()]
        public string CostCenterCodeName { get; set; }

        [DataMember()]
        public string AuthorizationNumber { get; set; }

        [DataMember()]
        public string PerformsHealthProfessionalCode { get; set; }

        [DataMember()]
        public string PerformsProfessionalSpecialty { get; set; }

        [DataMember()]
        public DateTime ServiceDate { get; set; }

        [DataMember()]
        public byte RecordType { get; set; }

        [DataMember()]
        public byte? Presentation { get; set; }

        [DataMember()]
        public byte SettlementType { get; set; }

        [DataMember()]
        public byte DistributionType { get; set; }

        [DataMember()]
        public int InvoicedQuantity { get; set; }

        [DataMember()]
        public int SupplyQuantity { get; set; }

        [DataMember()]
        public int DevolutionQuantity { get; set; }

        [DataMember()]
        public decimal CostValue { get; set; }

        [DataMember()]
        public decimal RateManualSalePrice { get; set; }

        [DataMember()]
        public decimal GrandTotalSalesPrice { get; set; }

        [DataMember()]
        public decimal SubTotalSalesPrice { get; set; }

        [DataMember()]
        public decimal ThirdPartyDiscount { get; set; }

        [DataMember()]
        public decimal GrandTotalDiscount { get; set; }

        [DataMember()]
        public decimal ThirdPartyDiscountPercentage { get; set; }

        [DataMember()]
        public decimal TotalSalesPrice { get; set; }

        [DataMember()]
        public decimal ThirdPartySalesPrice { get; set; }

        [DataMember()]
        public decimal ThirdPartyPercentage { get; set; }

        [DataMember()]
        public string SurchargeApply { get; set; }

        [DataMember()]
        public byte RecoveryFeeType { get; set; }

        [DataMember()]
        public byte ApplyRecoveryFee { get; set; }

        [DataMember()]
        public decimal SubTotalPatientSalesPrice { get; set; }

        [DataMember()]
        public decimal PatientPercentage { get; set; }

        [DataMember()]
        public bool IsPackage { get; set; }

        [DataMember()]
        public string CodeAssociateService { get; set; }

        [DataMember()]
        public string GuidHomologation { get; set; }

        [DataMember()]
        public bool ApplyRIAS { get; set; }

        [DataMember()]
        public int RIASCupsId { get; set; }

        [DataMember()]
        public bool AllowValueChange { get; set; }

        [DataMember()]
        public string ServiceCode { get; set; }

        [DataMember()]
        public string ServiceName { get; set; }

        [DataMember()]
        public string IPSServiceCode { get; set; }

        [DataMember()]
        public string IPSServiceName { get; set; }

        [DataMember()]
        public string CUPS { get; set; }

        [DataMember()]
        public string ContractDescriptionCodeName { get; set; }

        [DataMember()]
        public string ServiceBillingGroupCodeName { get; set; }

        [DataMember()]
        public int ProductId { get; set; }

        [DataMember()]
        public string ProductCode { get; set; }

        [DataMember()]
        public string ProductName { get; set; }

        [DataMember()]
        public bool IsPOSProduct { get; set; }

        [DataMember()]
        public string ProductBillingGroupCodeName { get; set; }

        [DataMember()]
        public string ProductATCCode { get; set; }

        [DataMember()]
        public bool HasPathologies { get; set; }

        [DataMember()]
        public string IPSServiceGroupCodeName { get; set; }

        [DataMember()]
        public string MiPres { get; set; }

        [DataMember()]
        public bool IsItemProduction { get; set; }

        [DataMember()]
        public Byte IconType { get; set; }

        [DataMember()]
        public Byte FlagProductServiceDetail { get; set; }

        [DataMember()]
        public Byte ProductLiquidationType { get; set; }
    }
}
