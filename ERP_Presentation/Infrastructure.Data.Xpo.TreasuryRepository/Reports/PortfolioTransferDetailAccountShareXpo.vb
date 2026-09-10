Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioTransferDetailIAccountShare")> _
Public Class PortfolioTransferDetailAccountShareXpo
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
    Dim fPortfolioTransferDetailId As PortfolioTransferDetailXpo
    <Association("PortfolioTransferDetailAccountShareXpoReferencesPortfolioTransferDetailXpo")> _
    Public Property PortfolioTransferDetailId() As PortfolioTransferDetailXpo
        Get
            Return fPortfolioTransferDetailId
        End Get
        Set(ByVal value As PortfolioTransferDetailXpo)
            SetPropertyValue(Of PortfolioTransferDetailXpo)("PortfolioTransferDetailId", fPortfolioTransferDetailId, value)
        End Set
    End Property
    Dim fAccountReceivableId As PortfolioAccountReceivableReportXpo
    <Association("PortfolioTransferDetailAccountShareXpoReferencesPortfolioAccountReceivableReportXpo")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableReportXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableReportXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fAccountReceivableShareId As PortfolioAccountReceivableShareXpo
    <Association("PortfolioTransferDetailAccountShareXpoReferencesPortfolioAccountReceivableShareXpo")> _
    Public Property AccountReceivableShareId() As PortfolioAccountReceivableShareXpo
        Get
            Return fAccountReceivableShareId
        End Get
        Set(ByVal value As PortfolioAccountReceivableShareXpo)
            SetPropertyValue(Of PortfolioAccountReceivableShareXpo)("AccountReceivableShareId", fAccountReceivableShareId, value)
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
