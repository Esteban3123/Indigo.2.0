using Application.Events.Models.Thirdparty;
using Application.Events.Repository;
using System;
using System.Data;

namespace Application.Events.Models
{
    public class GeneralMethods
    {
        public IdentificacionCity identificacionCity(int? IdentificationCity)
        {
            IdentificacionCity identificacionCity = new IdentificacionCity();
            ExecuteCommand execute = new ExecuteCommand();
            String query = "SELECT TOP 1 Code,Name FROM Common.City WHERE Id = @IdentificationCity";
            DataTable dtidentificacionCity = execute.GeneralExecuteQuerySqlCommand(query, "IdentificationCity", IdentificationCity);
            if (dtidentificacionCity == null || dtidentificacionCity.Rows.Count == 0) { return identificacionCity; }
            identificacionCity.Code = Convert.ToString(dtidentificacionCity.Rows[0]["Code"]);
            identificacionCity.Name = Convert.ToString(dtidentificacionCity.Rows[0]["Name"]);
            return identificacionCity;
        }
        public string identificacionCityCode(int IdentificationCity)
        {
            IdentificacionCity identificacionCity = new IdentificacionCity();
            ExecuteCommand execute = new ExecuteCommand();
            String query = "SELECT TOP 1 Code FROM Common.City WHERE Id = @IdentificationCity";
            DataTable dtidentificacionCity = execute.GeneralExecuteQuerySqlCommand(query, "IdentificationCity", IdentificationCity);
            if (dtidentificacionCity == null || dtidentificacionCity.Rows.Count == 0) { return ""; }
            identificacionCity.Code = Convert.ToString(dtidentificacionCity.Rows[0]["Code"]);
            return identificacionCity.Code;
        }

    }
}
