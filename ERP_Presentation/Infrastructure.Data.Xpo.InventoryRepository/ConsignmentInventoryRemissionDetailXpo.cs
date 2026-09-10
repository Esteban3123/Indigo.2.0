using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ConsignmentInventoryRemissionDetail")]
    public class ConsignmentInventoryRemissionDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        ConsignmentInventoryRemissionXpo fConsignmentInventoryRemissionId;
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesConsignmentInventoryRemissionXpo")]
        public ConsignmentInventoryRemissionXpo ConsignmentInventoryRemissionId
        {
            get { return fConsignmentInventoryRemissionId; }
            set { SetPropertyValue<ConsignmentInventoryRemissionXpo>("ConsignmentInventoryRemissionId", ref fConsignmentInventoryRemissionId, value); }
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
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesInventoryPurcharseOrderDetailXpo")]
        public InventoryPurcharseOrderDetailXpo PurchaseOrderDetailId
        {
            get { return fPurchaseOrderDetailId; }
            set { SetPropertyValue<InventoryPurcharseOrderDetailXpo>("PurchaseOrderDetailId", ref fPurchaseOrderDetailId, value); }
        }
        InventoryContractDetailXpo fContractDetailId;
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesInventoryContractDetailXpo")]
        public InventoryContractDetailXpo ContractDetailId
        {
            get { return fContractDetailId; }
            set { SetPropertyValue<InventoryContractDetailXpo>("ContractDetailId", ref fContractDetailId, value); }
        }
        ConsignmentInventoryRemissionDetailXpo fConsignmentInventoryRemissionDetailId;
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesConsignmentInventoryRemissionDetailXpo")]
        public ConsignmentInventoryRemissionDetailXpo ConsignmentInventoryRemissionDetailId
        {
            get { return fConsignmentInventoryRemissionDetailId; }
            set { SetPropertyValue<ConsignmentInventoryRemissionDetailXpo>("ConsignmentInventoryRemissionDetailId", ref fConsignmentInventoryRemissionDetailId, value); }
        }
        InventoryProductXpo fProductId;
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesInventoryProductXpo")]
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

        [Association(@"ConsignmentInventoryRemissionDetailBatchSerialXpoReferencesConsignmentInventoryRemissionDetailXpo", typeof(ConsignmentInventoryRemissionDetailBatchSerialXpo))]
        public XPCollection<ConsignmentInventoryRemissionDetailBatchSerialXpo> ConsignmentInventoryRemissionDetailBatchSerialXpo { get { return GetCollection<ConsignmentInventoryRemissionDetailBatchSerialXpo>("ConsignmentInventoryRemissionDetailBatchSerialXpo"); } }
        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesConsignmentInventoryRemissionDetailXpo", typeof(ConsignmentInventoryRemissionDetailXpo))]
        public XPCollection<ConsignmentInventoryRemissionDetailXpo> ConsignmentInventoryRemissionDetailReferenceXpo { get { return GetCollection<ConsignmentInventoryRemissionDetailXpo>("ConsignmentInventoryRemissionDetailReferenceXpo"); } }

        [Association(@"ConsignmentInventoryRemissionDetailXpo_References_ConsignmentInventoryRemissionDetailControlXpo", typeof(ConsignmentInventoryRemissionDetailControlXpo))]
        public XPCollection<ConsignmentInventoryRemissionDetailControlXpo> ConsignmentInventoryRemissionDetailControlXpo { get { return GetCollection< ConsignmentInventoryRemissionDetailControlXpo>("ConsignmentInventoryRemissionDetailControlXpo"); } }

        public ConsignmentInventoryRemissionDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
