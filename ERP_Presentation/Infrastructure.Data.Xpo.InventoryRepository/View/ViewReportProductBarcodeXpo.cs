#region "Imports"

using System;
using DevExpress.Xpo;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewReportProductBarcode")]
    public partial class ViewReportProductBarcodeXpo : XPLiteObject
    {
        #region "Members"

        string fUUID;
        [Key(true)]
        public string UUID
        {
            get { return fUUID; }
            set { SetPropertyValue<string>("UUID", ref fUUID, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }

        string fWarehouseCodeName;
        public string WarehouseCodeName
        {
            get { return fWarehouseCodeName; }
            set { SetPropertyValue<string>("WarehouseCodeName", ref fWarehouseCodeName, value); }
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

        string fProducName;
        public string ProducName
        {
            get { return fProducName; }
            set { SetPropertyValue<string>("ProducName", ref fProducName, value); }
        }

        int? fBatchSerialId;
        public int? BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<int?>("BatchSerialId", ref fBatchSerialId, value); }
        }

        string fBatchCode;
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }

        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }

        string fBarcode;
        public string Barcode
        {
            get { return fBarcode; }
            set { SetPropertyValue<string>("Barcode", ref fBarcode, value); }
        }

        string fCalculatedBarCode;
        public string CalculatedBarCode
        {
            get { return fCalculatedBarCode; }
            set { SetPropertyValue<string>("CalculatedBarCode", ref fCalculatedBarCode, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        #endregion

        #region Builders

        public ViewReportProductBarcodeXpo(Session session) : base(session)
        {
        }

        public ViewReportProductBarcodeXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
