//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Daniel Eduardo Arévalo
//' Created          : 11/04/2019
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
using System.Transactions;

namespace Application.Inventory.ATCEntity
{
    public class ATCEntityAdminService : IATCEntityAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmATCEntity";
        private IATCEntityRepository _atcEntityRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public ATCEntityAdminService(IATCEntityRepository ATCEntityRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((ATCEntityRepository == null))
            {
                throw new ArgumentNullException("Repositorio de ATCEntityRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _atcEntityRepository = ATCEntityRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }

        #region Methods

        /// <summary>
        /// Guarda o actualiza una via de administracion
        /// </summary>
        /// <param name="AdministrationRoute"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ATCEntity> SaveATCEntity(Domain.Entities.ATCEntity ATCEntity, AuditMessage audit, long idSecuence = 0)
        {

            if (ATCEntity == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._atcEntityRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;

                    Domain.Entities.ATCEntity auxAtcEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ATCEntity> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (ATCEntity.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        ATCEntity.CreationUser = audit.CodeUser;
                        ATCEntity.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        auxAtcEntity = ATCEntity.OriginalValue;
                        ATCEntity.ModificationUser = audit.CodeUser;
                        ATCEntity.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ATCEntity>(ATCEntity, audit, status, auxAtcEntity);
                    _atcEntityRepository.SaveEntity(ATCEntity);
                    TriggerEvent(ATCEntity, audit);
                    unitOfWork.Commit();
                    auditProcess.Execute();
                    scope.Complete();

                    return new ActionResult<Domain.Entities.ATCEntity> { StateResult = true, ObjectEmbbeded = ATCEntity, Message = "Se guardó correctamente el ATC con Código " + ATCEntity.Code };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ATCEntity> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ATCEntity> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina una via de admistracion
        /// </summary>
        /// <param name="AdministrationRoute"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteATCEntity(Domain.Entities.ATCEntity ATCEntity, AuditMessage audit)
        {

            if (ATCEntity == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._atcEntityRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    ATCEntity.ModificationUser = audit.CodeUser;
                    ATCEntity.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ATCEntity>(ATCEntity, audit, status);

                    ATCEntity.MarkAsDeleted();
                    _atcEntityRepository.SaveEntity(ATCEntity);
                    unitOfWork.Commit();

                    TriggerEvent(ATCEntity, audit);

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
        public ActionResult<Domain.Entities.ATCEntity> ChangeStateATCEntity(string code, bool state, AuditMessage audit)
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
                Domain.Entities.ATCEntity ATCEntity = this._atcEntityRepository.GetATCEntity(code.Trim());
                if (ATCEntity != null && ATCEntity.Id > 0)
                {
                    ATCEntity.State = state;
                }
                var result = this.SaveATCEntity(ATCEntity, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ATCEntity> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ATCEntity> GetATCEntity(string code, AuditMessage audit)
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
                Domain.Entities.ATCEntity ATCEntity = _atcEntityRepository.GetATCEntity(code);
                return new ActionResult<Domain.Entities.ATCEntity> { StateResult = true, ObjectEmbbeded = ATCEntity };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ATCEntity> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene una via de administracion por id
        /// </summary>
        /// <param name="idAdministrationRoute"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ATCEntity> GetATCEntityById(int idATCEntity, AuditMessage audit)
        {
            if (idATCEntity == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.ATCEntity ATCEntity = _atcEntityRepository.GetATCEntityById(idATCEntity);
                return new ActionResult<Domain.Entities.ATCEntity> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ATCEntity> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #endregion

        #region Events

        public void TriggerEvent(Domain.Entities.ATCEntity ATCEntity, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = ATCEntity.ChangeTracker.State.ToString().ToLower();
            if (ATCEntity.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(ATCEntity, audit.CodeUser, ChangeTracker, DittoSourceType.aTCEntity);
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
                _atcEntityRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
