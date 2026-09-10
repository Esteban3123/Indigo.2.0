using Application.Events.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Application.Events.Models.MedicalFeesContract
{
    class MethodsMedicalFeesContract
    {
        public List<string> GetHealthProfessionalsCodes(int contractId)
        {
            var professionals = new List<string>();
            ExecuteCommand execute = new ExecuteCommand();
            string query = @"
                SELECT DISTINCT hpc.HealthProfessionalCode
                FROM MedicalFees.HealthProfessionalContract hpc
                WHERE hpc.MedicalFeesContractId = @ContractId";
                
            DataTable dtProfessionals = execute.GeneralExecuteQuerySqlCommand(query, "ContractId", contractId);
            
            if (dtProfessionals != null && dtProfessionals.Rows.Count > 0)
            {
                foreach (DataRow row in dtProfessionals.Rows)
                {
                    var professionalCode = Convert.ToString(row["HealthProfessionalCode"]);
                    if (!string.IsNullOrWhiteSpace(professionalCode))
                    {
                        professionals.Add(professionalCode.Trim());
                    }
                }
            }
            
            return professionals;
        }
    }
}
