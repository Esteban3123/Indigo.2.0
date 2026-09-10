//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 22/09/2014
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
using System.Text;
using System.Transactions;

namespace Application.Inventory.PharmaceuticalForm
{
    public class PharmaceuticalFormAdminService : IPharmaceuticalFormAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmPharmaceuticalForm";
        private IPharmaceuticalFormRepository _pharmaceuticalFormRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public PharmaceuticalFormAdminService(IPharmaceuticalFormRepository pharmaceuticalFormRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((pharmaceuticalFormRepository == null))
            {
                throw new ArgumentNullException("Repositorio de pharmaceuticalFormRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _pharmaceuticalFormRepository = pharmaceuticalFormRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza una forma farmaceutica
        /// </summary>
        /// <param name="PharmaceuticalForm"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalForm> SavePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, AuditMessage audit, long idSecuence = 0)
        {
            if (PharmaceuticalForm == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._pharmaceuticalFormRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(PharmaceuticalForm.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                PharmaceuticalForm.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), PharmaceuticalForm.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.PharmaceuticalForm> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.PharmaceuticalForm auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalForm> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (PharmaceuticalForm.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    if (PharmaceuticalForm.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) { PharmaceuticalForm.CreationUser = audit.CodeUser; } else { PharmaceuticalForm.ModificationUser = audit.CodeUser; PharmaceuticalForm.ModificationDate = DateTime.Now; }

                    string xml = ConvertPharmaceuticalFormToXml(PharmaceuticalForm);
                    var resultSave = this._pharmaceuticalFormRepository.SavePharmaceuticalForm(xml, audit.CodeUser);
                    TriggerEvent(PharmaceuticalForm, audit);
                    if (resultSave.CodeMessage == 0)
                    {
                        auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalForm>(PharmaceuticalForm, audit, status, auxObjEntity);
                        auditProcess.Execute();
                        scope.Complete();
                        return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = PharmaceuticalForm, Message = resultSave.Message };
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = false, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = PharmaceuticalForm, Message = resultSave.Message };
                    }
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Convierte toda la entidad a un XML como string
        /// </summary>
        /// <param name="pharmaceuticalForm"></param>
        /// <returns></returns>
        private string ConvertPharmaceuticalFormToXml(Domain.Entities.PharmaceuticalForm pharmaceuticalForm)
        {
            if (pharmaceuticalForm == null)
            {
                throw new ArgumentNullException("PharmaceuticalForm");
            }

            StringBuilder result = new StringBuilder();
            result.Append("<PharmaceuticalForm>");
            result.Append("<Code>" + pharmaceuticalForm.Code + "</Code>");
            result.Append("<Name>" + pharmaceuticalForm.Name + "</Name>");
            result.Append("<Status>" + pharmaceuticalForm.Status + "</Status>");
            result.Append("<RequireStability>" + pharmaceuticalForm.RequireStability + "</RequireStability>");
            if (pharmaceuticalForm.PharmaceuticalFormGroupingId != null)
            {
                result.Append("<PharmaceuticalFormGroupingId>" + pharmaceuticalForm.PharmaceuticalFormGroupingId + "</PharmaceuticalFormGroupingId>");
            }
            
            result.Append("</PharmaceuticalForm>");

            return result.ToString();
        }

        /// <summary>
        /// Elimina una forma farmaceutica
        /// </summary>
        /// <param name="PharmaceuticalForm"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeletePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, AuditMessage audit)
        {
            if (PharmaceuticalForm == null)
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
                    SP_DeletePharmaceuticalForm_Result result = _pharmaceuticalFormRepository.SP_DeletePharmaceuticalForm(PharmaceuticalForm.Id);

                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }
                    
                    PharmaceuticalForm.MarkAsDeleted();
                    TriggerEvent(PharmaceuticalForm, audit);

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
        /// Cambia el estado de una forma farmaceutica
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalForm> ChangeStatePharmaceuticalForm(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.PharmaceuticalForm PharmaceuticalForm = _pharmaceuticalFormRepository.GetPharmaceuticalForm(code);
            //PharmaceuticalForm.Status = state;
            //return SavePharmaceuticalForm(PharmaceuticalForm, audit);


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
                Domain.Entities.PharmaceuticalForm Warehouse = this._pharmaceuticalFormRepository.GetPharmaceuticalForm(code.Trim());
                if (Warehouse != null && Warehouse.Id > 0)
                {
                    Warehouse.Status = state;
                }
                var result = this.SavePharmaceuticalForm(Warehouse, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene una forma farmaceutica por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalForm(string code, AuditMessage audit)
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
                Domain.Entities.PharmaceuticalForm PharmaceuticalForm = _pharmaceuticalFormRepository.GetPharmaceuticalForm(code);
                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = true, ObjectEmbbeded = PharmaceuticalForm };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene una forma farmaceutica por id
        /// </summary>
        /// <param name="idPharmaceuticalForm"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalFormById(int idPharmaceuticalForm, AuditMessage audit)
        {
            if (idPharmaceuticalForm == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.PharmaceuticalForm PharmaceuticalForm = _pharmaceuticalFormRepository.GetPharmaceuticalFormById(idPharmaceuticalForm);
                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalForm> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #region Events

        public void TriggerEvent(Domain.Entities.PharmaceuticalForm pharmaceuticalForm, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = pharmaceuticalForm.ChangeTracker.State.ToString().ToLower();
            if (pharmaceuticalForm.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(pharmaceuticalForm, audit.CodeUser, ChangeTracker, DittoSourceType.pharmaceuticalForm);
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
                _pharmaceuticalFormRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
