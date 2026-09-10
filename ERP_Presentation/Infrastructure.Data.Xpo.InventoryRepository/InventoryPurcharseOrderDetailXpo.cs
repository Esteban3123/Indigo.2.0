using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PurchaseOrderDetail")]
    public class InventoryPurcharseOrderDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryPurchaseOrderXpo fPurchaseOrderId;
        [Association(@"InventoryPurcharseOrderDetailXpoReferencesInventoryPurchaseOrderXpo")]
        public InventoryPurchaseOrderXpo PurchaseOrderId
        {
            get { return fPurchaseOrderId; }
            set { SetPropertyValue<InventoryPurchaseOrderXpo>("PurchaseOrderId", ref fPurchaseOrderId, value); }
        }
        InventoryProductXpo fProductId;
        [Association(@"InventoryPurcharseOrderDetailXpoReferencesInventoryProductXpo")]
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

        [NonPersistent]
        public int DevolutionQuantity;

        [Association(@"InventoryRemissionEntranceDetailXpoReferencesInventoryPurcharseOrderDetailXpo", typeof(InventoryRemissionEntranceDetailXpo))]
        public XPCollection<InventoryRemissionEntranceDetailXpo> InventoryRemissionEntranceDetailXpo { get { return GetCollection<InventoryRemissionEntranceDetailXpo>("InventoryRemissionEntranceDetailXpo"); } }
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesInventoryPurcharseOrderDetailXpo", typeof(ConsignmentInventoryRemissionDetailXpo))]
        public XPCollection<ConsignmentInventoryRemissionDetailXpo> ConsignmentInventoryRemissionDetailXpo { get { return GetCollection<ConsignmentInventoryRemissionDetailXpo>("ConsignmentInventoryRemissionDetailXpo"); } }

        public InventoryPurcharseOrderDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
