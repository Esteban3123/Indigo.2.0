using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Inventory.POCO
{
    public class AccountingMovement
    {
        public int Id { get; set; }
        public int LegalBookId { get; set; }
        public int JournalVoucherTypeId { get; set; }
        public DateTime VoucherDate { get; set; }
        public string Detail { get; set; }
        public string EntityCode { get; set; }
        public int? EntityId { get; set; }
        public string EntityName { get; set; }
        public string JournalVoucherXml { get; set; }
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
