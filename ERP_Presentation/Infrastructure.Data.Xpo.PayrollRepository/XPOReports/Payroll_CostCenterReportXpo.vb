Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.CostCenter")> _
Public Class Payroll_CostCenterReportXpo

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
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fName As String
    <Size(200)> _
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
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    <Association("Payroll_EmployeeReferencesPayroll_CostCenter", GetType(PayrollEmployee))> _
    Public ReadOnly Property Payroll_Employees() As XPCollection(Of PayrollEmployee)
        Get
            Return GetCollection(Of PayrollEmployee)("Payroll_Employees")
        End Get
    End Property
    <Association("Payroll_CostDistributionsReferencesPayroll_CostCenter", GetType(PayrollCostDistributionsReportXpo))> _
    Public ReadOnly Property PayrollCostDistributionsReportXpo() As XPCollection(Of PayrollCostDistributionsReportXpo)
        Get
            Return GetCollection(Of PayrollCostDistributionsReportXpo)("PayrollCostDistributionsReportXpo")
        End Get
    End Property
    <Association("Payroll_LiquidationReferencesPayroll_CostCenter", GetType(PayrollLiquidation))> _
    Public ReadOnly Property PayrollLiquidation() As XPCollection(Of PayrollLiquidation)
        Get
            Return GetCollection(Of PayrollLiquidation)("PayrollLiquidation")
        End Get
    End Property
    <Association("Payroll_FunctionalUnitReferencesPayroll_CostCenter", GetType(Payroll_FunctionalUnit))> _
    Public ReadOnly Property Payroll_FunctionalUnits() As XPCollection(Of Payroll_FunctionalUnit)
        Get
            Return GetCollection(Of Payroll_FunctionalUnit)("Payroll_FunctionalUnits")
        End Get
    End Property
    <Association("Payroll_ScheduleDetailReferencesPayroll_CostCenter", GetType(PayrollScheduleDetail))> _
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
