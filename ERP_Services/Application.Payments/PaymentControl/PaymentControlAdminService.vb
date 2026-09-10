'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
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
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class PaymentControlAdminService
    Implements IPaymentControlAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de control de documentos de tesoreria
    ''' </summary>
    Private _paymentControlRepository As IPaymentControlRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal paymentControlRepository As IPaymentControlRepository)
        If paymentControlRepository Is Nothing Then
            Throw New ArgumentNullException("paymentControlRepository")
        End If
        _paymentControlRepository = paymentControlRepository
    End Sub

    ''' <summary>
    ''' Elimina un registro de control de los documentos de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">treasuryControl</exception>
    Public Function DeletePaymentControl(paymentControl As PaymentsControl, audit As AuditMessage) As ActionResult Implements IPaymentControlAdminService.DeletePaymentControl
        If paymentControl Is Nothing Then
            Throw New ArgumentNullException("paymentControl")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentControlRepository.UnitWork
        Try
            If paymentControl.ChangeTracker.State = ObjectState.Deleted Then
                Me._paymentControlRepository.DeleteEntity(paymentControl)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(PaymentsControl).Name, audit.Functional, paymentControl.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentsControl)(paymentControl, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de pago por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPaymentControlById(Id As Integer) As PaymentsControl Implements IPaymentControlAdminService.GetPaymentControlById
        Try
            Return _paymentControlRepository.GetPaymentControlById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro de control de los documentos de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">treasuryControl</exception>
    Public Function SavePaymentControl(paymentControl As PaymentsControl, audit As AuditMessage) As ActionResult(Of PaymentsControl) Implements IPaymentControlAdminService.SavePaymentControl
        If paymentControl Is Nothing Then
            Throw New ArgumentNullException("paymentControl")
        End If
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Dim unitOfWork As IUnitWork = Me._paymentControlRepository.UnitWork
            Try
                Dim auxPaymentControl As PaymentsControl = paymentControl.OriginalValue
                If paymentControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse paymentControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Me._paymentControlRepository.SaveEntity(paymentControl)
                End If
                unitOfWork.Commit()
                If paymentControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    'IndigoAuditBasic.Execute(GetType(PaymentsControl).Name, audit.Functional, paymentControl.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentsControl)(paymentControl, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf paymentControl.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    'IndigoAuditBasic.Execute(GetType(PaymentsControl).Name, audit.Functional, paymentControl.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                    '/***** Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentsControl)(paymentControl, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxPaymentControl)
                    auditObject.Execute()
                End If

                'Se marca la entidad como sin cambios
                paymentControl.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of PaymentsControl) With {.StateResult = True, .ObjectEmbbeded = paymentControl}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of PaymentsControl) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PaymentsControl) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de pago por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Public Function GetPaymentControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As PaymentsControl Implements IPaymentControlAdminService.GetPaymentControlByDocumentNumber
        Try
            Return _paymentControlRepository.GetPaymentControlByDocumentNumber(DocumentNumber, DocumentType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
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
            _paymentControlRepository = Nothing
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