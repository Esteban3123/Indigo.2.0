//'************************************************************
//' Assembly         : Domain.Inventory.InventoryContractTypeRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 26/12/2014
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
using Infrastructure.CrossCutting.Resources;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace Application.Inventory.InventoryContractType
{

    public class InventoryContractTypeAdminService : IInventoryContractTypeAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmInventoryContractType";
        private IInventoryContractTypeRepository _InventoryContractTypeRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        #endregion

        #region Builder
        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="inventoryContractTypeRepository"></param>
        /// <param name="sequenseRepository"></param>
        public InventoryContractTypeAdminService(IInventoryContractTypeRepository inventoryContractTypeRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            if (inventoryContractTypeRepository == null)
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _InventoryContractTypeRepository = inventoryContractTypeRepository;
            _sequenseRepository = sequenseRepository;
        }
        #endregion

        #region Methods
        public ActionResult<Domain.Entities.InventoryContractType> SaveInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, AuditMessage audit, long idSecuence = 0)
        {
            if (inventoryContractType  == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._InventoryContractTypeRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(inventoryContractType.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                inventoryContractType.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.InventoryContractType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), inventoryContractType .Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.InventoryContractType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.InventoryContractType auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryContractType> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (inventoryContractType.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        inventoryContractType.CreationUser = audit.CodeUser;
                        inventoryContractType.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = inventoryContractType.OriginalValue;
                        inventoryContractType.ModificationUser = audit.CodeUser;
                        inventoryContractType.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._InventoryContractTypeRepository.SaveEntity(inventoryContractType);
                    unitOfWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryContractType>(inventoryContractType, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    inventoryContractType.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.InventoryContractType> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = inventoryContractType , Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.InventoryContractType> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContractType> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        public ActionResult DeleteInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, AuditMessage audit)
        {
            //if (inventoryContractType == null)
            //{
            //    throw new ArgumentNullException("product");
            //}

            //IUnitWork unitOfWork = _InventoryContractTypeRepository.UnitWork;
            //try
            //{
            //    inventoryContractType.MarkAsDeleted();
            //    IndigoAuditSimpleEntity<Domain.Entities.InventoryContractType> auditProcess;
            //    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryContractType>(inventoryContractType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
            //    _InventoryContractTypeRepository.DeleteEntity(inventoryContractType);
            //    unitOfWork.Commit();
            //    auditProcess.Execute();

            //    return new ActionResult { StateResult = true };

            //}
            //catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            //{
            //    unitOfWork.RollbackChangesUnitOfWork();
            //    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            //    return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            //}
            //catch (DbUpdateException ex)
            //{
            //    unitOfWork.RollbackChangesUnitOfWork();
            //    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            //    return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            //}
            //catch (System.Data.Entity.Core.UpdateException ex)
            //{
            //    unitOfWork.RollbackChangesUnitOfWork();
            //    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            //    return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            //}
            //catch (Exception ex)
            //{
            //    unitOfWork.RollbackChangesUnitOfWork();
            //    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            //    return new ActionResult { StateResult = false, MessageResult = { ex.Message } };
            //}


            if (inventoryContractType == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._InventoryContractTypeRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    inventoryContractType.ModificationUser = audit.CodeUser;
                    inventoryContractType.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryContractType>(inventoryContractType, audit, status);

                    //while (inventoryContractType.WarehouseUser.Count > 0)
                    //{
                    //    warehouse.WarehouseUser[warehouse.WarehouseUser.Count - 1].MarkAsDeleted();
                    //}
                    inventoryContractType.MarkAsDeleted();
                    _InventoryContractTypeRepository.SaveEntity(inventoryContractType);
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

        public ActionResult<Domain.Entities.InventoryContractType> ChangeStateInventoryContractType(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.InventoryContractType inventoryContractType = _InventoryContractTypeRepository.GetInventoryContractType(code);
            //inventoryContractType.Status = state;
            //return SaveInventoryContractType(inventoryContractType, audit);


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
                Domain.Entities.InventoryContractType inventoryContractType = this._InventoryContractTypeRepository.GetInventoryContractType(code.Trim());
                if (inventoryContractType != null && inventoryContractType.Id > 0)
                {
                    inventoryContractType.Status = state;
                }
                var result = this.SaveInventoryContractType(inventoryContractType, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContractType> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        public ActionResult<Domain.Entities.InventoryContractType> GetInventoryContractType(string code, AuditMessage audit)
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
                Domain.Entities.InventoryContractType inventoryContractType = _InventoryContractTypeRepository.GetInventoryContractType(code);
                if (inventoryContractType != null && inventoryContractType.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryContractType> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryContractType>(inventoryContractType, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryContractType> { StateResult = true, ObjectEmbbeded = inventoryContractType };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContractType> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        public Domain.Entities.InventoryContractType GetInventoryContractTypeById(int idInventoryContractType)
        {
            if (idInventoryContractType == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _InventoryContractTypeRepository.GetInventoryContractTypeById(idInventoryContractType);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
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
                _InventoryContractTypeRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
