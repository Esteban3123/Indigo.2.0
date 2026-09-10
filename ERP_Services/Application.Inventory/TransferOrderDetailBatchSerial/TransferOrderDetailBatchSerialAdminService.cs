///************************************************************
/// Assembly         : Aplication.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 03-06-2015
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

namespace Application.Inventory.TransferOrderDetailBatchSerial
{
    public class TransferOrderDetailBatchSerialAdminService : ITransferOrderDetailBatchSerialAdminService
    {

        #region Fields
        ITransferOrderDetailBatchSerialRepository  _transferOrderDetailBatchSerialRepository;
        #endregion

        #region Builder
        public TransferOrderDetailBatchSerialAdminService(ITransferOrderDetailBatchSerialRepository transferOrderDetailBatchSerialRepository)
        {
            if (transferOrderDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("transferOrderDetailBatchSerialRepository");
            }
            _transferOrderDetailBatchSerialRepository = transferOrderDetailBatchSerialRepository;
        }
        #endregion

        #region Methods
       
        /// <summary>
        /// lista los detalles del detalle de la orden de servicio
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Domain.Entities.TransferOrderDetailBatchSerial> ListTransferOrderDetailBatchSerialByTransferOrderId(int transferOrderId, bool flagQuantiyZero)
        {
            try
            {
                return _transferOrderDetailBatchSerialRepository.ListTransferOrderDetailBatchSerialByTransferOrderId(transferOrderId,flagQuantiyZero);
            }
            catch (Exception ex)
            {
                
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.TransferOrderDetailBatchSerial>();
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
                _transferOrderDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
