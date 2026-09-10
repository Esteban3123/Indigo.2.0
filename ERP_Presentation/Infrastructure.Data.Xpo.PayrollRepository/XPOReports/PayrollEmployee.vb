Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.Employee")> _
Public Class PayrollEmployee
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
    Dim fThirdPartyId As CommonThirdParty
    <Association("Payroll_EmployeeReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdParty
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdParty)
            SetPropertyValue(Of CommonThirdParty)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fAdmissionDate As DateTime
    Public Property AdmissionDate() As DateTime
        Get
            Return fAdmissionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdmissionDate", fAdmissionDate, value)
        End Set
    End Property
    Dim fHousingDeductionValue As Decimal
    Public Property HousingDeductionValue() As Decimal
        Get
            Return fHousingDeductionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HousingDeductionValue", fHousingDeductionValue, value)
        End Set
    End Property
    Dim fEducationDeductionValue As Decimal
    Public Property EducationDeductionValue() As Decimal
        Get
            Return fEducationDeductionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("EducationDeductionValue", fEducationDeductionValue, value)
        End Set
    End Property
    Dim fProfessionalRiskPercentage As Decimal
    Public Property ProfessionalRiskPercentage() As Decimal
        Get
            Return fProfessionalRiskPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProfessionalRiskPercentage", fProfessionalRiskPercentage, value)
        End Set
    End Property
    Dim fAverageYearHealth As Decimal
    Public Property AverageYearHealth() As Decimal
        Get
            Return fAverageYearHealth
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AverageYearHealth", fAverageYearHealth, value)
        End Set
    End Property
    Dim fPensionary As Boolean
    Public Property Pensionary() As Boolean
        Get
            Return fPensionary
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Pensionary", fPensionary, value)
        End Set
    End Property
    Dim fEmployeeTypeId As PayrollEmployeeTypeReportXpo
    <Association("Payroll_EmployeeReferencesPayroll_EmployeeType")> _
    Public Property EmployeeTypeId() As PayrollEmployeeTypeReportXpo
        Get
            Return fEmployeeTypeId
        End Get
        Set(ByVal value As PayrollEmployeeTypeReportXpo)
            SetPropertyValue(Of PayrollEmployeeTypeReportXpo)("EmployeeTypeId", fEmployeeTypeId, value)
        End Set
    End Property
    Dim fPensionaryStatus As Boolean
    Public Property PensionaryStatus() As Boolean
        Get
            Return fPensionaryStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PensionaryStatus", fPensionaryStatus, value)
        End Set
    End Property
    Dim fPensionaryTypeId As Integer
    Public Property PensionaryTypeId() As Integer
        Get
            Return fPensionaryTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PensionaryTypeId", fPensionaryTypeId, value)
        End Set
    End Property
    Dim fTradeUnion As Byte
    Public Property TradeUnion() As Byte
        Get
            Return fTradeUnion
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("TradeUnion", fTradeUnion, value)
        End Set
    End Property
    Dim fRetiredForeign As Boolean
    Public Property RetiredForeign() As Boolean
        Get
            Return fRetiredForeign
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RetiredForeign", fRetiredForeign, value)
        End Set
    End Property
    Dim fCostCenterId As Payroll_CostCenterReportXpo
    <Association("Payroll_EmployeeReferencesPayroll_CostCenter")> _
    Public Property CostCenterId() As Payroll_CostCenterReportXpo
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Payroll_CostCenterReportXpo)
            SetPropertyValue(Of Payroll_CostCenterReportXpo)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fWorkCenterId As Integer
    Public Property WorkCenterId() As Integer
        Get
            Return fWorkCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WorkCenterId", fWorkCenterId, value)
        End Set
    End Property
    Dim fRemainingVacationDays As Integer
    Public Property RemainingVacationDays() As Integer
        Get
            Return fRemainingVacationDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RemainingVacationDays", fRemainingVacationDays, value)
        End Set
    End Property
    Dim fVacationLastDateLiquidation As DateTime
    Public Property VacationLastDateLiquidation() As DateTime
        Get
            Return fVacationLastDateLiquidation
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("VacationLastDateLiquidation", fVacationLastDateLiquidation, value)
        End Set
    End Property
    Dim fPhotoPath As String
    Public Property PhotoPath() As String
        Get
            Return fPhotoPath
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PhotoPath", fPhotoPath, value)
        End Set
    End Property
    Dim fEmployeeFootprint() As Byte
    <Size(SizeAttribute.Unlimited)> _
    Public Property EmployeeFootprint() As Byte()
        Get
            Return fEmployeeFootprint
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("EmployeeFootprint", fEmployeeFootprint, value)
        End Set
    End Property
    Dim fUserModified As String
    <Size(10)> _
    Public Property UserModified() As String
        Get
            Return fUserModified
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserModified", fUserModified, value)
        End Set
    End Property
    Dim fDateModified As DateTime
    Public Property DateModified() As DateTime
        Get
            Return fDateModified
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateModified", fDateModified, value)
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
    <Association("Payroll_ContractReferencesPayroll_Employee", GetType(PayrollContract))> _
    Public ReadOnly Property Payroll_Contract() As XPCollection(Of PayrollContract)
        Get
            Return GetCollection(Of PayrollContract)("Payroll_Contract")
        End Get
    End Property
    <Association("Payroll_NoveltyReferencesPayroll_Employee", GetType(PayrollNovelty))> _
    Public ReadOnly Property Payroll_Novelty() As XPCollection(Of PayrollNovelty)
        Get
            Return GetCollection(Of PayrollNovelty)("Payroll_Novelty")
        End Get
    End Property
    <Association("Payroll_UnemployedLiquidationReferencesPayroll_Employee", GetType(PayrollUnemployedLiquidation))> _
    Public ReadOnly Property Payroll_UnemployedLiquidation() As XPCollection(Of PayrollUnemployedLiquidation)
        Get
            Return GetCollection(Of PayrollUnemployedLiquidation)("Payroll_UnemployedLiquidation")
        End Get
    End Property
    <Association("Payroll_ContractLiquidationReferencesPayroll_Employee", GetType(PayrollContractLiquidation))> _
    Public ReadOnly Property Payroll_ContractLiquidation() As XPCollection(Of PayrollContractLiquidation)
        Get
            Return GetCollection(Of PayrollContractLiquidation)("Payroll_ContractLiquidation")
        End Get
    End Property
    <Association("Payroll_AgreementsCReferencesPayroll_Employee", GetType(PayrollAgreementsCReportXpo))> _
    Public ReadOnly Property Payroll_AgreementsCs() As XPCollection(Of PayrollAgreementsCReportXpo)
        Get
            Return GetCollection(Of PayrollAgreementsCReportXpo)("Payroll_AgreementsCs")
        End Get
    End Property
    <Association("Payroll_LiquidationReferencesPayroll_Employee", GetType(PayrollLiquidation))> _
    Public ReadOnly Property Payroll_Liquidation() As XPCollection(Of PayrollLiquidation)
        Get
            Return GetCollection(Of PayrollLiquidation)("Payroll_Liquidation")
        End Get
    End Property
    <Association("Payroll_ScheduleReferencesPayroll_Employee", GetType(PayrollSchedule))> _
    Public ReadOnly Property Payroll_Schedule() As XPCollection(Of PayrollSchedule)
        Get
            Return GetCollection(Of PayrollSchedule)("Payroll_Schedule")
        End Get
    End Property
    <Association("Payroll_VacationPeriodReferencesPayroll_Employee", GetType(PayrollVacationPeriod))> _
    Public ReadOnly Property Payroll_VacationPeriod() As XPCollection(Of PayrollVacationPeriod)
        Get
            Return GetCollection(Of PayrollVacationPeriod)("Payroll_VacationPeriod")
        End Get
    End Property
    <Association("Payroll_ManualConceptsReferencesPayroll_Employee", GetType(PayrollManualConceptsReportXpo))> _
    Public ReadOnly Property Payroll_ManualConcepts() As XPCollection(Of PayrollManualConceptsReportXpo)
        Get
            Return GetCollection(Of PayrollManualConceptsReportXpo)("Payroll_ManualConcepts")
        End Get
    End Property
    <Association("Payroll_CostDistributionsReferencesPayroll_Employee", GetType(PayrollCostDistributionsReportXpo))> _
    Public ReadOnly Property PayrollCostDistributionsReportXpo() As XPCollection(Of PayrollCostDistributionsReportXpo)
        Get
            Return GetCollection(Of PayrollCostDistributionsReportXpo)("PayrollCostDistributionsReportXpo")
        End Get
    End Property
    <Association("Payroll_ScheduleDetailReferencesPayroll_Employee", GetType(PayrollScheduleDetail))> _
    Public ReadOnly Property Payroll_ScheduleDetails() As XPCollection(Of PayrollScheduleDetail)
        Get
            Return GetCollection(Of PayrollScheduleDetail)("Payroll_ScheduleDetails")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

