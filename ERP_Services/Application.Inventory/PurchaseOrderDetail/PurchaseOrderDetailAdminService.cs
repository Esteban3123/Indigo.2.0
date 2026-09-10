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

namespace Application.Inventory.PurchaseOrderDetail
{
    public class PurchaseOrderDetailAdminService : IPurchaseOrderDetailAdminService
    {
        IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;

        public PurchaseOrderDetailAdminService(IPurchaseOrderDetailRepository purchaseOrderDetailRepository)
        {
            if (purchaseOrderDetailRepository == null)
            {
                throw new ArgumentNullException("purchaseOrderDetailRepository");
            }
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        }

        public List<Domain.Entities.PurchaseOrderDetail> GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(int supplierId, int supplierDistributionLineId)
        {
            try
            {
                return _purchaseOrderDetailRepository.GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PurchaseOrderDetail>();
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
                _purchaseOrderDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
