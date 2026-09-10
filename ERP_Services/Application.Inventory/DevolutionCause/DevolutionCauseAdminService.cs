#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Application.Base;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using System.Data;
using System.Transactions;
using Infrastructure.CrossCutting.Resources;

#endregion

namespace Application.Inventory.DevolutionCause
{
    public class DevolutionCauseAdminService : IDevolutionCauseAdminService
    {

        #region Variables

        private const string FORM_NAME = "FrmDevolutionCause";
        private IDevolutionCauseRepository _DevolutionCauseRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;

        #endregion

        #region Builder

        /// <summary>
        /// inicia el repositorio de causa de devolución
        /// </summary>
        /// <param name="measureUnitRepository">Repositorio de causa de devolución</param>
        /// <remarks></remarks>
        public DevolutionCauseAdminService(IDevolutionCauseRepository devolutionCauseRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            _DevolutionCauseRepository = devolutionCauseRepository;
            _sequenseRepository = sequenseRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Consulta la causa de devolución por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DevolutionCause> GetDevolutionCauseByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.DevolutionCause DevolutionCause = _DevolutionCauseRepository.GetDevolutionCauseByCode(code);
                return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = true, ObjectEmbbeded = DevolutionCause };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = false, MessageResult = { Utils.GetInnerExceptionMessageToString(ex) } };
            }
        }

        /// <summary>
        /// Consulta la causa de devolución por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DevolutionCause> GetDevolutionCauseById(int id, AuditMessage audit)
        {
            try
            {
                Domain.Entities.DevolutionCause DevolutionCause = _DevolutionCauseRepository.GetDevolutionCauseById(id);
                return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = false, MessageResult = { Utils.GetInnerExceptionMessageToString(ex) } };
            }
        }

        /// <summary>
        /// Guarda o actualiza una causa de devolución
        /// </summary>
        /// <param name="devolutionCause"></param>
        /// <param name="audit"></param>
        /// <param name="secuenceId"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DevolutionCause> SaveDevolutionCause(Domain.Entities.DevolutionCause devolutionCause, AuditMessage audit, long secuenceId = 0)
        {
            IUnitWork unitOfWork = this._DevolutionCauseRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;

            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(devolutionCause.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)secuenceId);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                devolutionCause.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.DevolutionCause> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), devolutionCause.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.DevolutionCause> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    Domain.Entities.DevolutionCause auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.DevolutionCause> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (devolutionCause.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        devolutionCause.CreationUser = audit.CodeUser;
                        devolutionCause.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = devolutionCause.OriginalValue;
                        devolutionCause.ModificationUser = audit.CodeUser;
                        devolutionCause.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    this._DevolutionCauseRepository.SaveEntity(devolutionCause);
                    unitOfWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.DevolutionCause>(devolutionCause, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    devolutionCause.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = devolutionCause, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Cambia el estado de una causa de devolución
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DevolutionCause> ChangeStateDevolutionCause(int id, bool state, AuditMessage audit)
        {   
            try
            {
                Domain.Entities.DevolutionCause devolutionCause = this._DevolutionCauseRepository.GetDevolutionCauseById(id);
                devolutionCause.Status = state;
                var result = this.SaveDevolutionCause(devolutionCause, audit);
                if (result.StateResult)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DevolutionCause> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina una causa de devolución
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteDevolutionCause(int id, AuditMessage audit)
        {           
            IUnitWork unitOfWork = this._DevolutionCauseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    Domain.Entities.DevolutionCause devolutionCause = this._DevolutionCauseRepository.GetDevolutionCauseById(id);

                    devolutionCause.ModificationUser = audit.CodeUser;
                    devolutionCause.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.DevolutionCause>(devolutionCause, audit, status);
                    devolutionCause.MarkAsDeleted();
                    _DevolutionCauseRepository.SaveEntity(devolutionCause);
                    unitOfWork.Commit();
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

                _DevolutionCauseRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion

    }
}
