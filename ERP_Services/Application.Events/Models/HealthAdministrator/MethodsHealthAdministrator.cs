using Application.Events.Models.Customer;
using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.HealthAdministrator
{
    class MethodsHealthAdministrator
    {
        public ThirdParty getThirdParty(int? thirdPartyId)
        {
            ThirdParty thirdParty = new ThirdParty();
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Nit,Name FROM Common.ThirdParty WHERE Id = @ThirdPartyId";
            DataTable dtThirdParty = execute.GeneralExecuteQuerySqlCommand(query, "ThirdPartyId", thirdPartyId);
            if (dtThirdParty == null && dtThirdParty.Rows.Count == 0) { return thirdParty; }
            thirdParty.Nit = Convert.ToString(dtThirdParty.Rows[0]["Nit"]);
            thirdParty.Name = Convert.ToString(dtThirdParty.Rows[0]["Name"]);
            return thirdParty;
        }

    }
}
