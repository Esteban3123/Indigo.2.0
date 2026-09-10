Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.CommitmentDetail")> _
Public Class BudgetCommitmentDetailReportXpo
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
    Dim fCommitmentId As BudgetCommitmentReportXpo
    <Association("Budget_CommitmentDetailReferencesBudget_Commitment")> _
    Public Property CommitmentId() As BudgetCommitmentReportXpo
        Get
            Return fCommitmentId
        End Get
        Set(ByVal value As BudgetCommitmentReportXpo)
            SetPropertyValue(Of BudgetCommitmentReportXpo)("CommitmentId", fCommitmentId, value)
        End Set
    End Property
    Dim fAvailabilityDetailId As BudgetAvailabilityDetailReportXpo
    <Association("Budget_CommitmentDetailReferencesBudget_AvailabilityDetail")> _
    Public Property AvailabilityDetailId() As BudgetAvailabilityDetailReportXpo
        Get
            Return fAvailabilityDetailId
        End Get
        Set(ByVal value As BudgetAvailabilityDetailReportXpo)
            SetPropertyValue(Of BudgetAvailabilityDetailReportXpo)("AvailabilityDetailId", fAvailabilityDetailId, value)
        End Set
    End Property
    Dim fCategoryId As BudgetCategoryReportXpo
    <Association("Budget_CommitmentDetailReferencesBudget_Category")> _
    Public Property CategoryId() As BudgetCategoryReportXpo
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As BudgetCategoryReportXpo)
            SetPropertyValue(Of BudgetCategoryReportXpo)("CategoryId", fCategoryId, value)
        End Set
    End Property
    Dim fRevenueTypeId As BudgetRevenueTypeReportXpo
    <Association("Budget_CommitmentDetailReferencesBudget_BudgetRevenueType")> _
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
    <Association("Budget_CommitmentModificationDetailReferencesBudget_CommitmentDetail", GetType(BudgetCommitmentModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_CommitmentModificationDetails() As XPCollection(Of BudgetCommitmentModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetCommitmentModificationDetailReportXpo)("Budget_CommitmentModificationDetails")
        End Get
    End Property
    <Association("Budget_ObligationDetailReferencesBudget_CommitmentDetail", GetType(BudgetObligationDetailReportXpo))> _
    Public ReadOnly Property Budget_ObligationDetail() As XPCollection(Of BudgetObligationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetObligationDetailReportXpo)("Budget_ObligationDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
