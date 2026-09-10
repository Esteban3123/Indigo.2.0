using Domain.Crystal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Inventory.POCO
{
    public class SurgicalExpenseSheetModel
    {
        public string FunctionalUnitName { get; set; }
        public string UserName { get; set; }
        public string WarehouseCode { get; set; }
        public string CostCenterCode { get; set; }
        public string CenterOfAttentionCode { get; set; }
        public string FunctionalUnitCode { get; set; }
        public string AdmissionNumber { get; set; }
        public string PatientCode { get; set; }
        public string ProfessionalCode { get; set; }
        public List<ProductSurgicalExpenseSheetModel> Products { get; set; }
        public int SurgicalExpenseSheetId { get; set; }
        public string TimeStamp { get; set; }


    }
}