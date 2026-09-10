using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventHandlers.Model
{
    public class ErrorHandler
    {
        public string InvoiceNumber { get; set; }
        public DateTime DateError { get; set; }
        public string Error { get; set; }
        public string CodeError { get; set; }
    }
}
