///***********************************************************************
/// Assembly         : DistributedService.Payroll
/// Author           : Cristhian Salazar
/// Created          : 07/07/2013
///
/// Last Modified By : Daniel Arevalo
/// Last Modified On : 07-04-2013
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

#region Imports

using System.ServiceModel;

#endregion Imports

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryService : IInventoryProductGroups, IInventorySequense, IInventoryBlockRecordInventory, IInventoryProductSubGroups, IInventoryMeasureUnit, IInventoryWarehouse, IInventoryPharmacologicalGroup,
        IInventoryAdjustmentConcept, IInventoryManufacturer, IInventoryOtherWithholdingDeductions, IInventoryProductType, IInventoryAttributeProductType, IInventoryDCI, IInventoryPharmaceuticalForm,
        IInventoryAdministrationRoute, IInventoryRiskLevel, IInventoryProduct, IInventoryATC, IInventoryProductTemplate, IInventoryContract, IInventoryContractType, IInventoryBatchSerial,
        IInventoryServiceRemissionEntrance, IInventoryServiceRemissionEntranceDetail, IInventoryServiceInventoryContractDetail, IInventoryServicePurchaseOrderDetail, IInventoryPurchaseOrder, IInventoryEntranceVoucher,
        IInventoryServicePhysicalInventory, IInventoryServiceRemissionOutput, IInventoryServiceRemissionOutputDetail, IInventorySettingInventory, IInventoryServiceRemissionEntranceDetailBatchSerial, IInventoryPharmaceuticalDispensing,
        IInventoryServiceRemissionOutputDetailPhysical, IInventoryServiceRemissionDevolution, IInventoryProductRateDetail, IInventoryEntranceVoucherDevolution, IInventoryServicePackagingUnit, IInventoryInventoryControl,
        IInventoryServicePharmaceuticalDispensingDetailBatchSerial, IInventoryServiceInventoryAdjustment, IInventoryServicePharmaceuticalDispensingDevolution, IInventoryServicePharmaceuticalDispensingDevolutionDetail,
        IInventoryServiceLoanMerchandise, IInventoryServiceLoanMerchandiseDetail, IInventoryRequest, IInventoryServiceLoanMerchandiseDevolution, IInventoryServiceLoanMerchandiseDevolutionDetail, IInventoryTrasnferOrder,
        IInventoryServiceTransferOrderDetail, IInventoryServiceInventoryRequestDetail, IInventoryTransferOrderDevolution, IInventoryTrasnferOrderDetailBatchSerial, IInventoryServiceWarehouseStock,
        IInventoryServiceDocumentInvoiceProductSales, IInventoryServiceDocumentInvoiceProductSalesDetail, IInventoryServiceInventoryMassiveConfirm, IInventoryPurchaseOrderDevolution, IInventoryRequestDevolution,
        IInventoryServiceConsignmentInventoryRemission, IInventoryServiceConsignmentInventoryRemissionDetail, IInventoryServiceConsignmentInventoryRemissionDetailBatchSerial, IInventoryServiceConsumeService, IInventoryATCEntity,
        IInventoryServicePurchaseRequest, IInventoryServicePurchaseRequestDetail, IInventoryShelfType, IInventoryServiceInventorySupplie, IInventoryContractAssignment, IInventoryContractModification,
        IInventoryServiceReports, IInventoryServiceBacterialResistanceMedication, IInventoryServiceDevolutionCause, IInventoryServiceAntineoplasicoMedication, IInventoryPharmaceuticalDispensingTransfer, IInventoryRequestParam,
        IInventoryServiceConsignmentTransfer,IInventoryServiceProductInTransit, IInventoryServiceProductInTransitDetail, IInventoryConsignmentCostList, IInventoryUPRUnits, IInventoryServicePharmaceuticalFormGrouping, IInventoryKardex, 
        IInventoryDashBoardPharmacy, IInventoryMedicationType, IInventoryStorageTemperature,IInventoryServiceRollbackStrategy, IInventoryServiceSurgicalPackageProcess
    {
    }
}