using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Models.ElectronicRIPS
{
    class MElectronicRIPS
    {
        /// <summary>
        /// Lista de los codigos (Codigo de nota, numero de factura)
        /// </summary>
        public List<string> EntityCode { get; set; }

        /// <summary>
        /// proceso que dispara el evento
        /// "Invoice",
        ///"BillingNote",
        ///"BillingNoteAdjustment",
        ///"ResendInvoice",
        ///"ResendNote",
        ///"ResendNoteAdjustment"
        /// </summary>
        public string EntityName { get; set; }
    }
}
