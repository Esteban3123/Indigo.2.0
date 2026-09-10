///************************************************************
/// Assembly         : Application.Inventory
/// Author           : Daniel Eduardo Arévalo Bonilla
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Resources;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Base;
using System.Transactions;


namespace Application.Inventory.PurchaseOrderDevolution
{
    public class PurchaseOrderDevolutionAdminService : IPurchaseOrderDevolutionAdminService
    {
        private IPurchaseOrderDevolutionRepository _purchaseOrderDevolutionRepository;
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;

        public PurchaseOrderDevolutionAdminService(IPurchaseOrderDevolutionRepository purchaseOrderDevolutionRepository, IInventorySequenceDetailRepository sequenseRepository,
            IPurchaseOrderDetailRepository purchaseOrderDetailRepository)
        {
            _purchaseOrderDevolutionRepository = purchaseOrderDevolutionRepository;
            _sequenseRepository = sequenseRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        }

        public List<PurchaseOrderDevolutionDetail> GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(int PurchaseOrderDevolutionId)
        {
            try
            {
                return _purchaseOrderDevolutionRepository.GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(PurchaseOrderDevolutionId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<PurchaseOrderDevolutionDetail>();
            }
        }

        public Domain.Entities.PurchaseOrderDevolution GetPurchaseOrderDevolutionByCode(string Code)
        {
            try
            {
                return _purchaseOrderDevolutionRepository.GetPurchaseOrderDevolutionByCode(Code);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PurchaseOrderDevolution();
            }
        }

        public Domain.Entities.PurchaseOrderDevolution GetPurchaseOrderDevolutionById(int Id)
        {
            try
            {
                return _purchaseOrderDevolutionRepository.GetPurchaseOrderDevolutionById(Id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PurchaseOrderDevolution();
            }
        }

        public ActionResult<Domain.Entities.PurchaseOrderDevolution> SavePurchaseOrderDevolution(Domain.Entities.PurchaseOrderDevolution PurchaseOrderDevolution, AuditMessage audit, long idSecuence = 0)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    InventorySequenceDetail seq = null;
                    if (PurchaseOrderDevolution.Code == null || PurchaseOrderDevolution.Code.Trim().Equals(string.Empty))
                    {
                        seq = this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence));
                        if (seq != null && seq.Id > 0 && seq.InventorySequence.Sequential)
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                PurchaseOrderDevolution.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                _sequenseRepository.UnitWork.Commit();
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.PurchaseOrderDevolution> { StatusCode = eStatusResult.WARNING, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.PurchaseOrderDevolution> { StatusCode = eStatusResult.WARNING, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }

                    Domain.Entities.PurchaseOrderDevolution auxPurchaseOrderDevolution = null;
                    IndigoAuditSimpleEntity<Domain.Entities.PurchaseOrderDevolution> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (PurchaseOrderDevolution.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        PurchaseOrderDevolution.CreationUser = audit.CodeUser;
                        PurchaseOrderDevolution.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;

                    }
                    else
                    {
                        auxPurchaseOrderDevolution = PurchaseOrderDevolution.OriginalValue;
                        PurchaseOrderDevolution.ModificationUser = audit.CodeUser;
                        PurchaseOrderDevolution.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    // Si se confirma la devolucion de la Orden de Compra
                    if (PurchaseOrderDevolution.Status == 2)
                    {
                        PurchaseOrderDevolution.ConfirmationDate = DateTime.Now;
                        PurchaseOrderDevolution.ConfirmationUser = audit.CodeUser;
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm;
                        var errors = new StringBuilder();
                        foreach (var item in PurchaseOrderDevolution.PurchaseOrderDevolutionDetail.Where(x => x.ChangeTracker.State != ObjectState.Deleted))
                        {
                            var detail = _purchaseOrderDetailRepository.GetPurchaseOrderDetailById(item.PurchaseOrderDetailId);
                            if (detail != null)
                            {
                                if (detail.OutstandingQuantity == 0)
                                {
                                    errors.AppendLine("El item con cantidad a devolver " + item.Quantity.ToString() + " de la orden de compra " + item.PurchaseCode + " no tiene cantidad pendiente por devolver");
                                    continue;
                                }
                                detail.CancelledQuantity += item.Quantity;
                                detail.OutstandingQuantity -= item.Quantity;
                                detail.MarkAsModified();
                                _purchaseOrderDetailRepository.SaveEntity(detail);
                                _purchaseOrderDevolutionRepository.UnitWork.Commit();
                            }
                        }
                        if (errors.Length > 0)
                        {
                            return new ActionResult<Domain.Entities.PurchaseOrderDevolution> { StatusCode = eStatusResult.WARNING, Message = errors.ToString() };
                        }
                    }

                    // Si se anula la devolucion de la Orden de Compra
                    if (PurchaseOrderDevolution.Status == 3)
                    {
                        PurchaseOrderDevolution.AnnulmentDate = DateTime.Now;
                        PurchaseOrderDevolution.AnnulmentUser = audit.CodeUser;
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular;
                    }
                    _purchaseOrderDevolutionRepository.SaveEntity(PurchaseOrderDevolution);
                    _purchaseOrderDevolutionRepository.UnitWork.Commit();
                    
                    // unitOfWorkControlDocuments.Commit(); 
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PurchaseOrderDevolution>(PurchaseOrderDevolution, audit, status, auxPurchaseOrderDevolution);
                    auditProcess.Execute();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.PurchaseOrderDevolution> { StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = PurchaseOrderDevolution };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    _purchaseOrderDevolutionRepository.UnitWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PurchaseOrderDevolution> { StatusCode = eStatusResult.EXCEPTION, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    _purchaseOrderDevolutionRepository.UnitWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PurchaseOrderDevolution> { StatusCode = eStatusResult.EXCEPTION, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    _purchaseOrderDevolutionRepository.UnitWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PurchaseOrderDevolution> { StatusCode = eStatusResult.EXCEPTION, Message = ex.Message.ToString() };
                }
            }
            
        }

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
                _purchaseOrderDevolutionRepository = null;
                _sequenseRepository = null;
                _purchaseOrderDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
