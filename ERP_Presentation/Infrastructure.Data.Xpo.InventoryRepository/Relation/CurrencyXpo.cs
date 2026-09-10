using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Common.Currency")]
    public class CurrencyXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        private string fCode;
        public string Code
        {
            get => fCode;
            set { SetPropertyValue("Code", ref fCode, value); }
        }

        private string fName;
        public string Name
        {
            get => fName;
            set { SetPropertyValue("Name", ref fName, value); }
        }

        private bool fState;
        public bool State
        {
            get => fState;
            set { SetPropertyValue("State", ref fState, value); }
        }

        private string fAbbreviation;
        public string Abbreviation
        {
            get => fAbbreviation;
            set { SetPropertyValue("Abbreviation", ref fAbbreviation, value); }
        }

        [PersistentAlias("ISO4217Xpo.Id")]
        public int ISO4217Id
        {
            get { return Convert.ToInt32(EvaluateAlias("ISO4217Id")); }
        }

        private ISO4217Xpo fISO4217Xpo;
        [Persistent("ISO4217Id")]
        [Association("ISO4217Xpo_Reference_Currency")]
        public ISO4217Xpo ISO4217Xpo
        {
            get { return fISO4217Xpo; }
            set { SetPropertyValue("ISO4217Xpo", ref fISO4217Xpo, value); }
        }

        [Association("Currency_References_InventoryContractXpo", typeof(InventoryContractXpo))]
        public XPCollection<InventoryContractXpo> InventoryContractXpo { get { return GetCollection<InventoryContractXpo>("InventoryContractXpo"); } }

        [Association("Currency_References_InventoryPurchaseOrderXpo", typeof(InventoryPurchaseOrderXpo))]
        public XPCollection<InventoryPurchaseOrderXpo> InventoryPurchaseOrderXpo { get { return GetCollection<InventoryPurchaseOrderXpo>("InventoryPurchaseOrderXpo"); } }

        [Association("Currency_References_InventoryPurchaseOrderReportXpo", typeof(InventoryPurchaseOrderReportXpo))]
        public XPCollection<InventoryPurchaseOrderReportXpo> InventoryPurchaseOrderReportXpo { get { return GetCollection<InventoryPurchaseOrderReportXpo>("InventoryPurchaseOrderReportXpo"); } }

        [Association("Currency_References_RemissionEntranceXpo", typeof(RemissionEntranceXpo))]
        public XPCollection<RemissionEntranceXpo> RemissionEntranceXpo { get { return GetCollection<RemissionEntranceXpo>("RemissionEntranceXpo"); } }

        [Association("Currency_References_ConsignmentInventoryRemissionXpo", typeof(ConsignmentInventoryRemissionXpo))]
        public XPCollection<ConsignmentInventoryRemissionXpo> ConsignmentInventoryRemissionXpo { get { return GetCollection<ConsignmentInventoryRemissionXpo>("ConsignmentInventoryRemissionXpo"); } }

        [Association("Currency_References_InventoryEntranceVoucherReportXpo", typeof(InventoryEntranceVoucherReportXpo))]
        public XPCollection<InventoryEntranceVoucherReportXpo> InventoryEntranceVoucherReportXpo { get { return GetCollection<InventoryEntranceVoucherReportXpo>("InventoryEntranceVoucherReportXpo"); } }

        [Association("Currency_References_InventoryRemissionEntranceReportXpo", typeof(InventoryRemissionEntranceReportXpo))]
        public XPCollection<InventoryRemissionEntranceReportXpo> InventoryRemissionEntranceReportXpo { get { return GetCollection<InventoryRemissionEntranceReportXpo>("InventoryRemissionEntranceReportXpo"); } }

        [Association("Currency_References_InventoryViewRemissionEntranceDetailReportXpo", typeof(InventoryViewRemissionEntranceDetailReportXpo))]
        public XPCollection<InventoryViewRemissionEntranceDetailReportXpo> InventoryViewRemissionEntranceDetailReportXpo { get { return GetCollection<InventoryViewRemissionEntranceDetailReportXpo>("InventoryViewRemissionEntranceDetailReportXpo"); } }

        [Association("Currency_References_InventoryConsignmentInventoryRemissionReportXpo", typeof(InventoryConsignmentInventoryRemissionReportXpo))]
        public XPCollection<InventoryConsignmentInventoryRemissionReportXpo> InventoryConsignmentInventoryRemissionReportXpo { get { return GetCollection<InventoryConsignmentInventoryRemissionReportXpo>("InventoryConsignmentInventoryRemissionReportXpo"); } }

        [Association("Currency_References_ProductInTransitXpo", typeof(ProductInTransitXpo))]
        public XPCollection<ProductInTransitXpo> ProductInTransitXpo { get { return GetCollection<ProductInTransitXpo>("ProductInTransitXpo"); } }


        public CurrencyXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
