'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
'Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
#End Region

Public Class ExpenseTypeAdminService
    Implements IExpenseTypeAdminService


#Region "Fields"
    Private Const FORM_NAME As String = "FrmExpenseType"
    ''' <summary>
    ''' Repositorio de tipos de gasto
    ''' </summary>
    Private _ExpenseTypeRepository As IExpenseTypeRepository

    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ExpenseTypeRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ExpenseTypeRepository As IExpenseTypeRepository, ByVal sequenseRepository As ISequenseBudgetDRepository)
        If ExpenseTypeRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _ExpenseTypeRepository = ExpenseTypeRepository
        _sequenseBudgetDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    Public Function GetExpenseType(code As String, validityId As Integer, type As Integer, audit As AuditMessage) As RevenueType Implements IExpenseTypeAdminService.GetExpenseType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RevenueType As RevenueType = Me._ExpenseTypeRepository.GetExpenseType(code.Trim(), validityId, type)
            If RevenueType IsNot Nothing AndAlso RevenueType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RevenueType)(RevenueType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return RevenueType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetExpenseTypeByValidity(code As String, ValidityId As String, audit As AuditMessage) As RevenueType Implements IExpenseTypeAdminService.GetExpenseTypeByValidity
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RevenueType As RevenueType = Me._ExpenseTypeRepository.GetExpenseTypeByValidity(code.Trim(), ValidityId)
            If RevenueType IsNot Nothing AndAlso RevenueType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RevenueType)(RevenueType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return RevenueType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveExpenseType(RevenueType As RevenueType, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RevenueType) Implements IExpenseTypeAdminService.SaveExpenseType
        'If RevenueType Is Nothing Then
        '    Throw New ArgumentNullException("RevenueType")
        'End If
        'Dim unitOfWork As IUnitWork = Me._ExpenseTypeRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        'Try
        '    Dim seq As BudgetSequenceDetail = Nothing
        '    If RevenueType.Code Is Nothing OrElse RevenueType.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                RevenueType.Code = res
        '                seq.Next += 1
        '                Me._sequenseBudgetDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxRevenueType As RevenueType = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of RevenueType)
        '    Dim status As Integer

        '    If RevenueType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        RevenueType.CreationUser = audit.CodeUser
        '        RevenueType.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxRevenueType = RevenueType.OriginalValue
        '        RevenueType.ModificationUser = audit.CodeUser
        '        RevenueType.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._ExpenseTypeRepository.SaveEntity(RevenueType)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of RevenueType)(RevenueType, audit, status, auxRevenueType)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    RevenueType.MarkAsUnchanged()

        '    Return New ActionResult(Of RevenueType) With {.StateResult = True, .ObjectEmbbeded = RevenueType}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of RevenueType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try



        If RevenueType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._ExpenseTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(RevenueType.Code) Then
                    Dim seq As BudgetSequenceDetail = Me._sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            RevenueType.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RevenueType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BudgetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), RevenueType.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RevenueType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As RevenueType = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RevenueType)
                Dim status As Integer

                If RevenueType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    RevenueType.CreationUser = audit.CodeUser
                    RevenueType.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = RevenueType.OriginalValue
                    RevenueType.ModificationUser = audit.CodeUser
                    RevenueType.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._ExpenseTypeRepository.SaveEntity(RevenueType)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RevenueType)(RevenueType, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                RevenueType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RevenueType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = RevenueType, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RevenueType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RevenueType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function DeleteExpenseType(RevenueType As RevenueType, audit As AuditMessage) As ActionResult Implements IExpenseTypeAdminService.DeleteExpenseType
        'If RevenueType Is Nothing Then
        '    Throw New ArgumentNullException("RevenueType")
        'End If
        'Dim unitOfWork As IUnitWork = Me._ExpenseTypeRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of RevenueType)
        '    auditProcess = New IndigoAuditSimpleEntity(Of RevenueType)(RevenueType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._ExpenseTypeRepository.DeleteEntity(RevenueType)
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


        If RevenueType Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._ExpenseTypeRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                RevenueType.ModificationUser = audit.CodeUser
                RevenueType.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of RevenueType)(RevenueType, audit, status)

                'While earningsType.InvoiceCategoriesUser.Count > 0
                '    earningsType.InvoiceCategoriesUser(earningsType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                RevenueType.MarkAsDeleted()
                Me._ExpenseTypeRepository.SaveEntity(RevenueType)
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
    ''' metodo para cambiar el estado a la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateExpenseType(code As String, validityId As Integer, type As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of RevenueType) Implements IExpenseTypeAdminService.ChangeStateExpenseType


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
            Dim expenseType As RevenueType = GetExpenseType(code, validityId, type, audit)
            If expenseType IsNot Nothing AndAlso expenseType.Id > 0 Then
                expenseType.Status = state
            End If
            Dim result = Me.SaveExpenseType(expenseType, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RevenueType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _ExpenseTypeRepository = Nothing
            _sequenseBudgetDRepository = Nothing
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
