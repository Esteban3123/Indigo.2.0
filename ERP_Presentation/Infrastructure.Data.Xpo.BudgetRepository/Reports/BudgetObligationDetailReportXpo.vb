Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.ObligationDetail")> _
Public Class BudgetObligationDetailReportXpo
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
    Dim fObligationId As BudgetObligationReportXpo
    <Association("Budget_ObligationDetailReferencesBudget_Obligation")> _
    Public Property ObligationId() As BudgetObligationReportXpo
        Get
            Return fObligationId
        End Get
        Set(ByVal value As BudgetObligationReportXpo)
            SetPropertyValue(Of BudgetObligationReportXpo)("ObligationId", fObligationId, value)
        End Set
    End Property
    Dim fCommitmentDetailId As BudgetCommitmentDetailReportXpo
    <Association("Budget_ObligationDetailReferencesBudget_CommitmentDetail")> _
        Public Property CommitmentDetailId() As BudgetCommitmentDetailReportXpo
        Get
            Return fCommitmentDetailId
        End Get
        Set(ByVal value As BudgetCommitmentDetailReportXpo)
            SetPropertyValue(Of BudgetCommitmentDetailReportXpo)("CommitmentDetailId", fCommitmentDetailId, value)
        End Set
    End Property
    Dim fCategoryId As BudgetCategoryReportXpo
    <Association("Budget_ObligationDetailReferencesBudget_Category")> _
        Public Property CategoryId() As BudgetCategoryReportXpo
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As BudgetCategoryReportXpo)
            SetPropertyValue(Of BudgetCategoryReportXpo)("CategoryId", fCategoryId, value)
        End Set
    End Property
    Dim fRevenueTypeId As BudgetRevenueTypeReportXpo
    <Association("Budget_ObligationDetailReferencesBudget_BudgetRevenueType")> _
        Public Property RevenueTypeId() As BudgetRevenueTypeReportXpo
        Get
            Return fRevenueTypeId
        End Get
        Set(ByVal value As BudgetRevenueTypeReportXpo)
            SetPropertyValue(Of BudgetRevenueTypeReportXpo)("RevenueTypeId", fRevenueTypeId, value)
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
    Dim fTotalObligation As Decimal
    Public Property TotalObligation() As Decimal
        Get
            Return fTotalObligation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalObligation", fTotalObligation, value)
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
    <Association("Budget_ObligationModificationDetailReferencesBudget_ObligationDetail", GetType(BudgetObligationModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_ObligationModificationDetails() As XPCollection(Of BudgetObligationModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetObligationModificationDetailReportXpo)("Budget_ObligationModificationDetails")
        End Get
    End Property
    <Association("Budget_PaymentOrderDetailReferencesBudget_ObligationDetail", GetType(BudgetPaymentOrderDetailReportXpo))> _
    Public ReadOnly Property Budget_PaymentOrderDetail() As XPCollection(Of BudgetPaymentOrderDetailReportXpo)
        Get
            Return GetCollection(Of BudgetPaymentOrderDetailReportXpo)("Budget_PaymentOrderDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
