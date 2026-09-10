'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
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

Public Class CheckBlockAdminService
    Implements ICheckBlockAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _checkBlockRepository As ICheckBlockRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal checkBlockRepository As ICheckBlockRepository)
        If checkBlockRepository Is Nothing Then
            Throw New ArgumentNullException("checkBlockRepository")
        End If
        _checkBlockRepository = checkBlockRepository
    End Sub

    ''' <summary>
    ''' Elimina un cheque bloqueado
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">checkBlock</exception>
    Public Function DeleteCheckBlock(checkBlock As CheckBlock, audit As AuditMessage) As ActionResult Implements ICheckBlockAdminService.DeleteCheckBlock
        If checkBlock Is Nothing Then
            Throw New ArgumentNullException("checkBlock")
        End If
        Dim unitOfWork As IUnitWork = Me._checkBlockRepository.UnitWork
        Try
            If checkBlock.ChangeTracker.State = ObjectState.Deleted Then

                'cashRegister.ModificationUser = audit.CodeUser
                'cashRegister.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CheckBlock)(checkBlock, audit, status)

                Me._checkBlockRepository.DeleteEntity(checkBlock)
                unitOfWork.Commit()
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
    ''' Obtiene un cheque bloqueado por id
    ''' </summary>
    Public Function GetCheckBlockById(Id As Integer, audit As AuditMessage) As CheckBlock Implements ICheckBlockAdminService.GetCheckBlockById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim checkBlock As CheckBlock = Me._checkBlockRepository.GetCheckBlockById(Id)
            Return checkBlock
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un cheque bloqueado por el id de la chequera y numero del cheque
    ''' </summary>
    Public Function GetCheckBlockByIdCheckBookAndNumber(IdCheckBook As Integer, checkNumber As Long, audit As AuditMessage) As ActionResult(Of CheckBlock) Implements ICheckBlockAdminService.GetCheckBlockByIdCheckBookAndNumber
        If IdCheckBook = 0 Then
            Throw New ArgumentNullException("IdCheckBook")
        End If
        If checkNumber = 0 Then
            Throw New ArgumentNullException("checkNumber")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim checkBlock As CheckBlock = Me._checkBlockRepository.GetCheckBlockByIdCheckBookAndNumber(IdCheckBook, checkNumber)
            Return New ActionResult(Of CheckBlock) With {.StateResult = True, .ObjectEmbbeded = checkBlock}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CheckBlock) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Bloquea un cheque
    ''' </summary>
    ''' <param name="checkBlock">The check block.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">checkBlock</exception>
    Public Function SaveCheckBlock(checkBlock As CheckBlock, audit As AuditMessage) As ActionResult(Of CheckBlock) Implements ICheckBlockAdminService.SaveCheckBlock
        If checkBlock Is Nothing Then
            Throw New ArgumentNullException("checkBlock")
        End If
        Dim unitOfWork As IUnitWork = Me._checkBlockRepository.UnitWork

        Try

            Me._checkBlockRepository.SaveEntity(checkBlock)
            unitOfWork.Commit()
            'Se marca la entidad como sin cambios
            checkBlock.MarkAsUnchanged()

            Return New ActionResult(Of CheckBlock) With {.StateResult = True, .ObjectEmbbeded = checkBlock}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CheckBlock) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CheckBlock) With {.StateResult = False}
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
            _checkBlockRepository = Nothing
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
