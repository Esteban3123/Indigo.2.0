using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.InventoryControlDetailBatchSerial")]
    public partial class InventoryControlDetailBatchSerialReportXpo : XPLiteObject
    {
        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        InventoryControlDetailReportXpo fInventoryControlDetailId;
        [Association(@"Inventory_InventoryControlDetailBatchSerialReferencesInventory_InventoryControlDetail")]
        public InventoryControlDetailReportXpo InventoryControlDetailId
        {
            get { return fInventoryControlDetailId; }
            set { SetPropertyValue<InventoryControlDetailReportXpo>("InventoryControlDetailId", ref fInventoryControlDetailId, value); }
        }

        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"Inventory_InventoryControlDetailBatchSerialReferencesInventory_BatchSerial")]
        public InventoryBatchSerialReportXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<InventoryBatchSerialReportXpo>("BatchSerialId", ref fBatchSerialId, value); }
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

        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        #endregion
        #region"Association"
        [Association(@"InventoryAdjustmentControlReportXpoReferencesInventoryControlDetailBatchSerial", typeof(InventoryAdjustmentControlReportXpo))]
        public XPCollection<InventoryAdjustmentControlReportXpo> InventoryAdjustmentControlReportXpo { get { return GetCollection<InventoryAdjustmentControlReportXpo>("InventoryAdjustmentControlReportXpo"); } }
        #endregion

        #region "Builders"

        public InventoryControlDetailBatchSerialReportXpo(Session session) : base(session) { }

        #endregion
    }

}
