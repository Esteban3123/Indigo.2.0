Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VReportTreasuryNewsletterSummary")> _
Public Class TreasuryVReportTreasuryNewsletterSummary
    Inherits XPLiteObject


#Region "Properties"

    Dim fRow As String
    <Key(True)>
    Public Property Row() As String
        Get
            Return fRow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Row", fRow, value)
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

    Dim fCashRegisterCode As String
    Public Property CashRegisterCode() As String
        Get
            Return fCashRegisterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CashRegisterCode", fCashRegisterCode, value)
        End Set
    End Property

    Dim fNameCash As String
    Public Property NameCash() As String
        Get
            Return fNameCash
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameCash", fNameCash, value)
        End Set
    End Property

    Dim fCashRegisterStatus As Boolean
    Public Property CashRegisterStatus() As Boolean
        Get
            Return fCashRegisterStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CashRegisterStatus", fCashRegisterStatus, value)
        End Set
    End Property

    Dim fEntityBankAccountId As Integer
    Public Property EntityBankAccountId() As Integer
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fEntityBankAccountCode As String
    Public Property EntityBankAccountCode() As String
        Get
            Return fEntityBankAccountCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityBankAccountCode", fEntityBankAccountCode, value)
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

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property

    Dim fNumber As String
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property

    Dim fEntityBankStatus As Boolean
    Public Property EntityBankStatus() As Boolean
        Get
            Return fEntityBankStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("EntityBankStatus", fEntityBankStatus, value)
        End Set
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDocumentDate As Date
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fMainAccount As String
    Public Property MainAccount() As String
        Get
            Return fMainAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccount", fMainAccount, value)
        End Set
    End Property

    Dim fSumReceipts As Decimal
    Public Property SumReceipts() As Decimal
        Get
            Return fSumReceipts
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SumReceipts", fSumReceipts, value)
        End Set
    End Property

    Dim fSumExpenditures As Decimal
    Public Property SumExpenditures() As Decimal
        Get
            Return fSumExpenditures
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SumExpenditures", fSumExpenditures, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fCurrencyAbbreviation As String
    Public Property CurrencyAbbreviation() As String
        Get
            Return fCurrencyAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyAbbreviation", fCurrencyAbbreviation, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    Dim fNumberNameCash As String
    <PersistentAlias("concat(CodeCash,' - ',NameCash)")>
    Public ReadOnly Property NumberNameCash() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberNameCash"))
        End Get
    End Property

    Dim fCodeName As String
    <PersistentAlias("concat(EntityBankAccountCode,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fSaldoAnterior As Decimal
    <NonPersistent()>
    Public Property SaldoAnterior() As Decimal
        Get
            Return fSaldoAnterior
        End Get
        Set(ByVal value As Decimal)
            Me.fSaldoAnterior = value
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
