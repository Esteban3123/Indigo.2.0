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
    [Persistent(@"Inventory.ConsignmentInventoryRemissionDetail")]
    public class InventoryConsignmentInventoryRemissionDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryConsignmentInventoryRemissionReportXpo fConsignmentInventoryRemissionId;
        [Association(@"Inventory_ConsignmentInventoryRemissionDetailReferencesInventory_ConsignmentInventoryRemission")]
        public InventoryConsignmentInventoryRemissionReportXpo ConsignmentInventoryRemissionId
        {
            get { return fConsignmentInventoryRemissionId; }
            set { SetPropertyValue<InventoryConsignmentInventoryRemissionReportXpo>("ConsignmentInventoryRemissionId", ref fConsignmentInventoryRemissionId, value); }
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
        [Association(@"Inventory_ConsignmentInventoryRemissionDetailReferencesInventory_PurchaseOrderDetail")]
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
        [Association(@"Inventory_ConsignmentInventoryRemissionDetailBatchSerialReferencesInventory_ConsignmentInventoryRemissionDetail", typeof(InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo))]
        public XPCollection<InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo> Inventory_ConsignmentInventoryRemissionDetailBatchSerials { get { return GetCollection<InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo>("Inventory_ConsignmentInventoryRemissionDetailBatchSerials"); } }

        public InventoryConsignmentInventoryRemissionDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
