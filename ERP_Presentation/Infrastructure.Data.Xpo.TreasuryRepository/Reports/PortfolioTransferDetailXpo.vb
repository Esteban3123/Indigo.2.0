Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Portfolio.PortfolioTransferDetail")> _
Public Class PortfolioTransferDetailXpo
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
    Dim fPortfolioTrasferId As PortfolioTransferXpo
    <Association("PortfolioTransferDetailXpoReferencesPortfolioTransferXpo")> _
    Public Property PortfolioTrasferId() As PortfolioTransferXpo
        Get
            Return fPortfolioTrasferId
        End Get
        Set(ByVal value As PortfolioTransferXpo)
            SetPropertyValue(Of PortfolioTransferXpo)("PortfolioTrasferId", fPortfolioTrasferId, value)
        End Set
    End Property
    Dim fAccountReceivableId As PortfolioAccountReceivableReportXpo
    <Association("PortfolioTransferDetailXpoReferencesPortfolioAccountReceivableReportXpo")> _
    Public Property AccountReceivableId() As PortfolioAccountReceivableReportXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableReportXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioTransferDetailXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpo
    <Association("PortfolioTransferDetailXpoReferencesPayrollCostCenterXpo")> _
    Public Property CostCenterId() As PayrollCostCenterXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpo)
            SetPropertyValue(Of PayrollCostCenterXpo)("CostCenterId", fCostCenterId, value)
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
    <Association("PortfolioTransferDetailAccountShareXpoReferencesPortfolioTransferDetailXpo", GetType(PortfolioTransferDetailAccountShareXpo))> _
    Public ReadOnly Property PortfolioTransferDetailAccountShareXpo() As XPCollection(Of PortfolioTransferDetailAccountShareXpo)
        Get
            Return GetCollection(Of PortfolioTransferDetailAccountShareXpo)("PortfolioTransferDetailAccountShareXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
