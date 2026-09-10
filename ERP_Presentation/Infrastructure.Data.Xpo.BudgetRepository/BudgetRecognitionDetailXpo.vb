Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.RecognitionDetail")> _
Public Class BudgetRecognitionDetailXpo
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
    Dim fRecognitionId As BudgetRecognitionXpo
    <Association("Budget_RecognitionDetailReferencesBudget_Recognition")> _
    Public Property RecognitionId() As BudgetRecognitionXpo
        Get
            Return fRecognitionId
        End Get
        Set(ByVal value As BudgetRecognitionXpo)
            SetPropertyValue(Of BudgetRecognitionXpo)("RecognitionId", fRecognitionId, value)
        End Set
    End Property
    Dim fCategoryId As BudgetCategoryXpo
    <Association("BudgetRecognitionDetailXpoReferencesBudgetCategoryXpo")> _
    Public Property CategoryId() As BudgetCategoryXpo
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As BudgetCategoryXpo)
            SetPropertyValue(Of BudgetCategoryXpo)("CategoryId", fCategoryId, value)
        End Set
    End Property
    Dim fRevenueTypeId As BudgetRevenueTypeXpo
    <Association("BudgetRecognitionDetailXpoReferencesBudgetRevenueTypeXpo")> _
    Public Property RevenueTypeId() As BudgetRevenueTypeXpo
        Get
            Return fRevenueTypeId
        End Get
        Set(ByVal value As BudgetRevenueTypeXpo)
            SetPropertyValue(Of BudgetRevenueTypeXpo)("RevenueTypeId", fRevenueTypeId, value)
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
    Dim fExecutedValue As Decimal
    Public Property ExecutedValue() As Decimal
        Get
            Return fExecutedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ExecutedValue", fExecutedValue, value)
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
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    <Association("BudgetRecognitionModificationDetailXpoReferencesBudgetRecognitionDetailXpo", GetType(BudgetRecognitionModificationDetailXpo))> _
    Public ReadOnly Property BudgetRecognitionModificationDetailXpo() As XPCollection(Of BudgetRecognitionModificationDetailXpo)
        Get
            Return GetCollection(Of BudgetRecognitionModificationDetailXpo)("BudgetRecognitionModificationDetailXpo")
        End Get
    End Property

    <Association("Budget_CollectionDetailReferencesBudget_RecognitionDetail", GetType(BudgetCollectionDetailXpo))> _
    Public ReadOnly Property Budget_CollectionDetails() As XPCollection(Of BudgetCollectionDetailXpo)
        Get
            Return GetCollection(Of BudgetCollectionDetailXpo)("Budget_CollectionDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
