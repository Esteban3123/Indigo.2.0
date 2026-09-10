'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 2021-01-14
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Class PaymentsService

    ''' <summary>
    ''' Lista todas las resoluciones 
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListDocumentSupportByUserCode(userCode As String) As List(Of Domain.Entities.DocumentSupport) Implements IPaymentsDocumentSupport.ListDocumentSupportByUserCode
        Using service As IDocumentSupportAdminService = Container.Current.Resolve(Of IDocumentSupportAdminService)()
            Return service.ListDocumentSupportByUserCode(userCode)
        End Using
        'Return Me._DocumentSupportAdminService.ListDocumentSupportByUserCode(userCode)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion 
    ''' </summary>
    ''' <param name="DocumentSupport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDocumentSupport(DocumentSupport As Domain.Entities.DocumentSupport, audit As AuditMessage) As ActionResult Implements IPaymentsDocumentSupport.DeleteDocumentSupport
        Using service As IDocumentSupportAdminService = Container.Current.Resolve(Of IDocumentSupportAdminService)()
            Return service.DeleteDocumentSupport(DocumentSupport, audit)
        End Using
        'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        'Return _DocumentSupportAdminService.DeleteDocumentSupport(DocumentSupport, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion 
    ''' </summary>
    ''' <param name="DocumentSupport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDocumentSupport(DocumentSupport As Domain.Entities.DocumentSupport, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport) Implements IPaymentsDocumentSupport.SaveDocumentSupport
        Using service As IDocumentSupportAdminService = Container.Current.Resolve(Of IDocumentSupportAdminService)()
            Return service.SaveDocumentSupport(DocumentSupport, audit, idSequense)
        End Using
        'Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
        'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        'Return _DocumentSupportAdminService.SaveDocumentSupport(DocumentSupport, audit, idSequense)
    End Function

    ''' <summary>
    ''' Obtiene una autorizacion de documento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentSupportById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport) Implements IPaymentsDocumentSupport.GetDocumentSupportById
        Using service As IDocumentSupportAdminService = Container.Current.Resolve(Of IDocumentSupportAdminService)()
            Return service.GetDocumentSupportById(id, audit)
        End Using
        'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        'Return _DocumentSupportAdminService.GetDocumentSupportById(id, audit)
    End Function

    ''' <summary>
    ''' Obtiene una autorizacion de documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentSupportByCode(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport) Implements IPaymentsDocumentSupport.GetDocumentSupportByCode
        Using service As IDocumentSupportAdminService = Container.Current.Resolve(Of IDocumentSupportAdminService)()
            Return service.GetDocumentSupportByCode(code, audit)
        End Using
        'Return _DocumentSupportAdminService.GetDocumentSupportByCode(code, audit)
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDocumentSupport(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport) Implements IPaymentsDocumentSupport.ChangeStateDocumentSupport
        Using service As IDocumentSupportAdminService = Container.Current.Resolve(Of IDocumentSupportAdminService)()
            Return service.ChangeStateDocumentSupport(code, state, audit)
        End Using
        'Return _DocumentSupportAdminService.ChangeStateDocumentSupport(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentSupportResolution(operatingUnitId As Integer, DocumentSupport As Domain.Entities.DocumentSupport, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport) Implements IPaymentsDocumentSupport.GetDocumentSupportResolution
        Using service As IDocumentSupportAdminService = Container.Current.Resolve(Of IDocumentSupportAdminService)()
            Return service.GetDocumentSupportResolution(operatingUnitId, DocumentSupport, audit)
        End Using
    End Function

End Class
