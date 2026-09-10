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
    [Persistent(@"Inventory.ProductType")]
    public class InventoryProductTypeReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        byte fClass;
        public byte Class
        {
            get { return fClass; }
            set { SetPropertyValue<byte>("Class", ref fClass, value); }
        }
        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
        }
        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fModificationUser;
        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }
        DateTime fModificationDate;
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }
        [Association(@"InventoryProductReportXpoReferencesInventoryProductTypeReportXpo", typeof(InventoryProductReportXpo))]
        public XPCollection<InventoryProductReportXpo> InventoryProductReportXpo { get { return GetCollection<InventoryProductReportXpo>("InventoryProductReportXpo"); } }

        [Association(@"InventoryProductType_InventoryProductRateGeneral", typeof(ProductRateGeneralXpo))]
        public XPCollection<ProductRateGeneralXpo> ProductRateGeneralXpo { get { return GetCollection<ProductRateGeneralXpo>("ProductRateGeneralXpo"); } }

        [Association(@"InventoryProductType_WarehouseRestrictedConditionsXpo", typeof(WarehouseRestrictedConditionsXpo))]
        public XPCollection<WarehouseRestrictedConditionsXpo> WarehouseRestrictedConditionsXpo { get { return GetCollection<WarehouseRestrictedConditionsXpo>("WarehouseRestrictedConditionsXpo"); } }


        public InventoryProductTypeReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
