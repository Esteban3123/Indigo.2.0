'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
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

Public Class OutstandingChecksAdminService
    Implements IOutstandingChecksAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de cheques pendientes
    ''' </summary>
    Private _outstandingChecksRepository As IOutstandingChecksRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal outstandingChecksRepository As IOutstandingChecksRepository)
        If outstandingChecksRepository Is Nothing Then
            Throw New ArgumentNullException("outstandingChecksRepository")
        End If
        _outstandingChecksRepository = outstandingChecksRepository
    End Sub

#End Region

    ''' <summary>
    ''' Deletes the outstanding checks.
    ''' </summary>
    ''' <param name="outstandingChecks"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">outstandingChecks</exception>
    Public Function DeleteOutstandingChecks(outstandingChecks As OutstandingChecks, audit As AuditMessage) As ActionResult Implements IOutstandingChecksAdminService.DeleteOutstandingChecks
        If outstandingChecks Is Nothing Then
            Throw New ArgumentNullException("outstandingChecks")
        End If
        Dim unitOfWork As IUnitWork = Me._outstandingChecksRepository.UnitWork
        Try
            outstandingChecks.MarkAsDeleted()
            If outstandingChecks.ChangeTracker.State = ObjectState.Deleted Then

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of OutstandingChecks)(outstandingChecks, audit, status)

                Me._outstandingChecksRepository.DeleteEntity(outstandingChecks)
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
    ''' Obtiene el primer cheque que esta en espera
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFirstOutstandingChecks(IdCheckBook As Integer) As OutstandingChecks Implements IOutstandingChecksAdminService.GetFirstOutstandingChecks
        Try
            Dim outstandingChecks As OutstandingChecks = Me._outstandingChecksRepository.GetFirstOutstandingChecks(IdCheckBook)
            Return outstandingChecks
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un cheque pendiente por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetOutstandingChecksById(Id As Integer) As OutstandingChecks Implements IOutstandingChecksAdminService.GetOutstandingChecksById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim outstandingChecks As OutstandingChecks = Me._outstandingChecksRepository.GetOutstandingChecksById(Id)
            Return outstandingChecks
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los cheques pendientes por id de la chequera
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdCheckBook</exception>
    Public Function ListOutstandingChecksByIdCheckBook(IdCheckBook As Integer) As List(Of OutstandingChecks) Implements IOutstandingChecksAdminService.ListOutstandingChecksByIdCheckBook
        If IdCheckBook = 0 Then
            Throw New ArgumentNullException("IdCheckBook")
        End If
        Try
            Dim outstandingChecks As List(Of OutstandingChecks) = Me._outstandingChecksRepository.ListOutstandingChecksByIdCheckBook(IdCheckBook)
            Return outstandingChecks
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the outstanding checks.
    ''' </summary>
    ''' <param name="outstandingChecks"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">outstandingChecks</exception>
    Public Function SaveOutstandingChecks(outstandingChecks As OutstandingChecks, audit As AuditMessage) As ActionResult(Of OutstandingChecks) Implements IOutstandingChecksAdminService.SaveOutstandingChecks
        If outstandingChecks Is Nothing Then
            Throw New ArgumentNullException("outstandingChecks")
        End If
        Dim unitOfWork As IUnitWork = Me._outstandingChecksRepository.UnitWork

        Try

            Dim auxOutstandingChecks As OutstandingChecks = Nothing
            Dim status As Integer
            If outstandingChecks.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxOutstandingChecks = outstandingChecks.OriginalValue
            End If

            Me._outstandingChecksRepository.SaveEntity(outstandingChecks)
            unitOfWork.Commit()
            Dim auditProcess As New IndigoAuditSimpleEntity(Of OutstandingChecks)(outstandingChecks, audit, status, auxOutstandingChecks)
            auditProcess.Execute()
            'Se marca la entidad como sin cambios
            outstandingChecks.MarkAsUnchanged()

            Return New ActionResult(Of OutstandingChecks) With {.StateResult = True, .ObjectEmbbeded = outstandingChecks}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of OutstandingChecks) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of OutstandingChecks) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un cheque pendiente por id de la chequera y numero del cheque
    ''' </summary>
    Public Function GetOutstandingCheckByCheckBookIdAndCheckNumber(checkBookId As Integer, checkNumber As Long) As OutstandingChecks Implements IOutstandingChecksAdminService.GetOutstandingCheckByCheckBookIdAndCheckNumber
        If checkBookId = 0 Then
            Throw New ArgumentNullException("checkBookId")
        End If
        If checkNumber = 0 Then
            Throw New ArgumentNullException("checkNumber")
        End If

        Try
            Return _outstandingChecksRepository.GetOutstandingCheckByCheckBookIdAndCheckNumber(checkBookId, checkNumber)
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
            _outstandingChecksRepository = Nothing
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
