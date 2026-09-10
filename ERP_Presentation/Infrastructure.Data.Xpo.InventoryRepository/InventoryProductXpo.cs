using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.InventoryProduct")]
    public partial class InventoryProductXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        [Indexed(Name = @"IX_InventoryProduct", Unique = true)]
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

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        ProductTypeXpo fProductTypeId;
        [Association("ProductTypeReferencesInventoryProduct")]
        public ProductTypeXpo ProductTypeId
        {
            get { return fProductTypeId; }
            set { SetPropertyValue<ProductTypeXpo>("ProductTypeId", ref fProductTypeId, value); }
        }

        [PersistentAlias("ProductTypeId.ClassName")]
        public string ClassName { get { return Convert.ToString(EvaluateAlias("ClassName")); } }

        ATCXpo fATCId;
        [Association(@"Inventory_InventoryProductReferencesInventory_ATC")]
        public ATCXpo ATCId
        {
            get { return fATCId; }
            set { SetPropertyValue<ATCXpo>("ATCId", ref fATCId, value); }
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

        MeasureUnitXpo fMeasurementUnitId;
        [Association(@"InventoryMeasurementUnit")]
        public MeasureUnitXpo MeasurementUnitId
        {
            get { return fMeasurementUnitId; }
            set { SetPropertyValue<MeasureUnitXpo>("MeasurementUnitId", ref fMeasurementUnitId, value); }
        }

        ProductGroupXpo fProductGroupId;
        [Association(@"ProductGroup")]
        public ProductGroupXpo ProductGroupId
        {
            get { return fProductGroupId; }
            set { SetPropertyValue<ProductGroupXpo>("ProductGroupId", ref fProductGroupId, value); }
        }

        ProductSubGroupXpo fProductSubGroupId;
        [Association(@"ProductSubGroup")]
        public ProductSubGroupXpo ProductSubGroupId
        {
            get { return fProductSubGroupId; }
            set { SetPropertyValue<ProductSubGroupXpo>("ProductSubGroupId", ref fProductSubGroupId, value); }
        }

        PackagingUnitXpo fPackagingUnitId;
        [Association(@"PackagingUnit")]
        public PackagingUnitXpo PackagingUnitId
        {
            get { return fPackagingUnitId; }
            set { SetPropertyValue<PackagingUnitXpo>("PackagingUnitId", ref fPackagingUnitId, value); }
        }

        ManufacturersXpo fManufacturerId;
        [Association(@"Manufacturer")]
        public ManufacturersXpo ManufacturerId
        {
            get { return fManufacturerId; }
            set { SetPropertyValue<ManufacturersXpo>("ManufacturerId", ref fManufacturerId, value); }
        }

        GeneralLedgerIVAXpo fIVAId;
        [Association(@"GeneralLedgerIVA")]
        public GeneralLedgerIVAXpo IVAId
        {
            get { return fIVAId; }
            set { SetPropertyValue<GeneralLedgerIVAXpo>("IVAId", ref fIVAId, value); }
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

        bool fTaxedProduct;
        public bool TaxedProduct
        {
            get { return fTaxedProduct; }
            set { SetPropertyValue<bool>("TaxedProduct", ref fTaxedProduct, value); }
        }

        bool fHandlesHealthRegistration;
        public bool HandlesHealthRegistration
        {
            get { return fHandlesHealthRegistration; }
            set { SetPropertyValue<bool>("HandlesHealthRegistration", ref fHandlesHealthRegistration, value); }
        }

        string fHealthRegistration;
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

        int? fSanitaryRegistration;
        public int? SanitaryRegistration
        {
            get { return fSanitaryRegistration; }
            set { SetPropertyValue<int?>("SanitaryRegistration", ref fSanitaryRegistration, value); }
        }

        int fBillingGroupId;
        public int BillingGroupId
        {
            get { return fBillingGroupId; }
            set { SetPropertyValue<int>("BillingGroupId", ref fBillingGroupId, value); }
        }

        InventorySupplieXpo fSupplieId;
        [Association(@"Inventory_InventoryProductReferencesInventory_Supplie")]
        public InventorySupplieXpo SupplieId
        {
            get { return fSupplieId; }
            set { SetPropertyValue<InventorySupplieXpo>("SupplieId", ref fSupplieId, value); }
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

        int FMaximumStockWarehouse;
        [NonPersistent()]
        public int MaximumStockWarehouse
        {
            get { return FMaximumStockWarehouse; }
            set { this.FMaximumStockWarehouse = value; }
        }

        int FMinimumStockWarehouse;
        [NonPersistent()]
        public int MinimumStockWarehouse
        {
            get { return FMinimumStockWarehouse; }
            set { this.FMinimumStockWarehouse = value; }
        }

        int FRepositionPointWarehouse;
        [NonPersistent()]
        public int RepositionPointWarehouse
        {
            get { return FRepositionPointWarehouse; }
            set { this.FRepositionPointWarehouse = value; }
        }

        string fCreationUserWarehouseStock;
        [NonPersistent()]
        public string CreationUserWarehouseStock
        {
            get { return fCreationUserWarehouseStock; }
            set { SetPropertyValue<string>("CreationUserWarehouseStock", ref fCreationUserWarehouseStock, value); }
        }

        DateTime fCreationDateWarehouseStock;
        [NonPersistent()]
        public DateTime CreationDateWarehouseStock
        {
            get { return fCreationDateWarehouseStock; }
            set { SetPropertyValue<DateTime>("CreationDateWarehouseStock", ref fCreationDateWarehouseStock, value); }
        }

        string fModificationUserWarehouseStock;
        [NonPersistent()]
        public string ModificationUserWarehouseStock
        {
            get { return fModificationUserWarehouseStock; }
            set { SetPropertyValue<string>("ModificationUserWarehouseStock", ref fModificationUserWarehouseStock, value); }
        }

        DateTime fModificationDateWarehouseStock;
        [NonPersistent()]
        public DateTime ModificationDateWarehouseStock
        {
            get { return fModificationDateWarehouseStock; }
            set { SetPropertyValue<DateTime>("ModificationDateWarehouseStock", ref fModificationDateWarehouseStock, value); }
        }

        bool fSelectOption;
        [NonPersistent()]
        public bool SelectOption
        {
            get { return fSelectOption; }
            set { SetPropertyValue<bool>("SelectOption", ref fSelectOption, value); }
        }

        #endregion

        #region Custom Members

        [PersistentAlias("Iif(" +
            "SanitaryRegistration = 1, 'Vigente', " +
            "SanitaryRegistration = 2, 'En Tramite Renov', " +
            "SanitaryRegistration = 3, 'Vencido', " +
            "SanitaryRegistration = 4, 'Abandono', " +
            "SanitaryRegistration = 5, 'Cancelado', " +
            "SanitaryRegistration = 6, 'Negado', " +
            "SanitaryRegistration = 7, 'Perdida Fuerza Ejec', " +
            "SanitaryRegistration = 8, 'Revocado', " +
            "SanitaryRegistration = 9, 'Suspendido', " +
            "SanitaryRegistration = 10, 'Inactivo', " +
            "'N/A')")]
        public string SanitaryRegistrationName
        {
            get { return Convert.ToString(this.EvaluateAlias("SanitaryRegistrationName")); }
        }

        #endregion

        #region Associations

        [Association(@"Inventory_PhysicalInventoryReferencesInventory_InventoryProduct", typeof(PhysicalInventoryXpo))]
        public XPCollection<PhysicalInventoryXpo> PhysicalInventoryXpo { get { return GetCollection<PhysicalInventoryXpo>("PhysicalInventoryXpo"); } }

        [Association(@"InventoryTransferOrderDetailXpoReferencesInventoryProductXpo", typeof(InventoryTransferOrderDetailXpo))]
        public XPCollection<InventoryTransferOrderDetailXpo> InventoryTransferOrderDetailXpo { get { return GetCollection<InventoryTransferOrderDetailXpo>("InventoryTransferOrderDetailXpo"); } }

        [Association(@"Inventory_InventoryControlDetailReferencesInventory_InventoryProduct", typeof(InventoryControlDetailXpo))]
        public XPCollection<InventoryControlDetailXpo> Inventory_InventoryControlDetails { get { return GetCollection<InventoryControlDetailXpo>("Inventory_InventoryControlDetails"); } }

        [Association(@"Inventory_BatchSerialReferencesInventory_InventoryProduct", typeof(BatchSerialXpo))]
        public XPCollection<BatchSerialXpo> Inventory_BatchSerials { get { return GetCollection<BatchSerialXpo>("Inventory_BatchSerials"); } }

        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_InventoryProduct", typeof(PharmaceuticalDispensingDetailXpo))]
        public XPCollection<PharmaceuticalDispensingDetailXpo> Inventory_PharmaceuticalDispensingDetails { get { return GetCollection<PharmaceuticalDispensingDetailXpo>("Inventory_PharmaceuticalDispensingDetails"); } }

        [Association("ProductTemplateReferencesInventoryProduct", typeof(ProductRateDetailXpo))]
        public XPCollection<ProductRateDetailXpo> ProductRateDetailXpo { get { return GetCollection<ProductRateDetailXpo>("ProductRateDetailXpo"); } }

        [Association(@"InventoryRemissionEntranceDetailXpoReferencesInventoryProductXpo", typeof(InventoryRemissionEntranceDetailXpo))]
        public XPCollection<InventoryRemissionEntranceDetailXpo> InventoryRemissionEntranceDetailXpo { get { return GetCollection<InventoryRemissionEntranceDetailXpo>("InventoryRemissionEntranceDetailXpo"); } }

        [Association(@"InventoryPurcharseOrderDetailXpoReferencesInventoryProductXpo", typeof(InventoryPurcharseOrderDetailXpo))]
        public XPCollection<InventoryPurcharseOrderDetailXpo> InventoryPurcharseOrderDetailXpo { get { return GetCollection<InventoryPurcharseOrderDetailXpo>("InventoryPurcharseOrderDetailXpo"); } }

        [Association(@"InventoryContractDetailXpoReferencesInventoryProductXpo", typeof(InventoryContractDetailXpo))]
        public XPCollection<InventoryContractDetailXpo> InventoryContractDetailXpo { get { return GetCollection<InventoryContractDetailXpo>("InventoryContractDetailXpo"); } }

        [Association(@"ConsignmentInventoryRemissionDetailXpoReferencesInventoryProductXpo", typeof(ConsignmentInventoryRemissionDetailXpo))]
        public XPCollection<ConsignmentInventoryRemissionDetailXpo> ConsignmentInventoryRemissionDetailXpo { get { return GetCollection<ConsignmentInventoryRemissionDetailXpo>("ConsignmentInventoryRemissionDetailXpo"); } }

        [Association(@"InventoryRequestDetailXpoReferencesInventoryProductXpo", typeof(InventoryRequestDetailXpo))]
        public XPCollection<InventoryRequestDetailXpo> InventoryRequestDetailXpo { get { return GetCollection<InventoryRequestDetailXpo>("InventoryRequestDetailXpo"); } }

        [Association(@"Inventory_PhysicalInventoryCustodyReferencesInventory_InventoryProduct", typeof(PhysicalInventoryCustodyXpo))]
        public XPCollection<PhysicalInventoryCustodyXpo> PhysicalInventoryCustody { get { return GetCollection<PhysicalInventoryCustodyXpo>("PhysicalInventoryCustody"); } }

        [Association("ProductRateDetailPackage_References_Product", typeof(ProductRateDetailPackageXpo))]
        public XPCollection<ProductRateDetailPackageXpo> ProductRateDetailPackages { get { return GetCollection<ProductRateDetailPackageXpo>("ProductRateDetailPackages"); } }

        [Association("PackageDetail_References_Product", typeof(PackageDetailXpo))]
        public XPCollection<PackageDetailXpo> PackageDetails { get { return GetCollection<PackageDetailXpo>("PackageDetails"); } }

        [Association("ProductHierarchyXpoReferencesPurchaseRequestXpo", typeof(ProductHierarchyXpo))]
        public XPCollection<ProductHierarchyXpo> ProductHierarchy { get { return GetCollection<ProductHierarchyXpo>("ProductHierarchy"); } }

        [Association("InventoryProductInTransitDetailXpoReferencesInventoryProductXpo", typeof(InventoryProductInTransitDetailXpo))]
        public XPCollection<InventoryProductInTransitDetailXpo> InventoryProductInTransitDetailXpo { get { return GetCollection<InventoryProductInTransitDetailXpo>("InventoryProductInTransitDetailXpo"); } }

        [Association("InventortyProduct_WarehouseRestrictedConditionsXpo", typeof(WarehouseRestrictedConditionsXpo))]
        public XPCollection<WarehouseRestrictedConditionsXpo> WarehouseRestrictedConditionsXpo { get { return GetCollection<WarehouseRestrictedConditionsXpo>("WarehouseRestrictedConditionsXpo"); } }

        [Association("InventoryRequestOtherDetailReportXpo_Reference_Product", typeof(InventoryRequestOtherDetailReportXpo))]
        public XPCollection<InventoryRequestOtherDetailReportXpo> InventoryRequestOtherDetailReportXpo { get { return GetCollection<InventoryRequestOtherDetailReportXpo>("InventoryRequestOtherDetailReportXpo"); } }

        [Association("InventoryConsigmentTransferDetailXpoReferencesInventoryProduct", typeof(InventoryConsigmentTransferDetailXpo))]
        public XPCollection<InventoryConsigmentTransferDetailXpo> InventoryConsigmentTransferDetailXpo { get { return GetCollection<InventoryConsigmentTransferDetailXpo>("InventoryConsigmentTransferDetailXpo"); } }

        #endregion

        #region Builder

        public InventoryProductXpo(Session session) : base(session) { }
        public InventoryProductXpo()
          : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }

}
