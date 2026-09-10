using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.EntranceVoucherDevolutionDetail")]
    public class InventoryEntranceVoucherDevolutionDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryEntranceVouhcerDevolutionReportXpo fEntranceVoucherDevolutionId;
        [Association(@"Inventory_EntranceVoucherDevolutionDetailReferencesInventory_EntranceVoucherDevolution")]
        public InventoryEntranceVouhcerDevolutionReportXpo EntranceVoucherDevolutionId
        {
            get { return fEntranceVoucherDevolutionId; }
            set { SetPropertyValue<InventoryEntranceVouhcerDevolutionReportXpo>("EntranceVoucherDevolutionId", ref fEntranceVoucherDevolutionId, value); }
        }
        InventoryEntranceVoucherDetailBatchSerialReportXpo fEntranceVoucherDetailBatchSerialId;
        [Association(@"Inventory_EntranceVoucherDevolutionDetailReferencesInventory_EntranceVoucherDetailBatchSerial")]
        public InventoryEntranceVoucherDetailBatchSerialReportXpo EntranceVoucherDetailBatchSerialId
        {
            get { return fEntranceVoucherDetailBatchSerialId; }
            set { SetPropertyValue<InventoryEntranceVoucherDetailBatchSerialReportXpo>("EntranceVoucherDetailBatchSerialId", ref fEntranceVoucherDetailBatchSerialId, value); }
        }
        DevolutionCauseReportXpo fDevolutionCauseId;
        [Association(@"Inventory_EntranceVoucherDevolutionDetail_References_Inventory_DevolutionCause")]
        public DevolutionCauseReportXpo DevolutionCauseId
        {
            get { return fDevolutionCauseId; }
            set { SetPropertyValue<DevolutionCauseReportXpo>("DevolutionCauseId", ref fDevolutionCauseId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        public InventoryEntranceVoucherDevolutionDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
