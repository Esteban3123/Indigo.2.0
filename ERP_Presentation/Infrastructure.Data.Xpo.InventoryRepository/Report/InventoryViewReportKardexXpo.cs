using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewReportKardex")]
    public class InventoryViewReportKardexXpo : XPLiteObject
    {
        string fKardexId;
        [Key(true)]
        public string KardexId
        {
            get { return fKardexId; }
            set { SetPropertyValue<string>("KardexId", ref fKardexId, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }

        string fWarehouseCode;
        public string WarehouseCode
        {
            get { return fWarehouseCode; }
            set { SetPropertyValue<string>("WarehouseCode", ref fWarehouseCode, value); }
        }

        string fWarehouseName;
        public string WarehouseName
        {
            get { return fWarehouseName; }
            set { SetPropertyValue<string>("WarehouseName", ref fWarehouseName, value); }
        }

        string fWarehouseDescription;
        public string WarehouseDescription
        {
            get { return fWarehouseDescription; }
            set { SetPropertyValue<string>("WarehouseDescription", ref fWarehouseDescription, value); }
        }

        int fProductId;
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
        }

        string fProductCode;
        public string ProductCode
        {
            get { return fProductCode; }
            set { SetPropertyValue<string>("ProductCode", ref fProductCode, value); }
        }

        string fProductName;
        public string ProductName
        {
            get { return fProductName; }
            set { SetPropertyValue<string>("ProductName", ref fProductName, value); }
        }

        string fProductDescription;
        public string ProductDescription
        {
            get { return fProductDescription; }
            set { SetPropertyValue<string>("ProductDescription", ref fProductDescription, value); }
        }

        Boolean fProductControl;
        public Boolean ProductControl
        {
            get { return fProductControl; }
            set { SetPropertyValue<Boolean>("ProductControl", ref fProductControl, value); }
        }

        string fCodeCUM;
        public string CodeCUM
        {
            get { return fCodeCUM; }
            set { SetPropertyValue<string>("CodeCUM", ref fCodeCUM, value); }
        }

        string fHealthRegistration;
        public string HealthRegistration
        {
            get { return fHealthRegistration; }
            set { SetPropertyValue<string>("HealthRegistration", ref fHealthRegistration, value); }
        }

        int fBatchSerialId;
        public int BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<int>("BatchSerialId", ref fBatchSerialId, value); }
        }

        string fBatchCode;
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }

        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }

        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }

        byte fMovementType;
        public byte MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<byte>("MovementType", ref fMovementType, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }

        decimal fPreviousCost;
        public decimal PreviousCost
        {
            get { return fPreviousCost; }
            set { SetPropertyValue<decimal>("PreviousCost", ref fPreviousCost, value); }
        }

        decimal fAverageCost;
        public decimal AverageCost
        {
            get { return fAverageCost; }
            set { SetPropertyValue<decimal>("AverageCost", ref fAverageCost, value); }
        }

        decimal fPreviousAverageCost;
        public decimal PreviousAverageCost
        {
            get { return fPreviousAverageCost; }
            set { SetPropertyValue<decimal>("PreviousAverageCost", ref fPreviousAverageCost, value); }
        }

        int fPreviousAmountProduct;
        public int PreviousAmountProduct
        {
            get { return fPreviousAmountProduct; }
            set { SetPropertyValue<int>("PreviousAmountProduct", ref fPreviousAmountProduct, value); }
        }

        int fPreviousAmountWarehouse;
        public int PreviousAmountWarehouse
        {
            get { return fPreviousAmountWarehouse; }
            set { SetPropertyValue<int>("PreviousAmountWarehouse", ref fPreviousAmountWarehouse, value); }
        }

        int fPreviousAmountBatch;
        public int PreviousAmountBatch
        {
            get { return fPreviousAmountBatch; }
            set { SetPropertyValue<int>("PreviousAmountBatch", ref fPreviousAmountBatch, value); }
        }

        int fEntityId;
        public int EntityId
        {
            get { return fEntityId; }
            set { SetPropertyValue<int>("EntityId", ref fEntityId, value); }
        }

        string fEntityCode;
        public string EntityCode
        {
            get { return fEntityCode; }
            set { SetPropertyValue<string>("EntityCode", ref fEntityCode, value); }
        }

        string fEntityName;
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }

        int fImportedEntityId;
        public int ImportedEntityId
        {
            get { return fImportedEntityId; }
            set { SetPropertyValue<int>("ImportedEntityId", ref fImportedEntityId, value); }
        }

        string fImportedEntityCode;
        public string ImportedEntityCode
        {
            get { return fImportedEntityCode; }
            set { SetPropertyValue<string>("ImportedEntityCode", ref fImportedEntityCode, value); }
        }

        string fImportedEntityName;
        public string ImportedEntityName
        {
            get { return fImportedEntityName; }
            set { SetPropertyValue<string>("ImportedEntityName", ref fImportedEntityName, value); }
        }

        bool fAffectInventory;
        public bool AffectInventory
        {
            get { return fAffectInventory; }
            set { SetPropertyValue<bool>("AffectInventory", ref fAffectInventory, value); }
        }

        string fNamePatient;
        public string NamePatient
        {
            get { return fNamePatient; }
            set { SetPropertyValue<string>("NamePatient", ref fNamePatient, value); }
        }

        int fQuantityEntrance;
        public int QuantityEntrance
        {
            get { return fQuantityEntrance; }
            set { SetPropertyValue<int>("QuantityEntrance", ref fQuantityEntrance, value); }
        }

        int fQuantityExit;
        public int QuantityExit
        {
            get { return fQuantityExit; }
            set { SetPropertyValue<int>("QuantityExit", ref fQuantityExit, value); }
        }

        decimal fPreviousCostTotalCalculated;
        public decimal PreviousCostTotalCalculated
        {
            get { return fPreviousCostTotalCalculated; }
            set { SetPropertyValue<decimal>("PreviousCostTotalCalculated", ref fPreviousCostTotalCalculated, value); }
        }

        decimal fCostTotalCalculated;
        public decimal CostTotalCalculated
        {
            get { return fCostTotalCalculated; }
            set { SetPropertyValue<decimal>("CostTotalCalculated", ref fCostTotalCalculated, value); }
        }

        decimal fPreviousAmountProductCalculated;
        public decimal PreviousAmountProductCalculated
        {
            get { return fPreviousAmountProductCalculated; }
            set { SetPropertyValue<decimal>("PreviousAmountProductCalculated", ref fPreviousAmountProductCalculated, value); }
        }

        decimal fPreviousAmountWarehouseCalculated;
        public decimal PreviousAmountWarehouseCalculated
        {
            get { return fPreviousAmountWarehouseCalculated; }
            set { SetPropertyValue<decimal>("PreviousAmountWarehouseCalculated", ref fPreviousAmountWarehouseCalculated, value); }
        }

        decimal fPreviousAmountBatchCalculated;
        public decimal PreviousAmountBatchCalculated
        {
            get { return fPreviousAmountBatchCalculated; }
            set { SetPropertyValue<decimal>("PreviousAmountBatchCalculated", ref fPreviousAmountBatchCalculated, value); }
        }

        string fDocumentDescription;
        public string DocumentDescription
        {
            get { return fDocumentDescription; }
            set { SetPropertyValue<string>("DocumentDescription", ref fDocumentDescription, value); }
        }

        public InventoryViewReportKardexXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
