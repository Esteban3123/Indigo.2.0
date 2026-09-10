#Region "Imports"

Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' Elimina un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteDocumentType(doc As Domain.Entities.JournalVoucherTypes) As Task(Of Domain.Base.Entities.ActionResult) Implements IAccountingDocumentType.DeleteDocumentType
        Using service As IDocumentTypeAdminService = Container.Current.Resolve(Of IDocumentTypeAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return Await service.DeleteDocumentType(doc, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="code">Código del documento a consultar</param>
    ''' <returns>Tipo de documento consultado</returns>
    Public Async Function GetDocumentType(code As String) As Task(Of Domain.Entities.JournalVoucherTypes) Implements IAccountingDocumentType.GetDocumentType
        Using service As IDocumentTypeAdminService = Container.Current.Resolve(Of IDocumentTypeAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return Await service.GetDocumentType(code.Trim(), audit)
        End Using
    End Function

    ''' <summary>
    ''' Graba un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function SaveDocumentType(doc As Domain.Entities.JournalVoucherTypes) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.JournalVoucherTypes)) Implements IAccountingDocumentType.SaveDocumentType
        Using service As IDocumentTypeAdminService = Container.Current.Resolve(Of IDocumentTypeAdminService)()
            Dim idSequence As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return Await service.SaveDocumentType(doc, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateDocumentType(code As String, state As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.JournalVoucherTypes)) Implements IAccountingDocumentType.UpdateStateDocumentType
        Using service As IDocumentTypeAdminService = Container.Current.Resolve(Of IDocumentTypeAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return Await service.UpdateStateDocumentType(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de comprobante por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetJournalVoucherById(id As Long) As Domain.Base.Entities.ActionResult(Of Domain.Entities.JournalVoucherTypes) Implements IAccountingDocumentType.GetJournalVoucherById
        Using service As IDocumentTypeAdminService = Container.Current.Resolve(Of IDocumentTypeAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetJournalVoucherById(id, audit)
        End Using
        'Return Me._documentTypeAdminService.GetJournalVoucherById(id, audit)
    End Function

#End Region

End Class
