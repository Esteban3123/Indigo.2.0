using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.DocumentInvoiceProductSalesDetailBatchSerial")]
    public class InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryDocumentInvoiceProductSalesDetailReportXpo fDocumentInvoiceProductSalesDetailId;
        [Association(@"Inventory_DocumentInvoiceProductSalesDetailBatchSerialReferencesInventory_DocumentInvoiceProductSalesDetail")]
        public InventoryDocumentInvoiceProductSalesDetailReportXpo DocumentInvoiceProductSalesDetailId
        {
            get { return fDocumentInvoiceProductSalesDetailId; }
            set { SetPropertyValue<InventoryDocumentInvoiceProductSalesDetailReportXpo>("DocumentInvoiceProductSalesDetailId", ref fDocumentInvoiceProductSalesDetailId, value); }
        }
        InventoryPhysicalInventoryReportXpo fPhysicalInventoryId;
        [Association(@"InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo")]
        public InventoryPhysicalInventoryReportXpo PhysicalInventoryId
        {
            get { return fPhysicalInventoryId; }
            set { SetPropertyValue<InventoryPhysicalInventoryReportXpo>("PhysicalInventoryId", ref fPhysicalInventoryId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        public InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
