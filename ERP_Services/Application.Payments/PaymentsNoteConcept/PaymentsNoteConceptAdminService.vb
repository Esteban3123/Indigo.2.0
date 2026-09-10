'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/04/2014
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
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

Public Class PaymentsNoteConceptAdminService
    Implements IPaymentsNoteConceptAdminService
    Private Const FORM_NAME As String = "FrmConceptsNotes"
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentsNoteConceptRepository As IPaymentsNoteConceptRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal paymentsNoteConceptRepository As IPaymentsNoteConceptRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository)
        If paymentsNoteConceptRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _paymentsNoteConceptRepository = paymentsNoteConceptRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un concepto de nota
    ''' </summary>
    ''' <param name="paymentNoteConcept"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentNoteConcept(paymentNoteConcept As AccountPayableConceptNotes, audit As AuditMessage) As ActionResult Implements IPaymentsNoteConceptAdminService.DeletePaymentNoteConcept
        'If paymentNoteConcept Is Nothing Then
        '    Throw New ArgumentNullException("paymentConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._paymentsNoteConceptRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)
        '    auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)(paymentNoteConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._paymentsNoteConceptRepository.DeleteEntity(paymentNoteConcept)
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


        If paymentNoteConcept Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentsNoteConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                paymentNoteConcept.ModificationUser = audit.CodeUser
                paymentNoteConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)(paymentNoteConcept, audit, status)

                'While paymentNoteConcept.InvoiceCategoriesUser.Count > 0
                '    paymentNoteConcept.InvoiceCategoriesUser(paymentNoteConcept.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                paymentNoteConcept.MarkAsDeleted()
                Me._paymentsNoteConceptRepository.SaveEntity(paymentNoteConcept)
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
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNoteConcept(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes) Implements IPaymentsNoteConceptAdminService.GetPaymentNoteConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim paymentNoteConcept As AccountPayableConceptNotes = Me._paymentsNoteConceptRepository.GetPaymentNoteConcept(code.Trim())
            If paymentNoteConcept IsNot Nothing AndAlso paymentNoteConcept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)(paymentNoteConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = True, .ObjectEmbbeded = paymentNoteConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPaymentNoteConcept(audit As AuditMessage) As List(Of AccountPayableConceptNotes) Implements IPaymentsNoteConceptAdminService.ListAllPaymentNoteConcept
        Try
            Dim paymentNoteConcept = Me._paymentsNoteConceptRepository.GetAll()
            For Each item As AccountPayableConceptNotes In paymentNoteConcept
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return paymentNoteConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de nota
    ''' </summary>
    ''' <param name="paymentNoteConcept"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentNoteConcept(paymentNoteConcept As AccountPayableConceptNotes, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountPayableConceptNotes) Implements IPaymentsNoteConceptAdminService.SavePaymentNoteConcept
        'If paymentNoteConcept Is Nothing Then
        '    Throw New ArgumentNullException("paymentNoteConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._paymentsNoteConceptRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try
        '    Dim seq As PaymentsSecuenceDetail = Nothing
        '    If paymentNoteConcept.Code Is Nothing OrElse paymentNoteConcept.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                paymentNoteConcept.Code = res
        '                seq.Next += 1
        '                Me._secuenseDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxPaymentNoteConcept As AccountPayableConceptNotes = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)
        '    Dim status As Integer

        '    If paymentNoteConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        paymentNoteConcept.CreationUser = audit.CodeUser
        '        paymentNoteConcept.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxPaymentNoteConcept = paymentNoteConcept.OriginalValue
        '        paymentNoteConcept.ModificationUser = audit.CodeUser
        '        paymentNoteConcept.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._paymentsNoteConceptRepository.SaveEntity(paymentNoteConcept)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)(paymentNoteConcept, audit, status, auxPaymentNoteConcept)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    paymentNoteConcept.MarkAsUnchanged()

        '    Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = True, .ObjectEmbbeded = paymentNoteConcept}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try





        If paymentNoteConcept Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentsNoteConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(paymentNoteConcept.Code) Then
                    Dim seq As PaymentsSecuenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            paymentNoteConcept.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AccountPayableConceptNotes) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PaymentsSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), paymentNoteConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AccountPayableConceptNotes) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As AccountPayableConceptNotes = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)
                Dim status As Integer

                If paymentNoteConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    paymentNoteConcept.CreationUser = audit.CodeUser
                    paymentNoteConcept.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = paymentNoteConcept.OriginalValue
                    paymentNoteConcept.ModificationUser = audit.CodeUser
                    paymentNoteConcept.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._paymentsNoteConceptRepository.SaveEntity(paymentNoteConcept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)(paymentNoteConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                paymentNoteConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = paymentNoteConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes) Implements IPaymentsNoteConceptAdminService.ChangeState
        'Dim paymentNoteConcept As AccountPayableConceptNotes = _paymentsNoteConceptRepository.GetPaymentNoteConcept(code)
        'paymentNoteConcept.Status = state
        'Return SavePaymentNoteConcept(paymentNoteConcept, audit)




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
            Dim paymentNoteConcept As AccountPayableConceptNotes = Me._paymentsNoteConceptRepository.GetPaymentNoteConcept(code.Trim())
            If paymentNoteConcept IsNot Nothing AndAlso paymentNoteConcept.Id > 0 Then
                paymentNoteConcept.Status = state
            End If
            Dim result = Me.SavePaymentNoteConcept(paymentNoteConcept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Consulta el concepto de nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNoteConceptById(id As String, audit As AuditMessage) As ActionResult(Of AccountPayableConceptNotes) Implements IPaymentsNoteConceptAdminService.GetPaymentNoteConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim paymentNoteConcept As AccountPayableConceptNotes = Me._paymentsNoteConceptRepository.GetPaymentNoteConceptById(id)
            If paymentNoteConcept IsNot Nothing AndAlso paymentNoteConcept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableConceptNotes)(paymentNoteConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = True, .ObjectEmbbeded = paymentNoteConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableConceptNotes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _secuenseDRepository = Nothing
            _paymentsNoteConceptRepository = Nothing
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
