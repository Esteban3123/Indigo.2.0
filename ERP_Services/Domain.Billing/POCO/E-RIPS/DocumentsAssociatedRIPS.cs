using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
    public class DocumentsAssociatedRIPS
    {

        public string id { get; set; }

        public string Container { get; set; }

        public object Data { get; set; }

        public string CosmosRIPSId { get; set; }

        public string EntityName { get; set; }

        public string BlobUrl { get; set; }
    }
}
