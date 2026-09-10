Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewVerifyAutoliquidationFile")>
Public Class PayrollViewVerifyAutoliquidationFileXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fPayrollDateLiquidated As DateTime
    Public Property PayrollDateLiquidated() As DateTime
        Get
            Return fPayrollDateLiquidated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PayrollDateLiquidated", fPayrollDateLiquidated, value)
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

    Dim fRegisterStatus As Boolean
    Public Property RegisterStatus() As Boolean
        Get
            Return fRegisterStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RegisterStatus", fRegisterStatus, value)
        End Set
    End Property

    Dim fIdentificationType As String
    <Size(5)>
    Public Property IdentificationType() As String
        Get
            Return fIdentificationType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationType", fIdentificationType, value)
        End Set
    End Property

    Dim fNit As String
    <Size(50)>
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    Dim fInsuredCCSSCode As String
    <Size(20)>
    Public Property InsuredCCSSCode() As String
        Get
            Return fInsuredCCSSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InsuredCCSSCode", fInsuredCCSSCode, value)
        End Set
    End Property

    Dim fTypeContractEmployee As String
    <Size(5)>
    Public Property TypeContractEmployee() As String
        Get
            Return fTypeContractEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeContractEmployee", fTypeContractEmployee, value)
        End Set
    End Property

    Dim fSubTypeEmployee As String
    <Size(5)>
    Public Property SubTypeEmployee() As String
        Get
            Return fSubTypeEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubTypeEmployee", fSubTypeEmployee, value)
        End Set
    End Property

    Dim fForeignNotBound As String
    <Size(5)>
    Public Property ForeignNotBound() As String
        Get
            Return fForeignNotBound
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ForeignNotBound", fForeignNotBound, value)
        End Set
    End Property

    Dim fColombianForeignResident As String
    <Size(5)>
    Public Property ColombianForeignResident() As String
        Get
            Return fColombianForeignResident
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ColombianForeignResident", fColombianForeignResident, value)
        End Set
    End Property

    Dim fCodeCityBranchOffice As String
    <Size(10)>
    Public Property CodeCityBranchOffice() As String
        Get
            Return fCodeCityBranchOffice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCityBranchOffice", fCodeCityBranchOffice, value)
        End Set
    End Property

    Dim fNameEmployee As String
    <Size(300)>
    Public Property NameEmployee() As String
        Get
            Return fNameEmployee
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameEmployee", fNameEmployee, value)
        End Set
    End Property

    Dim fEntry As String
    <Size(1)>
    Public Property Entry() As String
        Get
            Return fEntry
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Entry", fEntry, value)
        End Set
    End Property

    Dim fIngressDate As DateTime?
    Public Property IngressDate() As DateTime?
        Get
            Return fIngressDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("IngressDate", fIngressDate, value)
        End Set
    End Property

    Dim fRetirement As String
    <Size(1)>
    Public Property Retirement() As String
        Get
            Return fRetirement
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Retirement", fRetirement, value)
        End Set
    End Property

    Dim fDateRetirement As DateTime?
    Public Property DateRetirement() As DateTime?
        Get
            Return fDateRetirement
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("DateRetirement", fDateRetirement, value)
        End Set
    End Property

    Dim fIBCOtrosParafiscales As Decimal
    Public Property IBCOtrosParafiscales() As Decimal
        Get
            Return fIBCOtrosParafiscales
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IBCOtrosParafiscales", fIBCOtrosParafiscales, value)
        End Set
    End Property

    Dim fVSP As String
    <Size(1)>
    Public Property VSP() As String
        Get
            Return fVSP
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VSP", fVSP, value)
        End Set
    End Property

    Dim fFechaInicioVSP As DateTime?
    Public Property FechaInicioVSP() As DateTime?
        Get
            Return fFechaInicioVSP
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FechaInicioVSP", fFechaInicioVSP, value)
        End Set
    End Property

    Dim fValueVSP As Decimal
    Public Property ValueVSP() As Decimal
        Get
            Return fValueVSP
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueVSP", fValueVSP, value)
        End Set
    End Property

    Dim fVST As String
    <Size(1)>
    Public Property VST() As String
        Get
            Return fVST
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VST", fVST, value)
        End Set
    End Property

    Dim fSLN As String
    <Size(1)>
    Public Property SLN() As String
        Get
            Return fSLN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SLN", fSLN, value)
        End Set
    End Property

    Dim fSanctionInitialDate As DateTime?
    Public Property SanctionInitialDate() As DateTime?
        Get
            Return fSanctionInitialDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("SanctionInitialDate", fSanctionInitialDate, value)
        End Set
    End Property

    Dim fSanctionEndDate As DateTime?
    Public Property SanctionEndDate() As DateTime?
        Get
            Return fSanctionEndDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("SanctionEndDate", fSanctionEndDate, value)
        End Set
    End Property

    Dim fBaseLiquidacionSLN As Decimal?
    Public Property BaseLiquidacionSLN() As Decimal?
        Get
            Return fBaseLiquidacionSLN
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("BaseLiquidacionSLN", fBaseLiquidacionSLN, value)
        End Set
    End Property

    Dim fIBCSLN As Decimal?
    Public Property IBCSLN() As Decimal?
        Get
            Return fIBCSLN
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("IBCSLN", fIBCSLN, value)
        End Set
    End Property

    Dim fIGE As String
    <Size(1)>
    Public Property IGE() As String
        Get
            Return fIGE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IGE", fIGE, value)
        End Set
    End Property

    Dim fAmbulatoryDisabilityInitialDate As DateTime?
    Public Property AmbulatoryDisabilityInitialDate() As DateTime?
        Get
            Return fAmbulatoryDisabilityInitialDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("AmbulatoryDisabilityInitialDate", fAmbulatoryDisabilityInitialDate, value)
        End Set
    End Property

    Dim fAmbulatoryDisabiltyEndDate As DateTime?
    Public Property AmbulatoryDisabiltyEndDate() As DateTime?
        Get
            Return fAmbulatoryDisabiltyEndDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("AmbulatoryDisabiltyEndDate", fAmbulatoryDisabiltyEndDate, value)
        End Set
    End Property

    Dim fBaseLiquidacionIGE As Decimal?
    Public Property BaseLiquidacionIGE() As Decimal?
        Get
            Return fBaseLiquidacionIGE
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("BaseLiquidacionIGE", fBaseLiquidacionIGE, value)
        End Set
    End Property

    Dim fBaseIGE As Decimal?
    Public Property BaseIGE() As Decimal?
        Get
            Return fBaseIGE
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("BaseIGE", fBaseIGE, value)
        End Set
    End Property

    Dim fLMA As String
    <Size(1)>
    Public Property LMA() As String
        Get
            Return fLMA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LMA", fLMA, value)
        End Set
    End Property

    Dim fMaternityLeaveInitialDate As DateTime?
    Public Property MaternityLeaveInitialDate() As DateTime?
        Get
            Return fMaternityLeaveInitialDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("MaternityLeaveInitialDate", fMaternityLeaveInitialDate, value)
        End Set
    End Property

    Dim fMaternityLeaveEndDate As DateTime?
    Public Property MaternityLeaveEndDate() As DateTime?
        Get
            Return fMaternityLeaveEndDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("MaternityLeaveEndDate", fMaternityLeaveEndDate, value)
        End Set
    End Property

    Dim fBaseLiquidacionLMA As Decimal?
    Public Property BaseLiquidacionLMA() As Decimal?
        Get
            Return fBaseLiquidacionLMA
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("BaseLiquidacionLMA", fBaseLiquidacionLMA, value)
        End Set
    End Property

    Dim fBaseLMA As Decimal?
    Public Property BaseLMA() As Decimal?
        Get
            Return fBaseLMA
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("BaseLMA", fBaseLMA, value)
        End Set
    End Property

    Dim fVAC As String
    <Size(1)>
    Public Property VAC() As String
        Get
            Return fVAC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VAC", fVAC, value)
        End Set
    End Property

    Dim fVacationInitialDate As DateTime?
    Public Property VacationInitialDate() As DateTime?
        Get
            Return fVacationInitialDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("VacationInitialDate", fVacationInitialDate, value)
        End Set
    End Property

    Dim fVacationEndDate As DateTime?
    Public Property VacationEndDate() As DateTime?
        Get
            Return fVacationEndDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("VacationEndDate", fVacationEndDate, value)
        End Set
    End Property

    Dim fBaseLiquidacionVAC As Decimal?
    Public Property BaseLiquidacionVAC() As Decimal?
        Get
            Return fBaseLiquidacionVAC
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("BaseLiquidacionVAC", fBaseLiquidacionVAC, value)
        End Set
    End Property

    Dim fBaseVAC As Decimal?
    Public Property BaseVAC() As Decimal?
        Get
            Return fBaseVAC
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("BaseVAC", fBaseVAC, value)
        End Set
    End Property

    Dim fIRL As String
    <Size(1)>
    Public Property IRL() As String
        Get
            Return fIRL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IRL", fIRL, value)
        End Set
    End Property

    Dim fFechaInicioIRL As DateTime?
    Public Property FechaInicioIRL() As DateTime?
        Get
            Return fFechaInicioIRL
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FechaInicioIRL", fFechaInicioIRL, value)
        End Set
    End Property

    Dim fFechaFinIRL As DateTime?
    Public Property FechaFinIRL() As DateTime?
        Get
            Return fFechaFinIRL
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FechaFinIRL", fFechaFinIRL, value)
        End Set
    End Property

    Dim fBaseLiquidacionIRL As Decimal
    Public Property BaseLiquidacionIRL() As Decimal
        Get
            Return fBaseLiquidacionIRL
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseLiquidacionIRL", fBaseLiquidacionIRL, value)
        End Set
    End Property

    Dim fBaseIRL As Decimal
    Public Property BaseIRL() As Decimal
        Get
            Return fBaseIRL
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseIRL", fBaseIRL, value)
        End Set
    End Property

    Dim fPensionAdministratorCode As String
    <Size(20)>
    Public Property PensionAdministratorCode() As String
        Get
            Return fPensionAdministratorCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PensionAdministratorCode", fPensionAdministratorCode, value)
        End Set
    End Property

    Dim fEPSCode As String
    <Size(20)>
    Public Property EPSCode() As String
        Get
            Return fEPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EPSCode", fEPSCode, value)
        End Set
    End Property

    Dim fCCFCode As String
    <Size(20)>
    Public Property CCFCode() As String
        Get
            Return fCCFCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CCFCode", fCCFCode, value)
        End Set
    End Property

    Dim fPensionDays As Integer
    Public Property PensionDays() As Integer
        Get
            Return fPensionDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PensionDays", fPensionDays, value)
        End Set
    End Property

    Dim fHealthDays As Integer
    Public Property HealthDays() As Integer
        Get
            Return fHealthDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthDays", fHealthDays, value)
        End Set
    End Property

    Dim fProfessionalRiskDays As Integer
    Public Property ProfessionalRiskDays() As Integer
        Get
            Return fProfessionalRiskDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProfessionalRiskDays", fProfessionalRiskDays, value)
        End Set
    End Property

    Dim fCompensationFundDays As Integer
    Public Property CompensationFundDays() As Integer
        Get
            Return fCompensationFundDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CompensationFundDays", fCompensationFundDays, value)
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

    Dim fIntegralSalary As String
    <Size(1)>
    Public Property IntegralSalary() As String
        Get
            Return fIntegralSalary
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IntegralSalary", fIntegralSalary, value)
        End Set
    End Property

    Dim fVSTValue As Decimal
    Public Property VSTValue() As Decimal
        Get
            Return fVSTValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VSTValue", fVSTValue, value)
        End Set
    End Property

    Dim fIBCPension As Decimal
    Public Property IBCPension() As Decimal
        Get
            Return fIBCPension
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IBCPension", fIBCPension, value)
        End Set
    End Property

    Dim fIBCHealth As Decimal
    Public Property IBCHealth() As Decimal
        Get
            Return fIBCHealth
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IBCHealth", fIBCHealth, value)
        End Set
    End Property

    Dim fIBCProfessionalRisk As Decimal
    Public Property IBCProfessionalRisk() As Decimal
        Get
            Return fIBCProfessionalRisk
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IBCProfessionalRisk", fIBCProfessionalRisk, value)
        End Set
    End Property

    Dim fIBCCompensationFund As Decimal
    Public Property IBCCompensationFund() As Decimal
        Get
            Return fIBCCompensationFund
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IBCCompensationFund", fIBCCompensationFund, value)
        End Set
    End Property

    Dim fRateContributionPension As Decimal
    Public Property RateContributionPension() As Decimal
        Get
            Return fRateContributionPension
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateContributionPension", fRateContributionPension, value)
        End Set
    End Property

    Dim fValuePension As Decimal
    Public Property ValuePension() As Decimal
        Get
            Return fValuePension
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValuePension", fValuePension, value)
        End Set
    End Property

    Dim fPensionSolidarityFundValueContribution As Decimal
    Public Property PensionSolidarityFundValueContribution() As Decimal
        Get
            Return fPensionSolidarityFundValueContribution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionSolidarityFundValueContribution", fPensionSolidarityFundValueContribution, value)
        End Set
    End Property

    Dim fPensionSolidarityFundValueContributionSubsistence As Decimal
    Public Property PensionSolidarityFundValueContributionSubsistence() As Decimal
        Get
            Return fPensionSolidarityFundValueContributionSubsistence
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PensionSolidarityFundValueContributionSubsistence", fPensionSolidarityFundValueContributionSubsistence, value)
        End Set
    End Property

    Dim fRateContributionHealth As Decimal
    Public Property RateContributionHealth() As Decimal
        Get
            Return fRateContributionHealth
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateContributionHealth", fRateContributionHealth, value)
        End Set
    End Property

    Dim fValueHealth As Decimal
    Public Property ValueHealth() As Decimal
        Get
            Return fValueHealth
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueHealth", fValueHealth, value)
        End Set
    End Property

    Dim fValueSena As Decimal
    Public Property ValueSena() As Decimal
        Get
            Return fValueSena
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueSena", fValueSena, value)
        End Set
    End Property

    Dim fRateContributionProfessionalRisk As Decimal
    Public Property RateContributionProfessionalRisk() As Decimal
        Get
            Return fRateContributionProfessionalRisk
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateContributionProfessionalRisk", fRateContributionProfessionalRisk, value)
        End Set
    End Property

    Dim fWorkCenter As String
    <Size(50)>
    Public Property WorkCenter() As String
        Get
            Return fWorkCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WorkCenter", fWorkCenter, value)
        End Set
    End Property

    Dim fValueContributionProfessionalRisk As Decimal
    Public Property ValueContributionProfessionalRisk() As Decimal
        Get
            Return fValueContributionProfessionalRisk
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueContributionProfessionalRisk", fValueContributionProfessionalRisk, value)
        End Set
    End Property

    Dim fRateContributorCCF As Decimal
    Public Property RateContributorCCF() As Decimal
        Get
            Return fRateContributorCCF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateContributorCCF", fRateContributorCCF, value)
        End Set
    End Property

    Dim fValueContributionCCF As Decimal
    Public Property ValueContributionCCF() As Decimal
        Get
            Return fValueContributionCCF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateContributorCCF", fValueContributionCCF, value)
        End Set
    End Property

    Dim fRateContributorSENA As Decimal
    Public Property RateContributorSENA() As Decimal
        Get
            Return fRateContributorSENA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateContributorSENA", fRateContributorSENA, value)
        End Set
    End Property

    Dim fRateContributionICBF As Decimal
    Public Property RateContributionICBF() As Decimal
        Get
            Return fRateContributionICBF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateContributionICBF", fRateContributionICBF, value)
        End Set
    End Property

    Dim fValueICBF As Decimal
    Public Property ValueICBF() As Decimal
        Get
            Return fValueICBF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueICBF", fValueICBF, value)
        End Set
    End Property

    Dim fTarifaEspecialPensiones As Decimal
    Public Property TarifaEspecialPensiones() As Decimal
        Get
            Return fTarifaEspecialPensiones
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TarifaEspecialPensiones", fTarifaEspecialPensiones, value)
        End Set
    End Property

    Dim fObservations As String
    <Size(500)>
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fPensionary As String
    <Size(500)>
    Public Property Pensionary() As String
        Get
            Return fPensionary
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Pensionary", fPensionary, value)
        End Set
    End Property

    Dim fCCSSCode As String
    <Size(500)>
    Public Property CCSSCode() As String
        Get
            Return fCCSSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CCSSCode", fCCSSCode, value)
        End Set
    End Property

    Dim fHoursDaily As Integer
    Public Property HoursDaily() As Integer
        Get
            Return fHoursDaily
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HoursDaily", fHoursDaily, value)
        End Set
    End Property

    Dim fNationalityCode As String
    Public Property NationalityCode() As String
        Get
            Return fNationalityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NationalityCode", fNationalityCode, value)
        End Set
    End Property

    Dim fINSCode As String
    Public Property INSCode() As String
        Get
            Return fINSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("INSCode", fINSCode, value)
        End Set
    End Property
    Dim fChangeType As String
    Public Property ChangeType() As String
        Get
            Return fChangeType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ChangeType", fChangeType, value)
        End Set
    End Property
    Dim fChangeCode As String
    Public Property ChangeCode() As String
        Get
            Return fChangeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ChangeCode", fChangeCode, value)
        End Set
    End Property

    Dim fInitialDate As Date?
    Public Property InitialDate() As Date?
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fFinalDate As Date?
    Public Property FinalDate() As Date?
        Get
            Return fFinalDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("FinalDate", fFinalDate, value)
        End Set
    End Property


#Region "Custom Members"
    <NonPersistent>
    Public ReadOnly Property IdNumber() As String
        Get
            If String.IsNullOrEmpty(InsuredCCSSCode) Then
                Return Nit
            Else
                Return InsuredCCSSCode
            End If
        End Get
    End Property
#End Region

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
