using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AccountManagement.Model
{
    public class VPendingAssignment
    {
        public string AdmissionNumber { get; set; }
        public string PatientFullName { get; set; }
        public string PatientCode { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string Nit { get; set; }
        public string FunctionalUnitCode { get; set; }
        public string FunctionalUnitCodeName { get; set; }
        public string CareGroup { get; set; }
        public string CareGroupCode { get; set; }
        public string Bed { get; set; }
        public string Diagnosis { get; set; }
        public string Folio { get; set; }
        public int TypeIncome { get; set; }
        public string TypeIncomeName
        {
            get
            {
                if (TypeIncome == 1)
                {
                    return "Ambulatorio";
                }
                else if (TypeIncome == 2)
                {
                    return "Hospitalario";
                }
                else
                {
                    return "Tipo de ingreso no valido";
                }
            }
        }
        public string IncomeStatus { get; set; }
        public string UserCreation { get; set; }
        public string UserModificacion { get; set; }
        public bool Check { get; set; }

    }
}
