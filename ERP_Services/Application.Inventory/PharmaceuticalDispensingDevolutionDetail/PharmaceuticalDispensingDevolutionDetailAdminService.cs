///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Carlos Ernesto Cordoba
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
using Infrastructure.CrossCutting.Resources;
using System.Data;
using Application.Base;
using Application.Inventory.PhysicalInventory;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Text;
using Domain.Entities.Service;

namespace Application.Inventory.PharmaceuticalDispensingDevolutionDetail
{
    public class PharmaceuticalDispensingDevolutionDetailAdminService : IPharmaceuticalDispensingDevolutionDetailAdminService
    {

        private IPharmaceuticalDispensingDevolutionDetailRepository _pharmaceuticalDispensingDevolutionDetailRepository;

        public PharmaceuticalDispensingDevolutionDetailAdminService(IPharmaceuticalDispensingDevolutionDetailRepository pharmaceuticalDispensingDevolutionDetailRepository)
        {
            if (pharmaceuticalDispensingDevolutionDetailRepository == null)
            {
                throw new ArgumentNullException("pharmaceuticalDispensingDevolutionDetailRepository");
            }
            _pharmaceuticalDispensingDevolutionDetailRepository = pharmaceuticalDispensingDevolutionDetailRepository;
        }

        /// <summary>
        /// lista los detalles de la devolucion
        /// </summary>
        /// <param name="IdPharmaceuticalDispensingDevolution"></param>
        /// <returns></returns>
        public List<Domain.Entities.PharmaceuticalDispensingDevolutionDetail> GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(int IdPharmaceuticalDispensingDevolution)
        {
            try
            {
                return _pharmaceuticalDispensingDevolutionDetailRepository.GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(IdPharmaceuticalDispensingDevolution);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PharmaceuticalDispensingDevolutionDetail>();
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
                _pharmaceuticalDispensingDevolutionDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
