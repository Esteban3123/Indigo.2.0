'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/07/2014
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

Public Class PaymentNotesAccountPayableAdvanceAdminService
    Implements IPaymentNotesAccountPayableAdvanceAdminService

    ''' <summary>
    ''' Variable tipo repositorio para paymentNotesAccountPayableAdvance
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentNotesAccountPayableAdvanceRepository As IPaymentNotesAccountPayableAdvanceRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal paymentsNotesAccountPayableAdvanceRepository As IPaymentNotesAccountPayableAdvanceRepository)
        If paymentsNotesAccountPayableAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("paymentsNotesAccountPayableAdvanceRepository Vacio")
        End If
        Me._paymentNotesAccountPayableAdvanceRepository = paymentsNotesAccountPayableAdvanceRepository
    End Sub

    ''' <summary>
    ''' Elimina la relacion de notas a facturas o anticipo
    ''' </summary>
    ''' <param name="paymentNotesAccountPayableAdvance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance As PaymentNotesAccountPayableAdvance, audit As AuditMessage) As ActionResult Implements IPaymentNotesAccountPayableAdvanceAdminService.DeletePaymentNotesAccountPayableAdvance
        If paymentNotesAccountPayableAdvance Is Nothing Then
            Throw New ArgumentNullException("paymentNotesAccountPayableAdvance")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentNotesAccountPayableAdvanceRepository.UnitWork
        Try
            If paymentNotesAccountPayableAdvance.ChangeTracker.State = ObjectState.Deleted Then
                Me._paymentNotesAccountPayableAdvanceRepository.DeleteEntity(paymentNotesAccountPayableAdvance)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(PaymentNotesAccountPayableAdvance).Name, audit.Functional, paymentNotesAccountPayableAdvance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentNotesAccountPayableAdvance)(paymentNotesAccountPayableAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la relacion entre notas y facturas o anticipo por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNotesAccountPayableAdvanceById(id As String) As PaymentNotesAccountPayableAdvance Implements IPaymentNotesAccountPayableAdvanceAdminService.GetPaymentNotesAccountPayableAdvanceById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim paymentNotesAccountPayableAdvance As PaymentNotesAccountPayableAdvance = Me._paymentNotesAccountPayableAdvanceRepository.GetPaymentNotesAccountPayableAdvanceById(id)
            Return paymentNotesAccountPayableAdvance
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado afectados por la nota de facturas o anticipos segun corresponda
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id As Integer, type As Integer) As List(Of PaymentNotesAccountPayableAdvance) Implements IPaymentNotesAccountPayableAdvanceAdminService.GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim listPaymentNotesAccountPayableAdvance As List(Of PaymentNotesAccountPayableAdvance) = Me._paymentNotesAccountPayableAdvanceRepository.GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id, type)
            Return listPaymentNotesAccountPayableAdvance
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la relacion entre las notas y las facturas o anticipos
    ''' </summary>
    ''' <param name="paymentNotesAccountPayableAdvance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentNotesAccountPayableAdvance(paymentNotesAccountPayableAdvance As PaymentNotesAccountPayableAdvance, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PaymentNotesAccountPayableAdvance) Implements IPaymentNotesAccountPayableAdvanceAdminService.SavePaymentNotesAccountPayableAdvance
        If paymentNotesAccountPayableAdvance Is Nothing Then
            Throw New ArgumentNullException("paymentNotesAccountPayableAdvance")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentNotesAccountPayableAdvanceRepository.UnitWork
        Dim auxPaymentNotesAccountPayableAdvance As PaymentNotesAccountPayableAdvance = paymentNotesAccountPayableAdvance.OriginalValue
        Try
            'If supplierDistributionLine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse supplierDistributionLine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            Me._paymentNotesAccountPayableAdvanceRepository.SaveEntity(paymentNotesAccountPayableAdvance)
            ' End If
            unitOfWork.Commit()
            If paymentNotesAccountPayableAdvance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(PaymentNotesAccountPayableAdvance).Name, audit.Functional, paymentNotesAccountPayableAdvance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentNotesAccountPayableAdvance)(paymentNotesAccountPayableAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf paymentNotesAccountPayableAdvance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(PaymentNotesAccountPayableAdvance).Name, audit.Functional, paymentNotesAccountPayableAdvance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentNotesAccountPayableAdvance)(paymentNotesAccountPayableAdvance, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxPaymentNotesAccountPayableAdvance)
                auditObject.Execute()
            End If

            'Se marca la entidad como sin cambios
            paymentNotesAccountPayableAdvance.MarkAsUnchanged()

            Return New ActionResult(Of PaymentNotesAccountPayableAdvance) With {.StateResult = True, .ObjectEmbbeded = paymentNotesAccountPayableAdvance}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PaymentNotesAccountPayableAdvance) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PaymentNotesAccountPayableAdvance) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _paymentNotesAccountPayableAdvanceRepository = Nothing
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
