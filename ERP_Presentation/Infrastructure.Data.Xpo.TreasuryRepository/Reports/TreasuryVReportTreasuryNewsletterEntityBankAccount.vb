Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VReportTreasuryNewsletterEntityBankAccount")> _
Public Class TreasuryVReportTreasuryNewsletterEntityBankAccount
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

    Dim fEntityBankId As Integer
    Public Property EntityBankId() As Integer
        Get
            Return fEntityBankId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityBankId", fEntityBankId, value)
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

    Dim fEntityBankAccountName As String
    Public Property EntityBankAccountName() As String
        Get
            Return fEntityBankAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityBankAccountName", fEntityBankAccountName, value)
        End Set
    End Property

    Dim fEntityBankAccountNumber As String
    Public Property EntityBankAccountNumber() As String
        Get
            Return fEntityBankAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityBankAccountNumber", fEntityBankAccountNumber, value)
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

#Region "Custom Properties"

    'Propiedad Añadida
    Dim fSaldoAnterior As Long
    <NonPersistent()>
    Public Property SaldoAnterior() As Long
        Get
            Return fSaldoAnterior
        End Get
        Set(ByVal value As Long)
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
