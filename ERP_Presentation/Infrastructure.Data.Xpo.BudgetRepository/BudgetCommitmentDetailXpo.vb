Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.CommitmentDetail")> _
Public Class BudgetCommitmentDetailXpo
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
    Dim fCommitmentId As BudgetCommitmentXpo
    <Association("BudgetCommitmentDetailXpoReferencesBudgetCommitmentXpo")> _
    Public Property CommitmentId() As BudgetCommitmentXpo
        Get
            Return fCommitmentId
        End Get
        Set(ByVal value As BudgetCommitmentXpo)
            SetPropertyValue(Of BudgetCommitmentXpo)("CommitmentId", fCommitmentId, value)
        End Set
    End Property
    Dim fAvailabilityDetailId As BudgetAvailabilityDetailXpo
    <Association("BudgetCommitmentDetailXpoReferencesBudgetAvailabilityDetailXpo")> _
    Public Property AvailabilityDetailId() As BudgetAvailabilityDetailXpo
        Get
            Return fAvailabilityDetailId
        End Get
        Set(ByVal value As BudgetAvailabilityDetailXpo)
            SetPropertyValue(Of BudgetAvailabilityDetailXpo)("AvailabilityDetailId", fAvailabilityDetailId, value)
        End Set
    End Property
    Dim fCategoryId As BudgetCategoryXpo
    <Association("BudgetCommitmentDetailXpoReferencesBudgetCategoryXpo")> _
    Public Property CategoryId() As BudgetCategoryXpo
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As BudgetCategoryXpo)
            SetPropertyValue(Of BudgetCategoryXpo)("CategoryId", fCategoryId, value)
        End Set
    End Property
    Dim fRevenueTypeId As BudgetRevenueTypeXpo
    <Association("BudgetCommitmentDetailXpoReferencesBudgetRevenueTypeXpo")> _
    Public Property RevenueTypeId() As BudgetRevenueTypeXpo
        Get
            Return fRevenueTypeId
        End Get
        Set(ByVal value As BudgetRevenueTypeXpo)
            SetPropertyValue(Of BudgetRevenueTypeXpo)("RevenueTypeId", fRevenueTypeId, value)
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
    Dim fTotalCommitment As Decimal
    Public Property TotalCommitment() As Decimal
        Get
            Return fTotalCommitment
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalCommitment", fTotalCommitment, value)
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

    <Association("BudgetCommitmentModificationDetailXpoReferencesBudgetCommitmentDetailXpo", GetType(BudgetCommitmentModificationDetailXpo))> _
    Public ReadOnly Property BudgetCommitmentModificationDetailXpo() As XPCollection(Of BudgetCommitmentModificationDetailXpo)
        Get
            Return GetCollection(Of BudgetCommitmentModificationDetailXpo)("BudgetCommitmentModificationDetailXpo")
        End Get
    End Property

    <Association("Budget_ObligationDetailReferencesBudget_CommitmentDetail", GetType(BudgetObligationDetailXpo))> _
    Public ReadOnly Property BudgetObligationDetailXpo() As XPCollection(Of BudgetObligationDetailXpo)
        Get
            Return GetCollection(Of BudgetObligationDetailXpo)("BudgetObligationDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
