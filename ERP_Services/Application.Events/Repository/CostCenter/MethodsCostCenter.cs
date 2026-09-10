using Application.Events.Models.CostCenter;
using System.Data;
using System;

namespace Application.Events.Repository.CostCenter
{
    public class MethodsCostCenter
    {
        public BranchOffice GetBranchOffice(int? BranchOfficeId)
        {
            BranchOffice branchOffice = new BranchOffice();
            if (BranchOfficeId == null || BranchOfficeId == 0) { return branchOffice; }
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Code,Name FROM Payroll.BranchOffice WHERE Id = @BranchOfficeId";
            DataTable dtBranchOffice = execute.GeneralExecuteQuerySqlCommand(query, "BranchOfficeId", BranchOfficeId);
            if (dtBranchOffice == null || dtBranchOffice.Rows.Count == 0) { return branchOffice; }
            branchOffice.Code = Convert.ToString(dtBranchOffice.Rows[0]["Code"]);
            branchOffice.Name = Convert.ToString(dtBranchOffice.Rows[0]["Name"]);
            return branchOffice;
        }

    }
}
