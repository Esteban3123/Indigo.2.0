'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base

Public Class ScheduleTemplateAdminService
    Implements IScheduleTemplateAdminService


    ''' <summary>
    ''' Repositorio de las plantillas
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleRepository As IScheduleTemplateRepository

    ''' <summary>
    ''' Contructor el cual obtiene el repositorio de plantillas
    ''' </summary>
    ''' <param name="scheduleRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal scheduleRepository As IScheduleTemplateRepository)
        If scheduleRepository Is Nothing Then
            Throw New ArgumentNullException("scheduleRepository Vacio")
        End If
        _scheduleRepository = scheduleRepository
    End Sub

    ''' <summary>
    ''' Elimina una plantilla
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteScheduleTemplate(schedule As ScheduleTemplate, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IScheduleTemplateAdminService.DeleteScheduleTemplate
        If schedule Is Nothing Then
            Throw New ArgumentNullException("Plantilla vacia")
        End If
        Dim unitWork As IUnitWork = _scheduleRepository.UnitWork
        Try
            _scheduleRepository.DeleteEntity(schedule)
            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of ScheduleTemplate).Execute(schedule, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, schedule)
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="code">Codigo de la plantilla</param>
    ''' <returns>Plantilla</returns>
    Public Function GetScheduleTemplate(code As String) As ScheduleTemplate Implements IScheduleTemplateAdminService.GetScheduleTemplate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Vacio")
        End If
        Try
            Return _scheduleRepository.GetScheduleTemplate(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una plantilla a través del id
    ''' </summary>
    ''' <param name="id">Codigo de la plantilla</param>
    ''' <returns>Plantilla</returns>
    Public Function GetScheduleTemplateById(id As String) As ScheduleTemplate Implements IScheduleTemplateAdminService.GetScheduleTemplateById
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("id Vacio")
        End If
        Try
            Return _scheduleRepository.GetScheduleTemplateById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns>Listado de plantillas</returns>
    ''' <remarks></remarks>
    Public Function ListAllScheduleTemplate() As List(Of ScheduleTemplate) Implements IScheduleTemplateAdminService.ListAllScheduleTemplate
        Try
            Return _scheduleRepository.ListAllScheduleTemplate()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza una plantilla y sus agregados
    ''' </summary>
    ''' <param name="schedule">Plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleTemplate(schedule As ScheduleTemplate, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IScheduleTemplateAdminService.SaveScheduleTemplate
        If schedule Is Nothing Then
            Throw New ArgumentNullException("Plantilla vacia")
        End If
        Dim unitWork As IUnitWork = _scheduleRepository.UnitWork
        Dim scheduleTmp As ScheduleTemplate = Nothing
        Try
            If schedule.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                scheduleTmp = _scheduleRepository.GetScheduleTemplateById(schedule.Id, False)
            End If
            _scheduleRepository.SaveEntity(schedule)
            unitWork.Commit()
            If schedule.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                IndigoAuditBasic.Execute(schedule.GetType.Name, audit.Functional, schedule.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, Date.Now(), Infrastructure.CrossCutting.Base.ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleTemplate)(schedule, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf schedule.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                IndigoAuditBasic.Execute(schedule.GetType.Name, audit.Functional, schedule.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, Date.Now(), Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleTemplate)(schedule, audit, Infrastructure.CrossCutting.Audit.Actions.Update, scheduleTmp)
                auditObject.Execute()
            End If
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IScheduleTemplateAdminService.ChangeState
        Dim ScheduleTemplate As ScheduleTemplate = _scheduleRepository.GetScheduleTemplate(code)
        ScheduleTemplate.State = state
        'ScheduleTemplate.MarkAsModified()
        Return SaveScheduleTemplate(ScheduleTemplate, audit)
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns>Listado de plantillas</returns>
    ''' <remarks></remarks>
    Public Function ListAllScheduleTemplateByStatus(State As Boolean) As List(Of ScheduleTemplate) Implements IScheduleTemplateAdminService.ListAllScheduleTemplateByStatus
        Try
            Return _scheduleRepository.ListAllScheduleTemplateByStatus(State)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _scheduleRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
