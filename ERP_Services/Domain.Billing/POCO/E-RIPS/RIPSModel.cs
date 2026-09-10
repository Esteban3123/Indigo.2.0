using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO
{
    public class RIPSModel
    {
        public ElectronicRIPSModel rips { get; set; }
        public string xmlFevFile { get; set; } = string.Empty;
    }
}
