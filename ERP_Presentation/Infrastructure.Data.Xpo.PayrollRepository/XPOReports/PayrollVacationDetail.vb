Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.VacationDetail")>
Public Class PayrollVacationDetail

    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdVacation As PayrollVacation
    <Association("Payroll_VacationReferencesPayroll_Vacation")>
    Public Property IdVacation() As PayrollVacation
        Get
            Return fIdVacation
        End Get
        Set(ByVal value As PayrollVacation)
            SetPropertyValue(Of PayrollVacation)("IdVacation", fIdVacation, value)
        End Set
    End Property
    Dim fIdConcept As Integer
    Public Property IdConcept() As Integer
        Get
            Return fIdConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdConcept", fIdConcept, value)
        End Set
    End Property
    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fAccrued As Decimal
    Public Property Accrued() As Decimal
        Get
            Return fAccrued
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Accrued", fAccrued, value)
        End Set
    End Property
    Dim fDeducted As Decimal
    Public Property Deducted() As Decimal
        Get
            Return fDeducted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Deducted", fDeducted, value)
        End Set
    End Property
    Dim fConceptFormulate As String
    Public Property ConceptFormulate() As String
        Get
            Return fConceptFormulate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptFormulate", fConceptFormulate, value)
        End Set
    End Property
    Dim fReplaceConceptFormulate As String
    Public Property ReplaceConceptFormulate() As String
        Get
            Return fReplaceConceptFormulate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReplaceConceptFormulate", fReplaceConceptFormulate, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
