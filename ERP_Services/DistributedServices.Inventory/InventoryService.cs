///***********************************************************************
/// Assembly         : DistributedService.Payroll
/// Author           : Cristhian Salazar
/// Created          : 07-04-2013
///
/// Last Modified By : Daniel Arevalo
/// Last Modified On : 07-04-2013
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

#region Imports

using Application.Inventory;
using Application.Inventory.AdministrationRoute;
using Application.Inventory.AppEntranceVoucherDevolution;
using Application.Inventory.ATC;
using Application.Inventory.ATCEntity;
using Application.Inventory.AttributeProductType;
using Application.Inventory.BatchSerial;
using Application.Inventory.BlockRecordInventory;
using Application.Inventory.ConsignmentInventoryRemission;
using Application.Inventory.ConsignmentInventoryRemissionDetail;
using Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial;
using Application.Inventory.DCI;
using Application.Inventory.DocumentInvoiceProductSales;
using Application.Inventory.DocumentInvoiceProductSalesDetail;
using Application.Inventory.EntranceVoucher;
using Application.Inventory.InventoryAdjustment;
using Application.Inventory.InventoryContract;
using Application.Inventory.InventoryContractDetail;
using Application.Inventory.InventoryContractType;
using Application.Inventory.InventoryControl;
using Application.Inventory.InventoryMassiveConfirm;
using Application.Inventory.InventoryProduct;
using Application.Inventory.InventoryRequest;
using Application.Inventory.InventoryRequestDetail;
using Application.Inventory.InventoryRequestDevolution;
using Application.Inventory.InventoryRiskLevel;
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
using Application.Inventory.ProductRateDetail;
using Application.Inventory.ProductSubGroups;
using Application.Inventory.ProductTemplate;
using Application.Inventory.ProductType;
using Application.Inventory.PurchaseOrder;
using Application.Inventory.PurchaseOrderDetail;
using Application.Inventory.PurchaseOrderDevolution;
using Application.Inventory.RemissionDevolution;
using Application.Inventory.RemissionEntrance;
using Application.Inventory.RemissionEntranceDetail;
using Application.Inventory.RemissionEntranceDetailBatchSerial;
using Application.Inventory.RemissionOutput;
using Application.Inventory.RemissionOutputDetail;
using Application.Inventory.RemissionOutputDetailPhysical;
using Application.Inventory.Sequense;
using Application.Inventory.SettingInventory;
using Application.Inventory.TransferOrder;
using Application.Inventory.TransferOrderDetail;
using Application.Inventory.TransferOrderDetailBatchSerial;
using Application.Inventory.TransferOrderDevolution;
using Application.Inventory.Warehouse;
using Application.Inventory.WarehouseStock;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Domain.Crystal.Entities;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Application.Inventory.Rollback;
using Infrastructure.CrossCutting.Rollback;
using DistributedServices.Authentication;

#endregion Imports

namespace DistributedServices.Inventory
{
    [JwtMessageServiceBehavior, ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class InventoryService : IInventoryService
    {

        public async Task<bool> ExecuteRollbackAsync(string rollbackObjectCode, string entityName)
        {
            using (var factory = DistributedServices.Inventory.Unity.Container.Current.Resolve<IRollbackStrategyFactory>())
            {
                Type strategy = factory.GetStrategy(entityName);

                using (var service = (IAdminServiceRollbackStrategy)DistributedServices.Inventory.Unity.Container.Current.Resolve(strategy))
                {
                    var res = await service.ExecuteRollbackAsync(rollbackObjectCode);
                    return res > 0;
                }

            }
        }
    }
}