using System;
using System.Runtime.Serialization;

namespace Domain.AccountManagement.Model
{

    [DataContract]
    public class VDashboardProperties
    {
        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string PatientFullName { get; set; }

        [DataMember]
        public DateTime AdmissionDate { get; set; }

        [DataMember]
        public string PatientCode { get; set; }

        [DataMember]
        public string FunctionalUnitName { get; set; }

        [DataMember]
        public string CareGroupName { get; set; }

        [DataMember]
        public string BedNumber { get; set; }

        [DataMember]
        public string Diagnosis { get; set; }

        [DataMember]
        public byte FolioNumber { get; set; }

        [DataMember]
        public decimal FolioTotalValue { get; set; }

        [DataMember]
        public string FolioType { get; set; }

        [DataMember]
        public string FolioStatusDescription { get; set; }

        [DataMember]
        public string ReceivingUser { get; set; }

        [DataMember]
        public string ReceivingUserCodeName { get; set; }

        [DataMember]
        public string AdmissionCreationUser { get; set; }

        [DataMember]
        public string AdmissionModificationUser { get; set; }

        [DataMember]
        public string TransferStatus { get; set; }

        [DataMember]
        public int TimeStatus { get; set; }

        [DataMember]
        public string PreviousUser { get; set; }

        [DataMember]
        public string PreviousUserCodeName { get; set; }

        [DataMember]
        public string PreviousManagementArea { get; set; }

        [DataMember]
        public string PreviousManagementAreaCode { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string AdmissionManagementArea { get; set; }

        [DataMember]
        public DateTime TransferDate { get; set; }

        [DataMember]
        public string InvoiceUser { get; set; }

        [DataMember]
        public string FolioTransferId { get; set; }
    }
}
