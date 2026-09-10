#Region "Imports"

Imports System.ServiceModel
Imports Application.Accounting
Imports Application.Inventory.InventoryAdjustment
Imports Application.Inventory.InventoryProduct
Imports Application.Inventory.InventoryRequest
Imports Application.Inventory.PhysicalInventory
Imports Application.Inventory.Sequense
Imports Application.Inventory.TransferOrder
Imports Application.Inventory.TransferOrderDetail
Imports Application.Security
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Domain.Security
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Queue
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.PayrollRepository
Imports Infrastructure.Data.SecurityRepository
Imports Microsoft.Practices.Unity
Imports Microsoft.Extensions.Caching.Memory
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
                                                                                                                     Return New Infrastructure.Data.SecurityRepository.GenesisEntities()
                                                                                                                 End Function))
        'Inyectamos el contexto de payroll
        newContainer.RegisterType(Of IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                   Return New PayrollUnitOfWork(container)
                                                                                                               End Function))

        newContainer.RegisterType(Of IFactoryStorage)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                Return New FactoryStorage()
                                                                                                            End Function))
        newContainer.RegisterType(Of IMemoryCache, MemoryCache)(
            New ContainerControlledLifetimeManager(),  ' Singleton
            New InjectionFactory(Function(c) New MemoryCache(New MemoryCacheOptions()))
        )

        Dim injector As New ModuleInjector()
        injector.Load(newContainer, GetType(Domain.Base.Inject))

        newContainer.RegisterType(Of ICupsEntityRepository, CupsEntityRepository)()
        newContainer.RegisterType(Of IPOSPathologiesRepository, POSPathologiesRepository)()
        newContainer.RegisterType(Of IIHLISTPRORepository, IHLISTPRORepository)()
        newContainer.RegisterType(Of IINPRODPATRepository, INPRODPATRepository)()
        newContainer.RegisterType(Of IProductHierarchyRepository, ProductHierarchyRepository)()
        newContainer.RegisterType(Of IInventoryProductAdminService, InventoryProductAdminService)()
        newContainer.RegisterType(Of IProductRateDetailRepository, ProductRateDetailRepository)()
        newContainer.RegisterType(Of IProductGroupsRepository, ProductGroupsRepository)()
        newContainer.RegisterType(Of IFunctionalUnitRepository, FunctionalUnitRepository)()
        newContainer.RegisterType(Of ICareGroupRepository, CareGroupRepository)()
        newContainer.RegisterType(Of Domain.Entities.IContractRepository, Infrastructure.Data.ModelRepository.ContractRepository)()
        newContainer.RegisterType(Of IHealthAdministratorRepository, HealthAdministratorRepository)()
        newContainer.RegisterType(Of IAdmissionRepository, AdmissionRepository)()
        newContainer.RegisterType(Of IPaymentsConceptRepository, PaymentsConceptRepository)()
        newContainer.RegisterType(Of IRevenueControlDetailRepository, RevenueControlDetailRepository)()
        newContainer.RegisterType(Of IServiceOrderRepository, ServiceOrderRepository)()
        newContainer.RegisterType(Of IServiceOrderDetailDistributionRepository, ServiceOrderDetailDistributionRepository)()
        newContainer.RegisterType(Of IPharmaceuticalDispensingDetailRepository, PharmaceuticalDispensingDetailRepository)()
        newContainer.RegisterType(Of ITransferOrderDetailBatchSerialRepository, TransferOrderDetailBatchSerialRepository)()
        newContainer.RegisterType(Of IWarehouseStockRepository, WarehouseStockRepository)()
        newContainer.RegisterType(Of IBatchSerialRepository, BatchSerialRepository)()
        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()
        newContainer.RegisterType(Of IRetentionConceptRepository, RetentionConceptRepository)()
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()
        newContainer.RegisterType(Of IPUCRepository, PUCRepository)()
        newContainer.RegisterType(Of IAccountingBalanceRepository, AccountingBalanceRepository)()
        newContainer.RegisterType(Of ICloseMonthRepository, CloseMonthRepository)()
        newContainer.RegisterType(Of IAccountingBalanceAdminService, AccountingBalanceAdminService)()
        newContainer.RegisterType(Of IDocumentTypeRepository, DocumentTypeRepository)()
        newContainer.RegisterType(Of ISequenseAccountingDRepository, SequenseAccountingDRepository)()
        newContainer.RegisterType(Of IAccountingDocumentRepository, DocumentAccountingRepository)()
        newContainer.RegisterType(Of IKardexRepository, KardexRepository)()
        newContainer.RegisterType(Of IInventoryAdjustmentRepository, InventoryAdjustmentRepository)()
        newContainer.RegisterType(Of IWarehouseRepository, WarehouseRepository)()
        newContainer.RegisterType(Of IPhysicalInventoryAdminService, PhysicalInventoryAdminService)()
        newContainer.RegisterType(Of IAdjustmentConceptRepository, AdjustmentConceptRepository)()
        newContainer.RegisterType(Of IAccountingDocumentAdminService, AccountingDocumentAdminService)()
        newContainer.RegisterType(Of ISettingInventoryRepository, SettingInventoryRepository)()
        newContainer.RegisterType(Of IInventoryControlRepository, InventoryControlRepository)()
        newContainer.RegisterType(Of IInventoryControlDocumentRepository, InventoryControlDocumentRepository)()
        newContainer.RegisterType(Of IInventoryService, InventoryServices)()
        newContainer.RegisterType(Of IPackagePersonalizedDetailRepository, PackagePersonalizedDetailRepository)()
        newContainer.RegisterType(Of IInventorySequenceAdminService, InventorySequenceAdminService)()
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        newContainer.RegisterType(Of IInventoryAdjustmentAdminService, InventoryAdjustmentAdminService)()
        newContainer.RegisterType(Of IProductTypeRepository, ProductTypeRepository)()
        newContainer.RegisterType(Of IPhysicalInventoryRepository, PhysicalInventoryRepository)()
        newContainer.RegisterType(Of IATCRepository, ATCRepository)()
        newContainer.RegisterType(Of IInventoryProductRepository, InventoryProductRepository)()
        newContainer.RegisterType(Of IInventorySupplieRepository, InventorySupplieRepository)()
        newContainer.RegisterType(Of IMeasureUnitRepository, MeasureUnitRepository)()
        newContainer.RegisterType(Of IOperatingUnitRepository, OperatingUnitRepository)()
        newContainer.RegisterType(Of ISettingsAccountRepository, SettingAccountRepository)()
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IMixingStationService, MixingStationService)()

        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)()

        newContainer.RegisterType(Of IReasonscancellationNPTRepository, ReasonscancellationNPTRepository)()

        'User
        newContainer.RegisterType(Of IUserAdminService, UserAdminService)()
        newContainer.RegisterType(Of IUserRepository, UserRepository)()

        'Seguridad
        'newContainer.RegisterType(Of ISeguridadUnitOfWork, Infrastructure.Data.SecurityRepository.GenesisEntities)()
        newContainer.RegisterType(Of IUserMembershipRepository, IndigoAutentication)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IFileUserRepository, FileUserRepository)(New TransientLifetimeManager)
        newContainer.RegisterType(Of IPermissionCompanyRepository, PermissionCompanyRepository)(New TransientLifetimeManager)

        'Crystal
        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()
        newContainer.RegisterType(Of IINCUPSSUBRepository, INCUPSSUBRepository)()

        newContainer.RegisterType(Of ISEGrolesuRepository, SEGrolesuRepository)()
        newContainer.RegisterType(Of ISEGgruusuRepository, SEGgruusuRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        ' Inventory
        newContainer.RegisterType(Of ITransferOrderAdminService, TransferOrderAdminService)()
        newContainer.RegisterType(Of ITransferOrderRepository, TransferOrderRepository)()
        newContainer.RegisterType(Of ITransferOrderDetailAdminService, TransferOrderDetailAdminService)()
        newContainer.RegisterType(Of ITransferOrderDetailRepository, TransferOrderDetailRepository)()
        newContainer.RegisterType(Of IInventoryRequestAdminService, InventoryRequestAdminService)()
        newContainer.RegisterType(Of IInventoryRequestRepository, InventoryRequestRepository)()
        newContainer.RegisterType(Of IInventorySequenceDetailRepository, InventorySequenceDetailRepository)()
        newContainer.RegisterType(Of IInventorySequenceRepository, InventorySequenceRepository)()
        newContainer.RegisterType(Of IHCFARMEPDRepository, HCFARMEPDRepository)()
        newContainer.RegisterType(Of ICMConfigurationUserRepository, CMConfigurationUserRepository)()
        newContainer.RegisterType(Of IPharmaceuticalDispensingDevolutionRepository, PharmaceuticalDispensingDevolutionRepository)()
        newContainer.RegisterType(Of IEmployeeRepository, EmployeeRepository)()
        newContainer.RegisterType(Of IBookRepository, BookRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()
        newContainer.RegisterType(Of IMedicationTypeRepository, MedicationTypeRepository)()
        _currentContainer = newContainer

    End Sub

#End Region

End Class