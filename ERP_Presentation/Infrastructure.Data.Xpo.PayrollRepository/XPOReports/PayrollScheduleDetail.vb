Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.ScheduleDetail")> _
Public Class PayrollScheduleDetail
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
    Dim fGroupId As PayrollGroup
    <Association("Payroll_ScheduleDetailReferencesPayroll_Group")> _
    Public Property GroupId() As PayrollGroup
        Get
            Return fGroupId
        End Get
        Set(ByVal value As PayrollGroup)
            SetPropertyValue(Of PayrollGroup)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fEmployeeId As PayrollEmployee
    <Association("Payroll_ScheduleDetailReferencesPayroll_Employee")> _
    Public Property EmployeeId() As PayrollEmployee
        Get
            Return fEmployeeId
        End Get
        Set(ByVal value As PayrollEmployee)
            SetPropertyValue(Of PayrollEmployee)("EmployeeId", fEmployeeId, value)
        End Set
    End Property
    Dim fContractId As PayrollContract
    <Association("Payroll_ScheduleDetailReferencesPayroll_Contract")> _
    Public Property ContractId() As PayrollContract
        Get
            Return fContractId
        End Get
        Set(ByVal value As PayrollContract)
            SetPropertyValue(Of PayrollContract)("ContractId", fContractId, value)
        End Set
    End Property
    Dim fCompanyId As Integer
    Public Property CompanyId() As Integer
        Get
            Return fCompanyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CompanyId", fCompanyId, value)
        End Set
    End Property
    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Integer
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property
    Dim fFunctionalUnitId As Payroll_FunctionalUnit
    <Association("Payroll_ScheduleDetailReferencesPayroll_FunctionalUnit")> _
    Public Property FunctionalUnitId() As Payroll_FunctionalUnit
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As Payroll_FunctionalUnit)
            SetPropertyValue(Of Payroll_FunctionalUnit)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property
    Dim fCenterCostId As Payroll_CostCenterReportXpo
    <Association("Payroll_ScheduleDetailReferencesPayroll_CostCenter")> _
    Public Property CenterCostId() As Payroll_CostCenterReportXpo
        Get
            Return fCenterCostId
        End Get
        Set(ByVal value As Payroll_CostCenterReportXpo)
            SetPropertyValue(Of Payroll_CostCenterReportXpo)("CenterCostId", fCenterCostId, value)
        End Set
    End Property
    Dim fScheduleFunctionalUnitId As Payroll_FunctionalUnit
    <Association("Payroll_ScheduleDetailReferencesPayroll_FunctionalUnit1")> _
    Public Property ScheduleFunctionalUnitId() As Payroll_FunctionalUnit
        Get
            Return fScheduleFunctionalUnitId
        End Get
        Set(ByVal value As Payroll_FunctionalUnit)
            SetPropertyValue(Of Payroll_FunctionalUnit)("ScheduleFunctionalUnitId", fScheduleFunctionalUnitId, value)
        End Set
    End Property
    Dim fPayrollLiquidationNumber As String
    <Size(50)> _
    Public Property PayrollLiquidationNumber() As String
        Get
            Return fPayrollLiquidationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PayrollLiquidationNumber", fPayrollLiquidationNumber, value)
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
    Dim fDateDetail As DateTime
    Public Property DateDetail() As DateTime
        Get
            Return fDateDetail
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateDetail", fDateDetail, value)
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
    Dim fScheduleTemplateId As PayrollScheduleTemplateReportXpo
    <Association("Payroll_ScheduleDetailReferencesPayroll_ScheduleTemplate")> _
    Public Property ScheduleTemplateId() As PayrollScheduleTemplateReportXpo
        Get
            Return fScheduleTemplateId
        End Get
        Set(ByVal value As PayrollScheduleTemplateReportXpo)
            SetPropertyValue(Of PayrollScheduleTemplateReportXpo)("ScheduleTemplateId", fScheduleTemplateId, value)
        End Set
    End Property
    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
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
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail", GetType(PayrollSchedule))> _
    Public ReadOnly Property PayrollSchedule() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("PayrollSchedule")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail1", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule1() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule1")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail2", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule2() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule2")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail3", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule3() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule3")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail4", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule4() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule4")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail5", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule5() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule5")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail6", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule6() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule6")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail7", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule7() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule7")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail8", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule8() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule8")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail9", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule9() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule9")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail10", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule10() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule10")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail11", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule11() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule11")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail12", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule12() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule12")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail13", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule13() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule13")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail14", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule14() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule14")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail15", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule15() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule15")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail16", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule16() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule16")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail17", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule17() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule17")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail18", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule18() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule18")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail19", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule19() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule19")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail20", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule20() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule20")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail21", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule21() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule21")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail22", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule22() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule22")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail23", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule23() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule23")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail24", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule24() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule24")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail25", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule25() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule25")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail26", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule26() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule26")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail27", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule27() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule27")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail28", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule28() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule28")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail29", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule29() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule29")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail30", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule30() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule30")
        End Get
    End Property
    <Association("Payroll_ScheduleDetailHourReferencesPayroll_ScheduleDetail", GetType(PayrollScheduleDetailHour))> _
    Public ReadOnly Property Payroll_ScheduleDetailHour() As XPCollection(Of PayrollScheduleDetailHour)
        Get
            Return GetCollection(Of PayrollScheduleDetailHour)("Payroll_ScheduleDetailHour")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
