Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.ExpenseConceptCashRegisters")> _
 Public Class TreasuryExpenseConceptCashRegistersXpo
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
    Dim fIdExpenseConcept As TreasuryExpenseConceptsXpo
    <Association("TreasuryExpenseConceptCashRegistersXpoReferencesTreasuryExpenseConceptsXpo")> _
    Public Property IdExpenseConcept() As TreasuryExpenseConceptsXpo
        Get
            Return fIdExpenseConcept
        End Get
        Set(ByVal value As TreasuryExpenseConceptsXpo)
            SetPropertyValue(Of TreasuryExpenseConceptsXpo)("IdExpenseConcept", fIdExpenseConcept, value)
        End Set
    End Property
    Dim fIdCashRegister As TreasuryCashRegistersXpo
    <Association("TreasuryExpenseConceptCashRegistersXpoReferencesTreasuryCashRegistersXpo")> _
    Public Property IdCashRegister() As TreasuryCashRegistersXpo
        Get
            Return fIdCashRegister
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
