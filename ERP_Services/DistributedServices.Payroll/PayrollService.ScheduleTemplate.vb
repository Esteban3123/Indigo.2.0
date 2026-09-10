'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Partial Class PayrollService

    ''' <summary>
    ''' Elimina una plantilla
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteScheduleTemplate(schedule As Domain.Payroll.Entities.ScheduleTemplate, session As SessionValues) As Boolean Implements IPayrollScheduleTemplate.DeleteScheduleTemplate
        Using scheduleAdminService As IScheduleTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleTemplateAdminService)()
            Return scheduleAdminService.DeleteScheduleTemplate(schedule, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="code">Codigo de la plantilla</param>
    ''' <returns>Plantilla</returns>
    Public Function GetScheduleTemplate(code As String, session As SessionValues) As Domain.Payroll.Entities.ScheduleTemplate Implements IPayrollScheduleTemplate.GetScheduleTemplate
        Using scheduleAdminService As IScheduleTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleTemplateAdminService)()
            Return scheduleAdminService.GetScheduleTemplate(code)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una plantilla a través del id
    ''' </summary>
    ''' <param name="id">id de la plantilla</param>
    ''' <returns>Plantilla</returns>
    Public Function GetScheduleTemplateById(id As String, session As SessionValues) As Domain.Payroll.Entities.ScheduleTemplate Implements IPayrollScheduleTemplate.GetScheduleTemplateById
        Using scheduleAdminService As IScheduleTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleTemplateAdminService)()
            Return scheduleAdminService.GetScheduleTemplateById(id)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns>Listado de plantillas</returns>
    ''' <remarks></remarks>
    Public Function ListAllScheduleTemplate(session As SessionValues) As List(Of Domain.Payroll.Entities.ScheduleTemplate) Implements IPayrollScheduleTemplate.ListAllScheduleTemplate
        Using scheduleAdminService As IScheduleTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleTemplateAdminService)()
            Return scheduleAdminService.ListAllScheduleTemplate()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza una plantilla y sus agregados
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleTemplate(schedule As Domain.Payroll.Entities.ScheduleTemplate, session As SessionValues) As Boolean Implements IPayrollScheduleTemplate.SaveScheduleTemplate
        Using scheduleAdminService As IScheduleTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleTemplateAdminService)()
            Return scheduleAdminService.SaveScheduleTemplate(schedule, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o edita un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Public Function ChangeStateScheduleTemplate(code As String, state As Boolean, session As SessionValues) As Boolean Implements IPayrollScheduleTemplate.ChangeStateScheduleTemplate
        Using scheduleAdminService As IScheduleTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleTemplateAdminService)()
            Return scheduleAdminService.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns>Listado de plantillas</returns>
    ''' <remarks></remarks>
    Public Function ListAllScheduleTemplateByStatus(State As Boolean, session As SessionValues) As List(Of Domain.Payroll.Entities.ScheduleTemplate) Implements IPayrollScheduleTemplate.ListAllScheduleTemplateByStatus
        Using scheduleAdminService As IScheduleTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IScheduleTemplateAdminService)()
            Return scheduleAdminService.ListAllScheduleTemplateByStatus(State)
        End Using
    End Function

End Class
