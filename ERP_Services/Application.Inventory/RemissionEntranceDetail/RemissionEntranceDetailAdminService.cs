///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
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

namespace Application.Inventory.RemissionEntranceDetail
{
    public class RemissionEntranceDetailAdminService : IRemissionEntranceDetailAdminService
    {
        #region Fields
        IRemissionEntranceDetailRepository _remissionEntranceDetailRepository;
        #endregion

        #region Builder
        public RemissionEntranceDetailAdminService(IRemissionEntranceDetailRepository remissionEntranceDetailRepository)
        {
            if (remissionEntranceDetailRepository == null)
            {
                throw new ArgumentNullException("remissionEntranceDetailRepository");
            }
            _remissionEntranceDetailRepository = remissionEntranceDetailRepository;
        }
        #endregion


        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionEntranceDetail> GetRemissionEntranceDetailByRemissionEntranceId(int RemissionEntranceId)
        {
            try
            {
                return _remissionEntranceDetailRepository.ListRemissionEntranceDetailByIdRemissionEntrance(RemissionEntranceId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.RemissionEntranceDetail >();
            }
        }

        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionEntranceDetail> ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(int SupplierId, int SupplierDistributionLineId)
        {
            try
            {
                return _remissionEntranceDetailRepository.ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(SupplierId, SupplierDistributionLineId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.RemissionEntranceDetail >();
            }
        }

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
                _remissionEntranceDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
