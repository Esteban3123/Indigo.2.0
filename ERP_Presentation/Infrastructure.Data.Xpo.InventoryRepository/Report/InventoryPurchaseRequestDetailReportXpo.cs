using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository.Report
{
    [Persistent(@"Inventory.PurchaseRequestDetail")]
    public class InventoryPurchaseRequestDetailReportXpo : XPLiteObject
    {

        #region "Properties"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        InventoryPurchaseRequestReportXpo fPurchaseRequestId;
        [Association(@"Inventory_PurchaseRequestDetailReferencesInventory_PurchaseRequest")]
        public InventoryPurchaseRequestReportXpo PurchaseRequestId
        {
            get { return fPurchaseRequestId; }
            set { SetPropertyValue<InventoryPurchaseRequestReportXpo>("PurchaseRequestId", ref fPurchaseRequestId, value); }
        }

        InventoryProductReportXpo fInventoryProductId;
        [Association(@"Inventory_PurchaseRequestDetailReferencesInventory_InventoryProduct")]
        public InventoryProductReportXpo InventoryProductId
        {
            get { return fInventoryProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("InventoryProductId", ref fInventoryProductId, value); }
        }

        FixedAssetFixedAssetItemReportXpo fFixedAssetItemId;
        [Association(@"Inventory_PurchaseRequestDetailReferencesFixedAsset_FixedAssetItem")]
        public FixedAssetFixedAssetItemReportXpo FixedAssetItemId
        {
            get { return fFixedAssetItemId; }
            set { SetPropertyValue<FixedAssetFixedAssetItemReportXpo>("FixedAssetItemId", ref fFixedAssetItemId, value); }
        }

        string fOtherRequest;
        public string OtherRequest
        {
            get { return fOtherRequest; }
            set { SetPropertyValue<string>("OtherRequest", ref fOtherRequest, value); }
        }

        FixedAssetTrademarkReportXpo fTrademarkId;
        [Association(@"Inventory_PurchaseRequestDetailReferencesFixedAsset_FixedAssetTrademark")]
        public FixedAssetTrademarkReportXpo TrademarkId
        {
            get { return fTrademarkId; }
            set { SetPropertyValue<FixedAssetTrademarkReportXpo>("TrademarkId", ref fTrademarkId, value); }
        }

        string fModel;
        public string Model
        {
            get { return fModel; }
            set { SetPropertyValue<string>("Model", ref fModel, value); }
        }

        InventoryMeasurementUnitReportXpo fMeasurementUnitId;
        [Association(@"InventoryRequestDetailReferencesInventory_MeasurementUnit")]
        public InventoryMeasurementUnitReportXpo MeasurementUnitId
        {
            get { return fMeasurementUnitId; }
            set { SetPropertyValue<InventoryMeasurementUnitReportXpo>("MeasurementUnitId", ref fMeasurementUnitId, value); }
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
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        string fApproveObservation;
        public string ApproveObservation
        {
            get { return fApproveObservation; }
            set { SetPropertyValue<string>("ApproveObservation", ref fApproveObservation, value); }
        }

        string fApproveUser;
        public string ApproveUser
        {
            get { return fApproveUser; }
            set { SetPropertyValue<string>("ApproveUser", ref fApproveUser, value); }
        }

        DateTime fApproveDate;
        public DateTime ApproveDate
        {
            get { return fApproveDate; }
            set { SetPropertyValue<DateTime>("ApproveDate", ref fApproveDate, value); }
        }

        #endregion

        #region "Builder"

        public InventoryPurchaseRequestDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion
    }
}
