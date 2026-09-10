Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Budget.SuspensionCancellationDetail")> _
Public Class BudgetSuspensionCancellationDetailXpo
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
    Dim fSuspensionCancellationId As BudgetSuspensionCancellationXpo
    <Association("BudgetSuspensionCancellationDetailXpoReferencesBudgetSuspensionCancellationXpo")> _
    Public Property SuspensionCancellationId() As BudgetSuspensionCancellationXpo
        Get
            Return fSuspensionCancellationId
        End Get
        Set(ByVal value As BudgetSuspensionCancellationXpo)
            SetPropertyValue(Of BudgetSuspensionCancellationXpo)("SuspensionCancellationId", fSuspensionCancellationId, value)
        End Set
    End Property
    Dim fSuspensionDetailId As BudgetSuspensionDetailXpo
    <Association("BudgetSuspensionCancellationDetailXpoReferencesBudgetSuspensionDetailXpo")> _
    Public Property SuspensionDetailId() As BudgetSuspensionDetailXpo
        Get
            Return fSuspensionDetailId
        End Get
        Set(ByVal value As BudgetSuspensionDetailXpo)
            SetPropertyValue(Of BudgetSuspensionDetailXpo)("SuspensionDetailId", fSuspensionDetailId, value)
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
