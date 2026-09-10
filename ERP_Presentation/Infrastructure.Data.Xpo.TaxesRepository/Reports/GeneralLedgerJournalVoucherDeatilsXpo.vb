Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("GeneralLedger.JournalVoucherDetails")> _
Public Class GeneralLedgerJournalVoucherDeatilsXpo
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
    Dim fIdAccounting As GeneralLedgerJournalVouchersXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_JournalVouchers")> _
    Public Property IdAccounting() As GeneralLedgerJournalVouchersXpo
        Get
            Return fIdAccounting
        End Get
        Set(ByVal value As GeneralLedgerJournalVouchersXpo)
            SetPropertyValue(Of GeneralLedgerJournalVouchersXpo)("IdAccounting", fIdAccounting, value)
        End Set
    End Property
    Dim fIdMainAccount As GeneralLedgerMainAccountsXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesGeneralLedger_MainAccounts")> _
    Public Property IdMainAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property
    Dim fIdThirdParty As CommonThirdPartyXpo
    <Association("GeneralLedger_JournalVoucherDetailsReferencesCommon_ThirdParty")> _
    Public Property IdThirdParty() As CommonThirdPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("IdThirdParty", fIdThirdParty, value)
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
    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property
    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(500)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property
    Dim fIdRetention As GeneralLedgerRetentionConceptsReportXpo
    <Association("GeneralLedger_MainAccountsReferencesGeneralLedger_RetentionConcepts")> _
    Public Property IdRetention() As GeneralLedgerRetentionConceptsReportXpo
        Get
            Return fIdRetention
        End Get
        Set(ByVal value As GeneralLedgerRetentionConceptsReportXpo)
            SetPropertyValue(Of GeneralLedgerRetentionConceptsReportXpo)("IdRetention", fIdRetention, value)
        End Set
    End Property
    Dim fRetentionRate As Decimal
    Public Property RetentionRate() As Decimal
        Get
            Return fRetentionRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionRate", fRetentionRate, value)
        End Set
    End Property
    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property
    Dim fBillingValue As Decimal
    Public Property BillingValue() As Decimal
        Get
            Return fBillingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillingValue", fBillingValue, value)
        End Set
    End Property

    'Propiedad Añadida valor total en letras
    Dim fValueTotal As Decimal
    <NonPersistent()> _
    Public Property ValueTotal() As Decimal
        Get
            Return fValueTotal
        End Get
        Set(ByVal value As Decimal)
            Me.fValueTotal = value
        End Set
    End Property

    'Propiedad Añadida valor total en letras
    Dim fValueLetters As String
    <NonPersistent()> _
    Public Property ValueLetters() As String
        Get
            Return fValueLetters
        End Get
        Set(ByVal value As String)
            Me.fValueLetters = value
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
