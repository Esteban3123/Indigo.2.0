

Public Interface ILiquidationDomain

    ''' <summary>
    ''' Obtiene la Fecha Final Nómina
    ''' </summary>
    ''' <param name="payrollLiquidation">Forma Liquidación Nómina</param>
    ''' <param name="payrollStarDate">Fecha Inicial Nómina</param>
    ''' <returns>Fecha Final nómina</returns>
    ''' <remarks></remarks>
    Function GetEndPayrollDate(payrollLiquidation As Integer, payrollStarDate As Date) As Date

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
    ''' Evalúa y ejecuta una función escrita en código VB
    ''' </summary>
    ''' <typeparam name="T">Tipo de dato que retorna la función a evaluar</typeparam>
    ''' <param name="codeFunc">Código de la función a evaluar</param>
    ''' <returns>El resultado de tipo T</returns>
    Function EvalFunc(Of T)(ByVal codeFunc As String) As T

    ''' <summary>
    ''' Función 360 días para calcular en dos rangos de Fechas siempre con meses de 30 días
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Días Totales (Calculado en Meses de 30 días)</returns>
    ''' <remarks></remarks>
    Function Days360(ByVal initialDate As Date, ByVal endDate As Date) As Integer

    ''' <summary>
    ''' Función para Redondear Valores
    ''' </summary>
    ''' <param name="ValueIn">Valor a Redondear</param>
    ''' <returns>Valor Redondeado</returns>
    ''' <remarks></remarks>
    Function RoundedValues(ByVal ValueIn As Double) As Double

    ''' <summary>
    ''' Obtiene el Número de Horas por Concepto
    ''' </summary>
    ''' <param name="handlesTurnsChart">Maneja Cuadro de Turno</param>
    ''' <param name="PayrollDays">Días de la Nómina</param>
    ''' <param name="AuthorizationConcept">Objeto Conceptos Autorizados</param>
    ''' <param name="HoursDaily">Horas Diarias</param>
    ''' <param name="ScheduleEmployee">Objeto Detalle del Cuadro de Turno por Empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function HourByConcept(ByVal handlesTurnsChart As Boolean, ByVal PayrollDays As Integer, AuthorizationConcept As AuthorizationConcept, HoursDaily As Integer, ScheduleEmployee As List(Of ScheduleDetail)) As Integer

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
    Function CalculatinDaysInabilities(ByVal InitialDateInability As Date, ByVal EndDateInability As Date, ByVal PayrollInitialDate As Date, ByVal PayrollEndDate As Date, ByVal TotalInabilityDays As Integer) As Integer

    ''' <summary>
    ''' Obtiene los Días de la nómina
    ''' </summary>
    ''' <param name="PayrollInitialDate">Fecha Inicial Nómina</param>
    ''' <param name="PayronEndDate">Fecha Final Nómina</param>
    ''' <param name="PayrollMonth">Nómina de 30 días / Días Calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function PayrollDays(ByVal PayrollInitialDate As Date, ByVal PayronEndDate As Date, ByVal payrollMonth As Char) As Integer

    Function ReplaceData(payrollParameter As PayrollParameter, BasicSalary As String, DaysWorkedEmployee As String, WorkHours As String, PayrollDays As String, FormulaConcept As String, employeePayroll As Contract, _
                         BaseIBCPension As Double, BaseIBCHealth As Double, IBCHealth As Double, ByVal BaseIBCHealthEmployer As Double, BaseIBCPensionEmployer As Double, IBCSENA As Double, IBCICBF As Double, IBCPeriod As Double, _
                         IBCSeverance As Double, IBCCompensationFund As Double, IBCPension As Double, IBCARP As Double, ValueAmbulatoryInability As Double, ValueHospitalInability As Double, ValueMaternity As Double, ValueSanctions As Double, _
                         TotalPeriodDaysInabilities As Integer, HospitalInabilityDays As Integer, RemuneratedLicensesDays As Integer, UnpaidLicensesDays As Integer, ValueUnpaidLicenses As Double, ValueProfesionalInabilities As Double, _
                         HealthEmployee As Double, PensionEmployee As Double, BaseIBCArp As Double, BaseParafiscalCompensationFund As Double, BaseParafiscalICBF As Double, RetentionValue As Double, ProvisionDays As Integer, ProfessionalRiskPercentage As Double, _
                         VacationValueCash As Double, AdjustVacationValue As Double, incentiePaymentValue As Double, unemployedInterestValue As Double) As Dictionary(Of String, String)

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
    Function GetIBCPension(ByVal EmployeeSalary As Double, ByVal LegalSalaryMinimum As Double, ByVal VarIBCPension As Double, PayrollDays As Integer, ByVal UnpaidLicensesDay As Integer, ByVal AccruedValue As Double, ByVal EmployeePensionPercentage As Double) As Double

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
    Function GetIBCHealth(ByVal VarIBCHealth As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal EmployeeHealthPercentage As Double, ByVal AccruedValue As Double, ByVal UnpaidLicensesDay As Integer) As Double

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
    Function GetIBCHealthEmployer(ByVal VarIBCHealth As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal VacationDays As Integer, ByVal PayrollDays As Integer, ByVal EmployeeDays As Integer, ByVal VarIBCHealthEmployee As Double, ByVal HealthIBC As Double, ByVal EmployerHealthContributionPercentage As Double, TotalDaysLicensesUnpaid As Integer) As Double

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
    Function GetIBCPensionEmployer(ByVal PensionIBC As Double, ByVal LegalSalaryMinimum As Double, ByVal EmployeeSalary As Double, ByVal UnpaidLicensesDay As Integer, PayrollDays As Integer, EmployerPensionContributionPercentage As Double) As Double

    ''' <summary>
    ''' Genera la liquidación de un empleado
    ''' </summary>
    ''' <param name="payrollParameter">Parámetros de Nómina</param>
    ''' <param name="payrollEmployee">Empleados Nómina</param>
    ''' <param name="employeeAutorizationConcept">Conceptos Autorizados de Nómina</param>
    ''' <param name="ScheduleEmployee">Schedule del Empleado</param>
    ''' <param name="employeeNovelty">Novedades del Empleado</param>
    ''' <param name="PayrollConfirmada">Nómina Confirmada</param>
    ''' <param name="payrollEndDate">Fecha Fin Nómina</param>
    ''' <param name="PayrollMonth">Nómina Mensual</param>
    ''' <param name="PayrollStarDate">Fecha Inicio Nómina</param>
    ''' <param name="PayrollLiquidation">Liquidación Nómina</param>
    ''' <param name="EmployeeLiquidation">Empleados de Liquidación</param>
    ''' <param name="retention">Retención</param>
    ''' <returns>Liquidation</returns>
    ''' <remarks></remarks>
    Function EmployeeLiquitadion(payrollParameter As PayrollParameter, payrollEmployee As List(Of Contract), employeeAutorizationConcept As List(Of AuthorizationConcept), ScheduleEmployee As GetScheduleDetailByEmployeeFuctionalUnitBetweenDateWithoutNovelties, employeeNovelty As ILiquidationDomain.GetNoveltyByEmployeeDateInitialEnd, PayrollConfirmada As String, payrollEndDate As Date, PayrollMonth As String, PayrollStarDate As Date, PayrollLiquidation As Byte, EmployeeLiquidation As ILiquidationDomain.EmployeePayrollCheckLiquidation, retention As Retention, valida As ILiquidationDomain.GetPayrollValidation, VacationEmployee As ILiquidationDomain.GetEmployeeVacation, IncentivePayment As ILiquidationDomain.GetIncentivePaymentByContractIdPayrollNextDate, unemployedEmployee As ILiquidationDomain.GetUnemployedLiquidationByContractIdInterestPayDay, completePayroll As Boolean) As List(Of Liquidation)

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
    Function GetBaseParafiscalCompensationFund(ByVal IBCFundValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ByVal CompensationFundContributionPercentage As Double) As Double

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
    Function GetBaseParafiscalICBF(ByVal IBCICBFValue As Double, ByVal PayrollDays As Integer, LegalSalaryMinimun As Double, PaidLicensesDays As Integer, UnpaidLicensesDays As Integer, HospitalInabilitiesDays As Integer, InabilitiesPeriodDays As Integer, EmployeeSalary As Double, VacationDays As Integer, ICBFContributionPercentage As Double) As Double

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


End Interface
