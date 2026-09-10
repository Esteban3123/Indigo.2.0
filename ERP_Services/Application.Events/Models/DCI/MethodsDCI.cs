using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.DCI
{
    public class MethodsDCI
    {
        public DCIATCEntity[] GetDCIATCEntity(Domain.Entities.DCI dCI)
        {
            int count = 0;
            if (dCI.DCIATCEntity.Count > 0) { count = dCI.DCIATCEntity.Count; }
            DCIATCEntity[] dCIATCEntities = new DCIATCEntity[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in dCI.DCIATCEntity)
            {
                string query = "SELECT TOP 1 Code,Name FROM Inventory.ATCEntity WHERE Id = @dCIId";
                DataTable dtDCIATCEntity = execute.GeneralExecuteQuerySqlCommand(query, "dCIId", item.IdATCEntity);
                if (dtDCIATCEntity == null || dtDCIATCEntity.Rows.Count == 0) { return new DCIATCEntity[0]; }
                DCIATCEntity DCIATCEntity = new DCIATCEntity();
                DCIATCEntity.ATCEntity = new ATCEntity();
                DCIATCEntity.ATCEntity.Code = Convert.ToString(dtDCIATCEntity.Rows[0]["Code"]);
                DCIATCEntity.ATCEntity.Name = Convert.ToString(dtDCIATCEntity.Rows[0]["Name"]);
                DCIATCEntity.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                dCIATCEntities[loop] = DCIATCEntity;
                loop++;
            }
            return dCIATCEntities;
        }

        public DrugInteraction[] GetDrugInteraction(Domain.Entities.DCI dCI)
        {
            int count = 0;
            if (dCI.DrugInteraction.Count > 0) { count = dCI.DrugInteraction.Count; }
            DrugInteraction[] drugInteractionsList = new DrugInteraction[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in dCI.DrugInteraction)
            {
                DrugInteraction drugInteraction = new DrugInteraction();
                drugInteraction.DCI = new DCI();
                string queryDCI = "SELECT TOP 1 Code,Name FROM Inventory.DCI WHERE Id = @dCIId";
                DataTable dtDCI = execute.GeneralExecuteQuerySqlCommand(queryDCI, "dCIId", item.DCIId);
                if (dtDCI is null || dtDCI.Rows.Count == 0) { return new DrugInteraction[0]; }
                drugInteraction.DCI.Code = Convert.ToString(dtDCI.Rows[0]["Code"]);
                drugInteraction.DCI.Name = Convert.ToString(dtDCI.Rows[0]["Name"]);
                drugInteraction.RiskLevel = item.RiskLevel;
                drugInteraction.Description = item.Description;
                drugInteraction.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                drugInteractionsList[loop] = drugInteraction;
                loop++;
            }
            return drugInteractionsList;
        }
    }
}
