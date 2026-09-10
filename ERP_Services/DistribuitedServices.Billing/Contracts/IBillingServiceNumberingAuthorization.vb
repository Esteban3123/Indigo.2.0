#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceNumberingAuthorization

#Region "Methods"

    ''' <summary>
    ''' Lista todas las resoluciones 
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    <OperationContract()>
    Function ListNumberingAuthorizationByUserCode(userCode As String) As List(Of NumberingAuthorization)

    '<OperationContract()>
    'Function SaveSequence(ByVal seq As PaymentsSecuence) As ActionResult

    ''' <summary>
    ''' elimina una  autorizacion de factura
    ''' </summary>
    ''' <param name="NumberingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteNumberingAuthorization(NumberingAuthorization As Domain.Entities.NumberingAuthorization, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o actualiza una  autorizacion de factura
    ''' </summary>
    ''' <param name="NumberingAuthorization"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveNumberingAuthorization(NumberingAuthorization As Domain.Entities.NumberingAuthorization, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetNumberingAuthorizationById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetNumberingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateNumberingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization)

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetNumberingAuthorizationResolution(operatingUnitId As Integer, NumberingAuthorization As NumberingAuthorization, audit As AuditMessage) As ActionResult(Of NumberingAuthorization)

#End Region
End Interface
