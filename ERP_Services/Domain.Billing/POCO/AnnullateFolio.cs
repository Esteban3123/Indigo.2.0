using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Billing.POCO
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    [Serializable()]
    [KnownType(typeof(AnnullateFolio))]
    public class AnnullateFolio
    {
        [DataMember()]
        public Int32 InvoiceId { get; set; }

        [DataMember()]
        public String Observation { get; set; }

        [DataMember()]
        public Int32 InvoiceCategoryId { get; set; }

        [DataMember()]
        public DateTime InvoiceDate { get; set; }

        [DataMember()]
        public String InvoicedUser { get; set; }

        [DataMember()]
        public String InvoiceCategoryCodeName { get; set; }

        [DataMember()]
        public String AdmissionNumber { get; set; }

        [DataMember()]
        public String InvoiceNumber { get; set; }

        [DataMember()]
        public Byte FolioType { get; set; }

        [DataMember()]
        public Int32 HealthAdministratorId { get; set; }

        [DataMember()]
        public String HealthAdministratorCodeName { get; set; }

        [DataMember()]
        public Int32 ThirdPartyId { get; set; }

        [DataMember()]
        public String ThirdPartyNitName { get; set; }

        [DataMember()]
        public Int32 CareGroupId { get; set; }

        [DataMember()]
        public Int32 CaregroupEntityType { get; set; }

        [DataMember()]
        public String CareGroupCodeName { get; set; }

        [DataMember()]
        public Decimal TotalFolio { get; set; }

        [DataMember()]
        public Byte ResponsibleRecoveryFee { get; set; }

        [DataMember()]
        public Decimal PatientDiscountPercentage { get; set; }

        [DataMember()]
        public Decimal PatientDiscount { get; set; }

        [DataMember()]
        public Decimal TotalPatientSalesPrice { get; set; }

        [DataMember()]
        public Decimal TotalPatientWithDiscount { get; set; }

        [DataMember()]
        public Decimal VoucherValue { get; set; }

        [DataMember()]
        public Int32 Status { get; set; }

        [DataMember()]
        public Int32 ThirdPartyPatientId { get; set; }

        [DataMember()]
        public List<FolioDetail> FolioDetails { get; set; }

        public AnnullateFolio()
        {
            this.FolioDetails = new List<FolioDetail>();
        }
    }
}
