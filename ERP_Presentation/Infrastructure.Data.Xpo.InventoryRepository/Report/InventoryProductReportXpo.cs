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
    [Persistent(@"Inventory.InventoryProduct")]
    public class InventoryProductReportXpo : XPLiteObject
    {

        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
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

        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        InventoryProductTypeReportXpo fProductTypeId;
        [Association(@"InventoryProductReportXpoReferencesInventoryProductTypeReportXpo")]
        public InventoryProductTypeReportXpo ProductTypeId
        {
            get { return fProductTypeId; }
            set { SetPropertyValue<InventoryProductTypeReportXpo>("ProductTypeId", ref fProductTypeId, value); }
        }

        [PersistentAlias("ProductTypeId.Name")]
        public string ProductTypeIdName
        {
            get { return Convert.ToString(this.EvaluateAlias("ProductTypeIdName")); }
        }

        [PersistentAlias("ProductSubGroupId.Name")]
        public string ProductSubGroupIdName
        {
            get { return Convert.ToString(this.EvaluateAlias("ProductSubGroupIdName")); }
        }

        [PersistentAlias("ProductGroupId.Name")]
        public string ProductGroupIdName
        {
            get { return Convert.ToString(this.EvaluateAlias("ProductGroupIdName")); }
        }

        InventoryATCReportXpo fATCId;
        [Association(@"InventoryProductReportXpoReferencesInventoryATCReportXpo")]
        public InventoryATCReportXpo ATCId
        {
            get { return fATCId; }
            set { SetPropertyValue<InventoryATCReportXpo>("ATCId", ref fATCId, value); }
        }

        string fCodeCUM;
        [Size(20)]
        public string CodeCUM
        {
            get { return fCodeCUM; }
            set { SetPropertyValue<string>("CodeCUM", ref fCodeCUM, value); }
        }

        string fCodeAlternative;
        [Size(20)]
        public string CodeAlternative
        {
            get { return fCodeAlternative; }
            set { SetPropertyValue<string>("CodeAlternative", ref fCodeAlternative, value); }
        }

        string fCodeAlternativeTwo;
        [Size(20)]
        public string CodeAlternativeTwo
        {
            get { return fCodeAlternativeTwo; }
            set { SetPropertyValue<string>("CodeAlternativeTwo", ref fCodeAlternativeTwo, value); }
        }

        string fDescription;
        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        InventoryProductGroupReportXpo fProductGroupId;
        [Association(@"InventoryProductReportXpoReferencesInventoryProductGroupReportXpo")]
        public InventoryProductGroupReportXpo ProductGroupId
        {
            get { return fProductGroupId; }
            set { SetPropertyValue<InventoryProductGroupReportXpo>("ProductGroupId", ref fProductGroupId, value); }
        }

        InventoryProductSubGroupReportXpo fProductSubGroupId;
        [Association(@"InventoryProductReportXpoReferencesInventoryProductSubGroupReportXpo")]
        public InventoryProductSubGroupReportXpo ProductSubGroupId
        {
            get { return fProductSubGroupId; }
            set { SetPropertyValue<InventoryProductSubGroupReportXpo>("ProductSubGroupId", ref fProductSubGroupId, value); }
        }

        InventoryMeasurementUnitReportXpo fMeasurementUnitId;
        [Association(@"InventoryProductReportXpoReferencesInventoryMeasurementUnitReportXpo")]
        public InventoryMeasurementUnitReportXpo MeasurementUnitId
        {
            get { return fMeasurementUnitId; }
            set { SetPropertyValue<InventoryMeasurementUnitReportXpo>("MeasurementUnitId", ref fMeasurementUnitId, value); }
        }

        InventoryPackagingReportXpo fPackagingUnitId;
        [Association(@"Inventory_InventoryProductReferencesInventory_PackagingUnit")]
        public InventoryPackagingReportXpo PackagingUnitId
        {
            get { return fPackagingUnitId; }
            set { SetPropertyValue<InventoryPackagingReportXpo>("PackagingUnitId", ref fPackagingUnitId, value); }
        }

        int fManufacturerId;
        public int ManufacturerId
        {
            get { return fManufacturerId; }
            set { SetPropertyValue<int>("ManufacturerId", ref fManufacturerId, value); }
        }

        int fIVAId;
        public int IVAId
        {
            get { return fIVAId; }
            set { SetPropertyValue<int>("IVAId", ref fIVAId, value); }
        }

        string fPresentation;
        [Size(200)]
        public string Presentation
        {
            get { return fPresentation; }
            set { SetPropertyValue<string>("Presentation", ref fPresentation, value); }
        }

        string fCodeSICE;
        [Size(20)]
        public string CodeSICE
        {
            get { return fCodeSICE; }
            set { SetPropertyValue<string>("CodeSICE", ref fCodeSICE, value); }
        }

        bool fHandlesSerial;
        public bool HandlesSerial
        {
            get { return fHandlesSerial; }
            set { SetPropertyValue<bool>("HandlesSerial", ref fHandlesSerial, value); }
        }

        bool fHandlesHealthRegistration;
        public bool HandlesHealthRegistration
        {
            get { return fHandlesHealthRegistration; }
            set { SetPropertyValue<bool>("HandlesHealthRegistration", ref fHandlesHealthRegistration, value); }
        }

        string fHealthRegistration;
        [Size(30)]
        public string HealthRegistration
        {
            get { return fHealthRegistration; }
            set { SetPropertyValue<string>("HealthRegistration", ref fHealthRegistration, value); }
        }

        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }

        int fBillingGroupId;
        public int BillingGroupId
        {
            get { return fBillingGroupId; }
            set { SetPropertyValue<int>("BillingGroupId", ref fBillingGroupId, value); }
        }

        bool fProductControl;
        public bool ProductControl
        {
            get { return fProductControl; }
            set { SetPropertyValue<bool>("ProductControl", ref fProductControl, value); }
        }

        bool fProductWithPriceControl;
        public bool ProductWithPriceControl
        {
            get { return fProductWithPriceControl; }
            set { SetPropertyValue<bool>("ProductWithPriceControl", ref fProductWithPriceControl, value); }
        }

        bool fPOSProduct;
        public bool POSProduct
        {
            get { return fPOSProduct; }
            set { SetPropertyValue<bool>("POSProduct", ref fPOSProduct, value); }
        }

        int fAuthorizationByOrderNumber;
        public int AuthorizationByOrderNumber
        {
            get { return fAuthorizationByOrderNumber; }
            set { SetPropertyValue<int>("AuthorizationByOrderNumber", ref fAuthorizationByOrderNumber, value); }
        }

        int fExpirationDay;
        public int ExpirationDay
        {
            get { return fExpirationDay; }
            set { SetPropertyValue<int>("ExpirationDay", ref fExpirationDay, value); }
        }

        bool fMaximumControlPeriod;
        public bool MaximumControlPeriod
        {
            get { return fMaximumControlPeriod; }
            set { SetPropertyValue<bool>("MaximumControlPeriod", ref fMaximumControlPeriod, value); }
        }

        int fControlDays;
        public int ControlDays
        {
            get { return fControlDays; }
            set { SetPropertyValue<int>("ControlDays", ref fControlDays, value); }
        }

        bool fControlOrderQuantity;
        public bool ControlOrderQuantity
        {
            get { return fControlOrderQuantity; }
            set { SetPropertyValue<bool>("ControlOrderQuantity", ref fControlOrderQuantity, value); }
        }

        int fProductOrderAmount;
        public int ProductOrderAmount
        {
            get { return fProductOrderAmount; }
            set { SetPropertyValue<int>("ProductOrderAmount", ref fProductOrderAmount, value); }
        }

        DateTime fLastPurchase;
        public DateTime LastPurchase
        {
            get { return fLastPurchase; }
            set { SetPropertyValue<DateTime>("LastPurchase", ref fLastPurchase, value); }
        }

        DateTime fLastSale;
        public DateTime LastSale
        {
            get { return fLastSale; }
            set { SetPropertyValue<DateTime>("LastSale", ref fLastSale, value); }
        }

        byte fProductOrigin;
        public byte ProductOrigin
        {
            get { return fProductOrigin; }
            set { SetPropertyValue<byte>("ProductOrigin", ref fProductOrigin, value); }
        }

        int fMinimumStock;
        public int MinimumStock
        {
            get { return fMinimumStock; }
            set { SetPropertyValue<int>("MinimumStock", ref fMinimumStock, value); }
        }

        int fMaximumStock;
        public int MaximumStock
        {
            get { return fMaximumStock; }
            set { SetPropertyValue<int>("MaximumStock", ref fMaximumStock, value); }
        }

        decimal fCommissionPercentage;
        public decimal CommissionPercentage
        {
            get { return fCommissionPercentage; }
            set { SetPropertyValue<decimal>("CommissionPercentage", ref fCommissionPercentage, value); }
        }

        int fRepositionPoint;
        public int RepositionPoint
        {
            get { return fRepositionPoint; }
            set { SetPropertyValue<int>("RepositionPoint", ref fRepositionPoint, value); }
        }

        int fResetTime;
        public int ResetTime
        {
            get { return fResetTime; }
            set { SetPropertyValue<int>("ResetTime", ref fResetTime, value); }
        }

        byte fCurrencyType;
        public byte CurrencyType
        {
            get { return fCurrencyType; }
            set { SetPropertyValue<byte>("CurrencyType", ref fCurrencyType, value); }
        }

        decimal fProductCost;
        public decimal ProductCost
        {
            get { return fProductCost; }
            set { SetPropertyValue<decimal>("ProductCost", ref fProductCost, value); }
        }

        decimal fFinalProductCost;
        public decimal FinalProductCost
        {
            get { return fFinalProductCost; }
            set { SetPropertyValue<decimal>("FinalProductCost", ref fFinalProductCost, value); }
        }

        decimal fSellingPrice;
        public decimal SellingPrice
        {
            get { return fSellingPrice; }
            set { SetPropertyValue<decimal>("SellingPrice", ref fSellingPrice, value); }
        }

        bool fAllPOSPathologies;
        public bool AllPOSPathologies
        {
            get { return fAllPOSPathologies; }
            set { SetPropertyValue<bool>("AllPOSPathologies", ref fAllPOSPathologies, value); }
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

        #endregion

        #region "Navigation"

        [Association(@"Inventory_PurchaseOrderDetailReferencesInventory_InventoryProduct", typeof(InventoryPurchaseOrderDetailReportXpo))]
        public XPCollection<InventoryPurchaseOrderDetailReportXpo> Inventory_PurchaseOrderDetails { get { return GetCollection<InventoryPurchaseOrderDetailReportXpo>("Inventory_PurchaseOrderDetails"); } }

        [Association(@"InventoryBatchSerialReportXpoReferencesInventoryProductReportXpo", typeof(InventoryBatchSerialReportXpo))]
        public XPCollection<InventoryBatchSerialReportXpo> InventoryBatchSerialReportXpo { get { return GetCollection<InventoryBatchSerialReportXpo>("InventoryBatchSerialReportXpo"); } }

        [Association(@"InventoryKardexReportXpoReferencesInventoryProductReportXpo", typeof(InventoryKardexReportXpo))]
        public XPCollection<InventoryKardexReportXpo> InventoryKardexReportXpo { get { return GetCollection<InventoryKardexReportXpo>("InventoryKardexReportXpo"); } }
        
        [Association(@"Inventory_RemissionEntranceDetailReferencesInventoryProductReportXpo", typeof(InventoryRemissionEntranceDetailReportXpo))]
        public XPCollection<InventoryRemissionEntranceDetailReportXpo> InventoryRemissionEntranceDetails { get { return GetCollection<InventoryRemissionEntranceDetailReportXpo>("InventoryRemissionEntranceDetails"); } }

        [Association(@"Inventory_EntranceVoucherDetailReferencesInventoryProductReportXpo", typeof(InventoryEntranceVoucherDetailReportXpo))]
        public XPCollection<InventoryEntranceVoucherDetailReportXpo> InventoryEntranceVoucherDetails { get { return GetCollection<InventoryEntranceVoucherDetailReportXpo>("InventoryEntranceVoucherDetails"); } }

        [Association(@"InventoryPhysicalInventoryReportXpoReferencesInventoryProductReportXpo", typeof(InventoryPhysicalInventoryReportXpo))]
        public XPCollection<InventoryPhysicalInventoryReportXpo> InventoryPhysicalInventoryReportXpo { get { return GetCollection<InventoryPhysicalInventoryReportXpo>("InventoryPhysicalInventoryReportXpo"); } }
        
        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventoryProductReportXpo", typeof(InventoryRemissionDevolutionDetailReportXpo))]
        public XPCollection<InventoryRemissionDevolutionDetailReportXpo> Inventory_RemissionDevolutionDetailReportXpo { get { return GetCollection<InventoryRemissionDevolutionDetailReportXpo>("Inventory_RemissionDevolutionDetailReportXpo"); } }
        
        [Association(@"InventoryRemissionOutputDetailReportXpoReferencesInventoryProductReportXpo", typeof(InventoryRemissionOutputDetailReportXpo))]
        public XPCollection<InventoryRemissionOutputDetailReportXpo> InventoryRemissionOutputDetailReportXpo { get { return GetCollection<InventoryRemissionOutputDetailReportXpo>("InventoryRemissionOutputDetailReportXpo"); } }
        
        [Association(@"InventoryAdjustmentDetailReportXpoReferencesInventoryProductReportXpo", typeof(InventoryAdjustmentDetailReportXpo))]
        public XPCollection<InventoryAdjustmentDetailReportXpo> InventoryAdjustmentDetailReportXpo { get { return GetCollection<InventoryAdjustmentDetailReportXpo>("InventoryAdjustmentDetailReportXpo"); } }

        [Association(@"InventoryLoanMerchandiseDetailReportXpoReferencesInventoryProductReportXpo", typeof(InventoryLoanMerchandiseDetailReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDetailReportXpo> InventoryLoanDetailProductReportXpo { get { return GetCollection<InventoryLoanMerchandiseDetailReportXpo>("InventoryLoanDetailProductReportXpo"); } }
        
        [Association(@"InventoryTransferOrderDetailReportXpoReferencesInventoryProductReportXpo", typeof(InventoryTransferOrderDetailReportXpo))]
        public XPCollection<InventoryTransferOrderDetailReportXpo> InventoryTransferOrderDetailProductReportXpo { get { return GetCollection<InventoryTransferOrderDetailReportXpo>("InventoryTransferOrderDetailProductReportXpo"); } }

        [Association(@"InventoryRequestDetailReportXpoReferencesInventoryProductReportXpo", typeof(InventoryRequestDetailReportXpo))]
        public XPCollection<InventoryRequestDetailReportXpo> InventoryRequestDetailProductReportXpo { get { return GetCollection<InventoryRequestDetailReportXpo>("InventoryRequestDetailProductReportXpo"); } }

        [Association(@"InventoryDocumentInvoiceProductSalesDetailReportXpoReferencesInventoryProductReportXpo", typeof(InventoryDocumentInvoiceProductSalesDetailReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesDetailReportXpo> InventoryDocumentInvoiceProductSalesDetailReport { get { return GetCollection<InventoryDocumentInvoiceProductSalesDetailReportXpo>("InventoryDocumentInvoiceProductSalesDetailReport"); } }

        [Association(@"Inventory_InventoryControlDetailReferencesInventory_InventoryProduct", typeof(InventoryControlDetailReportXpo))]
        public XPCollection<InventoryControlDetailReportXpo> InventoryControlDetails { get { return GetCollection<InventoryControlDetailReportXpo>("InventoryControlDetails"); } }

        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_InventoryProduct", typeof(InventoryPharmaceuticalDispensingDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailReportXpo> Inventory_PharmaceuticalDispensingDetails { get { return GetCollection<InventoryPharmaceuticalDispensingDetailReportXpo>("Inventory_PharmaceuticalDispensingDetails"); } }

        [Association(@"Inventory_PurchaseRequestDetailReferencesInventory_InventoryProduct", typeof(Report.InventoryPurchaseRequestDetailReportXpo))]
        public XPCollection<Report.InventoryPurchaseRequestDetailReportXpo> Inventory_PurchaseRequestDetails { get { return GetCollection<Report.InventoryPurchaseRequestDetailReportXpo>("Inventory_PurchaseRequestDetails"); } }

        #endregion

        public InventoryProductReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
