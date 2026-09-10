'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPayrollLiquidationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Ejecuta y almacena la Liquidación de Nómina
    ''' </summary>
    ''' <param name="PayrollLiquidation">Lista de Liquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveLiquidation(PayrollLiquidation As List(Of Liquidation), ByVal session As SessionValues) As ActionMessageResult(Of List(Of Liquidation))

    ''' <summary>
    ''' Función para Calcular la Liquidación de la Nómina
    ''' </summary>
    ''' <param name="StringGroup">Id del Grupo</param>
    ''' <param name="employeeNit">Cédula del Empleado</param>
    ''' <returns>Action Result de Liquidaciones</returns>
    ''' <remarks></remarks>
    Function CalculatePayrollLiquidation(StringGroup As String, ByVal audit As SessionValues, Optional employeeNit As String = "") As ActionMessageResult(Of List(Of Liquidation))

    ''' <summary>
    ''' Obtiene una Lista de Liquidaciones por Fecha
    ''' </summary>
    ''' <param name="DatePayrollLiquidated">Fecha Liquidación</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Function GetEmployeeLiquidated(DatePayrollLiquidated As Date, groupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Elimina una Liquidación de Nómina
    ''' </summary>
    ''' <param name="liquidation">Objeto Liquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePayrollLiquidated(liquidation As Liquidation) As Boolean

    ''' <summary>
    ''' Función que me consulta si un Contrato tiene una Nómina Confirmada
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function LiquidationConfirmatedByContract(ByVal contractId As String) As Boolean

    ''' <summary>
    ''' Obtiene una liquidación por id
    ''' </summary>
    Function GetLiquidationById(id As Integer) As Liquidation

    ''' <summary>
    ''' Obtiene el valor de las patronales de un empleado por año y mes
    ''' </summary>
    Function GetPatronalesValue(employeeId As Integer, year As Integer, month As Integer) As Decimal

    ''' <summary>
    ''' Obtiene una liquidación por id del empleado, año y mes
    ''' </summary>
    ''' <param name="employeeId">The employee identifier.</param>
    ''' <returns></returns>
    Function GetLiquidationByEmployeeIdYearMonth(employeeId As Integer, year As Integer, month As Integer) As Liquidation

    ''' <summary>
    ''' Función Para Eliminar Liquidaciones por Id del Contrato
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteLiquidationByContractId(ByVal contractId As String) As Boolean

    ''' <summary>
    ''' Devuelve la fecha menor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationMinDate() As Date

    ''' <summary>
    ''' Devuelve la fecha maxima de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationMaxDate() As Date

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
    ''' Lista de Grupos por Id para el forntal de Liquidación de Nómna, opción Consultar
    ''' </summary>
    ''' <param name="StringGroup">Cadena de Id de Grupos</param>
    ''' <returns>List(Of Liquidation)</returns>
    ''' <remarks></remarks>
    Function GetLiquidationByGrupoIdConsultLiquidation(StringGroup As String) As List(Of Liquidation)

    ''' <summary>
    ''' Almacena las Liquidaciones de Saldos Iniciales
    ''' </summary>
    ''' <param name="LiquidationList">Lista de Liquidaciones</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveLiquidationOpenBalances(LiquidationList As List(Of Liquidation)) As Boolean

    ''' <summary>
    ''' Obtiene un Diccionario con el Id del Grupo y Verdadero en caso de que exista una liquidación EN BORRADOR para el grupo y la Fecha Seleccionada (Válido únicamente para la carga de la Liquidación de Nómina)
    ''' </summary>
    ''' <param name="StringIdGroup">StringIdGroup</param>
    ''' <returns>Diccionario</returns>
    ''' <remarks></remarks>
    Function GetOnlyLiquidationByGrupoIdDateLiquidation() As Dictionary(Of String, Date)

    ''' <summary>
    ''' Lista de Liquidaciones Sin Confirmar
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación </param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Function PayrollNotCheckLiquidatedToDelete(ByVal DatePayrollLiquidated As Date, groupId As String) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes para Nóminas Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationDatesConfirmPayroll() As List(Of Date)

    ''' <summary>
    ''' Función que obtiene la Lista de Grupos con Fecha para el Frontal de Liquidación, Consulta Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationByGroupIdConsultLiquidation(GroupId As Integer) As String

    ''' <summary>
    ''' Función para Cargar la Cabecera, para el precargue de las Liquidaciones de Nómina, cuando se abre el frontal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getLiquidationHeader() As List(Of Liquidation)

    Function GetDetailMessageLiquidation(groupId As Integer) As List(Of Liquidation)

    ''''##############################


    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "Payrol.SP_ReportKardex"
    ''' </summary>
    ''' <param name="status">The status.</param>
    ''' <param name="employeeId">The employee identifier.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Function GetListReportPayrollReportKardex(conceptType As Integer, status As Integer, employeeId As Integer, session As SessionValues) As DataSet


    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure [Payroll].[SP_ReportNovelties].
    ''' </summary>
    ''' <param name="InitialDate">The initial date.</param>
    ''' <param name="EndDate">The end date.</param>
    ''' <param name="InitialCodeGroup">The initial code group.</param>
    ''' <param name="EndCodeGroup">The end code group.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Function GetListReportPayrollReportNovelties(InitialDate As Date, EndDate As Date, InitialCodeGroup As String, EndCodeGroup As String, pBranchOfficeIdStart? As Integer, pBranchOfficeIdEnd? As Integer, session As SessionValues) As DataSet

    ''' <summary>
    ''' Función para Calcular Días 360
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    Function Days360(InitialDate As Date, EndDate As Date) As Integer

    Function GetPayrollReport(pStartDate As Date, pEndDate As Date, pRegisterStatus As String, pCédula As String, pStartGroupCode As String, pEndGroupCode As String, pSession As SessionValues) As DataTable

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
    ''' <param name="pSession">Valores de sesión</param>
    ''' <returns>DataTable con el reporte de liquidación detallado</returns>
    Function GetLiquidationDetailReport(InitialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional groupInitial As Integer? = Nothing, Optional groupFinal As Integer? = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional registerStatus As Char? = Nothing, Optional pSession As SessionValues = Nothing) As DataTable

    ''' <summary>
    ''' Función para cargar todos los empleados de un grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    Function CountEmployeePayroll(groupId As String) As List(Of Employee)

    ''' <summary>
    ''' Función para Cargar los empleados que se van a liquidar
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <param name="NitEmployee"></param>
    ''' <returns></returns>
    Function GetEmployesPayrollLiquidation(GroupId As Integer, NitEmployee As String) As List(Of Employee)

    ''' <summary>
    ''' Función para Liquidar la Nómina por Fragmentos
    ''' </summary>
    ''' <param name="ListEmployee"></param>
    ''' <param name="IdGroup"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function CalculatePayrollLiquidationFragment(ListEmployee As List(Of Employee), IdGroup As Integer, ByVal session As SessionValues, Optional CountLiquidation As Integer = 0) As ActionMessageResult(Of List(Of Liquidation))

    ''' <summary>
    ''' Funcion que obtiene el reporte de talento humano
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Lista de información de empleados</returns>
    Function GetReportHumanTalent(initialDate As Date, finalDate As Date, session As SessionValues, Optional employeeId As Integer? = Nothing) As ActionResult(Of List(Of SP_ReportHumanTalent_Result))

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure [Payroll].[SP_ReportPersonnelActions].
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del rango</param>
    ''' <param name="endDate">Fecha final del rango</param>
    ''' <param name="initialCodeGroup">Código del grupo de nómina inicial (opcional)</param>
    ''' <param name="endCodeGroup">Código del grupo de nómina final (opcional)</param>
    ''' <param name="branchOfficeInitial">Id de la sucursal inicial para filtrar (opcional)</param>
    ''' <param name="branchOfficeFinal">Id de la sucursal final para filtrar (opcional)</param>
    ''' <param name="personnelActionTypes">Códigos numéricos de tipo de novedad separados por coma (opcional)</param>
    ''' <param name="pSession">Valores de sesión</param>
    ''' <returns>DataTable con las acciones de personal del rango</returns>
    Function GetReportPersonnelActions(initialDate As Date, endDate As Date, Optional initialCodeGroup As String = Nothing, Optional endCodeGroup As String = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional personnelActionTypes As String = Nothing, Optional pSession As SessionValues = Nothing) As DataTable

End Interface
