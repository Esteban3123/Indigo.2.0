Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.PaymentTransfer")> _
Public Class PaymentsPaymentTransfer
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
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PaymentsPaymentTransferReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fSupplierId As Maintenance_Supplier
    <Association("PaymentsPaymentTransferReferencesMaintenance_Supplier")> _
    Public Property SupplierId() As Maintenance_Supplier
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property
    Dim fTransferType As Byte
    Public Property TransferType() As Byte
        Get
            Return fTransferType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TransferType", fTransferType, value)
        End Set
    End Property
    Dim fAdvancePaymentId As AdvancePaymentsXpo
    <Association("PaymentsPaymentTransferReferencesPaymentsAdvancePaymentsXpo")>
    Public Property AdvancePaymentId() As AdvancePaymentsXpo
        Get
            Return fAdvancePaymentId
        End Get
        Set(ByVal value As AdvancePaymentsXpo)
            SetPropertyValue(Of AdvancePaymentsXpo)("AdvancePaymentId", fAdvancePaymentId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PaymentsPaymentTransferReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpoP
    <Association("PaymentsPaymentTransferReferencesPayrollCostCenterXpoP")> _
    Public Property CostCenterId() As PayrollCostCenterXpoP
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
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
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property
    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()>
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    <Association("PaymentsPaymentTransferDetailReferencesPaymentsPaymentTransfer", GetType(PaymentsPaymentTransferDetail))> _
    Public ReadOnly Property PaymentsPaymentTransferDetail() As XPCollection(Of PaymentsPaymentTransferDetail)
        Get
            Return GetCollection(Of PaymentsPaymentTransferDetail)("PaymentsPaymentTransferDetail")
        End Get
    End Property
    <Association("Payments_PaymentTransferOtherConceptReferencesPayments_PaymentTransfer", GetType(PaymentsPaymentTransferOtherConcept))> _
    Public ReadOnly Property PaymentsPaymentTransferOtherConcept() As XPCollection(Of PaymentsPaymentTransferOtherConcept)
        Get
            Return GetCollection(Of PaymentsPaymentTransferOtherConcept)("PaymentsPaymentTransferOtherConcept")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
