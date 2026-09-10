'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 18-09-2015
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

Public Class SuspensionAdminService
    Implements ISuspensionAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _suspensionRepository As ISuspensionRepository

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

#End Region

#Region "Builder"

    Public Sub New(suspensionRepository As ISuspensionRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, budgetRepository As IBudgetRepository)
        If suspensionRepository Is Nothing Then
            Throw New ArgumentNullException("suspensionRepository")
        End If
        _suspensionRepository = suspensionRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _budgetRepository = budgetRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una suspencion de presupuesto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionByCode(code As String, audit As AuditMessage) As Suspension Implements ISuspensionAdminService.GetSuspensionByCode
        Try
            Dim suspension = _suspensionRepository.GetSuspensionByCode(code)
            If suspension IsNot Nothing AndAlso suspension.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Suspension)(suspension, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return suspension
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Suspension
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una suspencion de presupuesto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionById(id As Integer) As Suspension Implements ISuspensionAdminService.GetSuspensionById
        Try
            Return _suspensionRepository.GetSuspensionById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Suspension
        End Try
    End Function

    ''' <summary>
    ''' guarda una suspencion de presupuesto
    ''' </summary>
    ''' <param name="suspension"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSuspension(suspension As Suspension, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Suspension) Implements ISuspensionAdminService.SaveSuspension
        If suspension Is Nothing Then
            Throw New ArgumentNullException("suspension")
        End If

        Dim unitOfWork As IUnitWork = Me._suspensionRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Dim unitOfWorkControlDocument As IUnitWork = Me._BudgetControlRepository.UnitWork
        Dim unitOfworkBudget As IUnitWork = Me._budgetRepository.UnitWork

        Dim txtSettings = New TransactionOptions()
        txtSettings.Timeout = TransactionManager.MaximumTimeout
        txtSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txtSettings)
            Try
                Dim seq As BudgetSequenceDetail = Nothing
                If suspension.Code Is Nothing OrElse suspension.Code.Trim().Equals(String.Empty) Then
                    seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            suspension.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of Suspension) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        Return New ActionResult(Of Suspension) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                'valido que el mes y el año coincidan con el de la vigencia
                Dim resultValidateMonth = _budgetService.ValidateBudgetPeriod(suspension.BudgetaryValidityId, suspension.DocumentDate, EBudgetType.Expense)
                If resultValidateMonth.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of Suspension) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateMonth.Message}
                End If

                'Valido los datos de la suspención presupuestal
                Dim resultValidateSuspension = _budgetService.ValidateSuspension(suspension)
                If resultValidateSuspension.Length > 0 Then
                    Return New ActionResult(Of Suspension) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateSuspension}
                End If

                Dim auxSuspension As Domain.Entities.Suspension = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Suspension)
                Dim status As Integer

                If suspension.ChangeTracker.State = ObjectState.Added Then
                    suspension.CreationUser = audit.CodeUser
                    suspension.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf suspension.ChangeTracker.State = ObjectState.Modified Then
                    auxSuspension = suspension.OriginalValue
                    If suspension.Status = 1 OrElse suspension.Status = 2 Then
                        suspension.ModificationUser = audit.CodeUser
                        suspension.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    ElseIf suspension.Status = 3 Then
                        suspension.ModificationUser = audit.CodeUser
                        suspension.ModificationDate = DateTime.Now
                        suspension.AnnulmentUser = audit.CodeUser
                        suspension.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                'Afectamos los items en pac inicial
                If suspension.Status = 2 Then
                    For Each item In suspension.SuspensionDetail.ToList
                        If item.ChangeTracker.State <> ObjectState.Deleted Then
                            Dim budget As Domain.Entities.Budget = _budgetRepository.GetBudgetById(item.BudgetId)
                            budget.SuspendedValue = budget.SuspendedValue + item.InitialValue
                            budget.Balance = budget.TotalBudget - budget.ExecutedValue - budget.SuspendedValue
                            _budgetRepository.SaveEntity(budget)
                        End If
                    Next
                    suspension.ConfirmationUser = audit.CodeUser
                    suspension.ConfirmationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                End If

                If suspension.Status = 1 Then
                    If suspension.ChangeTracker.State = ObjectState.Added Then
                        Dim pc = _BudgetControlRepository.GetBudgetControl(suspension.Code, 27)
                        If pc.Id = 0 Then
                            Dim BudgetControlDocument As BudgetControl = New BudgetControl()
                            BudgetControlDocument.DocumentNumber = suspension.Code
                            BudgetControlDocument.DocumentType = 27
                            BudgetControlDocument.DocumentUser = audit.CodeUser
                            BudgetControlDocument.DocumentDate = suspension.CreationDate
                            _BudgetControlRepository.SaveEntity(BudgetControlDocument)
                        End If
                    End If
                ElseIf (suspension.Status = 2 AndAlso suspension.Id > 0) OrElse suspension.Status = 3 Then
                    Dim BudgetControl = _BudgetControlRepository.GetBudgetControl(suspension.Code, 27)
                    If BudgetControl.Id > 0 Then
                        BudgetControl.MarkAsDeleted()
                        _BudgetControlRepository.DeleteEntity(BudgetControl)
                    End If
                End If

                _suspensionRepository.SaveEntity(suspension)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                unitOfWorkControlDocument.Commit()
                unitOfworkBudget.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Suspension)(suspension, audit, status, auxSuspension)
                auditProcess.Execute()
                Transaction.Complete()
                Return New ActionResult(Of Suspension) With {.StateResult = True, .ObjectEmbbeded = suspension}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of Suspension) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As InvalidOperationException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Suspension) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Suspension) With {.StateResult = False, .StateResultAux = False, .Message = ex.Message}
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
            _suspensionRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _budgetRepository = Nothing
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
