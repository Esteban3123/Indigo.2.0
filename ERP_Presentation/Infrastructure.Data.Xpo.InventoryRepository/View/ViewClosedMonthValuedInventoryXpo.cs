using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewClosedMonthValuedInventory")]
    public class ViewClosedMonthValuedInventoryXpo : XPLiteObject
    {
        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        int fYear;
        public int Year
        {
            get { return fYear; }
            set { SetPropertyValue<int>("Year", ref fYear, value); }
        }

        int fMonth;
        public int Month
        {
            get { return fMonth; }
            set { SetPropertyValue<int>("Month", ref fMonth, value); }
        }
        string fCodeNameWarehouse;
        public string CodeNameWarehouse
        {
            get { return fCodeNameWarehouse; }
            set { SetPropertyValue<string>("CodeNameWarehouse", ref fCodeNameWarehouse, value); }
        }

        string fCodeProduct;
        public string CodeProduct
        {
            get { return fCodeProduct; }
            set { SetPropertyValue<string>("CodeProduct", ref fCodeProduct, value); }
        }

        string fNameProduct;
        public string NameProduct
        {
            get { return fNameProduct; }
            set { SetPropertyValue<string>("NameProduct", ref fNameProduct, value); }
        }

        decimal fQuantity;
        public decimal Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<decimal>("Quantity", ref fQuantity, value); }
        }

        decimal fFinalProductCost;
        public decimal FinalProductCost
        {
            get { return fFinalProductCost; }
            set { SetPropertyValue<decimal>("FinalProductCost", ref fFinalProductCost, value); }
        }

        decimal fTotalProductCost;
        public decimal TotalProductCost
        {
            get { return fTotalProductCost; }
            set { SetPropertyValue<decimal>("TotalProductCost", ref fTotalProductCost, value); }
        }
        #endregion

        #region Builders

        public ViewClosedMonthValuedInventoryXpo(Session session) : base(session)
        {
        }

        public ViewClosedMonthValuedInventoryXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
