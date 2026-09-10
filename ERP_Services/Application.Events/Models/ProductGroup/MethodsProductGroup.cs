using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.ProductGroup
{
    public class MethodsProductGroup
    {

        public GeneralAccount GetGeneralAccount(int idGeneralAccount)
        {
            GeneralAccount generalAccount = new GeneralAccount();
            ExecuteCommand execute = new ExecuteCommand();
            if (idGeneralAccount == 0) { return generalAccount; }
            string query = "SELECT TOP 1 LegalBookId,Number,Name FROM GeneralLedger.MainAccounts WHERE Id = @idGeneralAccount";
            DataTable dtGeneralAccount = execute.GeneralExecuteQuerySqlCommand(query, "idGeneralAccount", idGeneralAccount);
            if (dtGeneralAccount == null || dtGeneralAccount.Rows.Count == 0) { return generalAccount; }
            int legaBookId = Convert.ToInt16(dtGeneralAccount.Rows[0]["LegalBookId"]);
            generalAccount.Number = Convert.ToString(dtGeneralAccount.Rows[0]["Number"]);
            generalAccount.Name = Convert.ToString(dtGeneralAccount.Rows[0]["Name"]);            
            generalAccount.LegalBook = new LegalBook();
            string queryTwo = "SELECT TOP 1 Code,Name FROM GeneralLedger.LegalBook WHERE Id = @idLegalbook";
            DataTable dtLegalBook = execute.GeneralExecuteQuerySqlCommand(queryTwo, "idLegalbook", legaBookId);
            generalAccount.LegalBook.Code = Convert.ToString(dtLegalBook.Rows[0]["Code"]);
            generalAccount.LegalBook.Name = Convert.ToString(dtLegalBook.Rows[0]["Name"]);
            return generalAccount;
        }
        public InventoryAccountPayableConcept GetInventoryAccountPayableConcept(int idInventoryAccountPayableConcept)
        {
            InventoryAccountPayableConcept inventoryAccountPayableConcept = new InventoryAccountPayableConcept();
            ExecuteCommand execute = new ExecuteCommand();
            if (idInventoryAccountPayableConcept == 0) { return inventoryAccountPayableConcept; }
            string query = "SELECT TOP 1 Code,Name FROM Payments.AccountPayableConcepts WHERE Id = @idInventoryAccountPayableConcept";
            DataTable dtAccountPayableConcept = execute.GeneralExecuteQuerySqlCommand(query, "idInventoryAccountPayableConcept", idInventoryAccountPayableConcept);
            if (dtAccountPayableConcept == null || dtAccountPayableConcept.Rows.Count == 0) { return inventoryAccountPayableConcept; }
            inventoryAccountPayableConcept.Code = Convert.ToString(dtAccountPayableConcept.Rows[0]["Code"]);
            inventoryAccountPayableConcept.Name = Convert.ToString(dtAccountPayableConcept.Rows[0]["Name"]);
            return inventoryAccountPayableConcept;
        }
        public NotDeclarantRetentionAccountPayableConcept GetNotDeclarantRetentionAccountPayableConcept(int idNotDeclarantRetentionAccountPayableConcept)
        {
            NotDeclarantRetentionAccountPayableConcept notDeclarantRetentionAccountPayableConcept = new NotDeclarantRetentionAccountPayableConcept();
            ExecuteCommand execute = new ExecuteCommand();
            if (idNotDeclarantRetentionAccountPayableConcept == 0) { return notDeclarantRetentionAccountPayableConcept; }
            string query = "SELECT TOP 1 Code,Name FROM Payments.AccountPayableConcepts WHERE Id = @idNotDeclarantRetentionAccountPayableConcept";
            DataTable dtNotDeclarantRetentionAccountPayableConcept = execute.GeneralExecuteQuerySqlCommand(query, "idNotDeclarantRetentionAccountPayableConcept", idNotDeclarantRetentionAccountPayableConcept);
            if (dtNotDeclarantRetentionAccountPayableConcept == null || dtNotDeclarantRetentionAccountPayableConcept.Rows.Count == 0) { return notDeclarantRetentionAccountPayableConcept; }
            notDeclarantRetentionAccountPayableConcept.Code = Convert.ToString(dtNotDeclarantRetentionAccountPayableConcept.Rows[0]["Code"]);
            notDeclarantRetentionAccountPayableConcept.Name = Convert.ToString(dtNotDeclarantRetentionAccountPayableConcept.Rows[0]["Name"]);
            return notDeclarantRetentionAccountPayableConcept;
        }

        public CostCenter GetCostCenter(int idCostCenter)
        {
            CostCenter CostCenter = new CostCenter();
            ExecuteCommand execute = new ExecuteCommand();
            if (idCostCenter == 0) { return CostCenter; }
            string query = "SELECT TOP 1 Code,Name FROM Payroll.CostCenter WHERE Id = @idCostCenter";
            DataTable dtCostCenter = execute.GeneralExecuteQuerySqlCommand(query, "idCostCenter", idCostCenter);
            if (dtCostCenter == null || dtCostCenter.Rows.Count == 0) { return CostCenter; }
            CostCenter.Code = Convert.ToString(dtCostCenter.Rows[0]["Code"]);
            CostCenter.Name = Convert.ToString(dtCostCenter.Rows[0]["Name"]);
            return CostCenter;
        }

        public ProductGroupFunctionalUnit[] GetProductGroupFunctionalUnit(Domain.Entities.ProductGroup productGroup)
        {
            int count = 0;
            if (productGroup.ProductGroupFunctionalUnit.Count > 0) { count = productGroup.ProductGroupFunctionalUnit.Count; }
            ProductGroupFunctionalUnit[] ProductGroupFunctionalUnitList = new ProductGroupFunctionalUnit[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in productGroup.ProductGroupFunctionalUnit)
            {
                ProductGroupFunctionalUnit productGroupFunctionalUnit = new ProductGroupFunctionalUnit();
                productGroupFunctionalUnit.FunctionalUnit = new FunctionalUnit();

                string queryDCI = "SELECT TOP 1 Code,Name FROM Payroll.FunctionalUnit WHERE Id = @FunctionalUnitId";
                DataTable dtDCI = execute.GeneralExecuteQuerySqlCommand(queryDCI, "FunctionalUnitId", item.FunctionalUnitId);
                productGroupFunctionalUnit.FunctionalUnit.Code = Convert.ToString(dtDCI.Rows[0]["Code"]);
                productGroupFunctionalUnit.FunctionalUnit.Name = Convert.ToString(dtDCI.Rows[0]["Name"]);
                productGroupFunctionalUnit.CostAccount = new GeneralAccount();
                productGroupFunctionalUnit.CostAccount = GetGeneralAccount(item.CostAccountId);
                productGroupFunctionalUnit.SalesAccount = new GeneralAccount();
                productGroupFunctionalUnit.SalesAccount = GetGeneralAccount(item.SalesAccountId);
                productGroupFunctionalUnit.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                ProductGroupFunctionalUnitList[loop] = productGroupFunctionalUnit;
                loop++;
            }
            return ProductGroupFunctionalUnitList;
        }
        
        public ReteFuenteConcept GetReteFuenteConcept(int idReteFuenteConcept)
        {
            ReteFuenteConcept reteFuenteConcept = new ReteFuenteConcept();
            ExecuteCommand execute = new ExecuteCommand();
            if (idReteFuenteConcept == 0) { return reteFuenteConcept; }
            string query = "SELECT TOP 1 Code,Name FROM GeneralLedger.RetentionConcepts WHERE Id = @idReteFuenteConcept";
            DataTable dtreteFuenteConcept = execute.GeneralExecuteQuerySqlCommand(query, "idReteFuenteConcept", idReteFuenteConcept);
            if (dtreteFuenteConcept == null || dtreteFuenteConcept.Rows.Count == 0) { return reteFuenteConcept; }
            reteFuenteConcept.Code = Convert.ToString(dtreteFuenteConcept.Rows[0]["Code"]);
            reteFuenteConcept.Name = Convert.ToString(dtreteFuenteConcept.Rows[0]["Name"]);
            return reteFuenteConcept;
        }




    }
}
