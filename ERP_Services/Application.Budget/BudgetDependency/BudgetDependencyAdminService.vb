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

Public Class BudgetDependencyAdminService
    Implements IBudgetDependencyAdminService

#Region "Fields"
    Private Const FORM_NAME As String = "FrmBudgetDependency"
    ''' <summary>
    ''' Repositorio de dependencias
    ''' </summary>
    Private _BudgetDependencyRepository As IBudgetDependencyRepository

    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="BudgetDependencyRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal BudgetDependencyRepository As IBudgetDependencyRepository, ByVal sequenseRepository As ISequenseBudgetDRepository)
        If BudgetDependencyRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _BudgetDependencyRepository = BudgetDependencyRepository
        _sequenseBudgetDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    Public Function GetBudgetDependency(code As String, validityId As Integer, audit As AuditMessage) As Dependency Implements IBudgetDependencyAdminService.GetBudgetDependency
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Dependency As Dependency = Me._BudgetDependencyRepository.GetBudgetDependency(code.Trim(), validityId)
            If Dependency IsNot Nothing AndAlso Dependency.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Dependency)(Dependency, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Dependency
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetBudgetDependencyByValidity(code As String, ValidityId As String, audit As AuditMessage) As Dependency Implements IBudgetDependencyAdminService.GetBudgetDependencyByValidity
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Dependency As Dependency = Me._BudgetDependencyRepository.GetBudgetDependencyByValidity(code.Trim(), ValidityId)
            If Dependency IsNot Nothing AndAlso Dependency.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Dependency)(Dependency, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Dependency
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveBudgetDependency(Dependency As Dependency, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Dependency) Implements IBudgetDependencyAdminService.SaveBudgetDependency
        'If Dependency Is Nothing Then
        '    Throw New ArgumentNullException("Dependency")
        'End If
        'Dim unitOfWork As IUnitWork = Me._BudgetDependencyRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        'Try
        '    Dim seq As BudgetSequenceDetail = Nothing
        '    If Dependency.Code Is Nothing OrElse Dependency.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                Dependency.Code = res
        '                seq.Next += 1
        '                Me._sequenseBudgetDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of Dependency) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of Dependency) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxDependency As Dependency = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of Dependency)
        '    Dim status As Integer

        '    If Dependency.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        Dependency.CreationUser = audit.CodeUser
        '        Dependency.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxDependency = Dependency.OriginalValue
        '        Dependency.ModificationUser = audit.CodeUser
        '        Dependency.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._BudgetDependencyRepository.SaveEntity(Dependency)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of Dependency)(Dependency, audit, status, auxDependency)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    Dependency.MarkAsUnchanged()

        '    Return New ActionResult(Of Dependency) With {.StateResult = True, .ObjectEmbbeded = Dependency}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of Dependency) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of Dependency) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try



        If Dependency Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetDependencyRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Dependency.Code) Then
                    Dim seq As BudgetSequenceDetail = Me._sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Dependency.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Dependency) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BudgetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Dependency.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Dependency) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Dependency = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Dependency)
                Dim status As Integer

                If Dependency.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Dependency.CreationUser = audit.CodeUser
                    Dependency.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Dependency.OriginalValue
                    Dependency.ModificationUser = audit.CodeUser
                    Dependency.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._BudgetDependencyRepository.SaveEntity(Dependency)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Dependency)(Dependency, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Dependency.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Dependency) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Dependency, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Dependency) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Dependency) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function DeleteBudgetDependency(Dependency As Dependency, audit As AuditMessage) As ActionResult Implements IBudgetDependencyAdminService.DeleteBudgetDependency
        'If Dependency Is Nothing Then
        '    Throw New ArgumentNullException("Dependency")
        'End If
        'Dim unitOfWork As IUnitWork = Me._BudgetDependencyRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of Dependency)
        '    auditProcess = New IndigoAuditSimpleEntity(Of Dependency)(Dependency, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._BudgetDependencyRepository.DeleteEntity(Dependency)
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


        If Dependency Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetDependencyRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dependency.ModificationUser = audit.CodeUser
                Dependency.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Dependency)(Dependency, audit, status)

                'While earningsType.InvoiceCategoriesUser.Count > 0
                '    earningsType.InvoiceCategoriesUser(earningsType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                Dependency.MarkAsDeleted()
                Me._BudgetDependencyRepository.SaveEntity(Dependency)
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
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateBudgetDependency(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of Dependency) Implements IBudgetDependencyAdminService.ChangeStateBudgetDependency

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
            Dim Dependency As Dependency = GetBudgetDependency(code, validityId, audit)
            If Dependency IsNot Nothing AndAlso Dependency.Id > 0 Then
                Dependency.Status = state
            End If
            Dim result = Me.SaveBudgetDependency(Dependency, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Dependency) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetBudgetDependencyById(id As Integer, audit As AuditMessage) As Dependency Implements IBudgetDependencyAdminService.GetBudgetDependencyById
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Dependency As Dependency = Me._BudgetDependencyRepository.GetBudgetDependencyById(id)
            If Dependency IsNot Nothing AndAlso Dependency.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Dependency)(Dependency, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Dependency
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
            _BudgetDependencyRepository = Nothing
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
