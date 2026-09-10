//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Juan Carlos Bermudez Gutierrez
//' Created          : 09/04/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;

using Application.Base;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Application.Inventory.ProductSubGroups;
using Domain.Entities;
using System.Data;
using System.Transactions;
using Infrastructure.CrossCutting.Resources;
using Infrastructure.CrossCutting.Queue;
using Application.Events.Serializers;

namespace Application.Inventory.PackagingUnit
{
    public class PackagingUnitAdminService : IPackagingUnitAdminService
    {

        #region Variables
        private const string FORM_NAME = "FrmPackagingUnit";
        private IPackagingUnitRepository _packagingUnitRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de unidad de paquete
        /// </summary>
        /// <param name="measureUnitRepository">Repositorio de unidad de paquete</param>
        /// <remarks></remarks>
        public PackagingUnitAdminService(IPackagingUnitRepository packagingUnitRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue FactoryQueue)
        {
            if ((packagingUnitRepository == null))
            {
                throw new ArgumentNullException("Repositorio de PackagingUnitRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _packagingUnitRepository = packagingUnitRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = FactoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// guarda o actualiza una entidad de unidades de paquete
        /// </summary>
        /// <param name="packagingUnit"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PackagingUnit> SavePackagingUnit(Domain.Entities.PackagingUnit packagingUnit, AuditMessage audit, long idSecuence = 0)
        {
            if (packagingUnit == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._packagingUnitRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(packagingUnit.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                packagingUnit.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.PackagingUnit> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), packagingUnit.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.PackagingUnit> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.PackagingUnit auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.PackagingUnit> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (packagingUnit.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        packagingUnit.CreationUser = audit.CodeUser;
                        packagingUnit.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = packagingUnit.OriginalValue;
                        packagingUnit.ModificationUser = audit.CodeUser;
                        packagingUnit.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._packagingUnitRepository.SaveEntity(packagingUnit);
                    TriggerEvent(packagingUnit, audit);
                    unitOfWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PackagingUnit>(packagingUnit, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    packagingUnit.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = packagingUnit, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina una unidad de paquete
        /// </summary>
        /// <param name="packagingUnit"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeletePackagingUnit(Domain.Entities.PackagingUnit packagingUnit, AuditMessage audit)
        {
            if (packagingUnit == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._packagingUnitRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    packagingUnit.ModificationUser = audit.CodeUser;
                    packagingUnit.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PackagingUnit>(packagingUnit, audit, status);
                    packagingUnit.MarkAsDeleted();
                    _packagingUnitRepository.SaveEntity(packagingUnit);
                    unitOfWork.Commit();

                    TriggerEvent(packagingUnit, audit);

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
        public ActionResult<Domain.Entities.PackagingUnit> ChangeStatePackagingUnit(string code, bool state, AuditMessage audit)
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
                Domain.Entities.PackagingUnit packagingUnit = this._packagingUnitRepository.GetPackagingUnitByCode(code.Trim());
                if (packagingUnit != null && packagingUnit.Id > 0)
                {
                    packagingUnit.Status = state;
                }
                var result = this.SavePackagingUnit(packagingUnit, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene una unidad de paquete por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PackagingUnit> GetPackagingUnitByCode(string code, AuditMessage audit)
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
                Domain.Entities.PackagingUnit packagingUnit = _packagingUnitRepository.GetPackagingUnitByCode(code);
                return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = true, ObjectEmbbeded = packagingUnit };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene una unidad de paquete por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PackagingUnit> GetPackagingUnitById(int id, AuditMessage audit)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("id");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.PackagingUnit packagingUnit = _packagingUnitRepository.GetPackagingUnitById(id);
                return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PackagingUnit> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion
        #region Events
        public void TriggerEvent(Domain.Entities.PackagingUnit packagingUnit, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            string ChangeTracker = packagingUnit.ChangeTracker.State.ToString().ToLower();
            if (packagingUnit.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(packagingUnit, audit.CodeUser, ChangeTracker, DittoSourceType.packagingUnit);
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
                _packagingUnitRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
