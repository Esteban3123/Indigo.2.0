'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListBillingAuthorizationByUserCode(userCode As String) As List(Of Domain.Entities.BillingAuthorization) Implements IBillingAuthorization.ListBillingAuthorizationByUserCode
        Using service As IBillingAuthorizationAdminService = Container.Current.Resolve(Of IBillingAuthorizationAdminService)()
            Return service.ListBillingAuthorizationByUserCode(userCode)
        End Using
        'Return Me._billingAuthorizationAdminService.ListBillingAuthorizationByUserCode(userCode)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion de factura
    ''' </summary>
    ''' <param name="billingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBillingAuthorization(billingAuthorization As Domain.Entities.BillingAuthorization, audit As AuditMessage) As ActionResult Implements IBillingAuthorization.DeleteBillingAuthorization
        Using service As IBillingAuthorizationAdminService = Container.Current.Resolve(Of IBillingAuthorizationAdminService)()
            Return service.DeleteBillingAuthorization(billingAuthorization, audit)
        End Using
        'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        'Return _billingAuthorizationAdminService.DeleteBillingAuthorization(billingAuthorization, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion de factura
    ''' </summary>
    ''' <param name="billingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBillingAuthorization(billingAuthorization As Domain.Entities.BillingAuthorization, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization) Implements IBillingAuthorization.SaveBillingAuthorization
        Using service As IBillingAuthorizationAdminService = Container.Current.Resolve(Of IBillingAuthorizationAdminService)()
            Return service.SaveBillingAuthorization(billingAuthorization, audit, idSequense)
        End Using
        'Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
        'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        'Return _billingAuthorizationAdminService.SaveBillingAuthorization(billingAuthorization, audit, idSequense)
    End Function

    ''' <summary>
    ''' Obtiene una autorizacion de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingAuthorizationById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization) Implements IBillingAuthorization.GetBillingAuthorizationById
        Using service As IBillingAuthorizationAdminService = Container.Current.Resolve(Of IBillingAuthorizationAdminService)()
            Return service.GetBillingAuthorizationById(id, audit)
        End Using
        'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        'Return _billingAuthorizationAdminService.GetBillingAuthorizationById(id, audit)
    End Function

    ''' <summary>
    ''' Obtiene una autorizacion de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization) Implements IBillingAuthorization.GetBillingAuthorizationByCode
        Using service As IBillingAuthorizationAdminService = Container.Current.Resolve(Of IBillingAuthorizationAdminService)()
            Return service.GetBillingAuthorizationByCode(code, audit)
        End Using
        'Return _billingAuthorizationAdminService.GetBillingAuthorizationByCode(code, audit)
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateBillingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization) Implements IBillingAuthorization.ChangeStateBillingAuthorization
        Using service As IBillingAuthorizationAdminService = Container.Current.Resolve(Of IBillingAuthorizationAdminService)()
            Return service.ChangeStateBillingAuthorization(code, state, audit)
        End Using
        'Return _billingAuthorizationAdminService.ChangeStateBillingAuthorization(code, state, audit)
    End Function

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As Domain.Entities.BillingAuthorization, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization) Implements IBillingAuthorization.GetBillingAuthorizationResolution
        Using service As IBillingAuthorizationAdminService = Container.Current.Resolve(Of IBillingAuthorizationAdminService)()
            Return service.GetBillingAuthorizationResolution(operatingUnitId, billingAuthorization, audit)
        End Using
    End Function

End Class
