Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.ScheduleDetailHour")> _
Public Class PayrollScheduleDetailHour
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
    Dim fScheduleDetailId As PayrollScheduleDetail
    <Association("Payroll_ScheduleDetailHourReferencesPayroll_ScheduleDetail")> _
    Public Property ScheduleDetailId() As PayrollScheduleDetail
        Get
            Return fScheduleDetailId
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("ScheduleDetailId", fScheduleDetailId, value)
        End Set
    End Property
    Dim fDateTimeInitial As DateTime
    Public Property DateTimeInitial() As DateTime
        Get
            Return fDateTimeInitial
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateTimeInitial", fDateTimeInitial, value)
        End Set
    End Property
    Dim fDateTimeEnding As DateTime
    Public Property DateTimeEnding() As DateTime
        Get
            Return fDateTimeEnding
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateTimeEnding", fDateTimeEnding, value)
        End Set
    End Property
    Dim fTotalNumberHours As Integer
    Public Property TotalNumberHours() As Integer
        Get
            Return fTotalNumberHours
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalNumberHours", fTotalNumberHours, value)
        End Set
    End Property
    Dim fNextDay As Boolean
    Public Property NextDay() As Boolean
        Get
            Return fNextDay
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NextDay", fNextDay, value)
        End Set
    End Property
    Dim fEvent1 As Boolean
    <Persistent("Event")> _
    Public Property Event1() As Boolean
        Get
            Return fEvent1
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Event1", fEvent1, value)
        End Set
    End Property
    Dim fApproved As Boolean
    Public Property Approved() As Boolean
        Get
            Return fApproved
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Approved", fApproved, value)
        End Set
    End Property
    Dim fAppliedLiquidationConcept As Boolean
    Public Property AppliedLiquidationConcept() As Boolean
        Get
            Return fAppliedLiquidationConcept
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AppliedLiquidationConcept", fAppliedLiquidationConcept, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    <Association("Payroll_ScheduleDetailConceptReferencesPayroll_ScheduleDetailHour", GetType(PayrollScheduleDetailConcept))> _
    Public ReadOnly Property PayrollScheduleDetailConcept() As XPCollection(Of PayrollScheduleDetailConcept)
        Get
            Return GetCollection(Of PayrollScheduleDetailConcept)("PayrollScheduleDetailConcept")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
