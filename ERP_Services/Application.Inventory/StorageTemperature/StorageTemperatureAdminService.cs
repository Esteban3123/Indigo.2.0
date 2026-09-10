//************************************************************
// Assembly         : Application.Inventory
// Author           : Andres Alaron
// Created          : 29-11-2024
//
// Copyright        : (c) . All rights reserved.
//************************************************************

using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Application.Inventory.StorageTemperature
{
    public class StorageTemperatureAdminService : IStorageTemperatureAdminService
    {

        #region Variables

        private const string FORM_NAME = "FrmStorageTemperature";
        private IStorageTemperatureRepository _StorageTemperatureRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;

        #endregion

        #region Builder

        /// <summary>
        /// inicia el repositorio de rangos de temperatura
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public StorageTemperatureAdminService(IStorageTemperatureRepository storageTemperature, 
                                                IInventorySequenceDetailRepository inventorySequenceDetailRepository)
        {
            if (storageTemperature == null)
            {
                throw new ArgumentNullException("Repositorio de productRepository vacio");
            }

            if (inventorySequenceDetailRepository == null)
            {
                throw new ArgumentNullException("Repositorio de inventorySequenceDetailRepository vacio");
            }

            _StorageTemperatureRepository = storageTemperature;
            _sequenseRepository = inventorySequenceDetailRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene un rango de temperatura por id
        /// </summary>
        public ActionResult<Domain.Entities.StorageTemperature> GetStorageTemperatureById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                Domain.Entities.StorageTemperature storageTemperature = _StorageTemperatureRepository.GetStorageTemperatureById(id);
                return new ActionResult<Domain.Entities.StorageTemperature> { StateResult = true, ObjectEmbbeded = storageTemperature };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene un rango de temperatura por codigo
        /// </summary>
        public ActionResult<Domain.Entities.StorageTemperature> GetStorageTemperature(string code, AuditMessage audit)
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
                Domain.Entities.StorageTemperature storageTemperature = _StorageTemperatureRepository.GetStorageTemperature(code);
                if(storageTemperature is null)
                {
                    return new ActionResult<Domain.Entities.StorageTemperature> { StateResult = true, ObjectEmbbeded = null, Message =  "No existe el código: " + code  };
                }

                return new ActionResult<Domain.Entities.StorageTemperature> { StateResult = true, ObjectEmbbeded = storageTemperature };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.StorageTemperature> { StateResult = false, Message =  ex.Message };
            }
        }

        /// <summary>
        /// Guarda o actualiza un rango de temperatura
        /// </summary>
        public ActionResult<Domain.Entities.StorageTemperature> SaveStorageTemperature(Domain.Entities.StorageTemperature storageTemperature, AuditMessage audit, long idSecuence = 0)
        {
            if (storageTemperature == null)
            {
                throw new ArgumentNullException("storageTemperature");
            }

            IUnitWork unitOfWork = this._StorageTemperatureRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(storageTemperature.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                storageTemperature.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.StorageTemperature> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), storageTemperature.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.StorageTemperature> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    if (storageTemperature.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        storageTemperature.CreationUser = audit.CodeUser;
                        storageTemperature.CreationDate = DateTime.Now;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        storageTemperature.ModificationUser = audit.CodeUser;
                        storageTemperature.ModificationDate = DateTime.Now;
                    }

                    this._StorageTemperatureRepository.SaveEntity(storageTemperature);
                    unitOfWork.Commit();
                    scope.Complete();

                    return new ActionResult<Domain.Entities.StorageTemperature> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = storageTemperature, Message = MessageResult };
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.StorageTemperature> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
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
                _StorageTemperatureRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }

    #endregion
}