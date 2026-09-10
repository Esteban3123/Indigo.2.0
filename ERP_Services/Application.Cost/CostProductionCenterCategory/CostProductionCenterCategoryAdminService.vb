#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
#End Region

Public Class CostProductionCenterCategoryAdminService
    Implements ICostProductionCenterCategoryAdminService

#Region "Fields"

    'Repositories
    Private _sequenceRepository As ICostSequenceDetailRepository
    Private _CostProductionCenterCategoryRepository As ICostProductionCenterCategoryRepository

    'Nombre del formulario
    Private Const FORM_NAME As String = "FrmCostProductionCenterCategory"

#End Region

#Region "Builder"

    Public Sub New(sequenceRepository As ICostSequenceDetailRepository, CostProductionCenterCategoryRepository As ICostProductionCenterCategoryRepository)
        _sequenceRepository = sequenceRepository
        _CostProductionCenterCategoryRepository = CostProductionCenterCategoryRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostProductionCenterCategory(Code As String) As CostProductionCenterCategory Implements ICostProductionCenterCategoryAdminService.GetCostProductionCenterCategory
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo del indicio vacio")
        End If
        Try
            Return _CostProductionCenterCategoryRepository.GetCostProductionCenterCategoryByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New CostProductionCenterCategory()
        End Try
    End Function

    Public Function ListAllCostProductionCenterCategory() As List(Of CostProductionCenterCategory) Implements ICostProductionCenterCategoryAdminService.ListAllCostProductionCenterCategory
        Try
            Return _CostProductionCenterCategoryRepository.ListAllCostProductionCenterCategory()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveCostProductionCenterCategory(CostProductionCenterCategory As CostProductionCenterCategory, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CostProductionCenterCategory) Implements ICostProductionCenterCategoryAdminService.SaveCostProductionCenterCategory
        If CostProductionCenterCategory Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._CostProductionCenterCategoryRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(CostProductionCenterCategory.Code) Then
                    Dim seq As CostSecuenceDetail = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            CostProductionCenterCategory.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of CostProductionCenterCategory) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), CostProductionCenterCategory.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of CostProductionCenterCategory) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As CostProductionCenterCategory = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CostProductionCenterCategory)
                Dim status As Integer

                If CostProductionCenterCategory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CostProductionCenterCategory.CreationUser = audit.CodeUser
                    CostProductionCenterCategory.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = CostProductionCenterCategory.OriginalValue
                    CostProductionCenterCategory.ModificationUser = audit.CodeUser
                    CostProductionCenterCategory.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._CostProductionCenterCategoryRepository.SaveEntity(CostProductionCenterCategory)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CostProductionCenterCategory)(CostProductionCenterCategory, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                CostProductionCenterCategory.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of CostProductionCenterCategory) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = CostProductionCenterCategory, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostProductionCenterCategory) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostProductionCenterCategory) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function CostProductionCenterCategoryChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of CostProductionCenterCategory) Implements ICostProductionCenterCategoryAdminService.CostProductionCenterCategoryChangeState
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
            Dim ObjCostProductionCenterCategory As CostProductionCenterCategory = Me._CostProductionCenterCategoryRepository.GetCostProductionCenterCategoryByCode(code.Trim())
            If ObjCostProductionCenterCategory IsNot Nothing AndAlso ObjCostProductionCenterCategory.Id > 0 Then
                ObjCostProductionCenterCategory.Status = state
            End If
            Dim result = Me.SaveCostProductionCenterCategory(ObjCostProductionCenterCategory, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostProductionCenterCategory) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function DeleteCostProductionCenterCategory(CostProductionCenterCategory As CostProductionCenterCategory, audit As AuditMessage) As ActionResult Implements ICostProductionCenterCategoryAdminService.DeleteCostProductionCenterCategory
        If CostProductionCenterCategory Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._CostProductionCenterCategoryRepository.UnitWork
        Try
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                CostProductionCenterCategory.ModificationUser = audit.CodeUser
                CostProductionCenterCategory.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostProductionCenterCategory)(CostProductionCenterCategory, audit, status)

                CostProductionCenterCategory.MarkAsDeleted()
                Me._CostProductionCenterCategoryRepository.SaveEntity(CostProductionCenterCategory)
                unitOfWork.Commit()
                auditProcess.Execute()
                transaction.Complete()
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

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _CostProductionCenterCategoryRepository = Nothing
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
