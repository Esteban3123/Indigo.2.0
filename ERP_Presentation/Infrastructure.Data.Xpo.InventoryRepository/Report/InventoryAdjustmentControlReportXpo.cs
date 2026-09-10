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
    [Persistent(@"Inventory.InventoryAdjustmentControl")]
    public class InventoryAdjustmentControlReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryAdjustmentReportXpo fInventoryAdjustmentId;
        [Association(@"InventoryAdjustmentControlReportXpoReferencesInventoryAdjustmentReportXpo")]
        public InventoryAdjustmentReportXpo InventoryAdjustmentId
        {
            get { return fInventoryAdjustmentId; }
            set { SetPropertyValue<InventoryAdjustmentReportXpo>("InventoryAdjustmentId", ref fInventoryAdjustmentId, value); }
        }
        InventoryControlDetailBatchSerialReportXpo fInventoryControlDetailBatchSerialId;
        [Association(@"InventoryAdjustmentControlReportXpoReferencesInventoryControlDetailBatchSerial")]
        public InventoryControlDetailBatchSerialReportXpo InventoryControlDetailBatchSerialId
        {
            get { return fInventoryControlDetailBatchSerialId; }
            set { SetPropertyValue<InventoryControlDetailBatchSerialReportXpo>("InventoryControlDetailBatchSerialId", ref fInventoryControlDetailBatchSerialId, value); }
        }
        byte fAdjustmentType;
        public byte AdjustmentType
        {
            get { return fAdjustmentType; }
            set { SetPropertyValue<byte>("AdjustmentType", ref fAdjustmentType, value); }
        }
        int fQuantityAdjustment;
        public int QuantityAdjustment
        {
            get { return fQuantityAdjustment; }
            set { SetPropertyValue<int>("QuantityAdjustment", ref fQuantityAdjustment, value); }
        }

        public InventoryAdjustmentControlReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
