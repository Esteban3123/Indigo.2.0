Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.AvailabilityDetail")> _
Public Class BudgetAvailabilityDetailReportXpo
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
    Dim fAvailabilityId As BudgetAvailabilityReportXpo
    <Association("Budget_AvailabilityDetailReferencesBudget_Availability")> _
    Public Property AvailabilityId() As BudgetAvailabilityReportXpo
        Get
            Return fAvailabilityId
        End Get
        Set(ByVal value As BudgetAvailabilityReportXpo)
            SetPropertyValue(Of BudgetAvailabilityReportXpo)("AvailabilityId", fAvailabilityId, value)
        End Set
    End Property
    Dim fBudgetId As BudgetReportXpo
    <Association("Budget_AvailabilityDetailReferencesBudget_Report")>
    Public Property BudgetId() As BudgetReportXpo
        Get
            Return fBudgetId
        End Get
        Set(ByVal value As BudgetReportXpo)
            SetPropertyValue(Of BudgetReportXpo)("BudgetId", fBudgetId, value)
        End Set
    End Property
    <Association("Budget_BudgetAvailabilityDetailXpoReferencesBudget_CPCCatalog")>
    Dim fCPCId As BudgetCPCCatalogXpo
    Public Property CPCCodeId() As BudgetCPCCatalogXpo
        Get
            Return fCPCId
        End Get
        Set(ByVal value As BudgetCPCCatalogXpo)
            SetPropertyValue(Of BudgetCPCCatalogXpo)("CPCCodeId", fCPCId, value)
        End Set
    End Property
    Dim fInitialValue As Decimal
    Public Property InitialValue() As Decimal
        Get
            Return fInitialValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialValue", fInitialValue, value)
        End Set
    End Property
    Dim fDebitModificationValue As Decimal
    Public Property DebitModificationValue() As Decimal
        Get
            Return fDebitModificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitModificationValue", fDebitModificationValue, value)
        End Set
    End Property
    Dim fCreditModificationValue As Decimal
    Public Property CreditModificationValue() As Decimal
        Get
            Return fCreditModificationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditModificationValue", fCreditModificationValue, value)
        End Set
    End Property
    Dim fTotalAvailability As Decimal
    Public Property TotalAvailability() As Decimal
        Get
            Return fTotalAvailability
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAvailability", fTotalAvailability, value)
        End Set
    End Property
    Dim fExecutedValue As Decimal
    Public Property ExecutedValue() As Decimal
        Get
            Return fExecutedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExecutedValue", fExecutedValue, value)
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
    <Association("Budget_AvailabilityModificationDetailReferencesBudget_AvailabilityDetail", GetType(BudgetAvailabilityModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_AvailabilityModificationDetails() As XPCollection(Of BudgetAvailabilityModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityModificationDetailReportXpo)("Budget_AvailabilityModificationDetails")
        End Get
    End Property
    <Association("Budget_CommitmentDetailReferencesBudget_AvailabilityDetail", GetType(BudgetCommitmentDetailReportXpo))> _
    Public ReadOnly Property Budget_CommitmentDetail() As XPCollection(Of BudgetCommitmentDetailReportXpo)
        Get
            Return GetCollection(Of BudgetCommitmentDetailReportXpo)("Budget_CommitmentDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
