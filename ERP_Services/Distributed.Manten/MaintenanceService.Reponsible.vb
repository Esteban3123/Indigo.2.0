Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Application.Maintenance

Partial Public Class MaintanceService

    ''' <summary>
    ''' eliminar las marcas
    ''' </summary>
    ''' <param name="Responsible"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteResponsible(Responsible As Domain.Entities.MaintenanceResponsible, audit As AuditMessage) As ActionResult Implements IMaintenanceResponsibleService.DeleteResponsible
        Using service As IMaintenanceResponsibleAdminService = Container.Current.Resolve(Of IMaintenanceResponsibleAdminService)()
            Return service.DeleteResponsible(Responsible, audit)
        End Using
        'Return _MaintenanceResponsibleAdminService.DeleteResponsible(Responsible, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetResponsibleByCode(Code As String) As Domain.Entities.MaintenanceResponsible Implements IMaintenanceResponsibleService.GetResponsibleByCode
        Using service As IMaintenanceResponsibleAdminService = Container.Current.Resolve(Of IMaintenanceResponsibleAdminService)()
            Return service.GetResponsibleByCode(Code)
        End Using
        'Return _MaintenanceResponsibleAdminService.GetResponsibleByCode(Code)
    End Function

    Public Function GetItemCatalogByCode(Code As String) As FixedAssetItemCatalog Implements IMaintenanceResponsibleService.GetItemCatalogByCode
        Using service As IMaintenanceResponsibleAdminService = Container.Current.Resolve(Of IMaintenanceResponsibleAdminService)()
            Return service.GetItemCatalogByCode(Code)
        End Using
        'Return _MaintenanceResponsibleAdminService.GetFunctionalUnitByCode(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllResponsible(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceResponsible) Implements IMaintenanceResponsibleService.ListAllResponsible
        Using service As IMaintenanceResponsibleAdminService = Container.Current.Resolve(Of IMaintenanceResponsibleAdminService)()
            Return service.ListAllResponsible()
        End Using
        'Return _MaintenanceResponsibleAdminService.ListAllResponsible()
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ResponsibleType">Objeto ResponsibleType</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveResponsible(Responsible As Domain.Entities.MaintenanceResponsible, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceResponsible) Implements IMaintenanceResponsibleService.SaveResponsible
        Using service As IMaintenanceResponsibleAdminService = Container.Current.Resolve(Of IMaintenanceResponsibleAdminService)()
            Return service.SaveResponsible(Responsible, audit, idSequense)
        End Using
        'Return _MaintenanceResponsibleAdminService.SaveResponsible(Responsible, audit, idSequense)
    End Function

    Public Function GetItemCatalogByIdUser(IdResponsible As Integer) As List(Of ResponsibleCatalogOfArticles) Implements IMaintenanceResponsibleService.GetItemCatalogByIdUser
        Using service As IMaintenanceResponsibleAdminService = Container.Current.Resolve(Of IMaintenanceResponsibleAdminService)()
            Return service.GetItemCatalogByIdUser(IdResponsible)
        End Using
        'Return _MaintenanceResponsibleAdminService.GetFuncionalUnitByIdUser(IdResponsible)
    End Function

    Public Function ChangeStateMaintenanceResponsible(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of MaintenanceResponsible) Implements IMaintenanceResponsibleService.ChangeStateMaintenanceResponsible
        Using service As IMaintenanceResponsibleAdminService = Container.Current.Resolve(Of IMaintenanceResponsibleAdminService)()
            Return service.ChangeStateMaintenanceResponsible(Code, State, audit)
        End Using
        'Return _MaintenanceResponsibleAdminService.ChangeStateMaintenanceResponsible(Code, State, audit)
    End Function
End Class
