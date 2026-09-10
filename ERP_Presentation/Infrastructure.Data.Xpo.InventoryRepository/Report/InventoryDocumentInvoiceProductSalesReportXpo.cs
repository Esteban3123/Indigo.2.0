using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.DocumentInvoiceProductSales")]
    public class InventoryDocumentInvoiceProductSalesReportXpo : XPLiteObject
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
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        BillingAuthorizationReportXpo fBillingAuthorizationId;
        [Association(@"Inventory_DocumentInvoiceProductSalesReferencesBilling_BillingAuthorization")]
        public BillingAuthorizationReportXpo BillingAuthorizationId
        {
            get { return fBillingAuthorizationId; }
            set { SetPropertyValue<BillingAuthorizationReportXpo>("BillingAuthorizationId", ref fBillingAuthorizationId, value); }
        }
        InventoryPayrollFunctionalUnitXpo fFunctionalUnitId;
        [Association(@"Inventory_DocumentInvoiceProductSalesReportXpoReferencesPayroll_FunctionalUnit")]
        public InventoryPayrollFunctionalUnitXpo FunctionalUnitId
        {
            get { return fFunctionalUnitId; }
            set { SetPropertyValue<InventoryPayrollFunctionalUnitXpo>("FunctionalUnitId", ref fFunctionalUnitId, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }
        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }
        InventoryCommonThirdPartyXpo fThirdPartyId;
        [Association(@"Inventory_DocumentInvoiceProductSalesReportXpoReferencesCommon_ThirdParty")]
        public InventoryCommonThirdPartyXpo ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<InventoryCommonThirdPartyXpo>("ThirdPartyId", ref fThirdPartyId, value); }
        }
        InventoryPayrollBranchOfficeReportXpo fBranchOfficeId;
        [Association(@"InventoryDocumentInvoiceProductSalesReportXpoReferencesInventoryPayrollBranchOfficeReportXpo", typeof(InventoryDocumentInvoiceProductSalesReportXpo))]
        public InventoryPayrollBranchOfficeReportXpo BranchOfficeId
        {
            get { return fBranchOfficeId; }
            set { SetPropertyValue<InventoryPayrollBranchOfficeReportXpo>("BranchOfficeId", ref fBranchOfficeId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"Inventory_DocumentInvoiceProductSalesReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        string fDescription;
        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
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
        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
        }
        InventoryBillingInvoiceReportXpo fInvoiceId;
        [Association(@"InventoryDocumentInvoiceProductSalesReportXpoReferencesInventoryBillingInvoiceReportXpo")]
        public InventoryBillingInvoiceReportXpo InvoiceId
        {
            get { return fInvoiceId; }
            set { SetPropertyValue<InventoryBillingInvoiceReportXpo>("InvoiceId", ref fInvoiceId, value); }
        }
        byte fSaleModality;
        public byte SaleModality
        {
            get { return fSaleModality; }
            set { SetPropertyValue<byte>("SaleModality", ref fSaleModality, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        int? fConditionSalesId;
        public int? ConditionSalesId
        {
            get { return fConditionSalesId; }
            set { SetPropertyValue<int?>("ConditionSalesId", ref fConditionSalesId, value); }
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

        #endregion

        #region Custom Properties

        [PersistentAlias("Iif(SaleModality = 1, 'Contado', 'Crédito')")]
        public string PaymentMeans
        {
            get { return Convert.ToString(this.EvaluateAlias("PaymentMeans")); }
        }

        #endregion

        #region Navigation Properties

        [Association(@"Inventory_DocumentInvoiceProductSalesDetailReferencesInventory_DocumentInvoiceProductSales", typeof(InventoryDocumentInvoiceProductSalesDetailReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesDetailReportXpo> Inventory_DocumentInvoiceProductSalesDetails { get { return GetCollection<InventoryDocumentInvoiceProductSalesDetailReportXpo>("Inventory_DocumentInvoiceProductSalesDetails"); } }

        #endregion

        #region Builders

        public InventoryDocumentInvoiceProductSalesReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion
    }
}
