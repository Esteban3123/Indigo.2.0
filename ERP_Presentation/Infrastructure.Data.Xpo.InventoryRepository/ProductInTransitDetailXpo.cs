using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ProductInTransitDetail")]
    public class InventoryProductInTransitDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        ProductInTransitXpo fProductInTransitId;
        [Association(@"InventoryProductInTransitDetailXpoReferencesProductInTransitXpo")]
        public ProductInTransitXpo ProductInTransitId
        {
            get { return fProductInTransitId; }
            set { SetPropertyValue<ProductInTransitXpo>("ProductInTransitId", ref fProductInTransitId, value); }
        }
        
        InventoryProductXpo fProductId;
        [Association(@"InventoryProductInTransitDetailXpoReferencesInventoryProductXpo")]
        public InventoryProductXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductXpo>("ProductId", ref fProductId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        decimal fUnitValue;
        public decimal UnitValue
        {
            get { return fUnitValue; }
            set { SetPropertyValue<decimal>("UnitValue", ref fUnitValue, value); }
        }
        
        decimal fSubTotalValue;
        public decimal SubTotalValue
        {
            get { return fSubTotalValue; }
            set { SetPropertyValue<decimal>("SubTotalValue", ref fSubTotalValue, value); }
        }
        decimal fIvaPercentage;
        public decimal IvaPercentage
        {
            get { return fIvaPercentage; }
            set { SetPropertyValue<decimal>("IvaPercentage", ref fIvaPercentage, value); }
        }
        decimal fIvaValue;
        public decimal IvaValue
        {
            get { return fIvaValue; }
            set { SetPropertyValue<decimal>("IvaValue", ref fIvaValue, value); }
        }
        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
        }
        decimal fGrossUnitValue;
        public decimal GrossUnitValue
        {
            get { return fGrossUnitValue; }
            set { SetPropertyValue<decimal>("GrossUnitValue", ref fGrossUnitValue, value); }
        }
        decimal fDiscountPercentage;
        public decimal DiscountPercentage
        {
            get { return fDiscountPercentage; }
            set { SetPropertyValue<decimal>("DiscountPercentage", ref fDiscountPercentage, value); }
        }
        decimal fNetDiscount;
        public decimal NetDiscount
        {
            get { return fNetDiscount; }
            set { SetPropertyValue<decimal>("NetDiscount", ref fNetDiscount, value); }
        }
        [PersistentAlias("ProductInTransitId.CurrencyAbbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        [PersistentAlias("ProductInTransitId.CurrencyId")]
        public int CurrencyId
        {
            get { return Convert.ToInt32(EvaluateAlias("CurrencyId")); }

        }

        public InventoryProductInTransitDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
