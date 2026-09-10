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

namespace Application.Inventory.RemissionOutputDetail
{
    public class RemissionOutputDetailAdminService : IRemissionOutputDetailAdminService
    {
        private IRemissionOutputDetailRepository _remissionOutputDetailRepository;

        public RemissionOutputDetailAdminService(IRemissionOutputDetailRepository remissionOutputDetailRepository)
        {
            if (remissionOutputDetailRepository == null)
            {
                throw new ArgumentNullException("remissionOutputDetailRepository");
            }
            _remissionOutputDetailRepository = remissionOutputDetailRepository;
        }

        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="RemissionOutputId"></param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionOutputDetail> GetRemissionOutputDetailByRemissionOutputId(int RemissionOutputId)
        {
            try
            {
                return _remissionOutputDetailRepository.ListRemissionOutputDetailByIdRemissionOutput(RemissionOutputId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.RemissionOutputDetail>();
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
                _remissionOutputDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
