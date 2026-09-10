'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
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

#End Region

Public Class CostGeneralExpenseCategoryAdminService
    Implements ICostGeneralExpenseCategoryAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de estructura organizacional
    ''' </summary>
    Private _costGeneralExpenseCategoryRepository As ICostGeneralExpenseCategoryRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal costGeneralExpenseCategoryRepository As ICostGeneralExpenseCategoryRepository)
        If costGeneralExpenseCategoryRepository Is Nothing Then
            Throw New ArgumentNullException("costGeneralExpenseCategoryRepository")
        End If
        _costGeneralExpenseCategoryRepository = costGeneralExpenseCategoryRepository
    End Sub

#End Region

#Region "Methods"

    Public Function DeleteCostGeneralExpenseCategory(CostGeneralExpenseCategory As CostGeneralExpenseCategory, audit As AuditMessage) As ActionResult Implements ICostGeneralExpenseCategoryAdminService.DeleteCostGeneralExpenseCategory
        If CostGeneralExpenseCategory Is Nothing Then
            Throw New ArgumentNullException("CostGeneralExpenseCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._costGeneralExpenseCategoryRepository.UnitWork
        Try
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
            Dim auditProcess As New IndigoAuditSimpleEntity(Of CostGeneralExpenseCategory)(CostGeneralExpenseCategory, audit, status)
            Me._costGeneralExpenseCategoryRepository.DeleteEntity(CostGeneralExpenseCategory)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function GetCostGeneralExpenseCategory(code As String, audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory) Implements ICostGeneralExpenseCategoryAdminService.GetCostGeneralExpenseCategory
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CostGeneralExpenseCategory As CostGeneralExpenseCategory = Me._costGeneralExpenseCategoryRepository.GetCostGeneralExpenseCategory(code.Trim())
            If CostGeneralExpenseCategory IsNot Nothing AndAlso CostGeneralExpenseCategory.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostGeneralExpenseCategory)(CostGeneralExpenseCategory, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = True, .ObjectEmbbeded = CostGeneralExpenseCategory}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = False}
        End Try
    End Function

    Public Function GetCostGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory) Implements ICostGeneralExpenseCategoryAdminService.GetCostGeneralExpenseCategoryById
        If id = 0 Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CostGeneralExpenseCategory As CostGeneralExpenseCategory = Me._costGeneralExpenseCategoryRepository.GetCostGeneralExpenseCategoryById(id)
            If CostGeneralExpenseCategory IsNot Nothing AndAlso CostGeneralExpenseCategory.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostGeneralExpenseCategory)(CostGeneralExpenseCategory, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = True, .ObjectEmbbeded = CostGeneralExpenseCategory}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = False}
        End Try
    End Function

    Public Function SaveCostGeneralExpenseCategory(CostGeneralExpenseCategory As CostGeneralExpenseCategory, audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory) Implements ICostGeneralExpenseCategoryAdminService.SaveCostGeneralExpenseCategory
        If CostGeneralExpenseCategory Is Nothing Then
            Throw New ArgumentNullException("CostGeneralExpenseCategory")
        End If
        Using scope As New TransactionScope()
            Dim unitOfWork As IUnitWork = Me._costGeneralExpenseCategoryRepository.UnitWork
            Try

                Dim auxCostGeneralExpenseCategory As CostGeneralExpenseCategory = Nothing
                Dim status As Integer
                If CostGeneralExpenseCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CostGeneralExpenseCategory.CreationDate = Date.Now
                    CostGeneralExpenseCategory.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    CostGeneralExpenseCategory.ModificationDate = Date.Now
                    CostGeneralExpenseCategory.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxCostGeneralExpenseCategory = CostGeneralExpenseCategory.OriginalValue
                End If

                Me._costGeneralExpenseCategoryRepository.SaveEntity(CostGeneralExpenseCategory)
                unitOfWork.Commit()

                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostGeneralExpenseCategory)(CostGeneralExpenseCategory, audit, status, auxCostGeneralExpenseCategory)
                auditProcess.Execute()
                Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = True, .ObjectEmbbeded = CostGeneralExpenseCategory}

            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    Public Function UpdateState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostGeneralExpenseCategory) Implements ICostGeneralExpenseCategoryAdminService.UpdateState
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
            Dim CostGeneralExpenseCategory As CostGeneralExpenseCategory = Me._costGeneralExpenseCategoryRepository.GetCostGeneralExpenseCategory(code.Trim())
            If CostGeneralExpenseCategory IsNot Nothing AndAlso CostGeneralExpenseCategory.Id > 0 Then
                CostGeneralExpenseCategory.Status = state
            End If
            Return Me.SaveCostGeneralExpenseCategory(CostGeneralExpenseCategory, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostGeneralExpenseCategory) With {.StateResult = False}
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
            _costGeneralExpenseCategoryRepository = Nothing
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
