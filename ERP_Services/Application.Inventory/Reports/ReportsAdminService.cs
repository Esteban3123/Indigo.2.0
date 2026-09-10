#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Base;
using Application.Inventory.PhysicalInventory;
using Domain.Entities;
using Domain.Entities.Service;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using static System.Collections.Specialized.BitVector32;

#endregion

namespace Application.Inventory.Reports
{
    public class ReportsAdminService : IReportsAdminService
    {

        #region fields        
            private ISettingInventoryRepository _IReportValuedInventoryRepository;
            private List<SP_ReportValuedInventory_Result> ReportValuedInventoryData;
        #endregion

        #region Builder
        public ReportsAdminService(ISettingInventoryRepository SettingInventoryRepository) 
        {
            if (SettingInventoryRepository == null)
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }
            _IReportValuedInventoryRepository = SettingInventoryRepository;
        }
        #endregion

        #region Methods

        public DataSet GetReportControlMedications(Dictionary<string, string> criterias, SessionValues session)
        {
            try
            {
                var xmlCriterias = Utils.DictionaryToXML(criterias);

                DataSet ds = new System.Data.DataSet();
                string query = "EXEC [Inventory].[SP_ReportControlMedications] '" + xmlCriterias + "'";
                DataTable dt = GetDatatable(query, session, "ReportControlMedications");
                ds.Tables.Add(dt.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        public DataSet GetReportFiscalAccount(Dictionary<string, string> criterias, SessionValues session)
        {
            try
            {
                var xmlCriterias = Utils.DictionaryToXML(criterias);

                DataSet ds = new System.Data.DataSet();
                string query = "EXEC [Inventory].[SP_ReportFiscalAccount] '" + xmlCriterias + "'";
                DataTable dt = GetDatatable(query, session, "ReportFiscalAccount");
                ds.Tables.Add(dt.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        public List<SP_ReportValuedInventory_Result> GetReportValuedInventory(Dictionary<String, String> filters, Dictionary<String, String> range, SessionValues session)
        {
            try
            {
                var xmlCriterias = Utils.DictionaryToXML(filters).ToString();
                var xmlRange = Utils.DictionaryToXML(range).ToString();

                ReportValuedInventoryData = _IReportValuedInventoryRepository.GetSP_ReportValuedInventory(xmlCriterias, xmlRange);
                return ReportValuedInventoryData;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }
        #endregion

        #region Private Methods

        public System.Data.DataTable GetDatatable(string Comando, SessionValues session, string nameDt)
        {
            System.Data.DataTable functionReturnValue = default(System.Data.DataTable);
            System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection();

            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion = new System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, string.Empty, session.TransactionalContainer, false));
                    conexion.Open();
                }
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(Comando, conexion);
                da.SelectCommand.CommandTimeout = 30000;
                DataSet ds = new DataSet();
                da.Fill(ds, nameDt);
                functionReturnValue = ds.Tables[nameDt];
                da = null;
                ds = null;
                conexion.Close();
                return functionReturnValue;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }                
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion

    }
}
