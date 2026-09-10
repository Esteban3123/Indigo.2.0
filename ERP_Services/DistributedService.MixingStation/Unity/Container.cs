using Application.Accounting;
using Application.Inventory.InventoryAdjustment;
using Application.Inventory.InventoryProduct;
using Application.Inventory.InventoryRequest;
using Application.Inventory.PhysicalInventory;
using Application.Inventory.Sequense;
using Application.Inventory.TransferOrder;
using Application.MixingStation;
using Domain.Crystal;
using Domain.Entities;
using Domain.Entities.Service;
using Domain.Payroll;
using Domain.Security;
using Infrastructure.CrossCutting.AzureBlobStorage.Factory;
using Infrastructure.CrossCutting.AzureBlobStorage.Storage;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.Data.CrystalRepository;
using Infrastructure.Data.ModelRepository;
using Infrastructure.Data.PayrollRepository;
using Infrastructure.Data.SecurityRepository;
using Unity;
using Unity.Lifetime;

namespace DistributedService.MixingStation.Unity
{
    public sealed class Container
    {
        private static IUnityContainer _currentContainer;

        /// <summary>
        /// Obtiene la unica instancia del contenedor
        /// </summary>
        /// <returns>Contenedor configurado</returns>
        public static IUnityContainer Instance
        {
            get
            {
                if (_currentContainer == null)
                {
                    ConfigureContainer();
                }

                return _currentContainer;
            }
        }

        private static void ConfigureContainer()
        {
            var container = System.Configuration.ConfigurationManager.AppSettings.Get("IndigoContainer");
            var seccontainer = System.Configuration.ConfigurationManager.AppSettings.Get("IndigoSecurityContainer");
            var newContainer = new UnityContainer();

            newContainer.RegisterFactory<IGlobalModelUnitOfWork>((uc) =>
            {
                var dd = new GlobalModelUnitOfWork(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            newContainer.RegisterFactory<ICrystalModelUnitOfWork>((uc) =>
            {
                var dd = new CrystalModelUnitOfWork(container);
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
                var dd = new GenesisEntities(seccontainer);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            
            newContainer.RegisterType<IRawMaterialAdminService, RawMaterialAdminService>();
            newContainer.RegisterType<IRawMaterialRepository, RawMaterialRepository>();
            newContainer.RegisterType<IMixingStationSequenceDetailRepository, MixingStationSequenceDetailRepository>();
            newContainer.RegisterType<IProductionBasketsRepository, ProductionBasketsRepository>();
            newContainer.RegisterType<ICampaignRepository, CampaignRepository>();
            newContainer.RegisterType<IPickingRepository, PickingRepository>();
            newContainer.RegisterType<ICampaignDetailRepository, CampaignDetailRepository>();
            newContainer.RegisterType<IPhysicalInventoryRepository, PhysicalInventoryRepository>();
            newContainer.RegisterType<IWarehouseRepository, WarehouseRepository>();
            newContainer.RegisterType<ICMWarehouseRepository, CMWarehouseRepository>();
            newContainer.RegisterType<ICampaignKardexAdminService, CampaignKardexAdminService>();
            newContainer.RegisterType<IInventoryService, InventoryServices>();
            newContainer.RegisterType<ICMConfigRepository, CMConfigRepository>();
            newContainer.RegisterType<ITransferOrderAdminService, TransferOrderAdminService>();
            newContainer.RegisterType<IInventoryRequestAdminService, InventoryRequestAdminService>();
            newContainer.RegisterType<ICampaignDetailValidationRepository, CampaignDetailValidationRepository>();
            newContainer.RegisterType<ICampaignReportsAdminService, CampaignReportsAdminService>();
            newContainer.RegisterType<ICampaignReportsRepository, CampaignReportsRepository>();
            newContainer.RegisterType<IContractExternalClientsDetailRepository, ContractExternalClientsDetailRepository>();
            newContainer.RegisterType<ICampaignDetailBasketDetailRepository, CampaignDetailBasketDetailRepository>();
            newContainer.RegisterType<ICampaignKardexRepository, CampaignKardexRepository>();
            newContainer.RegisterType<ITransferOrderRepository, TransferOrderRepository>();
            newContainer.RegisterType<IInventoryRequestRepository, InventoryRequestRepository>();
            newContainer.RegisterType<IInventorySequenceDetailRepository, InventorySequenceDetailRepository>();
            newContainer.RegisterType<ICampaignItemsRepository, CampaignItemsRepository>();
            newContainer.RegisterType<IRequestParamFunctionalUnitRepository, RequestParamFunctionalUnitRepository>();
            newContainer.RegisterType<IRequestParamWarehouseRepository, RequestParamWarehouseRepository>();
            newContainer.RegisterType<IRequestParamAuthUserRepository, RequestParamAuthUserRepository>();
            newContainer.RegisterType<IRawMaterialDevolutionAdminService, RawMaterialDevolutionAdminService>();
            newContainer.RegisterType<IATCRepository, ATCRepository>();
            newContainer.RegisterType<IProductTypeRepository, ProductTypeRepository>();
            newContainer.RegisterType<IInventoryProductRepository, InventoryProductRepository>();
            newContainer.RegisterType<IRawMaterialDevolutionRepository, RawMaterialDevolutionRepository>();
            newContainer.RegisterType<IRawMaterialDevolutionDetailRepository, RawMaterialDevolutionDetailRepository>();
            newContainer.RegisterType<IMixingStationSequenceAdminService, MixingStationSequenceAdminService>();
            newContainer.RegisterType<IMixingStationSequenceRepository, MixingStationSequenceRepository>();
            newContainer.RegisterType<IOperatingUnitRepository, OperatingUnitRepository>();
            newContainer.RegisterType<ICampaignAdminService, CampaignAdminService>();
            newContainer.RegisterType<IRequestMixingStationDetailRepository, RequestMixingStationDetailRepository>();
            newContainer.RegisterType<IRequestPackageDetailStatusRepository, RequestPackageDetailStatusRepository>();
            newContainer.RegisterType<IMixingStationSettingRepository, MixingStationSettingRepository>();
            newContainer.RegisterType<ICampaignDetailPickingRepository, CampaignDetailPickingRepository>();
            newContainer.RegisterType<IInventorySupplieRepository, InventorySupplieRepository>();
            newContainer.RegisterType<IRequestMixingStationRepository, RequestMixingStationRepository>();
            newContainer.RegisterType<IPackageDetailRepository, PackageDetailRepository>();
            newContainer.RegisterType<IPackagePersonalizedDetailRepository, PackagePersonalizedDetailRepository>();
            newContainer.RegisterType<IThirdPartyRepository, ThirdPartyRepository>();
            newContainer.RegisterType<IInventoryAdjustmentAdminService, InventoryAdjustmentAdminService>();
            newContainer.RegisterType<IInventorySequenceAdminService, InventorySequenceAdminService>();
            newContainer.RegisterType<IPackagePersonalizedRepository, PackagePersonalizedRepository>();
            newContainer.RegisterType<IPackageRepository, PackageRepository>();
            newContainer.RegisterType<IInventoryProductAdminService, InventoryProductAdminService>();
            newContainer.RegisterType<ICampaignRawMaterialRepository, CampaignRawMaterialRepository>();
            newContainer.RegisterType<IBatchSerialRepository, BatchSerialRepository>();
            newContainer.RegisterType<IMeasureUnitRepository, MeasureUnitRepository>();
            newContainer.RegisterType<IReleaseLineRepository, ReleaseLineRepository>();
            newContainer.RegisterType<IConfirmationUnitDoseRepository, ConfirmationUnitDoseRepository>();
            newContainer.RegisterType<ICampaignDetailWitnessFileRepository, CampaignDetailWitnessFileRepository>();
            newContainer.RegisterType<IFactoryStorage, FactoryStorage>();
            newContainer.RegisterType<Infrastructure.CrossCutting.AzureBlobStorage.IStorage, AzureBlobStorateService>();
            newContainer.RegisterType<IReadjustmentsRepository, ReadjustmentsRepository>();
            newContainer.RegisterType<IUnitDoseTypeRepository, UnitDoseTypeRepository>();
            newContainer.RegisterType<IReasonscancellationNPTRepository, ReasonscancellationNPTRepository>();
            newContainer.RegisterType<IInventoryAdjustmentRepository, InventoryAdjustmentRepository>();
            newContainer.RegisterType<IPhysicalInventoryAdminService, PhysicalInventoryAdminService>();
            newContainer.RegisterType<IAdjustmentConceptRepository, AdjustmentConceptRepository>();
            newContainer.RegisterType<IAccountingDocumentAdminService, AccountingDocumentAdminService>();
            newContainer.RegisterType<ISettingInventoryRepository, SettingInventoryRepository>();
            newContainer.RegisterType<IInventoryControlRepository, InventoryControlRepository>();
            newContainer.RegisterType<IInventoryControlDocumentRepository, InventoryControlDocumentRepository>();
            newContainer.RegisterType<IInventorySequenceRepository, InventorySequenceRepository>();
            newContainer.RegisterType<IProductHierarchyRepository, ProductHierarchyRepository>();
            newContainer.RegisterType<IINPRODPATRepository, INPRODPATRepository>();
            newContainer.RegisterType<IIHLISTPRORepository, IHLISTPRORepository>();
            newContainer.RegisterType<IPOSPathologiesRepository, POSPathologiesRepository>();
            newContainer.RegisterType<IFactoryQueue, FactoryQueue>();
            newContainer.RegisterType<IHCFARMEPDRepository, HCFARMEPDRepository>();
            newContainer.RegisterType<IAccountingDocumentRepository, DocumentAccountingRepository>();
            newContainer.RegisterType<IDocumentTypeRepository, DocumentTypeRepository>();
            newContainer.RegisterType<IAccountingBalanceAdminService, AccountingBalanceAdminService>();
            newContainer.RegisterType<IAccountingBalanceRepository, AccountingBalanceRepository>();
            newContainer.RegisterType<IPUCRepository, PUCRepository>();
            newContainer.RegisterType<ICloseMonthRepository, CloseMonthRepository>();
            newContainer.RegisterType<ICostCenterRepository, CostCenterRepository>();
            newContainer.RegisterType<IRetentionConceptRepository, RetentionConceptRepository>();
            newContainer.RegisterType<IBookRepository, BookRepository>();
            newContainer.RegisterType<IKardexRepository, KardexRepository>();
            newContainer.RegisterType<ISequenseAccountingDRepository, SequenseAccountingDRepository>();
            newContainer.RegisterType<IContainersRepository, ContainersRepository>();
            newContainer.RegisterType<IMedicationTypeRepository, MedicationTypeRepository>();
            newContainer.RegisterType<IPharmaDoseRepository, PharmaDoseRepository>();
            
            _currentContainer = newContainer;
        }
    }
}