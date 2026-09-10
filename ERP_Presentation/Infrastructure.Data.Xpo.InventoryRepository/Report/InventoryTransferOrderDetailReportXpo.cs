using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.TransferOrderDetail")]
    public class InventoryTransferOrderDetailReportXpo : XPLiteObject  
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryTransferOrderReportXpo fTransferOrderId;
        [Association(@"Inventory_TransferOrderDetailReferencesInventory_TransferOrder")]
        public InventoryTransferOrderReportXpo TransferOrderId
        {
            get { return fTransferOrderId; }
            set { SetPropertyValue<InventoryTransferOrderReportXpo>("TransferOrderId", ref fTransferOrderId, value); }
        }
        int fInventoryRequestDetailId;
        public int InventoryRequestDetailId
        {
            get { return fInventoryRequestDetailId; }
            set { SetPropertyValue<int>("InventoryRequestDetailId", ref fInventoryRequestDetailId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"InventoryTransferOrderDetailReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        int fInventoryQuantity;
        public int InventoryQuantity
        {
            get { return fInventoryQuantity; }
            set { SetPropertyValue<int>("InventoryQuantity", ref fInventoryQuantity, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }
        string fDescription;
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        [Association(@"Inventory_TransferOrderDetailBatchSerialReferencesInventory_TransferOrderDetail", typeof(InventoryTransferOrderDetailBatchSerialReportXpo))]
        public XPCollection<InventoryTransferOrderDetailBatchSerialReportXpo> Inventory_TransferOrderDetailBatchSerials { get { return GetCollection<InventoryTransferOrderDetailBatchSerialReportXpo>("Inventory_TransferOrderDetailBatchSerials"); } }

        public InventoryTransferOrderDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
