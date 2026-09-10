
using DevExpress.Xpo;
using Infrastructure.Data.Xpo.FixedAssetRepository;
using Infrastructure.Data.Xpo.InteropCostRepository;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PurchaseRequestDetail")]
    public class PurchaseRequestDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        PurchaseRequestXpo fPurchaseRequestId;
        [Association(@"PurchaseRequestDetailXpoReferencesPurchaseRequestXpo")]
        public PurchaseRequestXpo PurchaseRequestId
        {
            get { return fPurchaseRequestId; }
            set { SetPropertyValue<PurchaseRequestXpo>("PurchaseRequestId", ref fPurchaseRequestId, value); }
        }
        InventoryProductXpo fInventoryProductId;
        [Association(@"PurchaseRequestDetailXpoReferencesInventoryProductXpo")]
        public InventoryProductXpo InventoryProductId
        {
            get { return fInventoryProductId; }
            set { SetPropertyValue<InventoryProductXpo>("InventoryProductId", ref fInventoryProductId, value); }
        }
        FixedAssetEquipmentXpo fFixedAssetItemId;
        [Association(@"PurchaseRequestDetailXpoReferencesFixedAssetItemXpo")]
        public FixedAssetEquipmentXpo FixedAssetItemId
        {
            get { return fFixedAssetItemId; }
            set { SetPropertyValue<FixedAssetEquipmentXpo>("FixedAssetItemId", ref fFixedAssetItemId, value); }
        }
        string fOtherRequest;
        [Size(300)]
        public string OtherRequest
        {
            get { return fOtherRequest; }
            set { SetPropertyValue<string>("OtherRequest", ref fOtherRequest, value); }
        }
        FixedAssetTrademarkXpo fTrademarkId;
        [Association(@"PurchaseRequestDetailXpoReferencesTrademarkXpo")]
        public FixedAssetTrademarkXpo TrademarkId
        {
            get { return fTrademarkId; }
            set { SetPropertyValue<FixedAssetTrademarkXpo>("TrademarkId", ref fTrademarkId, value); }
        }
        string fModel;
        [Size(100)]
        public string Model
        {
            get { return fModel; }
            set { SetPropertyValue<string>("Model", ref fModel, value); }
        }
        InventoryMeasurementUnitXpo fMeasurementUnitId;
        [Association(@"PurchaseRequestDetailXpoReferencesMeasurementUnitXpo")]
        public InventoryMeasurementUnitXpo MeasurementUnitId
        {
            get { return fMeasurementUnitId; }
            set { SetPropertyValue<InventoryMeasurementUnitXpo>("MeasurementUnitId", ref fMeasurementUnitId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        string fDescription;
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        public PurchaseRequestDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
