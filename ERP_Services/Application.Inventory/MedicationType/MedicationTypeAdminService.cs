using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Application.Base;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.CrossCutting.Resources;

namespace Application.Inventory.MedicationType
{
    public class MedicationTypeAdminService :IMedicationTypeAdminService
    {
        #region Variables
        private IMedicationTypeRepository _medicationTypeRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        private const string FORM_NAME = "FrmMedicationType";
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de tipo de medicamentos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public MedicationTypeAdminService(IMedicationTypeRepository medicationTypeRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((medicationTypeRepository == null))
            {
                throw new ArgumentNullException("Repositorio de medicationTypeRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _medicationTypeRepository = medicationTypeRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods


        /// <summary>
        /// Obtiene registro por código
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.MedicationType> GetMedicationTypeByCode(string code, AuditMessage audit)
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
                Domain.Entities.MedicationType medicationType = _medicationTypeRepository.GetMedicationTypeByCode(code);
                return new ActionResult<Domain.Entities.MedicationType> { StateResult = true, ObjectEmbbeded = medicationType };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.MedicationType> { StateResult = false, MessageResult = { ex.Message } };
            }
        }


        /// <summary>
        /// Guarda un nuevo tipo de medicamento.
        /// </summary>
        /// <param name="medicationType"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.MedicationType> SaveMedicationType(Domain.Entities.MedicationType medicationType, AuditMessage audit, long idSecuence = 0)
        {
            if (medicationType == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._medicationTypeRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {

                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(medicationType.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                medicationType.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.MedicationType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), medicationType.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.MedicationType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    Domain.Entities.MedicationType auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.MedicationType> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (medicationType.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        medicationType.CreationUser = audit.CodeUser;
                        medicationType.CreationDate = DateTime.Now;
                    }
                    else
                    {
                        auxObjEntity = medicationType.OriginalValue;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        medicationType.ModificationUser = audit.CodeUser;
                        medicationType.ModificationDate = DateTime.Now;
                    }

                    this._medicationTypeRepository.SaveEntity(medicationType);
                    unitOfWork.Commit();
                    sequenseUnitOfWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.MedicationType>(medicationType, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    medicationType.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.MedicationType> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = medicationType, Message = MessageResult };
                   
                }
            }
            catch (IndigoValidationException ex)
            {
                return new ActionResult<Domain.Entities.MedicationType> { StateResult = false, StatusCode = eStatusResult.WARNING, Message = ex.Message };
            }
            catch (OptimisticConcurrencyException)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.MedicationType> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.MedicationType> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }


        /// <summary>
        ///  Elimina un tipo de medicamento.
        /// </summary>
        /// <param name="medicationType"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteMedicationType(Domain.Entities.MedicationType medicationType, AuditMessage audit)
        {
            if (medicationType == null)
            {
                throw new ArgumentNullException("measureUnit");
            }
            IUnitWork unitOfWork = this._medicationTypeRepository.UnitWork;

            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    medicationType.ModificationUser = audit.CodeUser;
                    medicationType.ModificationDate = DateTime.Now;

                    var status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.MedicationType>(medicationType, audit, status);

                    medicationType.MarkAsDeleted();
                    this._medicationTypeRepository.SaveEntity(medicationType);
                    unitOfWork.Commit();
                    auditProcess.Execute();
                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult
                {
                    StateResult = false,
                    StatusCode = eStatusResult.EXCEPTION,
                    Message = IndigoManagementExceptions.GetExceptionDetails(ex)
                };
            }

        }


        /// <summary>
        ///  Cambia el estado de un tipo de medicamento.
        /// </summary>
        /// <param name="medicationType"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.MedicationType> ChangeStateMedicationType(Domain.Entities.MedicationType medicationType, bool state, AuditMessage audit)
        {
            if (medicationType == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }

            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                if (medicationType != null && medicationType.Id > 0)
                {
                    medicationType.Status = state;
                }

                var result = SaveMedicationType(medicationType, audit);

                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = ResourceManager.get_GetString("UpdateState");
                }

                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.MedicationType>
                {
                    StateResult = false,
                    StatusCode = eStatusResult.EXCEPTION,
                    Message = IndigoManagementExceptions.GetExceptionDetails(ex)
                };
            }
        }

        #endregion


        #region IDisposable 
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
                _medicationTypeRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
