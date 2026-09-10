'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 05-09-2015
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

Public Class CommitmentModificationAdminService
    Implements ICommitmentModificationAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _commitmentModificationRepository As ICommitmentModificationRepository

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
    ''' repositorio de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    ''' <summary>
    ''' repositorio de disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityDetailRepository As IAvailabilityDetailRepository

    Private _categoryRepository As IBudgetItemRepository

    Private _revenueTypeRepository As IExpenseTypeRepository

    Private _commitmentDetailRepository As ICommitmentDetailRepository

#End Region

#Region "Builder"

    Public Sub New(commitmentModificationRepository As ICommitmentModificationRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, budgetRepository As IBudgetRepository,
                   availabilityDetailRepository As IAvailabilityDetailRepository, categoryRepository As IBudgetItemRepository, revenueTypeRepository As IExpenseTypeRepository,
                   commitmentDetailRepository As ICommitmentDetailRepository)
        If commitmentModificationRepository Is Nothing Then
            Throw New ArgumentNullException("commitmentModificationRepository")
        End If
        _commitmentModificationRepository = commitmentModificationRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _budgetRepository = budgetRepository
        _availabilityDetailRepository = availabilityDetailRepository
        _categoryRepository = categoryRepository
        _revenueTypeRepository = revenueTypeRepository
        _commitmentDetailRepository = commitmentDetailRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una modificacion de compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As CommitmentModification Implements ICommitmentModificationAdminService.GetCommitmentModificationByCode
        Try
            Dim commitmentModification = _commitmentModificationRepository.GetCommitmentModificationByCode(code, BudgetaryValidityId)
            If commitmentModification IsNot Nothing AndAlso commitmentModification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CommitmentModification)(commitmentModification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return commitmentModification
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CommitmentModification
        End Try
    End Function

    ''' <summary>
    ''' obtiene una modificacion de compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommitmentModificationById(id As Integer) As CommitmentModification Implements ICommitmentModificationAdminService.GetCommitmentModificationById
        Try
            Return _commitmentModificationRepository.GetCommitmentModificationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CommitmentModification
        End Try
    End Function

    ''' <summary>
    ''' Guarda una modificacion de compromiso
    ''' </summary>
    ''' <param name="commitmentModification"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCommitmentModification(CommitmentModification As CommitmentModification, listCommitmentModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of CommitmentModification) Implements ICommitmentModificationAdminService.SaveCommitmentModification
        If CommitmentModification Is Nothing Then
            Throw New ArgumentNullException("CommitmentModification")
        End If

        Dim unitOfWork As IUnitWork = Me._commitmentModificationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlCommitmentModification(CommitmentModification)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listCommitmentModificationDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(CommitmentModification.Id, CommitmentModification.Status)

                Dim resultStore = Me._commitmentModificationRepository.SP_SaveCommitmentModification(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of CommitmentModification) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                CommitmentModification.Id = resultStore.Id
                CommitmentModification.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of CommitmentModification)(CommitmentModification, audit, auditStatus, CommitmentModification.OriginalValue)
                auditProcess.Execute()

                CommitmentModification.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of CommitmentModification) With {.StateResult = True, .ObjectEmbbeded = CommitmentModification, .Message = resultStore.MessageResult, .MessageAux = resultStore.AuxiliaryResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of CommitmentModification) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CommitmentModification) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlCommitmentModification(CommitmentModification As CommitmentModification) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<CommitmentModification>")

        builder.Append("<Id>" & CommitmentModification.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & CommitmentModification.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & CommitmentModification.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & CommitmentModification.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & CommitmentModification.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<CommitmentId>" & CommitmentModification.CommitmentId & "</CommitmentId>")
        builder.Append("<UpTo>" & CommitmentModification.UpTo & "</UpTo>")
        builder.Append("<Document>" & CommitmentModification.Document & "</Document>")
        builder.Append("<Observations>" & CommitmentModification.Observations & "</Observations>")
        builder.Append("<Status>" & CommitmentModification.Status & "</Status>")
        If CommitmentModification.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & CommitmentModification.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & CommitmentModification.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & CommitmentModification.EntityName & "</EntityName>")
        End If

        For Each detail In CommitmentModification.CommitmentModificationDetail
            builder.Append("<CommitmentModificationDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<CommitmentDetailId>" & detail.CommitmentDetailId & "</CommitmentDetailId>")
            builder.Append("<DateExpired>" & detail.DateExpired.ToString("dd/MM/yyyy HH:mm:ss") & "</DateExpired>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<Nature>" & detail.Nature & "</Nature>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")
            builder.Append("<IsLogBase>" & detail.IsLogBase & "</IsLogBase>")
            If detail.AvailabilityDetailId IsNot Nothing Then
                builder.Append("<AvailabilityDetailId>" & detail.AvailabilityDetailId & "</AvailabilityDetailId>")
            End If

            builder.Append("</CommitmentModificationDetail>")
        Next

        builder.Append("</CommitmentModification>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listCommitmentModificationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listCommitmentModificationDetailDelete IsNot Nothing AndAlso listCommitmentModificationDetailDelete.Count > 0 Then
            For Each detail In listCommitmentModificationDetailDelete
                builder.Append("<CommitmentModificationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</CommitmentModificationDetail>")
            Next
        End If

        Return builder.ToString()
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
            _commitmentModificationRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _budgetRepository = Nothing
            _availabilityDetailRepository = Nothing
            _categoryRepository = Nothing
            _revenueTypeRepository = Nothing
            _commitmentDetailRepository = Nothing
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
