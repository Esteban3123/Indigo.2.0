Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

Public Interface ILiquidationFunctions


    Function AdjustDominicalValue(ByVal ScheduleDetailEmployee As List(Of ScheduleDetail), ListHolidays As List(Of Holiday), ByVal group As Group, Optional LastMonthInitialDate As Date = Nothing, Optional LastMonthEvent As Boolean = False) As List(Of Tuple(Of String, Decimal))
    Function HourByConcept(ByVal handlesTurnsChart As Boolean, ByVal PayrollDays As Integer, AuthorizationConcept As AuthorizationConcept, HoursDaily As Integer, ByVal ScheduleDetailEmployee As List(Of ScheduleDetail)) As Decimal
    Function AdjustFestiveHours(ByVal ScheduleDetailEmployee As List(Of ScheduleDetail), ListHolidays As List(Of Holiday), TmpScheduleDetailEmployeeLastMonth As List(Of ScheduleDetail), ByVal PayrollStarDate As Date) As Decimal

    Function NoveltyHourByConcept(ByVal handlesTurnsChart As Boolean, ByVal PayrollDays As Integer, AuthorizationConcept As AuthorizationConcept, HoursDaily As Integer, ByVal ScheduleDetailEmployee As List(Of NoveltyScheduleDetail)) As Decimal
    Function ValidatedConceptNoveltySchedule(ScheduleDetailEmployee As List(Of NoveltyScheduleDetail), PayrollSettings As PayrollSettings) As ActionMessageResult(Of List(Of Liquidation))

    ''' <summary>
    ''' Obtiene el valor Base de Cálculo de Pensión del Empleado
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="VarIBCPension">Valor IBC de Pensión</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="UnpaidLicensesDay">Días de Licencias No remuneradas</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Pénsión</returns>
    ''' <remarks></remarks>
    Function GetIBCPension(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimum As Double, ByVal VarIBCPension As Double, PayrollDays As Integer, ByVal UnpaidLicensesDay As Integer, ByVal AccruedValue As Double, ByVal EmployeePensionPercentage As Double, VacationDays As Integer, RetirementFlag As Boolean, IBCHealth As Double, NumberOfContract As Integer, DaysWorkedEmployee As Integer, InabilitiesDays As Integer, WorkedDays As Integer, FlagIBCPension As Boolean) As Double

    ''' <summary>
    ''' Obtiene el Valor Base de Cálculo de Salud del Empleado
    ''' </summary>
    ''' <param name="VarIBCHealth">Valor IBC Salud del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Salud</returns>
    ''' <remarks></remarks>
    ''' Concepto 501
    Function GetIBCHealth(ByVal VarIBCHealth As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal EmployeeHealthPercentage As Double, ByVal AccruedValue As Double, ByVal UnpaidLicensesDay As Integer, IBCPension As Double, NumberContract As Integer, PeriodDays As Integer, RetirementFlag As Boolean, FlagIBCHealth As Boolean) As Double

    ''' <summary>
    ''' Obtiene la base del Fondo de la Caja de Compensación
    ''' </summary>
    ''' <param name="IBCFundValue">Valor IBC del Fondo</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Legal Mínimo</param>
    ''' <param name="PaidLicensesDays">Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="CompensationFundContributionPercentage">Porcentaje de la Caja de Compensación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseParafiscalCompensationFund(ByVal IBCFundValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ByVal CompensationFundContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer, IBCPension As Double) As Double

    ''' <summary>
    ''' Obtiene la base del Fondo de la Caja de Compensación
    ''' </summary>
    ''' <param name="IBCFundValue">Valor IBC del Fondo</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Legal Mínimo</param>
    ''' <param name="PaidLicensesDays">Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="CompensationFundContributionPercentage">Porcentaje de la Caja de Compensación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseParafiscalCompensationFundIntegral(ByVal IBCFundValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ByVal CompensationFundContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double

    ''' <summary>
    ''' Obtiene la base del ICBF
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="ICBFContributionPercentage">Porcentaje del ICBF</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseParafiscalICBF(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ICBFContributionPercentage As Double, SalaryMonth As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double

    ''' <summary>
    ''' Obtiene la base del ICBF Integral
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="ICBFContributionPercentage">Porcentaje del ICBF</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseParafiscalICBFIntegral(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ICBFContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer) As Double

    ''' <summary>
    ''' Obtiene el Valor Base de Salud del Patrono
    ''' </summary>
    ''' <param name="VarIBCHealth">Valor IBC de Salud</param>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días de Vacaciones</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <param name="VarIBCHealthEmployee">Valor IBC de la Salud del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Salud del Patrono</returns>
    ''' <remarks></remarks>
    Function GetIBCHealthEmployer(ByVal LegalSalaryMinimum As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal IBCHealth As Double, EmployerHealthContributionPercentage As Double, TotalDaysLicensesUnpaid As Integer, IBCPension As Double, EmployeeSalary As Double, EmployeeHealthContributionPercentage As Double, NumberOfContract As Integer, PeriodDays As Integer, TotalPeriodDaysInabilities As Integer) As Double


    ''' <summary>
    ''' Obtiene el Valor Base de Salud del Patrono para Salarios Integrales
    ''' </summary>
    ''' <param name="LegalSalaryMinimum">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <returns>Valor Base de Cálculo de Salud del Patrono</returns>
    ''' <remarks></remarks>
    ''' Concepto 913
    Function GetIBCHealthEmployerIntegral(ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal PayrollDays As Integer, ByVal IBCHealth As Double, EmployerHealthContributionPercentage As Double, TotalDaysLicensesUnpaid As Integer, NumberOfContract As Integer, PeriodDays As Integer) As Double

    ''' <summary>
    ''' Obtiene la base del Sena Integral
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="SenaContributionPercentage">Porcentaje del SENA</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseSenaIntegral(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, SenaContributionPercentage As Double, NumberOfContract As Integer, DaysWorkedEmployee As Integer) As Double

    ''' <summary>
    ''' Obtiene la base del Sena
    ''' </summary>
    ''' <param name="IBCICBFValue">Valor IBC ICBF</param>
    ''' <param name="PayrollDays">Días Nómina</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal Vigente</param>
    ''' <param name="PaidLicensesDays">Días de Licencias Remuneradas</param>
    ''' <param name="UnpaidLicensesDays">Días de Licencias NO Remuneradas</param>
    ''' <param name="HospitalInabilitiesDays">Días Incapacidades Hospitalarias</param>
    ''' <param name="InabilitiesPeriodDays">Días de Incapacidades en el Periodo</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="VacationDays">Días Vacaciones</param>
    ''' <param name="SenaContributionPercentage">Porcentaje del SENA</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseSena(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, SenaContributionPercentage As Double, NumberOfContract As Integer, DaysWorkedEmployee As Integer) As Double


    ''' <summary>
    ''' Obtiene el Valor Base de Pensión del Patrono
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Legal Mínimo Vigente</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="UnpaidLicensesDay">Días de Licencias</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <param name="VarIBCPensionEmployee">Valor IBC Pensión del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Pensión del Patrono</returns>
    ''' <remarks></remarks>
    ''' Concepto 902
    Function GetIBCPensionEmployer(ByVal PensionIBC As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal UnpaidLicensesDay As Integer, PayrollDays As Integer, NumberOfContract As Integer, PeriodDays As Integer, TotalInabilitiesDays As Integer, IBCHealth As Double, WorkedDays As Integer, VacationDays As Integer) As Double

    ''' <summary>
    ''' Obtiene el Valor Base de Pensión del Patrono
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimum">Salario Legal Mínimo Vigente</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="UnpaidLicensesDay">Días de Licencias</param>
    ''' <param name="EmployeeDays">Días del Empleado</param>
    ''' <param name="VarIBCPensionEmployee">Valor IBC Pensión del Empleado</param>
    ''' <returns>Valor Base de Cálculo de Pensión del Patrono</returns>
    ''' <remarks></remarks>
    Function GetIBCPensionEmployerIntegral(ByVal PensionIBC As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal UnpaidLicensesDay As Integer, PayrollDays As Integer, NumberOfContract As Integer, PeriodDays As Integer) As Double

    ''' <summary>
    ''' Obtiene el valor Base del ARL
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal</param>
    ''' <param name="ARPDays">Días ARL</param>
    ''' <param name="IBCArp">IBC ARL</param>
    ''' <param name="ProfessionalRiskPercentage">Porcentaje Riesgo Profesional</param>
    ''' <returns>Valor Base de Cálculo de ARP</returns>
    ''' <remarks></remarks>
    Function GetBaseARP(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimun As Double, ByVal ARPDays As Integer, ByVal IBCArp As Double, ByVal ProfessionalRiskPercentage As Double) As Double

    ''' <summary>
    ''' Obtiene el valor Base del ARL Integral
    ''' </summary>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="LegalSalaryMinimun">Salario Mínimo Legal</param>
    ''' <param name="ARPDays">Días ARL</param>
    ''' <param name="IBCArp">IBC ARL</param>
    ''' <param name="ProfessionalRiskPercentage">Porcentaje Riesgo Profesional</param>
    ''' <returns>Valor Base de Cálculo de ARP</returns>
    ''' <remarks></remarks>
    Function GetBaseARPIntegral(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimun As Double, ByVal ARPDays As Integer, ByVal IBCArp As Double, ByVal ProfessionalRiskPercentage As Double) As Double

    ' Concepto 532
    ''' <summary>
    ''' Obtiene la Base de Cálculo para el Concepto Fondo de Seguridad Pensional Integral 
    ''' </summary>
    ''' <param name="HealthIBC">IBC Salud</param>
    ''' <param name="LegalSalaryMinium">Salario Mínimo Legal Vigente</param>
    ''' <param name="EmployeeSalary">Salario del Empleado</param>
    ''' <param name="PayrollDays">Días de Nómina</param>
    ''' <param name="PensionIBC">IBC Pensión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseSecurityPensionalIntegralFound(HealthIBC As Double, LegalSalaryMinium As Double, EmployeeSalary As Double, PayrollDays As Integer, PensionIBC As Double)

    Function EmployeeFundContract(ListFundContract As List(Of FundContract)) As List(Of Tuple(Of String, Integer, Integer, String, Decimal))

    Function AnalisisVacation(liquidationPayrollDomain As ILiquidationDomain, PayrollStarDate As Date, PayrollEndingDate As Date, ListVacation As List(Of Vacation), ContractEmployee As Entities.Contract, ByRef PaidVacation As Byte, ByRef PaidCredit As Byte, ByRef VacationInitialDate As Date, ByRef VacationEndDate As Date, ByRef VacationPaidType As Integer, ByRef EnjoyDays As Integer, ByRef VacationType As Byte) As List(Of Tuple(Of String, Decimal))

    Function AgreementsPaid(PayrollStarDate As Date, PayrollEndingDate As Date, ListAgreements As List(Of AgreementsC), ByRef PaidVacation As Byte, ByRef VacationDays As Integer, ByRef NumAgreement As Integer, ByRef AgreementsDId As Integer, ByRef ThirdPartyAgreementsName As String, ByRef ThirdPartyAgreementsId As Integer, ByRef AgreementsId As Integer) As Decimal

    Function HourManualConcept(PayrollStarDate As Date, PayrollEndingDate As Date, ListManualConcept As List(Of ManualConcepts), ByRef ManualConceptPaidHourFlag As Boolean) As Decimal

    Function ValueManualConcept(PayrollStarDate As Date, PayrollEndingDate As Date, ObjManualConcept As ManualConcepts, ByRef ManualConceptPaidFlag As Boolean, QuarterFlag As Byte) As Decimal

    Function ValueAFCManualConcept(PayrollStarDate As Date, PayrollEndingDate As Date, ObjManualConcept As ManualConcepts, ByRef ManualConceptPaidFlag As Boolean) As Decimal

    Function ValueForeclousure(PayrollStarDate As Date, PayrollEndingDate As Date, ListForeclousure As List(Of Foreclousure), ByRef PercentageForeclousure As Decimal) As Decimal

    Function AnalisisNovelty(liquidationPayrollDomain As ILiquidationDomain, ContractEmployee As Entities.Contract, PayrollStarDate As Date, PayrollEndingDate As Date, ListNovelties As List(Of Novelty), PayrollDays As Integer, noveltyRepository As INoveltyRepository, ByRef VacationInitialModifiedDate As Date?, ByRef VacationEndModifiedDate As Date?, ByVal SessionValues As SessionValues, ByRef shouldExcludeAccruedDeducted As Boolean, Optional Day31 As Boolean = False) As List(Of Tuple(Of String, Integer, Decimal, Decimal, Date, Date, String, Tuple(Of Integer, Decimal, Decimal, Integer, Integer, Byte)))

    Function VacationAdjustNovelty(LiquidationDomain As LiquidationDomain, enjoyDays As Integer, VacationInitialDate As Date, PayrollStarDate As Date, PayrollEndDate As Date, VacationEndModifiedDate As Date?, InitialDateInability As Date?, VacationInitialModifiedDate As Date) As Integer

    Function PermanentInability(Employee As Domain.Payroll.Entities.Employee, PayrollStarDate As Date, PayrollEndDate As Date, LiquidationDomain As LiquidationDomain) As Integer

    Function GetDaysInabilityVacation(PayrollEndDate As Date, PayrollStarDate As Date, InabilityEndDate As Date?, InabilityStartDate As Date?, VacationEndDate As Date, VacationStartDate As Date, LiquidationDomain As LiquidationDomain) As Integer

End Interface
