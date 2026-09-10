using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.EntranceVoucherDevolutionOtherDeduction")]
    public class InventoryEntranceVoucherDevolutionOtherDeductionReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryEntranceVouhcerDevolutionReportXpo fEntranceVoucherDevolutionId;
        [Association(@"Inventory_EntranceVoucherDevolutionOtherDeductionReferencesInventory_EntranceVoucherDevolution")]
        public InventoryEntranceVouhcerDevolutionReportXpo EntranceVoucherDevolutionId
        {
            get { return fEntranceVoucherDevolutionId; }
            set { SetPropertyValue<InventoryEntranceVouhcerDevolutionReportXpo>("EntranceVoucherDevolutionId", ref fEntranceVoucherDevolutionId, value); }
        }
        InventoryEntranceVoucherOtherDeductionReportXpo fEntranceVoucherOtherDeductionId;
        [Association(@"Inventory_EntranceVoucherDevolutionOtherDeductionReferencesInventory_EntranceVoucherOtherDeduction")]
        public InventoryEntranceVoucherOtherDeductionReportXpo EntranceVoucherOtherDeductionId
        {
            get { return fEntranceVoucherOtherDeductionId; }
            set { SetPropertyValue<InventoryEntranceVoucherOtherDeductionReportXpo>("EntranceVoucherOtherDeductionId", ref fEntranceVoucherOtherDeductionId, value); }
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

        public InventoryEntranceVoucherDevolutionOtherDeductionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
