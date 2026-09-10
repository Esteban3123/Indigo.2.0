'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-06-2015
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
Imports Domain.Entities.Service
Imports System.Transactions

Public Class ReversalReasonAdminService
    Implements IReversalReasonAdminService
    Private Const FORM_NAME As String = "FrmReversalReason"
#Region "Interface"
    Private _reversalReasonRepository As IBillingReversalReasonRepository
    Private _sequenceDetailRepository As IBillingSequenceDetailRepository
#End Region

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="ReversalReasonAdminService"/> class.
    ''' </summary>
    ''' <param name="reversalReasonRepository">The reversal reason repository.</param>
    Public Sub New(reversalReasonRepository As IBillingReversalReasonRepository, sequenceDetailRepository As IBillingSequenceDetailRepository)
        _reversalReasonRepository = reversalReasonRepository
        _sequenceDetailRepository = sequenceDetailRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteReversalReason(reversalReason As BillingReversalReason, audit As AuditMessage) As ActionResult Implements IReversalReasonAdminService.DeleteReversalReason
        'If reversalReason Is Nothing Then
        '    Throw New ArgumentNullException("expenseConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._reversalReasonRepository.UnitWork
        'Try
        '    Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

        '        If reversalReason.ChangeTracker.State = ObjectState.Deleted Then
        '            reversalReason.ModificationUser = audit.CodeUser
        '            reversalReason.ModificationDate = Date.Now
        '            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
        '            Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingReversalReason)(reversalReason, audit, status)

        '            Me._reversalReasonRepository.DeleteEntity(reversalReason)
        '            unitOfWork.Commit()
        '            auditProcess.Execute()
        '            scope.Complete()
        '            Return New ActionResult With {.StateResult = True}
        '        Else
        '            unitOfWork.RollbackChangesUnitOfWork()
        '            scope.Dispose()
        '            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        '        End If
        '    End Using

        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChangesUnitOfWork()
        '    Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        'Catch ex As UpdateException
        '    unitOfWork.RollbackChangesUnitOfWork()
        '    Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorDependence")}
        'Catch ex As DbUpdateException
        '    unitOfWork.RollbackChangesUnitOfWork()
        '    Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorDependence")}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        'End Try


        If reversalReason Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._reversalReasonRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                reversalReason.ModificationUser = audit.CodeUser
                reversalReason.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingReversalReason)(reversalReason, audit, status)

                'While reversalReason.InvoiceCategoriesUser.Count > 0
                '    reversalReason.InvoiceCategoriesUser(reversalReason.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                reversalReason.MarkAsDeleted()
                Me._reversalReasonRepository.SaveEntity(reversalReason)
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
    ''' Obtiene una razón de anulacóon por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetReversalReason(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of BillingReversalReason) Implements IReversalReasonAdminService.GetReversalReason
        Try
            Dim reversalReason As BillingReversalReason = Me._reversalReasonRepository.GetReversalReason(code.Trim(), tracking)
            If reversalReason IsNot Nothing AndAlso reversalReason.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingReversalReason)(reversalReason, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of BillingReversalReason) With {.StateResult = True, .ObjectEmbbeded = reversalReason}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una razón ade anulación por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetReversalReasonById(id As Integer, tracking As Boolean) As BillingReversalReason Implements IReversalReasonAdminService.GetReversalReasonById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._reversalReasonRepository.GetReversalReasonById(id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una razón de anulación
    ''' </summary>
    ''' <param name="reversalReason">The reversal reason.</param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveReversalReason(reversalReason As BillingReversalReason, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of BillingReversalReason) Implements IReversalReasonAdminService.SaveReversalReason
        'If reversalReason Is Nothing Then
        '    Throw New ArgumentNullException("expenseConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._reversalReasonRepository.UnitWork
        'Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDetailRepository.UnitWork

        'Try

        '    Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
        '        If String.IsNullOrEmpty(reversalReason.Code) Then
        '            Dim seq As BillingSequenceDetail = _sequenceDetailRepository.GetSequenseDById(idSequence)
        '            If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
        '                Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '                If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                    reversalReason.Code = res
        '                    seq.Next += 1
        '                    Me._sequenceDetailRepository.SaveEntity(seq)
        '                Else
        '                    unitOfWork.RollbackChangesUnitOfWork()
        '                    scope.Dispose()
        '                    Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
        '                End If
        '            Else
        '                unitOfWork.RollbackChangesUnitOfWork()
        '                scope.Dispose()
        '                Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
        '            End If
        '        End If

        '        Dim auxNoteConcept As BillingReversalReason = Nothing
        '        Dim status As Integer
        '        If reversalReason.ChangeTracker.State = ObjectState.Added Then
        '            reversalReason.CreationDate = Date.Now
        '            reversalReason.CreationUser = audit.CodeUser
        '            status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '        Else
        '            reversalReason.ModificationDate = Date.Now
        '            reversalReason.ModificationUser = audit.CodeUser
        '            status = Infrastructure.CrossCutting.Audit.Actions.Update
        '            auxNoteConcept = reversalReason.OriginalValue
        '        End If

        '        Me._reversalReasonRepository.SaveEntity(reversalReason)
        '        unitOfWork.Commit()
        '        sequenceUnitOfWork.Commit()
        '        Dim auditProcess As New IndigoAuditSimpleEntity(Of BillingReversalReason)(reversalReason, audit, status, auxNoteConcept)
        '        auditProcess.Execute()
        '        'Se marca la entidad como sin cambios
        '        reversalReason.MarkAsUnchanged()
        '        scope.Complete()
        '        Return New ActionResult(Of BillingReversalReason) With {.StateResult = True, .ObjectEmbbeded = reversalReason}
        '    End Using

        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChangesUnitOfWork()
        '    Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        'Catch ex As Exception
        '    unitOfWork.RollbackChangesUnitOfWork()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        'End Try



        If reversalReason Is Nothing Then
            Throw New ArgumentNullException("expenseConcept")
        End If
        Dim unitOfWork As IUnitWork = Me._reversalReasonRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(reversalReason.Code) Then
                    Dim seq As BillingSequenceDetail = Me._sequenceDetailRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            reversalReason.Code = res
                            seq.Next += 1
                            Me._sequenceDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of BillingReversalReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), reversalReason.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of BillingReversalReason) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As BillingReversalReason = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BillingReversalReason)
                Dim status As Integer

                If reversalReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    reversalReason.CreationUser = audit.CodeUser
                    reversalReason.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = reversalReason.OriginalValue
                    reversalReason.ModificationUser = audit.CodeUser
                    reversalReason.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._reversalReasonRepository.SaveEntity(reversalReason)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BillingReversalReason)(reversalReason, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                reversalReason.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BillingReversalReason) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = reversalReason, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state reversal reason.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateStateReversalReason(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BillingReversalReason) Implements IReversalReasonAdminService.UpdateStateReversalReason
        'If String.IsNullOrEmpty(code) Then
        '    Throw New ArgumentNullException("code")
        'End If
        'If String.IsNullOrEmpty(state) Then
        '    Throw New ArgumentNullException("state")
        'End If
        'If audit Is Nothing Then
        '    Throw New ArgumentNullException("audit")
        'End If

        'Try
        '    Dim reversalReason As BillingReversalReason = Me._reversalReasonRepository.GetReversalReason(code.Trim(), True)
        '    If reversalReason IsNot Nothing AndAlso reversalReason.Id > 0 Then
        '        reversalReason.Status = state
        '    End If
        '    Return Me.SaveReversalReason(reversalReason, audit)
        'Catch ex As Exception
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        'End Try

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
            Dim reversalReason As BillingReversalReason = Me._reversalReasonRepository.GetReversalReason(code.Trim(), True)
            If reversalReason IsNot Nothing AndAlso reversalReason.Id > 0 Then
                reversalReason.Status = state
            End If
            Dim result = Me.SaveReversalReason(reversalReason, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingReversalReason) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _reversalReasonRepository = Nothing
            _sequenceDetailRepository = Nothing
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
