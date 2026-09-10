'***********************************************************************
' Assembly         : Domain.Maintenance
' Author           : Diego Andrés Roldán Lozano
' Created          : 2018-09-14
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Public Interface IMaintenancePlanAndMetrologyRepository
    Inherits IRepository(Of MaintenancePlanAndMetrology)

    Function GetMaintenancePlanAndMetrology(PhysicalAssetId As Integer, MaintenanceprotocolId As Integer) As MaintenancePlanAndMetrology
    Function GetMaintenancePlanAndMetrologyById(PlanMaintenanceId As Integer) As MaintenancePlanAndMetrology
    Function RemoveRange(ids As List(Of Integer)) As Boolean
    Function SP_DeleteMaintenanceProgramed(maintenancePlanAndMetrologyIds As String) As SP_DeleteMaintenanceProgramed_Result
End Interface
