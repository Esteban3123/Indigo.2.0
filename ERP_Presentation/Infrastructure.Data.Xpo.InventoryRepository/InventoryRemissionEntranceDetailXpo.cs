using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.RemissionEntranceDetail")]
    public class InventoryRemissionEntranceDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        RemissionEntranceXpo fRemissionEntranceId;
        [Association(@"InventoryRemissionEntranceDetailXpoReferencesRemissionEntranceXpo")]
        public RemissionEntranceXpo RemissionEntranceId
        {
            get { return fRemissionEntranceId; }
            set { SetPropertyValue<RemissionEntranceXpo>("RemissionEntranceId", ref fRemissionEntranceId, value); }
        }
        byte fRemissionSource;
        public byte RemissionSource
        {
            get { return fRemissionSource; }
            set { SetPropertyValue<byte>("RemissionSource", ref fRemissionSource, value); }
        }
        string fSourceCode;
        [Size(20)]
        public string SourceCode
        {
            get { return fSourceCode; }
            set { SetPropertyValue<string>("SourceCode", ref fSourceCode, value); }
        }
        InventoryPurcharseOrderDetailXpo fPurchaseOrderDetailId;
        [Association(@"InventoryRemissionEntranceDetailXpoReferencesInventoryPurcharseOrderDetailXpo")]
        public InventoryPurcharseOrderDetailXpo PurchaseOrderDetailId
        {
            get { return fPurchaseOrderDetailId; }
            set { SetPropertyValue<InventoryPurcharseOrderDetailXpo>("PurchaseOrderDetailId", ref fPurchaseOrderDetailId, value); }
        }
        InventoryContractDetailXpo fContractDetailId;
        [Association(@"InventoryRemissionEntranceDetailXpoReferencesInventoryContractDetailXpo")]
        public InventoryContractDetailXpo ContractDetailId
        {
            get { return fContractDetailId; }
            set { SetPropertyValue<InventoryContractDetailXpo>("ContractDetailId", ref fContractDetailId, value); }
        }
        InventoryProductXpo fProductId;
        [Association(@"InventoryRemissionEntranceDetailXpoReferencesInventoryProductXpo")]
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
        decimal fLastValue;
        public decimal LastValue
        {
            get { return fLastValue; }
            set { SetPropertyValue<decimal>("LastValue", ref fLastValue, value); }
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
        [PersistentAlias("RemissionEntranceId.CurrencyAbbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        [PersistentAlias("RemissionEntranceId.CurrencyId")]
        public int CurrencyId
        {
            get { return Convert.ToInt32(EvaluateAlias("CurrencyId")); }

        }

        [Association(@"InventoryRemissionEntranceDetailBatchSerialXpoReferencesInventoryRemissionEntranceDetailXpo", typeof(InventoryRemissionEntranceDetailBatchSerialXpo))]
        public XPCollection<InventoryRemissionEntranceDetailBatchSerialXpo> InventoryRemissionEntranceDetailBatchSerialXpo { get { return GetCollection<InventoryRemissionEntranceDetailBatchSerialXpo>("InventoryRemissionEntranceDetailBatchSerialXpo"); } }

        public InventoryRemissionEntranceDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
