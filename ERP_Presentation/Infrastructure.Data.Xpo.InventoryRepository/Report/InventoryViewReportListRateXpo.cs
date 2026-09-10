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
    [Persistent("Inventory.ViewReportPriceListRate")]

    public class InventoryViewReportListRateXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        String fCodeProduct;
        [Size(20)]
        public String CodeProduct
        {
            get { return fCodeProduct; }
            set { SetPropertyValue<String>("CodeProduct", ref fCodeProduct, value); }
        }
        String fNameProduct;
        [Size(20)]
        public String NameProduct
        {
            get { return fNameProduct; }
            set { SetPropertyValue<String>("NameProduct", ref fNameProduct, value); }
        }
        String fNamePackagin;
        [Size(100)]
        public String NamePackagin
        {
            get { return fNamePackagin; }
            set { SetPropertyValue<String>("NamePackagin", ref fNamePackagin, value); }
        }
        String fCodeGroup;
        [Size(20)]
        public String CodeGroup
        {
            get { return fCodeGroup; }
            set { SetPropertyValue<String>("CodeGroup", ref fCodeGroup, value); }
        }
        String fNameGroup;
        [Size(100)]
        public String NameGroup
        {
            get { return fNameGroup; }
            set { SetPropertyValue<String>("NameGroup", ref fNameGroup, value); }
        }
        String fCodeSubgroup;
        [Size(20)]
        public String CodeSubgroup
        {
            get { return fCodeSubgroup; }
            set { SetPropertyValue<String>("CodeSubgroup", ref fCodeSubgroup, value); }
        }
        String fNameSubGroup;
        [Size(100)]
        public String NameSubGroup
        {
            get { return fNameSubGroup; }
            set { SetPropertyValue<String>("NameSubGroup", ref fNameSubGroup, value); }
        }
        String fRateManual;
        [Size(123)]
        public String RateManual
        {
            get { return fRateManual; }
            set { SetPropertyValue<String>("RateManual", ref fRateManual, value); }
        }
        decimal fSalesValue;
        public decimal SalesValue
        {
            get { return fSalesValue; }
            set { SetPropertyValue<decimal>("SalesValue", ref fSalesValue, value); }
        }
        String fCodeRate;
        public String CodeRate
        {
            get { return fCodeRate; }
            set { SetPropertyValue<String>("CodeRate", ref fCodeRate, value); }
        }

        public InventoryViewReportListRateXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
