///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Angi Camila Duran Vargas
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

namespace Application.Inventory.ProductInTransitDetail
{
    public class ProductInTransitDetailAdminService : IProductInTransitDetailAdminService
    {
        #region Fields
        IProductInTransitDetailRepository _productInTransitDetailRepository;
        #endregion

        #region Builder
        public ProductInTransitDetailAdminService(IProductInTransitDetailRepository productInTransitDetailRepository)
        {
            if (productInTransitDetailRepository == null)
            {
                throw new ArgumentNullException("productInTransitDetailRepository");
            }
            _productInTransitDetailRepository = productInTransitDetailRepository;
        }
        #endregion


        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ProductInTransitId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ProductInTransitDetail> GetProductInTransitDetailByProductInTransitId(int ProductInTransitId)
        {
            try
            {
                return _productInTransitDetailRepository.ListProductInTransitDetailByIdProductInTransit(ProductInTransitId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.ProductInTransitDetail>();
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
                _productInTransitDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
