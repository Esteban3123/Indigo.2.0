Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.AvailabilityDetail")> _
Public Class BudgetAvailabilityDetailXpo
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
    Dim fAvailabilityId As BudgetAvailabilityXpo
    <Association("BudgetAvailabilityDetailXpoReferencesBudgetAvailabilityXpo")> _
    Public Property AvailabilityId() As BudgetAvailabilityXpo
        Get
            Return fAvailabilityId
        End Get
        Set(ByVal value As BudgetAvailabilityXpo)
            SetPropertyValue(Of BudgetAvailabilityXpo)("AvailabilityId", fAvailabilityId, value)
        End Set
    End Property
    Dim fBudgetId As BudgetXpo
    <Association("BudgetAvailabilityDetailXpoReferencesBudgetXpo")>
    Public Property BudgetId() As BudgetXpo
        Get
            Return fBudgetId
        End Get
        Set(ByVal value As BudgetXpo)
            SetPropertyValue(Of BudgetXpo)("BudgetId", fBudgetId, value)
        End Set
    End Property
    Dim fCPCCodeId As BudgetCPCCatalogXpo
    <Association("Budget_BudgetAvailabilityModificationDetailXpoReferencesBudget_CPCCatalog")>
    Public Property CPCCodeId As BudgetCPCCatalogXpo
        Get
            Return fCPCCodeId
        End Get
        Set(value As BudgetCPCCatalogXpo)
            SetPropertyValue(Of BudgetCPCCatalogXpo)("CPCCodeId", fCPCCodeId, value)
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

    <Association("BudgetAvailabilityModificationDetailXpoReferencesBudgetAvailabilityDetailXpo", GetType(BudgetAvailabilityModificationDetailXpo))>
    Public ReadOnly Property BudgetAvailabilityModificationDetailXpo() As XPCollection(Of BudgetAvailabilityModificationDetailXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityModificationDetailXpo)("BudgetAvailabilityModificationDetailXpo")
        End Get
    End Property

    <Association("BudgetCommitmentDetailXpoReferencesBudgetAvailabilityDetailXpo", GetType(BudgetCommitmentDetailXpo))> _
    Public ReadOnly Property BudgetCommitmentDetailXpo() As XPCollection(Of BudgetCommitmentDetailXpo)
        Get
            Return GetCollection(Of BudgetCommitmentDetailXpo)("BudgetCommitmentDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
