using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.iPSService
{
    public class MethodsiPSService
    {
        public BillingConcept GetBillingConcept(int BillingConceptId)
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

        public AssociatedMaterialIPSService GetAssociatedMaterialIPSService(int AssociatedMaterialIPSServiceId)
        {
            AssociatedMaterialIPSService associatedMaterialIPSService = new AssociatedMaterialIPSService();
            ExecuteCommand execute = new ExecuteCommand();
            if (AssociatedMaterialIPSServiceId == 0) { return associatedMaterialIPSService; }
            string query = "SELECT TOP 1 Code,Name FROM Contract.IPSService WHERE Id = @AssociatedMaterialIPSServiceId";
            DataTable dtAssociatedMaterial = execute.GeneralExecuteQuerySqlCommand(query, "AssociatedMaterialIPSServiceId", AssociatedMaterialIPSServiceId);
            if (dtAssociatedMaterial == null || dtAssociatedMaterial.Rows.Count == 0) { return associatedMaterialIPSService; }            
            associatedMaterialIPSService.Code = Convert.ToString(dtAssociatedMaterial.Rows[0]["Code"]);
            associatedMaterialIPSService.Name = Convert.ToString(dtAssociatedMaterial.Rows[0]["Name"]);
            return associatedMaterialIPSService;
        }

        public SurgicalGroup GetSurgicalGroup(int SurgicalGroupId)
        {
            SurgicalGroup surgicalGroup = new SurgicalGroup();
            ExecuteCommand execute = new ExecuteCommand();
            if (SurgicalGroupId == 0) { return surgicalGroup; }
            string query = "SELECT TOP 1 Code,Name FROM Contract.SurgicalGroup WHERE Id = @SurgicalGroupId";
            DataTable dtsurgicalGroup = execute.GeneralExecuteQuerySqlCommand(query, "SurgicalGroupId", SurgicalGroupId);
            if (dtsurgicalGroup == null || dtsurgicalGroup.Rows.Count == 0) { return surgicalGroup; }
            surgicalGroup.Code = Convert.ToString(dtsurgicalGroup.Rows[0]["Code"]);
            surgicalGroup.Name = Convert.ToString(dtsurgicalGroup.Rows[0]["Name"]);
            return surgicalGroup;
        }

        public CupsHomologation[] GetCupsHomologation(Domain.Entities.IPSService iPSServiceEntity)
        {
            int count = 0;
            if (iPSServiceEntity.CupsHomologation.Count > 0) { count = iPSServiceEntity.CupsHomologation.Count; }
            CupsHomologation[] aCupsHomologation = new CupsHomologation[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in iPSServiceEntity.CupsHomologation)
            {
                string query = "SELECT TOP 1 Code,Description FROM Contract.CUPSEntity WHERE Id = @CupsEntityId";
                DataTable dtCupsHomologation = execute.GeneralExecuteQuerySqlCommand(query, "CupsEntityId", item.CupsEntityId);
                if (dtCupsHomologation == null || dtCupsHomologation.Rows.Count == 0) { return aCupsHomologation; }
                CupsHomologation CupsHomologationEntity = new CupsHomologation();
                CupsHomologationEntity.CUPSEntity = new CUPSEntity();
                CupsHomologationEntity.CUPSEntity.Code = Convert.ToString(dtCupsHomologation.Rows[0]["Code"]);
                CupsHomologationEntity.CUPSEntity.Description = Convert.ToString(dtCupsHomologation.Rows[0]["Description"]); ;
                CupsHomologationEntity.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                aCupsHomologation[loop] = CupsHomologationEntity;
                loop++;
            }
            return aCupsHomologation;
        }

        public SurgicalProcedureService[] GetSurgicalProcedureService(Domain.Entities.IPSService iPSServiceEntity)
        {
            int count = 0;
            if (iPSServiceEntity.SurgicalProcedureService.Count > 0) { count = iPSServiceEntity.SurgicalProcedureService.Count; }
            SurgicalProcedureService[] aSurgicalProcedureService = new SurgicalProcedureService[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in iPSServiceEntity.SurgicalProcedureService)
            {
                string query = "SELECT TOP 1 Code,Name FROM Contract.IPSService WHERE Id = @IPSServiceId";
                DataTable dtSurgicalProcedureService = execute.GeneralExecuteQuerySqlCommand(query, "IPSServiceId", item.IPSServiceId);
                if (dtSurgicalProcedureService == null || dtSurgicalProcedureService.Rows.Count == 0) { return aSurgicalProcedureService; }
                SurgicalProcedureService SurgicalProcedureServiceEntity = new SurgicalProcedureService();
                SurgicalProcedureServiceEntity.IPSServiceParent = new IPSService();
                SurgicalProcedureServiceEntity.IPSService = new IPSService();
                SurgicalProcedureServiceEntity.IPSServiceParent.Code = iPSServiceEntity.Code;
                SurgicalProcedureServiceEntity.IPSServiceParent.Name = iPSServiceEntity.Name;
                SurgicalProcedureServiceEntity.IPSService.Code = Convert.ToString(dtSurgicalProcedureService.Rows[0]["Code"]);
                SurgicalProcedureServiceEntity.IPSService.Name = Convert.ToString(dtSurgicalProcedureService.Rows[0]["Name"]);
                SurgicalProcedureServiceEntity.ServiceAmount = item.ServiceAmount;
                SurgicalProcedureServiceEntity.DefaultService = Convert.ToInt16(item.DefaultService);
                SurgicalProcedureServiceEntity.PerformsHealthProfessionalCode = item.PerformsHealthProfessionalCode;
                SurgicalProcedureServiceEntity.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                aSurgicalProcedureService[loop] = SurgicalProcedureServiceEntity;
                loop++;
            }
            return aSurgicalProcedureService;
        }

    }
}
