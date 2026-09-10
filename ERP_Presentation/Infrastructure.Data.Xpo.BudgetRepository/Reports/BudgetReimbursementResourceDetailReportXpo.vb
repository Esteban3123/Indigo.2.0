Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ReimbursementResourceDetaill")> _
Public Class BudgetReimbursementResourceDetailReportXpo
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
    Dim fReimbursementResourceId As BudgetReimbursementResourceReportXpo
    <Association("Budget_ReimbursementResourceDetaillReferencesBudget_ReimbursementResource")> _
    Public Property ReimbursementResourceId() As BudgetReimbursementResourceReportXpo
        Get
            Return fReimbursementResourceId
        End Get
        Set(ByVal value As BudgetReimbursementResourceReportXpo)
            SetPropertyValue(Of BudgetReimbursementResourceReportXpo)("ReimbursementResourceId", fReimbursementResourceId, value)
        End Set
    End Property
    Dim fPaymentOrderDetailId As BudgetPaymentOrderDetailReportXpo
    <Association("Budget_ReimbursementResourceDetaillReferencesBudget_PaymentOrderDetail")> _
    Public Property PaymentOrderDetailId() As BudgetPaymentOrderDetailReportXpo
        Get
            Return fPaymentOrderDetailId
        End Get
        Set(ByVal value As BudgetPaymentOrderDetailReportXpo)
            SetPropertyValue(Of BudgetPaymentOrderDetailReportXpo)("PaymentOrderDetailId", fPaymentOrderDetailId, value)
        End Set
    End Property
    Dim fObligationModificationId As BudgetObligationModificationReportXpo
    <Association("Budget_ReimbursementResourceDetailReferencesBudget_ObligationModification")> _
    Public Property ObligationModificationId() As BudgetObligationModificationReportXpo
        Get
            Return fObligationModificationId
        End Get
        Set(ByVal value As BudgetObligationModificationReportXpo)
            SetPropertyValue(Of BudgetObligationModificationReportXpo)("ObligationModificationId", fObligationModificationId, value)
        End Set
    End Property
    Dim fCommitmentModificationId As BudgetCommitmentModificationReportXpo
    <Association("Budget_ReimbursementResourceDetailsBudget_CommitmentModification")> _
    Public Property CommitmentModificationId() As BudgetCommitmentModificationReportXpo
        Get
            Return fCommitmentModificationId
        End Get
        Set(ByVal value As BudgetCommitmentModificationReportXpo)
            SetPropertyValue(Of BudgetCommitmentModificationReportXpo)("CommitmentModificationId", fCommitmentModificationId, value)
        End Set
    End Property
    Dim fAvailabilityModificationId As BudgetAvailabilityModificationReportXpo
    <Association("Budget_ReimbursementResourceDetailReferencesBudget_AvailabilityModification")> _
        Public Property AvailabilityModificationId() As BudgetAvailabilityModificationReportXpo
        Get
            Return fAvailabilityModificationId
        End Get
        Set(ByVal value As BudgetAvailabilityModificationReportXpo)
            SetPropertyValue(Of BudgetAvailabilityModificationReportXpo)("AvailabilityModificationId", fAvailabilityModificationId, value)
        End Set
    End Property
    Dim fBudgetModificationId As BudgetModificationReportXpo
    <Association("Budget_ReimbursementResourceDetailReferencesBudget_BudgetModification")>
    Public Property BudgetModificationId() As BudgetModificationReportXpo
        Get
            Return fBudgetModificationId
        End Get
        Set(ByVal value As BudgetModificationReportXpo)
            SetPropertyValue(Of BudgetModificationReportXpo)("BudgetModificationId", fBudgetModificationId, value)
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
