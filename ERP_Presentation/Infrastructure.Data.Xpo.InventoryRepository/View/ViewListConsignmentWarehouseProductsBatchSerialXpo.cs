using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewListConsignmentWarehouseProductsBatchSerial")]
   public partial class ViewListConsignmentWarehouseProductsBatchSerialXpo : XPLiteObject
    {
        string fBatchCode;
        [Key(true)]
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }


        int fProductId;
        
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
        }


        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
     

        int fQuantityKardex;
        public int QuantityKardex
        {
            get { return fQuantityKardex; }
            set { SetPropertyValue<int>("QuantityKardex", ref fQuantityKardex, value); }
        }

        int fBatchSerialId;
        public int BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<int>("BatchSerialId", ref fBatchSerialId, value); }
        }

        int fQuantityDecrease;
        public int QuantityDecrease
        {
            get { return fQuantityDecrease; }
            set { SetPropertyValue<int>("QuantityDecrease", ref fQuantityDecrease, value); }
        }

        public ViewListConsignmentWarehouseProductsBatchSerialXpo(Session session) : base(session) { }
        
    }
}
