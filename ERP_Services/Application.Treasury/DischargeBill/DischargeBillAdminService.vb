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
Imports Infrastructure.CrossCutting.Resources

Public Class DischargeBillAdminService
    Implements IDischargeBillAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _dischargeBillRepository As IDischargeBillRepository

#End Region

    Public Sub New(ByVal dischargeBillRepository As IDischargeBillRepository)
        If dischargeBillRepository Is Nothing Then
            Throw New ArgumentNullException("dischargeBillRepository")
        End If
        _dischargeBillRepository = dischargeBillRepository
    End Sub

    ''' <summary>
    ''' Elimina una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    Public Function DeleteDischargeBill(dischargeBill As DischargeBill, audit As AuditMessage) As ActionResult Implements IDischargeBillAdminService.DeleteDischargeBill
        If dischargeBill Is Nothing Then
            Throw New ArgumentNullException("dischargeBill")
        End If
        Dim unitOfWork As IUnitWork = Me._dischargeBillRepository.UnitWork
        Try
            If dischargeBill.ChangeTracker.State = ObjectState.Deleted Then

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DischargeBill)(dischargeBill, audit, status)

                Me._dischargeBillRepository.DeleteEntity(dischargeBill)
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
    ''' Obtiene o establece una factura de egresos por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    Public Function GetDischargeBillById(Id As Integer, audit As AuditMessage) As DischargeBill Implements IDischargeBillAdminService.GetDischargeBillById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim dischargeBill As DischargeBill = Me._dischargeBillRepository.GetDischargeBillById(Id)
            Return dischargeBill
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una factura de egreso por id de la cuota de la factura
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    Public Function GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare As Integer, audit As AuditMessage) As ActionResult(Of DischargeBill) Implements IDischargeBillAdminService.GetDischargeBillByIdAccountPayableShare
        If IdAccountPayableShare = 0 Then
            Throw New ArgumentNullException("IdAccountPayableShare")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim dischargeBill As DischargeBill = Me._dischargeBillRepository.GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare)
            Return New ActionResult(Of DischargeBill) With {.StateResult = True, .ObjectEmbbeded = dischargeBill}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DischargeBill) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una factura de egreso por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    Public Function ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD As Integer, audit As AuditMessage) As ActionResult(Of List(Of DischargeBill)) Implements IDischargeBillAdminService.ListDischargeBillByIdVoucherTransactionD
        If IdVoucherTransactionD = 0 Then
            Throw New ArgumentNullException("IdVoucherTransactionD")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim dischargeBill As List(Of DischargeBill) = Me._dischargeBillRepository.ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD)
            Return New ActionResult(Of List(Of DischargeBill)) With {.StateResult = True, .ObjectEmbbeded = dischargeBill}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of DischargeBill)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Guarda una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    Public Function SaveDischargeBill(dischargeBill As DischargeBill, audit As AuditMessage) As ActionResult(Of DischargeBill) Implements IDischargeBillAdminService.SaveDischargeBill
        If dischargeBill Is Nothing Then
            Throw New ArgumentNullException("dischargeBill")
        End If
        Dim unitOfWork As IUnitWork = Me._dischargeBillRepository.UnitWork

        Try

            Dim auxDischargeBill As DischargeBill = Nothing
            Dim status As Integer
            If dischargeBill.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxDischargeBill = dischargeBill.OriginalValue
            End If

            Me._dischargeBillRepository.SaveEntity(dischargeBill)
            unitOfWork.Commit()
            Dim auditProcess As New IndigoAuditSimpleEntity(Of DischargeBill)(dischargeBill, audit, status, auxDischargeBill)
            auditProcess.Execute()
            'Se marca la entidad como sin cambios
            dischargeBill.MarkAsUnchanged()

            Return New ActionResult(Of DischargeBill) With {.StateResult = True, .ObjectEmbbeded = dischargeBill}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DischargeBill) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DischargeBill) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _dischargeBillRepository = Nothing
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