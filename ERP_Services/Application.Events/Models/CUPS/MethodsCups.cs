using Application.Events.Models.iPSService;
using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.CUPS
{
    public class MethodsCups
    {
        public CUPSSubGroup GenerateCUPSSubGroup(int CUPSSubGroupId )
        {
            CUPSSubGroup cUPSSubGroup = new CUPSSubGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (CUPSSubGroupId == 0) { return cUPSSubGroup; }
            string query = "SELECT TOP 1 Code,Name FROM Contract.CupsSubgroup WHERE Id = @CUPSSubGroupId";
            DataTable dtcUPSSubGroupt = execute.GeneralExecuteQuerySqlCommand(query, "CUPSSubGroupId", CUPSSubGroupId);
            if (dtcUPSSubGroupt == null || dtcUPSSubGroupt.Rows.Count == 0) { return cUPSSubGroup; }            
            cUPSSubGroup.Code = Convert.ToString(dtcUPSSubGroupt.Rows[0]["Code"]);
            cUPSSubGroup.Name = Convert.ToString(dtcUPSSubGroupt.Rows[0]["Name"]);
            return cUPSSubGroup;            
        }

        public BillingConcept GenerateBillingConcept(int BillingConceptId)
        {
            BillingConcept billingConcept = new BillingConcept();
            ExecuteCommand execute = new ExecuteCommand();
            if (BillingConceptId == 0) { return billingConcept; }
            string query = "SELECT TOP 1 Code,Name FROM Billing.BillingConcept WHERE Id = @BillingConceptId";
            DataTable dtbillingConcept = execute.GeneralExecuteQuerySqlCommand(query, "BillingConceptId", BillingConceptId);
            if (dtbillingConcept == null || dtbillingConcept.Rows.Count == 0) { return billingConcept; }
            billingConcept.Code = Convert.ToString(dtbillingConcept.Rows[0]["Code"]);
            billingConcept.Name = Convert.ToString(dtbillingConcept.Rows[0]["Name"]);
            return billingConcept;
        }

       public BillingGroup GenerateBillingGroup(int BillingGroupId)
        {
            BillingGroup billingGroup = new BillingGroup();           
            ExecuteCommand execute = new ExecuteCommand();
            if (BillingGroupId == 0) { return billingGroup; }
            string query = "SELECT TOP 1 Code,Name FROM Billing.BillingGroup WHERE Id = @BillingGroupId";
            DataTable dtbillingGroup = execute.GeneralExecuteQuerySqlCommand(query, "BillingGroupId", BillingGroupId);
            if (dtbillingGroup == null || dtbillingGroup.Rows.Count == 0) { return billingGroup; }
            billingGroup.Code = Convert.ToString(dtbillingGroup.Rows[0]["Code"]);
            billingGroup.Name = Convert.ToString(dtbillingGroup.Rows[0]["Name"]);
            return billingGroup;
        }

        public RIASBillingConcept GenerateRIASBillingConcept(int RIASBillingConceptId)
        {
            RIASBillingConcept rIASBillingConcept = new RIASBillingConcept();
            ExecuteCommand execute = new ExecuteCommand();
            if (RIASBillingConceptId == 0) { return rIASBillingConcept; }
            string query = "SELECT TOP 1 Code,Name FROM Billing.BillingConcept WHERE Id = @RIASBillingConceptId";
            DataTable dtrIASBillingConcept = execute.GeneralExecuteQuerySqlCommand(query, "RIASBillingConceptId", RIASBillingConceptId);
            if (dtrIASBillingConcept == null || dtrIASBillingConcept.Rows.Count == 0) { return rIASBillingConcept; }
            rIASBillingConcept.Code = Convert.ToString(dtrIASBillingConcept.Rows[0]["Code"]);
            rIASBillingConcept.Name = Convert.ToString(dtrIASBillingConcept.Rows[0]["Name"]);
            return rIASBillingConcept;
        }

        public RIASBillingGroup GenerateRIASBillingGroup(int RIASBillingGroupId)
        {
            RIASBillingGroup rIASBillingGroup = new RIASBillingGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (RIASBillingGroupId == 0) { return rIASBillingGroup; }
            string query = "SELECT TOP 1 Code,Name FROM Billing.BillingGroup WHERE Id = @RIASBillingGroupId";
            DataTable dtrIASBillingGroup = execute.GeneralExecuteQuerySqlCommand(query, "RIASBillingGroupId", RIASBillingGroupId);
            if (dtrIASBillingGroup == null || dtrIASBillingGroup.Rows.Count == 0) { return rIASBillingGroup; }
            rIASBillingGroup.Code = Convert.ToString(dtrIASBillingGroup.Rows[0]["Code"]);
            rIASBillingGroup.Name = Convert.ToString(dtrIASBillingGroup.Rows[0]["Name"]);
            return rIASBillingGroup;
        }

        public CUPSEntityContractDescriptions[] GenerateCUPSEntityContractDescriptions(Domain.Entities.CUPSEntity cUPSEntity)
        {
            int count = 0;
            if (cUPSEntity.CUPSEntityContractDescriptions.Count > 0) { count = cUPSEntity.CUPSEntityContractDescriptions.Count;  }
            CUPSEntityContractDescriptions[] aCUPSEntityContractDescriptions = new CUPSEntityContractDescriptions[count];
            ExecuteCommand execute = new ExecuteCommand();

            int loop = 0; string query = "";
            foreach (var item in cUPSEntity.CUPSEntityContractDescriptions)
            {
                CUPSEntityContractDescriptions cUPSEntityContractDescriptions = new CUPSEntityContractDescriptions();
                query = "SELECT TOP 1 Code,Name FROM Contract.ContractDescriptions WHERE Id = @ContractDescriptionId";
                cUPSEntityContractDescriptions.ContractDescriptions = new ContractDescriptions();
                DataTable dtContractDescriptions = execute.GeneralExecuteQuerySqlCommand(query, "ContractDescriptionId", item.ContractDescriptionId);
                if (dtContractDescriptions != null || dtContractDescriptions.Rows.Count > 0)
                {
                    cUPSEntityContractDescriptions.ContractDescriptions.Code = Convert.ToString(dtContractDescriptions.Rows[0]["Code"]);
                    cUPSEntityContractDescriptions.ContractDescriptions.Name = Convert.ToString(dtContractDescriptions.Rows[0]["Name"]);
                }

                query = "SELECT TOP 1 Code,Name FROM Contract.CupsSubgroup WHERE Id = @CupsSubgroupId";
                cUPSEntityContractDescriptions.CUPSSubgroup = new CUPSSubGroup();
                DataTable dtCupsSubgroup = execute.GeneralExecuteQuerySqlCommand(query, "CupsSubgroupId", item.CupsSubgroupId);
                if (dtCupsSubgroup != null || dtCupsSubgroup.Rows.Count > 0)
                {
                    cUPSEntityContractDescriptions.CUPSSubgroup.Code = Convert.ToString(dtCupsSubgroup.Rows[0]["Code"]);
                    cUPSEntityContractDescriptions.CUPSSubgroup.Name = Convert.ToString(dtCupsSubgroup.Rows[0]["Name"]);
                }

                cUPSEntityContractDescriptions.BillingGroup = new BillingGroup();
                cUPSEntityContractDescriptions.BillingGroup = GenerateBillingGroup(item.BillingGroupId);

                cUPSEntityContractDescriptions.BillingConcept = new BillingConcept();
                cUPSEntityContractDescriptions.BillingConcept = GenerateBillingConcept(item.BillingConceptId);
                
                cUPSEntityContractDescriptions.IsDelete = Convert.ToInt16(item.IsDelete);
                aCUPSEntityContractDescriptions[loop] = cUPSEntityContractDescriptions;
                loop++;
            }
            return aCUPSEntityContractDescriptions;
        }
    }
}
