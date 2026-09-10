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

namespace Application.Inventory.RemissionOutputDetailPhysical
{
    public class RemissionOutputDetailPhysicalAdminService : IRemissionOutputDetailPhysicalAdminService
    {
        #region fields
        IRemissionOutputDetailPhysicalRepository _remissionOutputDetailPhysicalRepository;
        #endregion

        #region builder
        public RemissionOutputDetailPhysicalAdminService(IRemissionOutputDetailPhysicalRepository remissionOutputDetailPhysicalRepository)
        {
            if (remissionOutputDetailPhysicalRepository == null)
            {
                throw new ArgumentNullException("remissionOutputDetailPhysicalRepository");
            }
            _remissionOutputDetailPhysicalRepository = remissionOutputDetailPhysicalRepository;
        }
        #endregion

        #region methods

        /// <summary>
        /// lista los detalles del detalle de la remision de salida
        /// </summary>
        /// <param name="RemissionOutputId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Domain.Entities.RemissionOutputDetailPhysical> ListRemissionOutputDetailPhysicalByRemissionOutputId(int RemissionOutputId)
        {
            try
            {
                return _remissionOutputDetailPhysicalRepository.ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId);
            }
            catch (Exception ex)
            {
                
               IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.RemissionOutputDetailPhysical>();
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
                _remissionOutputDetailPhysicalRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
