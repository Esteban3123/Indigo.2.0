using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.DocumentInvoiceProductSalesDetail")]
    public class InventoryDocumentInvoiceProductSalesDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryDocumentInvoiceProductSalesReportXpo fDocumentInvoiceProductSalesId;
        [Association(@"Inventory_DocumentInvoiceProductSalesDetailReferencesInventory_DocumentInvoiceProductSales")]
        public InventoryDocumentInvoiceProductSalesReportXpo DocumentInvoiceProductSalesId
        {
            get { return fDocumentInvoiceProductSalesId; }
            set { SetPropertyValue<InventoryDocumentInvoiceProductSalesReportXpo>("DocumentInvoiceProductSalesId", ref fDocumentInvoiceProductSalesId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"InventoryDocumentInvoiceProductSalesDetailReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        bool fHandlesBatch;
        public bool HandlesBatch
        {
            get { return fHandlesBatch; }
            set { SetPropertyValue<bool>("HandlesBatch", ref fHandlesBatch, value); }
        }
        decimal fSalePrice;
        public decimal SalePrice
        {
            get { return fSalePrice; }
            set { SetPropertyValue<decimal>("SalePrice", ref fSalePrice, value); }
        }
        decimal fSubTotalValue;
        public decimal SubTotalValue
        {
            get { return fSubTotalValue; }
            set { SetPropertyValue<decimal>("SubTotalValue", ref fSubTotalValue, value); }
        }
        decimal fDiscountPercentage;
        public decimal DiscountPercentage
        {
            get { return fDiscountPercentage; }
            set { SetPropertyValue<decimal>("DiscountPercentage", ref fDiscountPercentage, value); }
        }
        decimal fDiscountValue;
        public decimal DiscountValue
        {
            get { return fDiscountValue; }
            set { SetPropertyValue<decimal>("DiscountValue", ref fDiscountValue, value); }
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
        decimal fRTFPercentage;
        public decimal RTFPercentage
        {
            get { return fRTFPercentage; }
            set { SetPropertyValue<decimal>("RTFPercentage", ref fRTFPercentage, value); }
        }
        decimal fRTFValue;
        public decimal RTFValue
        {
            get { return fRTFValue; }
            set { SetPropertyValue<decimal>("RTFValue", ref fRTFValue, value); }
        }
        decimal fWithholdingICA;
        public decimal WithholdingICA
        {
            get { return fWithholdingICA; }
            set { SetPropertyValue<decimal>("WithholdingICA", ref fWithholdingICA, value); }
        }
        decimal fWithholdingTax;
        public decimal WithholdingTax
        {
            get { return fWithholdingTax; }
            set { SetPropertyValue<decimal>("WithholdingTax", ref fWithholdingTax, value); }

        }
        decimal fDistrictTax;
        public decimal DistrictTax
        {
            get { return fDistrictTax; }
            set { SetPropertyValue<decimal>("DistrictTax", ref fDistrictTax, value); }

        }
        [Association(@"Inventory_DocumentInvoiceProductSalesDetailBatchSerialReferencesInventory_DocumentInvoiceProductSalesDetail", typeof(InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo> Inventory_DocumentInvoiceProductSalesDetailBatchSerials { get { return GetCollection<InventoryDocumentInvoiceProductSalesDetailBatchSerialReportXpo>("Inventory_DocumentInvoiceProductSalesDetailBatchSerials"); } }

        public InventoryDocumentInvoiceProductSalesDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
