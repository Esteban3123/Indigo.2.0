Imports Application.Maintenance
Imports DistributedServices.Maintenance
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Microsoft.Practices.Unity

Partial Public Class MaintanceService
    Implements IMaintenancePlanAndMetrologyService

    Public Function GetMaintenancePlanAndMetrology(PhysicalAssetId As Integer, protocolId As Integer) As MaintenancePlanAndMetrology Implements IMaintenancePlanAndMetrologyService.GetMaintenancePlanAndMetrology
        Using service As IMaintenancePlanAndMetrologyAdminService = Container.Current.Resolve(Of IMaintenancePlanAndMetrologyAdminService)()
            Return service.GetMaintenancePlanAndMetrology(PhysicalAssetId, protocolId)
        End Using
    End Function

    Public Function SaveMaintenancePlanAndMetrology(maintenancePlan As MaintenancePlanAndMetrology) As ActionResult Implements IMaintenancePlanAndMetrologyService.SaveMaintenancePlanAndMetrology
        Using service As IMaintenancePlanAndMetrologyAdminService = Container.Current.Resolve(Of IMaintenancePlanAndMetrologyAdminService)()
            Return service.SaveMaintenancePlanAndMetrology(maintenancePlan)
        End Using
    End Function

    Public Function GetMaintenancePlanAndMetrologyById(PlanMaintenanceId As Integer) As MaintenancePlanAndMetrology Implements IMaintenancePlanAndMetrologyService.GetMaintenancePlanAndMetrologyById
        Using service As IMaintenancePlanAndMetrologyAdminService = Container.Current.Resolve(Of IMaintenancePlanAndMetrologyAdminService)()
            Return service.GetMaintenancePlanAndMetrologyById(PlanMaintenanceId)
        End Using
    End Function

    Public Function DeleteMaintenancePlanAndMetrology(id As List(Of Integer)) As ActionResult Implements IMaintenancePlanAndMetrologyService.DeleteMaintenancePlanAndMetrology
        Using service As IMaintenancePlanAndMetrologyAdminService = Container.Current.Resolve(Of IMaintenancePlanAndMetrologyAdminService)()
            Return service.DeleteMaintenancePlanAndMetrology(id)
        End Using
    End Function
End Class
