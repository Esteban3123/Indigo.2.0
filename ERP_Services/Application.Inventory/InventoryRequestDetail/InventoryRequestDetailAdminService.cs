///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 25-05-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Base.Entities;
using Domain.Base;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;

namespace Application.Inventory.InventoryRequestDetail
{
    public class InventoryRequestDetailAdminService : IInventoryRequestDetailAdminService
    {

        #region Fields
        IInventoryRequestDetailRepository _inventoryRequestDetailRepository;
        IInventoryRequestDetailOtherRepository _inventoryRequestDetailOtherRepository;
        #endregion

        #region Builder
        public InventoryRequestDetailAdminService(IInventoryRequestDetailRepository inventoryRequestDetailRepository, IInventoryRequestDetailOtherRepository inventoryRequestDetailOtherRepository)
        {
            if (inventoryRequestDetailRepository == null)
            {
                throw new ArgumentNullException("inventoryRequestDetailRepository");
            }
            _inventoryRequestDetailRepository = inventoryRequestDetailRepository;
            _inventoryRequestDetailOtherRepository = inventoryRequestDetailOtherRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// lista los detalle de solicitud de inventario por almacen o unidad funcional de destino
        /// </summary>
        /// <param name="idFilter"></param>
        /// <param name="orderType"></param>
        /// <param name="dispatchTo"></param>
        /// <returns></returns>
        public List<Domain.Entities.InventoryRequestDetail> ListInventoryRequestDetailByTarget(int idFilter, int orderType, int dispatchTo)
        {
            try
            {
                return _inventoryRequestDetailRepository.ListInventoryRequestDetailByTarget(idFilter, orderType, dispatchTo);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.InventoryRequestDetail>();
            }

        }

        /// <summary>
        /// Cambia La cantidad pendiente del item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionMessageResult ChangeQuantityInventoryRequestDetail(int id, int QuantityExported)
        {
            IUnitWork unitOfWork = _inventoryRequestDetailRepository.UnitWork;
            ActionMessageResult result = new ActionMessageResult();
            result.StateResult = true;

            try
            {
                Domain.Entities.InventoryRequestDetail inventoryRequestDetail = _inventoryRequestDetailRepository.GetInventoryRequestDetailById(id);
                inventoryRequestDetail.OutstandingQuantity = inventoryRequestDetail.OutstandingQuantity - QuantityExported;
                _inventoryRequestDetailRepository.SaveEntity(inventoryRequestDetail);
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
        /// Actualiza la cantidad de la solicitud seleccionada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Quantity"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionMessageResult UpdateQuantityAuthorizedInventoryRequestDetail(int id, int Quantity, string User)
        {
            IUnitWork unitOfWork = _inventoryRequestDetailOtherRepository.UnitWork;
            ActionMessageResult result = new ActionMessageResult();
            result.StateResult = true;

            try
            {
                Domain.Entities.InventoryRequestDetailOther inventoryRequestDetailOther = _inventoryRequestDetailOtherRepository.GetInventoryRequestDetailOtherById(id);

                if (Quantity == 0)
                {
                    inventoryRequestDetailOther.Status = 4;
                }

                inventoryRequestDetailOther.Quantity = Quantity;
                inventoryRequestDetailOther.OutstandingQuantity = Quantity;
                inventoryRequestDetailOther.UserConfirmAuthorization = User;
                _inventoryRequestDetailOtherRepository.SaveEntity(inventoryRequestDetailOther);
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
        /// Obtiene Una solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryRequestDetail GetInventoryRequestDetailById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _inventoryRequestDetailRepository.GetInventoryRequestDetailById(id);
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
                _inventoryRequestDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
