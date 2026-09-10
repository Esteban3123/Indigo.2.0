using Application.Inventory.DecreaseMaximumLimit;
using Application.Inventory.EntranceVoucher;
using Application.Inventory.Sequense;
using Application.Inventory.Warehouse;
using Application.Security;
using DistribuitedServices.Billing;
using DistributedServices.Accounting;
using DistributedServices.Base;
using DistributedServices.Common;
using DistributedServices.Crystal;
using DistributedServices.Inventory;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Maintenance;
using DistributedServices.Payments;
using DistributedServices.Payroll;
using Domain.Crystal;
using Domain.Entities;
using Domain.Security;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.CrossCutting.Security;
using Infrastructure.Data.CrystalRepository;
using Infrastructure.Data.ModelRepository;
using Infrastructure.Data.PayrollRepository;
using Infrastructure.Data.SecurityRepository;
using System;
using Unity;
using Unity.Lifetime;

namespace DistributedService.SCM.Unity
{
    public class ContainerSCM
    {
        private static IUnityContainer _currentContainer;

        /// <summary>
        /// Obtiene la unica instancia del contenedor
        /// </summary>
        /// <returns>Contenedor configurado</returns>
        public static IUnityContainer Current(String container, String hisContainer, string securityContainer = null)
        {
            if (_currentContainer != null)
            {
                ICommonVariables sessionVariables = _currentContainer.Resolve<ICommonVariables>();
                if (sessionVariables.getContainer() == container)
                {
                    return _currentContainer;
                }
            }

            ConfigureContainer(container, hisContainer, securityContainer);

            return _currentContainer;
        }

        private static void ConfigureContainer(string container, string hisContainer, string securityContainer = null)
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

            ///Inyectamos Security Context
            newContainer.RegisterFactory<ISeguridadUnitOfWork>((uc) =>
            {
                var dd = new GenesisEntities(securityContainer);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            //Inyectamos el contexto de payroll
            newContainer.RegisterFactory<IPayrollUnitOfWork>((uc) =>
            {
                var dd = new PayrollUnitOfWork(container);
                dd.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return dd;
            }, new PerResolveLifetimeManager());

            newContainer.RegisterType<DistributedServices.Inventory.Contracts.IInventoryService, InventoryService>();
            newContainer.RegisterType<Domain.Entities.Service.IInventoryService, Domain.Entities.Service.InventoryServices>();

            //IndigoQueue
            newContainer.RegisterType<IContainersRepository, ContainersRepository>();
            newContainer.RegisterType<IFactoryQueue, FactoryQueue>();

            newContainer.RegisterType<IEntranceVoucherAdminService, EntranceVoucherAdminService>();
            newContainer.RegisterType<IEntranceVoucherRepository, EntranceVoucherRepository>();
            newContainer.RegisterType<IInventorySequenceAdminService, InventorySequenceAdminService>();
            newContainer.RegisterType<IInventorySequenceRepository, InventorySequenceRepository>();
            newContainer.RegisterType<IInventorySequenceDetailRepository, InventorySequenceDetailRepository>();

            newContainer.RegisterType<IWarehouseAdminService, WarehouseAdminService>();
            newContainer.RegisterType<IWarehouseRepository, WarehouseRepository>();

            newContainer.RegisterType<IUserAdminService, UserAdminService>();
            newContainer.RegisterType<IUserRepository, UserRepository>();
            newContainer.RegisterType<IUserMembershipRepository, IndigoAutentication>();
            newContainer.RegisterType<IFileUserRepository, FileUserRepository>();

            newContainer.RegisterType<IPermissionCompanyRepository, PermissionCompanyRepository>();
            newContainer.RegisterType<ISEGusuaruRepository, SegusuaruRepository>();
            newContainer.RegisterType<IADCENATENRepository, ADCENATENRepository>();
            newContainer.RegisterType<IINUNIFUNCRepository, INUNIFUNCRepository>();
            newContainer.RegisterType<IINCUPSSUBRepository, INCUPSSUBRepository>();

            newContainer.RegisterType<IDecreaseMaximumLimitAdminService, DecreaseMaximumLimitAdminService>();
            newContainer.RegisterType<IDecreaseMaximumLimitRepository, DecreaseMaximumLimitRespository>();
            newContainer.RegisterType<IConsignmentInventoryRemissionDetailBatchSerialRepository, ConsignmentInventoryRemissionDetailBatchSerialRepository>();

            newContainer.RegisterType<ISupplierService, MaintanceService>();
            newContainer.RegisterType<ICommonERPSuppliersDistributionLines, CommonERPService>();
            newContainer.RegisterType<IAccountingGeneralLedgerIVA, AccountingService>();
            newContainer.RegisterType<IAccountingRetentionConcept, AccountingService>();
            newContainer.RegisterType<IPaymentsPaymentsConcept, PaymentsService>();

            newContainer.RegisterType<IInventoryServiceConsignmentInventoryRemission, InventoryService>();
            newContainer.RegisterType<IInventoryWarehouse, InventoryService>();
            newContainer.RegisterType<IInventoryKardex, InventoryService>();

            newContainer.RegisterType<ICommonERPThirdParty, CommonERPService>();
            newContainer.RegisterType<IInventoryServiceLoanMerchandise, InventoryService>();

            newContainer.RegisterType<IInventoryServiceRemissionEntrance, InventoryService>();

            newContainer.RegisterType<IInventoryPharmaceuticalDispensing, InventoryService>();
            newContainer.RegisterType<IInventoryDashBoardPharmacy, InventoryService>();
            newContainer.RegisterType<ICrystalServiceDashboardPharmacyDetail, CrystalService>();
            newContainer.RegisterType<IInventoryServicePhysicalInventory, InventoryService>();
            newContainer.RegisterType<IInventoryProductRateDetail, InventoryService>();
            newContainer.RegisterType<ICommonService, CommonService>();
            newContainer.RegisterType<IAccountingCompanySettings, AccountingService>();

            newContainer.RegisterType<IInventoryServiceInventoryAdjustment, InventoryService>();
            newContainer.RegisterType<IInventoryAdjustmentConcept, InventoryService>();

            newContainer.RegisterType<IInventorySettingInventory, InventoryService>();

            newContainer.RegisterType<IInventoryServiceRemissionEntranceDetailBatchSerial, InventoryService>();
            newContainer.RegisterType<IInventoryServiceConsignmentInventoryRemissionDetailBatchSerial, InventoryService>();

            newContainer.RegisterType<IInventoryServicePhysicalInventory, InventoryService>();
            newContainer.RegisterType<IInventoryTrasnferOrder, InventoryService>();
            newContainer.RegisterType<IPayrollFunctionalUnit, PayrollService>();

            newContainer.RegisterType<IPaymentsAccountPayable, PaymentsService>();
            newContainer.RegisterType<IInventoryEntranceVoucherDevolution, InventoryService>();

            newContainer.RegisterType<IInventoryServicePharmaceuticalDispensingDevolution, InventoryService>();
            newContainer.RegisterType<IDevolutionMedicationRepository, DevolutionMedicationRepository>();
            newContainer.RegisterType<ICrystalServiceDashboardPharmacyDetailDevolution, CrystalService>();
            newContainer.RegisterType<IInventoryServicePharmaceuticalDispensingDetailBatchSerial, InventoryService>();

            newContainer.RegisterType<IBillingServiceServiceOrder, BillingService>();

            newContainer.RegisterType<IInventoryTransferOrderDevolution, InventoryService>();
            newContainer.RegisterType<IInventoryTrasnferOrderDetailBatchSerial, InventoryService>();

            newContainer.RegisterType<IInventoryServiceRemissionDevolution, InventoryService>();

            newContainer.RegisterType<IInventoryServiceLoanMerchandiseDetail, InventoryService>();
            newContainer.RegisterType<IInventoryServiceLoanMerchandiseDevolution, InventoryService>();

            newContainer.RegisterType<IInventoryServiceRollbackStrategy, InventoryService>();

            newContainer.RegisterType<IInventoryServiceSurgicalPackageProcess, InventoryService>();
            newContainer.RegisterType<ISurgicalExpenseSheetRepository, SurgicalExpenseSheetRepository>();

            newContainer.RegisterType<IDashboardPharmacyDetailSurgicalPackageRepository, DashboardPharmacyDetailSurgicalPackageRepository>();

            newContainer.RegisterType<ISettingInventoryRepository, SettingInventoryRepository>();

            _currentContainer = newContainer;
        }
    }
}