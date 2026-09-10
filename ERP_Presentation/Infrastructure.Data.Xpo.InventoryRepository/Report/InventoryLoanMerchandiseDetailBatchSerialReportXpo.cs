using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.LoanMerchandiseDetailBatchSerial")]
    public class InventoryLoanMerchandiseDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryLoanMerchandiseDetailReportXpo fLoanMerchandiseDetailId;
        [Association(@"Inventory_LoanMerchandiseDetailBatchSerialReferencesInventory_LoanMerchandiseDetail")]
        public InventoryLoanMerchandiseDetailReportXpo LoanMerchandiseDetailId
        {
            get { return fLoanMerchandiseDetailId; }
            set { SetPropertyValue<InventoryLoanMerchandiseDetailReportXpo>("LoanMerchandiseDetailId", ref fLoanMerchandiseDetailId, value); }
        }
        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"InventoryLoanMerchandiseDetailBatchSerialReportXpoReferencesInventoryBatchSerialReportXpo")]
        public InventoryBatchSerialReportXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<InventoryBatchSerialReportXpo>("BatchSerialId", ref fBatchSerialId, value); }
        }
        InventoryPhysicalInventoryReportXpo fPhysicalInventoryId;
        [Association(@"InventoryLoanMerchandiseDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo")]
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

        public InventoryLoanMerchandiseDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
