#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IPaymentsDocumentSupport

#Region "Methods"

    ''' <summary>
    ''' Lista todas las resoluciones 
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    <OperationContract()>
    Function ListDocumentSupportByUserCode(userCode As String) As List(Of DocumentSupport)

    '<OperationContract()>
    'Function SaveSequence(ByVal seq As PaymentsSecuence) As ActionResult

    ''' <summary>
    ''' elimina una  autorizacion de factura
    ''' </summary>
    ''' <param name="DocumentSupport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteDocumentSupport(DocumentSupport As Domain.Entities.DocumentSupport, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o actualiza una  autorizacion de factura
    ''' </summary>
    ''' <param name="DocumentSupport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveDocumentSupport(DocumentSupport As Domain.Entities.DocumentSupport, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDocumentSupportById(id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport)

    ''' <summary>
    ''' Obtiene una autorizacion de factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDocumentSupportByCode(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateDocumentSupport(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport)

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDocumentSupportResolution(operatingUnitId As Integer, DocumentSupport As DocumentSupport, audit As AuditMessage) As ActionResult(Of DocumentSupport)

#End Region
End Interface
