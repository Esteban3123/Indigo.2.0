Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.SuspensionDetail")> _
Public Class BudgetSuspensionDetailXpo
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
    Dim fSuspensionId As BudgetSuspensionXpo
    <Association("BudgetSuspensionDetailXpoReferencesBudgetSuspensionXpo")> _
    Public Property SuspensionId() As BudgetSuspensionXpo
        Get
            Return fSuspensionId
        End Get
        Set(ByVal value As BudgetSuspensionXpo)
            SetPropertyValue(Of BudgetSuspensionXpo)("SuspensionId", fSuspensionId, value)
        End Set
    End Property
    Dim fBudgetId As BudgetXpo
    <Association("BudgetSuspensionDetailXpoReferencesBudgetXpo")> _
    Public Property BudgetId() As BudgetXpo
        Get
            Return fBudgetId
        End Get
        Set(ByVal value As BudgetXpo)
            SetPropertyValue(Of BudgetXpo)("BudgetId", fBudgetId, value)
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
    Dim fRaisedValue As Decimal
    Public Property RaisedValue() As Decimal
        Get
            Return fRaisedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RaisedValue", fRaisedValue, value)
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

    <Association("BudgetSuspensionCancellationDetailXpoReferencesBudgetSuspensionDetailXpo", GetType(BudgetSuspensionCancellationDetailXpo))> _
    Public ReadOnly Property BudgetSuspensionCancellationDetailXpo() As XPCollection(Of BudgetSuspensionCancellationDetailXpo)
        Get
            Return GetCollection(Of BudgetSuspensionCancellationDetailXpo)("BudgetSuspensionCancellationDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
