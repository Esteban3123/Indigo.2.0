'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-09-2015
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
Imports Domain.Entities.Service

#End Region

Public Class SuspensionCancellationAdminService
    Implements ISuspensionCancellationAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _suspensionCancellationRepository As ISuspensionCancellationRepository

    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

    ''' <summary>
    ''' repositorio de sequencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _sequenseBudgetDRepository As ISequenseBudgetDRepository

    Private _budgetService As IBudgetService

    ''' <summary>
    ''' Variable tipo repositorio para presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    ''' <summary>
    ''' Variable tipo repositorio de detalle de suspencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _suspensionDetailRepository As ISuspensionDetailRepository

#End Region

#Region "Builder"

    Public Sub New(suspensionCancellationRepository As ISuspensionCancellationRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, budgetRepository As IBudgetRepository,
                   suspensionDetailRepository As ISuspensionDetailRepository)
        If suspensionCancellationRepository Is Nothing Then
            Throw New ArgumentNullException("suspensionCancellationRepository")
        End If
        _suspensionCancellationRepository = suspensionCancellationRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _budgetRepository = budgetRepository
        _suspensionDetailRepository = suspensionDetailRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un levantamiento de suspención de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionCancellationByCode(code As String, audit As AuditMessage) As SuspensionCancellation Implements ISuspensionCancellationAdminService.GetSuspensionCancellationByCode
        Try
            Dim suspensionCancellation = _suspensionCancellationRepository.GetSuspensionCancellationByCode(code)
            If suspensionCancellation IsNot Nothing AndAlso suspensionCancellation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SuspensionCancellation)(suspensionCancellation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return suspensionCancellation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SuspensionCancellation
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un levantamiento de suspención de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionCancellationById(id As Integer) As SuspensionCancellation Implements ISuspensionCancellationAdminService.GetSuspensionCancellationById
        Try
            Return _suspensionCancellationRepository.GetSuspensionCancellationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SuspensionCancellation
        End Try
    End Function

    ''' <summary>
    ''' Guarda un levantamiento de suspención de presupuesto 
    ''' </summary>
    ''' <param name="suspensionCancellation"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSuspensionCancellation(suspensionCancellation As SuspensionCancellation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SuspensionCancellation) Implements ISuspensionCancellationAdminService.SaveSuspensionCancellation
        If suspensionCancellation Is Nothing Then
            Throw New ArgumentNullException("suspensionCancellation")
        End If

        Dim unitOfWork As IUnitWork = Me._suspensionCancellationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Dim unitOfWorkControlDocument As IUnitWork = Me._BudgetControlRepository.UnitWork
        Dim unitOfWorkSuspensionDetail As IUnitWork = Me._suspensionDetailRepository.UnitWork
        Dim unitOfworkBudget As IUnitWork = Me._budgetRepository.UnitWork

        Dim txtSettings = New TransactionOptions()
        txtSettings.Timeout = TransactionManager.MaximumTimeout
        txtSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txtSettings)
            Try
                Dim seq As BudgetSequenceDetail = Nothing
                If suspensionCancellation.Code Is Nothing OrElse suspensionCancellation.Code.Trim().Equals(String.Empty) Then
                    seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            suspensionCancellation.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of SuspensionCancellation) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        Return New ActionResult(Of SuspensionCancellation) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                'valido que el mes y el año coincidan con el de la vigencia
                Dim resultValidateMonth = _budgetService.ValidateBudgetPeriod(suspensionCancellation.BudgetaryValidityId, suspensionCancellation.DocumentDate, EBudgetType.Expense)
                If resultValidateMonth.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of SuspensionCancellation) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateMonth.Message}
                End If

                'Valido los datos del levantamiento de suspención presupuestal
                Dim resultValidateSuspensionCancellation = _budgetService.ValidateSuspensionCancellation(suspensionCancellation)
                If resultValidateSuspensionCancellation.Length > 0 Then
                    Return New ActionResult(Of SuspensionCancellation) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateSuspensionCancellation}
                End If

                Dim auxsuspensionCancellation As Domain.Entities.SuspensionCancellation = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of SuspensionCancellation)
                Dim status As Integer

                If suspensionCancellation.ChangeTracker.State = ObjectState.Added Then
                    suspensionCancellation.CreationUser = audit.CodeUser
                    suspensionCancellation.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf suspensionCancellation.ChangeTracker.State = ObjectState.Modified Then
                    auxsuspensionCancellation = suspensionCancellation.OriginalValue
                    If suspensionCancellation.Status = 1 OrElse suspensionCancellation.Status = 2 Then
                        suspensionCancellation.ModificationUser = audit.CodeUser
                        suspensionCancellation.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    ElseIf suspensionCancellation.Status = 3 Then
                        suspensionCancellation.ModificationUser = audit.CodeUser
                        suspensionCancellation.ModificationDate = DateTime.Now
                        suspensionCancellation.AnnulmentUser = audit.CodeUser
                        suspensionCancellation.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                'Afectamos los items en pac inicial
                If suspensionCancellation.Status = 2 Then
                    For Each item In suspensionCancellation.SuspensionCancellationDetail.ToList
                        If item.ChangeTracker.State <> ObjectState.Deleted Then
                            'afectamos la suspencion Presupuestal
                            Dim suspensionDetail As Domain.Entities.SuspensionDetail = _suspensionDetailRepository.GetSuspensionDetailById(item.SuspensionDetailId)
                            suspensionDetail.RaisedValue = suspensionDetail.RaisedValue + item.Value
                            suspensionDetail.Balance = suspensionDetail.InitialValue - suspensionDetail.RaisedValue
                            _suspensionDetailRepository.SaveEntity(suspensionDetail)
                            'afectamos el presupuesto inicial
                            Dim budget As Domain.Entities.Budget = _budgetRepository.GetBudgetById(suspensionDetail.BudgetId)
                            budget.SuspendedValue = budget.SuspendedValue - item.Value
                            budget.Balance = budget.TotalBudget - budget.ExecutedValue - budget.SuspendedValue
                            _budgetRepository.SaveEntity(budget)
                        End If
                    Next
                    suspensionCancellation.ConfirmationUser = audit.CodeUser
                    suspensionCancellation.ConfirmationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                End If

                If suspensionCancellation.Status = 1 Then
                    If suspensionCancellation.ChangeTracker.State = ObjectState.Added Then
                        Dim pc = _BudgetControlRepository.GetBudgetControl(suspensionCancellation.Code, 28)
                        If pc.Id = 0 Then
                            Dim BudgetControlDocument As BudgetControl = New BudgetControl()
                            BudgetControlDocument.DocumentNumber = suspensionCancellation.Code
                            BudgetControlDocument.DocumentType = 28
                            BudgetControlDocument.DocumentUser = audit.CodeUser
                            BudgetControlDocument.DocumentDate = suspensionCancellation.CreationDate
                            _BudgetControlRepository.SaveEntity(BudgetControlDocument)
                        End If
                    End If
                ElseIf (suspensionCancellation.Status = 2 AndAlso suspensionCancellation.Id > 0) OrElse suspensionCancellation.Status = 3 Then
                    Dim BudgetControl = _BudgetControlRepository.GetBudgetControl(suspensionCancellation.Code, 28)
                    If BudgetControl.Id > 0 Then
                        BudgetControl.MarkAsDeleted()
                        _BudgetControlRepository.DeleteEntity(BudgetControl)
                    End If
                End If

                _suspensionCancellationRepository.SaveEntity(suspensionCancellation)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                unitOfWorkControlDocument.Commit()
                unitOfworkBudget.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of SuspensionCancellation)(suspensionCancellation, audit, status, auxsuspensionCancellation)
                auditProcess.Execute()
                Transaction.Complete()
                Return New ActionResult(Of SuspensionCancellation) With {.StateResult = True, .ObjectEmbbeded = suspensionCancellation}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of SuspensionCancellation) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As InvalidOperationException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SuspensionCancellation) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SuspensionCancellation) With {.StateResult = False, .StateResultAux = False, .Message = ex.Message}
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _budgetService.Dispose()
            End If
            _suspensionCancellationRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _budgetRepository = Nothing
            _suspensionDetailRepository = Nothing
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
