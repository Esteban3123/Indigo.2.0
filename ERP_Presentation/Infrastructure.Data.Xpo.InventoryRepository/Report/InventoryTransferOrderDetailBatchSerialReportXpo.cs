using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.TransferOrderDetailBatchSerial")]
    public class InventoryTransferOrderDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryTransferOrderDetailReportXpo fTransferOrderDetailId;
        [Association(@"Inventory_TransferOrderDetailBatchSerialReferencesInventory_TransferOrderDetail")]
        public InventoryTransferOrderDetailReportXpo TransferOrderDetailId
        {
            get { return fTransferOrderDetailId; }
            set { SetPropertyValue<InventoryTransferOrderDetailReportXpo>("TransferOrderDetailId", ref fTransferOrderDetailId, value); }
        }
        InventoryPhysicalInventoryReportXpo fPhysicalInventoryId;
        [Association(@"InventoryTransferOrderDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo")]
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
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        [Association(@"Inventory_TransferOrderDevolutionDetailReferencesInventory_TransferOrderDetailBatchSerial", typeof(InventoryTransferOrderDevolutionDetailReportXpo))]
        public XPCollection<InventoryTransferOrderDevolutionDetailReportXpo> Inventory_TransferOrderDevolutionDetails { get { return GetCollection<InventoryTransferOrderDevolutionDetailReportXpo>("Inventory_TransferOrderDevolutionDetails"); } }

        public InventoryTransferOrderDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
