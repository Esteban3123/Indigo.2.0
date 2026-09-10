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
Imports System.Transactions

Public Class PaymentConceptAdminService
    Implements IPaymentConceptAdminService

#Region "Fields"
    Private Const FORM_NAME As String = "FrmPaymentConcepts"
    ''' <summary>
    ''' Repositorio de conceptos de pago
    ''' </summary>
    Private _paymentConceptRepository As IPaymentConceptRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository

#End Region

    Public Sub New(ByVal paymentConceptRepository As IPaymentConceptRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository)
        If paymentConceptRepository Is Nothing Then
            Throw New ArgumentNullException("TreasuryRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _paymentConceptRepository = paymentConceptRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <param name="paymentConcept">The payment concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">paymentConcept</exception>
    Public Function DeletePaymentConcept(paymentConcept As TreasuryPaymentConcepts, audit As AuditMessage) As ActionResult Implements IPaymentConceptAdminService.DeletePaymentConcept

        If paymentConcept Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                paymentConcept.ModificationUser = audit.CodeUser
                paymentConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of TreasuryPaymentConcepts)(paymentConcept, audit, status)

                'While paymentConcept.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                paymentConcept.MarkAsDeleted()
                Me._paymentConceptRepository.SaveEntity(paymentConcept)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStatePaymentConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of TreasuryPaymentConcepts) Implements IPaymentConceptAdminService.UpdateStatePaymentConcept

        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim paymentConcept As TreasuryPaymentConcepts = Me._paymentConceptRepository.GetPaymentConcept(code.Trim())
            If paymentConcept IsNot Nothing AndAlso paymentConcept.Id > 0 Then
                paymentConcept.Status = state
            End If
            Dim result = Me.SavePaymentConcept(paymentConcept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryPaymentConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetPaymentConcept(code As String, audit As AuditMessage) As ActionResult(Of TreasuryPaymentConcepts) Implements IPaymentConceptAdminService.GetPaymentConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim paymentConcept As TreasuryPaymentConcepts = Me._paymentConceptRepository.GetPaymentConcept(code.Trim())
            If paymentConcept IsNot Nothing AndAlso paymentConcept.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of TreasuryPaymentConcepts)(paymentConcept, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of TreasuryPaymentConcepts) With {.StateResult = True, .ObjectEmbbeded = paymentConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryPaymentConcepts) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un concepto de pago
    ''' </summary>
    ''' <param name="paymentConcept">The payment concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function SavePaymentConcept(paymentConcept As TreasuryPaymentConcepts, audit As AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of TreasuryPaymentConcepts) Implements IPaymentConceptAdminService.SavePaymentConcept

        If paymentConcept Is Nothing Then
            Throw New ArgumentNullException("paymentConcept")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(paymentConcept.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            paymentConcept.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of TreasuryPaymentConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), paymentConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of TreasuryPaymentConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As TreasuryPaymentConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of TreasuryPaymentConcepts)
                Dim status As Integer

                If paymentConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    paymentConcept.CreationUser = audit.CodeUser
                    paymentConcept.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = paymentConcept.OriginalValue
                    paymentConcept.ModificationUser = audit.CodeUser
                    paymentConcept.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._paymentConceptRepository.SaveEntity(paymentConcept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of TreasuryPaymentConcepts)(paymentConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                paymentConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of TreasuryPaymentConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = paymentConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TreasuryPaymentConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryPaymentConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _paymentConceptRepository = Nothing
            _secuenseDRepository = Nothing
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
