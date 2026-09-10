'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class RetentionAdminService
    Implements IRetentionAdminService

    'Repositorio de Tipos de contratos
    Private _RetentionRepository As IRetentionRepository
    Private _ConsecutiveRepository As IConsecutiveRepository

    ''' <summary>
    ''' inicia el repositorio de Retenciones
    ''' </summary>
    ''' <param name="retentionRepository">Repositorio de Retenciones</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal retentionRepository As IRetentionRepository, ByVal consecutiveRepository As IConsecutiveRepository)
        If (retentionRepository Is Nothing Or consecutiveRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Retenciones")
        End If
        _RetentionRepository = retentionRepository
        _ConsecutiveRepository = consecutiveRepository
    End Sub

    ''' <summary>
    ''' Elimina Retenciones
    ''' </summary>
    ''' <param name="retention">Retenciones</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteRetention(retention As Retention, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Retention) Implements IRetentionAdminService.DeleteRetention
        Dim result As New ActionMessageResult(Of Retention)
        result.StateResult = True
        If retention Is Nothing Then
            Throw New ArgumentNullException("Retenciones vacio")
        End If
        Dim unitWork As IUnitWork = _RetentionRepository.UnitWork
        Try
            _RetentionRepository.DeleteEntity(retention)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(retention.GetType.Name, audit.Functional, retention.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Retention)(retention, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", retention.Code))
            Return result
        Catch ex As Exception
            result.StateResult = False
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Retención
    ''' </summary>
    ''' <param name="code">Código de la Retención</param>
    ''' <returns>Retención</returns>
    ''' <remarks></remarks>
    Public Function GetRetention(code As String) As Retention Implements IRetentionAdminService.GetRetention
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _RetentionRepository.GetRetention(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Retention()
        End Try
    End Function

    ''' <summary>
    ''' Lista Todas las Retenciones
    ''' </summary>
    ''' <returns>Retenciones</returns>
    ''' <remarks></remarks>
    Public Function ListAllRetention() As List(Of Retention) Implements IRetentionAdminService.ListAllRetention
        Try
            Return _RetentionRepository.ListAllRetention()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Retención
    ''' </summary>
    ''' <param name="retention">Retenciones</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveRetention(retention As Retention, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IRetentionAdminService.SaveRetention
        If retention Is Nothing Then
            Throw New ArgumentNullException("Retenciones vacio")
        End If
        Dim unitWork As IUnitWork = _RetentionRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of Retention)
            Dim auxRetention As Retention = Nothing
            Dim status As Integer

            If retention.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                retention.ModificationUser = audit.CodeUser
                retention.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxRetention = _RetentionRepository.GetRetention(retention.Code, False)
            Else
                Dim consecutive As Domain.Entities.Consecutive = _ConsecutiveRepository.GetConsecutiveByCode("4")
                Dim consecutiveContext As New Domain.Payroll.Entities.Consecutive
                With consecutiveContext
                    .Id = consecutive.Id
                    .Code = consecutive.Code
                    .Description = consecutive.Description
                    .NumberConsecutive = consecutive.NumberConsecutive
                    .ChangeTracker = consecutive.ChangeTracker
                End With
                consecutiveContext.NumberConsecutive += 1
                retention.Consecutive1 = consecutiveContext.MarkAsModified()
                retention.Consecutive = consecutiveContext.NumberConsecutive
                retention.CreationUser = audit.CodeUser
                retention.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _RetentionRepository.SaveEntity(retention)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Retention)(retention, audit, status, auxRetention)
            auditProcess.Execute()
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
            _RetentionRepository = Nothing
            _ConsecutiveRepository = Nothing
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
