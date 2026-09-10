//'************************************************************
//' Assembly         : Application.Inventory.InventoryControlServiceAdminService
//' Author           : Miguel Angel Fonseca Castro
//' Created          : 2018-04-14
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using System;
using System.Transactions;

namespace Application.Inventory.InventoryControl
{
    public class InventoryControlServiceAdminService : IInventoryControlServiceAdminService
    {
        #region Variables

        private IInventoryControlServiceRepository _inventoryControlServiceRepository;

        #endregion Variables

        #region Builder

        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="InventoryControlServiceRepository"></param>
        public InventoryControlServiceAdminService(IInventoryControlServiceRepository inventoryControlServiceRepository)
        {
            if (inventoryControlServiceRepository == null)
            {
                throw new ArgumentNullException("Repositorio de InventoryControlServiceRepository vacio");
            }

            _inventoryControlServiceRepository = inventoryControlServiceRepository;
        }

        #endregion Builder

        #region Methods

        /// <summary>
        /// Consulta el InventoryControlService por EntityId y EntityName
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryControlService> GetInventoryControlServiceByEntityIdAndEntityName(int entityId, string entityName)
        {
            if (entityId == 0)
            {
                throw new ArgumentNullException("entityId");
            }
            if (String.IsNullOrEmpty(entityName))
            {
                throw new ArgumentNullException("audit");
            }

            try
            {
                Domain.Entities.InventoryControlService inventoryControlService = _inventoryControlServiceRepository.GetInventoryControlServiceByEntityIdAndEntityName(entityId, entityName);
                return new ActionResult<Domain.Entities.InventoryControlService> { StateResult = true, ObjectEmbbeded = inventoryControlService };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryControlService> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Consulta el InventoryControlService por EntityId y EntityName
        /// </summary>
        /// <param name="entityCode"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryControlService> GetInventoryControlServiceByEntityCodeAndEntityName(string entityCode, string entityName)
        {
            if (String.IsNullOrEmpty(entityCode))
            {
                throw new ArgumentNullException("entityCode");
            }
            if (String.IsNullOrEmpty(entityName))
            {
                throw new ArgumentNullException("audit");
            }

            try
            {
                Domain.Entities.InventoryControlService inventoryControlService = _inventoryControlServiceRepository.GetInventoryControlServiceByEntityCodeAndEntityName(entityCode, entityName);
                return new ActionResult<Domain.Entities.InventoryControlService> { StateResult = true, ObjectEmbbeded = inventoryControlService };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryControlService> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// funcion utilizada para guardar InventoryControl
        /// </summary>
        /// <param name="inventoryControlService"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryControlService> SaveInventoryControlService(Domain.Entities.InventoryControlService inventoryControlService)
        {
            if (inventoryControlService == null)
            {
                throw new ArgumentNullException("InventoryControlService");
            }

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _inventoryControlServiceRepository.UnitWork;

                try
                {
                    _inventoryControlServiceRepository.SaveEntity(inventoryControlService);
                    _inventoryControlServiceRepository.UnitWork.Commit();
                    unitOfWork.Commit();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.InventoryControlService> { StateResult = true, ObjectEmbbeded = inventoryControlService };
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryControlService> { StateResult = false, Message = ex.Message };
                }
            }
        }

        #endregion Methods

        #region "IDisposable Support"

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
                _inventoryControlServiceRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion "IDisposable Support"
    }
}