using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.WarehouseUser")]
    public class InventoryWarehouseUserReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryWarehouseUserReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        int fUserId;
        public int UserId
        {
            get { return fUserId; }
            set { SetPropertyValue<int>("UserId", ref fUserId, value); }
        }
        string fUserCode;
        [Size(20)]
        public string UserCode
        {
            get { return fUserCode; }
            set { SetPropertyValue<string>("UserCode", ref fUserCode, value); }
        }

        public InventoryWarehouseUserReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
