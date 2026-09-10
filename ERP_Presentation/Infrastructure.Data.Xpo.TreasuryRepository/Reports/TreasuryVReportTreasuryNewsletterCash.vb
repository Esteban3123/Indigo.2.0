Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VReportTreasuryNewsletterCash")> _
Public Class TreasuryVReportTreasuryNewsletterCash
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

    Dim fNameVoucher As String
    Public Property NameVoucher() As String
        Get
            Return fNameVoucher
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameVoucher", fNameVoucher, value)
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

    Dim fCashRegisterName As String
    Public Property CashRegisterName() As String
        Get
            Return fCashRegisterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CashRegisterName", fCashRegisterName, value)
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

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
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
    Public Property DocumentDate() As Date
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fCheckNumber As Integer
    Public Property CheckNumber() As Integer
        Get
            Return fCheckNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CheckNumber", fCheckNumber, value)
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

    Dim fVoucherClass As Integer
    Public Property VoucherClass() As Integer
        Get
            Return fVoucherClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VoucherClass", fVoucherClass, value)
        End Set
    End Property

    Dim fVoucherType As Integer
    Public Property VoucherType() As Integer
        Get
            Return fVoucherType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("VoucherType", fVoucherType, value)
        End Set
    End Property

    Dim fNature As Integer
    Public Property Nature() As Integer
        Get
            Return fNature
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Nature", fNature, value)
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
