Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioInitialBalanceAccountReceivable")> _
Public Class PortfolioInitialBalanceAccountReceivableReportXpo
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
    Dim fPortfolioInitialBalanceId As PortfolioInitialBalanceReportXpo
    <Association("PortfolioInitialBalanceAccountReceivableReportXpoReferencesPortfolioInitialBalanceReportXpo")> _
    Public Property PortfolioInitialBalanceId() As PortfolioInitialBalanceReportXpo
        Get
            Return fPortfolioInitialBalanceId
        End Get
        Set(ByVal value As PortfolioInitialBalanceReportXpo)
            SetPropertyValue(Of PortfolioInitialBalanceReportXpo)("PortfolioInitialBalanceId", fPortfolioInitialBalanceId, value)
        End Set
    End Property
    Dim fAccountReceivableType As Byte
    Public Property AccountReceivableType() As Byte
        Get
            Return fAccountReceivableType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AccountReceivableType", fAccountReceivableType, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("PortfolioInitialBalanceAccountReceivableReportXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fCustomerId As CommonCustomerReportXpo
    <Association("PortfolioInitialBalanceAccountReceivableReportXpoReferencesCommonCustomerReportXpo")> _
    Public Property CustomerId() As CommonCustomerReportXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CommonCustomerReportXpo)
            SetPropertyValue(Of CommonCustomerReportXpo)("CustomerId", fCustomerId, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(20)> _
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
    Dim fTerm As Integer
    Public Property Term() As Integer
        Get
            Return fTerm
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Term", fTerm, value)
        End Set
    End Property
    Dim fExpiredDate As DateTime
    Public Property ExpiredDate() As DateTime
        Get
            Return fExpiredDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpiredDate", fExpiredDate, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(300)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
    Dim fPortfolioStatus As Byte
    Public Property PortfolioStatus() As Byte
        Get
            Return fPortfolioStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PortfolioStatus", fPortfolioStatus, value)
        End Set
    End Property
    Dim fNumberShares As Integer
    Public Property NumberShares() As Integer
        Get
            Return fNumberShares
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NumberShares", fNumberShares, value)
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
    <Association("PortfolioInitialBalanceAccountReceivableAccountingReportXpoReferencesPortfolioInitialBalanceAccountReceivableReportXpo", GetType(PortfolioInitialBalanceAccountReceivableAccountingReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAccountReceivableAccountingReportXpo() As XPCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableAccountingReportXpo)("PortfolioInitialBalanceAccountReceivableAccountingReportXpo")
        End Get
    End Property
    <Association("PortfolioInitialBalanceAccountReceivableShareReportXpoReferencesPortfolioInitialBalanceAccountReceivableReportXpo", GetType(PortfolioInitialBalanceAccountReceivableShareReportXpo))> _
    Public ReadOnly Property PortfolioInitialBalanceAccountReceivableShareReportXpo() As XPCollection(Of PortfolioInitialBalanceAccountReceivableShareReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableShareReportXpo)("PortfolioInitialBalanceAccountReceivableShareReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
