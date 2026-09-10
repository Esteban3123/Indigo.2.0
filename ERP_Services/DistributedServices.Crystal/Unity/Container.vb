#Region "Imports"

Imports Application.Billing
Imports Application.Common
Imports Application.Crystal
Imports Domain.Crystal
Imports Domain.Crystal.Service
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.PayrollRepository
Imports Microsoft.Practices.Unity
Imports Infrastructure.CrossCutting.Queue
Imports Infrastructure.Data.SecurityRepository
Imports Domain.Security
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
            Dim container As String = If(String.IsNullOrEmpty(containerInfo?.container), SessionValues.Instance.TransactionalContainer, containerInfo?.container)
            Dim hisContainer As String = If(String.IsNullOrEmpty(containerInfo?.container), SessionValues.Instance.HisContainer, containerInfo?.hisContainer)

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

        'Inyectamos el contexto global
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))
        'Inyectamos el contexto de Crystal
        newContainer.RegisterType(Of ICrystalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                        Return New CrystalModelUnitOfWork(hisContainer)
                                                                                                                    End Function))

        'Inyectamos el contexto de payroll
        newContainer.RegisterType(Of IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                   Return New PayrollUnitOfWork(container)
                                                                                                               End Function))

        'Contexto de seguridad
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New GenesisEntities()
                                                                                                                 End Function))

        'Inyectamos el contexto de Glosas
        'newContainer.RegisterType(Of IGlosasUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
        '                                                                                                               Return New GENESISEntitiesGlosas(ServerSessionValues.Current.CurrentContainer)
        '                                                                                                           End Function))

        Dim injector As New ModuleInjector()
        injector.Load(newContainer, GetType(Domain.Base.Inject))

        'Entidades
        newContainer.RegisterType(Of ICrystalEntityRepository, CrystalEntityRepository)()
        'IProcedureCupsRepository
        newContainer.RegisterType(Of IProcedureCupsRepository, ProcedureCupsRepository)()
        newContainer.RegisterType(Of IDefinitionRateDetailRepository, DefinitionRateDetailRepository)()
        'caregroupDefinitionRate
        newContainer.RegisterType(Of ICareGroupDefinitionRateRepository, CareGroupDefinitionRateRepository)()
        'DefinitionRateDetailCondition
        newContainer.RegisterType(Of IDefinitionRateDetailConditionRepository, DefinitionRateDetailConditionRepository)()
        'Secuencias numericas
        newContainer.RegisterType(Of IBillingSequenceRepository, BillingSequenceRepository)()
        newContainer.RegisterType(Of IBillingSequenceDetailRepository, BillingSequenceDetailRepository)()

        'BillingConcept
        newContainer.RegisterType(Of IBillingConceptRepository, BillingConceptRepository)()

        'Secuencias numericas
        newContainer.RegisterType(Of ISequenseTreasuryCRepository, SequenseTreasuryCRepository)()
        newContainer.RegisterType(Of ISequenseTreasuryDRepository, SequenseTreasuryDRepository)()
        'Estancias
        newContainer.RegisterType(Of IParameterRepository, ParameterRepository)()
        newContainer.RegisterType(Of IStayRepository, StayRepository)()
        newContainer.RegisterType(Of IStayAdminService, StayAdminService)()

        newContainer.RegisterType(Of IContractExternalClientsRepository, ContractExternalClientsRepository)()

        newContainer.RegisterType(Of ICareGroupRepository, CareGroupRepository)()
        newContainer.RegisterType(Of IRateManualRepository, RateManualRepository)()
        newContainer.RegisterType(Of ICupsHomologationRepository, CupsHomologationRepository)()
        newContainer.RegisterType(Of IFunctionalUnitRepository, FunctionalUnitRepository)()
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()
        newContainer.RegisterType(Of ICupsEntityRepository, CupsEntityRepository)()
        'CUPSEntityContractDescriptionsRepository
        newContainer.RegisterType(Of ICupsEntityContractDescriptionsRepository, CUPSEntityContractDescriptionsRepository)()

        'ContractPackageServiceRepository
        newContainer.RegisterType(Of IContractPackageServiceRepository, ContractPackageServiceRepository)()
        newContainer.RegisterType(Of IIPSServicesRepository, IPSServicesRepository)()
        newContainer.RegisterType(Of ISurgicalProcedureServiceRepository, SurgicalProcedureServiceRepository)()
        newContainer.RegisterType(Of IRateManualDetailSurgicalRepository, RateManualDetailSurgicalRepository)()
        newContainer.RegisterType(Of IRateManualDetailRepository, RateManualDetailRepository)()
        newContainer.RegisterType(Of IRevenueControlRepository, RevenueControlRepository)()
        newContainer.RegisterType(Of IRevenueControlDetailRepository, RevenueControlDetailRepository)()
        newContainer.RegisterType(Of IAdmissionRepository, AdmissionRepository)()
        newContainer.RegisterType(Of IServiceOrderRepository, ServiceOrderRepository)()
        newContainer.RegisterType(Of IHCREGEGRERepository, HCREGEGRERepository)()
        newContainer.RegisterType(Of IServiceOrderDetailRepository, ServiceOrderDetailRepository)()
        newContainer.RegisterType(Of IServiceOrderDetailDistributionRepository, ServiceOrderDetailDistributionRepository)()
        newContainer.RegisterType(Of IHospitalInventoryRepository, HospitalInventoryRepository)()
        newContainer.RegisterType(Of IMedicalPrescriptionRepository, MedicalPrescriptionRepository)()
        newContainer.RegisterType(Of IPhysicalInventoryCrystalRepository, PhysicalInventoryCrystalRepository)()
        newContainer.RegisterType(Of IBillingSequenceRepository, BillingSequenceRepository)()
        newContainer.RegisterType(Of IBillingSequenceDetailRepository, BillingSequenceDetailRepository)()
        newContainer.RegisterType(Of IBillingSequenseAdminService, BillingSequenseAdminService)()
        newContainer.RegisterType(Of ICrossingAccountDetailOtherConceptsRepository, CrossingAccountDetailOtherConceptsRepository)()

        newContainer.RegisterType(Of IBedRepository, BedRepository)()
        newContainer.RegisterType(Of IStayDetailRepository, StayDetailRepository)()

        newContainer.RegisterType(Of IExternalConsultationRepository, ExternalConsultationRepository)()
        newContainer.RegisterType(Of IExternalConsultationAdminService, ExternalConsultationAdminService)()
        newContainer.RegisterType(Of IInvoiceRepository, InvoiceRepository)()
        newContainer.RegisterType(Of IBillingInvoiceDetailRepository, BillingInvoiceDetailRepository)()

        newContainer.RegisterType(Of IBranchOfficeRepository, BranchOfficeRepository)()

        'IHCORDLABORepository
        newContainer.RegisterType(Of IHCORDLABORepository, HCORDLABORepository)()

        newContainer.RegisterType(Of ICHTIPESTARepository, CHTIPESTARepository)()
        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()
        'newContainer.RegisterType(Of IAGCITASERIPSRepository, AGCITASERIPSRepository)()
        newContainer.RegisterType(Of IINCUPSIPSRepository, INCUPSIPSRepository)()
        newContainer.RegisterType(Of IAGACTMDDDRepository, AGACTMDDDRepository)()
        newContainer.RegisterType(Of IHCUNITHISRepository, HCUNITHISRepository)()
        newContainer.RegisterType(Of IHCUNITHISAdminService, HCUNITHISAdminService)()
        newContainer.RegisterType(Of IHCFARMEPCRepository, HCFARMEPCRepository)()
        newContainer.RegisterType(Of IHCINTESERRepository, HCINTESERRepository)()
        newContainer.RegisterType(Of IHCPARPACSRepository, HCPARPACSRepository)()
        newContainer.RegisterType(Of IHCJUNOPMHRepository, HCJUNOPMHRepository)()
        newContainer.RegisterType(Of IHCQXREALIRepository, HCQXREALIRepository)()
        newContainer.RegisterType(Of IHCQXEQUIPRepository, HCQXEQUIPRepository)()
        newContainer.RegisterType(Of IHCHISPACARepository, HCHISPACARepository)()


        newContainer.RegisterType(Of IINPACIENTTOPANURepository, INPACIENTTOPANURepository)()

        'Product Groups
        newContainer.RegisterType(Of IProductGroupsRepository, ProductGroupsRepository)()

        newContainer.RegisterType(Of IIPSServiceGroupRepository, IPSServiceGroupRepository)()

        'Paciente
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of Domain.Entities.IPersonRepository, Infrastructure.Data.ModelRepository.PersonRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IThirdPartyAdminService, ThirdPartyAdminService)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IPatientRepository, PatientRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IPatientAdminService, PatientAdminService)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IPatientConsecutiveRepository, PatientConsecutiveRepository)()

        newContainer.RegisterType(Of IStayAdminService, StayAdminService)()
        newContainer.RegisterType(Of IStayService, StayService)()
        newContainer.RegisterType(Of IBillingServices, BillingServices)()
        newContainer.RegisterType(Of IServiceOrderAdminService, ServiceOrderAdminService)()
        newContainer.RegisterType(Of IBasicBillingRepository, BasicBillingRepository)()

        'Tarifa de camas
        newContainer.RegisterType(Of IBedRateRepository, BedRateRepository)()
        newContainer.RegisterType(Of IBedRateAdminService, BedRateAdminService)()

        'Profesionales
        newContainer.RegisterType(Of IHealthCareProfessionalAdminService, HealthCareProfessionalAdminService)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IHealthCareProfessionalRepository, HealthCareProfessionalRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IMedicalFeesContractRepository, MedicalFeesContractRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository)()
        newContainer.RegisterType(Of ISupplierBankAccountRepository, SupplierBankAccountRepository)()

        newContainer.RegisterType(Of IHealthProfessionalContractRepository, HealthProfessionalContractRepository)()

        'DashboardPharmacy
        newContainer.RegisterType(Of IDashboardPharmacyDetailAdminService, DashboardPharmacyDetailAdminService)()
        newContainer.RegisterType(Of IDashboardPharmacyDetailRepository, DashboardPharmacyDetailRepository)()

        'DashboardPharmacyDevolution
        newContainer.RegisterType(Of IDashboardPharmacyDetailDevolutionAdminService, DashboardPharmacyDetailDevolutionAdminService)()
        newContainer.RegisterType(Of IDashboardPharmacyDetailDevolutionRepository, DashboardPharmacyDetailDevolutionRepository)()

        'DashboardPharmacySurgicalPackage
        newContainer.RegisterType(Of IDashboardPharmacyDetailSurgicalPackageRepository, DashboardPharmacyDetailSurgicalPackageRepository)()

        'ContractServices
        newContainer.RegisterType(Of IContractServices, ContractServices)()

        newContainer.RegisterType(Of IInvoicePortfolioAdvanceRepository, InvoicePortfolioAdvanceRepository)()


        'pharmacy
        newContainer.RegisterType(Of IPharmacyRepository, PharmacyRepository)()
        'devolution
        newContainer.RegisterType(Of IDevolutionMedicationDetailRepository, DevolutionMedicationDetailRepository)()
        'devolution detail
        newContainer.RegisterType(Of IDevolutionMedicationRepository, DevolutionMedicationRepository)()

        'parmacyDetail
        newContainer.RegisterType(Of IPharmacyDetailRepository, PharmacyDetailRepository)()
        'kardexCrystal
        newContainer.RegisterType(Of IKardexCrystalRepository, KardexCrystalRepository)()
        'ProductRateDetail
        newContainer.RegisterType(Of IProductRateDetailRepository, ProductRateDetailRepository)()
        'BillingAuthorization
        newContainer.RegisterType(Of IBillingAuthorizationRepository, BillingAuthorizationRepository)()
        'SettingsBilling
        newContainer.RegisterType(Of ISettingsBillingRepository, SettingsBillingRepository)()
        'HealthAdministrator
        newContainer.RegisterType(Of IHealthAdministratorRepository, HealthAdministratorRepository)()

        '*****ACOUNTING**********
        'MainAccoutns
        newContainer.RegisterType(Of IPUCRepository, PUCRepository)()


        '*******************COMMON*******************
        newContainer.RegisterType(Of ICustomerRepository, CustomerRepository)()

        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        '*****INVENTORY**********
        'InventoryProduct
        newContainer.RegisterType(Of IInventoryProductRepository, InventoryProductRepository)()
        'SettingsInventory
        newContainer.RegisterType(Of ISettingInventoryRepository, SettingInventoryRepository)()

        'Admissions
        newContainer.RegisterType(Of IAdmissionsAdminService, AdmissionsAdminService)()



        'consecutivos
        newContainer.RegisterType(Of Domain.Crystal.IConsecutiveRepository, Infrastructure.Data.CrystalRepository.ConsecutiveReporitory)()
        'niveles de pacientes
        newContainer.RegisterType(Of Domain.Crystal.ILevelPatientRepository, Infrastructure.Data.CrystalRepository.LevelPatientRepository)()
        'ciudades de comunes.
        newContainer.RegisterType(Of Domain.Entities.ICityRepository, Infrastructure.Data.ModelRepository.CityRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        newContainer.RegisterType(Of ISEGrolesuRepository, SEGrolesuRepository)()
        newContainer.RegisterType(Of ISEGgruusuRepository, SEGgruusuRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        newContainer.RegisterType(Of IRateManualValidityRepository, RateManualValidityRepository)()

        newContainer.RegisterType(Of IHCFARMEPDRepository, HCFARMEPDRepository)()
        newContainer.RegisterType(Of IRoutingLogRepository, RoutingLogRepository)()
        newContainer.RegisterType(Of ICompanySettingsRepository, CompanySettingsRepository)()
        newContainer.RegisterType(Of IAccountControlJustificationRepository, AccountControlJustificationRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()
        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)()
        newContainer.RegisterType(Of ICommonHealthProfessionalRepository, CommonHealthProfessionalRepository)()
        _currentContainer = newContainer

    End Sub

#End Region

End Class