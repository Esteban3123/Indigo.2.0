'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2015
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

Public Class AccountPayableRejectionReasonAdminService
    Implements IAccountPayableRejectionReasonAdminService
    Private Const FORM_NAME As String = "FrmAccountPayableRejectionReason"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRejectioReasonRepository As IAccountPayableRejectionReasonRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal accountPayableRejectioReasonRepository As IAccountPayableRejectionReasonRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository)
        If accountPayableRejectioReasonRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRejectioReasonRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _accountPayableRejectioReasonRepository = accountPayableRejectioReasonRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AccountPayableRejectionReason) Implements IAccountPayableRejectionReasonAdminService.ChangeState
        'Dim AccountPayableRejectionReason As AccountPayableRejectionReason = _accountPayableRejectioReasonRepository.GetAccountPayableRejectionReason(code)
        'AccountPayableRejectionReason.Status = state
        'Return SaveAccountPayableRejectionReason(AccountPayableRejectionReason, audit)
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
            Dim AccountPayableRejectionReason As AccountPayableRejectionReason = Me._accountPayableRejectioReasonRepository.GetAccountPayableRejectionReason(code.Trim())
            If AccountPayableRejectionReason IsNot Nothing AndAlso AccountPayableRejectionReason.Id > 0 Then
                AccountPayableRejectionReason.Status = state
            End If
            Dim result = Me.SaveAccountPayableRejectionReason(AccountPayableRejectionReason, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="AccountPayableRejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountPayableRejectionReason(AccountPayableRejectionReason As AccountPayableRejectionReason, audit As AuditMessage) As ActionResult Implements IAccountPayableRejectionReasonAdminService.DeleteAccountPayableRejectionReason

        If AccountPayableRejectionReason Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._accountPayableRejectioReasonRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                AccountPayableRejectionReason.ModificationUser = audit.CodeUser
                AccountPayableRejectionReason.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of AccountPayableRejectionReason)(AccountPayableRejectionReason, audit, status)

                'While AccountPayableRejectionReason.InvoiceCategoriesUser.Count > 0
                '    AccountPayableRejectionReason.InvoiceCategoriesUser(AccountPayableRejectionReason.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                AccountPayableRejectionReason.MarkAsDeleted()
                Me._accountPayableRejectioReasonRepository.SaveEntity(AccountPayableRejectionReason)
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
    ''' Obtiene el rechazo de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableRejectionReason(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableRejectionReason) Implements IAccountPayableRejectionReasonAdminService.GetAccountPayableRejectionReason
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AccountPayableRejectionReason As AccountPayableRejectionReason = Me._accountPayableRejectioReasonRepository.GetAccountPayableRejectionReason(code.Trim())
            If AccountPayableRejectionReason IsNot Nothing AndAlso AccountPayableRejectionReason.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableRejectionReason)(AccountPayableRejectionReason, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = True, .ObjectEmbbeded = AccountPayableRejectionReason}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el rechazo de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableRejectionReasonById(id As String, audit As AuditMessage) As ActionResult(Of AccountPayableRejectionReason) Implements IAccountPayableRejectionReasonAdminService.GetAccountPayableRejectionReasonById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AccountPayableRejectionReason As AccountPayableRejectionReason = Me._accountPayableRejectioReasonRepository.GetAccountPayableRejectionReasonById(id)
            If AccountPayableRejectionReason IsNot Nothing AndAlso AccountPayableRejectionReason.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableRejectionReason)(AccountPayableRejectionReason, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = True, .ObjectEmbbeded = AccountPayableRejectionReason}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el rechazo de factura
    ''' </summary>
    ''' <param name="AccountPayableRejectionReason"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountPayableRejectionReason(AccountPayableRejectionReason As AccountPayableRejectionReason, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AccountPayableRejectionReason) Implements IAccountPayableRejectionReasonAdminService.SaveAccountPayableRejectionReason


        If AccountPayableRejectionReason Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._accountPayableRejectioReasonRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(AccountPayableRejectionReason.Code) Then
                    Dim seq As PaymentsSecuenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AccountPayableRejectionReason.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AccountPayableRejectionReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PaymentsSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), AccountPayableRejectionReason.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AccountPayableRejectionReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As AccountPayableRejectionReason = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableRejectionReason)
                Dim status As Integer

                If AccountPayableRejectionReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AccountPayableRejectionReason.CreationUser = audit.CodeUser
                    AccountPayableRejectionReason.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = AccountPayableRejectionReason.OriginalValue
                    AccountPayableRejectionReason.ModificationUser = audit.CodeUser
                    AccountPayableRejectionReason.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._accountPayableRejectioReasonRepository.SaveEntity(AccountPayableRejectionReason)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableRejectionReason)(AccountPayableRejectionReason, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                AccountPayableRejectionReason.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = AccountPayableRejectionReason, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableRejectionReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Lista las razones de rechazo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRejectionReason() As List(Of AccountPayableRejectionReason) Implements IAccountPayableRejectionReasonAdminService.ListRejectionReason
        Try
            Return _accountPayableRejectioReasonRepository.ListRejectionReason()
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
            _secuenseDRepository = Nothing
            _accountPayableRejectioReasonRepository = Nothing
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
