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
    [Persistent(@"Inventory.EntranceVoucherDetailBatchSerial")]
    public class InventoryEntranceVoucherDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryEntranceVoucherDetailReportXpo fEntranceVoucherDetailId;
        [Association(@"Inventory_EntranceVoucherDetailBatchSerialReferencesInventory_EntranceVoucherDetail")]
        public InventoryEntranceVoucherDetailReportXpo EntranceVoucherDetailId
        {
            get { return fEntranceVoucherDetailId; }
            set { SetPropertyValue<InventoryEntranceVoucherDetailReportXpo>("EntranceVoucherDetailId", ref fEntranceVoucherDetailId, value); }
        }
        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"Inventory_EntranceVoucherDetailBatchSerialReferencesInventoryBatchSerialReportXpo")]
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
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        [Association(@"Inventory_EntranceVoucherDevolutionDetailReferencesInventory_EntranceVoucherDetailBatchSerial", typeof(InventoryEntranceVoucherDevolutionDetailReportXpo))]
        public XPCollection<InventoryEntranceVoucherDevolutionDetailReportXpo> Inventory_EntranceVoucherDevolutionDetailBatchSerial { get { return GetCollection<InventoryEntranceVoucherDevolutionDetailReportXpo>("Inventory_EntranceVoucherDevolutionDetailBatchSerial"); } }

        public InventoryEntranceVoucherDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
