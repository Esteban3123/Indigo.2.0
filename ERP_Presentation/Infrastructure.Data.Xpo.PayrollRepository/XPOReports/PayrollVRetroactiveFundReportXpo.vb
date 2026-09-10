Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.VRetroactiveFund")> _
Public Class PayrollVRetroactiveFundReportXpo
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
    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
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
    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fNitEmployee As String
    <Size(20)> _
    Public Property NitEmployee() As String
        Get
            Return fNitEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitEmployee", fNitEmployee, value)
        End Set
    End Property
    Dim fIdEmployee As Integer
    Public Property IdEmployee() As Integer
        Get
            Return fIdEmployee
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdEmployee", fIdEmployee, value)
        End Set
    End Property
    Dim fEmployeeName As String
    <Size(300)> _
    Public Property EmployeeName() As String
        Get
            Return fEmployeeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EmployeeName", fEmployeeName, value)
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
    Dim fPayrollDays As Integer
    Public Property PayrollDays() As Integer
        Get
            Return fPayrollDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PayrollDays", fPayrollDays, value)
        End Set
    End Property
    Dim fIBC As Decimal
    Public Property IBC() As Decimal
        Get
            Return fIBC
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IBC", fIBC, value)
        End Set
    End Property
    Dim fFundValue As Decimal
    Public Property FundValue() As Decimal
        Get
            Return fFundValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FundValue", fFundValue, value)
        End Set
    End Property
    Dim fFundValuePension As Decimal
    Public Property FundValuePension() As Decimal
        Get
            Return fFundValuePension
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FundValuePension", fFundValuePension, value)
        End Set
    End Property
    Dim fRetroactiveDate As DateTime
    Public Property RetroactiveDate() As DateTime
        Get
            Return fRetroactiveDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RetroactiveDate", fRetroactiveDate, value)
        End Set
    End Property
    Dim fRegisterStatus As Char
    Public Property RegisterStatus() As Char
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("RegisterStatus", fRegisterStatus, value)
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
    Dim fHealthFund As String
    Public Property HealthFund() As String
        Get
            Return fHealthFund
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthFund", fHealthFund, value)
        End Set
    End Property
    Dim fPensionFund As String
    Public Property PensionFund() As String
        Get
            Return fPensionFund
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PensionFund", fPensionFund, value)
        End Set
    End Property
    Dim fVoluntaryHealthFund As String
    Public Property VoluntaryHealthFund() As String
        Get
            Return fVoluntaryHealthFund
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoluntaryHealthFund", fVoluntaryHealthFund, value)
        End Set
    End Property
    Dim fVoluntaryPensionFund As String
    Public Property VoluntaryPensionFund() As String
        Get
            Return fVoluntaryPensionFund
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoluntaryPensionFund", fVoluntaryPensionFund, value)
        End Set
    End Property
    Dim fRiskFund As String
    Public Property RiskFund() As String
        Get
            Return fRiskFund
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RiskFund", fRiskFund, value)
        End Set
    End Property
    Dim fUnemploymentFund As String
    Public Property UnemploymentFund() As String
        Get
            Return fUnemploymentFund
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnemploymentFund", fUnemploymentFund, value)
        End Set
    End Property


    Dim fBranchOfficeId As String
    Public Property BranchOfficeId() As String
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
