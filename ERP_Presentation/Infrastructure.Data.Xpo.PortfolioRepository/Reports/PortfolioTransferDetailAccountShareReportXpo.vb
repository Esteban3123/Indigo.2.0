Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioTransferDetailIAccountShare")> _
Public Class PortfolioTransferDetailAccountShareReportXpo
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
    Dim fPortfolioTransferDetailId As PortfolioTransferDetailReportXpo
    <Association("PortfolioTransferDetailAccountShareReportXpoReferencesPortfolioTransferDetailReportXpo")> _
    Public Property PortfolioTransferDetailId() As PortfolioTransferDetailReportXpo
        Get
            Return fPortfolioTransferDetailId
        End Get
        Set(ByVal value As PortfolioTransferDetailReportXpo)
            SetPropertyValue(Of PortfolioTransferDetailReportXpo)("PortfolioTransferDetailId", fPortfolioTransferDetailId, value)
        End Set
    End Property
    Dim fAccountReceivableId As PortfolioAccountReceivableReportXpo
    <Association("PortfolioTransferDetailAccountShareReportXpoReferencesPortfolioAccountReceivableReportXpo")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableReportXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableReportXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fAccountReceivableShareId As PortfolioAccountReceivableShareReportXpo
    <Association("PortfolioTransferDetailAccountShareReportXpoPortfolioAccountReceivableShareReportXpo")> _
    Public Property AccountReceivableShareId() As PortfolioAccountReceivableShareReportXpo
        Get
            Return fAccountReceivableShareId
        End Get
        Set(ByVal value As PortfolioAccountReceivableShareReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableShareReportXpo)("AccountReceivableShareId", fAccountReceivableShareId, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
