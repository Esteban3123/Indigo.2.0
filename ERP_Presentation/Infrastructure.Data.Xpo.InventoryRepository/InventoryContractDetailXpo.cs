using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.InventoryContractDetail")]
    public class InventoryContractDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryContractXpo fInventoryContractId;
        [Association(@"InventoryContractDetailXpoReferencesInventoryContractXpo")]
        public InventoryContractXpo InventoryContractId
        {
            get { return fInventoryContractId; }
            set { SetPropertyValue<InventoryContractXpo>("InventoryContractId", ref fInventoryContractId, value); }
        }
        InventoryProductXpo fProductId;
        [Association(@"InventoryContractDetailXpoReferencesInventoryProductXpo")]
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

        [PersistentAlias("InventoryContractId.CurrencyAbbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        [Association(@"InventoryRemissionEntranceDetailXpoReferencesInventoryContractDetailXpo", typeof(InventoryRemissionEntranceDetailXpo))]
        public XPCollection<InventoryRemissionEntranceDetailXpo> InventoryRemissionEntranceDetailXpo { get { return GetCollection<InventoryRemissionEntranceDetailXpo>("InventoryRemissionEntranceDetailXpo"); } }
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesInventoryContractDetailXpo", typeof(ConsignmentInventoryRemissionDetailXpo))]
        public XPCollection<ConsignmentInventoryRemissionDetailXpo> ConsignmentInventoryRemissionDetailXpo { get { return GetCollection<ConsignmentInventoryRemissionDetailXpo>("ConsignmentInventoryRemissionDetailXpo"); } }

        public InventoryContractDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
