'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
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

Public Class ExpenseConceptAdminService
    Implements IExpenseConceptAdminService

#Region "Fields"

    Public Const FORM_NAME As String = "ExpenseConceptAdminService"

    ''' <summary>
    ''' Repositorio de conceptos de recibos de caja
    ''' </summary>
    Private _expenseConceptRepository As IExpenseConceptRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ISequenseTreasuryDRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal expenseConceptRepository As IExpenseConceptRepository, ByVal sequenceDRepository As ISequenseTreasuryDRepository)
        If expenseConceptRepository Is Nothing Then
            Throw New ArgumentNullException("expenseConceptRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _expenseConceptRepository = expenseConceptRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

    ''' <summary>
    ''' Elimina un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">expenseConcept</exception>
    Public Function DeleteExpenseConcept(expenseConcept As ExpenseConcepts, audit As AuditMessage) As ActionResult Implements IExpenseConceptAdminService.DeleteExpenseConcept
        'If expenseConcept Is Nothing Then
        '    Throw New ArgumentNullException("expenseConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._expenseConceptRepository.UnitWork
        'Try
        '    expenseConcept.MarkAsDeleted()

        '    expenseConcept.ModificationUser = audit.CodeUser
        '    expenseConcept.ModificationDate = Date.Now
        '    Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
        '    Dim auditProcess As New IndigoAuditSimpleEntity(Of ExpenseConcepts)(expenseConcept, audit, status)

        '    Me._expenseConceptRepository.DeleteEntity(expenseConcept)
        '    unitOfWork.Commit()
        '    auditProcess.Execute()
        '    Return New ActionResult With {.StateResult = True}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        'Catch ex As UpdateException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As DbUpdateException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult With {.StateResult = False}
        'End Try



        If expenseConcept Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._expenseConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                expenseConcept.ModificationUser = audit.CodeUser
                expenseConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ExpenseConcepts)(expenseConcept, audit, status)
                While expenseConcept.ExpenseConceptCashRegisters.Count > 0
                    expenseConcept.ExpenseConceptCashRegisters(expenseConcept.ExpenseConceptCashRegisters.Count - 1).MarkAsDeleted()
                End While
                expenseConcept.MarkAsDeleted()
                Me._expenseConceptRepository.SaveEntity(expenseConcept)
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
    Public Function UpdateStateExpenseConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ExpenseConcepts) Implements IExpenseConceptAdminService.UpdateStateExpenseConcept
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
        '    Dim expenseConcept As ExpenseConcepts = Me._expenseConceptRepository.GetExpenseConcept(code.Trim())
        '    If expenseConcept IsNot Nothing AndAlso expenseConcept.Id > 0 Then
        '        expenseConcept.Status = state
        '    End If
        '    Return Me.SaveExpenseConcept(expenseConcept, audit)
        'Catch ex As Exception
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False}
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
            Dim expenseConcept As ExpenseConcepts = Me._expenseConceptRepository.GetExpenseConcept(code.Trim())
            If expenseConcept IsNot Nothing AndAlso expenseConcept.Id > 0 Then
                expenseConcept.Status = state
            End If
            Dim result = Me.SaveExpenseConcept(expenseConcept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de egreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetExpenseConcept(code As String, audit As AuditMessage) As ActionResult(Of ExpenseConcepts) Implements IExpenseConceptAdminService.GetExpenseConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim expenseConcept As ExpenseConcepts = Me._expenseConceptRepository.GetExpenseConcept(code.Trim())
            If expenseConcept IsNot Nothing AndAlso expenseConcept.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ExpenseConcepts)(expenseConcept, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of ExpenseConcepts) With {.StateResult = True, .ObjectEmbbeded = expenseConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetExpenseConceptById(Id As Integer, audit As AuditMessage) As ExpenseConcepts Implements IExpenseConceptAdminService.GetExpenseConceptById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim expenseConcept As ExpenseConcepts = Me._expenseConceptRepository.GetExpenseConceptById(Id)
            Return expenseConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena un concepto de egreso
    ''' </summary>
    ''' <param name="expenseConcept">The expense concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">expenseConcept</exception>
    Public Function SaveExpenseConcept(expenseConcept As ExpenseConcepts, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ExpenseConcepts) Implements IExpenseConceptAdminService.SaveExpenseConcept
        'If expenseConcept Is Nothing Then
        '    Throw New ArgumentNullException("expenseConcept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._expenseConceptRepository.UnitWork
        'Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        'Try

        '    If String.IsNullOrEmpty(expenseConcept.Code) Then
        '        Dim seq As TreasurySequenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                expenseConcept.Code = res
        '                seq.Next += 1
        '                Me._sequenceDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxExpenseConcept As ExpenseConcepts = Nothing
        '    Dim status As Integer
        '    If expenseConcept.ChangeTracker.State = ObjectState.Added Then
        '        expenseConcept.CreationDate = Date.Now
        '        expenseConcept.CreationUser = audit.CodeUser
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        expenseConcept.ModificationDate = Date.Now
        '        expenseConcept.ModificationUser = audit.CodeUser
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        auxExpenseConcept = expenseConcept.OriginalValue
        '    End If

        '    Me._expenseConceptRepository.SaveEntity(expenseConcept)
        '    unitOfWork.Commit()
        '    Dim auditProcess As New IndigoAuditSimpleEntity(Of ExpenseConcepts)(expenseConcept, audit, status, auxExpenseConcept)
        '    auditProcess.Execute()
        '    'Se marca la entidad como sin cambios
        '    expenseConcept.MarkAsUnchanged()
        '    Return New ActionResult(Of ExpenseConcepts) With {.StateResult = True, .ObjectEmbbeded = expenseConcept}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False}
        'End Try




        If expenseConcept Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._expenseConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(expenseConcept.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            expenseConcept.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ExpenseConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), expenseConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ExpenseConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ExpenseConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ExpenseConcepts)
                Dim status As Integer

                If expenseConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    expenseConcept.CreationUser = audit.CodeUser
                    expenseConcept.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = expenseConcept.OriginalValue
                    expenseConcept.ModificationUser = audit.CodeUser
                    expenseConcept.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._expenseConceptRepository.SaveEntity(expenseConcept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ExpenseConcepts)(expenseConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                expenseConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ExpenseConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = expenseConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExpenseConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


    ''' <summary>
    ''' Obtiene los conceptos de egreso que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetExpenseConceptByFlowConcept(id As Integer) As List(Of ExpenseConcepts) Implements IExpenseConceptAdminService.GetExpenseConceptByFlowConcept
        If id = 0 Then
            Throw New InvalidOperationException("id")
        End If
        Try
            Return _expenseConceptRepository.GetExpenseConceptByFlowConcept(id)
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
            _expenseConceptRepository = Nothing
            _sequenceDRepository = Nothing
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
