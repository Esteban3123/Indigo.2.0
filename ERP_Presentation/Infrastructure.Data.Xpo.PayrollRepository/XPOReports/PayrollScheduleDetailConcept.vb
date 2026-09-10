Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.ScheduleDetailConcept")> _
Public Class PayrollScheduleDetailConcept
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
    Dim fScheduleDetailHourId As PayrollScheduleDetailHour
    <Association("Payroll_ScheduleDetailConceptReferencesPayroll_ScheduleDetailHour")> _
    Public Property ScheduleDetailHourId() As PayrollScheduleDetailHour
        Get
            Return fScheduleDetailHourId
        End Get
        Set(ByVal value As PayrollScheduleDetailHour)
            SetPropertyValue(Of PayrollScheduleDetailHour)("ScheduleDetailHourId", fScheduleDetailHourId, value)
        End Set
    End Property
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fConceptId As PayrollConcept
    <Association("Payroll_ScheduleDetailConceptReferencesPayroll_Concept")> _
    Public Property ConceptId() As PayrollConcept
        Get
            Return fConceptId
        End Get
        Set(ByVal value As PayrollConcept)
            SetPropertyValue(Of PayrollConcept)("ConceptId", fConceptId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
