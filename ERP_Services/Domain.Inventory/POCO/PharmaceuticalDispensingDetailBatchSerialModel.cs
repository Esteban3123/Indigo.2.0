using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Inventory.POCO
{
    public class PharmaceuticalDispensingDetailBatchSerialModel
    {
        public int PhysicalInventoryId { get; set; }
        public int Quantity { get; set; }
        public int OutstandingQuantity { get; set; }
        public int ProductId { get; set; }
        public int IdWarehouse { get; set; }
        public int? BatchSerialId { get; set; }
        public string UserIndigo { get; set; }

    }
}
