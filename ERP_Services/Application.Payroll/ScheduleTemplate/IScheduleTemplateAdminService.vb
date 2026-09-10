'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IScheduleTemplateAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns>Listado de plantillas</returns>
    ''' <remarks></remarks>
    Function ListAllScheduleTemplate() As List(Of ScheduleTemplate)

    ''' <summary>
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="code">Codigo de la plantilla</param>
    ''' <returns>Plantilla</returns>
    Function GetScheduleTemplate(ByVal code As String) As ScheduleTemplate

    ''' <summary>
    ''' Obtiene una plantilla a través del id
    ''' </summary>
    ''' <param name="id">id de la plantilla</param>
    ''' <returns>Plantilla</returns>
    Function GetScheduleTemplateById(ByVal id As String) As ScheduleTemplate

    ''' <summary>
    ''' Graba o Actualiza una plantilla y sus agregados
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveScheduleTemplate(ByVal schedule As ScheduleTemplate, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina una plantilla
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Function DeleteScheduleTemplate(ByVal schedule As ScheduleTemplate, ByVal audit As AuditMessage) As Boolean

    Function ChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean

    Function ListAllScheduleTemplateByStatus(State As Boolean) As List(Of ScheduleTemplate)

End Interface
