//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 18/09/2014
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

namespace Application.Inventory.AttributeProductType
{
    public class AttributeProductTypeAdminService : IAttributeProductTypeAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmAttributeProductType";
        private IAttributeProductTypeRepository _attributeProductTypeRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public AttributeProductTypeAdminService(IAttributeProductTypeRepository attributeProductTypeRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            if ((attributeProductTypeRepository == null))
            {
                throw new ArgumentNullException("Repositorio de attributeProductTypeRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _attributeProductTypeRepository = attributeProductTypeRepository;
            _sequenseRepository = sequenseRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza un atributo
        /// </summary>
        /// <param name="attributeProductType"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AttributeProductType> SaveAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, AuditMessage audit, long idSecuence = 0)
        {
            if (attributeProductType == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._attributeProductTypeRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(attributeProductType.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                attributeProductType.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.AttributeProductType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), attributeProductType.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.AttributeProductType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.AttributeProductType auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.AttributeProductType> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (attributeProductType.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        attributeProductType.CreationUser = audit.CodeUser;
                        attributeProductType.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = attributeProductType.OriginalValue;
                        attributeProductType.ModificationUser = audit.CodeUser;
                        attributeProductType.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._attributeProductTypeRepository.SaveEntity(attributeProductType);
                    unitOfWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.AttributeProductType>(attributeProductType, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    attributeProductType.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = attributeProductType, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un atributo
        /// </summary>
        /// <param name="attributeProductType"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, AuditMessage audit)
        {
            //if (attributeProductType == null)
            //{
            //    throw new ArgumentNullException("attributeProductType");
            //}

            //IUnitWork unitOfWork = _attributeProductTypeRepository.UnitWork;
            //try
            //{
            //    attributeProductType.StartTracking();
            //    while (attributeProductType.AttributeProductTypeOptionList.Count > 0)
            //    {
            //        attributeProductType.AttributeProductTypeOptionList.ElementAt(0).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted;
            //    }
            //    attributeProductType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted;
            //    IndigoAuditSimpleEntity<Domain.Entities.AttributeProductType> auditProcess;
            //    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.AttributeProductType>(attributeProductType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
            //    _attributeProductTypeRepository.SaveEntity(attributeProductType);
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




            if (attributeProductType == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._attributeProductTypeRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    attributeProductType.ModificationUser = audit.CodeUser;
                    attributeProductType.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.AttributeProductType>(attributeProductType, audit, status);

                    while (attributeProductType.AttributeProductTypeOptionList.Count > 0)
                    {
                        attributeProductType.AttributeProductTypeOptionList[attributeProductType.AttributeProductTypeOptionList.Count - 1].MarkAsDeleted();
                    }
                    attributeProductType.MarkAsDeleted();
                    _attributeProductTypeRepository.SaveEntity(attributeProductType);
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
        /// Cambia el estado de atributo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AttributeProductType> ChangeStateAttributeProductType(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.AttributeProductType attributeProductType = _attributeProductTypeRepository.GetAttributeProductType(code);
            //attributeProductType.Status = state;
            //return SaveAttributeProductType(attributeProductType, audit);



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
                Domain.Entities.AttributeProductType attributeProductType = this._attributeProductTypeRepository.GetAttributeProductType(code.Trim());
                if (attributeProductType != null && attributeProductType.Id > 0)
                {
                    attributeProductType.Status = state;
                }
                var result = this.SaveAttributeProductType(attributeProductType, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un atributo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductType(string code, AuditMessage audit)
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
                Domain.Entities.AttributeProductType attributeProductType = _attributeProductTypeRepository.GetAttributeProductType(code);
                return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = true, ObjectEmbbeded = attributeProductType };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un atributo por id
        /// </summary>
        /// <param name="idAttributeProductType"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductTypeById(int idAttributeProductType, AuditMessage audit)
        {
            if (idAttributeProductType == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.AttributeProductType attributeProductType = _attributeProductTypeRepository.GetAttributeProductTypeById(idAttributeProductType);
                return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.AttributeProductType> { StateResult = false, MessageResult = { ex.Message } };
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
                _attributeProductTypeRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
