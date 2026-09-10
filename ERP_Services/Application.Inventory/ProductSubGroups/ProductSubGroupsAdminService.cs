//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 12/09/2014
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Base;
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

namespace Application.Inventory.ProductSubGroups
{
    public class ProductSubGroupsAdminService : IProductSubGroupsAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmSubGroup";
        private IProductSubGroupsRepository _productSubGroupRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public ProductSubGroupsAdminService(IProductSubGroupsRepository productSubGroupRepository, IInventorySequenceDetailRepository sequenseRepository, IPhysicalInventoryRepository physicalInventoryRepository
            , IFactoryQueue FactoryQueue)
        {
            if ((productSubGroupRepository == null))
            {
                throw new ArgumentNullException("Repositorio de productSubGroupRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if ((physicalInventoryRepository == null))
            {
                throw new ArgumentNullException("Repositorio de physicalInventoryRepository vacio");
            }
            _productSubGroupRepository = productSubGroupRepository;
            _sequenseRepository = sequenseRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _factoryQueue = FactoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza un subgrupo
        /// </summary>
        /// <param name="productSubGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<ProductSubGroup> SaveProductSubGroup(ProductSubGroup productSubGroup, AuditMessage audit, long idSecuence = 0)
        {
            if (productSubGroup == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._productSubGroupRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(productSubGroup.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                productSubGroup.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.ProductSubGroup> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), productSubGroup.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.ProductSubGroup> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.ProductSubGroup auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ProductSubGroup> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (productSubGroup.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        productSubGroup.CreationUser = audit.CodeUser;
                        productSubGroup.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = productSubGroup.OriginalValue;
                        productSubGroup.ModificationUser = audit.CodeUser;
                        productSubGroup.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    this._productSubGroupRepository.SaveEntity(productSubGroup);
                    TriggerEvent(productSubGroup, audit);
                    unitOfWork.Commit();

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductSubGroup>(productSubGroup, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    productSubGroup.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = productSubGroup, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un subgrupo
        /// </summary>
        /// <param name="productSubGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteProductSubGroup(ProductSubGroup productSubGroup, AuditMessage audit)
        {
            if (productSubGroup == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._productSubGroupRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    productSubGroup.ModificationUser = audit.CodeUser;
                    productSubGroup.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductSubGroup>(productSubGroup, audit, status);

                    productSubGroup.MarkAsDeleted();
                    _productSubGroupRepository.SaveEntity(productSubGroup);
                    unitOfWork.Commit();

                    TriggerEvent(productSubGroup, audit);

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
        public ActionResult<ProductSubGroup> ChangeStateSubGroup(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.ProductSubGroup productSubGroup = _productSubGroupRepository.GetProductSubGroup(code);
            //productSubGroup.Status = state;
            //return SaveProductSubGroup(productSubGroup, audit);


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
                Domain.Entities.ProductSubGroup productSubGroup = this._productSubGroupRepository.GetProductSubGroup(code.Trim());
                if (productSubGroup != null && productSubGroup.Id > 0)
                {
                    productSubGroup.Status = state;
                }
                var result = this.SaveProductSubGroup(productSubGroup, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un subgrupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<ProductSubGroup> GetProductSubGroup(string code, AuditMessage audit)
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
                Domain.Entities.ProductSubGroup productSubGroup = _productSubGroupRepository.GetProductSubGroup(code);
                return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = true, ObjectEmbbeded = productSubGroup };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un subgrupo por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<ProductSubGroup> GetProductSubGroupById(int idProductGroup, AuditMessage audit)
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
                Domain.Entities.ProductSubGroup productSubGroup = _productSubGroupRepository.GetProductSubGroupById(idProductGroup);
                return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = true, ObjectEmbbeded = productSubGroup };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductSubGroup> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #region Events

        public void TriggerEvent(ProductSubGroup productSubGroup, AuditMessage audit)
        {
            Events.Serializers.Wrapper wrapperEvent = new Events.Serializers.Wrapper();
            String ChangeTracker = productSubGroup.ChangeTracker.State.ToString().ToLower();
            if (productSubGroup.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(productSubGroup, audit.CodeUser, ChangeTracker, DittoSourceType.productSubGroup);
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
                _productSubGroupRepository = null;
                _sequenseRepository = null;
                _physicalInventoryRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
