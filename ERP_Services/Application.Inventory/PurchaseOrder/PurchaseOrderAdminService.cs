///************************************************************
/// Assembly         : Application.Inventory
/// Author           : Daniel Eduardo Arévalo Bonilla
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

#region Imports

using Application.Base;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Domain.Entities.Service;
using Infrastructure.CrossCutting.Audit;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

#endregion Imports

namespace Application.Inventory.PurchaseOrder
{
    public class PurchaseOrderAdminService : IPurchaseOrderAdminService
    {
        #region Variables

        private IPurchaseOrderRepository _purchaseOrderRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        // private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IInventoryService _inventoryService;

        #endregion Variables

        #region Builder

        public PurchaseOrderAdminService(IPurchaseOrderRepository purchaseOrderRepository, IInventorySequenceDetailRepository sequenseRepository, 
            IInventoryControlDocumentRepository InventoryControlDocumentRepository, IWarehouseRepository warehouseRepository,
            IInventoryService inventoryService)
        {
            if (purchaseOrderRepository == null)
            {
                throw new ArgumentNullException("purchaseOrderRepository");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            //if (InventoryControlDocumentRepository == null)
            //{
            //    throw new ArgumentNullException("InventoryControlDocumentRepository");
            //}
            _inventoryService = inventoryService;
            _purchaseOrderRepository = purchaseOrderRepository;
            _sequenseRepository = sequenseRepository;
            // _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
        }

        #endregion Builder

        #region Methods

        public Domain.Entities.PurchaseOrder GetPurchaseOrderByCode(String Code)
        {
            try
            {
                return _purchaseOrderRepository.GetPurchaseOrderByCode(Code);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PurchaseOrder();
            }
        }

        public Domain.Entities.PurchaseOrder GetPurchaseOrderById(int Id)
        {
            try
            {
                return _purchaseOrderRepository.GetPurchaseOrderById(Id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PurchaseOrder();
            }
        }

        /// <summary>
        /// Guarda o actualiza una Orden de Compra
        /// </summary>
        /// <param name="PurchaseOrder"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PurchaseOrder> SavePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, AuditMessage audit, long idSecuence = 0, InventorySequence sequenceC = null)
        {
            if (PurchaseOrder == null)
            {
                throw new ArgumentNullException("PurchaseOrder");
            }

            if (PurchaseOrder.OrderType == 1)
            {
                if (PurchaseOrder.PurchaseOrderDetail == null || PurchaseOrder.PurchaseOrderDetail.Count() == 0)
                {
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = "La orden de compra no tiene detalles" };
                }

                if (PurchaseOrder.PurchaseOrderDetail.Where(i => i.Quantity <= 0).Count() > 0)
                {
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = "La orden de compra tiene detalles con cantidades iguales o inferiores a 0" };
                }
            }
            else if (PurchaseOrder.OrderType == 2)
            {
                if (PurchaseOrder.TotalValue <= 0)
                {
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = "Debe establecer un Valor del Servicio" };
                }
            }
            else
            {
                return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = "El tipo de orden no es válido" };
            }
            
            StringBuilder parameter = new StringBuilder();
            IUnitWork unitOfWork = _purchaseOrderRepository.UnitWork;
            IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
            // IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    if (PurchaseOrder.Code == null || PurchaseOrder.Code.Trim().Equals(string.Empty))
                    {
                        InventorySequenceDetail seq = (idSecuence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence)));
                        if (PurchaseOrder.Code == null || PurchaseOrder.Code.Trim().Equals(string.Empty))
                        {
                            if (seq != null)
                            {
                                if (seq.Id == 0)
                                {
                                    seq.IdSequense = sequenceC.IdSequence.Value;
                                    seq.InventorySequenceId = sequenceC.Id;
                                    seq.Next = 1;
                                    seq.Prefix = PurchaseOrder.Prefix;
                                }
                                var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(PurchaseOrder.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                                if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                                {
                                    PurchaseOrder.Code = res;
                                    seq.Next += 1;
                                    this._sequenseRepository.SaveEntity(seq);
                                }
                                else
                                {
                                    transaction.Dispose();
                                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                                }
                            }
                            else
                            {
                                transaction.Dispose();
                                return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                    }

                    Domain.Entities.PurchaseOrder auxPurchaseOrder = null;
                    IndigoAuditSimpleEntity<Domain.Entities.PurchaseOrder> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (PurchaseOrder.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        PurchaseOrder.CreationUser = audit.CodeUser;
                        PurchaseOrder.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        //InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        //inventoryControlDocument.DocumentNumber = PurchaseOrder.Code;
                        //inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.PurchaseOrder;
                        //inventoryControlDocument.DocumentUser = audit.CodeUser;
                        //inventoryControlDocument.DocumentDate = PurchaseOrder.CreationDate;
                        //_InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        auxPurchaseOrder = PurchaseOrder.OriginalValue;
                        PurchaseOrder.ModificationUser = audit.CodeUser;
                        PurchaseOrder.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    // Si se confirma la Orden de Compra
                    if (PurchaseOrder.Status == 2)
                    {
                        PurchaseOrder.ConfirmationDate = DateTime.Now;
                        PurchaseOrder.ConfirmationUser = audit.CodeUser;
                    }

                    // Si se anula la Orden de Compra
                    if (PurchaseOrder.Status == 3)
                    {
                        PurchaseOrder.AnnulmentDate = DateTime.Now;
                        PurchaseOrder.AnnulmentUser = audit.CodeUser;
                    }

                    foreach (Domain.Entities.PurchaseOrderDetail detail in PurchaseOrder.PurchaseOrderDetail)
                    {
                        detail.InventoryProduct = null;
                    }

                    _purchaseOrderRepository.SaveEntity(PurchaseOrder);
                    unitOfWork.Commit();

                    string messageResultConfirmPurchaseOrder = "";
                    
                    if (PurchaseOrder.Status == 2 || PurchaseOrder.UnConfirm)
                    {
                        if (PurchaseOrder.Status == 2)
                        {
                            parameter.Append("<PurchaseOrden>");
                            parameter.Append(String.Format("<{0}>{1}</{0}>", "Id", PurchaseOrder.Id));
                            parameter.Append(String.Format("<{0}>{1}</{0}>", "Status", PurchaseOrder.Status));
                            parameter.Append(String.Format("<{0}>{1}</{0}>", "BudgetaryValidityId", PurchaseOrder.BudgetaryValidityId));
                            parameter.Append("</PurchaseOrden>");
                        }
                        else if (PurchaseOrder.UnConfirm)
                        {
                            parameter.Append("<PurchaseOrden>");
                            parameter.Append(String.Format("<{0}>{1}</{0}>", "Id", PurchaseOrder.Id));
                            parameter.Append(String.Format("<{0}>{1}</{0}>", "Status", 3));
                            parameter.Append("</PurchaseOrden>");
                        }

                        SP_ConfirmPurchaseOrder_Result resultConfirmPurchaseOrder = _purchaseOrderRepository.ConfirmPurchaseOrder(parameter.ToString());
                        if (resultConfirmPurchaseOrder.Status != 1)
                        {
                                unitOfWork.RollbackChangesUnitOfWork();
                                List<String> messages = new List<String>();
                                messages.Add(resultConfirmPurchaseOrder.Message);
                                return new ActionResult<Domain.Entities.PurchaseOrder>{ StateResult = false, StateResultAux = false, MessageResult = messages };
                        }
                        messageResultConfirmPurchaseOrder = resultConfirmPurchaseOrder.Message;
                    }

                    unitOfWorkSequense.Commit();
                    // unitOfWorkControlDocuments.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PurchaseOrder>(PurchaseOrder, audit, status, auxPurchaseOrder);
                    auditProcess.Execute();
					transaction.Complete();
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = true, ObjectEmbbeded = PurchaseOrder, Message = messageResultConfirmPurchaseOrder };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
					transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
					transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
					transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
                }
            };            
        }

        

        /// <summary>
        /// Elimina una Orden de Compra
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeletePurchaseOrder(Domain.Entities.PurchaseOrder PurchaseOrder, AuditMessage audit)
        {
            if (PurchaseOrder == null)
            {
                throw new ArgumentNullException("PurchaseOrder");
            }

            IUnitWork unitOfWork = _purchaseOrderRepository.UnitWork;
            try
            {
                PurchaseOrder.StartTracking();
                while (PurchaseOrder.PurchaseOrderDetail.Count > 0)
                {
                    PurchaseOrder.PurchaseOrderDetail.ElementAt(0).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted;
                }
                PurchaseOrder.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted;
                IndigoAuditSimpleEntity<Domain.Entities.PurchaseOrder> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PurchaseOrder>(PurchaseOrder, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _purchaseOrderRepository.SaveEntity(PurchaseOrder);
                unitOfWork.Commit();
                auditProcess.Execute();

                return new ActionResult { StateResult = true };
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, MessageResult = new List<string> { "-999" } };
            }
            catch (UpdateException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, MessageResult = new List<string> { "-000" } };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// desconfirma una Orden de Compra
        /// </summary>
        /// <param name="PurchaseOrder">PurchaseOrder</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PurchaseOrder> DisconfirmPurchaseOrder(Domain.Entities.PurchaseOrder purchaseOrder, AuditMessage audit)
        {
            if (purchaseOrder == null)
            {
                throw new ArgumentNullException("purchaseOrder");
            }

            try
            {
                purchaseOrder.Status = 1;
                purchaseOrder.UnConfirm = true;
                ActionResult<Domain.Entities.PurchaseOrder> resSave = SavePurchaseOrder(purchaseOrder, audit);
                if (resSave.StateResult == true)
                {
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = true, Message = "El documento se desconfirmó con éxito", ObjectEmbbeded = resSave.ObjectEmbbeded };
                }
                else
                {
                    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = (resSave.MessageResult != null ? resSave.MessageResult[0] : resSave.Message) };
                }

                //ActionResult validateAsociateDocument = _purchaseOrderRepository.GetQuantityAsociateDocument(purchaseOrder.Code);
                //if (validateAsociateDocument.StateResult == false)
                //{
                //    return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = validateAsociateDocument.Message };
                //}
                //else
                //{
                //    purchaseOrder.Status = 1;
                //    ActionResult<Domain.Entities.PurchaseOrder> resSave = SavePurchaseOrder(purchaseOrder, audit);
                //    if (resSave.StateResult == true)
                //    {
                //        return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = true, Message = "El documento se desconfirmó con éxito", ObjectEmbbeded = resSave.ObjectEmbbeded };
                //    }
                //    else
                //    {
                //        return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, Message = resSave.Message };
                //    }
                //}
            }
            catch (Exception ex)
            {
                return new ActionResult<Domain.Entities.PurchaseOrder> { StateResult = false, MessageResult = { ex.Message } };
            }
        }
        
        public ActionResult<List<Domain.Entities.PurchaseOrderDetail>> SetProductsPurchaseOrderImportFile(List<ImportFileRow> data, int operatingUnitId, AuditMessage audit, decimal roundingType = 0.01m)
        {
            try
            {
                return _inventoryService.SetProductsPurchaseOrderImportFile(data, operatingUnitId, audit, roundingType);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.PurchaseOrderDetail>> { StateResult = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }           
        }

        #endregion Methods

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
                    _inventoryService.Dispose();
                }
                _inventoryService = null;
                _purchaseOrderRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}