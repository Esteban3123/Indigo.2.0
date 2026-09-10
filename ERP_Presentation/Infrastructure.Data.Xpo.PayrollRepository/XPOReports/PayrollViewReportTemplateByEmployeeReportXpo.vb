Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportTemplateByEmployee")> _
Public Class PayrollViewReportTemplateByEmployeeReportXpo
    Inherits XPLiteObject
    Dim fId As Long
    <Key(True)> _
    Public Property Id() As Long
        Get
            Return fId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Id", fId, value)
        End Set
    End Property
    Dim fNit As String
    <Size(20)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fNameEmployee As String
    <Size(300)> _
    Public Property NameEmployee() As String
        Get
            Return fNameEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameEmployee", fNameEmployee, value)
        End Set
    End Property
    Dim fDateDetail As DateTime
    Public Property DateDetail() As DateTime
        Get
            Return fDateDetail
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateDetail", fDateDetail, value)
        End Set
    End Property
    Dim fTotalNumberHours As Decimal
    Public Property TotalNumberHours() As Decimal
        Get
            Return fTotalNumberHours
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalNumberHours", fTotalNumberHours, value)
        End Set
    End Property
    Dim fEmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fNameConcept As String
    <Size(50)> _
    Public Property NameConcept() As String
        Get
            Return fNameConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameConcept", fNameConcept, value)
        End Set
    End Property
    Dim fScheduleDetailHour As Decimal
    Public Property ScheduleDetailHour() As Decimal
        Get
            Return fScheduleDetailHour
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ScheduleDetailHour", fScheduleDetailHour, value)
        End Set
    End Property
    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fNameGroup As String
    <Size(150)> _
    Public Property NameGroup() As String
        Get
            Return fNameGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameGroup", fNameGroup, value)
        End Set
    End Property
    Dim fFunctionalUnitId As Integer
    Public Property FunctionalUnitId() As Integer
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property
    Dim fFunctionalUnitName As String
    <Size(50)> _
    Public Property FunctionalUnitName() As String
        Get
            Return fFunctionalUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitName", fFunctionalUnitName, value)
        End Set
    End Property
    Dim fScheduleTemplateCode As String
    <Size(3)> _
    Public Property ScheduleTemplateCode() As String
        Get
            Return fScheduleTemplateCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ScheduleTemplateCode", fScheduleTemplateCode, value)
        End Set
    End Property
    Dim fScheduleTemplateName As String
    <Size(75)> _
    Public Property ScheduleTemplateName() As String
        Get
            Return fScheduleTemplateName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ScheduleTemplateName", fScheduleTemplateName, value)
        End Set
    End Property



    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
