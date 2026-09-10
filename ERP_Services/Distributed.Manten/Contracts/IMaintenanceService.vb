'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Oscar Sierra
' Created          : 04-08-2013
'
' Last Modified By : 04-08-2013
' Last Modified On : 11-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
#End Region
<ServiceContract()> _
Public Interface IMaintenanceService
    'Inherits IMaintenanceInsurance, IBranchService, IEquipmentTypeService, IInventoryTypeService
    Inherits ISupplierService, IAccesoryService, IConsumableService, IEquipmentService, IMeasurementUnitService, ITechnicalLogService, IEquipmentRegistrationService
    Inherits ITemplateEquipmentTypeService, IEquipmentFunctionService, IEquipmentHistoryService, IEquipmentRequirementService, IPhysicalRiskService, IBrandService, IMaintenanceSequense, IBlockRecordMaintenanceService
    Inherits IMaintenanceParameterService, IMaintenancePlanAndMetrologyService
    Inherits IMaintenancePlanService, IMaintenanceProtocolService, IWorkOrderService
    Inherits IMaintenanceResponsibleService, IMaintenanceFailureRequestService, IMaintenanceContractService, IMaintenanceServiceReports, IMaintenanceToolsService, IMaintenanceManufacturersService
End Interface
