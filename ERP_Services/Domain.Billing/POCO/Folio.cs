using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Billing.POCO
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    [Serializable()]
    [KnownType(typeof(Folio))]
    public class Folio
    {
        [DataMember()]
        public int RevenueControlDetailId { get; set; }

        [DataMember()]
        public string AdmissionNumber { get; set; }

        [DataMember()]
        public int? BillingAuthorizationId { get; set; }

        [DataMember()]
        public int ThirdPartyId { get; set; }

        [DataMember()]
        public string ThirdPartyNitName { get; set; }

        [DataMember()]
        public int CareGroupId { get; set; }

        [DataMember()]
        public byte CaregroupEntityType { get; set; }

        [DataMember()]
        public string CareGroupCodeName { get; set; }

        [DataMember()]
        public int CareGroupCostCenterId { get; set; }

        [DataMember()]
        public byte FolioType { get; set; }

        [DataMember()]
        public string Observation { get; set; }

        [DataMember()]
        public string TotalFolio { get; set; }

        [DataMember()]
        public byte ResponsibleRecoveryFee { get; set; }

        [DataMember()]
        public decimal PatientDiscountPercentage { get; set; }

        [DataMember()]
        public decimal PatientDiscount { get; set; }

        [DataMember()]
        public decimal TotalPatientSalesPrice { get; set; }

        [DataMember()]
        public decimal TotalPatientWithDiscount { get; set; }

        [DataMember()]
        public decimal VoucherValue { get; set; }

        [DataMember()]
        public byte Status { get; set; }

        [DataMember()]
        public int? StatusFolioId { get; set; }

        [DataMember()]
        public byte LiquidationType { get; set; }

        [DataMember()]
        public byte FolioOrder { get; set; }

        [DataMember()]
        public int? ContractId { get; set; }

        [DataMember()]
        public string ContractEntityCodeName { get; set; }

        [DataMember()]
        public int? InvoiceCategoryId { get; set; }

        [DataMember()]
        public string PatientCode { get; set; }

        [DataMember()]
        public int? ContractEntityId { get; set; }

        [DataMember()]
        public string ContractCodeName { get; set; }

        [DataMember()]
        public string InvoiceCategoryCodeName { get; set; }

        [DataMember()]
        public string AdmissionNumberPatient { get; set; }

        [DataMember()]
        public int ThirdPartyPatientId { get; set; }

        [DataMember()]
        public int? HealthAdministratorId { get; set; }

        [DataMember()]
        public string HealthAdministratorCodeName { get; set; }

        [DataMember()]
        public int? ThirdPartyHealthAdministrator { get; set; }

        [DataMember()]
        public int? InvoiceId { get; set; }

        [DataMember()]
        public string InvoiceNumber { get; set; }

        [DataMember()]
        public DateTime InvoiceDate { get; set; }

        [DataMember()]
        public string InvoicedUser { get; set; }

        [DataMember()]
        public string StatusFolioName { get; set; }

        [DataMember()]
        public string ThirdPartyResponsibleQuotaNitName { get; set; }

        [DataMember()]
        public int? PatientQuotaResponsibleThirdPartyId { get; set; }

        [DataMember()]
        public List<FolioDetail> FolioDetails { get; set; }

        public Folio()
        {
            this.FolioDetails = new List<FolioDetail>();
        }
    }
}
