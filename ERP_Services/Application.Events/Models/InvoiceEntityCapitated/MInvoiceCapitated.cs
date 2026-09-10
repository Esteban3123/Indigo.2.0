using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Models.InvoiceEntityCapitated
{
    public class MInvoiceCapitated
    {
        public string Code { get; set; }
        public OperatingUnit OperatingUnit { get; set; }
        public string DocumentDate { get; set; }
        public CareGroup CareGroup { get; set; } 
        public string InitialDate { get; set; }
        public string EndDate { get; set; }
        public int UserNumber { get; set; }
        public decimal UserValue { get; set; }
        public InvoiceCategory InvoiceCategory { get; set; }
        public string InvoiceNumber { get; set; }
        public byte Status { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TotalValue { get; set; }
        public byte? InvoicePeriod { get; set; }
        public PreviousRIPSInvoice PreviousRIPSInvoice { get; set; }
        public string CreationUser { get; set; }
        public string CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ModificationDate { get; set; }

    }
}
