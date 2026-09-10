using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Models.DCI
{
    public class MDCI
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DCICrystal { get; set; }
        public int Status { get; set; }
        public string CreationUser { get; set; }
        public string CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ModificationDate { get; set; }
        public DCIATCEntity[] DCIATCEntity { get; set; }
        public DrugInteraction[] DrugInteraction { get; set; }
    }
}
