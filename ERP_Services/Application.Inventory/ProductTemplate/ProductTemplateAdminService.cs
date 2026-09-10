///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

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
using Domain.Entities.Service;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Transactions;
using Domain.Crystal.Entities;

namespace Application.Inventory.ProductTemplate
{
    public class ProductTemplateAdminService : IProductTemplateAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmProductCoverage";
        private IProductTemplateRepository _productTemplateRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        public ProductTemplateAdminService(IProductTemplateRepository productTemplateRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            if ((productTemplateRepository == null))
            {
                throw new ArgumentNullException("Repositorio de productTemplateRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _productTemplateRepository = productTemplateRepository;
            _sequenseRepository = sequenseRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda un cubrimiento de producto de forma asincrona
        /// </summary>
        public async Task<ActionResult<ProductRate>> SaveProductTemplate(ProductRate productTemplate, AuditMessage audit, long idSecuence = 0, bool updateHeaderAudit = true)
        {
            if (productTemplate == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }

            IUnitWork unitOfWork = this._productTemplateRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings,TransactionScopeAsyncFlowOption.Enabled))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(productTemplate.Code))
                    {
                        InventorySequenceDetail seq = await this._sequenseRepository.GetSequenseDByIdAsync((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                productTemplate.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<ProductRate> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), productTemplate.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<ProductRate> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.ProductRate auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ProductRate> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (productTemplate.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        productTemplate.CreationUser = audit.CodeUser;
                        productTemplate.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = productTemplate.OriginalValue;
                        if (updateHeaderAudit)
                        {
                            productTemplate.ModificationDate = DateTime.Now;
                            productTemplate.ModificationUser = audit.CodeUser;
                        }
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    ////Se cambian a null los que no tienen Id
                    foreach (ProductRateGeneral item in productTemplate.ProductRateGeneral)
                    {
                        if (item.ProductSubGroupId == 0)
                        {
                            item.ProductSubGroupId = null;
                        }
                        if (item.ProductTypeId == 0)
                        {
                            item.ProductTypeId = null;
                        }
                        if (item.ProductGroupId == 0)
                        {
                            item.ProductGroupId = null;
                        }
                        
                    }

                    //Se eliminan los que tenga changetracker en delete
                    List<ProductRateGeneral> deleteList = productTemplate.ProductRateGeneral.ToList().FindAll(x => x.ChangeTracker.State == ObjectState.Deleted);

                    foreach (ProductRateGeneral item in deleteList)
                    {

                        this._productTemplateRepository.DeleteConditionsById(item.Id);
                        productTemplate.ProductRateGeneral.Remove(item);

                    }

                    this._productTemplateRepository.SaveEntity(productTemplate);
                    await unitOfWork.CommitAsync();
                    if (updateHeaderAudit)
                    {
                        auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductRate>(productTemplate, audit, status, auxObjEntity);
                        auditProcess.Execute();
                    }
                    
                    //Se marca la entidad como sin cambios
                    productTemplate.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.ProductRate> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = productTemplate, Message = MessageResult };
                }
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ProductRate> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductRate> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un cubrimiento de producto
        /// </summary>
        public ActionResult DeleteProductTemplate(Domain.Entities.ProductRate productTemplate, AuditMessage audit)
        {
            if (productTemplate == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._productTemplateRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    productTemplate.ModificationUser = audit.CodeUser;
                    productTemplate.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductRate>(productTemplate, audit, status);

                    _productTemplateRepository.DeleteDetailUpdatedById(productTemplate.Id);
                    //Se eliminan los que tenga changetracker en delete
                    _productTemplateRepository.DeleteConditionsAllById(productTemplate.Id);
                    _productTemplateRepository.DeleteEntity(productTemplate);
                    unitOfWork.Commit();
                    auditProcess.Execute();
                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (System.Data.Entity.Core.UpdateException ex)
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
        /// Actualiza el estado del cubrimiento de producto
        /// </summary>
        public async Task<ActionResult<Domain.Entities.ProductRate>> UpdateStateProductTemplate(string code, bool state, AuditMessage audit)
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
                Domain.Entities.ProductRate productTemplate = await this._productTemplateRepository.GetProductTemplate(code.Trim());
                if (productTemplate != null && productTemplate.Id > 0)
                {
                    productTemplate.Status = state;
                }
                var result = await this.SaveProductTemplate(productTemplate, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductRate> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un cubrimiento de producto por codigo
        /// </summary>
        async Task<ActionResult<ProductRate>> IProductTemplateAdminService.GetProductTemplate(string code, AuditMessage audit)
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
                Domain.Entities.ProductRate productTemplate = await _productTemplateRepository.GetProductTemplate(code);
                if (productTemplate != null && productTemplate.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.ProductRate> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductRate>(productTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.ProductRate> { StateResult = true, ObjectEmbbeded = productTemplate };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ProductRate> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un cubrimiento de producto por id
        /// </summary>
        Domain.Entities.ProductRate IProductTemplateAdminService.GetProductTemplateById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _productTemplateRepository.GetProductTemplateById(id);
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
                _productTemplateRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
