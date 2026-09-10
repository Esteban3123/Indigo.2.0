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
    [Persistent(@"Inventory.EntranceVoucher")]
    public class InventoryEntranceVoucherReportXpo : XPLiteObject
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
        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }
        InventoryCommonSupplierXpo fSupplierId;
        [Association(@"Inventory_EntranceVoucherReferencesCommon_Supplier")]
        public InventoryCommonSupplierXpo SupplierId
        {
            get { return fSupplierId; }
            set { SetPropertyValue<InventoryCommonSupplierXpo>("SupplierId", ref fSupplierId, value); }
        }
        InventoryCommonSupplierDistributionLineReportXpo fSupplierDistributionLineId;
        [Association(@"Inventory_EntranceVoucherReferencesCommon_SuppliersDistributionLines")]
        public InventoryCommonSupplierDistributionLineReportXpo SupplierDistributionLineId
        {
            get { return fSupplierDistributionLineId; }
            set { SetPropertyValue<InventoryCommonSupplierDistributionLineReportXpo>("SupplierDistributionLineId", ref fSupplierDistributionLineId, value); }
        }
        int fSupplierTypeId;
        public int SupplierTypeId
        {
            get { return fSupplierTypeId; }
            set { SetPropertyValue<int>("SupplierTypeId", ref fSupplierTypeId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryEntranceVoucherReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        InventoryPaymentAccountPayableReport fAccountPayableId;
        [Association(@"InventoryEntranceVoucherReportXpoReferencesInventoryPaymentAccountPayableReport")]
        public InventoryPaymentAccountPayableReport AccountPayableId
        {
            get { return fAccountPayableId; }
            set { SetPropertyValue<InventoryPaymentAccountPayableReport>("AccountPayableId", ref fAccountPayableId, value); }
        }
        string fDescription;
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        byte fResourceType;
        public byte ResourceType
        {
            get { return fResourceType; }
            set { SetPropertyValue<byte>("ResourceType", ref fResourceType, value); }
        }
        int fRoundService;
        public int RoundService
        {
            get { return fRoundService; }
            set { SetPropertyValue<int>("RoundService", ref fRoundService, value); }
        }
        decimal fIcaPercentage;
        public decimal IcaPercentage
        {
            get { return fIcaPercentage; }
            set { SetPropertyValue<decimal>("IcaPercentage", ref fIcaPercentage, value); }
        }
        string fInvoiceNumber;
        public string InvoiceNumber
        {
            get { return fInvoiceNumber; }
            set { SetPropertyValue<string>("InvoiceNumber", ref fInvoiceNumber, value); }
        }
        DateTime fInvoiceDate;
        public DateTime InvoiceDate
        {
            get { return fInvoiceDate; }
            set { SetPropertyValue<DateTime>("InvoiceDate", ref fInvoiceDate, value); }
        }
        int fDayPeriod;
        public int DayPeriod
        {
            get { return fDayPeriod; }
            set { SetPropertyValue<int>("DayPeriod", ref fDayPeriod, value); }
        }
        decimal fFreightValue;
        public decimal FreightValue
        {
            get { return fFreightValue; }
            set { SetPropertyValue<decimal>("FreightValue", ref fFreightValue, value); }
        }
        decimal fFreightIVAPercentage;
        public decimal FreightIVAPercentage
        {
            get { return fFreightIVAPercentage; }
            set { SetPropertyValue<decimal>("FreightIVAPercentage", ref fFreightIVAPercentage, value); }
        }
        decimal fFreightIVAValue;
        public decimal FreightIVAValue
        {
            get { return fFreightIVAValue; }
            set { SetPropertyValue<decimal>("FreightIVAValue", ref fFreightIVAValue, value); }
        }
        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }
        decimal fValueDiscount;
        public decimal ValueDiscount
        {
            get { return fValueDiscount; }
            set { SetPropertyValue<decimal>("ValueDiscount", ref fValueDiscount, value); }
        }
        decimal fValueTax;
        public decimal ValueTax
        {
            get { return fValueTax; }
            set { SetPropertyValue<decimal>("ValueTax", ref fValueTax, value); }
        }
        decimal fWithholdingTax;
        public decimal WithholdingTax
        {
            get { return fWithholdingTax; }
            set { SetPropertyValue<decimal>("WithholdingTax", ref fWithholdingTax, value); }
        }
        decimal fWithholdingICA;
        public decimal WithholdingICA
        {
            get { return fWithholdingICA; }
            set { SetPropertyValue<decimal>("WithholdingICA", ref fWithholdingICA, value); }
        }
        decimal fRetentionSource;
        public decimal RetentionSource
        {
            get { return fRetentionSource; }
            set { SetPropertyValue<decimal>("RetentionSource", ref fRetentionSource, value); }
        }
        decimal fRetentionOther;
        public decimal RetentionOther
        {
            get { return fRetentionOther; }
            set { SetPropertyValue<decimal>("RetentionOther", ref fRetentionOther, value); }
        }
        decimal fDeductionOther;
        public decimal DeductionOther
        {
            get { return fDeductionOther; }
            set { SetPropertyValue<decimal>("DeductionOther", ref fDeductionOther, value); }
        }
        decimal fDistrictTax;
        public decimal DistrictTax
        {
            get { return fDistrictTax; }
            set { SetPropertyValue<decimal>("DistrictTax", ref fDistrictTax, value); }
        }
        //decimal fRoundingAdjustment;
        //public decimal RoundingAdjustment
        //{
        //    get { return fRoundingAdjustment; }
        //    set { SetPropertyValue<decimal>("RoundingAdjustment", ref fRoundingAdjustment, value); }
        //}
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

        [PersistentAlias("Iif([Status] = 1, 'Registrado', [Status] = 2, 'Confirmado','Anulado')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
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

        CurrencyXpo fCurrency;
        [Association(@"Currency_References_InventoryEntranceVoucherReportXpo")]
        [Persistent("CurrencyId")]
        public CurrencyXpo Currency
        {
            get { return fCurrency; }
            set { SetPropertyValue<CurrencyXpo>("Currency", ref fCurrency, value); }
        }

        [PersistentAlias("Currency.Id")]
        public int CurrencyId
        {
            get { return Convert.ToInt32(EvaluateAlias("CurrencyId")); }

        }

        [PersistentAlias("Currency.Abbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        BillingAuthorizationXpo fDocumentSupportId;
        [Association(@"BillingAuthorizationXpoReferencesInventoryEntranceVoucherReportXpo")]
        public BillingAuthorizationXpo DocumentSupportId
        {
            get { return fDocumentSupportId; }
            set { SetPropertyValue<BillingAuthorizationXpo>("DocumentSupportId", ref fDocumentSupportId, value); }
        }

        [Association(@"Inventory_EntranceVoucherDetailReferencesInventory_EntranceVoucher", typeof(InventoryEntranceVoucherDetailReportXpo))]
        public XPCollection<InventoryEntranceVoucherDetailReportXpo> Inventory_EntranceVoucherDetails { get { return GetCollection<InventoryEntranceVoucherDetailReportXpo>("Inventory_EntranceVoucherDetails"); } }
        [Association(@"Inventory_EntranceVoucherOtherDeductionReferencesInventory_EntranceVoucher", typeof(InventoryEntranceVoucherOtherDeductionReportXpo))]
        public XPCollection<InventoryEntranceVoucherOtherDeductionReportXpo> Inventory_EntranceVoucherOtherDeductions { get { return GetCollection<InventoryEntranceVoucherOtherDeductionReportXpo>("Inventory_EntranceVoucherOtherDeductions"); } }
        [Association(@"Inventory_EntranceVoucherDevolutionReferencesInventory_EntranceVoucher", typeof(InventoryEntranceVouhcerDevolutionReportXpo))]
        public XPCollection<InventoryEntranceVouhcerDevolutionReportXpo> Inventory_EntranceVoucherDevolution { get { return GetCollection<InventoryEntranceVouhcerDevolutionReportXpo>("Inventory_EntranceVoucherDevolution"); } }
     
        public InventoryEntranceVoucherReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
