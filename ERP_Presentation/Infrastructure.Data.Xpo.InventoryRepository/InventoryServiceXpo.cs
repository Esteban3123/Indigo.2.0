using System;
using System.Collections.Generic;
using System.Linq;
//'***********************************************************************
//' Assembly         : Infrastructure.Data.Xpo.PaymentsRepostory
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 04-04-2014
//'
//' Copyright        : (c) . All rights reserved.
//'***********************************************************************

#region Imports

using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System.ServiceModel;
using Infrastructure.CrossCutting.Xpo.Base;
using System.IO;
using Infrastructure.CrossCutting.Base;
using DevExpress.Data.Filtering;
using DevExpress.Data.Linq;
using System.Configuration;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    public class InventoryServiceXpo : XpoBaseService
    {
        #region Fields

        /// <summary>
        /// Uri donde estan localizados los servicios xpo
        /// </summary>
        string uriServiceEntitiesXpo;

        /// <summary>
        /// Protocolo utilizado para los servicios xpo
        /// </summary>
        Protocol protocolServicesXpo;

        /// <summary>
        /// Variable tipo consultas asincronas de xpo
        /// </summary>
        XPInstantFeedbackSource serverMode;

        /// <summary>
        /// Variable que contiene el mapeo especifico por entidad para realizar la consulta mediante xpo
        /// </summary>
        XPClassInfo classEntity;

        #endregion

        #region Builder

        public InventoryServiceXpo(string Company)
        {
            //verifico que exista el archivo
            ReadConfiguration();
            //establezclo la capa de datos para XPO
            XpoDefault.DataLayer = new SimpleDataLayer(new WCFServiceDataStore(GetEndPoint(), GetRemoteAddress(), Company));
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Metodo necesario para leer la configuracion xml de la aplicacion
        /// </summary>
        private void ReadConfiguration()
        {
            uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer;
            //cargo el protocolo
            protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer;
        }

        /// <summary>
        /// Funcion para concatenar el nombre del endpoint por cada protocolo
        /// </summary>
        /// <returns></returns>
        private string GetEndPoint()
        {
            return System.String.Format("{0}_Endpoint", Enum.GetName(typeof(Protocol), protocolServicesXpo));
        }

        /// <summary>
        /// Funcion para concatenar el remoteaddress por cada protocolo
        /// </summary>
        /// <returns></returns>
        private string GetRemoteAddress()
        {
            return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, Enum.GetName(typeof(Protocol), protocolServicesXpo));
        }


        #endregion

        #region PublicMethods
        public XPCollection<InventoryControlDocumentXpo> ListInventoryControlDocumentByDocumentType(int documentType)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("DocumentType=" + documentType + "");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryControlDocumentXpo));
            return new XPCollection<InventoryControlDocumentXpo>(sessionNew, criteria);
 
        }

        /// <summary>
        /// Lista los detalles del control de inventario
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlDetailByInventoryControlId(int inventoryControlId)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("InventoryControlId =" + inventoryControlId);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryControlDetailXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
            return serverMode;
        }
        /// <summary>
        /// Lista los detalles de los detalles del control de inventario
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlDetailBatchSerialByInventoryControlDetailId(int inventoryControlDetailId)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("InventoryControlDetailId =" + inventoryControlDetailId);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryControlDetailBatchSerialXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista todas las devoluciones de ordenes de traslado
        /// </summary>
        public XPInstantFeedbackSource ListTransferOrderDevolution()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(TransferOrderDevolutionXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;TransferOrderId.Code;Status;Description;StatusName", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// Lista todas las ordenes de traslado
        /// </summary>
        public XPInstantFeedbackSource ListTransferOrderByStatus(int status)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status =" + status);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryTransferOrderXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;OrderType;OrderTypeName;Status;Description;StatusName", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// Lista todas las ordenes de traslado
        /// </summary>
        public XPInstantFeedbackSource ListTransferOrder()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryTransferOrderXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;OrderType;OrderTypeName;Status;Description;StatusName", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// Lista las facturas de productos
        /// </summary>
        public XPInstantFeedbackSource ListDocumentInvoiceProductSales()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryDocumentInvoiceProductSalesXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ThirdPartyId.NitName;WarehouseId.CodeName;InvoiceId.InvoiceNumber;Status;StatusName", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// Lista todas los almacenes
        /// </summary>
        public XPInstantFeedbackSource ListInventoryRequest()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryRequestXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;RequestType;RequestTypeName;Status;Observation;StatusName", null);
            return serverMode;
        }

        /// <summary>
        /// lista los conceptos de ajuste de inventario por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAdjustmentConceptByConceptType(int movementClass, int conceptType, bool status)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("MovementClass = " + movementClass + "And ConceptType = " + conceptType + "And Status =" + status);
            classEntity = sessionNew.GetClassInfo(typeof(AdjustmentConceptXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ConceptType", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista los conceptos de ajuste de inventario por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAdjustmentConceptByConceptTypeAndMovement(int conceptType, int movement, bool status)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ConceptType = " + conceptType + "And MovementClass = " + movement + " And Status =" + status);
            classEntity = sessionNew.GetClassInfo(typeof(AdjustmentConceptXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ConceptType", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todas las ordenes de servicio con un filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPurchaseOrderReportByFilter(string filtro)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryPurchaseOrderReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;DeliveredDate;WarehouseId.Code;Status", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todas las ordenes de traslado por filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListTransferOrderByFilter(string filtro)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryTransferOrderReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;OrderType;OrderTypeName;Status;Description;StatusName", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todos los comprobantes de entrada por filtro
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListEntranceVoucherReportByFilter(string filtro)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryEntranceVoucherReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Code;Status", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// Lista las devoluciones de remision por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionEntranceReportFilter(string Filtro)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(Filtro);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryRemissionEntranceReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Name;Status;ProductStatus", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// Lista las devoluciones de remision por tipo
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionDevolutionByTypeReport(string filtro)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(filtro);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryRemissionDevolutionReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;RemissionEntranceId.Code;WarehouseId.Code;Status", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todos los comprobantes de entrada
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListEntranceVoucherReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryEntranceVoucherReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Code;Status", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todas las ordenes de servicio
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPurchaseOrderReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryPurchaseOrderReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;DeliveredDate;WarehouseId.Code;Status", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todos los proveedores
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionEntranceReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryRemissionEntranceReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;SupplierId.IdThirdParty.Nit;SupplierId.IdThirdParty.Name;WarehouseId.Name;Status;ProductStatus", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todos los proveedores
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListSupplierInventoryReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryCommonSupplierXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;IdThirdParty.Nit;IdThirdParty.Name;Name;IdCity.Name;Status", null);
            serverMode.DefaultSorting = "IdThirdParty.Nit";
            return serverMode;
        }

        /// <summary>
        /// lista todos los clientes
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListCustomerInventoryReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryCommonCustomerReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Nit;ThirdPartyId.Name;Name;ThirdPartyId.PersonId.IdentificacionCityId.Name;State", null);
            serverMode.DefaultSorting = "Nit";
            return serverMode;
        }

        /// <summary>
        /// lista todos los productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListProductsReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId.Name;ProductGroupId.Name;ProductSubGroupId.Name", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todos los almacenes
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListWarehouseReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryWarehouseReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todos los lotes
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListBatchSerialReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryBatchSerialReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;BatchCode;ExpirationDate;ProductId.Code;ProductId.Name", null);
            serverMode.DefaultSorting = "BatchCode";
            return serverMode;
        }

        /// <summary>
        /// lista todos los grupos por clase producto
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListGroupReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("GroupClass= 1");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductGroupReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// lista todos los subgrupos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListSubGroupReport()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductSubGroupReportXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;HandlesBatch;HandlesExpiry", null);
            serverMode.DefaultSorting = "Code";
            return serverMode;
        }

        /// <summary>
        /// retornna todas las devoluciones de remisiones
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllRemissionDevolution()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(RemissionDevolutionXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;WarehouseId.CodeName;StatusName;DevolutionTypeName", null);
            return serverMode;
        }
        /// <summary>
        /// retornna las remisiones de salida por unidad operativa
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllRemissionOutput()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(RemissionOutputXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;WarehouseId.CodeName;CustomerId.NitName;StatusName;ProductStatusName", null);
            return serverMode;
        }
        /// <summary>
        /// obtiene las remisiones de entrada por estado
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionOutputByStatus(int status = 2)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductStatus<>3 And Status=" + status);
            classEntity = sessionNew.GetClassInfo(typeof(RemissionOutputXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;RemissionDate;WarehouseId.CodeName;CustomerId.NitName;StatusName;ProductStatusName", criteria);
            return serverMode;
        }
        /// <summary>
        /// retornna las remisiones de entrada 
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllRemissionEntrance()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(RemissionEntranceXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DisplaySupplier;RemissionDate;WarehouseId.CodeName;TotalValue;StatusName;ProductStatusName", null);
            return serverMode;
        }
        /// <summary>
        /// retorna solicitud de prestamo
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllLoanMerchandise()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryLoanMerchandiseXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;ThirdPartyId.CodeName;StatusName;LoanTypeName", null);
            return serverMode;
        }
        /// <summary>
        /// retorna solicitud de prestamo
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllLoanMerchandiseByWareHouseIdAndConfirm(int IdStores)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=2 AND InventoryLoanMerchandiseDetails[OutstandingQuantity > 0] AND WarehouseId.Id =" + IdStores);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryLoanMerchandiseXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;CodeThirdParty;WarehouseId.CodeName;ThirdPartyId.CodeName;StatusName;LoanTypeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// lista los productos con cantidad para devolver
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(string admissionNumber)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("OutstandingQuantity > 0 AND PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.AdmissionNumber ='" + admissionNumber + "' and PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Status = 2");
            classEntity = sessionNew.GetClassInfo(typeof(PharmaceuticalDispensingDetailBatchSerialXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
            return serverMode;
        }

        /// <summary>
        /// retorna de devoluciones de prestamo
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllLoanMerchandiseDevolutions()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryLoanMerchandiseDevolutionXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;StatusName", null);
            return serverMode;
        }
        /// <summary>
        /// retornna las remisiones de entrada 
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListAllPharmaceuticalDispensingDevolution()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(PharmaceuticalDispensingDevolutionXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;AdmissionNumber;StatusName", null);
            return serverMode;
        }
        /// <summary>
        /// retornna las remisiones de entrada por unidad operativa
        /// </summary>
        /// <param name="idOperatinUnit"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListRemissionEntranceByStatus(int status = 2)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductStatus<>3 And Status=" + status);
            classEntity = sessionNew.GetClassInfo(typeof(RemissionEntranceXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DisplaySupplier;RemissionDate;WarehouseId.CodeName;TotalValue;StatusName;ProductStatusName", criteria);
            return serverMode;
        }
        /// <summary>
        /// Lista todas las dispensaciones farmaceuticas
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPharmaceuticalDispensing()
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventorylistPharmaceuticalDispensingXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;AdmissionNumber;CodeNamePatient;DocumentDate;StatusName;Status", null);
            return serverMode;
        }
        /// <summary>
        /// lista los lotes por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListBatchSerialByProductId(int ProductId)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductId=" + ProductId);
            classEntity = sessionNew.GetClassInfo(typeof(BatchSerialXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Type;BatchCode;ExpirationDate;Barcode", criteria);
            return serverMode;
        }
        /// <summary>
        /// lista el almacen por id del producto
        /// </summary>
        /// <returns></returns>
        public XPCollection<PhysicalInventoryXpo> ListPhysicalInventoryByProductId(int ProductId)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductId=" + ProductId);
            XPCollection<PhysicalInventoryXpo> collect = new XPCollection<PhysicalInventoryXpo>(sessionNew, criteria);

            return collect;
        }

        /// <summary>
        /// lista productos por admission (frmAdmissionProduct)
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryViewAdmissionProductXpo> ListViewAdmissionProduct(string admissionNumber)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber=?", admissionNumber.Trim());
            XPCollection<InventoryViewAdmissionProductXpo> collect = new XPCollection<InventoryViewAdmissionProductXpo>(sessionNew, criteria);
            return collect;
        }

        /// <summary>
        /// lista el detalle de productos por admission (frmAdmissionProduct)
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryViewAdmissionProductDetailXpo> ListViewAdmissionProductDetail(string admissionNumber, int functionalUnitId, int productId)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("AdmissionNumber='" + admissionNumber + "' and FunctionalUnitId=" + functionalUnitId + " and ProductId=" + productId);
            XPCollection<InventoryViewAdmissionProductDetailXpo> collect = new XPCollection<InventoryViewAdmissionProductDetailXpo>(sessionNew, criteria);

            return collect;
        }

        /// <summary>
        /// Lista los detalles de productRate
        /// </summary>
        /// <returns></returns>
        public XPCollection ListProductRateDetailByProductRateId(int productRateId)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductRateId.Id=" + productRateId);
            XPCollection collect = new XPCollection(sessionNew, typeof(ProductRateDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de DashboardPharmacy por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryDashboardPharmacyReportXpo> ListViewDashboardPharmacyFilters(string Filter)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryDashboardPharmacyReportXpo> collect = new XPCollection<InventoryDashboardPharmacyReportXpo>(sessionNew, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de PharmaceuticalDispensingDevolutionDeytail por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo> ListViewPharmaceuticalDispensingDevolutionDetailFilters(string Filter)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo> collect = new XPCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo>(sessionNew, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de PharmaceuticalDispensingDevolution por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryPharmaceuticalViewDispensingDevolutionReportXpo> ListViewPharmaceuticalDispensingDevolutionFilters(string Filter)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryPharmaceuticalViewDispensingDevolutionReportXpo> collect = new XPCollection<InventoryPharmaceuticalViewDispensingDevolutionReportXpo>(sessionNew, criteria);
            return collect;
        }

        /// <summary>
        /// lista reporte de PharmaceuticalDispensing por filtros---------------------------
        /// </summary>
        /// <returns></returns>
        public XPCollection<InventoryPharmaceuticalViewDispensingReportXpo> ListViewPharmaceuticalDispensingFilters(string Filter)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse(Filter);
            XPCollection<InventoryPharmaceuticalViewDispensingReportXpo> collect = new XPCollection<InventoryPharmaceuticalViewDispensingReportXpo>(sessionNew, criteria, null);
            return collect;
        }

        /// <summary>
        /// lista todos los subdetalles de la remision de entrada por proveedor y linea de distribuccion
        /// </summary>
        /// <returns></returns>
        public XPCollection ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineId(int SupplierId, int SupplierDistributionLineId)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("RemissionEntranceDetailId.RemissionEntranceId.SupplierId.Id =" + SupplierId + " AND RemissionEntranceDetailId.RemissionEntranceId.SupplierDistributionLineId.Id = " + SupplierDistributionLineId + " AND RemissionEntranceDetailId.RemissionEntranceId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(sessionNew, typeof(InventoryRemissionEntranceDetailBatchSerialXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles del contrato por proveedor y linea de distribuccion
        /// </summary>
        /// <returns></returns>
        public XPCollection ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(int SupplierId, int SupplierDistributionLineId)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("InventoryContractId.SupplierId.Id =" + SupplierId + " AND InventoryContractId.SupplierDistributionLineId = " + SupplierDistributionLineId + " AND InventoryContractId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(sessionNew, typeof(InventoryContractDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de la orden de compra por proveedor y linea de distribuccion
        /// </summary>
        /// <returns></returns>
        public XPCollection ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(int SupplierId, int SupplierDistributionLineId)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("PurchaseOrderId.SupplierId.Id =" + SupplierId + " AND PurchaseOrderId.SupplierDistributionLineId = " + SupplierDistributionLineId + " AND PurchaseOrderId.Status = 2 AND OutstandingQuantity > 0");
            XPCollection collect = new XPCollection(sessionNew, typeof(InventoryPurcharseOrderDetailXpo), criteria);
            return collect;
        }

        /// <summary>
        /// lista todos los detalles de las solicitudes por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho
        /// </summary>
        /// <returns></returns>
        public XPCollection ListRequestDetailByFilterFunctionalUnitWarehouseAndOrderTypeAndDispatchTo(int filterFunctionalUnitWarehouse, byte orderType, byte dispatchTo)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = null;
            if (orderType == 1 || orderType == 2 && dispatchTo == 1)
            {
                criteria = CriteriaOperator.Parse("InventoryRequestId.TargetWarehouseId =" + filterFunctionalUnitWarehouse + " AND InventoryRequestId.Status = 2 AND OutstandingQuantity > 0");
            }
            if (orderType == 2 && dispatchTo == 2)
            {
                criteria = CriteriaOperator.Parse("InventoryRequestId.TargetFunctionalUnitId =" + filterFunctionalUnitWarehouse + " AND InventoryRequestId.Status = 2 AND OutstandingQuantity > 0");
            }
            XPCollection collect = new XPCollection(sessionNew, typeof(InventoryRequestDetailXpo), criteria);
            return collect;
        }

        public XPCollection<InventoryProductXpo> ListInventoryProductFrmStock()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);

            XPCollection<InventoryProductXpo> collect = new XPCollection<InventoryProductXpo>(sessionNew);

            return collect;
        }

        /// <summary>
        /// lista los inventarios fisicios filtrados por id del almacen
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPCollection<PhysicalInventoryXpo> ListPhysicalInventoryByWarehouseIdFrmStock(int WareHouseId)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("WarehouseId=" + WareHouseId);
            XPCollection<PhysicalInventoryXpo> collect = new XPCollection<PhysicalInventoryXpo>(sessionNew, criteria);

            return collect;
        }

        /// <summary>
        /// lista el almacen por id del Almacen
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPhysicalInventoryByWarehouseId(int WareHouseId)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(PhysicalInventoryXpo));
            CriteriaOperator criteria = CriteriaOperator.Parse("WarehouseId=" + WareHouseId);
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;WarehouseId.CodeName;ProductId.CodeName;ProductId.Presentation;BatchSerialId.BatchCode;Quantity", criteria);
            XPCollection<PhysicalInventoryXpo> collect = new XPCollection<PhysicalInventoryXpo>(sessionNew, criteria);

            return serverMode;
        }
        /// <summary>
        /// Lista todos los cubrimientos de productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListProductTemplate()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(ProductRateXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todas las patologías
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource LostPOSPathologies()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(POSPathologiesXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id,ProductId;DiagnosticId.Id;DiagnosticId.CodeName;DiagnosticId.Code;DiagnosticId.Name", null);
            return serverMode;
        }

        /// <summary>
        /// Lista los diagnosticos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListDiagnostic()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(DiagnosticXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;CodeName;Code;Name", null);
            return serverMode;
        }


        /// <summary>
        /// Lista los diagnosticos
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPCollection<DiagnosticXpo> ListDiagnosticById(int id)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Id=" + id);
            XPCollection<DiagnosticXpo> collect = new XPCollection<DiagnosticXpo>(sessionNew, criteria);

            return collect;
        }

        /// <summary>
        /// lista los productos con cantidad para devolver
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        public XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo> ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumberXpcollection(string admissionNumber)
        {
            dynamic sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("OutstandingQuantity > 0 AND PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.AdmissionNumber ='" + admissionNumber + "' and PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Status = 2");
            classEntity = sessionNew.GetClassInfo(typeof(PharmaceuticalDispensingDetailBatchSerialXpo));
            XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo> collect = new XPCollection<PharmaceuticalDispensingDetailBatchSerialXpo>(sessionNew, criteria);

            return collect;
        }

        /// <summary>
        /// Lista los productos por clase del tipo de producto
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByProductType(int ClassProductType)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class=" + ClassProductType + "");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;CodeName", criteria);
            return serverMode;
        }

        public XPInstantFeedbackSource ListInventoryProductByProductTypeClasses(List<string> ProductTypeClasses)
        {
            string InCriteria = String.Join(",", ProductTypeClasses.ToArray());
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class in (" + InCriteria + ")");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;CodeName", criteria);
            return serverMode;
        }


        /// <summary>
        /// Lista los productos en los cuales la clase del tipo sea distinto al deseado
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByNoClassType(int ClassProductType)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class!=" + ClassProductType + "");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;CodeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista los productos por estado y en los cuales la clase del tipo sea distinto al deseado
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByStatusByNoClassType(bool status, int ClassProductType)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class!=" + ClassProductType + "and Status=" + status);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, null, criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista todos los productos
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProduct()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ATCId.Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;CodeName", null);
            return serverMode;
        }

        public XPInstantFeedbackSource ListInventoryProductByATCCode(string atcCode)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("ATCId.Code = '" + atcCode + "'");
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ATCId.Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;CodeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista los productos en los cuales la clase del tipo sea el deseado y los productos que manejen lote
        /// </summary>
        /// <param name="ClassProductType">Type of the class product.</param>
        /// <returns></returns>
        public XPInstantFeedbackSource ListInventoryProductByClassTypeAndHandlesBatch(bool status, int ClassProductType)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ProductTypeId.Class <>" + ClassProductType + "and Status=" + status + " AND ProductSubGroupId.HandlesBatch = 1 AND ProductSubGroupId.HandlesExpiry = 1");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryProductXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;ProductTypeId;ProductTypeId.ClassName;ProductTypeId.Name;ProductTypeId.Class;CodeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista todos los ATC
        /// </summary>
        public XPInstantFeedbackSource ListATC()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(ATCXpo));
            serverMode = new XPInstantFeedbackSource(classEntity);
            return serverMode;
        }

        /// <summary>
        /// Lists the billing group.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListBillingGroup()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(BillingGroupXpo));
            serverMode = new XPInstantFeedbackSource(classEntity);
            return serverMode;
        }

        /// <summary>
        /// Lists the general ledger iva.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListGeneralLedgerIva()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(GeneralLedgerIVAXpo));
            serverMode = new XPInstantFeedbackSource(classEntity);
            return serverMode;
        }

        /// <summary>
        /// Lists the packaging unit.
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListPackagingUnit()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(PackagingUnitXpo));
            serverMode = new XPInstantFeedbackSource(classEntity);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los grupos
        /// </summary>
        public XPInstantFeedbackSource ListProductGroup()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(ProductGroupXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;GroupClassName;SubclassCodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los subgrupos
        /// </summary>
        public XPInstantFeedbackSource ListProductSubGroup()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(ProductSubGroupXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todas las unidades de medida
        /// </summary>
        public XPInstantFeedbackSource ListMeasureUnit()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(MeasureUnitXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;Abbreviation", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todas las unidades de medida
        /// </summary>
        public XPInstantFeedbackSource ListMeasureUnitByType(byte unitType)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("UnitType=" + unitType);
            classEntity = sessionNew.GetClassInfo(typeof(MeasureUnitXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;UnitType;CodeName;Abbreviation", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los almacenes
        /// </summary>
        public XPInstantFeedbackSource ListWarehouse()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(WarehouseXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los almacenes
        /// </summary>
        public XPInstantFeedbackSource ListWarehouseWithoutVirtualStore()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("VirtualStore = 0");
            classEntity = sessionNew.GetClassInfo(typeof(WarehouseXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los almacenes
        /// </summary>
        public XPInstantFeedbackSource ListWarehouseByStatus(bool status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(WarehouseXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
            return serverMode;
        }
        /// <summary>
        /// Lista los almacenes por estado y por usuario
        /// </summary>
        public XPInstantFeedbackSource ListWarehouseByStatusAndUser(bool status, string codeUser)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status = ? And Inventory_WarehouseUsers[UserCode = '" + codeUser + "']", status);
            classEntity = sessionNew.GetClassInfo(typeof(WarehouseXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
            return serverMode;
        }


        /// <summary>
        /// Lista los diagnosticos
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public XPCollection<WarehouseXpo> ListWarehouseByStatusAndUserXpCollection(bool status, string codeUser)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status = ? And Inventory_WarehouseUsers[UserCode = '" + codeUser + "']", status);

            XPCollection<WarehouseXpo> collect = new XPCollection<WarehouseXpo>(sessionNew, criteria);

            return collect;
        }

        /// <summary>
        /// Lista todas los grupos farmacologicos
        /// </summary>
        public XPInstantFeedbackSource ListPharmacologicalGroup()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(PharmacologicalGroupXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los conceptos de movimiento
        /// </summary>
        public XPInstantFeedbackSource ListAdjustmentConcept()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(AdjustmentConceptXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;ConceptType;ConceptTypeName;MovementClassName;MovementClass", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los conceptos de ajuste
        /// </summary>
        public XPInstantFeedbackSource ListAdjustmentConceptByType(byte Type)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("ConceptType = " + Type);
            classEntity = sessionNew.GetClassInfo(typeof(AdjustmentConceptXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;ConceptType;ConceptTypeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista todas los fabricantes
        /// </summary>
        public XPInstantFeedbackSource ListManufacturers()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(ManufacturersXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista otras deducciones o retenciones
        /// </summary>
        public XPInstantFeedbackSource ListOtherWitholdingDeductions()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(OtherWithholdingDeductionsXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Status;Type;TypeName;RoundType;RoundTypeName;AccountPayableConceptId.CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista los tipos de producto
        /// </summary>
        public XPInstantFeedbackSource ListProductType()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(ProductTypeXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;Class;ClassName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista los tipos de producto por estado
        /// </summary>
        public XPInstantFeedbackSource ListProductTypeByStatus(bool status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(ProductTypeXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;Class;ClassName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista los atributos de tipos de producto
        /// </summary>
        public XPInstantFeedbackSource ListAttributeProductType()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(AttributeProductTypeXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;ProductTypeId;ProductTypeId.CodeName;DataType;DataTypeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista todos los DCI
        /// </summary>
        public XPInstantFeedbackSource ListDCI()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(DCIXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista los DCI por estado
        /// </summary>
        public XPInstantFeedbackSource ListDCIByStatus(bool status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(DCIXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista las formas farmaceuticas
        /// </summary>
        public XPInstantFeedbackSource ListPharmaceuticalForm()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(PharmaceuticalFormXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista las formas farmaceuticas por estado
        /// </summary>
        public XPInstantFeedbackSource ListPharmaceuticalFormByStatus(bool status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(PharmaceuticalFormXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Lista las vias de administracion
        /// </summary>
        public XPInstantFeedbackSource ListAdministrationRoute()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(AdministrationRouteXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName;PharmaceuticalFormId.CodeName", null);
            return serverMode;
        }

        /// <summary>
        /// Lista los niveles de riesgo
        /// </summary>
        public XPInstantFeedbackSource ListRiskLevel()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            //CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status + "");
            classEntity = sessionNew.GetClassInfo(typeof(InventoryRiskLevelXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", null);
            return serverMode;
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

            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(DCIXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los Contract Type
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractType()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryContractTypeXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;TypenName", null);
            return serverMode;
        }

        /// <summary>
        /// Listar los Contract Type
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractTypeByStatus(bool status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryContractTypeXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;TypenName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListAllInventoryContract()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryContractXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ContractTypeId.CodeName;ContractNumber;InitialDate;EndDate;SupplierId.CodeName;Status", null);
            return serverMode;
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListInventoryContractByStatus(byte status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryContractXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;ContractTypeId.CodeName;SupplierId.CodeName;Status", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los inventorycontrol
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControl()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryControlXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;DocumentType;DocumentTypeName;Status;StatusName", null);
            return serverMode;
        }

        /// <summary>
        /// Listar los inventorycontrol por estado
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlByStatus(byte status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
            classEntity = sessionNew.GetClassInfo(typeof(InventoryControlXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;DocumentType;DocumentTypeName;Status;StatusName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los inventorycontrol por estado
        /// </summary>
        public XPInstantFeedbackSource ListInventoryControlByDocumentType(byte DocumentType)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("DocumentType=" + DocumentType.ToString());
            classEntity = sessionNew.GetClassInfo(typeof(InventoryControlXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;DocumentType;DocumentTypeName;Status;StatusName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los inventorycontrol
        /// </summary>
        public XPInstantFeedbackSource ListInventoryAdjustment()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryAdjustmentXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Description;AdjustmentTyp;AdjustmentTypeName;Status;StatusName", null);
            return serverMode;
        }

        /// <summary>
        /// Listar los inventorycontrol por estado
        /// </summary>
        public XPInstantFeedbackSource ListInventoryAdjustmentByStatus(byte status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
            classEntity = sessionNew.GetClassInfo(typeof(InventoryAdjustmentXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Description;AdjustmentTyp;AdjustmentTypeName;Status;StatusName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucher()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(EntranceVoucherXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.CodeName;WarehouseId.CodeName;Description;Status;StatusName", null);
            return serverMode;
        }

        /// <summary> WarehouseId.CodeName
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucherByStatus(byte status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
            classEntity = sessionNew.GetClassInfo(typeof(EntranceVoucherXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;SupplierId.CodeName;WarehouseId.CodeName;Description;Status;StatusName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucherDevolution()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(EntranceVoucherDevolutionXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;EntranceVoucherId.Code;Description;Status;StatusName", null);
            return serverMode;
        }

        /// <summary> WarehouseId.CodeName
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListEntranceVoucherDevolutionByStatus(byte status)
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            CriteriaOperator criteria = CriteriaOperator.Parse("Status=" + status.ToString());
            classEntity = sessionNew.GetClassInfo(typeof(EntranceVoucherXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;WarehouseId.CodeName;EntranceVoucherId.Code;Description;Status;StatusName", criteria);
            return serverMode;
        }

        /// <summary>
        /// Listar los Contract
        /// </summary>
        public XPInstantFeedbackSource ListAllInventoryPurchaserOrder()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(InventoryPurchaseOrderXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;DeliveredDate;Description;Status;SupplierId.CodeName;StatusName", null);
            return serverMode;
        }

        /// <summary>
        /// lista todos los comprobantes contables
        /// </summary>
        /// <returns></returns>
        public XPInstantFeedbackSource ListGeneralLedgerJournalVoucherTypes()
        {
            var sessionNew = new Session(XpoDefault.DataLayer);
            classEntity = sessionNew.GetClassInfo(typeof(GeneralLedgerJournalVoucherTypesXpo));
            serverMode = new XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", null);
            return serverMode;
        }

        #endregion
    }
}
