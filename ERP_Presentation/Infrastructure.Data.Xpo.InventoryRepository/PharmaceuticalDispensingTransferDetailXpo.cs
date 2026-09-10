using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PharmaceuticalDispensingDevolutionDetail")]
    public class PharmaceuticalDispensingTransferDetailXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        PharmaceuticalDispensingTransferXpo fPharmaceuticalDispensingTransferId;
        [Association(@"Inventory_PharmaceuticalDispensingTransferDetail_References_Inventory_PharmaceuticalDispensingTransfer")]
        public PharmaceuticalDispensingTransferXpo PharmaceuticalDispensingTransferId
        {
            get { return fPharmaceuticalDispensingTransferId; }
            set { SetPropertyValue<PharmaceuticalDispensingTransferXpo>("PharmaceuticalDispensingTransferId", ref fPharmaceuticalDispensingTransferId, value); }
        }

        int fPharmaceuticalDispensingDetailBatchSerialId;
        public int PharmaceuticalDispensingDetailBatchSerialId
        {
            get { return fPharmaceuticalDispensingDetailBatchSerialId; }
            set { SetPropertyValue<int>("PharmaceuticalDispensingDetailBatchSerialId", ref fPharmaceuticalDispensingDetailBatchSerialId, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        #endregion

        #region Builder

        public PharmaceuticalDispensingTransferDetailXpo(Session session) : base(session) { }

        #endregion

    }
}