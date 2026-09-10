using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewListConsignmentWarehouseProducts")]
    public partial class ViewListConsignmentWarehouseProductsXpo : XPLiteObject
    {
        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }


        int fProductId;
        [Key(true)]
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
        }


        string fCodeName;
        public string CodeName
        {
            get { return fCodeName; }
            set { SetPropertyValue<string>("CodeName", ref fCodeName, value); }
        }
              

        int fQuantityMax;
        public int QuantityMax
        {
            get { return fQuantityMax; }
            set { SetPropertyValue<int>("QuantityMax", ref fQuantityMax, value); }
        }

        int fQuantityKardex;
        public int QuantityKardex
        {
            get { return fQuantityKardex; }
            set { SetPropertyValue<int>("QuantityKardex", ref fQuantityKardex, value); }
        }

        int fDecreaseQuantity;
        public int DecreaseQuantity
        {
            get { return fDecreaseQuantity; }
            set { SetPropertyValue<int>("DecreaseQuantity", ref fDecreaseQuantity, value); }
        }

        string fJustificaton;
        public string Justificaton
        {
            get { return fJustificaton; }
            set { SetPropertyValue<string>("Justificaton", ref fJustificaton, value); }
        }

        bool fHandlesBatch;
        public bool HandlesBatch
        {
            get { return fHandlesBatch; }
            set { SetPropertyValue<bool>("HandlesBatch", ref fHandlesBatch, value); }
        }


        public  ViewListConsignmentWarehouseProductsXpo(Session session) : base(session) { }
              
        public ViewListConsignmentWarehouseProductsXpo()
        {
        }
    }
}
