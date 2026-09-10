#region Imports

using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

#endregion

namespace Application.Inventory.Reports
{
    public interface IReportsAdminService :IDisposable
    {

        /// <summary>
        /// Metodo que consulta los medicamentos de control
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportControlMedications(Dictionary<string, string> criterias, SessionValues session);

        /// <summary>
        /// Metodo que consulta del reporte de cuenta fiscal
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        System.Data.DataSet GetReportFiscalAccount(Dictionary<string, string> criterias, SessionValues session);

        /// <summary>
        /// Metodo que consulta el reporte de inventario valorizado
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        List<SP_ReportValuedInventory_Result> GetReportValuedInventory( Dictionary<String, String> filters, Dictionary<String, String> range, SessionValues session);

    }
}
