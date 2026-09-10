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
    [Persistent(@"Inventory.RemissionOutputDetail")]
    public class InventoryRemissionOutputDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRemissionOutputReportXpo fRemissionOutputId;
        [Association(@"Inventory_RemissionOutputDetailReferencesInventory_RemissionOutput")]
        public InventoryRemissionOutputReportXpo RemissionOutputId
        {
            get { return fRemissionOutputId; }
            set { SetPropertyValue<InventoryRemissionOutputReportXpo>("RemissionOutputId", ref fRemissionOutputId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"InventoryRemissionOutputDetailReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        string fProductDescription;
        [Size(300)]
        public string ProductDescription
        {
            get { return fProductDescription; }
            set { SetPropertyValue<string>("ProductDescription", ref fProductDescription, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        int fOutstandingAmount;
        public int OutstandingAmount
        {
            get { return fOutstandingAmount; }
            set { SetPropertyValue<int>("OutstandingAmount", ref fOutstandingAmount, value); }
        }
        byte fVariationType;
        public byte VariationType
        {
            get { return fVariationType; }
            set { SetPropertyValue<byte>("VariationType", ref fVariationType, value); }
        }
        decimal fPercentageVariation;
        public decimal PercentageVariation
        {
            get { return fPercentageVariation; }
            set { SetPropertyValue<decimal>("PercentageVariation", ref fPercentageVariation, value); }
        }
        decimal fSalePrice;
        public decimal SalePrice
        {
            get { return fSalePrice; }
            set { SetPropertyValue<decimal>("SalePrice", ref fSalePrice, value); }
        }
        decimal fSalesPriceOriginal;
        public decimal SalesPriceOriginal
        {
            get { return fSalesPriceOriginal; }
            set { SetPropertyValue<decimal>("SalesPriceOriginal", ref fSalesPriceOriginal, value); }
        }
        string fDetailDescription;
        [Size(300)]
        public string DetailDescription
        {
            get { return fDetailDescription; }
            set { SetPropertyValue<string>("DetailDescription", ref fDetailDescription, value); }
        }
        decimal fSalePriceWithDiscount;
        public decimal SalePriceWithDiscount
        {
            get { return fSalePriceWithDiscount; }
            set { SetPropertyValue<decimal>("SalePriceWithDiscount", ref fSalePriceWithDiscount, value); }
        }
        decimal fTotalPriceWithDiscount;
        public decimal TotalPriceWithDiscount
        {
            get { return fTotalPriceWithDiscount; }
            set { SetPropertyValue<decimal>("TotalPriceWithDiscount", ref fTotalPriceWithDiscount, value); }
        }

        [Association(@"InventoryRemissionOutputDetailPhysicalReportXpoReferencesInventoryRemissionOutputDetailReportXpo", typeof(InventoryRemissionOutputDetailPhysicalReportXpo))]
        public XPCollection<InventoryRemissionOutputDetailPhysicalReportXpo> InventoryRemissionOutputDetailPhysicalReportXpo { get { return GetCollection<InventoryRemissionOutputDetailPhysicalReportXpo>("InventoryRemissionOutputDetailPhysicalReportXpo"); } }

        public InventoryRemissionOutputDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
