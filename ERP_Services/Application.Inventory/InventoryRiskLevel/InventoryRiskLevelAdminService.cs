//'************************************************************
//' Assembly         : Domain.Inventory.InventoryRiskLevel
//' Author           : John Ortiz
//' Created          : 30/10/2014
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

namespace Application.Inventory.InventoryRiskLevel
{
    public class InventoryRiskLevelAdminService : IInventoryRiskLevelAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmInventoryRiskLevel";
        private IInventoryRiskLevelRepository _riskLevelRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// Contructor del servicio de aplicacion de niveles de riesgos
        /// </summary>
        /// <param name="riskLevelRepository">Repositorio de niveles de riesgo</param>
        /// <param name="sequenseRepository">Repositorio de secuencia numerica</param>
        /// <remarks></remarks>
        public InventoryRiskLevelAdminService(IInventoryRiskLevelRepository riskLevelRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((riskLevelRepository == null))
            {
                throw new ArgumentNullException("riskLevelRepository", "Repositorio de riskLevelRepository esta vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("sequenseRepository", "Repositorio de sequenseRepository vacio");
            }
            _riskLevelRepository = riskLevelRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        /// <summary>
        /// Guarda o actualiza un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va guardar</param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryRiskLevel> SaveRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, AuditMessage audit, long idSecuence = 0)
        {
            if (riskLevel == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._riskLevelRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(riskLevel.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                riskLevel.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.InventoryRiskLevel> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), riskLevel.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.InventoryRiskLevel> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.InventoryRiskLevel auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryRiskLevel> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (riskLevel.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        sequenseUnitOfWork.Commit();
                    }
                    else
                    {
                        auxObjEntity = _riskLevelRepository.GetRiskLevelByCode(riskLevel.Code, false);
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    if (riskLevel.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) { riskLevel.CreationUser = audit.CodeUser; } else { riskLevel.ModificationUser = audit.CodeUser; riskLevel.ModificationDate = DateTime.Now; }
                    var resultSave = this._riskLevelRepository.SaveRiskLevel(riskLevel.Code, riskLevel.Name, riskLevel.Status, audit.CodeUser);
                    TriggerEvent(riskLevel, audit);
                    if (resultSave.CodeMessage == 0)
                    {
                        auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryRiskLevel>(riskLevel, audit, status, auxObjEntity);
                        auditProcess.Execute();
                        //Se marca la entidad como sin cambios
                        //measureUnit.MarkAsUnchanged();
                        scope.Complete();
                        return new ActionResult<Domain.Entities.InventoryRiskLevel> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = riskLevel, Message = resultSave.Message };
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.InventoryRiskLevel> { StateResult = false, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = riskLevel, Message = resultSave.Message };
                    }
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.InventoryRiskLevel> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRiskLevel> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va a eliminar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, AuditMessage audit)
        {
            if (riskLevel == null)
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
                    SP_DeleteRiskLevels_Result result = _riskLevelRepository.SP_DeleteRiskLevels(riskLevel.Id);
                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }

                    riskLevel.MarkAsDeleted();
                    TriggerEvent(riskLevel, audit);

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
        /// Actualiza el estado de un nivel de riesgo
        /// </summary>
        /// <param name="code">Codigo</param>
        /// <param name="audit"></param>
        /// <returns></returns>

        public ActionResult<Domain.Entities.InventoryRiskLevel> UpdateStateInventoryRiskLevel(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.Warehouse warehouse = _warehouseRepository.GetWarehouse(code);
            //warehouse.Status = state;
            //return SaveWarehouse(warehouse, audit);



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
                Domain.Entities.InventoryRiskLevel InventoryRiskLevel = this._riskLevelRepository.GetRiskLevelByCode(code.Trim());
                if (InventoryRiskLevel != null && InventoryRiskLevel.Id > 0)
                {
                    InventoryRiskLevel.Status = state;
                }
                var result = this.SaveRiskLevel(InventoryRiskLevel, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRiskLevel> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Consulta el nivel de riesgo por codigo
        /// </summary>
        /// <param name="code">Codigo del nivel de riesgo que se va almacenar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryRiskLevel> GetRiskLevelByCode(string code, AuditMessage audit)
        {
            if (code == null)
            {
                throw new ArgumentNullException("code", "El codigo en la funcion GetRiskLevelByCode es nulo");
            }
            try
            {
                Domain.Entities.InventoryRiskLevel riskLevel = _riskLevelRepository.GetRiskLevelByCode(code);
                IndigoAuditSimpleEntity<Domain.Entities.InventoryRiskLevel> auditoria = new IndigoAuditSimpleEntity<Domain.Entities.InventoryRiskLevel>(riskLevel, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                auditoria.Execute();
                return new ActionResult<Domain.Entities.InventoryRiskLevel>() { StateResult = true, ObjectEmbbeded = riskLevel };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRiskLevel> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #region Events

        public void TriggerEvent(Domain.Entities.InventoryRiskLevel InventoryRiskLevel, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = InventoryRiskLevel.ChangeTracker.State.ToString().ToLower();
            if (InventoryRiskLevel.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(InventoryRiskLevel, audit.CodeUser, ChangeTracker, DittoSourceType.inventoryRiskLevel);
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
                _riskLevelRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
