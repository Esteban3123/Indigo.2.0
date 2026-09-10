Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

Public Structure BudgetIncomeKey
    <Persistent("idCategory")> _
    Public Property idCategory As Integer

    <Persistent("CategoryCode")> _
    Public Property CategoryCode As String

    <Persistent("RevenueTypeCode")> _
    Public Property RevenueTypeCode As String

End Structure

<Persistent("Budget.VReportBudgetIncome")> _
Public Class VReportBudgetIncomeXpo
    Inherits XPLiteObject

    <Key(), Persistent()> _
    Public Property Key As BudgetIncomeKey

    Dim fBudgetaryValidityId As Integer
    Public Property BudgetaryValidityId() As Integer
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property
    Dim fidCategory As Integer
    Public Property idCategory() As Integer
        Get
            Return fidCategory
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("idCategory", fidCategory, value)
        End Set
    End Property
    Dim fCategoryOwnerId As Integer
    Public Property CategoryOwnerId() As Integer
        Get
            Return fCategoryOwnerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CategoryOwnerId", fCategoryOwnerId, value)
        End Set
    End Property
    Dim fCategoryCode As String
    Public Property CategoryCode() As String
        Get
            Return fCategoryCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryCode", fCategoryCode, value)
        End Set
    End Property
    Dim fCategoryName As String
    Public Property CategoryName() As String
        Get
            Return fCategoryName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryName", fCategoryName, value)
        End Set
    End Property
    Dim fAuxiliary As Boolean
    Public Property Auxiliary() As Boolean
        Get
            Return fAuxiliary
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Auxiliary", fAuxiliary, value)
        End Set
    End Property
    Dim fRevenueTypeCode As String
    Public Property RevenueTypeCode() As String
        Get
            Return fRevenueTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RevenueTypeCode", fRevenueTypeCode, value)
        End Set
    End Property
    Dim fRevenueTypeName As String
    Public Property RevenueTypeName() As String
        Get
            Return fRevenueTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RevenueTypeName", fRevenueTypeName, value)
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
    Dim fCreditValueModification As Decimal
    Public Property CreditValueModification() As Decimal
        Get
            Return fCreditValueModification
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValueModification", fCreditValueModification, value)
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
    Dim fCreditValueTransfer As Decimal
    Public Property CreditValueTransfer() As Decimal
        Get
            Return fCreditValueTransfer
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValueTransfer", fCreditValueTransfer, value)
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
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
