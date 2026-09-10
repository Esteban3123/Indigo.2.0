//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Andrés Steven Rojas Rodríguez
//' Created          : 29/02/2024
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

namespace Application.Inventory.UPRUnits
{
    public class UPRUnitsAdminService : IUPRUnitsAdminService
    {
        #region VariablesFrmConceptsInventorySettings
        private const string FORM_NAME = "FrmUPRUnits";
        private IUPRUnitsRepository _uPRUnitsRepository;
        #endregion

        #region Builder
        public UPRUnitsAdminService(IUPRUnitsRepository uPRUnitsRepository, IInventorySequenceDetailRepository sequenseRepository, IUserAdminService userAdminService)
        {
            if ((uPRUnitsRepository == null))
            {
                throw new ArgumentNullException("Repositorio de UPRUnitsRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _uPRUnitsRepository = uPRUnitsRepository;
        }
        #endregion

        #region Methods
        public ActionResult<Domain.Entities.UPRUnits> ChangeStateUPRUnits(string code, bool state, AuditMessage audit)
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
                Domain.Entities.UPRUnits uPRUnits = this._uPRUnitsRepository.getUPRUnits(code.Trim());
                if (uPRUnits != null && uPRUnits.Id > 0)
                {
                    uPRUnits.Status = state;
                }
                var result = this.SaveUPRUnits(uPRUnits, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.UPRUnits> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }
                

        public ActionResult<Domain.Entities.UPRUnits> GetUPRUnits(string code, AuditMessage audit)
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
                Domain.Entities.UPRUnits uPRUnits = _uPRUnitsRepository.getUPRUnits(code);
            
                return new ActionResult<Domain.Entities.UPRUnits> { StateResult = true, ObjectEmbbeded = uPRUnits };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.UPRUnits> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        public ActionResult<Domain.Entities.UPRUnits> GetUPRUnitsById(int id, AuditMessage audit)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.UPRUnits PharmaceuticalForm = _uPRUnitsRepository.getUPRUnitsById(id);
                return new ActionResult<Domain.Entities.UPRUnits> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.UPRUnits> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        public ActionResult<Domain.Entities.UPRUnits> SaveUPRUnits(Domain.Entities.UPRUnits uPRUnits, AuditMessage audit, long idSecuence = 0)
        {
            if (uPRUnits == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._uPRUnitsRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;

                    Domain.Entities.UPRUnits auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.UPRUnits> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (uPRUnits.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                        uPRUnits.CreationUser = audit.CodeUser;
                        uPRUnits.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = uPRUnits.OriginalValue;
                        uPRUnits.ModificationUser = audit.CodeUser;
                        uPRUnits.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._uPRUnitsRepository.SaveEntity(uPRUnits);
                    unitOfWork.Commit();

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.UPRUnits>(uPRUnits, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    uPRUnits.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.UPRUnits> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = uPRUnits, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.UPRUnits> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.UPRUnits> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
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
                _uPRUnitsRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
