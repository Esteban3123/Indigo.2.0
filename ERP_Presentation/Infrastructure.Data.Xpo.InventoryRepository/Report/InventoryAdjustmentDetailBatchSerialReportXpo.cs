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
    [Persistent(@"Inventory.InventoryAdjustmentDetailBatchSerial")]
    public class InventoryAdjustmentDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryAdjustmentDetailReportXpo fInventoryAdjustmentDetailId;
        [Association(@"InventoryAdjustmentDetailBatchSerialReportXpoReferencesInventoryAdjustmentDetailReportXpo")]
        public InventoryAdjustmentDetailReportXpo InventoryAdjustmentDetailId
        {
            get { return fInventoryAdjustmentDetailId; }
            set { SetPropertyValue<InventoryAdjustmentDetailReportXpo>("InventoryAdjustmentDetailId", ref fInventoryAdjustmentDetailId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"InventoryAdjustmentDetailBatchSerialReportXpoReferencesInventoryBatchSerialReportXpo")]
        public InventoryBatchSerialReportXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<InventoryBatchSerialReportXpo>("BatchSerialId", ref fBatchSerialId, value); }
        }

        public InventoryAdjustmentDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
