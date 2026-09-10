'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
#End Region

Public Class SettingsTreasuryAdminService
    Implements ISettingsTreasuryAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _settingTreasuryRepository As ISettingsTreasuryRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal settingTreasuryRepository As ISettingsTreasuryRepository)
        If settingTreasuryRepository Is Nothing Then
            Throw New ArgumentNullException("settingTreasuryRepository")
        End If
        _settingTreasuryRepository = settingTreasuryRepository
    End Sub

    ''' <summary>
    ''' Elimina un parámetro de tesoreria
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function DeleteSettingsTreasury(settingsTreasury As SettingsTreasury, audit As AuditMessage) As ActionResult Implements ISettingsTreasuryAdminService.DeleteSettingsTreasury
        If settingsTreasury Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If
        Dim unitOfWork As IUnitWork = Me._settingTreasuryRepository.UnitWork
        Try
            If settingsTreasury.ChangeTracker.State = ObjectState.Deleted Then

                settingsTreasury.ModificationUser = audit.CodeUser
                settingsTreasury.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of SettingsTreasury)(settingsTreasury, audit, status)

                Me._settingTreasuryRepository.DeleteEntity(settingsTreasury)
                unitOfWork.Commit()
                auditProcess.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' id
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetSettingsTreasuryById(id As Integer, audit As AuditMessage) As SettingsTreasury Implements ISettingsTreasuryAdminService.GetSettingsTreasuryById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim settingsTreasury As SettingsTreasury = Me._settingTreasuryRepository.GetSettingsTreasuryById(id)
            Return settingsTreasury
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetSettingsTreasuryByIdUnitOperative(IdUnitOperative As Integer, audit As AuditMessage) As ActionResult(Of SettingsTreasury) Implements ISettingsTreasuryAdminService.GetSettingsTreasuryByIdUnitOperative
        If IdUnitOperative = 0 Then
            Throw New ArgumentNullException("IdUnitOperative")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim settingsTreasury As SettingsTreasury = Me._settingTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(IdUnitOperative)
            Return New ActionResult(Of SettingsTreasury) With {.StateResult = True, .ObjectEmbbeded = settingsTreasury}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsTreasury) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un parámetro de Tesorería
    ''' </summary>
    ''' <param name="settingsTreasury">The settings treasury.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">settingsTreasury</exception>
    Public Function SaveSettingsTreasury(settingsTreasury As SettingsTreasury, audit As AuditMessage) As ActionResult(Of SettingsTreasury) Implements ISettingsTreasuryAdminService.SaveSettingsTreasury
        If settingsTreasury Is Nothing Then
            Throw New ArgumentNullException("settingsTreasury")
        End If
        Dim unitOfWork As IUnitWork = Me._settingTreasuryRepository.UnitWork

        Try

            Dim auxSettingsTreasury As SettingsTreasury = Nothing
            Dim status As Integer
            If settingsTreasury.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                settingsTreasury.CreationDate = Date.Now
                settingsTreasury.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                settingsTreasury.ModificationDate = Date.Now
                settingsTreasury.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxSettingsTreasury = settingsTreasury.OriginalValue
            End If

            Me._settingTreasuryRepository.SaveEntity(settingsTreasury)
            unitOfWork.Commit()
            Dim auditProcess As New IndigoAuditSimpleEntity(Of SettingsTreasury)(settingsTreasury, audit, status, auxSettingsTreasury)
            auditProcess.Execute()
            'Se marca la entidad como sin cambios
            settingsTreasury.MarkAsUnchanged()

            Return New ActionResult(Of SettingsTreasury) With {.StateResult = True, .ObjectEmbbeded = settingsTreasury}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingsTreasury) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsTreasury) With {.StateResult = False}
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
            _settingTreasuryRepository = Nothing
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