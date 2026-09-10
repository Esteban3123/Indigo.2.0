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
    [Persistent(@"Inventory.ProductGroup")]
    public class InventoryProductGroupReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        byte fGroupClass;
        public byte GroupClass
        {
            get { return fGroupClass; }
            set { SetPropertyValue<byte>("GroupClass", ref fGroupClass, value); }
        }
        byte fSubclassCode;
        public byte SubclassCode
        {
            get { return fSubclassCode; }
            set { SetPropertyValue<byte>("SubclassCode", ref fSubclassCode, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fIncomeAccountId;
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo")]
        public InventoryGeneralLedgerMainAccountsReportXpo IncomeAccountId
        {
            get { return fIncomeAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("IncomeAccountId", ref fIncomeAccountId, value); }
        }
        int fInventoryAccountPayableConceptId;
        public int InventoryAccountPayableConceptId
        {
            get { return fInventoryAccountPayableConceptId; }
            set { SetPropertyValue<int>("InventoryAccountPayableConceptId", ref fInventoryAccountPayableConceptId, value); }
        }
        //InventoryGeneralLedgerMainAccountsReportXpo fPurchasesAccountId;
        //[Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo4")]
        //public InventoryGeneralLedgerMainAccountsReportXpo PurchasesAccountId
        //{
        //    get { return fPurchasesAccountId; }
        //    set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("PurchasesAccountId", ref fPurchasesAccountId, value); }
        //}
        //InventoryGeneralLedgerMainAccountsReportXpo fReturnsAccountId;
        //[Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo5")]
        //public InventoryGeneralLedgerMainAccountsReportXpo ReturnsAccountId
        //{
        //    get { return fReturnsAccountId; }
        //    set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("ReturnsAccountId", ref fReturnsAccountId, value); }
        //}
        //int fRetentionAccountPayableConceptId;
        //public int RetentionAccountPayableConceptId
        //{
        //    get { return fRetentionAccountPayableConceptId; }
        //    set { SetPropertyValue<int>("RetentionAccountPayableConceptId", ref fRetentionAccountPayableConceptId, value); }
        //}
        InventoryPayrollCostCenterReportXpo fCostCenterId;
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryPayrollCostCenterReportXpo")]
        public InventoryPayrollCostCenterReportXpo CostCenterId
        {
            get { return fCostCenterId; }
            set { SetPropertyValue<InventoryPayrollCostCenterReportXpo>("CostCenterId", ref fCostCenterId, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fReferenceInputDebitAccountId;
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo6")]
        public InventoryGeneralLedgerMainAccountsReportXpo ReferenceInputDebitAccountId
        {
            get { return fReferenceInputDebitAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("ReferenceInputDebitAccountId", ref fReferenceInputDebitAccountId, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fReferenceInputCreditAccountId;
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo7")]
        public InventoryGeneralLedgerMainAccountsReportXpo ReferenceInputCreditAccountId
        {
            get { return fReferenceInputCreditAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("ReferenceInputCreditAccountId", ref fReferenceInputCreditAccountId, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fReferenceOutputDebitAccountId;
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo8")]
        public InventoryGeneralLedgerMainAccountsReportXpo ReferenceOutputDebitAccountId
        {
            get { return fReferenceOutputDebitAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("ReferenceOutputDebitAccountId", ref fReferenceOutputDebitAccountId, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fReferenceOutputCreditAccountId;
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpo9")]
        public InventoryGeneralLedgerMainAccountsReportXpo ReferenceOutputCreditAccountId
        {
            get { return fReferenceOutputCreditAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("ReferenceOutputCreditAccountId", ref fReferenceOutputCreditAccountId, value); }
        }
        bool fExcludeFreightCosts;
        public bool ExcludeFreightCosts
        {
            get { return fExcludeFreightCosts; }
            set { SetPropertyValue<bool>("ExcludeFreightCosts", ref fExcludeFreightCosts, value); }
        }
        int fProductReplacementTime;
        public int ProductReplacementTime
        {
            get { return fProductReplacementTime; }
            set { SetPropertyValue<int>("ProductReplacementTime", ref fProductReplacementTime, value); }
        }
        int fProductsSourcingTime;
        public int ProductsSourcingTime
        {
            get { return fProductsSourcingTime; }
            set { SetPropertyValue<int>("ProductsSourcingTime", ref fProductsSourcingTime, value); }
        }
        decimal fSecurityPercentage;
        public decimal SecurityPercentage
        {
            get { return fSecurityPercentage; }
            set { SetPropertyValue<decimal>("SecurityPercentage", ref fSecurityPercentage, value); }
        }
        int fBudgetCategoryId;
        public int BudgetCategoryId
        {
            get { return fBudgetCategoryId; }
            set { SetPropertyValue<int>("BudgetCategoryId", ref fBudgetCategoryId, value); }
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
        //Propiedad Añadida
        bool fSeleccionado = false;
        [NonPersistent()]
        public bool Seleccionado
        {
            get { return fSeleccionado; }
            set { this.fSeleccionado = value; }
        }
        [Association(@"InventoryProductReportXpoReferencesInventoryProductGroupReportXpo", typeof(InventoryProductReportXpo))]
        public XPCollection<InventoryProductReportXpo> InventoryProductReportXpo { get { return GetCollection<InventoryProductReportXpo>("InventoryProductReportXpo"); } }

        [Association(@"InventoryProductGroup_InventoryProductRateGeneral", typeof(ProductRateGeneralXpo))]
        public XPCollection<ProductRateGeneralXpo> ProductRateGeneralXpo { get { return GetCollection<ProductRateGeneralXpo>("ProductRateGeneralXpo"); } }

        [Association(@"InventoryProductGroup_WarehouseRestrictedConditionsXpo", typeof(WarehouseRestrictedConditionsXpo))]
        public XPCollection<WarehouseRestrictedConditionsXpo> WarehouseRestrictedConditionsXpo { get { return GetCollection<WarehouseRestrictedConditionsXpo>("WarehouseRestrictedConditionsXpo"); } }


        public InventoryProductGroupReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
