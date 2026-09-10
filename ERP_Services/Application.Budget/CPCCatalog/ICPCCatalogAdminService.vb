Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICPCCatalogAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un rubro por código 
    ''' </summary>
    '''<param name="Id">Id del CPCCatalog</param>
    ''' <returns></returns>
    Function GetCPCCatalogById(Id As Integer, audit As AuditMessage) As ActionResult(Of CPCCatalog)

    ''' <summary>
    ''' Obtiene un rubro por código 
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Function GetCPCCatalogByCode(Code As String, audit As AuditMessage) As ActionResult(Of CPCCatalog)

    ''' <summary>
    ''' Guarda o Actualiza un CPCCatalog
    ''' </summary>
    ''' <param name="CPCCatalog">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult(Of CPCCatalog)

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad.
    ''' </summary>
    ''' <param name="CPCCatalog">The CPCCatalog.</param>
    ''' <returns></returns>
    Function ChangeStatusCPCCatalog(CPCCatalog As CPCCatalog, status As Boolean, audit As AuditMessage) As ActionResult(Of CPCCatalog)

    ''' <summary>
    ''' Elimina  un CPCCatalog
    ''' </summary>
    ''' <param name="CPCCatalog">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult

End Interface
