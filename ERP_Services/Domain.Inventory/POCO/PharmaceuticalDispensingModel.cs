using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Inventory.POCO
{
    public class PharmaceuticalDispensingModel
    {
        public string Code { get; set; }
        public int OperatingUnitId { get; set; }
        public string AdmissionNumber { get; set; }
        public string DocumentDate { get; set; }
        public bool AffectInventory { get; set; }
        public byte Status { get; set; }
        public PharmaceuticalDispensingDetailModel[] PharmaceuticalDispensingDetail { get; set; }
        public string CodePatient { get; set; }
        public string PantientName { get; set; }
        public string CareCenterCode { get; set; }
        public string FunctionUnitCode { get; set; }
        public string FunctionUnitName { get; set; }
        public int? ConsecutivePescription { get; set; }
        public int ConsecutiveInputs { get; set; }
        public string ConsecutivePharmacy { get; set; }
        public string ConsecutiveCrystal { get; set; }
        public string HistoryType { get; set; }
        public string CodeNameWarehouse { get; set; }
        public int? OfficeType { get; set; }
        public int? LogisticOperator { get; set; }
        public bool Validado { get; set; }
    }
}
