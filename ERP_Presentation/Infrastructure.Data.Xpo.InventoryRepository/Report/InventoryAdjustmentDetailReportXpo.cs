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
     [Persistent(@"Inventory.InventoryAdjustmentDetail")]
    public class InventoryAdjustmentDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryAdjustmentReportXpo fInventoryAdjustmentId;
        [Association(@"InventoryAdjustmentDetailReportXpoReferencesInventoryAdjustmentReportXpo")]
        public InventoryAdjustmentReportXpo InventoryAdjustmentId
        {
            get { return fInventoryAdjustmentId; }
            set { SetPropertyValue<InventoryAdjustmentReportXpo>("InventoryAdjustmentId", ref fInventoryAdjustmentId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"InventoryAdjustmentDetailReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        InventoryAdjustmentConceptReportXpo fAdjustmentConceptId;
        [Association(@"InventoryAdjustmentDetailReportXpo_References_InventoryAdjustmentConceptReportXpo")]
        public InventoryAdjustmentConceptReportXpo AdjustmentConceptId
        {
            get { return fAdjustmentConceptId; }
            set { SetPropertyValue<InventoryAdjustmentConceptReportXpo>("AdjustmentConceptId", ref fAdjustmentConceptId, value); }
        }

        //[PersistentAlias("InventoryAdjustmentDetailBatchSerialReportXpo.join(',', BatchSerialId.BatchCode)")]
        //public string BatchCodes
        //{
        //    get { return Convert.ToString("BatchCodes"); }
        //}

        public string BatchCodes
        {
            get { return string.Join(",", InventoryAdjustmentDetailBatchSerialReportXpo?.Select(m => m.BatchSerialId?.BatchCode)); }
        }

        decimal fUnitValue;
        public decimal UnitValue
        {
            get { return fUnitValue; }
            set { SetPropertyValue<decimal>("UnitValue", ref fUnitValue, value); }
        }
        [Association(@"InventoryAdjustmentDetailBatchSerialReportXpoReferencesInventoryAdjustmentDetailReportXpo", typeof(InventoryAdjustmentDetailBatchSerialReportXpo))]
        public XPCollection<InventoryAdjustmentDetailBatchSerialReportXpo> InventoryAdjustmentDetailBatchSerialReportXpo { get { return GetCollection<InventoryAdjustmentDetailBatchSerialReportXpo>("InventoryAdjustmentDetailBatchSerialReportXpo"); } }

        public InventoryAdjustmentDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
