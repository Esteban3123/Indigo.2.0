'***********************************************************************
' Assembly         : DistributedService.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IContractService
    Inherits IContractBlockRecordContract, IContractSequense, IContractCupsGroup, IContractCupsSubGroup, IContractCupsEntity, IContractMarketingUnit, IContractHealthAdministrator, IContractIPSService, IContractContract,
        IContractUVRRange, IContractServiceProcedureTemplate, IContractRateManual, IContractServiceRequirementTemplate, IContractSurgicalGroup, IContractServiceContractMinimumWage, IContractRateManualDetail,
        IContractContractEntity, IContractServiceIPSServiceGroup, IContractServiceCupsHomologation, IContractRateManualDetailSurgical, IContractServiceSurgicalProcedureService,
        IContractCareGroup, IContractServiceSurgeriesPercentageManual, IContractServiceDefinitionRate, IContractServiceDefinitionRateDetail, IContractContractAccountingStructure, IContractGroupers,
        IContractServiceImagingGroup, IContractContractDescriptions, IContractSettingsContract, IContractContractPackage, IContractRateManualValidity, IContractTechnicalNote, IContractCompanyType, IContractDiscountTypes, IContractServiceRIPSServiceGroups,
        IContractServiceBillingItemsRestriction, IContractServiceRIPSServices
End Interface
