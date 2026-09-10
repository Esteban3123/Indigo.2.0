//***********************************************************************
// Assembly         : Application.Inventory.PurchaseRequestDetail
// Author           : Hector Rodriguez R
// Created          : 10-04-2019
//
// Copyright        : (c) . All rights reserved.
// About            : PBI3499
//***********************************************************************

using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Base.Entities;
using Domain.Base;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;

namespace Application.Inventory.PurchaseRequestDetail
{
    public class PurchaseRequestDetailAdminService : IPurchaseRequestDetailAdminService
    {

        #region Fields
        IPurchaseRequestDetailRepository _PurchaseRequestDetailRepository;
        #endregion

        #region Builder
        public PurchaseRequestDetailAdminService(IPurchaseRequestDetailRepository PurchaseRequestDetailRepository)
        {
            if (PurchaseRequestDetailRepository == null)
            {
                throw new ArgumentNullException("PurchaseRequestDetailRepository");
            }
            _PurchaseRequestDetailRepository = PurchaseRequestDetailRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// lista los detalle de solicitud de compra de inventario por almacen o unidad funcional de destino
        /// </summary>
        /// <param name="idFunctionalUnit"></param>
        /// <returns></returns>
        public List<Domain.Entities.PurchaseRequestDetail> ListPurchaseRequestDetailByFunctionalUnit(int idFunctionalUnit)
        {
            try
            {
                return _PurchaseRequestDetailRepository.ListPurchaseRequestDetailByFunctionalUnit(idFunctionalUnit);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PurchaseRequestDetail>();
            }

        }

        /// <summary>
        /// Cambia La cantidad pendiente del item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionMessageResult ChangeQuantityPurchaseRequestDetail(int id, int QuantityExported)
        {
            IUnitWork unitOfWork = _PurchaseRequestDetailRepository.UnitWork;
            ActionMessageResult result = new ActionMessageResult();
            result.StateResult = true;

            try
            {
                Domain.Entities.PurchaseRequestDetail PurchaseRequestDetail = _PurchaseRequestDetailRepository.GetPurchaseRequestDetailById(id);
                PurchaseRequestDetail.OutstandingQuantity = PurchaseRequestDetail.OutstandingQuantity - QuantityExported;
                _PurchaseRequestDetailRepository.SaveEntity(PurchaseRequestDetail);
                unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                result.StateResult = false;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Obtiene Una solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.PurchaseRequestDetail GetPurchaseRequestDetailById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _PurchaseRequestDetailRepository.GetPurchaseRequestDetailById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
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
                _PurchaseRequestDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
