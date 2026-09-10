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
using Infrastructure.CrossCutting.Resources;

namespace Application.Inventory.InventoryContractDetail
{
    public class InventoryContractDetailAdminService : IInventoryContractDetailAdminService
    {
        IInventoryContractDetailRepository _inventoryContractDetailRepository;

        public InventoryContractDetailAdminService(IInventoryContractDetailRepository inventoryContractDetailRepository)
        {
            if (inventoryContractDetailRepository == null)
            {
                throw new ArgumentNullException("inventoryContractRepository");
            }
            _inventoryContractDetailRepository = inventoryContractDetailRepository;
        }

        public List<Domain.Entities.InventoryContractDetail> GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(int supplierId, int supplierDistributionLineId, int contractType)
        {
            try
            {
                return _inventoryContractDetailRepository.GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId,contractType );
            }
            catch (Exception ex )
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.InventoryContractDetail>();
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
                _inventoryContractDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
