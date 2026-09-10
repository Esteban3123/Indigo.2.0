using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewReportPhysicalInventoryByColumns")]
    public class InventoryViewReportPhysicalInventoryByColumnsReportXpo : XPLiteObject
    {
        long fId;
        [Key(true)]
        public long Id
        {
            get { return fId; }
            set { SetPropertyValue<long>("Id", ref fId, value); }
        }
        string fWarehouseIdCode;
        [Size(20)]
        public string WarehouseIdCode
        {
            get { return fWarehouseIdCode; }
            set { SetPropertyValue<string>("WarehouseIdCode", ref fWarehouseIdCode, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fBatchCode;
        [Size(50)]
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        string fProductIdCode;
        [Size(20)]
        public string ProductIdCode
        {
            get { return fProductIdCode; }
            set { SetPropertyValue<string>("ProductIdCode", ref fProductIdCode, value); }
        }
        string fProductIdName;
        [Size(200)]
        public string ProductIdName
        {
            get { return fProductIdName; }
            set { SetPropertyValue<string>("ProductIdName", ref fProductIdName, value); }
        }
        string fProduct;
        [Size(223)]
        public string Product
        {
            get { return fProduct; }
            set { SetPropertyValue<string>("Product", ref fProduct, value); }
        }
        string fMeasurementUnitIdName;
        public string MeasurementUnitIdName
        {
            get { return fMeasurementUnitIdName; }
            set { SetPropertyValue<string>("MeasurementUnitIdName", ref fMeasurementUnitIdName, value); }
        }
        string fPharmaceuticalFormIdName;
        public string PharmaceuticalFormIdName
        {
            get { return fPharmaceuticalFormIdName; }
            set { SetPropertyValue<string>("PharmaceuticalFormIdName", ref fPharmaceuticalFormIdName, value); }
        }
        int fProductId;
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
        }


        public InventoryViewReportPhysicalInventoryByColumnsReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
