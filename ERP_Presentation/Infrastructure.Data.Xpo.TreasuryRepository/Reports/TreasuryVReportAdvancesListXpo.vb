Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VReportAdvancesList")> _
Public Class TreasuryVReportAdvancesListXpo
    Inherits XPLiteObject
    Dim fRow As Integer
    <Key(True)> _
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property
    Dim fThirdPartyNit As String
    <Size(15)> _
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property
    Dim fThirdPartyName As String
    <Size(100)> _
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property
    Dim fAccountNumber As String
    <Size(50)> _
    Public Property AccountNumber() As String
        Get
            Return fAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AccountNumber", fAccountNumber, value)
        End Set
    End Property
    Dim fAccountName As String
    <Size(100)> _
    Public Property AccountName() As String
        Get
            Return fAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AccountName", fAccountName, value)
        End Set
    End Property
    Dim fBillValueInitial As Decimal
    Public Property BillValueInitial() As Decimal
        Get
            Return fBillValueInitial
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillValueInitial", fBillValueInitial, value)
        End Set
    End Property
    Dim fBillCurrentBalance As Decimal
    Public Property BillCurrentBalance() As Decimal
        Get
            Return fBillCurrentBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillCurrentBalance", fBillCurrentBalance, value)
        End Set
    End Property
    Dim fAdvanceId As Integer
    Public Property AdvanceId() As Integer
        Get
            Return fAdvanceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AdvanceId", fAdvanceId, value)
        End Set
    End Property
    Dim fMovesCode As String
    <Size(20)> _
    Public Property MovesCode() As String
        Get
            Return fMovesCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MovesCode", fMovesCode, value)
        End Set
    End Property
    Dim fAdvanceCode As String
    <Size(20)> _
    Public Property AdvanceCode() As String
        Get
            Return fAdvanceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdvanceCode", fAdvanceCode, value)
        End Set
    End Property
    Dim fMovesDate As DateTime
    Public Property MovesDate() As DateTime
        Get
            Return fMovesDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("MovesDate", fMovesDate, value)
        End Set
    End Property
    Dim fMovesDebit As Decimal
    Public Property MovesDebit() As Decimal
        Get
            Return fMovesDebit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MovesDebit", fMovesDebit, value)
        End Set
    End Property
    Dim fMovesCredit As Decimal
    Public Property MovesCredit() As Decimal
        Get
            Return fMovesCredit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MovesCredit", fMovesCredit, value)
        End Set
    End Property
    Dim fNameVoucher As String
    <Size(21)> _
    Public Property NameVoucher() As String
        Get
            Return fNameVoucher
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameVoucher", fNameVoucher, value)
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
    Dim fAbbreviation As String
    Public Property Abbreviation() As String
        Get
            Return fAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Abbreviation", fAbbreviation, value)
        End Set
    End Property
    Dim fNumberName As String
    'columna que devuelve el numero y nombre de la cuenta
    <Size(150)> _
    <PersistentAlias("concat(concat(AccountNumber,' - '),AccountName)")>
    Public ReadOnly Property NumberName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberName"))
        End Get
    End Property

    Dim fNitName As String
    'columna que devuelve el numero y nombre de la cuenta
    <Size(150)> _
    <PersistentAlias("concat(concat(ThirdPartyNit,' - '),ThirdPartyName)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
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
