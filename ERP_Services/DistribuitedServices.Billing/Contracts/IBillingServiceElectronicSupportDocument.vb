#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceElectronicSupportDocument

#Region "Methods"

    ''' <summary>
    ''' obtiene  por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetElectronicSupportDocumentById(id As Integer) As ActionResult(Of ElectronicSupportDocument)

    ''' <summary>
    ''' obtiene  por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetElectronicSupportDocumentByCode(code As String, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ElectronicSupportDocument"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveConfirmElectronicSupportDocument(ElectronicSupportDocument As ElectronicSupportDocument, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult

    ''' <summary>
    ''' Actualiza un estado para ser reenviado
    ''' </summary>
    ''' <param name="Ids"></param>
    ''' <returns></returns>
    <OperationContract>
    Function UpdateStateElectronicSupportDocuments(Ids As List(Of Integer), audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument)

#End Region

End Interface
