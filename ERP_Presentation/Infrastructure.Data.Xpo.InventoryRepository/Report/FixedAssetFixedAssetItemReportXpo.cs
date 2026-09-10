using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository.Report
{
    [Persistent(@"FixedAsset.FixedAssetItem")]
    public class FixedAssetFixedAssetItemReportXpo : XPLiteObject
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

        string fDescription;
        [Size(20)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        #endregion

        #region "Navigation Properties"

        [Association(@"Inventory_PurchaseRequestDetailReferencesFixedAsset_FixedAssetItem", typeof(InventoryPurchaseRequestDetailReportXpo))]
        public XPCollection<InventoryPurchaseRequestDetailReportXpo> Inventory_PurchaseRequestDetails { get { return GetCollection<InventoryPurchaseRequestDetailReportXpo>("Inventory_PurchaseRequestDetails"); } }

        #endregion

        #region "Builder"

        public FixedAssetFixedAssetItemReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion

    }
}
