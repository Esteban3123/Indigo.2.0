Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.VReportSuretyFile")> _
Public Class VReportSuretyFileXpo
    Inherits XPLiteObject

    Private fId As Integer
    <Key(False)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)(NameOf(Id), fId, value)
        End Set
    End Property

    Dim fReceiptCode As String
    <Size(20)> _
    Public Property ReceiptCode() As String
        Get
            Return fReceiptCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceiptCode", fReceiptCode, value)
        End Set
    End Property
    Dim fCollectType As Integer
    Public Property CollectType() As Integer
        Get
            Return fCollectType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CollectType", fCollectType, value)
        End Set
    End Property
    Dim fDetail As String
    <Size(300)> _
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property
    Dim fNit As String
    <Size(20)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fBankAccount As String
    <Size(300)> _
    Public Property BankAccount() As String
        Get
            Return fBankAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAccount", fBankAccount, value)
        End Set
    End Property
    Dim fBank As String
    <Size(300)>
    Public Property Bank() As String
        Get
            Return fBank
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bank", fBank, value)
        End Set
    End Property
    Dim fAccount As String
    <Size(300)>
    Public Property Account() As String
        Get
            Return fAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Account", fAccount, value)
        End Set
    End Property
    Dim fBankName As String
    <Size(300)> _
    Public Property BankName() As String
        Get
            Return fBankName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankName", fBankName, value)
        End Set
    End Property
    Dim fCashRegister As String
    <Size(300)> _
    Public Property CashRegister() As String
        Get
            Return fCashRegister
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CashRegister", fCashRegister, value)
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
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fNetValue As Decimal
    Public Property NetValue() As Decimal
        Get
            Return fNetValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NetValue", fNetValue, value)
        End Set
    End Property
    Dim fWithholding As Long
    Public Property Withholding() As Long
        Get
            Return fWithholding
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Withholding", fWithholding, value)
        End Set
    End Property
    Dim fIVARetention As Long
    Public Property IVARetention() As Long
        Get
            Return fIVARetention
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("IVARetention", fIVARetention, value)
        End Set
    End Property
    Dim fICARetention As Long
    Public Property ICARetention() As Long
        Get
            Return fICARetention
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("ICARetention", fICARetention, value)
        End Set
    End Property
    Dim fOtherRetention As Long
    Public Property OtherRetention() As Long
        Get
            Return fOtherRetention
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("OtherRetention", fOtherRetention, value)
        End Set
    End Property
    'Dim fIdOperatingUnit As String
    'Public Property IdOperatingUnit() As String
    '    Get
    '        Return fIdOperatingUnit
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("IdOperatingUnit", fIdOperatingUnit, value)
    '    End Set
    'End Property
    'Dim fNameOperatingUnit As String
    '<Size(300)> _
    'Public Property NameOperatingUnit() As String
    '    Get
    '        Return fNameOperatingUnit
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("NameOperatingUnit", fNameOperatingUnit, value)
    '    End Set
    'End Property
    Dim fValueOperatingUnit As Decimal
    Public Property ValueOperatingUnit() As Decimal
        Get
            Return fValueOperatingUnit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueOperatingUnit", fValueOperatingUnit, value)
        End Set
    End Property
    Dim fTransferDate As DateTime
    Public Property TransferDate() As DateTime
        Get
            Return fTransferDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransferDate", fTransferDate, value)
        End Set
    End Property
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    Dim fStatus As String
    Public Property Status() As String
        Get
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
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

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
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
