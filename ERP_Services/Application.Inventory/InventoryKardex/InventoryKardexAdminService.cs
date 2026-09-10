using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using System;

namespace Application.Inventory.InventoryKardex
{
    public class InventoryKardexAdminService : IInventoryKardexAdminService
    {
        #region Fields
        IKardexRepository _kardexRepository;
        #endregion
        #region Builder
        public InventoryKardexAdminService(IKardexRepository kardexRepository)
        {
            if (kardexRepository == null)
            {
                throw new ArgumentNullException("kardexRepository");
            }
            _kardexRepository = kardexRepository;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Obtiene la cantidad total de productos en el kardex de una bodega
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int GetQuantityKardex(int warehouseId)
        {
            try
            {
                return _kardexRepository.GetQuantityKardex(warehouseId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return 0;
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
                _kardexRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
