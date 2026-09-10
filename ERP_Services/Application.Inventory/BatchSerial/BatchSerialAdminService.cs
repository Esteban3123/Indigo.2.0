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
using Application.Base;

namespace Application.Inventory.BatchSerial
{
    public class BatchSerialAdminService : IBatchSerialAdminService
    {
        #region variables
        IBatchSerialRepository _batchSerialRepository;
        #endregion


        public BatchSerialAdminService(IBatchSerialRepository batchSerialRepository)
        {
            if (batchSerialRepository == null)
            {
                throw new ArgumentNullException("batchSerialRepository");
            }
            _batchSerialRepository = batchSerialRepository;
        }


        /// <summary>
        /// guarda un lote
        /// </summary>
        /// <param name="BatchSerial"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<Domain.Entities.BatchSerial> SaveBatchSerial(Domain.Entities.BatchSerial BatchSerial, AuditMessage audit)
        {
            IUnitWork unitOfWork = _batchSerialRepository.UnitWork;
            try
            {
                if (BatchSerial == null)
                {
                    throw new ArgumentNullException("BatchSerial");
                }
                if (_batchSerialRepository.ValidateIfExistsBatchSerial(BatchSerial.ProductId, BatchSerial.BatchCode, BatchSerial.ExpirationDate))
                {
                    string msg = "Ya existe un lote con código " + BatchSerial.BatchCode;
                    if (BatchSerial.ExpirationDate != null){
                        msg = msg + " y fecha de vencimiento " + BatchSerial.ExpirationDate.ToString();
                    }
                    return new ActionResult<Domain.Entities.BatchSerial> { StateResult = false, Message = msg };
                }

                //Se debe validar que no se creen duplicados (Por el momento indice unico)
                BatchSerial.CreationDate = DateTime.Now;
                BatchSerial.CreationUser = audit.CodeUser;
                _batchSerialRepository.SaveEntity(BatchSerial);
                unitOfWork.Commit();
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = true, ObjectEmbbeded = BatchSerial };
            }
            catch (System.Data.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }


        /// <summary>
        /// guarda un lote y actualiza la fecha de vencimineto dejando un historico 
        /// </summary>
        /// <param name="BatchSerial"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<Domain.Entities.BatchSerial> SaveBatchSerial(List<Domain.Entities.BatchSerial> ListBatchSerial, AuditMessage audit)
        {
            IUnitWork unitOfWork = _batchSerialRepository.UnitWork;
            try
            {
                if (ListBatchSerial == null && ListBatchSerial.Count == 0)
                {
                    throw new ArgumentNullException("ListBatchSerial");
                }
                foreach (Domain.Entities.BatchSerial item in ListBatchSerial)
                {
                    _batchSerialRepository.SaveEntity(item);
                }
                unitOfWork.Commit();
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = true, ObjectEmbbeded = ListBatchSerial[0] };
            }
            catch (System.Data.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
            }
        }

        /// <summary>
        /// lista los seriales por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Domain.Entities.BatchSerial> ListBatchSerialByProductId(int ProductId, DateTime? DateBatchSerial, int? WarehouseId, int? RemissionType)
        {
            try
            {
                return _batchSerialRepository.GetBatchSerialByProductId(ProductId, DateBatchSerial, WarehouseId, RemissionType);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.BatchSerial>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdmissionNumber"></param>
        /// <param name="ProductId"></param>
        /// <param name="DateBatchSerial"></param>
        /// <param name="WarehouseId"></param>
        /// <param name="RemissionType"></param>
        /// <returns></returns>
        public List<Domain.Entities.BatchSerial> ListBatchSerialCustodyByProductId(string AdmissionNumber, int ProductId, DateTime? DateBatchSerial, int? WarehouseId, int? RemissionType)
        {
            try
            {
                return _batchSerialRepository.GetBatchSerialCustodyByProductId(AdmissionNumber, ProductId, DateBatchSerial, WarehouseId, RemissionType);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.BatchSerial>();
            }
        }


        /// <summary>
        /// lista los seriales por el id del producto
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Domain.Entities.BatchSerial> GetBatchSerialByProductIdIncludeExpirationDate(int ProductId)
        {
            try
            {
                return _batchSerialRepository.GetBatchSerialByProductIdIncludeExpirationDate(ProductId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.BatchSerial>();
            }
        }

        /// <summary>
        /// Obtiene un lote por codigo
        /// </summary>
        public ActionResult<Domain.Entities.BatchSerial> BatchSerialByCode(int productId, string code, AuditMessage audit)
        {
            if (code == string.Empty)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.BatchSerial BatchSerial = _batchSerialRepository.BatchSerialByCode(productId, code);
                if (BatchSerial != null && BatchSerial.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.BatchSerial> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.BatchSerial>(BatchSerial, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = true, ObjectEmbbeded = BatchSerial };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.BatchSerial> { StateResult = false, MessageResult = { ex.Message } };
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
                _batchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
