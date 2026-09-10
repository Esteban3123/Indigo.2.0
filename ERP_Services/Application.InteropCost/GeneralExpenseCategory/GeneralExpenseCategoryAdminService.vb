'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/11/2016
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

Public Class GeneralExpenseCategoryAdminService
    Implements IGeneralExpenseCategoryAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de estructura organizacional
    ''' </summary>
    Private _generalExpenseCategoryRepository As IGeneralExpenseCategoryRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal generalExpenseCategoryRepository As IGeneralExpenseCategoryRepository)
        If generalExpenseCategoryRepository Is Nothing Then
            Throw New ArgumentNullException("generalExpenseCategoryRepository")
        End If
        _generalExpenseCategoryRepository = generalExpenseCategoryRepository
    End Sub

#End Region

#Region "Methods"

    Public Function DeleteGeneralExpenseCategory(GeneralExpenseCategory As GeneralExpenseCategory, audit As AuditMessage) As ActionResult Implements IGeneralExpenseCategoryAdminService.DeleteGeneralExpenseCategory
        If GeneralExpenseCategory Is Nothing Then
            Throw New ArgumentNullException("GeneralExpenseCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._generalExpenseCategoryRepository.UnitWork
        Try
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
            Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralExpenseCategory)(GeneralExpenseCategory, audit, status)
            Me._generalExpenseCategoryRepository.DeleteEntity(GeneralExpenseCategory)
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

    Public Function GetGeneralExpenseCategory(code As String, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IGeneralExpenseCategoryAdminService.GetGeneralExpenseCategory
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim GeneralExpenseCategory As GeneralExpenseCategory = Me._generalExpenseCategoryRepository.GetGeneralExpenseCategory(code.Trim())
            If GeneralExpenseCategory IsNot Nothing AndAlso GeneralExpenseCategory.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralExpenseCategory)(GeneralExpenseCategory, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = True, .ObjectEmbbeded = GeneralExpenseCategory}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = False}
        End Try
    End Function

    Public Function GetGeneralExpenseCategoryById(id As Integer, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IGeneralExpenseCategoryAdminService.GetGeneralExpenseCategoryById
        If id = 0 Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim GeneralExpenseCategory As GeneralExpenseCategory = Me._generalExpenseCategoryRepository.GetGeneralExpenseCategoryById(id)
            If GeneralExpenseCategory IsNot Nothing AndAlso GeneralExpenseCategory.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralExpenseCategory)(GeneralExpenseCategory, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = True, .ObjectEmbbeded = GeneralExpenseCategory}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = False}
        End Try
    End Function

    Public Function SaveGeneralExpenseCategory(GeneralExpenseCategory As GeneralExpenseCategory, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IGeneralExpenseCategoryAdminService.SaveGeneralExpenseCategory
        If GeneralExpenseCategory Is Nothing Then
            Throw New ArgumentNullException("GeneralExpenseCategory")
        End If
        Using scope As New TransactionScope()
            Dim unitOfWork As IUnitWork = Me._generalExpenseCategoryRepository.UnitWork
            Try

                Dim auxGeneralExpenseCategory As GeneralExpenseCategory = Nothing
                Dim status As Integer
                If GeneralExpenseCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    GeneralExpenseCategory.CreationDate = Date.Now
                    GeneralExpenseCategory.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    GeneralExpenseCategory.ModificationDate = Date.Now
                    GeneralExpenseCategory.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxGeneralExpenseCategory = GeneralExpenseCategory.OriginalValue
                End If

                Me._generalExpenseCategoryRepository.SaveEntity(GeneralExpenseCategory)
                unitOfWork.Commit()

                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralExpenseCategory)(GeneralExpenseCategory, audit, status, auxGeneralExpenseCategory)
                auditProcess.Execute()
                Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = True, .ObjectEmbbeded = GeneralExpenseCategory}

            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    Public Function UpdateState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GeneralExpenseCategory) Implements IGeneralExpenseCategoryAdminService.UpdateState
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
            Dim GeneralExpenseCategory As GeneralExpenseCategory = Me._generalExpenseCategoryRepository.GetGeneralExpenseCategory(code.Trim())
            If GeneralExpenseCategory IsNot Nothing AndAlso GeneralExpenseCategory.Id > 0 Then
                GeneralExpenseCategory.Status = state
            End If
            Return Me.SaveGeneralExpenseCategory(GeneralExpenseCategory, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralExpenseCategory) With {.StateResult = False}
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
            _generalExpenseCategoryRepository = Nothing
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
