using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Models.InvoiceEntityCapitated
{
    public class PreviousRIPSInvoice
    {
        public string InvoiceNumber { get; set; }
        public string InitialDate { get; set; }
        public string EndDate { get; set; }
        public byte InvoicePeriod { get; set; }
    }
}
