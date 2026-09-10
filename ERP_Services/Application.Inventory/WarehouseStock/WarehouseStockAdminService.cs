///************************************************************
/// Assembly         : Aplication.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 05-06-2015
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Resources;
using Domain.Base.Entities;
using Domain.Entities;
using System.Data;
using Application.Base;
using System.Transactions;
using System.Data.Entity.Validation;
using Domain.Entities.Service;

namespace Application.Inventory.WarehouseStock
{
    public class WarehouseStockAdminService : IWarehouseStockAdminService
    {

        #region Fields
        IWarehouseStockRepository _warehouseStockRepository;
        #endregion

        #region Builder
        public WarehouseStockAdminService(IWarehouseStockRepository warehouseStockRepository)
        {
            if (warehouseStockRepository == null)
            {
                throw new ArgumentNullException("warehouseStockRepository");
            }
            _warehouseStockRepository = warehouseStockRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o Actualiza un listado de stock de almacenes
        /// </summary>
        /// <param name="listWarhouseStock"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult SaveListWarehouseStock(List<Domain.Entities.WarehouseStock> listWarhouseStock, AuditMessage audit)
        {
            if (listWarhouseStock.Count < 0)
            {
                throw new ArgumentNullException("listWarhouseStock");
            }
            IUnitWork unitOfWork = _warehouseStockRepository.UnitWork;

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    foreach (Domain.Entities.WarehouseStock itemWarehouseStock in listWarhouseStock)
                    {

                        Domain.Entities.WarehouseStock auxWarehouseStock = null;
                        IndigoAuditSimpleEntity<Domain.Entities.WarehouseStock> auditProcess;
                        Infrastructure.CrossCutting.Audit.Actions status;

                        if (itemWarehouseStock.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                        {
                            itemWarehouseStock.CreationUser = audit.CodeUser;
                            itemWarehouseStock.CreationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        }
                        else
                        {
                            auxWarehouseStock = itemWarehouseStock.OriginalValue;
                            itemWarehouseStock.ModificationUser = audit.CodeUser;
                            itemWarehouseStock.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }

                        _warehouseStockRepository.SaveEntity(itemWarehouseStock);
                        unitOfWork.Commit();
                        auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.WarehouseStock>(itemWarehouseStock, audit, status, auxWarehouseStock);
                        auditProcess.Execute();

                    }

                    transaction.Complete();
                    return new ActionResult { StateResult = true };

                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult { StateResult = false, MessageResult = new List<string> { "-999" } };
                }
                catch (UpdateException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult { StateResult = false, MessageResult = new List<string> { "-000" } };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult { StateResult = false, MessageResult = { ex.Message } };
                }
            }
        }

        /// <summary>
        /// Lista los stock de almacen por id de almacen
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public List<Domain.Entities.WarehouseStock> ListWarehouseStocksByWarehouseId(int warehouseId)
        {
            try
            {
                return _warehouseStockRepository.ListWarehouseStocksByWarehouseId(warehouseId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.WarehouseStock>();
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
                _warehouseStockRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
