//'************************************************************
//' Assembly         : Application.Inventory.PharmaceuticalFormGrouping
//' Author           : Cesar Augusto Collazos
//' Created          : 29/02/2024
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Base;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace Application.Inventory.PharmaceuticalFormGrouping
{
    public class PharmaceuticalFormGroupingAdminService : IPharmaceuticalFormGroupingAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmPharmaceuticalFormGrouping";
        private IPharmaceuticalFormGroupingRepository _pharmaceuticalFormGroupingRepository;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de farmacia
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public PharmaceuticalFormGroupingAdminService(IPharmaceuticalFormGroupingRepository pharmaceuticalFormGroupingRepository)
        {
            if ((pharmaceuticalFormGroupingRepository == null))
            { 
                throw new ArgumentNullException("Repositorio de pharmaceuticalFormGroupingRepository vacio");
            }
            _pharmaceuticalFormGroupingRepository = pharmaceuticalFormGroupingRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza una forma farmacéutica
        /// </summary>
        /// <param name="PharmaceuticalForm"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalFormGrouping> SavePharmaceuticalFormGrouping(Domain.Entities.PharmaceuticalFormGrouping PharmaceuticalForm, AuditMessage audit)
        {
            if (PharmaceuticalForm == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._pharmaceuticalFormGroupingRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;

                    Domain.Entities.PharmaceuticalFormGrouping auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalFormGrouping> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (PharmaceuticalForm.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                        PharmaceuticalForm.CreationUser = audit.CodeUser;
                        PharmaceuticalForm.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = PharmaceuticalForm.OriginalValue;
                        PharmaceuticalForm.ModificationUser = audit.CodeUser;
                        PharmaceuticalForm.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._pharmaceuticalFormGroupingRepository.SaveEntity(PharmaceuticalForm);
                    unitOfWork.Commit();

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalFormGrouping>(PharmaceuticalForm, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    PharmaceuticalForm.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = PharmaceuticalForm, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Cambia el estado de una forma farmacéutica
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalFormGrouping> ChangeStatePharmaceuticalFormGrouping(string code, bool state, AuditMessage audit)
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
                Domain.Entities.PharmaceuticalFormGrouping PharmaceuticalFormGrouping = this._pharmaceuticalFormGroupingRepository.GetPharmaceuticalFormGrouping(code.Trim());
                if (PharmaceuticalFormGrouping != null && PharmaceuticalFormGrouping.Id > 0)
                {
                    PharmaceuticalFormGrouping.Status = state;
                }
                var result = this.SavePharmaceuticalFormGrouping(PharmaceuticalFormGrouping, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene una forma farmacéutica por código
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalFormGrouping> GetPharmaceuticalFormGrouping(string code, AuditMessage audit)
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
                Domain.Entities.PharmaceuticalFormGrouping PharmaceuticalForm = _pharmaceuticalFormGroupingRepository.GetPharmaceuticalFormGrouping(code);
                return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = true, ObjectEmbbeded = PharmaceuticalForm };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene una forma farmaceutica por id
        /// </summary>
        /// <param name="idPharmaceuticalForm"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PharmaceuticalFormGrouping> GetPharmaceuticalFormGroupingById(int idPharmaceuticalForm, AuditMessage audit)
        {
            if (idPharmaceuticalForm == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.PharmaceuticalFormGrouping PharmaceuticalForm = _pharmaceuticalFormGroupingRepository.GetPharmaceuticalFormGroupingById(idPharmaceuticalForm);
                return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PharmaceuticalFormGrouping> { StateResult = false, MessageResult = { ex.Message } };
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
                _pharmaceuticalFormGroupingRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
