#region Imports

using Application.Accounting;
using Application.Billing;
using Application.Budget;
using Application.Contract;
using Application.Glosas;
using Application.Inventory;
using Application.Inventory.AdjustmentConcept;
using Application.Inventory.AdministrationRoute;
using Application.Inventory.AntineoplasicoMedication;
using Application.Inventory.AppEntranceVoucherDevolution;
using Application.Inventory.ATC;
using Application.Inventory.ATCEntity;
using Application.Inventory.AttributeProductType;
using Application.Inventory.BacterialResistanceMedication;
using Application.Inventory.BatchSerial;
using Application.Inventory.BlockRecordInventory;
using Application.Inventory.ConsignmentInventoryRemission;
using Application.Inventory.ConsignmentInventoryRemissionDetail;
using Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial;
using Application.Inventory.ConsumeService;
using Application.Inventory.DCI;
using Application.Inventory.DocumentInvoiceProductSales;
using Application.Inventory.DocumentInvoiceProductSalesDetail;
using Application.Inventory.EntranceVoucher;
using Application.Inventory.InventoryAdjustment;
using Application.Inventory.InventoryContract;
using Application.Inventory.InventoryContractAssignment;
using Application.Inventory.InventoryContractDetail;
using Application.Inventory.InventoryContractModification;
using Application.Inventory.InventoryContractType;
using Application.Inventory.InventoryControl;
using Application.Inventory.InventoryMassiveConfirm;
using Application.Inventory.InventoryProduct;
using Application.Inventory.InventoryRequest;
using Application.Inventory.InventoryRequestDetail;
using Application.Inventory.InventoryRequestDevolution;
using Application.Inventory.InventoryRiskLevel;
using Application.Inventory.InventorySupplie;
using Application.Inventory.LoanMerchandise;
using Application.Inventory.LoanMerchandiseDetail;
using Application.Inventory.LoanMerchandiseDevolution;
using Application.Inventory.LoanMerchandiseDevolutionDetail;
using Application.Inventory.Manufacturer;
using Application.Inventory.MeasureUnit;
using Application.Inventory.OtherWithholdingDeduction;
using Application.Inventory.PackagingUnit;
using Application.Inventory.PharmaceuticalDispensing;
using Application.Inventory.PharmaceuticalDispensingDetailBatchSerial;
using Application.Inventory.PharmaceuticalDispensingDevolution;
using Application.Inventory.PharmaceuticalDispensingDevolutionDetail;
using Application.Inventory.PharmaceuticalForm;
using Application.Inventory.PharmacologicalGroup;
using Application.Inventory.PhysicalInventory;
using Application.Inventory.ProductGroup;
using Application.Inventory.ProductGroups;
using Application.Inventory.ProductRateDetail;
using Application.Inventory.ProductSubGroups;
using Application.Inventory.ProductTemplate;
using Application.Inventory.ProductType;
using Application.Inventory.PurchaseOrder;
using Application.Inventory.PurchaseOrderDetail;
using Application.Inventory.PurchaseOrderDevolution;
using Application.Inventory.PurchaseRequest;
using Application.Inventory.RemissionDevolution;
using Application.Inventory.RemissionEntrance;
using Application.Inventory.RemissionEntranceDetail;
using Application.Inventory.RemissionEntranceDetailBatchSerial;
using Application.Inventory.RemissionOutput;
using Application.Inventory.RemissionOutputDetail;
using Application.Inventory.RemissionOutputDetailPhysical;
using Application.Inventory.Sequense;
using Application.Inventory.SettingInventory;
using Application.Inventory.ShelfType;
using Application.Inventory.TransferOrder;
using Application.Inventory.TransferOrderDetail;
using Application.Inventory.TransferOrderDetailBatchSerial;
using Application.Inventory.TransferOrderDevolution;
using Application.Inventory.UpdateExpirationDate;
using Application.Inventory.Warehouse;
using Application.Inventory.WarehouseStock;
using Application.MedicalFees;
using Application.Payments;
using Application.Portfolio;
using Application.Security;
using Application.Treasury;
using Domain.Crystal;
using Domain.Entities;
using Domain.Entities.Service;
using Domain.Payroll;
using Domain.Security;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Security;
using Infrastructure.CrossCutting.Rollback;
using Infrastructure.Data.CrystalRepository;
using Infrastructure.Data.MaintenanceRepository;
using Infrastructure.Data.ModelRepository;
using Infrastructure.Data.PayrollRepository;
using Infrastructure.Data.SecurityRepository;
using Microsoft.Practices.Unity;
using Application.Inventory.DevolutionCause;
using Application.Inventory.PharmaceuticalDispensingTransfer;
using Application.MixingStation;
using System.ServiceModel;
using System.Linq;
using Application.Inventory.PurchaseRequestDetail;
using Application.Common;
using Application.Inventory.DecreaseMaximumLimit;
using Application.Inventory.ProductInTransit;
using Application.Inventory.ProductInTransitDetail;
using Infrastructure.CrossCutting.Queue;
using Application.Inventory.ConsignmentCostList;
using Application.Inventory.UPRUnits;
using Application.Inventory.PharmaceuticalFormGrouping;
using Application.Inventory.InventoryKardex;
using Application.Inventory.DashBoardPharmacy;
using Application.Inventory.MedicationType;
using Application.Inventory.StorageTemperature;
using Application.Inventory.Rollback;
using DistributedServices.Authentication;
using Application.Inventory.SurgicalPackageProcess;
using Microsoft.Extensions.Caching.Memory;

#endregion Imports

namespace DistributedServices.Inventory.Unity
{
    public sealed class Container
    {
        #region Singleton

        /// <summary>
        /// Unica instancia del contenedor
        /// </summary>
        private static IUnityContainer _currentContainer;

        /// <summary>
        /// Obtiene la unica instancia del contenedor
        /// </summary>
        /// <returns>Contenedor configurado</returns>
        public static IUnityContainer Current
        {
            get
            {
                var containerInfo = JwtFactory.GetContainerFromToken();
                string container = string.IsNullOrEmpty(containerInfo?.container) ? SessionValues.Instance.TransactionalContainer : containerInfo?.container;
                string hisContainer = string.IsNullOrEmpty(containerInfo?.hisContainer) ? SessionValues.Instance.HisContainer : containerInfo?.hisContainer;

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
        }

        #endregion Singleton

        #region Methods

        /// <summary>
        /// Configura las dependencias en el contenedor
        /// </summary>
        private static void ConfigureContainer(string container, string hisContainer)
        {
            var newContainer = new UnityContainer();

            newContainer.RegisterType<ICommonVariables>(new PerResolveLifetimeManager(), new InjectionFactory((c) =>
            {
                return new CommonVariables(container, hisContainer);
            }));
            //Inyectamos el contexto
            newContainer.RegisterType<IGlobalModelUnitOfWork>(new PerResolveLifetimeManager(), new InjectionFactory((c) =>
            {
                return new GlobalModelUnitOfWork(container);
            }));
            //Contexto de Crystal
            newContainer.RegisterType<ICrystalModelUnitOfWork>(new PerResolveLifetimeManager(), new InjectionFactory((c) =>
            {
                return new CrystalModelUnitOfWork(hisContainer);
            }));
            //Inyectamos el contexto de payroll
            newContainer.RegisterType<IPayrollUnitOfWork>(new PerResolveLifetimeManager(), new InjectionFactory((c) =>
            {
                return new PayrollUnitOfWork(container);
            }));
            //contexto Mantenimiento
            newContainer.RegisterType<IMaintenanceModelUnitOfWork>(new PerResolveLifetimeManager(), new InjectionFactory(c =>
            {
                return new MaintenanceModelUnitOfWork(container);
            }));
            newContainer.RegisterType<ISeguridadUnitOfWork>(new PerResolveLifetimeManager(), new InjectionFactory((c) =>
            {
                return new GenesisEntities();
            }));


            var injector = new Extentions.ModuleInjector();
            injector.Load(newContainer, typeof(Domain.Base.Inject));

            newContainer.RegisterType<Domain.InterfaceERPGlosa.IInterfaceFOX, Domain.InterfaceERPGlosa.InterfaceFOX>(new InjectionConstructor(container));
            newContainer.RegisterType<Domain.InterfaceERPGlosa.IInterfaceNET, Domain.InterfaceERPGlosa.InterfaceNET>(new InjectionConstructor(container));
            newContainer.RegisterType<Domain.InterfaceERPGlosa.IInterfacePublicFOX, Domain.InterfaceERPGlosa.InterfacePublicFOX>(new InjectionConstructor(container));
            newContainer.RegisterType<Domain.InterfaceERPGlosa.IInterfacePublicNET, Domain.InterfaceERPGlosa.InterfacePublicNET>(new InjectionConstructor(container));

            newContainer.RegisterType<IBillingConceptRepository, BillingConceptRepository>();
            newContainer.RegisterType<ISequenseTreasuryCRepository, SequenseTreasuryCRepository>();
            newContainer.RegisterType<Domain.Entities.IInterfaceParametersRepository, Infrastructure.Data.ModelRepository.InterfacesParametersRepository>();
            newContainer.RegisterType<Domain.Entities.IMovementGlosaRepository, Infrastructure.Data.ModelRepository.MovementGlosaRepository>();
            newContainer.RegisterType<Domain.Entities.IPartialPaymentsMovementRepository, Infrastructure.Data.ModelRepository.PartialPaymentsMovementRepository>();
            newContainer.RegisterType<Domain.Entities.IPartialPaymentsDRepository, Infrastructure.Data.ModelRepository.PartialPaymentsDRepository>();
            newContainer.RegisterType<Domain.Entities.IPartialPaymentsCRepository, Infrastructure.Data.ModelRepository.PartialPaymentsCRepository>();
            newContainer.RegisterType<Domain.Entities.IConsecutiveRepository, Infrastructure.Data.ModelRepository.ConsecutiveRepository>();
            newContainer.RegisterType<IPartialPaymentsCAdminService, PartialPaymentsCAdminService>();
            newContainer.RegisterType<Domain.Entities.IPortfolioGlosadaRepository, Infrastructure.Data.ModelRepository.PortfolioGlosadaRepository>();
            newContainer.RegisterType<Domain.Entities.IBankRepository, Infrastructure.Data.ModelRepository.BankRepository>();
            newContainer.RegisterType<IVoucherTransactionRepository, VoucherTransactionRepository>();
            newContainer.RegisterType<ICrossingAccountDetailCxCRepository, CrossingAccountDetailCxCRepository>();
            newContainer.RegisterType<ICrossingAccountDetailCxPRepository, CrossingAccountDetailCxPRepository>();
            newContainer.RegisterType<IRefundRepository, RefundRepository>();
            newContainer.RegisterType<IDischargeBillRepository, DischargeBillRepository>();
            newContainer.RegisterType<IExpenseConceptRepository, ExpenseConceptRepository>();
            newContainer.RegisterType<Domain.Entities.Service.ITreasuryServices, Domain.Entities.Service.TreasuryServices>();
            newContainer.RegisterType<ITreasuryControlRepository, TreasuryControlRepository>();
            newContainer.RegisterType<ITreasuryControlAdminService, TreasuryControlAdminService>();
            newContainer.RegisterType<ISettingsTreasuryRepository, SettingsTreasuryRepository>();
            newContainer.RegisterType<IEntityBankAccountAdminService, EntityBankAccountAdminService>();
            newContainer.RegisterType<IEntityBankAccountRepository, EntityBankAccountRepository>();
            newContainer.RegisterType<ISequenseTreasuryDRepository, SequenseTreasuryDRepository>();
            newContainer.RegisterType<ICashRegisterAdminService, CashRegisterAdminService>();
            newContainer.RegisterType<ICashRegisterRepository, CashRegisterRepository>();
            newContainer.RegisterType<IReclassificationRepository, ReclassificationRepository>();
            newContainer.RegisterType<ICostDistributionsRepository, CostDistributionsRepository>();
            //
            newContainer.RegisterType<ICashReceiptsAdminService, CashReceiptsAdminService>();
            newContainer.RegisterType<ICashReceiptsRepository, CashReceiptsRepository>();
            newContainer.RegisterType<ICrossingAccountDetailOtherConceptsRepository, CrossingAccountDetailOtherConceptsRepository>();

            //--------------------------------------------- Esto para identificar ----------------------------------

            //Security.User
            newContainer.RegisterType<IUserAdminService, UserAdminService>();
            newContainer.RegisterType<IUserRepository, UserRepository>();
            newContainer.RegisterType<IUserMembershipRepository, IndigoAutentication>();

            newContainer.RegisterType<IFileUserRepository, FileUserRepository>();
            newContainer.RegisterType<IPermissionCompanyRepository, PermissionCompanyRepository>();
            newContainer.RegisterType<IGeneralLedgerIVARepository, GeneralLedgerIVARepository>();

            //-------------------------------------------------------------------------------------------------------

            newContainer.RegisterType<IINPRODPATRepository, INPRODPATRepository>();
            newContainer.RegisterType<IIHLISTPRORepository, IHLISTPRORepository>();
            newContainer.RegisterType<IPOSPathologiesRepository, POSPathologiesRepository>();

            newContainer.RegisterType<IInventoryMassiveConfirmAdminService, InventoryMassiveConfirmAdminService>();

            //
            newContainer.RegisterType<IIPSServiceGroupRepository, IPSServiceGroupRepository>();
            //
            newContainer.RegisterType<IProcedureCupsRepository, ProcedureCupsRepository>();
            //Inyectamos el servicio WCF
            newContainer.RegisterType<DistributedServices.Inventory.Contracts.IInventoryService, InventoryService>();
            newContainer.RegisterType<IDefinitionRateDetailRepository, DefinitionRateDetailRepository>();
            newContainer.RegisterType<ICareGroupDefinitionRateRepository, CareGroupDefinitionRateRepository>();
            newContainer.RegisterType<IDefinitionRateDetailConditionRepository, DefinitionRateDetailConditionRepository>();
            //BlockRecordInventory
            newContainer.RegisterType<IBlockRecordInventoryAdminService, BlockRecordInventoryAdminService>();
            newContainer.RegisterType<IBlockRecordInventoryRepository, BlockRecordInventoryRepository>();
            //Factura de productos
            newContainer.RegisterType<IDocumentInvoiceProductSalesAdminService, DocumentInvoiceProductSalesAdminService>();
            newContainer.RegisterType<IDocumentInvoiceProductSalesRepository, DocumentInvoiceProductSalesRepository>();
            //Factura de productos Detalle
            newContainer.RegisterType<IDocumentInvoiceProductSalesDetailAdminService, DocumentInvoiceProductSalesDetailAdminService>();
            newContainer.RegisterType<IDocumentInvoiceProductSalesDetailRepository, DocumentInvoiceProductSalesDetailRepository>();
            //ProductGroup
            newContainer.RegisterType<IProductGroupsAdminService, ProductGroupsAdminService>();
            newContainer.RegisterType<IProductGroupsRepository, ProductGroupsRepository>();
            //Sequence
            newContainer.RegisterType<IInventorySequenceAdminService, InventorySequenceAdminService>();
            newContainer.RegisterType<IInventorySequenceRepository, InventorySequenceRepository>();
            newContainer.RegisterType<IInventorySequenceDetailRepository, InventorySequenceDetailRepository>();
            //ProductSubGroup
            newContainer.RegisterType<IProductSubGroupsAdminService, ProductSubGroupsAdminService>();
            newContainer.RegisterType<IProductSubGroupsRepository, ProductSubGroupsRepository>();
            //MeasureUnit
            newContainer.RegisterType<IMeasureUnitAdminService, MeasureUnitAdminService>();
            newContainer.RegisterType<IMeasureUnitRepository, MeasureUnitRepository>();
			//ShelfType
			newContainer.RegisterType<IShelfTypeAdminService, ShelfTypeAdminService>();
			newContainer.RegisterType<IShelfTypeRepository, ShelfTypeRepository>();
			//Warehouse
			newContainer.RegisterType<IWarehouseAdminService, WarehouseAdminService>();
            newContainer.RegisterType<IWarehouseRepository, WarehouseRepository>();
            //PharmacologicalGroup
            newContainer.RegisterType<IPharmacologicalGroupAdminService, PharmacologicalGroupAdminService>();
            newContainer.RegisterType<IPharmacologicalGroupRepository, PharmacologicalGroupRepository>();
            //AdjustmentConcept
            newContainer.RegisterType<IAdjustmentConceptAdminService, AdjustmentConceptAdminService>();
            newContainer.RegisterType<IAdjustmentConceptRepository, AdjustmentConceptRepository>();
            //Manufacturer
            newContainer.RegisterType<IManufacturerAdminService, ManufacturerAdminService>();
            newContainer.RegisterType<IManufacturerRepository, ManufacturesRepository>();
            //OtherWithholdingDeductions            
            newContainer.RegisterType<IOtherWithholdingDeductionRepository, OtherWithholdingDeductionRepository>();
            newContainer.RegisterType<IOtherWithholdingDeductionAdminService, OtherWithholdingDeductionAdminService>();
            //ProductType
            newContainer.RegisterType<IProductTypeAdminService, ProductTypeAdminService>();
            newContainer.RegisterType<IProductTypeRepository, ProductTypeRepository>();
            //AttributeProductType
            newContainer.RegisterType<IAttributeProductTypeAdminService, AttributeProductTypeAdminService>();
            newContainer.RegisterType<IAttributeProductTypeRepository, AttributeProductTypeRepository>();
            //DCI
            newContainer.RegisterType<IDCIAdminService, DCIAdminService>();
            newContainer.RegisterType<IDCIRepository, DCIRepository>();
            //Drug interaction
            newContainer.RegisterType<IDrugInteractionRepository, DrugInteractionRepository>();
            //newContainer.RegisterType<IInvoicePortfolioAdvanceRepository, InvoicePortfolioAdvanceRepository>();
            //InventaryContractType
            newContainer.RegisterType<IInventoryContractTypeAdminService, InventoryContractTypeAdminService>();
            newContainer.RegisterType<IInventoryContractTypeRepository, InventoryContractTypeRepository>();
            //InventaryContract
            newContainer.RegisterType<IInventoryContractAdminService, InventoryContractAdminService>();
            newContainer.RegisterType<IInventoryContractRepository, InventoryContractRepository>();
            //EntranceVoucher
            newContainer.RegisterType<IEntranceVoucherAdminService, EntranceVoucherAdminService>();
            newContainer.RegisterType<IEntranceVoucherRepository, EntranceVoucherRepository>();
            //ConsignmentCostList
            newContainer.RegisterType<IConsignmentCostListAdminService, ConsignmentCostListAdminService>();
            newContainer.RegisterType<IConsignmentCostListRepository, ConsignmentCostListRepository>();
            //ConsignmentCostListDetailRecord
            newContainer.RegisterType<IConsignmentCostListDetailRecordRepository, ConsignmentCostListDetailRecordRepository>();
            //InventoryControl
            newContainer.RegisterType<IInventoryControlAdminService, InventoryControlAdminService>();
            newContainer.RegisterType<IInventoryControlRepository, InventoryControlRepository>();
            //InventoryAdjustment
            newContainer.RegisterType<IInventoryAdjustmentAdminService, InventoryAdjustmentAdminService>();
            newContainer.RegisterType<IInventoryAdjustmentRepository, InventoryAdjustmentRepository>();
            //EntranceVoucherDevolution
            newContainer.RegisterType<IEntranceVoucherDevolutionAdminService, EntranceVoucherDevolutionAdminService>();
            newContainer.RegisterType<IEntranceVoucherDevolutionRepository, EntranceVoucherDevoltionRepository>();
            //PharmaceuticalForm
            newContainer.RegisterType<IPharmaceuticalFormAdminService, PharmaceuticalFormAdminService>();
            newContainer.RegisterType<IPharmaceuticalFormRepository, PharmaceuticalFormRepository>();
            //AdministrationRoute
            newContainer.RegisterType<IAdministrationRouteAdminService, AdministrationRouteAdminService>();
            newContainer.RegisterType<IAdministrationRouteRepository, AdministrationRouteRepository>();
            //ATCEntity
            newContainer.RegisterType<IATCEntityAdminService, ATCEntityAdminService>();
            newContainer.RegisterType<IATCEntityRepository, ATCEntityRepository>();
            //RiskLevel
            newContainer.RegisterType<IInventoryRiskLevelAdminService, InventoryRiskLevelAdminService>();
            newContainer.RegisterType<IInventoryRiskLevelRepository, InventoryRiskLevelRepository>();
            //Productos
            newContainer.RegisterType<IInventoryProductAdminService, InventoryProductAdminService>();
            newContainer.RegisterType<IInventoryProductRepository, InventoryProductRepository>();
            //Rangos de temperatura
            newContainer.RegisterType<IStorageTemperatureAdminService, StorageTemperatureAdminService>();
            newContainer.RegisterType<IStorageTemperatureRepository, StorageTemperatureRepository>();
            //Solicitudes de Inventario
            newContainer.RegisterType<IInventoryRequestAdminService, InventoryRequestAdminService>();
            newContainer.RegisterType<IInventoryRequestRepository, InventoryRequestRepository>();
            //detalles de Solicitudes de Inventario
            newContainer.RegisterType<IInventoryRequestDetailAdminService, InventoryRequestDetailAdminService>();
            newContainer.RegisterType<IInventoryRequestDetailRepository, InventoryRequestDetailRepository>();
            newContainer.RegisterType<IInventoryRequestDetailOtherRepository, InventoryRequestDetailOtherRepository>();
            //Devolución de solicitudes de Inventario
            newContainer.RegisterType<IInventoryRequestDevolutionAdminService, InventoryRequestDevolutionAdminService>();
            newContainer.RegisterType<IInventoryRequestDevolutionRepository, InventoryRequestDevolutionRespository>();
            //Ordenes de traslado
            newContainer.RegisterType<ITransferOrderAdminService, TransferOrderAdminService>();
            newContainer.RegisterType<ITransferOrderRepository, TransferOrderRepository>();
            //detalles de Ordenes de traslado
            newContainer.RegisterType<ITransferOrderDetailAdminService, TransferOrderDetailAdminService>();
            newContainer.RegisterType<ITransferOrderDetailRepository, TransferOrderDetailRepository>();
            //detalle de los detalles de Ordenes de traslado
            newContainer.RegisterType<ITransferOrderDetailBatchSerialAdminService, TransferOrderDetailBatchSerialAdminService>();
            newContainer.RegisterType<ITransferOrderDetailBatchSerialRepository, TransferOrderDetailBatchSerialRepository>();
            //devolucion de Ordenes de traslado
            newContainer.RegisterType<ITransferOrderDevolutionAdminService, TransferOrderDevolutionAdminService>();
            newContainer.RegisterType<ITransferOrderDevolutionRepository, TransferOrderDevolutionRepository>();
            //Jerarquia
            newContainer.RegisterType<IProductHierarchyRepository, ProductHierarchyRepository>();
            //ATC
            newContainer.RegisterType<IATCAdminService, ATCAdminService>();
            newContainer.RegisterType<IATCRepository, ATCRepository>();
            //ProductTemplate
            newContainer.RegisterType<IProductTemplateAdminService, ProductTemplateAdminService>();
            newContainer.RegisterType<IProductTemplateRepository, ProductTemplateRepository>();
            //Batch
            newContainer.RegisterType<IBatchSerialAdminService, BatchSerialAdminService>();
            newContainer.RegisterType<IBatchSerialRepository, BatchSerialRepository>();
            //RemissionEntrance
            newContainer.RegisterType<IRemissionEntranceAdminService, RemissionEntranceAdminService>();
            newContainer.RegisterType<IRemissionEntranceRepository, RemissionEntranceRepository>();
            //RemissionEntranceDetail
            newContainer.RegisterType<IRemissionEntranceDetailAdminService, RemissionEntranceDetailAdminService>();
            newContainer.RegisterType<IRemissionEntranceDetailRepository, RemissionEntranceDetailRepository>();
            //RemissionEntranceDetailBatchSerial
            newContainer.RegisterType<IRemissionEntranceDetailBatchSerialAdminService, RemissionEntranceDetailBatchSerialAdminService>();
            newContainer.RegisterType<IRemissionEntranceDetailBatchSerialRepository, RemissionEntranceDetailBatchSerialRepository>();
            //InventoryContractDetail
            newContainer.RegisterType<IInventoryContractDetailAdminService, InventoryContractDetailAdminService>();
            newContainer.RegisterType<IInventoryContractDetailRepository, InventoryContractDetailRepository>();
            //PurchaseOrderDetail
            newContainer.RegisterType<IPurchaseOrderDetailAdminService, PurchaseOrderDetailAdminService>();
            newContainer.RegisterType<IPurchaseOrderDetailRepository, PurchaseOrderDetailRepository>();
            //PurchaseOrder
            newContainer.RegisterType<IPurchaseOrderAdminService, PurchaseOrderAdminService>();
            newContainer.RegisterType<IPurchaseOrderRepository, PurchaseOrderRepository>();
            //PurchaseOrderDevolution
            newContainer.RegisterType<IPurchaseOrderDevolutionAdminService, PurchaseOrderDevolutionAdminService>();
            newContainer.RegisterType<IPurchaseOrderDevolutionRepository, PurchaseOrderDevolutionRepository>();
            //PhysicalInventory
            newContainer.RegisterType<IPhysicalInventoryAdminService, PhysicalInventoryAdminService>();
            newContainer.RegisterType<IPhysicalInventoryRepository, PhysicalInventoryRepository>();
            //Kardex
            newContainer.RegisterType<IInventoryKardexAdminService, InventoryKardexAdminService>();
            newContainer.RegisterType<Domain.Entities.IKardexRepository, Infrastructure.Data.ModelRepository.KardexRepository>();
            //ProductRateDetail
            newContainer.RegisterType<IProductRateDetailRepository, ProductRateDetailRepository>();
            newContainer.RegisterType<IPackagePersonalizedDetailRepository, PackagePersonalizedDetailRepository>();
            newContainer.RegisterType<IProductRateDetailAdminService, ProductRateDetailAdminService>();
            //RemissionOutput
            newContainer.RegisterType<IRemissionOutputAdminService, RemissionOutputAdminService>();
            newContainer.RegisterType<IRemissionOutputRepository, RemissionOutputRepository>();
            //RemissionOutputDetail
            newContainer.RegisterType<IRemissionOutputDetailAdminService, RemissionOutputDetailAdminService>();
            newContainer.RegisterType<IRemissionOutputDetailRepository, RemissionOutputDetailRepository>();
            //SettingInventory
            newContainer.RegisterType<ISettingInventoryAdminService, SettingInventoryAdminService>();
            newContainer.RegisterType<ISettingInventoryRepository, SettingInventoryRepository>();
            //PharmaceuticalDispensing
            newContainer.RegisterType<IPharmaceuticalDispensingAdminService, PharmaceuticalDispensingAdminService>();
            newContainer.RegisterType<IPharmaceuticalDispensingRepository, PharmaceuticalDispensingRepository>();
            //PharmaceuticalDispensingDetail
            newContainer.RegisterType<IPharmaceuticalDispensingDetailRepository, PharmaceuticalDispensingDetailRepository>();
            //PharmaceuticalDispensingDevolution
            newContainer.RegisterType<IPharmaceuticalDispensingDevolutionAdminService, PharmaceuticalDispensingDevolutionAdminService>();
            newContainer.RegisterType<IPharmaceuticalDispensingDevolutionRepository, PharmaceuticalDispensingDevolutionRepository>();
            //PharmaceuticalDispensingDevolutionDetail
            newContainer.RegisterType<IPharmaceuticalDispensingDevolutionDetailAdminService, PharmaceuticalDispensingDevolutionDetailAdminService>();
            newContainer.RegisterType<IPharmaceuticalDispensingDevolutionDetailRepository, PharmaceuticalDispensingDevolutionDetailRepository>();
            //
            newContainer.RegisterType<IUPRUnitsAdminService, UPRUnitsAdminService>();
            newContainer.RegisterType<IUPRUnitsRepository, UPRUnitsRepository>();

            //PharmaceuticalFormGrouping
            newContainer.RegisterType<IPharmaceuticalFormGroupingAdminService, PharmaceuticalFormGroupingAdminService>();
            newContainer.RegisterType<IPharmaceuticalFormGroupingRepository, PharmaceuticalFormGroupingRepository>();

            //RemissionOutputDetailPhysical
            newContainer.RegisterType<IRemissionOutputDetailPhysicalAdminService, RemissionOutputDetailPhysicalAdminService>();
            newContainer.RegisterType<IRemissionOutputDetailPhysicalRepository, RemissionOutputDetailPhysicalRepository>();
            //RemissionDevolution
            newContainer.RegisterType<IRemissionDevolutionAdminService, RemissionDevolutionAdminService>();
            newContainer.RegisterType<IRemissionDevolutionRepository, RemissionDevolutionRepository>();
            //Contract
            newContainer.RegisterType<Domain.Entities.IContractRepository, Infrastructure.Data.ModelRepository.ContractRepository>();
            newContainer.RegisterType<Domain.Entities.Service.IContractServices, Domain.Entities.Service.ContractServices>();
            //HealthAdministrator
            newContainer.RegisterType<Domain.Entities.IHealthAdministratorRepository, Infrastructure.Data.ModelRepository.HealthAdministratorRepository>();
            //PackagingUnit
            newContainer.RegisterType<IPackagingUnitAdminService, PackagingUnitAdminService>();
            newContainer.RegisterType<IPackagingUnitRepository, PackagingUnitRepository>();
            //DevolutionCause
            newContainer.RegisterType<IDevolutionCauseAdminService, DevolutionCauseAdminService>();
            newContainer.RegisterType<IDevolutionCauseRepository, DevolutionCauseRepository>();

            newContainer.RegisterType<IPharmaceuticalDispensingDetailBatchSerialAdminService, PharmaceuticalDispensingDetailBatchSerialAdminService>();
            newContainer.RegisterType<IPharmaceuticalDispensingDetailBatchSerialRepository, PharmaceuticalDispensingDetailBatchSerialRepository>();

            //traslado de dispensacion
            newContainer.RegisterType<IPharmaceuticalDispensingTransferAdminService, PharmaceuticalDispensingTransferAdminService>();
            newContainer.RegisterType<IPharmaceuticalDispensingTransferRepository, PharmaceuticalDispensingTransferRepository>();

            //StayDetail
            newContainer.RegisterType<IStayDetailRepository, StayDetailRepository>();

            newContainer.RegisterType<ICashFlowConceptRepository, CashFlowConceptRepository>();

            //CostDistributionDirectCost
            newContainer.RegisterType<ICostDistributionDirectCostRepository, CostDistributionDirectCostRepository>();
            newContainer.RegisterType<ICostDistributionDirectCostDetailIvaRepository, CostDistributionDirectCostDetailIvaRepository>();

            //Billing
            //Secuencias numericas
            newContainer.RegisterType<IBillingSequenseAdminService, BillingSequenseAdminService>();
            newContainer.RegisterType<IBillingSequenceRepository, BillingSequenceRepository>();
            newContainer.RegisterType<IBillingSequenceDetailRepository, BillingSequenceDetailRepository>();
            //Bloqueo
            newContainer.RegisterType<IBlockRecordBillingAdminService, BlockRecordBillingAdminService>();
            newContainer.RegisterType<IBlockRecordBillingRepository, BlockRecordBillingRepository>();
            ////Orden de servicio
            newContainer.RegisterType<IServiceOrderAdminService, ServiceOrderAdminService>();
            newContainer.RegisterType<IServiceOrderRepository, ServiceOrderRepository>();
            ////Orden de servicio detalle
            newContainer.RegisterType<IServiceOrderDetailAdminService, ServiceOrderDetailAdminService>();
            newContainer.RegisterType<IServiceOrderDetailRepository, ServiceOrderDetailRepository>();
            ////Orden de servicio detalle quirurgico
            newContainer.RegisterType<IServiceOrderDetailSurgicalAdminService, ServiceOrderDetailSurgicalAdminService>();
            newContainer.RegisterType<IServiceOrderDetailSurgicalRepository, ServiceOrderDetailSurgicalRepository>();
            ////Orden de servicio detalle
            newContainer.RegisterType<IWarehouseStockAdminService, WarehouseStockAdminService>();
            newContainer.RegisterType<IWarehouseStockRepository, WarehouseStockRepository>();
            //SettingsBilling
            newContainer.RegisterType<ISettingsBillingRepository, SettingsBillingRepository>();
            ////Liquidación
            newContainer.RegisterType<ILiquidationAdminService, LiquidationAdminService>();
            newContainer.RegisterType<IAdmissionRepository, AdmissionRepository>();
            newContainer.RegisterType<IRevenueControlRepository, RevenueControlRepository>();
            newContainer.RegisterType<IRevenueControlDetailRepository, RevenueControlDetailRepository>();
            newContainer.RegisterType<ICompanySettingsRepository, CompanySettingsRepository>();
            newContainer.RegisterType<ISpecialityRepository, SpecialityRepository>();
            newContainer.RegisterType<IStayRepository, StayRepository>();
            newContainer.RegisterType<Domain.Entities.Service.IBillingServices, Domain.Entities.Service.BillingServices>();
            ///Detalle de Servicio del Producto
            newContainer.RegisterType<IProductServiceDetailRepository, ProductServiceDetailRepository>();


            //Invoice
            newContainer.RegisterType<IInvoiceRepository, InvoiceRepository>();
            newContainer.RegisterType<IInvoiceAdminService, InvoiceAdminService>();

            //Invoice
            newContainer.RegisterType<IAccountReceivableRepository, AccountReceivableRepository>();
            newContainer.RegisterType<IAccountReceivableAdminService, AccountReceivableAdminService>();

            //**********Common
            newContainer.RegisterType<IThirdPartyRepository, ThirdPartyRepository>();
            newContainer.RegisterType<IOperatingUnitRepository, OperatingUnitRepository>();

            ////Crystal HIS
            newContainer.RegisterType<IStayDetailRepository, StayDetailRepository>();
            newContainer.RegisterType<IStayRepository, StayRepository>();
            newContainer.RegisterType<IParameterRepository, ParameterRepository>();
            newContainer.RegisterType<IADCENATENRepository, ADCENATENRepository>();
            //newContainer.RegisterType<IStayService, StayService>();
            ////Autorización de facturación
            newContainer.RegisterType<IBillingAuthorizationAdminService, BillingAuthorizationAdminService>();
            newContainer.RegisterType<IBillingAuthorizationRepository, BillingAuthorizationRepository>();
            ////ServiceOrderDetailDistribution
            newContainer.RegisterType<IServiceOrderDetailDistributionRepository, ServiceOrderDetailDistributionRepository>();

            ///Pharmacy
            newContainer.RegisterType<IPharmacyRepository, PharmacyRepository>();
            //PharmacyDetail
            newContainer.RegisterType<IPharmacyDetailRepository, PharmacyDetailRepository>();
            //KardeCrystal
            newContainer.RegisterType<IKardexCrystalRepository, KardexCrystalRepository>();
            //Consecutive
            newContainer.RegisterType<Domain.Crystal.IConsecutiveRepository, Infrastructure.Data.CrystalRepository.ConsecutiveReporitory>();

            newContainer.RegisterType<IDevolutionMedicationDetailRepository, DevolutionMedicationDetailRepository>();
            newContainer.RegisterType<IDevolutionMedicationRepository, DevolutionMedicationRepository>();

            newContainer.RegisterType<IBedRepository, BedRepository>();

            //////dependencia de Contratos********************
            newContainer.RegisterType<ICareGroupRepository, CareGroupRepository>();
            ////IPSService
            newContainer.RegisterType<IIPSServiceAdminService, IPSServiceAdminService>();
            newContainer.RegisterType<IIPSServicesRepository, IPSServicesRepository>();
            ////CupsHomologation
            newContainer.RegisterType<ICupsHomologationAdminService, CupsHomologationAdminService>();
            newContainer.RegisterType<ICupsHomologationRepository, CupsHomologationRepository>();
            ////CupsEntity
            newContainer.RegisterType<ICupsEntityAdminService, CupsEntityAdminService>();
            newContainer.RegisterType<ICupsEntityRepository, CupsEntityRepository>();
            //CUPSEntityContractDescriptionsRepository
            newContainer.RegisterType<ICupsEntityContractDescriptionsRepository, CUPSEntityContractDescriptionsRepository>();

            //ContractPackageServiceRepository
            newContainer.RegisterType<IContractPackageServiceRepository, ContractPackageServiceRepository>();
            
            ////RateManual
            newContainer.RegisterType<IRateManualAdminService, RateManualAdminService>();
            newContainer.RegisterType<IRateManualRepository, RateManualRepository>();

            ////SurgicalProcedureService
            newContainer.RegisterType<ISurgicalProcedureServiceAdminService, SurgicalProcedureServiceAdminService>();
            newContainer.RegisterType<ISurgicalProcedureServiceRepository, SurgicalProcedureServiceRepository>();
            ////RateManualDetailSurgical
            newContainer.RegisterType<IRateManualDetailSurgicalAdminService, RateManualDetailSurgicalAdminService>();
            newContainer.RegisterType<IRateManualDetailSurgicalRepository, RateManualDetailSurgicalRepository>();
            ////RateManual
            newContainer.RegisterType<IRateManualAdminService, RateManualAdminService>();
            newContainer.RegisterType<IRateManualRepository, RateManualRepository>();
            ////RateManualDetail
            newContainer.RegisterType<IRateManualDetailAdminService, RateManualDetailAdminService>();
            newContainer.RegisterType<IRateManualDetailRepository, RateManualDetailRepository>();

            //RateManualValidity
            newContainer.RegisterType<IRateManualValidityRepository, RateManualValidityRepository>();
            newContainer.RegisterType<IRateManualValidityDetailRepository, RateManualValidityDetailRepository>();
            //////Payroll************************
            ////costCenter
            newContainer.RegisterType<ICostCenterRepository, CostCenterRepository>();
            ////Functional Unit
            newContainer.RegisterType<IFunctionalUnitRepository, FunctionalUnitRepository>();

            newContainer.RegisterType<IDecreaseMaximumLimitRepository, DecreaseMaximumLimitRespository>();
            newContainer.RegisterType<IDecreaseMaximumLimitAdminService, DecreaseMaximumLimitAdminService>(); 
            //Payments

            //Secuencias numericas
            newContainer.RegisterType<IPaymentsSequenseAdminService, PaymentsSequenseAdminService>();
            newContainer.RegisterType<ISequensePaymentsCRepository, SequensePaymentsCRepository>();
            newContainer.RegisterType<ISequensePaymentsDRepository, SequensePaymentsDRepository>();
            //Conceptos
            newContainer.RegisterType<IPaymentsConceptAdminService, PaymentsConceptAdminService>();
            newContainer.RegisterType<IPaymentsConceptRepository, PaymentsConceptRepository>();
            //Bloqueo
            newContainer.RegisterType<IBlockRecordPaymentsAdminService, BlockRecordPaymentsAdminService>();
            newContainer.RegisterType<IBlockRecordPaymentsRepository, BlockRecordPaymentsRepository>();
            //Conceptos
            newContainer.RegisterType<IPaymentsNoteConceptAdminService, PaymentsNoteConceptAdminService>();
            newContainer.RegisterType<IPaymentsNoteConceptRepository, PaymentsNoteConceptRepository>();
            //Cuenta por Pagar
            newContainer.RegisterType<IAccountPayableAdminService, AccountPayableAdminService>();
            newContainer.RegisterType<IAccountPayableRepository, AccountPayableRepository>();
            //Parametros de pagos
            newContainer.RegisterType<ISettingPaymentsAdminService, SettingPaymentsAdminService>();
            newContainer.RegisterType<ISettingPaymentsRepository, SettingPaymentsRepository>();
            //Saldos Iniciales
            newContainer.RegisterType<IOpeningBalanceAdminService, OpeningBalanceAdminService>();
            newContainer.RegisterType<IOpeningBalanceRepository, OpeningBalanceRepository>();
            //MoneyAdvance
            newContainer.RegisterType<IMoneyAdvanceAdminService, MoneyAdvanceAdminService>();
            newContainer.RegisterType<IMoneyAdvanceRepository, MoneyAdvanceRepository>();
            //PaymentsNotes
            newContainer.RegisterType<INotesDebitCreditAdminService, NotesDebitCreditAdminService>();
            newContainer.RegisterType<INotesDebitCreditRepository, NotesDebitCreditRepository>();
            //DeferredCausation
            newContainer.RegisterType<IDeferredCausationAdminService, DeferredCausationAdminService>();
            newContainer.RegisterType<IDeferredCausationRepository, DeferredCausationRepository>();
            //MovementAccountPayable
            newContainer.RegisterType<IMovementAccountPayableAdminService, MovementAccountPayableAdminService>();
            newContainer.RegisterType<IMovementAccountPayableRepository, MovementAccountPayableRepository>();
            //PaymentNotesAccountPayableAdvance
            newContainer.RegisterType<IPaymentNotesAccountPayableAdvanceAdminService, PaymentNotesAccountPayableAdvanceAdminService>();
            newContainer.RegisterType<IPaymentNotesAccountPayableAdvanceRepository, PaymentNotesAccountPayableAdvanceRepository>();
            //Transfer
            newContainer.RegisterType<ITransfersAdminService, TransfersAdminService>();
            newContainer.RegisterType<ITransfersRepository, TransfersRepository>();
            //AgesPayments
            newContainer.RegisterType<IAgesPaymentAdminService, AgesPaymentAdminService>();
            newContainer.RegisterType<IAgesPaymentsRepository, AgesPaymentsRepository>();
            //PaymentControl
            newContainer.RegisterType<IPaymentControlAdminService, PaymentControlAdminService>();
            newContainer.RegisterType<IPaymentControlRepository, PaymentControlRepository>();
            //MonthlyAmortization
            newContainer.RegisterType<IMonthlyAmortizationAdminService, MonthlyAmortizationAdminService>();
            newContainer.RegisterType<IMonthlyAmortizationRepository, MonthlyAmortizationRepository>();
            //DeferredCausationShare
            newContainer.RegisterType<IDeferredCausationShareAdminService, DeferredCausationShareAdminService>();
            newContainer.RegisterType<IDeferredCausationShareRepository, DeferredCausationShareRepository>();

            //DistributionLine
            newContainer.RegisterType<IDistributionLinesRepository, DistributionLinesRepository>();
            //SupplierDistributionLine
            newContainer.RegisterType<ISuppliersDistributionLinesRepository, SuppliersDistributionLinesRepository>();

            newContainer.RegisterType<ISupplierBankAccountRepository, SupplierBankAccountRepository>();

            //Supplier
            newContainer.RegisterType<Domain.Maintenance.ISupplierRepository, Infrastructure.Data.MaintenanceRepository.SupplierRepository>();
            newContainer.RegisterType<Domain.Entities.ISupplierRepository, Infrastructure.Data.ModelRepository.SupplierRepository>();

            //Accounting
            //Conceptos de retención
            newContainer.RegisterType<IRetentionConceptAdminService, RetentionConceptAdminService>();
            newContainer.RegisterType<IRetentionConceptRepository, RetentionConceptRepository>();
            //Secuencias numericas
            newContainer.RegisterType<IAccountingSequenseAdminService, AccountingSequenseAdminService>();
            newContainer.RegisterType<ISequenseAccountingCRepository, SequenseAccountingCRepository>();
            newContainer.RegisterType<ISequenseAccountingDRepository, SequenseAccountingDRepository>();
            //Clase contable
            newContainer.RegisterType<IAccountClassAdminService, AccountClassAdminService>();
            newContainer.RegisterType<IAccountClassRepository, AccountClassRepository>();
            //Niveles de cuentas
            newContainer.RegisterType<IAccountLevelAdminService, AccountLevelAdminService>();
            newContainer.RegisterType<IAccountLevelRepository, AccountLevelRepository>();
            //Tipos de documentos
            newContainer.RegisterType<IDocumentTypeAdminService, DocumentTypeAdminService>();
            newContainer.RegisterType<IDocumentTypeRepository, DocumentTypeRepository>();
            //Bloqueo de registros
            newContainer.RegisterType<IBlockRecordAccountingAdminService, BlockRecordAccountingAdminService>();
            newContainer.RegisterType<IBlockRecordAccountingRepository, BlockRecordAccountingRepository>();
            //Participación patrimonial
            newContainer.RegisterType<IPatrimonialPartAdminService, PatrimonialPartAdminService>();
            newContainer.RegisterType<IPatrimonialPartRepository, PatrimonialPartRepository>();
            //Anexos de declaración
            newContainer.RegisterType<IStatementFolioAdminService, StatementFolioAdminService>();
            newContainer.RegisterType<IStatementFolioRepository, StatementFolioRepository>();
            //MainAccount
            newContainer.RegisterType<IPUCAdminService, PUCAdminService>();
            newContainer.RegisterType<IPUCRepository, PUCRepository>();
            //Setting Account
            newContainer.RegisterType<ISettingAccountAdminService, SettingAccountAdminService>();
            newContainer.RegisterType<ISettingsAccountRepository, SettingAccountRepository>();
            //******************* document accounting
            newContainer.RegisterType<IAccountingDocumentRepository, DocumentAccountingRepository>();
            newContainer.RegisterType<IAccountingDocumentAdminService, AccountingDocumentAdminService>();
            //cierre de mes
            newContainer.RegisterType<ICloseMonthRepository, CloseMonthRepository>();
            newContainer.RegisterType<ICloseMonthAdminService, CloseMonthAdminService>();
            //balance
            newContainer.RegisterType<IAccountingBalanceRepository, AccountingBalanceRepository>();
            newContainer.RegisterType<IAccountingBalanceAdminService, AccountingBalanceAdminService>();
            //company settings
            newContainer.RegisterType<ICompanySettingsAdminService, CompanySettingsAdminService>();
            newContainer.RegisterType<ICompanySettingsRepository, CompanySettingsRepository>();
            //Patient
            newContainer.RegisterType<IPatientRepository, PatientRepository>();

            //UpdateExpirateDate
            newContainer.RegisterType<IUpdateExpirationDateAdminService, UpdateExpirationDateAdminService>();
            newContainer.RegisterType<IUpdateExpirationDateRepository, UpdateExpirationDateRepository>();

            newContainer.RegisterType<ICrossingAccountDetailOtherConceptsRepository, CrossingAccountDetailOtherConceptsRepository>();

            //RequestParamWarehouse
            newContainer.RegisterType<IRequestParamWarehouseRepository, RequestParamWarehouseRepository>();

            //RequestParamAuthUser
            newContainer.RegisterType<IRequestParamAuthUserRepository, RequestParamAuthUserRepository>();


            //*******************COMMON*******************
            newContainer.RegisterType<ICustomerRepository, CustomerRepository>();

            //legalbook
            newContainer.RegisterType<IBookRepository, BookRepository>();

            //Servicios de Dominio
            newContainer.RegisterType<Domain.Entities.Service.IInventoryService, Domain.Entities.Service.InventoryServices>();

            //Crystal
            newContainer.RegisterType<IAdmissionRepository, AdmissionRepository>();
            newContainer.RegisterType<IHealthProfessionalRepository, HealthProfessionalRepository>();
            //LoanMerchandiense
            newContainer.RegisterType<ILoanMerchandiseRepository, LoanMerchandiseRepository>();
            newContainer.RegisterType<ILoanMerchandiseAdminService, LoanMerchandiseAdminService>();
            //LoanMerchandienseDetail
            newContainer.RegisterType<ILoanMerchandiseDetailRepository, LoanMerchandiseDetailRepository>();
            newContainer.RegisterType<ILoanMerchandiseDetailAdminService, LoanMerchandiseDetailAdminService>();

            //LoanMerchandienseDevolution
            newContainer.RegisterType<ILoanMerchandiseDevolutionRepository, LoanMerchandiseDevolutionRepository>();
            newContainer.RegisterType<ILoanMerchandiseDevolutionAdminService, LoanMerchandiseDevolutionAdminService>();
            //LoanMerchandienseDevolutionDetail
            newContainer.RegisterType<ILoanMerchandiseDevolutionDetailRepository, LoanMerchandiseDevolutionDetailRepository>();
            newContainer.RegisterType<ILoanMerchandiseDevolutionDetailAdminService, LoanMerchandiseDevolutionDetailAdminService>();

            //LoanMerchandienseDetail
            newContainer.RegisterType<ISequensePortfolioDRepository, SequensePortfolioDRepository>();
            newContainer.RegisterType<IPortfolioSequenseAdminService, PortfolioSequenseAdminService>();

            newContainer.RegisterType<ISequensePortfolioCRepository, SequensePortfolioCRepository>();

            //LoanMerchandienseDetail
            newContainer.RegisterType<IMedicalFeesCausationRepository, MedicalFeesCausationRepository>();
            newContainer.RegisterType<IMedicalFeesCausationAdminService, MedicalFeesCausationAdminService>();

            newContainer.RegisterType<ISettingBillingAdminService, SettingBillingAdminService>();

            //LoanMerchandienseDetail
            newContainer.RegisterType<IPortfolioTransferRepository, PortfolioTransferRepository>();
            newContainer.RegisterType<IPortfolioTransfersAdminService, PortfolioTransfersAdminService>();

            //LoanMerchandienseDetail

            newContainer.RegisterType<IAccountReceivableAccountingRepository, AccountReceivableAccountingRepository>();
            newContainer.RegisterType<IPortfolioControlRepository, PortfolioControlRepository>();

            newContainer.RegisterType<IPortfolioAdvanceRepository, PortfolioAdvanceRepository>();
            newContainer.RegisterType<ISettingPortfolioRepository, SettingPortfolioRepository>();

            newContainer.RegisterType<ISequenseContractDRepository, SequenseContractDRepository>();

            newContainer.RegisterType<IHospitalInventoryRepository, HospitalInventoryRepository>();

            newContainer.RegisterType<IMedicalPrescriptionRepository, MedicalPrescriptionRepository>();

            newContainer.RegisterType<IPhysicalInventoryCrystalRepository, PhysicalInventoryCrystalRepository>();

            newContainer.RegisterType<IInventoryControlDocumentRepository, InventoryControlDocumentRepository>();

            newContainer.RegisterType<IINPACIENTTOPANURepository, INPACIENTTOPANURepository>();

            newContainer.RegisterType<IBillingConceptRepository, BillingConceptRepository>();

            newContainer.RegisterType<IBudgetSequenceRepository, BudgetSequenceRepository>();
            newContainer.RegisterType<IBudgetRepository, BudgetRepository>();
            newContainer.RegisterType<IBudgetItemRepository, BudgetItemRepository>();
            newContainer.RegisterType<IRecognitionRepository, RecognitionRepository>();

            newContainer.RegisterType<ICashReceiptConceptRepository, CashReceiptConceptRepository>();
            newContainer.RegisterType<IRecognitionAdminService, RecognitionAdminService>();

            newContainer.RegisterType<ISequenseBudgetDRepository, SequenseBudgetDRepository>();
            newContainer.RegisterType<IBudgetHeaderRepository, BudgetHeaderRepository>();

            newContainer.RegisterType<IBudgetService, BudgetService>();

            newContainer.RegisterType<IValidityRepository, ValidityRepository>();
            newContainer.RegisterType<IExpenseTypeRepository, ExpenseTypeRepository>();

            newContainer.RegisterType<IAvailabilityRepository, AvailabilityRepository>();
            newContainer.RegisterType<ICommitmentRepository, CommitmentRepository>();

            newContainer.RegisterType<IObligationRepository, ObligationRepository>();
            newContainer.RegisterType<ISuspensionDetailRepository, SuspensionDetailRepository>();

            newContainer.RegisterType<IAvailabilityDetailRepository, AvailabilityDetailRepository>();
            newContainer.RegisterType<ICollectionRepository, CollectionRepository>();

            //Exogena Format
            newContainer.RegisterType<IFormatosExogena, FormatosExogena>();

            //ConsignmentInventoryRemission
            newContainer.RegisterType<IConsignmentInventoryRemissionAdminService, ConsignmentInventoryRemissionAdminService>();
            newContainer.RegisterType<IConsignmentInventoryRemissionRepository, ConsignmentInventoryRemissionRepository>();
            //ConsignmentInventoryRemissionDetail
            newContainer.RegisterType<IConsignmentInventoryRemissionDetailAdminService, ConsignmentInventoryRemissionDetailAdminService>();
            newContainer.RegisterType<IConsignmentInventoryRemissionDetailRepository, ConsignmentInventoryRemissionDetailRepository>();
            //ConsignmentInventoryRemissionDetailBatchSerial
            newContainer.RegisterType<IConsignmentInventoryRemissionDetailBatchSerialAdminService, ConsignmentInventoryRemissionDetailBatchSerialAdminService>();
            newContainer.RegisterType<IConsignmentInventoryRemissionDetailBatchSerialRepository, ConsignmentInventoryRemissionDetailBatchSerialRepository>();
            //ConsignmentInventoryRemissionDetailControl
            newContainer.RegisterType<IConsignmentInventoryRemissionDetailControlRepository, ConsignmentInventoryRemissionDetailControlRepository>();
            //InventoryControlService
            newContainer.RegisterType<IInventoryControlServiceRepository, InventoryControlServiceRepository>();
            newContainer.RegisterType<IInventoryControlServiceAdminService, InventoryControlServiceAdminService>();

            newContainer.RegisterType<IConsumeServiceRepository, ConsumeServiceRepository>();
            newContainer.RegisterType<IConsumeServiceAdminService, ConsumeServiceAdminService>();

            //Repositorio usado para la Facturación Electrónica
            newContainer.RegisterType<IElectronicDocumentRepository, ElectronicDocumentRepository>();
            newContainer.RegisterType<IBillingNoteRepository, BillingNoteRepository>();
            newContainer.RegisterType<IBillingReversalReasonRepository, BillingReversalReasonRepository>();

            //Solicitudes de compra de inventario
            newContainer.RegisterType<IPurchaseRequestAdminService, PurchaseRequestAdminService>();
            newContainer.RegisterType<IPurchaseRequestRepository, PurchaseRequestRepository>();

            //cruce de cuentas
            newContainer.RegisterType<ICrossingAccountAdminService, CrossingAccountAdminService>();
            newContainer.RegisterType<ICrossingAccountRepository, CrossingAccountRepository>();

            //Insumo de inventario
            newContainer.RegisterType<IInventorySupplieAdminService, InventorySupplieAdminService>();
            newContainer.RegisterType<IInventorySupplieRepository, InventorySupplieRepository>();

            //otro si
            newContainer.RegisterType<IInventoryContractModificationAdminService, InventoryContractModificationAdminService>();
            newContainer.RegisterType<IInventoryContractModificationRepository, InventoryContractModificationRepository>();

            //cesion de contrato
            newContainer.RegisterType<IInventoryContractAssignmentAdminService, InventoryContractAssignmentAdminService>();
            newContainer.RegisterType<IInventoryContractAssignmentRepository, InventoryContractAssignmentRepository>();

            //Bacterial Resistance Medication
            newContainer.RegisterType<IBacterialResistanceMedicationAdminService, BacterialResistanceMedicationAdminService > ();
            newContainer.RegisterType<IBacterialResistanceMedicationRepository, BacterialResistanceMedicationRepository>();

            //Bacterial Resistance Medication
            newContainer.RegisterType<IAntineoplasicoMedicationAdminService, AntineoplasicoMedicationAdminService>();
            newContainer.RegisterType<IAntineoplasicoMedicationRepository, AntineoplasicoMedicationRepository>();

            //Contratos de centro de atencion externo
            newContainer.RegisterType<IContractExternalClientsAdminService, ContractExternalClientsAdminService>();
            newContainer.RegisterType<IContractExternalClientsRepository, ContractExternalClientsRepository>();

            //Contratos de centro de atencion externo
            newContainer.RegisterType<IProductInTransitAdminService, ProductInTransitAdminService>();
            newContainer.RegisterType<IProductInTransitRepository, ProductInTransitRepository>();

            //Contratos de centro de atencion externo
            newContainer.RegisterType<IProductInTransitDetailAdminService, ProductInTransitDetailAdminService>();
            newContainer.RegisterType<IProductInTransitDetailRepository, ProductInTransitDetailRepository>();

            //Paquete central de mezclas
            newContainer.RegisterType<IPackageRepository, PackageRepository>();

            //Reportes
            newContainer.RegisterType<Application.Inventory.Reports.IReportsAdminService, Application.Inventory.Reports.ReportsAdminService>();

            //Memory cache
            newContainer.RegisterType<IMemoryCache, MemoryCache>(
                new ContainerControlledLifetimeManager(), // Singleton
                new InjectionFactory(c => new MemoryCache(new MemoryCacheOptions()))
            );

            newContainer.RegisterType<ISEGrolesuRepository, SEGrolesuRepository>();
            newContainer.RegisterType<ISEGgruusuRepository, SEGgruusuRepository>();
            newContainer.RegisterType<ISEGusuaruRepository, SegusuaruRepository>();

            newContainer.RegisterType<IADCENATENRepository, ADCENATENRepository>();
            newContainer.RegisterType<IINUNIFUNCRepository, INUNIFUNCRepository>();
            newContainer.RegisterType<IINCUPSSUBRepository, INCUPSSUBRepository>();

            newContainer.RegisterType<IRateManualValidityRepository, RateManualValidityRepository>();

            newContainer.RegisterType<IDetailPhysicalCUM, DetailPhysicalCUMRepository>();
            newContainer.RegisterType<IHCFISIPRORepository, HCFISIPRORepository>();
            newContainer.RegisterType<IRequestPackageDetailStatusRepository, RequestPackageDetailStatusRepository>();
            newContainer.RegisterType<IPharmaDoseRepository, PharmaDoseRepository>();
            newContainer.RegisterType<IHCFARMEPDRepository, HCFARMEPDRepository>();

            newContainer.RegisterType<Application.EventHandlers.IEventProxy, Application.EventHandlers.Proxies.AzureServiceBusProxy>();
            newContainer.RegisterType<IElectronicSupportDocumentRepository, ElectronicSupportDocumentRepository>();
            newContainer.RegisterType<IElectronicSupportDocumentAdminService, ElectronicSupportDocumentAdminService>();

            newContainer.RegisterType<IPurchaseRequestDetailAdminService, PurchaseRequestDetailAdminService>();
            newContainer.RegisterType<IPurchaseRequestDetailRepository, PurchaseRequestDetailRepository>();
            newContainer.RegisterType<ICostDistributionsRepository, CostDistributionsRepository>();            
            newContainer.RegisterType<IHighRiskDrugsRepository, HighRiskDrugsRepository>();
            newContainer.RegisterType<ICurrencyAdminService, CurrencyAdminService>();
            newContainer.RegisterType<ICurrencyRepository, CurrencyRepository>();
            newContainer.RegisterType<IRateManualValidityDetailRepository, RateManualValidityDetailRepository>();
            newContainer.RegisterType<IInventoryConsignmentTransferRepository, InventoryConsignmentTransferRepository>();
            newContainer.RegisterType<Application.Inventory.ConsignmentTransfer.IConsignmentTransferAdminService, Application.Inventory.ConsignmentTransfer.ConsignmentTransferAdminService>();

            //MedicationType
            newContainer.RegisterType<IMedicationTypeAdminService, MedicationTypeAdminService>();
            newContainer.RegisterType<IMedicationTypeRepository, MedicationTypeRepository>();

            //IndigoQueue
            newContainer.RegisterType<IContainersRepository, ContainersRepository>();
            newContainer.RegisterType<IFactoryQueue, FactoryQueue>();

            newContainer.RegisterType<IDashBoardPharmacyAdminService, DashBoardPharmacyAdminService>();
            newContainer.RegisterType<IClosedMonthHeaderRepository, ClosedMonthHeaderRepository>();
            newContainer.RegisterType<IClosedMonthInventoryRepository, ClosedMonthInventoryRepository>();

            newContainer.RegisterType<IRollbackStrategyFactory, RollbackStrategyFactory>();

            newContainer.RegisterType<ISurgicalPackageProcessAdminService, SurgicalPackageProcessAdminService>();
            newContainer.RegisterType<ISurgicalExpenseSheetRepository, SurgicalExpenseSheetRepository>();

            newContainer.RegisterType<IDashboardPharmacyDetailSurgicalPackageRepository, DashboardPharmacyDetailSurgicalPackageRepository>();

            _currentContainer = newContainer;

        }

        #endregion Methods
    }
}