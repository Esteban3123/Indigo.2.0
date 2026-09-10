Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.BudgetTransferDetail")> _
Public Class BudgetTransferDetailReportXpo
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
    Dim fTransferId As BudgetTransferReportXpo
    <Association("Budget_BudgetTransferDetailReferencesBudget_BudgetTransfer")> _
    Public Property TransferId() As BudgetTransferReportXpo
        Get
            Return fTransferId
        End Get
        Set(ByVal value As BudgetTransferReportXpo)
            SetPropertyValue(Of BudgetTransferReportXpo)("TransferId", fTransferId, value)
        End Set
    End Property
    Dim fBudgetId As BudgetReportXpo
    <Association("Budget_TrasnferDetailReferencesBudget_Report")> _
    Public Property BudgetId() As BudgetReportXpo
        Get
            Return fBudgetId
        End Get
        Set(ByVal value As BudgetReportXpo)
            SetPropertyValue(Of BudgetReportXpo)("BudgetId", fBudgetId, value)
        End Set
    End Property
    Dim fRevenueTypeId As BudgetRevenueTypeReportXpo
    <Association("Budget_BudgetTransferDetailReferencesBudget_BudgetRevenueType")> _
    Public Property RevenueTypeId() As BudgetRevenueTypeReportXpo
        Get
            Return fRevenueTypeId
        End Get
        Set(ByVal value As BudgetRevenueTypeReportXpo)
            SetPropertyValue(Of BudgetRevenueTypeReportXpo)("RevenueTypeId", fRevenueTypeId, value)
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
