using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.TransferOrderDetail")]
    public class InventoryTransferOrderDetailXpo : XPLiteObject
    {

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryTransferOrderXpo fTransferOrderId;
        [Association(@"InventoryTransferOrderDetailXpoReferencesInventoryTransferOrderXpo")]
        public InventoryTransferOrderXpo TransferOrderId
        {
            get { return fTransferOrderId; }
            set { SetPropertyValue<InventoryTransferOrderXpo>("TransferOrderId", ref fTransferOrderId, value); }
        }
        InventoryProductXpo fProductId;
        [Association(@"InventoryTransferOrderDetailXpoReferencesInventoryProductXpo")]
        public InventoryProductXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductXpo>("ProductId", ref fProductId, value); }
        }
        int fInventoryQuantity;
        public int InventoryQuantity
        {
            get { return fInventoryQuantity; }
            set { SetPropertyValue<int>("InventoryQuantity", ref fInventoryQuantity, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        int fQuantityRequest;
        [NonPersistent()]
        public int QuantityRequest
        {
            get { return fQuantityRequest; }
            set { this.fQuantityRequest = value; }
        }

        [Association(@"InventoryTRansferOrderDetailBatchSerialXpoReferencesInventoryTransferOrderDetailXpo", typeof(InventoryTRansferOrderDetailBatchSerialXpo))]
        public XPCollection<InventoryTRansferOrderDetailBatchSerialXpo> Inventory_TransferOrderDetailBatchSerials { get { return GetCollection<InventoryTRansferOrderDetailBatchSerialXpo>("InventoryTRansferOrderDetailBatchSerialXpo"); } }

        public InventoryTransferOrderDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
