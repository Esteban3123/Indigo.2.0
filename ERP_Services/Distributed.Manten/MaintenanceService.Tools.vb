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
    ''' <param name="Tools"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteTools(Tools As Domain.Entities.MaintenanceTools, audit As AuditMessage) As ActionResult Implements IMaintenanceToolsService.DeleteTools
        Using service As IMaintenanceToolsAdminService = Container.Current.Resolve(Of IMaintenanceToolsAdminService)()
            Return service.DeleteTools(Tools, audit)
        End Using
        'Return _MaintenanceToolsAdminService.DeleteTools(Tools, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetToolsByCode(Code As String) As Domain.Entities.MaintenanceTools Implements IMaintenanceToolsService.GetToolsByCode
        Using service As IMaintenanceToolsAdminService = Container.Current.Resolve(Of IMaintenanceToolsAdminService)()
            Return service.GetToolsByCode(Code)
        End Using
        'Return _MaintenanceToolsAdminService.GetToolsByCode(Code)
    End Function

    Public Function GetItemByCode(Code As String) As FixedAssetItem Implements IMaintenanceToolsService.GetItemByCode
        Using service As IMaintenanceToolsAdminService = Container.Current.Resolve(Of IMaintenanceToolsAdminService)()
            Return service.GetItemByCode(Code)
        End Using
        'Return _MaintenanceToolsAdminService.GetFunctionalUnitByCode(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllTools(audit As AuditMessage) As List(Of Domain.Entities.MaintenanceTools) Implements IMaintenanceToolsService.ListAllTools
        Using service As IMaintenanceToolsAdminService = Container.Current.Resolve(Of IMaintenanceToolsAdminService)()
            Return service.ListAllTools()
        End Using
        'Return _MaintenanceToolsAdminService.ListAllTools()
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveTools(Tools As Domain.Entities.MaintenanceTools, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MaintenanceTools) Implements IMaintenanceToolsService.SaveTools
        Using service As IMaintenanceToolsAdminService = Container.Current.Resolve(Of IMaintenanceToolsAdminService)()
            Return service.SaveTools(Tools, audit, idSequense)
        End Using
        'Return _MaintenanceToolsAdminService.SaveTools(Tools, audit, idSequense)
    End Function

    Public Function GetItemDetailByIdUser(IdTools As Integer) As List(Of MaintenanceToolsItemDetail) Implements IMaintenanceToolsService.GetItemDetailByIdUser
        Using service As IMaintenanceToolsAdminService = Container.Current.Resolve(Of IMaintenanceToolsAdminService)()
            Return service.GetItemDetailByIdUser(IdTools)
        End Using
        'Return _MaintenanceToolsAdminService.GetFuncionalUnitByIdUser(IdTools)
    End Function

End Class
