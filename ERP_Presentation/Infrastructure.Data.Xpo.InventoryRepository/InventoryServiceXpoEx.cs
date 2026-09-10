//'***********************************************************************
//' Assembly         : Infrastructure.Data.Xpo.PaymentsRepostory
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 04-04-2014
//'
//' Copyright        : (c) . All rights reserved.
//'***********************************************************************

#region Imports

using DevExpress.Xpo;
using Infrastructure.CrossCutting.Xpo.Base;
using DevExpress.Data.Filtering;
using DevExpress.Data.Linq;
using Infrastructure.Data.Xpo.InventoryRepository.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion Imports

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    public class InventoryServiceXpoEx : XpoBaseService, IDisposable
    {
        #region PublicMethods

        /// <summary>
        /// Lista los atc asociados al dci
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListATCEntityRelatedDCI(int DCIId)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("DCIId = " + DCIId.ToString());
            var session = new IndigoXPOSession<ViewListATCEntityRelatedDCIXpo>();
            var classEntity = session.GetClassInfo(typeof(ViewListATCEntityRelatedDCIXpo));
            var serverMode = new XPInstantFeedbackSource(classEntity, "Id;ATCId;Code;Name;CodeName;DCIId", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;

        }

        /// <summary>
        /// lista todas las ordenes de traslado por filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRequestDevolution()
        {
            var session = new IndigoXPOSession<InventoryRequestDevolutionXpo>();
            var classEntity = session.GetClassInfo(typeof(InventoryRequestDevolutionXpo));
            var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Description;DocumentDate;StatusName;Status", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;

        }
        /// <summary>
        /// lista todos los movimientos manuales por codigo producto , ingreso, codigo paciente
        /// </summary>
        /// <returns></returns>
        public XPCollection<ViewManualMovementsXpo> ListManualMovements(string patientCode , string admissionNumber)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse($"PatientCode = '{patientCode}' AND AdmissionNumber = '{admissionNumber}'");
            var session = new IndigoXPOSession<ViewManualMovementsXpo>();
            var classEntity = session.GetClassInfo(typeof(InventoryRequestDetailXpo));
            return new XPCollection<ViewManualMovementsXpo>(session, criteria);
        }



        /// <summary>
        /// lista todas las ordenes de traslado por filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllMedicalFormula()
        {
            var session = new IndigoXPOSession<MedicalFormulaXpo>();
            var classEntity = session.GetClassInfo(typeof(MedicalFormulaXpo));
            var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Number;PatientCode;PatientName;CreationDate", null);
            serverMode.DefaultSorting = "Number";
            return serverMode;

        }

        public XPInstantFeedbackSource ListMeasurementUnitByIds(List<int> ListMeasurementUnitIds)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("Id in (" + string.Join(",", ListMeasurementUnitIds.ToArray()) + ")");
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {
                dynamic classEntity = session.GetClassInfo(typeof(MeasureUnitXpo));
                dynamic serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;Abbreviation;CostValue", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Metodo para obtener el listado de solicitudes en estado confirmado y filtradas por tipo
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public XPCollection<InventoryRequestDetailXpo> GetListRequestConfirmed(int type)
        {
            var session = new IndigoXPOSession<InventoryRequestDetailXpo>();

            CriteriaOperator criteria = CriteriaOperator.Parse("InventoryRequestId.RequestType=" + type + " And InventoryRequestId.Status=2 And OutstandingQuantity > 0");
            var classEntity = session.GetClassInfo(typeof(InventoryRequestDetailXpo));
            return new XPCollection<InventoryRequestDetailXpo>(session, criteria);
        }

        public XPInstantFeedbackSource LoadDevolutionCausesByStatus(bool status)
        {
            var session = new IndigoXPOSession<DevolutionCauseXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(String.Format("Status = {0}", status));
                dynamic classEntity = session.GetClassInfo(typeof(DevolutionCauseXpo));
                dynamic serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria);
                return serverMode;
            }
        }

        public XPInstantFeedbackSource ListProductsByWarehouseConsigment(int WarehouseId)
        {
            var session = new IndigoXPOSession<ViewListConsignmentWarehouseProductsXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(String.Format("WarehouseId = {0}", WarehouseId));
                dynamic classEntity = session.GetClassInfo(typeof(ViewListConsignmentWarehouseProductsXpo));
                dynamic serverMode = new XPInstantFeedbackSource(classEntity,null, criteria);
                return serverMode;
            }

        }

        public XPInstantFeedbackSource ListBatchByWarehouseConsigment(int WarehouseId, int ProductId)
        {
            var session = new IndigoXPOSession<ViewListConsignmentWarehouseProductsBatchSerialXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(String.Format($"WarehouseId = {WarehouseId} and ProductId = {ProductId}"));
                dynamic classEntity = session.GetClassInfo(typeof(ViewListConsignmentWarehouseProductsBatchSerialXpo));
                dynamic serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }

        }

        /// <summary>
        /// Lista todos los agrupadores de forma farmaceutica
        /// acepta status como parametro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPharmaceuticalFormGroups(bool? status = null)
        {
            var session = new IndigoXPOSession<PharmaceuticalFormGroupingXpo>();
            {
                CriteriaOperator criteria = null;
                if (status != null)
                {
                    criteria = CriteriaOperator.Parse(String.Format($"Status={status}"));
                }
                dynamic classEntity = session.GetClassInfo(typeof(PharmaceuticalFormGroupingXpo));
                dynamic serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria);
                return serverMode;
            }

        }


        /// <summary>
        /// Lista todas las unidades UPR
        /// acepta status como parametro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListUPRUnits(bool? status = null)
        {
            var session = new IndigoXPOSession<UPRUnitsXpo>();
            {
                CriteriaOperator criteria = null;
                if (status!=null)
                {
                    criteria = CriteriaOperator.Parse(String.Format($"Status={status}"));
                }
                dynamic classEntity = session.GetClassInfo(typeof(UPRUnitsXpo));
                dynamic serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria);
                return serverMode;
            }

        }

        /// <summary>
        /// Lista los tipos de solicitudes
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRequestType()
        {
            var session = new IndigoXPOSession<RequestTypeXpo>();
            {
                dynamic classEntity = session.GetClassInfo(typeof(RequestTypeXpo));
                dynamic serverMode = new XPInstantFeedbackSource(classEntity,null, null);
                return serverMode;
            }
        }
        #region Others

        /// <summary>
        /// metodo para validar que el producto tiene moviento en el kardex
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public int GetKardexByProductId(int ProductId)
        {
            var session = new IndigoXPOSession<KardexXpo>();
            {
                var count = session.Evaluate(typeof(KardexXpo), CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("ProductId=" + ProductId));
                return Convert.ToInt32(count);
            }
        }

        /// <summary>
        /// metodo para validar que el almacen tiene moviento en el kardex
        /// </summary>
        /// <param name="WarehouseId"></param>
        /// <returns></returns>
        public int GetKardexByWarehouseId(int WarehouseId)
        {
            var session = new IndigoXPOSession<KardexXpo>();
            {
                var count = session.Evaluate(typeof(KardexXpo), CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("WarehouseId=" + WarehouseId));
                return Convert.ToInt32(count);
            }
        }

        /// <summary>
        /// metodo para validar que el producto tiene cantidades > 0 en el PhysicalInventory
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public int GetPhysicalInventoryByProductId(int ProductId)
        {
            var session = new IndigoXPOSession<PhysicalInventoryXpo>();
            {
                var count = session.Evaluate(typeof(PhysicalInventoryXpo), CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("ProductId=" + ProductId + " AND Quantity > 0"));
                return Convert.ToInt32(count);
            }
        }


        /// <summary>
        /// Lista todas las devoluciones de ordenes de traslado
        /// </summary>
        public XPCollection<PhysicalInventoryXpo> GetWareHouseByPhysicalInventoryByProductId(int ProductId)
        {
            var session = new IndigoXPOSession<PhysicalInventoryXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse($"ProductId={ProductId} AND Quantity > 0");
            var classEntity = session.GetClassInfo(typeof(PhysicalInventoryXpo));
            var groupInfo = new XPCollection<PhysicalInventoryXpo>(session, criteria);
            return groupInfo;
        }

        /// <summary>
        /// lista todas las ordenes de traslado por filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListTransferOrderByFilter(string filtro)
        {
            var session = new IndigoXPOSession<InventoryTransferOrderReportXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
                var classEntity = session.GetClassInfo(typeof(InventoryTransferOrderReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;OrderType;OrderTypeName;Status;Description;StatusName", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        public XPCollection<InventoryControlDocumentXpo> ListInventoryControlDocumentByDocumentType(int documentType)
        {
            var session = new IndigoXPOSession<InventoryControlDocumentXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("DocumentType=" + documentType + "");
            var classEntity = session.GetClassInfo(typeof(InventoryControlDocumentXpo));
            return new XPCollection<InventoryControlDocumentXpo>(session, criteria);
        }

        /// <summary>
        /// Lista los detalles del Kardex
        /// </summary>
        public XPCollection<InventoryViewReportKardexXpo> ListKardex(string filtro, string orderby = "DocumentDate")
        {
            var session = new IndigoXPOSession<InventoryViewReportKardexXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
            var classEntity = session.GetClassInfo(typeof(InventoryViewReportKardexXpo));
            var list = new XPCollection<InventoryViewReportKardexXpo>(session, criteria);
            list.Sorting.Add(new SortProperty(orderby, DevExpress.Xpo.DB.SortingDirection.Ascending));
            list.Sorting.Add(new SortProperty("KardexId", DevExpress.Xpo.DB.SortingDirection.Ascending));
            return list;
        }

        public XPCollection<AttributeProductTypeXpo> ListAttibutesInventoryProductByProductTypeId(int productTypeId, bool status)
        {
            var session = new IndigoXPOSession<AttributeProductTypeXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse($"ProductTypeId={productTypeId} And Status={status}");
            var classEntity = session.GetClassInfo(typeof(AttributeProductTypeXpo));
            return new XPCollection<AttributeProductTypeXpo>(session, criteria);
        }

        public XPCollection<AttributeProductTypeOptionListXpo> ListAttibutesOptionListByAttributeProductTypeId(int attributeProductTypeId)
        {
            var session = new IndigoXPOSession<AttributeProductTypeOptionListXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse($"AttributeProductTypeId={attributeProductTypeId}");
            var classEntity = session.GetClassInfo(typeof(AttributeProductTypeOptionListXpo));
            return new XPCollection<AttributeProductTypeOptionListXpo>(session, criteria);
        }

        /// <summary>
        /// Lista los detalles del control de inventario
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlDetailByInventoryControlId(int inventoryControlId)
        {
            var session = new IndigoXPOSession<InventoryControlDetailXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("InventoryControlId =" + inventoryControlId);
                var classEntity = session.GetClassInfo(typeof(InventoryControlDetailXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los detalles de los detalles del control de inventario
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlDetailBatchSerialByInventoryControlDetailId(int inventoryControlDetailId)
        {
            var session = new IndigoXPOSession<InventoryControlDetailBatchSerialXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("InventoryControlDetailId =" + inventoryControlDetailId);
                var classEntity = session.GetClassInfo(typeof(InventoryControlDetailBatchSerialXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las devoluciones de ordenes de traslado
        /// </summary>
        public XPInstantFeedbackSource ListTransferOrderDevolution()
        {
            var session = new IndigoXPOSession<TransferOrderDevolutionXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(TransferOrderDevolutionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;TransferOrderId.Code;Status;Description;StatusName", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las ordenes de traslado
        /// </summary>
        public XPInstantFeedbackSource ListTransferOrderByStatus(int status)
        {
            var session = new IndigoXPOSession<InventoryTransferOrderXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status =" + status);
                var classEntity = session.GetClassInfo(typeof(InventoryTransferOrderXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;OrderType;OrderTypeName;Status;Description;StatusName", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las ordenes de traslado
        /// </summary>
        public XPInstantFeedbackSource ListTransferOrder()
        {
            var session = new IndigoXPOSession<InventoryTransferOrderXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryTransferOrderXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;OrderType;OrderTypeName;Status;Description;StatusName;DispatchToDescription;TargetDescription;SourceWarehouseId.CodeName", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las facturas de productos
        /// </summary>
        public XPInstantFeedbackSource ListDocumentInvoiceProductSales()
        {
            var session = new IndigoXPOSession<InventoryDocumentInvoiceProductSalesXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryDocumentInvoiceProductSalesXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ThirdPartyId.NitName;WarehouseId.CodeName;InvoiceId.InvoiceNumber;Status;StatusName", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las facturas de productos por estado
        /// </summary>
        public XPInstantFeedbackSource ListDocumentInvoiceProductSalesByStatus(int status, int warehouseId)
        {
            var session = new IndigoXPOSession<InventoryDocumentInvoiceProductSalesXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status = " + status + " and WarehouseId.Id = " + warehouseId);
                var classEntity = session.GetClassInfo(typeof(InventoryDocumentInvoiceProductSalesXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ThirdPartyId.NitName;WarehouseId.CodeName;WarehouseId.Id;InvoiceId.InvoiceNumber;Status;StatusName,ContractExternalClients", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los almacenes
        /// </summary>
        public XPInstantFeedbackSource ListInventoryRequest()
        {
            var session = new IndigoXPOSession<InventoryRequestXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(InventoryRequestXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;RequestType;RequestTypeName;Status;Observation;StatusName;SourceWarehouseId.CodeName;TargetWarehouseId.CodeName;TargetFunctionalUnitId.CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las solicitudes de compra de inventario
        /// </summary>
        public XPInstantFeedbackSource ListPurchaseRequest()
        {
            var session = new IndigoXPOSession<PurchaseRequestXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(PurchaseRequestXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;CreationDate;RequestTypeId;RequestTypeName;Status;Observation;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// lista los conceptos de ajuste de inventario por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAdjustmentConceptByConceptType(int movementClass, int conceptType, bool status)
        {
            var session = new IndigoXPOSession<AdjustmentConceptXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("MovementClass = " + movementClass + "And ConceptType = " + conceptType + "And Status =" + status);
                var classEntity = session.GetClassInfo(typeof(AdjustmentConceptXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ConceptType", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }


        /// <summary>
        /// lista los conceptos de ajuste de inventario por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAdjustmentConceptByUsersAndCostCenter(int movementClass, int conceptType, bool status, string UserCode, int? CostCenterId)
        {
            var session = new IndigoXPOSession<AdjustmentConceptXpo>();
            {
                string FilterCostcenter = (CostCenterId != null) ? $"AND CostCenterId={CostCenterId}" : "";
                CriteriaOperator criteria = CriteriaOperator.Parse($"MovementClass = {movementClass} And ConceptType = {conceptType} And Status ={status} And Inventory_AdjustmentConceptUsers[UserCode ='{UserCode}'] {FilterCostcenter}");
                var classEntity = session.GetClassInfo(typeof(AdjustmentConceptXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ConceptType", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }


        /// <summary>
        /// lista los conceptos de ajuste de inventario por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAdjustmentConceptByConceptTypeAndMovement(int conceptType, int movement, bool status,  string Codeuser = null)
        {
            var session = new IndigoXPOSession<AdjustmentConceptXpo>();
            {
                string filterCodeUser = "";
                if (Codeuser != null)
                {
                    filterCodeUser = $"And Inventory_AdjustmentConceptUsers[UserCode = '{Codeuser}']";
                }

                CriteriaOperator criteria = CriteriaOperator.Parse($"ConceptType = {conceptType} And MovementClass = {movement} And Status = {status} {filterCodeUser}"  );
                var classEntity = session.GetClassInfo(typeof(AdjustmentConceptXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ConceptType;ConceptTypeName", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todas las ordenes de servicio con un filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPurchaseOrderReportByFilter(string filtro)
        {
            var session = new IndigoXPOSession<InventoryPurchaseOrderReportXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
                var classEntity = session.GetClassInfo(typeof(InventoryPurchaseOrderReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;DeliveredDate;WarehouseId.Code;StatusName", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        public XPInstantFeedbackSource ListAllInventoryPurchaseOrderDevolution()
        {
            var session = new IndigoXPOSession<InventoryPurchaseOrderDevolutionXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryPurchaseOrderDevolutionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los comprobantes de entrada por filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListEntranceVoucherReportByFilter(string filtro)
        {
            var session = new IndigoXPOSession<InventoryEntranceVoucherReportXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
                var classEntity = session.GetClassInfo(typeof(InventoryEntranceVoucherReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Code;Status;StatusName", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las devoluciones de remision por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionEntranceReportFilter(string Filtro)
        {
            var session = new IndigoXPOSession<InventoryRemissionEntranceReportXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(Filtro);
                var classEntity = session.GetClassInfo(typeof(InventoryRemissionEntranceReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Name;Status;ProductStatus", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las devoluciones de remision por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionDevolutionByTypeReport(string filtro)
        {
            var session = new IndigoXPOSession<InventoryRemissionDevolutionReportXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
                var classEntity = session.GetClassInfo(typeof(InventoryRemissionDevolutionReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;RemissionCode;WarehouseId.CodeName;StatusName", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los comprobantes de entrada
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListEntranceVoucherReport()
        {
            var session = new IndigoXPOSession<InventoryEntranceVoucherReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryEntranceVoucherReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Code;Status", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todas las ordenes de servicio
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPurchaseOrderReport()
        {
            var session = new IndigoXPOSession<InventoryPurchaseOrderReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryPurchaseOrderReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;DeliveredDate;WarehouseId.Code;Status", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los proveedores
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionEntranceReport()
        {
            var session = new IndigoXPOSession<InventoryRemissionEntranceReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryRemissionEntranceReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Name;Status;ProductStatus", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los proveedores
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListSupplierInventoryReport()
        {
            var session = new IndigoXPOSession<InventoryCommonSupplierXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryCommonSupplierXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;IdThirdParty.Id;IdThirdParty.Nit;IdThirdParty.Name;Name;IdCity.Name;StatusName;ThirdPartyNit", null);
                serverMode.DefaultSorting = "IdThirdParty.Nit";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los clientes
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListCustomerInventoryReport()
        {
            var session = new IndigoXPOSession<InventoryCommonCustomerReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryCommonCustomerReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Nit;ThirdPartyId.Name;Name;ThirdPartyId.PersonId.IdentificacionCityId.Name;State;StateName", null);
                serverMode.DefaultSorting = "Nit";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListProductsReport()
        {
            var session = new IndigoXPOSession<InventoryProductReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryProductReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId.Name;ProductGroupId.Name;ProductSubGroupId.Name;ProductControl;CodeName;ProductTypeIdName;ProductSubGroupIdName;ProductGroupIdName", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los almacenes
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListWarehouseReport()
        {
            var session = new IndigoXPOSession<InventoryWarehouseReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryWarehouseReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los almacenes
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryWarehouseReportXpo> ListWarehouseReportC()
        {
            var session = new IndigoXPOSession<InventoryWarehouseReportXpo>();
            //CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber='" + admissionNumber + "'");
            XPCollection<InventoryWarehouseReportXpo> collect = new XPCollection<InventoryWarehouseReportXpo>(session);
            return collect;
        }

        /// <summary>
        /// lista todos los lotes
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListBatchSerialReport()
        {
            var session = new IndigoXPOSession<InventoryBatchSerialReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryBatchSerialReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;BatchCode;ExpirationDate;ProductId.Code;ProductId.Name", null);
                serverMode.DefaultSorting = "BatchCode";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los grupos por clase producto
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListGroupReport()
        {
            var session = new IndigoXPOSession<InventoryProductGroupReportXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("GroupClass= 1");
                var classEntity = session.GetClassInfo(typeof(InventoryProductGroupReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los subgrupos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListSubGroupReport()
        {
            var session = new IndigoXPOSession<InventoryProductSubGroupReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryProductSubGroupReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;HandlesBatch;HandlesExpiry;HandlesBatchType;HandlesExpiryType", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// lista todas las tarifas de los productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListProductRateByIdName()
        {
            var session = new IndigoXPOSession<InventoryProductRateReportXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryProductRateReportXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Code;Name", null);
                serverMode.DefaultSorting = "Code";
                return serverMode;
            }
        }

        /// <summary>
        /// retornna todas las devoluciones de remisiones
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllRemissionDevolution()
        {
            var session = new IndigoXPOSession<RemissionDevolutionXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(RemissionDevolutionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;WarehouseId.CodeName;StatusName;DevolutionTypeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retornna las remisiones de salida por unidad operativa
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllRemissionOutput()
        {
            var session = new IndigoXPOSession<RemissionOutputXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(RemissionOutputXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;WarehouseId.CodeName;CustomerId.NitName;StatusName;ProductStatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// obtiene las remisiones de entrada por estado
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionOutputByStatus(int status = 2, int? warehouseId = null)
        {
            var session = new IndigoXPOSession<RemissionOutputXpo>();
            {
                string filter = "ProductStatus<>3 And Status=" + status;
                if (warehouseId != null)
                {
                    filter = filter + " And WarehouseId.Id=" + warehouseId;
                }
                CriteriaOperator criteria = CriteriaOperator.Parse(filter);

                var classEntity = session.GetClassInfo(typeof(RemissionOutputXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;WarehouseId.CodeName;CustomerId.NitName;StatusName;ProductStatusName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// retornna las remisiones de entrada
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllRemissionEntrance()
        {
            var session = new IndigoXPOSession<RemissionEntranceXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(RemissionEntranceXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DisplaySupplier;RemissionDate;WarehouseId.CodeName;TotalValue;StatusName;ProductStatusName;CurrencyAbbreviation", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retorna los cargue de productos en transito
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllProductInTransit()
        {
            var session = new IndigoXPOSession<ProductInTransitXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(ProductInTransitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DisplaySupplier;DocumentDate;WarehouseId.CodeName;TotalValue;StatusName;ProductStatusName;CurrencyAbbreviation", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retorna los cargue de parametros pet
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public PETDefaultSettingsXpo ListPETParameters()
        {
            var session = new IndigoXPOSession<PETDefaultSettingsXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("");
            PETDefaultSettingsXpo PETDefaultSettings = new XPCollection<PETDefaultSettingsXpo>(session, criteria).FirstOrDefault();
            return PETDefaultSettings;
        }

        /// <summary>
        /// retornna las remisiones de entrada
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllConsignmentInventoryRemission()
        {
            var session = new IndigoXPOSession<ConsignmentInventoryRemissionXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(ConsignmentInventoryRemissionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DisplaySupplier;RemissionDate;WarehouseId.CodeName;TotalValue;StatusName;ProductStatusName;MovementTypeName;CurrencyAbbreviation", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retornna las remisiones de entrada
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPCollection ListAllConsignmentInventoryRemissionWithPendingQuantityReplacement(int SupplierId, int warehouseId)
        {
            var session = new IndigoXPOSession<ConsignmentInventoryRemissionDetailBatchSerialXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.SupplierId.Id =" + SupplierId + " AND ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.WarehouseId.Id = " + warehouseId + " AND ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.Status = 2 AND PendingQuantityReplacement > 0");
            XPCollection collect = new XPCollection(session, typeof(ConsignmentInventoryRemissionDetailBatchSerialXpo), criteria);
            return collect;
        }

        /// <summary>
        /// Lista los detalles que tenga pendiente legalizacion en la tabla de control
        /// </summary>
        /// <returns></returns>
        public XPCollection<ConsignmentInventoryRemissionDetailControlXpo> ListConsignmentInventoryRemissionDetailControlXpo(int DetailId ,int? BatchSerialId)
        {
            var session = new IndigoXPOSession<ConsignmentInventoryRemissionDetailControlXpo>();
            {
                var filterBatchSerial = BatchSerialId is null ? "BatchSerialId is null" : $"BatchSerialId = {BatchSerialId}";
                CriteriaOperator criteria = CriteriaOperator.Parse($"ConsignmentInventoryRemissionDetailId ={DetailId} AND QuantityPendingLegalization>0 AND {filterBatchSerial}");
                var serverMode = new XPCollection<ConsignmentInventoryRemissionDetailControlXpo>(session, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// retorna solicitud de prestamo
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllLoanMerchandise()
        {
            var session = new IndigoXPOSession<InventoryLoanMerchandiseXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryLoanMerchandiseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;ThirdPartyId.CodeName;StatusName;LoanTypeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retorna solicitud de prestamo
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllLoanMerchandiseByWareHouseIdAndConfirm(int IdStores)
        {
            var session = new IndigoXPOSession<InventoryLoanMerchandiseXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=2 AND InventoryLoanMerchandiseDetails[OutstandingQuantity > 0] AND WarehouseId.Id =" + IdStores);
                var classEntity = session.GetClassInfo(typeof(InventoryLoanMerchandiseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;CodeThirdParty;WarehouseId.CodeName;ThirdPartyId.CodeName;StatusName;LoanTypeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// lista los productos con cantidad para devolver
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(string admissionNumber)
        {
            var session = new IndigoXPOSession<PharmaceuticalDispensingDetailBatchSerialXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("OutstandingQuantity > 0 AND PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.AdmissionNumber ='" + admissionNumber + "' and PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Status = 2");
                var classEntity = session.GetClassInfo(typeof(PharmaceuticalDispensingDetailBatchSerialXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// retorna de devoluciones de prestamo
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllLoanMerchandiseDevolutions()
        {
            var session = new IndigoXPOSession<InventoryLoanMerchandiseDevolutionXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryLoanMerchandiseDevolutionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retornna las remisiones de entrada
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllPharmaceuticalDispensingDevolution()
        {
            var session = new IndigoXPOSession<PharmaceuticalDispensingDevolutionXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(PharmaceuticalDispensingDevolutionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;AdmissionNumber;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retornna las remisiones de entrada por unidad operativa
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionEntranceByStatus(int status = 2, int? warehouseId = null)
        {
            var session = new IndigoXPOSession<RemissionEntranceXpo>();
            {
                string filter = "ProductStatus<>3 And Status=" + status;
                if (warehouseId != null)
                {
                    filter = filter + " And WarehouseId.Id=" + warehouseId;
                }
                CriteriaOperator criteria = CriteriaOperator.Parse(filter);

                var classEntity = session.GetClassInfo(typeof(RemissionEntranceXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DisplaySupplier;RemissionDate;WarehouseId.CodeName;TotalValue;StatusName;ProductStatusName;CurrencyAbbreviation", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// retornna las remisiones de inventario en consignación por estado
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListConsignmentInventoryRemissionByStatus(int status = 2, int? warehouseId = null)
        {
            var session = new IndigoXPOSession<ConsignmentInventoryRemissionXpo>();
            {
                string filter = "ProductStatus<>3 And Status=" + status;
                if (warehouseId != null)
                {
                    filter = filter + " And WarehouseId.Id=" + warehouseId;
                }
                CriteriaOperator criteria = CriteriaOperator.Parse(filter);

                var classEntity = session.GetClassInfo(typeof(ConsignmentInventoryRemissionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DisplaySupplier;RemissionDate;WarehouseId.CodeName;TotalValue;StatusName;ProductStatusName;CurrencyAbbreviation", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las dispensaciones farmaceuticas
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPharmaceuticalDispensing()
        {
            var session = new IndigoXPOSession<InventorylistPharmaceuticalDispensingXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventorylistPharmaceuticalDispensingXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;AdmissionNumber;CodeNamePatient;DocumentDate;StatusName;Status;Warehouse;CreationUser", null);
                return serverMode;
            }
        }

        /// <summary>
        /// lista los lotes por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListBatchSerialByProductId(int ProductId)
        {
            var session = new IndigoXPOSession<BatchSerialXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ProductId=" + ProductId);
                var classEntity = session.GetClassInfo(typeof(BatchSerialXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Type;BatchCode;ExpirationDate;Barcode", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// lista el almacen por id del producto
        /// </summary>
        /// <returns></returns>
        public XPCollection<PhysicalInventoryXpo> ListPhysicalInventoryByProductId(int ProductId)
        {
            var session = new IndigoXPOSession<PhysicalInventoryXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductId=" + ProductId);
            XPCollection<PhysicalInventoryXpo> collect = new XPCollection<PhysicalInventoryXpo>(session, criteria);

            return collect;
        }

        /// <summary>
        /// lista todos los almacenes XPOCollection
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryWarehouseReportXpo> ListWarehouseReportXPCollection()
        {
            var session = new IndigoXPOSession<InventoryWarehouseReportXpo>();
            XPCollection<InventoryWarehouseReportXpo> collect = new XPCollection<InventoryWarehouseReportXpo>(session);
            return collect;
        }

        /// <summary>
        /// Lista los detalles del Kardex
        /// </summary>
        public XPCollection<InventoryViewRemissionEntranceDetailReportXpo> ListRemissionEntranceDeatilReport(string filtro)
        {
            var session = new IndigoXPOSession<InventoryViewRemissionEntranceDetailReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
            var classEntity = session.GetClassInfo(typeof(InventoryViewRemissionEntranceDetailReportXpo));
            var list = new XPCollection<InventoryViewRemissionEntranceDetailReportXpo>(session, criteria);
            //list.Sorting.Add(new SortProperty("DocumentDate", DevExpress.Xpo.DB.SortingDirection.Ascending));
            //list.Sorting.Add(new SortProperty("KardexId", DevExpress.Xpo.DB.SortingDirection.Ascending));
            return list;
        }

        /// <summary>
        /// lista productos por admission (frmAdmissionProduct)
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListViewAdmissionProductServer(string admissionNumber)
        {
            var session = new IndigoXPOSession<InventoryViewAdmissionProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber='" + admissionNumber + "'");
                var classEntity = session.GetClassInfo(typeof(InventoryViewAdmissionProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// lista productos por admission (frmAdmissionProduct)
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryViewAdmissionProductXpo> ListViewAdmissionProduct(string admissionNumber)
        {
            var session = new IndigoXPOSession<InventoryViewAdmissionProductXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber='" + admissionNumber + "'");
            XPCollection<InventoryViewAdmissionProductXpo> collect = new XPCollection<InventoryViewAdmissionProductXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// lista el detalle de productos por admission (frmAdmissionProduct)
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryViewAdmissionProductDetailXpo> ListViewAdmissionProductDetail(string admissionNumber, int functionalUnitId, int productId)
        {
            var session = new IndigoXPOSession<InventoryViewAdmissionProductDetailXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber='" + admissionNumber + "' and FunctionalUnitId=" + functionalUnitId + " and ProductId=" + productId);
            XPCollection<InventoryViewAdmissionProductDetailXpo> collect = new XPCollection<InventoryViewAdmissionProductDetailXpo>(session, criteria);

            return collect;
        }

        /// <summary>
        /// Lista los detalles de productRate
        /// </summary>
        /// <returns></returns>
        public XPCollection ListProductRateDetailByProductRateId(int productRateId)
        {
            var session = new IndigoXPOSession<ProductRateDetailXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductRateId.Id=" + productRateId);
            XPCollection collect = new XPCollection(session, typeof(ProductRateDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// Lista los detalles de productRate
        /// </summary>
        /// <returns></returns>
        public XPCollection ListProductRateConditionByProductRateId(int productRateId)
        {
            var session = new IndigoXPOSession<ProductRateGeneralXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductRateId.Id=" + productRateId);
            XPCollection collect = new XPCollection(session, typeof(ProductRateGeneralXpo), criteria);
            return collect;
        }

        /// <summary>
        /// Lista los detalles de productRate
        /// </summary>
        /// <returns></returns>
        public XPCollection ListWarehouseConditions(int warehouseId)
        {
            var session = new IndigoXPOSession<WarehouseRestrictedConditionsXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("WarehouseId=" + warehouseId);
            XPCollection collect = new XPCollection(session, typeof(WarehouseRestrictedConditionsXpo), criteria);
            return collect;
        }

        /// <summary>
        /// Obtiene los parametros de inventario por unidad operativa
        /// </summary>
        /// <returns></returns>
        public XPCollection GetSettingInventoryByOperatingUnitId(int operatingUnitId)
        {
            var session = new IndigoXPOSession<SettingInventoryXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("OperatingUnitId=" + operatingUnitId);
            XPCollection collect = new XPCollection(session, typeof(SettingInventoryXpo), criteria);
            return collect;
        }

        /// <summary>
        /// Obtiene los parametros de inventario por unidad operativa
        /// </summary>
        /// <returns></returns>
        public XPCollection<SettingInventoryXpo> GetSettingInventoryByOperatingUnit(int operatingUnitId)
        {
            var session = new IndigoXPOSession<SettingInventoryXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("OperatingUnitId=" + operatingUnitId);
            XPCollection<SettingInventoryXpo> collect = new XPCollection<SettingInventoryXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de DashboardPharmacy por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryDashboardPharmacyReportXpo> ListViewDashboardPharmacyFilters(string Filter)
        {
            var session = new IndigoXPOSession<InventoryDashboardPharmacyReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryDashboardPharmacyReportXpo> collect = new XPCollection<InventoryDashboardPharmacyReportXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de DashboardPharmacyDevolution por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryDashboardPharmacyDevolutionReportXpo> ListViewDashboardPharmacyDevolutionFilters(string Filter)
        {
            var session = new IndigoXPOSession<InventoryDashboardPharmacyDevolutionReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryDashboardPharmacyDevolutionReportXpo> collect = new XPCollection<InventoryDashboardPharmacyDevolutionReportXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de PharmaceuticalDispensingDevolutionDeytail por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo> ListViewPharmaceuticalDispensingDevolutionDetailFilters(string Filter)
        {
            var session = new IndigoXPOSession<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo> collect = new XPCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de PharmaceuticalDispensingDevolution por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryPharmaceuticalViewDispensingDevolutionReportXpo> ListViewPharmaceuticalDispensingDevolutionFilters(string Filter)
        {
            var session = new IndigoXPOSession<InventoryPharmaceuticalViewDispensingDevolutionReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryPharmaceuticalViewDispensingDevolutionReportXpo> collect = new XPCollection<InventoryPharmaceuticalViewDispensingDevolutionReportXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de PharmaceuticalDispensing por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryPharmaceuticalViewDispensingReportXpo> ListViewPharmaceuticalDispensingFilters(string Filter)
        {
            var session = new IndigoXPOSession<InventoryPharmaceuticalViewDispensingReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryPharmaceuticalViewDispensingReportXpo> collect = new XPCollection<InventoryPharmaceuticalViewDispensingReportXpo>(session, criteria, null);
            return collect;
        }

        /// <summary>
        /// Lista reportes tirillas por medicamentos
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public XPCollection<ViewReportPharmaceuticalDispensingNeckBandPatientXpo> ListReportPharmaceuticalDispensingNeckBandPatient(string Filter)
        {
            var session = new IndigoXPOSession<ViewReportPharmaceuticalDispensingNeckBandPatientXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<ViewReportPharmaceuticalDispensingNeckBandPatientXpo> collect = new XPCollection<ViewReportPharmaceuticalDispensingNeckBandPatientXpo>(session, criteria, null);
            return collect;
        }

        /// <summary>
        /// lista todos los subdetalles de la remision de entrada por proveedor y linea de distribuccion
        /// </summary>
        /// <returns></returns>
        public XPCollection ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineId(int SupplierId, int SupplierDistributionLineId)
        {
            var session = new IndigoXPOSession<InventoryRemissionEntranceDetailBatchSerialXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("RemissionEntranceDetailId.RemissionEntranceId.SupplierId.Id =" + SupplierId + " AND RemissionEntranceDetailId.RemissionEntranceId.SupplierDistributionLineId.Id = " + SupplierDistributionLineId + " AND RemissionEntranceDetailId.RemissionEntranceId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(InventoryRemissionEntranceDetailBatchSerialXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los subdetalles de la remision de entrada por proveedor, linea de distribuccion y codigo de usuario que tenga permiso a los almacenes
        /// </summary>
        /// <returns></returns>
        public XPCollection ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineIdAndCodeUser(int SupplierId, int SupplierDistributionLineId, string codeUser)
        {
            var session = new IndigoXPOSession<InventoryRemissionEntranceDetailBatchSerialXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("RemissionEntranceDetailId.RemissionEntranceId.SupplierId.Id =" + SupplierId + " AND RemissionEntranceDetailId.RemissionEntranceId.SupplierDistributionLineId.Id = " + SupplierDistributionLineId + " AND RemissionEntranceDetailId.RemissionEntranceId.WarehouseId.Inventory_WarehouseUsers[UserCode = '" + codeUser + "'] AND RemissionEntranceDetailId.RemissionEntranceId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(InventoryRemissionEntranceDetailBatchSerialXpo), criteria);
            return collect;
        }

        public XPCollection ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineIdAndCodeUserAndWarehouseId(int SupplierId, int SupplierDistributionLineId, int WarehouseId)
        {
            var session = new IndigoXPOSession<InventoryRemissionEntranceDetailBatchSerialXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("RemissionEntranceDetailId.RemissionEntranceId.SupplierId.Id =" + SupplierId + " AND RemissionEntranceDetailId.RemissionEntranceId.SupplierDistributionLineId.Id = " + SupplierDistributionLineId + " AND RemissionEntranceDetailId.RemissionEntranceId.Status = 2 AND OutstandingQuantity > 0 and RemissionEntranceDetailId.RemissionEntranceId.WarehouseId.Id = " + WarehouseId);
            XPCollection collect = new XPCollection(session, typeof(InventoryRemissionEntranceDetailBatchSerialXpo), criteria);
            return collect;
        }

        public XPCollection ListConsignmentInventoryRemissionDetailBatchSerialBySupplierIdAndSupplierDistributionLineIdAndCodeUserAndWarehouseId(int SupplierId, int SupplierDistributionLineId, int WarehouseId)
        {
            var session = new IndigoXPOSession<ConsignmentInventoryRemissionDetailBatchSerialXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.SupplierId.Id =" + SupplierId + " AND ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.SupplierDistributionLineId.Id = " + SupplierDistributionLineId + " AND ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.Status = 2 AND OutstandingLegalizedQuantity > 0 and ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.WarehouseId.Id = " + WarehouseId);
            XPCollection collect = new XPCollection(session, typeof(ConsignmentInventoryRemissionDetailBatchSerialXpo), criteria);
            return collect;
        }

        public XPCollection ListConsignmentInventoryRemissionWithoutLegalize(int SupplierId, int SupplierDistributionLineId, int WarehouseId)
        {
            var session = new IndigoXPOSession<ViewConsignmentInventoryRemissionWithoutLegalizeXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("SupplierId =" + SupplierId + " AND SupplierDistributionLineId = " + SupplierDistributionLineId + " AND WarehouseId = " + WarehouseId + " AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(ViewConsignmentInventoryRemissionWithoutLegalizeXpo), criteria);
            return collect;
        }

        public XPCollection LoadPurchaseRequestDetailToOrder()
        {
            var session = new IndigoXPOSession<ViewPurchaseRequestToOrderXpo>();
            XPCollection collect = new XPCollection(session, typeof(ViewPurchaseRequestToOrderXpo), null);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles del contrato por proveedor y linea de distribuccion
        /// </summary>
        /// <returns></returns>
        public XPCollection ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(int SupplierId, int SupplierDistributionLineId)
        {
            var session = new IndigoXPOSession<InventoryContractDetailXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("InventoryContractId.SupplierId.Id =" + SupplierId + " AND InventoryContractId.SupplierDistributionLineId = " + SupplierDistributionLineId + " AND InventoryContractId.Status = 4 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(InventoryContractDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de la orden de compra por proveedor y linea de distribuccion
        /// </summary>
        /// <returns></returns>
        public XPCollection ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(int SupplierId, int SupplierDistributionLineId)
        {
            var session = new IndigoXPOSession<InventoryPurcharseOrderDetailXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("PurchaseOrderId.SupplierId.Id =" + SupplierId + " AND PurchaseOrderId.SupplierDistributionLineId = " + SupplierDistributionLineId + " AND PurchaseOrderId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(InventoryPurcharseOrderDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de la orden de compra por proveedor y linea de distribuccion y coidgo de usuario que tenga permiso a los almacenes
        /// </summary>
        /// <returns></returns>
        public XPCollection ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineIdAndCodeUser(int SupplierId, int SupplierDistributionLineId, string CodeUser)
        {
            var session = new IndigoXPOSession<InventoryPurchaseOrderDetailReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("PurchaseOrderId.SupplierId.Id =" + SupplierId + " AND PurchaseOrderId.SupplierDistributionLineId = " + SupplierDistributionLineId + " AND PurchaseOrderId.WarehouseId.InventoryWarehouseUserReportXpo[UserCode = '" + CodeUser + "'] AND PurchaseOrderId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(InventoryPurchaseOrderDetailReportXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de la orden de compra por proveedor y linea de distribuccion y coidgo de usuario que tenga permiso a los almacenes
        /// </summary>
        /// <returns></returns>
        public XPCollection ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineIdAndWarehouse(int SupplierId, int SupplierDistributionLineId, int warehouseId)
        {
            var session = new IndigoXPOSession<InventoryPurchaseOrderDetailReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("PurchaseOrderId.SupplierId.Id =" + SupplierId + " AND PurchaseOrderId.SupplierDistributionLineId = " + SupplierDistributionLineId + " AND PurchaseOrderId.WarehouseId.Id = " + warehouseId + " AND PurchaseOrderId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(InventoryPurchaseOrderDetailReportXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de la orden de compra por proveedor y linea de distribuccion y coidgo de usuario que tenga permiso a los almacenes
        /// </summary>
        /// <returns></returns>
        public XPCollection ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineIdAndWarehouseAndWarehouseConsignment(int SupplierId, int SupplierDistributionLineId, int warehouseId, Boolean warehouseConsignment)
        {
            var session = new IndigoXPOSession<InventoryPurchaseOrderDetailReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("PurchaseOrderId.SupplierId.Id =" + SupplierId + " AND PurchaseOrderId.SupplierDistributionLineId = " + SupplierDistributionLineId + " AND PurchaseOrderId.WarehouseId.Id = " + warehouseId + " AND PurchaseOrderId.WarehouseId.WarehouseConsignment = ? AND PurchaseOrderId.Status = 2 AND OutstandingQuantity > 0", warehouseConsignment);
            XPCollection collect = new XPCollection(session, typeof(InventoryPurchaseOrderDetailReportXpo), criteria);
            return collect;
        }

        public XPCollection<InventoryPurcharseOrderDetailXpo> ListPurchaseOrderDetailBySupplierId(int SupplierId)
        {
            var session = new IndigoXPOSession<InventoryPurcharseOrderDetailXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("PurchaseOrderId.SupplierId.Id =" + SupplierId + " AND PurchaseOrderId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection<InventoryPurcharseOrderDetailXpo> collect = new XPCollection<InventoryPurcharseOrderDetailXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de las solicitudes por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho
        /// </summary>
        /// <returns></returns>
        public XPCollection ListRequestDetailByFilterFunctionalUnitWarehouseAndOrderTypeAndDispatchTo(int filterFunctionalUnitWarehouse, byte orderType, byte dispatchTo)
        {
            var session = new IndigoXPOSession<InventoryRequestDetailXpo>();
            CriteriaOperator criteria = null;
            if (orderType == 1 || orderType == 2 && dispatchTo == 1)
            {
                criteria = CriteriaOperator.Parse("InventoryRequestId.TargetWarehouseId =" + filterFunctionalUnitWarehouse + " AND InventoryRequestId.Status = 2 AND OutstandingQuantity > 0");
            }
            if (orderType == 2 && dispatchTo == 2)
            {
                criteria = CriteriaOperator.Parse("InventoryRequestId.TargetFunctionalUnitId =" + filterFunctionalUnitWarehouse + " AND InventoryRequestId.Status = 2 AND OutstandingQuantity > 0");
            }
            XPCollection collect = new XPCollection(session, typeof(InventoryRequestDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de las solicitudes que necesitan autorizacion
        /// </summary>
        /// <returns></returns>
        public XPCollection ListRequestDetailByOperatingUnitAndType(string OperatingUnitIds, string Type)
        {
            var session = new IndigoXPOSession<ViewListRequestDetailUnauthorizedXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("OperatingUnitId IN(" + OperatingUnitIds + ") AND Status = 2 AND RequestType IN (" + Type + ")  AND Quantity = 0 And OutstandingQuantity = 0");
            var serverMode = new XPCollection(session, typeof(ViewListRequestDetailUnauthorizedXpo), criteria);
            return serverMode;
        }

        /// <summary>
        /// lista todos los detalles de las solicitudes por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho y tambien de la sección
        /// </summary>
        /// <returns></returns>
        public XPCollection ListViewRequestDetailFiltered(int filterFunctionalUnitWarehouse, byte orderType, byte dispatchTo)
        {
            var session = new IndigoXPOSession<ViewListRequestDetailXpo>();
            CriteriaOperator criteria = null;
            if (orderType == 1 || orderType == 2 && dispatchTo == 1)
            {
                criteria = CriteriaOperator.Parse("TargetWarehouseId =" + filterFunctionalUnitWarehouse + " AND Status = 2 AND OutstandingQuantity > 0");
            }
            if (orderType == 2 && dispatchTo == 2)
            {
                criteria = CriteriaOperator.Parse("TargetFunctionalUnitId =" + filterFunctionalUnitWarehouse + " AND Status = 2 AND OutstandingQuantity > 0");
            }
            XPCollection collect = new XPCollection(session, typeof(ViewListRequestDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de las solicitudes por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho y tambien de la sección
        /// </summary>
        /// <returns></returns>
        public XPCollection ListViewRequestDetail(int filterFunctionalUnitWarehouse, byte orderType, byte dispatchTo)
        {
            var session = new IndigoXPOSession<ViewListRequestDetailImportXpo>();
            CriteriaOperator criteria = null;
            if (orderType == 1 || orderType == 2 && dispatchTo == 1)
            {
                 criteria = CriteriaOperator.Parse("TargetWarehouseId =" + filterFunctionalUnitWarehouse + " AND Status = 2 AND OutstandingQuantity > 0");
            }
            if (orderType == 2 && dispatchTo == 2)
            {
                 criteria = CriteriaOperator.Parse("TargetFunctionalUnitId =" + filterFunctionalUnitWarehouse + " AND Status = 2 AND OutstandingQuantity > 0");
            }
            XPCollection collect = new XPCollection(session, typeof(ViewListRequestDetailImportXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de las solicitudes por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho y tambien de la sección
        /// </summary>
        /// <returns></returns>
        public XPCollection ListViewRequestDetailByWarehouse(int warehouseId)
        {
            var session = new IndigoXPOSession<ViewListRequestDetailImportXpo>();
            CriteriaOperator criteria = null;
            criteria = CriteriaOperator.Parse("SourceWarehouseId =" + warehouseId + " AND Status = 2  AND MovementType = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(session, typeof(ViewListRequestDetailImportXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista el detalle de las solicitudes por id por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho y tambien de la sección
        /// </summary>
        /// <returns></returns>
        public XPCollection ListViewRequestDetailEntityId(int Row)
        {
            var session = new IndigoXPOSession<ViewListRequestDetailImportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Row =" + Row);          
            return new XPCollection(session, typeof(ViewListRequestDetailImportXpo), criteria);      
        }


        public XPCollection<InventoryProductXpo> ListInventoryProductFrmStock()
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            XPCollection<InventoryProductXpo> collect = new XPCollection<InventoryProductXpo>(session);
            return collect;
        }

        /// <summary>
        /// lista los inventarios fisicios filtrados por id del almacen
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPCollection<PhysicalInventoryXpo> ListPhysicalInventoryByWarehouseIdFrmStock(int WareHouseId)
        {
            var session = new IndigoXPOSession<PhysicalInventoryXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("WarehouseId=" + WareHouseId);
            XPCollection<PhysicalInventoryXpo> collect = new XPCollection<PhysicalInventoryXpo>(session, criteria);

            return collect;
        }

        /// <summary>
        /// lista el almacen por id del Almacen
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPhysicalInventoryByWarehouseId(int WareHouseId)
        {
            var session = new IndigoXPOSession<PhysicalInventoryXpo>();
            var classEntity = session.GetClassInfo(typeof(PhysicalInventoryXpo));
            CriteriaOperator criteria = CriteriaOperator.Parse("WarehouseId=" + WareHouseId);
            var serverMode = new XPInstantFeedbackSource(classEntity, "Id;WarehouseId.CodeName;ProductId.Code;ProductId.Name;ProductId.CodeName;ProductId.Presentation;BatchSerialId.BatchCode;BatchSerialId.ExpirationDate;Quantity", criteria);
            XPCollection<PhysicalInventoryXpo> collect = new XPCollection<PhysicalInventoryXpo>(session, criteria);

            return serverMode;
        }

        /// <summary>
        /// lista inventario fisico de custodia por control de ingreso y por id del Almacen
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPhysicalInventoryCustodyByWarehouseId(string AdmissionNumber, int WareHouseId)
        {
            var session = new IndigoXPOSession<PhysicalInventoryCustodyXpo>();
            var classEntity = session.GetClassInfo(typeof(PhysicalInventoryCustodyXpo));
            CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber='" + AdmissionNumber + "' AND WarehouseId=" + WareHouseId);
            var serverMode = new XPInstantFeedbackSource(classEntity, "Id;WarehouseId.CodeName;ProductId.Code;ProductId.Name;ProductId.CodeName;ProductId.Presentation;BatchSerialId.BatchCode;BatchSerialId.ExpirationDate;Quantity", criteria);
            XPCollection<PhysicalInventoryCustodyXpo> collect = new XPCollection<PhysicalInventoryCustodyXpo>(session, criteria);

            return serverMode;
        }

        /// <summary>
        /// lista inventario fisico de custodia por control de ingreso y por id del Almacen
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPhysicalInventoryCustodyByWarehouseIdPatientCodeAdmission(int warehouseId, string patientCode, string admission)
        {
            var session = new IndigoXPOSession<PhysicalInventoryCustodyXpo>();
            var classEntity = session.GetClassInfo(typeof(PhysicalInventoryCustodyXpo));
            CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber='" + admission + "' AND WarehouseId=" + warehouseId);
            var serverMode = new XPInstantFeedbackSource(classEntity, "Id;WarehouseId.CodeName;ProductId.CodeName;ProductId.Presentation;BatchSerialId.BatchCode;Quantity", criteria);
            XPCollection<PhysicalInventoryCustodyXpo> collect = new XPCollection<PhysicalInventoryCustodyXpo>(session, criteria);

            return serverMode;
        }

        /// <summary>
        /// Lista todos los cubrimientos de productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListProductTemplate()
        {
            var session = new IndigoXPOSession<ProductRateXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(ProductRateXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todos los cubrimientos de productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListProductTemplateByStatus(bool status)
        {
            var session = new IndigoXPOSession<ProductRateXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(ProductRateXpo));
                CriteriaOperator criteria = CriteriaOperator.Parse("Status = " + status);
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las patologías
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource LostPOSPathologies()
        {
            var session = new IndigoXPOSession<POSPathologiesXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(POSPathologiesXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id,ProductId;DiagnosticId.Id;DiagnosticId.CodeName;DiagnosticId.Code;DiagnosticId.Name", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los diagnosticos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListDiagnostic()
        {
            var session = new IndigoXPOSession<DiagnosticXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(DiagnosticXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;CodeName;Code;Name", null);
                return serverMode;
            }
        }

        /// <summary>
        /// retornna las remisiones de entrada
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPharmaceuticalDispensingTransfer()
        {
            var session = new IndigoXPOSession<PharmaceuticalDispensingTransferXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(PharmaceuticalDispensingTransferXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;AdmissionNumber;AdmissionNumberDestination;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los diagnosticos
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPCollection<DiagnosticXpo> ListDiagnosticById(int id)
        {
            var session = new IndigoXPOSession<DiagnosticXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Id=" + id);
            XPCollection<DiagnosticXpo> collect = new XPCollection<DiagnosticXpo>(session, criteria);

            return collect;
        }

        /// <summary>
        /// lista los productos con cantidad para devolver
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        public XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo> ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumberXpcollection(string admissionNumber)
        {
            var session = new IndigoXPOSession<PharmaceuticalDispensingDetailBatchSerialXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("OutstandingQuantity > 0 AND PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.AdmissionNumber ='" + admissionNumber + "' and PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Status = 2");
            var classEntity = session.GetClassInfo(typeof(PharmaceuticalDispensingDetailBatchSerialXpo));
            XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo> collect = new XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo>(session, criteria);

            return collect;
        }

        /// <summary>
        /// lista los productos con cantidad para devolver
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        public XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo> ListPharmaceuticalDispensingDetailBatchSerialByWarehouseAndAdmissionNumber(int? warehouseId, string admissionNumber)
        {
           string FilterWarehouse = string.Empty;
           if (warehouseId != null)
            {
                FilterWarehouse = $"PharmaceuticalDispensingDetailId.WarehouseId = {warehouseId} AND";
            }

            string query = String.Format("OutstandingQuantity > 0  AND {0} PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.AdmissionNumber = '{1}' AND PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Status = 2", FilterWarehouse, admissionNumber);
            var session = new IndigoXPOSession<PharmaceuticalDispensingDetailBatchSerialXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(query);
            var classEntity = session.GetClassInfo(typeof(PharmaceuticalDispensingDetailBatchSerialXpo));
            XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo> collect = new XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo>(session, criteria);

            return collect;
        }

        /// <summary>
        /// Lista los productos por clase del tipo de producto
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByProductType(int ClassProductType, int? ATCId= null, bool ProductNPT = false)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                string Filter = string.Empty;

                if (ProductNPT) {
                    Filter += string.Format("AND ATCId.ProductNPT = 1");
                }

                if (ATCId != null)
                {
                    Filter += string.Format("AND ATCId.Id={0}", ATCId);
                }
                CriteriaOperator criteria = CriteriaOperator.Parse(string.Format("ProductTypeId.Class={0} {1}", ClassProductType, Filter));

                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;ProductTypeId.CodeName;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los rangos de temperatura
        /// </summary>
        public XPInstantFeedbackSource ListStorageTemperature()
        {
            var session = new IndigoXPOSession<StorageTemperatureXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(StorageTemperatureXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;From;Until;TemperatureUnit;Description", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los productos por clase del tipo de producto según Parametros de Solicitudes
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByIds(List<int> ids)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(String.Format("Id IN ({0})", string.Join(",", ids.ToArray())));
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        public XPInstantFeedbackSource ListProductNPT()
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ATCId.ProductNPT= 1");
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;ProductTypeId.CodeName;CodeName", criteria);
                return serverMode;
            }          
        }

        public XPInstantFeedbackSource ListInventoryProductByProductTypeClasses(List<string> ProductTypeClasses)
        {
            string InCriteria = String.Join(",", ProductTypeClasses.ToArray());
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class in (" + InCriteria + ")");
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los productos en los cuales la clase del tipo sea distinto al deseado
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByNoClassType(int ClassProductType)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class!=" + ClassProductType + "");
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los productos por estado y en los cuales la clase del tipo sea distinto al deseado
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByStatusByNoClassType(bool status, int ClassProductType)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class!=" + ClassProductType + "and Status=" + status);
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los productos de un atc por estado y en los cuales la clase del tipo sea distinto al deseado
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByATC(bool status, int ClassProductType, int atcId)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class!=" + ClassProductType + " and Status=" + status + " and ATCId.Id=" + atcId);
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los productos de un supply por estado y en los cuales la clase del tipo sea distinto al deseado
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductBySupply(bool status, int ClassProductType, int supplyId)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class!=" + ClassProductType + " and Status=" + status + " and SupplieId.Id=" + supplyId);
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los productos por almacen, estado, que no tengan cantidad y en los cuales la clase del tipo sea distinto al deseado
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByStatusByNoClassTypeByAlmacen(bool status, int[] ClassProductType, int IdAlmacen, bool virtualStore = false)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = null;
                if (virtualStore == false) //Si el almacen no es virtual
                {
                    criteria = CriteriaOperator.Parse("not ProductTypeId.Class in (" + string.Join(",", ClassProductType.ToArray()) + ") and Status=" + status + " and PhysicalInventoryXpo[Quantity > 0 and WarehouseId.Id = " + IdAlmacen + " ]");
                }
                else //Si el almacen si es virtual
                {
                    criteria = CriteriaOperator.Parse("not ProductTypeId.Class in (" + string.Join(",", ClassProductType.ToArray()) + ") and Status=" + status + "");
                }
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ClassProductType"></param>
        /// <param name="IdAlmacen"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByStatusByNoClassTypeByAlmacenNoAffectedInventory(bool status, int ClassProductType, int IdAlmacen, bool virtualStore = false, List<int> ListProductsIds  = null)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = null;
                if (virtualStore == false) //Si el almacen no es virtual
                {
                    criteria = CriteriaOperator.Parse("ProductTypeId.Class != " + ClassProductType + " and Status=" + status + " and PhysicalInventoryXpo[Quantity >= 0 and WarehouseId.Id = " + IdAlmacen + " ]");
                }
                else //Si el almacen si es virtual
                {
                    string ProductIds = string.Empty;
                    if (ListProductsIds != null) { ProductIds =$" AND Id in ({string.Join(",", ListProductsIds)})"; }
                    criteria = CriteriaOperator.Parse($"ProductTypeId.Class != {ClassProductType} and Status={status} {ProductIds}");
                }
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Devuelve la entidad previamente enviada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Fun"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public List<T> GetCollection<T>(Func<T, bool> Fun = null, string criteria = null, bool withSort = true)
        {
            dynamic result = this.LoadCollection<T>(XpoDefault.DataLayer, Fun, criteria, withSort: withSort);
            result.Sort();
            return result;
        }

        /// <summary>
        /// Devuelve la entidad previamente enviada
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Fun"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<List<T>> GetCollectionAsync<T>(Func<T, bool> Fun = null, string criteria = null, bool withSort = true)
        {
            dynamic result = null;

            await Task.Factory.StartNew(() =>
            {
                result = this.LoadCollection<T>(XpoDefault.DataLayer, Fun, criteria, withSort: withSort);
                result.Sort();
            });
            
            return result;
        }

        /// <summary>
        /// Lista todos los productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProduct(bool Status = false)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = null;
                if (Status) {
                    criteria = CriteriaOperator.Parse("Status=" + Status + "");
                }
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ATCId.Code;SupplieId.Code;Name;ClassName;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todos los productos por Id
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductsById(List<int> Productsids)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(String.Format("Id IN ({0})", string.Join(",", Productsids.ToArray())));
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }


        /// <summary>
        /// Lista todos los productos por estado
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryProductXpo> ListInventoryProductByStatus(bool status)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Status = '" + status + "'");
            XPCollection<InventoryProductXpo> collect = new XPCollection<InventoryProductXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// Lista todos los productos por estado
        /// </summary>
        /// <returns></returns>
        public XPCollection ListInventoryProductByStatusCollection(bool status)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Status = '" + status + "'");
            XPCollection collect = new XPCollection(session,typeof(InventoryProductXpo), criteria);
            return collect;
        }

        /// <summary>
        /// Lista todos los subgrupos de productos por estado activo
        /// </summary>
        /// <returns></returns>
        public XPCollection ListInventoryProductSubGroupByStatusCollection(bool status)
        {
            var session = new IndigoXPOSession<ProductSubGroupXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Status = '" + status + "'");
            XPCollection collect = new XPCollection(session, typeof(ProductSubGroupXpo), criteria);
            return collect;
        }

        /// <summary>
        /// Lista todos los grupos de productos por estado
        /// </summary>
        /// <returns></returns>
        public XPCollection ListInventoryProductGroupByStatusCollection(bool status)
        {
            var session = new IndigoXPOSession<ProductGroupXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Status = '" + status + "'");
            XPCollection collect = new XPCollection(session, typeof(ProductGroupXpo), criteria);
            return collect;
        }

        public XPInstantFeedbackSource ListInventoryProductByATCCode(string atcCode)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("ATCId.Code = '" + atcCode + "'");
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ATCId.Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;CodeName", criteria);
                return serverMode;
            }
        }

        public XPInstantFeedbackSource ListInventoryProductByATCId(int atcId)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("ATCId.Id = " + atcId);
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ATCId.Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los productos en los cuales la clase del tipo sea el deseado y los productos que manejen lote
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByClassTypeAndHandlesBatch(bool status, int ClassProductType)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class <>" + ClassProductType + "and Status=" + status + " AND ProductSubGroupId.HandlesBatch = 1 AND ProductSubGroupId.HandlesExpiry = 1");
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todos los ATC
        /// </summary>
        public XPInstantFeedbackSource ListATC()
        {
            var session = new IndigoXPOSession<ATCXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(ATCXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los ATC por filtro
        /// </summary>
        public XPInstantFeedbackSource ListATCbyFilter( string filter = "")
        {
            var session = new IndigoXPOSession<ATCXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse((string.IsNullOrEmpty(filter))?null:filter);
                var classEntity = session.GetClassInfo(typeof(ATCXpo));
                var serverMode = new XPInstantFeedbackSource( classEntity, null, criteria);
                return serverMode;
            }
        }


        /// <summary>
        /// Lista los ATC por filtro
        /// </summary>
        public XPInstantFeedbackSource ListInventoryProductByFilter(string filter = "")
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse((string.IsNullOrEmpty(filter)) ? null : filter);
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista medicamentos de activos
        /// </summary>
        /// <returns></returns>
        public XPCollection<ATCXpo> ListATCActivos()
        {
            var session = new IndigoXPOSession<ATCXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Status = 1");
            XPCollection<ATCXpo> collect = new XPCollection<ATCXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// Lista medicamentos de tipo antibiotico
        /// </summary>
        /// <returns></returns>
        public XPCollection<ATCXpo> CollectionATCAntibiotic()
        {
            var session = new IndigoXPOSession<ATCXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Antibiotic = 1");
            XPCollection<ATCXpo> collect = new XPCollection<ATCXpo>(session, criteria);
            return collect;
        }

        /// <summary>
        /// Lista todos los insumos
        /// </summary>
        public XPInstantFeedbackSource ListInventorySupplie()
        {
            var session = new IndigoXPOSession<InventorySupplieXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventorySupplieXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todos los insumos por Ids
        /// </summary>
        public XPInstantFeedbackSource ListInventorySupplieByIds(List<int> ids)
        {
            var session = new IndigoXPOSession<InventorySupplieXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse(String.Format("Id IN ({0})", string.Join(",", ids.ToArray())));
                var classEntity = session.GetClassInfo(typeof(InventorySupplieXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lists the billing group.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListBillingGroup()
        {
            var session = new IndigoXPOSession<BillingGroupXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(BillingGroupXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity);
                return serverMode;
            }
        }

        /// <summary>
        /// Lists the general ledger iva.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListGeneralLedgerIva()
        {
            var session = new IndigoXPOSession<GeneralLedgerIVAXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(GeneralLedgerIVAXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity);
                return serverMode;
            }
        }

        /// <summary>
        /// Lists the general ledger iva.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListGeneralLedgerIvaByStatus(bool Status)
        {
            var session = new IndigoXPOSession<GeneralLedgerIVAXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + Status + "");
                var classEntity = session.GetClassInfo(typeof(GeneralLedgerIVAXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lists the packaging unit.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPackagingUnit()
        {
            var session = new IndigoXPOSession<PackagingUnitXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(PackagingUnitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los grupos
        /// </summary>
        public XPInstantFeedbackSource ListProductGroup()
        {
            var session = new IndigoXPOSession<ProductGroupXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(ProductGroupXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;GroupClassName;SubclassCodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los subgrupos
        /// </summary>
        public XPInstantFeedbackSource ListProductSubGroup()
        {
            var session = new IndigoXPOSession<ProductSubGroupXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(ProductSubGroupXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las unidades de medida
        /// </summary>
        public XPInstantFeedbackSource ListMeasureUnit()
        {
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(MeasureUnitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;Abbreviation", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las unidades de medida para sustancias combinadas
        /// </summary>
        public List<MeasureUnitXpo> ListMeasureUnitSubstance()
        {
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            XPCollection<MeasureUnitXpo> serverMode = new XPCollection<MeasureUnitXpo>(session);
            return serverMode.ToList();
        }

        /// <summary>
        /// Obtiene una unidad de medida por el id
        /// </summary>
        /// <param name="MeasureUnitId"></param>
        /// <returns></returns>
        public MeasureUnitXpo MeasureUnitById(int MeasureUnitId)
        {
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Id=" + MeasureUnitId.ToString());
                var serverMode = new XPCollection<MeasureUnitXpo>(session, criteria);
                return serverMode.FirstOrDefault();
            }
        }

        /// <summary>
        /// lista la unidad de medida por el id
        /// </summary>
        /// <param name="MeasureUnitId"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListMeasureUnitById(int MeasureUnitId)
        {

            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Id=" + MeasureUnitId.ToString());
                var classEntity = session.GetClassInfo(typeof(MeasureUnitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las unidades de medida
        /// </summary>
        public XPInstantFeedbackSource ListMeasureUnitByType(byte unitType)
        {
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse($"Status = true And UnitType={ unitType}");

                var classEntity = session.GetClassInfo(typeof(MeasureUnitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;UnitType;CodeName;Abbreviation", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las unidades de medidas, de los que pasen como filtro
        /// </summary>
        public XPInstantFeedbackSource ListMeasureUnitByTypes(List<int> unitTypes)
        {
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {
                var filter = String.Format("Status = true");
                if (unitTypes != null && unitTypes.Any())
                {
                    filter += String.Format(" AND UnitType in (" + string.Join(",", unitTypes.ToArray()) + ")");
                }
                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(MeasureUnitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;UnitType;CodeName;Abbreviation", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las unidades de medida por tipo y el nombre contenga la palabra de texto
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="texto"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListMeasureUnitByTypeName(byte unitType, string texto)
        {
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {

                CriteriaOperator criteria1 = CriteriaOperator.Parse("UnitType=" + unitType);
                CriteriaOperator criteria = CriteriaOperator.And(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty("Name"), texto),
                    criteria1);

                var classEntity = session.GetClassInfo(typeof(MeasureUnitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;UnitType;CodeName;Abbreviation", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas las unidades de medida diferentes al tipo enviado
        /// </summary>
        public XPInstantFeedbackSource ListNotMeasureUnitByType(byte unitType)
        {
            var session = new IndigoXPOSession<MeasureUnitXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("UnitType<>" + unitType);
                var classEntity = session.GetClassInfo(typeof(MeasureUnitXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;UnitType;CodeName;Abbreviation", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los almacenes
        /// </summary>
        public XPInstantFeedbackSource ListWarehouse()
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los almacenes excepto de custodia
        /// </summary>
        public XPInstantFeedbackSource ListWarehouseNoCustody()
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("CustodyStore = 0");
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes propios por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListOwnWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 0)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los almacenes activos
        /// </summary>
        public XPInstantFeedbackSource ListAllActiveWarehouse(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes propios por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListOwnWarehouseByStatusAndUserAndProduct(bool status, string userCode, List<int> listwarehouse)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND ((VirtualStore = 0 AND WarehouseConsignment = 1 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 0) ", userCode);
                filter += String.Format(" OR (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 0 AND WareHouseType =0)) ", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }
                if (listwarehouse != null && listwarehouse.Any())
                {
                    filter += String.Format(" AND Id in (" + string.Join(",", listwarehouse.ToArray()) + ")");
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes por Id
        /// </summary>
        public XPInstantFeedbackSource ListWarehouseByIdAndUser(string userCode, List<int> listwarehouse)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = 1");

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }
                if (listwarehouse != null && listwarehouse.Any())
                {
                    filter += String.Format(" AND Id in (" + string.Join(",", listwarehouse.ToArray()) + ")");
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }


        /// <summary>
        /// Lista almacenes propios y de consignación por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListOwnAndConsignmentWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 0)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes propios y de control por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListOwnAndControlWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = $"Status = {status} AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0)";
                
                if (!string.IsNullOrEmpty(userCode))
                {
                    filter += $" AND Inventory_WarehouseUsers[UserCode = '{userCode}']";
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes propios y de control por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListWareHouseByOrderType(byte orderType, bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = $"Status = {status} AND (VirtualStore = 0 AND CustodyStore = 0 AND TransitStore = 0)";

                if (orderType != 2)
                {
                    filter += " AND WarehouseConsignment = 0 AND ControlStore = 0 ";
                }

                if (!string.IsNullOrEmpty(userCode))
                {
                    filter += $" AND Inventory_WarehouseUsers[UserCode = '{userCode}']";
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;WarehouseConsignment", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes todos los almacenes por estado y usuario con permiso, excluyendo los almacenes virtuales
        /// </summary>
        public XPInstantFeedbackSource ListNoVirtualWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes todos los almacenes por estado y usuario con permiso, excluyendo los almacenes virtuales
        /// </summary>
        public XPInstantFeedbackSource ListWarehouseByUserCodeAndCareCenterCode(bool status, string userCode, string careCenterCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0)", userCode);

                filter += String.Format(" AND (CodeCenterAttention = '{0}')", careCenterCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes todos los almacenes por estado y usuario con permiso, excluyendo los almacenes de transito
        /// </summary>
        public XPInstantFeedbackSource ListNoTransitWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (TransitStore = 0)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes todos los almacenes por estado y usuario con permiso, excluyendo los almacenes de transito
        /// </summary>
        public XPCollection<WarehouseXpo> ListCollectionNoTransitWarehouseByStatusAndWarehouseAndUser(bool status, string warehouseCode, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (TransitStore = 0)", userCode);

                if (!String.IsNullOrEmpty(warehouseCode))
                {
                    filter += String.Format(" AND Code = '{0}'", warehouseCode);
                }

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                return new XPCollection<WarehouseXpo>(session, criteria);
            }
        }

        /// <summary>
        /// Lista almacenes virtuales por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListVirtualWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 1 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 0)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes en consignación por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListConsignmentWarehouseByStatusAndUser(bool status, int? supplierId, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0 AND WarehouseConsignment = 1 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 0)", userCode);

                if (supplierId != null)
                {
                    filter += String.Format(" AND SupplierId = {0}", supplierId);
                }

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes en custodia por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListCustodyWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 1 AND TransitStore = 0 AND ControlStore = 0)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes de transito por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListTransitWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 1 AND ControlStore = 0)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes de control por estado y usuario con permiso
        /// </summary>
        public XPInstantFeedbackSource ListControlWarehouseByStatusAndUser(bool status, string userCode)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = {0}", status);

                filter += String.Format(" AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 1)", userCode);

                if (!String.IsNullOrEmpty(userCode))
                {
                    filter += String.Format(" AND Inventory_WarehouseUsers[UserCode = '{0}']", userCode);
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes de control 
        /// </summary>
        public XPInstantFeedbackSource ListControlWarehouse()
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = 1 AND (VirtualStore = 0 AND WarehouseConsignment = 0 AND CustodyStore = 0 AND TransitStore = 0 AND ControlStore = 1)");
                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes filtrado por tipo
        /// </summary>
        public XPInstantFeedbackSource ListWarehousebyType(byte Type)
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = 1 AND WareHouseType ={0}", Type);
                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista almacenes para Asignar En el Dasboard de Calidad por estado excluyendo (Virtuales - control - Asignación)
        /// </summary>
        public XPInstantFeedbackSource ListAssignWarehouseDashboardQualityControl()
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                var filter = String.Format("Status = 1 AND VirtualStore = 0 AND WarehouseConsignment = 0 AND ControlStore = 0");

                CriteriaOperator criteria = CriteriaOperator.Parse(filter);
                var classEntity = session.GetClassInfo(typeof(WarehouseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Consulta una via de administracion por id
        /// </summary>
        /// <param name="administrationRouteId"></param>
        /// <returns></returns>
        public AdministrationRouteXpo GetAdministrationRouteById(int administrationRouteId)
        {
            var session = new IndigoXPOSession<AdministrationRouteXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Id = " + administrationRouteId);
            AdministrationRouteXpo administrationRoute = new XPCollection<AdministrationRouteXpo>(session, criteria).FirstOrDefault();
            return administrationRoute;
        }

        /// <summary>
        /// Lista todas los grupos farmacologicos
        /// </summary>
        public XPInstantFeedbackSource ListPharmacologicalGroup()
        {
            var session = new IndigoXPOSession<PharmacologicalGroupXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(PharmacologicalGroupXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los conceptos de movimiento
        /// </summary>
        public XPInstantFeedbackSource ListAdjustmentConcept()
        {
            var session = new IndigoXPOSession<AdjustmentConceptXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(AdjustmentConceptXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;ConceptType;ConceptTypeName;MovementClassName;MovementClass", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los conceptos de ajuste
        /// </summary>
        public XPInstantFeedbackSource ListAdjustmentConceptByType(byte Type)
        {
            var session = new IndigoXPOSession<AdjustmentConceptXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ConceptType = " + Type);
                var classEntity = session.GetClassInfo(typeof(AdjustmentConceptXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;ConceptType;ConceptTypeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todas los fabricantes
        /// </summary>
        public XPInstantFeedbackSource ListManufacturers()
        {
            var session = new IndigoXPOSession<ManufacturersXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(ManufacturersXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista otras deducciones o retenciones
        /// </summary>
        public XPInstantFeedbackSource ListOtherWitholdingDeductions()
        {
            var session = new IndigoXPOSession<OtherWithholdingDeductionsXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(OtherWithholdingDeductionsXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Status;Type;TypeName;RoundType;RoundTypeName;AccountPayableConceptId.CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los tipos de producto
        /// </summary>
        public XPInstantFeedbackSource ListProductType()
        {
            var session = new IndigoXPOSession<ProductTypeXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(ProductTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;Class;ClassName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los tipos de producto
        /// </summary>
        public XPInstantFeedbackSource ListProductTypeActiveMedicaInsumo()
        {
            var session = new IndigoXPOSession<ProductTypeXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=1 And Class In (2,3)");
                var classEntity = session.GetClassInfo(typeof(ProductTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;Class;ClassName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los tipos de producto por estado
        /// </summary>
        public XPInstantFeedbackSource ListProductTypeByStatus(bool status)
        {
            var session = new IndigoXPOSession<ProductTypeXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(ProductTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;Class;ClassName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los atributos de tipos de producto
        /// </summary>
        public XPInstantFeedbackSource ListAttributeProductType()
        {
            var session = new IndigoXPOSession<AttributeProductTypeXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(AttributeProductTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;ProductTypeId;ProductTypeId.CodeName;DataType;DataTypeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista todos los DCI
        /// </summary>
        public XPInstantFeedbackSource ListDCI(bool combined = true)
        {
            var session = new IndigoXPOSession<DCIXpo>();
            {
                CriteriaOperator criteria = null;
                if (combined == false) {
                    criteria = CriteriaOperator.Parse("Status = true And Combined = " + combined + "");
                }
                var classEntity = session.GetClassInfo(typeof(DCIXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;CombinedName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los DCI por estado
        /// </summary>
        public XPInstantFeedbackSource ListDCIByStatus(bool status)
        {
            var session = new IndigoXPOSession<DCIXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(DCIXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las formas farmaceuticas
        /// </summary>
        public XPInstantFeedbackSource ListPharmaceuticalForm()
        {
            var session = new IndigoXPOSession<PharmaceuticalFormXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(PharmaceuticalFormXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las formas farmaceuticas por estado
        /// </summary>
        public XPInstantFeedbackSource ListPharmaceuticalFormByStatus(bool status)
        {
            var session = new IndigoXPOSession<PharmaceuticalFormXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(PharmaceuticalFormXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las vias de administracion
        /// </summary>
        public XPInstantFeedbackSource ListAdministrationRoute()
        {
            var session = new IndigoXPOSession<AdministrationRouteXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(AdministrationRouteXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;PharmaceuticalFormId.CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las vias de administracion
        /// </summary>
        public XPInstantFeedbackSource ListAdministrationRouteByStatus(bool status)
        {
            var session = new IndigoXPOSession<AdministrationRouteXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(AdministrationRouteXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;PharmaceuticalFormId.CodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las vias de administracion por id de medicamento
        /// </summary>
        public XPInstantFeedbackSource ListAdministrationRouteByAtcId(int atcId)
        {
            var session = new IndigoXPOSession<ViewATCAdministrationRouteXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("ATCId=" + atcId.ToString());
                var classEntity = session.GetClassInfo(typeof(ViewATCAdministrationRouteXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;ATCId;AdministrationRouteCode;AdministrationRouteName;AdministrationRouteId;PharmaceuticalFormId;AdministrationRouteCodeName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista las vias de administracion por id de medicamento
        /// </summary>
        public XPCollection<ViewATCAdministrationRouteXpo> ListCollectionAdministrationRouteByAtcId(int atcId)
        {
            var session = new IndigoXPOSession<ViewATCAdministrationRouteXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("ATCId=" + atcId.ToString());
                CriteriaOperator criteria = CriteriaOperator.Parse("ATCId=?", atcId);
                return new XPCollection<ViewATCAdministrationRouteXpo>(session, criteria);
            }
        }

        public XPInstantFeedbackSource ListATCEntity()
        {
            var session = new IndigoXPOSession<ATCEntityXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(ATCEntityXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;IdPharmacologicalGroup.CodeName;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los niveles de riesgo
        /// </summary>
        public XPInstantFeedbackSource ListRiskLevel()
        {
            var session = new IndigoXPOSession<InventoryRiskLevelXpo>();
            {
                //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
                var classEntity = session.GetClassInfo(typeof(InventoryRiskLevelXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lista los DCI
        /// </summary>
        public XPInstantFeedbackSource ListDCI(string DCIId)
        {
            CriteriaOperator criteria = null;
            if (DCIId != string.Empty)
            {
                criteria = CriteriaOperator.Parse("Id !=" + DCIId);
            }

            var session = new IndigoXPOSession<DCIXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(DCIXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract Type
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractType()
        {
            var session = new IndigoXPOSession<InventoryContractTypeXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryContractTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;TypenName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract Type
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractTypeByStatus(bool status)
        {
            var session = new IndigoXPOSession<InventoryContractTypeXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status);
                var classEntity = session.GetClassInfo(typeof(InventoryContractTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;TypenName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListAllInventoryContract()
        {
            var session = new IndigoXPOSession<InventoryContractXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryContractXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ContractTypeId.CodeName;DocumentDate;ContractNumber;InitialDate;EndDate;SupplierId.CodeName;Status;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractByStatus(byte status)
        {
            var session = new IndigoXPOSession<InventoryContractXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status);
                var classEntity = session.GetClassInfo(typeof(InventoryContractXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ContractTypeId.CodeName;SupplierId.Id;SupplierId.CodeName;ContractNumber;Status", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los inventorycontrol
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControl()
        {
            var session = new IndigoXPOSession<InventoryControlXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryControlXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;DocumentType;DocumentTypeName;Status;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los inventorycontrol por estado
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlByStatus(byte status)
        {
            var session = new IndigoXPOSession<InventoryControlXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
                var classEntity = session.GetClassInfo(typeof(InventoryControlXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;DocumentType;DocumentTypeName;Status;StatusName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los inventorycontrol por estado
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlByDocumentType(byte DocumentType)
        {
            var session = new IndigoXPOSession<InventoryControlXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("DocumentType=" + DocumentType.ToString());
                var classEntity = session.GetClassInfo(typeof(InventoryControlXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;DocumentType;DocumentTypeName;Status;StatusName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los inventorycontrol
        /// </summary>
        public XPInstantFeedbackSource ListInventoryAdjustment()
        {
            var session = new IndigoXPOSession<InventoryAdjustmentXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryAdjustmentXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Description;AdjustmentType;AdjustmentTypeName;Status;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los inventorycontrol por estado
        /// </summary>
        public XPInstantFeedbackSource ListInventoryAdjustmentByStatus(byte status)
        {
            var session = new IndigoXPOSession<InventoryAdjustmentXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
                var classEntity = session.GetClassInfo(typeof(InventoryAdjustmentXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Description;AdjustmentTyp;AdjustmentTypeName;Status;StatusName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucher()
        {
            var session = new IndigoXPOSession<EntranceVoucherXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(EntranceVoucherXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.CodeName;WarehouseId.CodeName;Description;Status;StatusName", null);
                return serverMode;
            }
        }

        /// <summary> WarehouseId.CodeName
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucherByStatus(byte status)
        {
            var session = new IndigoXPOSession<EntranceVoucherXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
                var classEntity = session.GetClassInfo(typeof(EntranceVoucherXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.CodeName;WarehouseId.CodeName;Description;Status;StatusName", criteria);
                return serverMode;
            }
        }

        /// <summary> WarehouseId.CodeName
        /// Lista los comprobantes de entrada por estado y almacen
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucherByStatusAndWarehouse(byte status, int warehouseId = 0)
        {
            var session = new IndigoXPOSession<EntranceVoucherXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString() + " And WarehouseId.Id=" + warehouseId);
                var classEntity = session.GetClassInfo(typeof(EntranceVoucherXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.CodeName;WarehouseId.CodeName;Description;Status;StatusName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucherDevolution()
        {
            var session = new IndigoXPOSession<EntranceVoucherDevolutionXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(EntranceVoucherDevolutionXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;EntranceVoucherId.Code;Description;Status;StatusName", null);
                return serverMode;
            }
        }

        /// <summary> WarehouseId.CodeName
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucherDevolutionByStatus(byte status)
        {
            var session = new IndigoXPOSession<EntranceVoucherXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
                var classEntity = session.GetClassInfo(typeof(EntranceVoucherXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;EntranceVoucherId.Code;Description;Status;StatusName", criteria);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListAllInventoryPurchaserOrder()
        {
            var session = new IndigoXPOSession<InventoryPurchaseOrderXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryPurchaseOrderXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;DeliveredDate;Description;Status;SupplierId.CodeName;StatusName;TotalValue;CurrencyAbbreviation", null);
                return serverMode;
            }
        }

        /// <summary>
        /// lista todos los comprobantes contables
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListGeneralLedgerJournalVoucherTypes()
        {
            var session = new IndigoXPOSession<GeneralLedgerJournalVoucherTypesXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(GeneralLedgerJournalVoucherTypesXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Obtiene el grupo de atencion por el codigo del centro de atencion
        /// </summary>
        /// <returns></returns>
        public CareGroupByCareCenterXpo GetCareGroupByCareCenterXpo(String careCenterCode)
        {
            var session = new IndigoXPOSession<CareGroupByCareCenterXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("CareCenterCode=" + careCenterCode);
                var classEntity = session.GetClassInfo(typeof(CareGroupByCareCenterXpo));
                return session.FindObject<CareGroupByCareCenterXpo>(criteria);
            }
        }

        /// <summary>
        /// Lista los diagnosticos
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPCollection<ViewLASAMedication> ListViewLASAMedication(List<String> listATCs)
        {
            var session = new IndigoXPOSession<ViewLASAMedication>();
            CriteriaOperator criteria = CriteriaOperator.Parse("Code in (" + string.Join(",", listATCs.ToArray()) + ")");
            XPCollection <ViewLASAMedication> collect = new XPCollection<ViewLASAMedication>(session, criteria);
            return collect;
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractAssignment()
        {
            var session = new IndigoXPOSession<InventoryContractAssignmentXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryContractAssignmentXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ContractId.ContractNumber;ContractNumber;SupplierTransferorId.CodeName;SupplierAssigneeId.CodeName;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractModification()
        {
            var session = new IndigoXPOSession<InventoryContractModificationXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryContractModificationXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ContractId.ContractNumber;ContractNumber;ModificationTypeName;StatusName", null);
                return serverMode;
            }
        }

        /// <summary>
        /// Lists the devolution cause.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListDevolutionCause()
        {
            var session = new IndigoXPOSession<DevolutionCauseXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(DevolutionCauseXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity);
                return serverMode;
            }
        }

        /// <summary>
        ///  Lista los proveedores que tenga almacen en consignacion 
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public XPView ListSupplierByWarehouseConsignment()
        {
            var session = new IndigoXPOSession<WarehouseXpo>();
            {
                // Crear una vista para agrupar los datos por Codigo,Id,CodeName del proveedor
                XPView view = new XPView(session, typeof(WarehouseXpo));
                view.Properties.Add(new ViewProperty("SupplierId.Code", SortDirection.Ascending, "SupplierId.Code", true, true));
                view.Properties.Add(new ViewProperty("SupplierId.Id", SortDirection.Ascending, "SupplierId.Id", true, true));
                view.Properties.Add(new ViewProperty("SupplierId.CodeName", SortDirection.Ascending, "SupplierId.CodeName", true, true));
                view.Criteria = CriteriaOperator.Parse("WareHouseType=2", null);
                return view;
            }
            
        }

        /// <summary>
        /// Lista los tipos de medicamentos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListMedicationType()
        {
            var session = new IndigoXPOSession<MedicationTypeXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(MedicationTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName", null);
                return serverMode;
            }
        }

        public XPInstantFeedbackSource ListMedicationTypeByStatus(bool status )
        {
            var session = new IndigoXPOSession<MedicationTypeXpo>();
            {
                CriteriaOperator criteria = CriteriaOperator.Parse($"Status = {status}");
                var classEntity = session.GetClassInfo(typeof(MedicationTypeXpo));
                var serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName;CodeName", criteria);
                return serverMode;
            }
        }


        #endregion Others

        #region Dispensación Farmaceutica

        /// <summary>
        /// Código de la dispensación
        /// </summary>
        private string codePharma = string.Empty;

        /// <summary>
        /// Obtiene una dispensación farmaceutica por su codigo
        /// </summary>
        /// <param name="code">Código de la dispensación</param>
        public LinqInstantFeedbackSource GetPharmaceuticalDispensing(string code)
        {
            LinqInstantFeedbackSource linqPharma = new LinqInstantFeedbackSource();
            codePharma = code;
            linqPharma.KeyExpression = "Id";
            linqPharma.GetQueryable += linqPharma_GetQueryable;
            linqPharma.DismissQueryable += linqPharma_DismissQueryable;
            return linqPharma;
        }

        private void linqPharma_DismissQueryable(object sender, GetQueryableEventArgs e)
        {
            try
            {
                ((IDisposable)e.Tag).Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void linqPharma_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            try
            {
                var session = new Session(XpoDefault.DataLayer);
                XPQuery<PharmaceuticalDispensingXpo> pdTable = new XPQuery<PharmaceuticalDispensingXpo>(session);
                XPQuery<PharmaceuticalDispensingDetailXpo> pddTable = new XPQuery<PharmaceuticalDispensingDetailXpo>(session);
                XPQuery<Common_ThirdParty> ctpTable = new XPQuery<Common_ThirdParty>(session);
                XPQuery<Contract_CareGroup> cgTable = new XPQuery<Contract_CareGroup>(session);
                XPQuery<WarehouseXpo> whTable = new XPQuery<WarehouseXpo>(session);
                XPQuery<FunctionalUnitXpo> fuTable = new XPQuery<FunctionalUnitXpo>(session);
                XPQuery<InventoryProductXpo> pTable = new XPQuery<InventoryProductXpo>(session);

                var tmpQuerableSource = (from pdd in pddTable
                                         join pd in pdTable on pdd.PharmaceuticalDispensingId.Id equals pd.Id
                                         join ctp in ctpTable on pdd.OrderedHealthProfessionalThirdPartyId equals ctp.Id
                                         join fu in fuTable on pdd.FunctionalUnitId.Id equals fu.Id
                                         join wh in whTable on pdd.WarehouseId equals wh.Id
                                         join cg in cgTable on pdd.CareGroupId equals cg.Id
                                         join p in pTable on pdd.ProductId.Id equals p.Id
                                         where pdd.PharmaceuticalDispensingId.Code == codePharma
                                         select new
                                         {
                                             Id = pdd.Id,
                                             pdd.PharmaceuticalDispensingId,
                                             pdd.CareGroupId,
                                             pdd.ProductId,
                                             pdd.WarehouseId,
                                             pdd.Quantity,
                                             pdd.HealthAdministratorId,
                                             pdd.ReturnedQuantity,
                                             pdd.ServiceDate,
                                             pdd.OrderedHealthProfessionalThirdPartyId,
                                             DetailFunctionalUnitId = pdd.FunctionalUnitId,
                                             pdd.OrderedHealthProfessionalCode,
                                             pdd.OrderedProfessionalSpecialty,
                                             pdd.AuthorizationNumber,
                                             pdd.LiquidationType,
                                             pdd.CupsEntityId,
                                             pdd.SurchargeApply,
                                             pdd.SalePrice,
                                             pdd.TotalSalesPrice,
                                             pdd.GrandTotalSalesPrice,
                                             pdd.AverageCost,
                                             pdd.DiscountPercentage,
                                             pdd.DiscountValue,
                                             Iddd = pd.Id,
                                             pd.Code,
                                             pd.OperatingUnitId,
                                             pd.AdmissionNumber,
                                             pd.DocumentDate,
                                             pd.AffectInventory,
                                             pd.Status,
                                             pd.CreationUser,
                                             pd.CreationDate,
                                             pd.ModificationUser,
                                             pd.ModificationDate,
                                             pd.ConfirmationUser,
                                             pd.ConfirmationDate,
                                             pd.AnnulmentUser,
                                             pd.AnnulmentDate,
                                             FullNameFunctionalUnit = (fu.Code + " - " + fu.Name),
                                             CodeNameCareGroup = (cg.Code + " - " + cg.Name),
                                             CodeNameWareHouse = (wh.Code + " - " + wh.Name),
                                             PharmaceuticalDispensingDetailBatchSerialId = ctp.Id,
                                             CodeNameHealthProfessional = (pdd.OrderedHealthProfessionalCode.Trim() + " - " + ctp.Name.Trim()),
                                             PharmaceuticalDispensingDetailBatchSerialQuantity = pdd.Quantity,
                                             FuFunctionalUnitId = fu.Id,
                                             FunctionalUnitCode = fu.Code,
                                             FunctionalUnitName = fu.Name,
                                             fu.BranchOfficeId,
                                             fu.CostCenterId,
                                             fu.ProductionCenterId,
                                             fu.AccountingStructureId,
                                             fu.UnitType,
                                             fu.State,
                                             FunctionalUnitCreationUser = fu.CreationUser,
                                             FunctionalUnitCreationDate = fu.CreationDate,
                                             FunctionalUnitModificationUser = fu.ModificationUser,
                                             FunctionalUnitModificationDate = fu.ModificationDate,
                                             InventoryProductId = p.Id,
                                             InventoryProductCode = p.Code,
                                             p.Name,
                                             p.ProductTypeId,
                                             p.ATCId,
                                             p.CodeCUM,
                                             p.CodeAlternative,
                                             p.CodeAlternativeTwo,
                                             p.Description,
                                             p.ProductGroupId,
                                             p.ProductSubGroupId,
                                             p.PackagingUnitId,
                                             p.ManufacturerId,
                                             p.IVAId,
                                             p.Presentation,
                                             p.CodeSICE,
                                             p.HandlesSerial,
                                             p.HandlesHealthRegistration,
                                             p.HealthRegistration,
                                             p.ExpirationDate,
                                             p.BillingGroupId,
                                             p.ProductControl,
                                             p.ProductWithPriceControl,
                                             p.POSProduct,
                                             p.AuthorizationByOrderNumber,
                                             p.ExpirationDay,
                                             p.MaximumControlPeriod,
                                             p.ControlDays,
                                             p.ControlOrderQuantity,
                                             p.ProductOrderAmount,
                                             p.LastPurchase,
                                             p.LastSale,
                                             p.ProductOrigin,
                                             p.MinimumStock,
                                             p.MaximumStock,
                                             p.CommissionPercentage,
                                             p.RepositionPoint,
                                             p.ResetTime,
                                             p.CurrencyType,
                                             p.ProductCost,
                                             p.FinalProductCost,
                                             p.SellingPrice,
                                             p.AllPOSPathologies,
                                             InventoryProductStatus = p.Status,
                                             InventoryProductCreationUser = p.CreationUser,
                                             InventoryProductCreationDate = p.CreationDate,
                                             InventoryProductModificationUser = p.ModificationUser,
                                             InventoryProductModificationDate = p.ModificationDate
                                             ,Custody = wh.CustodyStore,
                                             QuotationPharmaceuticalDispensingDetailId = (pdd.QuotationPharmaceuticalDispensingDetailId != null) ? pdd.QuotationPharmaceuticalDispensingDetailId.Id : 0,
                                             QuotationCode = (pdd.QuotationPharmaceuticalDispensingDetailId != null) ? pdd.QuotationPharmaceuticalDispensingDetailId.QuotationId.Code : string.Empty,
                                             pdd.GrossValue,
                                             pdd.TaxValue
                                         });

                e.QueryableSource = tmpQuerableSource;
                e.Tag = session;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        #endregion Dispensación Farmaceutica

        #region Devolución Dispensación Farmaceutica

        /// <summary>
        /// Código de la devolución de dispensación
        /// </summary>
        private string codePharmaDev = string.Empty;

        /// <summary>
        /// Obtiene una devolución de dispensación farmaceutica por su codigo
        /// </summary>
        /// <param name="code">Código de la devolución de dispensación</param>
        public LinqInstantFeedbackSource GetPharmaceuticalDispensingDevolution(string code)
        {
            LinqInstantFeedbackSource linqPharmaDev = new LinqInstantFeedbackSource();
            codePharmaDev = code;
            linqPharmaDev.KeyExpression = "Id";
            linqPharmaDev.GetQueryable += linqPharmaDev_GetQueryable;
            linqPharmaDev.DismissQueryable += linqPharmaDev_DismissQueryable;
            return linqPharmaDev;
        }

        private void linqPharmaDev_DismissQueryable(object sender, GetQueryableEventArgs e)
        {
            try
            {
                ((IDisposable)e.Tag).Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void linqPharmaDev_GetQueryable(object sender, GetQueryableEventArgs e)
        {
            try
            {
                var session = new Session(XpoDefault.DataLayer);
                XPQuery<PharmaceuticalDispensingDevolutionXpo> pddevTable = new XPQuery<PharmaceuticalDispensingDevolutionXpo>(session);
                XPQuery<PharmaceuticalDispensingDevolutionDetailXpo> pdddevTable = new XPQuery<PharmaceuticalDispensingDevolutionDetailXpo>(session);
                XPQuery<PharmaceuticalDispensingDetailBatchSerialXpo> pdbTable = new XPQuery<PharmaceuticalDispensingDetailBatchSerialXpo>(session);
                XPQuery<PharmaceuticalDispensingDetailXpo> pddTable = new XPQuery<PharmaceuticalDispensingDetailXpo>(session);

                var tmpQuerableSource = (from pdddev in pdddevTable
                                         join pddev in pddevTable on pdddev.PharmaceuticalDispensingDevolutionId.Id equals pddev.Id
                                         join pdb in pdbTable on pdddev.PharmaceuticalDispensingDetailBatchSerialId equals pdb.Id
                                         join pdd in pddTable on pdb.PharmaceuticalDispensingDetailId.Id equals pdd.Id
                                         where pdddev.PharmaceuticalDispensingDevolutionId.Code == codePharmaDev
                                         select new
                                         {
                                             Id = pdddev.Id,
                                             PharmaceuticalDispensingDevolutionId = pdddev.PharmaceuticalDispensingDevolutionId.Id,
                                             pdddev.PharmaceuticalDispensingDetailBatchSerialId,
                                             pdddev.Quantity,
                                             pddev.Code,
                                             pddev.OperatingUnitId,
                                             pddev.DocumentDate,
                                             WarehouseId = pddev.WarehouseId.Id,
                                             pddev.AdmissionNumber,
                                             pddev.Observation,
                                             pddev.Status,
                                             pddev.CreationUser,
                                             pddev.CreationDate,
                                             pddev.ModificationUser,
                                             pddev.ModificationDate,
                                             pddev.ConfirmationUser,
                                             pddev.ConfirmationDate,
                                             pddev.AnnulmentUser,
                                             pddev.AnnulmentDate,
                                             CodePharmaceuticalDispensing = pdd.PharmaceuticalDispensingId.Code,
                                             ProductId = pdd.ProductId.Id,
                                             CodeNameProduct = (pdd.ProductId.Code + " - " + pdd.ProductId.Name),
                                             PharmaceuticalDispensingDetailId = pdd.Id,
                                             CodeNameWarehouse = (pddev.WarehouseId.Code + " - " + pddev.WarehouseId.Name),
                                             Prefix = pddev.WarehouseId.Prefix
                                         });

                e.QueryableSource = tmpQuerableSource;
                e.Tag = session;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        #endregion Devolución Dispensación Farmaceutica

        public InventoryProductXpo GetInventoryProductXpo(int id)
        {
            var session = new IndigoXPOSession<InventoryProductXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(InventoryProductXpo));
                return (InventoryProductXpo)session.GetObjectByKey(classEntity, id);
            }
        }

        public ATCXpo GetATCXpo(int id)
        {
            var session = new IndigoXPOSession<ATCXpo>();
            {
                var classEntity = session.GetClassInfo(typeof(ATCXpo));
                return (ATCXpo)session.GetObjectByKey(classEntity, id);
            }
        }

        #region Reports

        /// <summary>
        /// Metodo para obtener la fuente de datos del reporte informe de devolución de remisiones
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public XPCollection<InventoryRemissionDevolutionReportXpo> LoadDataSourceReportRemissionDevolution(DateTime? dateStart, DateTime? dateEnd, int devolutionType, string status, string thirdParties, string remissionDevolutions, int? currencyId = null)
        {
            var Filter = String.Empty;

            if (dateStart != null & dateEnd  != null ) {
                Filter = "GetDate(RemissionDate) >= #" + dateStart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "# AND GetDate(RemissionDate) <= #" + dateEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "#";
                Filter += String.Format(" AND DevolutionType = {0}", devolutionType);
            }

               

            if (!String.IsNullOrEmpty(status)){

               Filter += (Filter != String.Empty ? " AND " : "") + String.Format("Status IN ({0})", status);
                
            }

            if (!String.IsNullOrEmpty(thirdParties))
            {
                if (devolutionType == 1)
                {
                    Filter += (Filter != String.Empty ? " AND " : "") + String.Format("RemissionEntranceId.SupplierId.IdThirdParty.Id IN ({0})", thirdParties);
                }
                else if(devolutionType == 2)
                {
                    Filter += (Filter != String.Empty ? " AND " : "") + String.Format("RemissionOutputId.CustomerId.IdThirdParty.Id IN ({0})", thirdParties);
                }
                else if (devolutionType == 3)
                {
                    Filter += (Filter != String.Empty ? " AND " : "") + String.Format("ConsignmentInventoryRemissionId.SupplierId.IdThirdParty.Id IN ({0})", thirdParties);
                }
            }

            if (!String.IsNullOrEmpty(remissionDevolutions))
            {
                Filter += (Filter != String.Empty ? " AND " : "") + String.Format("Id IN ({0})", remissionDevolutions);
            }
            if (currencyId != null) { Filter += $" AND CurrencyId = {currencyId}"; }

            var session = new IndigoXPOSession<InventoryRemissionDevolutionReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            var classEntity = session.GetClassInfo(typeof(InventoryRemissionDevolutionReportXpo));
            return new XPCollection<InventoryRemissionDevolutionReportXpo>(session, criteria);
        }

        /// <summary>
        /// Metodo para obtener la fuente de datos del reporte informe de devolución de remisiones
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public XPCollection<InventoryEntranceVouhcerDevolutionReportXpo> LoadDataSourceReportEntranceVoucherDevolution(DateTime dateStart, DateTime dateEnd, string status, string thirdParties, string entranceVoucherDevolution)
        {
            var filtroConsulta = "GetDate(DocumentDate) >= #" + dateStart.ToString("yyyy-MM-dd HH:mm:ss") + "# AND GetDate(DocumentDate) <= #" + dateEnd.ToString("yyyy-MM-dd HH:mm:ss") + "#";

            if (!String.IsNullOrEmpty(status))
            {
                filtroConsulta += String.Format(" AND Status IN ({0})", status);
            }

            if (!String.IsNullOrEmpty(thirdParties))
            {
                filtroConsulta += String.Format(" AND EntranceVoucherId.SupplierId.IdThirdParty.Id IN ({0})", thirdParties);
            }

            if (!String.IsNullOrEmpty(entranceVoucherDevolution))
            {
                filtroConsulta += String.Format(" AND Id IN ({0})", entranceVoucherDevolution);
            }

            var session = new IndigoXPOSession<InventoryEntranceVouhcerDevolutionReportXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(filtroConsulta);
            var classEntity = session.GetClassInfo(typeof(InventoryEntranceVouhcerDevolutionReportXpo));
            return new XPCollection<InventoryEntranceVouhcerDevolutionReportXpo>(session, criteria);
        }

        /// <summary>
        /// Metodo para obtener la fuente de datos del reporte informe de devolución de remisiones
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public XPCollection<ViewReportProductBarcodeXpo> LoadDataSourceReportProductBarcode(string warehouses, string products, string batchSerials, bool includeZero)
        {
            var filtroConsulta = String.Empty;

            if (!String.IsNullOrEmpty(warehouses))
            {
                filtroConsulta += (String.IsNullOrEmpty(filtroConsulta) ? String.Empty : " AND ") + String.Format("WarehouseId IN ({0})", warehouses);
            }

            if (!String.IsNullOrEmpty(products))
            {
                filtroConsulta += (String.IsNullOrEmpty(filtroConsulta) ? String.Empty : " AND ") + String.Format("ProductId IN ({0})", products);
            }

            if (!String.IsNullOrEmpty(batchSerials))
            {
                filtroConsulta += (String.IsNullOrEmpty(filtroConsulta) ? String.Empty : " AND ") + String.Format("BatchSerialId IN ({0})", batchSerials);
            }

            if (!includeZero)
            {
                filtroConsulta += (String.IsNullOrEmpty(filtroConsulta) ? String.Empty : " AND ") + "Quantity <> 0";
            }

            var session = new IndigoXPOSession<ViewReportProductBarcodeXpo>();
            CriteriaOperator criteria = CriteriaOperator.Parse(filtroConsulta);
            var classEntity = session.GetClassInfo(typeof(ViewReportProductBarcodeXpo));
            return new XPCollection<ViewReportProductBarcodeXpo>(session, criteria);
        }

        #endregion

        #endregion PublicMethods

        public void Dispose()
        {
        }
    }
}