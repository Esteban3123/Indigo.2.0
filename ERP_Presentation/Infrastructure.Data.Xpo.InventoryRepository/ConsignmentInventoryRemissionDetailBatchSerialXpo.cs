using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ConsignmentInventoryRemissionDetailBatchSerial")]
    public class ConsignmentInventoryRemissionDetailBatchSerialXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        ConsignmentInventoryRemissionDetailXpo fConsignmentInventoryRemissionDetailId;
        [Association(@"ConsignmentInventoryRemissionDetailBatchSerialXpoReferencesConsignmentInventoryRemissionDetailXpo")]
        public ConsignmentInventoryRemissionDetailXpo ConsignmentInventoryRemissionDetailId
        {
            get { return fConsignmentInventoryRemissionDetailId; }
            set { SetPropertyValue<ConsignmentInventoryRemissionDetailXpo>("ConsignmentInventoryRemissionDetailId", ref fConsignmentInventoryRemissionDetailId, value); }
        }
        BatchSerialXpo fBatchSerialId;
        [Association(@"ConsignmentInventoryRemissionDetailBatchSerialXpoReferencesBatchSerialXpo")]
        public BatchSerialXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<BatchSerialXpo>("BatchSerialId", ref fBatchSerialId, value); }
        }
        string fCurrencyAbbreviation;
        [PersistentAlias("ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.CurrencyAbbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }
        string fCurrencyId;
        [PersistentAlias("ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.CurrencyId")]
        public string CurrencyId
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyId")); }

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
        int fReturnedQuantity;
        public int ReturnedQuantity
        {
            get { return fReturnedQuantity; }
            set { SetPropertyValue<int>("ReturnedQuantity", ref fReturnedQuantity, value); }
        }
        int fUsedQuantity;
        public int UsedQuantity
        {
            get { return fUsedQuantity; }
            set { SetPropertyValue<int>("UsedQuantity", ref fUsedQuantity, value); }
        }
        int fLegalizedQuantity;
        public int LegalizedQuantity
        {
            get { return fLegalizedQuantity; }
            set { SetPropertyValue<int>("LegalizedQuantity", ref fLegalizedQuantity, value); }
        }
        int fReplacementQuantity;
        public int ReplacementQuantity
        {
            get { return fReplacementQuantity; }
            set { SetPropertyValue<int>("ReplacementQuantity", ref fReplacementQuantity, value); }
        }
        [NonPersistent()]
        public int OutstandingLegalizedQuantity
        {
            get
            {
                return fUsedQuantity - fLegalizedQuantity;
            }
        }
        [NonPersistent()]
        public int PendingQuantityReplacement
        {
            get
            {
                return fUsedQuantity - fReplacementQuantity;
            }
        }

        public ConsignmentInventoryRemissionDetailBatchSerialXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
