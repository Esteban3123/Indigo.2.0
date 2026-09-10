using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository.Report
{
    [Persistent(@"FixedAsset.FixedAssetTrademark")]
    public class FixedAssetTrademarkReportXpo : XPLiteObject
    {
        #region "Properties"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        [Size(20)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        #endregion

        #region "Navigation Properties"

        [Association(@"Inventory_PurchaseRequestDetailReferencesFixedAsset_FixedAssetTrademark", typeof(InventoryPurchaseRequestDetailReportXpo))]
        public XPCollection<InventoryPurchaseRequestDetailReportXpo> Inventory_PurchaseRequestDetails { get { return GetCollection<InventoryPurchaseRequestDetailReportXpo>("Inventory_PurchaseRequestDetails"); } }

        #endregion

        #region "Builder"

        public FixedAssetTrademarkReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion

    }
}
