//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 12/09/2014
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
using Application.Security;

namespace Application.Inventory.AdjustmentConcept
{
    public class AdjustmentConceptAdminService : IAdjustmentConceptAdminService
    {
        #region VariablesFrmConceptsInventorySettings
        private const string FORM_NAME = "FrmConceptsInventorySettings";
        private IAdjustmentConceptRepository _adjustmentConceptRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IUserAdminService _IUserAdminService;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public AdjustmentConceptAdminService(IAdjustmentConceptRepository adjustmentConceptRepository, IInventorySequenceDetailRepository sequenseRepository, IUserAdminService IUserAdminService)
        {
            if ((adjustmentConceptRepository == null))
            {
                throw new ArgumentNullException("Repositorio de adjustmentConceptRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _adjustmentConceptRepository = adjustmentConceptRepository;
            _sequenseRepository = sequenseRepository;
            _IUserAdminService = IUserAdminService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza el concepto de ajuste
        /// </summary>
        /// <param name="adjustmentConcept"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AdjustmentConcept> SaveAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, AuditMessage audit, long idSecuence = 0)
        {
            if (adjustmentConcept == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._adjustmentConceptRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(adjustmentConcept.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                adjustmentConcept.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.AdjustmentConcept> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), adjustmentConcept.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.AdjustmentConcept> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.AdjustmentConcept auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.AdjustmentConcept> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (adjustmentConcept.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        adjustmentConcept.CreationUser = audit.CodeUser;
                        adjustmentConcept.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = adjustmentConcept.OriginalValue;
                        adjustmentConcept.ModificationUser = audit.CodeUser;
                        adjustmentConcept.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._adjustmentConceptRepository.SaveEntity(adjustmentConcept);
                    unitOfWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.AdjustmentConcept>(adjustmentConcept, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    adjustmentConcept.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = adjustmentConcept, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un concepto de ajuste
        /// </summary>
        /// <param name="adjustmentConcept"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, AuditMessage audit)
        {
            //if (adjustmentConcept == null)
            //{
            //    throw new ArgumentNullException("adjustmentConcept");
            //}

            //IUnitWork unitOfWork = _adjustmentConceptRepository.UnitWork;
            //try
            //{
            //    IndigoAuditSimpleEntity<Domain.Entities.AdjustmentConcept> auditProcess;
            //    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.AdjustmentConcept>(adjustmentConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
            //    _adjustmentConceptRepository.DeleteEntity(adjustmentConcept);
            //    unitOfWork.Commit();
            //    auditProcess.Execute();

            //    return new ActionResult { StateResult = true };

            //}
            //catch (OptimisticConcurrencyException ex)
            //{
            //    unitOfWork.RollbackChanges();
            //    return new ActionResult { StateResult = false, MessageResult = new List<string> { "-999" } };
            //}
            //catch (UpdateException ex)
            //{
            //    unitOfWork.RollbackChanges();
            //    return new ActionResult { StateResult = false, MessageResult = new List<string> { "-000" } };
            //}
            //catch (Exception ex)
            //{
            //    unitOfWork.RollbackChanges();
            //    return new ActionResult { StateResult = false, MessageResult = { ex.Message } };
            //}



            if (adjustmentConcept == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._adjustmentConceptRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    adjustmentConcept.ModificationUser = audit.CodeUser;
                    adjustmentConcept.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.AdjustmentConcept>(adjustmentConcept, audit, status);

                    //while (adjustmentConcept.WarehouseUser.Count > 0)
                    //{
                    //    adjustmentConcept.WarehouseUser[adjustmentConcept.WarehouseUser.Count - 1].MarkAsDeleted();
                    //}
                    adjustmentConcept.MarkAsDeleted();
                    _adjustmentConceptRepository.SaveEntity(adjustmentConcept);
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

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AdjustmentConcept> ChangeStateAdjustmentConcept(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.AdjustmentConcept adjustmentConcept = _adjustmentConceptRepository.GetAdjustmentConcept(code);
            //adjustmentConcept.Status = state;
            //return SaveAdjustmentConcept(adjustmentConcept, audit);



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
                Domain.Entities.AdjustmentConcept adjustmentConcept = this._adjustmentConceptRepository.GetAdjustmentConcept(code.Trim());
                if (adjustmentConcept != null && adjustmentConcept.Id > 0)
                {
                    adjustmentConcept.Status = state;
                }
                var result = this.SaveAdjustmentConcept(adjustmentConcept, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un concepto de ajuste por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConcept(string code, AuditMessage audit)
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
                Domain.Entities.AdjustmentConcept adjustmentConcept = _adjustmentConceptRepository.GetAdjustmentConcept(code);

                if (adjustmentConcept.AdjustmentConceptUser != null && adjustmentConcept.AdjustmentConceptUser.Any()) {

                    List<int> ListIds = adjustmentConcept.AdjustmentConceptUser.ToList().Select(x => x.UserId).ToList();
                    //Se saca el listado de ids de usuario para enviar
                    List<Domain.Security.Entities.User> ListUsers = new List<Domain.Security.Entities.User>();
                    //Se obtiene el listado de usuarios
                    ListUsers = _IUserAdminService.ListUsersByIds(ListIds);

                    foreach (Domain.Entities.AdjustmentConceptUser Item in adjustmentConcept.AdjustmentConceptUser.ToList() ) {

                        Domain.Security.Entities.User _User = ListUsers.Where(d => d.Id == Item.UserId).FirstOrDefault();

                        Item.FullNameUser = _User.Person.Fullname;

                    }

                }

                return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = true, ObjectEmbbeded = adjustmentConcept };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un concepto de ajuste por id
        /// </summary>
        /// <param name="idAdjustmentConcept"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConceptById(int idAdjustmentConcept, AuditMessage audit)
        {
            if (idAdjustmentConcept == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.AdjustmentConcept adjustmentConcept = _adjustmentConceptRepository.GetAdjustmentConceptById(idAdjustmentConcept);
                return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = true, ObjectEmbbeded = adjustmentConcept };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AdjustmentConcept> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un concepto de ajuste por cuenta contable y centro de costo.
        /// </summary>
        /// <param name="conceptType"></param>
        /// <param name="adjustmentAccountId"></param>
        /// <param name="costCenterId"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionResult<List<Domain.Entities.AdjustmentConcept>> GetListByAdjustmentAccountCostCenterId(Byte conceptType, int adjustmentAccountId, int costCenterId, AuditMessage audit)
        {
            if (conceptType == 0)
            {
                throw new ArgumentNullException("conceptType");
            }
            if (adjustmentAccountId == 0)
            {
                throw new ArgumentNullException("adjustmentAccountId");
            }
            if (costCenterId == 0)
            {
                throw new ArgumentNullException("costCenterId");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                var adjustmentConcept = _adjustmentConceptRepository.GetListByAdjustmentAccountCostCenterId(conceptType, adjustmentAccountId, costCenterId);
                return new ActionResult<List<Domain.Entities.AdjustmentConcept>> { StateResult = true, ObjectEmbbeded = adjustmentConcept };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.AdjustmentConcept>> { StateResult = false, MessageResult = { ex.Message } };
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
                _adjustmentConceptRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;            
        }
        #endregion
    }
}
