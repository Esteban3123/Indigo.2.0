Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ILiquidationDomain
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene la Fecha Final Nómina
    ''' </summary>
    ''' <param name="payrollLiquidation">Forma Liquidación Nómina</param>
    ''' <param name="payrollStarDate">Fecha Inicial Nómina</param>
    ''' <returns>Fecha Final nómina</returns>
    ''' <remarks></remarks>
    Function GetEndPayrollDate(payrollLiquidation As Integer, payrollStarDate As Date) As Date

    ''' <summary>
    ''' Obtiene la Fecha Inicial Nómina
    ''' </summary>
    ''' <param name="payrollLiquidation">Forma Liquidación Nómina</param>
    ''' <param name="payrollEndDate">Fecha Final Nómina</param>
    ''' <returns>Fecha Final nómina</returns>
    ''' <remarks></remarks>
    Function GetStarPayrollDate(payrollLiquidation As Integer, payrollEndDate As Date) As Date

    ''' <summary>
    ''' Obtiene los días trabajados del Empleado sin descuentos
    ''' </summary>
    ''' <param name="EmployeeInitialDate">Fecha Inicial Empleado</param>
    ''' <param name="employeEndDate">Fecha Fin Empleado</param>
    ''' <param name="payrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="payrollEndDate">Fecha Final Nómina</param>
    ''' <param name="payrollMonth">Mes Nómina</param>
    ''' <returns>Días trabajados Empleado</returns>
    Function DaysWorkedEmployee(ByVal EmployeeInitialDate As Date, ByVal employeEndDate As Date, ByVal payrollInitialDate As Date, ByVal payrollEndDate As Date, payrollMonth As Char) As Integer

    ''' <summary>
    ''' Función 360 días para calcular en dos rangos de Fechas siempre con meses de 30 días
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Días Totales (Calculado en Meses de 30 días)</returns>
    ''' <remarks></remarks>
    Function Days360(ByVal initialDate As Date, ByVal endDate As Date) As Integer

    Function Days365(ByVal initialDate As Date, ByVal endDate As Date) As Integer


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
    Function CalculatingValueInability(ByVal InitialDateInability As Date, ByVal EndDateInability As Date, ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal TotalInabilityDays As Integer, ByVal ValueTotalInability As Double, ByVal PeriodDays As Integer) As Double

    ''' <summary>
    ''' Calcula los días de las Incapacidades
    ''' </summary>
    ''' <param name="InitialDateInability">Fecha Inicial Incapacidad</param>
    ''' <param name="EndDateInability">Fecha Final Incapacidad</param>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <param name="TotalInabilityDays">Días Totales Incapacidad</param>
    ''' <returns>Días de Incapacidad</returns>
    ''' <remarks></remarks>
    Function CalculatinDaysInabilities(ByVal InitialDateInability As Date, ByVal EndDateInability As Date, ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal TotalInabilityDays As Integer, InabilityState As Byte, Day31 As Boolean, NumberContract As Integer, groupEmployee As Group, ContractInitialDate As Date, ContractEndingDate As Date, ObjNovelty As Novelty, Optional OtherInabilitiesDays As Integer = 0) As Integer

    ''' <summary>
    ''' Obtiene los Días de la nómina
    ''' </summary>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayronEndDate">Fecha Final Nómina</param>
    ''' <param name="PayrollMonth">Nómina de 30 días / Días Calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function PayrollDays(ByVal PayrollInitialDate As Date, ByVal PayronEndDate As Date, ByVal payrollMonth As Char) As Integer

    Function ReplaceDataLiquidation(payrollParameter As PayrollParameter, BasicSalary As Decimal, DaysWorkedEmployee As Integer, WorkHours As Decimal, PayrollDays As Integer, FormulaConcept As String, employeePayroll As Contract, BaseIBCPension As Double, BaseIBCHealth As Double, IBCHealth As Double, BaseIBCHealthEmployer As Double,
                                BaseIBCHealthEmployerIntegral As Double, BaseIBCPensionEmployer As Double, BaseIBCPensionEmployerIntegral As Double, IBCSENA As Double, IBCICBF As Double, IBCPeriod As Double, IBCSeverance As Double, IBCCompensationFund As Double, IBCPension As Double, IBCARP As Double, ValueAmbulatoryInability As Double, ValueHospitalInability As Double, ValueMaternity As Double,
                                ValueSanctions As Double, TotalPeriodDaysInabilities As Integer, HospitalInabilityDays As Integer, RemuneratedLicensesDays As Integer, FamilyDay As Short, UnpaidLicensesDays As Integer, ValueUnpaidLicenses As Double, ValueProfesionalInabilities As Double, HealthEmployee As Double,
                                PensionEmployee As Double, BaseIBCArp As Double, BaseIBCArpIntegral As Double, BaseParafiscalCompensationFund As Double, BaseParafiscalCompensationFundIntegral As Double, BaseParafiscalICBF As Double, BaseParafiscalICBFIntegral As Double, RetentionValue As Double, ProvisionDays As Integer, ProfessionalRiskPercentage As Double, VacationValue As Double, AdjustVacationValue As Double,
                                incentiePaymentValue As Double, unemployedInteresValue As Double, transportHelpValue As Double, BaseIBCSenaIntegral As Double, BaseIBCSena As Double, TotalAccrued As Double, TotalDeducted As Double, BaseIBCSecurityPensionalIntegralFound As Double, CalamidadDomesticaDays As Integer, VoluntaryPensionValue As Double, VoluntaryHealthValue As Double, IBCIncentivePayment As Double, BonusServices As Double,
                                ManualConceptValue As Double, SindicateFlag As Byte, PaidVacation As Byte, RepresentationCost As Boolean, PaidValueAverageIncentiveServices As Double, DailyHours As Integer, PayrollDateLiquidated As Date, ContractInitialDate As Date, NumberContract As Integer, GeneralInabilityValue As Double, ValueLuto As Double, ValuePaternity As Double, TotalIBCSolidaridad As Double, EmployeeType As String, AnoLaborado As Boolean,
                                BonificationValue As Double, TotalVacationDays As Integer, ContractVacationDays As Integer, ItemContract As Integer, ValueRetroactiveBonification As Decimal, RecreationBonificationValue As Decimal, VacationalIncrease As Decimal, VacationIncentivePayment As Decimal, VacationCompensationValue As Decimal, PaidCredit As Byte, HoursMinPosition As Integer, HoursMaxPosition As Integer, IBCVacation As Double, DecemberIncentivePaymentAverage As Double, IncentivePaymentAverage As Double,
                                VacationIncentiveValue As Double, ValueForeclousure As Double, PercentageForeclousure As Decimal, codeWorkCenter As String, HolidaysHours As Decimal, BasicSalaryDaily As Decimal, AjusteDominical As Decimal, AjusteHorasExtrasDiurnas As Decimal, AjusteHorasExtrasNocturnas As Decimal, AjusteHorasExtrasDiurnasFestivas As Decimal, AjusteHorasExtrasNocturnasFestivas As Decimal, AjusteRecargoNocturnasFestivas As Decimal, VacationIBCValue As Decimal, VacationPaidValue As Decimal,
                                    PeriodVacationDays As Integer, BaseForeclousure As Decimal, PosesitionDate As Date, ValueRetroactivePaid As Decimal, TransportDays As Integer, HorasAjustesDominicalEvento As Decimal, HorasAjusteExtrasDiurnasFestivasEvento As Decimal, HorasAjusteExtrasNocturnasFestivasEvento As Decimal, HorasAjusteExtrasNocturnasEvento As Decimal, HorasAjusteExtrasDiurnasEvento As Decimal, QuarterFlag As Byte, RepresentationCostValue As Decimal, VacationIncentiveValueProvision As Decimal,
                                           SanctionDays As Integer, ContributorAbroad As Boolean, IBCLastPeriod As Decimal, AmbulatoryInabilityEmployeerDays As Integer, AmbulatoryInabilityERPDays As Integer, ValueAmbulatoryEmployeerInability As Decimal, ValueAmbulatoryERPInability As Decimal, HospitalInabilityEmployeerDays As Integer, HospitalInabilityERPDays As Integer, ValueHospitalEmployeerInability As Decimal, ValueHospitalERPInability As Decimal, DaysPermission As Decimal, PaidLeaveDays As Decimal, VacationDaysInCash As Decimal,
                                    LutoDays As Decimal, EmployerOccupationalDisabilityDays As Decimal, EmployerOccupationalDisabilityAmount As Decimal, ERPOccupationalDisabilityDays As Decimal, ERPOccupationalDisabilityAmount As Decimal) As Dictionary(Of String, String)

    ''' <summary>
    ''' Función para Averiguar los Fondos ACTIVOS de un Empleado
    ''' </summary>
    ''' <param name="EmployeeContract">Objeto Contrato Empleado</param>
    ''' <param name="FundType">Tipo de Fondo</param>
    ''' <param name="Voluntary">Voluntario SI - NO</param>
    ''' <returns>Id del Fondo</returns>
    ''' <remarks></remarks>
    Function GetFundEmployee(ByVal EmployeeContract As Contract, ByVal FundType As String, ByVal Voluntary As Boolean) As FundContract

    ''' <summary>
    ''' Función que crea los mensajes
    ''' </summary>
    ''' <param name="Description">Descripción del Mensaje</param>
    ''' <param name="ErrorType">Tipo de Error</param>
    ''' <param name="payrollDate">Fecha de la Nómina a la que se le aplica el mensaje</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateMessage(ByVal Description As String, ByVal ErrorType As Boolean, payrollDate As Date) As Message



    ''' <summary>
    ''' Retorna el Cuadro de Turno de un Empleado en un rango de Fechas
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="FunctionalUnitId">Id de la Unidad Funcional</param>
    ''' <param name="PayrollStarDate">Fecha Inicio Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <returns>Lista de Cuadro de Turnos (Detalle)</returns>
    ''' <remarks></remarks>
    Delegate Function GetScheduleDetailByEmployeeFuctionalUnitBetweenDateWithoutNovelties(EmployeeId As Integer, FunctionalUnitId As Integer, PayrollStarDate As Date, PayrollEndDate As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Obtiene las Novedades de un Empleado en un rango de Fechas
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="PayrollStarDate">Fecha Inicio Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <returns>Lista de Novedades</returns>
    ''' <remarks></remarks>
    Delegate Function GetNoveltyByEmployeeDateInitialEnd(EmployeeId As Integer, PayrollStarDate As Date, PayrollEndDate As Date) As List(Of Novelty)

    ''' <summary>
    ''' Obtiene la Validación para saber si a un empleado se le paga nómina o no
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="PayrollLiquidation">Liquidación de Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Delegate Function GetPayrollValidation(EmployeeId As Integer, PayrollLiquidation As Integer, PayrollEndDate As Date) As Boolean

    ''' <summary>
    ''' Lista las Liquidaciones de un Empleado
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Delegate Function EmployeePayrollCheckLiquidation(EmployeeId As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene las Vacaciones que se pagan en una Liquidación
    ''' </summary>
    ''' <param name="PayrollDate">Fecha inicio de la Nómina</param>
    ''' <param name="State">Estado</param>
    ''' <returns>Lista de Vacaciones</returns>
    ''' <remarks></remarks>
    Delegate Function GetEmployeeVacation(PayrollDate As Date, ByVal State As Byte) As List(Of Vacation)


    ''' <summary>
    ''' Obtiene la lista de Primas por Id del Grupo y Fecha Próxima Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="payrollNextDate">Fecha Próxima Nómina</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Delegate Function GetIncentivePaymentByContractIdPayrollNextDate(groupId As String, PayrollNextDate As Date) As IncentivePayment

    ''' <summary>
    ''' Función que devuelve la liquidación de Cesantia de un Contrato con la Fecha de Pago del Interés de Cesantía
    ''' </summary>
    ''' <param name="ContractId">Id del Contrato</param>
    ''' <param name="InterestPayDay">Fecha Pago Interés</param>
    ''' <returns>Objeto Cesantia</returns>
    ''' <remarks></remarks>
    Delegate Function GetUnemployedLiquidationByContractIdInterestPayDay(ByVal ContractId As String, InterestPayDay As Date) As UnemployedLiquidation

    Function Retention(IBCRTF As Double, ObligatoryPensionEmployee As Double, VoluntaryPensionEmployee As Double, PensionalSolidarityFund As Double, CuentasAFC As Double, RiskValue As Double, ObligatoryHealthEmployee As Double, PrepaidVolunatyrHealthEmployee As Double, Dependientes As Double, HousingDeductedValue As Double,
                       UVTValue As Double, ExceptRTF As Decimal, LegalMinimunSalary As Double, RetentionProcedure As Byte, Contract As Domain.Payroll.Entities.Contract, Employee As Domain.Payroll.Entities.Employee, PayrollEndDate As Date, RepresentationCost As Double, PayrollSettings As PayrollSettings, ListLiquidationLastYear As List(Of Liquidation),
                       Optional CompanySettings As Boolean = 0, Optional UnemployementValue As Double = 0, Optional flagIncentivePayment As Boolean = False, Optional FractionRetencion As Boolean = False, Optional ByRef PercentageFractionRetention As Decimal = 0, Optional List383Concept As List(Of Domain.Payroll.Entities.RetentionConceptRanges) = Nothing, Optional QuarterFlag As Byte? = Nothing) As List(Of Tuple(Of String, Double))


    Function NewExecuteLiquitadion(payrollEmployee As List(Of Domain.Payroll.Entities.Employee), groupEmployee As Group, completePayroll As Boolean, SessionValues As SessionValues, Optional RetirementDate As Date = Nothing, Optional FlagIncentivePayment As Boolean = False) As ActionMessageResult(Of List(Of Liquidation))

    Function LiquidatedExtraTime(ObjGroup As Group, ObjContract As Domain.Payroll.Entities.Contract, ObjPosition As Domain.Payroll.Entities.Position, InitialDate As Date, EndDate As Date) As ActionMessageResult(Of Liquidation)

End Interface
