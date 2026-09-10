Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportTotalPaidEmployeeIncentive")> _
Public Class ViewReportTotalPaidEmployeeIncentive
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
    Dim fEmployeeId As Long
    Public Property EmployeeId() As Long
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fGroupCode As String
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property
    Dim fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fPeriodInitialDate As Date
    Public Property PeriodInitialDate() As Date
        Get
            Return fPeriodInitialDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("PeriodInitialDate", fPeriodInitialDate, value)
        End Set
    End Property
    Dim fPeriodEndDate As Date
    Public Property PeriodEndDate() As Date
        Get
            Return fPeriodEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("PeriodEndDate", fPeriodEndDate, value)
        End Set
    End Property

    Dim fBasicSalary As Decimal
    Public Property BasicSalary() As Decimal
        Get
            Return fBasicSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BasicSalary", fBasicSalary, value)
        End Set
    End Property

    Dim fTotalAccrued As Decimal
    Public Property TotalAccrued() As Decimal
        Get
            Return fTotalAccrued
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalAccrued", fTotalAccrued, value)
        End Set
    End Property

    Dim fTotalDeducted As Decimal
    Public Property TotalDeducted() As Decimal
        Get
            Return fTotalDeducted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalDeducted", fTotalDeducted, value)
        End Set
    End Property

    Dim fTotal As Decimal
    Public Property Total() As Decimal
        Get
            Return fTotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Total", fTotal, value)
        End Set
    End Property

    Dim fRegisterStatus As Integer
    Public Property RegisterStatus() As Integer
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property


    Dim fFunctionalCode As String
    Public Property FunctionalCode() As String
        Get
            Return fFunctionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalCode", fFunctionalCode, value)
        End Set
    End Property

    Dim fFunctionalName As String
    Public Property FunctionalName() As String
        Get
            Return fFunctionalName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalName", fFunctionalName, value)
        End Set
    End Property

    Dim fCostCenterCode As String
    Public Property CostCenterCode() As String
        Get
            Return fCostCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCode", fCostCenterCode, value)
        End Set
    End Property

    Dim fCostCenterName As String
    Public Property CostCenterName() As String
        Get
            Return fCostCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterName", fCostCenterName, value)
        End Set
    End Property
    Dim fPeriod As Char
    Public Property Period() As Char
        Get
            Return fPeriod
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Period", fPeriod, value)
        End Set
    End Property

    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Long
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
