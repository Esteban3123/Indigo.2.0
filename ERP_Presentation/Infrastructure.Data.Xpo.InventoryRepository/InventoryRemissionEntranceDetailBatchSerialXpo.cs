using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.RemissionEntranceDetailBatchSerial")]
    public class InventoryRemissionEntranceDetailBatchSerialXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRemissionEntranceDetailXpo fRemissionEntranceDetailId;
        [Association(@"InventoryRemissionEntranceDetailBatchSerialXpoReferencesInventoryRemissionEntranceDetailXpo")]
        public InventoryRemissionEntranceDetailXpo RemissionEntranceDetailId
        {
            get { return fRemissionEntranceDetailId; }
            set { SetPropertyValue<InventoryRemissionEntranceDetailXpo>("RemissionEntranceDetailId", ref fRemissionEntranceDetailId, value); }
        }
        BatchSerialXpo fBatchSerialId;
        [Association(@"InventoryRemissionEntranceDetailBatchSerialXpoReferencesBatchSerialXpo")]
        public BatchSerialXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<BatchSerialXpo>("BatchSerialId", ref fBatchSerialId, value); }
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

        [PersistentAlias("RemissionEntranceDetailId.CurrencyAbbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        [PersistentAlias("RemissionEntranceDetailId.CurrencyId")]
        public int CurrencyId
        {
            get { return Convert.ToInt32(EvaluateAlias("CurrencyId")); }

        }

        public InventoryRemissionEntranceDetailBatchSerialXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
