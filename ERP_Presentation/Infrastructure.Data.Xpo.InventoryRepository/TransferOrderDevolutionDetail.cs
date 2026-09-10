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
    [Persistent(@"Inventory.TransferOrderDevolutionDetail")]
    public class TransferOrderDevolutionDetail : XPLiteObject
    {

         int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        TransferOrderDevolutionXpo fTransferOrderDevolutionId;
        [Association(@"TransferOrderDevolutionDetailReferencesTransferOrderDevolutionXpo")]
        public TransferOrderDevolutionXpo TransferOrderDevolutionId
        {
            get { return fTransferOrderDevolutionId; }
            set { SetPropertyValue<TransferOrderDevolutionXpo>("TransferOrderDevolutionId", ref fTransferOrderDevolutionId, value); }
        }
        InventoryTRansferOrderDetailBatchSerialXpo fTransferOrderDetailBatchSerialId;
        [Association(@"TransferOrderDevolutionDetailReferencesInventoryTRansferOrderDetailBatchSerialXpo")]
        public InventoryTRansferOrderDetailBatchSerialXpo TransferOrderDetailBatchSerialId
        {
            get { return fTransferOrderDetailBatchSerialId; }
            set { SetPropertyValue<InventoryTRansferOrderDetailBatchSerialXpo>("TransferOrderDetailBatchSerialId", ref fTransferOrderDetailBatchSerialId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        public TransferOrderDevolutionDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
