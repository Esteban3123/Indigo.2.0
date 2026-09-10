using Application.Events.Models.Customer;
using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models.MainAccounts
{
    class MethodsMainAccounts
    {

        public LegalBook GetLegalBook(int? legalBookId)
        {
            LegalBook legalBook = new LegalBook();
            ExecuteCommand execute = new ExecuteCommand();

            if (legalBookId ==null || legalBookId == 0) { return legalBook; }
            string query = "SELECT TOP 1 Code,Name FROM GeneralLedger.LegalBook WHERE Id =  @LegalBookId";
            DataTable dtLegalBook = execute.GeneralExecuteQuerySqlCommand(query, "LegalBookId", legalBookId);
            if (dtLegalBook == null || dtLegalBook.Rows.Count == 0) { return legalBook; }
            legalBook.Code = Convert.ToString(dtLegalBook.Rows[0]["Code"]);
            legalBook.Name = Convert.ToString(dtLegalBook.Rows[0]["Name"]);
            return legalBook;
        }

        public AccountLevel GetAccountLevel(int IdAccountLevel)
        {
            AccountLevel accountLevel = new AccountLevel();
            ExecuteCommand execute = new ExecuteCommand();

            if (IdAccountLevel == 0) { return accountLevel; }
            string query = "SELECT TOP 1 Level,Length,digits FROM GeneralLedger.MainAccountLevels WHERE Id = @IdAccountLevel";
            DataTable dtaccountLevel = execute.GeneralExecuteQuerySqlCommand(query, "IdAccountLevel", IdAccountLevel);
            if (dtaccountLevel == null || dtaccountLevel.Rows.Count == 0) { return accountLevel; }
            accountLevel.Level = Convert.ToInt16(dtaccountLevel.Rows[0]["Level"]);
            accountLevel.Length = Convert.ToInt16(dtaccountLevel.Rows[0]["Length"]);
            accountLevel.digits = Convert.ToInt16(dtaccountLevel.Rows[0]["digits"]);
            return accountLevel;
        }

        public AccountClass GetAccountClass(int IdAccountClass)
        {
            AccountClass accountClass = new AccountClass();
            ExecuteCommand execute = new ExecuteCommand();

            if (IdAccountClass == 0) { return accountClass; }
            string query = "SELECT TOP 1 Code,Name FROM GeneralLedger.MainAccountClasses WHERE Id = @IdAccountClass";
            DataTable dtAccountClass = execute.GeneralExecuteQuerySqlCommand(query, "IdAccountClass", IdAccountClass);
            if (dtAccountClass == null || dtAccountClass.Rows.Count == 0) { return accountClass; }
            accountClass.Code= Convert.ToString(dtAccountClass.Rows[0]["Code"]);
            accountClass.Name = Convert.ToString(dtAccountClass.Rows[0]["Name"]);
            return accountClass;
        }

        public Parent GetParent(int IdParent)
        {
            Parent parent = new Parent();
            ExecuteCommand execute = new ExecuteCommand();
            parent.LegalBook = new LegalBook();
            if (IdParent == 0) { return parent; }
            string query = "SELECT TOP 1 Number,Name,LegalBookId FROM GeneralLedger.MainAccounts WHERE Id = @IdParent";
            DataTable dtParent = execute.GeneralExecuteQuerySqlCommand(query, "IdParent", IdParent);
            if (dtParent == null || dtParent.Rows.Count == 0) { return parent; }
            parent.LegalBook = GetLegalBook(Convert.ToInt16(dtParent.Rows[0]["LegalBookId"]));
            parent.Number = Convert.ToString(dtParent.Rows[0]["Number"]);
            parent.Name = Convert.ToString(dtParent.Rows[0]["Name"]);
            return parent;
        }

        public ThirdParty GetThirdParty(int IdThirdParty)
        {
            ThirdParty thirdParty = new ThirdParty();
            ExecuteCommand execute = new ExecuteCommand();
            if (IdThirdParty == 0) { return thirdParty; }
            string query = "SELECT TOP 1 Nit,Name FROM Common.ThirdParty WHERE Id = @IdThirdParty";
            DataTable dtThirdParty = execute.GeneralExecuteQuerySqlCommand(query, "IdThirdParty", IdThirdParty);
            if (dtThirdParty == null || dtThirdParty.Rows.Count == 0) { return thirdParty; }
            thirdParty.Nit = Convert.ToString(dtThirdParty.Rows[0]["Nit"]);
            thirdParty.Name = Convert.ToString(dtThirdParty.Rows[0]["Name"]);
            return thirdParty;
        }
    }
}
