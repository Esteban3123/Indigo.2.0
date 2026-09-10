using System;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Resources;
using Domain.Base.Entities;
using Domain.Entities;
using System.Data;
using Application.Base;
using System.Transactions;
using System.Collections.Generic;
using Application.Inventory.Rollback;
using System.Threading.Tasks;

namespace Application.Inventory.TransferOrder
{
    public class TransferOrderAdminService : ITransferOrderAdminService
    {
        #region Variables

        private ITransferOrderRepository _transferOrderRepository;
        private IInventoryRequestRepository _InventoryRequestRepository;
        #endregion

        #region Builder

        public TransferOrderAdminService(ITransferOrderRepository transferOrderRepository, IInventoryRequestRepository InventoryRequestRepository)
        {
            _transferOrderRepository = transferOrderRepository;
            _InventoryRequestRepository = InventoryRequestRepository;
        }

        #endregion

        #region Methods

        public Domain.Entities.TransferOrder GetTransferOrderById(int id)
        {
            try
            {
                return _transferOrderRepository.GetTransferOrderById(id);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.TransferOrder();
            }
        }

        public Domain.Entities.TransferOrder GetTranferOrderByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.TransferOrder transferOrder = _transferOrderRepository.GetTransferOrderByCode(code.Trim());
                if (transferOrder != null && transferOrder.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.TransferOrder> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.TransferOrder>(transferOrder, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return transferOrder;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.TransferOrder();
            }
        }

        public ActionResult<Domain.Entities.TransferOrder> SaveTrasnferOrder(Domain.Entities.TransferOrder transferOrder, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    string transferOrderXml = transferOrder.ToXML();
                    var auditStatus = (Infrastructure.CrossCutting.Audit.Actions)Utils.GetAuditStatus(transferOrder.Id, transferOrder.Status);

                    var result = this._transferOrderRepository.SP_SaveTransferOrder(transferOrderXml, audit.CodeUser);
                    if (result.CodeResult != 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.TransferOrder> { StateResult = false, Message = result.MessageResult };
                    }

                    transferOrder.Id = result.Id.Value;
                    transferOrder.Code = result.Code;
                    
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.TransferOrder>(transferOrder, audit, auditStatus, transferOrder.OriginalValue);
                    auditProcess.Execute();

                    transferOrder.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.TransferOrder> { StateResult = true, ObjectEmbbeded = transferOrder, Message = result.MessageResult, MessageAux = result.MessageResultAux };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.TransferOrder> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.TransferOrder> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.TransferOrder> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
            }
        }


        /// <summary>
        /// Cambiar el estado del item de la solicitud
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<List<Domain.Entities.ViewListRequestDetailImport>> ChangeStateInventoryRequestDetail(List<ViewListRequestDetailImport> data)
        {            
            if (data == null || data.Count == 0) {  return new ActionResult<List<Domain.Entities.ViewListRequestDetailImport>> { StateResult = false, Message = "No se ha seleccionado ninguna solicitud" };   }

            Domain.Entities.InventoryRequest inventoryRequest = null;

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                Domain.Base.IUnitWork unitOfWork = _InventoryRequestRepository.UnitWork;
                try
                {                 
                    foreach (ViewListRequestDetailImport item in data)
                    {
                        inventoryRequest = _InventoryRequestRepository.GetInventoryRequestByCode(item.Code);
                            
                        var resRequestDetail = inventoryRequest.InventoryRequestDetail;                            
                        var resRequestDetailOther = inventoryRequest.InventoryRequestDetailOther;

                        if (item.InventoryRequestDetailType == 1 && resRequestDetail != null) //InventoryRequestDetail
                        {
                            foreach (var itemresRequestDetail in resRequestDetail)
                            {
                                if (item.Row == itemresRequestDetail.Id) { itemresRequestDetail.Status = 3; }
                            }
                        }
                        else if (item.InventoryRequestDetailType == 1 && resRequestDetailOther != null)  //InventoryRequestDetailOther
                        {
                            foreach (var itemresresRequestDetailOther in resRequestDetailOther)
                            {
                                if (item.Row == itemresresRequestDetailOther.Id) {itemresresRequestDetailOther.Status = 3;}
                            }
                        }
                    }
                    _InventoryRequestRepository.SaveEntity(inventoryRequest);
                    unitOfWork.Commit();
                    scope.Complete();                
                    return new ActionResult<List<Domain.Entities.ViewListRequestDetailImport>> { StateResult = true, Message = "Se ha actualizado correctamente" };
                }
                catch (Exception ex)
                {
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<List<Domain.Entities.ViewListRequestDetailImport>> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }                
            }
        }

        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _transferOrderRepository.CascadeRollback(value);
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

                _transferOrderRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion
    }
}
