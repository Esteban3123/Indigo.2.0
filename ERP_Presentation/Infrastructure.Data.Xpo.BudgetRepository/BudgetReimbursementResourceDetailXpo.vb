Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ReimbursementResourceDetaill")> _
Public Class BudgetReimbursementResourceDetailXpo
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
    Dim fReimbursementResourceId As BudgetReimbursementResourceXpo
    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetReimbursementResourceXpo")> _
    Public Property ReimbursementResourceId() As BudgetReimbursementResourceXpo
        Get
            Return fReimbursementResourceId
        End Get
        Set(ByVal value As BudgetReimbursementResourceXpo)
            SetPropertyValue(Of BudgetReimbursementResourceXpo)("ReimbursementResourceId", fReimbursementResourceId, value)
        End Set
    End Property
    Dim fPaymentOrderDetailId As BudgetPaymentOrderDetailXpo
    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetPaymentOrderDetailXpo")> _
    Public Property PaymentOrderDetailId() As BudgetPaymentOrderDetailXpo
        Get
            Return fPaymentOrderDetailId
        End Get
        Set(ByVal value As BudgetPaymentOrderDetailXpo)
            SetPropertyValue(Of BudgetPaymentOrderDetailXpo)("PaymentOrderDetailId", fPaymentOrderDetailId, value)
        End Set
    End Property
    Dim fObligationModificationId As BudgetObligationModificationXpo
    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetObligationModificationXpo")> _
    Public Property ObligationModificationId() As BudgetObligationModificationXpo
        Get
            Return fObligationModificationId
        End Get
        Set(ByVal value As BudgetObligationModificationXpo)
            SetPropertyValue(Of BudgetObligationModificationXpo)("ObligationModificationId", fObligationModificationId, value)
        End Set
    End Property
    Dim fCommitmentModificationId As BudgetCommitmentModificationXpo
    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetCommitmentModificationXpo")> _
    Public Property CommitmentModificationId() As BudgetCommitmentModificationXpo
        Get
            Return fCommitmentModificationId
        End Get
        Set(ByVal value As BudgetCommitmentModificationXpo)
            SetPropertyValue(Of BudgetCommitmentModificationXpo)("CommitmentModificationId", fCommitmentModificationId, value)
        End Set
    End Property
    Dim fAvailabilityModificationId As BudgetAvailabilityModificationXpo
    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetAvailabilityModificationXpo")> _
    Public Property AvailabilityModificationId() As BudgetAvailabilityModificationXpo
        Get
            Return fAvailabilityModificationId
        End Get
        Set(ByVal value As BudgetAvailabilityModificationXpo)
            SetPropertyValue(Of BudgetAvailabilityModificationXpo)("AvailabilityModificationId", fAvailabilityModificationId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
