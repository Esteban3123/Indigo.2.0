///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Diego Andrés Roldán Lozano
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
using Application.Base;
using Application.Inventory.PhysicalInventory;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Text;
using Domain.Entities.Service;
using System.Threading.Tasks;

namespace Application.Inventory.RemissionEntrance
{
    public class RemissionEntranceAdminService : IRemissionEntranceAdminService
    {
        #region fields        
        private IRemissionEntranceRepository _remissionEntranceRepository;        
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IInventoryContractDetailRepository _inventoryContractDetailRepository;
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IInventoryService _inventoryServices;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IWarehouseRepository _warehouseRepository;
        #endregion

        #region Builder
        public RemissionEntranceAdminService(IRemissionEntranceRepository remissionEntranceRepository, IInventorySequenceDetailRepository sequenseRepository, IInventoryContractDetailRepository inventoryContractDetailRepository,
            IPurchaseOrderDetailRepository purchaseOrderDetailRepository, IPhysicalInventoryAdminService physicalInventoryAdminService, ISettingInventoryRepository settingInventoryRepository,
            IInventoryControlDocumentRepository InventoryControlDocumentRepository, IInventoryService inventoryServices, IPhysicalInventoryRepository physicalInventoryRepository, IWarehouseRepository warehouseRepository)
        {
            if (remissionEntranceRepository == null)
            {
                throw new ArgumentNullException("remissionEntranceRepository");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("sequenseRepository");
            }
            if (inventoryContractDetailRepository == null)
            {
                throw new ArgumentNullException("inventoryContractDetailRepository");
            }
            if (purchaseOrderDetailRepository == null)
            {
                throw new ArgumentNullException("purchaseOrderDetailRepository");
            }
            if (physicalInventoryAdminService == null)
            {
                throw new ArgumentNullException("physicalInventoryAdminService");
            }
            if (settingInventoryRepository == null)
            {
                throw new ArgumentNullException("settingInventoryRepository");
            }
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            if (inventoryServices == null)
            {
                throw new ArgumentNullException("inventoryServices");
            }
            if (physicalInventoryRepository == null)
            {
                throw new ArgumentNullException("physicalInventoryRepository");
            }
            if (warehouseRepository == null)
            {
                throw new ArgumentNullException("warehouseRepository");
            }

            _remissionEntranceRepository = remissionEntranceRepository;
            _sequenseRepository = sequenseRepository;
            _inventoryContractDetailRepository = inventoryContractDetailRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _settingInventoryRepository = settingInventoryRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _inventoryServices = inventoryServices;
            _physicalInventoryRepository = physicalInventoryRepository;
            _warehouseRepository = warehouseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Entities.RemissionEntrance GetRemissionEntranceByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.RemissionEntrance remissionEntrance = _remissionEntranceRepository.GetRemissionEntranceByCode(code.Trim());

                if (remissionEntrance != null && remissionEntrance.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.RemissionEntrance> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionEntrance>(remissionEntrance, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return remissionEntrance;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.RemissionEntrance();
            }
        }

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Entities.RemissionEntrance GetRemissionEntranceById(int id)
        {
            try
            {
                return _remissionEntranceRepository.GetRemissionEntranceById(id);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.RemissionEntrance();
            }
        }

        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.RemissionEntrance> SaveRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _remissionEntranceRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                try
                {
                    var warehouse = _warehouseRepository.GetWarehouseById(remissionEntrance.WarehouseId);
                    if (warehouse != null && warehouse.WarehouseConsignment == true)
                    {
                        return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = false, Message = "No es posible realizar una remisión de entrada con un almacén de consignación." };
                    }

                    var inventoryServices = new InventoryServices(_settingInventoryRepository);
                    var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(remissionEntrance.RemissionDate, remissionEntrance.OperatingUnitId);
                    if (resultValidatePeriod.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                    }

                    InventorySequenceDetail seq = (idSequence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSequence)));
                    if (remissionEntrance.Code == null || remissionEntrance.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = remissionEntrance.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(remissionEntrance.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                remissionEntrance.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    Domain.Entities.RemissionEntrance auxRemissionEntrance = null;
                    IndigoAuditSimpleEntity<Domain.Entities.RemissionEntrance> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (remissionEntrance.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        remissionEntrance.CreationUser = audit.CodeUser;
                        remissionEntrance.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        inventoryControlDocument.DocumentNumber = remissionEntrance.Code;
                        inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.ReferralInput;
                        inventoryControlDocument.DocumentUser = audit.CodeUser;
                        inventoryControlDocument.DocumentDate = remissionEntrance.CreationDate;
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        if (remissionEntrance.Status == 3)
                        {
                            auxRemissionEntrance = remissionEntrance.OriginalValue;
                            remissionEntrance.AnnulmentUser = audit.CodeUser;
                            remissionEntrance.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                            var documentControl = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(remissionEntrance.Code, (int)eTypeDocumentsControlInventory.ReferralInput);
                            if (documentControl != null && documentControl.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                                _InventoryControlDocumentRepository.UnitWork.Commit();
                            }
                        }
                        else
                        {
                            auxRemissionEntrance = remissionEntrance.OriginalValue;
                            remissionEntrance.ModificationUser = audit.CodeUser;
                            remissionEntrance.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }

                    }
                    foreach (Domain.Entities.RemissionEntranceDetail detail in remissionEntrance.RemissionEntranceDetail)
                    {
                        if (detail.InventoryProduct != null)
                        {
                            if (detail.InventoryProduct.ProductGroup != null)
                            {
                                var groupId = detail.InventoryProduct.ProductGroup.Id;
                                detail.InventoryProduct.ProductGroup = null;
                                detail.InventoryProduct.ProductGroupId = groupId;
                            }
                            if (detail.InventoryProduct.ProductSubGroup != null)
                            {
                                var subGroupId = detail.InventoryProduct.ProductSubGroup.Id;
                                detail.InventoryProduct.ProductSubGroup = null;
                                detail.InventoryProduct.ProductSubGroupId = subGroupId;
                            }
                        }
                    }
                    _remissionEntranceRepository.SaveEntity(remissionEntrance);
                    unitOfWork.Commit();
                    unitOfWorkSequense.Commit();
                    unitOfWorkControlDocuments.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionEntrance>(remissionEntrance, audit, status, auxRemissionEntrance);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = true, ObjectEmbbeded = remissionEntrance };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }

        }

        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<Domain.Entities.RemissionEntrance> ConfirmRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, AuditMessage audit, Boolean controlCost = false)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork remissionEntranceUnitWork = _remissionEntranceRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.RemissionEntrance> auditProcess;
                try
                {
                    var warehouse = _warehouseRepository.GetWarehouseById(remissionEntrance.WarehouseId);
                    if (warehouse != null && warehouse.WarehouseConsignment == true)
                    {
                        return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = false, Message = "No es posible realizar una remisión de entrada con un almacén de consignación." };
                    }

                    List<string> MessagesStock = default(List<string>);
                    MessagesStock = new List<string>();
                    //////---------Modifico las cantidades en el inventario de los documentos---------////////

                    SP_PhysicalInventory_Result resultSavePhysicalInventory = _physicalInventoryRepository.SavePhysicalInventory(remissionEntrance.Id, remissionEntrance.GetType().Name, remissionEntrance.CreationUser, "INDIGO999", controlCost);
                    if (resultSavePhysicalInventory.StatusResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = false, MessageResult = new List<string>() { resultSavePhysicalInventory.MessageResult } };
                    }

                    if (resultSavePhysicalInventory.MessageResult != null)
                    {
                        string[] ResultStock = resultSavePhysicalInventory.MessageResult.Split(';');
                        foreach (var itemStock in ResultStock)
                        {
                            MessagesStock.Add(itemStock);
                        }
                    }                 
                    //eliminamos doc. de control de inventarios
                    InventoryControlDocument inventoryControlDocument = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(remissionEntrance.Code, (int)eTypeDocumentsControlInventory.ReferralInput);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }

                    //Se genera el comprobante contable
                    SP_GenerateJournalVoucherByRemissionEntrance_Result resultJournalVoucher = _remissionEntranceRepository.SP_GenerateJournalVoucherByRemissionEntrance(remissionEntrance.Id, audit.CodeUser);
                    if (resultJournalVoucher.CodeMessage > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, StateResultAux = false, MessageResult = new List<string>() { resultJournalVoucher.Message } };
                    }

                    remissionEntrance.Status = 2;
                    remissionEntrance.ConfirmationDate = DateTime.Now;
                    remissionEntrance.ConfirmationUser = audit.CodeUser;
                    remissionEntrance.ModificationDate = DateTime.Now;
                    remissionEntrance.ModificationUser = audit.CodeUser;
                    remissionEntrance.MarkAsModified();
                    _remissionEntranceRepository.SaveEntity(remissionEntrance);
                    remissionEntranceUnitWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionEntrance>(remissionEntrance, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, remissionEntrance.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = true, ObjectEmbbeded = remissionEntrance, MessageResultAux = MessagesStock, Message = resultJournalVoucher.Message };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    remissionEntranceUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.RemissionEntrance>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorConcurrence") }
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    remissionEntranceUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.RemissionEntrance>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
                catch (Exception ex)
                {
                    //remissionEntranceUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.RemissionEntrance>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        //MessageResult = { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
            }
        }

        /// <summary>
        /// guardar y confirmar una remisio
        /// </summary>
        /// <param name="remissionEntrance"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<Domain.Entities.RemissionEntrance> SaveAndConfirmbRemissionEntrance(Domain.Entities.RemissionEntrance remissionEntrance, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false)
        {
            var result = SaveRemissionEntrance(remissionEntrance, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = ConfirmRemissionEntrance(result.ObjectEmbbeded, audit, controlCost);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };

                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };
                    }
                }
                else
                {
                    if (resultConfirm.StateResult == false && resultConfirm.StateResultAux == false)
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            if (resultConfirm.MessageResult == null || resultConfirm.MessageResult.Count == 0)
                            {
                                return new ActionResult<Domain.Entities.RemissionEntrance> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };                                
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.RemissionEntrance> { MessageResult = resultConfirm.MessageResult, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                            }
                        }
                        else
                        {
                            if (resultConfirm.MessageResult == null || resultConfirm.MessageResult.Count == 0)
                            {
                                return new ActionResult<Domain.Entities.RemissionEntrance> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.RemissionEntrance> { MessageResult = resultConfirm.MessageResult, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                            }                            
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    return new ActionResult<Domain.Entities.RemissionEntrance> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
                else
                {
                    return new ActionResult<Domain.Entities.RemissionEntrance> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
            }
        }
        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _remissionEntranceRepository.CascadeRollback(value);
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
                    _physicalInventoryAdminService.Dispose();
                    _inventoryServices.Dispose();
                }
                _remissionEntranceRepository = null;
                _sequenseRepository = null;
                _inventoryContractDetailRepository = null;
                _purchaseOrderDetailRepository = null;
                _physicalInventoryAdminService = null;
                _settingInventoryRepository = null;
                _InventoryControlDocumentRepository = null;
                _inventoryServices = null;
                _physicalInventoryRepository = null;
                _warehouseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
