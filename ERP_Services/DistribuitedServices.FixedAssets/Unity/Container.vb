#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities.Service
Imports Application.Payments
Imports Infrastructure.Data.MaintenanceRepository
Imports Infrastructure.Data.PayrollRepository
Imports Domain.Payroll
Imports Application.Common
Imports Application.Accounting
Imports Application.Portfolio
Imports System.ServiceModel
Imports DistributedServices.Authentication
Imports Infrastructure.CrossCutting.Queue
Imports Domain.Security
Imports Infrastructure.Data.SecurityRepository
Imports Application.EventHandlers
Imports Application.EventHandlers.Proxies

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
        Dim injector As New ModuleInjector()
        injector.Load(newContainer, GetType(Domain.Base.Inject))
        newContainer.RegisterType(Of ICommonVariables)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                 Return New CommonVariables(container, hisContainer)
                                                                                                             End Function))
        'Inyectamos el contexto
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IFixedAssetService, FixedAssetService)()
        newContainer.RegisterType(Of IEventProxy, AzureServiceBusProxy)()
        'Secuencias numericas
        newContainer.RegisterType(Of IFixedAssetSequenseAdminService, FixedAssetSequenseAdminService)()
        newContainer.RegisterType(Of IFixedAssetSequenceRepository, FixedAssetSequenceRepository)()
        newContainer.RegisterType(Of IFixedAssetSequenceDetailRepository, FixedAssetSequenceDetailRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordFixedAssetAdminService, BlockRecordFixedAssetAdminService)()
        newContainer.RegisterType(Of IBlockRecordFixedAssetRepository, BlockRecordFixedAssetRepository)()
        'Ubicaciones
        newContainer.RegisterType(Of IFixedAssetLocationAdminService, FixedAssetLocationAdminService)()
        newContainer.RegisterType(Of IFixedAssetLocationRepository, FixedAssetLocationRepository)()
        'Marcas
        newContainer.RegisterType(Of IFixedAssetTrademarkAdminService, FixedAssetTrademarkAdminService)()
        newContainer.RegisterType(Of IFixedAssetTrademarkRepository, FixedAssetTrademarkRepository)()
        'Tipos de Poliza
        newContainer.RegisterType(Of IFixedAssetPolicyTypeAdminService, FixedAssetPolicyTypeAdminService)()
        newContainer.RegisterType(Of IFixedAssetPolicyTypeRepository, FixedAssetPolicyTypeRepository)()
        'Poliza
        newContainer.RegisterType(Of IFixedAssetPolicyAdminService, FixedAssetPolicyAdminService)()
        newContainer.RegisterType(Of IFixedAssetPolicyRepository, FixedAssetPolicyRepository)()
        'Aseguradoras
        newContainer.RegisterType(Of IFixedAssetInsuranceAdminService, FixedAssetInsuranceAdminsService)()
        newContainer.RegisterType(Of IFixedAssetInsuranceRepository, FixedAssetInsuranceRepository)()
        'Partes, Accesorios y Consumibles
        newContainer.RegisterType(Of IFixedAssetPartsAccesoriesConsumablesAdminService, FixedAssetPartsAccesoriesConsumablesAdminService)()
        newContainer.RegisterType(Of IFixedAssetPartsAccesoriesConsumablesRepository, FixedAssetPartsAccesoriesConsumablesRepository)()
        'Parametros
        newContainer.RegisterType(Of ISettingFixedAssetAdminService, SettingFixedAssetAdminService)()
        newContainer.RegisterType(Of ISettingFixedAssetRepository, SettingFixedAssetRepository)()
        'Catalogo de Equipos
        newContainer.RegisterType(Of IEquipmentCatalogAdminService, EquipmentCatalogAdminService)()
        newContainer.RegisterType(Of IFixedAssetItemCatalogRepository, FixedAssetItemCatalogRepository)()
        newContainer.RegisterType(Of IFixedAssetItemCatalogDetailRepository, FixedAssetItemCatalogDetailRepository)()
        'Tipos de Inventarios
        newContainer.RegisterType(Of IFixedAssetInventoryTypeAdminService, FixedAssetInventoryTypeAdminService)()
        newContainer.RegisterType(Of IFixedAssetInventoryTypeRepository, FixedAssetInventoryTypeRepository)()
        'Tipos de Equipos
        newContainer.RegisterType(Of IFixedAssetItemTypeAdminService, FixedAssetItemTypeAdminService)()
        newContainer.RegisterType(Of IFixedAssetItemTypeRepository, FixedAssetItemTypeRepository)()
        'Tipos de Responsable
        newContainer.RegisterType(Of IFixedAssetResponsibleTypeAdminService, FixedAssetResponsibleTypeAdminService)()
        newContainer.RegisterType(Of IFixedAssetResponsibleTypeRepository, FixedAssetResponsibleTypeRepository)()
        'Tipo de Vinculacion
        newContainer.RegisterType(Of IFixedAssetVinculationTypeAdminService, FixedAssetVinculationTypeAdminService)()
        newContainer.RegisterType(Of IFixedAssetVinculationTypeRepository, FixedAssetVinculationTypeRepository)()
        'Responsables
        newContainer.RegisterType(Of IFixedAssetResponsibleAdminService, FixedAssetResponsibleAdminService)()
        newContainer.RegisterType(Of IFixedAssetResponsibleRepository, FixedAssetResponsibleRepository)()
        'PhysicalAsset
        newContainer.RegisterType(Of IFixedAssetPhysicalAssetRepository, FixedAssetPhysicalAssetRepository)()
        'remisión de Entrada
        newContainer.RegisterType(Of IFixedAssetRemissionEntranceAdminService, FixedAssetRemissionEntranceAdminService)()
        newContainer.RegisterType(Of IFixedAssetFixedAssetRemissionEntranceRepository, FixedAssetFixedAssetRemissionEntranceRepository)()
        'Tipos de Equipo x Partes, Accesorios y Consumibles
        newContainer.RegisterType(Of IFixedAssetItemTypePartsAccesoriesConsumablesAdminService, FixedAssetItemTypePartsAccesoriesConsumablesAdminService)()
        newContainer.RegisterType(Of IFixedAssetItemTypePartsAccesoriesConsumablesRepository, FixedAssetItemTypePartsAccesoriesRepository)()
        'Ingreso de Activos Fijos
        newContainer.RegisterType(Of IFixedAssetEntryAdminService, FixedAssetEntryAdminService)()
        newContainer.RegisterType(Of IFixedAssetEntryRepository, FixedAssetEntryRepository)()
        newContainer.RegisterType(Of IFixedAssetEntryItemRepository, FixedAssetEntryItemRepository)()

        newContainer.RegisterType(Of ICurrencyRepository, CurrencyRepository)()

        'Catalogo de bienes y serviciocs
        newContainer.RegisterType(Of IFixedAssetCatalogOfPropertyandServicesAdminService, FixedAssetCatalogOfPropertyandServicesAdminService)()
        newContainer.RegisterType(Of IFixedAssetCatalogOfPropertyandServicesRepository, FixedAssetCatalogOfPropertyandServicesRepository)()

        newContainer.RegisterType(Of ICostDistributionDirectCostRepository, CostDistributionDirectCostRepository)()
        ' Ordenes de Compras de Activos Fijos
        newContainer.RegisterType(Of IFixedAssetPurchaseOrderAdminService, FixedAssetPurchaseOrderAdminService)()
        newContainer.RegisterType(Of IFixedAssetPurchaseOrderRepository, FixedAssetPurchaseOrderRepository)()
        ' Equipos
        newContainer.RegisterType(Of IFixedAssetItemAdminService, FixedAssetItemAdminService)()
        newContainer.RegisterType(Of IFixedAssetItemAllRepository, FixedAssetItemAllRepository)()
        'Valorización/Desvalorización
        newContainer.RegisterType(Of IFixedAssetValorizationAdminService, FixedAssetValorizationAdminService)()
        newContainer.RegisterType(Of IFixedAssetValorizationRepository, FixedAssetValorizationRepository)()
        'Tipo de Localización
        newContainer.RegisterType(Of IFixedAssetLocationTypeAdminService, FixedAssetLocationTypeAdminService)()
        newContainer.RegisterType(Of IFixedAssetLocationTypeRepository, FixedAssetLocationTypeRepository)()
        'Saldo Inicial
        newContainer.RegisterType(Of IFixedAssetInitialBalanceAdminService, FixedAssetInitialBalanceAdminService)()
        newContainer.RegisterType(Of IFixedAssetInitialBalanceRepository, FixedAssetInitialBalanceRepository)()
        'Traslado de activos
        newContainer.RegisterType(Of IFixedAssetTransferAdminService, FixedAssetTransferAdminService)()
        newContainer.RegisterType(Of IFixedAssetTransferRepository, FixedAssetTransferRepository)()

        'Moneda
        newContainer.RegisterType(Of ICurrencyAdminService, CurrencyAdminService)()
        newContainer.RegisterType(Of ISettingsBillingRepository, SettingsBillingRepository)()


        'Tipos de baja de activos fijos
        newContainer.RegisterType(Of IFixedAssetRetirementTypesAdminService, FixedAssetRetirementTypesAdminService)()
        newContainer.RegisterType(Of IFixedAssetRetirementTypesRepository, FixedAssetRetirementTypesRepository)()
        'Salida de activos
        newContainer.RegisterType(Of IFixedAssetActiveOutputAdminService, FixedAssetActiveOutputAdminService)()
        newContainer.RegisterType(Of IFixedAssetActiveOutputRepository, FixedAssetActiveOutputRepository)()
        'Activos Fijos
        newContainer.RegisterType(Of IFixedAssetPhysicalAssetAdminService, FixedAssetPhysicalAssetAdminService)()
        newContainer.RegisterType(Of IFixedAssetPhysicalAssetRepository, FixedAssetPhysicalAssetRepository)()
        'Cambio de placa
        newContainer.RegisterType(Of IFixedAssetChangePlateAdminService, FixedAssetChangePlateAdminService)()
        newContainer.RegisterType(Of IFixedAssetChangePlateRepository, FixedAssetChangePlateRepository)()
        'Depreciación
        newContainer.RegisterType(Of IFixedAssetDepreciationAdminService, FixedAssetDepreciationAdminService)()
        newContainer.RegisterType(Of IFixedAssetDepreciationRepository, FixedAssetDepreciationRepository)()
        'Devolución de ingreso de activos
        newContainer.RegisterType(Of IFixedAssetEntryDevolutionAdminService, FixedAssetEntryDevolutionAdminService)()
        newContainer.RegisterType(Of IFixedAssetEntryDevolutionRepository, FixedAssetEntryDevolutionRepository)()
        ''Detalle orden de compra
        newContainer.RegisterType(Of IFixedAssetPurchaseOrderItemRepository, FixedAssetPurchaseOrderItemRepository)()
        'Dominio
        newContainer.RegisterType(Of IFixedAssetServices, FixedAssetServices)()
        'Estados
        newContainer.RegisterType(Of IFixedAssetStatusAdminService, FixedAssetStatusAdminService)()
        newContainer.RegisterType(Of IFixedAssetStatusRepository, FixedAssetStatusRepository)()
        'Reclasificación de Activos
        newContainer.RegisterType(Of IFixedAssetReclassificationAdminService, FixedAssetReclassificationAdminService)()
        newContainer.RegisterType(Of IFixedAssetReclassificationRepository, FixedAssetReclassificationRepository)()
        'Indicios de deterioro
        newContainer.RegisterType(Of IDeteriorationIndicationAdminService, DeteriorationIndicationAdminService)()
        newContainer.RegisterType(Of IFixedAssetDeteriorationIndicationRepository, FixedAssetDeteriorationIndicationRepository)()

        'AccountReceivable
        newContainer.RegisterType(Of IAccountReceivableAdminService, AccountReceivableAdminService)()
        newContainer.RegisterType(Of ISequensePortfolioCRepository, SequensePortfolioCRepository)()
        newContainer.RegisterType(Of ISequensePortfolioDRepository, SequensePortfolioDRepository)()
        newContainer.RegisterType(Of IAccountReceivableRepository, AccountReceivableRepository)()

        'Secuencias numericas
        newContainer.RegisterType(Of IPaymentsSequenseAdminService, PaymentsSequenseAdminService)()
        newContainer.RegisterType(Of ISequensePaymentsCRepository, SequensePaymentsCRepository)()
        newContainer.RegisterType(Of ISequensePaymentsDRepository, SequensePaymentsDRepository)()
        'Conceptos
        newContainer.RegisterType(Of IPaymentsConceptAdminService, PaymentsConceptAdminService)()
        newContainer.RegisterType(Of IPaymentsConceptRepository, PaymentsConceptRepository)()
        'Tarifa IVA
        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()
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
        'FilingUnit
        newContainer.RegisterType(Of IFilingUnitAdminService, FilingUnitAdminService)()
        newContainer.RegisterType(Of IFilingUnitRepository, FilingUnitRepository)()
        'SupplierType
        newContainer.RegisterType(Of ISupplierTypeAdminService, SupplierTypeAdminService)()
        newContainer.RegisterType(Of ISupplierTypeRepository, SupplierTypeRepository)()
        'AccountPayableRejectionReason
        newContainer.RegisterType(Of IAccountPayableRejectionReasonAdminService, AccountPayableRejectionReasonAdminService)()
        newContainer.RegisterType(Of IAccountPayableRejectionReasonRepository, AccountPayableRejectionReasonRepository)()
        'AccountPayableTransfer
        newContainer.RegisterType(Of IAccountPayableTransferAdminService, AccountPayableTransferAdminService)()
        newContainer.RegisterType(Of IAccountPayableTransferRepository, AccountPayableTransferRepository)()
        'AccountPayableTransfer details
        newContainer.RegisterType(Of IAccountPayableTransferDetailRepository, AccountPayableTransferDetailRepository)()


        'Mantenimiento ny Payroll

        newContainer.RegisterType(Of Infrastructure.Data.MaintenanceRepository.IMaintenanceModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                                      Return New MaintenanceModelUnitOfWork(container)
                                                                                                                                                                  End Function))
        newContainer.RegisterType(Of Infrastructure.Data.PayrollRepository.IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                                                         Return New PayrollUnitOfWork(container)
                                                                                                                                                     End Function))
        'newContainer.RegisterType(Of IMaintenanceUnitOfWork, GENESISEntitiesMaintenance)()

        newContainer.RegisterType(Of ICostDistributionsRepository, CostDistributionsRepository)()
        'CostCenter
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()
        'DistributionLine
        newContainer.RegisterType(Of IDistributionLinesRepository, DistributionLinesRepository)()
        'SupplierDistributionLine
        newContainer.RegisterType(Of ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository)()
        'SupplierBankAccountRepository
        newContainer.RegisterType(Of ISupplierBankAccountRepository, SupplierBankAccountRepository)()

        'Supplier
        newContainer.RegisterType(Of Domain.Maintenance.ISupplierRepository, Infrastructure.Data.MaintenanceRepository.SupplierRepository)()
        '''''''''''''''''''''''''''''''''''''''''''''
        'supplier (Commons)
        newContainer.RegisterType(Of ISupplierAdminService, SupplierAdminService)()
        newContainer.RegisterType(Of ISupplierRepository, Infrastructure.Data.ModelRepository.SupplierRepository)()
        newContainer.RegisterType(Of Domain.Entities.IMaintenanceSequenceDetailRepository, Infrastructure.Data.ModelRepository.MaintenanceSequenceDetailRepository)(New TransientLifetimeManager)

        'Contexto de seguridad
        newContainer.RegisterType(Of ISeguridadUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                     Return New GenesisEntities()
                                                                                                                 End Function))

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
        'IndigoQueue
        newContainer.RegisterType(Of IContainersRepository, ContainersRepository)()
        newContainer.RegisterType(Of IFactoryQueue, FactoryQueue)()
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
        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()

        newContainer.RegisterType(Of IBookRepository, BookRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        'VieBot
        newContainer.RegisterType(Of IVieBotRepository, VieBotRepository)()

        'Partes de Activos Fijos
        newContainer.RegisterType(Of IFixedAssetPhysicalAssetPartAdminService, FixedAssetPhysicalAssetPartAdminService)()
        newContainer.RegisterType(Of IFixedAssetPhysicalAssetPartRepository, FixedAssetPhysicalAssetPartRepository)()

        'Reportes
        newContainer.RegisterType(Of Application.FixedAsset.IReportAdminService, Application.FixedAsset.ReportAdminService)()
        newContainer.RegisterType(Of IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()




        _currentContainer = newContainer
    End Sub

#End Region

End Class