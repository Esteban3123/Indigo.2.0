using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.ATCEntity
{
    public class MethodsATCEntity
    {
        public PharmacologicalGroup GetPharmacologicalGroup(int IdPharmacologicalGroup)
        {
            PharmacologicalGroup pharmacologicalGroup = new PharmacologicalGroup();            
            if (IdPharmacologicalGroup == 0) { return pharmacologicalGroup; };
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Code,Name FROM Inventory.PharmacologicalGroup WHERE Id = @IdPharmacologicalGroup";
            DataTable dbPharmacologicalGroup = execute.GeneralExecuteQuerySqlCommand(query, "IdPharmacologicalGroup", IdPharmacologicalGroup);
            if (dbPharmacologicalGroup == null || dbPharmacologicalGroup.Rows.Count == 0) { return pharmacologicalGroup; }
            pharmacologicalGroup.Code = Convert.ToString(dbPharmacologicalGroup.Rows[0]["Code"]);
            pharmacologicalGroup.Name = Convert.ToString(dbPharmacologicalGroup.Rows[0]["Name"]);
            return pharmacologicalGroup;
        }
    }
}
