using System;
using System.Runtime.Serialization;

namespace Domain.AccountManagement.Model
{
    [DataContract]
    public class VFolioTraceabilityProperties
    {
        [DataMember]
        public int Id { get; set; }

        // Información del ingreso
        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string PatientFullName { get; set; }

        [DataMember]
        public DateTime AdmissionDate { get; set; }

        [DataMember]
        public string PatientCode { get; set; }

        [DataMember]
        public string AttentionCenterCode { get; set; }

        [DataMember]
        public string BedNumber { get; set; }

        [DataMember]
        public string Diagnosis { get; set; }

        [DataMember]
        public string FunctionalUnitName { get; set; }


        // Información del folio
        [DataMember]
        public byte FolioNumber { get; set; }

        [DataMember]
        public decimal FolioTotalValue { get; set; }

        [DataMember]
        public int RevenueControlDetailId { get; set; }

        [DataMember]
        public string CareGroupName { get; set; }

        [DataMember]
        public string FolioType { get; set; }

        [DataMember]
        public string FolioStatusDescription { get; set; }

        // Información del evento/traslado
        [DataMember]
        public DateTime EventDate { get; set; }

        [DataMember]
        public string TransferStatus { get; set; }

        [DataMember]
        public int TransferOrder { get; set; }

        // Usuarios involucrados
        [DataMember]
        public string CurrentUserCode { get; set; }

        [DataMember]
        public string CurrentUserCodeName { get; set; }

        [DataMember]
        public string PreviousUserCode { get; set; }

        [DataMember]
        public string PreviousUserCodeName { get; set; }

        [DataMember]
        public string ReceivingUserCode { get; set; }

        [DataMember]
        public string ReceivingUserCodeName { get; set; }

        // Áreas de gestión
        [DataMember]
        public int? PreviousManagementAreaId { get; set; }

        [DataMember]
        public string PreviousManagementAreaName { get; set; }

        [DataMember]
        public int? ReceivingManagementAreaId { get; set; }

        [DataMember]
        public string ReceivingManagementAreaName { get; set; }

        // Información adicional
        [DataMember]
        public string AdmissionCreationUser { get; set; }

        [DataMember]
        public string AdmissionModificationUser { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string Comments { get; set; }
    }
}
