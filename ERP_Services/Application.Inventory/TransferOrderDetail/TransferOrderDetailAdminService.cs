///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 25-05-2014
///
/// Copyright        : (c) . All rights reserved.
//***********************************************************

using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Base.Entities;
using Domain.Base;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Application.Inventory.TransferOrderDetail
{
   public class TransferOrderDetailAdminService : ITransferOrderDetailAdminService
    {

        #region Fields
        ITransferOrderDetailRepository _transferOrderDetailRepository;
        #endregion

        #region Builder
        public TransferOrderDetailAdminService(ITransferOrderDetailRepository transferOrderDetailRepository)
        {
            if (transferOrderDetailRepository == null)
            {
                throw new ArgumentNullException("transferOrderDetailRepository");
            }
            _transferOrderDetailRepository = transferOrderDetailRepository;
        }
        #endregion

        #region Methods
        
       /// <summary>
       /// lista los detalle de orden de traslado por almacen o unidad funcional de destino
       /// </summary>
       /// <param name="idFilter"></param>
       /// <param name="orderType"></param>
       /// <param name="dispatchTo"></param>
       /// <returns></returns>
        public List<Domain.Entities.TransferOrderDetail> ListTrasnferOrderDetailByTarget(int idFilter, int orderType, int dispatchTo)
        {
            try
            {
                return _transferOrderDetailRepository.ListTransferOrderDetailByTarget(idFilter, orderType, dispatchTo);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.TransferOrderDetail>();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet ListAverageConsumptionTransfer(string parameters, SessionValues session)
        {
            if (string.IsNullOrEmpty(parameters))
                throw new ArgumentNullException("parameters");

            try
            {
                System.Data.DataSet ds;
                string query1;
                query1 = string.Format("exec Inventory.SP_AverageConsumptionTransfer '{0}'", parameters);

                ds = this.GetDatatable(query1, session, "Inventory_SP_AverageConsumptionTransfer");
                
                return ds;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        ///  Metodo para listar el reporte de promedio de entradas de inventario. 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public DataSet ListAverageEntranceOrder(string parameters, SessionValues session)
        {
            if (string.IsNullOrEmpty(parameters))
                throw new ArgumentNullException("parameters");

            try
            {
                DataSet ds;
                string query1;
                query1 = string.Format("exec Inventory.SP_AverageReportEntranceOrder '{0}'", parameters);
                ds = this.GetDatatable(query1, session, "Inventory_SP_AverageReportEntranceOrder");
                
                return ds;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Comando"></param>
        /// <param name="session"></param>
        /// <param name="nameDt"></param>
        /// <returns></returns>
        private DataSet GetDatatable(string Comando, SessionValues session, string nameDt)
        {
            SqlConnection conexion = new SqlConnection();
            DataSet _GetDatatable;
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion = new SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, string.Empty, session.TransactionalContainer, false));
                    conexion.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(Comando, conexion);
                da.SelectCommand.CommandTimeout = 30000;
                DataSet ds = new DataSet();
                da.Fill(ds, nameDt);
                _GetDatatable = ds; //ds.Tables[nameDt];

                da = null/* TODO Change to default(_) if this is not a reference type */;
                ds = null/* TODO Change to default(_) if this is not a reference type */;
                conexion.Close();
                return _GetDatatable;
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
                _transferOrderDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
