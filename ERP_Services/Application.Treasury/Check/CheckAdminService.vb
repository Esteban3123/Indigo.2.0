'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-06-2014
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

Public Class CheckAdminService
    Implements ICheckAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de chequeras
    ''' </summary>
    Private _checkRepository As ICheckRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal checkRepository As ICheckRepository)
        If checkRepository Is Nothing Then
            Throw New ArgumentNullException("checkRepository")
        End If
        _checkRepository = checkRepository
    End Sub

    ''' <summary>
    ''' Elimina una chequera
    ''' </summary>
    ''' <param name="checks">The checks.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">checks</exception>
    Public Function DeleteCheck(checks As Checkbooks, audit As AuditMessage) As ActionResult Implements ICheckAdminService.DeleteCheck
        If checks Is Nothing Then
            Throw New ArgumentNullException("checks")
        End If
        Dim unitOfWork As IUnitWork = Me._checkRepository.UnitWork
        Try
            If checks.ChangeTracker.State = ObjectState.Deleted Then

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Checkbooks)(checks, audit, status)

                Me._checkRepository.DeleteEntity(checks)
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
    ''' Obtiene una chequera por el id de la cuenta bancaria
    ''' </summary>
    ''' <param name="IdEntity">The identifier entity.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' IdEntity
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetCheckByIdEntityBankAccountAndStatus(IdEntity As Integer, status As Short, audit As AuditMessage) As ActionResult(Of Checkbooks) Implements ICheckAdminService.GetCheckByIdEntityBankAccountAndStatus
        If IdEntity = 0 Then
            Throw New ArgumentNullException("IdEntity")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim check As Checkbooks = Me._checkRepository.GetCheckByIdEntityBankAccountAndStatus(IdEntity, status)
            Return New ActionResult(Of Checkbooks) With {.StateResult = True, .ObjectEmbbeded = check}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Checkbooks) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' guarda una chequera
    ''' </summary>
    ''' <param name="check">The check.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">check</exception>
    Public Function SaveCheck(check As Checkbooks, audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionResult(Of Checkbooks) Implements ICheckAdminService.SaveCheck
        If check Is Nothing Then
            Throw New ArgumentNullException("check")
        End If
        Dim unitOfWork As IUnitWork = Me._checkRepository.UnitWork

        Try
            Dim auxCheck As Checkbooks = Nothing
            Dim status As Integer
            If check.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxCheck = check.OriginalValue
            End If

            Me._checkRepository.SaveEntity(check)
            If withCommit Then
                unitOfWork.Commit()
            End If
            Dim auditProcess As New IndigoAuditSimpleEntity(Of Checkbooks)(check, audit, status, auxCheck)
            auditProcess.Execute()

            Return New ActionResult(Of Checkbooks) With {.StateResult = True, .ObjectEmbbeded = check}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Checkbooks) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Checkbooks) With {.StateResult = False}
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
            _checkRepository = Nothing
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
