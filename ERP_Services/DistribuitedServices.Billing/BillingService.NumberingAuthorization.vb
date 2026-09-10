'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Andres Alarcon
' Created          : 2022-08-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService

    ''' <summary>
    ''' Lista todas las resoluciones 
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListNumberingAuthorizationByUserCode(userCode As String) As List(Of Domain.Entities.NumberingAuthorization) Implements IBillingServiceNumberingAuthorization.ListNumberingAuthorizationByUserCode
        Using service As INumberingAuthorizationAdminService = Container.Current.Resolve(Of INumberingAuthorizationAdminService)()
            Return service.ListNumberingAuthorizationByUserCode(userCode)
        End Using
        'Return Me._DocumentSupportAdminService.ListDocumentSupportByUserCode(userCode)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion 
    ''' </summary>
    ''' <param name="NumberingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteNumberingAuthorization(NumberingAuthorization As Domain.Entities.NumberingAuthorization, audit As AuditMessage) As ActionResult Implements IBillingServiceNumberingAuthorization.DeleteNumberingAuthorization
        Using service As INumberingAuthorizationAdminService = Container.Current.Resolve(Of INumberingAuthorizationAdminService)()
            Return service.DeleteNumberingAuthorization(NumberingAuthorization, audit)
        End Using
        'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        'Return _DocumentSupportAdminService.DeleteDocumentSupport(DocumentSupport, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion 
    ''' </summary>
    ''' <param name="NumberingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveNumberingAuthorization(NumberingAuthorization As Domain.Entities.NumberingAuthorization, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization) Implements IBillingServiceNumberingAuthorization.SaveNumberingAuthorization
        Using service As INumberingAuthorizationAdminService = Container.Current.Resolve(Of INumberingAuthorizationAdminService)()
            Return service.SaveNumberingAuthorization(NumberingAuthorization, audit, idSequense)
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
    Public Function GetNumberingAuthorizationById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization) Implements IBillingServiceNumberingAuthorization.GetNumberingAuthorizationById
        Using service As INumberingAuthorizationAdminService = Container.Current.Resolve(Of INumberingAuthorizationAdminService)()
            Return service.GetNumberingAuthorizationById(id, audit)
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
    Public Function GetNumberingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization) Implements IBillingServiceNumberingAuthorization.GetNumberingAuthorizationByCode
        Using service As INumberingAuthorizationAdminService = Container.Current.Resolve(Of INumberingAuthorizationAdminService)()
            Return service.GetNumberingAuthorizationByCode(code, audit)
        End Using
        'Return _NumberingAuthorizationAdminService.GetNumberingAuthorizationByCode(code, audit)
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateNumberingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization) Implements IBillingServiceNumberingAuthorization.ChangeStateNumberingAuthorization
        Using service As INumberingAuthorizationAdminService = Container.Current.Resolve(Of INumberingAuthorizationAdminService)()
            Return service.ChangeStateNumberingAuthorization(code, state, audit)
        End Using
        'Return _DocumentSupportAdminService.ChangeStateDocumentSupport(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNumberingAuthorizationResolution(operatingUnitId As Integer, NumberingAuthorization As Domain.Entities.NumberingAuthorization, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization) Implements IBillingServiceNumberingAuthorization.GetNumberingAuthorizationResolution
        Using service As INumberingAuthorizationAdminService = Container.Current.Resolve(Of INumberingAuthorizationAdminService)()
            Return service.GetNumberingAuthorizationResolution(operatingUnitId, NumberingAuthorization, audit)
        End Using
    End Function

End Class