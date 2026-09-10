Imports System.Text
Imports System.Transactions
Imports Application.Billing
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class RevenueRecognitionAdminService
    Implements IRevenueRecognitionAdminService

    Private _careGroupRepository As ICareGroupRepository
    Private _recognitionRepository As IRevenueRecognitionRepository

    Public Sub New(caregroupRepository As ICareGroupRepository,
                   recognitionRepository As IRevenueRecognitionRepository)
        _careGroupRepository = caregroupRepository
        _recognitionRepository = recognitionRepository
    End Sub

    Public Function GenerateRecognition(careGroupXml As String, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As ActionResult Implements IRevenueRecognitionAdminService.GenerateRecognition
        Try
            Dim cgXml As New Xml.XmlDocument()
            cgXml.LoadXml(careGroupXml)

            Dim nodes = cgXml.SelectNodes("RecognitionEntrance/RecognitionEntranceDetail")
            Dim careGroupListId As List(Of Integer) = _careGroupRepository.ListCareGroupIdsRecognition(operatingUnitId)
            Dim msg As New StringBuilder()
            Dim consecutives As New List(Of String)()

            For Each cgId In careGroupListId
                Dim result = Me.GenerateRecognitionByCareGroup(cgId, 0, operatingUnitId, recognitionDate, userCode)
                msg.AppendLine(result.Message)
                If result.StateResult Then
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        consecutives.Add(result.MessageResult(0))
                    End If
                End If
            Next

            Return New ActionResult With {.StateResult = True, .Message = msg.ToString(), .MessageResult = consecutives}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GenerateRecognitionByCareGroup(careGroupId As Integer, careGroupTotal As Decimal, operatingUnitId As Integer, recognitionDate As Date, userCode As String) As ActionResult Implements IRevenueRecognitionAdminService.GenerateRecognitionByCareGroup
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim result As SP_GenerateRecognition_Result = _recognitionRepository.SP_GenerateRecognition(careGroupId, careGroupTotal, operatingUnitId, recognitionDate, userCode)
                If Not result.StatusResult Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = result.MessageResult, .MessageResult = {result.RevenueRecognitionId.ToString}.ToList()}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ReverseRecognition(revenueRecognitionId As Integer, userCode As String) As ActionResult Implements IRevenueRecognitionAdminService.ReverseRecognition
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim result As SP_ReverseRecognition_Result = _recognitionRepository.SP_ReverseRecognition(revenueRecognitionId, userCode)
                If Not result.StatusResult Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = result.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = result.MessageResult, .MessageResult = {result.RevenueRecognitionId.ToString}.ToList()}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _careGroupRepository = Nothing
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
