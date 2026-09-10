'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 13-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports DistributedServices.Payroll
Imports Domain.Base.Entities

Partial Class PayrollService
    Implements IPayrollLiquidation

    ''' <summary>
    ''' Elimina una Liquidación de Nómina
    ''' </summary>
    ''' <param name="Liquidated">Objeto Liquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePayrollLiquidated(Liquidated As Domain.Payroll.Entities.Liquidation, session As SessionValues) As Boolean Implements IPayrollLiquidation.DeletePayrollLiquidated
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            'Return liquidationPayroll.DeletePayrollLiquidated(Liquidated)
        End Using
    End Function

    ''' <summary>
    ''' Lista Liquidación de Empleados
    ''' </summary>
    ''' <param name="DatePayrollLiquidated">Fecha Liquidación</param>
    ''' <returns>Lista de Empleados liquidados</returns>
    ''' <remarks></remarks>
    Public Function ListEmployeeLiquitaded(DatePayrollLiquidated As Date, groupId As String, session As SessionValues) As List(Of Domain.Payroll.Entities.Liquidation) Implements IPayrollLiquidation.ListEmployeeLiquitaded
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetEmployeeLiquidated(DatePayrollLiquidated, groupId)
        End Using
    End Function

    ''' <summary>
    ''' Almacena la Liquidación de Nómina
    ''' </summary>
    ''' <param name="listPayrollLiquidation"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveLiquidation(listPayrollLiquidation As List(Of Domain.Payroll.Entities.Liquidation), session As SessionValues) As Domain.Base.Entities.ActionMessageResult(Of List(Of Domain.Payroll.Entities.Liquidation)) Implements IPayrollLiquidation.SaveLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.SaveLiquidation(listPayrollLiquidation, session)
        End Using
    End Function

    ''' <summary>
    ''' Función que devuelve si un empleado tiene alguna nómina confirmada
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function LiquidationConfirmatedByContract(contractId As String, session As SessionValues) As Boolean Implements IPayrollLiquidation.LiquidationConfirmatedByContract
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.LiquidationConfirmatedByContract(contractId)
        End Using
    End Function

    ''' <summary>
    ''' Función Para Eliminar Liquidaciones por Id del Contrato
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteLiquidationByContractId(ByVal contractId As String, session As SessionValues) As Boolean Implements IPayrollLiquidation.DeleteLiquidationByContractId
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.DeleteLiquidationByContractId(contractId)
        End Using
    End Function

    ''' <summary>
    ''' Devuelve la fecha menor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationMinDate(session As SessionValues) As Date Implements IPayrollLiquidation.GetLiquidationMinDate
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationMinDate()
        End Using
    End Function

    ''' <summary>
    ''' Devuelve la fecha maxima de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationMaxDate(session As SessionValues) As Date Implements IPayrollLiquidation.GetLiquidationMaxDate
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationMaxDate()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes segun un grupo
    ''' </summary>
    ''' <param name="groupId">id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationDatesByGroup(groupId As Integer, session As SessionValues) As List(Of Date) Implements IPayrollLiquidation.GetLiquidationDatesByGroup
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationDatesByGroup(groupId)
        End Using
    End Function

    ''' <summary>
    ''' Función para Calcular la Liquidación de la Nómina
    ''' </summary>
    ''' <param name="StringGroup">Id del Grupo</param>
    ''' <param name="employeeNit">Cédula del Empleado</param>
    ''' <returns>Action Result de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function CalculatePayrollLiquidation(session As SessionValues, StringGroup As String, Optional employeeNit As String = "") As Domain.Base.Entities.ActionMessageResult(Of List(Of Domain.Payroll.Entities.Liquidation)) Implements IPayrollLiquidation.CalculatePayrollLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.CalculatePayrollLiquidation(StringGroup, session, employeeNit)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGrupoId(GroupId As String, session As SessionValues) As List(Of Liquidation) Implements IPayrollLiquidation.GetLiquidationByGrupoId
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationByGrupoId(GroupId)
        End Using
    End Function

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago y grupo
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Public Function ListLiquitadionByGroupAndDateLiquidated(PayrollDateLiquidated As Date, ByVal groupId As String, session As SessionValues) As List(Of Liquidation) Implements IPayrollLiquidation.ListLiquitadionByGroupAndDateLiquidated
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.ListLiquitadionByGroupAndDateLiquidated(PayrollDateLiquidated, groupId)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGrupoIdConsultLiquidation(GroupId As String, session As SessionValues) As List(Of Liquidation) Implements IPayrollLiquidation.GetLiquidationByGrupoIdConsultLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationByGrupoIdConsultLiquidation(GroupId)
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar Las liquidaciones por Saldo Inicial
    ''' </summary>
    ''' <param name="LiquidationList">Lista de Liquidaciones</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveLiquidationOpenBalances(LiquidationList As List(Of Liquidation), session As SessionValues) As Boolean Implements IPayrollLiquidation.SaveLiquidationOpenBalances
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.SaveLiquidationOpenBalances(LiquidationList)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Diccionario con el Id del Grupo y Verdadero en caso de que exista una liquidación EN BORRADOR para el grupo y la Fecha Seleccionada (Válido únicamente para la carga de la Liquidación de Nómina)
    ''' </summary>
    ''' <param name="StrIdGroup">String Id de los Grupos</param>
    ''' <returns>Diccionario</returns>
    ''' <remarks></remarks>
    Public Function GetOnlyLiquidationByGrupoIdDateLiquidation(session As SessionValues) As Dictionary(Of String, Date) Implements IPayrollLiquidation.GetOnlyLiquidationByGrupoIdDateLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetOnlyLiquidationByGrupoIdDateLiquidation()
        End Using
    End Function

    ''' <summary>
    ''' Lista los Empleados Liquidados
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <param name="groupId">groupId</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function PayrollNotCheckLiquidatedToDelete(PayrollDateLiquidated As Date, groupId As String, session As SessionValues) As List(Of Liquidation) Implements IPayrollLiquidation.PayrollNotCheckLiquidatedToDelete
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.PayrollNotCheckLiquidatedToDelete(PayrollDateLiquidated, groupId)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationDatesConfirmPayroll(session As SessionValues) As List(Of Date) Implements IPayrollLiquidation.GetLiquidationDatesConfirmPayroll
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationDatesConfirmPayroll()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una liquidación por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetLiquidationById(id As Integer, session As SessionValues) As Liquidation Implements IPayrollLiquidation.GetLiquidationById
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una liquidación por id del empleado, año y mes
    ''' </summary>
    Public Function GetLiquidationByEmployeeIdYearMonth(employeeId As Integer, year As Integer, month As Integer, session As SessionValues) As Liquidation Implements IPayrollLiquidation.GetLiquidationByEmployeeIdYearMonth
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationByEmployeeIdYearMonth(employeeId, year, month)
        End Using
    End Function

    Public Function GetPatronalesValue(employeeId As Integer, year As Integer, month As Integer, session As SessionValues) As Decimal Implements IPayrollLiquidation.GetPatronalesValue
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetPatronalesValue(employeeId, year, month)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene la Lista de Grupos con Fecha para el Frontal de Liquidación, Consulta Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGroupIdConsultLiquidation(GroupId As String, session As SessionValues) As String Implements IPayrollLiquidation.GetLiquidationByGroupIdConsultLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationByGroupIdConsultLiquidation(GroupId)
        End Using
    End Function

    Public Function GetHeadLiquidation(session As SessionValues) As List(Of Liquidation) Implements IPayrollLiquidation.GetHeadLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.getLiquidationHeader()
        End Using
    End Function

    Public Function GetDetailMessageLiquidation(groupId As Integer, session As SessionValues) As List(Of Liquidation) Implements IPayrollLiquidation.GetDetailMessageLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetDetailMessageLiquidation(groupId)
        End Using
    End Function

    '##################################
    Function GetListReportPayrollReportKardex(conceptType As Integer, status As Integer, employeeId As Integer, session As SessionValues) As DataSet Implements IPayrollLiquidation.GetListReportPayrollReportKardex
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetListReportPayrollReportKardex(conceptType, status, employeeId, session)
        End Using
    End Function

    Function GetListReportPayrollReportNovelties(InitialDate As Date, EndDate As Date, InitialCodeGroup As String, EndCodeGroup As String, pBranchOfficeIdStart? As Integer, pBranchOfficeIdEnd? As Integer, session As SessionValues) As DataSet Implements IPayrollLiquidation.GetListReportPayrollReportNovelties
        Using liquidationPayrollNovelties As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayrollNovelties.GetListReportPayrollReportNovelties(InitialDate, EndDate, InitialCodeGroup, EndCodeGroup, pBranchOfficeIdStart, pBranchOfficeIdEnd, session)
        End Using
    End Function

    Public Function Days360(InitialDate As Date, EndDate As Date, session As SessionValues) As Integer Implements IPayrollLiquidation.Days360
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.Days360(InitialDate, EndDate)
        End Using
    End Function

    Public Function GetPayrollReport(pStartDate As Date, pEndDate As Date, pRegisterStatus As String, pCédula As String, pStartGroupCode As String, pEndGroupCode As String, pSession As SessionValues) As DataTable Implements IPayrollLiquidation.GetPayrollReport
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetPayrollReport(pStartDate, pEndDate, pRegisterStatus, pCédula, pStartGroupCode, pEndGroupCode, pSession)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="pSession">Valores de sesión</param>
    ''' <returns>DataTable con el reporte de liquidación detallado</returns>
    Public Function GetLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional groupInitial As Integer? = Nothing, Optional groupFinal As Integer? = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional registerStatus As Char? = Nothing, Optional pSession As SessionValues = Nothing) As DataTable Implements IPayrollLiquidation.GetLiquidationDetailReport
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetLiquidationDetailReport(initialDate, endDate, employeeId, groupInitial, groupFinal, branchOfficeInitial, branchOfficeFinal, registerStatus, pSession)
        End Using
    End Function

    Public Function CountEmployeePayroll(groupId As String, ByVal session As SessionValues) As List(Of Employee) Implements IPayrollLiquidation.CountEmployeePayroll
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.CountEmployeePayroll(groupId)
        End Using
    End Function

    Public Function GetEmployesPayrollLiquidation(GroupId As Integer, NitEmployee As String, ByVal session As SessionValues) As List(Of Employee) Implements IPayrollLiquidation.GetEmployesPayrollLiquidation
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetEmployesPayrollLiquidation(GroupId, NitEmployee)
        End Using
    End Function


    Public Function CalculatePayrollLiquidationFragment(ListEmployee As List(Of Employee), IdGroup As Integer, ByVal session As SessionValues, Optional CountLiquidation As Integer = 0) As ActionMessageResult(Of List(Of Liquidation)) Implements IPayrollLiquidation.CalculatePayrollLiquidationFragment
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.CalculatePayrollLiquidationFragment(ListEmployee, IdGroup, session, CountLiquidation)
        End Using
    End Function

    Public Function GetReportHumanTalent(initialDate As Date, finalDate As Date, session As SessionValues, Optional employeeId As Integer? = Nothing) As ActionResult(Of List(Of SP_ReportHumanTalent_Result)) Implements IPayrollLiquidation.GetReportHumanTalent
        Using liquidationPayroll As IPayrollLiquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationAdminService)()
            Return liquidationPayroll.GetReportHumanTalent(initialDate, finalDate, session, employeeId)
        End Using
    End Function


End Class
