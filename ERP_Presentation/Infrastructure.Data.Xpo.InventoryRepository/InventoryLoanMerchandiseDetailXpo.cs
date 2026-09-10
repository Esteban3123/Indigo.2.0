using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.LoanMerchandiseDetail")]
    public class InventoryLoanMerchandiseDetailXpo: XPLiteObject
    {
        #region Members
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryLoanMerchandiseXpo fLoanMerchandiseId;
        [Association(@"Inventory_LoanMerchandiseDetailReferencesInventory_LoanMerchandise")]
        public InventoryLoanMerchandiseXpo LoanMerchandiseId
        {
            get { return fLoanMerchandiseId; }
            set { SetPropertyValue<InventoryLoanMerchandiseXpo>("LoanMerchandiseId", ref fLoanMerchandiseId, value); }
        }
        int fProductId;
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
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
        #endregion 

        #region Builder
        public InventoryLoanMerchandiseDetailXpo(Session session) : base(session) { }
        #endregion
    }

}
