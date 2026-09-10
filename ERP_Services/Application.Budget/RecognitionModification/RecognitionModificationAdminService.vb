'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 28-08-2015
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

Public Class RecognitionModificationAdminService
    Implements IRecognitionModificationAdminService

#Region "Properties"

    ''' <summary>
    ''' repositorio de traslado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Private _recognitionModificationRepository As IRecognitionModificationRepository

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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetService As IBudgetService

    ''' <summary>
    ''' repositorio de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

#End Region

#Region "Builder"

    Public Sub New(recognitionModificationRepository As IRecognitionModificationRepository, budgetControlRepository As IBudgetControlRepository,
                   sequenseBudgetDRepository As ISequenseBudgetDRepository, budgetService As IBudgetService, budgetRepository As IBudgetRepository)
        If recognitionModificationRepository Is Nothing Then
            Throw New ArgumentNullException("recognitionModificationRepository")
        End If
        _recognitionModificationRepository = recognitionModificationRepository
        _BudgetControlRepository = budgetControlRepository
        _sequenseBudgetDRepository = sequenseBudgetDRepository
        _budgetService = budgetService
        _budgetRepository = budgetRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un modificacion reconocimiento por codigo 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognitionModificationByCode(code As String, budgetaryValidityId As Integer, audit As AuditMessage) As RecognitionModification Implements IRecognitionModificationAdminService.GetRecognitionModificationByCode
        Try
            Dim recognitionModification = _recognitionModificationRepository.GetRecognitionModificationByCode(code, budgetaryValidityId)
            If recognitionModification IsNot Nothing AndAlso recognitionModification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RecognitionModification)(recognitionModification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return recognitionModification
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RecognitionModification
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un modificacion reconocimiento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognitionModificationById(id As Integer) As RecognitionModification Implements IRecognitionModificationAdminService.GetRecognitionModificationById
        Try
            Return _recognitionModificationRepository.GetRecognitionModificationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RecognitionModification
        End Try
    End Function

    ''' <summary>
    ''' Guarda un modificacion reconocimiento
    ''' </summary>
    ''' <param name="recognitionModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRecognitionModification(recognitionModification As RecognitionModification, listModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of RecognitionModification) Implements IRecognitionModificationAdminService.SaveRecognitionModification
        If recognitionModification Is Nothing Then
            Throw New ArgumentNullException("recognitionModification")
        End If

        Dim unitOfWork As IUnitWork = Me._recognitionModificationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlRecognitionModification(recognitionModification)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listModificationDetailDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(recognitionModification.Id, recognitionModification.Status)

                Dim resultStore = Me._recognitionModificationRepository.SP_SaveRecognitionModification(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of RecognitionModification) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                recognitionModification.Id = resultStore.Id
                recognitionModification.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of RecognitionModification)(recognitionModification, audit, auditStatus, recognitionModification.OriginalValue)
                auditProcess.Execute()

                recognitionModification.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of RecognitionModification) With {.StateResult = True, .ObjectEmbbeded = recognitionModification, .Message = resultStore.MessageResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of RecognitionModification) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of RecognitionModification) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertToXmlRecognitionModification(RecognitionModification As RecognitionModification) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<RecognitionModification>")

        builder.Append("<Id>" & RecognitionModification.Id & "</Id>")
        builder.Append("<Code>" & RecognitionModification.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & RecognitionModification.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<BudgetaryValidityId>" & RecognitionModification.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & RecognitionModification.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") & "</DocumentDate>")
        builder.Append("<RecognitionId>" & RecognitionModification.RecognitionId & "</RecognitionId>")
        builder.Append("<Document>" & RecognitionModification.Document & "</Document>")
        builder.Append("<Observations>" & RecognitionModification.Observations & "</Observations>")
        builder.Append("<Status>" & RecognitionModification.Status & "</Status>")
        If RecognitionModification.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & RecognitionModification.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & RecognitionModification.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & RecognitionModification.EntityName & "</EntityName>")
        End If

        For Each detail In RecognitionModification.RecognitionModificationDetail
            builder.Append("<RecognitionModificationDetail>")

            If detail.Id > 0 Then
                builder.Append("<Id>" & detail.Id & "</Id>")
            End If

            builder.Append("<RecognitionModificationId>" & detail.RecognitionModificationId & "</RecognitionModificationId>")
            builder.Append("<RecognitionDetailId>" & detail.RecognitionDetailId & "</RecognitionDetailId>")
            If detail.RecognitionDetail IsNot Nothing Then
                builder.Append("<CategoryId>" & detail.RecognitionDetail.CategoryId & "</CategoryId>")
                builder.Append("<RevenueTypeId>" & detail.RecognitionDetail.RevenueTypeId & "</RevenueTypeId>")
            End If
            builder.Append("<Nature>" & detail.Nature & "</Nature>")
            builder.Append("<Value>" & detail.Value.ToString().Replace(",", ".") & "</Value>")

            builder.Append("</RecognitionModificationDetail>")
        Next

        builder.Append("</RecognitionModification>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listModificationDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listModificationDetailDelete IsNot Nothing AndAlso listModificationDetailDelete.Count > 0 Then
            For Each detail In listModificationDetailDelete
                builder.Append("<RecognitionModificationDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</RecognitionModificationDetail>")
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
            _recognitionModificationRepository = Nothing
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
