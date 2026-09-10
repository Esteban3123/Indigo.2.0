Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportContributionFundsIncentive")> _
Public Class PayrollVContributionFundsIncentiveReportXpo
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
    Dim fConceptCode As String
    <Size(4)> _
    Public Property ConceptCode() As String
        Get
            Return fConceptCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptCode", fConceptCode, value)
        End Set
    End Property
    Dim fConceptClass As String
    <Size(3)> _
    Public Property ConceptClass() As String
        Get
            Return fConceptClass
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptClass", fConceptClass, value)
        End Set
    End Property
    Dim fGroupCode As String
    <Size(20)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    <Size(150)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
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
    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fPeriodInitialDate As DateTime
    Public Property PeriodInitialDate() As DateTime
        Get
            Return fPeriodInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodInitialDate", fPeriodInitialDate, value)
        End Set
    End Property
    Dim fPeriodEndDate As DateTime
    Public Property PeriodEndDate() As DateTime
        Get
            Return fPeriodEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodEndDate", fPeriodEndDate, value)
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
    Dim fBasicSalary As Decimal
    Public Property BasicSalary() As Decimal
        Get
            Return fBasicSalary
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BasicSalary", fBasicSalary, value)
        End Set
    End Property
    Dim fWorkingDays As Short
    Public Property WorkingDays() As Short
        Get
            Return fWorkingDays
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("WorkingDays", fWorkingDays, value)
        End Set
    End Property
    Dim fIBCUnemployment As Decimal
    Public Property IBCUnemployment() As Decimal
        Get
            Return fIBCUnemployment
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IBCUnemployment", fIBCUnemployment, value)
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
    Dim fRegisterStatus As String
    <Size(1)> _
    Public Property RegisterStatus() As String
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property
    'Dim fRegisterStatus As Byte
    'Public Property RegisterStatus() As Byte
    '    Get
    '        Return fRegisterStatus
    '    End Get
    '    Set(ByVal value As Byte)
    '        SetPropertyValue(Of Byte)("RegisterStatus", fRegisterStatus, value)
    '    End Set
    'End Property
    Dim fIdThirdParty As Integer
    Public Property IdThirdParty() As Integer
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fFundCode As String
    <Size(20)> _
    Public Property FundCode() As String
        Get
            Return fFundCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FundCode", fFundCode, value)
        End Set
    End Property
    Dim fFundName As String
    Public Property FundName() As String
        Get
            Return fFundName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FundName", fFundName, value)
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
