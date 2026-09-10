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
    [Persistent(@"GeneralLedger.MainAccounts")]
    public class InventoryGeneralLedgerMainAccountsReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryGeneralLedgerMainAccountsLevelsReportXpo fIdAccountLevel;
        [Association(@"InventoryGeneralLedgerMainAccountsReportXpoReferencesInventoryGeneralLedgerMainAccountsLevelsReportXpo")]
        public InventoryGeneralLedgerMainAccountsLevelsReportXpo IdAccountLevel
        {
            get { return fIdAccountLevel; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsLevelsReportXpo>("IdAccountLevel", ref fIdAccountLevel, value); }
        }
        InventoryGeneralLedgerMainAccountClassesReportXpo fIdAccountClass;
        [Association(@"InventoryGeneralLedgerMainAccountsReportXpoReferencesInventoryGeneralLedgerMainAccountClassesReportXpo")]
        public InventoryGeneralLedgerMainAccountClassesReportXpo IdAccountClass
        {
            get { return fIdAccountClass; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountClassesReportXpo>("IdAccountClass", ref fIdAccountClass, value); }
        }
        string fNumber;
        [Size(50)]
        public string Number
        {
            get { return fNumber; }
            set { SetPropertyValue<string>("Number", ref fNumber, value); }
        }

        [PersistentAlias("concat(concat(Number,' - '),Name)")]
        public string NumberName
        {
            get { return Convert.ToString(this.EvaluateAlias("NumberName")); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fIdParent;
        [Association(@"InventoryGeneralLedgerMainAccountsReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo")]
        public InventoryGeneralLedgerMainAccountsReportXpo IdParent
        {
            get { return fIdParent; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("IdParent", ref fIdParent, value); }
        }
        bool fHandlesThirdParty;
        public bool HandlesThirdParty
        {
            get { return fHandlesThirdParty; }
            set { SetPropertyValue<bool>("HandlesThirdParty", ref fHandlesThirdParty, value); }
        }
        bool fCloseThirdParty;
        public bool CloseThirdParty
        {
            get { return fCloseThirdParty; }
            set { SetPropertyValue<bool>("CloseThirdParty", ref fCloseThirdParty, value); }
        }
        int fIdThirdParty;
        public int IdThirdParty
        {
            get { return fIdThirdParty; }
            set { SetPropertyValue<int>("IdThirdParty", ref fIdThirdParty, value); }
        }
        bool fReconcileAccount;
        public bool ReconcileAccount
        {
            get { return fReconcileAccount; }
            set { SetPropertyValue<bool>("ReconcileAccount", ref fReconcileAccount, value); }
        }
        byte fAvailability;
        public byte Availability
        {
            get { return fAvailability; }
            set { SetPropertyValue<byte>("Availability", ref fAvailability, value); }
        }
        bool fHandlesCostCenter;
        public bool HandlesCostCenter
        {
            get { return fHandlesCostCenter; }
            set { SetPropertyValue<bool>("HandlesCostCenter", ref fHandlesCostCenter, value); }
        }
        byte fRetencionType;
        public byte RetencionType
        {
            get { return fRetencionType; }
            set { SetPropertyValue<byte>("RetencionType", ref fRetencionType, value); }
        }
        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
        }
        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fModificationUser;
        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }
        DateTime fModificationDate;
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }
        [Association(@"InventoryGeneralLedgerMainAccountsReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo", typeof(InventoryGeneralLedgerMainAccountsReportXpo))]
        public XPCollection<InventoryGeneralLedgerMainAccountsReportXpo> InventoryGeneralLedgerMainAccountsReportXpoCollection { get { return GetCollection<InventoryGeneralLedgerMainAccountsReportXpo>("InventoryGeneralLedgerMainAccountsReportXpoCollection"); } }
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo", typeof(InventoryProductGroupReportXpo))]
        public XPCollection<InventoryProductGroupReportXpo> Inventory_ProductGroups { get { return GetCollection<InventoryProductGroupReportXpo>("Inventory_ProductGroups"); } }
        //[Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo4", typeof(InventoryProductGroupReportXpo))]
        //public XPCollection<InventoryProductGroupReportXpo> Inventory_ProductGroups4 { get { return GetCollection<InventoryProductGroupReportXpo>("Inventory_ProductGroups4"); } }
        //[Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo5", typeof(InventoryProductGroupReportXpo))]
        //public XPCollection<InventoryProductGroupReportXpo> Inventory_ProductGroups5 { get { return GetCollection<InventoryProductGroupReportXpo>("Inventory_ProductGroups5"); } }
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo6", typeof(InventoryProductGroupReportXpo))]
        public XPCollection<InventoryProductGroupReportXpo> Inventory_ProductGroups6 { get { return GetCollection<InventoryProductGroupReportXpo>("Inventory_ProductGroups6"); } }
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo7", typeof(InventoryProductGroupReportXpo))]
        public XPCollection<InventoryProductGroupReportXpo> Inventory_ProductGroups7 { get { return GetCollection<InventoryProductGroupReportXpo>("Inventory_ProductGroups7"); } }
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo8", typeof(InventoryProductGroupReportXpo))]
        public XPCollection<InventoryProductGroupReportXpo> Inventory_ProductGroups8 { get { return GetCollection<InventoryProductGroupReportXpo>("Inventory_ProductGroups8"); } }
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo9", typeof(InventoryProductGroupReportXpo))]
        public XPCollection<InventoryProductGroupReportXpo> Inventory_ProductGroups9 { get { return GetCollection<InventoryProductGroupReportXpo>("Inventory_ProductGroups9"); } }
        [Association(@"InventoryWarehouseReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpoDebit", typeof(InventoryWarehouseReportXpo))]
        public XPCollection<InventoryWarehouseReportXpo> InventoryWarehouseReportXpoDebit { get { return GetCollection<InventoryWarehouseReportXpo>("InventoryWarehouseReportXpoDebit"); } }
        [Association(@"InventoryWarehouseReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpoCredit", typeof(InventoryWarehouseReportXpo))]
        public XPCollection<InventoryWarehouseReportXpo> InventoryWarehouseReportXpoCredit { get { return GetCollection<InventoryWarehouseReportXpo>("InventoryWarehouseReportXpoCredit"); } }
        [Association(@"InventoryAdjustmentConceptReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo", typeof(InventoryAdjustmentConceptReportXpo))]
        public XPCollection<InventoryAdjustmentConceptReportXpo> InventoryAdjustmentConceptReportXpo { get { return GetCollection<InventoryAdjustmentConceptReportXpo>("InventoryAdjustmentConceptReportXpo"); } }
        [Association(@"GeneralLedgerIVAXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo", typeof(GeneralLedgerIVAXpo))]
        public XPCollection<GeneralLedgerIVAXpo> GeneralLedgerIVA { get { return GetCollection<GeneralLedgerIVAXpo>("GeneralLedgerIVA"); } }

        public InventoryGeneralLedgerMainAccountsReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
