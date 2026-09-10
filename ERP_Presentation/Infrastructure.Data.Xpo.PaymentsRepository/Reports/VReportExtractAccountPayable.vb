Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.VReportExtractAccountPayable")> _
Public Class VReportExtractAccountPayable
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
    Dim fDistributionLineName As String
    <Size(100)> _
    Public Property DistributionLineName() As String
        Get
            Return fDistributionLineName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DistributionLineName", fDistributionLineName, value)
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
    Dim fBillNumber As String
    <Size(20)> _
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
        End Set
    End Property
    Dim fEntityName As String
    <Size(250)> _
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
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
    Dim fVoucherName As String
    <Size(21)> _
    Public Property VoucherName() As String
        Get
            Return fVoucherName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoucherName", fVoucherName, value)
        End Set
    End Property
    Dim fIdSupplier As Integer
    Public Property IdSupplier() As Integer
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdSupplier", fIdSupplier, value)
        End Set
    End Property
    Dim fIdBills As Integer
    Public Property IdBills() As Integer
        Get
            Return fIdBills
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdBills", fIdBills, value)
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
    Dim fCodeAccountPayable As String
    <Size(15)> _
    Public Property CodeAccountPayable() As String
        Get
            Return fCodeAccountPayable
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeAccountPayable", fCodeAccountPayable, value)
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
