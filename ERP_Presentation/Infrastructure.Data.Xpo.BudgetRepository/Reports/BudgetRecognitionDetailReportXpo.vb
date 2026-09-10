Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.RecognitionDetail")> _
Public Class BudgetRecognitionDetailReportXpo
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
    Dim fRecognitionId As BudgetRecognitionReportXpo
    <Association("Budget_RecognitionDetailReferencesBudget_Recognition")> _
    Public Property RecognitionId() As BudgetRecognitionReportXpo
        Get
            Return fRecognitionId
        End Get
        Set(ByVal value As BudgetRecognitionReportXpo)
            SetPropertyValue(Of BudgetRecognitionReportXpo)("RecognitionId", fRecognitionId, value)
        End Set
    End Property
    Dim fCategoryId As BudgetCategoryReportXpo
    <Association("Budget_RecognitionDetailReferencesBudget_Category")> _
    Public Property CategoryId() As BudgetCategoryReportXpo
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As BudgetCategoryReportXpo)
            SetPropertyValue(Of BudgetCategoryReportXpo)("CategoryId", fCategoryId, value)
        End Set
    End Property
    Dim fRevenueTypeId As BudgetRevenueTypeReportXpo
    <Association("Budget_RecognitionDetailReferencesBudget_BudgetRevenueType")> _
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
    Dim fTotalRecognition As Decimal
    Public Property TotalRecognition() As Decimal
        Get
            Return fTotalRecognition
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalRecognition", fTotalRecognition, value)
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
    <Association("Budget_RecognitionModificationDetailReferencesBudget_RecognitionDetail", GetType(BudgetRecognitionModificationDetailReportXpo))> _
    Public ReadOnly Property Budget_RecognitionModificationDetails() As XPCollection(Of BudgetRecognitionModificationDetailReportXpo)
        Get
            Return GetCollection(Of BudgetRecognitionModificationDetailReportXpo)("Budget_RecognitionModificationDetails")
        End Get
    End Property
    <Association("Budget_CollectionDetailReferencesBudget_RecognitionDetail", GetType(BudgetCollectionDetailReportXpo))> _
    Public ReadOnly Property Budget_CollectionDetail() As XPCollection(Of BudgetCollectionDetailReportXpo)
        Get
            Return GetCollection(Of BudgetCollectionDetailReportXpo)("Budget_CollectionDetail")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
