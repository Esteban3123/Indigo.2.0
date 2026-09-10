//'*************************************************************
//' Assembly         : Infrastructure.Data.Xpo.InventoryRepository
//' Author           : Mariana Gonzalez Calderon
//' Created          : 06-11-2025
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ConsignmentTransferDetail")]
    public class InventoryConsigmentTransferDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        ConsignmentTransferXpo fConsignmentTransferId;
        [Association(@"ConsignmentTransferDetailReferencesConsignmentTransfer")]
        [Persistent("ConsignmentTransferId")]
        public ConsignmentTransferXpo ConsignmentTransferId
        {
            get { return fConsignmentTransferId; }
            set { SetPropertyValue<ConsignmentTransferXpo>("ConsignmentTransferId", ref fConsignmentTransferId, value); }
        }

        WarehouseXpo fWarehouseId;
        [Association(@"InventoryConsigmentTransferDetailXpoReferencesWarehouse")]
        [Persistent("WarehouseId")]
        public WarehouseXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<WarehouseXpo>("WarehouseId", ref fWarehouseId, value); }
        }

        InventoryProductXpo fProductId;
        [Association(@"InventoryConsigmentTransferDetailXpoReferencesInventoryProduct")]
        [Persistent("ProductId")]
        public InventoryProductXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductXpo>("ProductId", ref fProductId, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        decimal fProductCost;
        public decimal ProductCost
        {
            get { return fProductCost; }
            set { SetPropertyValue<decimal>("ProductCost", ref fProductCost, value); }
        }

        string fDescription;
        [Size(500)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        [NonPersistent()]
        public decimal TotalCost
        {
            get { return fQuantity * fProductCost; }
        }

        [Association(@"ConsignmentTransferDetail_Reference_BatchSerials", typeof(ConsignmentTransferDetailBatchSerialXpo))]
        public XPCollection<ConsignmentTransferDetailBatchSerialXpo> BatchSerials
        {
            get { return GetCollection<ConsignmentTransferDetailBatchSerialXpo>(nameof(BatchSerials)); }
        }

        public InventoryConsigmentTransferDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
