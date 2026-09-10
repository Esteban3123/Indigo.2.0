//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 17/09/2014
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

namespace Application.Inventory.OtherWithholdingDeduction
{
    public class OtherWithholdingDeductionAdminService : IOtherWithholdingDeductionAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmOtherDeductions";
        private IOtherWithholdingDeductionRepository _otherWithholdingDeductionsRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public OtherWithholdingDeductionAdminService(IOtherWithholdingDeductionRepository otherWithholdingDeductionsRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            if ((otherWithholdingDeductionsRepository == null))
            {
                throw new ArgumentNullException("Repositorio de otherWithholdingDeductionsRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _otherWithholdingDeductionsRepository = otherWithholdingDeductionsRepository;
            _sequenseRepository = sequenseRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza otras deducciones y retenciones
        /// </summary>
        /// <param name="otherWithholdingDeduction"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> SaveOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, AuditMessage audit, long idSecuence = 0)
        {   
            if (otherWithholdingDeduction == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._otherWithholdingDeductionsRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(otherWithholdingDeduction.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                otherWithholdingDeduction.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), otherWithholdingDeduction.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.OtherWithholdingDeduction auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.OtherWithholdingDeduction> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (otherWithholdingDeduction.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        otherWithholdingDeduction.CreationUser = audit.CodeUser;
                        otherWithholdingDeduction.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = otherWithholdingDeduction.OriginalValue;
                        otherWithholdingDeduction.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._otherWithholdingDeductionsRepository.SaveEntity(otherWithholdingDeduction);
                    unitOfWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.OtherWithholdingDeduction>(otherWithholdingDeduction, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    otherWithholdingDeduction.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = otherWithholdingDeduction, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina otras deducciones y retenciones
        /// </summary>
        /// <param name="otherWithholdingDeduction"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteOtherWithholdingDeduction(Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction, AuditMessage audit)
        {
            //if (otherWithholdingDeduction == null)
            //{
            //    throw new ArgumentNullException("otherWithholdingDeduction");
            //}

            //IUnitWork unitOfWork = _otherWithholdingDeductionsRepository.UnitWork;
            //try
            //{
            //    IndigoAuditSimpleEntity<Domain.Entities.OtherWithholdingDeduction> auditProcess;
            //    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.OtherWithholdingDeduction>(otherWithholdingDeduction, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
            //    _otherWithholdingDeductionsRepository.DeleteEntity(otherWithholdingDeduction);
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




            if (otherWithholdingDeduction == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._otherWithholdingDeductionsRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    otherWithholdingDeduction.ModificationUser = audit.CodeUser;
                    otherWithholdingDeduction.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.OtherWithholdingDeduction>(otherWithholdingDeduction, audit, status);

                    //while (otherWithholdingDeduction.WarehouseUser.Count > 0)
                    //{
                    //    otherWithholdingDeduction.WarehouseUser[warehouse.WarehouseUser.Count - 1].MarkAsDeleted();
                    //}
                    otherWithholdingDeduction.MarkAsDeleted();
                    _otherWithholdingDeductionsRepository.SaveEntity(otherWithholdingDeduction);
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
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> ChangeStateOtherWithholdingDeduction(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.OtherWithholdingDeduction otherWithholdingDeductions = _otherWithholdingDeductionsRepository.GetOtherWithholdingDeduction(code);
            //otherWithholdingDeductions.Status = state;
            //return SaveOtherWithholdingDeduction(otherWithholdingDeductions, audit);


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
                Domain.Entities.OtherWithholdingDeduction otherWithholdingDeductions = this._otherWithholdingDeductionsRepository.GetOtherWithholdingDeduction(code.Trim());
                if (otherWithholdingDeductions != null && otherWithholdingDeductions.Id > 0)
                {
                    otherWithholdingDeductions.Status = state;
                }
                var result = this.SaveOtherWithholdingDeduction(otherWithholdingDeductions, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene otras deducciones y retenciones por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeduction(string code, AuditMessage audit)
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
                Domain.Entities.OtherWithholdingDeduction otherWithholdingDeductions = _otherWithholdingDeductionsRepository.GetOtherWithholdingDeduction(code);
                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = true, ObjectEmbbeded = otherWithholdingDeductions };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene otras deducciones y retenciones por id
        /// </summary>
        /// <param name="idWarehouse"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.OtherWithholdingDeduction> GetOtherWithholdingDeductionById(int idWarehouse, AuditMessage audit)
        {
            if (idWarehouse == 0)
            {
                throw new ArgumentNullException("id");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.OtherWithholdingDeduction otherWithholdingDeductions = _otherWithholdingDeductionsRepository.GetOtherWithholdingDeductionById(idWarehouse);
                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.OtherWithholdingDeduction> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un listado de las deducciones y retenciones
        /// </summary>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.OtherWithholdingDeduction>> ListOtherWithholdingDeduction()
        {
            try
            {
                List<Domain.Entities.OtherWithholdingDeduction> listOtherWithholdingDeductions = _otherWithholdingDeductionsRepository.ListOtherWithholdingDeduction();
                return new ActionResult<List<Domain.Entities.OtherWithholdingDeduction>> { StateResult = true, ObjectEmbbeded = listOtherWithholdingDeductions };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.OtherWithholdingDeduction>> { StateResult = false, MessageResult = { ex.Message } };
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
                _otherWithholdingDeductionsRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
