'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Tarjetas usado en los servicios Xpo
''' </summary>
<Persistent("Treasury.ExpenseConceptCashRegisters")> _
Public Class ExpenseConceptCashRegisterXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdExpenseConcept As ExpenseConceptXpo
    <Association("TreasuryExpConceptCRegisterRelationExpenseConcept")> _
    Public Property IdExpenseConcept() As ExpenseConceptXpo
        Get
            Return fIdExpenseConcept
        End Get
        Set(ByVal value As ExpenseConceptXpo)
            SetPropertyValue(Of ExpenseConceptXpo)("IdExpenseConcept", fIdExpenseConcept, value)
        End Set
    End Property
    Dim fIdCashRegister As CashRegisterXpo
    <Association("TreasuryExpConceptCRegisterRelationCashRegister")> _
    Public Property IdCashRegister() As CashRegisterXpo
        Get
            Return fIdCashRegister
        End Get
        Set(ByVal value As CashRegisterXpo)
            SetPropertyValue(Of CashRegisterXpo)("IdCashRegister", fIdCashRegister, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region
End Class
