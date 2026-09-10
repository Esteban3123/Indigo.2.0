'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 27-08-2015
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
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

#End Region

Public Class AnnualizedCashFlowTransferAdminService
    Implements IAnnualizedCashFlowTransferAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _annualizedCashFlowTransferRepository As IAnnualizedCashFlowTransferRepository

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

    Private _annualizedCashFlowRepository As IAnnualizedCashFlowRepository

    Private _categoryRepository As IBudgetItemRepository

#End Region

#Region "Builder"

    Public Sub New(annualizedCashFlowTransferRepository As IAnnualizedCashFlowTransferRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, annualizedCashFlowRepository As IAnnualizedCashFlowRepository,
                   categoryRepository As IBudgetItemRepository)
        If annualizedCashFlowTransferRepository Is Nothing Then
            Throw New ArgumentNullException("annualizedCashFlowTransferRepository")
        End If
        _annualizedCashFlowTransferRepository = annualizedCashFlowTransferRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _annualizedCashFlowRepository = annualizedCashFlowRepository
        _categoryRepository = categoryRepository
    End Sub

#End Region

    ''' <summary>
    ''' Obtiene un traslado del pac por codigo 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowTransferByCode(code As String, type As Integer, audit As AuditMessage) As AnnualizedCashFlowTransfer Implements IAnnualizedCashFlowTransferAdminService.GetAnnualizedCashFlowTransferByCode
        Try
            Dim annualizedCashFlowTransfer = _annualizedCashFlowTransferRepository.GetAnnualizedCashFlowTrasnferByCode(code, type)
            If annualizedCashFlowTransfer IsNot Nothing AndAlso annualizedCashFlowTransfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AnnualizedCashFlowTransfer)(annualizedCashFlowTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return annualizedCashFlowTransfer
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AnnualizedCashFlowTransfer
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowTransferById(id As Integer) As AnnualizedCashFlowTransfer Implements IAnnualizedCashFlowTransferAdminService.GetAnnualizedCashFlowTransferById
        Try
            Return _annualizedCashFlowTransferRepository.GetAnnualizedCashFlowTransferById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AnnualizedCashFlowTransfer
        End Try
    End Function

    ''' <summary>
    ''' Guarda un traslado del pac
    ''' </summary>
    ''' <param name="annualizedCashFlowTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAnnualizedCashFlowTransfer(annualizedCashFlowTransfer As AnnualizedCashFlowTransfer, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AnnualizedCashFlowTransfer) Implements IAnnualizedCashFlowTransferAdminService.SaveAnnualizedCashFlowTransfer
        If annualizedCashFlowTransfer Is Nothing Then
            Throw New ArgumentNullException("annualizedCashFlowTransfer")
        End If

        Dim unitOfWork As IUnitWork = Me._annualizedCashFlowTransferRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseBudgetDRepository.UnitWork
        Dim unitOfWorkControlDocument As IUnitWork = Me._BudgetControlRepository.UnitWork
        Dim annualizedCashUnitOfWork = _annualizedCashFlowRepository.UnitWork

        Dim txtSettings = New TransactionOptions()
        txtSettings.Timeout = TransactionManager.MaximumTimeout
        txtSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txtSettings)
            Try
                Dim seq As BudgetSequenceDetail = Nothing
                If annualizedCashFlowTransfer.Code Is Nothing OrElse annualizedCashFlowTransfer.Code.Trim().Equals(String.Empty) Then
                    seq = _sequenseBudgetDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            annualizedCashFlowTransfer.Code = res
                            seq.Next += 1
                            Me._sequenseBudgetDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                'valido que el mes y el año coincidan con el de la vigencia
                Dim resultValidateMonth = _budgetService.ValidateBudgetPeriod(annualizedCashFlowTransfer.BudgetaryValidityId, annualizedCashFlowTransfer.DocumentDate, annualizedCashFlowTransfer.DocumentSource)
                If resultValidateMonth.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateMonth.Message}
                End If

                'Valido los datos del traslado de PAC
                Dim resultannualizedCashFlowTransfer = _budgetService.ValidatePACTransfer(annualizedCashFlowTransfer)
                If resultannualizedCashFlowTransfer.Length > 0 Then
                    Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = False, .StateResultAux = True, .Message = resultannualizedCashFlowTransfer}
                End If

                Dim auxAnnualizedCashFlowTransfer As Domain.Entities.AnnualizedCashFlowTransfer = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AnnualizedCashFlowTransfer)
                Dim status As Integer

                If annualizedCashFlowTransfer.ChangeTracker.State = ObjectState.Added Then
                    annualizedCashFlowTransfer.CreationUser = audit.CodeUser
                    annualizedCashFlowTransfer.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf annualizedCashFlowTransfer.ChangeTracker.State = ObjectState.Modified Then
                    auxAnnualizedCashFlowTransfer = _annualizedCashFlowTransferRepository.GetAnnualizedCashFlowTransferById(annualizedCashFlowTransfer.Id)
                    If annualizedCashFlowTransfer.Status = 1 OrElse annualizedCashFlowTransfer.Status = 2 Then
                        annualizedCashFlowTransfer.ModificationUser = audit.CodeUser
                        annualizedCashFlowTransfer.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    ElseIf annualizedCashFlowTransfer.Status = 3 Then
                        annualizedCashFlowTransfer.ModificationUser = audit.CodeUser
                        annualizedCashFlowTransfer.ModificationDate = DateTime.Now
                        annualizedCashFlowTransfer.AnnulmentUser = audit.CodeUser
                        annualizedCashFlowTransfer.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                'Afectamos los items en pac inicial
                If annualizedCashFlowTransfer.Status = 2 Then
                    Dim errorConfirm As New StringBuilder
                    For Each item In annualizedCashFlowTransfer.AnnualizedCashFlowTransferDetail.ToList
                        If item.ChangeTracker.State <> ObjectState.Deleted Then
                            Dim pac = _annualizedCashFlowRepository.GetAnnualizedCashFlowById(item.AnnualizedCashFlowId)
                            Dim category = _categoryRepository.GetCategoryById(pac.CategoryId)
                            Dim month As String = DateAndTime.MonthName(CInt(pac.Month))
                            If item.Nature = 0 Then
                                errorConfirm.AppendLine("El PAC con rubro " + category.Code + " - " + category.Name + " en el mes de " + month + " la naturaleza no puede ser ninguna")
                            End If
                            If item.Nature = 1 Then
                                If pac.Balance < item.Value Then
                                    errorConfirm.AppendLine("El PAC con rubro " + category.Code + " - " + category.Name + " en el mes de " + month + " tiene un saldo menor al valor del traslado")
                                    Continue For
                                End If
                                pac.DebitModificationValue += item.Value
                            Else
                                pac.CreditModificationValue += item.Value
                            End If
                            pac.TotalScheduled = pac.InitialValue - pac.DebitModificationValue + pac.CreditModificationValue - pac.DebitTransferValue + pac.CreditTransferValue
                            pac.Balance = pac.TotalScheduled - pac.ExecutedValue
                            pac.MarkAsModified()
                            pac.ModificationDate = DateTime.Now
                            pac.ModificationUser = audit.CodeUser
                            _annualizedCashFlowRepository.SaveEntity(pac)
                        End If
                    Next
                    annualizedCashFlowTransfer.ConfirmationUser = audit.CodeUser
                    annualizedCashFlowTransfer.ConfirmationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                End If

                'registramos Archivo de Control
                Dim DocumentType As Integer = 0
                If annualizedCashFlowTransfer.DocumentSource = 1 Then
                    DocumentType = 4
                Else
                    DocumentType = 34
                End If
                If annualizedCashFlowTransfer.Status = 1 Then
                    If annualizedCashFlowTransfer.ChangeTracker.State = ObjectState.Added Then
                        Dim pc = _BudgetControlRepository.GetBudgetControl(annualizedCashFlowTransfer.Code, DocumentType)
                        If pc.Id = 0 Then
                            Dim BudgetControlDocument As BudgetControl = New BudgetControl()
                            BudgetControlDocument.DocumentNumber = annualizedCashFlowTransfer.Code
                            BudgetControlDocument.DocumentType = DocumentType
                            BudgetControlDocument.DocumentUser = audit.CodeUser
                            BudgetControlDocument.DocumentDate = annualizedCashFlowTransfer.CreationDate
                            _BudgetControlRepository.SaveEntity(BudgetControlDocument)
                        End If
                    End If
                ElseIf (annualizedCashFlowTransfer.Status = 2 AndAlso annualizedCashFlowTransfer.Id > 0) OrElse annualizedCashFlowTransfer.Status = 3 Then
                    Dim BudgetControl = _BudgetControlRepository.GetBudgetControl(annualizedCashFlowTransfer.Code, DocumentType)
                    If BudgetControl.Id > 0 Then
                        BudgetControl.MarkAsDeleted()
                        _BudgetControlRepository.DeleteEntity(BudgetControl)
                    End If
                End If

                _annualizedCashFlowTransferRepository.SaveEntity(annualizedCashFlowTransfer)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                unitOfWorkControlDocument.Commit()
                annualizedCashUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AnnualizedCashFlowTransfer)(annualizedCashFlowTransfer, audit, status, auxAnnualizedCashFlowTransfer)
                auditProcess.Execute()
                Transaction.Complete()
                Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = True, .ObjectEmbbeded = annualizedCashFlowTransfer}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As InvalidOperationException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AnnualizedCashFlowTransfer) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _annualizedCashFlowTransferRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _annualizedCashFlowRepository = Nothing
            _categoryRepository = Nothing
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
