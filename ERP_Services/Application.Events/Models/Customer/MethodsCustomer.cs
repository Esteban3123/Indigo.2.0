using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.Customer
{
    public class MethodsCustomer
    {
        public ThirdParty getThirdParty(Domain.Entities.Customer customerThirdParty)
        {
            ThirdParty thirdParty = new ThirdParty();
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Nit,Name FROM Common.ThirdParty WHERE Id = @ThirdPartyId";
            DataTable dtThirdParty = execute.GeneralExecuteQuerySqlCommand(query, "ThirdPartyId", customerThirdParty.ThirdPartyId);
            if (dtThirdParty == null && dtThirdParty.Rows.Count == 0) { return thirdParty; }
            thirdParty.Nit = Convert.ToString(dtThirdParty.Rows[0]["Nit"]);
            thirdParty.Name = Convert.ToString(dtThirdParty.Rows[0]["Name"]);
            return thirdParty;
        }

        public MainAccountReceivable getMainAccountReceivable(Domain.Entities.Customer customerMainAccounts)
        {
            MainAccountReceivable mainAccount = new MainAccountReceivable();
            ExecuteCommand execute = new ExecuteCommand();

            if (customerMainAccounts == null) { return mainAccount; }
            string query = "SELECT TOP 1 ma.Name As NameAccount,ma.Number, L.Code,L.Name FROM GeneralLedger.MainAccounts ma JOIN GeneralLedger.LegalBook L ON ma.LegalBookId = L.Id WHERE ma.Id =  @MainAccountReceivableId";
            DataTable dtLegalBook = execute.GeneralExecuteQuerySqlCommand(query, "MainAccountReceivableId", customerMainAccounts.MainAccountReceivableId);                     
            if (dtLegalBook != null && dtLegalBook.Rows.Count > 0)
            {
                mainAccount.LegalBook = new LegalBook();
                mainAccount.LegalBook.Code = Convert.ToString(dtLegalBook.Rows[0]["Code"]);
                mainAccount.LegalBook.Name = Convert.ToString(dtLegalBook.Rows[0]["Name"]);
                mainAccount.Number = Convert.ToString(dtLegalBook.Rows[0]["Number"]);
                mainAccount.Name = Convert.ToString(dtLegalBook.Rows[0]["NameAccount"]);
            }
            return mainAccount;
        }

        public Retentions[] getRetentions(Domain.Entities.Customer customerRetentions)
        {
            int count = 0;
            if (customerRetentions.CustomerRetention.Count > 0) { count = customerRetentions.CustomerRetention.Count; }

            Retentions[] retentions = new Retentions[count];
            
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;  string query = "";
            foreach (var item in customerRetentions.CustomerRetention)
            {
                Retentions objRetentios = new Retentions();
                query = "SELECT TOP 1 Code,Name FROM Portfolio.PortfolioNoteConcept WHERE Id = @PortfolioNoteConceptId ";
                DataTable dtPortfolioNoteConcept = execute.GeneralExecuteQuerySqlCommand(query, "PortfolioNoteConceptId", item.PortfolioNoteConceptId);
                objRetentios.PortfolioNoteConcept = new PortfolioNoteConcept();
                if (dtPortfolioNoteConcept != null && dtPortfolioNoteConcept.Rows.Count > 0)
                {                    
                    objRetentios.PortfolioNoteConcept.Code = Convert.ToString(dtPortfolioNoteConcept.Rows[0]["Code"]);
                    objRetentios.PortfolioNoteConcept.Name = Convert.ToString(dtPortfolioNoteConcept.Rows[0]["Name"]);
                }

                query = "SELECT TOP 1 Code,Name FROM GeneralLedger.RetentionConcepts WHERE Id = @RetentionConcepts ";
                DataTable dtRetentionConcept = execute.GeneralExecuteQuerySqlCommand(query, "RetentionConcepts", item.RetentionConceptId);
                objRetentios.RetentionConcept = new RetentionConcept();
                if (dtRetentionConcept != null && dtRetentionConcept.Rows.Count > 0)
                {                 
                    objRetentios.RetentionConcept.Code = Convert.ToString(dtRetentionConcept.Rows[0]["Code"]);
                    objRetentios.RetentionConcept.Name = Convert.ToString(dtRetentionConcept.Rows[0]["Name"]);
                }
                objRetentios.Status = Convert.ToInt16(item.Status);
                retentions[loop] = objRetentios;
                loop++;
            }
            return retentions;        
        }
    }
}
