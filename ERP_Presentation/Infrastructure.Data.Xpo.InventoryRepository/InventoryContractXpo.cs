using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.InventoryContract")]
    public class InventoryContractXpo : XPLiteObject
    {

        #region Properties

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        [Indexed(Name = @"IX_InventoryContract", Unique = true)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }

        InventoryContractTypeXpo fContractTypeId;
        [Association(@"Inventory_InventoryContractReferencesInventory_InventoryContractType")]
        public InventoryContractTypeXpo ContractTypeId
        {
            get { return fContractTypeId; }
            set { SetPropertyValue<InventoryContractTypeXpo>("ContractTypeId", ref fContractTypeId, value); }
        }

        DateTime? fDocumentDate;
        public DateTime? DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime?>("DocumentDate", ref fDocumentDate, value); }
        }

        DateTime fInitialDate;
        public DateTime InitialDate
        {
            get { return fInitialDate; }
            set { SetPropertyValue<DateTime>("InitialDate", ref fInitialDate, value); }
        }

        DateTime fEndDate;
        public DateTime EndDate
        {
            get { return fEndDate; }
            set { SetPropertyValue<DateTime>("EndDate", ref fEndDate, value); }
        }

        SupplierXpo fSupplierId;
        [Association(@"Inventory_InventoryContractReferencesCommon_Supplier")]
        public SupplierXpo SupplierId
        {
            get { return fSupplierId; }
            set { SetPropertyValue<SupplierXpo>("SupplierId", ref fSupplierId, value); }
        }

        int fSupplierDistributionLineId;
        public int SupplierDistributionLineId
        {
            get { return fSupplierDistributionLineId; }
            set { SetPropertyValue<int>("SupplierDistributionLineId", ref fSupplierDistributionLineId, value); }
        }

        string fContractNumber;
        public string ContractNumber
        {
            get { return fContractNumber; }
            set { SetPropertyValue<string>("ContractNumber", ref fContractNumber, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fPaymentMethod;
        public string PaymentMethod
        {
            get { return fPaymentMethod; }
            set { SetPropertyValue<string>("PaymentMethod", ref fPaymentMethod, value); }
        }

        string fDeliveryMethod;
        public string DeliveryMethod
        {
            get { return fDeliveryMethod; }
            set { SetPropertyValue<string>("DeliveryMethod", ref fDeliveryMethod, value); }
        }

        string fDeliveryPlace;
        public string DeliveryPlace
        {
            get { return fDeliveryPlace; }
            set { SetPropertyValue<string>("DeliveryPlace", ref fDeliveryPlace, value); }
        }

        byte fSourceOrder;
        public byte SourceOrder
        {
            get { return fSourceOrder; }
            set { SetPropertyValue<byte>("SourceOrder", ref fSourceOrder, value); }
        }

        byte fPurchaseProcess;
        public byte PurchaseProcess
        {
            get { return fPurchaseProcess; }
            set { SetPropertyValue<byte>("PurchaseProcess", ref fPurchaseProcess, value); }
        }

        bool fExclusivity;
        public bool Exclusivity
        {
            get { return fExclusivity; }
            set { SetPropertyValue<bool>("Exclusivity", ref fExclusivity, value); }
        }

        bool fManageProducts;
        public bool ManageProducts
        {
            get { return fManageProducts; }
            set { SetPropertyValue<bool>("ManageProducts", ref fManageProducts, value); }
        }

        bool fOnlyGuarantee;
        public bool OnlyGuarantee
        {
            get { return fOnlyGuarantee; }
            set { SetPropertyValue<bool>("OnlyGuarantee", ref fOnlyGuarantee, value); }
        }

        string fTechnicalSupervicion;
        public string TechnicalSupervicion
        {
            get { return fTechnicalSupervicion; }
            set { SetPropertyValue<string>("TechnicalSupervicion", ref fTechnicalSupervicion, value); }
        }

        string fSupervisionExecution;
        public string SupervisionExecution
        {
            get { return fSupervisionExecution; }
            set { SetPropertyValue<string>("SupervisionExecution", ref fSupervisionExecution, value); }
        }

        string fClauses;
        [Size(SizeAttribute.Unlimited)]
        public string Clauses
        {
            get { return fClauses; }
            set { SetPropertyValue<string>("Clauses", ref fClauses, value); }
        }

        string fAttachments;
        [Size(SizeAttribute.Unlimited)]
        public string Attachments
        {
            get { return fAttachments; }
            set { SetPropertyValue<string>("Attachments", ref fAttachments, value); }
        }

        string fAvailability;
        public string Availability
        {
            get { return fAvailability; }
            set { SetPropertyValue<string>("Availability", ref fAvailability, value); }
        }

        string fResolution;
        public string Resolution
        {
            get { return fResolution; }
            set { SetPropertyValue<string>("Resolution", ref fResolution, value); }
        }

        DateTime fResolutionDate;
        public DateTime ResolutionDate
        {
            get { return fResolutionDate; }
            set { SetPropertyValue<DateTime>("ResolutionDate", ref fResolutionDate, value); }
        }

        string fQuoteNumber;
        public string QuoteNumber
        {
            get { return fQuoteNumber; }
            set { SetPropertyValue<string>("QuoteNumber", ref fQuoteNumber, value); }
        }

        DateTime fQuoteDate;
        public DateTime QuoteDate
        {
            get { return fQuoteDate; }
            set { SetPropertyValue<DateTime>("QuoteDate", ref fQuoteDate, value); }
        }

        string fRecordNumber;
        public string RecordNumber
        {
            get { return fRecordNumber; }
            set { SetPropertyValue<string>("RecordNumber", ref fRecordNumber, value); }
        }

        DateTime fRecordDate;
        public DateTime RecordDate
        {
            get { return fRecordDate; }
            set { SetPropertyValue<DateTime>("RecordDate", ref fRecordDate, value); }
        }

        string fNegotiationType;
        public string NegotiationType
        {
            get { return fNegotiationType; }
            set { SetPropertyValue<string>("NegotiationType", ref fNegotiationType, value); }
        }

        string fApproved;
        public string Approved
        {
            get { return fApproved; }
            set { SetPropertyValue<string>("Approved", ref fApproved, value); }
        }

        DateTime fDeadline;
        public DateTime Deadline
        {
            get { return fDeadline; }
            set { SetPropertyValue<DateTime>("Deadline", ref fDeadline, value); }
        }

        DateTime fValidityDate;
        public DateTime ValidityDate
        {
            get { return fValidityDate; }
            set { SetPropertyValue<DateTime>("ValidityDate", ref fValidityDate, value); }
        }

        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }

        decimal fDiscountValue;
        public decimal DiscountValue
        {
            get { return fDiscountValue; }
            set { SetPropertyValue<decimal>("DiscountValue", ref fDiscountValue, value); }
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

        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        string fCreationUser;
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

        CurrencyXpo fCurrency;
        [Association(@"Currency_References_InventoryContractXpo")]
        [Persistent("CurrencyId")]
        public CurrencyXpo Currency
        {
            get { return fCurrency; }
            set { SetPropertyValue<CurrencyXpo>("Currency", ref fCurrency, value); }
        }

        [PersistentAlias("Currency.Id")]
        public  int CurrencyId
        {
            get { return Convert.ToInt32(EvaluateAlias("CurrencyId")); }
           
        }

        [PersistentAlias("Currency.Abbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        #endregion

        #region Custom Properties

        [PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', Status = 4, 'Legalizado', '')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        #endregion

        #region Navigation Properties

        [Association(@"InventoryContractDetailXpoReferencesInventoryContractXpo", typeof(InventoryContractDetailXpo))]
        public XPCollection<InventoryContractDetailXpo> InventoryContractDetailXpo { get { return GetCollection<InventoryContractDetailXpo>("InventoryContractDetailXpo"); } }

        [Association(@"Inventory_InventoryContractAssignment_References_Inventory_InventoryContract", typeof(InventoryContractAssignmentXpo))]
        public XPCollection<InventoryContractAssignmentXpo> InventoryContractAssignmentXpo { get { return GetCollection<InventoryContractAssignmentXpo>("InventoryContractAssignmentXpo"); } }

        [Association(@"Inventory_InventoryContractModification_References_Inventory_InventoryContract", typeof(InventoryContractModificationXpo))]
        public XPCollection<InventoryContractModificationXpo> InventoryContractModificationXpo { get { return GetCollection<InventoryContractModificationXpo>("InventoryContractModificationXpo"); } }

        #endregion

        #region Builders

        public InventoryContractXpo(Session session) : base(session) { }

        #endregion

    }

}
