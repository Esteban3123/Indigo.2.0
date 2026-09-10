#Region "Imports"

Imports Application.Accounting
Imports Application.Contract
Imports Application.MedicalFees
Imports Application.Payments
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Domain.Security
Imports Infrastructure.CrossCutting.Queue
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.MaintenanceRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.PayrollRepository
Imports Infrastructure.Data.SecurityRepository
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

        '
        newContainer.RegisterType(Of ICausationPendingRepository, CausationPendingRepository)()
        newContainer.RegisterType(Of ISupplierRepository, Infrastructure.Data.ModelRepository.SupplierRepository)()
        newContainer.RegisterType(Of IDefinitionRateDetailRepository, DefinitionRateDetailRepository)()
        newContainer.RegisterType(Of IViewListNoSurgicalRepository, ViewListNoSurgicalRepository)()
        newContainer.RegisterType(Of IIPSServiceGroupRepository, IPSServiceGroupRepository)()
        'caregroupDefinitionRate
        newContainer.RegisterType(Of ICareGroupDefinitionRateRepository, CareGroupDefinitionRateRepository)()
        'DefinitionRateDetailCondition
        newContainer.RegisterType(Of IDefinitionRateDetailConditionRepository, DefinitionRateDetailConditionRepository)()
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IMedicalFeesService, MedicalFeesService)()
        'Secuencias numericas
        newContainer.RegisterType(Of IMedicalFeesSequenseAdminService, MedicalFeesSequenseAdminService)()
        newContainer.RegisterType(Of IMedicalFeesSecuenceRepository, MedicalFeesSecuenceRepository)()
        newContainer.RegisterType(Of IMedicalFeesSecuenceDetailRepository, MedicalFeesSecuenceDetailRepository)()
        'BlockRecord
        newContainer.RegisterType(Of IBlockRecordMedicalFeesAdminService, BlockRecordMedicalFeesAdminService)()
        newContainer.RegisterType(Of IBlockRecordMedicalFeesRepository, BlockRecordMedicalFeesRepository)()
        'MedicalFeesContract
        newContainer.RegisterType(Of IMedicalFeesContractAdminService, MedicalFeesContractAdminService)()
        newContainer.RegisterType(Of IMedicalFeesContractRepository, MedicalFeesContractRepository)()
        'MedicalFeesCausation
        newContainer.RegisterType(Of IMedicalFeesCausationAdminService, MedicalFeesCausationAdminService)()
        newContainer.RegisterType(Of IMedicalFeesCausationRepository, MedicalFeesCausationRepository)()
        'CausationRecognition (Reconocimiento de Causaciones por Proveedor)
        newContainer.RegisterType(Of ICausationRecognitionAdminService, CausationRecognitionAdminService)()
        'MedicalFeesSettings
        newContainer.RegisterType(Of IMedicalFeesSettingsAdminService, MedicalFeesSettingsAdminService)()
        newContainer.RegisterType(Of IMedicalFeesSettingsRepository, MedicalFeesSettingsRepository)()
        'MedicalFeesLiquidation
        newContainer.RegisterType(Of IMedicalFeesLiquidationAdminService, MedicalFeesLiquidationAdminService)()
        newContainer.RegisterType(Of IMedicalFeesLiquidationRepository, MedicalFeesLiquidationRepository)()
        'MedicalFeesNotes
        newContainer.RegisterType(Of IMedicalFeesNoteAdminService, MedicalFeesNoteAdminService)()
        newContainer.RegisterType(Of IMedicalFeesNoteRepository, MedicalFeesNoteRepository)()
        'MedicalFeesLiquidationDetail
        newContainer.RegisterType(Of IMedicalFeesLiquidationDetailRepository, MedicalFeesLiquidationDetailRepository)()
        'HealthProfessionalContract
        newContainer.RegisterType(Of IHealthProfessionalContractAdminService, HealthProfessionalContractAdminService)()
        newContainer.RegisterType(Of IHealthProfessionalContractRepository, HealthProfessionalContractRepository)()
        newContainer.RegisterType(Of IInvoicePortfolioAdvanceRepository, InvoicePortfolioAdvanceRepository)()

        'Contract
        newContainer.RegisterType(Of Domain.Entities.IContractRepository, Infrastructure.Data.ModelRepository.ContractRepository)()
        'RateManual
        newContainer.RegisterType(Of IRateManualRepository, RateManualRepository)()
        'RateManualDetail
        newContainer.RegisterType(Of IRateManualDetailRepository, RateManualDetailRepository)()
        'RateManualDetailSurgical
        newContainer.RegisterType(Of IRateManualDetailSurgicalRepository, RateManualDetailSurgicalRepository)()
        'HealthProfessional
        newContainer.RegisterType(Of IHealthProfessionalRepository, HealthProfessionalRepository)()
        'CupsEntity
        newContainer.RegisterType(Of ICupsEntityRepository, CupsEntityRepository)()
        'CUPSEntityContractDescriptionsRepository
        newContainer.RegisterType(Of ICupsEntityContractDescriptionsRepository, CUPSEntityContractDescriptionsRepository)()
        'ContractPackageServiceRepository
        newContainer.RegisterType(Of IContractPackageServiceRepository, ContractPackageServiceRepository)()
        'IPSService
        newContainer.RegisterType(Of IIPSServicesRepository, IPSServicesRepository)()
        'CareGroup
        newContainer.RegisterType(Of ICareGroupRepository, CareGroupRepository)()
        'ProcedureCups
        newContainer.RegisterType(Of IProcedureCupsRepository, ProcedureCupsRepository)()
        'CupsHomologation
        newContainer.RegisterType(Of ICupsHomologationRepository, CupsHomologationRepository)()
        'SurgicalProcedureService
        newContainer.RegisterType(Of ISurgicalProcedureServiceRepository, SurgicalProcedureServiceRepository)()
        'DefinitionRate
        newContainer.RegisterType(Of IDefinitionRateAdminService, DefinitionRateAdminService)()
        newContainer.RegisterType(Of IDefinitionRateRepository, DefinitionRateRepository)()
        'DefinitionRateDetail
        newContainer.RegisterType(Of IDefinitionRateDetailAdminService, DefinitionRateDetailAdminService)()
        newContainer.RegisterType(Of IDefinitionRateDetailRepository, DefinitionRateDetailRepository)()

        'ServiceOrderDetail
        newContainer.RegisterType(Of IServiceOrderDetailRepository, ServiceOrderDetailRepository)()
        'ServiceOrderDetailSurgical
        newContainer.RegisterType(Of IServiceOrderDetailSurgicalRepository, ServiceOrderDetailSurgicalRepository)()
        'ServiceOrderDetailDistribution
        newContainer.RegisterType(Of IServiceOrderDetailDistributionRepository, ServiceOrderDetailDistributionRepository)()
        'FunctionalUnit
        newContainer.RegisterType(Of IFunctionalUnitRepository, FunctionalUnitRepository)()
        'RevenueControlDetail
        newContainer.RegisterType(Of IRevenueControlDetailRepository, RevenueControlDetailRepository)()
        'ProductRateDetail
        newContainer.RegisterType(Of IProductRateDetailRepository, ProductRateDetailRepository)()
        'InventoryProduct
        newContainer.RegisterType(Of IInventoryProductRepository, InventoryProductRepository)()
        'RevenueControl
        newContainer.RegisterType(Of IRevenueControlRepository, RevenueControlRepository)()
        'Patient
        newContainer.RegisterType(Of IPatientRepository, PatientRepository)()
        'BillingAuthorization
        newContainer.RegisterType(Of IBillingAuthorizationRepository, BillingAuthorizationRepository)()
        'SettingsBilling
        newContainer.RegisterType(Of ISettingsBillingRepository, SettingsBillingRepository)()
        'SettingsInventory
        newContainer.RegisterType(Of ISettingInventoryRepository, SettingInventoryRepository)()
        'BillingService
        newContainer.RegisterType(Of IBillingServices, BillingServices)()
        'ContractService
        newContainer.RegisterType(Of IContractServices, ContractServices)()
        'Product Groups
        newContainer.RegisterType(Of IProductGroupsRepository, ProductGroupsRepository)()
        'billing concept
        newContainer.RegisterType(Of IBillingConceptRepository, BillingConceptRepository)()
        newContainer.RegisterType(Of IViewListSurgicalAndPackageRepository, ViewListSurgicalAndPackageRepository)()
        'Payments

        'Secuencias numericas
        newContainer.RegisterType(Of IPaymentsSequenseAdminService, PaymentsSequenseAdminService)()
        newContainer.RegisterType(Of ISequensePaymentsCRepository, SequensePaymentsCRepository)()
        newContainer.RegisterType(Of ISequensePaymentsDRepository, SequensePaymentsDRepository)()
        'Conceptos
        newContainer.RegisterType(Of IPaymentsConceptAdminService, PaymentsConceptAdminService)()
        newContainer.RegisterType(Of IPaymentsConceptRepository, PaymentsConceptRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordPaymentsAdminService, BlockRecordPaymentsAdminService)()
        newContainer.RegisterType(Of IBlockRecordPaymentsRepository, BlockRecordPaymentsRepository)()
        'Conceptos
        newContainer.RegisterType(Of IPaymentsNoteConceptAdminService, PaymentsNoteConceptAdminService)()
        newContainer.RegisterType(Of IPaymentsNoteConceptRepository, PaymentsNoteConceptRepository)()
        'Cuenta por Pagar
        newContainer.RegisterType(Of IAccountPayableAdminService, AccountPayableAdminService)()
        newContainer.RegisterType(Of IAccountPayableRepository, AccountPayableRepository)()
        'Parametros de pagos
        newContainer.RegisterType(Of ISettingPaymentsAdminService, SettingPaymentsAdminService)()
        newContainer.RegisterType(Of ISettingPaymentsRepository, SettingPaymentsRepository)()
        'Saldos Iniciales
        newContainer.RegisterType(Of IOpeningBalanceAdminService, OpeningBalanceAdminService)()
        newContainer.RegisterType(Of IOpeningBalanceRepository, OpeningBalanceRepository)()
        'MoneyAdvance
        newContainer.RegisterType(Of IMoneyAdvanceAdminService, MoneyAdvanceAdminService)()
        newContainer.RegisterType(Of IMoneyAdvanceRepository, MoneyAdvanceRepository)()
        'PaymentsNotes
        newContainer.RegisterType(Of INotesDebitCreditAdminService, NotesDebitCreditAdminService)()
        newContainer.RegisterType(Of INotesDebitCreditRepository, NotesDebitCreditRepository)()
        'DeferredCausation
        newContainer.RegisterType(Of IDeferredCausationAdminService, DeferredCausationAdminService)()
        newContainer.RegisterType(Of IDeferredCausationRepository, DeferredCausationRepository)()
        'MovementAccountPayable
        newContainer.RegisterType(Of IMovementAccountPayableAdminService, MovementAccountPayableAdminService)()
        newContainer.RegisterType(Of IMovementAccountPayableRepository, MovementAccountPayableRepository)()
        'PaymentNotesAccountPayableAdvance
        newContainer.RegisterType(Of IPaymentNotesAccountPayableAdvanceAdminService, PaymentNotesAccountPayableAdvanceAdminService)()
        newContainer.RegisterType(Of IPaymentNotesAccountPayableAdvanceRepository, PaymentNotesAccountPayableAdvanceRepository)()
        'Transfer
        newContainer.RegisterType(Of ITransfersAdminService, TransfersAdminService)()
        newContainer.RegisterType(Of ITransfersRepository, TransfersRepository)()
        'AgesPayments
        newContainer.RegisterType(Of IAgesPaymentAdminService, AgesPaymentAdminService)()
        newContainer.RegisterType(Of IAgesPaymentsRepository, AgesPaymentsRepository)()
        'PaymentControl
        newContainer.RegisterType(Of IPaymentControlAdminService, PaymentControlAdminService)()
        newContainer.RegisterType(Of IPaymentControlRepository, PaymentControlRepository)()
        'MonthlyAmortization
        newContainer.RegisterType(Of IMonthlyAmortizationAdminService, MonthlyAmortizationAdminService)()
        newContainer.RegisterType(Of IMonthlyAmortizationRepository, MonthlyAmortizationRepository)()
        'DeferredCausationShare
        newContainer.RegisterType(Of IDeferredCausationShareAdminService, DeferredCausationShareAdminService)()
        newContainer.RegisterType(Of IDeferredCausationShareRepository, DeferredCausationShareRepository)()

        'Invoice
        newContainer.RegisterType(Of IInvoiceRepository, InvoiceRepository)()

        'Mantenimiento, Payroll y Glosas

        'newContainer.RegisterType(Of Infrastructure.Data.GlosasRepository.IGlosasUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
        '                                                                                                                                                    Return New GENESISEntitiesGlosas(ServerSessionValues.Current.CurrentContainer)
        '                                                                                                                                                End Function))

        newContainer.RegisterType(Of Infrastructure.Data.MaintenanceRepository.IMaintenanceModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                                      Return New MaintenanceModelUnitOfWork(container)
                                                                                                                                                                  End Function))
        newContainer.RegisterType(Of Infrastructure.Data.PayrollRepository.IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                         Return New PayrollUnitOfWork(container)
                                                                                                                                                     End Function))
        'newContainer.RegisterType(Of IMaintenanceUnitOfWork, GENESISEntitiesMaintenance)()

        'Customer
        newContainer.RegisterType(Of ICustomerRepository, CustomerRepository)()

        'CostCenter
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()
        'DistributionLine
        newContainer.RegisterType(Of IDistributionLinesRepository, DistributionLinesRepository)()
        'SupplierDistributionLine
        newContainer.RegisterType(Of ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository)()
        newContainer.RegisterType(Of ISupplierBankAccountRepository, SupplierBankAccountRepository)()
        'Supplier
        newContainer.RegisterType(Of Domain.Maintenance.ISupplierRepository, Infrastructure.Data.MaintenanceRepository.SupplierRepository)()

        'Accounting
        'Inyectamos el servicio WCF
        'newContainer.RegisterType(Of IAccountingService, AccountingService)()
        'Conceptos de retención
        newContainer.RegisterType(Of IRetentionConceptAdminService, RetentionConceptAdminService)()
        newContainer.RegisterType(Of IRetentionConceptRepository, RetentionConceptRepository)()
        'Secuencias numericas
        newContainer.RegisterType(Of IAccountingSequenseAdminService, AccountingSequenseAdminService)()
        newContainer.RegisterType(Of ISequenseAccountingCRepository, SequenseAccountingCRepository)()
        newContainer.RegisterType(Of ISequenseAccountingDRepository, SequenseAccountingDRepository)()
        'Clase contable
        newContainer.RegisterType(Of IAccountClassAdminService, AccountClassAdminService)()
        newContainer.RegisterType(Of IAccountClassRepository, AccountClassRepository)()
        'Niveles de cuentas
        newContainer.RegisterType(Of IAccountLevelAdminService, AccountLevelAdminService)()
        newContainer.RegisterType(Of IAccountLevelRepository, AccountLevelRepository)()
        'Tipos de documentos
        newContainer.RegisterType(Of IDocumentTypeAdminService, DocumentTypeAdminService)()
        newContainer.RegisterType(Of IDocumentTypeRepository, DocumentTypeRepository)()
        'Bloqueo de registros
        newContainer.RegisterType(Of IBlockRecordAccountingAdminService, BlockRecordAccountingAdminService)()
        newContainer.RegisterType(Of IBlockRecordAccountingRepository, BlockRecordAccountingRepository)()
        'Participación patrimonial
        newContainer.RegisterType(Of IPatrimonialPartAdminService, PatrimonialPartAdminService)()
        newContainer.RegisterType(Of IPatrimonialPartRepository, PatrimonialPartRepository)()
        'Anexos de declaración
        newContainer.RegisterType(Of IStatementFolioAdminService, StatementFolioAdminService)()
        newContainer.RegisterType(Of IStatementFolioRepository, StatementFolioRepository)()
        'MainAccount
        newContainer.RegisterType(Of IPUCAdminService, PUCAdminService)()
        newContainer.RegisterType(Of IPUCRepository, PUCRepository)()
        'Setting Account
        newContainer.RegisterType(Of ISettingAccountAdminService, SettingAccountAdminService)()
        newContainer.RegisterType(Of ISettingsAccountRepository, SettingAccountRepository)()
        '******************* document accounting
        newContainer.RegisterType(Of IAccountingDocumentRepository, DocumentAccountingRepository)()
        newContainer.RegisterType(Of IAccountingDocumentAdminService, AccountingDocumentAdminService)()
        'cierre de mes
        newContainer.RegisterType(Of ICloseMonthRepository, CloseMonthRepository)()
        newContainer.RegisterType(Of ICloseMonthAdminService, CloseMonthAdminService)()
        'balance
        newContainer.RegisterType(Of IAccountingBalanceRepository, AccountingBalanceRepository)()
        newContainer.RegisterType(Of IAccountingBalanceAdminService, AccountingBalanceAdminService)()
        'company settings
        newContainer.RegisterType(Of ICompanySettingsAdminService, CompanySettingsAdminService)()
        newContainer.RegisterType(Of ICompanySettingsRepository, CompanySettingsRepository)()

        'Crystal
        newContainer.RegisterType(Of IINPACIENTTOPANURepository, INPACIENTTOPANURepository)()
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        newContainer.RegisterType(Of ISEGrolesuRepository, SEGrolesuRepository)()
        newContainer.RegisterType(Of ISEGgruusuRepository, SEGgruusuRepository)()
        newContainer.RegisterType(Of ISEGusuaruRepository, SegusuaruRepository)()

        'Conceptos de honoriarios medicos glosados
        newContainer.RegisterType(Of IGlosaMedicalFeesConceptsAdminService, GlosaMedicalFeesConceptsAdminService)()
        newContainer.RegisterType(Of IGlosaMedicalFeesConceptsRepository, GlosaMedicalFeesConceptsRepository)()

        'Conceptos de honoriarios medicos glosados
        newContainer.RegisterType(Of IGlosaMedicalFeesAdminService, GlosaMedicalFeesAdminService)()
        newContainer.RegisterType(Of IGlosaMedicalFeesRepository, GlosaMedicalFeesRepository)()

        newContainer.RegisterType(Of IRateManualValidityRepository, RateManualValidityRepository)()

        newContainer.RegisterType(Of IContractExternalClientsRepository, ContractExternalClientsRepository)()

        newContainer.RegisterType(Of Application.EventHandlers.IEventProxy, Application.EventHandlers.Proxies.AzureServiceBusProxy)()
        newContainer.RegisterType(Of IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        newContainer.RegisterType(Of IViewListDiagnosticImagingAmbulatoryRepository, ViewListDiagnosticImagingAmbulatoryRepository)()
        newContainer.RegisterType(Of IViewListDiagnosticImagingRepository, ViewListDiagnosticImagingRepository)()

        newContainer.RegisterType(Of IAccountReceivableRepository, AccountReceivableRepository)()
        newContainer.RegisterType(Of ICostDistributionDirectCostRepository, CostDistributionDirectCostRepository)()

        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)()

        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class