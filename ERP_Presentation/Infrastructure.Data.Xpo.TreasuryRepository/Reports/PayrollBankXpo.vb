Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.Bank")> _
Public Class PayrollBankXpo
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
    '<Indexed("IX_Corporation")> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("PayrollBankXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fName As String
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fAchCode As String
    <Size(10)> _
    Public Property AchCode() As String
        Get
            Return fAchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AchCode", fAchCode, value)
        End Set
    End Property
    Dim fBankFileCode As String
    <Size(3)> _
    Public Property BankFileCode() As String
        Get
            Return fBankFileCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankFileCode", fBankFileCode, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    <PersistentAlias("concat(Code,' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("TreasuryEntityBankAccountsXpoReferencesPayrollBankXpo", GetType(TreasuryEntityBankAccountsXpo))> _
    Public ReadOnly Property TreasuryEntityBankAccountsXpo() As XPCollection(Of TreasuryEntityBankAccountsXpo)
        Get
            Return GetCollection(Of TreasuryEntityBankAccountsXpo)("TreasuryEntityBankAccountsXpo")
        End Get
    End Property

    <Association("TreasuryPaymentMethodsXpoReferencesPayrollBankXpo", GetType(TreasuryPaymentMethodsXpo))> _
    Public ReadOnly Property TreasuryPaymentMethodsXpo() As XPCollection(Of TreasuryPaymentMethodsXpo)
        Get
            Return GetCollection(Of TreasuryPaymentMethodsXpo)("TreasuryPaymentMethodsXpo")
        End Get
    End Property

    <Association("TreasuryPaymentMethodsXpo_References_SupplierBankAccountXpo", GetType(SupplierBankAccountXpo))>
    Public ReadOnly Property SupplierBankAccountXpo() As XPCollection(Of SupplierBankAccountXpo)
        Get
            Return GetCollection(Of SupplierBankAccountXpo)("SupplierBankAccountXpo")
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
