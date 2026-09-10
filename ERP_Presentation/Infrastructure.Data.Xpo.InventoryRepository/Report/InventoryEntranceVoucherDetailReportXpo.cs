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
    [Persistent(@"Inventory.EntranceVoucherDetail")]
    public class InventoryEntranceVoucherDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryEntranceVoucherReportXpo fEntranceVoucherId;
        [Association(@"Inventory_EntranceVoucherDetailReferencesInventory_EntranceVoucher")]
        public InventoryEntranceVoucherReportXpo EntranceVoucherId
        {
            get { return fEntranceVoucherId; }
            set { SetPropertyValue<InventoryEntranceVoucherReportXpo>("EntranceVoucherId", ref fEntranceVoucherId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"Inventory_EntranceVoucherDetailReferencesInventoryProductReportXpo")]
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
        byte fEntranceSource;
        public byte EntranceSource
        {
            get { return fEntranceSource; }
            set { SetPropertyValue<byte>("EntranceSource", ref fEntranceSource, value); }
        }
        string fSourceCode;
        [Size(20)]
        public string SourceCode
        {
            get { return fSourceCode; }
            set { SetPropertyValue<string>("SourceCode", ref fSourceCode, value); }
        }
        int fPurchaseOrderDetailId;
        public int PurchaseOrderDetailId
        {
            get { return fPurchaseOrderDetailId; }
            set { SetPropertyValue<int>("PurchaseOrderDetailId", ref fPurchaseOrderDetailId, value); }
        }
        int fContractDetailId;
        public int ContractDetailId
        {
            get { return fContractDetailId; }
            set { SetPropertyValue<int>("ContractDetailId", ref fContractDetailId, value); }
        }
        int fRemissionEntranceDetailBatchSerialId;
        public int RemissionEntranceDetailBatchSerialId
        {
            get { return fRemissionEntranceDetailBatchSerialId; }
            set { SetPropertyValue<int>("RemissionEntranceDetailBatchSerialId", ref fRemissionEntranceDetailBatchSerialId, value); }
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
        [Association(@"Inventory_EntranceVoucherDetailBatchSerialReferencesInventory_EntranceVoucherDetail", typeof(InventoryEntranceVoucherDetailBatchSerialReportXpo))]
        public XPCollection<InventoryEntranceVoucherDetailBatchSerialReportXpo> Inventory_EntranceVoucherDetailBatchSerials { get { return GetCollection<InventoryEntranceVoucherDetailBatchSerialReportXpo>("Inventory_EntranceVoucherDetailBatchSerials"); } }

        public InventoryEntranceVoucherDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
