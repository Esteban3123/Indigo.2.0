'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IPayrollLiquidationRepository
    Inherits IRepository(Of Liquidation)

    ''' <summary>
    ''' Obtiene el valor de las patronales de un empleado por año y mes
    ''' </summary>
    Function GetPatronalesValue(employeeId As Integer, year As Integer, month As Integer) As Decimal

    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="payrollEndDate">Fecha Final Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Function GetEmployesPayroll(groupId As String, ByVal payrollEndDate As Date, ByVal initialPayrollDate As Date, Optional ByVal EmployeeNit As String = "") As List(Of Contract)

    ''' <summary>
    ''' Obtiene los Cargos de los Empleados
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Lista de Cargos</returns>
    ''' <remarks></remarks>
    Function GetEmployesPosition(employeeId As String) As Position

    ''' <summary>
    ''' Obtiene la lista de Contratos de Empleados por Id del Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Lista de Empleados</returns>
    ''' <remarks></remarks>
    Function GetEmployesContract(employeeId As String) As List(Of Contract)

    ''' <summary>
    ''' Obtiene para saber si a un empleado se le va a pagar la nómina
    ''' </summary>
    ''' <param name="employeeId">Id dem Empelado</param>
    ''' <param name="PayrollLiquidation">Tipo de Liquidación de Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function GetPayrollValidation(employeeId As String, PayrollLiquidation As Integer, PayrollEndDate As Date) As Boolean

    ''' <summary>
    ''' Guarda la Liquidación de Nómina
    ''' </summary>
    ''' <param name="liquidation">Objeto Liquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveLiquitadion(ByVal liquidation As Liquidation) As Boolean

    ''' <summary>
    ''' Lista los empleados que se les ha pagado una Nómina
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Function GetEmployeeLiquidated(ByVal PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene una validación para saber si la nómina ya fue Confirmada o no
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de la Nómina</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function PayrollNotCheckLiquidated(ByVal PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Lista de Liquidaciones ya confirmadas de un Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Function EmployeePayrollCheckLiquidation(employeeId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Lista de Liquidaciones Sin Confirmar
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación </param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Function PayrollNotCheckLiquidatedToDelete(ByVal PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene la Fecha Máxima de Liquidación
    ''' </summary>
    ''' <param name="groupId">Group Id</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function PayrollMaxDateLiquidation(groupId As String) As Date

    ''' <summary>
    ''' Obtiene una liquidación por id
    ''' </summary>
    Function GetLiquidationById(id As Integer) As Liquidation
    ''' <summary>
    ''' Liquidacion por periodo y centro de trabajo
    ''' </summary>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="workcenter"></param>
    ''' <returns></returns>
    Function GetLiquidationByPeriodAndWorkCenter(periodLiquidation As String, workCenter As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene una liquidación por id del empleado, año y mes
    ''' </summary>
    ''' <param name="employeeId">The employee identifier.</param>
    ''' <returns></returns>
    Function GetLiquidationByEmployeeIdYearMonth(employeeId As Integer, year As Integer, month As Integer) As Liquidation

    ''' <summary>
    ''' Función que me consulta si un Contrato tiene una Nómina Confirmada
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function LiquidationConfirmatedByContract(ByVal contractId As String) As Boolean

    ''' <summary>
    ''' Obtiene las liquidacion de un contrato de los ultimos meses que le envien como parametro
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="numberLastMonth">numero de meses</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LiquidationLastMonths(contractId As Integer, numberLastMonth As Integer, PayrollDate As Date) As List(Of Liquidation)

    ''' <summary>
    ''' Función Para Obtener las Liquidaciones por Id del Contrato
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Liquidacion</returns>
    ''' <remarks></remarks>
    Function ContractNotCheckLiquidation(contractId As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Funcion para calcular las liquidacion en cierto rango de fecha
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Lista de liquidaciones</returns>
    ''' <remarks></remarks>
    Function LiquidationByDate(contractId As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation)

    ''' <summary>
    ''' Funcion para calcular las liquidacion en cierto rango de fecha para Primas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Lista de liquidaciones</returns>
    ''' <remarks></remarks>
    Function LiquidationByDateIncentivePayment(contractId As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation)

    ''' <summary>
    ''' Funcion para calcular las liquidacion por id de empleado en cierto rango de fecha
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Lista de liquidaciones</returns>
    ''' <remarks></remarks>
    Function LiquidationEmployeeByDate(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation)

    Function GetLiquidationPendingByEmployeeAndMonth(employeeId As Integer, periodDate As Date) As List(Of Liquidation)

    ''' <summary>
    ''' Consulta liquidaciones por estado del contrato
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="initialDate"></param>
    ''' <param name="endDate"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Function LiquidationEmployeeByDateAndContractStatus(employeeId As Integer, initialDate As Date, endDate As Date, status As List(Of Byte)) As List(Of Liquidation)

    ''' <summary>
    ''' Devuelve la fecha menor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationMinDate() As Date

    ''' <summary>
    ''' Devuelve la fecha mayor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationMaxDate() As Date

    ''' <summary>
    ''' Obtiene una lista de Liquidaciones Confirmadas por Grupos, Mes Inicial, Mes Final y Año
    ''' </summary>
    ''' <param name="groupId">Id Grupo</param>
    ''' <param name="InitialMonth">Mes Inicial</param>
    ''' <param name="EndMonth">Mes Final</param>
    ''' <param name="YearActive">Año</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Function LiquidationConfirmatedByGroupAndMonthYear(ByVal groupId As String, ByVal InitialMonth As Integer, ByVal EndMonth As Integer, YearActive As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Funcion que retorna las liquidacion de un periodo especifico
    ''' </summary>
    ''' <param name="periodLiquidation">Periodo de liquidacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationByPeriod(periodLiquidation As String) As List(Of Liquidation)

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Function GetLiquidationByGrupoId(GroupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes segun un grupo
    ''' </summary>
    ''' <param name="groupId">id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationDatesByGroup(groupId As Integer) As List(Of Date)

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago y grupo
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Function ListLiquitadionByGroupAndDateLiquidated(PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene las Liqudiaciones por Groupo ID para el Mostrar cuando se Consulta las Liquidaciaones desde el Frontal de Liquidación Nómina
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>List(Of Liquidation)</returns>
    ''' <remarks></remarks>
    Function GetLiquidationByGrupoIdConsultLiquidation(GroupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha Final Nómina</param>
    ''' <param name="initialDatePayroll">Fecha Inicial Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Function GetEmployesPayrollLiquidation(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date, Optional ByVal EmployeeNit As String = "") As List(Of Employee)

    ''' <summary>
    ''' Obtiene un Diccionario con el Id del Grupo y Verdadero en caso de que exista una liquidación EN BORRADOR para el grupo y la Fecha Seleccionada (Válido únicamente para la carga de la Liquidación de Nómina)
    ''' </summary>
    ''' <param name="IdGroup">Id Group</param>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación de Nómina</param>
    ''' <returns>Diccionario</returns>
    ''' <remarks></remarks>
    Function GetOnlyLiquidationByGrupoIdDateLiquidation(IdGroup As String, PayrollDateLiquidated As Date) As Dictionary(Of String, Date)

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago para los Archivos de Bancos
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Function ListLiquitadionByDateLiquidatedBankFile(PayrollDateLiquidated As Date, CompanyId As Integer, BankId As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes de las Nóminas Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationDatesConfirmPayroll() As List(Of Date)

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago para los Archivos de Bancos
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Function ListLiquitadionByDateLiquidatedNationalSavingsFund(PayrollDateLiquidated As Date, CompanyId As Integer) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene una Lista de Liquidaciones CONFIRMADAS por Fecha Inicio y Fecha Fin
    ''' </summary>
    ''' <param name="InitialDate">Fecha Inicio</param>
    ''' <param name="EndDate">Fecha fin</param>
    ''' <returns>List(Of Liquidation)</returns>
    ''' <remarks></remarks>
    Function GetConfirmLiquidationByStarEndDate(InitialDate As Date, EndDate As Date) As List(Of Liquidation)

    Function DeleteLiquidation(ListLiquidation As List(Of Liquidation)) As Boolean

    Function SaveListLiquidation(ListLiquidation As List(Of Liquidation)) As Boolean

    Function GetEmployesIncentivePayment(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date, Optional ByVal EmployeeNit As String = "") As List(Of Employee)
    ''' <summary>
    ''' Obtiene SOLO el conteo de empleados para liquidar primas (optimizado, sin cargar entidades completas)
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha final de liquidación</param>
    ''' <param name="initialDatePayroll">Fecha inicial de liquidación</param>
    ''' <returns>Cantidad de empleados</returns>
    Function GetEmployesIncentivePaymentCount(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date) As Integer

    ''' <summary>
    ''' Función que obtiene la Lista de Grupos con Fecha para el Frontal de Liquidación, Consulta Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationByGroupIdConsultLiquidation(GroupId As String) As Dynamic.ExpandoObject

    ''' <summary>
    ''' Función para Cargar la Cabecera, para el precargue de las Liquidaciones de Nómina, cuando se abre el frontal
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHeadLiquidation(GroupId As Integer, ByVal PayrollDateLiquidated As Date) As List(Of Liquidation)

    Function GetDetailMessageLiquidation(GroupId As Integer, ByVal PayrollDateLiquidated As Date) As List(Of Liquidation)

    Function GetFundsContractFundByIdFund(IdContract As Integer) As List(Of FundContract)

    Function GetThirdParty(IdThirdParty As Integer) As ThirdParty

    Function GetConfirmLiquidationByStarEndDateRetroactive(InitialDate As Date, EndDate As Date, groupId As Integer, Optional EmployeeId As Integer = 0) As List(Of Liquidation)

    Function GetLastConceptClass(ByVal ContractId As Integer, ConceptClass As String) As LiquidationDetail

    Function GetLastConceptClassListDate(ByVal ContractId As Integer, ConceptClass As String, DateInitial As Date) As List(Of LiquidationDetail)

    Function GetLastConceptClassListDateBetween(ByVal ContractId As Integer, ConceptClass As String, DateInitial As Date, endDate As Date) As List(Of LiquidationDetail)

    Function ListLiquitadionByGroupAndDateLiquidatedCostDistribution(PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation)

    Function GetEmployesIncentivePaymentPrivateCompany(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date, Optional ByVal EmployeeNit As String = "") As List(Of Employee)

    Function GetConfirmLiquidationByStarEndDateRetefuente(InitialDate As Date, EndDate As Date, IdEmployee As Integer) As List(Of Liquidation)

    Function GetLastConceptByCode(ByVal ContractId As Integer, ConceptCode As String) As LiquidationDetail

    Function LiquidationByContract(ByVal contractId As String) As Boolean

    Function GetLastConceptClassBetweenDate(ConceptClass As String, DateInitial As Date, endDate As Date) As List(Of LiquidationDetail)

    Function GetLastListConceptClassListDateBetween(ContractId As Integer, ListConceptClass As List(Of String), DateInitial As Date, endDate As Date) As List(Of LiquidationDetail)

    Function GetEmployeeLiquidation(IdEmployee As String, ByVal groupId As Integer, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date) As Employee

    Function SP_DeleteLiquidation(PayrollEndDate As Date, GroupId As Integer, EmployeeNit As String) As Integer

    Function CountEmployeePayroll(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date) As List(Of Employee)

    Function GetListEmployesPayrollLiquidation(IdListEmployee As List(Of Integer), groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date) As List(Of Employee)

    Function ListLiquitadionByGroupAndDateLiquidatedByPayroll(PayrollDateLiquidated As Date, ByVal groupId As String, ListIdEmployee As List(Of Integer)) As List(Of Liquidation)

    Function SP_DeleteLiquidationNoConfirm(payrollEndDate As Date, groupId As String, employeeNit As String) As Integer

    ''' <summary>
    ''' Funcion que obtiene el reporte de talento humano
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <returns>Lista de información de empleados</returns>
    Function GetReportHumanTalent(initialDate As Date, finalDate As Date, Optional employeeId As Integer? = Nothing) As List(Of SP_ReportHumanTalent_Result)

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="groupInitial">Id del grupo inicial para filtrar (opcional)</param>
    ''' <param name="groupFinal">Id del grupo final para filtrar (opcional)</param>
    ''' <param name="branchOfficeInitial">Id de la sucursal inicial para filtrar (opcional)</param>
    ''' <param name="branchOfficeFinal">Id de la sucursal final para filtrar (opcional)</param>
    ''' <param name="registerStatus">Estado de registro de liquidación (opcional)</param>
    ''' <param name="session">Valores de sesión para obtener la conexión</param>
    ''' <returns>DataTable con el reporte de liquidación detallado</returns>
    Function GetLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional groupInitial As Integer? = Nothing, Optional groupFinal As Integer? = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional registerStatus As Char? = Nothing, Optional session As Infrastructure.CrossCutting.Base.SessionValues = Nothing) As System.Data.DataTable

    ''' <summary>
    ''' Obtiene el reporte consolidado de novedades de Talento Humano con impacto en nómina
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del rango</param>
    ''' <param name="endDate">Fecha final del rango</param>
    ''' <param name="initialCodeGroup">Código del grupo de nómina inicial (opcional)</param>
    ''' <param name="endCodeGroup">Código del grupo de nómina final (opcional)</param>
    ''' <param name="branchOfficeInitial">Id de la sucursal inicial para filtrar (opcional)</param>
    ''' <param name="branchOfficeFinal">Id de la sucursal final para filtrar (opcional)</param>
    ''' <param name="personnelActionTypes">Códigos numéricos de tipo de novedad separados por coma (opcional)</param>
    ''' <param name="session">Valores de sesión para obtener la conexión</param>
    ''' <returns>DataTable con las acciones de personal del rango</returns>
    Function GetReportPersonnelActions(initialDate As Date, endDate As Date, Optional initialCodeGroup As String = Nothing, Optional endCodeGroup As String = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional personnelActionTypes As String = Nothing, Optional session As Infrastructure.CrossCutting.Base.SessionValues = Nothing) As System.Data.DataTable


End Interface
