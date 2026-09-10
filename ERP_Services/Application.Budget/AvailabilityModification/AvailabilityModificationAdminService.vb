'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 01-09-2015
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

Public Class AvailabilityModificationAdminService
    Implements IAvailabilityModificationAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityModificationRepository As IAvailabilityModificationRepository

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

    ''' <summary>
    ''' Variable tipo repositorio para presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    Private _budgetService As IBudgetService

    Private _categoryRepository As IBudgetItemRepository

    Private _revenueTypeRepository As IExpenseTypeRepository

    Private _availabilityDetailRepository As IAvailabilityDetailRepository

#End Region

#Region "Builder"

    Public Sub New(availabilityModificationRepository As IAvailabilityModificationRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, categoryRepository As IBudgetItemRepository,
                   revenueTypeRepository As IExpenseTypeRepository, availabilityDetailRepository As IAvailabilityDetailRepository, budgetRepository As IBudgetRepository)
        If availabilityModificationRepository Is Nothing Then
            Throw New ArgumentNullException("availabilityModificationRepository")
        End If
        If budgetRepository Is Nothing Then
            Throw New ArgumentNullException("budgetRepository vacío")
        End If
        _availabilityModificationRepository = availabilityModificationRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _categoryRepository = categoryRepository
        _budgetRepository = budgetRepository
        _revenueTypeRepository = revenueTypeRepository
        _availabilityDetailRepository = availabilityDetailRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As AvailabilityModification Implements IAvailabilityModificationAdminService.GetAvailabilityModificationByCode
        Try
            Dim availabilityModification = _availabilityModificationRepository.GetAvailabilityModificationByCode(code, BudgetaryValidityId)
            If availabilityModification IsNot Nothing AndAlso availabilityModification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AvailabilityModification)(availabilityModification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return availabilityModification
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AvailabilityModification
        End Try
    End Function

    ''' <summary>
    ''' obtiene una modificacion de disponibilidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityModificationById(id As Integer) As AvailabilityModification Implements IAvailabilityModificationAdminService.GetAvailabilityModificationById
        Try
            Return _availabilityModificationRepository.GetAvailabilityModificationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AvailabilityModification
        End Try
    End Function

    ''' <summary>
    ''' Guarda una modificacion de disponibilidad
    ''' </summary>
    ''' <param name="availabilityModification"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAvailabilityModification(AvailabilityModification As AvailabilityModification, listAvailabilityModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of AvailabilityModification) Implements IAvailabilityModificationAdminService.SaveAvailabilityModification
        If AvailabilityModification Is Nothing Then
            Throw New ArgumentNullException("AvailabilityModification")
        End If

        Dim unitOfWork As IUnitWork = Me._availabilityModificationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlAvailabilityModification(AvailabilityModification)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listAvailabilityModificationDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(AvailabilityModification.Id, AvailabilityModification.Status)

                Dim resultStore = Me._availabilityModificationRepository.SP_SaveAvailabilityModification(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of AvailabilityModification) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                AvailabilityModification.Id = resultStore.Id
                AvailabilityModification.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of AvailabilityModification)(AvailabilityModification, audit, auditStatus, AvailabilityModification.OriginalValue)
                auditProcess.Execute()

                AvailabilityModification.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of AvailabilityModification) With {.StateResult = True, .ObjectEmbbeded = AvailabilityModification, .Message = resultStore.MessageResult, .MessageAux = resultStore.AuxiliaryResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of AvailabilityModification) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AvailabilityModification) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlAvailabilityModification(AvailabilityModification As AvailabilityModification) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<AvailabilityModification>")

        builder.Append("<Id>" & AvailabilityModification.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & AvailabilityModification.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & AvailabilityModification.Code & "</Code>")
        builder.Append("<BudgetaryValidityId>" & AvailabilityModification.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & AvailabilityModification.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<AvailabilityId>" & AvailabilityModification.AvailabilityId & "</AvailabilityId>")
        builder.Append("<UpTo>" & AvailabilityModification.UpTo & "</UpTo>")
        builder.Append("<Document>" & AvailabilityModification.Document & "</Document>")
        builder.Append("<Observations>" & AvailabilityModification.Observations & "</Observations>")
        builder.Append("<Status>" & AvailabilityModification.Status & "</Status>")
        If AvailabilityModification.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & AvailabilityModification.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & AvailabilityModification.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & AvailabilityModification.EntityName & "</EntityName>")
        End If

        For Each detail In AvailabilityModification.AvailabilityModificationDetail
            builder.Append("<AvailabilityModificationDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<AvailabilityDetailId>" & detail.AvailabilityDetailId & "</AvailabilityDetailId>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<CPCCodeId>" & detail.CPCCodeId & "</CPCCodeId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<Nature>" & detail.Nature & "</Nature>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")

            builder.Append("</AvailabilityModificationDetail>")
        Next

        builder.Append("</AvailabilityModification>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listAvailabilityModificationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listAvailabilityModificationDetailDelete IsNot Nothing AndAlso listAvailabilityModificationDetailDelete.Count > 0 Then
            For Each detail In listAvailabilityModificationDetailDelete
                builder.Append("<AvailabilityModificationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</AvailabilityModificationDetail>")
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
            _availabilityModificationRepository = Nothing
            _BudgetControlRepository = Nothing
            _sequenseBudgetDRepository = Nothing
            _budgetService = Nothing
            _categoryRepository = Nothing
            _budgetRepository = Nothing
            _revenueTypeRepository = Nothing
            _availabilityDetailRepository = Nothing
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
