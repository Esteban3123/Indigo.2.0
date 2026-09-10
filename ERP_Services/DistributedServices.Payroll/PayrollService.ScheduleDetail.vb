'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 15-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base

Partial Class PayrollService


    ''' <summary>
    ''' Elimina un detalle horario
    ''' </summary>
    ''' <param name="scheduleDetail">Detalle Horario a eliminar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteScheduleDetail(scheduleDetail As Domain.Payroll.Entities.ScheduleDetail, session As SessionValues) As Boolean Implements IPayrollScheduleDetail.DeleteScheduleDetail
        Using detailAdmin As IScheduleDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleDetailAdminService)()
            Return detailAdmin.DeleteSchedule(scheduleDetail, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un detalle del calendario
    ''' </summary>
    ''' <param name="id">id del detalle del calendario</param>
    ''' <returns>ScheduleDetail</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetail(id As String, session As SessionValues) As Domain.Payroll.Entities.ScheduleDetail Implements IPayrollScheduleDetail.GetScheduleDetail
        Using detailAdmin As IScheduleDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleDetailAdminService)()
            Return detailAdmin.GetScheduleDetail(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un detalle Horario
    ''' </summary>
    ''' <param name="scheduleDetail">Detalle Horario</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleDetail(scheduleDetail As Domain.Payroll.Entities.ScheduleDetail, session As SessionValues) As Boolean Implements IPayrollScheduleDetail.SaveScheduleDetail
        Using detailAdmin As IScheduleDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleDetailAdminService)()
            Return detailAdmin.SaveScheduleDetail(scheduleDetail, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la lista de detalles que tiene un contrato especifico
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Lista de ScheduleDetail</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByContractId(contractId As Integer, session As SessionValues) As List(Of Domain.Payroll.Entities.ScheduleDetail) Implements IPayrollScheduleDetail.GetScheduleDetailByContractId
        Using detailAdmin As IScheduleDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleDetailAdminService)()
            Return detailAdmin.GetScheduleDetailByContractId(contractId)
        End Using
    End Function

    ''' <summary>
    ''' obtiene el listado de los detalles que estan dentro de un rango de fechas, de un determinado grupo, y que estan marcados como eventos
    ''' </summary>
    ''' <param name="functionalunitId">id de la unidad funcional</param>
    ''' <param name="dateInitial">fecha inicio</param>
    ''' <param name="dateEnd">fecha fin</param>
    ''' <returns>los schedule details</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailWithEvents(functionalunitId As Integer, dateInitial As Date, dateEnd As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.ScheduleDetail) Implements IPayrollScheduleDetail.GetScheduleDetailWithEvents
        Using detailAdmin As IScheduleDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleDetailAdminService)()
            Return detailAdmin.GetScheduleDetailWithEvents(functionalunitId, dateInitial, dateEnd)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un listado de schedule details con los Eventos aprobados
    ''' </summary>
    ''' <param name="list_schedule_details">Listado de detalles con eventos aprobados</param>
    ''' <param name="session">Variables de sesion</param>
    ''' <returns>si realizo o no la accion</returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleDetailWithEventMasive(list_schedule_details As List(Of Domain.Payroll.Entities.ScheduleDetail), session As SessionValues) As Boolean Implements IPayrollScheduleDetail.SaveScheduleDetailWithEventMasive
        Using detailAdmin As IScheduleDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleDetailAdminService)()
            Return detailAdmin.SaveScheduleDetailWithEventMasive(list_schedule_details, session)
        End Using
    End Function
End Class
