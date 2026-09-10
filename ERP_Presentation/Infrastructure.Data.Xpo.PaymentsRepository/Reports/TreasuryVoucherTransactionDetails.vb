Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VoucherTransactionDetails")> _
Public Class TreasuryVoucherTransactionDetails
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
    Dim fIdVoucherTransaction As TreasuryVoucherTransaction
    <Association("TreasuryVoucherTransactionDetailsReferencesTreasuryVoucherTransaction")> _
    Public Property IdVoucherTransaction() As TreasuryVoucherTransaction
        Get
            Return fIdVoucherTransaction
        End Get
        Set(ByVal value As TreasuryVoucherTransaction)
            SetPropertyValue(Of TreasuryVoucherTransaction)("IdVoucherTransaction", fIdVoucherTransaction, value)
        End Set
    End Property
    Dim fIdEntityBankAccount As Integer
    Public Property IdEntityBankAccount() As Integer
        Get
            Return fIdEntityBankAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEntityBankAccount", fIdEntityBankAccount, value)
        End Set
    End Property
    Dim fCashRegisterId As Integer
    Public Property CashRegisterId() As Integer
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("TreasuryVoucherTransactionDetailsReferencesCommonThirdPartyXpo")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdExpenseConcept As Integer
    Public Property IdExpenseConcept() As Integer
        Get
            Return fIdExpenseConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdExpenseConcept", fIdExpenseConcept, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("TreasuryVoucherTransactionDetailsReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fIdCostCenter As Integer
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fIdRetentionConcept As Integer
    Public Property IdRetentionConcept() As Integer
        Get
            Return fIdRetentionConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConcept", fIdRetentionConcept, value)
        End Set
    End Property
    Dim fPercentRetention As Decimal
    Public Property PercentRetention() As Decimal
        Get
            Return fPercentRetention
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentRetention", fPercentRetention, value)
        End Set
    End Property
    Dim fDetail As String
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property
    <Association("TreasuryDischargeBillReferencesTreasuryVoucherTransactionDetails", GetType(TreasuryDischargeBill))> _
    Public ReadOnly Property TreasuryDischargeBill() As XPCollection(Of TreasuryDischargeBill)
        Get
            Return GetCollection(Of TreasuryDischargeBill)("TreasuryDischargeBill")
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
