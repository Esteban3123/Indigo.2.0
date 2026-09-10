using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.LoanMerchandiseDevolutionDetail")]
    public class InventoryLoanMerchandiseDevolutionDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryLoanMerchandiseDevolutionReportXpo fLoanMerchandiseDevolutionId;
        [Association(@"Inventory_LoanMerchandiseDevolutionDetailReferencesInventory_LoanMerchandiseDevolution")]
        public InventoryLoanMerchandiseDevolutionReportXpo LoanMerchandiseDevolutionId
        {
            get { return fLoanMerchandiseDevolutionId; }
            set { SetPropertyValue<InventoryLoanMerchandiseDevolutionReportXpo>("LoanMerchandiseDevolutionId", ref fLoanMerchandiseDevolutionId, value); }
        }
        InventoryLoanMerchandiseDetailReportXpo fLoanMerchandiseDetaillId;
        [Association(@"Inventory_LoanMerchandiseDevolutionDetailReferencesInventory_LoanMerchandiseDetail")]
        public InventoryLoanMerchandiseDetailReportXpo LoanMerchandiseDetaillId
        {
            get { return fLoanMerchandiseDetaillId; }
            set { SetPropertyValue<InventoryLoanMerchandiseDetailReportXpo>("LoanMerchandiseDetaillId", ref fLoanMerchandiseDetaillId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        [Association(@"Inventory_LoanMerchandiseDevolutionDetailBatchSerialReferencesInventory_LoanMerchandiseDevolutionDetail", typeof(InventoryLoanMerchandiseDevolutionDetailBatchSerialReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDevolutionDetailBatchSerialReportXpo> Inventory_LoanMerchandiseDevolutionDetailBatchSerials { get { return GetCollection<InventoryLoanMerchandiseDevolutionDetailBatchSerialReportXpo>("Inventory_LoanMerchandiseDevolutionDetailBatchSerials"); } }

        public InventoryLoanMerchandiseDevolutionDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
