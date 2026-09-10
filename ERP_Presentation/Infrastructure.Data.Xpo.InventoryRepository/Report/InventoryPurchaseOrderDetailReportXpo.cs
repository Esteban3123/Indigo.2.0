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
    [Persistent(@"Inventory.PurchaseOrderDetail")]
    public class InventoryPurchaseOrderDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryPurchaseOrderReportXpo fPurchaseOrderId;
        [Association(@"Inventory_PurchaseOrderDetailReferencesInventory_PurchaseOrder")]
        public InventoryPurchaseOrderReportXpo PurchaseOrderId
        {
            get { return fPurchaseOrderId; }
            set { SetPropertyValue<InventoryPurchaseOrderReportXpo>("PurchaseOrderId", ref fPurchaseOrderId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"Inventory_PurchaseOrderDetailReferencesInventory_InventoryProduct")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        ProductHierarchyXpo fProductHierarchyId;
        [Association(@"Inventory_PurchaseOrderDetailReferencesInventory_ProductHierarchyId")]
        public ProductHierarchyXpo ProductHierarchyId
        {
            get { return fProductHierarchyId; }
            set { SetPropertyValue<ProductHierarchyXpo>("ProductHierarchyId", ref fProductHierarchyId, value); }
        }
        int fHierarchyQuantity;
        public int HierarchyQuantity
        {
            get { return fHierarchyQuantity; }
            set { SetPropertyValue<int>("HierarchyQuantity", ref fHierarchyQuantity, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        int fCancelledQuantity;
        public int CancelledQuantity
        {
            get { return fCancelledQuantity; }
            set { SetPropertyValue<int>("CancelledQuantity", ref fCancelledQuantity, value); }
        }
        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
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
        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
        }

        [PersistentAlias("PurchaseOrderId.CurrencyAbbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        [Association(@"Inventory_RemissionEntranceDetailReferencesInventory_PurchaseOrderDetail", typeof(InventoryRemissionEntranceDetailReportXpo))]
        public XPCollection<InventoryRemissionEntranceDetailReportXpo> Inventory_RemissionEntranceDetails { get { return GetCollection<InventoryRemissionEntranceDetailReportXpo>("Inventory_RemissionEntranceDetails"); } }
        [Association(@"Inventory_ConsignmentInventoryRemissionDetailReferencesInventory_PurchaseOrderDetail", typeof(InventoryConsignmentInventoryRemissionDetailReportXpo))]
        public XPCollection<InventoryConsignmentInventoryRemissionDetailReportXpo> Inventory_ConsignmentInventoryRemissionDetails { get { return GetCollection<InventoryConsignmentInventoryRemissionDetailReportXpo>("Inventory_ConsignmentInventoryRemissionDetails"); } }
        [Association(@"Inventory_PurchaseOrderDevolutionDetailReferencesInventory_PurchaseOrderDetail", typeof(InventoryPurchaseOrderDevolutionDetailReportXpo))]
        public XPCollection<InventoryPurchaseOrderDevolutionDetailReportXpo> Inventory_PurchaseOrderDevolutionDetails { get { return GetCollection<InventoryPurchaseOrderDevolutionDetailReportXpo>("Inventory_PurchaseOrderDevolutionDetails"); } }

        public InventoryPurchaseOrderDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
