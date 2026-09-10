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

Public Class CollectionModificationAdminService
    Implements ICollectionModificationAdminService

    Private _collectionModificationRepository As ICollectionModificationRepository

    Private _collectionDetailRepository As ICollectionDetailRepository

    Private _recognitionDetailRepository As IRecognitionDetailRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository

    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

    Private _budgetService As IBudgetService

    Public Sub New(collectionModificationRepository As ICollectionModificationRepository, collectionDetailRepository As ICollectionDetailRepository, secuenseDRepository As ISequenseBudgetDRepository,
                   recognitionDetailRepository As IRecognitionDetailRepository, budgetService As IBudgetService, budgetControlRepository As IBudgetControlRepository)
        _collectionModificationRepository = collectionModificationRepository
        _collectionDetailRepository = collectionDetailRepository
        _secuenseDRepository = secuenseDRepository
        _recognitionDetailRepository = recognitionDetailRepository
        _budgetService = budgetService
        _BudgetControlRepository = budgetControlRepository
    End Sub

    ''' <summary>
    ''' obtiene una modificacion del recaudo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetCollectionModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As CollectionModification Implements ICollectionModificationAdminService.GetCollectionModificationByCode
        Try
            Dim CollectionModification = _collectionModificationRepository.GetCollectionModificationByCode(code, BudgetaryValidityId)
            If CollectionModification IsNot Nothing AndAlso CollectionModification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CollectionModification)(CollectionModification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return CollectionModification
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CollectionModification
        End Try
    End Function

    Public Function GetCollectionModificationById(id As Integer) As CollectionModification Implements ICollectionModificationAdminService.GetCollectionModificationById
        Try
            Return _collectionModificationRepository.GetCollectionModificationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CollectionModification
        End Try
    End Function

    Public Function SaveCollectionModification(CollectionModification As CollectionModification, listModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of CollectionModification) Implements ICollectionModificationAdminService.SaveCollectionModification
        If CollectionModification Is Nothing Then
            Throw New ArgumentNullException("CollectionModification")
        End If

        Dim unitOfWork As IUnitWork = Me._collectionModificationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlCollectionModification(CollectionModification)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listModificationDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(CollectionModification.Id, CollectionModification.Status)

                Dim resultStore = Me._collectionModificationRepository.SP_SaveCollectionModification(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of CollectionModification) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                CollectionModification.Id = resultStore.Id
                CollectionModification.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of CollectionModification)(CollectionModification, audit, auditStatus, CollectionModification.OriginalValue)
                auditProcess.Execute()

                CollectionModification.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of CollectionModification) With {.StateResult = True, .ObjectEmbbeded = CollectionModification, .Message = resultStore.MessageResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of CollectionModification) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CollectionModification) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#Region "Private Methods"

    Private Function ConvertToXmlCollectionModification(CollectionModification As CollectionModification) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<CollectionModification>")

        builder.Append("<Id>" & CollectionModification.Id & "</Id>")
        builder.Append("<Code>" & CollectionModification.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & CollectionModification.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<BudgetaryValidityId>" & CollectionModification.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & CollectionModification.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<CollectionId>" & CollectionModification.CollectionId & "</CollectionId>")
        builder.Append("<Document>" & CollectionModification.Document & "</Document>")
        builder.Append("<Observations>" & CollectionModification.Observations & "</Observations>")
        builder.Append("<Status>" & CollectionModification.Status & "</Status>")
        If CollectionModification.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & CollectionModification.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & CollectionModification.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & CollectionModification.EntityName & "</EntityName>")
        End If

        For Each detail In CollectionModification.CollectionModificationDetail
            builder.Append("<CollectionModificationDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<CollectionModificationId>" & detail.CollectionModificationId & "</CollectionModificationId>")
            builder.Append("<CollectionDetailId>" & detail.CollectionDetailId & "</CollectionDetailId>")
            builder.Append("<Nature>" & detail.Nature & "</Nature>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")

            builder.Append("</CollectionModificationDetail>")
        Next

        builder.Append("</CollectionModification>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listModificationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listModificationDetailDelete IsNot Nothing AndAlso listModificationDetailDelete.Count > 0 Then
            For Each detail In listModificationDetailDelete
                builder.Append("<CollectionModificationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</CollectionModificationDetail>")
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
            _collectionModificationRepository = Nothing
            _collectionDetailRepository = Nothing
            _secuenseDRepository = Nothing
            _recognitionDetailRepository = Nothing
            _budgetService = Nothing
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
