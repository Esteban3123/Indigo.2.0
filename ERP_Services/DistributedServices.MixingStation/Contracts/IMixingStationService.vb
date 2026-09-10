'***********************************************************************
' Assembly         : DistributedServices.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 12/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel

<ServiceContract()>
Public Interface IMixingStationService
    Inherits IMixingStationServiceTurn, IMixingStationServiceSequence, IMixingStationBlockRecordMixingStation, IMixingStationServiceCMConfig,
        IMixingStationServiceUnitDoseType, IMixingStationServicePackage, IMixingStationServicePackageDetail, IMixingStationCenterAttention,
        IMixingStationServiceProductionLine, IMixingStationServiceStabilityTable,
        IMixingStationServiceCauseReprocessingRejection, IMixingStationServicePatientExternalCareCenter,
        IMixingStationServiceProductionBaskets, IMixingStationServiceTransportationAssistant, IMixingStationServiceTransportation, IMixingStationServiceContractExternalClients,
        IMixingStationServiceExternalCareCenter, IMixingStationServiceConfirmationUnitDose, IMixingStationServiceRequestUnitDoseInventory,
        IMixingStationServiceRequestUnitDoseExternalCareCenter, IMixingStationServiceRequestMixingStation,
        IMixingStationServiceProductionSchedule, IMixingStationServiceCampaign, IMixingStationServiceRawMaterial, IMixingStationServiceRawMaterialDevolution, IMixingStationServiceQuantityRemaining, IMixingStationServiceHarnessed,
        IMixingStationServiceCampaignKardex, IMixingStationSettingService, IMixingStationServiceQualityControl, IMixingStationServiceCampaignReports, IMixingStationServiceNPTConfiguration, IBatchSerialSettingService, IMixingStationServiceReadjustments, IMixingStationServiceCategoryDefects,
        IMixingStationServiceDilutionFactors, IMixingStationServiceDefectClassificationItem, IMixingStationServicelineClearanceCriteria

End Interface
