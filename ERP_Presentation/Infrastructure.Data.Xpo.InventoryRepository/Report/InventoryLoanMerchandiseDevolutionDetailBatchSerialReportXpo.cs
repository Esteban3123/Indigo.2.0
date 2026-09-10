using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.LoanMerchandiseDevolutionDetailBatchSerial")]
    public class InventoryLoanMerchandiseDevolutionDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryLoanMerchandiseDevolutionDetailReportXpo fLoanMerchandiseDevolutionDetailId;
        [Association(@"Inventory_LoanMerchandiseDevolutionDetailBatchSerialReferencesInventory_LoanMerchandiseDevolutionDetail")]
        public InventoryLoanMerchandiseDevolutionDetailReportXpo LoanMerchandiseDevolutionDetailId
        {
            get { return fLoanMerchandiseDevolutionDetailId; }
            set { SetPropertyValue<InventoryLoanMerchandiseDevolutionDetailReportXpo>("LoanMerchandiseDevolutionDetailId", ref fLoanMerchandiseDevolutionDetailId, value); }
        }
        int fPhysicalInventoryId;
        public int PhysicalInventoryId
        {
            get { return fPhysicalInventoryId; }
            set { SetPropertyValue<int>("PhysicalInventoryId", ref fPhysicalInventoryId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        public InventoryLoanMerchandiseDevolutionDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
