using Application.Accounting;
using Application.APM;
using Application.APM.Newrelic;
using Application.Contract;
using Application.MedicalFees;
using Application.Payments;
using DistributedService.Causation.Services;
using Domain.Crystal;
using Domain.Entities;
using Domain.Entities.Service;
using Domain.Payroll;
using Domain.Security;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.Data.CrystalRepository;
using Infrastructure.Data.MaintenanceRepository;
using Infrastructure.Data.ModelRepository;
using Infrastructure.Data.PayrollRepository;
using Infrastructure.Data.SecurityRepository;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace DistributedService.Causation.Unity
{
    public sealed class Container
    {
        private static IUnityContainer _currentContainer;

        /// <summary>
        /// Obtiene la unica instancia del contenedor
        /// </summary>
        /// <returns>Contenedor configurado</returns>
        public static IUnityContainer Current(string container, string hisContainer)
        {
            if (_currentContainer != null)
            {
                ICommonVariables sessionVariables = _currentContainer.Resolve<ICommonVariables>();
                if (sessionVariables.getContainer() == container)
                {
                    return _currentContainer;
                }
            }

            ConfigureContainer(container, hisContainer);

            return _currentContainer;
        }

        private static void ConfigureContainer(string container, string hisContainer)
        {
            var newContainer = new UnityContainer();
            newContainer.RegisterFactory<ICommonVariables>((uc) =>
            {
                return new CommonVariables(container, hisContainer);
            }, new PerResolveLifetimeManager());

            newContainer.RegisterType<IGlobalModelUnitOfWork>(new PerResolveLifetimeManager(), new InjectionFactory(m => new GlobalModelUnitOfWork(container)));


            newContainer.RegisterFactory<ICrystalModelUnitOfWork>((uc) =>
            {
                var dd = new CrystalModelUnitOfWork(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            newContainer.RegisterFactory<IMaintenanceModelUnitOfWork>((uc) =>
            {
                var dd = new MaintenanceModelUnitOfWork(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            newContainer.RegisterFactory<IPayrollUnitOfWork>((uc) =>
            {
                var dd = new PayrollUnitOfWork(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            newContainer.RegisterFactory<ISeguridadUnitOfWork>((uc) =>
            {
                var dd = new GenesisEntities(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            newContainer.RegisterType<ApmHandler, NewrelicApm>();
            newContainer.RegisterType<ICausationPendingRepository, CausationPendingRepository>();
            newContainer.RegisterType<IViewListNoSurgicalRepository, ViewListNoSurgicalRepository>();
            newContainer.RegisterType<ISupplierRepository, Infrastructure.Data.ModelRepository.SupplierRepository>();
            newContainer.RegisterType<IDefinitionRateDetailRepository, DefinitionRateDetailRepository>();
            newContainer.RegisterType<IIPSServiceGroupRepository, IPSServiceGroupRepository>();
            newContainer.RegisterType<ICareGroupDefinitionRateRepository, CareGroupDefinitionRateRepository>();
            newContainer.RegisterType<IDefinitionRateDetailConditionRepository, DefinitionRateDetailConditionRepository>();
            newContainer.RegisterType<IMedicalFeesSequenseAdminService, MedicalFeesSequenseAdminService>();
            newContainer.RegisterType<IMedicalFeesSecuenceRepository, MedicalFeesSecuenceRepository>();
            newContainer.RegisterType<IMedicalFeesSecuenceDetailRepository, MedicalFeesSecuenceDetailRepository>();
            newContainer.RegisterType<IBlockRecordMedicalFeesAdminService, BlockRecordMedicalFeesAdminService>();
            newContainer.RegisterType<IBlockRecordMedicalFeesRepository, BlockRecordMedicalFeesRepository>();
            newContainer.RegisterType<IMedicalFeesContractAdminService, MedicalFeesContractAdminService>();
            newContainer.RegisterType<IMedicalFeesContractRepository, MedicalFeesContractRepository>();
            newContainer.RegisterType<IMedicalFeesCausationAdminService, MedicalFeesCausationAdminService>();
            newContainer.RegisterType<IMedicalFeesCausationRepository, MedicalFeesCausationRepository>();
            newContainer.RegisterType<IMedicalFeesSettingsAdminService, MedicalFeesSettingsAdminService>();
            newContainer.RegisterType<IMedicalFeesSettingsRepository, MedicalFeesSettingsRepository>();
            newContainer.RegisterType<IMedicalFeesLiquidationAdminService, MedicalFeesLiquidationAdminService>();
            newContainer.RegisterType<IMedicalFeesLiquidationRepository, MedicalFeesLiquidationRepository>();
            newContainer.RegisterType<IMedicalFeesNoteAdminService, MedicalFeesNoteAdminService>();
            newContainer.RegisterType<IMedicalFeesNoteRepository, MedicalFeesNoteRepository>();
            newContainer.RegisterType<IMedicalFeesLiquidationDetailRepository, MedicalFeesLiquidationDetailRepository>();
            newContainer.RegisterType<IHealthProfessionalContractAdminService, HealthProfessionalContractAdminService>();
            newContainer.RegisterType<IHealthProfessionalContractRepository, HealthProfessionalContractRepository>();
            newContainer.RegisterType<IInvoicePortfolioAdvanceRepository, InvoicePortfolioAdvanceRepository>();
            newContainer.RegisterType<Domain.Entities.IContractRepository, Infrastructure.Data.ModelRepository.ContractRepository>();
            newContainer.RegisterType<IRateManualRepository, RateManualRepository>();
            newContainer.RegisterType<IRateManualDetailRepository, RateManualDetailRepository>();
            newContainer.RegisterType<IRateManualDetailSurgicalRepository, RateManualDetailSurgicalRepository>();
            newContainer.RegisterType<IHealthProfessionalRepository, HealthProfessionalRepository>();
            newContainer.RegisterType<ICupsEntityRepository, CupsEntityRepository>();
            newContainer.RegisterType<ICupsEntityContractDescriptionsRepository, CUPSEntityContractDescriptionsRepository>();
            newContainer.RegisterType<IContractPackageServiceRepository, ContractPackageServiceRepository>();
            newContainer.RegisterType<IIPSServicesRepository, IPSServicesRepository>();
            newContainer.RegisterType<ICareGroupRepository, CareGroupRepository>();
            newContainer.RegisterType<IProcedureCupsRepository, ProcedureCupsRepository>();
            newContainer.RegisterType<ICupsHomologationRepository, CupsHomologationRepository>();
            newContainer.RegisterType<ISurgicalProcedureServiceRepository, SurgicalProcedureServiceRepository>();
            newContainer.RegisterType<IDefinitionRateAdminService, DefinitionRateAdminService>();
            newContainer.RegisterType<IDefinitionRateRepository, DefinitionRateRepository>();
            newContainer.RegisterType<IDefinitionRateDetailAdminService, DefinitionRateDetailAdminService>();
            newContainer.RegisterType<IDefinitionRateDetailRepository, DefinitionRateDetailRepository>();
            newContainer.RegisterType<IServiceOrderDetailRepository, ServiceOrderDetailRepository>();
            newContainer.RegisterType<IServiceOrderDetailSurgicalRepository, ServiceOrderDetailSurgicalRepository>();
            newContainer.RegisterType<IServiceOrderDetailDistributionRepository, ServiceOrderDetailDistributionRepository>();
            newContainer.RegisterType<IFunctionalUnitRepository, FunctionalUnitRepository>();
            newContainer.RegisterType<IRevenueControlDetailRepository, RevenueControlDetailRepository>();
            newContainer.RegisterType<IProductRateDetailRepository, ProductRateDetailRepository>();
            newContainer.RegisterType<IInventoryProductRepository, InventoryProductRepository>();
            newContainer.RegisterType<IRevenueControlRepository, RevenueControlRepository>();
            newContainer.RegisterType<IPatientRepository, PatientRepository>();
            newContainer.RegisterType<IBillingAuthorizationRepository, BillingAuthorizationRepository>();
            newContainer.RegisterType<ISettingsBillingRepository, SettingsBillingRepository>();
            newContainer.RegisterType<ISettingInventoryRepository, SettingInventoryRepository>();
            newContainer.RegisterType<IBillingServices, BillingServices>();
            newContainer.RegisterType<IContractServices, ContractServices>();
            newContainer.RegisterType<IProductGroupsRepository, ProductGroupsRepository>();
            newContainer.RegisterType<IBillingConceptRepository, BillingConceptRepository>();
            newContainer.RegisterType<IPaymentsSequenseAdminService, PaymentsSequenseAdminService>();
            newContainer.RegisterType<ISequensePaymentsCRepository, SequensePaymentsCRepository>();
            newContainer.RegisterType<ISequensePaymentsDRepository, SequensePaymentsDRepository>();
            newContainer.RegisterType<IPaymentsConceptAdminService, PaymentsConceptAdminService>();
            newContainer.RegisterType<IPaymentsConceptRepository, PaymentsConceptRepository>();
            newContainer.RegisterType<IBlockRecordPaymentsAdminService, BlockRecordPaymentsAdminService>();
            newContainer.RegisterType<IBlockRecordPaymentsRepository, BlockRecordPaymentsRepository>();
            newContainer.RegisterType<IPaymentsNoteConceptAdminService, PaymentsNoteConceptAdminService>();
            newContainer.RegisterType<IPaymentsNoteConceptRepository, PaymentsNoteConceptRepository>();
            newContainer.RegisterType<IAccountPayableAdminService, AccountPayableAdminService>();
            newContainer.RegisterType<IAccountPayableRepository, AccountPayableRepository>();
            newContainer.RegisterType<ISettingPaymentsAdminService, SettingPaymentsAdminService>();
            newContainer.RegisterType<ISettingPaymentsRepository, SettingPaymentsRepository>();
            newContainer.RegisterType<IOpeningBalanceAdminService, OpeningBalanceAdminService>();
            newContainer.RegisterType<IOpeningBalanceRepository, OpeningBalanceRepository>();
            newContainer.RegisterType<IMoneyAdvanceAdminService, MoneyAdvanceAdminService>();
            newContainer.RegisterType<IMoneyAdvanceRepository, MoneyAdvanceRepository>();
            newContainer.RegisterType<INotesDebitCreditAdminService, NotesDebitCreditAdminService>();
            newContainer.RegisterType<INotesDebitCreditRepository, NotesDebitCreditRepository>();
            newContainer.RegisterType<IDeferredCausationAdminService, DeferredCausationAdminService>();
            newContainer.RegisterType<IDeferredCausationRepository, DeferredCausationRepository>();
            newContainer.RegisterType<IMovementAccountPayableAdminService, MovementAccountPayableAdminService>();
            newContainer.RegisterType<IMovementAccountPayableRepository, MovementAccountPayableRepository>();
            newContainer.RegisterType<IPaymentNotesAccountPayableAdvanceAdminService, PaymentNotesAccountPayableAdvanceAdminService>();
            newContainer.RegisterType<IPaymentNotesAccountPayableAdvanceRepository, PaymentNotesAccountPayableAdvanceRepository>();
            newContainer.RegisterType<ITransfersAdminService, TransfersAdminService>();
            newContainer.RegisterType<ITransfersRepository, TransfersRepository>();
            newContainer.RegisterType<IAgesPaymentAdminService, AgesPaymentAdminService>();
            newContainer.RegisterType<IAgesPaymentsRepository, AgesPaymentsRepository>();
            newContainer.RegisterType<IPaymentControlAdminService, PaymentControlAdminService>();
            newContainer.RegisterType<IPaymentControlRepository, PaymentControlRepository>();
            newContainer.RegisterType<IMonthlyAmortizationAdminService, MonthlyAmortizationAdminService>();
            newContainer.RegisterType<IMonthlyAmortizationRepository, MonthlyAmortizationRepository>();
            newContainer.RegisterType<IDeferredCausationShareAdminService, DeferredCausationShareAdminService>();
            newContainer.RegisterType<IDeferredCausationShareRepository, DeferredCausationShareRepository>();
            newContainer.RegisterType<IInvoiceRepository, InvoiceRepository>();
            newContainer.RegisterType<ICustomerRepository, CustomerRepository>();
            newContainer.RegisterType<ICostCenterRepository, CostCenterRepository>();
            newContainer.RegisterType<IDistributionLinesRepository, DistributionLinesRepository>();
            newContainer.RegisterType<ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository>();
            newContainer.RegisterType<ISupplierBankAccountRepository, SupplierBankAccountRepository>();
            newContainer.RegisterType<Domain.Maintenance.ISupplierRepository, Infrastructure.Data.MaintenanceRepository.SupplierRepository>();
            newContainer.RegisterType<IRetentionConceptAdminService, RetentionConceptAdminService>();
            newContainer.RegisterType<IRetentionConceptRepository, RetentionConceptRepository>();
            newContainer.RegisterType<IAccountingSequenseAdminService, AccountingSequenseAdminService>();
            newContainer.RegisterType<ISequenseAccountingCRepository, SequenseAccountingCRepository>();
            newContainer.RegisterType<ISequenseAccountingDRepository, SequenseAccountingDRepository>();
            newContainer.RegisterType<IAccountClassAdminService, AccountClassAdminService>();
            newContainer.RegisterType<IAccountClassRepository, AccountClassRepository>();
            newContainer.RegisterType<IAccountLevelAdminService, AccountLevelAdminService>();
            newContainer.RegisterType<IAccountLevelRepository, AccountLevelRepository>();
            newContainer.RegisterType<IDocumentTypeAdminService, DocumentTypeAdminService>();
            newContainer.RegisterType<IDocumentTypeRepository, DocumentTypeRepository>();
            newContainer.RegisterType<IBlockRecordAccountingAdminService, BlockRecordAccountingAdminService>();
            newContainer.RegisterType<IBlockRecordAccountingRepository, BlockRecordAccountingRepository>();
            newContainer.RegisterType<IPatrimonialPartAdminService, PatrimonialPartAdminService>();
            newContainer.RegisterType<IPatrimonialPartRepository, PatrimonialPartRepository>();
            newContainer.RegisterType<IStatementFolioAdminService, StatementFolioAdminService>();
            newContainer.RegisterType<IStatementFolioRepository, StatementFolioRepository>();
            newContainer.RegisterType<IPUCAdminService, PUCAdminService>();
            newContainer.RegisterType<IPUCRepository, PUCRepository>();
            newContainer.RegisterType<ISettingAccountAdminService, SettingAccountAdminService>();
            newContainer.RegisterType<ISettingsAccountRepository, SettingAccountRepository>();
            newContainer.RegisterType<IAccountingDocumentRepository, DocumentAccountingRepository>();
            newContainer.RegisterType<IAccountingDocumentAdminService, AccountingDocumentAdminService>();
            newContainer.RegisterType<ICloseMonthRepository, CloseMonthRepository>();
            newContainer.RegisterType<ICloseMonthAdminService, CloseMonthAdminService>();
            newContainer.RegisterType<IAccountingBalanceRepository, AccountingBalanceRepository>();
            newContainer.RegisterType<IAccountingBalanceAdminService, AccountingBalanceAdminService>();
            newContainer.RegisterType<ICompanySettingsAdminService, CompanySettingsAdminService>();
            newContainer.RegisterType<ICompanySettingsRepository, CompanySettingsRepository>();
            newContainer.RegisterType<IINPACIENTTOPANURepository, INPACIENTTOPANURepository>();
            newContainer.RegisterType<IThirdPartyRepository, ThirdPartyRepository>();
            newContainer.RegisterType<IFormatosExogena, FormatosExogena>();
            newContainer.RegisterType<ISEGrolesuRepository, SEGrolesuRepository>();
            newContainer.RegisterType<ISEGgruusuRepository, SEGgruusuRepository>();
            newContainer.RegisterType<ISEGusuaruRepository, SegusuaruRepository>();
            newContainer.RegisterType<IGlosaMedicalFeesConceptsAdminService, GlosaMedicalFeesConceptsAdminService>();
            newContainer.RegisterType<IGlosaMedicalFeesConceptsRepository, GlosaMedicalFeesConceptsRepository>();
            newContainer.RegisterType<IGlosaMedicalFeesAdminService, GlosaMedicalFeesAdminService>();
            newContainer.RegisterType<IGlosaMedicalFeesRepository, GlosaMedicalFeesRepository>();
            newContainer.RegisterType<IRateManualValidityRepository, RateManualValidityRepository>();
            newContainer.RegisterType<IContractExternalClientsRepository, ContractExternalClientsRepository>();
            newContainer.RegisterType<IViewListSurgicalAndPackageRepository, ViewListSurgicalAndPackageRepository>();
            
            newContainer.RegisterType<ICausationService, CausationService>();
            newContainer.RegisterType<IViewListDiagnosticImagingRepository, ViewListDiagnosticImagingRepository>();
            newContainer.RegisterType<IViewListDiagnosticImagingAmbulatoryRepository, ViewListDiagnosticImagingAmbulatoryRepository>();
            newContainer.RegisterType<IRateManualValidityDetailRepository, RateManualValidityDetailRepository>();

            newContainer.RegisterType<IContainersRepository, ContainersRepository>();
            newContainer.RegisterType<IFactoryQueue, FactoryQueue>();

            _currentContainer = newContainer;
        }
    }
}