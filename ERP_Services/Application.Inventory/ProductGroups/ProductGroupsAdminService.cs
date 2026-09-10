//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 12/09/2014
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Base;
using Application.Events.Serializers;
using Application.Inventory.ProductGroup;
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

namespace Application.Inventory.ProductGroups
{
    public class ProductGroupsAdminService : IProductGroupsAdminService
    {

        #region Variables
        private const string FORM_NAME = "FrmGroup";
        private IProductGroupsRepository _productGroupRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public ProductGroupsAdminService(IProductGroupsRepository productGroupsRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if ((productGroupsRepository == null))
            {
                throw new ArgumentNullException("Repositorio de productGroupsRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _productGroupRepository = productGroupsRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ProductGroup> SaveProductGroup(Domain.Entities.ProductGroup productGroup, AuditMessage audit, Int64 idSecuence = 0)
        {
            if (productGroup == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._productGroupRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {

                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(productGroup.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                productGroup.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.ProductGroup> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), productGroup.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.ProductGroup> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.ProductGroup auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ProductGroup> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (productGroup.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        productGroup.CreationUser = audit.CodeUser;
                        productGroup.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = productGroup.OriginalValue;
                        productGroup.ModificationUser = audit.CodeUser;
                        productGroup.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    this._productGroupRepository.SaveEntity(productGroup);
                    unitOfWork.Commit();
                
                    sequenseUnitOfWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductGroup>(productGroup, audit, status, auxObjEntity);
                    auditProcess.Execute();

                    TriggerEvent(productGroup, audit);

                    //Se marca la entidad como sin cambios
                    productGroup.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.ProductGroup> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = productGroup, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ProductGroup> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                string Message = string.Empty;
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message != string.Empty)
                {
                    Message = ex.InnerException.InnerException.Message;
                }
                else
                {
                    Message = IndigoManagementExceptions.GetExceptionDetails(ex);
                };
                return new ActionResult<Domain.Entities.ProductGroup> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = new List<string> { ex.Message }, Message = Message };
            }

        }

        /// <summary>
        /// Elimina un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteProductGroup(Domain.Entities.ProductGroup productGroup, AuditMessage audit)
        {
            if (productGroup == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._productGroupRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    productGroup.ModificationUser = audit.CodeUser;
                    productGroup.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductGroup>(productGroup, audit, status);

                    productGroup.MarkAsDeleted();
                    _productGroupRepository.SaveEntity(productGroup);
                    unitOfWork.Commit();

                    TriggerEvent(productGroup, audit);

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
        /// Cambia el estado del grupo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ProductGroup> ChangeState(string code, bool state, AuditMessage audit)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.ProductGroup productGroup = this._productGroupRepository.GetProductGroup(code.Trim());
                if (productGroup != null && productGroup.Id > 0)
                {
                    productGroup.Status = state;
                }
                var result = this.SaveProductGroup(productGroup, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductGroup> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un grupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ProductGroup> GetProductGroup(string code, AuditMessage audit)
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
                Domain.Entities.ProductGroup productGroup = _productGroupRepository.GetProductGroup(code);
                return new ActionResult<Domain.Entities.ProductGroup> { StateResult = true, ObjectEmbbeded = productGroup };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductGroup> { StateResult = false, MessageResult = { ex.Message } };
            }

        }

        /// <summary>
        /// Obtiene un grupo por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ProductGroup> GetProductGroupById(int idProductGroup, AuditMessage audit)
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
                Domain.Entities.ProductGroup productGroup = _productGroupRepository.GetProductGroupById(idProductGroup);
                return new ActionResult<Domain.Entities.ProductGroup> { StateResult = true, ObjectEmbbeded = productGroup };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductGroup> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #region Events

        public void TriggerEvent(Domain.Entities.ProductGroup productGroup, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = productGroup.ChangeTracker.State.ToString().ToLower();
            if (productGroup.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(productGroup, audit.CodeUser, ChangeTracker, DittoSourceType.productGroup);
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
                _productGroupRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
