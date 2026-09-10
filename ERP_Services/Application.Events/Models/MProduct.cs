using Application.Events.Models.Product;

namespace Application.Events.Models
{
    public class MProduct
    {
        public string Code;
        public string Name;
        public Product.ProductType ProductType;
        public ATC ATC;
        public string CodeCUM;
        public string CodeAlternative;
        public string CodeAlternativeTwo;
        public string Description;
        public Product.ProductGroup ProductGroup;
        public Product.ProductSubGroup ProductSubGroup;
        public MeasurementUnit MeasurementUnit;
        public Models.Product.PackagingUnit PackagingUnit;
        public Product.Manufacturer Manufacturer;
        public IVA IVA;
        public string Presentation;
        public string CodeSICE;
        public int? HandlesSerial;
        public int? HandlesHealthRegistration;
        public string HealthRegistration;
        public string ExpirationDate;
        public BillingGroup BillingGroup;
        public int? ProductControl;
        public int? ProductWithPriceControl;
        public int? POSProduct;
        public int? AuthorizationByOrderNumber;
        public int? ExpirationDay;
        public int? MaximumControlPeriod;
        public int? ControlDays;
        public int? ControlOrderQuantity;
        public int? ProductOrderAmount;
        public string LastPurchase;
        public string LastSale;
        public int? ProductOrigin;
        public int? MinimumStock;
        public int? MaximumStock;
        public decimal? CommissionPercentage;
        public int? RepositionPoint;
        public int? ResetTime;
        public int? CurrencyType;
        public decimal ProductCost;
        public decimal? FinalProductCost;
        public decimal? SellingPrice;
        public int AllPOSPathologies;
        public int Status;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
        public PBillingGroupNoPos BillingGroupNoPos;
        public decimal? ControlCostPercentage;
        public PInventoryRiskLevel InventoryRiskLevel;
        public string SerialNumber;
        public int? DriveUnit;
        public int? MinimumTemperature;
        public int? MaximumTemperature;
        public int? SanitaryRegistration;
        public int? Consumption;
        public int? JustificationSuppliesDispositives;
        public int? OsteosynthesisMaterial;
        public string Abbreviation;
        public Supplie Supplie;
        public string IUM;
        public int? Storage;
        public byte TaxedProduct;
        public decimal Osmolarity;
        public byte LiquidateSalesTaxes;
        public byte SismedReport;
        public byte DairyComponent;
        public int? DairyComponentType;
        public ProductBarcode[] ProductBarcode;        
    }
}
