Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.CareGroup")> _
Public Class ContractCareGroupXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fCareGroupType As Byte
    Public Property CareGroupType() As Byte
        Get
            Return fCareGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CareGroupType", fCareGroupType, value)
        End Set
    End Property
    Dim fDefaultManual As Byte
    Public Property DefaultManual() As Byte
        Get
            Return fDefaultManual
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DefaultManual", fDefaultManual, value)
        End Set
    End Property
    Dim fContractId As ContractContractXpo
    <Association("Contract_CareGroupReferencesContract_Contract")> _
    Public Property ContractId() As ContractContractXpo
        Get
            Return fContractId
        End Get
        Set(ByVal value As ContractContractXpo)
            SetPropertyValue(Of ContractContractXpo)("ContractId", fContractId, value)
        End Set
    End Property
    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LiquidationType", fLiquidationType, value)
        End Set
    End Property
    Dim fBillingPeriod As Byte
    Public Property BillingPeriod() As Byte
        Get
            Return fBillingPeriod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("BillingPeriod", fBillingPeriod, value)
        End Set
    End Property
    Dim fMaximumIndividualBilling As Decimal
    Public Property MaximumIndividualBilling() As Decimal
        Get
            Return fMaximumIndividualBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaximumIndividualBilling", fMaximumIndividualBilling, value)
        End Set
    End Property
    Dim fPeriodMaximumBilling As Decimal
    Public Property PeriodMaximumBilling() As Decimal
        Get
            Return fPeriodMaximumBilling
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PeriodMaximumBilling", fPeriodMaximumBilling, value)
        End Set
    End Property
    Dim fTypeLiquidationEmergencyStays As Byte
    Public Property TypeLiquidationEmergencyStays() As Byte
        Get
            Return fTypeLiquidationEmergencyStays
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TypeLiquidationEmergencyStays", fTypeLiquidationEmergencyStays, value)
        End Set
    End Property
    Dim fMinimumObservationTime As Byte
    Public Property MinimumObservationTime() As Byte
        Get
            Return fMinimumObservationTime
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MinimumObservationTime", fMinimumObservationTime, value)
        End Set
    End Property
    Dim fMaximumObservationTime As Byte
    Public Property MaximumObservationTime() As Byte
        Get
            Return fMaximumObservationTime
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MaximumObservationTime", fMaximumObservationTime, value)
        End Set
    End Property
    Dim fHoursOfRecoveryIncluded As Byte
    Public Property HoursOfRecoveryIncluded() As Byte
        Get
            Return fHoursOfRecoveryIncluded
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("HoursOfRecoveryIncluded", fHoursOfRecoveryIncluded, value)
        End Set
    End Property
    Dim fRequirementsTemplateId As Integer
    Public Property RequirementsTemplateId() As Integer
        Get
            Return fRequirementsTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequirementsTemplateId", fRequirementsTemplateId, value)
        End Set
    End Property
    Dim fInvoiceDeadlines As Integer
    Public Property InvoiceDeadlines() As Integer
        Get
            Return fInvoiceDeadlines
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDeadlines", fInvoiceDeadlines, value)
        End Set
    End Property
    Dim fProcedureTemplateId As Integer
    Public Property ProcedureTemplateId() As Integer
        Get
            Return fProcedureTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProcedureTemplateId", fProcedureTemplateId, value)
        End Set
    End Property
    Dim fProductRateId As Integer
    Public Property ProductRateId() As Integer
        Get
            Return fProductRateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductRateId", fProductRateId, value)
        End Set
    End Property
    Dim fConceptToBill As Byte
    Public Property ConceptToBill() As Byte
        Get
            Return fConceptToBill
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptToBill", fConceptToBill, value)
        End Set
    End Property
    Dim fEntityType As Byte
    Public Property EntityType() As Byte
        Get
            Return fEntityType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("EntityType", fEntityType, value)
        End Set
    End Property
    Dim fAccountRecoveryFeeId As Integer
    Public Property AccountRecoveryFeeId() As Integer
        Get
            Return fAccountRecoveryFeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountRecoveryFeeId", fAccountRecoveryFeeId, value)
        End Set
    End Property
    Dim fAccountParticularId As Integer
    Public Property AccountParticularId() As Integer
        Get
            Return fAccountParticularId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountParticularId", fAccountParticularId, value)
        End Set
    End Property
    Dim fAccountWithoutRadicateId As Integer
    Public Property AccountWithoutRadicateId() As Integer
        Get
            Return fAccountWithoutRadicateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountWithoutRadicateId", fAccountWithoutRadicateId, value)
        End Set
    End Property
    Dim fAccountRadicateId As Integer
    Public Property AccountRadicateId() As Integer
        Get
            Return fAccountRadicateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountRadicateId", fAccountRadicateId, value)
        End Set
    End Property
    Dim fAccountObjectionRemediedId As Integer
    Public Property AccountObjectionRemediedId() As Integer
        Get
            Return fAccountObjectionRemediedId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountObjectionRemediedId", fAccountObjectionRemediedId, value)
        End Set
    End Property
    Dim fAccountConciliationId As Integer
    Public Property AccountConciliationId() As Integer
        Get
            Return fAccountConciliationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountConciliationId", fAccountConciliationId, value)
        End Set
    End Property
    Dim fAccountLegalCollectionId As Integer
    Public Property AccountLegalCollectionId() As Integer
        Get
            Return fAccountLegalCollectionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountLegalCollectionId", fAccountLegalCollectionId, value)
        End Set
    End Property
    Dim fAccountDebtorOrder As Integer
    Public Property AccountDebtorOrder() As Integer
        Get
            Return fAccountDebtorOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountDebtorOrder", fAccountDebtorOrder, value)
        End Set
    End Property
    Dim fAccountCreditorOrder As Integer
    Public Property AccountCreditorOrder() As Integer
        Get
            Return fAccountCreditorOrder
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountCreditorOrder", fAccountCreditorOrder, value)
        End Set
    End Property
    Dim fAffectedService As Boolean
    Public Property AffectedService() As Boolean
        Get
            Return fAffectedService
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AffectedService", fAffectedService, value)
        End Set
    End Property
    Dim fDevolutionInjustificate As Byte
    Public Property DevolutionInjustificate() As Byte
        Get
            Return fDevolutionInjustificate
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DevolutionInjustificate", fDevolutionInjustificate, value)
        End Set
    End Property
    Dim fGeneralGlossConceptNoteId As Integer
    Public Property GeneralGlossConceptNoteId() As Integer
        Get
            Return fGeneralGlossConceptNoteId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GeneralGlossConceptNoteId", fGeneralGlossConceptNoteId, value)
        End Set
    End Property
    Dim fDetailedGlossConceptNoteId As Integer
    Public Property DetailedGlossConceptNoteId() As Integer
        Get
            Return fDetailedGlossConceptNoteId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DetailedGlossConceptNoteId", fDetailedGlossConceptNoteId, value)
        End Set
    End Property
    Dim fPreviousLifetimesConceptNoteId As Integer
    Public Property PreviousLifetimesConceptNoteId() As Integer
        Get
            Return fPreviousLifetimesConceptNoteId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PreviousLifetimesConceptNoteId", fPreviousLifetimesConceptNoteId, value)
        End Set
    End Property
    Dim fRadicationJournalVoucherTypeId As Integer
    Public Property RadicationJournalVoucherTypeId() As Integer
        Get
            Return fRadicationJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicationJournalVoucherTypeId", fRadicationJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fReceptionObjectionJournalVoucherTypeId As Integer
    Public Property ReceptionObjectionJournalVoucherTypeId() As Integer
        Get
            Return fReceptionObjectionJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReceptionObjectionJournalVoucherTypeId", fReceptionObjectionJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fConciliationJournalVoucherTypeId As Integer
    Public Property ConciliationJournalVoucherTypeId() As Integer
        Get
            Return fConciliationJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConciliationJournalVoucherTypeId", fConciliationJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fDevolutionJournalVoucherTypeId As Integer
    Public Property DevolutionJournalVoucherTypeId() As Integer
        Get
            Return fDevolutionJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DevolutionJournalVoucherTypeId", fDevolutionJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fTransferLegalJournalVoucherTypeId As Integer
    Public Property TransferLegalJournalVoucherTypeId() As Integer
        Get
            Return fTransferLegalJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TransferLegalJournalVoucherTypeId", fTransferLegalJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
