Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.SchedulePayment")> _
Public Class TreasurySchedulePaymentXpo
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
    Dim fScheduledDate As DateTime
    Public Property ScheduledDate() As DateTime
        Get
            Return fScheduledDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ScheduledDate", fScheduledDate, value)
        End Set
    End Property
    Dim fEntityBankAccountId As TreasuryEntityBankAccountsXpo
    <Association("TreasurySchedulePaymentXpoReferencesTreasuryEntityBankAccountsXpo")> _
    Public Property EntityBankAccountId() As TreasuryEntityBankAccountsXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("TreasurySchedulePaymentXpoReferencesPayrollCostCenterXpo")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fPaymentMethod As Byte
    Public Property PaymentMethod() As Byte
        Get
            Return fPaymentMethod
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PaymentMethod", fPaymentMethod, value)
        End Set
    End Property
    Dim fCheckId As Integer
    Public Property CheckId() As Integer
        Get
            Return fCheckId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CheckId", fCheckId, value)
        End Set
    End Property
    Dim fNumberNote As String
    <Size(50)> _
    Public Property NumberNote() As String
        Get
            Return fNumberNote
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberNote", fNumberNote, value)
        End Set
    End Property
    Dim fTaxByMil As Boolean
    Public Property TaxByMil() As Boolean
        Get
            Return fTaxByMil
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("TaxByMil", fTaxByMil, value)
        End Set
    End Property
    Dim fOperativeUnitId As Integer
    Public Property OperativeUnitId() As Integer
        Get
            Return fOperativeUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperativeUnitId", fOperativeUnitId, value)
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
    Dim fPartialPaymentUser As String
    <Size(20)> _
    Public Property PartialPaymentUser() As String
        Get
            Return fPartialPaymentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PartialPaymentUser", fPartialPaymentUser, value)
        End Set
    End Property
    Dim fPartialPaymentDate As DateTime
    Public Property PartialPaymentDate() As DateTime
        Get
            Return fPartialPaymentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PartialPaymentDate", fPartialPaymentDate, value)
        End Set
    End Property
    Dim fFullPaymentUser As String
    <Size(20)> _
    Public Property FullPaymentUser() As String
        Get
            Return fFullPaymentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullPaymentUser", fFullPaymentUser, value)
        End Set
    End Property
    Dim fFullPaymentDate As DateTime
    Public Property FullPaymentDate() As DateTime
        Get
            Return fFullPaymentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FullPaymentDate", fFullPaymentDate, value)
        End Set
    End Property
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasurySchedulePaymentXpo", GetType(TreasurySchedulePaymentDetailXpo))> _
    Public ReadOnly Property TreasurySchedulePaymentDetailXpo() As XPCollection(Of TreasurySchedulePaymentDetailXpo)
        Get
            Return GetCollection(Of TreasurySchedulePaymentDetailXpo)("TreasurySchedulePaymentDetailXpo")
        End Get
    End Property
    <Association("TreasuryVoucherTransactionXpoReferencesTreasurySchedulePaymentXpo", GetType(TreasuryVoucherTransactionXpo))> _
    Public ReadOnly Property TreasuryVoucherTransactionXpo() As XPCollection(Of TreasuryVoucherTransactionXpo)
        Get
            Return GetCollection(Of TreasuryVoucherTransactionXpo)("TreasuryVoucherTransactionXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
