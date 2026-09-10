Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.Budget")> _
Public Class BudgetReportXpo
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
    Dim fBudgetHeaderId As BudgetHeaderReportXpo
    <Association("Budget_BudgetReferencesBudget_BudgetHeader")> _
    Public Property BudgetHeaderId() As BudgetHeaderReportXpo
        Get
            Return fBudgetHeaderId
        End Get
        Set(ByVal value As BudgetHeaderReportXpo)
            SetPropertyValue(Of BudgetHeaderReportXpo)("BudgetHeaderId", fBudgetHeaderId, value)
        End Set
    End Property
    Dim fCategoryId As BudgetCategoryReportXpo
    <Association("Budget_BudgetReportReferencesBudget_Category")> _
        Public Property CategoryId() As BudgetCategoryReportXpo
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As BudgetCategoryReportXpo)
            SetPropertyValue(Of BudgetCategoryReportXpo)("CategoryId", fCategoryId, value)
        End Set
    End Property
    Dim fRevenueTypeId As BudgetRevenueTypeReportXpo
    <Association("Budget_BudgetReportReferencesBudget_BudgetRevenueType")> _
    Public Property RevenueTypeId() As BudgetRevenueTypeReportXpo
        Get
            Return fRevenueTypeId
        End Get
        Set(ByVal value As BudgetRevenueTypeReportXpo)
            SetPropertyValue(Of BudgetRevenueTypeReportXpo)("RevenueTypeId", fRevenueTypeId, value)
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
    Dim fDebitValueModification As Decimal
    Public Property DebitValueModification() As Decimal
        Get
            Return fDebitValueModification
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValueModification", fDebitValueModification, value)
        End Set
    End Property
    Dim fCreditValueModification As Decimal
    Public Property CreditValueModification() As Decimal
        Get
            Return fCreditValueModification
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValueModification", fCreditValueModification, value)
        End Set
    End Property
    Dim fDebitValueTransfer As Decimal
    Public Property DebitValueTransfer() As Decimal
        Get
            Return fDebitValueTransfer
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValueTransfer", fDebitValueTransfer, value)
        End Set
    End Property
    Dim fCreditValueTransfer As Decimal
    Public Property CreditValueTransfer() As Decimal
        Get
            Return fCreditValueTransfer
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValueTransfer", fCreditValueTransfer, value)
        End Set
    End Property
    Dim fTotalBudget As Decimal
    Public Property TotalBudget() As Decimal
        Get
            Return fTotalBudget
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalBudget", fTotalBudget, value)
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
    Dim fSuspendedValue As Decimal
    Public Property SuspendedValue() As Decimal
        Get
            Return fSuspendedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SuspendedValue", fSuspendedValue, value)
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
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    <Association("Budget_ModificationDetail_BudgetReport", GetType(BudgetModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_ModificationDetail() As XPCollection(Of BudgetModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetModificationDetailReportXpo)("Budget_ModificationDetail")
        End Get
    End Property
    <Association("Budget_TrasnferDetailReferencesBudget_Report", GetType(BudgetTransferDetailReportXpo))> _
    Public ReadOnly Property Budget_TransferDetail() As XPCollection(Of BudgetTransferDetailReportXpo)
        Get
            Return GetCollection(Of BudgetTransferDetailReportXpo)("Budget_TransferDetail")
        End Get
    End Property
    <Association("Budget_AvailabilityDetailReferencesBudget_Report", GetType(BudgetAvailabilityDetailReportXpo))> _
    Public ReadOnly Property Budget_AvailabilityDetail() As XPCollection(Of BudgetAvailabilityDetailReportXpo)
        Get
            Return GetCollection(Of BudgetAvailabilityDetailReportXpo)("Budget_AvailabilityDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
