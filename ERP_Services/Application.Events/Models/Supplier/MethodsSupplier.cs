using Application.Events.Models.Thirdparty;
using Application.Events.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Application.Events.Models.Supplier
{
    public class MethodsSupplier
    {

        public MThirdpartySupplier GetThirdParty(Domain.Entities.ThirdParty thirdpartyEntity)
        {
            MThirdpartySupplier thirdparty = new MThirdpartySupplier();
            if (thirdpartyEntity == null) { return thirdparty; }
            methodsThirdparty methodsThirdparty = new methodsThirdparty();
            thirdparty.Nit = thirdpartyEntity.Nit;
            thirdparty.Person = GeneratePerson(thirdpartyEntity);
            return thirdparty;
        }

        public PersonSupplier GeneratePerson(Domain.Entities.ThirdParty thirdpartyEntity)
        {
            PersonSupplier person = new PersonSupplier();
            ExecuteCommand execute = new ExecuteCommand();
            GeneralMethods generalMethods = new GeneralMethods();

            person.IdentificationNumber = thirdpartyEntity.Nit; //Juridico
            if (thirdpartyEntity.PersonType == 1) { person.IdentificationNumber = thirdpartyEntity.Person.IdentificationNumber; }//Natural 

            MAddress[] aAddress = new MAddress[thirdpartyEntity.Person.Address.Count];
            int loop = 0;
            foreach (var item in thirdpartyEntity.Person.Address)
            {
                MAddress address = new MAddress();
                address.Address = item.Addresss;
                address.CityCode = generalMethods.identificacionCityCode(item.CityId ?? 0);
                address.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                aAddress[loop] = address;
                loop++;
            }
            person.Address = aAddress;

            Phones[] phones = new Phones[thirdpartyEntity.Person.Phone.Count];
            int loopTwo = 0;
            foreach (var item in thirdpartyEntity.Person.Phone)
            {
                Phones varphone = new Phones();
                varphone.Phone = item.Phone1;
                varphone.PhoneType = Convert.ToInt16(item.IdPhoneType);
                varphone.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                phones[loopTwo] = varphone;
                loopTwo++;
            }
            person.Phone = phones;

            Emails[] emails = new Emails[thirdpartyEntity.Person.Email.Count];
            int loopThree = 0;
            foreach (var item in thirdpartyEntity.Person.Email)
            {
                Emails email = new Emails();
                email.Email = item.Email1;
                email.Type = item.Type;
                email.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                emails[loopThree] = email;
                loopThree++;
            }
            person.Email = emails;

            return person;
        }

        public IdentificacionCity getCodeCity(int IdentificationCity)
        {
            IdentificacionCity identificacionCity = new IdentificacionCity();
            GeneralMethods generalMethods = new GeneralMethods();
            identificacionCity = generalMethods.identificacionCity(IdentificationCity);
            return identificacionCity;
        }

        public SupplierBankAccount[] getsupplierBankAccounts(Domain.Entities.Supplier supplier)
        {
            int count = 0;
            if (supplier.SupplierBankAccount.Count > 0) { count = supplier.SupplierBankAccount.Count; }

            SupplierBankAccount[] SupplierBankAccount = new SupplierBankAccount[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in supplier.SupplierBankAccount)
            {
                string query = "SELECT TOP 1 Code FROM Payroll.Bank WHERE Id = @IdBank";
                DataTable dtBankAccounts = execute.GeneralExecuteQuerySqlCommand(query, "IdBank", item.BankId);
                SupplierBankAccount SupplierBankAccountEntity = new SupplierBankAccount();
                SupplierBankAccountEntity.CodeBank = Convert.ToString(dtBankAccounts.Rows[0]["Code"]);
                SupplierBankAccountEntity.Type = item.Type;
                SupplierBankAccountEntity.Number = item.Number;
                SupplierBankAccountEntity.PaymentDefault = Convert.ToInt16(item.PaymentDefault);
                SupplierBankAccountEntity.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                SupplierBankAccount[loop] = SupplierBankAccountEntity;
                loop++;
            }
           
            return SupplierBankAccount;
        }

        public SupplierType[] getSupplierType(Domain.Entities.Supplier supplier)
        {
            int count = 0;
            if (supplier.SupplierDetailType.Count > 0) { count = supplier.SupplierDetailType.Count; }
            SupplierType[] SupplierType = new SupplierType[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in supplier.SupplierDetailType)
            {
                string query = "SELECT TOP 1 Code,Name FROM Common.SupplierType WHERE Id = @SupplierType";
                DataTable dtSupplierType = execute.GeneralExecuteQuerySqlCommand(query, "SupplierType", item.SupplierTypeId);
                SupplierType SupplierTypeEntity = new SupplierType();
                SupplierTypeEntity.Code = Convert.ToString(dtSupplierType.Rows[0]["Code"]);
                SupplierTypeEntity.Name = Convert.ToString(dtSupplierType.Rows[0]["Name"]); ;
                SupplierTypeEntity.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                SupplierType[loop] = SupplierTypeEntity;
                loop++;
            }
            return SupplierType;
        }

        public DistributionLines[] getSuppliersDistributionLines(Domain.Entities.Supplier supplier)
        {

            int count = 0;
            if (supplier.SuppliersDistributionLines.Count > 0) { count = supplier.SuppliersDistributionLines.Count; }

            DistributionLines[] DistributionLines = new DistributionLines[count];
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in supplier.SuppliersDistributionLines)
            {
                string query = "SELECT TOP 1 Code,Name,Status FROM Common.DistributionLines WHERE Id = @IdDistributionLine";
                DataTable dtSuppliersDistribution = execute.GeneralExecuteQuerySqlCommand(query, "IdDistributionLine", item.IdDistributionLine);
                DistributionLines DistributionLinesEntity = new DistributionLines();
                DistributionLinesEntity.Code = Convert.ToString(dtSuppliersDistribution.Rows[0]["Code"]);
                DistributionLinesEntity.Name = Convert.ToString(dtSuppliersDistribution.Rows[0]["Name"]);
                DistributionLinesEntity.Position = GetPosition(item.PositionId??0);
                DistributionLinesEntity.Status = Convert.ToInt16(item.Status);
                DistributionLinesEntity.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                DistributionLines[loop] = DistributionLinesEntity;
                loop++;
            }
            return DistributionLines;
        }

        public Position GetPosition(int IdPosititon)
        {
            Position position = new Position();
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Code,Name FROM Payroll.Position WHERE Id = @IdPosition";
            DataTable dtPosition = execute.GeneralExecuteQuerySqlCommand(query, "IdPosition", IdPosititon);            
            if (dtPosition == null || dtPosition.Rows.Count == 0) { return position; }
            position.Code = Convert.ToString(dtPosition.Rows[0]["Code"]);
            position.Name = Convert.ToString(dtPosition.Rows[0]["Name"]);
            return position;                
        }

    }
}
