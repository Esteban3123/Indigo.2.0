using Application.Events.Models;
using Application.Events.Repository;
using System;
using System.Data;
using Domain.Entities;
using Newtonsoft.Json;

namespace Application.Events.Models.Thirdparty
{
    public class methodsThirdparty
    {

        public IdentificacionCity identificacionCity(int? IdentificationCity)
        {
            IdentificacionCity identificacionCity = new IdentificacionCity();
            GeneralMethods generalMethods = new GeneralMethods();
            if (IdentificationCity == null || IdentificationCity == 0) { return identificacionCity; }
            identificacionCity = generalMethods.identificacionCity(IdentificationCity);
            return identificacionCity;
        }

        public string identificacionCityCode(int IdentificationCity)
        {
            IdentificacionCity identificacionCity = new IdentificacionCity();
            ExecuteCommand execute = new ExecuteCommand();
            String query = "SELECT TOP 1 Code,Name FROM Common.City WHERE Id = @IdentificationCity";
            DataTable dtidentificacionCity = execute.GeneralExecuteQuerySqlCommand(query, "IdentificationCity", IdentificationCity);
            if (dtidentificacionCity == null || dtidentificacionCity.Rows.Count == 0) { return ""; }
            identificacionCity.Code = Convert.ToString(dtidentificacionCity.Rows[0]["Code"]);
            return identificacionCity.Code;
        }

        public Person GeneratePerson(Domain.Entities.ThirdParty thirdpartyEntity)
        {
            Person person = new Person();
            ExecuteCommand execute = new ExecuteCommand();

            if (thirdpartyEntity.PersonType == 1) //Natural
            {
                person.IdentificationNumber = thirdpartyEntity.Person.IdentificationNumber;
                person.IdentificationType = thirdpartyEntity.Person.IdentificationType;
                person.FirstName = thirdpartyEntity.Person.FirstName;
                person.MiddleName = thirdpartyEntity.Person.SecondName;
                person.Surname = thirdpartyEntity.Person.FirstLastName;
                person.SecondSurname = thirdpartyEntity.Person.SecondLastName;
                person.IdentificationCity = identificacionCity(thirdpartyEntity.Person.IdentificacionCityId ?? 0);
            }
            else //Juridico
            {
                person.IdentificationNumber = thirdpartyEntity.Nit;
                person.IdentificationType = 7;
                person.IdentificationCity = identificacionCity(thirdpartyEntity.Person.IdentificacionCityId ?? 0);
            }
            person.State = Convert.ToInt16(thirdpartyEntity.Person.State);
            MAddress[] aAddress = new MAddress[thirdpartyEntity.Person.Address.Count];
            int loop = 0;
            foreach (var item in thirdpartyEntity.Person.Address)
            {
                MAddress address = new MAddress();
                address.Address = item.Addresss;
                address.CityCode = identificacionCityCode(item.CityId ?? 0);
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

        /// <summary>
        /// Concepto de cuentas por pagar para la Retencion al IVA, Este concepto de pagos debe de ser de tipo de retencion y debe ser de tipo Especifico, Este campo solo se llena si el Tipo de Contribuyente es Comun(1).
        /// </summary>
        /// <param name="IVARetentionAccountPayableConceptId"></param>
        /// <returns></returns>
        public IVARetentionAccountPayableConcept GetIVAccountPayableConcept(int? IVARetentionAccountPayableConceptId)
        {
            IVARetentionAccountPayableConcept AccountPayableConcept = new IVARetentionAccountPayableConcept();
            ExecuteCommand execute = new ExecuteCommand();
            if (IVARetentionAccountPayableConceptId == null) { IVARetentionAccountPayableConceptId = 0; }
            string query = "SELECT TOP 1 Code,Name FROM  Payments.AccountPayableConcepts WHERE Id = @IVARetentionAccountPayableConceptId ";
            DataTable dtAccountPayableConcept = execute.GeneralExecuteQuerySqlCommand(query, "IVARetentionAccountPayableConceptId", IVARetentionAccountPayableConceptId);
            if (dtAccountPayableConcept == null || dtAccountPayableConcept.Rows.Count == 0) { return AccountPayableConcept; }
            AccountPayableConcept.Code = Convert.ToString(dtAccountPayableConcept.Rows[0]["Code"]);
            AccountPayableConcept.Name = Convert.ToString(dtAccountPayableConcept.Rows[0]["Name"]);
            return AccountPayableConcept;
        }

        public EconomicActivity GetEconomicActivity(int EconomicActivityId)
        {
            EconomicActivity economicActivity = new EconomicActivity();
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Code,Name FROM Common.EconomicActivity WHERE Id = @EconomicActivityId";
            DataTable dtEconomicActivity = execute.GeneralExecuteQuerySqlCommand(query, "EconomicActivityId", EconomicActivityId);
            if (dtEconomicActivity == null || dtEconomicActivity.Rows.Count == 0) { return economicActivity; }
            economicActivity.Code = Convert.ToString(dtEconomicActivity.Rows[0]["Code"]);
            economicActivity.Name = Convert.ToString(dtEconomicActivity.Rows[0]["Name"]);
            return economicActivity;
        }

        public IVARetentionConcept GetIVARetentionConcept(int IVARetentionConcept)
        {
            IVARetentionConcept VarIVARetentionConcep = new IVARetentionConcept();
            ExecuteCommand execute = new ExecuteCommand();
            string query = "SELECT TOP 1 Code,Name FROM  GeneralLedger.RetentionConcepts WHERE Id = @IVARetentionConcep";
            DataTable dtIVARetentionConcep = execute.GeneralExecuteQuerySqlCommand(query, "IVARetentionConcep", IVARetentionConcept);
            if (dtIVARetentionConcep == null || dtIVARetentionConcep.Rows.Count == 0) { return VarIVARetentionConcep; }
            VarIVARetentionConcep.Code = Convert.ToString(dtIVARetentionConcep.Rows[0]["Code"]);
            VarIVARetentionConcep.Name = Convert.ToString(dtIVARetentionConcep.Rows[0]["Name"]);
            return VarIVARetentionConcep;
        }


        public BranchOffice[] GetBranchOffice(Domain.Entities.ThirdParty thirdpartyEntity)
        {
            BranchOffice[] branchOffices = new BranchOffice[thirdpartyEntity.ThirdPartyBranchOffice.Count];
            if (thirdpartyEntity == null) { return branchOffices; }
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in thirdpartyEntity.ThirdPartyBranchOffice)
            {
                string query = $"SELECT TOP 1 b.Code,b.Name,c.nit As CompanyNit FROM Payroll.BranchOffice b LEFT JOIN Payroll.Company c ON B.CompanyId = c.Id WHERE b.Id = @BranchOfficeId";
                DataTable dtbranchOffices = execute.GeneralExecuteQuerySqlCommand(query, "BranchOfficeId", item.BranchOfficeId);

                if (dtbranchOffices != null && dtbranchOffices.Rows.Count > 0)
                {
                    BranchOffice BranchOffice = new BranchOffice();
                    BranchOffice.Code = Convert.ToString(dtbranchOffices.Rows[0]["Code"]);
                    BranchOffice.Name = Convert.ToString(dtbranchOffices.Rows[0]["Name"]);
                    BranchOffice.CompanyNit = Convert.ToString(dtbranchOffices.Rows[0]["CompanyNit"]);
                    BranchOffice.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                    branchOffices[loop] = BranchOffice;
                }

                loop++;
            }
            return branchOffices;
        }


        public FiscalResponsibility[] GetFiscalResponsibility(Domain.Entities.ThirdParty thirdpartyEntity)
        {
            FiscalResponsibility[] FiscalResponsibilitys = new FiscalResponsibility[thirdpartyEntity.ThirdPartyFiscalResponsibility.Count];
            if (thirdpartyEntity == null) { return FiscalResponsibilitys; }
            ExecuteCommand execute = new ExecuteCommand();
            int loop = 0;
            foreach (var item in thirdpartyEntity.ThirdPartyFiscalResponsibility)
            {
                string query = "SELECT TOP 1 Code,Name FROM Common.FiscalResponsibility WHERE Id = @FiscalResponsibilityId";
                DataTable dtFiscalResponsibilitys = execute.GeneralExecuteQuerySqlCommand(query, "FiscalResponsibilityId", item.FiscalResponsibilityId);
                FiscalResponsibility FiscalResponsibility = new FiscalResponsibility();
                FiscalResponsibility.Code = Convert.ToString(dtFiscalResponsibilitys.Rows[0]["Code"]);
                FiscalResponsibility.Name = Convert.ToString(dtFiscalResponsibilitys.Rows[0]["Name"]);
                FiscalResponsibility.IsDelete = (item.ChangeTracker.State == Domain.Base.Entities.ObjectState.Deleted) ? 1 : 0;
                FiscalResponsibilitys[loop] = FiscalResponsibility;
                loop++;
            }
            return FiscalResponsibilitys;
        }
    }
}
