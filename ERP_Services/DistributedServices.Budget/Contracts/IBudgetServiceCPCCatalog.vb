#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceCPCCatalog

    ''' <summary>
    ''' Obtiene un CPCCatalog por ID 
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCPCCatalogById(Id As Integer, audit As AuditMessage) As ActionResult(Of CPCCatalog)

    ''' <summary>
    ''' Obtiene un CPCCatalog por código 
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCPCCatalogByCode(Code As String, audit As AuditMessage) As ActionResult(Of CPCCatalog)

    ''' <summary>
    ''' Guarda o Actualiza 
    ''' </summary>
    ''' <param name="CPCCatalog">la entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult(Of CPCCatalog)

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="CPCCatalog">The CPCCatalog.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStatusCPCCatalog(CPCCatalog As CPCCatalog, status As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CPCCatalog)

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="CPCCatalog">La entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult

End Interface

