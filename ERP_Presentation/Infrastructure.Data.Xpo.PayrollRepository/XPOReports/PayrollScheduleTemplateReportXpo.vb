Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ScheduleTemplate")> _
Public Class PayrollScheduleTemplateReportXpo
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
    Dim fCode As String
    <Size(3)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fGroupId As PayrollGroup
    <Association("Payroll_ScheduleTemplateReferencesPayroll_Group")> _
    Public Property GroupId() As PayrollGroup
        Get
            Return fGroupId
        End Get
        Set(ByVal value As PayrollGroup)
            SetPropertyValue(Of PayrollGroup)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fLetter As String
    <Size(2)> _
    Public Property Letter() As String
        Get
            Return fLetter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Letter", fLetter, value)
        End Set
    End Property
    Dim fName As String
    <Size(75)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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
    <Association("Payroll_ScheduleDetailReferencesPayroll_ScheduleTemplate", GetType(PayrollScheduleDetail))> _
    Public ReadOnly Property Payroll_ScheduleDetails() As XPCollection(Of PayrollScheduleDetail)
        Get
            Return GetCollection(Of PayrollScheduleDetail)("Payroll_ScheduleDetails")
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
