'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService
    Implements IPayrollNovelty

    ''' <summary>
    ''' Elimina una Incapacidad
    ''' </summary>
    ''' <param name="Novelty">Novedad</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteNovelty(novelty As Novelty, session As SessionValues) As Boolean Implements IPayrollNovelty.DeleteNovelty
        Using noveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return noveltyAdminService.DeleteNovelty(novelty, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las Incapacidades por Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Incapacidades por Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeNovelty(employeeId As Integer, session As SessionValues) As List(Of Novelty) Implements IPayrollNovelty.GetEmployeeNovelty
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetEmployeeNovelty(employeeId)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Incapacidad
    ''' </summary>
    ''' <param name="code">Código de la Incapacidad</param>
    ''' <returns>Incapacidad</returns>
    ''' <remarks></remarks>
    Public Function GetNovelty(code As String, session As SessionValues) As Novelty Implements IPayrollNovelty.GetNovelty
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetNovelty(code)
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Incapacidad
    ''' </summary>
    ''' <param name="Novelty">Novedad</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveNovelty(Novelty As Novelty, session As SessionValues) As ActionMessageResult(Of Novelty) Implements IPayrollNovelty.SaveNovelty
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.SaveNovelty(Novelty, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista las Novedades de un empleado y filtra por tipo de novedad (Sancion, Licencia o Incapacidad)
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="typeNovelty">Tipo de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByTypeNovelty(employeeId As Integer, typeNovelty As Byte, session As SessionValues) As List(Of Novelty) Implements IPayrollNovelty.GetNoveltyByTypeNovelty
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetNoveltyByTypeNovelty(employeeId, typeNovelty)
        End Using
    End Function

    ''' <summary>
    ''' Lista las incapacidades de un empleados que esten liquidadas o no 
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="liquidate">Si esta liquidado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyLiquidate(employeeId As Integer, liquidate As Boolean, session As SessionValues) As List(Of Novelty) Implements IPayrollNovelty.GetNoveltyLiquidate
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetNoveltyLiquidate(employeeId, liquidate)
        End Using
    End Function

    ''' <summary>
    ''' Funcion que lista las novedades de un empleado en un rango establecido
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="fechaInicio">Fecha Inicio</param>
    ''' <param name="fechaFin">Fecha Fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByEmployeeDateInitialEnd(employeeId As Integer, fechaInicio As Date, fechaFin As Date, session As SessionValues) As List(Of Novelty) Implements IPayrollNovelty.GetNoveltyByEmployeeDateInitialEnd
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetNoveltyByEmployeeDateInitialEnd(employeeId, fechaInicio, fechaFin)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una novedad por id
    ''' </summary>
    ''' <param name="id">id de la Incapacidad</param>
    ''' <returns>Novedad</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyById(id As Integer, session As SessionValues) As Novelty Implements IPayrollNovelty.GetNoveltyById
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetNoveltyById(id)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeBetweenDateFromNovelty(employeeId As Integer, dateInitial As Date, dateEnd As Date, session As SessionValues) As List(Of ScheduleDetail) Implements IPayrollNovelty.GetScheduleDetailByEmployeeBetweenDateFromNovelty
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetScheduleDetailByEmployeeBetweenDate(employeeId, dateInitial, dateEnd)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="noveltyId">id de la novedad que se va a exonerar</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyDistinctNoveltyBetweenDate(noveltyId As Integer, employeeId As Integer, initialDate As Date, endDate As Date, session As SessionValues) As List(Of Novelty) Implements IPayrollNovelty.GetNoveltyDistinctNoveltyBetweenDate
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetNoveltyDistinctNoveltyBetweenDate(noveltyId, employeeId, initialDate, endDate)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la novedad que tenga la fecha mas alta del mismo codigo consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo a buscar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUltimateNoveltyConsecutive(consecutive As Integer, session As SessionValues) As Novelty Implements IPayrollNovelty.GetUltimateNoveltyConsecutive
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetUltimateNoveltyConsecutive(consecutive)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un promedio del ibc del empleado en los ultimos meses
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="numberLastMonth">Meses que se quiere calcular</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAverageIBCLiquidationLastMonth(contractId As Integer, numberLastMonth As Integer, session As SessionValues) As Decimal Implements IPayrollNovelty.GetAverageIBCLiquidationLastMonth
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetAverageIBCLiquidationLastMonth(contractId, numberLastMonth)
        End Using
    End Function

    ''' <summary>
    ''' Función para Obtener un listado de Novedades por Consecutivo
    ''' </summary>
    ''' <param name="Consecutive">Consecutive</param>
    ''' <param name="session">session</param>
    ''' <returns>List(Of Novelty)</returns>
    ''' <remarks></remarks>
    Public Function GetListNoveltyByConsecutive(Consecutive As Integer, session As SessionValues) As List(Of Novelty) Implements IPayrollNovelty.GetListNoveltyByConsecutive
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.GetListNoveltyByConsecutive(Consecutive)
        End Using
    End Function

    Public Function CalculateTwoFirstDays(employeeId As Integer, session As SessionValues) As ActionResult(Of Decimal) Implements IPayrollNovelty.CalculateTwoFirstDays
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.CalculateTwoFirstDays(employeeId, session)
        End Using
    End Function
    ''' <summary>
    ''' Calcula el valor del concepto de salario integral con la incapacidad 
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="noveltyDays"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function CalculateNoveltyConcept(contract As Contract, noveltyDays As Integer, session As SessionValues) As ActionResult(Of Decimal) Implements IPayrollNovelty.CalculateNoveltyConcept
        Using NoveltyAdminService As INoveltyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of INoveltyAdminService)()
            Return NoveltyAdminService.CalculateNoveltyConcept(contract, noveltyDays, session)
        End Using
    End Function
End Class
