#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports Infrastructure.CrossCutting.Base
Imports Application.Contract
Imports Infrastructure.Data.CrystalRepository
Imports Domain.Crystal
Imports Domain.Entities.Service
Imports System.ServiceModel
Imports Domain.Security
Imports Infrastructure.Data.SecurityRepository
Imports Infrastructure.CrossCutting.Queue
Imports DistributedServices.Authentication

#End Region

Public NotInheritable Class Container

#Region "Singleton"

    ''' <summary>
    ''' Unica instancia del contenedor
    ''' </summary>
    Private Shared _currentContainer As IUnityContainer

    ''' <summary>
    ''' Obtiene la unica instancia del contenedor
    ''' </summary>
    ''' <returns>Contenedor configurado</returns>
    Public Shared ReadOnly Property Current() As IUnityContainer
        Get
            Dim containerInfo = JwtFactory.GetContainerFromToken()
            Dim container As String = containerInfo?.container
            Dim hisContainer As String = containerInfo?.hisContainer

            If _currentContainer IsNot Nothing Then
                Dim sessionVariables As ICommonVariables = _currentContainer.Resolve(Of ICommonVariables)()
                If sessionVariables.getContainer().Equals(container) Then
                    Return _currentContainer
                End If
            End If


            ConfigureContainer(container, hisContainer)

            Return _currentContainer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configura las dependencias en el contenedor
    ''' </summary>
    Private Shared Sub ConfigureContainer(container As String, hisContainer As String)
        Dim newContainer = New UnityContainer()

        newContainer.RegisterType(Of ICommonVariables)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                 Return New CommonVariables(container, hisContainer)
                                                                                                             End Function))
        'Inyectamos el contexto
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))
        'Inyectamos el contexto de Crystal
        newContainer.RegisterType(Of ICrystalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                        Return New CrystalModelUnitOfWork(hisContainer)
                                                                                                                    End Function))
        'Contexto de seguridad
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New GenesisEntities()
                                                                                                                 End Function))

        'Inyectamos el contexto de payroll
        newContainer.RegisterType(Of Infrastructure.Data.PayrollRepository.IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                         Return New Infrastructure.Data.PayrollRepository.PayrollUnitOfWork(container)
                                                                                                                                                     End Function))
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IContractService, ContractService)()
        'Secuencias numericas
        newContainer.RegisterType(Of IContractSequenseAdminService, ContractSequenseAdminService)()
        newContainer.RegisterType(Of ISequenseContractCRepository, SequenseContractCRepository)()
        newContainer.RegisterType(Of ISequenseContractDRepository, SequenseContractDRepository)()
        'caregroupDefinitionRate
        newContainer.RegisterType(Of ICareGroupDefinitionRateRepository, CareGroupDefinitionRateRepository)()
        'DefinitionRateDetailCondition
        newContainer.RegisterType(Of IDefinitionRateDetailConditionRepository, DefinitionRateDetailConditionRepository)()

        'BlockRecord
        newContainer.RegisterType(Of IBlockRecordContractAdminService, BlockRecordContractAdminService)()
        newContainer.RegisterType(Of IBlockRecordContractRepository, BlockRecordContractRepository)()
        'CupsGroup
        newContainer.RegisterType(Of ICupsGroupAdminService, CupsGroupAdminService)()
        newContainer.RegisterType(Of ICupsGroupRepository, CupsGroupRepository)()
        'CupsSubGroup
        newContainer.RegisterType(Of ICupsSubGroupAdminService, CupsSubGroupAdminService)()
        newContainer.RegisterType(Of ICupsSubGroupRepository, CupsSubGroupRepository)()
        'CupsEntity
        newContainer.RegisterType(Of ICupsEntityAdminService, CupsEntityAdminService)()
        newContainer.RegisterType(Of ICupsEntityRepository, CupsEntityRepository)()

        'CUPSEntityContractDescriptionsRepository
        newContainer.RegisterType(Of ICupsEntityContractDescriptionsRepository, CUPSEntityContractDescriptionsRepository)()

        'ContractPackageServiceRepository
        newContainer.RegisterType(Of IContractPackageServiceRepository, ContractPackageServiceRepository)()

        'MarketingUnit
        newContainer.RegisterType(Of IMarketingUnitAdminService, MarketingUnitAdminService)()
        newContainer.RegisterType(Of IMarketingUnitRepository, MarketingUnitRepository)()
        'HealthAdministrator
        newContainer.RegisterType(Of IHealthAdministratorAdminService, HealthAdministratorAdminService)()
        newContainer.RegisterType(Of IHealthAdministratorRepository, HealthAdministratorRepository)()
        'IPSService
        newContainer.RegisterType(Of IIPSServiceAdminService, IPSServiceAdminService)()
        newContainer.RegisterType(Of IIPSServicesRepository, IPSServicesRepository)()
        'Contract
        newContainer.RegisterType(Of IContractAdminService, ContractAdminService)()
        newContainer.RegisterType(Of IContractRepository, ContractRepository)()
        'SurgicalProcedureService
        newContainer.RegisterType(Of ISurgicalProcedureServiceAdminService, SurgicalProcedureServiceAdminService)()
        newContainer.RegisterType(Of ISurgicalProcedureServiceRepository, SurgicalProcedureServiceRepository)()
        'CupsHomologation
        newContainer.RegisterType(Of ICupsHomologationAdminService, CupsHomologationAdminService)()
        newContainer.RegisterType(Of ICupsHomologationRepository, CupsHomologationRepository)()
        'UVRRange
        newContainer.RegisterType(Of IUVRRangeAdminService, UVRRangeAdminService)()
        newContainer.RegisterType(Of IUVRRangeRepository, UVRRangeRepository)()
        'ProcedurTemplatee
        newContainer.RegisterType(Of IProcedureTemplateAdminService, ProcedureTemplateAdminService)()
        newContainer.RegisterType(Of IProcedureTemplateRepository, ProcedureTemplateRepository)()
        'ProcedureCups
        newContainer.RegisterType(Of IProcedureCupsRepository, ProcedureCupsRepository)()
        'RateManual
        newContainer.RegisterType(Of IRateManualAdminService, RateManualAdminService)()
        newContainer.RegisterType(Of IRateManualRepository, RateManualRepository)()
        'RequirementTemplate
        newContainer.RegisterType(Of IRequirementTemplateAdminService, RequirementTemplateAdminService)()
        newContainer.RegisterType(Of IRequirementTemplateRepository, RequirementTemplateRepository)()
        'SurgicalGroup
        newContainer.RegisterType(Of ISurgicalGroupAdminService, SurgicalGroupAdminService)()
        newContainer.RegisterType(Of ISurgicalGroupRepository, SurgicalGroupRepository)()
        'ContractMinimumWage
        newContainer.RegisterType(Of IContractMinimumWageAdminService, ContractMinimumWageAdminService)()
        newContainer.RegisterType(Of IContractMinimumWageRepository, ContractMinimumWageRepository)()
        'RateManualDetail
        newContainer.RegisterType(Of IRateManualDetailAdminService, RateManualDetailAdminService)()
        newContainer.RegisterType(Of IRateManualDetailRepository, RateManualDetailRepository)()
        'ContractEntity
        newContainer.RegisterType(Of IContractEntityAdminService, ContractEntityAdminService)()
        newContainer.RegisterType(Of IContractEntityRepository, ContractEntityRepository)()
        'IPSServiceGroup
        newContainer.RegisterType(Of IIPSServiceGroupAdminService, IPSServiceGroupAdminService)()
        newContainer.RegisterType(Of IIPSServiceGroupRepository, IPSServiceGroupRepository)()

        'RateManualDetailSurgical
        newContainer.RegisterType(Of IRateManualDetailSurgicalAdminService, RateManualDetailSurgicalAdminService)()
        newContainer.RegisterType(Of IRateManualDetailSurgicalRepository, RateManualDetailSurgicalRepository)()
        'CareGroup
        newContainer.RegisterType(Of ICareGroupAdminService, CareGroupAdminService)()
        newContainer.RegisterType(Of ICareGroupRepository, CareGroupRepository)()


        newContainer.RegisterType(Of ISpecialityRepository, SpecialityRepository)()
        newContainer.RegisterType(Of IInvoicePortfolioAdvanceRepository, InvoicePortfolioAdvanceRepository)()

        newContainer.RegisterType(Of IBillingSequenceDetailRepository, BillingSequenceDetailRepository)()

        'SurgeriesPercentageManual
        newContainer.RegisterType(Of ISurgeriesPercentageManualAdminService, SurgeriesPercentageManualAdminService)()
        newContainer.RegisterType(Of ISurgeriesPercentageManualRepository, SurgeriesPercentageManualRepository)()

        'DefinitionRate
        newContainer.RegisterType(Of IDefinitionRateAdminService, DefinitionRateAdminService)()
        newContainer.RegisterType(Of IDefinitionRateRepository, DefinitionRateRepository)()
        'DefinitionRateDetail
        newContainer.RegisterType(Of IDefinitionRateDetailAdminService, DefinitionRateDetailAdminService)()
        newContainer.RegisterType(Of IDefinitionRateDetailRepository, DefinitionRateDetailRepository)()

        'ContractAccountingStructure
        newContainer.RegisterType(Of IContractAccountingStructureAdminService, ContractAccountingStructureAdminService)()
        newContainer.RegisterType(Of IContractAccountingStructureRepository, ContractAccountingStructureRepository)()

        'ContractServices
        newContainer.RegisterType(Of IContractServices, ContractServices)()
        'Functional Unit
        newContainer.RegisterType(Of Domain.Payroll.IFunctionalUnitRepository, Infrastructure.Data.PayrollRepository.FunctionalUnitRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        'Groupers
        newContainer.RegisterType(Of IGroupersAdminService, GroupersAdminService)()
        newContainer.RegisterType(Of IGroupersRepository, GroupersRepository)()

        'ImagingGroup
        newContainer.RegisterType(Of IImagingGroupAdminService, ImagingGroupAdminService)()
        newContainer.RegisterType(Of IRISGRIMAGERepository, RISGRIMAGERepository)()

        'ContractDescriptions
        newContainer.RegisterType(Of IContractDescriptionsAdminService, ContractDescriptionsAdminService)()
        newContainer.RegisterType(Of IContractDescriptionsRepository, ContractDescriptionsRepository)()

        'SettingsContract
        newContainer.RegisterType(Of ISettingsContractAdminService, SettingsContractAdminService)()
        newContainer.RegisterType(Of ISettingsContractRepository, SettingsContractRepository)()

        'Vigencia Manual de Tarifas
        newContainer.RegisterType(Of IRateManualValidityAdminService, RateManualValidityAdminService)()
        newContainer.RegisterType(Of IRateManualValidityRepository, RateManualValidityRepository)()

        newContainer.RegisterType(Of ISEGrolesuRepository, SEGrolesuRepository)()
        newContainer.RegisterType(Of ISEGgruusuRepository, SEGgruusuRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        'ContractPackage
        newContainer.RegisterType(Of IContractPackageAdminService, ContractPackageAdminService)()
        newContainer.RegisterType(Of IContractPackageRepository, ContractPackageRepository)()
        newContainer.RegisterType(Of IInventoryProductRepository, InventoryProductRepository)()

        'TechnicalNote
        newContainer.RegisterType(Of ITechnicalNoteAdminService, TechnicalNoteAdminService)()
        newContainer.RegisterType(Of ITechnicalNoteRepository, TechnicalNoteRepository)()

        newContainer.RegisterType(Of IAGACTIMEDRepository, AGACTIMEDRepository)()

        'CompanyType
        newContainer.RegisterType(Of ICompanyTypeAdminService, CompanyTypeAdminService)()
        newContainer.RegisterType(Of ICompanyTypeRepository, CompanyTypeRepository)()

        'DiscountTypes
        newContainer.RegisterType(Of IDiscountTypesAdminService, DiscountTypesAdminService)()
        newContainer.RegisterType(Of IDiscountTypesRepository, DiscountTypesRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        'ServiceOrderDetail
        newContainer.RegisterType(Of IServiceOrderDetailRepository, ServiceOrderDetailRepository)()

        'RIPSServiceGroups
        newContainer.RegisterType(Of IRIPSServiceGroupsAdminService, RIPSServiceGroupsAdminService)()
        newContainer.RegisterType(Of IRIPSServiceGroupsRepository, RIPSServiceGroupsRepository)()

        'BillingItemsRestriction
        newContainer.RegisterType(Of IBillingItemsRestrictionRepository, BillingItemsRestrictionRepository)()
        newContainer.RegisterType(Of IBillingItemsRestrictionAdminService, BillingItemsRestrictionAdminService)()
        newContainer.RegisterType(Of IBillingItemsRestrictionDetailRepository, BillingItemsRestrictionDetailRepository)()
        'RIPSServices
        newContainer.RegisterType(Of IRIPSServicesAdminService, RIPSServicesAdminService)()
        newContainer.RegisterType(Of IRIPSServicesRepository, RIPSServicesRepository)()

        'IndigoQueue
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)()
        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class