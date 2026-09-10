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
    [Persistent("Inventory.ViewReportWarehousesProducts")]
    public class InventoryViewReportWarehousesProductsReportXpo : XPLiteObject
    {
        long fRow;
        [Key(true)]
        public long Row
        {
            get { return fRow; }
            set { SetPropertyValue<long>("Row", ref fRow, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fWarehouseIdCode;
        [Size(20)]
        public string WarehouseIdCode
        {
            get { return fWarehouseIdCode; }
            set { SetPropertyValue<string>("WarehouseIdCode", ref fWarehouseIdCode, value); }
        }



        public InventoryViewReportWarehousesProductsReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
