using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewPurchaseRequestToOrder")]
    public class ViewPurchaseRequestToOrderXpo : XPLiteObject
    {
        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fPurchaseRequestId;
        public int PurchaseRequestId
        {
            get { return fPurchaseRequestId; }
            set { SetPropertyValue<int>("PurchaseRequestId", ref fPurchaseRequestId, value); }
        }

        string fPurchaseRequestCode;
        public string PurchaseRequestCode
        {
            get { return fPurchaseRequestCode; }
            set { SetPropertyValue<string>("PurchaseRequestCode", ref fPurchaseRequestCode, value); }
        }

        int fInventoryProductId;
        public int InventoryProductId
        {
            get { return fInventoryProductId; }
            set { SetPropertyValue<int>("InventoryProductId", ref fInventoryProductId, value); }
        }

        string fProductCode;
        public string ProductCode
        {
            get { return fProductCode; }
            set { SetPropertyValue<string>("ProductCode", ref fProductCode, value); }
        }

        string fProductName;
        public string ProductName
        {
            get { return fProductName; }
            set { SetPropertyValue<string>("ProductName", ref fProductName, value); }
        }

        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }

        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }

        #endregion

        #region Builders

        public ViewPurchaseRequestToOrderXpo(Session session) : base(session)
        {
        }

        public ViewPurchaseRequestToOrderXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
