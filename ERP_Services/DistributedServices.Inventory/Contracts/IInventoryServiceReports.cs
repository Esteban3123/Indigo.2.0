#region Imports

using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.ServiceModel;

#endregion

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceReports
    {

        /// <summary>
        /// Metodo que consulta los medicamentos de control
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportControlMedications(Dictionary<string, string> criterias, SessionValues session);

        /// <summary>
        /// Metodo que consulta del reporte de cuenta fiscal
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet GetReportFiscalAccount(Dictionary<string, string> criterias, SessionValues session);
        
        /// <summary>
        /// Metodo que consulta el reporte de inventario valorizado
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="range"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        List<SP_ReportValuedInventory_Result> GetReportValuedInventory(Dictionary<String, String> filters, Dictionary<String, String> range, SessionValues session);

 }
}
