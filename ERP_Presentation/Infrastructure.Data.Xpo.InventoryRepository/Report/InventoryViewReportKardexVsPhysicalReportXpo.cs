using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewReportKardexVsPhysical")]
    public class InventoryViewReportKardexVsPhysicalReportXpo : XPLiteObject
    {
        #region "Members"

        string fId;
        [Key(true)]
        public string Id
        {
            get { return fId; }
            set { SetPropertyValue<string>("Id", ref fId, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }

        string fWarehouseCode;
        public string WarehouseCode
        {
            get { return fWarehouseCode; }
            set { SetPropertyValue<string>("WarehouseCode", ref fWarehouseCode, value); }
        }

        string fWarehouseName;
        public string WarehouseName
        {
            get { return fWarehouseName; }
            set { SetPropertyValue<string>("WarehouseName", ref fWarehouseName, value); }
        }

        int fProductId;
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
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

        int fPhysicalQuantity;
        public int PhysicalQuantity
        {
            get { return fPhysicalQuantity; }
            set { SetPropertyValue<int>("PhysicalQuantity", ref fPhysicalQuantity, value); }
        }

        int fKardexQuantity;
        public int KardexQuantity
        {
            get { return fKardexQuantity; }
            set { SetPropertyValue<int>("KardexQuantity", ref fKardexQuantity, value); }
        }

        #endregion

        #region "Builders"

        public InventoryViewReportKardexVsPhysicalReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion
    }
}
