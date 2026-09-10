'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 15-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base

Public Class ScheduleDetailAdminService
    Implements IScheduleDetailAdminService


    ' Repositorio detalles de horarios
    Private _ScheduleRepository As IScheduleDetailRepository

    ''' <summary>
    ''' Contructor el cual inicia la instancia del repositorio de Schedule
    ''' </summary>
    ''' <param name="repository">Repositorio de cuadro de turnos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IScheduleDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("ScheduleRepository Vacio")
        End If
        _ScheduleRepository = repository
    End Sub

    ''' <summary>
    ''' Elimina un detalle horario
    ''' </summary>
    ''' <param name="scheduleDetail">Detalle Horario a eliminar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSchedule(scheduleDetail As ScheduleDetail, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IScheduleDetailAdminService.DeleteSchedule
        If scheduleDetail Is Nothing Then
            Throw New ArgumentNullException("El detalle del horario es vacio")
        End If
        Dim unitWork As IUnitWork = _ScheduleRepository.UnitWork
        Try
            _ScheduleRepository.DeleteEntity(scheduleDetail)
            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of ScheduleDetail).Execute(scheduleDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, scheduleDetail)
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' obtiene un detalle del calendario
    ''' </summary>
    ''' <param name="id">id del detalle del calendario</param>
    ''' <returns>ScheduleDetail</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetail(id As String) As ScheduleDetail Implements IScheduleDetailAdminService.GetScheduleDetail
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("Id vacio")
        End If
        Try
            Return _ScheduleRepository.GetScheduleDetail(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un detalle Horario
    ''' </summary>
    ''' <param name="scheduleDetail">Detalle Horario</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleDetail(scheduleDetail As ScheduleDetail, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IScheduleDetailAdminService.SaveScheduleDetail
        If scheduleDetail Is Nothing Then
            Throw New ArgumentNullException("Detalle de horario vacio")
        End If
        Dim unitWork As IUnitWork = _ScheduleRepository.UnitWork
        Try
            If scheduleDetail.ChangeTracker.State = ObjectState.Modified Then
                scheduleDetail.ScheduleDetailAux = _ScheduleRepository.GetScheduleDetail(scheduleDetail.Id, False)
            End If
            _ScheduleRepository.SaveEntity(scheduleDetail)
            unitWork.Commit()

            If scheduleDetail IsNot Nothing AndAlso scheduleDetail.ChangeTracker.State = ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(scheduleDetail.GetType.Name, audit.Functional, scheduleDetail.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)                '/*****Auditoria Avanzada ******/
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(scheduleDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf scheduleDetail IsNot Nothing AndAlso scheduleDetail.ChangeTracker.State = ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(scheduleDetail.GetType.Name, audit.Functional, scheduleDetail.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(scheduleDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Update, scheduleDetail.ScheduleDetailAux)
                auditObject.Execute()
            ElseIf scheduleDetail IsNot Nothing AndAlso scheduleDetail.ChangeTracker.State = ObjectState.Deleted Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(scheduleDetail.GetType.Name, audit.Functional, scheduleDetail.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(scheduleDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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
    ''' Obtiene la lista de detalles que tiene un contrato especifico
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Lista de ScheduleDetail</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByContractId(contractId As Integer) As List(Of ScheduleDetail) Implements IScheduleDetailAdminService.GetScheduleDetailByContractId
        Try
            Return _ScheduleRepository.GetScheduleDetailByContractId(contractId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ScheduleDetail)
        End Try
    End Function

    ''' <summary>
    ''' obtiene el listado de los detalles que estan dentro de un rango de fechas, de un determinado grupo, y que estan marcados como eventos
    ''' </summary>
    ''' <param name="functionalunitId">id de la unidad funcional</param>
    ''' <param name="dateInitial">fecha inicio</param>
    ''' <param name="dateEnd">fecha fin</param>
    ''' <returns>los schedule details</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailWithEvents(functionalunitId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailAdminService.GetScheduleDetailWithEvents
        Try
            Return _ScheduleRepository.GetScheduleDetailWithEvents(functionalunitId, dateInitial, dateEnd)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ScheduleDetail)
        End Try
    End Function

    ''' <summary>
    ''' Guarda un listado de schedule details con los Eventos aprobados
    ''' </summary>
    ''' <param name="list_schedule_details">Listado de detalles con eventos aprobados</param>
    ''' <param name="session">Variables de sesion</param>
    ''' <returns>si realizo o no la accion</returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleDetailWithEventMasive(list_schedule_details As List(Of ScheduleDetail), session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IScheduleDetailAdminService.SaveScheduleDetailWithEventMasive
        If list_schedule_details Is Nothing AndAlso list_schedule_details.Count = 0 Then
            Throw New ArgumentNullException("Detalle de horario vacio")
        End If
        Dim unitWork As IUnitWork = _ScheduleRepository.UnitWork
        Try
            For Each itemS As ScheduleDetail In list_schedule_details
                For Each itemH As ScheduleDetailHour In itemS.ScheduleDetailHour
                    If itemH.ChangeTracker.State = ObjectState.Modified Then
                        itemS.MarkAsModified()
                        Exit For
                    End If
                Next
                _ScheduleRepository.SaveEntity(itemS)
            Next
            unitWork.Commit()

            For Each itemS As ScheduleDetail In list_schedule_details
                For Each itemH As ScheduleDetailHour In itemS.ScheduleDetailHour
                    If itemH.ChangeTracker.State = ObjectState.Modified Then
                        IndigoAuditBasic.Execute(itemH.GetType.Name, session.AuditMessageWcf.Functional, itemH.Id, session.AuditMessageWcf.NameUser, session.AuditMessageWcf.CodeUser, session.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Modificar, session.AuditMessageWcf.Company, session.AuditMessageWcf.ContainerSecurity)
                    End If
                Next
                IndigoAuditBasic.Execute(itemS.GetType.Name, session.AuditMessageWcf.Functional, itemS.Id, session.AuditMessageWcf.NameUser, session.AuditMessageWcf.CodeUser, session.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Modificar, session.AuditMessageWcf.Company, session.AuditMessageWcf.ContainerSecurity)
            Next
            'If scheduleDetail.ChangeTracker.State = ObjectState.Added Then
            '    IndigoAuditSimpleEntity(Of ScheduleDetail).Execute(scheduleDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, scheduleDetail)
            'Else
            '    IndigoAuditSimpleEntity(Of ScheduleDetail).Execute(scheduleDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Update, scheduleDetail)
            'End If
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ScheduleRepository = Nothing
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
