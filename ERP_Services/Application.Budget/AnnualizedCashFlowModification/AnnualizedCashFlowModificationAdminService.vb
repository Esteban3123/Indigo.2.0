'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
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
Imports System.Text
Imports Domain.Entities.Service

#End Region

Public Class AnnualizedCashFlowModificationAdminService
    Implements IAnnualizedCashFlowModificationAdminService


    Private _annualizedCashFlowRepository As IAnnualizedCashFlowRepository
    Private _annualizedCashFlowModificationRepository As IAnnualizedCashFlowModificationRepository
    Private _budgetService As IBudgetService
    Private _categoryRepository As IBudgetItemRepository
    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository

    Public Sub New(annualizedCashFlowModificationRepository As IAnnualizedCashFlowModificationRepository, secuenseDRepository As ISequenseBudgetDRepository, annualizedCashFlowRepository As IAnnualizedCashFlowRepository,
                   budgetService As IBudgetService, categoryRepository As IBudgetItemRepository, budgetControlRepository As IBudgetControlRepository)
        If annualizedCashFlowModificationRepository Is Nothing Then
            Throw New ArgumentNullException("annualizedCashFlowModificationRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _annualizedCashFlowModificationRepository = annualizedCashFlowModificationRepository
        _secuenseDRepository = secuenseDRepository
        _annualizedCashFlowRepository = annualizedCashFlowRepository
        _budgetService = budgetService
        _categoryRepository = categoryRepository
        _BudgetControlRepository = budgetControlRepository
    End Sub

    ''' <summary>
    ''' obtiene una modificacion del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationByCode(code As String, type As Integer, audit As AuditMessage) As AnnualizedCashFlowModification Implements IAnnualizedCashFlowModificationAdminService.GetAnnualizedCashFlowModificationByCode
        Try
            Dim annualizedCashFlowModification = _annualizedCashFlowModificationRepository.GetAnnualizedCashFlowModificationByCode(code, type)
            If annualizedCashFlowModification IsNot Nothing AndAlso annualizedCashFlowModification.Id > 0 Then
                Dim auditobject As New IndigoAuditSimpleEntity(Of AnnualizedCashFlowModification)(annualizedCashFlowModification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditobject.Execute()
            End If
            Return annualizedCashFlowModification
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AnnualizedCashFlowModification
        End Try
    End Function

    ''' <summary>
    ''' obtiene una modificacion del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationById(id As Integer) As AnnualizedCashFlowModification Implements IAnnualizedCashFlowModificationAdminService.GetAnnualizedCashFlowModificationById
        Try
            Return _annualizedCashFlowModificationRepository.GetAnnualizedCashFlowModificationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AnnualizedCashFlowModification
        End Try
    End Function

    Public Function SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification As AnnualizedCashFlowModification, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AnnualizedCashFlowModification) Implements IAnnualizedCashFlowModificationAdminService.SaveAnnualizedCashFlowModification
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Dim annualizedCashFlowModificationUnitOfWork = _annualizedCashFlowModificationRepository.UnitWork
        Dim annualizedCashFlowUnitOfWork = _annualizedCashFlowRepository.UnitWork
        Dim unitOfWorkControlDocument As IUnitWork = Me._BudgetControlRepository.UnitWork
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As BudgetSequenceDetail = Nothing
                If AnnualizedCashFlowModification.Code Is Nothing OrElse AnnualizedCashFlowModification.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AnnualizedCashFlowModification.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Transaction.Dispose()
                            Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        Transaction.Dispose()
                        Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = False, .StateResultAux = True, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                'valido que el mes y el año coincidan con el de la vigencia
                Dim resultValidateMonth = _budgetService.ValidateBudgetPeriod(AnnualizedCashFlowModification.BudgetaryValidityId, AnnualizedCashFlowModification.DocumentDate, AnnualizedCashFlowModification.DocumentSource)
                If resultValidateMonth.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateMonth.Message}
                End If

                'Valido los datos del presupuesto modificaciones
                Dim resultValidateAnnualizedCashFlowModification = _budgetService.ValidatePACModification(AnnualizedCashFlowModification)
                If resultValidateAnnualizedCashFlowModification.Length > 0 Then
                    Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateAnnualizedCashFlowModification}
                End If

                Dim auxAnnualizedCashFlowModification As AnnualizedCashFlowModification = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AnnualizedCashFlowModification)
                Dim status As Integer

                If AnnualizedCashFlowModification.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AnnualizedCashFlowModification.CreationUser = audit.CodeUser
                    AnnualizedCashFlowModification.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf AnnualizedCashFlowModification.ChangeTracker.State = ObjectState.Modified Then
                    auxAnnualizedCashFlowModification = AnnualizedCashFlowModification.OriginalValue
                    Select Case AnnualizedCashFlowModification.Status
                        Case 1 'Actualizar
                            AnnualizedCashFlowModification.ModificationUser = audit.CodeUser
                            AnnualizedCashFlowModification.ModificationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                        Case 2 'confirmar 
                            AnnualizedCashFlowModification.ModificationUser = audit.CodeUser
                            AnnualizedCashFlowModification.ModificationDate = DateTime.Now
                            AnnualizedCashFlowModification.ConfirmationUser = audit.CodeUser
                            AnnualizedCashFlowModification.ConfirmationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                        Case 3 'anular
                            AnnualizedCashFlowModification.ModificationUser = audit.CodeUser
                            AnnualizedCashFlowModification.ModificationDate = DateTime.Now
                            AnnualizedCashFlowModification.AnnulmentUser = audit.CodeUser
                            AnnualizedCashFlowModification.AnnulmentDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End Select
                End If

                If AnnualizedCashFlowModification.Status = 2 Then 'afectamos el PAC cuando se confirme
                    Dim errorConfirm As New StringBuilder
                    For Each item In AnnualizedCashFlowModification.AnnualizedCashFlowModificationDetail
                        If item.ChangeTracker.State <> ObjectState.Deleted Then
                            Dim pac = _annualizedCashFlowRepository.GetAnnualizedCashFlowById(item.AnnualizedCashFlowId)
                            Dim category = _categoryRepository.GetCategoryById(pac.CategoryId)
                            Dim month As String = DateAndTime.MonthName(CInt(pac.Month))
                            If item.Nature = 0 Then
                                errorConfirm.AppendLine("El PAC con rubro " + category.Code + " - " + category.Name + " en el mes de " + month + " la naturaleza no puede ser ninguna")
                            End If
                            If item.Nature = 1 Then
                                If pac.Balance < item.Value Then
                                    errorConfirm.AppendLine("El PAC con rubro " + category.Code + " - " + category.Name + " en el mes de " + month + " tiene un saldo menor al valor de la modificación")
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

                    If errorConfirm.Length > 0 Then
                        Transaction.Dispose()
                        Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = False, .Message = errorConfirm.ToString()}
                    End If
                    AnnualizedCashFlowModification.ModificationUser = audit.CodeUser
                    AnnualizedCashFlowModification.ModificationDate = DateTime.Now
                    AnnualizedCashFlowModification.ConfirmationUser = audit.CodeUser
                    AnnualizedCashFlowModification.ConfirmationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                End If

                'registramos Archivo de Control
                Dim DocumentType As Integer = 0
                If AnnualizedCashFlowModification.DocumentSource = 1 Then
                    DocumentType = 3
                Else
                    DocumentType = 33
                End If
                If AnnualizedCashFlowModification.Status = 1 Then
                    If AnnualizedCashFlowModification.ChangeTracker.State = ObjectState.Added Then

                        Dim pc = _BudgetControlRepository.GetBudgetControl(AnnualizedCashFlowModification.Code, DocumentType)
                        If pc.Id = 0 Then
                            Dim BudgetControlDocument As BudgetControl = New BudgetControl()
                            BudgetControlDocument.DocumentNumber = AnnualizedCashFlowModification.Code
                            BudgetControlDocument.DocumentType = DocumentType
                            BudgetControlDocument.DocumentUser = audit.CodeUser
                            BudgetControlDocument.DocumentDate = AnnualizedCashFlowModification.CreationDate
                            _BudgetControlRepository.SaveEntity(BudgetControlDocument)
                        End If
                    End If
                ElseIf (AnnualizedCashFlowModification.Status = 2 AndAlso AnnualizedCashFlowModification.Id > 0) OrElse AnnualizedCashFlowModification.Status = 3 Then
                    Dim BudgetControl = _BudgetControlRepository.GetBudgetControl(AnnualizedCashFlowModification.Code, DocumentType)
                    If BudgetControl.Id > 0 Then
                        BudgetControl.MarkAsDeleted()
                        _BudgetControlRepository.DeleteEntity(BudgetControl)
                    End If
                End If

                _annualizedCashFlowModificationRepository.SaveEntity(AnnualizedCashFlowModification)
                annualizedCashFlowModificationUnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                annualizedCashFlowUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AnnualizedCashFlowModification)(AnnualizedCashFlowModification, audit, status, auxAnnualizedCashFlowModification)
                auditProcess.Execute()
                Transaction.Complete()
                Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = True, .ObjectEmbbeded = AnnualizedCashFlowModification}

            Catch ex As OptimisticConcurrencyException
                annualizedCashFlowModificationUnitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                annualizedCashFlowModificationUnitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AnnualizedCashFlowModification) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _annualizedCashFlowModificationRepository = Nothing
            _secuenseDRepository = Nothing
            _annualizedCashFlowRepository = Nothing
            _budgetService = Nothing
            _categoryRepository = Nothing
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
