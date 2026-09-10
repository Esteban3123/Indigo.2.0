//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 12/09/2014
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


namespace Application.Inventory.MeasureUnit
{
    public class MeasureUnitAdminService : IMeasureUnitAdminService
    {
        #region Variables
        private IMeasureUnitRepository _measureUnitRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        private const string FORM_NAME = "FrmMeasureUnit";
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public MeasureUnitAdminService(IMeasureUnitRepository measureUnitRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((measureUnitRepository == null))
            {
                throw new ArgumentNullException("Repositorio de measureUnitRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _measureUnitRepository = measureUnitRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza una unidad de medida
        /// </summary>
        /// <param name="measureUnit"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<InventoryMeasurementUnit> SaveMeasureUnit(InventoryMeasurementUnit measureUnit, AuditMessage audit, long idSecuence = 0)
        {
            if (measureUnit == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }


            IUnitWork unitOfWork = this._measureUnitRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
              
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(measureUnit.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                measureUnit.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<InventoryMeasurementUnit> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), measureUnit.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<InventoryMeasurementUnit> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    InventoryMeasurementUnit auxObjEntity = null;
                    IndigoAuditSimpleEntity<InventoryMeasurementUnit> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (measureUnit.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        auxObjEntity = measureUnit.OriginalValue;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    if (measureUnit.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) { measureUnit.CreationUser = audit.CodeUser; } else { measureUnit.ModificationUser = audit.CodeUser; measureUnit.ModificationDate = DateTime.Now; }
                    var resultSave = this._measureUnitRepository.SaveMeasurementUnit(measureUnit.Code, measureUnit.Name, measureUnit.Abbreviation, measureUnit.UnitType, measureUnit.Status, measureUnit.AllowEditCostValue, measureUnit.CostValue, audit.CodeUser, measureUnit.RequiresStandardCode, measureUnit.StandardCode);
                    TriggerEvent(measureUnit, audit);
                    if (resultSave.CodeMessage == 0)
                    {
                        auditProcess = new IndigoAuditSimpleEntity<InventoryMeasurementUnit>(measureUnit, audit, status, auxObjEntity);
                        auditProcess.Execute();
                        //Se marca la entidad como sin cambios
                        scope.Complete();
                        return new ActionResult<InventoryMeasurementUnit> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = measureUnit, Message = resultSave.Message };
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult<InventoryMeasurementUnit> { StateResult = false, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = measureUnit, Message = resultSave.Message };
                    }
                }
            }
            catch (IndigoValidationException ex)
            {
                return new ActionResult<InventoryMeasurementUnit> { StateResult = false, StatusCode = eStatusResult.WARNING, Message = ex.Message };
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<InventoryMeasurementUnit> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<InventoryMeasurementUnit> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina una unidad de medida
        /// </summary>
        /// <param name="measureUnit"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteMeasureUnit(InventoryMeasurementUnit measureUnit, AuditMessage audit)
        {
            if (measureUnit == null)
            {
                throw new ArgumentNullException("measureUnit");
            }

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    SP_DeleteMeasurementUnit_Result result = _measureUnitRepository.SP_DeleteMeasurementUnit(measureUnit.Id);
                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }

                    measureUnit.MarkAsDeleted();
                    TriggerEvent(measureUnit, audit);

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
        public ActionResult<InventoryMeasurementUnit> ChangeStateMeasureUnit(string code, bool state, AuditMessage audit)
        {
            // Domain.Entities.InventoryMeasurementUnit measureUnit = _measureUnitRepository.GetMeasureUnit(code);
            // measureUnit.Status = state;
            //return SaveMeasureUnit(measureUnit, audit);


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
                Domain.Entities.InventoryMeasurementUnit measureUnit = this._measureUnitRepository.GetMeasureUnit(code.Trim());
                if (measureUnit != null && measureUnit.Id > 0)
                {
                    measureUnit.Status = state;
                }
                var result = this.SaveMeasureUnit(measureUnit, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<InventoryMeasurementUnit> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }


        }

        /// <summary>
        /// Obtiene una unidad de medida por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<InventoryMeasurementUnit> GetMeasureUnit(string code, AuditMessage audit)
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
                Domain.Entities.InventoryMeasurementUnit measureUnit = _measureUnitRepository.GetMeasureUnit(code);
                return new ActionResult<Domain.Entities.InventoryMeasurementUnit> { StateResult = true, ObjectEmbbeded = measureUnit };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryMeasurementUnit> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene una unidad de medida por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>        
        public ActionResult<InventoryMeasurementUnit> GetMeasureUnitById(int idProductGroup, AuditMessage audit)
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
                Domain.Entities.InventoryMeasurementUnit measureUnit = _measureUnitRepository.GetMeasureUnitById(idProductGroup);
                return new ActionResult<Domain.Entities.InventoryMeasurementUnit> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryMeasurementUnit> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #region Events
        public void TriggerEvent(Domain.Entities.InventoryMeasurementUnit measurementUnit, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = measurementUnit.ChangeTracker.State.ToString().ToLower();
            if (measurementUnit.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(measurementUnit, audit.CodeUser, ChangeTracker, DittoSourceType.inventoryMeasurementUnit);
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
                _measureUnitRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
