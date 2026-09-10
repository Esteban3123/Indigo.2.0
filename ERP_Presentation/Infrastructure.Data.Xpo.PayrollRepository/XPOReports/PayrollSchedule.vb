Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payroll.Schedule")> _
Public Class PayrollSchedule
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
    'Dim fEmployeeId As Integer
    'Public Property EmployeeId() As Integer
    '    Get
    '        Return fEmployeeId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("EmployeeId", fEmployeeId, value)
    '    End Set
    'End Property
    Dim fPeriod As String
    <Size(7)> _
    Public Property Period() As String
        Get
            Return fPeriod
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Period", fPeriod, value)
        End Set
    End Property
    Dim fFunctionalUnitId As Payroll_FunctionalUnit
    <Association("Payroll_ScheduleReferencesPayroll_FunctionalUnit")> _
    Public Property FunctionalUnitId() As Payroll_FunctionalUnit
        Get
            Return fFunctionalUnitId
        End Get
        Set(ByVal value As Payroll_FunctionalUnit)
            SetPropertyValue(Of Payroll_FunctionalUnit)("FunctionalUnitId", fFunctionalUnitId, value)
        End Set
    End Property
    Dim fD01 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail")> _
    Public Property D01() As PayrollScheduleDetail
        Get
            Return fD01
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D01", fD01, value)
        End Set
    End Property
    Dim fD02 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail1")> _
    Public Property D02() As PayrollScheduleDetail
        Get
            Return fD02
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D02", fD02, value)
        End Set
    End Property
    Dim fD03 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail2")> _
    Public Property D03() As PayrollScheduleDetail
        Get
            Return fD03
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D03", fD03, value)
        End Set
    End Property
    Dim fD04 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail3")> _
    Public Property D04() As PayrollScheduleDetail
        Get
            Return fD04
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D04", fD04, value)
        End Set
    End Property
    Dim fD05 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail4")> _
    Public Property D05() As PayrollScheduleDetail
        Get
            Return fD05
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D05", fD05, value)
        End Set
    End Property
    Dim fD06 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail5")> _
    Public Property D06() As PayrollScheduleDetail
        Get
            Return fD06
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D06", fD06, value)
        End Set
    End Property
    Dim fD07 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail6")> _
    Public Property D07() As PayrollScheduleDetail
        Get
            Return fD07
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D07", fD07, value)
        End Set
    End Property
    Dim fD08 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail7")> _
    Public Property D08() As PayrollScheduleDetail
        Get
            Return fD08
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D08", fD08, value)
        End Set
    End Property
    Dim fD09 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail8")> _
    Public Property D09() As PayrollScheduleDetail
        Get
            Return fD09
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D09", fD09, value)
        End Set
    End Property
    Dim fD10 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail9")> _
    Public Property D10() As PayrollScheduleDetail
        Get
            Return fD10
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D10", fD10, value)
        End Set
    End Property
    Dim fD11 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail10")> _
    Public Property D11() As PayrollScheduleDetail
        Get
            Return fD11
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D11", fD11, value)
        End Set
    End Property
    Dim fD12 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail11")> _
    Public Property D12() As PayrollScheduleDetail
        Get
            Return fD12
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D12", fD12, value)
        End Set
    End Property
    Dim fD13 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail12")> _
    Public Property D13() As PayrollScheduleDetail
        Get
            Return fD13
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D13", fD13, value)
        End Set
    End Property
    Dim fD14 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail13")> _
    Public Property D14() As PayrollScheduleDetail
        Get
            Return fD14
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D14", fD14, value)
        End Set
    End Property
    Dim fD15 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail14")> _
    Public Property D15() As PayrollScheduleDetail
        Get
            Return fD15
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D15", fD15, value)
        End Set
    End Property
    Dim fD16 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail15")> _
    Public Property D16() As PayrollScheduleDetail
        Get
            Return fD16
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D16", fD16, value)
        End Set
    End Property
    Dim fD17 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail16")> _
    Public Property D17() As PayrollScheduleDetail
        Get
            Return fD17
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D17", fD17, value)
        End Set
    End Property
    Dim fD18 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail17")> _
    Public Property D18() As PayrollScheduleDetail
        Get
            Return fD18
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D18", fD18, value)
        End Set
    End Property
    Dim fD19 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail18")> _
    Public Property D19() As PayrollScheduleDetail
        Get
            Return fD19
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D19", fD19, value)
        End Set
    End Property
    Dim fD20 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail19")> _
    Public Property D20() As PayrollScheduleDetail
        Get
            Return fD20
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D20", fD20, value)
        End Set
    End Property
    Dim fD21 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail20")> _
    Public Property D21() As PayrollScheduleDetail
        Get
            Return fD21
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D21", fD21, value)
        End Set
    End Property
    Dim fD22 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail21")> _
    Public Property D22() As PayrollScheduleDetail
        Get
            Return fD22
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D22", fD22, value)
        End Set
    End Property
    Dim fD23 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail22")> _
    Public Property D23() As PayrollScheduleDetail
        Get
            Return fD23
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D23", fD23, value)
        End Set
    End Property
    Dim fD24 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail23")> _
    Public Property D24() As PayrollScheduleDetail
        Get
            Return fD24
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D24", fD24, value)
        End Set
    End Property
    Dim fD25 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail24")> _
    Public Property D25() As PayrollScheduleDetail
        Get
            Return fD25
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D25", fD25, value)
        End Set
    End Property
    Dim fD26 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail25")> _
    Public Property D26() As PayrollScheduleDetail
        Get
            Return fD26
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D26", fD26, value)
        End Set
    End Property
    Dim fD27 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail26")> _
    Public Property D27() As PayrollScheduleDetail
        Get
            Return fD27
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D27", fD27, value)
        End Set
    End Property
    Dim fD28 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail27")> _
    Public Property D28() As PayrollScheduleDetail
        Get
            Return fD28
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D28", fD28, value)
        End Set
    End Property
    Dim fD29 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail28")> _
    Public Property D29() As PayrollScheduleDetail
        Get
            Return fD29
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D29", fD29, value)
        End Set
    End Property
    Dim fD30 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail29")> _
    Public Property D30() As PayrollScheduleDetail
        Get
            Return fD30
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D30", fD30, value)
        End Set
    End Property
    Dim fD31 As PayrollScheduleDetail
    <Association("Payroll_ScheduleReferencesPayroll_ScheduleDetail30")> _
    Public Property D31() As PayrollScheduleDetail
        Get
            Return fD31
        End Get
        Set(ByVal value As PayrollScheduleDetail)
            SetPropertyValue(Of PayrollScheduleDetail)("D31", fD31, value)
        End Set
    End Property
    Dim fTotalHour As Decimal
    Public Property TotalHour() As Decimal
        Get
            Return fTotalHour
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalHour", fTotalHour, value)
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

    Dim fDEmployeeId As PayrollEmployee
    <Association("Payroll_ScheduleReferencesPayroll_Employee")> _
    Public Property EmployeeId() As PayrollEmployee
        Get
            Return fDEmployeeId
        End Get
        Set(ByVal value As PayrollEmployee)
            SetPropertyValue(Of PayrollEmployee)("EmployeeId", fDEmployeeId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
