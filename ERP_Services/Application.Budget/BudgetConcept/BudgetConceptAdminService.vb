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
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

'Imports System.Data.Entity.Core
'Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
#End Region

Public Class BudgetConceptAdminService
    Implements IBudgetConceptAdminService

#Region "Fields"
    Private Const FORM_NAME As String = "FrmBudgetConcept"
    ''' <summary>
    ''' Repositorio de conceptos
    ''' </summary>
    Private _BudgetConceptRepository As IBudgetConceptRepository

    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="BudgetConceptRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal BudgetConceptRepository As IBudgetConceptRepository, ByVal sequenseRepository As ISequenseBudgetDRepository)
        If BudgetConceptRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _BudgetConceptRepository = BudgetConceptRepository
        _sequenseBudgetDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetBudgetConcept(code As String, validityId As Integer, audit As AuditMessage) As Concept Implements IBudgetConceptAdminService.GetBudgetConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Concept As Concept = Me._BudgetConceptRepository.GetBudgetConcept(code.Trim(), validityId)
            If Concept IsNot Nothing AndAlso Concept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Concept)(Concept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
            End If
            Return Concept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="ValidityId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetBudgetConceptByValidity(code As String, ValidityId As String, audit As AuditMessage) As Concept Implements IBudgetConceptAdminService.GetBudgetConceptByValidity
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Concept As Concept = Me._BudgetConceptRepository.GetBudgetConceptByValidity(code.Trim(), ValidityId)
            If Concept IsNot Nothing AndAlso Concept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Concept)(Concept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Concept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the budget concept.
    ''' </summary>
    ''' <param name="Concept">The budget concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Concept</exception>
    Public Function SaveBudgetConcept(Concept As Concept, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Concept) Implements IBudgetConceptAdminService.SaveBudgetConcept
        'If Concept Is Nothing Then
        '    Throw New ArgumentNullException("Concept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._BudgetConceptRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        'Try
        '    Dim seq As BudgetSequenceDetail = Nothing
        '    If Concept.Code Is Nothing OrElse Concept.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                Concept.Code = res
        '                seq.Next += 1
        '                Me._sequenseBudgetDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of Concept) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of Concept) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxConcept As Concept = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of Concept)
        '    Dim status As Integer

        '    If Concept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        Concept.CreationUser = audit.CodeUser
        '        Concept.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxConcept = Concept.OriginalValue
        '        Concept.ModificationUser = audit.CodeUser
        '        Concept.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._BudgetConceptRepository.SaveEntity(Concept)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of Concept)(Concept, audit, status, auxConcept)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    Concept.MarkAsUnchanged()

        '    Return New ActionResult(Of Concept) With {.StateResult = True, .ObjectEmbbeded = Concept}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of Concept) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of Concept) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try



        If Concept Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Concept.Code) Then
                    Dim seq As BudgetSequenceDetail = Me._sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Concept.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Concept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BudgetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Concept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Concept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Concept = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Concept)
                Dim status As Integer

                If Concept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Concept.CreationUser = audit.CodeUser
                    Concept.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Concept.OriginalValue
                    Concept.ModificationUser = audit.CodeUser
                    Concept.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._BudgetConceptRepository.SaveEntity(Concept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Concept)(Concept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Concept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Concept) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Concept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Concept) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Concept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un concepto
    ''' </summary>
    ''' <param name="Concept">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Concept vacio</exception>
    Public Function DeleteBudgetConcept(Concept As Concept, audit As AuditMessage) As ActionResult Implements IBudgetConceptAdminService.DeleteBudgetConcept
        'If Concept Is Nothing Then
        '    Throw New ArgumentNullException("Concept")
        'End If
        'Dim unitOfWork As IUnitWork = Me._BudgetConceptRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of Concept)
        '    auditProcess = New IndigoAuditSimpleEntity(Of Concept)(Concept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._BudgetConceptRepository.DeleteEntity(Concept)
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



        If Concept Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Concept.ModificationUser = audit.CodeUser
                Concept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Concept)(Concept, audit, status)

                'While earningsType.InvoiceCategoriesUser.Count > 0
                '    earningsType.InvoiceCategoriesUser(earningsType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                Concept.MarkAsDeleted()
                Me._BudgetConceptRepository.SaveEntity(Concept)
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
    Public Function ChangeStateBudgetConcept(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of Concept) Implements IBudgetConceptAdminService.ChangeStateBudgetConcept
        'Dim budgetConcept As Concept = GetBudgetConcept(code, validityId, audit)
        'budgetConcept.Status = state
        'budgetConcept.MarkAsModified()
        'Return SaveBudgetConcept(budgetConcept, audit)


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
            Dim Concept As Concept = GetBudgetConcept(code, validityId, audit)
            If Concept IsNot Nothing AndAlso Concept.Id > 0 Then
                Concept.Status = state
            End If
            Dim result = Me.SaveBudgetConcept(Concept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Concept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _BudgetConceptRepository = Nothing
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
