Imports Domain.Base.Entities
Imports Domain.Entities

Public Interface IMaintenancePlanAndMetrologyAdminService
    Inherits IDisposable

    Function GetMaintenancePlanAndMetrology(PhysicalAssetId As Integer, protocolId As Integer) As MaintenancePlanAndMetrology

    Function SaveMaintenancePlanAndMetrology(maintenancePlan As MaintenancePlanAndMetrology) As ActionResult

    Function GetMaintenancePlanAndMetrologyById(PlanMaintenanceId As Integer) As MaintenancePlanAndMetrology
    Function DeleteMaintenancePlanAndMetrology(id As List(Of Integer)) As ActionResult
End Interface
