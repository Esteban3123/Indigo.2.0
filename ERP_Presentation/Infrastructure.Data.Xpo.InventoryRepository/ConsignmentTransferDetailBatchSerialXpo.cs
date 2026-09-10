//'*************************************************************
//' Assembly         : Infrastructure.Data.Xpo.InventoryRepository
//' Author           : Mariana Gonzalez Calderon
//' Created          : 06-11-2025
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ConsignmentTransferDetailBatchSerial")]
    public class ConsignmentTransferDetailBatchSerialXpo : XPLiteObject
    {
        public ConsignmentTransferDetailBatchSerialXpo(Session session) : base(session) { }

        [Key(true)]
        public int Id { get; set; }

        [Association(@"ConsignmentTransferDetail_Reference_BatchSerials"), Persistent("ConsignmentTransferDetailId")]
        public InventoryConsigmentTransferDetailXpo ConsignmentTransferDetail { get; set; }

        [Association(@"PhysicalInventoryConsignmentTransferDetailBatchSerials"), Persistent("PhysicalInventoryId")]
        public PhysicalInventoryXpo PhysicalInventory { get; set; }

        public int Quantity { get; set; }

        [PersistentAlias("PhysicalInventory.BatchSerialId.BatchCode")]
        public string BatchCode => Convert.ToString(EvaluateAlias(nameof(BatchCode)));

        [PersistentAlias("PhysicalInventory.BatchSerialId.ExpirationDate")]
        public DateTime? ExpirationDate
        {
            get
            {
                object value = EvaluateAlias(nameof(ExpirationDate));
                return value != null ? Convert.ToDateTime(value) : (DateTime?)null;
            }
        }
    }
}

