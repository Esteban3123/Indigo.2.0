#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources
#End Region


<Persistent("Portfolio.ViewAccountReceivableAccountingByPortfolioNote")>
Public Class ViewAccountReceivableAccountingByPortfolioNoteXpo
    Inherits XPLiteObject
#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

#Region "Members"
    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property

    Dim fAccountReceivableAccountingId As Integer
    Public Property AccountReceivableAccountingId() As Integer
        Get
            Return fAccountReceivableAccountingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableAccountingId", fAccountReceivableAccountingId, value)
        End Set
    End Property

    Dim fInvoiceId As Integer?
    Public Property InvoiceId() As Integer?
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fAccountReceivableDate As DateTime
    Public Property AccountReceivableDate() As DateTime
        Get
            Return fAccountReceivableDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AccountReceivableDate", fAccountReceivableDate, value)
        End Set
    End Property

    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fMainAccountNumber As String
    Public Property MainAccountNumber() As String
        Get
            Return fMainAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountNumber", fMainAccountNumber, value)
        End Set
    End Property

    Dim fMainAccountName As String
    Public Property MainAccountName() As String
        Get
            Return fMainAccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountName", fMainAccountName, value)
        End Set
    End Property

    Dim fMainAccountNumberName As String
    Public Property MainAccountNumberName() As String
        Get
            Return fMainAccountNumberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountNumberName", fMainAccountNumberName, value)
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

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
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

    Dim fThirdPartyNitName As String
    Public Property ThirdPartyNitName() As String
        Get
            Return fThirdPartyNitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNitName", fThirdPartyNitName, value)
        End Set
    End Property

    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property

    Dim fCostCenterCode As String
    Public Property CostCenterCode() As String
        Get
            Return fCostCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCode", fCostCenterCode, value)
        End Set
    End Property

    Dim fCostCenterName As String
    Public Property CostCenterName() As String
        Get
            Return fCostCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterName", fCostCenterName, value)
        End Set
    End Property

    Dim fSpecificPortfolioStatus As Byte
    Public Property SpecificPortfolioStatus() As Byte
        Get
            Return fSpecificPortfolioStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SpecificPortfolioStatus", fSpecificPortfolioStatus, value)
        End Set
    End Property

    Dim fPortfolioStatusName As String
    Public Property PortfolioStatusName() As String
        Get
            Return fPortfolioStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PortfolioStatusName", fPortfolioStatusName, value)
        End Set
    End Property

    Dim fInvoiceDocumentType As Integer
    Public Property InvoiceDocumentType() As Integer
        Get
            Return fInvoiceDocumentType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceDocumentType", fInvoiceDocumentType, value)
        End Set
    End Property

    Dim fCurrencyId As Integer?
    Public Property CurrencyId() As Integer?
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CurrencyId", fCurrencyId, value)
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

    Dim fTRMValue As Decimal?
    Public Property TRMValue() As Decimal?
        Get
            Return fTRMValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TRMValue", fTRMValue, value)
        End Set
    End Property

#End Region
End Class
