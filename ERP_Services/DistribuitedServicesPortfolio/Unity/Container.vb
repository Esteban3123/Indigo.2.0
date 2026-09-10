#Region "Imports"

Imports System.ServiceModel
Imports Application.Accounting
Imports Application.EventHandlers
Imports Application.EventHandlers.Proxies
Imports Application.FileManager
Imports Application.Portfolio
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.PayrollRepository
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
        'Inyectamos el contexto
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))

        'Inyectamos el contexto de Crystal
        newContainer.RegisterType(Of ICrystalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                        Return New CrystalModelUnitOfWork(hisContainer)
                                                                                                                    End Function))


        newContainer.RegisterType(Of IEventProxy, AzureServiceBusProxy)()
        newContainer.RegisterType(Of IPortfolioMassiveConfirmAdminService, PortfolioMassiveConfirmAdminService)()
        newContainer.RegisterType(Of IBudgetAllocationInitialBalancesAdminService, BudgetAllocationInitialBalancesAdminService)()
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IPortfolioService, PortfolioService)()
        'servicios de dominio 
        newContainer.RegisterType(Of Domain.Entities.Service.IPortfolioService, Domain.Entities.Service.PortfolioServices)()
        'Secuencias numericas
        newContainer.RegisterType(Of IPortfolioSequenseAdminService, PortfolioSequenseAdminService)()

        newContainer.RegisterType(Of IHardCollectionAdminService, HardCollectionAdminService)()
        newContainer.RegisterType(Of IHardCollectionRepository, HardCollectionRepository)()

        newContainer.RegisterType(Of ISequensePortfolioCRepository, SequensePortfolioCRepository)()
        newContainer.RegisterType(Of ISequensePortfolioDRepository, SequensePortfolioDRepository)()
        'Indicadores economicos
        newContainer.RegisterType(Of IEconomicIndicatorAdminService, EconomicIndicatorAdminService)()
        newContainer.RegisterType(Of IEconomicIndicatorRepository, EconomicIndicatorRepository)()
        'Concepto de cartera
        newContainer.RegisterType(Of IAccountReceivableConceptAdminService, AccountReceivableConceptAdminService)()
        newContainer.RegisterType(Of IAccountReceivableConceptRepository, AccountReceivableConceptRepository)()

        'Abogado
        newContainer.RegisterType(Of ILawyerAdminService, LawyerAdminService)()
        newContainer.RegisterType(Of ILawyerRepository, LawyerRepository)()

        'Clasificacion deterioro de cartera
        newContainer.RegisterType(Of IPortfolioDeteriorationClassificationAdminService, PortfolioDeteriorationClassificationAdminService)()
        newContainer.RegisterType(Of IPortfolioDeteriorationClassificationRepository, PortfolioDeteriorationClassificationRepository)()

        'Rangos de provicion
        newContainer.RegisterType(Of IProvisionRangesAdminService, ProvisionRangesAdminService)()
        newContainer.RegisterType(Of IProvisionRangesRepository, ProvisionRangesRepository)()
        'Conceptos de nota
        newContainer.RegisterType(Of IPortfolioNoteConceptAdminService, PortfolioNoteConceptAdminService)()
        newContainer.RegisterType(Of IPortfolioNoteConceptRepository, PortfolioNoteConceptRepository)()
        'Bloqueo
        newContainer.RegisterType(Of IBlockRecordPortfolioAdminService, BlockRecordPortfolioAdminService)()
        newContainer.RegisterType(Of IBlockRecordPortfolioRepository, BlockRecordPortfolioRepository)()
        'Anticipo
        newContainer.RegisterType(Of IPortfolioAdvanceAdminService, PortfolioAdvanceAdminService)()
        newContainer.RegisterType(Of IPortfolioAdvanceRepository, PortfolioAdvanceRepository)()
        'cuotas
        newContainer.RegisterType(Of IAccountReceivableShareAdminService, AccountReceivableShareAdminService)()
        newContainer.RegisterType(Of IAccountReceivableShareRepository, AccountReceivableShareRepository)()
        'Edades de cartera
        newContainer.RegisterType(Of IAgePortfolioAdminService, AgePortfolioAdminService)()
        newContainer.RegisterType(Of IAgePortfolioRepository, AgePortfolioRepository)()
        'Provision
        newContainer.RegisterType(Of IPortfolioProvisionAdminService, PortfolioProvisionAdminService)()
        newContainer.RegisterType(Of IPortfolioProvisionRepository, PortfolioProvisionRepository)()
        'notas
        newContainer.RegisterType(Of IInvoiceRepository, InvoiceRepository)()
        newContainer.RegisterType(Of IPortfolioNoteAdminService, PortfolioNoteAdminService)()
        newContainer.RegisterType(Of IPortfolioNoteRepository, PortfolioNoteRepository)()
        'IVA
        newContainer.RegisterType(Of IGeneralLedgerIVARepository, GeneralLedgerIVARepository)()
        'portfolioNoteDistribution
        newContainer.RegisterType(Of IPortfolioNoteDistributionRepository, PortfolioNoteDistributionRepository)()
        newContainer.RegisterType(Of ICostDistributionsRepository, CostDistributionsRepository)()
        'control
        newContainer.RegisterType(Of IPortfolioControlRepository, PortfolioControlRepository)()
        'facturas
        newContainer.RegisterType(Of IAccountReceivableAdminService, AccountReceivableAdminService)()
        newContainer.RegisterType(Of IAccountReceivableRepository, AccountReceivableRepository)()
        'setting
        newContainer.RegisterType(Of ISettingPortfolioAdminService, SettingPortfolioAdminService)()
        newContainer.RegisterType(Of ISettingPortfolioRepository, SettingPortfolioRepository)()
        'saldos iniciales
        newContainer.RegisterType(Of IPortfolioInitialBalanceAdminService, PortfolioInitialBalanceAdminService)()
        newContainer.RegisterType(Of IPortfolioInitialBalanceRepository, PortfolioInitialBalanceRepository)()
        ' estructura contable de contratos (resuelve cuentas de saldos iniciales por code)
        newContainer.RegisterType(Of IContractAccountingStructureRepository, ContractAccountingStructureRepository)()
        'saldos iniciales
        newContainer.RegisterType(Of IPortfolioTransfersAdminService, PortfolioTransfersAdminService)()
        newContainer.RegisterType(Of IPortfolioTransferRepository, PortfolioTransferRepository)()
        'Documento de cuentas x pagar
        newContainer.RegisterType(Of IAccountReceivableDocumentAdminService, AccountReceivableDocumentAdminService)()
        newContainer.RegisterType(Of IAccountReceivableDocumentRepository, AccountReceivableDocumentRepository)()
        'file manager
        newContainer.RegisterType(Of IFileManagerInternalService, FileManagerInternalService)()
        newContainer.RegisterType(Of IPortfolioInitialBalanceAccountReceivableRepository, PortfolioInitialBalanceAccountReceivableRepository)()
        '******************* document accounting
        newContainer.RegisterType(Of IAccountingDocumentRepository, DocumentAccountingRepository)()
        newContainer.RegisterType(Of IAccountingDocumentAdminService, AccountingDocumentAdminService)()

        newContainer.RegisterType(Of IInvoicePortfolioAdvanceRepository, InvoicePortfolioAdvanceRepository)()
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
        'CircularZeroThirty
        newContainer.RegisterType(Of ICircularZeroThirtyAdminService, CircularZeroThirtyAdminService)()
        newContainer.RegisterType(Of ICircularZeroThirtyRepository, CircularZeroThirtyRepository)()
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
        'reclasificacion de documentos
        newContainer.RegisterType(Of IPortfolioReclassificationAdminService, PortfolioReclassificationAdminService)()
        newContainer.RegisterType(Of IReclassificationRepository, ReclassificationRepository)()

        'Paciente
        newContainer.RegisterType(Of IPatientRepository, PatientRepository)()

        'demand status
        newContainer.RegisterType(Of IPortfolioDemandStatusAdminService, PortfolioDemandStatusAdminService)()
        newContainer.RegisterType(Of IPortfolioDemandStatusRepository, PortfolioDemandStatusRepository)()

        newContainer.RegisterType(Of IBillingInvoiceCategories, BillingInvoiceCategories)()

        '**********************Glosas
        'newContainer.RegisterType(Of IGlosasUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
        '                                                                                                               Return New GENESISEntitiesGlosas(ServerSessionValues.Current.CurrentContainer)
        '                                                                                                           End Function))
        newContainer.RegisterType(Of ICustomerRepository, CustomerRepository)()

        newContainer.RegisterType(Of IThirdPartyRepository, ThirdPartyRepository)()
        '*********************************Payroll
        newContainer.RegisterType(Of IPayrollUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                   Return New PayrollUnitOfWork(container)
                                                                                                               End Function))
        newContainer.RegisterType(Of ICostCenterRepository, CostCenterRepository)()

        'Exogena Format
        newContainer.RegisterType(Of IFormatosExogena, FormatosExogena)()

        'Conceptos de conciliacion
        newContainer.RegisterType(Of IPortfolioConciliationConceptsAdminService, PortfolioConciliationConceptsAdminService)()
        newContainer.RegisterType(Of IPortfolioConciliationConceptsRepository, PortfolioConciliationConceptsRepository)()

        'Repositorio usado para la Facturación Electrónica
        newContainer.RegisterType(Of IBillingSequenceRepository, BillingSequenceRepository)()
        newContainer.RegisterType(Of IBillingNoteRepository, BillingNoteRepository)()
        newContainer.RegisterType(Of IElectronicDocumentRepository, ElectronicDocumentRepository)()
        newContainer.RegisterType(Of IElectronicsPropertiesRepository, ElectronicsPropertiesRepository)()
        newContainer.RegisterType(Of IElectronicsRIPSRepository, ElectronicsRIPSRepository)()
        newContainer.RegisterType(Of IInitialBalanceInvoiceRepository, InitialBalanceInvoiceRepository)()
        newContainer.RegisterType(Of IOperatingUnitRepository, OperatingUnitRepository)()

        'NOTA: lookup Cosmos + carga InitialBalanceInvoiceDetail vive en RCM (DistributedService.RCM), no aquí.
        'Portfolio Confirm sólo crea AR + Invoice + ED + EP + ERIPS + InitialBalanceInvoice header (CosmosId NULL,
        'Status=1 = DetailPending). El frontend, tras Confirm exitoso, dispara endpoint RCM
        'POST /electronicRIPS/PopulateInitialBalanceDetail que hidrata Cosmos + inserta detail rows + UPDATE
        'IBI.Status=2 + IBI.CosmosId.

        'Conciliacion de cartera
        newContainer.RegisterType(Of IPortfolioConciliationAdminService, PortfolioConciliationAdminService)()
        newContainer.RegisterType(Of IPortfolioConciliationRepository, PortfolioConciliationRepository)()

        'Reportes
        newContainer.RegisterType(Of IReportAdminService, ReportAdminService)()
        newContainer.RegisterType(Of IRateManualValidityDetailRepository, RateManualValidityDetailRepository)()

        'Revalorizacion
        newContainer.RegisterType(Of IPortfolioRevaluationAdminService, PortfolioRevaluationAdminService)()
        newContainer.RegisterType(Of IPortfolioRevaluationRepository, PortfolioRevaluationRepository)()

        'Currency
        newContainer.RegisterType(Of ICurrencyRepository, CurrencyRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class