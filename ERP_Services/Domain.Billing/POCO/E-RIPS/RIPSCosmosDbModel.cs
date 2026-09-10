using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
   public class RIPSCosmosDbModel
    {
        public string id { get; set; }
        public int? Retry { get; set; }
        public string Container { get; set; }
        public RIPSModel JsonRIPS { get; set; }
        public DocumentsAssociatedRIPS DocumentsAssociatedRIPS { get; set; }
        public string BlobUrl { get; set; }
        public string EntityName { get; set; }
    }
}
