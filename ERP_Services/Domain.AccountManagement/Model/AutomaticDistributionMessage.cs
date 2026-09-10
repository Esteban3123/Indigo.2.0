using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AccountManagement.Model
{
    public class AutomaticDistributionMessage
    {
        public string AdmissionNumber { get; set; }
        public string PatientCode { get; set; }
        public int EntryType { get; set; }
        public string EntryStatus { get; set; }
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; } 

    }
}
