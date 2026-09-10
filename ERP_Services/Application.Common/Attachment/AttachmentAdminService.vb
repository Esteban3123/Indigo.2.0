#Region "Imports"

Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class AttachmentAdminService
    Implements IAttachmentAdminService

#Region "Builder"

    Private _attachmentRepository As IAttachmentRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="attachmentRepository">el repositorio para el manejo de los Países.</param>
    Public Sub New(ByVal attachmentRepository As IAttachmentRepository)
        _attachmentRepository = attachmentRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetAttachmentById(id As Integer) As ActionResult(Of Attachment) Implements IAttachmentAdminService.GetAttachmentById
        Try
            Dim attachment = _attachmentRepository.GetAttachmentById(id)
            Return New ActionResult(Of Attachment) With {.StateResult = True, .ObjectEmbbeded = attachment}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Attachment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetAttachmentsByFormAndEntity(formId As Integer, entityName As String, entityId As Integer, ByVal withTop As Boolean) As ActionResult(Of List(Of Attachment)) Implements IAttachmentAdminService.GetAttachmentsByFormAndEntity
        Try
            Dim listAttachments = _attachmentRepository.GetAttachmentsByFormAndEntity(formId, entityName, entityId, withTop)
            Return New ActionResult(Of List(Of Attachment)) With {.StateResult = True, .ObjectEmbbeded = listAttachments}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Attachment)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveAttachment(attachment As Attachment, session As SessionValues) As ActionResult(Of Attachment) Implements IAttachmentAdminService.SaveAttachment
        If attachment Is Nothing Then
            Throw New ArgumentNullException("Attachment")
        End If

        Dim unitOfWork As IUnitWork = Me._attachmentRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                attachment.CreationUser = session.AuditMessageWcf.CodeUser
                attachment.CreationDate = DateTime.Now
                _attachmentRepository.SaveEntity(attachment)
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult(Of Attachment) With {.StateResult = True, .Message = "Se guardo el documento adjunto."}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of Attachment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function DeleteAttachment(attachment As Attachment, session As SessionValues) As ActionResult(Of Attachment) Implements IAttachmentAdminService.DeleteAttachment
        If attachment Is Nothing Then
            Throw New ArgumentNullException("Attachment")
        End If

        Dim unitOfWork As IUnitWork = Me._attachmentRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                attachment.MarkAsDeleted()
                _attachmentRepository.SaveEntity(attachment)
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult(Of Attachment) With {.StateResult = True, .Message = "Se eliminó el documento adjunto."}
            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of Attachment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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

            _attachmentRepository = Nothing
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
