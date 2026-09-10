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
    [Persistent(@"Inventory.RemissionEntranceDetail")]
    public class InventoryRemissionEntranceDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRemissionEntranceReportXpo fRemissionEntranceId;
        [Association(@"Inventory_RemissionEntranceDetailReferencesInventory_RemissionEntrance")]
        public InventoryRemissionEntranceReportXpo RemissionEntranceId
        {
            get { return fRemissionEntranceId; }
            set { SetPropertyValue<InventoryRemissionEntranceReportXpo>("RemissionEntranceId", ref fRemissionEntranceId, value); }
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
        InventoryPurchaseOrderDetailReportXpo fPurchaseOrderDetailId;
        [Association(@"Inventory_RemissionEntranceDetailReferencesInventory_PurchaseOrderDetail")]
        public InventoryPurchaseOrderDetailReportXpo PurchaseOrderDetailId
        {
            get { return fPurchaseOrderDetailId; }
            set { SetPropertyValue<InventoryPurchaseOrderDetailReportXpo>("PurchaseOrderDetailId", ref fPurchaseOrderDetailId, value); }
        }
        int fContractDetailId;
        public int ContractDetailId
        {
            get { return fContractDetailId; }
            set { SetPropertyValue<int>("ContractDetailId", ref fContractDetailId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"Inventory_RemissionEntranceDetailReferencesInventoryProductReportXpo")]
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
        decimal fNetDiscount;
        public decimal NetDiscount
        {
            get { return fNetDiscount; }
            set { SetPropertyValue<decimal>("NetDiscount", ref fNetDiscount, value); }
        }

        decimal fDiscountPercentage;
        public decimal DiscountPercentage
        {
            get { return fDiscountPercentage; }
            set { SetPropertyValue<decimal>("DiscountPercentage", ref fDiscountPercentage, value); }
        }

        decimal fGrossUnitValue;
        public decimal GrossUnitValue
        {
            get { return fGrossUnitValue; }
            set { SetPropertyValue<decimal>("GrossUnitValue", ref fGrossUnitValue, value); }
        }

        [Association(@"Inventory_RemissionEntranceDetailBatchSerialReferencesInventory_RemissionEntranceDetail", typeof(InventoryRemissionEntranceDetailBatchSerialReportXpo))]
        public XPCollection<InventoryRemissionEntranceDetailBatchSerialReportXpo> Inventory_RemissionEntranceDetailBatchSerials { get { return GetCollection<InventoryRemissionEntranceDetailBatchSerialReportXpo>("Inventory_RemissionEntranceDetailBatchSerials"); } }

        public InventoryRemissionEntranceDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
