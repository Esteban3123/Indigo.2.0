//'***********************************************************************
//' Assembly         : Application.Payments
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 07/04/2014
//'
//' Copyright        : (c) . All rights reserved.
//'***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Audit;
using Application.Base;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Application.Inventory.ProductGroup;
using Domain.Entities;
using System.Data;

namespace Application.Inventory.BlockRecordInventory
{
    public class BlockRecordInventoryAdminService : IBlockRecordInventoryAdminService
    {
        #region Fields

        private IBlockRecordInventoryRepository _repository;

        #endregion

        #region Builder

        public BlockRecordInventoryAdminService(IBlockRecordInventoryRepository repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("Repositorio de repository vacio");
            }
            _repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene un registro bloqueado
        /// </summary>
        /// <param name="IdForm"></param>
        /// <param name="IdRecord"></param>
        /// <returns></returns>
        public Domain.Entities.BlockRecordInventory GetBlockRecordInventoryByIdformAndIdRecord(string IdForm, string IdRecord)
        {
            if(IdForm == string.Empty || IdRecord == string.Empty)
            {
                throw new ArgumentNullException("IdForm IdRecord");
            }

            try
            {
                Domain.Entities.BlockRecordInventory blockRecordInventory = _repository.GetBlockRecordInventoryByIdformAndIdRecord(IdForm, IdRecord);
                return blockRecordInventory;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }   

        }

        /// <summary>
        /// Guarda un registro de bloqueo
        /// </summary>
        /// <param name="blockRecordInventory"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.BlockRecordInventory> SaveBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecordInventory)
        {
            if(blockRecordInventory == null)
            {
                throw new ArgumentNullException("blockRecordInventory Vacio");
            }

            IUnitWork UnitOfWork = _repository.UnitWork;
            try
            {
                _repository.SaveEntity(blockRecordInventory);
                UnitOfWork.Commit();
                return new ActionResult<Domain.Entities.BlockRecordInventory>{StateResult = true, ObjectEmbbeded = blockRecordInventory};
            }
            catch(Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "UIPolicy");
                return new ActionResult<Domain.Entities.BlockRecordInventory>{StateResult = false, Message = ex.Message};
            }        
        
        }

        /// <summary>
        /// Elimina el registro bloqueado
        /// </summary>
        /// <param name="blockRecorInventory"></param>
        /// <returns></returns>
        public ActionResult DeleteBlockRecordInventory(Domain.Entities.BlockRecordInventory blockRecorInventory)
        {
            if(blockRecorInventory == null)
            {
                throw new ArgumentNullException("blockRecorInventory Vacio");
            }

            IUnitWork UnitOfWork = _repository.UnitWork;
            try
            {
                _repository.DeleteEntity(blockRecorInventory);
                UnitOfWork.Commit();
                return new ActionResult{StateResult = true};
            }
            catch(OptimisticConcurrencyException ex)
            {
                UnitOfWork.RollbackChanges();
                return new ActionResult{StateResult = false, MessageResult = new List<string>()};
            }
            catch(UpdateException ex)
            {
                UnitOfWork.RollbackChanges();
                return new ActionResult{StateResult = false, MessageResult = new List<string>()};
            }
            catch(Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "UIPolicy");
                return new ActionResult{StateResult = false, Message = ex.Message};
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
                _repository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion        
    }
}
