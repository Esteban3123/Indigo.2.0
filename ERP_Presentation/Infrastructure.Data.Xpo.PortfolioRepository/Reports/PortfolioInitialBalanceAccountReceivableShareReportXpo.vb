Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioInitialBalanceAccountReceivableShare")> _
Public Class PortfolioInitialBalanceAccountReceivableShareReportXpo
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
    Dim fPortfolioInitialBalanceAccountReceivableId As PortfolioInitialBalanceAccountReceivableReportXpo
    <Association("PortfolioInitialBalanceAccountReceivableShareReportXpoReferencesPortfolioInitialBalanceAccountReceivableReportXpo")> _
    Public Property PortfolioInitialBalanceAccountReceivableId() As PortfolioInitialBalanceAccountReceivableReportXpo
        Get
            Return fPortfolioInitialBalanceAccountReceivableId
        End Get
        Set(ByVal value As PortfolioInitialBalanceAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioInitialBalanceAccountReceivableReportXpo)("PortfolioInitialBalanceAccountReceivableId", fPortfolioInitialBalanceAccountReceivableId, value)
        End Set
    End Property
    Dim fNumber As Integer
    Public Property Number() As Integer
        Get
            Return fNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Number", fNumber, value)
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
