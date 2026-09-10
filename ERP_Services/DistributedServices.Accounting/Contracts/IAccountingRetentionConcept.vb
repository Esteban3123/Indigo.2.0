#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountingRetentionConcept

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto de retencion
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRetentionConcept(ByVal code As String) As RetentionConcepts

    ''' <summary>
    ''' Guarda un concepto de retencion
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRetentionConcept(ByVal doc As RetentionConcepts) As ActionResult(Of RetentionConcepts)

    ''' <summary>
    ''' Elimina un concepto de retencion
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRetentionConcept(ByVal doc As RetentionConcepts) As ActionResult


    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRetentionById(ByVal id As Integer, Optional auditParam As AuditMessage = Nothing) As RetentionConcepts

    ''' <summary>
    ''' Gets the retention concept by city.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="addressId">The address identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRetentionConceptByCity(id As Integer, addressId As Integer) As RetentionConceptByCity

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateRetentionConcept(code As String, state As Boolean) As ActionResult(Of RetentionConcepts)

    ''' <summary>
    ''' Obtiene un listado de rangos de retencion para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As ActionResult(Of List(Of RetentionConceptRanges))

    ''' <summary>
    ''' Obtiene el concepto de retención ICA asociado a una determinada sucursal
    ''' </summary>
    ''' <param name="brachOfficeId">The brach office identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRetentionConceptByIdBrachOfficeId(brachOfficeId As Integer) As RetentionConcepts
    <OperationContract()>
    Function GetIVARetentionConceptByThirdPartyId(thirdPartyId As Integer) As RetentionConcepts

#End Region

End Interface
