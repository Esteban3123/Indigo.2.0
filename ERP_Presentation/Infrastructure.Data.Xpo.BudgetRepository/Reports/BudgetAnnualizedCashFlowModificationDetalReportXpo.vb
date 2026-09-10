Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.AnnualizedCashFlowModificationDetail")> _
Public Class BudgetAnnualizedCashFlowModificationDetalReportXpo
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
    Dim fPACModificationId As BudgetAnnualizedCashFlowModificationReportXpo
    <Association("Budget_AnnualizedCashFlowModificationDetailReferencesBudget_AnnualizedCashFlowModification")> _
    Public Property PACModificationId() As BudgetAnnualizedCashFlowModificationReportXpo
        Get
            Return fPACModificationId
        End Get
        Set(ByVal value As BudgetAnnualizedCashFlowModificationReportXpo)
            SetPropertyValue(Of BudgetAnnualizedCashFlowModificationReportXpo)("PACModificationId", fPACModificationId, value)
        End Set
    End Property
    Dim fAnnualizedCashFlowId As BudgetAnnualizedCashFlowReportXpo
    <Association("Budget_AnnualizedCashFlowModificationDetailReferencesBudget_AnnualizedCashFlow")> _
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
