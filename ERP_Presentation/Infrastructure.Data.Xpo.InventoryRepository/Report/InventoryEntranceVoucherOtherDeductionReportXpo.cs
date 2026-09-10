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
    [Persistent(@"Inventory.EntranceVoucherOtherDeduction")]
    public class InventoryEntranceVoucherOtherDeductionReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryEntranceVoucherReportXpo fEntranceVoucherId;
        [Association(@"Inventory_EntranceVoucherOtherDeductionReferencesInventory_EntranceVoucher")]
        public InventoryEntranceVoucherReportXpo EntranceVoucherId
        {
            get { return fEntranceVoucherId; }
            set { SetPropertyValue<InventoryEntranceVoucherReportXpo>("EntranceVoucherId", ref fEntranceVoucherId, value); }
        }
        int fOtherWithholdingDeductionId;
        public int OtherWithholdingDeductionId
        {
            get { return fOtherWithholdingDeductionId; }
            set { SetPropertyValue<int>("OtherWithholdingDeductionId", ref fOtherWithholdingDeductionId, value); }
        }
        byte fType;
        public byte Type
        {
            get { return fType; }
            set { SetPropertyValue<byte>("Type", ref fType, value); }
        }
        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }
        decimal fValueOutstanding;
        public decimal ValueOutstanding
        {
            get { return fValueOutstanding; }
            set { SetPropertyValue<decimal>("ValueOutstanding", ref fValueOutstanding, value); }
        }
        [Association(@"Inventory_EntranceVoucherDevolutionOtherDeductionReferencesInventory_EntranceVoucherOtherDeduction", typeof(InventoryEntranceVoucherDevolutionOtherDeductionReportXpo))]
        public XPCollection<InventoryEntranceVoucherDevolutionOtherDeductionReportXpo> Inventory_EntranceVoucherDevolutionOtherDeductions { get { return GetCollection<InventoryEntranceVoucherDevolutionOtherDeductionReportXpo>("Inventory_EntranceVoucherDevolutionOtherDeductions"); } }

        public InventoryEntranceVoucherOtherDeductionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
