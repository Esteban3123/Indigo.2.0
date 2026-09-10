Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioNoteDistribution")> _
Public Class PortfolioNoteDistributionXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fPortfolioNoteId As PortfolioNoteReportXpo
    <Association("Portfolio_PortfolioNoteDistributionReferencesPortfolio_PortfolioNote")>
    Public Property PortfolioNoteId() As PortfolioNoteReportXpo
        Get
            Return fPortfolioNoteId
        End Get
        Set(ByVal value As PortfolioNoteReportXpo)
            SetPropertyValue(Of PortfolioNoteReportXpo)("PortfolioNoteId", fPortfolioNoteId, value)
        End Set
    End Property

    Dim fCustomerId As CommonCustomerReportXpo
    <Association("PortfolioNoteDistributionReferencesCommonCustomerReportXpo")>
    Public Property CustomerId() As CommonCustomerReportXpo
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As CommonCustomerReportXpo)
            SetPropertyValue(Of CommonCustomerReportXpo)("CustomerId", fCustomerId, value)
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

    Dim fPortfolioAdvanceId As PortfolioAdvanceReportXpo
    <Association("PortfolioNoteDistributionReferencesPortfolioAdvanceReportXpo")> _
    Public Property PortfolioAdvanceId() As PortfolioAdvanceReportXpo
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As PortfolioAdvanceReportXpo)
            SetPropertyValue(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
