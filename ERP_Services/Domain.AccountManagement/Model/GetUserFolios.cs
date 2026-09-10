using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AccountManagement.Model
{
    public class GetUserFolios
    {
        // ====================================================================
        // INFORMACIÓN DEL INGRESO
        // ====================================================================

        /// <summary>
        /// Id secuencial generado por ROW_NUMBER
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Número de ingreso (NUMINGRES)
        /// </summary>
        public string AdmissionNumber { get; set; }

        /// <summary>
        /// Nombre completo del paciente
        /// </summary>
        public string PatientFullName { get; set; }

        /// <summary>
        /// Fecha de ingreso del paciente
        /// </summary>
        public DateTime AdmissionDate { get; set; }

        /// <summary>
        /// Código del paciente
        /// </summary>
        public string PatientCode { get; set; }

        /// <summary>
        /// Indica si el folio tiene alertas activas
        /// </summary>
        public bool HasFolioAlert { get; set; }

        /// <summary>
        /// Número de cama
        /// </summary>
        public string BedNumber { get; set; }

        /// <summary>
        /// Diagnóstico del paciente
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// Código del centro de atención
        /// </summary>
        public string AttentionCenterCode { get; set; }

        /// <summary>
        /// Usuario que creó el ingreso
        /// </summary>
        public string AdmissionCreationUser { get; set; }

        /// <summary>
        /// Usuario que modificó el ingreso
        /// </summary>
        public string AdmissionModificationUser { get; set; }

        // ====================================================================
        // INFORMACIÓN DEL FOLIO
        // ====================================================================

        /// <summary>
        /// Orden del folio (número de folio)
        /// </summary>
        public byte FolioNumber { get; set; }

        /// <summary>
        /// Valor total del folio
        /// </summary>
        public decimal FolioTotalValue { get; set; }

        /// <summary>
        /// Id del folio (RevenueControlDetail.Id)
        /// </summary>
        public int RevenueControlDetailId { get; set; }

        /// <summary>
        /// Tipo de folio: 'EAPB con contrato', 'EAPB sin contrato', 'Particulares', 'Aseguradoras'
        /// </summary>
        public string FolioType { get; set; }

        /// <summary>
        /// Estado del folio: 'Registrado', 'Facturado', 'Bloqueado', 'Anulado', etc.
        /// </summary>
        public string FolioStatusDescription { get; set; }

        // ====================================================================
        // INFORMACIÓN DE UNIDADES Y GRUPOS
        // ====================================================================

        /// <summary>
        /// Nombre de la unidad funcional
        /// </summary>
        public string FunctionalUnitName { get; set; }

        /// <summary>
        /// Nombre del grupo de atención (CareGroup)
        /// </summary>
        public string CareGroupName { get; set; }

        /// <summary>
        /// Id del grupo de atención (CareGroup)
        /// </summary>
        public int CareGroupId { get; set; }

        // ====================================================================
        // INFORMACIÓN DE ASIGNACIÓN Y USUARIO
        // ====================================================================

        /// <summary>
        /// Fecha de asignación al usuario o fecha de traslado efectivo
        /// </summary>
        public DateTime AssignmentDate { get; set; }

        /// <summary>
        /// Nombre completo del usuario asignado actualmente
        /// </summary>
        public string AssignedUserFullname { get; set; }

        /// <summary>
        /// Código del usuario asignado actualmente
        /// </summary>
        public string AssignedUserCode { get; set; }

        /// <summary>
        /// Nombre del área de gestión del usuario
        /// </summary>
        public string ManagementAreaName { get; set; }

        /// <summary>
        /// Código del usuario que es el owner actual del folio
        /// </summary>
        public string CurrentOwnerCode { get; set; }

        // ====================================================================
        // INFORMACIÓN DE FACTURACIÓN
        // ====================================================================

        /// <summary>
        /// Usuario que facturó
        /// </summary>
        public string InvoiceUser { get; set; }

        /// <summary>
        /// Número de factura asociada
        /// </summary>
        public string AssociatedInvoice { get; set; }

        /// <summary>
        /// Indica si está a tiempo (1-3, actualmente siempre 3)
        /// </summary>
        public int IsOnTime { get; set; }

        // ====================================================================
        // INFORMACIÓN DE TRASLADOS
        // ====================================================================

        /// <summary>
        /// Estado del traslado: 'Sin traslado', 'Pendiente de aceptación', 'Aceptada', 'Rechazada'
        /// </summary>
        public string TransferStatus { get; set; }

        /// <summary>
        /// Código del usuario anterior en el traslado (quien trasladó)
        /// </summary>
        public string TransferPreviousUser { get; set; }

        /// <summary>
        /// Código del usuario receptor en el traslado
        /// </summary>
        public string TransferReceivingUser { get; set; }

        /// <summary>
        /// Indica si el folio está pendiente de aceptar por el usuario actual
        /// </summary>
        public bool IsPendingToAccept { get; set; }

        /// <summary>
        /// Indica si el folio fue rechazado y volvió al usuario actual
        /// </summary>
        public bool WasRejected { get; set; }

        /// <summary>
        /// Indica si el usuario actual trasladó el folio y está pendiente
        /// </summary>
        public bool IsPendingFromMe { get; set; }

        // ====================================================================
        // INFORMACIÓN DE TIPO DE INGRESO
        // ====================================================================

        /// <summary>
        /// Descripción del tipo de ingreso: 'Ambulatorio' o 'Hospitalario'
        /// </summary>
        public string EntryTypeDescription { get; set; }

        /// <summary>
        /// Tipo de ingreso: 1=Ambulatorio, 2=Hospitalario
        /// </summary>
        public byte EntryType { get; set; }
    }
}
