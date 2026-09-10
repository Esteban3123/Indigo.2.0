#Region "Imports"

Imports Application.Accounting
Imports Application.Billing
Imports Application.Budget
Imports Application.Common
Imports Application.Contract
Imports Application.EventHandlers
Imports Application.EventHandlers.Proxies
Imports Application.Glosas
Imports Application.Inventory.InventoryAdjustment
Imports Application.Inventory.PhysicalInventory
Imports Application.Inventory.Sequense
Imports Application.Payments
Imports Application.Portfolio
Imports Application.Security
Imports Application.Treasury
Imports DistributedServices.Authentication
Imports Domain.Billing.Repositories
Imports Domain.Crystal
Imports Domain.Crystal.Service
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.InterfaceERPGlosa
Imports Domain.Payroll
Imports Domain.Security
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Queue
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.Data.Billing.Repositories
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.PayrollRepository
Imports Infrastructure.Data.SecurityRepository
Imports Microsoft.Extensions.Caching.Memory
Imports Microsoft.Practices.Unity

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
            Dim container = containerInfo?.container
            Dim hisContainer = containerInfo?.hisContainer

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

        'Inyectamos el contexto de Billing
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

        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New Infrastructure.Data.SecurityRepository.GenesisEntities()
                                                                                                                 End Function))

        newContainer.RegisterType(Of IFactoryStorage)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                Return New FactoryStorage()
                                                                                                            End Function))

        Dim injector As New ModuleInjector()
        injector.Load(newContainer, GetType(Domain.Base.Inject))

        newContainer.RegisterType(Of IEventProxy, AzureServiceBusProxy)()
        newContainer.RegisterType(Of IMipresCodeRepository, MipresCodeRepository)()
        newContainer.RegisterType(Of IBillingConceptRepository, BillingConceptRepository)()

        newContainer.RegisterType(Of IBillingService, BillingService)()
        newContainer.RegisterType(Of IProcedureCupsRepository, ProcedureCupsRepository)()
        newContainer.RegisterType(Of IDefinitionRateDetailRepository, DefinitionRateDetailRepository)()
        'Secuencias numericas
        newContainer.RegisterType(Of IBillingSequenseAdminService, BillingSequenseAdminService)()
        newContainer.RegisterType(Of IBillingSequenceRepository, BillingSequenceRepository)()
        newContainer.RegisterType(Of IBillingSequenceDetailRepository, BillingSequenceDetailRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordBillingAdminService, BlockRecordBillingAdminService)()
        newContainer.RegisterType(Of IBlockRecordBillingRepository, BlockRecordBillingRepository)()
        'Orden de servicio
        newContainer.RegisterType(Of IServiceOrderAdminService, ServiceOrderAdminService)()
        newContainer.RegisterType(Of IServiceOrderRepository, ServiceOrderRepository)()
        'grupo
        newContainer.RegisterType(Of IBillingGroupAdminService, BillingGroupAdminService)()
        newContainer.RegisterType(Of IBillingGroupRepository, BillingGroupRepository)()
        'Orden de servicio detalle
        newContainer.RegisterType(Of IServiceOrderDetailAdminService, ServiceOrderDetailAdminService)()
        newContainer.RegisterType(Of IServiceOrderDetailRepository, ServiceOrderDetailRepository)()

        newContainer.RegisterType(Of ISlipOutAdminService, SlipOutAdminService)()
        newContainer.RegisterType(Of ISlipOutRepository, SlipOutRepository)()

        'Orden de servicio detalle quirurgico
        newContainer.RegisterType(Of IServiceOrderDetailSurgicalAdminService, ServiceOrderDetailSurgicalAdminService)()
        newContainer.RegisterType(Of IServiceOrderDetailSurgicalRepository, ServiceOrderDetailSurgicalRepository)()
        'Liquidación
        newContainer.RegisterType(Of ILiquidationAdminService, LiquidationAdminService)()
        newContainer.RegisterType(Of IAdmissionRepository, AdmissionRepository)()
        newContainer.RegisterType(Of IRevenueControlRepository, RevenueControlRepository)()
        newContainer.RegisterType(Of IRevenueControlDetailRepository, RevenueControlDetailRepository)()
        newContainer.RegisterType(Of IRevenueControlDetailAdminService, RevenueControlDetailAdminService)()
        newContainer.RegisterType(Of ICompanySettingsRepository, CompanySettingsRepository)()
        newContainer.RegisterType(Of ISpecialityRepository, SpecialityRepository)()
        newContainer.RegisterType(Of IStayRepository, StayRepository)()
        newContainer.RegisterType(Of IBillingServices, BillingServices)()

        'Datos de Liquidacion
        newContainer.RegisterType(Of ILiquidationDataAdminService, LiquidationDataAdminService)()
        newContainer.RegisterType(Of ILiquidationDataRepository, LiquidationDataRepository)()
        newContainer.RegisterType(Of IAuditDataRepository, AuditDataRepository)()
        newContainer.RegisterType(Of ILiquidationDataDetailRepository, LiquidationDataDetailRepository)()
        newContainer.RegisterType(Of ICupsGroupRepository, CupsGroupRepository)()
        newContainer.RegisterType(Of ICupsSubGroupRepository, CupsSubGroupRepository)()
        newContainer.RegisterType(Of ICupsEntityRepository, CupsEntityRepository)()


        'Crystal HIS
        newContainer.RegisterType(Of IParameterRepository, ParameterRepository)()
        newContainer.RegisterType(Of IStayService, StayService)()
        'Autorización de facturación
        newContainer.RegisterType(Of IBillingAuthorizationAdminService, BillingAuthorizationAdminService)()
        newContainer.RegisterType(Of IBillingAuthorizationRepository, BillingAuthorizationRepository)()

        'Condiciones de venta
        newContainer.RegisterType(Of IConditionSalesAdminService, ConditionSalesAdminService)()
        newContainer.RegisterType(Of IConditionSalesRepository, ConditionsSaleRepository)()

        'ServiceOrderDetailDistribution
        newContainer.RegisterType(Of IServiceOrderDetailDistributionRepository, ServiceOrderDetailDistributionRepository)()
        newContainer.RegisterType(Of IServiceOrderDetailDistributionAdminService, ServiceOrderDetailDistributionAdminService)()
        'Invoice
        newContainer.RegisterType(Of IInvoiceAdminService, InvoiceAdminService)()
        newContainer.RegisterType(Of IInvoiceRepository, InvoiceRepository)()
        'Paciente
        newContainer.RegisterType(Of IPatientRepository, PatientRepository)()
        'SettingsBilling
        newContainer.RegisterType(Of ISettingsBillingRepository, SettingsBillingRepository)()
        newContainer.RegisterType(Of ISettingBillingAdminService, SettingBillingAdminService)()
        'ContractServices
        newContainer.RegisterType(Of Domain.Entities.IContractRepository, Infrastructure.Data.ModelRepository.ContractRepository)()
        newContainer.RegisterType(Of IContractServices, ContractServices)()
        'Control de Cuentas
        newContainer.RegisterType(Of IAccountControlAdminService, AccountControlAdminService)()
        newContainer.RegisterType(Of IAccountControlJustificationRepository, AccountControlJustificationRepository)()
        'DocumentInvoiceProductSalesDevolution
        newContainer.RegisterType(Of IDocumentInvoiceProductSalesDevolutionRepository, DocumentInvoiceProductSalesDevolutionRepository)()
        newContainer.RegisterType(Of IDocumentInvoiceProductSalesDevolutionAdminService, DocumentInvoiceProductSalesDevolutionAdminService)()
        'RIPS Support Record
        newContainer.RegisterType(Of IRIPSSupportRecordRepository, RIPSSupportRecordRepository)()
        newContainer.RegisterType(Of IRIPSSupportRecordAdminService, RIPSSupportRecordAdminService)()

        newContainer.RegisterType(Of IStayDetailRepository, StayDetailRepository)()
        'BillingServices
        newContainer.RegisterType(Of IBillingServices, BillingServices)()
        'terceros
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        'IHCORDLABORepository
        newContainer.RegisterType(Of IHCORDLABORepository, HCORDLABORepository)()
        newContainer.RegisterType(Of IControlOutPatientServicesAdminService, ControlOutPatientServicesAdminService)()
        'ImagenesDx
        newContainer.RegisterType(Of IHCORDIMAGRepository, HCORDIMAGRepository)()
        newContainer.RegisterType(Of IHCORDPRONRepository, HCORDPRONRepository)()
        newContainer.RegisterType(Of IHCPLAOTRPROCUPSRepository, HCPLAOTRPROCUPSRepository)()
        newContainer.RegisterType(Of IHCORDPATORepository, HCORDPATORepository)()
        newContainer.RegisterType(Of IHCORDINTERepository, HCORDINTERepository)()
        newContainer.RegisterType(Of IHCPROCTERRepository, HCPROCTERRepository)()
        newContainer.RegisterType(Of IHCHOGASINRepository, HCHOGASINRepository)()
        newContainer.RegisterType(Of IHCCONOXIGRepository, HCCONOXIGRepository)()
        newContainer.RegisterType(Of IINCONSECURepository, INCONSECURepository)()
        newContainer.RegisterType(Of IAGASICITARepository, AGASICITARepository)()
        newContainer.RegisterType(Of IADCONCOEXrepository, ADCONCOEXrepository)()
        newContainer.RegisterType(Of IAMBORDLABRepository, AMBORDLABRepository)()
        newContainer.RegisterType(Of IAMBORDIMARepository, AMBORDIMARepository)()
        newContainer.RegisterType(Of IAMBORDPATRepository, AMBORDPATRepository)()
        newContainer.RegisterType(Of IINPACIENTTOPANURepository, INPACIENTTOPANURepository)()
        newContainer.RegisterType(Of IINCUPSIPSRepository, INCUPSIPSRepository)()
        newContainer.RegisterType(Of IAGACTMDDDRepository, AGACTMDDDRepository)()
        newContainer.RegisterType(Of IHCUNITHISRepository, HCUNITHISRepository)()
        newContainer.RegisterType(Of IHCFARMEPCRepository, HCFARMEPCRepository)()
        newContainer.RegisterType(Of IHCINTESERRepository, HCINTESERRepository)()
        newContainer.RegisterType(Of IHCPARPACSRepository, HCPARPACSRepository)()
        newContainer.RegisterType(Of IHCJUNOPMHRepository, HCJUNOPMHRepository)()
        newContainer.RegisterType(Of IHCQXREALIRepository, HCQXREALIRepository)()
        newContainer.RegisterType(Of IHCQXEQUIPRepository, HCQXEQUIPRepository)()
        newContainer.RegisterType(Of IHCHISPACARepository, HCHISPACARepository)()

        newContainer.RegisterType(Of ICrossingAccountDetailOtherConceptsRepository, CrossingAccountDetailOtherConceptsRepository)()
        newContainer.RegisterType(Of ICrystalEntityRepository, CrystalEntityRepository)()

        newContainer.RegisterType(Of IUserRepository, UserRepository)()
        newContainer.RegisterType(Of IPharmaceuticalDispensingRepository, PharmaceuticalDispensingRepository)()
        newContainer.RegisterType(Of IPharmaceuticalDispensingDevolutionRepository, PharmaceuticalDispensingDevolutionRepository)()

        'InvoicePortfolioAdvance
        newContainer.RegisterType(Of IInvoicePortfolioAdvanceRepository, InvoicePortfolioAdvanceRepository)()

        'HealthProfessional
        newContainer.RegisterType(Of IHealthProfessionalRepository, HealthProfessionalRepository)()

        newContainer.RegisterType(Of IIPSServiceGroupRepository, IPSServiceGroupRepository)()
        newContainer.RegisterType(Of IBillingInvoiceDetailRepository, BillingInvoiceDetailRepository)()

        'Product Groups
        newContainer.RegisterType(Of IProductGroupsRepository, ProductGroupsRepository)()
        'ReversalReason
        newContainer.RegisterType(Of IBillingReversalReasonRepository, BillingReversalReasonRepository)()
        newContainer.RegisterType(Of IReversalReasonAdminService, ReversalReasonAdminService)()

        'InvoiceEntityCapitated
        newContainer.RegisterType(Of IInvoiceEntityCapitedRepository, InvoiceEntityCapitedRepository)()
        newContainer.RegisterType(Of IInvoiceEntityCapitatedAdminService, InvoiceEntityCapitatedAdminService)()
        'BillingControl
        newContainer.RegisterType(Of IBillingControlRepository, BillingControlRepository)()
        newContainer.RegisterType(Of IBillingControlAdminService, BillingControlAdminService)()

        'Quotation
        newContainer.RegisterType(Of IQuotationRepository, QuotationRepository)()
        newContainer.RegisterType(Of IQuotationAdminService, QuotationAdminService)()

        'DashboardQuoted
        newContainer.RegisterType(Of IDashboardQuotedRepository, DashboardQuotedRepository)()
        newContainer.RegisterType(Of IDashboardQuotedAdminService, DashboardQuotedAdminService)()

        newContainer.RegisterType(Of ICHTIPESTARepository, CHTIPESTARepository)()
        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()

        newContainer.RegisterType(Of IHCORDPROQRepository, HCORDPROQRepository)()
        newContainer.RegisterType(Of IHCQXINFORRepository, HCQXINFORRepository)()

        newContainer.RegisterType(Of IInvoiceCategoriesAdminService, InvoiceCategoriesAdminService)()
        newContainer.RegisterType(Of IBillingInvoiceCategories, BillingInvoiceCategories)()

        'Egreso
        newContainer.RegisterType(Of IHCREGEGRERepository, HCREGEGRERepository)()

        ''dependencia de Contratos********************

        newContainer.RegisterType(Of ICareGroupRepository, CareGroupRepository)()
        'DefinitionRateDetailCondition
        newContainer.RegisterType(Of IDefinitionRateDetailConditionRepository, DefinitionRateDetailConditionRepository)()
        'caregroupDefinitionRate
        newContainer.RegisterType(Of ICareGroupDefinitionRateRepository, CareGroupDefinitionRateRepository)()
        'IPSService
        newContainer.RegisterType(Of IIPSServiceAdminService, IPSServiceAdminService)()
        newContainer.RegisterType(Of IIPSServicesRepository, IPSServicesRepository)()
        'CupsHomologation
        newContainer.RegisterType(Of ICupsHomologationAdminService, CupsHomologationAdminService)()
        newContainer.RegisterType(Of ICupsHomologationRepository, CupsHomologationRepository)()

        'CUPSEntityContractDescriptionsRepository
        newContainer.RegisterType(Of ICupsEntityContractDescriptionsRepository, CUPSEntityContractDescriptionsRepository)()

        'CupsEntity
        newContainer.RegisterType(Of ICupsEntityAdminService, CupsEntityAdminService)()
        newContainer.RegisterType(Of ICupsEntityRepository, CupsEntityRepository)()
        'RateManual
        newContainer.RegisterType(Of IRateManualAdminService, RateManualAdminService)()
        newContainer.RegisterType(Of IRateManualRepository, RateManualRepository)()
        'SurgicalProcedureService
        newContainer.RegisterType(Of ISurgicalProcedureServiceAdminService, SurgicalProcedureServiceAdminService)()
        newContainer.RegisterType(Of ISurgicalProcedureServiceRepository, SurgicalProcedureServiceRepository)()
        'RateManualDetailSurgical
        newContainer.RegisterType(Of IRateManualDetailSurgicalAdminService, RateManualDetailSurgicalAdminService)()
        newContainer.RegisterType(Of IRateManualDetailSurgicalRepository, RateManualDetailSurgicalRepository)()
        'RateManual
        newContainer.RegisterType(Of IRateManualAdminService, RateManualAdminService)()
        newContainer.RegisterType(Of IRateManualRepository, RateManualRepository)()
        'RateManualDetail
        newContainer.RegisterType(Of IRateManualDetailAdminService, RateManualDetailAdminService)()
        newContainer.RegisterType(Of IRateManualDetailRepository, RateManualDetailRepository)()
        'RateManualValidity
        newContainer.RegisterType(Of IRateManualValidityRepository, RateManualValidityRepository)()

        'HealthAdministrator
        newContainer.RegisterType(Of IHealthAdministratorRepository, HealthAdministratorRepository)()

        '-------------------- Esto para identificar -----------------------------------------------------------------------

        'newContainer.RegisterType(Of ISeguridadUnitOfWork, Infrastructure.Data.SecurityRepository.GenesisEntities)()
        'Security.User
        newContainer.RegisterType(Of IUserAdminService, UserAdminService)()
        newContainer.RegisterType(Of IUserRepository, UserRepository)()

        newContainer.RegisterType(Of IUserMembershipRepository, IndigoAutentication)(New TransientLifetimeManager)

        newContainer.RegisterType(Of IFileUserRepository, FileUserRepository)(New TransientLifetimeManager)

        newContainer.RegisterType(Of IPermissionCompanyRepository, PermissionCompanyRepository)(New TransientLifetimeManager)
        '-------------------------------------------------------------------------------------------------------------------

        'Accounting**********************
        'MainAccounts
        newContainer.RegisterType(Of IPUCRepository, PUCRepository)()
        'AccountingDocument
        newContainer.RegisterType(Of IAccountingDocumentAdminService, AccountingDocumentAdminService)()
        newContainer.RegisterType(Of IAccountingDocumentRepository, DocumentAccountingRepository)()
        'DocumentType
        newContainer.RegisterType(Of IDocumentTypeRepository, DocumentTypeRepository)()
        'AccountingBalance
        newContainer.RegisterType(Of IAccountingBalanceRepository, AccountingBalanceRepository)()
        'CloseMonth
        newContainer.RegisterType(Of ICloseMonthRepository, CloseMonthRepository)()
        'AccountingBalance
        newContainer.RegisterType(Of IAccountingBalanceAdminService, AccountingBalanceAdminService)()
        'SequenceD
        newContainer.RegisterType(Of ISequenseAccountingDRepository, SequenseAccountingDRepository)()
        'setting Account
        newContainer.RegisterType(Of ISettingsAccountRepository, SettingAccountRepository)()

        newContainer.RegisterType(Of ISurgeriesPercentageManualRepository, SurgeriesPercentageManualRepository)()
        newContainer.RegisterType(Of ISurgeriesPercentageManualAdminService, SurgeriesPercentageManualAdminService)()

        '*PORTFOLIO***********************
        newContainer.RegisterType(Of IAccountReceivableAdminService, AccountReceivableAdminService)()
        newContainer.RegisterType(Of IAccountReceivableRepository, AccountReceivableRepository)()
        newContainer.RegisterType(Of ISequensePortfolioDRepository, SequensePortfolioDRepository)()
        newContainer.RegisterType(Of ISequensePortfolioCRepository, SequensePortfolioCRepository)()
        newContainer.RegisterType(Of IPortfolioSequenseAdminService, PortfolioSequenseAdminService)()
        newContainer.RegisterType(Of IPortfolioTransfersAdminService, PortfolioTransfersAdminService)()
        newContainer.RegisterType(Of IPortfolioTransferRepository, PortfolioTransferRepository)()

        newContainer.RegisterType(Of IPortfolioControlRepository, PortfolioControlRepository)()
        newContainer.RegisterType(Of IPortfolioAdvanceRepository, PortfolioAdvanceRepository)()
        newContainer.RegisterType(Of IPortfolioAdvanceAdminService, PortfolioAdvanceAdminService)()
        newContainer.RegisterType(Of IPUCRepository, PUCRepository)()
        newContainer.RegisterType(Of ISettingPortfolioRepository, SettingPortfolioRepository)()

        '*******************COMMON*******************
        newContainer.RegisterType(Of ICustomerRepository, CustomerRepository)()
        newContainer.RegisterType(Of IOperatingUnitRepository, OperatingUnitRepository)()

        '******MEDICALFEESCAUSATION
        newContainer.RegisterType(Of IMedicalFeesCausationRepository, MedicalFeesCausationRepository)()

        ''Payroll************************
        'costCenter
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()
        'Functional Unit
        newContainer.RegisterType(Of IFunctionalUnitRepository, FunctionalUnitRepository)()

        ''Inventory**********************
        'Repositories
        newContainer.RegisterType(Of IInventorySequenceRepository, InventorySequenceRepository)()
        newContainer.RegisterType(Of IInventorySequenceDetailRepository, InventorySequenceDetailRepository)()
        newContainer.RegisterType(Of IInventoryAdjustmentRepository, InventoryAdjustmentRepository)()
        newContainer.RegisterType(Of IAdjustmentConceptRepository, AdjustmentConceptRepository)()
        newContainer.RegisterType(Of IInventoryControlRepository, InventoryControlRepository)()
        newContainer.RegisterType(Of IInventoryControlDocumentRepository, InventoryControlDocumentRepository)()
        newContainer.RegisterType(Of IPhysicalInventoryRepository, PhysicalInventoryRepository)()
        newContainer.RegisterType(Of IKardexRepository, KardexRepository)()
        newContainer.RegisterType(Of IHCFARMEPDRepository, HCFARMEPDRepository)()
        newContainer.RegisterType(Of IProductTypeRepository, ProductTypeRepository)()
        newContainer.RegisterType(Of IPharmaceuticalDispensingDetailRepository, PharmaceuticalDispensingDetailRepository)()
        newContainer.RegisterType(Of ITransferOrderRepository, TransferOrderRepository)()
        newContainer.RegisterType(Of ITransferOrderDetailRepository, TransferOrderDetailRepository)()
        newContainer.RegisterType(Of ITransferOrderDetailBatchSerialRepository, TransferOrderDetailBatchSerialRepository)()
        newContainer.RegisterType(Of IBatchSerialRepository, BatchSerialRepository)()
        newContainer.RegisterType(Of IWarehouseStockRepository, WarehouseStockRepository)()
        newContainer.RegisterType(Of IATCRepository, ATCRepository)()
        'ProductRateDetail
        newContainer.RegisterType(Of IProductRateDetailRepository, ProductRateDetailRepository)()
        'InventoryProduct
        newContainer.RegisterType(Of IInventoryProductRepository, InventoryProductRepository)()
        'SettingInventory
        newContainer.RegisterType(Of ISettingInventoryRepository, SettingInventoryRepository)()
        'Services
        newContainer.RegisterType(Of IInventorySequenceAdminService, InventorySequenceAdminService)()
        newContainer.RegisterType(Of IInventoryAdjustmentAdminService, InventoryAdjustmentAdminService)()
        newContainer.RegisterType(Of IPhysicalInventoryAdminService, PhysicalInventoryAdminService)()
        newContainer.RegisterType(Of IInventoryService, InventoryServices)()

        'BillingServices******************
        newContainer.RegisterType(Of ISequenseContractDRepository, SequenseContractDRepository)()
        newContainer.RegisterType(Of IRetentionConceptRepository, RetentionConceptRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        newContainer.RegisterType(Of IRevenueRecognitionRepository, RevenueRecognitionRepository)()
        newContainer.RegisterType(Of IRevenueRecognitionAdminService, RevenueRecognitionAdminService)()

        newContainer.RegisterType(Of IRevenueRecognitionDetailRepository, RevenueRecognitionDetailRepository)()

        'Facturación electronica
        newContainer.RegisterType(Of Application.ElectronicDocuments.IElectronicDocumentsAdminService, Application.ElectronicDocuments.ElectronicDocumentsAdminService)
        newContainer.RegisterType(Of IElectronicDocumentsAdminService, ElectronicDocumentsAdminService)
        newContainer.RegisterType(Of IElectronicDocumentRepository, ElectronicDocumentRepository)()
        newContainer.RegisterType(Of IElectronicDocumentDetailRepository, ElectronicDocumentDetailRepository)
        newContainer.RegisterType(Of IElectronicDocumentNotificationRepository, ElectronicDocumentNotificationRepository)
        newContainer.RegisterType(Of IBillingNoteRepository, BillingNoteRepository)()
        newContainer.RegisterType(Of IBillingReversalReasonRepository, BillingReversalReasonRepository)()
        ' Discriminador saldo inicial en GetHealthSegmentFromInvoiceXml (notas tipo 6).
        newContainer.RegisterType(Of IInitialBalanceInvoiceRepository, InitialBalanceInvoiceRepository)()

        'Facturación Básica
        newContainer.RegisterType(Of IBasicBillingAdminService, BasicBillingAdminService)()
        newContainer.RegisterType(Of IBasicBillingRepository, BasicBillingRepository)()

        'Tarifas de productos y servicios
        newContainer.RegisterType(Of IProductAndServiceFeeAdminService, ProductAndServiceFeeAdminService)()
        newContainer.RegisterType(Of IProductAndServiceFeeRepository, ProductAndServiceFeeRepository)()

        'Currency
        newContainer.RegisterType(Of ICurrencyAdminService, CurrencyAdminService)()
        newContainer.RegisterType(Of ICurrencyRepository, CurrencyRepository)()

        'cruce de cuentas
        newContainer.RegisterType(Of ICrossingAccountAdminService, CrossingAccountAdminService)()
        newContainer.RegisterType(Of ICrossingAccountRepository, CrossingAccountRepository)()

        'Con el Fin de realizar Recibos De caja
        newContainer.RegisterType(Of IInterfaceFOX, InterfaceFOX)(New InjectionConstructor(container))
        newContainer.RegisterType(Of IInterfaceNET, InterfaceNET)(New InjectionConstructor(container))
        newContainer.RegisterType(Of IInterfacePublicFOX, InterfacePublicFOX)(New InjectionConstructor(container))
        newContainer.RegisterType(Of IInterfacePublicNET, InterfacePublicNET)(New InjectionConstructor(container))
        newContainer.RegisterType(Of ISequenseTreasuryCRepository, SequenseTreasuryCRepository)()
        newContainer.RegisterType(Of ICashReceiptsAdminService, CashReceiptsAdminService)()
        newContainer.RegisterType(Of ICashReceiptsRepository, CashReceiptsRepository)()
        newContainer.RegisterType(Of ICashRegisterRepository, CashRegisterRepository)()
        newContainer.RegisterType(Of ICashRegisterAdminService, CashRegisterAdminService)()
        newContainer.RegisterType(Of ISequenseTreasuryDRepository, SequenseTreasuryDRepository)()
        newContainer.RegisterType(Of IEntityBankAccountRepository, EntityBankAccountRepository)()
        newContainer.RegisterType(Of IEntityBankAccountAdminService, EntityBankAccountAdminService)()
        newContainer.RegisterType(Of ISettingsTreasuryRepository, SettingsTreasuryRepository)()
        newContainer.RegisterType(Of IMoneyAdvanceRepository, MoneyAdvanceRepository)()
        newContainer.RegisterType(Of IMoneyAdvanceAdminService, MoneyAdvanceAdminService)()
        newContainer.RegisterType(Of ISequensePaymentsDRepository, SequensePaymentsDRepository)()
        newContainer.RegisterType(Of ITreasuryControlAdminService, TreasuryControlAdminService)()
        newContainer.RegisterType(Of ITreasuryControlRepository, TreasuryControlRepository)()
        newContainer.RegisterType(Of ITreasuryServices, TreasuryServices)()
        newContainer.RegisterType(Of IExpenseConceptRepository, ExpenseConceptRepository)()
        newContainer.RegisterType(Of IDischargeBillAdminService, DischargeBillAdminService)()
        newContainer.RegisterType(Of IDischargeBillRepository, DischargeBillRepository)()
        newContainer.RegisterType(Of IAccountPayableRepository, AccountPayableRepository)()
        newContainer.RegisterType(Of IAccountPayableAdminService, AccountPayableAdminService)()
        newContainer.RegisterType(Of IRefundRepository, RefundRepository)()
        newContainer.RegisterType(Of IRefundAdminService, RefundAdminService)()
        newContainer.RegisterType(Of ISupplierRepository, SupplierRepository)()
        newContainer.RegisterType(Of ICrossingAccountDetailCxPRepository, CrossingAccountDetailCxPRepository)()
        newContainer.RegisterType(Of ICrossingAccountDetailCxCRepository, CrossingAccountDetailCxCRepository)()
        newContainer.RegisterType(Of Domain.Entities.IBankRepository, Infrastructure.Data.ModelRepository.BankRepository)()
        newContainer.RegisterType(Of IVoucherTransactionRepository, VoucherTransactionRepository)()
        newContainer.RegisterType(Of IAccountReceivableAccountingRepository, AccountReceivableAccountingRepository)()
        newContainer.RegisterType(Of IPortfolioGlosadaRepository, PortfolioGlosadaRepository)()
        newContainer.RegisterType(Of IPartialPaymentsCAdminService, PartialPaymentsCAdminService)()
        newContainer.RegisterType(Of Domain.Entities.IConsecutiveRepository, ConsecutiveRepository)()
        newContainer.RegisterType(Of IPartialPaymentsCRepository, PartialPaymentsCRepository)()
        newContainer.RegisterType(Of IPartialPaymentsDRepository, PartialPaymentsDRepository)()
        newContainer.RegisterType(Of IPartialPaymentsMovementRepository, PartialPaymentsMovementRepository)()
        newContainer.RegisterType(Of IMovementGlosaRepository, MovementGlosaRepository)()
        newContainer.RegisterType(Of IInterfaceParametersRepository, InterfacesParametersRepository)()
        newContainer.RegisterType(Of ICashReceiptConceptRepository, CashReceiptConceptRepository)()
        newContainer.RegisterType(Of IBudgetSequenceRepository, BudgetSequenceRepository)()
        newContainer.RegisterType(Of IBudgetRepository, BudgetRepository)()
        newContainer.RegisterType(Of IBudgetItemRepository, BudgetItemRepository)()
        newContainer.RegisterType(Of IRecognitionAdminService, RecognitionAdminService)()
        newContainer.RegisterType(Of IRecognitionRepository, RecognitionRepository)()
        newContainer.RegisterType(Of ISequenseBudgetDRepository, SequenseBudgetDRepository)()
        newContainer.RegisterType(Of IBudgetHeaderRepository, BudgetHeaderRepository)()
        newContainer.RegisterType(Of IBudgetService, BudgetService)()
        newContainer.RegisterType(Of IValidityRepository, ValidityRepository)()
        newContainer.RegisterType(Of IExpenseTypeRepository, ExpenseTypeRepository)()
        newContainer.RegisterType(Of IAvailabilityRepository, AvailabilityRepository)()
        newContainer.RegisterType(Of ICommitmentRepository, CommitmentRepository)()
        newContainer.RegisterType(Of IObligationRepository, ObligationRepository)()
        newContainer.RegisterType(Of ISuspensionDetailRepository, SuspensionDetailRepository)()
        newContainer.RegisterType(Of IAvailabilityDetailRepository, AvailabilityDetailRepository)()
        newContainer.RegisterType(Of ICollectionRepository, CollectionRepository)()
        newContainer.RegisterType(Of IReclassificationRepository, ReclassificationRepository)()
        newContainer.RegisterType(Of ICashFlowConceptRepository, CashFlowConceptRepository)()

        'InvoiceEntityCapitatedDistribution
        newContainer.RegisterType(Of IInvoiceEntityCapitatedDistributionRepository, InvoiceEntityCapitatedDistributionRepository)()
        newContainer.RegisterType(Of IInvoiceEntityCapitatedDistributionAdminService, InvoiceEntityCapitatedDistributionAdminService)()

        'InvoiceEntityCapitatedDistributionDetail
        newContainer.RegisterType(Of IInvoiceEntityCapitatedDistributionDetailRepository, InvoiceEntityCapitatedDistributionDetailRepository)()
        newContainer.RegisterType(Of IInvoiceEntityCapitatedDistributionDetailAdminService, InvoiceEntityCapitatedDistributionDetailAdminService)()

        newContainer.RegisterType(Of IHCORHEMSERRepository, HCORHEMSERRepository)

        newContainer.RegisterType(Of ISEGrolesuRepository, SEGrolesuRepository)()
        newContainer.RegisterType(Of ISEGgruusuRepository, SEGgruusuRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        newContainer.RegisterType(Of IADCENATENRepository, ADCENATENRepository)()
        newContainer.RegisterType(Of IINUNIFUNCRepository, INUNIFUNCRepository)()
        newContainer.RegisterType(Of IINCUPSSUBRepository, INCUPSSUBRepository)()

        'Conceptos Causas de Estado Folio
        newContainer.RegisterType(Of IConceptsCausesStatusFolioAdminService, ConceptsCausesStatusFolioAdminService)()
        newContainer.RegisterType(Of IConceptsCausesStatusFolioRepository, ConceptsCausesStatusFolioRepository)()

        'Ejecutivo de Ventas
        newContainer.RegisterType(Of ISalesExecutiveAdminService, SalesExecutiveAdminService)()
        newContainer.RegisterType(Of ISalesExecutiveRepository, SalesExecutiveRepository)()

        'Contract package service
        newContainer.RegisterType(Of IContractPackageServiceRepository, ContractPackageServiceRepository)()
        newContainer.RegisterType(Of IContractPackageProductRepository, ContractPackageProductRepository)()

        newContainer.RegisterType(Of IViewListNoSurgicalRepository, ViewListNoSurgicalRepository)()
        newContainer.RegisterType(Of IHCORDIMAGRepository, HCORDIMAGRepository)()

        ' Inventory Settings
        newContainer.RegisterType(Of ISettingInventoryRepository, SettingInventoryRepository)()
        'ProductGroupsRepository
        newContainer.RegisterType(Of IProductGroupsRepository, ProductGroupsRepository)()
        'WarehouseRepository
        newContainer.RegisterType(Of IWarehouseRepository, WarehouseRepository)()
        'PharmaDoseRepository
        newContainer.RegisterType(Of IPharmaDoseRepository, PharmaDoseRepository)()
        'ElectronicSupportDocument
        newContainer.RegisterType(Of IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository)()
        newContainer.RegisterType(Of IElectronicSupportDocumentAdminService, ElectronicSupportDocumentAdminService)()
        newContainer.RegisterType(Of INumberingAuthorizationRepository, NumberingAuthorizationRepository)()
        newContainer.RegisterType(Of INumberingAuthorizationAdminService, NumberingAuthorizationAdminService)()


        'AccountControlJustification
        newContainer.RegisterType(Of IAccountControlJustificationAdminService, AccountControlJustificationAdminService)()
        newContainer.RegisterType(Of IAccountControlJustificationRepository, AccountControlJustificationRepository)()

        'AccountControlJustification
        newContainer.RegisterType(Of IBillingJustificationControlAdminService, BillingJustificationControlAdminService)()
        newContainer.RegisterType(Of IBillingJustificationControlRepository, BillingJustificationContolRepository)()

        'legalbook
        newContainer.RegisterType(Of IBookRepository, BookRepository)()

        'GeneralLedgerIVARepository
        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()

        'CostDistributions
        newContainer.RegisterType(Of ICostDistributionsRepository, CostDistributionsRepository)()
        newContainer.RegisterType(Of ICostDistributionDirectCostRepository, CostDistributionDirectCostRepository)
        newContainer.RegisterType(Of ICostDistributionDirectCostDetailIvaRepository, CostDistributionDirectCostDetailIvaRepository)

        'PortfolioNote
        newContainer.RegisterType(Of IPortfolioNoteAdminService, PortfolioNoteAdminService)()
        newContainer.RegisterType(Of IPortfolioNoteRepository, PortfolioNoteRepository)()
        'PortfolioNoteConcept
        newContainer.RegisterType(Of IPortfolioNoteConceptRepository, PortfolioNoteConceptRepository)()
        'TreasuryNote
        newContainer.RegisterType(Of ITreasuryNoteAdminService, TreasuryNoteAdminService)()
        newContainer.RegisterType(Of ITreasuryNoteRepository, TreasuryNoteRepository)()
        'VoucherTransaction
        newContainer.RegisterType(Of IVoucherTransactionAdminService, VoucherTransactionAdminService)()
        'Check
        newContainer.RegisterType(Of ICheckRepository, CheckRepository)()
        'CheckBlock
        newContainer.RegisterType(Of ICheckBlockRepository, CheckBlockRepository)()
        'Setting Payments
        newContainer.RegisterType(Of ISettingPaymentsRepository, SettingPaymentsRepository)()
        newContainer.RegisterType(Of IPaymentsConceptRepository, PaymentsConceptRepository)()
        'Deferred Causation
        newContainer.RegisterType(Of IDeferredCausationAdminService, DeferredCausationAdminService)()
        newContainer.RegisterType(Of IDeferredCausationRepository, DeferredCausationRepository)()
        'Monthly Amortization
        newContainer.RegisterType(Of IMonthlyAmortizationRepository, MonthlyAmortizationRepository)()
        'Payment Control
        newContainer.RegisterType(Of IPaymentControlAdminService, PaymentControlAdminService)()
        newContainer.RegisterType(Of IPaymentControlRepository, PaymentControlRepository)()
        'Treasury Advance
        newContainer.RegisterType(Of ITreasuryAdvanceRepository, TreasuryAdvanceRepository)()
        'MovementAccountPayable
        newContainer.RegisterType(Of IMovementAccountPayableAdminService, MovementAccountPayableAdminService)()
        newContainer.RegisterType(Of IMovementAccountPayableRepository, MovementAccountPayableRepository)()
        'Supplier
        newContainer.RegisterType(Of ISupplierRepository, SupplierRepository)()
        newContainer.RegisterType(Of ISupplierAdminService, SupplierAdminService)()
        'Maintenance Sequence Detail
        newContainer.RegisterType(Of IMaintenanceSequenceDetailRepository, MaintenanceSequenceDetailRepository)()
        'Payments Sequense
        newContainer.RegisterType(Of IPaymentsSequenseAdminService, PaymentsSequenseAdminService)()
        'Sequense Payments C
        newContainer.RegisterType(Of ISequensePaymentsCRepository, SequensePaymentsCRepository)()
        'Cancellation Check
        newContainer.RegisterType(Of ICancellationCheckRepository, CancellationCheckRepository)()
        'Outstanding Checks
        newContainer.RegisterType(Of IOutstandingChecksRepository, OutstandingChecksRepository)()
        'Payment Notes Account Payable Advance
        newContainer.RegisterType(Of IPaymentNotesAccountPayableAdvanceRepository, PaymentNotesAccountPayableAdvanceRepository)()
        'Transfers
        newContainer.RegisterType(Of ITransfersRepository, TransfersRepository)()
        'Consignment Transfer
        newContainer.RegisterType(Of IConsignmentTransferAdminService, ConsignmentTransferAdminService)()
        newContainer.RegisterType(Of IConsignmentTransferRepository, ConsignmentTransferRepository)()
        'Fomag Validation
        newContainer.RegisterType(Of IValidationFomagAdminService, ValidationFomagAdminService)
        newContainer.RegisterType(Of IFomagRepository, FomagRepository)

        newContainer.RegisterType(Of IFolioAdminService, FolioAdminService)()
        newContainer.RegisterType(Of IPOSPathologiesRepository, POSPathologiesRepository)()
        newContainer.RegisterType(Of IFolioRepository, FolioRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()
        newContainer.RegisterType(Of IBedRateRepository, BedRateRepository)()
        newContainer.RegisterType(Of ICustomTRMRepository, CustomTRMRepository)()
        newContainer.RegisterType(Of IFixedAssetEntryRepository, FixedAssetEntryRepository)
        newContainer.RegisterType(Of IElectronicsPropertiesRepository, ElectronicsPropertiesRepository)
        newContainer.RegisterType(Of IEndpointsRepository, EndPointsRepository)
        newContainer.RegisterType(Of IFeeNotCollectedRepository, FeeNotCollectedRepository)

        newContainer.RegisterType(Of IMemoryCache, MemoryCache)(
            New ContainerControlledLifetimeManager(),  ' Singleton
            New InjectionFactory(Function(c) New MemoryCache(New MemoryCacheOptions()))
        )
        newContainer.RegisterType(Of IOutBoxRepository, OutBoxRepository)
        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)(New TransientLifetimeManager)

        _currentContainer = newContainer
    End Sub

#End Region

End Class