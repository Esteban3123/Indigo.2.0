//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 16/09/2014
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Base;
using Application.Events.Serializers;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace Application.Inventory.Manufacturer
{
    public class ManufacturerAdminService : IManufacturerAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmManufacturers";
        private IManufacturerRepository _manufacturerRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public ManufacturerAdminService(IManufacturerRepository manufacturerRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((manufacturerRepository == null))
            {
                throw new ArgumentNullException("Repositorio de manufacturerRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _manufacturerRepository = manufacturerRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza el fabricante
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.Manufacturer> SaveManufacturer(Domain.Entities.Manufacturer manufacturer, AuditMessage audit, long idSecuence = 0)
        {
            if (manufacturer == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._manufacturerRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(manufacturer.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                manufacturer.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.Manufacturer> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), manufacturer.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.Manufacturer> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.Manufacturer auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.Manufacturer> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (manufacturer.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        manufacturer.CreationUser = audit.CodeUser;
                        manufacturer.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = manufacturer.OriginalValue;
                        manufacturer.ModificationUser = audit.CodeUser;
                        manufacturer.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }                    
                    this._manufacturerRepository.SaveEntity(manufacturer);
                    unitOfWork.Commit();
                    TriggerEvent(manufacturer, audit);
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.Manufacturer>(manufacturer, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    manufacturer.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.Manufacturer> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = manufacturer, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.Manufacturer> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Manufacturer> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un fabricante
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteManufacturer(Domain.Entities.Manufacturer manufacturer, AuditMessage audit)
        {
            if (manufacturer == null)
            {
                throw new ArgumentNullException("manufacturer");
            }

            IUnitWork unitOfWork = this._manufacturerRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    manufacturer.ModificationUser = audit.CodeUser;
                    manufacturer.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.Manufacturer>(manufacturer, audit, status);

                    _manufacturerRepository.DeleteEntity(manufacturer);
                    unitOfWork.Commit();

                    manufacturer.MarkAsDeleted();
                    TriggerEvent(manufacturer, audit);

                    auditProcess.Execute();
                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (UpdateException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-000" }, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-000" }, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.Manufacturer> ChangeStateManufacturer(string code, bool state, AuditMessage audit)
        {


            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            //if (Boolean .isn(state)) {
            //    throw new ArgumentNullException("state");
            //}
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.Manufacturer manufacturer = this._manufacturerRepository.GetManufacturer(code.Trim());
                if (manufacturer != null && manufacturer.Id > 0)
                {
                    manufacturer.Status = state;
                }
                var result = this.SaveManufacturer(manufacturer, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Manufacturer> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un fabricante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.Manufacturer> GetManufacturer(string code, AuditMessage audit)
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
                Domain.Entities.Manufacturer manufacturer = _manufacturerRepository.GetManufacturer(code);
                return new ActionResult<Domain.Entities.Manufacturer> { StateResult = true, ObjectEmbbeded = manufacturer };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Manufacturer> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un fabricante por id
        /// </summary>
        /// <param name="idManufacturer"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.Manufacturer> GetManufacturerById(int idManufacturer, AuditMessage audit)
        {
            if (idManufacturer == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.Manufacturer manufacturer = _manufacturerRepository.GetManufacturerById(idManufacturer);
                return new ActionResult<Domain.Entities.Manufacturer> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Manufacturer> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #region Events

        public void TriggerEvent(Domain.Entities.Manufacturer manufacturer, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = manufacturer.ChangeTracker.State.ToString().ToLower();
            if (manufacturer.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(manufacturer, audit.CodeUser, ChangeTracker, DittoSourceType.manufacturer);
            IIndigoQueue queue = _factoryQueue.CreateQueue();
            queue.Publish(eventData);
            return;
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
                _manufacturerRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
