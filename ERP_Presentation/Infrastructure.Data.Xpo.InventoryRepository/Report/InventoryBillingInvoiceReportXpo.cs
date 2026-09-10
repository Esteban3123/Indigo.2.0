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
    [Persistent(@"Billing.Invoice")]
    public class InventoryBillingInvoiceReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }
        byte fDocumentType;
        public byte DocumentType
        {
            get { return fDocumentType; }
            set { SetPropertyValue<byte>("DocumentType", ref fDocumentType, value); }
        }
        string fInvoiceNumber;
        [Indexed(Name = @"IX_Invoice", Unique = true)]
        [Size(15)]
        public string InvoiceNumber
        {
            get { return fInvoiceNumber; }
            set { SetPropertyValue<string>("InvoiceNumber", ref fInvoiceNumber, value); }
        }
        int fRevenueControlDetailId;
        public int RevenueControlDetailId
        {
            get { return fRevenueControlDetailId; }
            set { SetPropertyValue<int>("RevenueControlDetailId", ref fRevenueControlDetailId, value); }
        }
        string fAdmissionNumber;
        [Indexed(Name = @"IX_Invoice_AdmissionNumber")]
        [Size(10)]
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }
        int fHealthAdministratorId;
        public int HealthAdministratorId
        {
            get { return fHealthAdministratorId; }
            set { SetPropertyValue<int>("HealthAdministratorId", ref fHealthAdministratorId, value); }
        }
        int fThirdPartyId;
        public int ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<int>("ThirdPartyId", ref fThirdPartyId, value); }
        }
        string fPatientCode;
        [Size(15)]
        public string PatientCode
        {
            get { return fPatientCode; }
            set { SetPropertyValue<string>("PatientCode", ref fPatientCode, value); }
        }
        int fCareGroupId;
        public int CareGroupId
        {
            get { return fCareGroupId; }
            set { SetPropertyValue<int>("CareGroupId", ref fCareGroupId, value); }
        }
        DateTime fInvoiceDate;
        public DateTime InvoiceDate
        {
            get { return fInvoiceDate; }
            set { SetPropertyValue<DateTime>("InvoiceDate", ref fInvoiceDate, value); }
        }
        DateTime fInvoiceExpirationDate;
        public DateTime InvoiceExpirationDate
        {
            get { return fInvoiceExpirationDate; }
            set { SetPropertyValue<DateTime>("InvoiceExpirationDate", ref fInvoiceExpirationDate, value); }
        }
        decimal fTotalInvoice;
        public decimal TotalInvoice
        {
            get { return fTotalInvoice; }
            set { SetPropertyValue<decimal>("TotalInvoice", ref fTotalInvoice, value); }
        }
        DateTime fCapitationInitialDate;
        public DateTime CapitationInitialDate
        {
            get { return fCapitationInitialDate; }
            set { SetPropertyValue<DateTime>("CapitationInitialDate", ref fCapitationInitialDate, value); }
        }
        DateTime fCapitationEndDate;
        public DateTime CapitationEndDate
        {
            get { return fCapitationEndDate; }
            set { SetPropertyValue<DateTime>("CapitationEndDate", ref fCapitationEndDate, value); }
        }
        int fCapitationlPatientsAmount;
        public int CapitationlPatientsAmount
        {
            get { return fCapitationlPatientsAmount; }
            set { SetPropertyValue<int>("CapitationlPatientsAmount", ref fCapitationlPatientsAmount, value); }
        }
        decimal fCapitationPatientValue;
        public decimal CapitationPatientValue
        {
            get { return fCapitationPatientValue; }
            set { SetPropertyValue<decimal>("CapitationPatientValue", ref fCapitationPatientValue, value); }
        }
        decimal fThirdPartySalesValue;
        public decimal ThirdPartySalesValue
        {
            get { return fThirdPartySalesValue; }
            set { SetPropertyValue<decimal>("ThirdPartySalesValue", ref fThirdPartySalesValue, value); }
        }
        decimal fThirdPartyDiscountValue;
        public decimal ThirdPartyDiscountValue
        {
            get { return fThirdPartyDiscountValue; }
            set { SetPropertyValue<decimal>("ThirdPartyDiscountValue", ref fThirdPartyDiscountValue, value); }
        }
        byte fResponsibleRecoveryFee;
        public byte ResponsibleRecoveryFee
        {
            get { return fResponsibleRecoveryFee; }
            set { SetPropertyValue<byte>("ResponsibleRecoveryFee", ref fResponsibleRecoveryFee, value); }
        }
        decimal fTotalPatientSalesPrice;
        public decimal TotalPatientSalesPrice
        {
            get { return fTotalPatientSalesPrice; }
            set { SetPropertyValue<decimal>("TotalPatientSalesPrice", ref fTotalPatientSalesPrice, value); }
        }
        decimal fPatientDiscount;
        public decimal PatientDiscount
        {
            get { return fPatientDiscount; }
            set { SetPropertyValue<decimal>("PatientDiscount", ref fPatientDiscount, value); }
        }
        decimal fPatientDiscountPercentage;
        public decimal PatientDiscountPercentage
        {
            get { return fPatientDiscountPercentage; }
            set { SetPropertyValue<decimal>("PatientDiscountPercentage", ref fPatientDiscountPercentage, value); }
        }
        decimal fTotalPatientWithDiscount;
        public decimal TotalPatientWithDiscount
        {
            get { return fTotalPatientWithDiscount; }
            set { SetPropertyValue<decimal>("TotalPatientWithDiscount", ref fTotalPatientWithDiscount, value); }
        }
        decimal fValueVoucher;
        public decimal ValueVoucher
        {
            get { return fValueVoucher; }
            set { SetPropertyValue<decimal>("ValueVoucher", ref fValueVoucher, value); }
        }
        int fCashReceiptId;
        public int CashReceiptId
        {
            get { return fCashReceiptId; }
            set { SetPropertyValue<int>("CashReceiptId", ref fCashReceiptId, value); }
        }
        int fJournalVoucherId;
        public int JournalVoucherId
        {
            get { return fJournalVoucherId; }
            set { SetPropertyValue<int>("JournalVoucherId", ref fJournalVoucherId, value); }
        }
        decimal fPatientPaidValue;
        public decimal PatientPaidValue
        {
            get { return fPatientPaidValue; }
            set { SetPropertyValue<decimal>("PatientPaidValue", ref fPatientPaidValue, value); }
        }
        decimal fThirdPartyAccountReceivableValue;
        public decimal ThirdPartyAccountReceivableValue
        {
            get { return fThirdPartyAccountReceivableValue; }
            set { SetPropertyValue<decimal>("ThirdPartyAccountReceivableValue", ref fThirdPartyAccountReceivableValue, value); }
        }
        decimal fPatientAccountReceivableValue;
        public decimal PatientAccountReceivableValue
        {
            get { return fPatientAccountReceivableValue; }
            set { SetPropertyValue<decimal>("PatientAccountReceivableValue", ref fPatientAccountReceivableValue, value); }
        }
        int fPatientAccountReceivableId;
        public int PatientAccountReceivableId
        {
            get { return fPatientAccountReceivableId; }
            set { SetPropertyValue<int>("PatientAccountReceivableId", ref fPatientAccountReceivableId, value); }
        }
        int fPatientType;
        public int PatientType
        {
            get { return fPatientType; }
            set { SetPropertyValue<int>("PatientType", ref fPatientType, value); }
        }
        int fPatientAffiliatedType;
        public int PatientAffiliatedType
        {
            get { return fPatientAffiliatedType; }
            set { SetPropertyValue<int>("PatientAffiliatedType", ref fPatientAffiliatedType, value); }
        }
        int fPatientPaidAbility;
        public int PatientPaidAbility
        {
            get { return fPatientPaidAbility; }
            set { SetPropertyValue<int>("PatientPaidAbility", ref fPatientPaidAbility, value); }
        }
        string fPatientSocialClass;
        [Size(3)]
        public string PatientSocialClass
        {
            get { return fPatientSocialClass; }
            set { SetPropertyValue<string>("PatientSocialClass", ref fPatientSocialClass, value); }
        }
        decimal fCREETaxRetentionValue;
        public decimal CREETaxRetentionValue
        {
            get { return fCREETaxRetentionValue; }
            set { SetPropertyValue<decimal>("CREETaxRetentionValue", ref fCREETaxRetentionValue, value); }
        }
        decimal fCREETaxRetentionBaseValue;
        public decimal CREETaxRetentionBaseValue
        {
            get { return fCREETaxRetentionBaseValue; }
            set { SetPropertyValue<decimal>("CREETaxRetentionBaseValue", ref fCREETaxRetentionBaseValue, value); }
        }
        string fObservation;
        [Size(SizeAttribute.Unlimited)]
        public string Observation
        {
            get { return fObservation; }
            set { SetPropertyValue<string>("Observation", ref fObservation, value); }
        }
        int fInvoiceCategoryId;
        public int InvoiceCategoryId
        {
            get { return fInvoiceCategoryId; }
            set { SetPropertyValue<int>("InvoiceCategoryId", ref fInvoiceCategoryId, value); }
        }
        DateTime fInitialDate;
        public DateTime InitialDate
        {
            get { return fInitialDate; }
            set { SetPropertyValue<DateTime>("InitialDate", ref fInitialDate, value); }
        }
        bool fIsCutAccount;
        public bool IsCutAccount
        {
            get { return fIsCutAccount; }
            set { SetPropertyValue<bool>("IsCutAccount", ref fIsCutAccount, value); }
        }
        string fOutputDiagnosis;
        [Size(4)]
        public string OutputDiagnosis
        {
            get { return fOutputDiagnosis; }
            set { SetPropertyValue<string>("OutputDiagnosis", ref fOutputDiagnosis, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }
        string fInvoicedUser;
        [Size(20)]
        public string InvoicedUser
        {
            get { return fInvoicedUser; }
            set { SetPropertyValue<string>("InvoicedUser", ref fInvoicedUser, value); }
        }
        DateTime fInvoicedDate;
        public DateTime InvoicedDate
        {
            get { return fInvoicedDate; }
            set { SetPropertyValue<DateTime>("InvoicedDate", ref fInvoicedDate, value); }
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
        int fReversalReasonId;
        public int ReversalReasonId
        {
            get { return fReversalReasonId; }
            set { SetPropertyValue<int>("ReversalReasonId", ref fReversalReasonId, value); }
        }
        string fDescriptionReversal;
        [Size(SizeAttribute.Unlimited)]
        public string DescriptionReversal
        {
            get { return fDescriptionReversal; }
            set { SetPropertyValue<string>("DescriptionReversal", ref fDescriptionReversal, value); }
        }
        DateTime fOutputDate;
        public DateTime OutputDate
        {
            get { return fOutputDate; }
            set { SetPropertyValue<DateTime>("OutputDate", ref fOutputDate, value); }
        }
        byte fCutType;
        public byte CutType
        {
            get { return fCutType; }
            set { SetPropertyValue<byte>("CutType", ref fCutType, value); }
        }
        string fCUFE;
        public string CUFE
        {
            get { return fCUFE; }
            set { SetPropertyValue<string>("CUFE", ref fCUFE, value); }
        }
        string fQR;
        public string QR
        {
            get { return fQR; }
            set { SetPropertyValue<string>("QR", ref fQR, value); }
        }

        bool fIsElectronicTicket;
        public bool IsElectronicTicket
        {
            get { return fIsElectronicTicket; }
            set { SetPropertyValue<bool>("IsElectronicTicket", ref fIsElectronicTicket, value); }
        }

        [Association(@"InventoryDocumentInvoiceProductSalesReportXpoReferencesInventoryBillingInvoiceReportXpo", typeof(InventoryDocumentInvoiceProductSalesReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesReportXpo> InventoryDocumentInvoiceProductSalesReportXpo { get { return GetCollection<InventoryDocumentInvoiceProductSalesReportXpo>("InventoryDocumentInvoiceProductSalesReportXpo"); } }
        
        public InventoryBillingInvoiceReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
