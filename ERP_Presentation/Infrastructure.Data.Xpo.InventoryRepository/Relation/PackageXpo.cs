using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("MixingStation.Package")]
    public class PackageXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        private string fCode;
        public string Code
        {
            get => fCode;
            set { SetPropertyValue("Code", ref fCode, value); }
        }

        private string fName;
        public string Name
        {
            get => fName;
            set { SetPropertyValue("Name", ref fName, value); }
        }

        private int? fATCId;
        public int? ATCId
        {
            get => fATCId;
            set { SetPropertyValue("ATCId", ref fATCId, value); }
        }

        [Association("ProductRateDetail_References_Package", typeof(ProductRateDetailXpo))]
        public XPCollection<ProductRateDetailXpo> ProductRateDetails { get { return GetCollection<ProductRateDetailXpo>("ProductRateDetails"); } }

        public PackageXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
