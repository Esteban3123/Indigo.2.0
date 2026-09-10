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

namespace Application.Inventory.PharmacologicalGroup
{
    public class PharmacologicalGroupAdminService : IPharmacologicalGroupAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmGroupsPharmacological";
        private IPharmacologicalGroupRepository _pharmacologicalGroupRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public PharmacologicalGroupAdminService(IPharmacologicalGroupRepository pharmacologicalGroupRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((pharmacologicalGroupRepository == null))
            {
                throw new ArgumentNullException("Repositorio de pharmacologicalGroupRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _pharmacologicalGroupRepository = pharmacologicalGroupRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza un grupo farmacologico
        /// </summary>
        /// <param name="pharmacologicalGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmacologicalGroup> SavePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, AuditMessage audit, long idSecuence = 0)
        {
            if (pharmacologicalGroup == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._pharmacologicalGroupRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(pharmacologicalGroup.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                pharmacologicalGroup.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), pharmacologicalGroup.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.PharmacologicalGroup> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.PharmacologicalGroup auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.PharmacologicalGroup> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (pharmacologicalGroup.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    if (pharmacologicalGroup.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) { pharmacologicalGroup.CreationUser = audit.CodeUser; } else { pharmacologicalGroup.ModificationUser = audit.CodeUser; pharmacologicalGroup.ModificationDate = DateTime.Now; }
                    var resultSave = this._pharmacologicalGroupRepository.SavePharmacologicalGroup(pharmacologicalGroup.Code, pharmacologicalGroup.Name, pharmacologicalGroup.Status, audit.CodeUser);
                    TriggerEvent(pharmacologicalGroup, audit);
                    if (resultSave.CodeMessage == 0)
                    {
                        auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmacologicalGroup>(pharmacologicalGroup, audit, status, auxObjEntity);
                        auditProcess.Execute();
                        //Se marca la entidad como sin cambios
                        //measureUnit.MarkAsUnchanged();
                        scope.Complete();
                        return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = pharmacologicalGroup, Message = resultSave.Message };
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = false, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = pharmacologicalGroup, Message = resultSave.Message };
                    }

                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un grupo farmacologico
        /// </summary>
        /// <param name="pharmacologicalGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeletePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, AuditMessage audit)
        {
            if (pharmacologicalGroup == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    SP_DeletePharmacologicalGroup_Result result = _pharmacologicalGroupRepository.SP_DeletePharmacologicalGroup(pharmacologicalGroup.Id);
                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }
                    
                    pharmacologicalGroup.MarkAsDeleted();
                    TriggerEvent(pharmacologicalGroup, audit);

                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmacologicalGroup> ChangeStatePharmacologicalGroup(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.PharmacologicalGroup pharmacologicalGroup = _pharmacologicalGroupRepository.GetPharmacologicalGroup(code);
            //pharmacologicalGroup.Status = state;
            //return SavePharmacologicalGroup(pharmacologicalGroup, audit);



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
                Domain.Entities.PharmacologicalGroup pharmacologicalGroup = this._pharmacologicalGroupRepository.GetPharmacologicalGroup(code.Trim());
                if (pharmacologicalGroup != null && pharmacologicalGroup.Id > 0)
                {
                    pharmacologicalGroup.Status = state;
                }
                var result = this.SavePharmacologicalGroup(pharmacologicalGroup, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un grupo farmacologico por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroup(string code, AuditMessage audit)
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
                Domain.Entities.PharmacologicalGroup pharmacologicalGroup = _pharmacologicalGroupRepository.GetPharmacologicalGroup(code);
                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = true, ObjectEmbbeded = pharmacologicalGroup };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un grupo farmacologico por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroupById(int idProductGroup, AuditMessage audit)
        {
            if (idProductGroup == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.PharmacologicalGroup pharmacologicalGroup = _pharmacologicalGroupRepository.GetPharmacologicalGroupById(idProductGroup);
                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmacologicalGroup> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion


        #region Events

        public void TriggerEvent(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = pharmacologicalGroup.ChangeTracker.State.ToString().ToLower();
            if (pharmacologicalGroup.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(pharmacologicalGroup, audit.CodeUser, ChangeTracker, DittoSourceType.pharmacologicalGroup);
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
                _pharmacologicalGroupRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
