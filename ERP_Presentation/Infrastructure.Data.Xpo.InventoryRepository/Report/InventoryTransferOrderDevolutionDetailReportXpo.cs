using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.TransferOrderDevolutionDetail")]
    public class InventoryTransferOrderDevolutionDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryTransferOrderDevolutionReportXpo fTransferOrderDevolutionId;
        [Association(@"Inventory_TransferOrderDevolutionDetailReferencesInventory_TransferOrderDevolution")]
        public InventoryTransferOrderDevolutionReportXpo TransferOrderDevolutionId
        {
            get { return fTransferOrderDevolutionId; }
            set { SetPropertyValue<InventoryTransferOrderDevolutionReportXpo>("TransferOrderDevolutionId", ref fTransferOrderDevolutionId, value); }
        }
        InventoryTransferOrderDetailBatchSerialReportXpo fTransferOrderDetailBatchSerialId;
        [Association(@"Inventory_TransferOrderDevolutionDetailReferencesInventory_TransferOrderDetailBatchSerial")]
        public InventoryTransferOrderDetailBatchSerialReportXpo TransferOrderDetailBatchSerialId
        {
            get { return fTransferOrderDetailBatchSerialId; }
            set { SetPropertyValue<InventoryTransferOrderDetailBatchSerialReportXpo>("TransferOrderDetailBatchSerialId", ref fTransferOrderDetailBatchSerialId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        public InventoryTransferOrderDevolutionDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
