using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Billing.POCO
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    [Serializable()]
    [KnownType(typeof(InvoicePartialMasterAccount))]
    public class InvoicePartialMasterAccount
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string InvoiceCategory { get; set; }

        [DataMember()]
        public string NitWithOutDig { get; set; }

        [DataMember()]
        public string CustomerName { get; set; }

        [DataMember()]
        public string CustomerAddress { get; set; }

        [DataMember()]
        public string CustomerPhone { get; set; }

        [DataMember()]
        public string CareGroup { get; set; }

        [DataMember()]
        public byte CareGroupType { get; set; }

        [DataMember()]
        public string HealthEntityCode { get; set; }

        [DataMember()]
        public string DescriptionHealthAdministrator { get; set; }

        [DataMember()]
        public string Contract { get; set; }

        [DataMember()]
        public byte? PrintingMode { get; set; }

        [DataMember()]
        public int? Term { get; set; }

        [DataMember()]
        public string PatientCode { get; set; }

        [DataMember()]
        public string IdentificationName { get; set; }

        [DataMember()]
        public string PatientName { get; set; }

        [DataMember()]
        public string PatientFirstName { get; set; }

        [DataMember()]
        public string PatientSecondName { get; set; }

        [DataMember()]
        public string PatientFirstLastName { get; set; }

        [DataMember()]
        public string PatientSecondLastName { get; set; }

        [DataMember()]
        public int PatientType { get; set; }

        [DataMember()]
        public int AffiliateType { get; set; }

        [DataMember()]
        public string PatientLevel { get; set; }

        [DataMember()]
        public string PatientAddress { get; set; }

        [DataMember()]
        public string PatientTelephoneNumber { get; set; }

        [DataMember()]
        public string PatientPhoneMovil { get; set; }

        [DataMember()]
        public string PatientEmail { get; set; }

        [DataMember()]
        public string PatientAge { get; set; }

        [DataMember()]
        public int AdmissionType { get; set; }

        [DataMember()]
        public string AdmissionNumber { get; set; }

        [DataMember()]
        public DateTime AdmissionDate { get; set; }

        [DataMember()]
        public DateTime? EgressDate { get; set; }

        [DataMember()]
        public string AuthorizationNumber { get; set; }

        [DataMember()]
        public DateTime? ProcessingDate { get; set; }

        [DataMember()]
        public string ProcessLine { get; set; }

        [DataMember()]
        public string PacientEntity { get; set; }

        [DataMember()]
        public string PacientRegimen { get; set; }

        [DataMember()]
        public string AffiliateStatus { get; set; }

        [DataMember()]
        public string ERPConfirm { get; set; }

        [DataMember()]
        public string IPSReport { get; set; }

        [DataMember()]
        public string CODCENATE { get; set; }

        [DataMember()]
        public string UFUIGRMED { get; set; }

        [DataMember()]
        public string UFUEGRMED { get; set; }

        [DataMember()]
        public byte DocumentType { get; set; }

        [DataMember()]
        public string OutputDiagnosis { get; set; }

        [DataMember()]
        public decimal SubTotalService { get; set; }

        [DataMember()]
        public decimal ThirdPartyDiscountValue { get; set; }

        [DataMember()]
        public decimal TotalPatientSalesPrice { get; set; }

        [DataMember()]
        public decimal PatientDiscount { get; set; }

        [DataMember()]
        public decimal ThirdPartySalesValue { get; set; }

        [DataMember()]
        public string UserCode { get; set; }

        [DataMember()]
        public string UserFullName { get; set; }

        [DataMember()]
        public byte Status { get; set; }

        [DataMember()]
        public decimal CoinsuranceInsurance { get; set; }

        [DataMember()]
        public decimal CopaymentValueInsurance { get; set; }

        [DataMember()]
        public decimal DeductibleValueInsurance { get; set; }

        [DataMember()]
        public decimal PatientCoinsurance { get; set; }

        [DataMember()]
        public decimal PatientCopaymentValue { get; set; }

        [DataMember()]
        public decimal PatientDeductibleValue { get; set; }

        [DataMember()]
        public byte IsMasterAccount { get; set; }

        [DataMember()]
        public decimal DataDiscountPercentage { get; set; }

        [DataMember()]
        public string CenterAttentionCode { get; set; }

        [DataMember()]
        public string CenterAttentionAddress { get; set; }

        [DataMember()]
        public string CenterAttentionPhone { get; set; }

        [DataMember()]
        public DateTime CreationDate { get; set; }

        [DataMember()]
        public int? CurrencyId { get; set; }

        [DataMember()]
        public string CurrencyAbbreviation { get; set; }

        [DataMember()]
        public string CurrencyName { get; set; }

        [DataMember()]
        public decimal GrandTotalSalesPrice { get; set; }

        [DataMember()]
        public decimal GrandTotalDiscount { get; set; }

        [DataMember()]
        public decimal TRMValue { get; set; }

        [DataMember()]
        public decimal TotalPatientWithDiscount { get; set; }

        [DataMember()]
        public List<InvoicePartialDetailMasterAccount> InvoicePartialDetail { get; set; }

        [DataMember()]
        public List<TaxDevolution> TaxDevolution { get; set; }

        public InvoicePartialMasterAccount()
        {
            this.InvoicePartialDetail = new List<InvoicePartialDetailMasterAccount>();
            this.TaxDevolution = new List<TaxDevolution>();
        }
    }
}
