
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities

<ServiceContract()>
Public Interface IMaintenancePlanAndMetrologyService

    <OperationContract()>
    Function GetMaintenancePlanAndMetrology(PhysicalAssetId As Integer, protocolId As Integer) As MaintenancePlanAndMetrology

    <OperationContract()>
    Function SaveMaintenancePlanAndMetrology(maintenancePlan As MaintenancePlanAndMetrology) As ActionResult

    <OperationContract()>
    Function GetMaintenancePlanAndMetrologyById(PlanMaintenanceId As Integer) As MaintenancePlanAndMetrology

    <OperationContract()>
    Function DeleteMaintenancePlanAndMetrology(id As List(Of Integer)) As ActionResult

End Interface
