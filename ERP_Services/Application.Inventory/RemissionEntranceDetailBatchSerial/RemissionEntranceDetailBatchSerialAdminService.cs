///************************************************************
/// Assembly         : Aplication.Inventory
/// Author           : Henry Alejandro Vargas Polania
/// Created          : 28-01-2015
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.RemissionEntranceDetailBatchSerial
{
    public class RemissionEntranceDetailBatchSerialAdminService: IRemissionEntranceDetailBatchSerialAdminService 
    {
        #region Fields
        IRemissionEntranceDetailBatchSerialRepository  _remissionEntranceDetailBatchSerialRepository;
        #endregion

        #region Builder
        public RemissionEntranceDetailBatchSerialAdminService(IRemissionEntranceDetailBatchSerialRepository remissionEntranceDetailBatchSerialRepository)
        {
            if (remissionEntranceDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("remissionEntranceDetailBatchSerialRepository");
            }
            _remissionEntranceDetailBatchSerialRepository = remissionEntranceDetailBatchSerialRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtiene los productos de la remision de entrada desde los lotes
        /// </summary>
        /// <param name="idSupplier"></param>
        /// <param name="idSupplierDistributionLine"></param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine(int idSupplier, int idSupplierDistributionLine)
        {
            try
            {
                return _remissionEntranceDetailBatchSerialRepository.ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier, idSupplierDistributionLine);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.RemissionEntranceDetailBatchSerial>();
            }
        }

        /// <summary>
        /// lista los detalles del detalle de la remision de entrada
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(int RemissionEntranceId)
        {
            try
            {
                return _remissionEntranceDetailBatchSerialRepository.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId);
            }
            catch (Exception ex)
            {
                
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.RemissionEntranceDetailBatchSerial>();
            }
        }

        /// <summary>
        /// lista los detalles del detalle de la remision de entrada por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(string code)
        {
            try
            {
                return _remissionEntranceDetailBatchSerialRepository.ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(code);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.RemissionEntranceDetailBatchSerial>();
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
                _remissionEntranceDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
