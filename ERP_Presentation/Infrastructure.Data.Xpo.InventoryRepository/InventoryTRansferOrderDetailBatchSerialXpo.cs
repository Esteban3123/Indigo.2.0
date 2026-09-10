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
    [Persistent(@"Inventory.TransferOrderDetailBatchSerial")]
    public class InventoryTRansferOrderDetailBatchSerialXpo : XPLiteObject
    {

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryTransferOrderDetailXpo fTransferOrderDetailId;
        [Association(@"InventoryTRansferOrderDetailBatchSerialXpoReferencesInventoryTransferOrderDetailXpo")]
        public InventoryTransferOrderDetailXpo TransferOrderDetailId
        {
            get { return fTransferOrderDetailId; }
            set { SetPropertyValue<InventoryTransferOrderDetailXpo>("TransferOrderDetailId", ref fTransferOrderDetailId, value); }
        }
        PhysicalInventoryXpo fPhysicalInventoryId;
        [Association(@"InventoryTRansferOrderDetailBatchSerialXpoReferencesPhysicalInventoryXpo")]
        public PhysicalInventoryXpo PhysicalInventoryId
        {
            get { return fPhysicalInventoryId; }
            set { SetPropertyValue<PhysicalInventoryXpo>("PhysicalInventoryId", ref fPhysicalInventoryId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }

        [Association(@"TransferOrderDevolutionDetailReferencesInventoryTRansferOrderDetailBatchSerialXpo", typeof(TransferOrderDevolutionDetail))]
        public XPCollection<TransferOrderDevolutionDetail> TransferOrderDevolutionDetail { get { return GetCollection<TransferOrderDevolutionDetail>("TransferOrderDevolutionDetail"); } }

        public InventoryTRansferOrderDetailBatchSerialXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
