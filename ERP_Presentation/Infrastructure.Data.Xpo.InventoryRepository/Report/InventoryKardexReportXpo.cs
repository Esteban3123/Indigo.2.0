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
    [Persistent(@"Inventory.Kardex")]
    public class InventoryKardexReportXpo : XPLiteObject
    {

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        byte fMovementType;
        public byte MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<byte>("MovementType", ref fMovementType, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryKardexReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"InventoryKardexReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"InventoryKardexReportXpoReferencesInventoryBatchSerialReportXpo")]
        public InventoryBatchSerialReportXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<InventoryBatchSerialReportXpo>("BatchSerialId", ref fBatchSerialId, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
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

        public InventoryKardexReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
