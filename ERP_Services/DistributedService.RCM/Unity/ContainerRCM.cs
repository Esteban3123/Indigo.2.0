using Application.AccountManagement;
using Application.Autentication.JwtService;
using Application.Billing;
using Application.Common;
using Application.Crystal;
using Application.EventHandlers;
using Application.EventHandlers.Proxies;
using Application.Glosas;
using DistribuitedServices.Billing;
using DistributedServices.AccountManagement;
using Domain.Autentication;
using Domain.Autentication.Interfaces;
using Domain.Billing.Repositories;
using Domain.Crystal;
using Domain.Crystal.Service;
using Domain.Entities;
using Domain.Entities.Service;
using Domain.Payroll;
using Domain.Security;
using Infrastructure.CrossCutting.Autentication.Secrets.AZKeyVault;
using Infrastructure.CrossCutting.Autentication.Secrets.CacheSecrect;
using Infrastructure.CrossCutting.AzureBlobStorage.Factory;
using Infrastructure.CrossCutting.AzureBlobStorage.Storage;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.Data.Billing.Repositories;
using Infrastructure.Data.CosmosModelRepository.Repositories.Billing;
using Infrastructure.Data.CrystalRepository;
using Infrastructure.Data.ModelRepository;
using Infrastructure.Data.PayrollRepository;
using Infrastructure.Data.SecurityRepository;
using System;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace DistributedService.Rest.Unity
{
    public sealed class ContainerRCM
    {
        private static IUnityContainer _currentContainer;

        /// <summary>
        /// Obtiene la unica instancia del contenedor
        /// </summary>
        /// <returns>Contenedor configurado</returns>
        public static IUnityContainer Current(string container,
                                              string hisContainer,
                                              string securityContainer = null,
                                              bool obligatoryCosmos = false)
        {
            if (_currentContainer != null)
            {
                ICommonVariables sessionVariables = _currentContainer.Resolve<ICommonVariables>();
                if (sessionVariables.getContainer() == container)
                {
                    return _currentContainer;
                }
            }

            string blobContainerName = ServerSessionValues.Current.BlobContainerName;


            ConfigureContainer(container, hisContainer, obligatoryCosmos, securityContainer, blobContainerName);

            return _currentContainer;
        }

        private static void ConfigureContainer(string container,
                                                string hisContainer,
                                                bool obligatoryCosmos,
                                                string securityContainer,
                                                string blobContainerName)
        {

            var newContainer = new UnityContainer();

            newContainer.RegisterFactory<ICommonVariables>((uc) =>
            {
                return new CommonVariables(container, hisContainer);
            }, new PerResolveLifetimeManager());

            ////Inyectamos el contexto de Billing
            newContainer.RegisterFactory<IGlobalModelUnitOfWork>((uc) =>
            {
                var dd = new GlobalModelUnitOfWork(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            ////Inyectamos el contexto de Crystal
            newContainer.RegisterFactory<ICrystalModelUnitOfWork>((uc) =>
            {
                var dd = new CrystalModelUnitOfWork(hisContainer);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            ////Inyectamos el contexto de Payroll
            newContainer.RegisterFactory<IPayrollUnitOfWork>((uc) =>
            {
                var dd = new PayrollUnitOfWork(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            ///Inyectamos Security Context
            newContainer.RegisterFactory<ISeguridadUnitOfWork>((uc) =>
            {
                var dd = new GenesisEntities(securityContainer);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            ////Inyectamos el contexto de la DB Cosmos
            newContainer.RegisterFactory<Infrastructure.Data.CosmosModelRepository.UnitOfWork.IUnitOfWork>((uc) =>
            {
                var dd = new Infrastructure.Data.CosmosModelRepository.UnitOfWork.UnitOfWork(SessionValues.Instance.CosmosDB, SessionValues.Instance.CosmosDbContainer);
                return dd;
            }, new PerResolveLifetimeManager());

            //Blob Instance
            newContainer.RegisterFactory<IFactoryStorage>((uc) =>
            {
                var dd = new FactoryStorage(blobContainerName);
                return dd;
            }, new PerResolveLifetimeManager());

            newContainer.RegisterType<IRevenueControlDetailRepository, RevenueControlDetailRepository>();
            newContainer.RegisterType<IServiceOrderDetailDistributionRepository, ServiceOrderDetailDistributionRepository>();
            newContainer.RegisterType<ICareGroupRepository, CareGroupRepository>();
            newContainer.RegisterType<IInvoiceRepository, InvoiceRepository>();
            newContainer.RegisterType<IThirdPartyRepository, ThirdPartyRepository>();
            newContainer.RegisterType<Domain.Entities.IContractRepository, Infrastructure.Data.ModelRepository.ContractRepository>();
            newContainer.RegisterType<IBillingInvoiceCategories, BillingInvoiceCategories>();
            newContainer.RegisterType<IConceptsCausesStatusFolioRepository, ConceptsCausesStatusFolioRepository>();
            newContainer.RegisterType<IPOSPathologiesRepository, POSPathologiesRepository>();
            newContainer.RegisterType<IHealthAdministratorRepository, HealthAdministratorRepository>();
            newContainer.RegisterType<IFolioRepository, FolioRepository>();

            newContainer.RegisterType<IFolioAdminService, FolioAdminService>();
            newContainer.RegisterType<IBillingServiceAccountControl, BillingService>();

            //Estancias
            newContainer.RegisterType<IParameterRepository, ParameterRepository>();
            newContainer.RegisterType<IStayRepository, StayRepository>();
            newContainer.RegisterType<IStayAdminService, StayAdminService>();
            newContainer.RegisterType<IStayService, StayService>();
            newContainer.RegisterType<ICupsEntityRepository, CupsEntityRepository>();
            newContainer.RegisterType<IFunctionalUnitRepository, FunctionalUnitRepository>();
            newContainer.RegisterType<IServiceOrderAdminService, ServiceOrderAdminService>();
            newContainer.RegisterType<IBillingSequenceDetailRepository, BillingSequenceDetailRepository>();
            newContainer.RegisterType<IIPSServicesRepository, IPSServicesRepository>();
            newContainer.RegisterType<IBillingSequenseAdminService, BillingSequenseAdminService>();
            newContainer.RegisterType<IRevenueControlRepository, RevenueControlRepository>();
            newContainer.RegisterType<IAdmissionRepository, AdmissionRepository>();
            newContainer.RegisterType<IBillingServices, BillingServices>();
            newContainer.RegisterType<IRateManualValidityRepository, RateManualValidityRepository>();
            newContainer.RegisterType<IRateManualRepository, RateManualRepository>();
            newContainer.RegisterType<ICupsHomologationRepository, CupsHomologationRepository>();
            newContainer.RegisterType<IBedRateRepository, BedRateRepository>();
            newContainer.RegisterType<IAccountControlStayRepository, AccountControlStayRepository>();
            newContainer.RegisterType<IServiceOrderRepository, ServiceOrderRepository>();
            newContainer.RegisterType<IStayDetailRepository, StayDetailRepository>();
            newContainer.RegisterType<IProductGroupsRepository, ProductGroupsRepository>();
            newContainer.RegisterType<IBillingConceptRepository, BillingConceptRepository>();
            newContainer.RegisterType<IBillingSequenceRepository, BillingSequenceRepository>();
            newContainer.RegisterType<ICostCenterRepository, CostCenterRepository>();
            newContainer.RegisterType<ISurgicalProcedureServiceRepository, SurgicalProcedureServiceRepository>();
            newContainer.RegisterType<IRateManualDetailSurgicalRepository, RateManualDetailSurgicalRepository>();
            newContainer.RegisterType<IRateManualDetailRepository, RateManualDetailRepository>();
            newContainer.RegisterType<IProductRateDetailRepository, ProductRateDetailRepository>();
            newContainer.RegisterType<IInventoryProductRepository, InventoryProductRepository>();
            newContainer.RegisterType<IPatientRepository, PatientRepository>();
            newContainer.RegisterType<IBillingAuthorizationRepository, BillingAuthorizationRepository>();
            newContainer.RegisterType<ISettingsBillingRepository, SettingsBillingRepository>();
            newContainer.RegisterType<IServiceOrderDetailRepository, ServiceOrderDetailRepository>();
            newContainer.RegisterType<IPUCRepository, PUCRepository>();
            newContainer.RegisterType<ISettingInventoryRepository, SettingInventoryRepository>();
            newContainer.RegisterType<ICustomerRepository, CustomerRepository>();
            newContainer.RegisterType<IContractServices, ContractServices>();
            newContainer.RegisterType<IINPACIENTTOPANURepository, INPACIENTTOPANURepository>();
            newContainer.RegisterType<ICareGroupDefinitionRateRepository, CareGroupDefinitionRateRepository>();
            newContainer.RegisterType<IDefinitionRateDetailConditionRepository, DefinitionRateDetailConditionRepository>();
            newContainer.RegisterType<IDefinitionRateDetailRepository, DefinitionRateDetailRepository>();
            newContainer.RegisterType<IIPSServiceGroupRepository, IPSServiceGroupRepository>();
            newContainer.RegisterType<IContractExternalClientsRepository, ContractExternalClientsRepository>();
            newContainer.RegisterType<IProcedureCupsRepository, ProcedureCupsRepository>();
            newContainer.RegisterType<ICompanySettingsRepository, CompanySettingsRepository>();
            newContainer.RegisterType<ILiquidationDataRepository, LiquidationDataRepository>();
            newContainer.RegisterType<IAccountControlJustificationRepository, AccountControlJustificationRepository>();
            newContainer.RegisterType<ICupsEntityContractDescriptionsRepository, CUPSEntityContractDescriptionsRepository>();
            newContainer.RegisterType<ICurrencyAdminService, CurrencyAdminService>();
            newContainer.RegisterType<ICurrencyRepository, CurrencyRepository>();
            newContainer.RegisterType<ISequenseAccountingDRepository, SequenseAccountingDRepository>();
            newContainer.RegisterType<IRateManualValidityDetailRepository, RateManualValidityDetailRepository>();
            newContainer.RegisterType<IGeneralLedgerIVARepository, GeneralLedgerIVARepository>();
            newContainer.RegisterType<IRIPSPlaneAdminService, RIPSPlaneAdminService>();
            newContainer.RegisterType<IRadicateInvoiceCRepository, RadicateInvoiceCRepository>();
            newContainer.RegisterType<IRadicateInvoiceDRepository, RadicateInvoiceDRepository>();
            newContainer.RegisterType<IRIPSPlane, RIPSPlane>();
            newContainer.RegisterType<IFactoryQueue, FactoryQueue>();
            newContainer.RegisterType<IBillingNoteRepository, BillingNoteRepository>();
            newContainer.RegisterType<IContainersRepository, ContainersRepository>();
            newContainer.RegisterType<IRIPSCosmosDbModelRepository, RIPSCosmosDbModelRepository>();
            newContainer.RegisterType<IElectronicsRIPSRepository, ElectronicsRIPSRepository>();
            newContainer.RegisterType<IDocumentsAssociatedRIPSRepository, DocumentsAssociatedRIPSRepository>();
            newContainer.RegisterType<Infrastructure.CrossCutting.AzureBlobStorage.IStorage, AzureBlobStorateService>();
            newContainer.RegisterType<Infrastructure.CrossCutting.AzureBlobStorage.IStorage, LocalStorateService>();
            newContainer.RegisterType<IAccountManagementParametersRepository, AccountManagementParametersRepository>();
            newContainer.RegisterType<IUsersAssignmentRepository, UsersAssignmentRepository>();
            newContainer.RegisterType<IUserNoveltiesRepository, UserNoveltiesRepository>();
            newContainer.RegisterType<IAccountManagementParametersAdminService, AccountManagementParametersAdminService>();
            _currentContainer = newContainer;

            try
            {
                
                var jwtSettings = new JwtSettings
                {
                    SecretName = ConfigurationManager.AppSettings["MySecretJwt"] ?? "JwtSettings--SecretKey",
                    IssuerSecretName = ConfigurationManager.AppSettings["JwtIssuer"] ?? "JwtSettings--Issuer",
                    AudienceSecretName = ConfigurationManager.AppSettings["JwtAudience"] ?? "JwtSettings--Audience",
                    ExpiresInMinutes = ConfigurationManager.AppSettings["JwtExpiresInMinutes"] ?? "JwtSettings--ExpirationMinutes"
                };
                newContainer.RegisterInstance<JwtSettings>(jwtSettings);

                var keyVaultUrl = ConfigurationManager.AppSettings["KeyVaultUrl"];
                var tenantId = ConfigurationManager.AppSettings["TenantId"];
                var clientId = ConfigurationManager.AppSettings["ClientId"];
                var clientSecret = ConfigurationManager.AppSettings["ClientSecretId"];
                if (string.IsNullOrEmpty(keyVaultUrl) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                {
                    Debug.WriteLine("JWT: KeyVaultUrl, TenantId, ClientId o ClientSecretId no configurados en appSettings. No se registrará ISecretProvider.");
                }
                else
                {
                    

                    // 1. IMemoryCache singleton
                    newContainer.RegisterFactory<IMemoryCache>((uc) => new MemoryCache(new MemoryCacheOptions()), new ContainerControlledLifetimeManager());

                    // 2. KeyVaultSecretProvider singleton
                    newContainer.RegisterFactory<KeyVaultSecretProvider>((uc) => new KeyVaultSecretProvider(keyVaultUrl, tenantId, clientId, clientSecret), new ContainerControlledLifetimeManager());

                    // 3. ISecretProvider = CachedSecretProvider (caché 10 min)
                    newContainer.RegisterFactory<ISecretProvider>((uc) =>
                    {
                        var memoryCache = uc.Resolve<IMemoryCache>();
                        var keyVaultProvider = uc.Resolve<KeyVaultSecretProvider>();
                        return new CachedSecretProvider(keyVaultProvider, memoryCache, TimeSpan.FromMinutes(10));
                    }, new ContainerControlledLifetimeManager());

                    // 4. IJwtTokenService
                    newContainer.RegisterFactory<IJwtTokenService>((uc) =>
                    {
                        var secretProvider = uc.Resolve<ISecretProvider>();
                        return new JwtTokenService(secretProvider, jwtSettings);
                    }, new ContainerControlledLifetimeManager());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error configurando JWT: {ex.Message}");
                throw;
            }

        }

    }
}