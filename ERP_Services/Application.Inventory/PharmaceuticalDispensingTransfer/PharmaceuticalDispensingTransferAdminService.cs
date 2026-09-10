using System;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Resources;
using Domain.Base.Entities;
using Domain.Entities;
using System.Data;
using Application.Base;
using System.Transactions;

namespace Application.Inventory.PharmaceuticalDispensingTransfer
{
    public class PharmaceuticalDispensingTransferAdminService : IPharmaceuticalDispensingTransferAdminService
    {
        #region Variables

        private IPharmaceuticalDispensingTransferRepository _PharmaceuticalDispensingTransferRepository;

        #endregion

        #region Builder

        public PharmaceuticalDispensingTransferAdminService(IPharmaceuticalDispensingTransferRepository PharmaceuticalDispensingTransferRepository)
        {
            _PharmaceuticalDispensingTransferRepository = PharmaceuticalDispensingTransferRepository;
        }

        #endregion

        #region Methods

        public Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferById(int id)
        {
            try
            {
                return _PharmaceuticalDispensingTransferRepository.GetPharmaceuticalDispensingTransferById(id);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PharmaceuticalDispensingTransfer();
            }
        }

        public Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.PharmaceuticalDispensingTransfer PharmaceuticalDispensingTransfer = _PharmaceuticalDispensingTransferRepository.GetPharmaceuticalDispensingTransferByCode(code.Trim());
                if (PharmaceuticalDispensingTransfer != null && PharmaceuticalDispensingTransfer.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensingTransfer> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensingTransfer>(PharmaceuticalDispensingTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return PharmaceuticalDispensingTransfer;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PharmaceuticalDispensingTransfer();
            }
        }

        public ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> SavePharmaceuticalDispensingTransfer(Domain.Entities.PharmaceuticalDispensingTransfer pharmaceuticalDispensingTransfer, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    string PharmaceuticalDispensingTransferXml = pharmaceuticalDispensingTransfer.ToXML();
                    var auditStatus = (Infrastructure.CrossCutting.Audit.Actions)Utils.GetAuditStatus(pharmaceuticalDispensingTransfer.Id, pharmaceuticalDispensingTransfer.Status);

                    var result = this._PharmaceuticalDispensingTransferRepository.SP_SavePharmaceuticalDispensingTransfer(PharmaceuticalDispensingTransferXml, audit.CodeUser);
                    if (result.CodeResult != 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> { StateResult = false, Message = result.MessageResult };
                    }

                    pharmaceuticalDispensingTransfer.Id = result.Id.Value;
                    pharmaceuticalDispensingTransfer.Code = result.Code;
                    
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PharmaceuticalDispensingTransfer>(pharmaceuticalDispensingTransfer, audit, auditStatus, pharmaceuticalDispensingTransfer.OriginalValue);
                    auditProcess.Execute();

                    pharmaceuticalDispensingTransfer.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> { StateResult = true, ObjectEmbbeded = pharmaceuticalDispensingTransfer, Message = result.MessageResult };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
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

                _PharmaceuticalDispensingTransferRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion
    }
}
