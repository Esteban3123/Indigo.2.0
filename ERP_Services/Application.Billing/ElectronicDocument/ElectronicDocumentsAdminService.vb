#Region "Imports"

Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Application.Base
Imports Application.EventHandlers
Imports Application.EventHandlers.Enums.Enums
Imports Application.EventHandlers.Model
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class ElectronicDocumentsAdminService
    Implements IElectronicDocumentsAdminService

#Region "Fields"

    Private _electronicDocumentRepository As IElectronicDocumentRepository

    'Events
    Private ReadOnly _eventProxy As IEventProxy

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(electronicDocumentRepository As IElectronicDocumentRepository, eventProxy As IEventProxy)
        Me._electronicDocumentRepository = electronicDocumentRepository

        _eventProxy = eventProxy
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Actualiza el estado de un documento electrónico
    ''' </summary>
    ''' <returns></returns>
    Public Function UpdateStateElectronicDocument(id As Integer, status As Byte, audit As AuditMessage) As ActionResult(Of ElectronicDocument) Implements IElectronicDocumentsAdminService.UpdateStateElectronicDocument
        Dim UnitOfWork As IUnitWork = _electronicDocumentRepository.UnitWork
        Try
            Dim documentTypeNumber = "N/A"
            Dim electronicDocument As ElectronicDocument = _electronicDocumentRepository.GetElectronicDocumentById(id)
            If electronicDocument IsNot Nothing AndAlso electronicDocument.Id > 0 Then
                documentTypeNumber = String.Format("{0} - {1}", electronicDocument.getDocumentTypeName(), electronicDocument.GetDocumentNumber())

                If electronicDocument.Status = 3 Then
                    Return New ActionResult(Of ElectronicDocument) With {.StateResult = False, .Message = $"La {documentTypeNumber} no se puede actualizar debido a que se encuentra en un estado válido."}
                End If

                electronicDocument.Status = status
                electronicDocument.MarkAsModified()

                _electronicDocumentRepository.SaveEntity(electronicDocument)
                UnitOfWork.Commit()

                Dim auditProcess As IndigoAuditSimpleEntity(Of ElectronicDocument)
                auditProcess = New IndigoAuditSimpleEntity(Of ElectronicDocument)(electronicDocument, audit, Infrastructure.CrossCutting.Audit.Actions.Update, electronicDocument.OriginalValue)
                auditProcess.Execute()
            End If

            Return New ActionResult(Of ElectronicDocument) With {.StateResult = True, .ObjectEmbbeded = electronicDocument, .Message = $"El estado de la {documentTypeNumber} fue actualizado correctamente"}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicDocument) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Reenvia la factura al proceso de facturacion electronica en Costa Rica
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="InvoiceId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ReSendElectronicDocument(InvoiceNumber As String, InvoiceId As Integer, session As SessionValues) As ActionResult Implements IElectronicDocumentsAdminService.ReSendElectronicDocument
        Try
            Dim messages As New StringBuilder
            Dim entityEvent As New ReSendInvoice

            ''Se asignan los valores
            entityEvent.InvoiceId = InvoiceId
            entityEvent.InvoiceNumber = InvoiceNumber

            ''Se publica el evento
            If entityEvent IsNot Nothing Then
                _eventProxy.Publish(New EventData(entityEvent, NameOf(EventType.ResendInvoice), NameOf(EventAction.added), session.AuditMessageWcf.Company, session.AuditMessageWcf.CodeUser, DateTime.Now().GetTimestamp))
            End If

            messages.AppendLine($"La factura {InvoiceNumber} fue reenviada correctamente")

            Return New ActionResult With {.StateResult = True, .Message = messages.ToString()}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Reenvia documentos electronicos al proceso de facturacion electronica en Costa Rica
    ''' </summary>
    ''' <param name="listElectronicDocuments"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ReSendElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument), session As SessionValues) As ActionResult Implements IElectronicDocumentsAdminService.ReSendElectronicDocuments
        Try
            Dim messages As New StringBuilder

            For Each electronicDocument In listElectronicDocuments
                Dim lastElectronicDocument = Me._electronicDocumentRepository.GetLastElectronicDocuments(electronicDocument.EntityId, electronicDocument.EntityName)
                If Not lastElectronicDocument.IsValidStatusToResend() Then
                    messages.AppendLine($"La {lastElectronicDocument.getDocumentTypeName()} {lastElectronicDocument.GetDocumentNumber()} no se puede reenviar porque se encuentra en estado {lastElectronicDocument.GetDocumentStatusName()}")
                    Continue For
                End If

                Dim entityEvent As New ReSendElectronicDocument With {
                    .Id = electronicDocument.Id
                }

                _eventProxy.Publish(New EventData(
                    entityEvent,
                    NameOf(EventType.ResendElectronicDocument),
                    NameOf(EventAction.added),
                    session.AuditMessageWcf.Company,
                    session.AuditMessageWcf.CodeUser,
                    DateTime.Now().GetTimestamp)
                )

                messages.AppendLine($"La {electronicDocument.getDocumentTypeName()} {electronicDocument.GetDocumentNumber()} fue reenviada correctamente")
            Next

            Return New ActionResult With {.StateResult = True, .Message = messages.ToString()}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el estado de uno o más documentos electrónicos
    ''' </summary>
    ''' <returns></returns>
    Public Function UpdateStateElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument), audit As AuditMessage) As ActionResult(Of ElectronicDocument) Implements IElectronicDocumentsAdminService.UpdateStateElectronicDocuments
        Dim messages As New StringBuilder
        Dim errors As New StringBuilder

        For Each electronicDocument In listElectronicDocuments
            Dim result = Me.UpdateStateElectronicDocument(electronicDocument.Id, electronicDocument.Status, audit)

            If result.StateResult Then
                messages.AppendLine(result.Message)
            Else
                errors.AppendLine(result.Message)
            End If
        Next

        Return New ActionResult(Of ElectronicDocument) With {.StateResult = True, .Message = messages.ToString(), .MessageAux = errors.ToString()}
    End Function

    ''' <summary>
    ''' Lista todas las facturas electronicas
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeInvoices(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IElectronicDocumentsAdminService.ListElectronicDocumentsTypeInvoices
        Try
            Return _electronicDocumentRepository.ListElectronicDocumentsTypeInvoices(OperatingUnitId, StatusId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ElectronicDocument)
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las notas debitos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeDebitNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IElectronicDocumentsAdminService.ListElectronicDocumentsTypeDebitNotes
        Try
            Return _electronicDocumentRepository.ListElectronicDocumentsTypeDebitNotes(OperatingUnitId, StatusId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ElectronicDocument)
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las notas creditos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeCreditNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IElectronicDocumentsAdminService.ListElectronicDocumentsTypeCreditNotes
        Try
            Return _electronicDocumentRepository.ListElectronicDocumentsTypeCreditNotes(OperatingUnitId, StatusId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ElectronicDocument)
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                'Others Services
            End If

            _electronicDocumentRepository = Nothing
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
