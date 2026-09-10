Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.Group")> _
Public Class PayrollGroup
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
    '<Indexed(Name:="IX_Group", Unique:=True)> _
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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
    Dim fPayrollParameterId As PayrollPayrollParameter
    <Association("Payroll_GroupReferencesPayroll_PayrollParameter")> _
    Public Property PayrollParameterId() As PayrollPayrollParameter
        Get
            Return fPayrollParameterId
        End Get
        Set(ByVal value As PayrollPayrollParameter)
            SetPropertyValue(Of PayrollPayrollParameter)("PayrollParameterId", fPayrollParameterId, value)
        End Set
    End Property
    Dim fName As String
    <Size(150)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fLiquidation As Byte
    Public Property Liquidation() As Byte
        Get
            Return fLiquidation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Liquidation", fLiquidation, value)
        End Set
    End Property
    Dim fLastDateLiquidation As DateTime
    Public Property LastDateLiquidation() As DateTime
        Get
            Return fLastDateLiquidation
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastDateLiquidation", fLastDateLiquidation, value)
        End Set
    End Property
    Dim fNextDateLiquidation As DateTime
    Public Property NextDateLiquidation() As DateTime
        Get
            Return fNextDateLiquidation
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("NextDateLiquidation", fNextDateLiquidation, value)
        End Set
    End Property
    Dim fMonth As Char
    Public Property Month() As Char
        Get
            Return fMonth
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Month", fMonth, value)
        End Set
    End Property
    Dim fHandlingProvisions As Boolean
    Public Property HandlingProvisions() As Boolean
        Get
            Return fHandlingProvisions
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlingProvisions", fHandlingProvisions, value)
        End Set
    End Property
    Dim fContractClass As Byte
    Public Property ContractClass() As Byte
        Get
            Return fContractClass
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ContractClass", fContractClass, value)
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
    <Association("Payroll_ContractReferencesPayroll_Group", GetType(PayrollContract))> _
    Public ReadOnly Property Payroll_Contract() As XPCollection(Of PayrollContract)
        Get
            Return GetCollection(Of PayrollContract)("Payroll_Contract")
        End Get
    End Property
    <Association("Payroll_LiquidationReferencesPayroll_Group", GetType(PayrollLiquidation))> _
    Public ReadOnly Property Payroll_Liquidation() As XPCollection(Of PayrollLiquidation)
        Get
            Return GetCollection(Of PayrollLiquidation)("Payroll_Liquidation")
        End Get
    End Property
    <Association("PayrollNoveltyReferencesPayrollGroup", GetType(PayrollNovelty))> _
    Public ReadOnly Property PayrollNoveltys() As XPCollection(Of PayrollNovelty)
        Get
            Return GetCollection(Of PayrollNovelty)("PayrollNoveltys")
        End Get
    End Property
    <Association("Payroll_AgreementsCReferencesPayroll_Group", GetType(PayrollAgreementsCReportXpo))> _
    Public ReadOnly Property PayrollAgreementsCReport() As XPCollection(Of PayrollAgreementsCReportXpo)
        Get
            Return GetCollection(Of PayrollAgreementsCReportXpo)("PayrollAgreementsCReport")
        End Get
    End Property
    <Association("Payroll_ManualConceptsReferencesPayroll_Group", GetType(PayrollManualConceptsReportXpo))> _
    Public ReadOnly Property Payroll_ManualConceptss() As XPCollection(Of PayrollManualConceptsReportXpo)
        Get
            Return GetCollection(Of PayrollManualConceptsReportXpo)("Payroll_ManualConceptss")
        End Get
    End Property
    <Association("Payroll_CostDistributionsReferencesPayroll_Group", GetType(PayrollCostDistributionsReportXpo))> _
    Public ReadOnly Property PayrollCostDistributionsReportXpo() As XPCollection(Of PayrollCostDistributionsReportXpo)
        Get
            Return GetCollection(Of PayrollCostDistributionsReportXpo)("PayrollCostDistributionsReportXpo")
        End Get
    End Property
    <Association("Payroll_IncentivePaymentReferencesPayroll_Group", GetType(PayrollIncentivePayment))> _
    Public ReadOnly Property PayrollIncentivePayment() As XPCollection(Of PayrollIncentivePayment)
        Get
            Return GetCollection(Of PayrollIncentivePayment)("PayrollIncentivePayment")
        End Get
    End Property
    <Association("Payroll_ScheduleTemplateReferencesPayroll_Group", GetType(PayrollScheduleTemplateReportXpo))> _
    Public ReadOnly Property Payroll_ScheduleTemplates() As XPCollection(Of PayrollScheduleTemplateReportXpo)
        Get
            Return GetCollection(Of PayrollScheduleTemplateReportXpo)("Payroll_ScheduleTemplates")
        End Get
    End Property
    <Association("Payroll_ScheduleDetailReferencesPayroll_Group", GetType(PayrollScheduleDetail))>
    Public ReadOnly Property Payroll_ScheduleDetails() As XPCollection(Of PayrollScheduleDetail)
        Get
            Return GetCollection(Of PayrollScheduleDetail)("Payroll_ScheduleDetails")
        End Get
    End Property

    <Association("Payroll_UnemployedLiquidationReferencesPayroll_Group", GetType(PayrollUnemployedLiquidation))>
    Public ReadOnly Property Payroll_UnemployedLiquidation() As XPCollection(Of PayrollUnemployedLiquidation)
        Get
            Return GetCollection(Of PayrollUnemployedLiquidation)("Payroll_UnemployedLiquidation")
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

