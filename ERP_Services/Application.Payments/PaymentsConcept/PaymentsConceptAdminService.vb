'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
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

Public Class PaymentsConceptAdminService
    Implements IPaymentsConceptAdminService

    Private Const FORM_NAME As String = "FrmConceptsAccountsPayable"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentsConceptRepository As IPaymentsConceptRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    Private _pucRepository As IPUCRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal paymentsConceptRepository As IPaymentsConceptRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository, ByVal pucRepository As IPUCRepository)
        If paymentsConceptRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If pucRepository Is Nothing Then
            Throw New ArgumentNullException("pucRepository")
        End If
        _paymentsConceptRepository = paymentsConceptRepository
        Me._secuenseDRepository = secuenseDRepository
        _pucRepository = pucRepository
    End Sub

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <param name="paymentConcept"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentConcept(paymentConcept As AccountPayableConcepts, audit As AuditMessage) As ActionResult Implements IPaymentsConceptAdminService.DeletePaymentConcept
        'If paymentConcept Is Nothing Then
        '    Throw New ArgumentNullException("paymentConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._paymentsConceptRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableConcepts)
        '    auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableConcepts)(paymentConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._paymentsConceptRepository.DeleteEntity(paymentConcept)
        '    unitOfWork.Commit()
        '    auditProcess.Execute()
        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As UpdateException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try




        If paymentConcept Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentsConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                paymentConcept.ModificationUser = audit.CodeUser
                paymentConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of AccountPayableConcepts)(paymentConcept, audit, status)

                'While paymentConcept.InvoiceCategoriesUser.Count > 0
                '    paymentConcept.InvoiceCategoriesUser(paymentConcept.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                paymentConcept.MarkAsDeleted()
                Me._paymentsConceptRepository.SaveEntity(paymentConcept)
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
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConcept(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableConcepts) Implements IPaymentsConceptAdminService.GetPaymentConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim paymentConcept As AccountPayableConcepts = Me._paymentsConceptRepository.GetPaymentConcept(code.Trim())
            If paymentConcept IsNot Nothing AndAlso paymentConcept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableConcepts)(paymentConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = True, .ObjectEmbbeded = paymentConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPaymentConcept(audit As AuditMessage) As List(Of AccountPayableConcepts) Implements IPaymentsConceptAdminService.ListAllPaymentConcept
        Try
            Dim paymentConcept = Me._paymentsConceptRepository.GetAll()
            For Each item As AccountPayableConcepts In paymentConcept
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableConcepts)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return paymentConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <param name="paymentConcept"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentConcept(paymentConcept As AccountPayableConcepts, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountPayableConcepts) Implements IPaymentsConceptAdminService.SavePaymentConcept
        'If paymentConcept Is Nothing Then
        '    Throw New ArgumentNullException("paymentConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._paymentsConceptRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try
        '    Dim seq As PaymentsSecuenceDetail = Nothing
        '    If paymentConcept.Code Is Nothing OrElse paymentConcept.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                paymentConcept.Code = res
        '                seq.Next += 1
        '                Me._secuenseDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If


        '    If paymentConcept.IdAccount IsNot Nothing AndAlso paymentConcept.HandlesRetention = True Then
        '        Dim mainAccount = _pucRepository.GetAccountById(paymentConcept.IdAccount, False)
        '        If mainAccount.RetencionType = 0 Then
        '            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .MessageResult = {"DontRetention"}.ToList()}
        '        End If
        '    End If



        '    Dim auxPaymentConcept As AccountPayableConcepts = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableConcepts)
        '    Dim status As Integer

        '    If paymentConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        paymentConcept.CreationUser = audit.CodeUser
        '        paymentConcept.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxPaymentConcept = paymentConcept.OriginalValue
        '        paymentConcept.ModificationUser = audit.CodeUser
        '        paymentConcept.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._paymentsConceptRepository.SaveEntity(paymentConcept)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableConcepts)(paymentConcept, audit, status, auxPaymentConcept)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    paymentConcept.MarkAsUnchanged()

        '    Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = True, .ObjectEmbbeded = paymentConcept}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try




        If paymentConcept Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentsConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(paymentConcept.Code) Then
                    Dim seq As PaymentsSecuenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            paymentConcept.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AccountPayableConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PaymentsSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), paymentConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AccountPayableConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As AccountPayableConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableConcepts)
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

                Me._paymentsConceptRepository.SaveEntity(paymentConcept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableConcepts)(paymentConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                paymentConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = paymentConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AccountPayableConcepts) Implements IPaymentsConceptAdminService.ChangeState
        'Dim paymentConcept As AccountPayableConcepts = _paymentsConceptRepository.GetPaymentConcept(code)
        'paymentConcept.Status = state
        'Return SavePaymentConcept(paymentConcept, audit)



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
            Dim paymentConcept As AccountPayableConcepts = Me._paymentsConceptRepository.GetPaymentConcept(code.Trim())
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
            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Concepto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConceptById(id As Integer, audit As AuditMessage) As ActionResult(Of AccountPayableConcepts) Implements IPaymentsConceptAdminService.GetPaymentConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim paymentConcept As AccountPayableConcepts = Me._paymentsConceptRepository.GetPaymentConceptById(id)
            If paymentConcept IsNot Nothing AndAlso paymentConcept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableConcepts)(paymentConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = True, .ObjectEmbbeded = paymentConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableConcepts) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _paymentsConceptRepository = Nothing
            _secuenseDRepository = Nothing
            _pucRepository = Nothing
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
