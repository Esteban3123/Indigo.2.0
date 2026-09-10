using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"MixingStation.PackageDetail")]
    public class PackageDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        
        [PersistentAlias("InventoryProduct.Id")]
        public int? ProductId
        {
            get => EvaluateAlias("ProductId") as int?;
        }

        [PersistentAlias("InventorySupplie.Id")]
        public int? SupplieId
        {
            get => EvaluateAlias("SupplieId") as int?;
        }

        [PersistentAlias("ATC.Id")]
        public int? AtcId
        {
            get => EvaluateAlias("AtcId") as int?;
        }

        byte fComponentType;
        public byte ComponentType
        {
            get => fComponentType;
            set { SetPropertyValue<byte>("ComponentType", ref fComponentType, value); }
        }

        bool fMainMedicine;
        public bool MainMedicine
        {
            get => fMainMedicine;
            set { SetPropertyValue<bool>("MainMedicine", ref fMainMedicine, value); }
        }

        bool fThinner;
        public bool Thinner
        {
            get => fThinner;
            set { SetPropertyValue<bool>("Thinner", ref fThinner, value); }
        }

        bool fVehicle;
        public bool Vehicle
        {
            get => fVehicle;
            set { SetPropertyValue<bool>("Vehicle", ref fVehicle, value); }
        }

        [PersistentAlias("Iif(ComponentType = 1, ATC.CodeName, Iif(ComponentType = 2, InventorySupplie.CodeName, InventoryProduct.CodeName))")]
        public string ItemCodeName { get => Convert.ToString(EvaluateAlias("ItemCodeName")); }

        private InventoryProductXpo fInventoryProduct;
        [Persistent("ProductId")]
        [Association("PackageDetail_References_Product")]
        public InventoryProductXpo InventoryProduct
        {
            get => fInventoryProduct;
            set { SetPropertyValue("InventoryProduct", ref fInventoryProduct, value); }
        }
        
        private InventorySupplieXpo fInventorySupplie;
        [Persistent("SupplieId")]
        [Association("PackageDetail_References_Supply")]
        public InventorySupplieXpo InventorySupplie
        {
            get => fInventorySupplie;
            set { SetPropertyValue("InventorySupplie", ref fInventorySupplie, value); }
        }

        private ATCXpo fATC;
        [Persistent("AtcId")]
        [Association("PackageDetail_References_Atc")]
        public ATCXpo ATC
        {
            get => fATC;
            set { SetPropertyValue("ATC", ref fATC, value); }
        }

        [Association("ProductRateDetailPackage_References_PackageDetail", typeof(ProductRateDetailPackageXpo))]
        public XPCollection<ProductRateDetailPackageXpo> ProductRateDetailPackages { get { return GetCollection<ProductRateDetailPackageXpo>("ProductRateDetailPackages"); } }

        public PackageDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
