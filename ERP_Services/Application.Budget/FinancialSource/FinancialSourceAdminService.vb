'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
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

Public Class FinancialSourceAdminService
    Implements IFinancialSourceAdminService

#Region "Fields"
    Private Const FORM_NAME As String = "FrmFinancialSource"
    ''' <summary>
    ''' Repositorio de las fuentes de financiacion
    ''' </summary>
    Private _financialSourceRepository As IFinancialSourceRepository

    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="financialSourceRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal financialSourceRepository As IFinancialSourceRepository, ByVal sequenseRepository As ISequenseBudgetDRepository)
        If financialSourceRepository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _financialSourceRepository = financialSourceRepository
        _sequenseBudgetDRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una Fuente financiera
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Public Function GetFinancialSource(code As String, validityId As Integer, audit As AuditMessage) As FinancialSource Implements IFinancialSourceAdminService.GetFinancialSource
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim financialSource As FinancialSource = Me._financialSourceRepository.GetFinancialSource(code.Trim(), validityId)
            If financialSource IsNot Nothing AndAlso financialSource.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FinancialSource)(financialSource, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return financialSource
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una fuente de financiación
    ''' </summary>
    ''' <param name="financialSource">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Function DeleteFinancialSource(financialSource As FinancialSource, audit As AuditMessage) As ActionResult Implements IFinancialSourceAdminService.DeleteFinancialSource
        'If financialSource Is Nothing Then
        '    Throw New ArgumentNullException("financialSource")
        'End If
        'Dim unitOfWork As IUnitWork = Me._financialSourceRepository.UnitWork
        'Try
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of FinancialSource)
        '    auditProcess = New IndigoAuditSimpleEntity(Of FinancialSource)(financialSource, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    Me._financialSourceRepository.DeleteEntity(financialSource)
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


        If financialSource Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._financialSourceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                financialSource.ModificationUser = audit.CodeUser
                financialSource.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FinancialSource)(financialSource, audit, status)

                'While financialSource.InvoiceCategoriesUser.Count > 0
                '    financialSource.InvoiceCategoriesUser(financialSource.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                financialSource.MarkAsDeleted()
                Me._financialSourceRepository.SaveEntity(financialSource)
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
    ''' Guarda o Actualiza una fuente de financiacion
    ''' </summary>
    ''' <param name="financialSource">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Function SaveFinancialSource(financialSource As FinancialSource, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FinancialSource) Implements IFinancialSourceAdminService.SaveFinancialSource
        'If financialSource Is Nothing Then
        '    Throw New ArgumentNullException("financialSource")
        'End If
        'Dim unitOfWork As IUnitWork = Me._financialSourceRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        'Try
        '    Dim seq As BudgetSequenceDetail = Nothing
        '    If financialSource.Code Is Nothing OrElse financialSource.Code.Trim().Equals(String.Empty) Then
        '        seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                financialSource.Code = res
        '                seq.Next += 1
        '                Me._sequenseBudgetDRepository.SaveEntity(seq)
        '            Else
        '                Return New ActionResult(Of FinancialSource) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
        '            End If
        '        Else
        '            Return New ActionResult(Of FinancialSource) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
        '        End If
        '    End If

        '    Dim auxFinancialSource As FinancialSource = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of FinancialSource)
        '    Dim status As Integer

        '    If financialSource.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        financialSource.CreationUser = audit.CodeUser
        '        financialSource.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxFinancialSource = financialSource.OriginalValue
        '        financialSource.ModificationUser = audit.CodeUser
        '        financialSource.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._financialSourceRepository.SaveEntity(financialSource)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of FinancialSource)(financialSource, audit, status, auxFinancialSource)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    financialSource.MarkAsUnchanged()

        '    Return New ActionResult(Of FinancialSource) With {.StateResult = True, .ObjectEmbbeded = financialSource}
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return New ActionResult(Of FinancialSource) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return New ActionResult(Of FinancialSource) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        'End Try


        If financialSource Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._financialSourceRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(financialSource.Code) Then
                    Dim seq As BudgetSequenceDetail = Me._sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            financialSource.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FinancialSource) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BudgetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), financialSource.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FinancialSource) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FinancialSource = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FinancialSource)
                Dim status As Integer

                If financialSource.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    financialSource.CreationUser = audit.CodeUser
                    financialSource.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = financialSource.OriginalValue
                    financialSource.ModificationUser = audit.CodeUser
                    financialSource.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._financialSourceRepository.SaveEntity(financialSource)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FinancialSource)(financialSource, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                financialSource.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FinancialSource) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = financialSource, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FinancialSource) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FinancialSource) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado a le entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateFinancialSource(code As String, validityId As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of FinancialSource) Implements IFinancialSourceAdminService.ChangeStateFinancialSource
        'Dim financialSource As FinancialSource = GetFinancialSource(code, validityId, audit)
        'financialSource.Status = state
        'financialSource.MarkAsModified()
        'Return SaveFinancialSource(financialSource, audit)



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
            Dim financialSource As FinancialSource = Me.GetFinancialSource(code.Trim(), validityId, audit)
            If financialSource IsNot Nothing AndAlso financialSource.Id > 0 Then
                financialSource.Status = state
            End If
            Dim result = Me.SaveFinancialSource(financialSource, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FinancialSource) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _financialSourceRepository = Nothing
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
