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
    [Persistent(@"Common.ThirdParty")]
    public class InventoryCommonThirdPartyXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryCommonPersonXpo fPersonId;
        [Association(@"Common_ThirdPartyReferencesCommon_Person")]
        public InventoryCommonPersonXpo PersonId
        {
            get { return fPersonId; }
            set { SetPropertyValue<InventoryCommonPersonXpo>("PersonId", ref fPersonId, value); }
        }
        string fNit;
        //[Indexed(Name = @"IX_ThirdParty_Nit_UNI", Unique = true)]
        [Size(15)]
        public string Nit
        {
            get { return fNit; }
            set { SetPropertyValue<string>("Nit", ref fNit, value); }
        }
        string fDigitVerification;
        [Size(1)]
        public string DigitVerification
        {
            get { return fDigitVerification; }
            set { SetPropertyValue<string>("DigitVerification", ref fDigitVerification, value); }
        }
        string fName;
        [Size(300)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        [Size(200)]
        [PersistentAlias("concat(concat(Nit,' - '),Name)")]
        public string NitName
        {
            get { return Convert.ToString(this.EvaluateAlias("NitName")); }
        }

        byte fPersonType;
        public byte PersonType
        {
            get { return fPersonType; }
            set { SetPropertyValue<byte>("PersonType", ref fPersonType, value); }
        }
        byte fRetentionType;
        public byte RetentionType
        {
            get { return fRetentionType; }
            set { SetPropertyValue<byte>("RetentionType", ref fRetentionType, value); }
        }
        byte fContributionType;
        public byte ContributionType
        {
            get { return fContributionType; }
            set { SetPropertyValue<byte>("ContributionType", ref fContributionType, value); }
        }
        int fIVARetentionAccountPayableConceptId;
        public int IVARetentionAccountPayableConceptId
        {
            get { return fIVARetentionAccountPayableConceptId; }
            set { SetPropertyValue<int>("IVARetentionAccountPayableConceptId", ref fIVARetentionAccountPayableConceptId, value); }
        }
        bool fIca;
        public bool Ica
        {
            get { return fIca; }
            set { SetPropertyValue<bool>("Ica", ref fIca, value); }
        }
        decimal fIcaPercentage;
        public decimal IcaPercentage
        {
            get { return fIcaPercentage; }
            set { SetPropertyValue<decimal>("IcaPercentage", ref fIcaPercentage, value); }
        }
        bool fIcaTop;
        public bool IcaTop
        {
            get { return fIcaTop; }
            set { SetPropertyValue<bool>("IcaTop", ref fIcaTop, value); }
        }
        decimal fIcaTopValue;
        public decimal IcaTopValue
        {
            get { return fIcaTopValue; }
            set { SetPropertyValue<decimal>("IcaTopValue", ref fIcaTopValue, value); }
        }
        string fEntityCode;
        [Size(15)]
        public string EntityCode
        {
            get { return fEntityCode; }
            set { SetPropertyValue<string>("EntityCode", ref fEntityCode, value); }
        }
        int fEconomicActivityId;
        public int EconomicActivityId
        {
            get { return fEconomicActivityId; }
            set { SetPropertyValue<int>("EconomicActivityId", ref fEconomicActivityId, value); }
        }
        byte fClass;
        public byte Class
        {
            get { return fClass; }
            set { SetPropertyValue<byte>("Class", ref fClass, value); }
        }
        byte[] fDigitalSignature;
        [Size(SizeAttribute.Unlimited)]
        public byte[] DigitalSignature
        {
            get { return fDigitalSignature; }
            set { SetPropertyValue<byte[]>("DigitalSignature", ref fDigitalSignature, value); }
        }
        bool fState;
        public bool State
        {
            get { return fState; }
            set { SetPropertyValue<bool>("State", ref fState, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        int fUserId;
        public int UserId
        {
            get { return fUserId; }
            set { SetPropertyValue<int>("UserId", ref fUserId, value); }
        }

        InventoryRetentionConceptsXpo fIVARetentionConceptId;
        [Association(@"ThirdPartyReferencesRetentionConcept")]
        public InventoryRetentionConceptsXpo IVARetentionConceptId
        {
            get { return fIVARetentionConceptId; }
            set { SetPropertyValue<InventoryRetentionConceptsXpo>("IVARetentionConceptId", ref fIVARetentionConceptId, value); }
        }

        [Association(@"Common_SupplierReferencesCommon_ThirdParty", typeof(InventoryCommonSupplierXpo))]
        public XPCollection<InventoryCommonSupplierXpo> Common_Suppliers { get { return GetCollection<InventoryCommonSupplierXpo>("Common_Suppliers"); } }
        [Association(@"InventoryCommonCustomerReportXpoReferencesInventoryCommonThirdPartyXpo", typeof(InventoryCommonCustomerReportXpo))]
        public XPCollection<InventoryCommonCustomerReportXpo> InventoryCommonCustomerReportXpo { get { return GetCollection<InventoryCommonCustomerReportXpo>("InventoryCommonCustomerReportXpo"); } }
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesCommon_ThirdParty", typeof(InventoryPharmaceuticalDispensingDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailReportXpo> Inventory_PharmaceuticalDispensingDetails { get { return GetCollection<InventoryPharmaceuticalDispensingDetailReportXpo>("Inventory_PharmaceuticalDispensingDetails"); } }
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesCommon_OrderedHealthProfessionalThirdPartyId", typeof(InventoryPharmaceuticalDispensingDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailReportXpo> Inventory_OrderedHealthProfessional_PharmaceuticalDispensingDetails { get { return GetCollection<InventoryPharmaceuticalDispensingDetailReportXpo>("Inventory_OrderedHealthProfessional_PharmaceuticalDispensingDetails"); } }
        [Association(@"InventoryAdjustmentReportXpoReferencesInventoryCommonThirdPartyXpo", typeof(InventoryAdjustmentReportXpo))]
        public XPCollection<InventoryAdjustmentReportXpo> InventoryAdjustmentReportXpo { get { return GetCollection<InventoryAdjustmentReportXpo>("InventoryAdjustmentReportXpo"); } }
        [Association(@"InventoryLoanMerchandiseReportXpoReferencesInventoryCommonThirdPartyXpo", typeof(InventoryLoanMerchandiseReportXpo))]
        public XPCollection<InventoryLoanMerchandiseReportXpo> InventoryLoanThirdReportXpo { get { return GetCollection<InventoryLoanMerchandiseReportXpo>("InventoryLoanThirdReportXpo"); } }
        [Association(@"Inventory_DocumentInvoiceProductSalesReferencesCommon_ThirdParty", typeof(InventoryDocumentInvoiceProductSalesXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesXpo> Inventory_DocumentInvoiceProductSaless { get { return GetCollection<InventoryDocumentInvoiceProductSalesXpo>("Inventory_DocumentInvoiceProductSaless"); } }
        [Association(@"Inventory_DocumentInvoiceProductSalesReportXpoReferencesCommon_ThirdParty", typeof(InventoryDocumentInvoiceProductSalesReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesReportXpo> Inventory_DocumentInvoiceProductSalesReportXpo { get { return GetCollection<InventoryDocumentInvoiceProductSalesReportXpo>("Inventory_DocumentInvoiceProductSalesReportXpo"); } }
        [Association(@"InventoryTransferOrderReportXpoReferencesInventoryCommonThirdPartyXpo", typeof(InventoryTransferOrderReportXpo))]
        public XPCollection<InventoryTransferOrderReportXpo> Inventory_TransferOrderReportXpo { get { return GetCollection<InventoryTransferOrderReportXpo>("Inventory_TransferOrderReportXpo"); } }
      
        public InventoryCommonThirdPartyXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
