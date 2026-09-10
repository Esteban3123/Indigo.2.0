'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
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
Imports System.Text
Imports System.Data.SqlClient

#End Region

Public Class RecognitionAdminService
    Implements IRecognitionAdminService

#Region "Properties"

    Private _recognitionRepository As IRecognitionRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal recognitionRepository As IRecognitionRepository)
        If recognitionRepository Is Nothing Then
            Throw New ArgumentNullException("recognitionRepository Vacío")
        End If

        _recognitionRepository = recognitionRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetRecognition(Code As String, ItemType As Byte, budgetaryValidityId As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition) Implements IRecognitionAdminService.GetRecognition
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Recognition As Recognition = Me._recognitionRepository.GetRecognition(Code.Trim(), ItemType, budgetaryValidityId)
            If Recognition IsNot Nothing AndAlso Recognition.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Recognition)(Recognition, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Recognition) With {.StateResult = True, .ObjectEmbbeded = Recognition}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Recognition) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetRecognitionById(Id As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition) Implements IRecognitionAdminService.GetRecognitionById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Recognition As Recognition = Me._recognitionRepository.GetRecognitionById(Id)
            If Recognition IsNot Nothing AndAlso Recognition.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Recognition)(Recognition, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Recognition) With {.StateResult = True, .ObjectEmbbeded = Recognition}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Recognition) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SaveRecognition(recognition As Domain.Entities.Recognition, listDetailsForDelete As List(Of Integer), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition) Implements IRecognitionAdminService.SaveRecognition
        If recognition Is Nothing Then
            Throw New ArgumentNullException("Recognition")
        End If

        Dim unitOfWork As IUnitWork = Me._recognitionRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim EntityXml As String = ConvertToXmlRecognition(recognition)
                Dim ListDeleteXml As String = ConvertToXmlListDelete(listDetailsForDelete)
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(recognition.Id, recognition.Status)

                Dim resultStore = Me._recognitionRepository.SaveRecognition(EntityXml, ListDeleteXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of Recognition) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                recognition.Id = resultStore.Id
                recognition.Code = resultStore.Code

                Dim auditProcess = New IndigoAuditSimpleEntity(Of Recognition)(recognition, audit, auditStatus, recognition.OriginalValue)
                auditProcess.Execute()

                recognition.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of Recognition) With {.StateResult = True, .ObjectEmbbeded = recognition, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of Recognition) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Recognition) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Public Function DeleteRecognition(recognition As Domain.Entities.Recognition, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult Implements IRecognitionAdminService.DeleteRecognition
        If recognition Is Nothing Then
            Throw New ArgumentNullException("recognition")
        End If
        Dim unitOfWork As IUnitWork = Me._recognitionRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Recognition)
            auditProcess = New IndigoAuditSimpleEntity(Of Recognition)(recognition, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._recognitionRepository.DeleteEntity(recognition)
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

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Convertir el objeto de reconocimiento en xml
    ''' </summary>
    ''' <param name="recognition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXmlRecognition(recognition As Recognition) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Recognition>")

        builder.Append("<Id>" & recognition.Id & "</Id>")
        builder.Append("<Code>" & recognition.Code & "</Code>")
        builder.Append("<OperatingUnitId>" & recognition.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<BudgetaryValidityId>" & recognition.BudgetaryValidityId & "</BudgetaryValidityId>")
        builder.Append("<DocumentDate>" & recognition.DocumentDate.ToString("dd/MM/yyyy hh:mm:ss") & "</DocumentDate>")
        builder.Append("<Document>" & recognition.Document & "</Document>")
        builder.Append("<Observations>" & recognition.Observations & "</Observations>")
        builder.Append("<RecognitonType>" & recognition.RecognitonType & "</RecognitonType>")
        builder.Append("<ThirdPartyId>" & recognition.ThirdPartyId & "</ThirdPartyId>")
        builder.Append("<DependencyId>" & recognition.DependencyId & "</DependencyId>")
        builder.Append("<AutomaticCollection>" & recognition.AutomaticCollection & "</AutomaticCollection>")
        builder.Append("<Applicant>" & recognition.Applicant & "</Applicant>")
        builder.Append("<Status>" & recognition.Status & "</Status>")
        If recognition.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & recognition.EntityId & "</EntityId>")
            builder.Append("<EntityCode>" & recognition.EntityCode & "</EntityCode>")
            builder.Append("<EntityName>" & recognition.EntityName & "</EntityName>")
        End If

        For Each detail As RecognitionDetail In recognition.RecognitionDetail
            builder.Append("<RecognitionDetail>")

            builder.Append("<Id>" & detail.Id & "</Id>")
            builder.Append("<RecognitionId>" & detail.RecognitionId & "</RecognitionId>")
            builder.Append("<CategoryId>" & detail.CategoryId & "</CategoryId>")
            builder.Append("<RevenueTypeId>" & detail.RevenueTypeId & "</RevenueTypeId>")
            builder.Append("<InitialValue>" & detail.InitialValue.ToString().Replace(",", ".") & "</InitialValue>")

            builder.Append("</RecognitionDetail>")
        Next

        builder.Append("</Recognition>")

        Return builder.ToString()
    End Function

    Private Function ConvertToXmlListDelete(listCollectionDetailDelete As List(Of Integer)) As String
        Dim builder As StringBuilder = New StringBuilder()

        If listCollectionDetailDelete IsNot Nothing AndAlso listCollectionDetailDelete.Count > 0 Then
            For Each detail In listCollectionDetailDelete
                builder.Append("<RecognitionDetail>")
                builder.Append("<Id>" & detail & "</Id>")
                builder.Append("</RecognitionDetail>")
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

            _recognitionRepository = Nothing
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
