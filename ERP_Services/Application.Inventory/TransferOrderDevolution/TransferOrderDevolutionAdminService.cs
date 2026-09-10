using System;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Resources;
using Domain.Base.Entities;
using Domain.Entities;
using System.Data;
using Application.Base;
using System.Transactions;
using System.Threading.Tasks;

namespace Application.Inventory.TransferOrderDevolution
{
    public class TransferOrderDevolutionAdminService : ITransferOrderDevolutionAdminService
    {

        #region Variables

        private ITransferOrderDevolutionRepository _transferOrderDevolutionRepository;

        #endregion

        #region Builder

        public TransferOrderDevolutionAdminService(ITransferOrderDevolutionRepository transferOrderDevolutionRepository)
        {
            _transferOrderDevolutionRepository = transferOrderDevolutionRepository;
        }

        #endregion

        #region Methods

        public Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionById(int id, AuditMessage audit)
        {
            try
            {
                Domain.Entities.TransferOrderDevolution transferOrderDevolution = _transferOrderDevolutionRepository.GetTransferOrderDevolutionById(id);
                if (transferOrderDevolution != null && transferOrderDevolution.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.TransferOrderDevolution> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.TransferOrderDevolution>(transferOrderDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return transferOrderDevolution;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.TransferOrderDevolution();
            }
        }

        public Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.TransferOrderDevolution transferOrderDevolution = _transferOrderDevolutionRepository.GetTransferOrderDevolutionByCode(code.Trim());
                if (transferOrderDevolution != null && transferOrderDevolution.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.TransferOrderDevolution> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.TransferOrderDevolution>(transferOrderDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return transferOrderDevolution;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.TransferOrderDevolution();
            }
        }

        public ActionResult<Domain.Entities.TransferOrderDevolution> SaveTransferOrderDevolution(Domain.Entities.TransferOrderDevolution transferOrderDevolution, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    string transferOrderDevolutionXml = transferOrderDevolution.ToXML();
                    var auditStatus = (Infrastructure.CrossCutting.Audit.Actions)Utils.GetAuditStatus(transferOrderDevolution.Id, transferOrderDevolution.Status);

                    var result = this._transferOrderDevolutionRepository.SP_SaveTransferOrderDevolution(transferOrderDevolutionXml, audit.CodeUser);
                    if (result.CodeResult != 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.TransferOrderDevolution> { StateResult = false, Message = result.MessageResult };
                    }

                    transferOrderDevolution.Id = result.Id.Value;
                    transferOrderDevolution.Code = result.Code;

                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.TransferOrderDevolution>(transferOrderDevolution, audit, auditStatus, transferOrderDevolution.OriginalValue);
                    auditProcess.Execute();

                    transferOrderDevolution.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.TransferOrderDevolution> { StateResult = true, ObjectEmbbeded = transferOrderDevolution, Message = result.MessageResult, MessageAux = result.MessageResultAux };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.TransferOrderDevolution> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.TransferOrderDevolution> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.TransferOrderDevolution> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
            }
        }

        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _transferOrderDevolutionRepository.CascadeRollback(value);
            return await Task.FromResult(res);
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

                _transferOrderDevolutionRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion

    }
}
