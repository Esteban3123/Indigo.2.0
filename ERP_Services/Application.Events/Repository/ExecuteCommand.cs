using Application.Events.Models;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Interface;
using System;
using System.Data;

namespace Application.Events.Repository
{
    public class ExecuteCommand : IDisposable
    {

        /// <summary>
        /// Regresar datatable de seguridad
        /// </summary>
        /// <param name="query"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public DataTable ExecuteQueryPublish(AuditMessage audit)
        {
            var ContainerSecurity = System.Configuration.ConfigurationManager.AppSettings.Get("containerSecurity");
            using (ConectionSQL sql = new ConectionSQL(ContainerSecurity))
            {
                String query = String.Format("SELECT TOP 1 * FROM Security.Containers WHERE TransactionalContainer = @CurrentContainer");
                QueueParameter queueParameter = new QueueParameter();
                return sql.SecurityConnection(query, "CurrentContainer", ServerSessionValues.Current.CurrentContainer, ContainerSecurity);
            }           
        }

        /// <summary>
        /// General Execute Query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GeneralExecuteQuery(String query)
        {
            QueueParameter queueParameter = new QueueParameter();           
            try
            {
                using (var sql = new ConectionSQL(ServerSessionValues.Current.CurrentContainer))
                {
                    DataTable dtPublish = sql.ExecuteCommand_Data(query);
                    return dtPublish;
                }         
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// SQLCOMAND Execute Query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GeneralExecuteQuerySqlCommand(String query,string NameParameter, Object value)
        {
            QueueParameter queueParameter = new QueueParameter();
            try
            {
                using (ConectionSQL sql = new ConectionSQL(ServerSessionValues.Current.CurrentContainer))
                {
                    DataTable dtPublish = sql.ExecuteSqlCommand_Data(query, NameParameter,value);
                    if (dtPublish == null){ return new DataTable(); }                
                    return dtPublish;
                }
                   
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ExecuteCommand() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
