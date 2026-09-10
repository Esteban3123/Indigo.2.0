using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PhysicalInventory")]
    public class InventoryPhysicalInventoryReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryPhysicalInventoryReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"InventoryPhysicalInventoryReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"InventoryPhysicalInventoryReportXpoReferencesInventoryBatchSerialReportXpo")]
        public InventoryBatchSerialReportXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<InventoryBatchSerialReportXpo>("BatchSerialId", ref fBatchSerialId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        string FPharmaceuticalForm;
        [NonPersistent()]
        public string PharmaceuticalForm
        {
            get { return FPharmaceuticalForm; }
            set { this.FPharmaceuticalForm = value; }
        }

        [PersistentAlias("Iif(BatchSerialId is null,'',BatchSerialId.BatchCode)")]
        public string BatchCode
        {
            get { return Convert.ToString(this.EvaluateAlias("BatchCode")); }
        }

        [Association(@"InventoryRemissionOutputDetailPhysicalReportXpoReferencesInventoryPhysicalInventoryReportXpo", typeof(InventoryRemissionOutputDetailPhysicalReportXpo))]
        public XPCollection<InventoryRemissionOutputDetailPhysicalReportXpo> InventoryRemissionOutputDetailPhysicalReportXpo { get { return GetCollection<InventoryRemissionOutputDetailPhysicalReportXpo>("InventoryRemissionOutputDetailPhysicalReportXpo"); } }
        [Association(@"InventoryLoanMerchandiseDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo", typeof(InventoryLoanMerchandiseDetailBatchSerialReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDetailBatchSerialReportXpo> InventoryLoanMerchandiseDetailBatchSerialPhysicalReportXpo { get { return GetCollection<InventoryLoanMerchandiseDetailBatchSerialReportXpo>("InventoryLoanMerchandiseDetailBatchSerialPhysicalReportXpo"); } }
        [Association(@"InventoryTransferOrderDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo", typeof(InventoryTransferOrderDetailBatchSerialReportXpo))]
        public XPCollection<InventoryTransferOrderDetailBatchSerialReportXpo> InventoryTransferOrderDetailBatchSerialPhysicalReportXpo { get { return GetCollection<InventoryTransferOrderDetailBatchSerialReportXpo>("InventoryTransferOrderDetailBatchSerialPhysicalReportXpo"); } }
        [Association(@"InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo", typeof(InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo> InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo { get { return GetCollection<InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo>("InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo"); } }
        [Association(@"InventoryPharmaceuticalDispensingDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo", typeof(InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo> InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo { get { return GetCollection<InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo>("InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo"); } }

        public InventoryPhysicalInventoryReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
