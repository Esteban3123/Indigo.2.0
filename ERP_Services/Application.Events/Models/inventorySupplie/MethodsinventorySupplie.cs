using Application.Events.Models.inventoryRiskLevel;
using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.inventorySupplie
{
    public class MethodsinventorySupplie

    {         
        public inventoryRiskLevelSupplie GetMinventoryRiskLevel(int RiskLevelId)
        {
            inventoryRiskLevelSupplie minventoryRiskLevel = new inventoryRiskLevelSupplie();
            if ( RiskLevelId == 0) { return minventoryRiskLevel; };
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Code,Name FROM Inventory.InventoryRiskLevel WHERE Id = @RiskLevelId";
            DataTable dbRiskLevel = execute.GeneralExecuteQuerySqlCommand(query, "RiskLevelId", RiskLevelId);
            if (dbRiskLevel == null || dbRiskLevel.Rows.Count == 0) { return minventoryRiskLevel; }
            minventoryRiskLevel.Code = Convert.ToString(dbRiskLevel.Rows[0]["Code"]);
            minventoryRiskLevel.Name = Convert.ToString(dbRiskLevel.Rows[0]["Name"]);
            return minventoryRiskLevel;
        }
    }
}
