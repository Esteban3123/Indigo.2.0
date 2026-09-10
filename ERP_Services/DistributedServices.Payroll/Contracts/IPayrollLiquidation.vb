'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 13-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollLiquidation


    <OperationContract()>
    Function SaveLiquidation(listLiquidationPayroll As List(Of Domain.Payroll.Entities.Liquidation), session As SessionValues) As Domain.Base.Entities.ActionMessageResult(Of List(Of Domain.Payroll.Entities.Liquidation))

    ''' <summary>
    ''' Función para Calcular la Liquidación de la Nómina
    ''' </summary>
    ''' <param name="StringGroup">Id del Grupo</param>
    ''' <param name="employeeNit">Cédula del Empleado</param>
    ''' <returns>Action Result de Liquidaciones</returns>
    ''' <remarks></remarks>>
    <OperationContract()>
    Function CalculatePayrollLiquidation(session As SessionValues, StringGroup As String, Optional employeeNit As String = "") As ActionMessageResult(Of List(Of Liquidation))

    ''' <summary>
    ''' Lista Empleados Liquidados
    ''' </summary>
    ''' <param name="DatePayrollLiquidated">Fecha Liquidación</param>
    ''' <returns>Lista de Empleados Liquidados</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListEmployeeLiquitaded(DatePayrollLiquidated As Date, ByVal groupId As String, session As SessionValues) As List(Of Liquidation)

    ''' <summary>
    ''' Elimina una liquidación
    ''' </summary>
    ''' <param name="Liquidated">Objeto Liquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeletePayrollLiquidated(ByVal Liquidated As Liquidation, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una liquidación por id
    ''' </summary>
    <OperationContract()>
    Function GetLiquidationById(id As Integer, session As SessionValues) As Liquidation

    ''' <summary>
    ''' Obtiene una liquidación por id del empleado, año y mes
    ''' </summary>
    ''' <param name="employeeId">The employee identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLiquidationByEmployeeIdYearMonth(employeeId As Integer, year As Integer, month As Integer, session As SessionValues) As Liquidation

    ''' <summary>
    ''' Función que me consulta si un Contrato tiene una Nómina Confirmada
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function LiquidationConfirmatedByContract(ByVal contractId As String, session As SessionValues) As Boolean

    ''' <summary>
    ''' Función Para Eliminar Liquidaciones por Id del Contrato
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteLiquidationByContractId(ByVal contractId As String, session As SessionValues) As Boolean

    ''' <summary>
    ''' Devuelve la fecha menor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLiquidationMinDate(session As SessionValues) As Date

    ''' <summary>
    ''' Devuelve la fecha maxima de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLiquidationMaxDate(session As SessionValues) As Date

    ''' <summary>
    ''' Obtiene el valor de las patronales de un empleado por año y mes
    ''' </summary>
    <OperationContract()>
    Function GetPatronalesValue(employeeId As Integer, year As Integer, month As Integer, session As SessionValues) As Decimal

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes segun un grupo
    ''' </summary>
    ''' <param name="groupId">id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLiquidationDatesByGroup(groupId As Integer, session As SessionValues) As List(Of Date)

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLiquidationByGrupoId(GroupId As String, session As SessionValues) As List(Of Liquidation)

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago y grupo
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListLiquitadionByGroupAndDateLiquidated(PayrollDateLiquidated As Date, ByVal groupId As String, session As SessionValues) As List(Of Liquidation)

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo para el formulario de Liquidación de Nómina, Opción Consultar Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLiquidationByGrupoIdConsultLiquidation(GroupId As String, session As SessionValues) As List(Of Liquidation)

    ''' <summary>
    ''' Función para Almacenar Las liquidaciones por Saldo Inicial
    ''' </summary>
    ''' <param name="LiquidationList">Lista de Liquidaciones</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveLiquidationOpenBalances(LiquidationList As List(Of Liquidation), session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un Diccionario con el Id del Grupo y Verdadero en caso de que exista una liquidación EN BORRADOR para el grupo y la Fecha Seleccionada (Válido únicamente para la carga de la Liquidación de Nómina)
    ''' </summary>
    ''' <returns>Diccionario</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetOnlyLiquidationByGrupoIdDateLiquidation(session As SessionValues) As Dictionary(Of String, Date)

    ''' <summary>
    ''' Función que trae las liquidaciones NO confirmadas para la Carga Inicial del Formulario
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha Liquidacion</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function PayrollNotCheckLiquidatedToDelete(PayrollDateLiquidated As Date, groupId As String, session As SessionValues) As List(Of Liquidation)

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes segun un grupo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLiquidationDatesConfirmPayroll(session As SessionValues) As List(Of Date)

    ''' <summary>
    ''' Función que obtiene la Lista de Grupos con Fecha para el Frontal de Liquidación, Consulta Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLiquidationByGroupIdConsultLiquidation(GroupId As String, session As SessionValues) As String

    <OperationContract()>
    Function GetHeadLiquidation(session As SessionValues) As List(Of Liquidation)

    <OperationContract()>
    Function GetDetailMessageLiquidation(IdGroup As Integer, session As SessionValues) As List(Of Liquidation)


    '################################    
    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "Payrol.SP_ReportKardex"
    ''' </summary>
    ''' <param name="status">The status.</param>
    ''' <param name="employeeId">The employee identifier.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportPayrollReportKardex(conceptType As Integer, status As Integer, employeeId As Integer, session As SessionValues) As DataSet


    '################################    
    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure [Payroll].[SP_ReportNovelties].
    ''' </summary>
    ''' <param name="InitialDate">The initial date.</param>
    ''' <param name="EndDate">The end date.</param>
    ''' <param name="InitialCodeGroup">The initial code group.</param>
    ''' <param name="EndCodeGroup">The end code group.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportPayrollReportNovelties(InitialDate As Date, EndDate As Date, InitialCodeGroup As String, EndCodeGroup As String, pBranchOfficeIdStart? As Integer, pBranchOfficeIdEnd? As Integer, session As SessionValues) As DataSet

    <OperationContract()>
    Function Days360(InitialDate As Date, EndDate As Date, session As SessionValues) As Integer

    <OperationContract()>
    Function GetPayrollReport(pStartDate As Date, pEndDate As Date, pRegisterStatus As String, pCédula As String, pStartGroupCode As String, pEndGroupCode As String, pSession As SessionValues) As DataTable

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="pSession">Valores de sesión</param>
    ''' <returns>DataTable con el reporte de liquidación detallado</returns>
    <OperationContract()>
    Function GetLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional groupInitial As Integer? = Nothing, Optional groupFinal As Integer? = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional registerStatus As Char? = Nothing, Optional pSession As SessionValues = Nothing) As DataTable

    <OperationContract()>
    Function CountEmployeePayroll(groupId As String, ByVal session As SessionValues) As List(Of Employee)

    <OperationContract()>
    Function GetEmployesPayrollLiquidation(GroupId As Integer, NitEmployee As String, ByVal session As SessionValues) As List(Of Employee)

    <OperationContract()>
    Function CalculatePayrollLiquidationFragment(ListEmployee As List(Of Employee), IdGroup As Integer, ByVal session As SessionValues, Optional CountLiquidation As Integer = 0) As ActionMessageResult(Of List(Of Liquidation))

    <OperationContract()>
    Function GetReportHumanTalent(initialDate As Date, finalDate As Date, session As SessionValues, Optional employeeId As Integer? = Nothing) As ActionResult(Of List(Of SP_ReportHumanTalent_Result))
End Interface
