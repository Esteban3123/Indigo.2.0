#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingAuthorization

#Region "Methods"

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    <OperationContract()>
    Function ListBillingAuthorizationByUserCode(userCode As String) As List(Of BillingAuthorization)

    <OperationContract()>
    Function SaveSequence(ByVal seq As BillingSequence) As ActionResult

    ''' <summary>
    ''' elimina una  autorizacion de factura
    ''' </summary>
    ''' <param name="billingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteBillingAuthorization(billingAuthorization As Domain.Entities.BillingAuthorization, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o actualiza una  autorizacion de factura
    ''' </summary>
    ''' <param name="billingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveBillingAuthorization(billingAuthorization As Domain.Entities.BillingAuthorization, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBillingAuthorizationById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBillingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateBillingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.BillingAuthorization)

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetBillingAuthorizationResolution(operatingUnitId As Integer, billingAuthorization As BillingAuthorization, audit As AuditMessage) As ActionResult(Of BillingAuthorization)

#End Region
End Interface
