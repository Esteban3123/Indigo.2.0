'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/08/2015
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
Imports System.Transactions
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class BudgetTransferAdminService
    Implements IBudgetTransferAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetTransferRepository As IBudgetTransferRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository
    ''' <summary>
    ''' Repositorio de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetHeaderRepository As IBudgetHeaderRepository
    Private _budgetService As IBudgetService

    ''' <summary>
    ''' repositorio de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal budgetTransferRepository As IBudgetTransferRepository, ByVal secuenseDRepository As ISequenseBudgetDRepository, budgetHeaderRepository As IBudgetHeaderRepository,
                    budgetService As IBudgetService, budgetRepository As IBudgetRepository, budgetControlRepository As IBudgetControlRepository)
        If budgetTransferRepository Is Nothing Then
            Throw New ArgumentNullException("budgetTransferRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If budgetHeaderRepository Is Nothing Then
            Throw New ArgumentNullException("budgetHeaderRepository")
        End If
        _budgetTransferRepository = budgetTransferRepository
        _secuenseDRepository = secuenseDRepository
        _budgetHeaderRepository = budgetHeaderRepository
        _budgetService = budgetService
        Me._budgetRepository = budgetRepository
        _BudgetControlRepository = budgetControlRepository
    End Sub

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <param name="budgetTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBudgetTransfer(budgetTransfer As BudgetTransfer, audit As AuditMessage) As ActionResult Implements IBudgetTransferAdminService.DeleteBudgetTransfer
        If budgetTransfer Is Nothing Then
            Throw New ArgumentNullException("budgetTransfer")
        End If
        Dim unitOfWork As IUnitWork = Me._budgetTransferRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of BudgetTransfer)
            auditProcess = New IndigoAuditSimpleEntity(Of BudgetTransfer)(budgetTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._budgetTransferRepository.DeleteEntity(budgetTransfer)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <param name="yearValidity"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransfer(Code As String, ItemType As Byte, yearValidity As Integer, audit As AuditMessage) As ActionResult(Of BudgetTransfer) Implements IBudgetTransferAdminService.GetBudgetTransfer
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BudgetTransfer As BudgetTransfer = Me._budgetTransferRepository.GetBudgetTransfer(Code.Trim(), ItemType, yearValidity)
            If BudgetTransfer IsNot Nothing AndAlso BudgetTransfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetTransfer)(BudgetTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BudgetTransfer) With {.StateResult = True, .ObjectEmbbeded = BudgetTransfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransferById(Id As Integer, audit As AuditMessage) As ActionResult(Of BudgetTransfer) Implements IBudgetTransferAdminService.GetBudgetTransferById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BudgetTransfer As BudgetTransfer = Me._budgetTransferRepository.GetBudgetTransferById(Id)
            If BudgetTransfer IsNot Nothing AndAlso BudgetTransfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetTransfer)(BudgetTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BudgetTransfer) With {.StateResult = True, .ObjectEmbbeded = BudgetTransfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado
    ''' </summary>
    ''' <param name="budgetTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBudgetTransfer(budgetTransfer As BudgetTransfer, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BudgetTransfer) Implements IBudgetTransferAdminService.SaveBudgetTransfer
        If budgetTransfer Is Nothing Then
            Throw New ArgumentNullException("budgetTransfer")
        End If
        Dim unitOfWork As IUnitWork = Me._budgetTransferRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Dim unitOfWorkBudgetHeader As IUnitWork = Me._budgetHeaderRepository.UnitWork
        Dim budgetUnitOfWork As IUnitWork = Me._budgetRepository.UnitWork
        Dim unitOfWorkControlDocument As IUnitWork = Me._BudgetControlRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As BudgetSequenceDetail = Nothing
                If budgetTransfer.Code Is Nothing OrElse budgetTransfer.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            budgetTransfer.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                'valido que el mes y el año coincidan con el de la vigencia
                Dim resultValidateMonth = _budgetService.ValidateBudgetPeriod(budgetTransfer.BudgetaryValidityId, budgetTransfer.DocumentDate, budgetTransfer.DocumentSource)
                If resultValidateMonth.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateMonth.Message}
                End If

                'Valido los datos del presupuesto modificaciones
                Dim resultValidateBudgetTransfer = _budgetService.ValidateBudgetTransfer(budgetTransfer)
                If resultValidateBudgetTransfer.Length > 0 Then
                    Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateBudgetTransfer}
                End If

                For Each item In budgetTransfer.BudgetTransferDetail.ToList.FindAll(Function(x) x.BudgetId = 0)
                    item.Budget.CreationUser = audit.CodeUser
                    item.Budget.CreationDate = DateTime.Now
                Next

                Dim auxBudget As BudgetTransfer = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BudgetTransfer)
                Dim status As Integer

                If budgetTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    budgetTransfer.CreationUser = audit.CodeUser
                    budgetTransfer.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf budgetTransfer.ChangeTracker.State = ObjectState.Modified Then
                    auxBudget = budgetTransfer.OriginalValue
                    Select Case budgetTransfer.Status
                        Case 1 'Actualizar
                            budgetTransfer.ModificationUser = audit.CodeUser
                            budgetTransfer.ModificationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                        Case 2
                            budgetTransfer.ModificationUser = audit.CodeUser
                            budgetTransfer.ModificationDate = DateTime.Now
                            budgetTransfer.ConfirmationUser = audit.CodeUser
                            budgetTransfer.ConfirmationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                        Case 3
                            budgetTransfer.ModificationUser = audit.CodeUser
                            budgetTransfer.ModificationDate = DateTime.Now
                            budgetTransfer.AnnulmentUser = audit.CodeUser
                            budgetTransfer.AnnulmentDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End Select
                End If

                'Afectamos los items en presupuesto inicial
                If budgetTransfer.Status = 2 Then
                    For Each item In budgetTransfer.BudgetTransferDetail.ToList
                        If item.BudgetId <> 0 Then
                            Dim bt As Domain.Entities.Budget = _budgetRepository.GetBudgetById(item.BudgetId)
                            bt.StartTracking()
                            If item.Nature = 1 Then
                                bt.DebitValueModification = bt.DebitValueModification + item.Value
                            Else
                                bt.CreditValueModification = bt.CreditValueModification + item.Value
                            End If
                            bt.TotalBudget = bt.InitialValue - bt.DebitValueModification + bt.CreditValueModification - bt.DebitValueTransfer + bt.CreditValueTransfer
                            bt.Balance = bt.TotalBudget - bt.ExecutedValue - bt.SuspendedValue
                            bt.ModificationUser = audit.CodeUser
                            bt.ModificationDate = DateTime.Now
                            _budgetRepository.SaveEntity(bt)
                            budgetUnitOfWork.Commit()
                            budgetUnitOfWork.Detach(bt)
                        Else
                            If item.ChangeTracker.State <> ObjectState.Deleted Then
                                If item.Nature = 1 Then
                                    item.Budget.DebitValueTransfer = item.Budget.DebitValueTransfer + item.Value
                                Else
                                    item.Budget.CreditValueTransfer = item.Budget.CreditValueTransfer + item.Value
                                End If
                                item.Budget.TotalBudget = item.Budget.InitialValue - item.Budget.DebitValueModification + item.Budget.CreditValueModification - item.Budget.DebitValueTransfer + item.Budget.CreditValueTransfer
                                item.Budget.Balance = item.Budget.TotalBudget - item.Budget.ExecutedValue - item.Budget.SuspendedValue
                                item.Budget.ModificationUser = audit.CodeUser
                                item.Budget.ModificationDate = DateTime.Now
                            End If
                        End If

                        
                    Next
                    budgetTransfer.ModificationUser = audit.CodeUser
                    budgetTransfer.ModificationDate = DateTime.Now
                    budgetTransfer.ConfirmationUser = audit.CodeUser
                    budgetTransfer.ConfirmationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                End If

                'registramos Archivo de Control
                Dim DocumentType As Integer = 0
                If budgetTransfer.DocumentSource = 1 Then
                    DocumentType = 2
                Else
                    DocumentType = 32
                End If
                If budgetTransfer.Status = 1 Then
                    If budgetTransfer.ChangeTracker.State = ObjectState.Added Then

                        Dim pc = _BudgetControlRepository.GetBudgetControl(budgetTransfer.Code, DocumentType)
                        If pc.Id = 0 Then
                            Dim BudgetControlDocument As BudgetControl = New BudgetControl()
                            BudgetControlDocument.DocumentNumber = budgetTransfer.Code
                            BudgetControlDocument.DocumentType = DocumentType
                            BudgetControlDocument.DocumentUser = audit.CodeUser
                            BudgetControlDocument.DocumentDate = budgetTransfer.CreationDate
                            _BudgetControlRepository.SaveEntity(BudgetControlDocument)
                        End If
                    End If
                ElseIf (budgetTransfer.Status = 2 AndAlso budgetTransfer.Id > 0) OrElse budgetTransfer.Status = 3 Then
                    Dim BudgetControl = _BudgetControlRepository.GetBudgetControl(budgetTransfer.Code, DocumentType)
                    If BudgetControl.Id > 0 Then
                        BudgetControl.MarkAsDeleted()
                        _BudgetControlRepository.DeleteEntity(BudgetControl)
                    End If
                End If

                Me._budgetTransferRepository.SaveEntity(budgetTransfer)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BudgetTransfer)(budgetTransfer, audit, status, auxBudget)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                budgetTransfer.MarkAsUnchanged()

                Transaction.Complete()
                Return New ActionResult(Of BudgetTransfer) With {.StateResult = True, .ObjectEmbbeded = budgetTransfer}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitOfWorkBudgetHeader.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitOfWorkBudgetHeader.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BudgetTransfer) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _budgetService.Dispose()
            End If
            _budgetTransferRepository = Nothing
            _secuenseDRepository = Nothing
            _budgetHeaderRepository = Nothing
            _budgetService = Nothing
            _budgetRepository = Nothing
            _BudgetControlRepository = Nothing
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
