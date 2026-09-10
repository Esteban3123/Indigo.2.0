#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Obtiene un CPCCatalog por Id
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Function GetCPCCatalogById(Id As Integer, audit As AuditMessage) As ActionResult(Of CPCCatalog) Implements IBudgetServiceCPCCatalog.GetCPCCatalogById
        Using service As ICPCCatalogAdminService = Container.Current.Resolve(Of ICPCCatalogAdminService)()
            Return service.GetCPCCatalogById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un CPCCatalog por código
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Function GetCPCCatalogByCode(Code As String, audit As AuditMessage) As ActionResult(Of CPCCatalog) Implements IBudgetServiceCPCCatalog.GetCPCCatalogByCode
        Using service As ICPCCatalogAdminService = Container.Current.Resolve(Of ICPCCatalogAdminService)()
            Return service.GetCPCCatalogByCode(Code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza 
    ''' </summary>
    ''' <param name="CPCCatalog">la entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult(Of CPCCatalog) Implements IBudgetServiceCPCCatalog.SaveCPCCatalog
        Using service As ICPCCatalogAdminService = Container.Current.Resolve(Of ICPCCatalogAdminService)()
            Return service.SaveCPCCatalog(CPCCatalog, audit)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="CPCCatalog">The CPCCatalog.</param>
    ''' <returns></returns>
    Public Function ChangeStatusCPCCatalog(CPCCatalog As CPCCatalog, status As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CPCCatalog) Implements IBudgetServiceCPCCatalog.ChangeStatusCPCCatalog
        Using service As ICPCCatalogAdminService = Container.Current.Resolve(Of ICPCCatalogAdminService)()
            Return service.ChangeStatusCPCCatalog(CPCCatalog, status, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="CPCCatalog">La entidad</param>
    ''' <param name="audit">The session.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult Implements IBudgetServiceCPCCatalog.DeleteCPCCatalog
        Using service As ICPCCatalogAdminService = Container.Current.Resolve(Of ICPCCatalogAdminService)()
            Return service.DeleteCPCCatalog(CPCCatalog, audit)
        End Using
    End Function
End Class