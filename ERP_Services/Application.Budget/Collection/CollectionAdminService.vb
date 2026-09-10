'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
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
Imports System.Text
Imports Domain.Entities.Service

#End Region

Public Class CollectionAdminService
    Implements ICollectionAdminService

#Region "Properties"

    Private _collectionRepository As ICollectionRepository

#End Region

#Region "Builder"

    Public Sub New(collectionRepository As ICollectionRepository)
        If collectionRepository Is Nothing Then
            Throw New ArgumentNullException("collectionRepository")
        End If

        _collectionRepository = collectionRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetCollectionByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Collection Implements ICollectionAdminService.GetCollectionByCode
        Try
            Dim collection = _collectionRepository.GetCollectionByCode(code, BudgetaryValidityId)
            If collection IsNot Nothing AndAlso collection.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Collection)(collection, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return collection
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Collection
        End Try
    End Function

    Public Function GetCollectionById(id As Integer) As Collection Implements ICollectionAdminService.GetCollectionById
        Try
            Return _collectionRepository.GetCollectionById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Collection
        End Try
    End Function

    Public Function SaveCollection(collection As Collection, listDetailsForDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of Collection) Implements ICollectionAdminService.SaveCollection
        If collection Is Nothing Then
            Throw New ArgumentNullException("Collection")
        End If

        Dim unitOfWork As IUnitWork = Me._collectionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlCollection(collection)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listDetailsForDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(collection.Id, collection.Status)

                Dim resultStore = Me._collectionRepository.SaveCollection(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of Collection) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                collection.Id = resultStore.Id
                collection.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of Collection)(collection, audit, auditStatus, collection.OriginalValue)
                auditProcess.Execute()

                collection.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of Collection) With {.StateResult = True, .ObjectEmbbeded = collection, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of Collection) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Collection) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Convertir el objeto de recaudo en xml
    ''' </summary>
    ''' <param name="collection"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXmlCollection(collection As Collection) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Collection>")

        builder.Append("<Id>" & collection.Id & "</Id>")
        builder.Append("<Code>" & collection.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & collection.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<BudgetaryValidityId>" & collection.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & collection.DocumentDate.ToString("dd/MM/yyyy hh:mm:ss") & "</DocumentDate>")
        builder.Append("<Observations>" & collection.Observations & "</Observations>")
        builder.Append("<ThirdPartyId>" & collection.ThirdPartyId & "</ThirdPartyId>")
        builder.Append("<Status>" & collection.Status & "</Status>")
        If collection.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & collection.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & collection.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & collection.EntityName & "</EntityName>")
        End If

        For Each detail As CollectionDetail In collection.CollectionDetail
            builder.Append("<CollectionDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If
            builder.Append("<CollectionId>" & detail.CollectionId & "</CollectionId>")
            builder.Append("<RecognitionDetailId>" & detail.RecognitionDetailId & "</RecognitionDetailId>")
            builder.Append("<CollectionType>" & detail.CollectionType & "</CollectionType>")
            builder.Append("<InitialValue>" & detail.InitialValue.ToString().Replace(",", ".") & "</InitialValue>")

            builder.Append("</CollectionDetail>")
        Next

        builder.Append("</Collection>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listCollectionDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listCollectionDetailDelete IsNot Nothing AndAlso listCollectionDetailDelete.Count > 0 Then
            For Each detail In listCollectionDetailDelete
                builder.Append("<CollectionDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</CollectionDetail>")
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

            End If

            _collectionRepository = Nothing
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
