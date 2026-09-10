'***********************************************************************
' Assembly         : Application.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core

Public Class EconomicActivityAdminService
    Implements IEconomicActivityAdminService

#Region "Builder"

    ''' <summary>
    ''' Variable tipo repositorio para actividad economica
    ''' </summary>
    ''' <remarks></remarks>
    Private _economicActivityRepository As IEconomicActivityRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal economicActivityRepository As IEconomicActivityRepository)
        If economicActivityRepository Is Nothing Then
            Throw New ArgumentNullException("economicActivityRepository Vacio")
        End If
        _economicActivityRepository = economicActivityRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateEconomicActivity(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of EconomicActivity) Implements IEconomicActivityAdminService.ChangeStateEconomicActivity
        Dim economicActivity As EconomicActivity = _economicActivityRepository.GetEconomicActivity(code)
        economicActivity.Status = state
        Return SaveEconomicActivity(economicActivity, audit)
    End Function

    ''' <summary>
    ''' Elimina l actividad economica
    ''' </summary>
    ''' <param name="EconomicActivity"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEconomicActivity(EconomicActivity As EconomicActivity, audit As AuditMessage) As ActionResult Implements IEconomicActivityAdminService.DeleteEconomicActivity
        If EconomicActivity Is Nothing Then
            Throw New ArgumentNullException("EconomicActivity")
        End If
        Dim unitOfWork As IUnitWork = Me._economicActivityRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of EconomicActivity)
            auditProcess = New IndigoAuditSimpleEntity(Of EconomicActivity)(EconomicActivity, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._economicActivityRepository.DeleteEntity(EconomicActivity)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una actividad economica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEconomicActivity(code As String, audit As AuditMessage) As ActionResult(Of EconomicActivity) Implements IEconomicActivityAdminService.GetEconomicActivity
        If code = String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim EconomicActivity As EconomicActivity = Me._economicActivityRepository.GetEconomicActivity(code)
            If EconomicActivity IsNot Nothing AndAlso EconomicActivity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of EconomicActivity)(EconomicActivity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of EconomicActivity) With {.StateResult = True, .ObjectEmbbeded = EconomicActivity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EconomicActivity) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la actividad economica por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEconomicActivityById(id As Integer, audit As AuditMessage) As ActionResult(Of EconomicActivity) Implements IEconomicActivityAdminService.GetEconomicActivityById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim EconomicActivity As EconomicActivity = Me._economicActivityRepository.GetEconomicActivityById(id)
            If EconomicActivity IsNot Nothing AndAlso EconomicActivity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of EconomicActivity)(EconomicActivity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of EconomicActivity) With {.StateResult = True, .ObjectEmbbeded = EconomicActivity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EconomicActivity) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una actividad economica
    ''' </summary>
    ''' <param name="EconomicActivity"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEconomicActivity(EconomicActivity As EconomicActivity, audit As AuditMessage) As ActionResult(Of EconomicActivity) Implements IEconomicActivityAdminService.SaveEconomicActivity
        If EconomicActivity Is Nothing Then
            Throw New ArgumentNullException("EconomicActivity")
        End If
        Dim unitOfWork As IUnitWork = Me._economicActivityRepository.UnitWork
        Try

            Dim auxEconomicActivity As EconomicActivity = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of EconomicActivity)
            Dim status As Integer

            If EconomicActivity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                EconomicActivity.CreationUser = audit.CodeUser
                EconomicActivity.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxEconomicActivity = EconomicActivity.OriginalValue
                EconomicActivity.ModificationUser = audit.CodeUser
                EconomicActivity.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._economicActivityRepository.SaveEntity(EconomicActivity)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of EconomicActivity)(EconomicActivity, audit, status, auxEconomicActivity)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            EconomicActivity.MarkAsUnchanged()

            Return New ActionResult(Of EconomicActivity) With {.StateResult = True, .ObjectEmbbeded = EconomicActivity}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of EconomicActivity) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EconomicActivity) With {.StateResult = False}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _economicActivityRepository = Nothing
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
