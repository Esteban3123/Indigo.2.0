'***********************************************************************
' Assembly         : Application.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-27
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

#End Region

Public Interface IBillingAuthorizationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListBillingAuthorizationByUserCode(userCode As String) As List(Of BillingAuthorization)


    ''' <summary>
    ''' elimina una autorización
    ''' </summary>
    ''' <param name="billingAuthorization">The billing authorization.</param>
    ''' <returns></returns>
    Function DeleteBillingAuthorization(billingAuthorization As BillingAuthorization, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda una autorización
    ''' </summary>
    ''' <param name="billingAuthorization">The billing authorization.</param>
    ''' <returns></returns>
    Function SaveBillingAuthorization(billingAuthorization As BillingAuthorization, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BillingAuthorization)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingAuthorizationById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of BillingAuthorization)

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of BillingAuthorization)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateBillingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BillingAuthorization)

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As BillingAuthorization, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization)

End Interface
