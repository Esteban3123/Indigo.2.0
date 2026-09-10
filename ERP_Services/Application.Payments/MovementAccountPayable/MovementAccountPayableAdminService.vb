'***********************************************************************
' Assembly         : Application.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-07-2014
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

Public Class MovementAccountPayableAdminService
    Implements IMovementAccountPayableAdminService

    ''' <summary>
    ''' Variable tipo repositorio para movimientos de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _movementRepository As IMovementAccountPayableRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal movementRepository As IMovementAccountPayableRepository)
        If movementRepository Is Nothing Then
            Throw New ArgumentNullException("movementRepository")
        End If
        _movementRepository = movementRepository
    End Sub

    ''' <summary>
    ''' Elimina un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">movementAccountPayable</exception>
    Public Function DeleteMovementAccountPayable(movementAccountPayable As MovementAccountPayables, audit As AuditMessage) As ActionResult Implements IMovementAccountPayableAdminService.DeleteMovementAccountPayable
        If movementAccountPayable Is Nothing Then
            Throw New ArgumentNullException("movementAccountPayable")
        End If
        Dim unitOfWork As IUnitWork = Me._movementRepository.UnitWork
        Try
            If movementAccountPayable.ChangeTracker.State = ObjectState.Deleted Then
                Me._movementRepository.DeleteEntity(movementAccountPayable)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(MovementAccountPayables).Name, audit.Functional, movementAccountPayable.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of MovementAccountPayables)(movementAccountPayable, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
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
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetMovementAccountPayablesById(Id As Integer) As ActionResult(Of MovementAccountPayables) Implements IMovementAccountPayableAdminService.GetMovementAccountPayablesById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim movementAccountPayables As MovementAccountPayables = Me._movementRepository.GetMovementAccountPayablesById(Id)
            Return New ActionResult(Of MovementAccountPayables) With {.StateResult = True, .ObjectEmbbeded = movementAccountPayables}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MovementAccountPayables) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayable">The identifier account payable.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdAccountPayable</exception>
    Public Function GetMovementAccountPayablesByIdAccountPayable(IdAccountPayable As Integer) As ActionResult(Of List(Of MovementAccountPayables)) Implements IMovementAccountPayableAdminService.GetMovementAccountPayablesByIdAccountPayable
        If IdAccountPayable = 0 Then
            Throw New ArgumentNullException("IdAccountPayable")
        End If
        Try
            Dim movementAccountPayables As List(Of MovementAccountPayables) = Me._movementRepository.GetMovementAccountPayablesByIdAccountPayable(IdAccountPayable)
            Return New ActionResult(Of List(Of MovementAccountPayables)) With {.StateResult = True, .ObjectEmbbeded = movementAccountPayables}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of MovementAccountPayables)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de las cuotas de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdAccountPayable</exception>
    Public Function GetMovementAccountPayablesByIdAccountPayableShare(IdAccountPayableShare As Integer) As ActionResult(Of List(Of MovementAccountPayables)) Implements IMovementAccountPayableAdminService.GetMovementAccountPayablesByIdAccountPayableShare
        If IdAccountPayableShare = 0 Then
            Throw New ArgumentNullException("IdAccountPayable")
        End If
        Try
            Dim movementAccountPayables As List(Of MovementAccountPayables) = Me._movementRepository.GetMovementAccountPayablesByIdAccountPayableShare(IdAccountPayableShare)
            Return New ActionResult(Of List(Of MovementAccountPayables)) With {.StateResult = True, .ObjectEmbbeded = movementAccountPayables}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of MovementAccountPayables)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un movimiento de cuentas por pagar
    ''' </summary>
    ''' <param name="movementAccountPayable">The movement account payable.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">movementAccountPayable</exception>
    Public Function SaveMovementAccountPayable(movementAccountPayable As MovementAccountPayables, audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionResult(Of MovementAccountPayables) Implements IMovementAccountPayableAdminService.SaveMovementAccountPayable
        If movementAccountPayable Is Nothing Then
            Throw New ArgumentNullException("movementAccountPayable")
        End If
        Dim unitOfWork As IUnitWork = Me._movementRepository.UnitWork
        Try
            Dim auxMovementAccountPayables As MovementAccountPayables = movementAccountPayable.OriginalValue
            If movementAccountPayable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse movementAccountPayable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._movementRepository.SaveEntity(movementAccountPayable)
            End If
            If withCommit Then
                unitOfWork.Commit()
            End If
            If movementAccountPayable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(MovementAccountPayables).Name, audit.Functional, movementAccountPayable.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of MovementAccountPayables)(movementAccountPayable, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf movementAccountPayable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(MovementAccountPayables).Name, audit.Functional, movementAccountPayable.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of MovementAccountPayables)(movementAccountPayable, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxMovementAccountPayables)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MovementAccountPayables) With {.StateResult = True, .ObjectEmbbeded = movementAccountPayable}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MovementAccountPayables) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MovementAccountPayables) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _movementRepository = Nothing
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
