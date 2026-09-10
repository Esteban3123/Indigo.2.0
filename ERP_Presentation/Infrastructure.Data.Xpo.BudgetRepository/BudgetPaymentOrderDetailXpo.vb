Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.PaymentOrderDetail")> _
Public Class BudgetPaymentOrderDetailXpo
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
    Dim fPaymentOrderId As BudgetPaymentOrderXpo
    <Association("BudgetPaymentOrderDetailXpoReferencesBudgetPaymentOrderXpo")> _
    Public Property PaymentOrderId() As BudgetPaymentOrderXpo
        Get
            Return fPaymentOrderId
        End Get
        Set(ByVal value As BudgetPaymentOrderXpo)
            SetPropertyValue(Of BudgetPaymentOrderXpo)("PaymentOrderId", fPaymentOrderId, value)
        End Set
    End Property
    Dim fObligationDetailId As BudgetObligationDetailXpo
    <Association("BudgetPaymentOrderDetailXpoReferencesBudgetObligationDetailXpo")> _
    Public Property ObligationDetailId() As BudgetObligationDetailXpo
        Get
            Return fObligationDetailId
        End Get
        Set(ByVal value As BudgetObligationDetailXpo)
            SetPropertyValue(Of BudgetObligationDetailXpo)("ObligationDetailId", fObligationDetailId, value)
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
    Dim fTotalPaymentOrder As Decimal
    Public Property TotalPaymentOrder() As Decimal
        Get
            Return fTotalPaymentOrder
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPaymentOrder", fTotalPaymentOrder, value)
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

    <Association("BudgetReimbursementResourceDetailXpoReferencesBudgetPaymentOrderDetailXpo", GetType(BudgetReimbursementResourceDetailXpo))> _
    Public ReadOnly Property BudgetReimbursementResourceDetailXpo() As XPCollection(Of BudgetReimbursementResourceDetailXpo)
        Get
            Return GetCollection(Of BudgetReimbursementResourceDetailXpo)("BudgetReimbursementResourceDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
