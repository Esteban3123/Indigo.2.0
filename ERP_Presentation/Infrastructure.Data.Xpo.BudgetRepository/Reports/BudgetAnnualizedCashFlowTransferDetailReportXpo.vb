Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.AnnualizedCashFlowTransferDetail")> _
Public Class BudgetAnnualizedCashFlowTransferDetailReportXpo
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
    Dim fPACTransferId As BudgetAnnualizedCashFlowTransferReportXpo
    <Association("Budget_AnnualizedCashFlowTransferDetailReferencesBudget_AnnualizedCashFlowTransfer")> _
    Public Property PACTransferId() As BudgetAnnualizedCashFlowTransferReportXpo
        Get
            Return fPACTransferId
        End Get
        Set(ByVal value As BudgetAnnualizedCashFlowTransferReportXpo)
            SetPropertyValue(Of BudgetAnnualizedCashFlowTransferReportXpo)("PACTransferId", fPACTransferId, value)
        End Set
    End Property
    Dim fAnnualizedCashFlowId As BudgetAnnualizedCashFlowReportXpo
    <Association("Budget_AnnualizedCashFlowTrasnferDetailReferencesBudget_AnnualizedCashFlow")> _
        Public Property AnnualizedCashFlowId() As BudgetAnnualizedCashFlowReportXpo
        Get
            Return fAnnualizedCashFlowId
        End Get
        Set(ByVal value As BudgetAnnualizedCashFlowReportXpo)
            SetPropertyValue(Of BudgetAnnualizedCashFlowReportXpo)("AnnualizedCashFlowId", fAnnualizedCashFlowId, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
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
