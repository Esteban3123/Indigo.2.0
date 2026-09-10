using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Payments.AccountPayable")]
    public class InventoryPaymentAccountPayableReport : XPLiteObject
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
        long fNumberFiling;
        public long NumberFiling
        {
            get { return fNumberFiling; }
            set { SetPropertyValue<long>("NumberFiling", ref fNumberFiling, value); }
        }
        int fEntityId;
        public int EntityId
        {
            get { return fEntityId; }
            set { SetPropertyValue<int>("EntityId", ref fEntityId, value); }
        }
        string fEntityCode;
        [Size(20)]
        public string EntityCode
        {
            get { return fEntityCode; }
            set { SetPropertyValue<string>("EntityCode", ref fEntityCode, value); }
        }
        string fEntityName;
        [Size(250)]
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }
        int fIdSupplier;
        public int IdSupplier
        {
            get { return fIdSupplier; }
            set { SetPropertyValue<int>("IdSupplier", ref fIdSupplier, value); }
        }
        int fIdThirdParty;
        public int IdThirdParty
        {
            get { return fIdThirdParty; }
            set { SetPropertyValue<int>("IdThirdParty", ref fIdThirdParty, value); }
        }
        int fIdAccount;
        public int IdAccount
        {
            get { return fIdAccount; }
            set { SetPropertyValue<int>("IdAccount", ref fIdAccount, value); }
        }
        int fIdCostCenter;
        public int IdCostCenter
        {
            get { return fIdCostCenter; }
            set { SetPropertyValue<int>("IdCostCenter", ref fIdCostCenter, value); }
        }
        string fBillNumber;
        [Size(20)]
        public string BillNumber
        {
            get { return fBillNumber; }
            set { SetPropertyValue<string>("BillNumber", ref fBillNumber, value); }
        }
        DateTime fBillDate;
        public DateTime BillDate
        {
            get { return fBillDate; }
            set { SetPropertyValue<DateTime>("BillDate", ref fBillDate, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }
        DateTime fServicePeriodDate;
        public DateTime ServicePeriodDate
        {
            get { return fServicePeriodDate; }
            set { SetPropertyValue<DateTime>("ServicePeriodDate", ref fServicePeriodDate, value); }
        }
        int fFilingUnitId;
        public int FilingUnitId
        {
            get { return fFilingUnitId; }
            set { SetPropertyValue<int>("FilingUnitId", ref fFilingUnitId, value); }
        }
        int fSupplierTypeId;
        public int SupplierTypeId
        {
            get { return fSupplierTypeId; }
            set { SetPropertyValue<int>("SupplierTypeId", ref fSupplierTypeId, value); }
        }
        int fTerm;
        public int Term
        {
            get { return fTerm; }
            set { SetPropertyValue<int>("Term", ref fTerm, value); }
        }
        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }
        string fComents;
        [Size(SizeAttribute.Unlimited)]
        public string Coments
        {
            get { return fComents; }
            set { SetPropertyValue<string>("Coments", ref fComents, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }
        bool fInitialBalance;
        public bool InitialBalance
        {
            get { return fInitialBalance; }
            set { SetPropertyValue<bool>("InitialBalance", ref fInitialBalance, value); }
        }
        int fIdInitialBalance;
        public int IdInitialBalance
        {
            get { return fIdInitialBalance; }
            set { SetPropertyValue<int>("IdInitialBalance", ref fIdInitialBalance, value); }
        }
        bool fPreviousBudget;
        public bool PreviousBudget
        {
            get { return fPreviousBudget; }
            set { SetPropertyValue<bool>("PreviousBudget", ref fPreviousBudget, value); }
        }
        int fShares;
        public int Shares
        {
            get { return fShares; }
            set { SetPropertyValue<int>("Shares", ref fShares, value); }
        }
        decimal fInvoiceValue;
        public decimal InvoiceValue
        {
            get { return fInvoiceValue; }
            set { SetPropertyValue<decimal>("InvoiceValue", ref fInvoiceValue, value); }
        }
        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }
        decimal fBalance;
        public decimal Balance
        {
            get { return fBalance; }
            set { SetPropertyValue<decimal>("Balance", ref fBalance, value); }
        }
        int fIdOperatingUnit;
        public int IdOperatingUnit
        {
            get { return fIdOperatingUnit; }
            set { SetPropertyValue<int>("IdOperatingUnit", ref fIdOperatingUnit, value); }
        }
        int fIdSuppliersDistributionLines;
        public int IdSuppliersDistributionLines
        {
            get { return fIdSuppliersDistributionLines; }
            set { SetPropertyValue<int>("IdSuppliersDistributionLines", ref fIdSuppliersDistributionLines, value); }
        }
        int fDocumentSupportId;
        public int DocumentSupportId
        {
            get { return fDocumentSupportId; }
            set { SetPropertyValue<int>("DocumentSupportId", ref fDocumentSupportId, value); }
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
        string fConfirmationUser;
        [Size(20)]
        public string ConfirmationUser
        {
            get { return fConfirmationUser; }
            set { SetPropertyValue<string>("ConfirmationUser", ref fConfirmationUser, value); }
        }
        DateTime fConfirmationDate;
        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }
        string fAnnulmentUser;
        [Size(20)]
        public string AnnulmentUser
        {
            get { return fAnnulmentUser; }
            set { SetPropertyValue<string>("AnnulmentUser", ref fAnnulmentUser, value); }
        }
        DateTime fAnnulmentDate;
        public DateTime AnnulmentDate
        {
            get { return fAnnulmentDate; }
            set { SetPropertyValue<DateTime>("AnnulmentDate", ref fAnnulmentDate, value); }
        }

        [Association(@"PaymentsAccountPayableDocumentSupport_References_PaymentsAccountPayable", typeof(InventoryAccountPayableDocumentSupportReportXpo))]
        public XPCollection<InventoryAccountPayableDocumentSupportReportXpo> InventoryAccountPayableDocumentSupportReportXpo { get { return GetCollection<InventoryAccountPayableDocumentSupportReportXpo>("InventoryAccountPayableDocumentSupportReportXpo"); } }


        [Association(@"InventoryEntranceVoucherReportXpoReferencesInventoryPaymentAccountPayableReport", typeof(InventoryEntranceVoucherReportXpo))]
        public XPCollection<InventoryEntranceVoucherReportXpo> InventoryEntranceVoucherReportXpo { get { return GetCollection<InventoryEntranceVoucherReportXpo>("InventoryEntranceVoucherReportXpo"); } }
        
        public InventoryPaymentAccountPayableReport(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
