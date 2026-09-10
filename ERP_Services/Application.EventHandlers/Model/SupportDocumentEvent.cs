using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventHandlers.Model
{
    public class SupportDocumentEvent
    {
        public int EntityId { get; set; }

        public string EntityCode { get; set; }

        public string EntityName { get; set; }

        public string DocumentDate { get; set; }
    }
}
