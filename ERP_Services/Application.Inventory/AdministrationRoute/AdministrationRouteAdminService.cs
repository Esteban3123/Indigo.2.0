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
using System.Transactions;

namespace Application.Inventory.AdministrationRoute
{
    public class AdministrationRouteAdminService : IAdministrationRouteAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmAdministrationRoute";
        private IAdministrationRouteRepository _administrationRouteRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public AdministrationRouteAdminService(IAdministrationRouteRepository administrationRouteRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((administrationRouteRepository == null))
            {
                throw new ArgumentNullException("Repositorio de administrationRouteRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _administrationRouteRepository = administrationRouteRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza una via de administracion
        /// </summary>
        /// <param name="AdministrationRoute"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AdministrationRoute> SaveAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, AuditMessage audit, long idSecuence = 0)
        {
            if (AdministrationRoute == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._administrationRouteRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(AdministrationRoute.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                AdministrationRoute.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.AdministrationRoute> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), AdministrationRoute.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.AdministrationRoute> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.AdministrationRoute auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.AdministrationRoute> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (AdministrationRoute.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        AdministrationRoute.CreationUser = audit.CodeUser;
                        AdministrationRoute.CreationDate = DateTime.Now;
                    }
                    else
                    {
                        AdministrationRoute.ModificationUser = audit.CodeUser;
                        AdministrationRoute.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    var resultSave = this._administrationRouteRepository.SaveAdministrationRoute(AdministrationRoute.PharmaceuticalFormId, AdministrationRoute.Code, AdministrationRoute.Name, AdministrationRoute.Status,audit.CodeUser);

                    if (resultSave.CodeMessage == 0)
                    {
                        TriggerEvent(AdministrationRoute, audit);
                        auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.AdministrationRoute>(AdministrationRoute, audit, status, auxObjEntity);
                        auditProcess.Execute();
                        //Se marca la entidad como sin cambios
                        //measureUnit.MarkAsUnchanged();
                        scope.Complete();
                        return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = AdministrationRoute, Message = resultSave.Message };
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = false, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = AdministrationRoute, Message = resultSave.Message };
                    }
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina una via de admistracion
        /// </summary>
        /// <param name="AdministrationRoute"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, AuditMessage audit)
        {
            if (AdministrationRoute == null)
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
                    SP_DeleteAdministrationRoute_Result result = _administrationRouteRepository.SP_DeleteAdministrationRoute(AdministrationRoute.Id);
                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }

                    AdministrationRoute.MarkAsDeleted();
                    TriggerEvent(AdministrationRoute, audit);

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
        public ActionResult<Domain.Entities.AdministrationRoute> ChangeStateAdministrationRoute(string code, bool state, AuditMessage audit)
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
                Domain.Entities.AdministrationRoute AdministrationRoute = this._administrationRouteRepository.GetAdministrationRoute(code.Trim());
                if (AdministrationRoute != null && AdministrationRoute.Id > 0)
                {
                    AdministrationRoute.Status = state;
                }
                var result = this.SaveAdministrationRoute(AdministrationRoute, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRoute(string code, AuditMessage audit)
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
                Domain.Entities.AdministrationRoute AdministrationRoute = _administrationRouteRepository.GetAdministrationRoute(code);
                return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = true, ObjectEmbbeded = AdministrationRoute };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene una via de administracion por id
        /// </summary>
        /// <param name="idAdministrationRoute"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRouteById(int idAdministrationRoute, AuditMessage audit)
        {
            if (idAdministrationRoute == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.AdministrationRoute AdministrationRoute = _administrationRouteRepository.GetAdministrationRouteById(idAdministrationRoute);
                return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdministrationRoute> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #region Events

        public void TriggerEvent(Domain.Entities.AdministrationRoute administrationRoute, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = administrationRoute.ChangeTracker.State.ToString().ToLower();
            if (administrationRoute.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(administrationRoute, audit.CodeUser, ChangeTracker, DittoSourceType.administrationRoute);
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
                _administrationRouteRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
