Imports System.Globalization
Imports DevExpress.Data.Filtering
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll.Entities.HumanTalentParametrization
Imports Infrastructure.CrossCutting.Base

Public Class LiquidationDomain
    Implements ILiquidationDomain

    Dim DistributeExpense As Boolean = False
    Dim MessageRetention As Integer = 0
    Private ConceptFormulatePrefix As String = "[C_"

#Region "Repositories"
    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _payrollParameterRepository As IPayrollParameterRepository

    ''' <summary>
    ''' Repositorio de Grupo
    ''' </summary>
    ''' <remarks></remarks>
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Repositorio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquitationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de Conceptos Autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Private _autorizationConceptRepository As IAuthorizationConceptRepository

    ''' <summary>
    ''' Repositorio de Cuadro de Turno
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailRepository As IScheduleDetailRepository

    ''' <summary>
    ''' Repositorio de Novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyRepository As INoveltyRepository

    ''' <summary>
    ''' Repositorio de Retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _retentionRepository As IRetentionRepository

    ''' <summary>
    ''' Repositorio de Vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationRepository As IVacationRepository

    ''' <summary>
    ''' Repositorio de Primas
    ''' </summary>
    ''' <remarks></remarks>
    Private _incentivePaymentRepository As IIncentivePaymentRepository

    ''' <summary>
    ''' Repositorio de Cesantias
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationRepository As IUnemployedLiquidationRepository

    ''' <summary>
    ''' Repositorio de Convenios
    ''' </summary>
    ''' <remarks></remarks>
    Private _IAgreementsRepository As IAgreementsCRepository

    ''' <summary>
    ''' Repositorio de Conceptos Manuales
    ''' </summary>
    ''' <remarks></remarks>
    Private _ManualConceptsRepository As IManualConcepts

    ''' <summary>
    ''' Repositorio de Distribución de Costos
    ''' </summary>
    ''' <remarks></remarks>
    Private _CostDistributionDomain As ICostDistributionDomain

    Private _employeeRepository As IEmployeeRepository

    Private _FundsRepository As IFundsLevelRepository

    Private _retroactiveRepository As IRetroactiveCRepository

    Private _PayrollSettings As IPayrollSettingsRepository

    Private _thirdPartyRepository As IThirdPartyRepository

    Private _FixedPercentageRepository As IFixedPercentageRetentionRepository

    Private _conceptRepository As IConceptRepository

    Private _noveltyScheduleRepository As INoveltyScheduleDetailRepository

    Private _foreclousureRepository As IForeclousureRepository

    Private _holidayRepository As IHolidayRepository

    Private _functionsLiquidation As ILiquidationFunctions

    Private _CompanySettings As ICompanySettingsRepository

    ''' <summary>
    ''' repositorio de los Ingresos exentos acumulados por tercero y por año
    ''' </summary>
    Private _thirdPartyAccumulatedExemptIncomeRepository As IThirdPartyAccumulatedExemptIncomeRepository

#End Region

#Region "Constructor"
    Public Sub New(ByVal payrollParameterRepository As IPayrollParameterRepository, groupRepository As IGroupRepository, liquitationRepository As IPayrollLiquidationRepository, autorizationConceptRepository As IAuthorizationConceptRepository,
                   scheduleDetailRepository As IScheduleDetailRepository, noveltyRepository As INoveltyRepository, retentionRepository As IRetentionRepository, vacationRepository As IVacationRepository, incentivePaymentRepository As IIncentivePaymentRepository,
                   unemployedLiquidationRepository As IUnemployedLiquidationRepository, AgreementsRepository As IAgreementsCRepository, ManualConceptsRepository As IManualConcepts,
                   CostDistributionDomain As ICostDistributionDomain, employeeRepository As IEmployeeRepository, FundsRepository As IFundsLevelRepository, retroactiveRepository As IRetroactiveCRepository, PayrollSettings As IPayrollSettingsRepository,
                   thirdPartyRepository As IThirdPartyRepository, FixedPercentageRepository As IFixedPercentageRetentionRepository, conceptRepository As IConceptRepository, noveltyScheduleRepository As INoveltyScheduleDetailRepository, foreclousureRepository As IForeclousureRepository, holidayRepository As IHolidayRepository,
                   functionsLiquidation As ILiquidationFunctions, CompanySettings As ICompanySettingsRepository, thirdPartyAccumulatedExemptIncomeRepository As IThirdPartyAccumulatedExemptIncomeRepository)
        If payrollParameterRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Parametros Vacío")
        End If
        If groupRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Grupos Vacío")
        End If
        If liquitationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Liquidacion Vacío")
        End If
        If autorizationConceptRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Conceptos Vacío")
        End If
        If scheduleDetailRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Schedule Detail Vacío")
        End If
        If noveltyRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Novedades Vacío")
        End If
        If retentionRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Retenciones Vacío")
        End If
        If incentivePaymentRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Primas Vacío")
        End If
        If unemployedLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Cesantias Vacío")
        End If
        If ManualConceptsRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Conceptos Manuales Vacío")
        End If
        If employeeRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Empleados Vacío")
        End If
        If FixedPercentageRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de FixedPercentageRepository Vacío")
        End If

        _payrollParameterRepository = payrollParameterRepository
        _groupRepository = groupRepository
        _liquitationRepository = liquitationRepository
        _autorizationConceptRepository = autorizationConceptRepository
        _scheduleDetailRepository = scheduleDetailRepository
        _noveltyRepository = noveltyRepository
        _retentionRepository = retentionRepository
        _vacationRepository = vacationRepository
        _incentivePaymentRepository = incentivePaymentRepository
        _unemployedLiquidationRepository = unemployedLiquidationRepository
        _IAgreementsRepository = AgreementsRepository
        _ManualConceptsRepository = ManualConceptsRepository
        _CostDistributionDomain = CostDistributionDomain
        _employeeRepository = employeeRepository
        _FundsRepository = FundsRepository
        _retroactiveRepository = retroactiveRepository
        _PayrollSettings = PayrollSettings
        _thirdPartyRepository = thirdPartyRepository
        _FixedPercentageRepository = FixedPercentageRepository
        _conceptRepository = conceptRepository
        _noveltyScheduleRepository = noveltyScheduleRepository
        _foreclousureRepository = foreclousureRepository
        _holidayRepository = holidayRepository
        _functionsLiquidation = functionsLiquidation
        _CompanySettings = CompanySettings
        _thirdPartyAccumulatedExemptIncomeRepository = thirdPartyAccumulatedExemptIncomeRepository
    End Sub
#End Region

    Public Function ReplaceDataLiquidation(payrollParameter As PayrollParameter, BasicSalary As Decimal, DaysWorkedEmployee As Integer, WorkHours As Decimal, PayrollDays As Integer, FormulaConcept As String, employeePayroll As Domain.Payroll.Entities.Contract, BaseIBCPension As Double, BaseIBCHealth As Double, IBCHealth As Double, BaseIBCHealthEmployer As Double,
                                BaseIBCHealthEmployerIntegral As Double, BaseIBCPensionEmployer As Double, BaseIBCPensionEmployerIntegral As Double, IBCSENA As Double, IBCICBF As Double, IBCPeriod As Double, IBCSeverance As Double, IBCCompensationFund As Double, IBCPension As Double, IBCARP As Double, ValueAmbulatoryInability As Double, ValueHospitalInability As Double, ValueMaternity As Double,
                                ValueSanctions As Double, TotalPeriodDaysInabilities As Integer, HospitalInabilityDays As Integer, RemuneratedLicensesDays As Integer, FamilyDay As Short, UnpaidLicensesDays As Integer, ValueUnpaidLicenses As Double, ValueProfesionalInabilities As Double, HealthEmployee As Double,
                                PensionEmployee As Double, BaseIBCArp As Double, BaseIBCArpIntegral As Double, BaseParafiscalCompensationFund As Double, BaseParafiscalCompensationFundIntegral As Double, BaseParafiscalICBF As Double, BaseParafiscalICBFIntegral As Double, RetentionValue As Double, ProvisionDays As Integer, ProfessionalRiskPercentage As Double, VacationValue As Double, AdjustVacationValue As Double,
                                incentiePaymentValue As Double, unemployedInteresValue As Double, transportHelpValue As Double, BaseIBCSenaIntegral As Double, BaseIBCSena As Double, TotalAccrued As Double, TotalDeducted As Double, BaseIBCSecurityPensionalIntegralFound As Double, CalamidadDomesticaDays As Integer, VoluntaryPensionValue As Double, VoluntaryHealthValue As Double, IBCIncentivePayment As Double, BonusServices As Double,
                                ManualConceptValue As Double, SindicateFlag As Byte, PaidVacation As Byte, RepresentationCost As Boolean, PaidValueAverageIncentiveServices As Double, DailyHours As Integer, PayrollDateLiquidated As Date, JobBondingDate As Date, NumberContract As Integer, GeneralInabilityValue As Double, ValueLuto As Double, ValuePaternity As Double, TotalIBCSolidaridad As Double, CodeEmployeeType As String, AnoLaborado As Boolean,
                                BonificationValue As Double, TotalVacationDays As Integer, ContractVacationDays As Integer, ItemContract As Integer, ValueRetroactiveBonification As Decimal, RecreationBonificationValue As Decimal, VacationalIncrease As Decimal, VacationIncentivePayment As Decimal, VacationCompensationValue As Decimal, PaidCredit As Byte, HoursMinPosition As Integer, HoursMaxPosition As Integer, IBCVacation As Double, DecemberIncentivePaymentAverage As Double, IncentivePaymentAverage As Double,
                                VacationIncentiveValue As Double, ValueForeclousure As Double, PercentageForeclousure As Decimal, codeWorkCenter As String, HolidaysHours As Decimal, BasicSalaryDaily As Decimal, AjusteDominical As Decimal, AjusteHorasExtrasDiurnas As Decimal, AjusteHorasExtrasNocturnas As Decimal, AjusteHorasExtrasDiurnasFestivas As Decimal, AjusteHorasExtrasNocturnasFestivas As Decimal, AjusteRecargoNocturnasFestivas As Decimal, VacationIBCValue As Decimal, VacationPaidValue As Decimal,
                                           PeriodVacationDays As Integer, BaseForeclousure As Decimal, PosesitionDate As Date, ValueRetroactivePaid As Decimal, TransportDays As Integer, HorasAjustesDominicalEvento As Decimal, HorasAjusteExtrasDiurnasFestivasEvento As Decimal, HorasAjusteExtrasNocturnasFestivasEvento As Decimal, HorasAjusteExtrasNocturnasEvento As Decimal, HorasAjusteExtrasDiurnasEvento As Decimal, QuarterFlag As Byte, RepresentationCostValue As Decimal, VacationIncentiveValueProvision As Decimal,
                                           SanctionDays As Integer, ContributorAbroad As Boolean, IBCLastPeriod As Decimal, AmbulatoryInabilityEmployeerDays As Integer, AmbulatoryInabilityERPDays As Integer, ValueAmbulatoryEmployeerInability As Decimal, ValueAmbulatoryERPInability As Decimal, HospitalInabilityEmployeerDays As Integer, HospitalInabilityERPDays As Integer, ValueHospitalEmployeerInability As Decimal, ValueHospitalERPInability As Decimal, DaysPermission As Decimal, PaidLeaveDays As Decimal, VacationDaysInCash As Decimal,
                                           LutoDays As Decimal, EmployerOccupationalDisabilityDays As Decimal, EmployerOccupationalDisabilityAmount As Decimal, ERPOccupationalDisabilityDays As Decimal, ERPOccupationalDisabilityAmount As Decimal, ImmediateVacationValue As Decimal, ImmediateVacationBonificationValue As Decimal, ImmediateVacationIncentivePaymentValue As Decimal, ImmediateVacationalIncreaseValue As Decimal) As Dictionary(Of String, String) Implements ILiquidationDomain.ReplaceDataLiquidation

        Dim descriptions As Dictionary(Of String, String)

        FormulaConcept = Replace(FormulaConcept, "[Salario Mínimo]", Replace(payrollParameter.LegalSalaryMinimum.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Auxilio Transporte]", Format(transportHelpValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Salud Empleado]", Replace(payrollParameter.EmployeeHealthContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Salud Patrono]", Replace(payrollParameter.EmployerHealthContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Pensión Empleado]", Replace(payrollParameter.EmployeePensionContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Pensión Patrono]", Replace(payrollParameter.EmployerPensionContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% SENA]", Replace(payrollParameter.SenaContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% ICBF]", Replace(payrollParameter.ICBFContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Caja]", Replace(payrollParameter.CompensationFundContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Aporte Voluntario Pension]", Format(VoluntaryPensionValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Aporte Voluntario Salud]", Format(VoluntaryHealthValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Contrato]", Format(BasicSalary, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Tarifa ARP]", Replace(ProfessionalRiskPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Laboradas]", Format(WorkHours, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Incapacidad]", TotalPeriodDaysInabilities)
        FormulaConcept = Replace(FormulaConcept, "[Días Nómina]", PayrollDays)
        FormulaConcept = Replace(FormulaConcept, "[Base Pensión]", Format(BaseIBCPension, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Salud]", Replace(BaseIBCHealth.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Salud Patrono]", Replace(BaseIBCHealthEmployer.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Salud Patrono Integral]", Replace(BaseIBCHealthEmployerIntegral.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Pensión Patrono]", Replace(BaseIBCPensionEmployer.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Pensión Patrono Integral]", Replace(BaseIBCPensionEmployerIntegral.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Riesgos Profesionales]", Replace(BaseIBCArp.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Riesgos Profesionales Integral]", Replace(BaseIBCArpIntegral.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal Caja de Compensación]", Replace(BaseParafiscalCompensationFund.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal Caja de Compensación Integral]", Replace(BaseParafiscalCompensationFundIntegral.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal ICBF]", Replace(BaseParafiscalICBF.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal ICBF Integral]", Replace(BaseParafiscalICBFIntegral.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte SENA Integral]", Replace(BaseIBCSenaIntegral.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte SENA]", Replace(BaseIBCSena.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Fondo Seguridad Pensional Integral]", Replace(BaseIBCSecurityPensionalIntegralFound.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Ajuste Vacaciones]", Replace(AdjustVacationValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Paga Vacaciones]", PaidVacation)
        FormulaConcept = Replace(FormulaConcept, "[Fecha Liquidacion Nomina]", CriteriaOperator.Parse("?", PayrollDateLiquidated).ToString())
        FormulaConcept = Replace(FormulaConcept, "[Fecha Contratacion]", CriteriaOperator.Parse("?", JobBondingDate).ToString())
        FormulaConcept = Replace(FormulaConcept, "[Bonificacion Año Servicio]", Format(BonificationValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Total Dias Vacaciones]", TotalVacationDays)
        FormulaConcept = Replace(FormulaConcept, "[Bonificacion Año Servicio Retroactivo]", Format(ValueRetroactiveBonification, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Embargo]", ValueForeclousure)
        FormulaConcept = Replace(FormulaConcept, "[Porcentaje Embargo]", Format(PercentageForeclousure, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Codigo Centro Trabajo]", codeWorkCenter)
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajustes Festivos]", Format(HolidaysHours, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Basico Diario]", Format(BasicSalary, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Dominical]", Format(AjusteDominical, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Diurnas]", Format(AjusteHorasExtrasDiurnas, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Nocturnas]", Format(AjusteHorasExtrasNocturnas, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Ambulatoria]", Replace(ValueAmbulatoryInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Hospitalaria]", Replace(ValueHospitalInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Incapacidad Hospitalaria]", Replace(HospitalInabilityDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Maternidad]", Replace(ValueMaternity.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Licencia]", RemuneratedLicensesDays)
        FormulaConcept = Replace(FormulaConcept, "[Valor licencia no Remunerada]", Replace(ValueUnpaidLicenses.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días licencia no Remunerada]", Replace(UnpaidLicensesDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Sanciones]", Replace(ValueSanctions.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Riesgos Pro]", Replace(ValueProfesionalInabilities.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Licencia Luto]", Format(ValueLuto, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Paternidad]", Format(ValuePaternity, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad General]", Format(GeneralInabilityValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Trabajados]", DaysWorkedEmployee)
        FormulaConcept = Replace(FormulaConcept, "[Dias Provisión]", ProvisionDays)
        FormulaConcept = Replace(FormulaConcept, "[IBC SENA]", Replace(IBCSENA.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC ICBF]", Replace(IBCICBF.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Periodo]", Replace(IBCPeriod.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Periodo Anterior]", Replace(IBCLastPeriod.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Cesantias]", Replace(IBCSeverance.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Primas]", Format(IBCIncentivePayment, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Caja]", Replace(IBCCompensationFund.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Pensión]", Format(IBCPension, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC ARP]", Replace(IBCARP.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Salud]", Replace(IBCHealth.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Total Devengado]", TotalAccrued)
        FormulaConcept = Replace(FormulaConcept, "[Total Deducido]", TotalDeducted)
        FormulaConcept = Replace(FormulaConcept, "[Valor Retención]", CriteriaOperator.Parse("?", RetentionValue).ToString())
        FormulaConcept = Replace(FormulaConcept, "[Aporte a Salud Empleado]", Replace(HealthEmployee.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Aporte a Pensión Empleado]", Replace(PensionEmployee.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Vacaciones]", Replace(VacationValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Interes de Cesantias]", Replace(unemployedInteresValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Primas]", Replace(incentiePaymentValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Calamidad Domestica]", CalamidadDomesticaDays)
        FormulaConcept = Replace(FormulaConcept, "[Valor Bonificaciones]", Format(BonusServices, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Empleado Sindicalizado]", Format(SindicateFlag, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Concepto Manual]", Format(ManualConceptValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Gastos Representacion]", RepresentationCost)
        FormulaConcept = Replace(FormulaConcept, "[Valor Prima Servicios]", Format(PaidValueAverageIncentiveServices, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Diarias Laboradas]", Format(DailyHours, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Numero Contratos Periodo]", NumberContract)
        FormulaConcept = Replace(FormulaConcept, "[Codigo Tipo Empleado]", CodeEmployeeType)
        FormulaConcept = Replace(FormulaConcept, "[Año Laborado]", AnoLaborado)
        FormulaConcept = Replace(FormulaConcept, "[Valor Bonificacion Recreacion]", Format(RecreationBonificationValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Prima Vacaciones]", Format(VacationIncentivePayment, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incremento Vacacional]", Format(VacationalIncrease, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Paga Credito]", PaidCredit)
        FormulaConcept = Replace(FormulaConcept, "[Días Vacaciones]", Replace(TotalVacationDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Total IBC Solidaridad]", Format(TotalIBCSolidaridad, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Vacaciones Periodo]", ContractVacationDays)
        FormulaConcept = Replace(FormulaConcept, "[Item Contrato]", ItemContract)
        FormulaConcept = Replace(FormulaConcept, "[Valor Compensacion Vacaciones]", Format(VacationCompensationValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Incapacidad]", TotalPeriodDaysInabilities)
        FormulaConcept = Replace(FormulaConcept, "[Horas Minimas Cargo]", HoursMinPosition)
        FormulaConcept = Replace(FormulaConcept, "[Horas Maximas Cargo]", HoursMaxPosition)
        FormulaConcept = Replace(FormulaConcept, "[IBC Vacaciones]", IBCVacation)
        FormulaConcept = Replace(FormulaConcept, "[Prima Diciembre]", DecemberIncentivePaymentAverage)
        FormulaConcept = Replace(FormulaConcept, "[Prima Junio]", IncentivePaymentAverage)
        FormulaConcept = Replace(FormulaConcept, "[Prima Vacaciones Hist]", VacationIncentiveValue)
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Diurnas Festivas]", Format(AjusteHorasExtrasDiurnasFestivas, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Nocturnas Festivas]", Format(AjusteHorasExtrasNocturnasFestivas, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Recargos Nocturnas Festivas]", Format(AjusteRecargoNocturnasFestivas, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Vacaciones FSP]", Format(VacationIBCValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Vacaciones Pagado]", Format(VacationPaidValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Vacaciones Periodo]", Format(PeriodVacationDays, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Embargo]", Format(BaseForeclousure, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Fecha Posesion]", CriteriaOperator.Parse("?", PosesitionDate).ToString())
        FormulaConcept = Replace(FormulaConcept, "[Valor Retroactivo]", Format(ValueRetroactivePaid, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Auxilio Transporte]", Replace(TransportDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajustes Dominical Evento]", Replace(HorasAjustesDominicalEvento.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Diurnas Festivas Evento]", Replace(TransportDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Nocturnas Festivas Evento]", Replace(HorasAjusteExtrasNocturnasFestivasEvento.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Nocturnas Evento]", Replace(HorasAjusteExtrasNocturnasEvento.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Ajuste Extras Diurnas Evento]", Replace(HorasAjusteExtrasDiurnasEvento.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Quincena]", Replace(QuarterFlag.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Día Familia]", FamilyDay)
        FormulaConcept = Replace(FormulaConcept, "[Valor Gasto Representacion]", Replace(RepresentationCostValue.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Prima Vacaciones Provision]", Replace(VacationIncentiveValueProvision.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Sancion]", Replace(SanctionDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Cotizante Exterior]", ContributorAbroad)
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Patrono-Hospitalaria]", Replace(ValueHospitalEmployeerInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Patrono - Ambulatoria]", Replace(ValueAmbulatoryEmployeerInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad ERP- Hospitalaria]", Replace(ValueHospitalERPInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad ERP- Ambulatoria]", Replace(ValueAmbulatoryERPInability.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Permiso]", DaysPermission)
        FormulaConcept = Replace(FormulaConcept, "[Dias Licencia Remunerada]", PaidLeaveDays)
        FormulaConcept = Replace(FormulaConcept, "[Dias Vacaciones Dinero]", VacationDaysInCash)
        FormulaConcept = Replace(FormulaConcept, "[Días Licencia Luto]", LutoDays)

        FormulaConcept = Replace(FormulaConcept, "[Dias Incapacidad Profesional Patrono]", Replace(EmployerOccupationalDisabilityDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Incapacidad Profesional ERP]", Replace(ERPOccupationalDisabilityDays.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Profesional Patrono]", Replace(EmployerOccupationalDisabilityAmount.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incapacidad Profesional ERP]", Replace(ERPOccupationalDisabilityAmount.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Vacaciones Pago Inmediato]", Format(ImmediateVacationValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Bonificacion Recreacion Pago Inmediato]", Format(ImmediateVacationBonificationValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Prima Vacaciones Pago Inmediato]", Format(ImmediateVacationIncentivePaymentValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Incremento Vacacional Pago Inmediato]", Format(ImmediateVacationalIncreaseValue, "0.00").Replace(",", "."))

        Dim result = Utils.EvalExpression(FormulaConcept)
        Dim ConceptValue As Decimal
        If result.StateResult = True Then
            ConceptValue = CType(result.ObjectEmbbeded, Decimal)
        End If

        descriptions = New Dictionary(Of String, String)
        descriptions.Add("Formula", FormulaConcept)
        descriptions.Add("Valor", ConceptValue)

        Return descriptions

    End Function


    ''' <summary>
    ''' Obtiene la Fecha Final Nómina
    ''' </summary>
    ''' <param name="payrollLiquidation">Forma Liquidación Nómina</param>
    ''' <param name="payrollStarDate">Fecha Inicial Nómina</param>
    ''' <returns>Fecha Final nómina </returns>
    ''' <remarks></remarks>
    Public Function GetEndPayrollDate(payrollLiquidation As Integer, payrollStarDate As Date) As Date Implements ILiquidationDomain.GetEndPayrollDate

        Dim PayrollEndDate As Date

        If (payrollLiquidation = 1) Then '' Nómina Mensual 

            PayrollEndDate = DateAdd(DateInterval.Month, 1, payrollStarDate)
            PayrollEndDate = DateAdd(DateInterval.Day, -1, PayrollEndDate)

        ElseIf (payrollLiquidation = 2) Then '' Nómina Quincenal
            If Day(payrollStarDate) = 1 Then
                PayrollEndDate = New Date(payrollStarDate.Year, payrollStarDate.Month, 15)
            ElseIf Day(payrollStarDate) = 16 Then
                payrollStarDate = New Date(payrollStarDate.Year, payrollStarDate.Month, 1)
                PayrollEndDate = DateAdd(DateInterval.Month, 1, payrollStarDate)
                PayrollEndDate = DateAdd(DateInterval.Day, -1, PayrollEndDate)
            End If
        End If

        Return PayrollEndDate

    End Function

    ''' <summary>
    ''' Obtiene la Fecha Final Nómina
    ''' </summary>
    ''' <param name="payrollLiquidation">Forma Liquidación Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Final Nómina</param>
    ''' <returns>Fecha Final nómina </returns>
    ''' <remarks></remarks>
    Public Function GetStarPayrollDate(payrollLiquidation As Integer, PayrollEndDate As Date) As Date Implements ILiquidationDomain.GetStarPayrollDate

        Dim PayrollStarDate As Date

        If (payrollLiquidation = 1) Then '' Nómina Mensual 

            PayrollStarDate = DateAdd(DateInterval.Month, -1, PayrollEndDate)
            PayrollStarDate = DateAdd(DateInterval.Day, 1, PayrollStarDate)

        ElseIf (payrollLiquidation = 2) Then '' Nómina Quincenal
            If Day(PayrollEndDate) = 15 Then
                PayrollStarDate = DateAdd(DateInterval.Day, -14, PayrollEndDate)
                'PayrollStarDate = New Date(PayrollStarDate.Year, PayrollEndDate.Month, -15)
            ElseIf Day(PayrollEndDate) = 30 Or Day(PayrollEndDate) = 31 Or Day(PayrollEndDate) = 29 Or Day(PayrollEndDate) = 28 Then
                PayrollStarDate = New Date(PayrollEndDate.Year, PayrollEndDate.Month, 16)
            End If
        End If

        Return PayrollStarDate

    End Function

    ''' <summary>
    ''' Obtiene los días trabajados del Empleado sin descuentos
    ''' </summary>
    ''' <param name="EmployeeInitialDate">Fecha Inicial Empleado</param>
    ''' <param name="employeEndDate">Fecha Fin Empleado</param>
    ''' <param name="payrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="payrollEndDate">Fecha Final Nómina</param>
    ''' <param name="payrollMonth">Mes Nómina</param>
    ''' <returns>Días trabajados Empleado</returns>
    ''' <remarks></remarks>
    Public Function DaysWorkedEmployee(ByVal EmployeeInitialDate As Date, ByVal employeEndDate As Date, ByVal payrollInitialDate As Date, ByVal payrollEndDate As Date, payrollMonth As Char) As Integer Implements ILiquidationDomain.DaysWorkedEmployee

        Dim DaysWorkEmployee As Integer

        '' es un empleado antiguo
        If EmployeeInitialDate <= payrollInitialDate And employeEndDate >= payrollEndDate Then
            EmployeeInitialDate = payrollInitialDate
            employeEndDate = payrollEndDate
        End If

        '' es por que el empleado se le vence el contrato antes de la payrollEndDate y no le han renovado contrato
        If EmployeeInitialDate <= payrollInitialDate And employeEndDate < payrollEndDate Then
            EmployeeInitialDate = payrollInitialDate
            employeEndDate = employeEndDate
        End If

        '' El empleado ingresó después de iniciado el periodo de nómina
        If EmployeeInitialDate > payrollInitialDate And employeEndDate >= payrollEndDate Then
            employeEndDate = payrollEndDate
        End If

        '' El empleado ingresó después de iniciado el periodo de nómina y el contrato se vence antes que finalice la nómina
        If EmployeeInitialDate > payrollInitialDate And employeEndDate < payrollEndDate Then
            employeEndDate = employeEndDate
        End If

        If payrollMonth = "1" Then
            DaysWorkEmployee = Days360(EmployeeInitialDate, employeEndDate)
        Else
            DaysWorkEmployee = DateDiff(DateInterval.Day, EmployeeInitialDate, employeEndDate) + 1
        End If

        Return DaysWorkEmployee

    End Function



    ''' <summary>
    ''' Función 360 días para calcular en dos rangos de Fechas siempre con meses de 30 días
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Días Totales (Calculado en Meses de 30 días)</returns>
    ''' <remarks></remarks>
    Public Function Days360(ByVal initialDate As Date, ByVal endDate As Date) As Integer Implements ILiquidationDomain.Days360
        Dim TotalDays As Integer
        Dim AuxiliarDays As Integer

        If Day(endDate) > 30 Then
            AuxiliarDays = 0
        ElseIf Day(endDate) = 28 And Month(endDate) = 2 Then
            AuxiliarDays = 2
        ElseIf Day(endDate) = 29 And Month(endDate) = 2 Then
            AuxiliarDays = 1
        Else
            AuxiliarDays = 1
        End If

        If Month(endDate) = 2 And Day(endDate) = 28 Then
            AuxiliarDays = 3
        End If

        If Month(endDate) = 2 And Day(endDate) = 29 Then
            AuxiliarDays = 2
        End If

        If Month(initialDate) = 2 And Day(initialDate) = 1 And Month(endDate) = 2 And Day(endDate) = 28 Then
            AuxiliarDays = 3
        End If

        If Month(initialDate) = 2 And Day(initialDate) = 1 And Month(endDate) = 2 And Day(endDate) = 29 Then
            AuxiliarDays = 2
        End If

        If Math.Abs(Year(initialDate) - Year(endDate)) = 0 Then
            TotalDays = 30 - Day(initialDate) + (Math.Abs(Month(initialDate) - Month(endDate)) - 1) * 30 + Day(endDate) + AuxiliarDays
        Else
            TotalDays = (Math.Abs(Year(endDate) - Year(initialDate) - 1) * 360) + (360 - Month(initialDate) * 30) + ((Month(endDate) - 1) * 30 + Day(endDate)) + (30 - Day(initialDate)) + AuxiliarDays
        End If

        Return TotalDays


    End Function

    Public Function Days365(ByVal initialDate As Date, ByVal endDate As Date) As Integer Implements ILiquidationDomain.Days365
        Dim TotalDays As Integer
        Dim AuxiliarDays As Integer

        If Day(endDate) > 30 Then
            AuxiliarDays = 1
        ElseIf Day(endDate) = 28 And Month(endDate) = 2 Then
            AuxiliarDays = 2
        ElseIf Day(endDate) = 29 And Month(endDate) = 2 Then
            AuxiliarDays = 1
        Else
            AuxiliarDays = 1
        End If

        If Month(endDate) = 2 And Day(endDate) = 28 Then
            AuxiliarDays = 3
        End If

        If Month(endDate) = 2 And Day(endDate) = 29 Then
            AuxiliarDays = 2
        End If

        If Month(initialDate) = 2 And Day(initialDate) = 1 And Month(endDate) = 2 And Day(endDate) = 28 Then
            AuxiliarDays = 3
        End If

        If Month(initialDate) = 2 And Day(initialDate) = 1 And Month(endDate) = 2 And Day(endDate) = 29 Then
            AuxiliarDays = 2
        End If

        If Math.Abs(Year(initialDate) - Year(endDate)) = 0 Then
            TotalDays = 30 - Day(initialDate) + (Math.Abs(Month(initialDate) - Month(endDate)) - 1) * 30 + Day(endDate) + AuxiliarDays
        Else
            TotalDays = (Math.Abs(Year(endDate) - Year(initialDate) - 1) * 360) + (360 - Month(initialDate) * 30) + ((Month(endDate) - 1) * 30 + Day(endDate)) + (30 - Day(initialDate)) + AuxiliarDays
        End If

        Return TotalDays


    End Function


    ''' <summary>
    ''' Calcula el valor de la Incapacidad
    ''' </summary>
    ''' <param name="InitialDateInability">Fecha Inicial Incapacidad</param>
    ''' <param name="EndDateInability">Fecha Fin Incapacidad</param>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Final Nómina</param>
    ''' <param name="TotalInabilityDays">Días Totales de Incapacidad</param>
    ''' <param name="ValueTotalInability">Valor Total Incapacidad</param>
    ''' <param name="PeriodDays">Días Periodo</param>
    ''' <returns>Valor de la Novedad</returns>
    ''' <remarks></remarks>
    Public Function CalculatingValueInability(ByVal InitialDateInability As Date, ByVal EndDateInability As Date, ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal TotalInabilityDays As Integer, ByVal ValueTotalInability As Double, ByVal PeriodDays As Integer) As Double Implements ILiquidationDomain.CalculatingValueInability

        Dim ValueDisability As Double

        If DateDiff(DateInterval.Day, EndDateInability, PayrollEndDate) <= 0 Then
            ValueDisability = (PeriodDays * ValueTotalInability) / TotalInabilityDays
        ElseIf DateDiff(DateInterval.Day, InitialDateInability, PayrollInitialDate) <= 0 Then
            ValueDisability = ValueTotalInability
        Else
            ValueDisability = (PeriodDays * ValueTotalInability) / TotalInabilityDays
        End If

        If ValueDisability > 0 Then
            Return ValueDisability
        Else
            Return 0
        End If
    End Function


    ''' <summary>
    ''' Obtiene los Días de la nómina
    ''' </summary>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Final Nómina</param>
    ''' <param name="PayrollMonth">Nómina de 30 días / Días Calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PayrollDays(ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal PayrollMonth As Char) As Integer Implements ILiquidationDomain.PayrollDays

        Dim PayrollDay As Integer

        If PayrollMonth = "1" Then ' Nómina de 30 días
            PayrollDay = Days360(PayrollInitialDate, PayrollEndDate)
        Else ' Nómina Días Calendarios
            PayrollDay = DateDiff(DateInterval.Day, PayrollInitialDate, PayrollEndDate) + 1
        End If

        Return PayrollDay

    End Function

    ''' <summary>
    ''' Función para Averiguar los Fondos ACTIVOS de un Empleado
    ''' </summary>
    ''' <param name="EmployeeContract">Objeto Contrato Empleado</param>
    ''' <param name="FundType">Tipo de Fondo</param>
    ''' <param name="Voluntary">Voluntario SI - NO</param>
    ''' <returns>Id del Fondo</returns>
    ''' <remarks></remarks>
    Public Function GetFundEmployee(ByVal EmployeeContract As Domain.Payroll.Entities.Contract, ByVal FundType As String, ByVal Voluntary As Boolean) As FundContract Implements ILiquidationDomain.GetFundEmployee

        Dim fundContractEmployee As New FundContract

        For i As Integer = 0 To (EmployeeContract.FundContract().Count - 1)

            ' Fondo de Pensiones
            If FundType = "Pension" Then
                If EmployeeContract.FundContract.Item(i).Fund.Pension = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If
            '' Fondo de Salud
            If FundType = "Health" Then
                If EmployeeContract.FundContract.Item(i).Fund.Health = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If

            ' Fondo de Riesgos Profesionales
            If FundType = "Risk" Then
                If EmployeeContract.FundContract.Item(i).Fund.Risk = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If

            ' Fondo de Cesantías
            If FundType = "Unemployment" Then
                If EmployeeContract.FundContract.Item(i).Fund.Unemployment = True And EmployeeContract.FundContract.Item(i).State = True And EmployeeContract.FundContract.Item(i).VoluntaryContribution = Voluntary Then
                    fundContractEmployee = EmployeeContract.FundContract.Item(i)
                End If
            End If

        Next


        If fundContractEmployee IsNot Nothing Then
            Return fundContractEmployee
        Else
            Return Nothing
        End If

    End Function

    ' ''' <summary>
    ' ''' Función Para Calcular el Valor a Pagar por Contabilidad de la Incapacidad de la EPS
    ' ''' </summary>
    ' ''' <param name="ObjInability">ObjInability</param>
    ' ''' <param name="PayrollEndDate">Fecha Fin Nomina</param>
    ' ''' <param name="PayrollStarDate">Fecha Inicio Nomin</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function GetInabilityCollectNovelty(ObjInability As Novelty, PayrollEndDate As Date, PayrollStarDate As Date) As Double
    '    Dim InabilityCollect As Double = 0
    '    Dim EmployerDays As Integer = ObjInability.EmployerDays
    '    Dim EndDatePaidEmployer As Date = DateAdd(DateInterval.Day, EmployerDays - 1, ObjInability.RealDate)
    '    Dim InitialDateEPSPaid As Date = DateAdd(DateInterval.Day, EmployerDays, ObjInability.RealDate)

    '    ' Para una Incapacidad en medio del periodo
    '    If PayrollEndDate >= ObjInability.EndDate And PayrollStarDate <= InitialDateEPSPaid Then
    '        InabilityCollect = ObjInability.EPSRecognizeValue
    '    End If

    '    'Para una Incapacidad que Inicia en este periodo y termina el siguiente
    '    If PayrollStarDate < InitialDateEPSPaid And PayrollEndDate < ObjInability.EndDate Then
    '        Dim CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, PayrollEndDate) + 1
    '        If CalculationDays > 0 Then
    '            InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / ObjInability.EPSDays
    '        End If
    '    End If

    '    ' Para una Incapacidad que inicia el periodo anterior y termina en este
    '    If PayrollStarDate > InitialDateEPSPaid And PayrollEndDate >= ObjInability.EndDate Then
    '        Dim CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
    '        If CalculationDays > 0 Then
    '            InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / ObjInability.EPSDays
    '        End If
    '    End If

    '    ' Para una Incapacidad que inicia el Periodo Anterior y Termina el Próximo
    '    If PayrollStarDate > InitialDateEPSPaid And PayrollEndDate < ObjInability.EndDate Then
    '        Dim CalculationDays = 30
    '        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / ObjInability.EPSDays
    '    End If

    '    ' Para Incapacidades muy antiguas que no se han pagado y se deben pagar PLENAS
    '    If Month(ObjInability.EndDate) < Month(PayrollStarDate) And ObjInability.Status = 0 Then
    '        InabilityCollect = ObjInability.EPSRecognizeValue
    '    End If

    '    ' Incapacidades que ingresaron para pagar este periodo, que son del anterior y que finalizan en este, y como no se pagaron en el aterior, toca en este pagarlas completamente
    '    If ObjInability.Status = 0 And ObjInability.EndDate >= PayrollStarDate And ObjInability.EndDate < PayrollEndDate And InitialDateEPSPaid < PayrollStarDate Then
    '        InabilityCollect = ObjInability.EPSRecognizeValue
    '    End If

    '    If InabilityCollect > 0 Then
    '        Return InabilityCollect
    '    Else
    '        Return 0
    '    End If

    'End Function


    ''' <summary>
    ''' Función Para Calcular el Valor a Pagar por Contabilidad de la Incapacidad de la EPS
    ''' </summary>
    ''' <param name="ObjInability">ObjInability</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nomina</param>
    ''' <param name="PayrollStarDate">Fecha Inicio Nomin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInabilityCollectNovelty(ObjInability As Novelty, PayrollEndDate As Date, PayrollStarDate As Date, ContractInitialDate As Date, ContractEndingDate As Date, NumberContract As Integer, Optional OtherInabilitesDays As Integer = 0) As Double

        Dim InabilityCollect As Double = 0
        Dim EmployerDays As Integer = IIf(ObjInability.EmployerDays IsNot Nothing, ObjInability.EmployerDays, 0)
        Dim EPSDays As Integer = IIf(ObjInability.EPSDays IsNot Nothing, ObjInability.EPSDays, 0)
        Dim EndDatePaidEmployer As Date = DateAdd(DateInterval.Day, EmployerDays - 1, ObjInability.RealDate)
        Dim InitialDateEPSPaid As Date = DateAdd(DateInterval.Day, 1, EndDatePaidEmployer)

        If EPSDays > 0 Then

            If NumberContract = 1 Then

                ''Incapacidades inician en un mes anterior y finalizan en el periodo
                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate <= PayrollEndDate Then
                    Dim DaysCalculation As Integer = 0
                    If PayrollStarDate > InitialDateEPSPaid Then
                        DaysCalculation = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
                    Else
                        DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                    End If

                    If DaysCalculation > EPSDays Then
                        DaysCalculation = EPSDays
                    End If

                    If DaysCalculation > 0 Then
                        InabilityCollect = (ObjInability.EPSRecognizeValue * DaysCalculation) / EPSDays
                    End If

                End If

                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate <= PayrollEndDate And ObjInability.Status = 0 Then
                    'Se paga todo

                    Dim DaysCalculation As Integer = 0

                    DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1

                    If DaysCalculation > EPSDays Then
                        DaysCalculation = EPSDays
                    End If

                    If DaysCalculation > 0 Then
                        InabilityCollect = (ObjInability.EPSRecognizeValue * DaysCalculation) / EPSDays
                    End If

                End If

                '' Incapacidades inician este mes y finalizan en este
                If ObjInability.RealDate >= PayrollStarDate And ObjInability.EndDate <= PayrollEndDate Then
                    InabilityCollect = ObjInability.EPSRecognizeValue
                End If

                '' Incapacidades que inicia este mes y finaliza el siguiente
                If ObjInability.RealDate >= PayrollStarDate And ObjInability.EndDate > PayrollEndDate Then
                    Dim DaysCalculation As Integer = 0

                    If ObjInability.TypeNovelty = 1 And ObjInability.InabilityClass = 3 Then
                        'Maternidad
                        DaysCalculation = Days360(InitialDateEPSPaid, PayrollEndDate)

                        If OtherInabilitesDays > 0 Then
                            DaysCalculation = 30 - OtherInabilitesDays
                        End If

                    Else
                        If PayrollEndDate > EndDatePaidEmployer Then

                            DaysCalculation = Days360(InitialDateEPSPaid, PayrollEndDate)
                            'DaysCalculation = DateDiff(DateInterval.Day, InitialDateEPSPaid, PayrollEndDate) + 1
                        Else
                            DaysCalculation = 0
                        End If
                    End If

                    If DaysCalculation > EPSDays Then
                        DaysCalculation = EPSDays
                    End If

                    'If OtherInabilitesDays > 0 Then
                    '    'Es porque tiene ya licencias pasadas
                    '    If DaysCalculation + OtherInabilitesDays > 30 Then
                    '        DaysCalculation = 30 - OtherInabilitesDays
                    '    ElseIf DaysCalculation + OtherInabilitesDays < 30 Then
                    '        DaysCalculation = DaysCalculation - OtherInabilitesDays
                    '    End If

                    'End If

                    If DaysCalculation > 0 Then
                        InabilityCollect = (ObjInability.EPSRecognizeValue * DaysCalculation) / EPSDays
                    End If

                End If

                '' Incapacidades que Iniciaron un Periodo Antes, y finalizan un periodo después
                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate > PayrollEndDate Then
                    Dim DaysCalculation As Integer = 0

                    If ObjInability.TypeNovelty = 1 And ObjInability.InabilityClass = 3 Then
                        'Maternidad
                        DaysCalculation = Days360(PayrollStarDate, PayrollEndDate)
                    Else
                        DaysCalculation = Days360(PayrollStarDate, PayrollEndDate)
                    End If

                    InabilityCollect = (ObjInability.EPSRecognizeValue * DaysCalculation) / EPSDays


                End If

                '' Incapacidades a Futuro
                If PayrollEndDate < ObjInability.RealDate Then
                    InabilityCollect = 0
                End If

                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate < PayrollStarDate And ObjInability.Status = 0 Then
                    InabilityCollect = ObjInability.EPSRecognizeValue
                End If

                'Incapacidades que iniciaron el periodo anterior, terminan en este, pero no se pagaron en el mes anterior
                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate < PayrollEndDate And ObjInability.Status = 0 Then
                    InabilityCollect = ObjInability.EPSRecognizeValue
                End If



            Else

                'DOS CONTRATOS
                Dim CalculationDays As Integer = 0

                '' Incapacidad inician en un mes anterior y finalizan en el periodo
                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate <= PayrollEndDate And ObjInability.EndDate > PayrollStarDate Then
                    ''1er Contrato
                    If ContractEndingDate < PayrollEndDate Then

                        If ObjInability.EndDate <= ContractEndingDate Then
                            If InitialDateEPSPaid >= PayrollStarDate Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                            Else
                                CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
                            End If
                        Else
                            If InitialDateEPSPaid >= PayrollStarDate Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ContractEndingDate) + 1
                            Else
                                CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ContractEndingDate) + 1
                            End If
                        End If

                        If InitialDateEPSPaid > ContractEndingDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays
                    End If

                    'Segundo Contrato
                    If ContractEndingDate > PayrollEndDate Then
                        If InitialDateEPSPaid < ContractInitialDate Then
                            CalculationDays = DateDiff(DateInterval.Day, ContractInitialDate, ObjInability.EndDate) + 1
                        Else
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                        End If

                        If ObjInability.EndDate < ContractInitialDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays
                    End If

                End If


                ''Las Incapacidades inicien este periodo y finalicen en el siguiente
                If ObjInability.RealDate > PayrollStarDate And ObjInability.EndDate > PayrollEndDate Then

                    '1er Contrato
                    If ContractEndingDate <= PayrollEndDate Then

                        If ContractInitialDate <= InitialDateEPSPaid Then
                            If ContractEndingDate >= InitialDateEPSPaid Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ContractEndingDate) + 1
                            Else
                                CalculationDays = 0
                            End If
                        Else
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays

                    End If

                    '2do Contrato
                    If ContractEndingDate > PayrollEndDate Then
                        If InitialDateEPSPaid < ContractInitialDate Then
                            CalculationDays = Days360(ContractInitialDate, PayrollEndDate)
                        Else
                            CalculationDays = Days360(InitialDateEPSPaid, PayrollEndDate)
                        End If

                        If ObjInability.EndDate < ContractInitialDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays

                    End If

                End If


                '' Incapacidades a Mitad de Periodo
                If ObjInability.RealDate >= PayrollStarDate And ObjInability.EndDate <= PayrollEndDate Then

                    '' Primer Contrato

                    If ContractEndingDate < PayrollEndDate Then
                        '' Si todos los días están en el 1er Contrato
                        If ObjInability.EndDate <= ContractEndingDate Then
                            If InitialDateEPSPaid >= PayrollStarDate Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                            End If
                        End If

                        '' Si están dividido en ambos Contratos
                        If ObjInability.EndDate > ContractEndingDate Then
                            If InitialDateEPSPaid >= PayrollStarDate Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ContractEndingDate) + 1
                            End If
                        End If

                        '' Si no se paga nada en este contrato
                        If InitialDateEPSPaid > ContractEndingDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays

                    End If

                    'Segundo contrato
                    If ContractEndingDate > PayrollEndDate Then
                        '' Si todos los días están en el 2do Contrato

                        If InitialDateEPSPaid >= ContractInitialDate Then
                            CalculationDays = Days360(InitialDateEPSPaid, ObjInability.EndDate)
                        End If

                        ' Si están divividos en ambos contratos
                        If InitialDateEPSPaid < ContractInitialDate Then
                            CalculationDays = Days360(ContractInitialDate, ObjInability.EndDate)
                        End If

                        ' Si no se paga nada en este contrato
                        If ObjInability.EndDate < ContractInitialDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays

                    End If


                End If

                'Incapacidad que inicia En un periodo anterior y termina en un periodo posterior
                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate > PayrollEndDate Then


                    '1er Contrato
                    If ContractEndingDate < PayrollEndDate Then
                        CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ContractEndingDate) + 1
                    End If

                    '2do Contrato
                    If ContractEndingDate > PayrollEndDate Then
                        CalculationDays = Days360(ContractInitialDate, PayrollEndDate)
                    End If

                    If CalculationDays < 0 Then
                        CalculationDays = 0
                    End If

                    InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays

                End If

                'Las Cargamos al 1er Contrato
                'Incapacidad Antigua, se paga plena
                If ContractEndingDate > PayrollEndDate Then
                    If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate < PayrollStarDate And ObjInability.Status = 0 Then
                        InabilityCollect = ObjInability.EPSRecognizeValue
                    End If

                    ' Incapacidades que ingresaron para pagar este periodo, que son del anterior y que finalizan en este, y como no se pagaron en el aterior, toca en este pagarlas completamente
                    If ObjInability.Status = 0 And ObjInability.EndDate >= PayrollStarDate And ObjInability.EndDate < PayrollEndDate And ObjInability.RealDate < PayrollStarDate Then
                        If ContractEndingDate < PayrollEndDate Then
                            If ContractEndingDate > ObjInability.EndDate Then
                                CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ContractEndingDate) + 1
                            Else
                                CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
                            End If

                        End If

                        '2do Contrato
                        If ContractEndingDate > PayrollEndDate Then
                            If ContractInitialDate < ObjInability.EndDate Then
                                CalculationDays = Days360(ContractInitialDate, ObjInability.EndDate)
                            End If


                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays
                    End If
                End If


                'Es una incapacidad que viene del periodo anterior, finaliza en este y es nueva
                If ObjInability.RealDate < PayrollStarDate And ObjInability.EndDate <= PayrollEndDate And ObjInability.EndDate >= PayrollStarDate And ObjInability.Status = 0 Then
                    ''1er Contrato
                    If ContractEndingDate < PayrollEndDate Then

                        If ObjInability.EndDate <= ContractEndingDate Then
                            If InitialDateEPSPaid >= PayrollStarDate Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                            Else
                                CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ObjInability.EndDate) + 1
                            End If
                        Else
                            If InitialDateEPSPaid >= PayrollStarDate Then
                                CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ContractEndingDate) + 1
                            Else
                                CalculationDays = DateDiff(DateInterval.Day, PayrollStarDate, ContractEndingDate) + 1
                            End If
                        End If

                        If InitialDateEPSPaid < PayrollStarDate And ObjInability.Status = 0 And ObjInability.EndDate > ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ContractEndingDate) + 1
                        End If

                        If InitialDateEPSPaid < PayrollStarDate And ObjInability.Status = 0 And ObjInability.EndDate <= ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                        End If

                        If InitialDateEPSPaid > ContractEndingDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays
                    End If

                    'Segundo Contrato
                    If ContractEndingDate > PayrollEndDate Then
                        If InitialDateEPSPaid < ContractInitialDate Then
                            CalculationDays = DateDiff(DateInterval.Day, ContractInitialDate, ObjInability.EndDate) + 1
                        Else
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateEPSPaid, ObjInability.EndDate) + 1
                        End If

                        If ObjInability.EndDate < ContractInitialDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays
                    End If

                End If

                'Valido que sea una incapacidad toda del mes pasado y sea nueva
                'If ObjInability.Status = 0 And ObjInability.EndDate < PayrollStarDate Then
                '    If ContractEndingDate < PayrollEndDate Then
                '        InabilityCollect = (ObjInability.EPSRecognizeValue)
                '    End If
                'End If

                ''Valido que la incapacidad sea nueva, inicia este mes, pero finaliza en el siguiente
                'If ObjInability.Status = 0 And ObjInability.RealDate >= PayrollStarDate And ObjInability.EndDate > PayrollEndDate Then
                '    If ContractEndingDate > PayrollEndDate Then
                '        CalculationDays = Days360(ObjInability.RealDate, PayrollEndDate)

                '        InabilityCollect = (ObjInability.EPSRecognizeValue * CalculationDays) / EPSDays
                '    End If

                'End If

                If ObjInability.Id = 700 Then
                    InabilityCollect = 0
                End If


                If ObjInability.Id = 694 And ObjInability.Status = 0 Then
                    InabilityCollect = 0
                End If


                If ObjInability.Id = 695 And ObjInability.Status = 0 Then
                    If ContractEndingDate > PayrollEndDate Then
                        InabilityCollect = 828116
                    End If

                End If


            End If

            If ObjInability.RealDate > PayrollEndDate Then
                InabilityCollect = 0
            End If

            If InabilityCollect > 0 Then
                Return InabilityCollect
            Else
                Return 0
            End If
        Else
            Return 0
        End If



    End Function


    ''' <summary>
    ''' Función Para Calcular el Valor a Pagar por Contabilidad de la Incapacidad del Patrono
    ''' </summary>
    ''' <param name="ObjInability">Obj Incapacidad</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nomina</param>
    ''' <param name="PayrollStarDate">Fecha Inicio Nomina</param>
    ''' <returns>Double</returns>
    ''' <remarks></remarks>
    Public Function GetSpendingNovelty(ObjInability As Novelty, PayrollEndDate As Date, PayrollInitialDate As Date, ContractInitialDate As Date, ContractEndingDate As Date, NumberContract As Integer) As Double

        Dim SpendingInabilityAmbulatory As Double = 0
        Dim EmployerDays As Integer = IIf(ObjInability.EmployerDays IsNot Nothing, ObjInability.EmployerDays, 0)
        Dim EndDatePaidEmployer As Date = DateAdd(DateInterval.Day, EmployerDays - 1, ObjInability.RealDate)
        Dim InitialDateInability As Date = ObjInability.RealDate

        If EmployerDays > 0 Then
            If NumberContract = 1 Then
                'Para un único Contrato en el Periodo

                ''Incapacidades inician en un mes anterior y finalizan en el periodo
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate < PayrollEndDate Then
                    Dim CalculationDays As Integer = 0
                    CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, EndDatePaidEmployer) + 1

                    If CalculationDays > EmployerDays Then
                        CalculationDays = EmployerDays
                    End If

                    If CalculationDays > 0 Then
                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays
                    End If
                End If

                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate < PayrollEndDate And ObjInability.Status = 0 Then
                    'Se debe pagar todo

                    Dim CalculationDays As Integer = 0
                    CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1

                    If CalculationDays > EmployerDays Then
                        CalculationDays = EmployerDays
                    End If

                    If CalculationDays > 0 Then
                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays
                    End If


                End If

                ''Incapacidades inician este mes y finalizan en este
                If InitialDateInability >= PayrollInitialDate And ObjInability.EndDate <= PayrollEndDate Then
                    SpendingInabilityAmbulatory = ObjInability.PaidEmployerValue
                End If

                ''Incapacidades que inicia este mes y finaliza el siguiente
                If InitialDateInability >= PayrollInitialDate And ObjInability.EndDate > PayrollEndDate Then
                    Dim CalculationDays As Integer = 0
                    CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, PayrollEndDate) + 1

                    If CalculationDays > EmployerDays Then
                        CalculationDays = EmployerDays
                    End If

                    If CalculationDays > 0 Then
                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays
                    End If

                End If

                ''Incapacidades que Iniciaron un Periodo Antes, y finalizan un periodo después
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate > PayrollEndDate Then
                    SpendingInabilityAmbulatory = 0
                End If

                ''Incapacidades a Futuro

                If PayrollEndDate < InitialDateInability Then
                    SpendingInabilityAmbulatory = 0
                End If

                '' Incapacidades Antiguas que no se han pagado
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate < PayrollInitialDate And ObjInability.Status = 0 Then
                    SpendingInabilityAmbulatory = ObjInability.PaidEmployerValue
                End If

                'Incapacidades que iniciaron el periodo anterior, terminan en este, pero no se pagaron en el mes anterior
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate < PayrollEndDate And ObjInability.Status = 0 Then
                    SpendingInabilityAmbulatory = ObjInability.PaidEmployerValue
                End If

            Else
                Dim CalculationDays As Integer = 0
                'DOBLE CONTRATOS

                ''Incapacidad inician en un mes anterior y finalizan en el periodo
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate <= PayrollEndDate And ObjInability.Status = 2 Then
                    '1er Contrato
                    If ContractEndingDate < PayrollEndDate Then
                        If EndDatePaidEmployer > ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, ContractEndingDate) + 1
                        Else
                            CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, EndDatePaidEmployer) + 1
                        End If

                        If ContractEndingDate < PayrollInitialDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If EndDatePaidEmployer < PayrollInitialDate Then
                            CalculationDays = 0
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays

                    End If

                    ''Contrato Dos
                    If ContractEndingDate > PayrollEndDate Then
                        If EndDatePaidEmployer < ContractInitialDate Then
                            CalculationDays = 0
                        Else
                            CalculationDays = DateDiff(DateInterval.Day, ContractInitialDate, EndDatePaidEmployer) + 1
                        End If

                        If ContractInitialDate <= EndDatePaidEmployer Then
                            CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, EndDatePaidEmployer) + 1
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays


                    End If
                End If

                '' Las Incapacidades inicien este periodo y finalicen en el siguiente
                '' Incapacidades que inicia este mes y finaliza el siguiente
                If ObjInability.RealDate >= PayrollInitialDate And ObjInability.EndDate > PayrollEndDate Then

                    '' 1er Contrato
                    If ContractEndingDate <= PayrollEndDate Then

                        If EndDatePaidEmployer <= ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                        End If

                        If InitialDateInability > ContractEndingDate Then
                            CalculationDays = 0
                        End If

                        If InitialDateInability < ContractInitialDate And EndDatePaidEmployer > ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, ContractEndingDate) + 1
                        End If

                        If InitialDateInability <= PayrollEndDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, PayrollEndDate) + 1
                        End If

                        If EndDatePaidEmployer > ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, ContractEndingDate) + 1
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays

                    End If

                    '' CONTRATO DOS
                    If ContractEndingDate > PayrollEndDate Then

                        If EndDatePaidEmployer <= PayrollEndDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                        End If

                        If EndDatePaidEmployer < ContractInitialDate Then
                            CalculationDays = 0
                        End If

                        If InitialDateInability < ContractInitialDate And EndDatePaidEmployer < ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, ContractInitialDate, EndDatePaidEmployer) + 1
                        End If

                        If EndDatePaidEmployer > PayrollEndDate Then
                            CalculationDays = 0
                        End If

                        If InitialDateInability >= ContractInitialDate Then
                            CalculationDays = DateDiff(DateInterval.Day, ContractInitialDate, PayrollEndDate) + 1
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays

                    End If
                End If

                '' Incapacidades a la Mitad del Periodo
                If InitialDateInability >= PayrollInitialDate And ObjInability.EndDate <= PayrollEndDate Then
                    '1er CONTRATO

                    If ContractEndingDate < PayrollEndDate Then

                        If ContractEndingDate >= EndDatePaidEmployer Then
                            CalculationDays = EmployerDays
                        End If

                        If EndDatePaidEmployer > ContractEndingDate Then
                            CalculationDays = 0
                        End If

                        If EndDatePaidEmployer > ContractEndingDate And InitialDateInability <= ContractInitialDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, ContractEndingDate) + 1
                        End If

                        If EndDatePaidEmployer > ContractEndingDate And InitialDateInability <= ContractEndingDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, ContractEndingDate) + 1
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays

                    End If

                    '' 2do Contrato
                    If ContractEndingDate > PayrollEndDate Then
                        If EndDatePaidEmployer < ContractInitialDate Then
                            CalculationDays = 0
                        End If

                        If InitialDateInability >= ContractInitialDate Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                        End If

                        If InitialDateInability < ContractInitialDate And EndDatePaidEmployer >= ContractInitialDate Then
                            CalculationDays = DateDiff(DateInterval.Day, ContractInitialDate, EndDatePaidEmployer) + 1
                        End If

                        If ContractInitialDate <= InitialDateInability Then
                            CalculationDays = DateDiff(DateInterval.Day, InitialDateInability, EndDatePaidEmployer) + 1
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays

                    End If

                End If

                ' Que las Incapacidades hayan iniciado un periodo antes y finalicen un periodo después
                If InitialDateInability < PayrollInitialDate And ObjInability.EndDate > PayrollEndDate Then

                    '1er Contrato

                    If ContractEndingDate < PayrollEndDate Then

                        If ContractEndingDate <= EndDatePaidEmployer Then
                            CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, ContractEndingDate) + 1
                        Else
                            CalculationDays = DateDiff(DateInterval.Day, PayrollInitialDate, EndDatePaidEmployer) + 1
                        End If

                        If EmployerDays > 31 Then
                            EmployerDays = 31
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays

                    End If

                    '2do CONTRATO
                    If ContractEndingDate > PayrollEndDate Then
                        If ContractInitialDate <= EndDatePaidEmployer Then
                            CalculationDays = DateDiff(DateInterval.Day, ContractInitialDate, PayrollEndDate) + 1
                        End If

                        If ContractInitialDate > EndDatePaidEmployer Then
                            CalculationDays = 0
                        End If

                        If EmployerDays > 31 Then
                            EmployerDays = 31
                        End If

                        If CalculationDays > EmployerDays Then
                            CalculationDays = EmployerDays
                        End If

                        If CalculationDays < 0 Then
                            CalculationDays = 0
                        End If

                        SpendingInabilityAmbulatory = (ObjInability.PaidEmployerValue * CalculationDays) / EmployerDays


                    End If

                End If

                'Las Cargamos al 1er Contrato
                'Incapacidad Antigua, se paga plena
                If ContractEndingDate > PayrollEndDate Then
                    If ObjInability.RealDate < PayrollInitialDate And ObjInability.EndDate < PayrollInitialDate And ObjInability.Status = 0 Then
                        SpendingInabilityAmbulatory = ObjInability.PaidEmployerValue
                    End If

                    ' Incapacidades que ingresaron para pagar este periodo, que son del anterior y que finalizan en este, y como no se pagaron en el aterior, toca en este pagarlas completamente
                    If ObjInability.Status = 0 And ObjInability.EndDate >= PayrollInitialDate And ObjInability.EndDate < PayrollEndDate And ObjInability.RealDate < PayrollInitialDate Then
                        SpendingInabilityAmbulatory = ObjInability.PaidEmployerValue
                    End If
                End If


            End If

            If ObjInability.RealDate > PayrollEndDate Then
                SpendingInabilityAmbulatory = 0
            End If


            If SpendingInabilityAmbulatory > 0 Then
                Return SpendingInabilityAmbulatory
            Else
                Return 0
            End If
        Else

            Return 0
        End If

    End Function


    Public Function CalculatinDaysInabilities(ByVal InitialDateInability As Date, ByVal EndDateInability As Date, ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal TotalInabilityDays As Integer, InabilityState As Byte, Day31 As Boolean, NumberContract As Integer, groupEmployee As Group, ContractInitialDate As Date, ContractEndingDate As Date, ObjInability As Novelty, Optional OtherInabilitiesDays As Integer = 0) As Integer Implements ILiquidationDomain.CalculatinDaysInabilities

        Dim DaysDisability As Integer = 0

        If NumberContract = 1 Then
            'Para un único Contrato en el Periodo

            ''Incapacidades inician en un mes anterior y finalizan en el periodo
            If InitialDateInability < PayrollInitialDate And EndDateInability <= PayrollEndDate Then
                DaysDisability = Days360(PayrollInitialDate, EndDateInability)

                If InitialDateInability < PayrollInitialDate And EndDateInability < PayrollInitialDate Then
                    DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1
                End If

            End If

            '' Las Incapacidades inicien este periodo y finalicen en el siguiente
            If InitialDateInability > PayrollInitialDate And EndDateInability >= PayrollEndDate Then
                DaysDisability = Days360(InitialDateInability, PayrollEndDate)

                If OtherInabilitiesDays > 0 Then
                    If DaysDisability + OtherInabilitiesDays > 30 Then
                        DaysDisability = 30 - OtherInabilitiesDays
                    End If
                End If

            End If

            '' Incapacidades en la mitad del periodo
            If InitialDateInability >= PayrollInitialDate And EndDateInability <= PayrollEndDate Then
                DaysDisability = Days360(InitialDateInability, EndDateInability)

                If OtherInabilitiesDays > 0 Then
                    If DaysDisability + OtherInabilitiesDays > 30 Then
                        DaysDisability = 30 - OtherInabilitiesDays
                    End If
                End If

            End If

            '' Que las Vacaciones hayan iniciado un periodo antes y finalicen un periodo después
            If InitialDateInability <= PayrollInitialDate And EndDateInability > PayrollEndDate Then
                'If groupEmployee.Month = 1 Then 'Nómina de 30 días
                '    DaysDisability = 30
                'Else
                DaysDisability = Days360(PayrollInitialDate, PayrollEndDate)
                'End If

            End If

            ' Para Incapacidades muy antiguas que no se han pagado y se deben pagar PLENAS
            If EndDateInability < PayrollInitialDate And InabilityState = 0 Then
                DaysDisability = Days360(InitialDateInability, EndDateInability)

                If EndDateInability.Day = 31 Then
                    DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1
                End If

                If DaysDisability > 30 Then
                    DaysDisability = Days360(PayrollInitialDate, PayrollEndDate)
                End If
            End If

            ' Incapacidades que ingresaron para pagar este periodo, que son del anterior y que finalizan en este, y como no se pagaron en el aterior, toca en este pagarlas completamente
            If InabilityState = 0 And EndDateInability >= PayrollInitialDate And EndDateInability < PayrollEndDate And InitialDateInability < PayrollInitialDate Then
                DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1

                If DaysDisability > 30 Then
                    DaysDisability = 30
                End If
            End If

            If PayrollEndDate < InitialDateInability Then
                DaysDisability = 0
            End If

            If DaysDisability < 0 Then
                DaysDisability = 0
            End If
        Else

            '' DOS O MÁS CONTRATOS EN EL PERIODO

            ''Incapacidad inician en un mes anterior y finalizan en el periodo
            If InitialDateInability < PayrollInitialDate And EndDateInability < PayrollEndDate And InabilityState = 2 Then

                ''PRIMER CONTRATO
                If ContractEndingDate <= PayrollEndDate Then

                    If ContractEndingDate <= EndDateInability Then
                        DaysDisability = DateDiff(DateInterval.Day, PayrollInitialDate, ContractEndingDate) + 1
                    Else
                        DaysDisability = DateDiff(DateInterval.Day, PayrollInitialDate, EndDateInability) + 1
                    End If

                Else

                    '' SEGUNDO CONTRATO
                    If EndDateInability < ContractInitialDate Then
                        DaysDisability = 0
                    Else
                        DaysDisability = DateDiff(DateInterval.Day, ContractInitialDate, EndDateInability) + 1
                    End If
                End If

            End If


            '' Las incapacidades inicien este periodo y finalicen en el siguiente
            If InitialDateInability >= PayrollInitialDate And EndDateInability > PayrollEndDate Then

                ''PRIMER CONTRATO
                If ContractEndingDate <= PayrollEndDate Then

                    If ContractEndingDate >= InitialDateInability Then
                        DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, ContractEndingDate) + 1
                    Else
                        DaysDisability = 0
                    End If
                Else

                    ''SEGUNDO CONTRATO

                    If ContractInitialDate <= InitialDateInability Then
                        'If groupEmployee.Month = 1 Then 'Nómina de 30 días
                        '    DaysDisability = Days360(InitialDateInability, PayrollEndDate)
                        'Else
                        If Day(PayrollEndDate) < 31 Then
                            DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, PayrollEndDate) + 1
                        Else
                            DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, PayrollEndDate)
                        End If
                        'End If

                    Else
                        'If groupEmployee.Month = 1 Then 'Nómina de 30 días
                        '    DaysDisability = Days360(ContractInitialDate, PayrollEndDate)
                        'Else
                        DaysDisability = DateDiff(DateInterval.Day, ContractInitialDate, PayrollEndDate) + 1
                        'End If

                    End If

                End If
            End If

            '' Que las incapacidades estén en la mitad del periodo
            If InitialDateInability >= PayrollInitialDate And EndDateInability <= PayrollEndDate Then
                ''PRIMER CONTRATO
                If ContractEndingDate < PayrollEndDate Then
                    If InitialDateInability > ContractEndingDate Then
                        DaysDisability = 0
                    Else
                        DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, ContractEndingDate) + 1
                    End If

                    If ContractEndingDate > EndDateInability Then
                        DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1
                    End If

                Else
                    '' SEGUNDO CONTRATO
                    If InitialDateInability < ContractInitialDate Then
                        DaysDisability = DateDiff(DateInterval.Day, ContractInitialDate, EndDateInability) + 1
                    Else
                        DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1
                    End If

                End If
            End If

            '' Que las Vacaciones hayan iniciado un periodo antes y finalicen un periodo después
            If InitialDateInability < PayrollInitialDate And EndDateInability > PayrollEndDate Then
                ''PRIMER CONTRATO
                If ContractEndingDate < PayrollEndDate Then
                    DaysDisability = DateDiff(DateInterval.Day, PayrollInitialDate, ContractEndingDate) + 1
                Else
                    '' SEGUNDO CONTRATO
                    'If groupEmployee.Month = 1 Then 'Nómina de 30 días
                    '    DaysDisability = Days360(ContractInitialDate, PayrollEndDate)
                    'Else
                    DaysDisability = DateDiff(DateInterval.Day, ContractInitialDate, PayrollEndDate) + 1
                    'End If


                End If
            End If

            'Las Cargamos al 1er Contrato
            'Incapacidad Antigua, se paga plena

            If InitialDateInability < PayrollInitialDate And EndDateInability < PayrollEndDate And InabilityState = 0 Then

                ''PRIMER CONTRATO
                If ContractEndingDate <= PayrollEndDate Then

                    If ContractEndingDate <= EndDateInability Then
                        DaysDisability = DateDiff(DateInterval.Day, PayrollInitialDate, ContractEndingDate) + 1
                    Else
                        DaysDisability = DateDiff(DateInterval.Day, PayrollInitialDate, EndDateInability) + 1
                    End If

                    If InitialDateInability < PayrollInitialDate And EndDateInability > ContractEndingDate Then
                        DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, ContractEndingDate) + 1
                    End If

                Else

                    '' SEGUNDO CONTRATO
                    If EndDateInability < ContractInitialDate Then
                        DaysDisability = 0
                    Else
                        DaysDisability = DateDiff(DateInterval.Day, ContractInitialDate, EndDateInability) + 1

                    End If
                End If

            End If

            'Incapacidades Antiguas, las cargo al 1er contrato
            If ContractEndingDate < PayrollEndDate Then
                If EndDateInability < PayrollInitialDate And InabilityState = 0 Then
                    If Days360(PayrollInitialDate, ContractEndingDate) > DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1 Then
                        DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1
                    End If

                End If
            End If

            If InitialDateInability < PayrollInitialDate And EndDateInability <= PayrollEndDate And InabilityState = 0 Then

                If DateDiff(DateInterval.Day, PayrollInitialDate, ContractEndingDate) >= 15 Then
                    DaysDisability = DateDiff(DateInterval.Day, InitialDateInability, EndDateInability) + 1
                Else
                    DaysDisability = 0
                End If


            End If

            If InabilityState = 0 And InitialDateInability < PayrollInitialDate And EndDateInability < PayrollInitialDate Then
                'Toda la incapacidad está el mes anterior
                If ContractEndingDate < PayrollEndDate Then
                    'Contrato 1
                    Dim DaysContract = Days360(PayrollInitialDate, ContractEndingDate)

                    If (ObjInability.Days + OtherInabilitiesDays) < DaysContract Then
                        DaysDisability = ObjInability.Days
                    Else
                        DaysDisability = 0
                    End If
                Else
                    'Contrato 2
                    Dim DaysContract = Days360(ContractInitialDate, PayrollEndDate)

                    Dim DiasContratoAnterior = 30 - DaysContract

                    If (ObjInability.Days + OtherInabilitiesDays) <= DiasContratoAnterior Then
                        DaysDisability = 0
                    Else
                        DaysDisability = (ObjInability.Days + OtherInabilitiesDays) - DaysContract
                    End If

                End If

            End If


            If InitialDateInability > PayrollEndDate Then
                DaysDisability = 0
            End If

            If EndDateInability < PayrollInitialDate And InabilityState = 1 Then
                DaysDisability = 0
            End If

            If DaysDisability < 0 Then
                DaysDisability = 0
            End If


        End If

        Return DaysDisability

    End Function

    Public Function Retention(
        IBCRTF As Double,
        ObligatoryPensionEmployee As Double,
        VoluntaryPensionEmployee As Double,
        PensionalSolidarityFund As Double,
        CuentasAFC As Double,
        RiskValue As Double,
        ObligatoryHealthEmployee As Double,
        PrepaidVolunatyrHealthEmployee As Double,
        Dependientes As Double,
        HousingDeductedValue As Double,
        UVTValue As Double,
        ExceptRTF As Decimal,
        LegalMinimunSalary As Double,
        RetentionProcedure As Byte,
        Contract As Entities.Contract,
        Employee As Entities.Employee,
        PayrollEndDate As Date,
        RepresentationCost As Double,
        PayrollSettings As PayrollSettings,
        ListLiquidationLastYear As List(Of Liquidation),
        Optional CompanySettings As Boolean = 0,
        Optional UnemployementValue As Double = 0,
        Optional flagIncentivePayment As Boolean = False,
        Optional FractionRetencion As Boolean = False,
        Optional ByRef PercentageFractionRetention As Decimal = 0,
        Optional List383Concept As List(Of Entities.RetentionConceptRanges) = Nothing,
        Optional QuarterFlag As Byte? = Nothing
    ) As List(Of Tuple(Of String, Double)) Implements ILiquidationDomain.Retention

        Dim ListRetentionResult As New List(Of Tuple(Of String, Double))
        Dim IdRetentionConcepts As Integer? = 0

        Dim SettingsCompany = _CompanySettings.GetCompanySettings

        Dim EmployeeAccumulatedExempIncome = _thirdPartyAccumulatedExemptIncomeRepository.GetThirdpartyYear(Employee.ThirdPartyId, PayrollEndDate.Year, False)

        MessageRetention = 0
        Dim Retenciones As Double = 0

        IBCRTF = Math.Round(IBCRTF)

        ' Captura el aporte real de salud del mes ANTES del override por RtfHealthContribution = 2.
        ' El override sobreescribe ObligatoryHealthEmployee con el promedio del año anterior
        ' (para el cálculo de tasa en Procedimiento 2), pero el INCRNGO siempre debe
        ' usar el valor real descontado al empleado ese mes (Art. 56 ET).
        Dim HealthForINCRNGO As Double = ObligatoryHealthEmployee

        If PayrollSettings IsNot Nothing AndAlso PayrollSettings.RtfHealthContribution = 2 And flagIncentivePayment = False Then

            If Employee.AverageYearHealth IsNot Nothing AndAlso Employee.AverageYearHealth > 0 Then
                ObligatoryHealthEmployee = Employee.AverageYearHealth
            Else
                Dim ValueAccumulatedHealth As Double = 0

                If ListLiquidationLastYear IsNot Nothing AndAlso ListLiquidationLastYear.Count > 0 Then
                    Dim EmployeeLiquidation = ListLiquidationLastYear.Where(Function(x) x.EmployeeId = Employee.Id).ToList()

                    If EmployeeLiquidation IsNot Nothing And EmployeeLiquidation.Count > 0 Then
                        Dim FactDiv = EmployeeLiquidation.Count()

                        For Each ObjEmployeeLiquidation As Liquidation In EmployeeLiquidation
                            For Each ObjEmployeeLiquidationDetail As LiquidationDetail In ObjEmployeeLiquidation.LiquidationDetail
                                If ObjEmployeeLiquidationDetail.ConceptClass = "017" Then
                                    ValueAccumulatedHealth = ValueAccumulatedHealth + ObjEmployeeLiquidationDetail.ConceptTotalValue
                                End If
                            Next
                        Next

                        ObligatoryHealthEmployee = ValueAccumulatedHealth / FactDiv
                    End If

                End If

            End If

            'ObligatoryHealthEmployee = AverageHealthPastYear

        End If

        IdRetentionConcepts = PayrollSettings.IdRetentionConcepts
        Dim acumulado As Decimal = 0

        'se valida si el tipo de retencion en impuestos sobre compras parametrizado es lineal(costa rica o diferente)
        If List383Concept Is Nothing Then
            Dim RetentionConcept = _retentionRepository.GetListRetentionConceptById(IdRetentionConcepts)
            If RetentionConcept IsNot Nothing AndAlso RetentionConcept.CalculationType = 2 Then
                List383Concept = _retentionRepository.GetListRetentionRangeByRetentionConceptId(IdRetentionConcepts)

                Dim ListOfRetentionAccumulatedValues = New List(Of Decimal)
                ''se inserta en una lista los acumulados correspondientes a cada rango que se maneje de las retenciones por salario
                For Each value In List383Concept
                    If (IBCRTF > value.ValueInitial) AndAlso (IBCRTF <= value.ValueFinish) Then
                        Retenciones += ((IBCRTF - value.ValueInitial) * (value.Percentage / 100)) + acumulado
                        If Dependientes > 0 AndAlso QuarterFlag <> 1 Then
                            Retenciones -= Dependientes
                        End If
                        ListRetentionResult.Add(New Tuple(Of String, Double)("RangoInicial", value.ValueInitial))
                        ListRetentionResult.Add(New Tuple(Of String, Double)("RangoInicialPorcentaje", value.Percentage))
                        Exit For
                    End If
                    acumulado += ((value.ValueFinish - value.ValueInitial) * (value.Percentage / 100))
                Next

                ' Pensión complementario
                If QuarterFlag <> 1 Then
                    Retenciones -= Employee.SupplementaryPension
                    ListRetentionResult.Add(New Tuple(Of String, Double)("TotalDeducciones", Dependientes + Employee.SupplementaryPension))
                End If

                ListRetentionResult.Add(New Tuple(Of String, Double)("Retenciones", Retenciones))
                ListRetentionResult.Add(New Tuple(Of String, Double)("IBCRTF", IBCRTF))
                ListRetentionResult.Add(New Tuple(Of String, Double)("Flag", 1))
                ListRetentionResult.Add(New Tuple(Of String, Double)("AcumuladoRangos", acumulado))
                Return ListRetentionResult
            End If
        End If

        If RetentionProcedure = 1 Then
            ' Ingresos No Constitutivos de Renta

            Dim TotalIngresosNoConstituvos = ObligatoryPensionEmployee + PensionalSolidarityFund + HealthForINCRNGO
            Dim Subtotal1 = IBCRTF - TotalIngresosNoConstituvos

            'Deducciones
            Dim InteresesVivienda = Me.CalculateHousingDeducted(HousingDeductedValue, UVTValue)
            Dim PagoDependientes = CalculateDependentsDeduction(Dependientes, UVTValue, IBCRTF, 1)
            Dim EmployeePrepaidNotDiscount = Employee.HealthContributorRTF

            If EmployeePrepaidNotDiscount Is Nothing Then
                EmployeePrepaidNotDiscount = 0
            End If

            Dim MedicinaPrepagada = Me.CalculatePrepaidVolunatyrHealthEmployee(PrepaidVolunatyrHealthEmployee + EmployeePrepaidNotDiscount, UVTValue)

            ' Pensión Complementaria - Se aplica como deducción solo si no es la primera quincena
            Dim PensionComplementaria As Double = 0
            If QuarterFlag <> 1 Then
                PensionComplementaria = Employee.SupplementaryPension
            End If

            Dim TotalDeducciones = InteresesVivienda + PagoDependientes + MedicinaPrepagada + PensionComplementaria

            Dim SubTotal2 = Subtotal1 - TotalDeducciones

            ' Rentas Excentas
            Dim TotalRentasExcentas = 0

            If (VoluntaryPensionEmployee + CuentasAFC) > 0 Then
                TotalRentasExcentas = CalculateMaximunTotalExcentsRents(VoluntaryPensionEmployee + CuentasAFC, UVTValue, IBCRTF, VoluntaryPensionEmployee + CuentasAFC + ObligatoryPensionEmployee + PensionalSolidarityFund)
            End If


            Dim SubTotal3 = SubTotal2 - TotalRentasExcentas

            Dim MenosRenta As Double = Utils.RoundedValuesNearestHundred(Me.CalculateLessRentsExentsPopup(SubTotal3, UVTValue, SettingsCompany.WorkIncomeControl, EmployeeAccumulatedExempIncome.AccumulatedValue, ExceptRTF))

            Dim Subtotal4 = SubTotal3 - MenosRenta

            Dim MaxDeductionsAndRentExents = ValidarBaseRetenciones(TotalDeducciones + TotalRentasExcentas + MenosRenta, Subtotal1, UVTValue, SettingsCompany.WorkIncomeControl, EmployeeAccumulatedExempIncome.AccumulatedMaxDeductionsAndRentExents, Subtotal4)

            Dim BaseGravable = Subtotal1 - MaxDeductionsAndRentExents


            Dim ValueInitialRetentionFraction As Decimal = 0
            Dim ValuePercentageRetentionFraction As Decimal = 0

            Dim Retention383 As Double = Me.CalculateRetention383(BaseGravable, UVTValue, IdRetentionConcepts, List383Concept, ValueInitialRetentionFraction, ValuePercentageRetentionFraction)

            If FractionRetencion And PercentageFractionRetention = 0 Then
                Dim TmpValue = ((BaseGravable / UVTValue) - ValueInitialRetentionFraction) * (ValuePercentageRetentionFraction / 100)
                PercentageFractionRetention = TmpValue / (BaseGravable / UVTValue)
            End If

            If FractionRetencion And PercentageFractionRetention > 0 Then
                Retention383 = CDec(Utils.RoundValue(Convert.ToDecimal(BaseGravable * PercentageFractionRetention), Utils.RoundLevel.Thousands))
            End If


            Retenciones = Retention383

            ListRetentionResult.Add(New Tuple(Of String, Double)("Retention383", Retention383))
            ListRetentionResult.Add(New Tuple(Of String, Double)("IBCRTF", IBCRTF))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalIngresosNoConstitutivos", TotalIngresosNoConstituvos))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal1", Subtotal1))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalDeducciones", TotalDeducciones))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal2", SubTotal2))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalRentasExcentas", TotalRentasExcentas))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal3", SubTotal3))
            ListRetentionResult.Add(New Tuple(Of String, Double)("MenosRenta", MenosRenta))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal4", Subtotal4))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalRentasExentasyDeducciones", MaxDeductionsAndRentExents))
            ListRetentionResult.Add(New Tuple(Of String, Double)("BaseGrabable", BaseGravable))
            ListRetentionResult.Add(New Tuple(Of String, Double)("Retenciones", Retenciones))
            ListRetentionResult.Add(New Tuple(Of String, Double)("AccumulatedExemptIncomeControl", CDbl(EmployeeAccumulatedExempIncome.AccumulatedValue)))
            ListRetentionResult.Add(New Tuple(Of String, Double)("AccumulatedMaxDeductionsControl", CDbl(EmployeeAccumulatedExempIncome.AccumulatedMaxDeductionsAndRentExents)))


        Else


            ' Calculo Porcentaje Fijo de Retención
            'Si no existe el Porcentaje Fijo, lo creo

            Dim InitialDateTmp As New Date()
            Dim EndDateTmp As New Date()

            Dim InitialDateSearchTmp As New Date()
            Dim EndDateSearchTmp As New Date()
            Dim Period As Byte

            If Month(PayrollEndDate) >= 7 And Month(PayrollEndDate) <= 12 Then
                'Se busca entre Junio del Año Anterior y Mayo del Presente
                InitialDateTmp = New Date(Year(PayrollEndDate) - 1, 6, 1)
                EndDateTmp = New Date(Year(PayrollEndDate), 5, 31)
                InitialDateSearchTmp = New Date(Year(PayrollEndDate), 7, 1)
                EndDateSearchTmp = New Date(Year(PayrollEndDate), 12, 31)
                Period = 2
            Else
                'Se busca entre Diciembre de Hace dos Años y Noviembre del Año Anterior
                InitialDateTmp = New Date(Year(PayrollEndDate) - 2, 12, 1)
                EndDateTmp = New Date(Year(PayrollEndDate) - 1, 11, 30)
                InitialDateSearchTmp = New Date(Year(PayrollEndDate), 1, 1)
                EndDateSearchTmp = New Date(Year(PayrollEndDate), 6, 30)
                Period = 1
            End If

            'Si el Empleado ingresó Después de la Fecha Inicio de Corte
            If Contract.JobBondingDate > InitialDateTmp Then
                InitialDateTmp = Contract.JobBondingDate
            End If

            Dim ObjFixedPercentageRetention As FixedPercentageRetention

            'Consulto haber si tiene el Porcentaje creado ya para este periodo
            ObjFixedPercentageRetention = _FixedPercentageRepository.GetFixedPercentageRetentionByEmployeeDate(Employee.Id, InitialDateSearchTmp, EndDateSearchTmp)

            If ObjFixedPercentageRetention Is Nothing Then
                'Si no es así, creamos uno
                ObjFixedPercentageRetention = FixedPercentageRetention(PayrollEndDate, Contract, Employee, UVTValue, ExceptRTF, Period, InitialDateTmp, EndDateTmp, InitialDateSearchTmp, EndDateSearchTmp)
            End If


            'ObligatoryHealthEmployee = ObligatoryPensionEmployee

            Dim TotalIngresosNoConstituvos = ObligatoryPensionEmployee + PensionalSolidarityFund + HealthForINCRNGO

            Dim Subtotal1 = IBCRTF - TotalIngresosNoConstituvos

            'Deducciones

            Dim EmployeePrepaidNotDiscount = Employee.HealthContributorRTF

            If EmployeePrepaidNotDiscount Is Nothing Then
                EmployeePrepaidNotDiscount = 0
            End If

            Dim MedicinaPrepagada = Me.CalculatePrepaidVolunatyrHealthEmployee(PrepaidVolunatyrHealthEmployee + EmployeePrepaidNotDiscount, UVTValue)

            Dim InteresesVivienda = Me.CalculateHousingDeducted(HousingDeductedValue, UVTValue)
            Dim PagoDependientes = CalculateDependentsDeduction(Dependientes, UVTValue, IBCRTF, 1)

            ' Pensión Complementaria - Se aplica como deducción solo si no es la primera quincena
            Dim PensionComplementaria As Double = 0
            If QuarterFlag <> 1 Then
                PensionComplementaria = Employee.SupplementaryPension
            End If

            Dim TotalDeducciones = InteresesVivienda + PagoDependientes + MedicinaPrepagada + PensionComplementaria

            Dim SubTotal2 = Subtotal1 - TotalDeducciones

            ' Rentas Excentas
            Dim TotalRentasExcentas = 0

            If (VoluntaryPensionEmployee + CuentasAFC) > 0 Then
                TotalRentasExcentas = CalculateMaximunTotalExcentsRents(VoluntaryPensionEmployee + CuentasAFC, UVTValue, IBCRTF, VoluntaryPensionEmployee + CuentasAFC + ObligatoryPensionEmployee + PensionalSolidarityFund)
            End If


            Dim SubTotal3 = SubTotal2 - TotalRentasExcentas

            Dim MenosRenta As Double = Utils.RoundedValuesNearestHundred(Me.CalculateLessRentsExentsPopup(SubTotal3, UVTValue, SettingsCompany.WorkIncomeControl, EmployeeAccumulatedExempIncome.AccumulatedValue, ExceptRTF))

            Dim Subtotal4 = SubTotal3 - MenosRenta

            Dim MaxDeductionsAndRentExents = ValidarBaseRetenciones(TotalDeducciones + TotalRentasExcentas + MenosRenta, Subtotal1, UVTValue, SettingsCompany.WorkIncomeControl, EmployeeAccumulatedExempIncome.AccumulatedMaxDeductionsAndRentExents, Subtotal4)

            Dim BaseGravable = Subtotal1 - MaxDeductionsAndRentExents

            Retenciones = CDec(Utils.RoundValue(Convert.ToDecimal(BaseGravable * (ObjFixedPercentageRetention.PercentageRetention / 100)), Utils.RoundLevel.Thousands))

            Dim Retention383 = Retenciones


            ListRetentionResult.Add(New Tuple(Of String, Double)("Retention383", Retention383))
            ListRetentionResult.Add(New Tuple(Of String, Double)("IBCRTF", IBCRTF))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalIngresosNoConstitutivos", TotalIngresosNoConstituvos))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal1", Subtotal1))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalDeducciones", TotalDeducciones))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal2", SubTotal2))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalRentasExcentas", TotalRentasExcentas))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal3", SubTotal3))
            ListRetentionResult.Add(New Tuple(Of String, Double)("MenosRenta", MenosRenta))
            ListRetentionResult.Add(New Tuple(Of String, Double)("SubTotal4", Subtotal4))
            ListRetentionResult.Add(New Tuple(Of String, Double)("TotalRentasExentasyDeducciones", MaxDeductionsAndRentExents))
            ListRetentionResult.Add(New Tuple(Of String, Double)("BaseGrabable", BaseGravable))
            ListRetentionResult.Add(New Tuple(Of String, Double)("Retenciones", Retenciones))
            ListRetentionResult.Add(New Tuple(Of String, Double)("PorcentajeRetencion", ObjFixedPercentageRetention.PercentageRetention))
            ListRetentionResult.Add(New Tuple(Of String, Double)("AccumulatedExemptIncomeControl", CDbl(EmployeeAccumulatedExempIncome.AccumulatedValue)))
            ListRetentionResult.Add(New Tuple(Of String, Double)("AccumulatedMaxDeductionsControl", CDbl(EmployeeAccumulatedExempIncome.AccumulatedMaxDeductionsAndRentExents)))


            If Employee.DeclarantType = 2 Then
                'Si es declarante, se validan los dos valores y se escoge el mayor
                If Retention383 > 0 Then
                    MessageRetention = 0
                    Retenciones = Retention383
                Else
                    MessageRetention = 1
                    Retenciones = 0
                End If
            Else
                'Si NO es declarante, únicamente se valida el Artículo 383
                MessageRetention = 0
                Retenciones = Retention383
            End If


        End If

        Return ListRetentionResult
        'Return Retenciones

    End Function

    ''' <summary>
    ''' Metodo que calcula menos renta exenta -25% del subtotal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateLessRentsExentsPopup(subTotal As Decimal, UVT As Integer, IncomeControl As Boolean, AccumulatedExemptIncomeValue As Decimal, ExceptRTF As Decimal) As Decimal
        Dim valSubTotal As Decimal = subTotal * ExceptRTF / 100

        If AccumulatedExemptIncomeValue = vbEmpty Then
            AccumulatedExemptIncomeValue = 0
        End If

        If IncomeControl Then
            ' Control mensual: límite mensual para el mes actual, límite anual para el acumulado
            Dim monthlyLimit As Decimal = Math.Round(CDec((790 / 12) * UVT), 3)
            Dim annualLimit As Decimal = CDec(790) * CDec(UVT)

            If AccumulatedExemptIncomeValue >= annualLimit Then Return 0

            If valSubTotal > monthlyLimit Then valSubTotal = monthlyLimit

            Dim remainingCapacity As Decimal = annualLimit - AccumulatedExemptIncomeValue
            If valSubTotal > remainingCapacity Then valSubTotal = remainingCapacity

            Return valSubTotal
        Else
            ' Control anual: compara acumulado contra límite anual
            Dim annualLimit As Decimal = CDec(790) * CDec(UVT)

            If AccumulatedExemptIncomeValue >= annualLimit Then Return 0

            Dim remainingCapacity As Decimal = annualLimit - AccumulatedExemptIncomeValue
            If valSubTotal > remainingCapacity Then valSubTotal = remainingCapacity
            If valSubTotal > annualLimit Then Return annualLimit

            Return valSubTotal
        End If
    End Function

    ''' <summary>
    ''' Calcula el valor del uvt para la retencion 383
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateRetention383(taxBase As Decimal, UVT As Decimal, IdRetentionConcepts As Integer, ListRange383 As List(Of Domain.Payroll.Entities.RetentionConceptRanges), Optional ByRef ValueInitial As Decimal = 0, Optional ByRef Percentage As Decimal = 0) As Decimal

        If ListRange383 Is Nothing Then
            ListRange383 = _retentionRepository.GetListRetentionRangeByRetentionConceptId(IdRetentionConcepts)
        End If

        Dim UVTTaxBase As Decimal = taxBase / UVT
        Dim ValueReturn As Decimal = 0
        If ListRange383 IsNot Nothing AndAlso ListRange383.Count > 0 Then
            Dim retentionConceptRange As Domain.Payroll.Entities.RetentionConceptRanges = (From item In ListRange383 Where UVTTaxBase >= item.ValueInitial AndAlso UVTTaxBase < item.ValueFinish Select item).FirstOrDefault
            If retentionConceptRange IsNot Nothing Then
                'Art. 383 ET resta el umbral redondo del rango (95, 150, 360...), no el valor desplazado.
                Dim RangeStartValue As Decimal = Math.Floor(retentionConceptRange.ValueInitial)
                ValueReturn = ((UVTTaxBase - RangeStartValue) * (retentionConceptRange.Percentage / 100.0)) + retentionConceptRange.UVTIncrement
                ValueReturn = CDec(Utils.RoundValue(ValueReturn * UVT, Utils.RoundLevel.Thousands))
                'Para Retención por Fracción
                ValueInitial = RangeStartValue
                Percentage = retentionConceptRange.Percentage

            End If
        End If
        Return ValueReturn
    End Function


    ''' <summary>
    ''' Calcula el valor del uvt para la retencion 384
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateRetention384(FeeCommissionService As Decimal, PaymentHealthObligatory As Decimal, RiskWork As Decimal, RequiredContributions As Decimal, SolidarityPension As Decimal, UVT As Decimal) As Decimal

        'Dim ListRangeRetention384 = _retentionRepository.GetListRetentionRangeByRetentionConceptId(3)
        'Dim ListRangeRetention384 = _retentionRepository.GetListRetentionRangeByRetentionConceptId(77)
        Dim ListRangeRetention384 = _retentionRepository.GetListRetentionRangeRetentionConceptIdByNumber("384")

        Dim TaxBase As Decimal = FeeCommissionService - PaymentHealthObligatory - RiskWork - RequiredContributions - SolidarityPension
        Dim UVTTaxBase As Decimal = TaxBase / UVT
        Dim valueFinally As Decimal = 0
        Dim RangeUVT As Decimal = 0
        If ListRangeRetention384 IsNot Nothing AndAlso ListRangeRetention384.Count > 0 Then
            Dim retentionConceptRange As Domain.Payroll.Entities.RetentionConceptRanges = (From item In ListRangeRetention384 Where UVTTaxBase >= item.ValueInitial AndAlso UVTTaxBase < item.ValueFinish Select item).FirstOrDefault()
            If retentionConceptRange IsNot Nothing Then
                If retentionConceptRange.ValueDeducted > 0 Then
                    RangeUVT = (UVTTaxBase * (retentionConceptRange.Percentage / 100)) - retentionConceptRange.ValueDeducted
                Else
                    RangeUVT = retentionConceptRange.Percentage
                End If
                valueFinally = CDec(Utils.RoundValue(RangeUVT * UVT, Utils.RoundLevel.Thousands))
            End If
        End If
        Return valueFinally
    End Function

    ''' <summary>
    ''' Calcula aportes obligatorios a fondos de pensiones y
    ''' fondo de solidaridad pensional
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CalculateRequiredContributions(ByVal Value As Decimal, ByVal LegalMinimunSalary As Decimal, ByVal Percentage As Decimal) As Decimal

        Dim valueMinimumWage As Decimal = 25 * LegalMinimunSalary * (Percentage / 100)

        If Value > valueMinimumWage Then
            Return valueMinimumWage
        Else
            Return Value
        End If

    End Function

    ''' <summary>
    ''' Calcula el Valor del Total de las Rentas validado
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CalculateMaximunTotalExcentsRents(ByVal Value As Decimal, UVTValue As Decimal, IBCRtf As Decimal, TopeValidacion As Decimal) As Decimal

        Dim TopeMaximo = UVTValue * (3800 / 12)
        Dim TotalManejado = IIf(TopeValidacion < (IBCRtf * (30 / 100)), Value, IBCRtf * (30 / 100))

        If TopeMaximo > TotalManejado Then
            Return TotalManejado
        Else
            Return TopeMaximo
        End If

    End Function

    ''' <summary>
    ''' Calcular el Aporte de Salud Obligatoria
    ''' </summary>
    ''' <param name="Value">Valor del Aporte</param>
    ''' <param name="IBCRtf">IBC Retefuente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateMaximunHealthEmployeeContribution(ByVal Value As Decimal, IBCRtf As Decimal) As Decimal

        Dim TopeMaximo = IBCRtf * 0.4 * (12.5 / 100)

        If TopeMaximo > Value Then
            Return Value
        Else
            Return TopeMaximo
        End If

    End Function

    Public Function CalculatePrepaidVolunatyrHealthEmployee(ByVal Value As Decimal, UVTValue As Decimal) As Decimal

        Dim TopeMaximo = 16 * UVTValue

        If TopeMaximo > Value Then
            Return Value
        Else
            Return TopeMaximo
        End If

    End Function

    Public Function CalculateHousingDeducted(ByVal Value As Decimal, UVTValue As Decimal) As Decimal

        Dim TopeMaximo = 100 * UVTValue

        If TopeMaximo > Value Then
            Return Value
        Else
            Return TopeMaximo
        End If

    End Function

    Public Function CalculateDependentsDeduction(ByVal Value As Decimal, UVTValue As Decimal, IBCRtf As Decimal, VarOption As Integer) As Decimal


        If VarOption = 1 Then

            Dim TopeMaximo = 32 * UVTValue
            Dim TopeMinimo = 0.1 * IBCRtf

            Dim ReturnTopeMinimo As Decimal
            Dim ReturnTopeMaximo As Decimal

            If TopeMaximo > Value Then
                ReturnTopeMaximo = Value
            Else
                ReturnTopeMaximo = TopeMaximo
            End If

            If TopeMinimo > Value Then
                ReturnTopeMinimo = Value
            Else
                ReturnTopeMinimo = TopeMinimo
            End If

            If ReturnTopeMinimo > ReturnTopeMaximo Then
                Return ReturnTopeMaximo
            Else
                Return ReturnTopeMinimo
            End If
        Else

            ' VarOption=2 se usa únicamente en FixedPercentageRetention() para el promedio anual
            ' del % fijo (Procedimiento 2): "Value" ya es la suma de 12 meses de deducción por
            ' dependientes, así que el tope de 32 UVT debe escalarse por esos 12 meses
            Dim TopeMaximo = 32 * UVTValue * 12

            If TopeMaximo > Value Then
                Return Value
            Else
                Return TopeMaximo
            End If

        End If

    End Function

    Public Function FixedPercentageRetention(ByVal PayrollDate As Date, Contract As Domain.Payroll.Entities.Contract, Employee As Domain.Payroll.Entities.Employee, UVTValue As Decimal, ExceptRTF As Decimal, Period As Byte, InitialDateTmp As Date, EndDateTmp As Date, InitialDateSearchTmp As Date, EndDateSearchTmp As Date) As FixedPercentageRetention

        Dim ObjFixedPercentageRetention As New FixedPercentageRetention
        Dim VarIBCRft As Double = 0
        Dim VarDeduccionXDependientes As Double = 0
        Dim VarInteresesVivienda As Double = 0
        Dim VarPrepaidHealth As Double = 0
        Dim VarObligatoryHealth As Double = 0
        Dim VarObligatoryPension As Double = 0
        Dim VarVoluntaryPension As Double = 0
        Dim VarAFCAccount As Double = 0
        Dim VarRepresentationCost As Double = 0
        Dim VarPensionSolidarityFund As Double = 0
        Dim MonthDiv As Integer = 13

        Dim ListLiquidationRete = _liquitationRepository.GetConfirmLiquidationByStarEndDateRetefuente(InitialDateTmp, EndDateTmp, Employee.Id)

        If ListLiquidationRete IsNot Nothing AndAlso ListLiquidationRete.Count > 0 Then
            For Each ObjListLiquidationRete As Liquidation In ListLiquidationRete

                'Retención Salarios
                VarIBCRft = VarIBCRft + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.AffectIBCRTF = True And x.Concept.ConceptType = 1).Sum(Function(x) x.ConceptTotalValue)

                'Salud Prepagada
                VarPrepaidHealth = VarPrepaidHealth + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.ConceptClass = "019").Sum(Function(x) x.ConceptTotalValue)

                'Salud Obligatoria
                VarObligatoryHealth = VarObligatoryHealth + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.ConceptClass = "017").Sum(Function(x) x.ConceptTotalValue)

                'Pensión Obligatoria
                VarObligatoryPension = VarObligatoryPension + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.ConceptClass = "014").Sum(Function(x) x.ConceptTotalValue)

                'Pension Voluntaria
                VarVoluntaryPension = VarVoluntaryPension + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.ConceptClass = "016").Sum(Function(x) x.ConceptTotalValue)

                'Cuentas AFC
                VarAFCAccount = VarAFCAccount + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.ConceptClass = "045").Sum(Function(x) x.ConceptTotalValue)

                'Gastos de Representación
                VarRepresentationCost = VarRepresentationCost + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.ConceptClass = "047").Sum(Function(x) x.ConceptTotalValue)

                'Fondo de Solidaridad Pensional
                VarPensionSolidarityFund = VarPensionSolidarityFund + ObjListLiquidationRete.LiquidationDetail.Where(Function(x) x.Concept.ConceptClass = "038").Sum(Function(x) x.ConceptTotalValue)

            Next


            'Deducción por Dependientes
            VarDeduccionXDependientes = ListLiquidationRete.Sum(Function(x) x.DependentsDeduction)

            'Vivienda
            VarInteresesVivienda = ListLiquidationRete.Sum(Function(x) x.HousingDeductionValue)
        End If

        If Month(PayrollDate) <> 7 Or Month(PayrollDate) <> 1 Then
            'No se debe calcular el nuevo porcentaje y el empleado ingresó después de la Fecha de Corte
            If Contract.JobBondingDate > InitialDateSearchTmp Then

            End If


        End If


        'Cargo los Datos de los Retroactivos y de los Incentivos (Primas) de la ventana de 12 meses.
        'Se filtra por InitialDateTmp-EndDateTmp y no por año calendario: recorrer los años completos
        'metía pagos posteriores al corte (p.ej. la prima de junio en una ventana que cierra en mayo).
        Dim ListRetroactiveC = _retroactiveRepository.GetListRetroactiveByEmployeeIdBetweenDates(Employee.Id, InitialDateTmp, EndDateTmp)

        If ListRetroactiveC IsNot Nothing Then

            For Each ObjRetroactiveC As RetroactiveC In ListRetroactiveC

                VarIBCRft = VarIBCRft + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.AffectIBCRTF = True).Sum(Function(x) x.ValueConceptWithRetroactive)

                'Salud Prepagada
                VarPrepaidHealth = VarPrepaidHealth + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.ConceptClass = "019").Sum(Function(x) x.ValueConceptWithRetroactive)

                'Salud Obligatoria
                VarObligatoryHealth = VarObligatoryHealth + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.ConceptClass = "017").Sum(Function(x) x.ValueConceptWithRetroactive)

                'Pensión Obligatoria
                VarObligatoryPension = VarObligatoryPension + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.ConceptClass = "014").Sum(Function(x) x.ValueConceptWithRetroactive)

                'Pension Voluntaria
                VarVoluntaryPension = VarVoluntaryPension + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.ConceptClass = "016").Sum(Function(x) x.ValueConceptWithRetroactive)

                'Cuentas AFC
                VarAFCAccount = VarAFCAccount + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.ConceptClass = "045").Sum(Function(x) x.ValueConceptWithRetroactive)

                'Gastos de Representación
                VarRepresentationCost = VarRepresentationCost + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.ConceptClass = "047").Sum(Function(x) x.ValueConceptWithRetroactive)

                'Fondo de Solidaridad Pensional
                VarPensionSolidarityFund = VarPensionSolidarityFund + ObjRetroactiveC.RetroactiveD.Where(Function(x) x.Concept.ConceptClass = "038").Sum(Function(x) x.ValueConceptWithRetroactive)

            Next

        End If

        Dim ListIncentivePayment = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdRetefuenteBetweenDates(Employee.Id, InitialDateTmp, EndDateTmp)

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            VarIBCRft = VarIBCRft + ListIncentivePayment.Sum(Function(x) x.TotalAccrued)
        End If

        Dim Subtotal1 = VarIBCRft

        Dim Subtotal2 = Subtotal1

        'Deducciones
        VarDeduccionXDependientes = Me.CalculateDependentsDeduction(VarDeduccionXDependientes, UVTValue, 0, 2)

        VarInteresesVivienda = Me.CalculateHousingDeducted(VarInteresesVivienda, UVTValue)

        VarPrepaidHealth = Me.CalculatePrepaidVolunatyrHealthEmployee(VarPrepaidHealth, UVTValue)

        Dim TotalDeductions = VarDeduccionXDependientes + VarInteresesVivienda + VarPrepaidHealth + VarObligatoryHealth

        Dim Subtotal4 = Subtotal2 - TotalDeductions

        'RENTAS EXCENTAS
        Dim RentasExcentas = VarObligatoryPension + VarPensionSolidarityFund + VarVoluntaryPension + VarAFCAccount + VarRepresentationCost

        Dim SubTotal3 = Subtotal4 - RentasExcentas

        Dim RentaTrabajoExenta = SubTotal3 * (ExceptRTF / 100)

        ' Tope legal de 790 UVT anuales para la renta exenta del 25% (Art. 206 num. 10 ET).
        ' Este cálculo es siempre un agregado anual de los 12 meses que se promedian para el
        ' % fijo
        Dim TopeRentaExentaAnual As Double = 790 * UVTValue
        If RentaTrabajoExenta > TopeRentaExentaAnual Then
            RentaTrabajoExenta = TopeRentaExentaAnual
        End If

        Dim SubTotal5 = SubTotal3 - RentaTrabajoExenta

        Dim SubTotal6 = SubTotal5 / MonthDiv

        Dim IngresoEnUVT = SubTotal6 / UVTValue

        Dim PercentageRetention = CalculatePercentageRetention(IngresoEnUVT)

        ObjFixedPercentageRetention.EmployeeId = Employee.Id
        ObjFixedPercentageRetention.Period = Period
        ObjFixedPercentageRetention.YearCalculation = Year(PayrollDate)
        ObjFixedPercentageRetention.ValidStartDate = InitialDateSearchTmp
        ObjFixedPercentageRetention.ValidEndDate = EndDateSearchTmp
        ObjFixedPercentageRetention.TotalIncome = VarIBCRft
        ObjFixedPercentageRetention.TotalDeduction = TotalDeductions
        ObjFixedPercentageRetention.TotalIncomeExempt = RentasExcentas
        ObjFixedPercentageRetention.ExemptIncome25 = RentaTrabajoExenta
        ObjFixedPercentageRetention.Subtotal = VarIBCRft - TotalDeductions - RentasExcentas - RentaTrabajoExenta
        ObjFixedPercentageRetention.CalculationMonth = MonthDiv
        ObjFixedPercentageRetention.RetentionBase = SubTotal6
        ObjFixedPercentageRetention.UVTValue = UVTValue
        ObjFixedPercentageRetention.PercentageRetention = PercentageRetention

        Return ObjFixedPercentageRetention

    End Function

    ''' <summary>
    ''' Función para Crear Mensajes
    ''' </summary>
    ''' <param name="Description">Descripción del Mensaje</param>
    ''' <param name="ErrorType">Tipo de Error: True: Grave, False: Informativos</param>
    ''' <param name="payrollDate">Fecha de Liquidación</param>
    ''' <returns>Mensaje</returns>
    ''' <remarks></remarks>
    Public Function CreateMessage(ByVal Description As String, ByVal ErrorType As Boolean, payrollDate As Date) As Message Implements ILiquidationDomain.CreateMessage

        Dim MessageLiquidation As New Message

        MessageLiquidation.Description = Description
        MessageLiquidation.Error = ErrorType
        MessageLiquidation.PayrollDate = payrollDate

        Return MessageLiquidation

    End Function

    ''' <summary>
    ''' Calcula el valor del uvt para la retencion 384
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculatePercentageRetention(ByVal UVTIngresoLaboral As Decimal) As Decimal

        Dim Retention As Decimal = 0
        Dim Percentage As Decimal = 0

        If UVTIngresoLaboral > 0 Then

            UVTIngresoLaboral = Math.Round(UVTIngresoLaboral, 2)

            If UVTIngresoLaboral >= 0 And UVTIngresoLaboral <= 95 Then
                Retention = 0
            End If

            If UVTIngresoLaboral > 95 And UVTIngresoLaboral <= 150 Then
                Retention = (UVTIngresoLaboral - 95) * (19 / 100)
            End If

            If UVTIngresoLaboral > 150 And UVTIngresoLaboral <= 360 Then
                Retention = (UVTIngresoLaboral - 150) * (28 / 100) + 10
            End If

            If UVTIngresoLaboral > 360 And UVTIngresoLaboral <= 640 Then
                Retention = (UVTIngresoLaboral - 360) * (33 / 100) + 69
            End If

            If UVTIngresoLaboral > 640 And UVTIngresoLaboral <= 945 Then
                Retention = (UVTIngresoLaboral - 640) * (35 / 100) + 162
            End If

            If UVTIngresoLaboral > 945 And UVTIngresoLaboral <= 2300 Then
                Retention = (UVTIngresoLaboral - 945) * (37 / 100) + 268
            End If

            If UVTIngresoLaboral > 2300 Then
                Retention = (UVTIngresoLaboral - 2300) * (39 / 100) + 770
            End If

            Percentage = Retention / UVTIngresoLaboral
        End If

        Return Percentage * 100

    End Function


    Public Function ValidarBaseRetenciones(ByVal Value As Decimal, ByVal Subtotal1 As Decimal, UVTValue As Decimal, IncomeControl As Boolean, AccumulatedTotalValue As Decimal, SubTotal4 As Decimal) As Decimal

        Dim ValueReturn As Decimal
        'Primer tope el 40% del ingreso neto
        Dim SubTotalLimit As Decimal = Subtotal1 * 0.4

        'Segundo tope Hasta 1340 UVT
        Dim UvtLimitMax As Decimal = CDec(1340 * UVTValue)
        Dim tmpAccumulatedTotalValue As Decimal = AccumulatedTotalValue + SubTotalLimit

        'Se valida si el tope es mensual o anual
        Dim UvtLimit As Decimal = If(IncomeControl, Math.Round((1340D / 12) * UVTValue, 3), 1340D * UVTValue)

        If AccumulatedTotalValue = vbEmpty Then
            AccumulatedTotalValue = 0
        End If

        ' Si el total acumulado supera el tope anual, retorno 0
        If AccumulatedTotalValue >= UvtLimitMax Then Return 0

        ' Ajustar el límite si la suma acumulada excede el tope anual
        If IncomeControl = False Then
            If tmpAccumulatedTotalValue > UvtLimitMax Then
                SubTotalLimit = UvtLimitMax - AccumulatedTotalValue
            End If
        Else
            SubTotalLimit = UvtLimitMax - AccumulatedTotalValue
        End If

        'Si el total de rentas exentas y deducciones es mayor al 40% de los ingresos netos, entonces el valor límite es igual al 40% de los ingresos netos y calculo la diferencia entre los ingresos netos y el valor límite
        If Value > SubTotalLimit Then
            ValueReturn = SubTotalLimit
        Else
            ''Se devuelve el valor minimoo entre el valor, el subtotal y el limite del uvt
            ValueReturn = Math.Min(Math.Min(SubTotalLimit, Value), UvtLimit)
        End If

        Return ValueReturn

    End Function


    Public Function VacationDays(PayrollStarDate As Date, PayrollEndingDate As Date, VacationInitialDate As Date, VacationEndDate As Date, ObjContract As Domain.Payroll.Entities.Contract, NumberContract As Integer, groupEmployee As Group) As Integer

        Dim ReturnVacationDays As Integer = 0

        If NumberContract = 1 Then
            '1 contrato al mes

            'Para Vacaciones que están dentro del mismo mes
            If PayrollStarDate >= VacationInitialDate And PayrollEndingDate <= VacationEndDate Then
                ReturnVacationDays = DateDiff(DateInterval.Day, VacationInitialDate, VacationEndDate) + 1
            End If

            'Para Vacaciones que iniciaron un mes antes, y finalizan el mes de la Nómina
            If PayrollStarDate > VacationInitialDate And PayrollEndingDate <= VacationEndDate Then
                ReturnVacationDays = DateDiff(DateInterval.Day, PayrollStarDate, VacationEndDate) + 1
            End If

            'Para Vacaciones que se iniciaron el mes de la Nómina, y finaliza el siguiente
            If PayrollStarDate <= VacationInitialDate And VacationEndDate > PayrollEndingDate Then
                If groupEmployee.Month = 1 Then 'Nómina de 30 días
                    ReturnVacationDays = Days360(VacationInitialDate, PayrollEndingDate)
                Else
                    ReturnVacationDays = DateDiff(DateInterval.Day, VacationInitialDate, PayrollEndingDate) + 1
                End If
            End If

            'Para Vacaciones que Iniciaron antes de la Nómina y Finalizan después de la Nómina
            If VacationInitialDate < PayrollStarDate And VacationEndDate > PayrollEndingDate Then
                ReturnVacationDays = 30
            End If

        Else
            '2 contrato al mes

            Dim ContractInitialDate = ObjContract.ContractInitialDate
            Dim ContractEndingDate = ObjContract.ContractEndingDate

            'Para Vacaciones que están dentro del mismo mes
            If PayrollStarDate >= VacationInitialDate And PayrollEndingDate <= VacationEndDate Then
                '1er Contrato
                If ContractEndingDate < PayrollEndingDate Then
                    If VacationEndDate <= ContractEndingDate Then
                        ReturnVacationDays = DateDiff(DateInterval.Day, VacationInitialDate, VacationEndDate) + 1
                    Else
                        ReturnVacationDays = DateDiff(DateInterval.Day, VacationInitialDate, ContractEndingDate) + 1
                    End If

                Else
                    '2do Contrato
                    If VacationInitialDate >= ContractInitialDate Then
                        ReturnVacationDays = DateDiff(DateInterval.Day, VacationInitialDate, VacationEndDate) + 1
                    End If

                    If VacationInitialDate < ContractInitialDate And VacationEndDate > ContractInitialDate Then
                        ReturnVacationDays = DateDiff(DateInterval.Day, ContractInitialDate, VacationEndDate) + 1
                    End If
                End If

            End If

            'Para Vacaciones que iniciaron un mes antes, y finalizan el mes de la Nómina
            If VacationInitialDate < PayrollStarDate And VacationEndDate > PayrollEndingDate Then
                ''PRIMER CONTRATO
                If ContractEndingDate < PayrollEndingDate Then

                    If ContractEndingDate <= VacationEndDate Then
                        ReturnVacationDays = DateDiff(DateInterval.Day, PayrollStarDate, ContractEndingDate) + 1
                    Else
                        ReturnVacationDays = DateDiff(DateInterval.Day, PayrollStarDate, VacationEndDate) + 1
                    End If

                Else

                    '' SEGUNDO CONTRATO
                    If VacationEndDate < ContractInitialDate Then
                        ReturnVacationDays = 0
                    Else
                        ReturnVacationDays = DateDiff(DateInterval.Day, ContractInitialDate, VacationEndDate) + 1

                    End If
                End If
            End If

            'Para Vacaciones que se iniciaron el mes de la Nómina, y finaliza el siguiente
            If PayrollStarDate <= VacationInitialDate And VacationEndDate > PayrollEndingDate Then
                ''PRIMER CONTRATO
                If ContractEndingDate < PayrollEndingDate Then

                    If ContractEndingDate >= VacationInitialDate Then
                        ReturnVacationDays = DateDiff(DateInterval.Day, VacationInitialDate, ContractEndingDate) + 1
                    Else
                        ReturnVacationDays = 0
                    End If
                Else

                    ''SEGUNDO CONTRATO

                    If ContractInitialDate <= VacationInitialDate Then
                        If groupEmployee.Month = 1 Then 'Nómina de 30 días
                            ReturnVacationDays = Days360(VacationInitialDate, PayrollEndingDate)
                        Else
                            ReturnVacationDays = DateDiff(DateInterval.Day, VacationInitialDate, PayrollEndingDate) + 1
                        End If

                    Else
                        If groupEmployee.Month = 1 Then 'Nómina de 30 días
                            ReturnVacationDays = Days360(ContractInitialDate, PayrollEndingDate)
                        Else
                            ReturnVacationDays = DateDiff(DateInterval.Day, ContractInitialDate, PayrollEndingDate) + 1
                        End If

                    End If

                End If
            End If

            'Para Vacaciones que Iniciaron antes de la Nómina y Finalizan después de la Nómina
            If VacationInitialDate < PayrollStarDate And VacationEndDate > PayrollEndingDate Then
                ''PRIMER CONTRATO
                If ContractEndingDate < PayrollEndingDate Then
                    ReturnVacationDays = DateDiff(DateInterval.Day, PayrollStarDate, ContractEndingDate) + 1
                Else
                    '' SEGUNDO CONTRATO
                    If groupEmployee.Month = 1 Then 'Nómina de 30 días
                        ReturnVacationDays = Days360(ContractInitialDate, PayrollEndingDate)
                    Else
                        ReturnVacationDays = DateDiff(DateInterval.Day, ContractInitialDate, PayrollEndingDate) + 1
                    End If

                End If
            End If

        End If

        If ReturnVacationDays > 0 Then
            Return ReturnVacationDays
        Else
            Return 0
        End If
    End Function

    Public Function NewExecuteLiquitadion(payrollEmployee As List(Of Entities.Employee), groupEmployee As Group, completePayroll As Boolean, SessionValues As SessionValues, Optional RetirementDate As Date = Nothing, Optional FlagIncentivePayment As Boolean = False) As ActionMessageResult(Of List(Of Liquidation)) Implements ILiquidationDomain.NewExecuteLiquitadion
        Dim ActionMessageResult As New ActionMessageResult(Of List(Of Liquidation))
        ActionMessageResult.StateResult = True

        Dim PayrollSettings = _PayrollSettings.GetSettingPayroll()

        'Valido si tiene Parametros de Nómina
        If PayrollSettings Is Nothing Then
            ActionMessageResult.MessageResult.Add(New MessageResult("-001: Parámetros de Nómina", "No se encontraron Parámetros de Nómina Definidos"))
            ActionMessageResult.StateResult = False
            Return ActionMessageResult
        End If

        'Valido si tiene Concepto de Retención Definido
        If PayrollSettings.IdRetentionConcepts Is Nothing Then
            ActionMessageResult.MessageResult.Add(New MessageResult("-001: Parámetros de Nómina", "No ha configurado el Concepto de Retención para el pago de dicho concepto. Agréguelo en Parámetros de Nómina"))
            ActionMessageResult.StateResult = False
            Return ActionMessageResult
        End If

        ' Averiguo los conceptos que tiene autorizado el Grupo
        Dim AuthorizationConcept = _autorizationConceptRepository.GetAuthorizationConceptByGroupId(groupEmployee.Id)

        Dim ListRange383 = _retentionRepository.GetListRetentionRangeByRetentionConceptId(PayrollSettings.IdRetentionConcepts)

        If AuthorizationConcept.Count <= 0 Then
            ActionMessageResult.MessageResult.Add(New MessageResult("-002: Autorización de Conceptos", "No ha autorizado ningún concepto de Nómina para este grupo"))
            ActionMessageResult.StateResult = False
            Return ActionMessageResult
        End If

        'Cargo por defecto Impuesto del CREE
        Dim CreeTax As Boolean = True
        CreeTax = PayrollSettings.CreeTax

        'Cargo configuraciones básicas del grupo
        Dim PayrollStarDate = groupEmployee.NextDateLiquidation
        Dim PayrollLiquidation = groupEmployee.Liquidation

        'Averiguo la Fecha Fin de la Nómina
        Dim PayrollEndDate = Me.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)

        ' Días de Nómina
        Dim PayrollDays = Me.PayrollDays(PayrollStarDate, PayrollEndDate, groupEmployee.Month)

        Dim ListLiquidationLastYear As List(Of Liquidation)

        Dim ErrorCount As Integer = 0
        Dim MessageLiquitadion As Message
        Dim ListLiquidation As New List(Of Liquidation)
        Dim EmployeeLiquidated As New Liquidation
        Dim ValueUVT = groupEmployee.PayrollParameter.UVTValue

        If ValueUVT = 0 Then
            ActionMessageResult.MessageResult.Add(New MessageResult("-999: Error", String.Format("El valor del uvt no puede ser cero, parametrícelo en el formulario de grupos, campo 'Valor UVT', segmento '7. Retención en la Fuente' del grupo {0}", groupEmployee.Code.ToString())))
            ActionMessageResult.StateResult = False
            Return ActionMessageResult
        End If

        'Cargo el listado de Vacaciones que se son de este mes
        Dim ListVacationEmployee = _vacationRepository.GetVacationLiquidationDate(PayrollStarDate, 1)

        Dim QuarterFlag As Byte

        'Cargo algunos datos de los grupos
        If groupEmployee.Liquidation = 1 Then ' Nómina Mensual
            QuarterFlag = 4 ' Bandera para Conceptos Manuales para saber que se paga Mensualmente 
        Else
            If Day(PayrollStarDate) <= 15 Then
                QuarterFlag = 1 ' Se paga la 1era Quincena
            Else
                QuarterFlag = 2 ' Se paga la 2da Quincena
            End If
        End If

        'Cargo el Listado de Nóminas de la quincena anterior. Aplica solo para la 2da quincena, y nómina quincenal

        Dim ListPastLiquidation As New List(Of Liquidation)
        Dim ListIdEmployee As List(Of Integer) = (From b In payrollEmployee Select b.Id).ToList()
        If QuarterFlag = 2 Then
            ListPastLiquidation = _liquitationRepository.ListLiquitadionByGroupAndDateLiquidatedByPayroll(New Date(PayrollStarDate.Year, PayrollStarDate.Month, 15), groupEmployee.Id, ListIdEmployee)
        End If

        'Cargo el listado de Retroactivos
        Dim ListRetroactive = _retroactiveRepository.GetListRetroactiveByYear(Year(PayrollStarDate) - 1, groupEmployee.Id)

        'Cargo los retroactivos de este año, si se pagan por nómina
        Dim PaidRetroactive = _retroactiveRepository.GetListRetroactiveByPayrollDate(PayrollStarDate, groupEmployee.Id)

        'Cargo los Festivos del mes de Nómina
        Dim ListHolidays = _holidayRepository.ListHolidayBetweenDate(PayrollStarDate, PayrollEndDate)

        Dim ICBFThirdParty = _thirdPartyRepository.GetThirdPartyByNit("899999239", False)
        Dim SENAThirdParty = _thirdPartyRepository.GetThirdPartyByNit("899999034", False)


        For Each objEmployeeTmp As Entities.Employee In payrollEmployee

            Try
                EmployeeLiquidated = New Liquidation()

                Dim ListStatusContract = {1, 4, 5}.ToList()
                Dim ContractList As New List(Of Entities.Contract)
                Dim CompareDate = New Date(1901, 1, 1)

                If CompareDate <> RetirementDate Then
                    ContractList = objEmployeeTmp.Contract.Where(Function(x) x.ContractEndingDate >= PayrollStarDate And x.ContractInitialDate <= PayrollEndDate And x.GroupId = groupEmployee.Id And x.LiquidationPayroll = "1" And (x.LastLiquidationDate Is Nothing Or x.LastLiquidationDate < PayrollStarDate) And x.ContractInitialDate <> x.ContractEndingDate And (x.Valid = True Or (x.ContractEndingDate >= RetirementDate And x.ContractInitialDate <= RetirementDate And x.ContractEndingDate < New Date(9999, 12, 31)) Or (x.Status = 4 And x.ContractEndingDate < RetirementDate))).ToList()
                Else
                    ContractList = objEmployeeTmp.Contract.Where(Function(x) x.ContractEndingDate >= PayrollStarDate And x.ContractInitialDate <= PayrollEndDate And x.LiquidationPayroll = "1" And ListStatusContract.Any(Function(y) y = x.Status) And x.ContractInitialDate <> x.ContractEndingDate And x.GroupId = groupEmployee.Id).ToList()
                End If

                If Not ContractList.Any() Then
                    EmployeeLiquidated = Nothing
                    ActionMessageResult.MessageResult.Add(New MessageResult("-999: Error", String.Format("No se encontró un contrato válido para el empleado No. {0}", objEmployeeTmp.Id.ToString())))
                    ActionMessageResult.StateResult = False
                    Continue For
                End If

                Dim DoubleContract As Boolean = False
                Dim BasicSalary As Decimal = 0
                Dim contractEmployee As Domain.Payroll.Entities.Contract
                Dim TransportDays As Integer = 0
                Dim tmpGroupId As Integer = 0

                'Se toma el salario minimo dependiendo de la bandera liquida salario minimo institucional
                Dim minimimunSalary = IIf(groupEmployee.PayrollParameter.LiquidaMinimumInstitutionalSalary, CDbl(groupEmployee.PayrollParameter.InstitutionalMinimumSalary), CDbl(groupEmployee.PayrollParameter.LegalSalaryMinimum))
                Dim transportHelpValue As Double = 0

                If ContractList.Count > 1 Then
                    'Cargo el listado de Vacaciones del periodo
                    'Dim ListPreviousVacation = _vacationRepository.GetVacationBetweenDate(objEmployeeTmp.Id, PayrollStarDate, PayrollEndDate)
                    Dim ListPreviousVacation = _vacationRepository.GetVacationBetweenDateAndActiveContract(objEmployeeTmp.Id, PayrollStarDate, PayrollEndDate)
                    If ListPreviousVacation IsNot Nothing Then
                        Dim ContractListToExclude As New List(Of Domain.Payroll.Entities.Contract)
                        For Each tmpContract As Domain.Payroll.Entities.Contract In ContractList
                            Dim DaysContract = Me.Days360(PayrollStarDate, tmpContract.ContractEndingDate)
                            Dim DaysVacations = 0

                            For Each tmpVacation In ListPreviousVacation.Where(Function(x) x.TypePayment = 2).ToList
                                Dim vacationStart = If(tmpContract.ContractInitialDate > PayrollStarDate, tmpContract.ContractInitialDate, PayrollStarDate)
                                vacationStart = If(tmpVacation.VacationStartDate > vacationStart, tmpVacation.VacationStartDate, vacationStart)
                                Dim vacationEnd = If(PayrollEndDate > tmpContract.ContractEndingDate, tmpContract.ContractEndingDate, PayrollEndDate)
                                vacationEnd = If(tmpVacation.IncorporationDateReal > vacationEnd, vacationEnd, tmpVacation.IncorporationDateReal)
                                vacationEnd = If(vacationStart > vacationEnd, vacationStart, vacationEnd)
                                DaysVacations += Me.Days360(vacationStart, vacationEnd)
                            Next

                            If DaysVacations > DaysContract Then
                                ContractListToExclude.Add(tmpContract)
                            End If
                        Next

                        If ContractListToExclude.Count > 0 Then
                            Dim ContractArrayToExclude = ContractListToExclude.Select(Function(c) c.Id).ToArray()
                            ContractList.RemoveAll(Function(c) ContractArrayToExclude.Contains(c.Id))
                        End If
                    End If
                End If

                If ContractList.Count > 1 Then
                    'El empleado tiene doble contrato en el mes

                    BasicSalary = ContractList.FirstOrDefault.BasicSalary
                    contractEmployee = ContractList.Where(Function(x) x.Valid = True And x.Status = 1).FirstOrDefault()

                    If CompareDate <> RetirementDate Then
                        contractEmployee = ContractList.LastOrDefault()
                    End If

                    If contractEmployee Is Nothing Then
                        contractEmployee = ContractList.LastOrDefault()
                    End If

                    If ContractList.Any(Function(x) x.BasicSalary <> BasicSalary) Then

                        Dim BasicSalaryUno As Decimal
                        Dim BasicSalaryDos As Decimal

                        For Each tmpContract As Domain.Payroll.Entities.Contract In ContractList

                            Dim DaysContract = 0

                            If tmpContract.ContractEndingDate < PayrollEndDate Then
                                '1er Contrato
                                DaysContract = Me.Days360(PayrollStarDate, tmpContract.ContractEndingDate)
                                BasicSalaryUno = (tmpContract.BasicSalary * DaysContract) / 30

                                If groupEmployee.Liquidation = 2 Then
                                    BasicSalaryUno = (tmpContract.BasicSalary * DaysContract) / 15
                                End If

                            Else

                                DaysContract = Me.Days360(tmpContract.ContractInitialDate, PayrollEndDate)

                                BasicSalaryDos = (tmpContract.BasicSalary * DaysContract) / 30

                                If groupEmployee.Liquidation = 2 Then
                                    BasicSalaryDos = (tmpContract.BasicSalary * DaysContract) / 15
                                End If
                            End If

                            If tmpContract.BasicSalary <= (2 * minimimunSalary) Then
                                transportHelpValue = groupEmployee.PayrollParameter.TransportHelpValue
                                TransportDays = TransportDays + DaysContract
                            End If

                        Next

                        BasicSalary = Math.Round(BasicSalaryUno + BasicSalaryDos)
                    Else

                        tmpGroupId = contractEmployee.GroupId

                        If ContractList.Any(Function(x) x.GroupId <> tmpGroupId) Then
                            DoubleContract = True
                        End If

                        If contractEmployee.BasicSalary <= (2 * minimimunSalary) Then
                            transportHelpValue = groupEmployee.PayrollParameter.TransportHelpValue
                        End If
                    End If

                Else
                    contractEmployee = ContractList.FirstOrDefault()
                    BasicSalary = contractEmployee.BasicSalary

                    ''Valido Auxilio de Transporte
                    transportHelpValue = groupEmployee.PayrollParameter.TransportHelpValue
                    If (BasicSalary > (minimimunSalary * 2)) Then
                        transportHelpValue = 0
                    End If

                    Dim tmpContractList = objEmployeeTmp.Contract.Where(Function(x) x.ContractEndingDate >= PayrollStarDate And x.ContractInitialDate <= PayrollEndDate And x.LiquidationPayroll = "1" And (x.LastLiquidationDate Is Nothing Or x.LastLiquidationDate < PayrollStarDate) And ListStatusContract.Any(Function(y) y = x.Status)).ToList()

                    If tmpContractList.Count > 1 Then
                        tmpGroupId = tmpContractList.FirstOrDefault.GroupId

                        If tmpContractList.Any(Function(x) x.GroupId <> tmpGroupId) Then
                            DoubleContract = True
                        End If

                    End If

                End If

                ''Valido Auxilio de Transporte
                If transportHelpValue = 0 Then
                    MessageLiquitadion = CreateMessage("No se paga Auxilio de Transporte puesto que gana más de 2 salarios mínimos", False, PayrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                End If

                Dim ObjEmployee = contractEmployee.Employee
                Dim EmployeePensionContributionPercentage = groupEmployee.PayrollParameter.EmployeePensionContributionPercentage
                Dim EmployeeHealthContributionPercentage = groupEmployee.PayrollParameter.EmployeeHealthContributionPercentage
                Dim EmployerPensionContributionPercentage = groupEmployee.PayrollParameter.EmployerPensionContributionPercentage
                Dim EmployerHealthContributionPercentage = groupEmployee.PayrollParameter.EmployerHealthContributionPercentage
                Dim SenaContributionPercentage = groupEmployee.PayrollParameter.SenaContributionPercentage
                Dim ICBFContributionPercentage = groupEmployee.PayrollParameter.ICBFContributionPercentage
                Dim CompensationFundContributionPercentage = groupEmployee.PayrollParameter.CompensationFundContributionPercentage
                Dim RTFExcempt = groupEmployee.PayrollParameter.RTFExemptPercentage
                Dim MaximunDiscountPercentage = groupEmployee.PayrollParameter.MaximunDiscountPercentage
                Dim EducationStudyDiscountPercentage = groupEmployee.PayrollParameter.EducationHealthDiscountPercentage
                Dim HousingDeductionMaximumValue = groupEmployee.PayrollParameter.HousingDeductionMaximumValue
                Dim HealthContributionMaximunSalary = groupEmployee.PayrollParameter.HealthContributionMaximunSalary
                Dim UVTValue = groupEmployee.PayrollParameter.UVTValue

                Dim Day31 = groupEmployee.PayrollParameter.Day31
                If Day31 Is Nothing Then
                    Day31 = False
                End If

                Dim NitEmployee As String
                Dim NameEmployee As String

                Dim tmpThirdParty = _liquitationRepository.GetThirdParty(ObjEmployee.ThirdPartyId)

                NitEmployee = tmpThirdParty.Nit
                NameEmployee = tmpThirdParty.Name

                If ObjEmployee.WorkCenterId Is Nothing Then
                    ActionMessageResult.MessageResult.Add(New MessageResult("-005: Centros de Trabajo", "El Centro de Trabajo del Empleado " + NitEmployee + " - " + NameEmployee + " está vacío. Favor verificar en el formulario de Talento Humano, Opción Contratos"))
                    ActionMessageResult.StateResult = False
                    Return ActionMessageResult
                End If

                If minimimunSalary <= 0 Or minimimunSalary.ToString() = "" Then
                    ActionMessageResult.MessageResult.Add(New MessageResult("-003: Grupos", "El Salario Mínimo Legal Vigente del Grupo está en Cero (0)"))
                    ActionMessageResult.StateResult = False
                    Return ActionMessageResult
                End If

                If groupEmployee.PayrollParameter.TransportHelpValue = 0 Or groupEmployee.PayrollParameter.TransportHelpValue.ToString() = "" Then
                    ActionMessageResult.MessageResult.Add(New MessageResult("-003: Grupos", "El Auxilio de Transporte del Grupo está en Cero (0)"))
                    ActionMessageResult.StateResult = False
                    Return ActionMessageResult
                End If

                If contractEmployee.ContractEndingDate < PayrollEndDate And DoubleContract = False Then
                    MessageLiquitadion = CreateMessage("A este empleado se le Vence el contrato este mes. Fecha Fin Contrato: " & contractEmployee.ContractEndingDate, False, PayrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)

                    ActionMessageResult.MessageResult.Add(New MessageResult("001: Vencimiento de Contrato", "El Empleado " & NitEmployee & " - " & NameEmployee & " se le vence el contrato el día " & contractEmployee.ContractEndingDate))
                End If

                'Cargo las primas
                Dim incentiePaymentValue As Decimal = 0

                If PayrollStarDate.Month = 6 Or PayrollStarDate.Month = 12 Then
                    Dim ObjIncentivePayment = _incentivePaymentRepository.GetIncentivePaymentByContractIdPayrollNextDate(contractEmployee.Id, PayrollStarDate)

                    If ObjIncentivePayment IsNot Nothing Then
                        incentiePaymentValue = ObjIncentivePayment.TotalAccrued
                    End If

                End If

                'Cargo los Conceptos Autorizados por Empleado
                Dim AutorizationConceptEmployee = _autorizationConceptRepository.GetAuthorizationConceptByEmployeeId(ObjEmployee.Id)
                Dim PayrollAuthorizationConcept = AuthorizationConcept
                If AutorizationConceptEmployee IsNot Nothing AndAlso AutorizationConceptEmployee.Count > 0 Then
                    For Each ObjAutoEmployee As AuthorizationConcept In AutorizationConceptEmployee
                        If AuthorizationConcept.Any(Function(x) x.ConceptId = ObjAutoEmployee.ConceptId) = False Then
                            PayrollAuthorizationConcept.Add(ObjAutoEmployee)
                        End If
                    Next
                End If
                PayrollAuthorizationConcept = PayrollAuthorizationConcept.OrderBy(Function(x) x.Concept.Code).ToList()

                Dim AgreementsList = _IAgreementsRepository.GetAgreementsByEmployee(objEmployeeTmp.Id)
                If AgreementsList IsNot Nothing AndAlso AgreementsList.Count > 0 Then
                    Dim groupAgreementsAuthorized = (From al In AgreementsList
                                                     Join ca In PayrollAuthorizationConcept On al.ConceptId Equals ca.ConceptId
                                                     Group ca By Key = New With {Key ca.Concept.Id, Key ca.Concept.Code, Key ca.Concept.Name}
                                                     Into Group
                                                     Select New With {
                                                        .Id = Key.Id,
                                                        .Code = Key.Code,
                                                        .Name = Key.Name,
                                                        .Quantity = Group.Count(Function(x) x.Id)
                                                    }).ToList()

                    If groupAgreementsAuthorized IsNot Nothing AndAlso groupAgreementsAuthorized.Any(Function(d) d.Quantity > 1) Then
                        Dim message As New Text.StringBuilder
                        For Each agreement In groupAgreementsAuthorized
                            message.AppendLine(String.Format("{0} - {1}", agreement.Code, agreement.Name))
                        Next

                        ActionMessageResult.MessageResult.Add(New MessageResult("-003: Convenios", String.Format("El Empleado {0} - {1} tiene más de un convenio activo para los siguientes conceptos: {2}", NitEmployee, NameEmployee, vbCrLf + message.ToString())))
                    End If
                End If

                Dim ObjRetroactiveEmployee As RetroactiveC

                If ListRetroactive IsNot Nothing AndAlso ListRetroactive.Count() > 0 Then
                    ObjRetroactiveEmployee = ListRetroactive.Where(Function(x) x.IdEmployee = ObjEmployee.Id).FirstOrDefault()
                End If

                'Retroactivo que se pagará por esta nómina
                Dim ValueRetroactivePaid As Decimal = 0
                Dim ObjRetroactivePaid As RetroactiveC

                If PaidRetroactive IsNot Nothing AndAlso PaidRetroactive.Count() > 0 Then
                    ObjRetroactivePaid = PaidRetroactive.Where(Function(x) x.IdEmployee = ObjEmployee.Id And x.PaymentType = "N").FirstOrDefault()
                End If

                If ObjRetroactivePaid IsNot Nothing AndAlso ObjRetroactivePaid.Id > 0 Then
                    ValueRetroactivePaid = ObjRetroactivePaid.TotalRetroactiveValue
                End If

                Dim NewInitialDate As Date = New Date(PayrollEndDate.Year, 1, 1)
                Dim NewEndDate As Date = New Date(PayrollEndDate.Year, 12, 31)

                'Cargo Datos de los Parámetros de Nómina
                Dim EmployeeSindicate = ObjEmployee.TradeUnion

                Dim CodeEmployeeType As String
                If ObjEmployee.EmployeeType IsNot Nothing Then
                    CodeEmployeeType = ObjEmployee.EmployeeType.Code
                End If


                Dim PublicClient = SessionValues.IndigoCompanyType

                If groupEmployee.Company Is Nothing Then
                    'Puede venir por Liquidación de Contrato, así que hay que cargarlo
                    groupEmployee = _groupRepository.GetGroupById(groupEmployee.Id)
                End If

                'Cargo el Tercero del Empleado
                Dim EmployeeThirdPartyId As Integer = ObjEmployee.ThirdPartyId

                EmployeeLiquidated = New Liquidation()
                MessageLiquitadion = New Message()

                Dim HousingDeducted As Decimal = 0
                Dim StudyDeducted As Decimal = 0

                If ObjEmployee.HousingDeductionValue.HasValue = True Then
                    HousingDeducted = ObjEmployee.HousingDeductionValue
                End If

                If ObjEmployee.EducationDeductionValue.HasValue = True Then
                    StudyDeducted = ObjEmployee.EducationDeductionValue
                End If


                Dim codeWorkCenter As String
                If ObjEmployee.WorkCenter IsNot Nothing Then
                    codeWorkCenter = RTrim(ObjEmployee.WorkCenter.Code)
                End If

                'Fechas del Contrato
                'Cargo las Fechas para Evaluación
                Dim InitialDateEval As Date
                Dim EndingDateEval As Date


                Dim LessInitialContract = ContractList.Min(Function(c)
                                                               Return c.ContractInitialDate
                                                           End Function)

                If LessInitialContract < PayrollStarDate Then
                    InitialDateEval = PayrollStarDate
                Else
                    InitialDateEval = LessInitialContract
                End If

                EndingDateEval = contractEmployee.ContractEndingDate

                If InitialDateEval < PayrollStarDate Then
                    InitialDateEval = PayrollStarDate
                End If

                If EndingDateEval > PayrollEndDate Then
                    EndingDateEval = PayrollEndDate
                End If

                If RetirementDate <> Nothing AndAlso DateDiff(DateInterval.Day, RetirementDate, New Date(1901, 1, 1)) <> 0 Then
                    If EndingDateEval > RetirementDate Then
                        EndingDateEval = RetirementDate
                    End If
                End If

                If contractEmployee.JobBondingDate > InitialDateEval Then
                    InitialDateEval = contractEmployee.JobBondingDate
                End If

                Dim RetirementFlag As Boolean = False

                If RetirementDate <> CompareDate Then
                    RetirementFlag = True
                End If

                If contractEmployee.Status = 5 Then
                    EndingDateEval = contractEmployee.RetirementDate
                End If

                If InitialDateEval > EndingDateEval Then
                    EmployeeLiquidated = Nothing
                    ActionMessageResult.MessageResult.Add(New MessageResult("-999: Error", String.Concat("La Fecha Final del contrato (", EndingDateEval, ") del Empleado " & NitEmployee & " - " & NameEmployee & " es inferior de la fecha inicial de la liquidación")))
                    ActionMessageResult.StateResult = False
                    Continue For
                End If

                Dim DaysWorked = 0
                DaysWorked = Me.DaysWorkedEmployee(InitialDateEval, EndingDateEval, PayrollStarDate, PayrollEndDate, groupEmployee.Month)

                'Cargamos las variable para el cálculo de Provisiones

                Dim BonificationValue As Decimal = 0
                Dim PaidValueAverageIncentiveServices As Decimal = 0
                Dim ValueRepresentationCost As Double = 0
                Dim VacationIncentiveValueProvision As Decimal = 0

                If PublicClient = 2 Then
                    'Buscamos el objeto de Bonificaciones
                    Dim datePeriod As Date = PayrollStarDate.AddYears(-1)
                    Dim ObjConceptClassBonification = _liquitationRepository.GetLastConceptClassListDate(IIf(contractEmployee.InitialContractNumber = 0, contractEmployee.Id, contractEmployee.InitialContractNumber), "055", datePeriod).ToList()

                    If ObjConceptClassBonification Is Nothing Or ObjConceptClassBonification.Count <= 0 Then
                        ObjConceptClassBonification = _liquitationRepository.GetLastConceptClassListDate(IIf(contractEmployee.InitialContractNumber = 0, contractEmployee.Id, contractEmployee.InitialContractNumber), "046", datePeriod).ToList()
                    End If

                    If ObjConceptClassBonification IsNot Nothing AndAlso ObjConceptClassBonification.Count > 0 Then

                        Dim ObjConceptBonification = ObjConceptClassBonification.Where(Function(x) x.Liquidation.RegisterStatus = "C").FirstOrDefault()

                        If ObjConceptBonification Is Nothing Then
                            ObjConceptBonification = ObjConceptClassBonification.FirstOrDefault()
                        End If


                        Dim BonificationConceptId = ObjConceptBonification.ConceptId
                        BonificationValue = ObjConceptBonification.ConceptTotalValue

                        If ObjRetroactiveEmployee IsNot Nothing Then
                            Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = BonificationConceptId).FirstOrDefault()
                            If ObjConceptRetroactive IsNot Nothing Then
                                BonificationValue = ObjConceptRetroactive.ValueConceptWithRetroactive + BonificationValue
                            End If
                        End If

                    End If

                    Dim ListPaidValueAverageIncentiveServices = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation(contractEmployee.EmployeeId)
                    Dim ObjPaidValueAverageIncentiveServices = ListPaidValueAverageIncentiveServices.Where(Function(x) x.Period = 1 And x.PeriodEndDate >= datePeriod).FirstOrDefault()

                    'Buscamos el objeto de Primas de Servicios
                    If ObjPaidValueAverageIncentiveServices IsNot Nothing Then
                        PaidValueAverageIncentiveServices = ObjPaidValueAverageIncentiveServices.TotalAccrued

                        If ObjRetroactiveEmployee IsNot Nothing Then

                            'Si toca buscar el retroactivo, busco el dato del Concepto
                            Dim ListStringConceptClass As New List(Of String)
                            ListStringConceptClass.Add("002")
                            Dim Concept = _conceptRepository.GetConceptByConceptClass(ListStringConceptClass).FirstOrDefault()

                            If Concept IsNot Nothing Then
                                Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                                If ObjConceptRetroactive IsNot Nothing Then
                                    PaidValueAverageIncentiveServices = ObjConceptRetroactive.ValueConceptWithRetroactive + PaidValueAverageIncentiveServices
                                End If
                            End If
                        End If

                    End If

                    'Gastos de Representación:
                    If contractEmployee.Position.RepresentationCost Then
                        ValueRepresentationCost = contractEmployee.BasicSalary * 0.135
                    End If

                    'Valor Prima de Vacaciones
                    Dim ObjConceptClassVacationIncentiveValue = _liquitationRepository.GetLastConceptClassListDateBetween(IIf(contractEmployee.InitialContractNumber = 0, contractEmployee.Id, contractEmployee.InitialContractNumber), "049", PayrollSettings.InitialDateChristmasIncentivePayment, PayrollSettings.EndDateChristmasIncentivePayment)
                    If ObjConceptClassVacationIncentiveValue.Count > 0 Then
                        Dim VacationIncentiveConceptId As Integer
                        For Each item In ObjConceptClassVacationIncentiveValue
                            VacationIncentiveValueProvision += item.ConceptTotalValue
                            VacationIncentiveConceptId = item.ConceptId
                        Next

                        If ObjRetroactiveEmployee IsNot Nothing Then
                            Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = VacationIncentiveConceptId).FirstOrDefault()
                            If ObjConceptRetroactive IsNot Nothing Then
                                VacationIncentiveValueProvision = ObjConceptRetroactive.ValueConceptWithRetroactive + VacationIncentiveValueProvision
                            End If

                        End If
                    End If
                End If

                'VARIABLES DE FONDOS

                'Salud
                Dim HealthFundId As Integer?
                Dim HealthFundName As String = String.Empty
                Dim ThirdPartyHealthFundId As Integer? = 0

                'Salud Voluntaria
                Dim HealthVoluntaryFundId As Integer?
                Dim HealthVoluntaryFundName As String = String.Empty
                Dim ThirdPartyVoluntaryHealthFundId As Integer? = 0
                Dim VoluntaryHealthValue As Decimal = 0

                Dim AfiliationHealthDays As Integer = 0

                'Pensión
                Dim PensionFundId As Integer?
                Dim PensionFundName As String = String.Empty
                Dim ThirdPartyPensionFundId As Integer? = 0

                'Pensión Voluntaria
                Dim PensionVoluntaryFundId As Integer?
                Dim PensionVoluntaryFundName As String = String.Empty
                Dim ThirdPartyVoluntaryPensionFundId As Integer? = 0
                Dim VoluntaryPensionValue As Decimal = 0

                Dim AfiliationPensionDays As Integer = 0

                'Cesantias
                Dim UnemploymentFundId As Integer?
                Dim UnemploymentFundName As String = String.Empty
                Dim ThirdPartyUnemploymentFundId As Integer? = 0

                'Trae los intereses de Cesantias 
                Dim ObjListUnemploymentInteres = _unemployedLiquidationRepository.GetUnemployedLiquidationByContractIdInterestPayDay(contractEmployee.Id, PayrollStarDate)
                Dim ListUnemployement = New List(Of UnemployedLiquidation)
                Dim UnemploymentInteresValue As Integer? = 0



                'Valida que el mes de nomina sea diciembre y que este marcado UnemployedInterestPaidWithPayroll como true
                If ObjListUnemploymentInteres Is Nothing Then
                    UnemploymentInteresValue = 0
                Else

                    If ObjListUnemploymentInteres.UnemployedInterestPaidWithPayroll AndAlso Month(PayrollStarDate) = 1 AndAlso (QuarterFlag = 1 Or QuarterFlag = 4) Then
                        ListUnemployement.Add(ObjListUnemploymentInteres)
                        UnemploymentInteresValue = ObjListUnemploymentInteres.UnemployedInterestTotal

                    End If

                End If

                'ARL
                Dim ARLFundId As Integer?
                Dim ARLFundName As String = String.Empty
                Dim ThirdPartyARLFundId As Integer? = 0

                'Caja de Compensación
                Dim CompensationFundId As Integer?
                Dim CompensationFundName As String = String.Empty
                Dim ThirdPartyCompensationFundId As Integer?

                ' Cargo los Fondos del Empleado
                Dim FundContract As List(Of FundContract)

                FundContract = _liquitationRepository.GetFundsContractFundByIdFund(contractEmployee.Id)

                If FundContract Is Nothing Then
                    FundContract = _liquitationRepository.GetFundsContractFundByIdFund(contractEmployee.InitialContractNumber)
                End If

                If FundContract IsNot Nothing Then
                    Dim EmployeeFundTuple = _functionsLiquidation.EmployeeFundContract(FundContract)

                    For Each ObjTuple As Tuple(Of String, Integer, Integer, String, Decimal) In EmployeeFundTuple
                        If ObjTuple.Item1 = "Salud" Then
                            HealthFundId = ObjTuple.Item2
                            ThirdPartyHealthFundId = ObjTuple.Item3
                            HealthFundName = ObjTuple.Item4
                            'VoluntaryHealthValue = 0
                        End If
                        If ObjTuple.Item1 = "SaludVoluntaria" Then
                            HealthVoluntaryFundId = ObjTuple.Item2
                            ThirdPartyVoluntaryHealthFundId = ObjTuple.Item3
                            HealthVoluntaryFundName = ObjTuple.Item4
                            VoluntaryHealthValue = ObjTuple.Item5
                        End If
                        If ObjTuple.Item1 = "Pension" Then
                            PensionFundId = ObjTuple.Item2
                            ThirdPartyPensionFundId = ObjTuple.Item3
                            PensionFundName = ObjTuple.Item4
                            'VoluntaryPensionValue = 0
                        End If
                        If ObjTuple.Item1 = "PensionVoluntaria" Then
                            PensionVoluntaryFundId = ObjTuple.Item2
                            ThirdPartyVoluntaryPensionFundId = ObjTuple.Item3
                            PensionVoluntaryFundName = ObjTuple.Item4
                            VoluntaryPensionValue = ObjTuple.Item5
                        End If
                        If ObjTuple.Item1 = "Cesantias" Then
                            UnemploymentFundId = ObjTuple.Item2
                            ThirdPartyUnemploymentFundId = ObjTuple.Item3
                            UnemploymentFundName = ObjTuple.Item4
                        End If
                        If ObjTuple.Item1 = "ARL" Then
                            ARLFundId = ObjTuple.Item2
                            ThirdPartyARLFundId = ObjTuple.Item3
                            ARLFundName = ObjTuple.Item4
                        End If
                        If ObjTuple.Item1 = "CajaCompensacion" Then
                            CompensationFundId = ObjTuple.Item2
                            ThirdPartyCompensationFundId = ObjTuple.Item3
                            CompensationFundName = ObjTuple.Item4
                        End If
                    Next
                Else
                    MessageLiquitadion = CreateMessage("El Empleado NO TIENE FONDOS ASIGNADOS ", True, PayrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    ActionMessageResult.MessageResult.Add(New MessageResult("-004: Fondos de Empleados", "El Empleado " & NitEmployee & " - " & NameEmployee & " NO tiene Fondos Asignados " & contractEmployee.ContractEndingDate))
                    ActionMessageResult.StateResult = False
                End If

                'Cargo las Vacaciones del Empleado

                'Variables
                Dim Vacation As Decimal = 0
                Dim ContractVacationDays = 0
                Dim VacationPaidValue As Decimal = 0
                Dim RecreationBonificationValue As Decimal = 0
                Dim VacationalIncrease As Decimal = 0
                Dim VacationIncentivePayment As Decimal = 0
                Dim VacationCompensationValue As Decimal = 0
                Dim VacationIncentiveValue As Decimal = 0
                Dim VacationValue As Decimal = 0
                Dim PensionVacation As Decimal = 0
                Dim HealthVacation As Decimal = 0
                Dim FSPVacation As Decimal = 0
                Dim PaidVacation As Byte = 0
                Dim PaidCredit As Byte = 0
                Dim VacationRTFValue As Decimal = 0
                Dim VacationHealthRTF As Decimal = 0
                Dim VacationPensionRTF As Decimal = 0
                Dim VacationInitialDate As Date = New Date()
                Dim VacationEndDate As Date = New Date()
                Dim VacationPaidType As Integer = 0
                Dim EnjoyDays As Integer = 0
                Dim VacationIBCValue As Decimal = 0
                Dim VacationType As Byte
                'Dias vacaciones en dinero
                Dim VacationDaysInCash As Decimal = 0
                Dim ImmediateVacationValue As Decimal = 0
                Dim ImmediateVacationBonificationValue As Decimal = 0
                Dim ImmediateVacationIncentivePaymentValue As Decimal = 0
                Dim ImmediateVacationalIncreaseValue As Decimal = 0
                'Se sube la variable para que calcule los dias totales de vacaciones
                Dim TotalVacationDays As Integer = 0

                Dim ListVacationPastEmployee = _vacationRepository.GetVacationBetweenDateAndActiveContract(contractEmployee.EmployeeId, PayrollStarDate, PayrollEndDate)
                Dim ListVacationContract = ListVacationPastEmployee.Where(Function(x) x.VacationPeriod.EmployeeId = contractEmployee.EmployeeId).Distinct().ToList()

                If ListVacationEmployee IsNot Nothing AndAlso ListVacationEmployee.Count > 0 Then
                    Dim GroupedVacations = ListVacationPastEmployee _
                        .GroupBy(Function(x) New With {Key .StartDate = x.VacationStartDate,
                                                       Key .EndDate = x.VacationEndDate,
                                                       Key .LiquidationType = x.TypeLiquidation,
                                                       Key .VacationType = x.TypeVacation,
                                                       Key .TypePayment = x.TypePayment,
                                                       Key .LiquidationDate = x.LiquidationDate,
                                                       Key .IncorporationDate = x.IncorporationDate,
                                                       Key .State = x.State,
                                                       Key .EmployeeId = x.VacationPeriod.EmployeeId}).ToList()

                    TotalVacationDays = GroupedVacations.Sum(Function(g) g.Where(Function(x) x.State <> 2 AndAlso x.TypePayment = 2).Select(Function(x) x.EnjoyDays).FirstOrDefault())
                    Dim ListIdVacation As New List(Of Integer)

                    For Each ObjId As Vacation In ListVacationPastEmployee
                        ListIdVacation.Add(ObjId.Id)
                    Next

                    Dim ObjVacationPaid = ListVacationEmployee.Where(Function(x) x.VacationPeriod.EmployeeId = contractEmployee.EmployeeId And Not ListIdVacation.Contains(x.Id)).ToList()

                    If ObjVacationPaid IsNot Nothing Then
                        ListVacationContract.AddRange(ObjVacationPaid)
                    End If

                End If

                If ListVacationContract IsNot Nothing AndAlso ListVacationContract.Count > 0 Then
                    Dim VacationTupleTuple = _functionsLiquidation.AnalisisVacation(Me, PayrollStarDate, PayrollEndDate, ListVacationContract, contractEmployee, PaidVacation, PaidCredit, VacationInitialDate, VacationEndDate, VacationPaidType, EnjoyDays, VacationType)
                    For Each ObjTuple As Tuple(Of String, Decimal) In VacationTupleTuple
                        If ObjTuple.Item1 = "DiasVacaciones" Then
                            ContractVacationDays = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "ValorVacaciones" Then
                            VacationValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "ValorPagarVacaciones" Then
                            VacationPaidValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "PensionVacaciones" Then
                            PensionVacation = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "SaludVacaciones" Then
                            HealthVacation = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "FSPVacaciones" Then
                            FSPVacation = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "BonificacionVacaciones" Then
                            RecreationBonificationValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "PrimaVacaciones" Then
                            VacationIncentivePayment = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "IncrementoVacaciones" Then
                            VacationalIncrease = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "ValorVacacionesRTF" Then
                            VacationRTFValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "AporteSaludVacaciones" Then
                            VacationHealthRTF = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "AportePensionVacaciones" Then
                            VacationPensionRTF = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "ValorVacacionesCompensadas" Then
                            VacationCompensationValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "DiasVacacionesCompensadas" Then
                            VacationDaysInCash = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "ValorVacacionesPagoInmediato" Then
                            ImmediateVacationValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "BonificacionVacacionesPagoInmediato" Then
                            ImmediateVacationBonificationValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "PrimaVacacionesPagoInmediato" Then
                            ImmediateVacationIncentivePaymentValue = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "IncrementoVacacionesPagoInmediato" Then
                            ImmediateVacationalIncreaseValue = ObjTuple.Item2
                        End If
                    Next

                End If

                If ContractVacationDays > 0 Then

                    If VacationInitialDate >= PayrollStarDate And VacationEndDate <= PayrollEndDate Then

                        Dim DaysDiv = Days360(VacationInitialDate, VacationEndDate)

                        If DaysDiv <= 0 Then
                            DaysDiv = 1
                        End If

                        VacationIBCValue = ((VacationHealthRTF * 100) / 4) * (Days360(VacationInitialDate, VacationEndDate)) / DaysDiv
                    End If

                    If VacationInitialDate.Month < VacationEndDate.Month Then
                        If New Date(VacationInitialDate.Date.Year, VacationInitialDate.Date.Month, DateTime.DaysInMonth(VacationInitialDate.Date.Year, VacationInitialDate.Date.Month)).Day = 31 Then
                            EnjoyDays = EnjoyDays - 1
                        End If
                    Else
                        If VacationEndDate.Day = 31 Then
                            EnjoyDays = EnjoyDays - 1
                        End If
                    End If

                    If VacationEndDate.DayOfWeek = DayOfWeek.Friday Then
                        VacationIBCValue = If(EnjoyDays = 0, 0.0, ((VacationHealthRTF * 100) / 4) * (Days360(VacationInitialDate, VacationEndDate)) / EnjoyDays)
                    End If

                    'Para vacaciones que vienen del mes anterior
                    If VacationInitialDate < PayrollStarDate And VacationEndDate < PayrollEndDate Then
                        If VacationEndDate > PayrollStarDate Then
                            VacationIBCValue = ((VacationHealthRTF * 100) / 4) * (ContractVacationDays) / Days360(VacationInitialDate, VacationEndDate)
                        End If
                    End If

                    'Para vacaciones que inician este mes y finalizan el otro
                    If VacationInitialDate > PayrollStarDate And VacationEndDate > PayrollEndDate And EnjoyDays > 0 Then
                        If PayrollEndDate.Month = 2 Then
                            Dim CalculoMesV = DateDiff(DateInterval.Day, VacationInitialDate, PayrollEndDate) + 1
                            VacationIBCValue = (((VacationHealthRTF * 100) / 4) * CalculoMesV) / EnjoyDays
                        Else
                            VacationIBCValue = ((VacationHealthRTF * 100) / 4) * (Days360(VacationInitialDate, PayrollEndDate)) / EnjoyDays
                        End If

                    End If


                    ActionMessageResult.MessageResult.Add(New MessageResult("004: Vacaciones", "El Empleado " & NitEmployee & " - " & NameEmployee & " está en Vacaciones"))
                End If

                'Incapacidades

                Dim TotalEmployeeInabilityDays As Integer = 0

                'Ambulatoria
                Dim AmbulatoryInabilityDays = 0
                Dim AmbulatoryInabilityEmployeerDays = 0
                Dim AmbulatoryInabilityERPDays = 0
                Dim AmbulatoryInability As Decimal = 0
                Dim ValueAmbulatoryInability As Decimal = 0
                Dim ValueAmbulatoryEmployeerInability As Decimal = 0
                Dim ValueAmbulatoryERPInability As Decimal = 0
                Dim AmbulatoryInabilityInitialDate As Date? = Nothing
                Dim AmbulatoryInabilityEndDate As Date? = Nothing
                Dim AutorizationNumberAmbulatoryInability As String = String.Empty
                Dim TotalSpendingInabilityAmbulatory As Decimal = 0
                Dim TotalInabilityCollectAmbulatory As Decimal = 0
                Dim ValueGeneralInability As Decimal = 0

                'Hospitalaria
                Dim HospitalInability As Decimal = 0
                Dim HospitalInabilityDays = 0
                Dim HospitalInabilityEmployeerDays = 0
                Dim HospitalInabilityERPDays = 0
                Dim ValueHospitalInability As Decimal = 0
                Dim ValueHospitalEmployeerInability As Decimal = 0
                Dim ValueHospitalERPInability As Decimal = 0
                Dim HospitalaryInabilityInitialDate As Date? = Nothing
                Dim HospitalaryInabilityEndDate As Date? = Nothing
                Dim AutorizationNumberHospitalaryInability As String = String.Empty
                Dim TotalSpendingInabilityHospital As Decimal = 0
                Dim TotalInabilityCollectHospital As Decimal = 0

                'Maternidad
                Dim Maternity As Decimal = 0
                Dim ValueMaternity As Decimal = 0
                Dim MaternityInabilityDays = 0
                Dim ValueMaternityInability As Decimal = 0
                Dim MAternityInabilityInitialDate As Date? = Nothing
                Dim MaternityInabilityEndDate As Date? = Nothing
                Dim AutorizationNumberMaternityInability As String = String.Empty
                Dim TotalSpendingInabilityMaternity As Decimal = 0
                Dim TotalInabilityCollectMaternity As Decimal = 0

                'Paternidad
                Dim ValuePaternity As Decimal = 0
                Dim PaternityInabilityDays = 0
                Dim PaternityInabilityInitialDate As Date? = Nothing
                Dim PaternityInabilityEndDate As Date? = Nothing
                Dim TotalSpendingInabilityPaternity As Decimal = 0
                Dim TotalInabilityCollectPaternity As Decimal = 0

                'Sancion
                Dim Sanctions As Decimal = 0
                Dim SanctionsDays = 0
                Dim SanctionInitialDate As Date? = Nothing
                Dim SanctionEndDate As Date? = Nothing

                'Licencia No Remunerada
                Dim UnpaidLicenses As Decimal = 0
                Dim UnpaidLicensesDays = 0
                Dim UnpaidLicencesesInitialDate As Date? = Nothing
                Dim UnpaidLicencesesEndDate As Date? = Nothing
                Dim AutorizationNumberUnpaidLicenses As String = String.Empty

                'Luto
                Dim ValueLuto As Decimal = 0
                Dim LutoInitialDate As Date? = Nothing
                Dim LutoEndDate As Date? = Nothing
                Dim LutoDays As Integer = 0
                Dim AutorizationNumberLuto As String = String.Empty
                Dim TotalSpendingInabilityLuto As Decimal = 0
                Dim TotalInabilityCollectLuto As Decimal = 0

                'EnfermedadProfesional
                Dim InabilityProfessionalRisk As Decimal = 0
                Dim ProfesionalInabilitiesDays As Integer = 0
                Dim ValueProfesionalInabilities As Decimal = 0
                Dim ProfessionalRiskInitialDate As Date? = Nothing
                Dim ProfessionalRiskEndDate As Date? = Nothing
                Dim AutorizationNumberProfessionalRisk As String = String.Empty
                Dim TotalSpendingInabilityProfessionalRisk As Decimal = 0
                Dim TotalInabilityCollectProfessionalRisk As Decimal = 0

                Dim EmployerProfessionalDisabilityDays As Integer = 0    ''Dias patrono incapacidad riesgo profesional patrono
                Dim EmployerProfessionalDisabilityAmount As Decimal = 0   ''Valor incapacidad riesgo profesional patrono
                Dim ERPProfessionalDisabilityDays As Integer = 0          ''Dias incapacidad riesgo profesional erp
                Dim ERPProfessionalDisabilityAmount As Decimal = 0         ''Valor incapacidad  riesgo profesional erp


                'Licencias Remuneradas
                Dim LicensesDays As Integer = 0
                Dim FamilyDay As Short = 0
                Dim Licenses As Decimal = 0
                Dim ValueRemuneratedLicenses As Decimal = 0
                Dim LicencesesInitialDate As Date? = Nothing
                Dim LicencesesEndDate As Date? = Nothing

                'Calamidad Doméstica
                Dim CalamidadDomestica As Decimal = 0
                Dim CalamidadDays = 0
                Dim CalamidadInitialDate As Date? = Nothing
                Dim CalamidadEndDate As Date? = Nothing


                'Dias permiso
                Dim DaysPermission As Decimal = 0
                'Dias licencia Remunerada
                Dim PaidLeaveDays As Decimal = 0

                Dim GeneralnabilityDays As Integer = 0

                Dim ValueSanction As Decimal = 0
                Dim ValueUnpaidLicenses As Decimal = 0

                Dim Permission As Decimal = 0

                Dim VacationInitialModifiedDate As Date? = Nothing
                Dim VacationEndModifiedDate As Date? = Nothing

                Dim employeeNovelty = _noveltyRepository.GetNoveltyByEmployeeIdPayrollLiquidation(contractEmployee.EmployeeId)

                Dim FlagPermanentInability As Integer = 0

                Dim InabilityInitialDate As Date? = Nothing
                Dim InabilityEndDate As Date? = Nothing
                Dim prevMonthEmployer As Decimal = 0


                If employeeNovelty IsNot Nothing AndAlso employeeNovelty.Count > 0 Then

                    ActionMessageResult.MessageResult.Add(New MessageResult("002: Incapacidades", "El Empleado " & NitEmployee & " - " & NameEmployee & " tiene novedades registradas en el periodo"))

                    Dim NoveltyTuple = _functionsLiquidation.AnalisisNovelty(Me, contractEmployee, PayrollStarDate, PayrollEndDate, employeeNovelty, PayrollDays, _noveltyRepository, VacationInitialModifiedDate, VacationEndModifiedDate, SessionValues, Day31, CDec(groupEmployee.PayrollParameter.LegalSalaryMinimum))

                    If NoveltyTuple.Count > 0 Then
                        ActionMessageResult.MessageResult.Add(New MessageResult("002: Incapacidades", "El Empleado " & NitEmployee & " - " & NameEmployee & " tiene novedades registradas en el periodo"))
                    End If

                    For Each ObjTuple As Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)) In NoveltyTuple

                        If ObjTuple.Rest.Item1 = 2 Then
                            MessageLiquitadion = CreateMessage("El Empleado se encuentra en Incapacidad Permanente", False, PayrollEndDate)
                            EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            TotalSpendingInabilityAmbulatory = 0
                            TotalInabilityCollectAmbulatory = 0

                            TotalSpendingInabilityHospital = 0
                            TotalInabilityCollectHospital = 0

                            TotalSpendingInabilityProfessionalRisk = 0
                            TotalInabilityCollectProfessionalRisk = 0

                            EmployerProfessionalDisabilityAmount = 0
                            ERPProfessionalDisabilityAmount = 0


                            FlagPermanentInability = 2

                        End If

                        If ObjTuple.Item1 = "Ambulatoria" Then
                            If ObjTuple.Item2 > 0 Then
                                AmbulatoryInabilityDays = AmbulatoryInabilityDays + ObjTuple.Item2
                                AmbulatoryInabilityEmployeerDays = AmbulatoryInabilityEmployeerDays + ObjTuple.Rest.Item4
                                AmbulatoryInabilityERPDays = AmbulatoryInabilityERPDays + ObjTuple.Rest.Item5
                                ValueAmbulatoryInability = ValueAmbulatoryInability + (ObjTuple.Item3 + ObjTuple.Item4)

                                AmbulatoryInabilityInitialDate = ObjTuple.Item5
                                AmbulatoryInabilityEndDate = ObjTuple.Item6
                                AutorizationNumberAmbulatoryInability = ObjTuple.Item7

                                TotalSpendingInabilityAmbulatory = TotalSpendingInabilityAmbulatory + ObjTuple.Item3
                                TotalInabilityCollectAmbulatory = TotalInabilityCollectAmbulatory + ObjTuple.Item4

                                FlagPermanentInability = ObjTuple.Rest.Item1
                                If AmbulatoryInabilityEmployeerDays > 0 Then
                                    prevMonthEmployer = ObjTuple.Rest.Item2
                                End If
                                ValueAmbulatoryEmployeerInability = ValueAmbulatoryEmployeerInability + prevMonthEmployer
                                ValueAmbulatoryERPInability = ValueAmbulatoryERPInability + ObjTuple.Rest.Item3


                                ValueGeneralInability = ValueAmbulatoryInability

                                MessageLiquitadion = CreateMessage("Existe registrada una Incapacidad Ambulatoria en el Periodo de la Nómina registrada entre el día " & AmbulatoryInabilityInitialDate & " y " & AmbulatoryInabilityEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                If FlagPermanentInability = 1 Then
                                    MessageLiquitadion = CreateMessage("Este mes este empleado cumple los 180 días de Incapacidad", False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                End If

                            End If
                        End If

                        If ObjTuple.Item1 = "Hospitalaria" Then
                            If ObjTuple.Item2 > 0 Then
                                HospitalInabilityDays = HospitalInabilityDays + ObjTuple.Item2
                                HospitalInabilityEmployeerDays = HospitalInabilityEmployeerDays + ObjTuple.Rest.Item4
                                HospitalInabilityERPDays = HospitalInabilityERPDays + ObjTuple.Rest.Item5

                                ValueHospitalInability = ValueHospitalInability + (ObjTuple.Item3 + ObjTuple.Item4)

                                HospitalaryInabilityInitialDate = ObjTuple.Item5
                                HospitalaryInabilityEndDate = ObjTuple.Item6
                                AutorizationNumberHospitalaryInability = ObjTuple.Item7

                                TotalSpendingInabilityHospital = TotalSpendingInabilityHospital + ObjTuple.Item3
                                TotalInabilityCollectHospital = TotalInabilityCollectHospital + ObjTuple.Item4

                                ValueHospitalEmployeerInability = ValueHospitalEmployeerInability + ObjTuple.Rest.Item2
                                ValueHospitalERPInability = ValueHospitalERPInability + ObjTuple.Rest.Item3

                                MessageLiquitadion = CreateMessage("Existe registrada una Incapacidad Hospitalaria en el Periodo de la Nómina registrada entre el día " & HospitalaryInabilityInitialDate & " y " & HospitalaryInabilityEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                If FlagPermanentInability = 1 Then
                                    MessageLiquitadion = CreateMessage("Este mes este empleado cumple los 180 días de Incapacidad", False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                End If

                            End If
                        End If

                        If ObjTuple.Item1 = "Maternidad" Then
                            If ObjTuple.Item2 > 0 Then
                                MaternityInabilityDays = MaternityInabilityDays + ObjTuple.Item2
                                ValueMaternityInability = ValueMaternityInability + (ObjTuple.Item3 + ObjTuple.Item4)
                                ValueMaternity = ValueMaternityInability

                                MAternityInabilityInitialDate = ObjTuple.Item5
                                MaternityInabilityEndDate = ObjTuple.Item6
                                AutorizationNumberMaternityInability = ObjTuple.Item7

                                TotalSpendingInabilityMaternity = TotalSpendingInabilityMaternity + ObjTuple.Item3
                                TotalInabilityCollectMaternity = TotalInabilityCollectMaternity + ObjTuple.Item4

                                MessageLiquitadion = CreateMessage("Existe registrada una Licencia de Maternidad en el Periodo de la Nómina registrada entre el día " & MAternityInabilityInitialDate & " y " & MaternityInabilityEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "Paternidad" Then
                            If ObjTuple.Item2 > 0 Then
                                PaternityInabilityDays = PaternityInabilityDays + ObjTuple.Item2
                                LicensesDays = LicensesDays + ObjTuple.Item2
                                ValuePaternity = ValuePaternity + (ObjTuple.Item3 + ObjTuple.Item4)

                                PaternityInabilityInitialDate = ObjTuple.Item5
                                PaternityInabilityEndDate = ObjTuple.Item6

                                MAternityInabilityInitialDate = ObjTuple.Item5
                                MaternityInabilityEndDate = ObjTuple.Item6

                                TotalSpendingInabilityPaternity = TotalSpendingInabilityPaternity + ObjTuple.Item3
                                TotalInabilityCollectPaternity = TotalInabilityCollectPaternity + ObjTuple.Item4

                                MessageLiquitadion = CreateMessage("Existe registrada una Licencia de Paternidad en el Periodo de la Nómina registrada entre el día " & PaternityInabilityInitialDate & " y " & PaternityInabilityEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "Sancion" Then
                            If ObjTuple.Item2 > 0 Then
                                SanctionsDays = SanctionsDays + ObjTuple.Item2

                                SanctionInitialDate = ObjTuple.Item5
                                SanctionEndDate = ObjTuple.Item6

                                MessageLiquitadion = CreateMessage("Existe registrada una Sanción en el Periodo de la Nómina registrada entre el día " & SanctionInitialDate & " y " & SanctionEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "LicenciasNoRemuneradas" Then
                            If ObjTuple.Item2 > 0 Then
                                UnpaidLicensesDays = UnpaidLicensesDays + ObjTuple.Item2

                                UnpaidLicencesesInitialDate = ObjTuple.Item5
                                UnpaidLicencesesEndDate = ObjTuple.Item6
                                AutorizationNumberUnpaidLicenses = ObjTuple.Item7

                                MessageLiquitadion = CreateMessage("Existe registrada una Licencia No Remunerada en el Periodo de la Nómina registrada entre el día " & UnpaidLicencesesInitialDate & " y " & UnpaidLicencesesEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "LicenciasRemuneradas" Then
                            If ObjTuple.Item2 > 0 Then
                                PaidLeaveDays = PaidLeaveDays + ObjTuple.Item2
                                LicencesesInitialDate = ObjTuple.Item5
                                LicencesesEndDate = ObjTuple.Item6

                                ValueRemuneratedLicenses = ValueRemuneratedLicenses + (ObjTuple.Item3 + ObjTuple.Item4)

                                MessageLiquitadion = CreateMessage("Existe registrada una Licencia Remunerada en el Periodo de la Nómina registrada entre el día " & LicencesesInitialDate & " y " & LicencesesEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "DiaFamilia" Then
                            If ObjTuple.Item2 > 0 Then
                                FamilyDay = ObjTuple.Item2
                                LicensesDays = LicensesDays + FamilyDay

                                LicencesesInitialDate = ObjTuple.Item5
                                LicencesesEndDate = ObjTuple.Item6

                                ValueRemuneratedLicenses = ValueRemuneratedLicenses + (ObjTuple.Item3 + ObjTuple.Item4)

                                MessageLiquitadion = CreateMessage("Existe registrado un Dia de la Familia en el Periodo de la Nómina registrada entre el día " & LicencesesInitialDate & " y " & LicencesesEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "Luto" Then
                            If ObjTuple.Item2 > 0 Then
                                LutoDays = LutoDays + ObjTuple.Item2
                                ValueLuto = ValueLuto + (ObjTuple.Item3 + ObjTuple.Item4)

                                LutoInitialDate = ObjTuple.Item5
                                LutoEndDate = ObjTuple.Item6
                                AutorizationNumberLuto = ObjTuple.Item7

                                TotalSpendingInabilityLuto = TotalSpendingInabilityLuto + ObjTuple.Item3
                                TotalInabilityCollectLuto = TotalInabilityCollectLuto + ObjTuple.Item4

                                MessageLiquitadion = CreateMessage("Existe registrada una Licencia por Luto en el Periodo de la Nómina registrada entre el día " & LutoInitialDate & " y " & LutoEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "Profesional" Then
                            If ObjTuple.Item2 > 0 Then
                                ProfesionalInabilitiesDays = ProfesionalInabilitiesDays + ObjTuple.Item2
                                ValueProfesionalInabilities = ValueProfesionalInabilities + (ObjTuple.Item3 + ObjTuple.Item4)

                                ProfessionalRiskInitialDate = ObjTuple.Item5
                                ProfessionalRiskEndDate = ObjTuple.Item6
                                AutorizationNumberProfessionalRisk = ObjTuple.Item7

                                TotalSpendingInabilityProfessionalRisk = TotalSpendingInabilityProfessionalRisk + ObjTuple.Item3 ''patrono
                                TotalInabilityCollectProfessionalRisk = TotalInabilityCollectProfessionalRisk + ObjTuple.Item4 ''erp

                                EmployerProfessionalDisabilityDays = EmployerProfessionalDisabilityDays + ObjTuple.Rest.Item4   ''Dias patrono incapacidad riesgo profesional patrono
                                ERPProfessionalDisabilityDays = ERPProfessionalDisabilityDays + ObjTuple.Rest.Item5  ''Dias incapacidad riesgo profesional erp
                                EmployerProfessionalDisabilityAmount = EmployerProfessionalDisabilityAmount + ObjTuple.Rest.Item2 ''valor  incapacidad riesgo profesional 
                                ERPProfessionalDisabilityAmount = ERPProfessionalDisabilityAmount + ObjTuple.Rest.Item3 ''valor  incapacidad riesgo profesional erp

                                MessageLiquitadion = CreateMessage("Existe registrada una Incapacidad por Riesgos en el Periodo de la Nómina registrada entre el día " & ProfessionalRiskInitialDate & " y " & ProfessionalRiskEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                If FlagPermanentInability = 1 Then
                                    MessageLiquitadion = CreateMessage("Este mes este empleado cumple los 180 días de Incapacidad", False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                End If

                            End If
                        End If

                        If ObjTuple.Item1 = "Calamidad" Then
                            If ObjTuple.Item2 > 0 Then
                                CalamidadDays = CalamidadDays + ObjTuple.Item2
                                CalamidadInitialDate = ObjTuple.Item5
                                CalamidadEndDate = ObjTuple.Item6

                                MessageLiquitadion = CreateMessage("Existe registrada una Calamidad Doméstica en el Periodo de la Nómina registrada entre el día " & CalamidadInitialDate & " y " & CalamidadEndDate, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                            End If
                        End If

                        If ObjTuple.Item1 = "Permisos" Then
                            If ObjTuple.Item2 > 0 Then
                                DaysPermission = DaysPermission + ObjTuple.Item2
                                MessageLiquitadion = CreateMessage("Existe registrada un permiso en el Periodo de la Nómina registrada entre el día " & ObjTuple.Item5 & " y " & ObjTuple.Item6, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)
                            End If
                        End If
                    Next
                End If


                'CONTABILIZO LOS DIAS

                'Averiguo si el empleado tiene incapacidad permanente
                Dim PermanentInabilities As Integer = 0
                If ObjEmployee.PermanentInability IsNot Nothing Then
                    If ObjEmployee.PermanentInability Then
                        PermanentInabilities = _functionsLiquidation.PermanentInability(ObjEmployee, PayrollStarDate, PayrollEndDate, Me)
                    End If
                End If

                ' Cotizante exterior
                Dim ContributorAbroad As Boolean = ObjEmployee.ContributorAbroad

                Dim TotalPeriodDaysInabilities = 0
                TotalEmployeeInabilityDays = ProfesionalInabilitiesDays + MaternityInabilityDays + HospitalInabilityDays + AmbulatoryInabilityDays + GeneralnabilityDays + PermanentInabilities
                TotalPeriodDaysInabilities = TotalEmployeeInabilityDays

                Dim HealthDays As Integer = DaysWorked - SanctionsDays
                Dim PensionDays As Integer = DaysWorked - SanctionsDays
                Dim ARLDays As Integer = DaysWorked - SanctionsDays

                Dim ProvisionDays As Integer = ARLDays

                'Analizo si el empleado tiene incapacidades y vacaciones en el mismo mes
                If ListVacationPastEmployee.Count < 1 AndAlso (ContractVacationDays > 0 AndAlso TotalEmployeeInabilityDays > 0 AndAlso VacationEndModifiedDate IsNot Nothing) Then
                    ContractVacationDays = _functionsLiquidation.VacationAdjustNovelty(Me, EnjoyDays, VacationInitialDate, PayrollStarDate, PayrollEndDate, VacationEndModifiedDate, AmbulatoryInabilityInitialDate, VacationInitialModifiedDate)
                End If
                DaysWorked = DaysWorked - TotalEmployeeInabilityDays - ContractVacationDays - SanctionsDays - UnpaidLicensesDays - CalamidadDays - DaysPermission - PaidLeaveDays - LicensesDays - LutoDays

                ' Solo aplica para incapacidades de 180 días (CalculationType = 7)
                ' que cubran todos los días reales del mes (evita residuo por Feb 28 vs base 30)
                If TotalEmployeeInabilityDays > 0 AndAlso DaysWorked > 0 AndAlso
                   TotalEmployeeInabilityDays >= Day(PayrollEndDate) AndAlso
                   employeeNovelty IsNot Nothing AndAlso
                    employeeNovelty.Any(Function(x) x.CalculationType.HasValue AndAlso x.CalculationType.Value = 7) Then
                    DaysWorked = 0
                ElseIf Month(PayrollEndDate) = 2 AndAlso TotalEmployeeInabilityDays = 28 Then
                    DaysWorked = 0
                End If

                If DaysWorked < 0 Then
                    DaysWorked = 0
                End If

                If TransportDays = 0 Then
                    TransportDays = DaysWorked
                End If

                Dim DaysWorkedEmployee = DaysWorked



                Dim TotalValuesInabilities = ValueProfesionalInabilities + ValueHospitalInability + ValueAmbulatoryInability

                Dim AjusteDominical As Decimal = 0
                Dim AjusteHorasExtrasDiurnas As Decimal = 0
                Dim AjusteHorasExtrasNocturnas As Decimal = 0
                Dim AjusteHorasExtrasDiurnasFestivas As Decimal = 0
                Dim AjusteHorasExtrasNocturnasFestivas As Decimal = 0
                Dim AjusteRecargoNocturnasFestivas As Decimal = 0
                Dim HolidaysHours As Integer = 0

                'CUADRO DE TURNOS
                Dim ScheduleDetailEmployee = New List(Of ScheduleDetail)

                ScheduleDetailEmployee = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDateWithoutNovelties(contractEmployee.EmployeeId, InitialDateEval, EndingDateEval)

                'Cargo el Cuadro de Turnos del Mes Anterior (Un día antes) para el pago del Festivo
                Dim TmpScheduleDetailEmployeeLastMonth = New List(Of ScheduleDetail)

                If InitialDateEval.Day = 1 Then
                    TmpScheduleDetailEmployeeLastMonth = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDateWithoutNovelties(contractEmployee.EmployeeId, DateAdd(DateInterval.Day, -1, InitialDateEval), DateAdd(DateInterval.Day, -1, InitialDateEval))
                End If

                If ScheduleDetailEmployee IsNot Nothing AndAlso ScheduleDetailEmployee.Count > 0 Then
                    'Cargo las horas de los días festivos trabajados en el mes

                    If ScheduleDetailEmployee IsNot Nothing AndAlso ScheduleDetailEmployee.Count > 0 Then
                        If ListHolidays IsNot Nothing AndAlso ListHolidays.Count > 0 Then
                            HolidaysHours = _functionsLiquidation.AdjustFestiveHours(ScheduleDetailEmployee, ListHolidays, TmpScheduleDetailEmployeeLastMonth, PayrollStarDate)
                        End If

                        Dim tmpListAdjustEvents = _functionsLiquidation.AdjustDominicalValue(ScheduleDetailEmployee, ListHolidays, groupEmployee)

                        If TmpScheduleDetailEmployeeLastMonth IsNot Nothing AndAlso TmpScheduleDetailEmployeeLastMonth.Count > 0 Then
                            For Each objtmpScheduleDetail As ScheduleDetail In TmpScheduleDetailEmployeeLastMonth
                                ScheduleDetailEmployee.Remove(objtmpScheduleDetail)
                            Next
                        End If

                        For Each ObjTuple As Tuple(Of String, Decimal) In tmpListAdjustEvents
                            If ObjTuple.Item1 = "DominicalFestivo" Then
                                AjusteDominical = ObjTuple.Item2
                            End If
                            If ObjTuple.Item1 = "HorasExtrasDiurnas" Then
                                AjusteHorasExtrasDiurnas = ObjTuple.Item2
                            End If
                            If ObjTuple.Item1 = "HorasExtrasNocturna" Then
                                AjusteHorasExtrasNocturnas = ObjTuple.Item2
                            End If
                            If ObjTuple.Item1 = "HoraExtraFestivaDiurna" Then
                                AjusteHorasExtrasDiurnasFestivas = ObjTuple.Item2
                            End If
                            If ObjTuple.Item1 = "HoraExtraFestivaNocturna" Then
                                AjusteHorasExtrasNocturnasFestivas = ObjTuple.Item2
                            End If
                            If ObjTuple.Item1 = "RecargoNocturnoFestivo" Then
                                AjusteRecargoNocturnasFestivas = ObjTuple.Item2
                            End If

                        Next
                    End If

                End If

                'Cargo las horas de los días festivos trabajados en el mes
                Dim AjusteDominicalEventoPasado As Decimal = 0
                Dim AjusteRecargoNocturnasFestivasEventoPasado As Decimal = 0
                Dim AjusteHoraExtraFestivaDiurnaEventoPasado As Decimal = 0
                Dim AjusteHoraExtraFestivaNocturnaEventoPasado As Decimal = 0

                ''Averiguo el Cuadro de Turnos del Empleado del mes anterior para Eventos ingresados por Cuadro de Turnos
                Dim LastMonthScheduleDetailEmployee = New List(Of ScheduleDetail)
                Dim LastMonthInitialDate = DateAdd(DateInterval.Month, -1, PayrollStarDate)
                Dim LastMonthEndDate = New Date(LastMonthInitialDate.Year, LastMonthInitialDate.Month, DateTime.DaysInMonth(LastMonthInitialDate.Year, LastMonthInitialDate.Month))
                LastMonthScheduleDetailEmployee = _scheduleDetailRepository.GetScheduleDetailByEmployeeUnitBetweenDateWithoutNoveltiesLiquidation(contractEmployee.EmployeeId, LastMonthInitialDate, LastMonthEndDate)

                If LastMonthScheduleDetailEmployee IsNot Nothing AndAlso LastMonthScheduleDetailEmployee.Count > 0 Then

                    'Cargo los Festivos del mes anterior de Nómina
                    Dim ListHolidaysLastMonth = _holidayRepository.ListHolidayBetweenDate(LastMonthInitialDate, LastMonthEndDate)

                    Dim tmpListAdjustEvents = _functionsLiquidation.AdjustDominicalValue(LastMonthScheduleDetailEmployee, ListHolidaysLastMonth, groupEmployee, PayrollStarDate, True)

                    For Each ObjTuple As Tuple(Of String, Decimal) In tmpListAdjustEvents
                        If ObjTuple.Item1 = "DominicalFestivo" Then
                            AjusteDominicalEventoPasado = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "RecargoNocturnoFestivo" Then
                            AjusteRecargoNocturnasFestivasEventoPasado = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "HoraExtraFestivaDiurna" Then
                            AjusteHoraExtraFestivaDiurnaEventoPasado = ObjTuple.Item2
                        End If
                        If ObjTuple.Item1 = "HoraExtraFestivaNocturna" Then
                            AjusteHoraExtraFestivaNocturnaEventoPasado = ObjTuple.Item2
                        End If
                    Next
                End If

                Dim NoveltyScheduleDetail = New List(Of NoveltyScheduleDetail)

                'Datos del Contrato

                Dim minHourAmount = contractEmployee.Position.MinHourAmount
                Dim maxHourAmount = contractEmployee.Position.MaxHourAmount
                Dim MinBasicSalary = contractEmployee.Position.MinBasicSalary
                Dim MaxBasicSalary = contractEmployee.Position.MaxBasicSalary
                Dim HandlesTurnChart = contractEmployee.Position.HandlesTurnsChart
                Dim RepresentationCost = contractEmployee.Position.RepresentationCost
                Dim ProfessionalRiskPercentage = contractEmployee.Employee.ProfessionalRiskPercentage
                Dim ContractInitialDate = contractEmployee.ContractInitialDate
                Dim ContractEndingDate = contractEmployee.ContractEndingDate
                Dim JobBondingDate = contractEmployee.JobBondingDate
                Dim AnoLaborado As Boolean = False
                Dim PosesionDateTmp = contractEmployee.PosesionDate
                Dim PosesionDate As Date

                If PosesionDateTmp Is Nothing Then
                    PosesionDate = JobBondingDate
                Else
                    PosesionDate = PosesionDateTmp
                End If

                Dim IBCHour = contractEmployee.HoursDaily
                If IBCHour = Nothing Then
                    IBCHour = 8
                End If

                If PublicClient = 2 Then
                    'Verifico si el empleado tiene derecho a Bonificación por Año de Servicio
                    Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(contractEmployee.EmployeeId)
                    Dim SanctionsNovelty = _noveltyRepository.GetNoveltySanctions(contractEmployee.EmployeeId)

                    Dim serviceAnniversaryThisYear As Date = New Date(Year(PayrollStarDate), Month(JobBondingDate), Day(JobBondingDate))
                    Dim serviceYearStart As Date = DateAdd(DateInterval.Year, -1, serviceAnniversaryThisYear)
                    Dim serviceYearEnd As Date = serviceAnniversaryThisYear.AddDays(-1)

                    ' Filtrar novedades que afectan el año de servicio que se está evaluando
                    Dim validUnpaidLicenses = UnpaidLicensesNovelty.Where(Function(n) n.EndDate >= serviceYearStart AndAlso n.RealDate <= serviceYearEnd).ToList()
                    Dim validSanctions = SanctionsNovelty.Where(Function(n) n.EndDate >= serviceYearStart AndAlso n.RealDate <= serviceYearEnd).ToList()

                    Dim PlusDaysLicenses As Integer = 0
                    Dim PlusDaysSanctions As Integer = 0

                    If UnpaidLicensesNovelty IsNot Nothing And validUnpaidLicenses.Count > 0 Then
                        PlusDaysLicenses = validUnpaidLicenses.Sum(Function(x) x.Days)
                    End If

                    If SanctionsNovelty IsNot Nothing And validSanctions.Count > 0 Then
                        PlusDaysSanctions = validSanctions.Sum(Function(x) x.Days)
                    End If

                    If PlusDaysLicenses > 0 Or PlusDaysSanctions > 0 Then
                        MessageLiquitadion = CreateMessage("Existe registrada unas Sanciones y/o licencias que cambiarán la Fecha para el pago de Bonificación x Año de Servicio ", False, PayrollEndDate)
                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    End If

                    ' Recalcula la fecha de aniversario con los días de sanción/licencia aplicados
                    Dim ComparisionJobBondingDate = DateAdd(DateInterval.Day, (PlusDaysLicenses + PlusDaysSanctions), contractEmployee.JobBondingDate)

                    If Month(ComparisionJobBondingDate) = Month(PayrollStarDate) Then
                        AnoLaborado = True
                    End If

                    Dim ObjConceptClassBonificationValue = _liquitationRepository.GetLastConceptClass(IIf(contractEmployee.InitialContractNumber = 0, contractEmployee.Id, contractEmployee.InitialContractNumber), "046")
                    If ObjConceptClassBonificationValue IsNot Nothing Then
                        If Year(ObjConceptClassBonificationValue.PayrollDate) = Year(PayrollStarDate) Then
                            AnoLaborado = False
                        End If
                    End If
                End If


                '' DEFINO VARIABLES
                'Variables de IBC
                Dim BaseIBCHealth As Decimal = 0
                Dim BaseIBCPension As Decimal = 0
                Dim BaseParafiscalCompensationFund As Decimal = 0
                Dim BaseParafiscalCompensationFundIntegral As Decimal = 0
                Dim BaseParafiscalICBF As Decimal = 0
                Dim BaseParafiscalICBFIntegral As Decimal = 0
                Dim BaseIBCHealthEmployer As Decimal = 0
                Dim BaseIBCHealthEmployerIntegral As Decimal = 0
                Dim BaseIBCSENAIntegral As Decimal = 0
                Dim BaseIBCSENA As Decimal = 0
                Dim IBCHealth As Decimal = 0
                Dim IBCHealthEmployer As Decimal = 0
                Dim IBCPension As Decimal = 0
                Dim IBCPensionEmployer As Decimal = 0
                Dim IBCCompensationFund As Decimal = 0
                Dim IBCICBF As Decimal = 0
                Dim IBCSENA As Decimal = 0
                Dim IBCARP As Decimal = 0
                Dim IBCRTF As Double = 0
                Dim IBCPeriod As Decimal = 0
                Dim IBCLastPeriod As Decimal = 0
                Dim IBCSeverance As Decimal = 0
                Dim IBCVacation As Decimal = 0
                Dim IBCIncentivePayment As Decimal = 0

                Dim HourSchedule As Decimal = 0

                Dim IncentivePaymentServices As Decimal = 0
                Dim IncetivePaymentServicesExtra As Decimal = 0
                Dim BonusServices As Decimal = 0
                Dim Salary As Decimal = 0
                Dim AcumulatedHelpTransport As Decimal = 0
                Dim Compensation As Decimal = 0
                Dim Unemployment As Decimal = 0
                Dim RiskContribution As Decimal = 0
                Dim OtherAccrued As Decimal = 0
                Dim OtherDeducted As Decimal = 0
                Dim EveningOvertime As Decimal = 0
                Dim SundayOvertime As Decimal = 0
                Dim PensionEmployer As Decimal = 0
                Dim PensionEmployee As Decimal = 0
                Dim VoluntaryPension As Decimal = 0
                Dim HealthEmployee As Decimal = 0
                Dim HealthEmployer As Decimal = 0
                Dim VoluntaryHealth As Decimal = 0
                Dim Withholding As Decimal = 0

                Dim ProvisionVacation As Decimal = 0
                Dim MaxNumberIncentiveProvision As Decimal = 0
                Dim UnemploymentInterestsProvision As Decimal = 0
                Dim SenaProvision As Decimal = 0
                Dim CompensationFundProvision As Decimal = 0
                Dim ICBFProvision As Decimal = 0
                Dim RecargoNocturnoNormal As Decimal = 0
                Dim RecargoNocturnoFestivo As Decimal = 0
                Dim AFCAccount As Double = 0

                'Variables de Días
                Dim TotalDaysLicensesUnpaid = 0

                Dim VacationDays = 0

                Dim PeriodDays = 0

                Dim NumberContract = 1
                Dim FlagIBCPension = False
                Dim FlagIBCHealth = False
                Dim ContractLearningFlag = False
                Dim WorkDays = DaysWorked

                Dim AdjustVacationValue As Decimal = 0
                Dim unemployedInterestValue As Decimal = CDec(UnemploymentInteresValue)

                Dim ValueRetroactiveBonification As Decimal = 0

                Dim ConceptoDevengadoSuma As Decimal = 0
                Dim ConceptoDeducidoSuma As Decimal = 0

                Dim TotalIBCSolidaridad As Decimal = 0


                Dim DecemberIncentivePaymentAverage As Decimal = 0
                Dim IncentivePaymentAverage As Decimal = 0

                Dim ContractItem As Integer = 0

                Dim DailyBasicSalary As Decimal = 0

                Dim ContributionPensionSolidarityFund As Decimal = 0

                'Retención en la Fuente
                Dim BaseForeclousure As Decimal = 0
                Dim ValDependsValue As Double = 0
                Dim ValDependentSupplement As Double = 0
                Dim RetentionBase As Decimal = 0
                Dim PercentageRetention As Decimal = 0
                Dim RetentionValue As Decimal = 0
                Dim ExemptIncomeValue As Decimal = 0

                ' Verifico que el Empleado Maneje Cuadro de Turnos
                If HandlesTurnChart = False Then
                    MessageLiquitadion = CreateMessage("El Cargo del Empleado (" & contractEmployee.Position.Name & ") NO maneja Cuadro de Turnos", False, PayrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                End If

                'Valido que el Salario del Empleado sea mayor que Cero (0)
                If BasicSalary <= 0 Then
                    MessageLiquitadion = CreateMessage("El Empleado tiene Salario Básico igual a Cero (0)", True, PayrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)

                    ActionMessageResult.MessageResult.Add(New MessageResult("-006: Salarios de Empleados", "El Empleado " & NitEmployee & " - " & NameEmployee & " tiene su Salario Básico Igual a Cero (0) " & contractEmployee.ContractEndingDate))
                    ActionMessageResult.StateResult = False
                End If

                If BasicSalary < minimimunSalary AndAlso ContractLearningFlag = False Then
                    MessageLiquitadion = CreateMessage("El Salario del Empleado" & BasicSalary & " es menor que el Salario Mínimo Legal Vigente " & minimimunSalary & "", False, PayrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)

                    ActionMessageResult.MessageResult.Add(New MessageResult("-006: Salarios de Empleados", "El Empleado " & NitEmployee & " - " & NameEmployee & " tiene su Salario Básico " & BasicSalary & " MENOR al salario mínimo " & minimimunSalary))
                    ActionMessageResult.StateResult = True
                End If


                'Cargo los Conceptos Manuales de los Empleado
                Dim ListManualConcept As New List(Of ManualConcepts)

                ListManualConcept = _ManualConceptsRepository.GetManualConceptsByContractNumberPayrollDate(IIf(contractEmployee.InitialContractNumber = 0, contractEmployee.Id, contractEmployee.InitialContractNumber), PayrollStarDate, PayrollEndDate, 1, 1)
                'Validacion cuando exista doble contrato con distinto grupo para que tome solo los conceptos manuales  en el  contrato actual
                If DoubleContract = True AndAlso contractEmployee.Status = 1 Then
                    ListManualConcept = _ManualConceptsRepository.GetManualConceptsByContractNumberPayrollDate(contractEmployee.Id, PayrollStarDate, PayrollEndDate, 1, 1)
                End If

                Dim lastLiquidationMonth = GetLastLiquidationMonthWithoutNovelty(ObjEmployee.Id, PayrollStarDate)
                If lastLiquidationMonth IsNot Nothing Then
                    IBCLastPeriod = lastLiquidationMonth.PeriodJCB
                End If

                Dim MenosRenta As Double = 0
                Dim TotalRentasExentasyDeducciones As Double = 0

                ''Creacion detalles de liquidacion
                Dim PayrollAuthorizationConcepts = PayrollAuthorizationConcept _
                    .OrderBy(Function(x) IIf(x.Concept.ConceptClass = "020", 1, 0)) _
                    .ThenBy(Function(x) IIf(x.Concept.Formulates.Contains(ConceptFormulatePrefix), 1, 0)) _
                    .ThenBy(Function(x)
                                ' ====================================================================
                                ' ORDEN INTEGRADO: Corrección de IBC + Cumplimiento Ley 1393/2010
                                ' ====================================================================
                                ' PRIORIDAD 1: Deducciones que afectan IBC (Type 2)
                                '   - Se procesan PRIMERO para corregir IBCs
                                '   - Esto garantiza cálculos correctos en conceptos dependientes
                                If x.Concept.ConceptType = 2 AndAlso (x.Concept.AffectIBCHealth OrElse x.Concept.AffectIBCPension OrElse x.Concept.AffectIBCARP) Then
                                    Return 0  ' MÁXIMA PRIORIDAD

                                    ' PRIORIDAD 2-5: Ingresos salariales (Type 1) - Ley 1393/2010
                                    '   Se procesan DESPUÉS cuando IBCs ya están corregidos
                                ElseIf x.Concept.ConceptType = 1 Then
                                    ' 2 - Salariales normales (AffectIBC=True, AffectLimit40=False)
                                    '     Estos construyen las bases IBC
                                    If x.Concept.AffectIBC = True AndAlso x.Concept.AffectLimit40Law1393 = False Then
                                        Return 1

                                        ' 3 - Bonificaciones no salariales (AffectLimit40Law1393=True)
                                        '     Estas consumen el límite del 40%
                                    ElseIf x.Concept.AffectLimit40Law1393 = True Then
                                        Return 2

                                        ' 4 - Conceptos que consumen IBCs en fórmulas
                                        '     IMPORTANTE: Se procesan DESPUÉS de tener IBCs correctos
                                    ElseIf x.Concept.Formulates.Contains("[IBC Salud]") OrElse
                                           x.Concept.Formulates.Contains("[IBC Pensión]") OrElse
                                           x.Concept.Formulates.Contains("[IBC ARP]") OrElse
                                           x.Concept.Formulates.Contains("[IBC Periodo]") Then
                                        Return 3

                                        ' 5 - Otros conceptos Type 1 (no salariales, no con límite 40%)
                                    Else
                                        Return 4
                                    End If

                                Else
                                    Return 5
                                End If
                            End Function) _
                    .ToList()

                ' IBC Primas, Cesantías, Vacaciones e Intereses cuando 0 días trabajados e incapacidades (para que conceptos patronales/provisiones usen la base)
                If DaysWorked = 0 AndAlso TotalEmployeeInabilityDays > 0 Then
                    If TotalPeriodDaysInabilities > 0 AndAlso ConceptoDevengadoSuma = 0 Then
                        Dim baseIBC As Decimal = 0
                        If BasicSalary > 0 Then
                            baseIBC = Math.Round((BasicSalary / 30) * PayrollDays, 0)
                        End If
                        If baseIBC > 0 Then
                            IBCSeverance = baseIBC
                            IBCVacation = baseIBC
                            IBCIncentivePayment = baseIBC
                        End If
                        If ContractVacationDays > 0 Then
                            IBCCompensationFund = VacationValue
                        End If
                    End If
                End If

                '============================================================================
                'LEY 1393/2010 - LÍMITE 40% BONIFICACIONES NO SALARIALES
                'Acumuladores para detectar si las bonificaciones no salariales superan el
                'límite del 40%. El ajuste se aplica JUSTO ANTES del primer concepto que
                'use [IBC Salud], [IBC Pensión] o [IBC ARP] en su fórmula, garantizando
                'que esos conceptos se calculen con los IBCs ya corregidos.
                'No aplica para Costa Rica (es-CR).
                '============================================================================
                Dim NonSalaryBonification As Decimal = 0   ' AffectLimit40=True, AffectIBC=False
                Dim valueAccrued As Decimal = 0             ' AffectIBC=True, AffectLimit40=False
                Dim AlreadyAppliedLaw1393 As Decimal = 0   ' Acumula lo ya sumado a los IBCs por Ley 1393

                For Each ConceptAuthorization As AuthorizationConcept In PayrollAuthorizationConcepts

                    Dim FormulaConcept = ConceptAuthorization.Concept.Formulates
                    Dim EmployeeLiquidatedDetail As New LiquidationDetail
                    Dim WorkHours As Decimal = 0

                    DistributeExpense = False

                    If ScheduleDetailEmployee IsNot Nothing Then
                        WorkHours = _functionsLiquidation.HourByConcept(HandlesTurnChart, PayrollDays, ConceptAuthorization, IBCHour, ScheduleDetailEmployee)
                        If WorkHours > 0 Then
                            DistributeExpense = True
                        End If

                    Else
                        WorkHours = 0
                    End If

                    If WorkHours = 0 Then
                        If NoveltyScheduleDetail IsNot Nothing AndAlso NoveltyScheduleDetail.Count > 0 Then
                            WorkHours = _functionsLiquidation.NoveltyHourByConcept(HandlesTurnChart, PayrollDays, ConceptAuthorization, IBCHour, NoveltyScheduleDetail)
                            If WorkHours > 0 Then
                                Dim TmpConcept = _conceptRepository.GetAdjustmentConceptId(ConceptAuthorization.ConceptId)
                                If TmpConcept IsNot Nothing Then
                                    FormulaConcept = TmpConcept.Formulates
                                Else
                                    ActionMessageResult.MessageResult.Add(New MessageResult("-008: Novedades Cuadros de Turno", "Se han agregado Novedades de ajustes de cuadro de turno, pero el Concepto " & ConceptAuthorization.Concept.Code & " - " & ConceptAuthorization.Concept.Name & " no tiene parametrizado el Concepto de Ajuste"))
                                    ActionMessageResult.StateResult = False
                                End If
                            End If

                        End If
                    End If

                    TotalDaysLicensesUnpaid = UnpaidLicensesDays

                    If ContractLearningFlag = False Then

                        ' Cargo la Base de Liquidación de Salud - Empleado
                        BaseIBCHealth = _functionsLiquidation.GetIBCHealth(IBCHealth, minimimunSalary, BasicSalary, ContractVacationDays, PayrollDays, EmployeeHealthContributionPercentage, ConceptoDevengadoSuma, TotalDaysLicensesUnpaid, IBCPension, NumberContract, PeriodDays, RetirementFlag, FlagIBCHealth)

                        'Cargo la Base de Liquidación de Pensión - Empleado
                        BaseIBCPension = _functionsLiquidation.GetIBCPension(BasicSalary, minimimunSalary, IBCPension, PayrollDays, TotalDaysLicensesUnpaid, ConceptoDevengadoSuma, EmployeePensionContributionPercentage, ContractVacationDays, RetirementFlag, IBCHealth, NumberContract, PeriodDays, TotalPeriodDaysInabilities, DaysWorkedEmployee, FlagIBCPension)
                        'IBCPension = Me.GetIBCPension(BasicSalary, legalMinimimunSalary, IBCPension, PayrollDays, TotalDaysLicensesUnpaid, ConceptoDevengadoSuma, EmployeePensionContributionPercentage, ContractVacationDays, RetirementFlag, IBCHealth, NumberContract, PeriodDays, TotalPeriodDaysInabilities, DaysWorkedEmployee)

                        ' Cargo la Base Parafiscal de la Caja de Compensación
                        BaseParafiscalCompensationFund = _functionsLiquidation.GetBaseParafiscalCompensationFund(IBCCompensationFund, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, CompensationFundContributionPercentage, NumberContract, PeriodDays, IBCPension)

                        ' Cargo la Base Parafiscal de la Caja de Compensación para Salarios Integrales
                        BaseParafiscalCompensationFundIntegral = _functionsLiquidation.GetBaseParafiscalCompensationFundIntegral(IBCCompensationFund, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, CompensationFundContributionPercentage, NumberContract, PeriodDays)


                        '' Exoneración por impuesto del CREE
                        If CreeTax = False Then
                            ' Cargo la Base Parafiscal del ICBF
                            BaseParafiscalICBF = _functionsLiquidation.GetBaseParafiscalICBF(IBCICBF, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage, Salary, NumberContract, PeriodDays)

                            ' Cargo la Base Parafiscal del ICBF para Salarios Integrales
                            BaseParafiscalICBFIntegral = _functionsLiquidation.GetBaseParafiscalICBFIntegral(IBCICBF, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage, NumberContract, PeriodDays)

                            ' Cargo la Base de Liquidación de Salud - Patrono
                            BaseIBCHealthEmployer = _functionsLiquidation.GetIBCHealthEmployer(minimimunSalary, ContractVacationDays, PayrollDays, BaseIBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid, IBCPension, BasicSalary, EmployerHealthContributionPercentage, NumberContract, PeriodDays, TotalPeriodDaysInabilities)

                            ' Cargo la Base de Liquidación de Salud INTEGRAL - Patrono      
                            BaseIBCHealthEmployerIntegral = _functionsLiquidation.GetIBCHealthEmployerIntegral(minimimunSalary, BasicSalary, PayrollDays, BaseIBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid, NumberContract, PeriodDays)

                            ' Cargo la Base Parafiscal del SENA para Salarios Integrales
                            BaseIBCSENAIntegral = _functionsLiquidation.GetBaseSenaIntegral(IBCSENA, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, SenaContributionPercentage, NumberContract, PeriodDays)

                            ' Cargo la Base Parafiscal del SENA
                            BaseIBCSENA = _functionsLiquidation.GetBaseSena(IBCSENA, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, SenaContributionPercentage, NumberContract, PeriodDays)
                        Else

                            If BasicSalary >= (10 * minimimunSalary) Then
                                ' Cargo la Base Parafiscal del ICBF
                                BaseParafiscalICBF = _functionsLiquidation.GetBaseParafiscalICBF(IBCICBF, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage, Salary, NumberContract, PeriodDays)

                                ' Cargo la Base Parafiscal del ICBF para Salarios Integrales
                                BaseParafiscalICBFIntegral = _functionsLiquidation.GetBaseParafiscalICBFIntegral(IBCICBF, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, ICBFContributionPercentage, NumberContract, PeriodDays)

                                ' Cargo la Base de Liquidación de Salud - Patrono
                                BaseIBCHealthEmployer = _functionsLiquidation.GetIBCHealthEmployer(minimimunSalary, VacationDays, PayrollDays, BaseIBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid, IBCPension, BasicSalary, EmployerHealthContributionPercentage, NumberContract, PeriodDays, TotalPeriodDaysInabilities)

                                ' Cargo la Base de Liquidación de Salud INTEGRAL - Patrono      
                                BaseIBCHealthEmployerIntegral = _functionsLiquidation.GetIBCHealthEmployerIntegral(minimimunSalary, BasicSalary, PayrollDays, BaseIBCHealth, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid, NumberContract, PeriodDays)

                                ' Cargo la Base Parafiscal del SENA para Salarios Integrales
                                BaseIBCSENAIntegral = _functionsLiquidation.GetBaseSenaIntegral(IBCSENA, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, SenaContributionPercentage, NumberContract, PeriodDays)

                                ' Cargo la Base Parafiscal del SENA
                                BaseIBCSENA = _functionsLiquidation.GetBaseSena(IBCSENA, PayrollDays, minimimunSalary, LicensesDays, UnpaidLicensesDays, HospitalInabilityDays, AmbulatoryInabilityDays, BasicSalary, VacationDays, SenaContributionPercentage, NumberContract, PeriodDays)
                            Else
                                BaseParafiscalICBF = 0
                                BaseParafiscalICBFIntegral = 0
                                BaseIBCHealthEmployer = 0
                                BaseIBCHealthEmployerIntegral = 0
                                BaseIBCSENAIntegral = 0
                                BaseIBCSENA = 0
                            End If
                        End If
                    Else
                        BaseIBCHealth = 0
                        BaseIBCPension = 0
                        BaseParafiscalCompensationFund = 0
                        BaseParafiscalCompensationFundIntegral = 0
                        BaseParafiscalICBF = 0
                        BaseIBCSENAIntegral = 0
                        BaseIBCSENA = 0
                        BaseParafiscalICBFIntegral = 0

                        ' Cargo la Base de Liquidación de Salud - Patrono
                        BaseIBCHealthEmployer = _functionsLiquidation.GetIBCHealthEmployer(minimimunSalary, VacationDays, PayrollDays, IBCHealthEmployer, EmployerHealthContributionPercentage, TotalDaysLicensesUnpaid, IBCPension, BasicSalary, EmployerHealthContributionPercentage, NumberContract, PeriodDays, TotalPeriodDaysInabilities)
                    End If

                    ' Cargo la Base de Liquidación de Pensión - Patrono
                    Dim BaseIBCPensionEmployer = _functionsLiquidation.GetIBCPensionEmployer(IBCPension, minimimunSalary, BasicSalary, UnpaidLicensesDays, PayrollDays, NumberContract, PeriodDays, TotalPeriodDaysInabilities, IBCHealth, DaysWorkedEmployee, ContractVacationDays)

                    ' Cargo la Base de Liquidación de Pensión INTEGRAL - Patrono
                    Dim BaseIBCPensionEmployerIntegral = _functionsLiquidation.GetIBCPensionEmployerIntegral(IBCPensionEmployer, minimimunSalary, BasicSalary, UnpaidLicensesDays, PayrollDays, NumberContract, PeriodDays)

                    ' Cargo la Base de ARP
                    Dim BaseIBCArp = _functionsLiquidation.GetBaseARP(BasicSalary, minimimunSalary, ARLDays, IBCARP, ProfessionalRiskPercentage)

                    ' Cargo la Base de ARP
                    Dim BaseIBCArpIntegral = _functionsLiquidation.GetBaseARPIntegral(BasicSalary, minimimunSalary, ARLDays, IBCARP, ProfessionalRiskPercentage)

                    ' Cargo la Base 
                    Dim BaseIBCSecurityPensionalIntegralFound = _functionsLiquidation.GetBaseSecurityPensionalIntegralFound(IBCHealth + VacationRTFValue, minimimunSalary, BasicSalary, WorkDays, IBCPension + VacationRTFValue)

                    Dim ConceptValue As Double

                    Dim ReplaceFormula As String

                    'Id del convenio
                    Dim AgreementsId As Integer = 0
                    Dim ManualConceptValue As Decimal = 0
                    Dim ThirdPartyAgreementsId As Integer = 0
                    Dim ThirdPartyAgreementsName As String = String.Empty
                    Dim NumAgreement As Integer = 0
                    Dim AgreementsDId As Integer = 0
                    Dim NumAgreementD As Integer = 0

                    'CONVENIOS
                    Dim StateAgreement = ""
                    If ConceptAuthorization.Concept.ConceptClass = "041" Then
                        If AgreementsList IsNot Nothing AndAlso AgreementsList.Count > 0 Then
                            Dim AnalisiAgreements = AgreementsList.Where(Function(x) x.ConceptId = ConceptAuthorization.ConceptId).ToList()
                            If AnalisiAgreements IsNot Nothing AndAlso AnalisiAgreements.Count > 0 Then
                                ManualConceptValue = _functionsLiquidation.AgreementsPaid(PayrollStarDate, PayrollEndDate, AnalisiAgreements, PaidCredit, VacationDays, NumAgreement, AgreementsDId, ThirdPartyAgreementsName, ThirdPartyAgreementsId, AgreementsId)
                                Dim LiquidatedAgreement = AnalisiAgreements.FirstOrDefault(Function(x) x.Id = AgreementsId)
                                If LiquidatedAgreement IsNot Nothing AndAlso LiquidatedAgreement.TermType = 1 Then
                                    NumAgreementD = 1
                                End If
                            End If
                        End If
                    End If

                    'CONCEPTOS MANUALES - HORA
                    Dim ManualConceptPaidHourFlag As Boolean
                    If ConceptAuthorization.Concept.ConceptClass = "001" Or ConceptAuthorization.Concept.ConceptClass = "005" Or ConceptAuthorization.Concept.ConceptClass = "012" Or ConceptAuthorization.Concept.ConceptClass = "013" Or ConceptAuthorization.Concept.ConceptClass = "042" Or ConceptAuthorization.Concept.ConceptClass = "043" Or ConceptAuthorization.Concept.ConceptClass = "050" Or ConceptAuthorization.Concept.ConceptClass = "052" Or ConceptAuthorization.Concept.ConceptClass = "051" Then
                        If ListManualConcept IsNot Nothing Then
                            Dim TmpListManualConceptHour = ListManualConcept.Where(Function(x) x.ConceptId = ConceptAuthorization.ConceptId).ToList()

                            If TmpListManualConceptHour IsNot Nothing AndAlso TmpListManualConceptHour.Count > 0 Then
                                WorkHours = WorkHours + (_functionsLiquidation.HourManualConcept(PayrollStarDate, PayrollEndDate, TmpListManualConceptHour, ManualConceptPaidHourFlag))

                            End If
                        End If
                    End If

                    Dim ManualConceptDeducted As ManualConcepts
                    Dim ManualConceptPaidFlag As Boolean

                    'CONCEPTOS MANUALES - DINERO
                    If ConceptAuthorization.Concept.ConceptClass <> "001" And ConceptAuthorization.Concept.ConceptClass <> "005" And ConceptAuthorization.Concept.ConceptClass <> "012" And ConceptAuthorization.Concept.ConceptClass <> "013" And ConceptAuthorization.Concept.ConceptClass <> "042" And ConceptAuthorization.Concept.ConceptClass <> "043" And ConceptAuthorization.Concept.ConceptClass <> "050" Or ConceptAuthorization.Concept.ConceptClass <> "052" Or ConceptAuthorization.Concept.ConceptClass <> "051" Then
                        If ListManualConcept IsNot Nothing Then
                            ManualConceptDeducted = ListManualConcept.Where(Function(x) x.ConceptId = ConceptAuthorization.ConceptId).FirstOrDefault()
                            If ManualConceptDeducted IsNot Nothing Then
                                ManualConceptValue = _functionsLiquidation.ValueManualConcept(PayrollStarDate, PayrollEndDate, ManualConceptDeducted, ManualConceptPaidFlag, QuarterFlag)

                                If ManualConceptDeducted.PaidEndContract = False Then
                                    Dim ListEvent = ListManualConcept.Where(Function(x) x.ConceptId = ConceptAuthorization.ConceptId).ToList()

                                    If ListEvent IsNot Nothing And ListEvent.Count > 1 Then
                                        ManualConceptValue = ListEvent.Sum(Function(x) x.QuoteValue)
                                    End If

                                End If

                            End If

                        End If
                    End If

                    'CUENTAS AFC
                    If ConceptAuthorization.Concept.ConceptClass = "045" Then
                        If ListManualConcept IsNot Nothing Then
                            ManualConceptDeducted = ListManualConcept.Where(Function(x) x.ConceptId = ConceptAuthorization.ConceptId).FirstOrDefault()
                            If ManualConceptDeducted IsNot Nothing Then
                                ManualConceptValue = _functionsLiquidation.ValueAFCManualConcept(PayrollStarDate, PayrollEndDate, ManualConceptDeducted, ManualConceptPaidFlag)
                            End If
                        End If
                        'Cargo los Conceptos Manuales de los Empleado
                        Dim ListManualConceptAFC As New List(Of ManualConcepts)
                        Dim dateInitialLiquidation = PayrollStarDate
                        If QuarterFlag = 2 Then
                            dateInitialLiquidation = New Date(PayrollStarDate.Year, PayrollStarDate.Month, 1)
                        End If
                        ListManualConceptAFC = _ManualConceptsRepository.GetManualConceptsByContractNumberPayrollDate(IIf(contractEmployee.InitialContractNumber = 0, contractEmployee.Id, contractEmployee.InitialContractNumber), dateInitialLiquidation, PayrollEndDate, 1, 6)
                        If ListManualConceptAFC IsNot Nothing AndAlso ListManualConceptAFC.Count > 0 Then
                            Dim ObjManualConceptAFC = ListManualConceptAFC.Where(Function(x) x.ConceptId = ConceptAuthorization.ConceptId).FirstOrDefault()
                            If ObjManualConceptAFC IsNot Nothing Then
                                AFCAccount = _functionsLiquidation.ValueAFCManualConcept(dateInitialLiquidation, PayrollEndDate, ObjManualConceptAFC, ManualConceptPaidFlag)
                            End If
                        End If
                    End If

                    'SINDICATO
                    Dim SindicateFlag = 0
                    Dim ThirdPartyIdSindicate As Integer? = 0
                    If EmployeeSindicate = 3 Then
                        If ObjEmployee.TradeUnionEmployee.Where(Function(x) x.TradeUnion.PayrollConceptId = ConceptAuthorization.ConceptId).Count() > 0 Then
                            SindicateFlag = 1
                            ThirdPartyIdSindicate = ObjEmployee.TradeUnionEmployee.Where(Function(x) x.TradeUnion.PayrollConceptId = ConceptAuthorization.ConceptId).FirstOrDefault().TradeUnion.IdThirdParty
                        End If
                    End If

                    'EMBARGOS
                    Dim PercentageForeclousure As Decimal = 0
                    Dim ValueForeclousure As Double = 0

                    If ConceptAuthorization.Concept.ConceptClass = "053" Then
                        Dim ForeclousureList = _foreclousureRepository.LisForeclousureByEmployeeIdStarDate(contractEmployee.EmployeeId, PayrollEndDate, ConceptAuthorization.ConceptId)

                        If ForeclousureList IsNot Nothing AndAlso ForeclousureList.Count > 0 Then
                            BaseForeclousure = IBCPeriod

                            ValueForeclousure = _functionsLiquidation.ValueForeclousure(PayrollStarDate, PayrollEndDate, ForeclousureList, PercentageForeclousure)
                        End If
                    End If

                    Dim endpayrolldate As Date = New Date(PayrollEndDate.Year, PayrollEndDate.Month, DateTime.DaysInMonth(PayrollEndDate.Year, PayrollEndDate.Month))
                    'Fondo de Solidaridad Pensional

                    If QuarterFlag = 1 AndAlso VacationPaidValue > 0 AndAlso VacationEndDate > endpayrolldate Then
                        If ConceptAuthorization.Concept.ConceptClass = "038" Then
                            TotalIBCSolidaridad = IBCVacation
                        End If

                    End If

                    If QuarterFlag = 2 Then
                        If ConceptAuthorization.Concept.ConceptClass = "038" AndAlso ListPastLiquidation IsNot Nothing Then
                            Dim ObjLiquidationTmp = ListPastLiquidation.Where(Function(x) x.EmployeeId = contractEmployee.EmployeeId).FirstOrDefault()
                            If ObjLiquidationTmp IsNot Nothing Then
                                If ObjLiquidationTmp.LiquidationDetail.Any(Function(x) x.Concept.AffectIBCPension = True) Then

                                    Dim AccruedLastQuarterIBCPension = ObjLiquidationTmp.LiquidationDetail.Where(Function(x) x.Concept.AffectIBCPension = True And x.ConceptType = 1).Sum(Function(x) x.ConceptTotalValue)
                                    Dim DeductedLastQuarterIBCPension = ObjLiquidationTmp.LiquidationDetail.Where(Function(x) x.Concept.AffectIBCPension = True And x.ConceptType = 2).Sum(Function(x) x.ConceptTotalValue)

                                    If DaysWorked > 0 Then
                                        TotalIBCSolidaridad = IBCPension + (AccruedLastQuarterIBCPension - DeductedLastQuarterIBCPension)
                                    Else
                                        TotalIBCSolidaridad = 0

                                    End If

                                End If
                            End If
                        End If
                    End If

                    'RETEFUENTE

                    Dim TotalRentasExcentas As Double = 0
                    Dim SubtotalA As Double = 0
                    Dim TotalDeducciones As Double = 0
                    Dim SubTotalB As Double = 0
                    Dim BaseGrabable As Double = 0
                    Dim Retention383 As Double = 0
                    Dim Retention384 As Double = 0
                    Dim ArticuloUsado As Integer = 0
                    Dim PorcentajeUsado As Decimal = 0
                    Dim TotalIngresosNoConstitutivos As Double = 0
                    Dim Subtotal1 As Double = 0
                    Dim SubTotal2 As Double = 0
                    Dim SubTotal3 As Double = 0
                    Dim SubTotal4 As Double = 0
                    Dim RangoInicial As Double = 0
                    Dim RangoInicialPorcentaje As Double = 0
                    Dim AcumuladoRangos As Double = 0
                    Dim messageFlag As Boolean = False

                    If ConceptAuthorization.Concept.Code = "701" Then
                        Dim RetentionTuple As List(Of Tuple(Of String, Double))
                        Dim ProcedureType As Byte = ObjEmployee.ProcedureTypeRTF
                        Dim employeeWithRelationShips = _employeeRepository.GetEmployeeRelationshipByEmployeeId(ObjEmployee.Id)
                        Dim relationshipEmployee = employeeWithRelationShips.Relationship.Where(Function(x) x.Dependent).ToList()

                        If relationshipEmployee.Any() Then
                            If relationshipEmployee.Any(Function(x) x.DependsType IsNot Nothing) Then
                                If relationshipEmployee.Any(Function(x) x.DependsType = 1) Then
                                    ValDependsValue = (From p In relationshipEmployee Where p.DependsType = 1 Select p.DependsValue).Sum()
                                End If

                                If relationshipEmployee.Any(Function(x) x.DependsType = 2) Then
                                    Dim PercentajeValue As Decimal = (From p In relationshipEmployee Where p.DependsType = 2 Select p.DependsPercentage).Sum()

                                    If groupEmployee.Liquidation = 2 Then
                                        ValDependsValue = ValDependsValue + (contractEmployee.BasicSalary * (PercentajeValue / 100))
                                    Else
                                        ValDependsValue = ValDependsValue + (ConceptoDevengadoSuma * (PercentajeValue / 100))
                                    End If
                                End If
                            End If
                        End If
                        ValDependentSupplement = ObjEmployee.SupplementaryPension
                        'Se valida que este en la primera quincena y si el parametro de descuento de retención en la fuente por quincena esta marcado como True
                        'No debe guardar los descuentos de deduccion para dependientes ni pension suplementaria
                        If QuarterFlag = 1 And PayrollSettings.WithholdingBiweeklyDiscount Then
                            ValDependentSupplement = 0
                            ValDependsValue = 0

                        End If

                        'Si el empleado sale a vacaciones este mes y se le han pagado las vacaciones por Pago Inmediato, la retención va sobre lo salarial del mes + el valor de las vacaciones sin descuento
                        If VacationPaidType = 1 And Month(VacationInitialDate) = Month(PayrollStarDate) And Year(VacationInitialDate) = Year(PayrollStarDate) Then
                            IBCRTF = IBCRTF + VacationRTFValue
                            HealthEmployee = HealthEmployee + VacationHealthRTF
                            PensionEmployee = PensionEmployee + VacationPensionRTF

                            Dim PercentajeValue As Decimal = (From p As Relationship In relationshipEmployee Where p.DependsType = 2 Select p.DependsPercentage).Sum()

                            If PercentajeValue > 0 Then
                                ValDependsValue = ((ConceptoDevengadoSuma + VacationRTFValue) * (PercentajeValue / 100))
                            Else
                                ValDependsValue = (From p As Relationship In relationshipEmployee Where p.DependsType = 1 Select p.DependsValue).Sum()
                            End If
                        End If


                        Dim FractionRetention As Boolean = False
                        Dim PercentageFractionRetention As Decimal = 0

                        If PayrollSettings.RetentionFraction AndAlso (contractEmployee.JobBondingDate >= PayrollStarDate And contractEmployee.JobBondingDate <= PayrollEndDate) Then
                            'Es Retención por Fracción
                            FractionRetention = True
                            RetentionTuple = Me.Retention(BasicSalary, BasicSalary * (EmployeePensionContributionPercentage / 100), VoluntaryPension, BasicSalary * 0.01, AFCAccount, 0, BasicSalary * (EmployeeHealthContributionPercentage / 100), VoluntaryHealth, ValDependsValue, HousingDeducted, UVTValue, RTFExcempt, minimimunSalary, ProcedureType, contractEmployee, ObjEmployee, PayrollEndDate, ValueRepresentationCost, PayrollSettings, ListLiquidationLastYear, 0, False, FractionRetention, PercentageFractionRetention)
                        End If

                        Dim IBCRTFLastQuarter As Decimal = 0
                        Dim PensionEmployeeLastQuarter As Decimal = 0
                        Dim ContributionPensionSolidarityFundLastQuarter As Decimal = 0
                        Dim HealthEmployeeLastQuarter As Decimal = 0


                        If QuarterFlag = 2 Then
                            'Aplica para la Segunda Quincena
                            If ListPastLiquidation?.Any() Then
                                Dim objLiquidationTmp = ListPastLiquidation.Where(Function(x) x.EmployeeId = contractEmployee.EmployeeId).FirstOrDefault()

                                If objLiquidationTmp IsNot Nothing Then

                                    If objLiquidationTmp.LiquidationDetail.Any(Function(x) x.ConceptClass = "038") Then
                                        If DaysWorked > 0 Then
                                            ContributionPensionSolidarityFundLastQuarter = ContributionPensionSolidarityFundLastQuarter + objLiquidationTmp.LiquidationDetail.Where(Function(x) x.ConceptClass = "038").Sum(Function(y) y.ConceptTotalValue)
                                        Else
                                            ContributionPensionSolidarityFundLastQuarter = 0
                                        End If
                                    End If

                                    If objLiquidationTmp.LiquidationDetail.Any(Function(x) x.ConceptClass = "014") Then
                                        PensionEmployeeLastQuarter = PensionEmployeeLastQuarter + objLiquidationTmp.LiquidationDetail.Where(Function(x) x.ConceptClass = "014").Sum(Function(y) y.ConceptTotalValue)
                                    End If

                                    If objLiquidationTmp.LiquidationDetail.Any(Function(x) x.ConceptClass = "017") Then
                                        HealthEmployeeLastQuarter = HealthEmployeeLastQuarter + objLiquidationTmp.LiquidationDetail.Where(Function(x) x.ConceptClass = "017").Sum(Function(y) y.ConceptTotalValue)
                                    End If

                                    If objLiquidationTmp.LiquidationDetail.Any(Function(x) x.Concept.AffectIBCRTF = True) Then

                                        Dim AccruedLastQuarterIBC = objLiquidationTmp.LiquidationDetail.Where(Function(x) x.Concept.AffectIBCRTF = True And x.ConceptType = 1).Sum(Function(x) x.ConceptTotalValue)
                                        ' Excluir clase "038" (FSP) para no doble-descontar (ya entra como INCRNGO en Retention)
                                        Dim DeductedLastQuarterIBC = objLiquidationTmp.LiquidationDetail.Where(Function(x) x.Concept.AffectIBCRTF = True And x.ConceptType = 2 And x.Concept.ConceptClass <> "038").Sum(Function(x) x.ConceptTotalValue)

                                        IBCRTFLastQuarter = AccruedLastQuarterIBC - DeductedLastQuarterIBC
                                    End If

                                End If

                            End If
                        End If

                        If QuarterFlag > 1 OrElse (PayrollSettings.WithholdingBiweeklyDiscount AndAlso QuarterFlag = 1) Then
                            Dim IBCRTFsaved = IBCRTF

                            If QuarterFlag = 1 Then
                                ' Si es la primera quincena duplicamos el salario para calcular las retenciones
                                IBCRTF = IBCRTF + (BasicSalary / 2)
                            End If

                            RetentionTuple = Me.Retention(
                                IBCRTF:=IBCRTF + IBCRTFLastQuarter,
                                ObligatoryPensionEmployee:=PensionEmployee + PensionEmployeeLastQuarter,
                                VoluntaryPensionEmployee:=VoluntaryPension,
                                PensionalSolidarityFund:=ContributionPensionSolidarityFund + ContributionPensionSolidarityFundLastQuarter,
                                CuentasAFC:=AFCAccount,
                                RiskValue:=0,
                                ObligatoryHealthEmployee:=HealthEmployee + HealthEmployeeLastQuarter,
                                PrepaidVolunatyrHealthEmployee:=VoluntaryHealth,
                                Dependientes:=ValDependsValue,
                                HousingDeductedValue:=HousingDeducted,
                                UVTValue:=UVTValue,
                                ExceptRTF:=RTFExcempt,
                                LegalMinimunSalary:=minimimunSalary,
                                RetentionProcedure:=ProcedureType,
                                Contract:=contractEmployee,
                                Employee:=ObjEmployee,
                                PayrollEndDate:=PayrollEndDate,
                                RepresentationCost:=ValueRepresentationCost,
                                PayrollSettings:=PayrollSettings,
                                ListLiquidationLastYear:=ListLiquidationLastYear,
                                CompanySettings:=False,
                                UnemployementValue:=0,
                                flagIncentivePayment:=False,
                                FractionRetencion:=FractionRetention,
                                PercentageFractionRetention:=PercentageFractionRetention,
                                QuarterFlag:=QuarterFlag
                            )

                            Dim valRetencion = If(RetentionTuple.FirstOrDefault(Function(m) m.Item1 = "Retenciones")?.Item2, 0)

                            If QuarterFlag = 1 Then
                                valRetencion = valRetencion / 2
                            ElseIf QuarterFlag = 2 AndAlso PayrollSettings.WithholdingBiweeklyDiscount Then
                                ' Consultamos la retencion liquidada en la p primera quincena
                                Dim employeeId = contractEmployee.EmployeeId
                                Dim lastLiquidation = ListPastLiquidation?.Where(Function(x) x.EmployeeId = employeeId).FirstOrDefault()

                                Dim liquidationDetail = lastLiquidation?.LiquidationDetail.FirstOrDefault(Function(m) m.ConceptClass = "020")
                                If liquidationDetail IsNot Nothing Then
                                    If valRetencion < 0 Then
                                        valRetencion = 0
                                    End If

                                    valRetencion -= liquidationDetail.ConceptTotalValue
                                    If valRetencion < 0 Then
                                        Dim detail = CreateWithholdingTaxRefundDetail(
                                            contractEmployee:=contractEmployee,
                                            ThirdPartyAgreementsId:=ThirdPartyAgreementsId,
                                            ThirdPartyCompensationFundId:=ThirdPartyCompensationFundId,
                                            ThirdPartyUnemploymentFundId:=ThirdPartyUnemploymentFundId,
                                            NumAgreement:=NumAgreement,
                                            PayrollEndDate:=PayrollEndDate,
                                            PublicClient:=PublicClient,
                                            WorkHours:=WorkHours,
                                            EmployeeThirdPartyId:=EmployeeThirdPartyId,
                                            ThirdPartyPensionFundId:=ThirdPartyPensionFundId,
                                            ThirdPartyHealthFundId:=ThirdPartyHealthFundId,
                                            ThirdPartyVoluntaryPensionFundId:=ThirdPartyVoluntaryPensionFundId,
                                            ThirdPartyVoluntaryHealthFundId:=ThirdPartyVoluntaryHealthFundId,
                                            ThirdPartyIdSindicate:=ThirdPartyIdSindicate,
                                            ThirdPartyARLFundId:=ThirdPartyARLFundId,
                                            AgreementsId:=AgreementsId,
                                            AgreementsDId:=AgreementsDId,
                                            ConceptValue:=-1 * valRetencion,
                                            RetirementFlag:=RetirementFlag,
                                            FlagIncentivePayment:=FlagIncentivePayment,
                                            TotalSpendingInabilityAmbulatory:=TotalSpendingInabilityAmbulatory,
                                            TotalInabilityCollectAmbulatory:=TotalInabilityCollectAmbulatory,
                                            TotalSpendingInabilityHospital:=TotalSpendingInabilityHospital,
                                            TotalInabilityCollectHospital:=TotalInabilityCollectHospital,
                                            TotalInabilityCollectMaternity:=TotalInabilityCollectMaternity,
                                            TotalSpendingInabilityMaternity:=TotalSpendingInabilityMaternity,
                                            TotalSpendingInabilityPaternity:=TotalSpendingInabilityPaternity,
                                            TotalInabilityCollectPaternity:=TotalInabilityCollectPaternity,
                                            TotalSpendingInabilityProfessionalRisk:=TotalSpendingInabilityProfessionalRisk,
                                            TotalInabilityCollectProfessionalRisk:=TotalInabilityCollectProfessionalRisk,
                                            TotalSpendingInabilityLuto:=TotalSpendingInabilityLuto,
                                            TotalInabilityCollectLuto:=TotalInabilityCollectLuto,
                                            PensionFundName:=PensionFundName,
                                            PensionVoluntaryFundName:=PensionVoluntaryFundName,
                                            HealthFundName:=HealthFundName,
                                            HealthVoluntaryFundName:=HealthVoluntaryFundName,
                                            ThirdPartyAgreementsName:=ThirdPartyAgreementsName,
                                            SENAThirdParty:=SENAThirdParty,
                                            ICBFThirdParty:=ICBFThirdParty,
                                            PayrollSettings:=PayrollSettings
                                        )
                                        EmployeeLiquidated.LiquidationDetail.Add(detail)
                                        valRetencion = 0
                                    End If
                                End If
                            End If

                            IBCRTF = IBCRTFsaved

                            For Each ObjTuple As Tuple(Of String, Double) In RetentionTuple
                                If ObjTuple.Item1 = "TotalRentasExcentas" Then
                                    TotalRentasExcentas = Math.Round(ObjTuple.Item2, 0)
                                    EmployeeLiquidated.ExemptIncome = TotalRentasExcentas
                                End If
                                If ObjTuple.Item1 = "Retenciones" Then
                                    RetentionValue = valRetencion
                                End If
                                If ObjTuple.Item1 = "SubtotalA" Then
                                    SubtotalA = Math.Round(ObjTuple.Item2, 0)
                                End If
                                If ObjTuple.Item1 = "TotalDeducciones" Then
                                    TotalDeducciones = Math.Round(ObjTuple.Item2, 0)
                                End If
                                If ObjTuple.Item1 = "MenosRenta" Then
                                    MenosRenta = Math.Round(ObjTuple.Item2, 0)
                                End If
                                If ObjTuple.Item1 = "BaseGrabable" Then
                                    BaseGrabable = Math.Round(ObjTuple.Item2, 0)
                                    EmployeeLiquidated.TaxBase = BaseGrabable
                                End If
                                If ObjTuple.Item1 = "Retention383" Then
                                    Retention383 = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "Retention384" Then
                                    Retention384 = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "SubTotalB" Then
                                    SubTotalB = Math.Round(ObjTuple.Item2, 0)
                                End If
                                If ObjTuple.Item1 = "PorcentajeRetencion" Then
                                    PorcentajeUsado = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "IBCRTF" Then
                                    PorcentajeUsado = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "TotalIngresosNoConstitutivos" Then
                                    TotalIngresosNoConstitutivos = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "SubTotal1" Then
                                    Subtotal1 = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "SubTotal2" Then
                                    SubTotal2 = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "SubTotal3" Then
                                    SubTotal3 = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "SubTotal4" Then
                                    SubTotal4 = ObjTuple.Item2
                                    EmployeeLiquidated.Subtotal = SubTotal4
                                End If
                                If ObjTuple.Item1 = "TotalRentasExentasyDeducciones" Then
                                    TotalRentasExentasyDeducciones = ObjTuple.Item2
                                    EmployeeLiquidated.TotalExemptIncomeandDeductions = TotalRentasExentasyDeducciones
                                End If
                                If ObjTuple.Item1 = "Flag" Then
                                    messageFlag = True
                                End If
                                If ObjTuple.Item1 = "RangoInicial" Then
                                    RangoInicial = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "RangoInicialPorcentaje" Then
                                    RangoInicialPorcentaje = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "AcumuladoRangos" Then
                                    AcumuladoRangos = ObjTuple.Item2
                                End If
                                If ObjTuple.Item1 = "AccumulatedExemptIncomeControl" Then
                                    EmployeeLiquidated.ExemptIncomeControl = CDec(ObjTuple.Item2)
                                End If
                                If ObjTuple.Item1 = "AccumulatedMaxDeductionsControl" Then
                                    EmployeeLiquidated.TotalExemptIncomeandDeductionsControl = CDec(ObjTuple.Item2)
                                End If

                            Next
                        End If

                        If RetentionValue > 0 Then
                            If messageFlag Then
                                MessageLiquitadion = CreateMessage("RETENCIONES: Se calculó así: Ingresos Mes = " & IBCRTF + IBCRTFLastQuarter & " Menos el rango inicial de renta = " & RangoInicial & " por el porcentaje de renta de " & RangoInicialPorcentaje & "% Mas el acumulado de renta" & AcumuladoRangos & " Menos Creditos Fiscales " & TotalDeducciones, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)
                            Else
                                MessageLiquitadion = CreateMessage("Para el cálculo de la Retención en la Fuente se usó el Artículo " & IIf(MessageRetention = 0, "383", "384"), False, PayrollEndDate)
                                If MessageRetention = 0 Then
                                    ArticuloUsado = 1
                                Else
                                    ArticuloUsado = 2
                                End If

                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                MessageLiquitadion = CreateMessage("RETENCIONES: Se utilizó el Procedimiento " & ProcedureType, False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                If ProcedureType = 2 Then
                                    MessageLiquitadion = CreateMessage("RETENCIONES: Se calculó así: Ingresos Mes = " & IBCRTF + IBCRTFLastQuarter & " Menos Ingresos No Constitutivos de Renta = " & TotalIngresosNoConstitutivos & " Subtotal (1) = " & Subtotal1 & " Menos Deducciones " & TotalDeducciones & " Subtotal (2) = " & SubTotal2 & " Menos Renta Exenta = " & TotalRentasExcentas & " Subtotal (3) = " & SubTotal3 & " Renta de Trabajo Excenta " & MenosRenta & " Subtotal (4) = " & SubTotal4 & " Total Rentas Exentas y Deducciones = " & TotalRentasExentasyDeducciones & " Base Gravable = " & BaseGrabable & " Art. 383 = " & Retention383 & " Valor UVT = " & UVTValue & " Porcentaje Usado = " & PorcentajeUsado, False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)

                                    PercentageRetention = RTFExcempt
                                Else
                                    MessageLiquitadion = CreateMessage("RETENCIONES: Se calculó así: Ingresos Mes = " & IBCRTF + IBCRTFLastQuarter & " Menos Ingresos No Constitutivos de Renta = " & TotalIngresosNoConstitutivos & " Subtotal (1) = " & Subtotal1 & " Menos Deducciones " & TotalDeducciones & " Subtotal (2) = " & SubTotal2 & " Menos Renta Exenta = " & TotalRentasExcentas & " Subtotal (3) = " & SubTotal3 & " Renta de Trabajo Excenta " & MenosRenta & " Subtotal (4) = " & SubTotal4 & " Total Rentas Exentas y Deducciones = " & TotalRentasExentasyDeducciones & " Base Gravable = " & BaseGrabable & " Art. 383 = " & Retention383 & " Valor UVT = " & UVTValue, False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                End If
                            End If
                        End If

                    End If

                    Dim OriginalFormulaConcept As String = FormulaConcept  ' ← GUARDAR ORIGINAL

                    If FormulaConcept.Contains(ConceptFormulatePrefix) Then
                        FormulaConcept = ReplaceConceptAsVariable(FormulaConcept, PayrollAuthorizationConcept)
                    End If

                    '------------------------------------------------------------------------
                    'LEY 1393/2010: Si este concepto usa IBCs en su fórmula, aplicar el
                    'ajuste AHORA antes de calcularlo, sumando solo el DELTA respecto a lo
                    'que ya se aplicó. Así funciona correctamente sin importar el orden:
                    '  - Bonif. antes de Salud  - aplica aquí el total
                    '  - Bonif. después de Salud - aplica el fallback post-loop
                    '  - Bonif. parcial antes/después - aplica delta en cada paso
                    '------------------------------------------------------------------------
                    ' LEY 1393/2010: Si este concepto consume IBCs en su fórmula, ajustar
                    ' los IBCs con el delta pendiente ANTES de calcularlo.
                    ' El mensaje se emite una sola vez en el bloque post-loop.
                    If SessionValues.LanguageCulture <> "es-CR" AndAlso NonSalaryBonification > 0 Then
                        If FormulaConcept.Contains("[IBC Salud]") OrElse
                           FormulaConcept.Contains("[IBC Pensión]") OrElse
                           FormulaConcept.Contains("[IBC ARP]") OrElse
                           FormulaConcept.Contains("[IBC Periodo]") Then

                            Dim TotalSum1393 As Decimal = valueAccrued + NonSalaryBonification
                            Dim Limit40Percent1393 As Decimal = TotalSum1393 * 0.4D
                            Dim NewDifference1393 As Decimal = NonSalaryBonification - Limit40Percent1393
                            Dim ToAdd1393 As Decimal = NewDifference1393 - AlreadyAppliedLaw1393

                            If ToAdd1393 > 0 Then
                                IBCHealth = IBCHealth + ToAdd1393
                                IBCPension = IBCPension + ToAdd1393
                                IBCARP = IBCARP + ToAdd1393
                                IBCPeriod = IBCPeriod + ToAdd1393
                                AlreadyAppliedLaw1393 = NewDifference1393
                            End If
                        End If
                    End If

                    Dim ConceptData = Me.ReplaceDataLiquidation(groupEmployee.PayrollParameter, BasicSalary, WorkDays, WorkHours, PayrollDays, FormulaConcept, contractEmployee, BaseIBCPension, BaseIBCHealth, IBCHealth, BaseIBCHealthEmployer, BaseIBCHealthEmployerIntegral, BaseIBCPensionEmployer, BaseIBCPensionEmployerIntegral, IBCSENA, IBCICBF,
                                                        IBCPeriod, IBCSeverance, IBCCompensationFund, IBCPension, IBCARP, ValueAmbulatoryInability, ValueHospitalInability, ValueMaternity, ValueSanction, TotalPeriodDaysInabilities, HospitalInabilityDays,
                                                        LicensesDays, FamilyDay, UnpaidLicensesDays, ValueUnpaidLicenses, ValueProfesionalInabilities, HealthEmployee, PensionEmployee, BaseIBCArp, BaseIBCArpIntegral, BaseParafiscalCompensationFund, BaseParafiscalCompensationFundIntegral, BaseParafiscalICBF, BaseParafiscalICBFIntegral, RetentionValue, ProvisionDays,
                                                        ProfessionalRiskPercentage, VacationValue, AdjustVacationValue, incentiePaymentValue, unemployedInterestValue, transportHelpValue, BaseIBCSENAIntegral, BaseIBCSENA, ConceptoDevengadoSuma, ConceptoDeducidoSuma, BaseIBCSecurityPensionalIntegralFound, CalamidadDays, VoluntaryPensionValue, VoluntaryHealthValue, IBCIncentivePayment,
                                                        BonusServices, ManualConceptValue, SindicateFlag, PaidVacation, RepresentationCost, PaidValueAverageIncentiveServices, IBCHour, PayrollEndDate, contractEmployee.JobBondingDate, NumberContract, ValueGeneralInability, ValueLuto, ValuePaternity, TotalIBCSolidaridad, CodeEmployeeType, AnoLaborado, BonificationValue, TotalVacationDays, ContractVacationDays, ContractItem,
                                                        ValueRetroactiveBonification, RecreationBonificationValue, VacationalIncrease, VacationIncentivePayment, VacationCompensationValue, PaidCredit, minHourAmount, maxHourAmount, IBCVacation, DecemberIncentivePaymentAverage, IncentivePaymentAverage, VacationIncentiveValue, ValueForeclousure, PercentageForeclousure, codeWorkCenter, HolidaysHours, DailyBasicSalary,
                                                        AjusteDominical, AjusteHorasExtrasDiurnas, AjusteHorasExtrasNocturnas, AjusteHorasExtrasDiurnasFestivas, AjusteHorasExtrasNocturnasFestivas, AjusteRecargoNocturnasFestivas, VacationIBCValue, VacationPaidValue, ContractVacationDays, BaseForeclousure, PosesionDate, ValueRetroactivePaid, TransportDays, AjusteDominicalEventoPasado, AjusteHoraExtraFestivaDiurnaEventoPasado,
                                                        AjusteHoraExtraFestivaNocturnaEventoPasado, AjusteHorasExtrasNocturnas, AjusteHorasExtrasDiurnas, QuarterFlag, ValueRepresentationCost, VacationIncentiveValueProvision, SanctionsDays, ContributorAbroad, IBCLastPeriod, AmbulatoryInabilityEmployeerDays, AmbulatoryInabilityERPDays, ValueAmbulatoryEmployeerInability, ValueAmbulatoryERPInability, HospitalInabilityEmployeerDays,
                                                        HospitalInabilityERPDays, ValueHospitalEmployeerInability, ValueHospitalERPInability, DaysPermission, PaidLeaveDays, VacationDaysInCash,
                                                        LutoDays, EmployerProfessionalDisabilityDays, EmployerProfessionalDisabilityAmount, ERPProfessionalDisabilityDays, ERPProfessionalDisabilityAmount, ImmediateVacationValue, ImmediateVacationBonificationValue, ImmediateVacationIncentivePaymentValue, ImmediateVacationalIncreaseValue)

                    ReplaceFormula = ConceptData("Formula")
                    ConceptValue = CDbl(ConceptData("Valor"))
                    ConceptAuthorization.ConceptValue = ConceptValue

                    ' Acumular para Ley 1393 con el valor real ya calculado
                    If SessionValues.LanguageCulture <> "es-CR" Then
                        If ConceptAuthorization.Concept.AffectLimit40Law1393 = True AndAlso
                           ConceptAuthorization.Concept.AffectIBC = False Then
                            NonSalaryBonification += CDec(ConceptValue)
                        ElseIf ConceptAuthorization.Concept.AffectIBC = True AndAlso
                               ConceptAuthorization.Concept.AffectLimit40Law1393 = False Then
                            valueAccrued += CDec(ConceptValue)
                        End If
                    End If


                    If ConceptValue > 0 Then

                        ConceptValue = Math.Round(ConceptValue, 0)

                        ''Porcentaje Descuento Estudio y Salud
                        If ConceptAuthorization.Concept.ConceptClass = "017" Or ConceptAuthorization.Concept.ConceptClass = "039" Then
                            If (ConceptoDevengadoSuma * EducationStudyDiscountPercentage) / 100 < ConceptoDeducidoSuma Then
                                If ConceptAuthorization.Concept.ConceptClass = "017" Then
                                    MessageLiquitadion = CreateMessage("Para el concepto " & ConceptAuthorization.Concept.Name & " sobrepasa el % de Deducción de Salud", False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                Else
                                    MessageLiquitadion = CreateMessage("Para el concepto " & ConceptAuthorization.Concept.Name & " sobrepasa el % de Deducción de Educación", False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                End If

                            End If


                        End If

                        'Deducción maxima por vivienda
                        If ConceptAuthorization.Concept.ConceptClass = "040" Then
                            If HousingDeductionMaximumValue < ConceptValue Then
                                MessageLiquitadion = CreateMessage("Para el concepto " & ConceptAuthorization.Concept.Name & " sobrepasa el valor de Deducción Maximo por Vivienda", False, PayrollEndDate)
                                EmployeeLiquidated.Message.Add(MessageLiquitadion)
                            End If
                        End If

                        'SMLV maximos para liquidar salud y pension
                        If ConceptAuthorization.Concept.ConceptClass = "014" Or ConceptAuthorization.Concept.ConceptClass = "017" Then
                            If HealthContributionMaximunSalary < (ConceptoDevengadoSuma / minimimunSalary) Then
                                If ConceptAuthorization.Concept.ConceptClass = "014" Then
                                    MessageLiquitadion = CreateMessage("Para el concepto " & ConceptAuthorization.Concept.Name & " sobrepasa el número de SMLV maximos para liquidar pensión", False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                Else
                                    MessageLiquitadion = CreateMessage("Para el concepto " & ConceptAuthorization.Concept.Name & " sobrepasa el número de SMLV maximos para liquidar salud", False, PayrollEndDate)
                                    EmployeeLiquidated.Message.Add(MessageLiquitadion)
                                End If
                            End If
                        End If



                        'Horas Extras
                        If ConceptAuthorization.Concept.ConceptClass = "001" Then HourSchedule = HourSchedule + ConceptValue

                        'Prima de Servicios
                        If ConceptAuthorization.Concept.ConceptClass = "002" Then IncentivePaymentServices = IncentivePaymentServices + ConceptValue

                        'otras nummaxpri
                        If ConceptAuthorization.Concept.ConceptClass = "003" Then IncetivePaymentServicesExtra = IncetivePaymentServicesExtra + ConceptValue

                        ' Bonificación por Servicios
                        If ConceptAuthorization.Concept.ConceptClass = "004" Then BonusServices = BonusServices + ConceptValue

                        ' Sueldo
                        If ConceptAuthorization.Concept.ConceptClass = "005" Then Salary = Salary + ConceptValue

                        ' Acumulado Auxilio de Transporte
                        If ConceptAuthorization.Concept.ConceptClass = "006" Then AcumulatedHelpTransport = AcumulatedHelpTransport + ConceptValue

                        'Indemnizaciones()
                        If ConceptAuthorization.Concept.ConceptClass = "007" Then Compensation = Compensation + ConceptValue

                        'Provision de Cesantias
                        If ConceptAuthorization.Concept.ConceptClass = "008" Then Unemployment = Unemployment + ConceptValue

                        'Aporte Riesgos
                        If ConceptAuthorization.Concept.ConceptClass = "009" Then RiskContribution = RiskContribution + ConceptValue

                        'Otros Devengados
                        If ConceptAuthorization.Concept.ConceptClass = "010" Then OtherAccrued = OtherAccrued + ConceptValue

                        'Otros Deducidos
                        If ConceptAuthorization.Concept.ConceptClass = "011" Then OtherDeducted = OtherDeducted + ConceptValue

                        'Horas Extras Nocturnas
                        If ConceptAuthorization.Concept.ConceptClass = "012" Then EveningOvertime = EveningOvertime + ConceptValue

                        'Horas Extras Dominicales
                        If ConceptAuthorization.Concept.ConceptClass = "013" Then SundayOvertime = SundayOvertime + ConceptValue

                        'Pensión Empleado
                        If ConceptAuthorization.Concept.ConceptClass = "014" Then PensionEmployee = PensionEmployee + ConceptValue

                        'Pensión Patrono
                        If ConceptAuthorization.Concept.ConceptClass = "015" Then PensionEmployer = PensionEmployer + ConceptValue

                        'Pensión Voluntaria
                        If ConceptAuthorization.Concept.ConceptClass = "016" Then VoluntaryPension = VoluntaryPension + ConceptValue

                        'Salud Empleado
                        If ConceptAuthorization.Concept.ConceptClass = "017" Then HealthEmployee = HealthEmployee + ConceptValue

                        'Salud Patrono
                        If ConceptAuthorization.Concept.ConceptClass = "018" Then HealthEmployer = HealthEmployer + ConceptValue

                        'Salud Voluntaria
                        If ConceptAuthorization.Concept.ConceptClass = "019" Then VoluntaryHealth = VoluntaryHealth + ConceptValue

                        'Retención en la fuente
                        If ConceptAuthorization.Concept.ConceptClass = "020" Then Withholding = Withholding + ConceptValue

                        'Incapacidad Ambulatoria
                        Select Case ConceptAuthorization.Concept.ConceptClass
                            Case "021", "067", "068"
                                AmbulatoryInability += ConceptValue
                        End Select


                        'Incapacidad Hospitalaria
                        Select Case ConceptAuthorization.Concept.ConceptClass
                            Case "022", "069", "070"
                                HospitalInability += ConceptValue
                        End Select


                        'Maternidad
                        If ConceptAuthorization.Concept.ConceptClass = "023" Then Maternity = Maternity + ConceptValue

                        'Licencias
                        If ConceptAuthorization.Concept.ConceptClass = "024" Then Licenses = Licenses + ConceptValue

                        'Licencias No Remuneradas
                        If ConceptAuthorization.Concept.ConceptClass = "025" Then UnpaidLicenses = UnpaidLicenses + ConceptValue

                        'Sanciones
                        If ConceptAuthorization.Concept.ConceptClass = "026" Then Sanctions = Sanctions + ConceptValue

                        'Incapacidad Riesgos Profesionales
                        Select Case ConceptAuthorization.Concept.ConceptClass
                            Case "027", "075", "076"
                                InabilityProfessionalRisk = InabilityProfessionalRisk + ConceptValue
                        End Select
                        ''If ConceptAuthorization.Concept.ConceptClass = "027" Then InabilityProfessionalRisk = InabilityProfessionalRisk + ConceptValue

                        'Permisos
                        If ConceptAuthorization.Concept.ConceptClass = "028" Then Permission = Permission + ConceptValue

                        'Vacaciones
                        If ConceptAuthorization.Concept.ConceptClass = "030" Then Vacation = Vacation + ConceptValue

                        'Vacaciones en dinero
                        If ConceptAuthorization.Concept.ConceptClass = "073" Then VacationCompensationValue = VacationCompensationValue + ConceptValue

                        'Provisión Vacaciones
                        If ConceptAuthorization.Concept.ConceptClass = "031" Then ProvisionVacation = ProvisionVacation + ConceptValue

                        'Provisión Número Máximo de Primas
                        If ConceptAuthorization.Concept.ConceptClass = "033" Then MaxNumberIncentiveProvision = MaxNumberIncentiveProvision + ConceptValue

                        'Provisión Intereses de Cesantías
                        If ConceptAuthorization.Concept.ConceptClass = "034" Then UnemploymentInterestsProvision = UnemploymentInterestsProvision + ConceptValue

                        'Provisión SENA
                        If ConceptAuthorization.Concept.ConceptClass = "035" Then SenaProvision = SenaProvision + ConceptValue

                        'Provisión Caja de Compensación
                        If ConceptAuthorization.Concept.ConceptClass = "036" Then CompensationFundProvision = CompensationFundProvision + ConceptValue

                        'Provisión ICBF
                        If ConceptAuthorization.Concept.ConceptClass = "037" Then ICBFProvision = ICBFProvision + ConceptValue

                        'Aporte Fondo de Solidaridad Pensional
                        If ConceptAuthorization.Concept.ConceptClass = "038" Then ContributionPensionSolidarityFund = ContributionPensionSolidarityFund + ConceptValue

                        'Recargo Nocturno Normal
                        If ConceptAuthorization.Concept.ConceptClass = "042" Then RecargoNocturnoNormal = RecargoNocturnoNormal + ConceptValue

                        'Recargo Nocturno Normal
                        If ConceptAuthorization.Concept.ConceptClass = "043" Then RecargoNocturnoFestivo = RecargoNocturnoFestivo + ConceptValue

                        'Gastos de Representacion
                        If ConceptAuthorization.Concept.ConceptClass = "047" Then ValueRepresentationCost = ValueRepresentationCost + ConceptValue

                        ' Cuentas AFC
                        If ConceptAuthorization.Concept.ConceptClass = "045" Then AFCAccount = AFCAccount + ConceptValue



                        If ConceptAuthorization.Concept.Code = "001" Then
                            DistributeExpense = False
                        End If

                        'Si el empleado está pensionado, no se decuenta ni pensión del empleado ni patronal
                        If ObjEmployee.Pensionary = True Then
                            If ConceptAuthorization.Concept.ConceptClass = "014" Or ConceptAuthorization.Concept.ConceptClass = "015" Or ConceptAuthorization.Concept.ConceptClass = "038" Then
                                ConceptValue = 0
                            End If
                        End If

                        If ConceptValue > 0 Then

                            EmployeeLiquidatedDetail = New LiquidationDetail()

                            EmployeeLiquidatedDetail.RegisterStatus = 1
                            EmployeeLiquidatedDetail.PayrollDate = PayrollEndDate
                            EmployeeLiquidatedDetail.LiquidationPeriod = "1"
                            EmployeeLiquidatedDetail.ConceptClass = ConceptAuthorization.Concept.ConceptClass.ToString()
                            EmployeeLiquidatedDetail.ConceptId = ConceptAuthorization.Concept.Id
                            EmployeeLiquidatedDetail.ConceptCode = ConceptAuthorization.Concept.Code


                            Dim DescriptionContract As String
                            Dim FundName As String = String.Empty

                            If PublicClient = 2 Then ' Cliente Público
                                DescriptionContract = "Id No. "
                            Else
                                DescriptionContract = "Contrato No. "
                            End If

                            Dim DescriptionConcept As String = ConceptAuthorization.Concept.Name & " " & DescriptionContract

                            If ConceptAuthorization.Concept.ConceptClass.ToString() = "014" AndAlso PensionFundName <> String.Empty Then
                                'Pensión Empleado
                                FundName = "(" & PensionFundName & ")"
                            End If

                            If ConceptAuthorization.Concept.ConceptClass.ToString() = "016" AndAlso PensionVoluntaryFundName <> String.Empty Then
                                'Pensión Voluntaria Empleado
                                FundName = "(" & PensionVoluntaryFundName & ")"


                            End If

                            If ConceptAuthorization.Concept.ConceptClass.ToString() = "017" AndAlso HealthFundName <> String.Empty Then
                                'Salud Empleado
                                FundName = "(" & HealthFundName & ")"
                            End If

                            If ConceptAuthorization.Concept.ConceptClass.ToString() = "019" AndAlso HealthVoluntaryFundName <> String.Empty Then
                                'Salud Voluntaria Empleado
                                FundName = "(" & HealthVoluntaryFundName & ")"
                            End If

                            Dim shortDetail As String = ""
                            '' Se agrega el número de Horas por Concepto para visualización de Reportes
                            If WorkHours > 0 And ConceptAuthorization.Concept.ConceptClass <> "005" Then
                                EmployeeLiquidatedDetail.TotalNumberHours = WorkHours
                                shortDetail = DescriptionConcept & contractEmployee.Id.ToString() & " - Horas Laboradas (" & WorkHours & ")"
                                EmployeeLiquidatedDetail.ConceptDetail = If(shortDetail.Length > 300, shortDetail.Substring(0, 300), shortDetail)
                            Else
                                EmployeeLiquidatedDetail.TotalNumberHours = Nothing
                                shortDetail = DescriptionConcept & contractEmployee.Id.ToString() & " " & FundName
                                EmployeeLiquidatedDetail.ConceptDetail = If(shortDetail.Length > 300, shortDetail.Substring(0, 300), shortDetail)
                            End If


                            If ThirdPartyAgreementsId > 0 And ConceptAuthorization.Concept.ConceptClass <> "042" And NumAgreement > 0 Then
                                shortDetail = DescriptionConcept & contractEmployee.Id.ToString() & " - Entidad (" & ThirdPartyAgreementsName & ") - Consecutivo (" & NumAgreement & ")"
                                EmployeeLiquidatedDetail.ConceptDetail = If(shortDetail.Length > 300, shortDetail.Substring(0, 300), shortDetail)
                            End If

                            'Retención en la Fuente
                            If ConceptAuthorization.Concept.ConceptClass = "020" Then
                                EmployeeLiquidatedDetail.RetentionBase = BaseGrabable
                                EmployeeLiquidatedDetail.RetentionPercentage = PercentageRetention
                                EmployeeLiquidatedDetail.TypeArticleRTF = ArticuloUsado
                            Else
                                EmployeeLiquidatedDetail.RetentionBase = 0
                                EmployeeLiquidatedDetail.RetentionPercentage = 0
                                EmployeeLiquidatedDetail.TypeArticleRTF = 0
                            End If

                            EmployeeLiquidatedDetail.ConceptTotalValue = ConceptValue


                            EmployeeLiquidatedDetail.InitialBalance = 0
                            EmployeeLiquidatedDetail.ConceptType = ConceptAuthorization.Concept.ConceptType
                            EmployeeLiquidatedDetail.ConceptFormulate = ConceptAuthorization.Concept.Formulates

                            EmployeeLiquidatedDetail.ReplaceConceptFormulate = ReplaceFormula
                            EmployeeLiquidatedDetail.DistribuirGasto = DistributeExpense



                            If ConceptAuthorization.Concept.ConceptType = 1 Then
                                'DEVENGADO
                                ConceptoDevengadoSuma = ConceptoDevengadoSuma + ConceptValue
                                EmployeeLiquidatedDetail.AccruedValue = ConceptValue
                                EmployeeLiquidatedDetail.DeductedValue = 0

                                'Cargo los IBC's
                                If ConceptAuthorization.Concept.AffectIBC = True Then IBCPeriod = IBCPeriod + ConceptValue
                                If ConceptAuthorization.Concept.AffectIBCRTF = True Then IBCRTF = IBCRTF + ConceptValue
                                If ConceptAuthorization.Concept.AffectIBCSENA = True Then IBCSENA = IBCSENA + ConceptValue
                                If ConceptAuthorization.Concept.AffectIBCICBF = True Then IBCICBF = IBCICBF + ConceptValue
                                If ConceptAuthorization.Concept.AffectIBCCompensationFund = True Then IBCCompensationFund = IBCCompensationFund + ConceptValue
                                If ConceptAuthorization.Concept.AffectIBCSeverance = True Then IBCSeverance = IBCSeverance + ConceptValue

                                Dim IBCMinValue = CDec(ConceptValue)
                                If SessionValues.LanguageCulture <> "es-CR" AndAlso BasicSalary < minimimunSalary Then
                                    IBCMinValue = RecalculateIBCWithMinimumSalary(OriginalFormulaConcept, minimimunSalary, DaysWorkedEmployee, PayrollDays)
                                End If
                                If ConceptAuthorization.Concept.AffectIBCHealth Then IBCHealth = IBCHealth + IBCMinValue
                                If ConceptAuthorization.Concept.AffectIBCPension Then IBCPension = IBCPension + IBCMinValue
                                If ConceptAuthorization.Concept.AffectIBCARP Then IBCARP = IBCARP + IBCMinValue

                                If ConceptAuthorization.Concept.AffectIBCVacation = True Then IBCVacation = IBCVacation + ConceptValue
                                If ConceptAuthorization.Concept.AffectIBCIncentivePayment = True Then IBCIncentivePayment = IBCIncentivePayment + ConceptValue



                                EmployeeLiquidatedDetail.IdThirdParty = EmployeeThirdPartyId
                            ElseIf ConceptAuthorization.Concept.ConceptType = 2 Then

                                ' Conceptos clase "038" (FSP) siempre se incluyen como INCRNGO dentro de Retention(),
                                ' se excluyen aquí para evitar doble descuento sin importar el valor de AffectIBCRTF.
                                If ConceptAuthorization.Concept.AffectIBCRTF AndAlso
                                   ConceptAuthorization.Concept.ConceptClass <> "038" Then
                                    IBCRTF = IBCRTF - ConceptValue
                                End If
                                If ConceptAuthorization.Concept.AffectIBC = True Then IBCPeriod = IBCPeriod - ConceptValue

                                Dim IBCMinValue = CDec(ConceptValue)
                                If SessionValues.LanguageCulture <> "es-CR" AndAlso BasicSalary < minimimunSalary Then
                                    IBCMinValue = RecalculateIBCWithMinimumSalary(OriginalFormulaConcept, minimimunSalary, DaysWorkedEmployee, PayrollDays)
                                End If
                                If ConceptAuthorization.Concept.AffectIBCHealth Then IBCHealth = IBCHealth - IBCMinValue
                                If ConceptAuthorization.Concept.AffectIBCPension Then IBCPension = IBCPension - IBCMinValue
                                If ConceptAuthorization.Concept.AffectIBCARP Then IBCARP = IBCARP - IBCMinValue

                                'DEDUCIDO
                                EmployeeLiquidatedDetail.AccruedValue = 0
                                EmployeeLiquidatedDetail.DeductedValue = ConceptValue
                                ConceptoDeducidoSuma = ConceptoDeducidoSuma + ConceptValue


                                'Validaciones para el Campo Tercero:
                                If ConceptAuthorization.Concept.ConceptClass = "041" Then
                                    'CONVENIOS
                                    If ThirdPartyAgreementsId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyAgreementsId
                                    End If
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "014" Or ConceptAuthorization.Concept.ConceptClass = "038" Then
                                    'PENSIÓN EMPLEADO o FONDO DE SOLIDARIDAD PENSIONAL
                                    If ThirdPartyPensionFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyPensionFundId
                                    End If

                                ElseIf ConceptAuthorization.Concept.ConceptClass = "017" Then
                                    'SALUD EMPLEADO
                                    If ThirdPartyHealthFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyHealthFundId
                                    End If
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "016" Then
                                    'PENSION VOLUNTARIA
                                    If ThirdPartyVoluntaryPensionFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyVoluntaryPensionFundId
                                    End If
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "019" Then
                                    'SALUD VOLUNTARIA
                                    If ThirdPartyVoluntaryHealthFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyVoluntaryHealthFundId
                                    End If

                                ElseIf ConceptAuthorization.Concept.ConceptClass = "020" Then
                                    'RETENCIONES
                                    EmployeeLiquidatedDetail.IdThirdParty = EmployeeThirdPartyId
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "044" Then
                                    'SINDICATOS
                                    If ThirdPartyIdSindicate > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyIdSindicate
                                    End If
                                Else
                                    EmployeeLiquidatedDetail.IdThirdParty = EmployeeThirdPartyId
                                End If

                            Else
                                'PATRONAL
                                EmployeeLiquidatedDetail.AccruedValue = ConceptValue
                                EmployeeLiquidatedDetail.DeductedValue = 0

                                If ConceptAuthorization.Concept.ConceptClass = "009" Then
                                    'APORTE RIESGOS PROFESIONALES
                                    If ThirdPartyARLFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyARLFundId
                                    End If
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "035" Then
                                    'SENA
                                    If SENAThirdParty IsNot Nothing And SENAThirdParty.Id > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = SENAThirdParty.Id
                                    End If

                                ElseIf ConceptAuthorization.Concept.ConceptClass = "036" Then
                                    'Caja de Compensación
                                    If ThirdPartyCompensationFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyCompensationFundId
                                    End If
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "037" Then
                                    'ICBF
                                    If ICBFThirdParty IsNot Nothing And ICBFThirdParty.Id > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ICBFThirdParty.Id
                                    End If

                                ElseIf ConceptAuthorization.Concept.ConceptClass = "015" Then
                                    'PENSIÓN PATRONO
                                    If ThirdPartyPensionFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyPensionFundId
                                    End If
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "018" Then
                                    'SALUD PATRONO
                                    If ThirdPartyHealthFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyHealthFundId
                                    End If
                                ElseIf ConceptAuthorization.Concept.ConceptClass = "008" Then
                                    'CESANTIAS
                                    If ThirdPartyUnemploymentFundId > 0 Then
                                        EmployeeLiquidatedDetail.IdThirdParty = ThirdPartyUnemploymentFundId
                                    End If
                                Else
                                    EmployeeLiquidatedDetail.IdThirdParty = EmployeeThirdPartyId
                                End If

                            End If

                            If AgreementsId > 0 Then
                                'Es un concepto de Convenio
                                EmployeeLiquidatedDetail.AgreementsId = AgreementsId
                            End If

                            If AgreementsDId > 0 Then
                                'Es porque es un Convenio subido por Archivo
                                EmployeeLiquidatedDetail.AgreementsDId = AgreementsDId
                            End If

                            If RetirementFlag = True Then
                                EmployeeLiquidatedDetail.Concept = ConceptAuthorization.Concept
                            End If

                            If FlagIncentivePayment = True Then
                                EmployeeLiquidatedDetail.Concept = ConceptAuthorization.Concept
                            End If

                            Select Case ConceptAuthorization.Concept.ConceptClass
                                ' Conceptos por horas trabajadas
                                Case "001", "012", "013", "042", "043", "050", "051" ' Horas extras y recargos
                                    If Not ConceptAuthorization.Concept.Formulates = "[Valor Concepto Manual]" Then
                                        EmployeeLiquidatedDetail.Quantity = WorkHours
                                    End If
                                Case "005", "006", "066"  'Sueldo Base - contrato aprendizaje
                                    EmployeeLiquidatedDetail.Quantity = DaysWorked
                                Case "014" 'Pensión Empleado
                                    EmployeeLiquidatedDetail.Quantity = PensionDays
                                Case "017" 'Salud Empleado
                                    EmployeeLiquidatedDetail.Quantity = HealthDays
                                Case "021" ' Incapacidad Ambulatoria
                                    EmployeeLiquidatedDetail.Quantity = AmbulatoryInabilityEmployeerDays + AmbulatoryInabilityERPDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityAmbulatory
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectAmbulatory
                                Case "022" ' Incapacidad Hospitalaria
                                    EmployeeLiquidatedDetail.Quantity = HospitalInabilityERPDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityHospital
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectHospital
                                Case "023" ' Maternidad
                                    EmployeeLiquidatedDetail.Quantity = MaternityInabilityDays
                                    If TotalInabilityCollectMaternity > 0 Then
                                        EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityMaternity
                                        EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectMaternity
                                    Else
                                        EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityPaternity
                                        EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectPaternity
                                    End If
                                Case "024" ' Licencias Luto
                                    EmployeeLiquidatedDetail.Quantity = If(LicensesDays = 0, PaternityInabilityDays, LicensesDays)
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityLuto
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectLuto
                                Case "025" ' Licencia No Remunerada
                                    EmployeeLiquidatedDetail.Quantity = UnpaidLicensesDays
                                Case "026" ' Sanción
                                    EmployeeLiquidatedDetail.Quantity = SanctionsDays
                                Case "027" ' Incapacidad Riesgos Profesionales
                                    EmployeeLiquidatedDetail.Quantity = ProfesionalInabilitiesDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityProfessionalRisk
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectProfessionalRisk
                                Case "028" ' Permisos
                                    EmployeeLiquidatedDetail.Quantity = DaysPermission
                                Case "030" ' Vacaciones
                                    EmployeeLiquidatedDetail.Quantity = ContractVacationDays
                                Case "041" 'Convenios
                                    EmployeeLiquidatedDetail.Quantity = NumAgreementD
                                Case "067" 'Incapacidad Ambulatoria Patrono
                                    EmployeeLiquidatedDetail.Quantity = AmbulatoryInabilityEmployeerDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityAmbulatory
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectAmbulatory
                                    EmployeeLiquidatedDetail.EmployeerDays = AmbulatoryInabilityEmployeerDays
                                Case "068" 'Incapacidad Ambulatoria ERP
                                    EmployeeLiquidatedDetail.Quantity = AmbulatoryInabilityERPDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityAmbulatory
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectAmbulatory
                                    EmployeeLiquidatedDetail.ErpDays = AmbulatoryInabilityERPDays
                                Case "069" 'Incapacidad Hospitalaria Patron
                                    EmployeeLiquidatedDetail.Quantity = HospitalInabilityEmployeerDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityHospital
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectHospital
                                    EmployeeLiquidatedDetail.EmployeerDays = HospitalInabilityEmployeerDays
                                Case "070" 'Incapacidad Hospitaloria ERP
                                    EmployeeLiquidatedDetail.Quantity = HospitalInabilityERPDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityHospital
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectHospital
                                    EmployeeLiquidatedDetail.ErpDays = HospitalInabilityERPDays
                                Case "071" ' Calamidad domestica
                                    EmployeeLiquidatedDetail.Quantity = CalamidadDays
                                Case "072" ' Licencia Remunerada
                                    EmployeeLiquidatedDetail.Quantity = PaidLeaveDays
                                Case "073" ' Vacaciones en Dinero
                                    EmployeeLiquidatedDetail.Quantity = VacationDaysInCash
                                Case "074" 'Licencia luto
                                    EmployeeLiquidatedDetail.Quantity = LutoDays
                                    EmployeeLiquidatedDetail.SpendingInability = TotalSpendingInabilityLuto
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectLuto
                                    EmployeeLiquidatedDetail.LutoDays = LutoDays
                                Case "075" 'Incapacidad Profesional - Patrono
                                    EmployeeLiquidatedDetail.Quantity = EmployerProfessionalDisabilityDays
                                    EmployeeLiquidatedDetail.SpendingInability = EmployerProfessionalDisabilityAmount
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalSpendingInabilityProfessionalRisk
                                Case "076" 'Incapacidad Profesional - ERP
                                    EmployeeLiquidatedDetail.Quantity = ERPProfessionalDisabilityDays
                                    EmployeeLiquidatedDetail.SpendingInability = ERPProfessionalDisabilityAmount
                                    EmployeeLiquidatedDetail.InabilityCollect = TotalInabilityCollectProfessionalRisk
                            End Select
                            EmployeeLiquidated.LiquidationDetail.Add(EmployeeLiquidatedDetail)
                        End If
                    End If
                Next

                '============================================================================
                'LEY 1393/2010 - AJUSTE FINAL: Aplica el delta pendiente después del loop.
                'Cubre dos casos:
                '  1. Ningún concepto usó [IBC Salud]/[IBC Pensión]/[IBC ARP] - aplica total
                '  2. La bonificación no salarial llegó DESPUÉS del último concepto que
                '     usó IBCs - aplica el incremento pendiente (delta)
                '============================================================================
                If SessionValues.LanguageCulture <> "es-CR" AndAlso NonSalaryBonification > 0 Then

                    Dim TotalSumFinal As Decimal = valueAccrued + NonSalaryBonification
                    Dim Limit40PercentFinal As Decimal = TotalSumFinal * 0.4D
                    Dim FinalDifference As Decimal = NonSalaryBonification - Limit40PercentFinal
                    Dim ToAddFinal As Decimal = FinalDifference - AlreadyAppliedLaw1393

                    If ToAddFinal > 0 Then
                        IBCHealth = IBCHealth + ToAddFinal
                        IBCPension = IBCPension + ToAddFinal
                        IBCARP = IBCARP + ToAddFinal
                        IBCPeriod = IBCPeriod + ToAddFinal
                    End If
                    ' Mensaje unico
                    If AlreadyAppliedLaw1393 > 0 Then
                        Dim messageTextFinal = String.Format("El empleado {0} - {1} sobrepasa el límite 40% Ley 1393 de 2010", NitEmployee, NameEmployee)
                        MessageLiquitadion = CreateMessage(messageTextFinal, False, PayrollEndDate)
                        ActionMessageResult.MessageResult.Add(
                                    New MessageResult("014: Ley 1393/2010 - Límite 40%", messageTextFinal))

                        ' Detalle del calculo
                        MessageLiquitadion = CreateMessage(
                        "LEY 1393/2010 - Se calculó así: " &
                        "Devengados Salariales (IBC) = " & valueAccrued &
                        " + Bonificaciones No Salariales = " & NonSalaryBonification &
                        " = Total = " & (valueAccrued + NonSalaryBonification) &
                        " | Límite 40% = " & ((valueAccrued + NonSalaryBonification) * 0.4D) &
                        " | Excedente sumado al IBC = " & AlreadyAppliedLaw1393,
                        False, PayrollEndDate)
                        EmployeeLiquidated.Message.Add(MessageLiquitadion)
                    End If
                End If

                If EmployeeLiquidated IsNot Nothing Then
                    EmployeeLiquidated.WorkCenterId = ObjEmployee.WorkCenterId
                    EmployeeLiquidated.GroupId = groupEmployee.Id
                    EmployeeLiquidated.EmployeeId = ObjEmployee.Id
                    EmployeeLiquidated.ContractId = contractEmployee.Id
                    EmployeeLiquidated.RegisterStatus = ""
                    EmployeeLiquidated.PayrollDateLiquidated = PayrollEndDate
                    EmployeeLiquidated.LiquidationPeriod = groupEmployee.Liquidation.ToString()
                    EmployeeLiquidated.BasicSalary = contractEmployee.BasicSalary
                    EmployeeLiquidated.SalaryType = contractEmployee.ContractType.SalaryType.ToString()
                    EmployeeLiquidated.PayrollDays = PayrollDays
                    EmployeeLiquidated.DaysWorked = DaysWorked
                    EmployeeLiquidated.ProvisionDays = ProvisionDays
                    EmployeeLiquidated.RecargoNocturno = RecargoNocturnoNormal
                    EmployeeLiquidated.RecargoNocturnoFestivo = RecargoNocturnoFestivo
                    EmployeeLiquidated.Overtime = HourSchedule
                    EmployeeLiquidated.EveningOvertime = EveningOvertime
                    EmployeeLiquidated.DiurnalOvertime = HourSchedule
                    EmployeeLiquidated.HoursHolidays = 0
                    EmployeeLiquidated.HolidaysEveningHours = 0 ' Revisar
                    EmployeeLiquidated.ValueTransportingRelief = AcumulatedHelpTransport
                    EmployeeLiquidated.VacationDays = ContractVacationDays
                    EmployeeLiquidated.VacationValueEnjoy = VacationValue
                    EmployeeLiquidated.VacationValueLiquidated = 0 ' Revisar
                    EmployeeLiquidated.BonusValueServices = 0 'TotalBonusServices
                    EmployeeLiquidated.ValueOtherBonuses = 0 'TotalIncetivePaymentServicesExtra
                    EmployeeLiquidated.PensionFundId = PensionFundId
                    EmployeeLiquidated.PensionContributionValue = PensionEmployee
                    EmployeeLiquidated.PensionContributionDays = PensionDays
                    EmployeeLiquidated.PensionEnrollmentDays = PensionDays ' AfiliationPensionDays
                    EmployeeLiquidated.EmployerPensionContributionValue = PensionEmployer
                    EmployeeLiquidated.VoluntaryPensionFundId = PensionVoluntaryFundId
                    EmployeeLiquidated.VoluntaryContributionPensionValue = VoluntaryPension
                    EmployeeLiquidated.PensionSolidarityFundId = PensionFundId
                    EmployeeLiquidated.PensionSolidarityFundValueContribution = ContributionPensionSolidarityFund
                    EmployeeLiquidated.PensionSolidarityFundContributionDays = PensionDays
                    EmployeeLiquidated.PensionSolidarityFundEnrollmentDays = PensionDays
                    EmployeeLiquidated.EmployeeHealthContributionValue = HealthEmployee
                    EmployeeLiquidated.HealthFundId = HealthFundId
                    EmployeeLiquidated.QuoteHealthDays = HealthDays
                    EmployeeLiquidated.AffiliateHealthDays = 0 'AfiliationHealthDays
                    EmployeeLiquidated.EmployerHealthContributionValue = HealthEmployer 'TotalHealthEmployer
                    EmployeeLiquidated.AdditionalPUTValue = 0 ' Revisar OJO
                    EmployeeLiquidated.HealthJCBLicenses = 0 ' Programa Antiguo así
                    EmployeeLiquidated.HealthContributionLicensesValue = 0 ' Programa Antiguo así
                    EmployeeLiquidated.HealthLicensesDays = 0 ' Programa Antiguo así
                    EmployeeLiquidated.VoluntaryHealthFundId = HealthVoluntaryFundId
                    EmployeeLiquidated.VoluntaryHealthContributionValue = VoluntaryHealth
                    EmployeeLiquidated.TotalBaseRetention = IBCRTF
                    EmployeeLiquidated.ExemptValueRetention = MenosRenta
                    EmployeeLiquidated.DeductionsAndRentExents = TotalRentasExentasyDeducciones
                    EmployeeLiquidated.RealBaseRetention = IBCRTF - RetentionBase
                    EmployeeLiquidated.CalculatedWithholdingValue = RetentionValue
                    EmployeeLiquidated.RetentionPercentageApplied = PercentageRetention
                    EmployeeLiquidated.AccumulatedOtherAccrued = OtherAccrued
                    EmployeeLiquidated.AccumulatedOtherDeducted = OtherDeducted
                    EmployeeLiquidated.DeductingAccumulated = ConceptoDeducidoSuma

                    If contractEmployee.ContractType.SalaryType = 2 Then
                        EmployeeLiquidated.PeriodJCB = IBCPeriod * 0.7
                        EmployeeLiquidated.PensionJCB = IBCPension * 0.7
                        EmployeeLiquidated.HealthJCB = IBCHealth * 0.7
                    Else
                        EmployeeLiquidated.PeriodJCB = IBCPeriod
                        EmployeeLiquidated.PensionJCB = IBCPension
                        EmployeeLiquidated.HealthJCB = IBCHealth
                    End If
                    EmployeeLiquidated.PreviousMonthIBC = IBCLastPeriod

                    EmployeeLiquidated.AccumulatedBenefit = 0 ' Acumulado de Prestaciones
                    EmployeeLiquidated.AccumulatedDisabilityValue = TotalValuesInabilities
                    EmployeeLiquidated.DisabilityDays = Math.Max(0, TotalEmployeeInabilityDays)

                    EmployeeLiquidated.AmbulatoryDisabilityDays = Math.Max(0, AmbulatoryInabilityDays)
                    EmployeeLiquidated.AmbulatoryDisabilityInitialDate = AmbulatoryInabilityInitialDate
                    EmployeeLiquidated.AmbulatoryDisabilityEndDate = AmbulatoryInabilityEndDate
                    EmployeeLiquidated.AmbulatoryDisabilityAuthorizationNumber = AutorizationNumberAmbulatoryInability
                    EmployeeLiquidated.AmbulatoryDisabilityValue = ValueAmbulatoryInability
                    EmployeeLiquidated.DisabilityHospitalInitialDate = HospitalaryInabilityInitialDate
                    EmployeeLiquidated.DisabilityHospitalEndDate = HospitalaryInabilityEndDate
                    EmployeeLiquidated.DisabilityHospitalReleasedNumber = AutorizationNumberHospitalaryInability
                    EmployeeLiquidated.DisabilityHospitalDays = Math.Max(0, HospitalInabilityDays)
                    EmployeeLiquidated.DisabilityHospitalValue = ValueHospitalInability
                    EmployeeLiquidated.MaternityLeaveDays = Math.Max(0, MaternityInabilityDays)
                    EmployeeLiquidated.MaternityLeaveInitialDate = MAternityInabilityInitialDate
                    EmployeeLiquidated.MaternityLeaveEndDate = MaternityInabilityEndDate
                    EmployeeLiquidated.MaternityLeaveAutorizationNumber = AutorizationNumberMaternityInability
                    EmployeeLiquidated.VacationInitialDate = VacationInitialDate
                    EmployeeLiquidated.MaternityLeaveValue = ValueMaternity
                    EmployeeLiquidated.VacationEndDate = VacationEndDate
                    EmployeeLiquidated.LicenseDays = Math.Max(0, LicensesDays)
                    EmployeeLiquidated.LicenseValue = ValueRemuneratedLicenses
                    EmployeeLiquidated.UnpaidLicenseValue = UnpaidLicenses
                    EmployeeLiquidated.UnpaidLicenseAutorizarionNumber = AutorizationNumberUnpaidLicenses
                    EmployeeLiquidated.UnpaidLicenseDays = Math.Max(0, UnpaidLicensesDays)
                    EmployeeLiquidated.UnpaidLicenseInitialDate = UnpaidLicencesesInitialDate
                    EmployeeLiquidated.UnpaidLicenseEndDate = UnpaidLicencesesEndDate
                    EmployeeLiquidated.SanctionInitialDate = SanctionInitialDate
                    EmployeeLiquidated.SanctionEndDate = SanctionEndDate
                    EmployeeLiquidated.SanctionValue = Sanctions
                    EmployeeLiquidated.SanctionDays = Math.Max(0, SanctionsDays)
                    EmployeeLiquidated.OccupationalRisksContributionValue = RiskContribution
                    EmployeeLiquidated.OccupationalRisksDisabilityValue = ValueProfesionalInabilities
                    EmployeeLiquidated.OccupationalRisksDays = Math.Max(0, ProfesionalInabilitiesDays)
                    EmployeeLiquidated.OccupationalRisksDisabilityAutorizationNumber = AutorizationNumberProfessionalRisk
                    EmployeeLiquidated.OccupationalRisksDisabilityInitialDate = ProfessionalRiskInitialDate
                    EmployeeLiquidated.OccupationalRisksDisabilityEndDate = ProfessionalRiskEndDate
                    EmployeeLiquidated.OccupationalRisksFundId = ARLFundId
                    EmployeeLiquidated.PermissionsValue = 0 'TotalPermission
                    EmployeeLiquidated.UnemploymentAccumulated = Unemployment
                    EmployeeLiquidated.CompensationAccumulated = Compensation ' Revisar OJO
                    EmployeeLiquidated.VacationHealthJCB = 0 ' Programa Antiguo así
                    EmployeeLiquidated.VacationValueEnjoy = VacationValue
                    EmployeeLiquidated.VacationHealthContributionValueEmployee = HealthVacation
                    EmployeeLiquidated.VacationHealthContributionValueEmployer = 0 'VacationHealthEmployer
                    EmployeeLiquidated.VacationPensionJCB = 0 ' Programa Antiguo así
                    EmployeeLiquidated.VacationPensionContributionValueEmployee = PensionVacation
                    EmployeeLiquidated.VacationPensionContributionValueEmployer = 0 'VacationPensionEmployer
                    EmployeeLiquidated.AccountingVouchersNumber = 0 ' Programa Antiguo así
                    Dim rawAccrued As Decimal = EmployeeLiquidated.LiquidationDetail.Where(Function(d) d.ConceptType = 1).Sum(Function(d) d.AccruedValue.GetValueOrDefault())
                    Dim rawDeducted As Decimal = EmployeeLiquidated.LiquidationDetail.Where(Function(d) d.ConceptType = 2).Sum(Function(d) d.DeductedValue.GetValueOrDefault())
                    EmployeeLiquidated.TotalAccrued = Math.Round(rawAccrued)
                    EmployeeLiquidated.TotalDeducted = Math.Round(rawDeducted)
                    EmployeeLiquidated.TotalPaid = EmployeeLiquidated.TotalAccrued - EmployeeLiquidated.TotalDeducted
                    EmployeeLiquidated.PermissionDays = Math.Max(0, DaysPermission) ' DayPermission
                    EmployeeLiquidated.PermissionsValue = Permission
                    EmployeeLiquidated.SenaContributionValue = SenaProvision
                    EmployeeLiquidated.FamilyCompensationFundContributionValue = CompensationFundProvision
                    EmployeeLiquidated.ICBFContributionValue = ICBFProvision
                    EmployeeLiquidated.ParafiscalContribution = SenaProvision + CompensationFundProvision + ICBFProvision
                    EmployeeLiquidated.ProvisionsValue = Unemployment + UnemploymentInterestsProvision + MaxNumberIncentiveProvision + ProvisionVacation
                    EmployeeLiquidated.VacationNumberBussinesDays = 0 ' Revisar OJO
                    EmployeeLiquidated.CompletePayroll = completePayroll
                    EmployeeLiquidated.PayrollProcessDate = Date.Today()
                    EmployeeLiquidated.PayrollConfirmationDate = Date.Today()
                    EmployeeLiquidated.PayrollProcessUser = 1
                    EmployeeLiquidated.PayrollConfirmationUser = 1
                    EmployeeLiquidated.AFCAccount = AFCAccount

                    If contractEmployee.ContractType.SalaryType = 2 Then
                        EmployeeLiquidated.IBCUnemployment = IBCSeverance * 0.7
                        EmployeeLiquidated.IBCSENA = IBCSENA * 0.7
                        EmployeeLiquidated.IBCCompensationFund = IBCCompensationFund * 0.7
                        EmployeeLiquidated.IBCICBF = IBCICBF * 0.7
                        EmployeeLiquidated.IBCUnemploymentNoSanctions = IBCSeverance * 0.7 'IBCSeveranceNoSanctions + VacationValueCash
                        EmployeeLiquidated.IBCVacation = IBCVacation * 0.7
                        EmployeeLiquidated.IBCIncentivePayment = IBCIncentivePayment * 0.7
                        EmployeeLiquidated.IBCOccupationalRisks = IBCARP * 0.7
                    Else
                        EmployeeLiquidated.IBCUnemployment = IBCSeverance
                        EmployeeLiquidated.IBCSENA = IBCSENA
                        EmployeeLiquidated.IBCCompensationFund = IBCCompensationFund
                        EmployeeLiquidated.IBCICBF = IBCICBF
                        EmployeeLiquidated.IBCUnemploymentNoSanctions = IBCSeverance  'IBCSeveranceNoSanctions + VacationValueCash
                        EmployeeLiquidated.IBCVacation = IBCVacation
                        EmployeeLiquidated.IBCIncentivePayment = IBCIncentivePayment
                        EmployeeLiquidated.IBCOccupationalRisks = IBCARP
                    End If

                    EmployeeLiquidated.QuotedOccupationalRisksDays = 0 'CompensationFundDays
                    EmployeeLiquidated.QuotedCompensationDays = 0 'CompensationFundDays

                    'Campos de Provisiones
                    EmployeeLiquidated.ProvisionIncentive = MaxNumberIncentiveProvision 'TotalMaxNumberIncentiveProvision
                    EmployeeLiquidated.ProvisionVacation = ProvisionVacation
                    EmployeeLiquidated.ProvisionInterestsUnemployment = UnemploymentInterestsProvision

                    EmployeeLiquidated.BankId = contractEmployee.BankId
                    EmployeeLiquidated.BankAccountNumber = contractEmployee.BankAccountNumber

                    EmployeeLiquidated.UnemployementFundId = UnemploymentFundId
                    EmployeeLiquidated.HousingDeductionValue = HousingDeducted
                    EmployeeLiquidated.ProcedureTypeRTF = ObjEmployee.ProcedureTypeRTF
                    EmployeeLiquidated.DependentsDeduction = ValDependsValue
                    EmployeeLiquidated.DependentsSupplementary = ValDependentSupplement

                    EmployeeLiquidated.NitEmployee = NitEmployee
                    EmployeeLiquidated.FullNameEmployee = NameEmployee

                    If contractEmployee.FunctionalUnit IsNot Nothing Then
                        EmployeeLiquidated.CostCenterId = contractEmployee.FunctionalUnit.CostCenterId
                    Else
                        EmployeeLiquidated.CostCenterId = contractEmployee.Employee.CostCenterId
                    End If

                    EmployeeLiquidated.InitialContractNumber = contractEmployee.InitialContractNumber


                    ListLiquidation.Add(EmployeeLiquidated)
                End If

                If ConceptoDevengadoSuma < ConceptoDeducidoSuma Then
                    MessageLiquitadion = CreateMessage("El TOTAL PAGADO está en Negativo. No se puede CONFIRMAR", True, PayrollEndDate)
                    EmployeeLiquidated.Message.Add(MessageLiquitadion)

                    ActionMessageResult.MessageResult.Add(New MessageResult("-007: Total a Pagar", "El Empleado " & NitEmployee & " - " & NameEmployee & " tiene el TOTAL A PAGAR en NEGATIVO"))
                End If

            Catch ex As Exception
                ErrorCount = ErrorCount + 1
                ActionMessageResult.MessageResult.Add(New MessageResult("-999: Error", "Se presentó un error desconocido con el Empleado No. " & objEmployeeTmp.Id.ToString() & " - " & ex.Message))
                ActionMessageResult.StateResult = False
                Continue For
            End Try
        Next

        If ListLiquidation IsNot Nothing And ListLiquidation.Count > 0 Then
            ActionMessageResult.ObjectEmbbeded = ListLiquidation
        Else
            ActionMessageResult.StateResult = False
        End If


        Return ActionMessageResult

    End Function
    ''' <summary>
    '''  Obtiene la última liquidación de un empleado en la que no haya novedades (como incapacidades, licencias no remuneradas o sanciones)
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="currentDate"></param>
    ''' <returns></returns>
    Private Function GetLastLiquidationMonthWithoutNovelty(employeeId As Integer, currentDate As Date) As Liquidation

        Dim dateLastMonth = New Date(currentDate.Year, currentDate.Month, 1).AddDays(-1)
        Dim liquidation As Liquidation = _liquitationRepository.GetLiquidationByEmployeeIdYearMonth(employeeId, dateLastMonth.Year, dateLastMonth.Month)

        If liquidation Is Nothing Then
            Return Nothing
        Else
            'Incapcidad Hospitalaria/Licencias no Remuneradas/Sanciones
            If liquidation.LiquidationDetail.Any(Function(c) c.ConceptClass = "022" Or c.ConceptClass = "024" Or c.ConceptClass = "026") Then
                Return GetLastLiquidationMonthWithoutNovelty(employeeId, dateLastMonth)
            End If

        End If

        Return liquidation
    End Function

    ''' <summary>
    ''' Reemplaza el valor de un concepto ya calculado si es utilizado en la fórmula
    ''' </summary>
    ''' <param name="formulaConcept"></param>
    ''' <param name="payrollConcepts"></param>
    ''' <returns></returns>
    Private Function ReplaceConceptAsVariable(formulaConcept As String, payrollConcepts As List(Of AuthorizationConcept)) As String
        Dim pattern As String = "\[C_(\d+)\]"
        Dim matches = Text.RegularExpressions.Regex.Matches(formulaConcept, pattern)

        For Each match As Text.RegularExpressions.Match In matches
            Dim conceptCode = match.Groups(1).Value
            Dim concept = payrollConcepts.FirstOrDefault(Function(m) m.Concept.Code = conceptCode)
            If concept IsNot Nothing Then
                formulaConcept = formulaConcept.Replace($"[C_{conceptCode}]", concept.ConceptValue.ToString())
            End If
        Next

        Return formulaConcept
    End Function

    ''' <summary>
    ''' Crea el detalle de liquidación para la devolución de retención
    ''' </summary>
    ''' <param name="contractEmployee"></param>
    ''' <param name="ThirdPartyAgreementsId"></param>
    ''' <param name="ThirdPartyCompensationFundId"></param>
    ''' <param name="ThirdPartyUnemploymentFundId"></param>
    ''' <param name="NumAgreement"></param>
    ''' <param name="PayrollEndDate"></param>
    ''' <param name="PublicClient"></param>
    ''' <param name="WorkHours"></param>
    ''' <param name="EmployeeThirdPartyId"></param>
    ''' <param name="ThirdPartyPensionFundId"></param>
    ''' <param name="ThirdPartyHealthFundId"></param>
    ''' <param name="ThirdPartyVoluntaryPensionFundId"></param>
    ''' <param name="ThirdPartyVoluntaryHealthFundId"></param>
    ''' <param name="ThirdPartyIdSindicate"></param>
    ''' <param name="ThirdPartyARLFundId"></param>
    ''' <param name="AgreementsId"></param>
    ''' <param name="AgreementsDId"></param>
    ''' <param name="ConceptValue"></param>
    ''' <param name="RetirementFlag"></param>
    ''' <param name="FlagIncentivePayment"></param>
    ''' <param name="TotalSpendingInabilityAmbulatory"></param>
    ''' <param name="TotalInabilityCollectAmbulatory"></param>
    ''' <param name="TotalSpendingInabilityHospital"></param>
    ''' <param name="TotalInabilityCollectHospital"></param>
    ''' <param name="TotalInabilityCollectMaternity"></param>
    ''' <param name="TotalSpendingInabilityMaternity"></param>
    ''' <param name="TotalSpendingInabilityPaternity"></param>
    ''' <param name="TotalInabilityCollectPaternity"></param>
    ''' <param name="TotalSpendingInabilityProfessionalRisk"></param>
    ''' <param name="TotalInabilityCollectProfessionalRisk"></param>
    ''' <param name="TotalSpendingInabilityLuto"></param>
    ''' <param name="TotalInabilityCollectLuto"></param>
    ''' <param name="PensionFundName"></param>
    ''' <param name="PensionVoluntaryFundName"></param>
    ''' <param name="HealthFundName"></param>
    ''' <param name="HealthVoluntaryFundName"></param>
    ''' <param name="ThirdPartyAgreementsName"></param>
    ''' <param name="SENAThirdParty"></param>
    ''' <param name="ICBFThirdParty"></param>
    ''' <returns></returns>
    Private Function CreateWithholdingTaxRefundDetail(
            contractEmployee As Entities.Contract,
            ThirdPartyAgreementsId As Integer,
            ThirdPartyCompensationFundId As Integer?,
            ThirdPartyUnemploymentFundId As Integer?,
            NumAgreement As Integer,
            PayrollEndDate As Date,
            PublicClient As Integer,
            WorkHours As Decimal,
            EmployeeThirdPartyId As Integer,
            ThirdPartyPensionFundId As Integer?,
            ThirdPartyHealthFundId As Integer?,
            ThirdPartyVoluntaryPensionFundId As Integer?,
            ThirdPartyVoluntaryHealthFundId As Integer?,
            ThirdPartyIdSindicate As Integer?,
            ThirdPartyARLFundId As Integer?,
            AgreementsId As Integer,
            AgreementsDId As Integer,
            ConceptValue As Double,
            RetirementFlag As Boolean,
            FlagIncentivePayment As Boolean,
            TotalSpendingInabilityAmbulatory As Decimal,
            TotalInabilityCollectAmbulatory As Decimal,
            TotalSpendingInabilityHospital As Decimal,
            TotalInabilityCollectHospital As Decimal,
            TotalInabilityCollectMaternity As Decimal,
            TotalSpendingInabilityMaternity As Decimal,
            TotalSpendingInabilityPaternity As Decimal,
            TotalInabilityCollectPaternity As Decimal,
            TotalSpendingInabilityProfessionalRisk As Decimal,
            TotalInabilityCollectProfessionalRisk As Decimal,
            TotalSpendingInabilityLuto As Decimal,
            TotalInabilityCollectLuto As Decimal,
            PensionFundName As String,
            PensionVoluntaryFundName As String,
            HealthFundName As String,
            HealthVoluntaryFundName As String,
            ThirdPartyAgreementsName As String,
            SENAThirdParty As Domain.Entities.ThirdParty,
            ICBFThirdParty As Domain.Entities.ThirdParty,
            PayrollSettings As PayrollSettings
        ) As LiquidationDetail
        Dim conceptId = PayrollSettings.WithholdingTaxRefundConceptId.Value
        Dim concept As Entities.Concept = _conceptRepository.FirstOrDefault(Function(m) m.Id = conceptId)
        Dim liquidatedDetail As New LiquidationDetail With {
            .RegisterStatus = 1,
            .PayrollDate = PayrollEndDate,
            .LiquidationPeriod = "1",
            .ConceptClass = concept.ConceptClass.ToString(),
            .ConceptId = concept.Id,
            .ConceptCode = concept.Code
        }

        Dim DescriptionContract As String
        Dim FundName As String = String.Empty

        If PublicClient = 2 Then ' Cliente Público
            DescriptionContract = "Id No. "
        Else
            DescriptionContract = "Contrato No. "
        End If

        Dim DescriptionConcept As String = concept.Name & " " & DescriptionContract

        If concept.ConceptClass.ToString() = "014" AndAlso PensionFundName <> String.Empty Then
            'Pensión Empleado
            FundName = "(" & PensionFundName & ")"
        End If

        If concept.ConceptClass.ToString() = "016" AndAlso PensionVoluntaryFundName <> String.Empty Then
            'Pensión Voluntaria Empleado
            FundName = "(" & PensionVoluntaryFundName & ")"
        End If

        If concept.ConceptClass.ToString() = "017" AndAlso HealthFundName <> String.Empty Then
            'Salud Empleado
            FundName = "(" & HealthFundName & ")"
        End If

        If concept.ConceptClass.ToString() = "019" AndAlso HealthVoluntaryFundName <> String.Empty Then
            'Salud Voluntaria Empleado
            FundName = "(" & HealthVoluntaryFundName & ")"
        End If


        '' Se agrega el número de Horas por Concepto para visualización de Reportes
        If WorkHours > 0 And concept.ConceptClass <> "005" Then
            liquidatedDetail.TotalNumberHours = WorkHours
            liquidatedDetail.ConceptDetail = DescriptionConcept & contractEmployee.Id.ToString() & " - Horas Laboradas (" & WorkHours & ")"
        Else
            liquidatedDetail.TotalNumberHours = Nothing
            liquidatedDetail.ConceptDetail = DescriptionConcept & contractEmployee.Id.ToString() & " " & FundName
        End If


        If ThirdPartyAgreementsId > 0 And concept.ConceptClass <> "042" And NumAgreement > 0 Then
            liquidatedDetail.ConceptDetail = DescriptionConcept & contractEmployee.Id.ToString() & " - Entidad (" & ThirdPartyAgreementsName & ") - Consecutivo (" & NumAgreement & ")"
        End If

        'Retención en la Fuente
        liquidatedDetail.RetentionBase = 0
        liquidatedDetail.RetentionPercentage = 0
        liquidatedDetail.TypeArticleRTF = 0
        liquidatedDetail.ConceptTotalValue = ConceptValue
        liquidatedDetail.InitialBalance = 0
        liquidatedDetail.ConceptType = concept.ConceptType
        liquidatedDetail.ConceptFormulate = concept.Formulates
        liquidatedDetail.ReplaceConceptFormulate = ConceptValue 'ReplaceFormula
        liquidatedDetail.DistribuirGasto = DistributeExpense

        If concept.ConceptType = 1 Then
            'DEVENGADO
            liquidatedDetail.AccruedValue = ConceptValue
            liquidatedDetail.DeductedValue = 0

            If concept.ConceptType = 1 Then
                'Cargo los IBC's
                liquidatedDetail.IdThirdParty = EmployeeThirdPartyId
            End If

        ElseIf concept.ConceptType = 2 Then

            'Cargo los IBC's con des

            'DEDUCIDO
            liquidatedDetail.AccruedValue = 0
            liquidatedDetail.DeductedValue = ConceptValue

            'Validaciones para el Campo Tercero:
            If concept.ConceptClass = "041" Then
                'CONVENIOS
                If ThirdPartyAgreementsId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyAgreementsId
                End If
            ElseIf concept.ConceptClass = "014" Or concept.ConceptClass = "038" Then
                'PENSIÓN EMPLEADO o FONDO DE SOLIDARIDAD PENSIONAL
                If ThirdPartyPensionFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyPensionFundId
                End If

            ElseIf concept.ConceptClass = "017" Then
                'SALUD EMPLEADO
                If ThirdPartyHealthFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyHealthFundId
                End If
            ElseIf concept.ConceptClass = "016" Then
                'PENSION VOLUNTARIA
                If ThirdPartyVoluntaryPensionFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyVoluntaryPensionFundId
                End If
            ElseIf concept.ConceptClass = "019" Then
                'SALUD VOLUNTARIA
                If ThirdPartyVoluntaryHealthFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyVoluntaryHealthFundId
                End If

            ElseIf concept.ConceptClass = "020" Then
                'RETENCIONES
                liquidatedDetail.IdThirdParty = EmployeeThirdPartyId
            ElseIf concept.ConceptClass = "044" Then
                'SINDICATOS
                If ThirdPartyIdSindicate > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyIdSindicate
                End If
            Else
                liquidatedDetail.IdThirdParty = EmployeeThirdPartyId
            End If

        Else
            'PATRONAL
            liquidatedDetail.AccruedValue = ConceptValue
            liquidatedDetail.DeductedValue = 0

            If concept.ConceptClass = "009" Then
                'APORTE RIESGOS PROFESIONALES
                If ThirdPartyARLFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyARLFundId
                End If
            ElseIf concept.ConceptClass = "035" Then
                'SENA
                If SENAThirdParty IsNot Nothing And SENAThirdParty.Id > 0 Then
                    liquidatedDetail.IdThirdParty = SENAThirdParty.Id
                End If
            ElseIf concept.ConceptClass = "036" Then
                'Caja de Compensación
                If ThirdPartyCompensationFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyCompensationFundId
                End If
            ElseIf concept.ConceptClass = "037" Then
                'ICBF
                If ICBFThirdParty IsNot Nothing And ICBFThirdParty.Id > 0 Then
                    liquidatedDetail.IdThirdParty = ICBFThirdParty.Id
                End If

            ElseIf concept.ConceptClass = "015" Then
                'PENSIÓN PATRONO
                If ThirdPartyPensionFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyPensionFundId
                End If
            ElseIf concept.ConceptClass = "018" Then
                'SALUD PATRONO
                If ThirdPartyHealthFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyHealthFundId
                End If
            ElseIf concept.ConceptClass = "008" Then
                'CESANTIAS
                If ThirdPartyUnemploymentFundId > 0 Then
                    liquidatedDetail.IdThirdParty = ThirdPartyUnemploymentFundId
                End If
            Else
                liquidatedDetail.IdThirdParty = EmployeeThirdPartyId
            End If
        End If

        If AgreementsId > 0 Then
            'Es un concepto de Convenio
            liquidatedDetail.AgreementsId = AgreementsId
        End If

        If AgreementsDId > 0 Then
            'Es porque es un Convenio subido por Archivo
            liquidatedDetail.AgreementsDId = AgreementsDId
        End If

        If RetirementFlag OrElse FlagIncentivePayment Then
            liquidatedDetail.Concept = concept
        End If

        If concept.ConceptClass = "021" Then
            liquidatedDetail.SpendingInability = TotalSpendingInabilityAmbulatory
            liquidatedDetail.InabilityCollect = TotalInabilityCollectAmbulatory
        ElseIf concept.ConceptClass = "022" Then
            liquidatedDetail.SpendingInability = TotalSpendingInabilityHospital
            liquidatedDetail.InabilityCollect = TotalInabilityCollectHospital
        ElseIf concept.ConceptClass = "023" Then
            If TotalInabilityCollectMaternity > 0 Then
                liquidatedDetail.SpendingInability = TotalSpendingInabilityMaternity
                liquidatedDetail.InabilityCollect = TotalInabilityCollectMaternity
            Else
                liquidatedDetail.SpendingInability = TotalSpendingInabilityPaternity
                liquidatedDetail.InabilityCollect = TotalInabilityCollectPaternity
            End If
        ElseIf concept.ConceptClass = "027" OrElse concept.ConceptClass = "075" OrElse concept.ConceptClass = "076" Then
            liquidatedDetail.SpendingInability = TotalSpendingInabilityProfessionalRisk
            liquidatedDetail.InabilityCollect = TotalInabilityCollectProfessionalRisk
        ElseIf concept.ConceptClass = "024" Then
            liquidatedDetail.SpendingInability = TotalSpendingInabilityLuto
            liquidatedDetail.InabilityCollect = TotalInabilityCollectLuto
        End If

        Return liquidatedDetail
    End Function

    Private Function LiquidatedExtraTime(ObjGroup As Group, ObjContract As Domain.Payroll.Entities.Contract, ObjPosition As Domain.Payroll.Entities.Position, InitialDate As Date, EndDate As Date) As ActionMessageResult(Of Liquidation) Implements ILiquidationDomain.LiquidatedExtraTime
        Dim ActionMessageResult As New ActionMessageResult(Of Liquidation)
        ActionMessageResult.StateResult = True

        Try

            Dim PayrollSettings = _PayrollSettings.GetSettingPayroll()

            Dim NewObjLiquidation As New Liquidation

            'Cargo los Conceptos Autorizados por Empleado
            Dim ListClassConcept = New List(Of String)

            ListClassConcept.Add("013")
            ListClassConcept.Add("050")
            ListClassConcept.Add("001")
            ListClassConcept.Add("012")
            ListClassConcept.Add("052")
            ListClassConcept.Add("051")
            ListClassConcept.Add("043")
            ListClassConcept.Add("042")

            'Cargo los Conceptos Manuales de los Empleado


            Dim ListManualConcept As New List(Of ManualConcepts)

            ListManualConcept = _ManualConceptsRepository.GetManualConceptsByContractNumberPayrollDate(IIf(ObjContract.InitialContractNumber = 0, ObjContract.Id, ObjContract.InitialContractNumber), InitialDate, EndDate, 1, 1)

            ' Averiguo los conceptos que tiene autorizado el Grupo
            Dim AuthorizationConcept = _autorizationConceptRepository.GetAuthorizationConceptByGroupId(ObjGroup.Id)

            'Cargo los Conceptos Autorizados por Empleado
            Dim AutorizationConceptEmployee = _autorizationConceptRepository.GetAuthorizationConceptByEmployeeId(ObjContract.EmployeeId)
            Dim PayrollAuthorizationConcept As List(Of AuthorizationConcept)
            PayrollAuthorizationConcept = AuthorizationConcept.OrderBy(Function(x) x.Concept.Code).ToList()

            If AutorizationConceptEmployee IsNot Nothing AndAlso AutorizationConceptEmployee.Count > 0 Then
                PayrollAuthorizationConcept = AuthorizationConcept.Union(AutorizationConceptEmployee).Distinct().OrderBy(Function(x) x.Concept.Code).ToList()
            End If

            Dim ObjListConcept = PayrollAuthorizationConcept.Where(Function(x) ListClassConcept.Contains(x.Concept.ConceptClass)).ToList

            Dim WorkHours As Decimal = 0
            Dim WorkDays As Integer = Me.Days360(InitialDate, EndDate)

            Dim ScheduleDetailEmployee = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDateWithoutNovelties(ObjContract.EmployeeId, InitialDate, EndDate)

            ' If ScheduleDetailEmployee IsNot Nothing Then

            For Each ObjConcept As AuthorizationConcept In ObjListConcept

                Dim ManualConceptValue = 0

                If ScheduleDetailEmployee IsNot Nothing Then
                    WorkHours = _functionsLiquidation.HourByConcept(ObjPosition.HandlesTurnsChart, 30, ObjConcept, 8, ScheduleDetailEmployee)
                    If WorkHours > 0 Then
                        DistributeExpense = True
                    End If
                Else
                    WorkHours = 0
                End If

                'CONCEPTOS MANUALES - HORA
                Dim ManualConceptPaidHourFlag As Boolean
                If ObjConcept.Concept.ConceptClass = "001" Or ObjConcept.Concept.ConceptClass = "005" Or ObjConcept.Concept.ConceptClass = "012" Or ObjConcept.Concept.ConceptClass = "013" Or ObjConcept.Concept.ConceptClass = "042" Or ObjConcept.Concept.ConceptClass = "043" Or ObjConcept.Concept.ConceptClass = "050" Or ObjConcept.Concept.ConceptClass = "052" Or ObjConcept.Concept.ConceptClass = "051" Then
                    If ListManualConcept IsNot Nothing Then
                        Dim TmpListManualConceptHour = ListManualConcept.Where(Function(x) x.ConceptId = ObjConcept.ConceptId).ToList()

                        If TmpListManualConceptHour IsNot Nothing AndAlso TmpListManualConceptHour.Count > 0 Then
                            WorkHours = WorkHours + (_functionsLiquidation.HourManualConcept(InitialDate, EndDate, TmpListManualConceptHour, ManualConceptPaidHourFlag))
                        End If
                    End If
                End If

                Dim IncomingDailyBase = 0

                If ObjContract.IncomeDailyBase Is Nothing Then
                    IncomingDailyBase = ObjContract.BasicSalary / ObjContract.HoursDaily
                End If

                If WorkHours > 0 Then
                    Dim ConceptData = Me.ReplaceDataLiquidation(ObjGroup.PayrollParameter, ObjContract.BasicSalary, WorkDays, WorkHours, WorkDays, ObjConcept.Concept.Formulates, ObjContract, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, WorkDays,
                                                                        ObjContract.Employee.ProfessionalRiskPercentage, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                                                        0, ManualConceptValue, 0, 0, ObjPosition.RepresentationCost, 0, 0, EndDate, ObjContract.JobBondingDate, 1, 0, 0, 0, 0, ObjContract.Employee.EmployeeType.Code, 0, 0, 0, 0, 1,
                                                                        0, 0, 0, 0, 0, 0, ObjPosition.MinHourAmount, ObjPosition.MaxHourAmount, 0, 0, 0, 0, 0, 0, ObjContract.Employee.WorkCenter.Code, 0, IncomingDailyBase,
                                                                                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ObjContract.JobBondingDate, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                                                        0, 0, 0, 0, 0, 0, 0, 0, 0)

                    Dim ReplaceFormula = ConceptData("Formula")
                    Dim ConceptValue = CDbl(ConceptData("Valor"))

                    If ConceptValue > 0 Then

                        Dim NewObjLiquidationDetail As New LiquidationDetail

                        NewObjLiquidationDetail.RegisterStatus = 1
                        NewObjLiquidationDetail.PayrollDate = EndDate
                        NewObjLiquidationDetail.LiquidationPeriod = 1
                        NewObjLiquidationDetail.Concept = ObjConcept.Concept
                        NewObjLiquidationDetail.ConceptClass = ObjConcept.Concept.ConceptClass
                        NewObjLiquidationDetail.ConceptId = ObjConcept.ConceptId
                        NewObjLiquidationDetail.ConceptCode = ObjConcept.Concept.Code
                        NewObjLiquidationDetail.ConceptDetail = ObjConcept.Concept.Name
                        NewObjLiquidationDetail.ConceptTotalValue = ConceptValue

                        If ObjConcept.Concept.ConceptType <> 2 Then
                            NewObjLiquidationDetail.AccruedValue = ConceptValue
                            NewObjLiquidationDetail.DeductedValue = 0
                        Else
                            NewObjLiquidationDetail.AccruedValue = 0
                            NewObjLiquidationDetail.DeductedValue = ConceptValue
                        End If

                        NewObjLiquidationDetail.RetentionBase = 0
                        NewObjLiquidationDetail.RetentionPercentage = 0
                        NewObjLiquidationDetail.InitialBalance = 0
                        NewObjLiquidationDetail.ConceptType = ObjConcept.Concept.ConceptType
                        NewObjLiquidationDetail.ConceptFormulate = ObjConcept.Concept.Formulates
                        NewObjLiquidationDetail.ReplaceConceptFormulate = ReplaceFormula
                        NewObjLiquidationDetail.DistribuirGasto = 0
                        NewObjLiquidationDetail.TotalNumberHours = WorkHours
                        NewObjLiquidationDetail.IdThirdParty = ObjContract.Employee.ThirdPartyId
                        NewObjLiquidationDetail.TypeArticleRTF = 0

                        NewObjLiquidation.LiquidationDetail.Add(NewObjLiquidationDetail)
                    End If

                End If


            Next

            NewObjLiquidation.WorkCenterId = ObjContract.Employee.WorkCenterId
            NewObjLiquidation.GroupId = ObjGroup.Id  'payrollEmployee.Item(i).GroupId
            NewObjLiquidation.EmployeeId = ObjContract.EmployeeId ' payrollEmployee.Item(i).EmployeeId
            NewObjLiquidation.ContractId = ObjContract.Id ' payrollEmployee.Item(i).Id
            NewObjLiquidation.RegisterStatus = ""
            NewObjLiquidation.PayrollDateLiquidated = EndDate
            NewObjLiquidation.LiquidationPeriod = 1
            NewObjLiquidation.BasicSalary = ObjContract.BasicSalary
            NewObjLiquidation.SalaryType = ObjContract.ContractType.SalaryType.ToString()
            NewObjLiquidation.PayrollDays = 30
            NewObjLiquidation.DaysWorked = WorkDays
            NewObjLiquidation.ProvisionDays = WorkDays
            NewObjLiquidation.RecargoNocturno = 0
            NewObjLiquidation.RecargoNocturnoFestivo = 0
            NewObjLiquidation.Overtime = 0
            NewObjLiquidation.EveningOvertime = 0
            NewObjLiquidation.DiurnalOvertime = 0
            NewObjLiquidation.HoursHolidays = 0
            NewObjLiquidation.HolidaysEveningHours = 0 ' Revisar
            NewObjLiquidation.ValueTransportingRelief = 0
            NewObjLiquidation.VacationDays = 0
            NewObjLiquidation.VacationValueEnjoy = 0
            NewObjLiquidation.VacationValueLiquidated = 0 ' Revisar
            NewObjLiquidation.BonusValueServices = 0 'TotalBonusServices
            NewObjLiquidation.ValueOtherBonuses = 0 'TotalIncetivePaymentServicesExtra
            NewObjLiquidation.PensionFundId = 0
            NewObjLiquidation.PensionContributionValue = 0
            NewObjLiquidation.PensionContributionDays = 0
            NewObjLiquidation.PensionEnrollmentDays = 0 ' AfiliationPensionDays
            NewObjLiquidation.EmployerPensionContributionValue = 0
            NewObjLiquidation.VoluntaryPensionFundId = 0
            NewObjLiquidation.VoluntaryContributionPensionValue = 0
            NewObjLiquidation.PensionSolidarityFundId = 0
            NewObjLiquidation.PensionSolidarityFundValueContribution = 0
            NewObjLiquidation.PensionSolidarityFundContributionDays = 0
            NewObjLiquidation.PensionSolidarityFundEnrollmentDays = 0
            NewObjLiquidation.EmployeeHealthContributionValue = 0
            NewObjLiquidation.HealthFundId = 0
            NewObjLiquidation.QuoteHealthDays = 0
            NewObjLiquidation.AffiliateHealthDays = 0 'AfiliationHealthDays
            NewObjLiquidation.EmployerHealthContributionValue = 0  'TotalHealthEmployer
            NewObjLiquidation.AdditionalPUTValue = 0 ' Revisar OJO
            NewObjLiquidation.HealthJCBLicenses = 0 ' Programa Antiguo así
            NewObjLiquidation.HealthContributionLicensesValue = 0 ' Programa Antiguo así
            NewObjLiquidation.HealthLicensesDays = 0 ' Programa Antiguo así
            NewObjLiquidation.VoluntaryHealthFundId = 0
            NewObjLiquidation.VoluntaryHealthContributionValue = 0
            NewObjLiquidation.TotalBaseRetention = 0
            NewObjLiquidation.ExemptValueRetention = 0
            NewObjLiquidation.RealBaseRetention = 0
            NewObjLiquidation.CalculatedWithholdingValue = 0
            NewObjLiquidation.RetentionPercentageApplied = 0
            NewObjLiquidation.AccumulatedOtherAccrued = 0
            NewObjLiquidation.AccumulatedOtherDeducted = 0
            NewObjLiquidation.DeductingAccumulated = 0

            NewObjLiquidation.PeriodJCB = 0
            NewObjLiquidation.PensionJCB = 0
            NewObjLiquidation.HealthJCB = 0

            NewObjLiquidation.AccumulatedBenefit = 0 'TotalHealthEmployee + TotalVoluntaryHealth + TotalPensionEmployee + TotalVoluntaryPension ' Acumulado de Prestaciones
            NewObjLiquidation.AccumulatedDisabilityValue = 0
            NewObjLiquidation.DisabilityDays = 0

            NewObjLiquidation.AmbulatoryDisabilityDays = 0
            NewObjLiquidation.AmbulatoryDisabilityInitialDate = Nothing
            NewObjLiquidation.AmbulatoryDisabilityEndDate = Nothing
            NewObjLiquidation.AmbulatoryDisabilityAuthorizationNumber = 0
            NewObjLiquidation.AmbulatoryDisabilityValue = 0
            NewObjLiquidation.DisabilityHospitalInitialDate = Nothing
            NewObjLiquidation.DisabilityHospitalEndDate = Nothing
            NewObjLiquidation.DisabilityHospitalReleasedNumber = 0
            NewObjLiquidation.DisabilityHospitalDays = 0
            NewObjLiquidation.DisabilityHospitalValue = 0
            NewObjLiquidation.MaternityLeaveDays = 0
            NewObjLiquidation.MaternityLeaveInitialDate = Nothing
            NewObjLiquidation.MaternityLeaveEndDate = Nothing
            NewObjLiquidation.MaternityLeaveAutorizationNumber = 0
            NewObjLiquidation.VacationInitialDate = Nothing
            NewObjLiquidation.MaternityLeaveValue = 0
            NewObjLiquidation.VacationEndDate = Nothing
            NewObjLiquidation.LicenseDays = 0
            NewObjLiquidation.LicenseValue = 0
            NewObjLiquidation.UnpaidLicenseValue = 0
            NewObjLiquidation.UnpaidLicenseAutorizarionNumber = 0
            NewObjLiquidation.UnpaidLicenseDays = 0
            NewObjLiquidation.UnpaidLicenseInitialDate = Nothing
            NewObjLiquidation.UnpaidLicenseEndDate = Nothing
            NewObjLiquidation.SanctionInitialDate = Nothing
            NewObjLiquidation.SanctionEndDate = Nothing
            NewObjLiquidation.SanctionValue = 0
            NewObjLiquidation.SanctionDays = 0
            NewObjLiquidation.OccupationalRisksContributionValue = 0
            NewObjLiquidation.OccupationalRisksDisabilityValue = 0
            NewObjLiquidation.OccupationalRisksDays = 0
            NewObjLiquidation.OccupationalRisksDisabilityAutorizationNumber = 0
            NewObjLiquidation.OccupationalRisksDisabilityInitialDate = Nothing
            NewObjLiquidation.OccupationalRisksDisabilityEndDate = Nothing
            NewObjLiquidation.OccupationalRisksFundId = 0
            NewObjLiquidation.PermissionsValue = 0 'TotalPermission
            NewObjLiquidation.UnemploymentAccumulated = 0
            NewObjLiquidation.CompensationAccumulated = 0 ' Revisar OJO
            NewObjLiquidation.VacationHealthJCB = 0 ' Programa Antiguo así
            NewObjLiquidation.VacationValueEnjoy = 0
            NewObjLiquidation.VacationHealthContributionValueEmployee = 0
            NewObjLiquidation.VacationHealthContributionValueEmployer = 0 'VacationHealthEmployer
            NewObjLiquidation.VacationPensionJCB = 0 ' Programa Antiguo así
            NewObjLiquidation.VacationPensionContributionValueEmployee = 0
            NewObjLiquidation.VacationPensionContributionValueEmployer = 0 'VacationPensionEmployer
            NewObjLiquidation.AccountingVouchersNumber = 0 ' Programa Antiguo así
            NewObjLiquidation.TotalAccrued = 0
            NewObjLiquidation.TotalDeducted = 0
            NewObjLiquidation.TotalPaid = 0
            NewObjLiquidation.PermissionDays = 0 ' DayPermission
            NewObjLiquidation.SenaContributionValue = 0
            NewObjLiquidation.FamilyCompensationFundContributionValue = 0
            NewObjLiquidation.ICBFContributionValue = 0
            NewObjLiquidation.ParafiscalContribution = 0
            NewObjLiquidation.ProvisionsValue = 0
            NewObjLiquidation.VacationNumberBussinesDays = 0 ' Revisar OJO
            NewObjLiquidation.CompletePayroll = 0
            NewObjLiquidation.PayrollProcessDate = Date.Today()
            NewObjLiquidation.PayrollConfirmationDate = Date.Today()
            NewObjLiquidation.PayrollProcessUser = 1
            NewObjLiquidation.PayrollConfirmationUser = 1


            NewObjLiquidation.IBCUnemployment = 0
            NewObjLiquidation.IBCSENA = 0
            NewObjLiquidation.IBCCompensationFund = 0
            NewObjLiquidation.IBCICBF = 0
            NewObjLiquidation.IBCUnemploymentNoSanctions = 0  'IBCSeveranceNoSanctions + VacationValueCash
            NewObjLiquidation.IBCVacation = 0
            NewObjLiquidation.IBCIncentivePayment = 0
            NewObjLiquidation.IBCOccupationalRisks = 0



            NewObjLiquidation.QuotedOccupationalRisksDays = 0 'CompensationFundDays
            NewObjLiquidation.QuotedCompensationDays = 0 'CompensationFundDays

            'Campos de Provisiones
            NewObjLiquidation.ProvisionIncentive = 0 'TotalMaxNumberIncentiveProvision
            NewObjLiquidation.ProvisionVacation = 0
            NewObjLiquidation.ProvisionInterestsUnemployment = 0

            NewObjLiquidation.BankId = ObjContract.BankId
            NewObjLiquidation.BankAccountNumber = ObjContract.BankAccountNumber

            NewObjLiquidation.UnemployementFundId = 0
            NewObjLiquidation.HousingDeductionValue = 0
            NewObjLiquidation.ProcedureTypeRTF = ObjContract.Employee.ProcedureTypeRTF
            NewObjLiquidation.DependentsDeduction = 0
            NewObjLiquidation.DependentsSupplementary = 0

            If ObjContract.FunctionalUnit IsNot Nothing Then
                NewObjLiquidation.CostCenterId = ObjContract.FunctionalUnit.CostCenterId
            Else
                NewObjLiquidation.CostCenterId = ObjContract.Employee.CostCenterId
            End If

            NewObjLiquidation.InitialContractNumber = ObjContract.InitialContractNumber

            ActionMessageResult.ObjectEmbbeded = NewObjLiquidation



        Catch ex As Exception
            ActionMessageResult.StateResult = False
            ActionMessageResult.ObjectEmbbeded = Nothing
        End Try

        Return ActionMessageResult

    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _CostDistributionDomain.Dispose()
            End If
            _groupRepository = Nothing
            _liquitationRepository = Nothing
            _autorizationConceptRepository = Nothing
            _scheduleDetailRepository = Nothing
            _noveltyRepository = Nothing
            _retentionRepository = Nothing
            _vacationRepository = Nothing
            _incentivePaymentRepository = Nothing
            _unemployedLiquidationRepository = Nothing
            _IAgreementsRepository = Nothing
            _ManualConceptsRepository = Nothing
            _CostDistributionDomain = Nothing
            _employeeRepository = Nothing
            _FundsRepository = Nothing
            _retroactiveRepository = Nothing
            _PayrollSettings = Nothing
            _thirdPartyRepository = Nothing
            _FixedPercentageRepository = Nothing
            _conceptRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    ''' <summary>
    ''' Recalcula el IBC usando el salario mínimo como base, reemplazando las variables
    ''' de la fórmula original y evaluando el resultado.
    ''' </summary>
    ''' <param name="OriginalFormula">Fórmula original del cálculo del IBC.</param>
    ''' <param name="minimimunSalary">Salario mínimo legal vigente usado en el cálculo.</param>
    ''' <param name="DaysWorkedEmployee">Días trabajados por el empleado.</param>
    ''' <param name="PayrollDays">Días de nómina del periodo.</param>
    ''' <returns>
    ''' Valor del IBC recalculado y redondeado hacia arriba. Retorna 0 si la fórmula no se evalúa correctamente.
    ''' </returns>
    Private Function RecalculateIBCWithMinimumSalary(
        OriginalFormula As String,
        minimimunSalary As Decimal,
        DaysWorkedEmployee As Integer,
        PayrollDays As Integer
    ) As Decimal

        Dim TempFormula As String = OriginalFormula

        If TempFormula.Trim().StartsWith("Round(") AndAlso TempFormula.Trim().EndsWith(")") Then
            TempFormula = TempFormula.Trim().Substring(6, TempFormula.Trim().Length - 7)
        End If
        TempFormula = Replace(TempFormula, "[Sueldo Contrato]", Format(minimimunSalary, "0.00").Replace(",", "."))
        TempFormula = Replace(TempFormula, "[Dias Trabajados]", DaysWorkedEmployee.ToString.Replace(",", "."))
        TempFormula = Replace(TempFormula, "[Días Nómina]", PayrollDays.ToString.Replace(",", "."))

        Dim TempResult = Utils.EvalExpression(TempFormula)
        If TempResult.StateResult = True Then
            Return Math.Ceiling(Convert.ToDecimal(TempResult.ObjectEmbbeded))
        End If

        Return 0
    End Function

End Class


