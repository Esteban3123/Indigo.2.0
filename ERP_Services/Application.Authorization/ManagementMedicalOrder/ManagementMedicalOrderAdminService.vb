#Region "Imports"

Imports System.Text
Imports System.Transactions
Imports Application.Common
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class ManagementMedicalOrderAdminService
    Implements IManagementMedicalOrderAdminService

#Region "Builder"

    Private _managementMedicalOrderRepository As IManagementMedicalOrderRepository
    Private _attachmentAdminService As IAttachmentAdminService

    Public Sub New(managementMedicalOrderRepository As IManagementMedicalOrderRepository, attachmentAdminService As IAttachmentAdminService)
        _managementMedicalOrderRepository = managementMedicalOrderRepository
        _attachmentAdminService = attachmentAdminService
    End Sub

#End Region

#Region "Methods"

    Public Function GetManagementMedicalOrderById(id As Integer) As ActionResult(Of ManagementMedicalOrder) Implements IManagementMedicalOrderAdminService.GetManagementMedicalOrderById
        Try
            Dim ManagementMedicalOrder = _managementMedicalOrderRepository.GetManagementMedicalOrderById(id)
            Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = True, .ObjectEmbbeded = ManagementMedicalOrder}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveManagementMedicalOrder(ListManagementMedicalOrder As List(Of ManagementMedicalOrder), session As SessionValues) As ActionResult(Of ManagementMedicalOrder) Implements IManagementMedicalOrderAdminService.SaveManagementMedicalOrder
        If ListManagementMedicalOrder Is Nothing OrElse ListManagementMedicalOrder.Count = 0 Then
            Throw New ArgumentNullException("ListManagementMedicalOrder")
        End If

        Dim unitOfWork As IUnitWork = Me._managementMedicalOrderRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim listManagementMedicalOrderXml = ConvertEntityToXml(ListManagementMedicalOrder)

                Dim result = _managementMedicalOrderRepository.SP_SaveManagementMedicalOrder(listManagementMedicalOrderXml, session.AuditMessageWcf.CodeUser)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = False, .Message = result.MessageResult}
                End If

                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = True, .Message = result.MessageResult}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveManagementMedicalOrderWithdrawal(managementMedicalOrder As ManagementMedicalOrder, ByVal attachment As Attachment, session As SessionValues) As ActionResult(Of ManagementMedicalOrder) Implements IManagementMedicalOrderAdminService.SaveManagementMedicalOrderWithdrawal
        If managementMedicalOrder Is Nothing Then
            Throw New ArgumentNullException("ManagementMedicalOrder")
        End If

        Dim unitOfWork As IUnitWork = Me._managementMedicalOrderRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                managementMedicalOrder = _managementMedicalOrderRepository.GetManagementMedicalOrderByEntity(managementMedicalOrder.EntityName, managementMedicalOrder.EntityId)
                If managementMedicalOrder Is Nothing Then
                    scope.Dispose()
                    Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = False, .Message = "No existe una gestión de orden médica asociada a la solicitud."}
                End If
                If Not managementMedicalOrder.Status = 1 Then
                    scope.Dispose()
                    Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = False, .Message = "No existe una gestión de orden médica en estado cancelada que requiera desistimiento."}
                End If

                Dim attachmentResult = _attachmentAdminService.SaveAttachment(attachment, session)
                If attachmentResult.StateResult = False Then
                    scope.Dispose()
                    Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = False, .Message = attachmentResult.Message}
                End If

                managementMedicalOrder.Status = 2
                managementMedicalOrder.ModificationUser = session.AuditMessageWcf.CodeUser
                managementMedicalOrder.ModificationDate = DateTime.Now
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = True, .Message = "El documento fue guardado correctamente."}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of ManagementMedicalOrder) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertEntityToXml(ListManagementMedicalOrder As List(Of ManagementMedicalOrder)) As String
        Dim builder As New StringBuilder
        If ListManagementMedicalOrder IsNot Nothing And ListManagementMedicalOrder.Any Then
            For Each ManagementMedicalOrder In ListManagementMedicalOrder
                builder.Append(ManagementMedicalOrder.ToXML())
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
                _attachmentAdminService.Dispose()
            End If

            _managementMedicalOrderRepository = Nothing
            _attachmentAdminService = Nothing
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
