using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.LoanMerchandiseDetail")]
    public class InventoryLoanMerchandiseDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryLoanMerchandiseReportXpo fLoanMerchandiseId;
        [Association(@"Inventory_LoanMerchandiseDetailReferencesInventory_LoanMerchandise")]
        public InventoryLoanMerchandiseReportXpo LoanMerchandiseId
        {
            get { return fLoanMerchandiseId; }
            set { SetPropertyValue<InventoryLoanMerchandiseReportXpo>("LoanMerchandiseId", ref fLoanMerchandiseId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"InventoryLoanMerchandiseDetailReportXpoReferencesInventoryProductReportXpo")]
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
        decimal fUnitValue;
        public decimal UnitValue
        {
            get { return fUnitValue; }
            set { SetPropertyValue<decimal>("UnitValue", ref fUnitValue, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        [Association(@"Inventory_LoanMerchandiseDetailBatchSerialReferencesInventory_LoanMerchandiseDetail", typeof(InventoryLoanMerchandiseDetailBatchSerialReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDetailBatchSerialReportXpo> Inventory_LoanMerchandiseDetailBatchSerials { get { return GetCollection<InventoryLoanMerchandiseDetailBatchSerialReportXpo>("Inventory_LoanMerchandiseDetailBatchSerials"); } }
        [Association(@"Inventory_LoanMerchandiseDevolutionDetailReferencesInventory_LoanMerchandiseDetail", typeof(InventoryLoanMerchandiseDevolutionDetailReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDevolutionDetailReportXpo> Inventory_LoanMerchandiseDevolutionDetails { get { return GetCollection<InventoryLoanMerchandiseDevolutionDetailReportXpo>("Inventory_LoanMerchandiseDevolutionDetails"); } }

        public InventoryLoanMerchandiseDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
