///************************************************************
/// Assembly         : Aplication.Inventory
/// Author           : Miguel Angel Fonseca
/// Created          : 2017-12-12
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

#region Imports

using Application.Base;
using Application.Inventory.PhysicalInventory;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Domain.Entities.Service;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Text;
using System.Transactions;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

#endregion Imports

namespace Application.Inventory.ConsignmentInventoryRemission
{
    public class ConsignmentInventoryRemissionAdminService : IConsignmentInventoryRemissionAdminService
    {
        #region fields

        private IConsignmentInventoryRemissionRepository _consignmentInventoryRemissionRepository;        
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IInventoryContractDetailRepository _inventoryContractDetailRepository;
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IInventoryService _inventoryServices;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IWarehouseRepository _warehouseRepository;
        private IConsignmentInventoryRemissionDetailRepository _consignmentInventoryRemissionDetailRepository;
        private IConsignmentInventoryRemissionDetailBatchSerialRepository _consignmentInventoryRemissionDetailBatchSerialRepository;

        #endregion fields

        #region Builder

        public ConsignmentInventoryRemissionAdminService(IConsignmentInventoryRemissionRepository consignmentInventoryRemissionRepository, IInventorySequenceDetailRepository sequenseRepository, IInventoryContractDetailRepository inventoryContractDetailRepository,
            IPurchaseOrderDetailRepository purchaseOrderDetailRepository, IPhysicalInventoryAdminService physicalInventoryAdminService, ISettingInventoryRepository settingInventoryRepository,
            IInventoryControlDocumentRepository InventoryControlDocumentRepository, IInventoryService inventoryServices, IPhysicalInventoryRepository physicalInventoryRepository, IWarehouseRepository warehouseRepository,
            IConsignmentInventoryRemissionDetailRepository consignmentInventoryRemissionDetailRepository, IConsignmentInventoryRemissionDetailBatchSerialRepository consignmentInventoryRemissionDetailBatchSerialRepository)
        {
            if (consignmentInventoryRemissionRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionRepository");
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
            if (consignmentInventoryRemissionDetailRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailRepository");
            }
            if (consignmentInventoryRemissionDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailBatchSerialRepository");
            }

            _consignmentInventoryRemissionRepository = consignmentInventoryRemissionRepository;
            _sequenseRepository = sequenseRepository;
            _inventoryContractDetailRepository = inventoryContractDetailRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _settingInventoryRepository = settingInventoryRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _inventoryServices = inventoryServices;
            _physicalInventoryRepository = physicalInventoryRepository;
            _warehouseRepository = warehouseRepository;
            _consignmentInventoryRemissionDetailRepository = consignmentInventoryRemissionDetailRepository;
            _consignmentInventoryRemissionDetailBatchSerialRepository = consignmentInventoryRemissionDetailBatchSerialRepository;
        }

        #endregion Builder

        #region Methods

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionById(int id)
        {
            try
            {
                return _consignmentInventoryRemissionRepository.GetConsignmentInventoryRemissionById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.ConsignmentInventoryRemission();
            }
        }

        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Entities.ConsignmentInventoryRemission GetConsignmentInventoryRemissionByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission = _consignmentInventoryRemissionRepository.GetConsignmentInventoryRemissionByCode(code.Trim());

                if (consignmentInventoryRemission != null && consignmentInventoryRemission.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.ConsignmentInventoryRemission> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ConsignmentInventoryRemission>(consignmentInventoryRemission, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return consignmentInventoryRemission;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.ConsignmentInventoryRemission();
            }
        }

        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            //Se realizan las validaciones si no se va a anular
            var validationResult = ValidateConsignmentInventoryRemission(consignmentInventoryRemission);
            if (validationResult != null)
            {
                return validationResult;
            }

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _consignmentInventoryRemissionRepository.UnitWork;
                try
                {

                    InventorySequenceDetail seq = (idSequence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSequence)));
                    if (consignmentInventoryRemission.Code == null || consignmentInventoryRemission.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                if (sequenceC == null || sequenceC.IdSequence == null)
                                {
                                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                                }

                                seq = this._sequenseRepository.GetSequenseDByIdSequenceAndPrefix(sequenceC.IdSequence.Value, sequenceC.Id, consignmentInventoryRemission.Prefix);
                                if (seq.Id == 0)
                                {
                                    seq.IdSequense = sequenceC.IdSequence.Value;
                                    seq.InventorySequenceId = sequenceC.Id;
                                    seq.Next = 1;
                                    seq.Prefix = consignmentInventoryRemission.Prefix;
                                }
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(consignmentInventoryRemission.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                consignmentInventoryRemission.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    Domain.Entities.ConsignmentInventoryRemission auxConsignmentInventoryRemission = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ConsignmentInventoryRemission> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (consignmentInventoryRemission.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        consignmentInventoryRemission.CreationUser = audit.CodeUser;
                        consignmentInventoryRemission.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        inventoryControlDocument.DocumentNumber = consignmentInventoryRemission.Code;
                        inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.ConsignmentInventoryRemission;
                        inventoryControlDocument.DocumentUser = audit.CodeUser;
                        inventoryControlDocument.DocumentDate = consignmentInventoryRemission.CreationDate;
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        if (consignmentInventoryRemission.Status == 3)
                        {
                            auxConsignmentInventoryRemission = consignmentInventoryRemission.OriginalValue;
                            consignmentInventoryRemission.AnnulmentUser = audit.CodeUser;
                            consignmentInventoryRemission.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                            var documentControl = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(consignmentInventoryRemission.Code, (int)eTypeDocumentsControlInventory.ConsignmentInventoryRemission);
                            if (documentControl != null && documentControl.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                            }
                        }
                        else
                        {
                            auxConsignmentInventoryRemission = consignmentInventoryRemission.OriginalValue;
                            consignmentInventoryRemission.ModificationUser = audit.CodeUser;
                            consignmentInventoryRemission.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }
                    }
                    foreach (Domain.Entities.ConsignmentInventoryRemissionDetail detail in consignmentInventoryRemission.ConsignmentInventoryRemissionDetail)
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
                    _consignmentInventoryRemissionRepository.SaveEntity(consignmentInventoryRemission);
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ConsignmentInventoryRemission>(consignmentInventoryRemission, audit, status, auxConsignmentInventoryRemission);
                    auditProcess.Execute();
                    unitOfWork.Commit();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = true, ObjectEmbbeded = consignmentInventoryRemission };
                }
                catch (DbUpdateException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = ((DbUpdateException)ex).InnerException.InnerException.Message };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
            }
        }

        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<Domain.Entities.ConsignmentInventoryRemission> ConfirmConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, AuditMessage audit, Boolean controlCost = false)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork consignmentInventoryRemissionUnitWork = _consignmentInventoryRemissionRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.ConsignmentInventoryRemission> auditProcess;
                try
                {
                    var warehouse = _warehouseRepository.GetWarehouseById(consignmentInventoryRemission.WarehouseId);
                    if (warehouse == null)
                    {
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = "El almacén seleccionado no existe." };                        
                    }
                    if (warehouse.WarehouseConsignment == false)
                    {
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = "La remisión de inventario en consignación sólo se pueden realizar con un almacén de consignación." };
                    }
                    if (warehouse.SupplierId != consignmentInventoryRemission.SupplierId)
                    {
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = "El proveedor del almacén es diferente del proveedor de la remisión." };
                    }

                    var errorsValidateQuantity = ValidateItemOutstandingQuantity(consignmentInventoryRemission);
                    if (errorsValidateQuantity.Length > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = errorsValidateQuantity };
                    }

                    List<string> MessagesStock = default(List<string>);
                    MessagesStock = new List<string>();
                    SP_PhysicalInventory_Result resultSavePhysicalInventory = _physicalInventoryRepository.SavePhysicalInventory(consignmentInventoryRemission.Id, consignmentInventoryRemission.GetType().Name, consignmentInventoryRemission.CreationUser, "INDIGO999", controlCost);
                    if (resultSavePhysicalInventory.StatusResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, MessageResult = new List<string>() { resultSavePhysicalInventory.MessageResult } };
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
                    InventoryControlDocument inventoryControlDocument = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(consignmentInventoryRemission.Code, (int)eTypeDocumentsControlInventory.ConsignmentInventoryRemission);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }

                    //Se genera el comprobante contable
                    SP_GenerateJournalVoucherByConsignmentInventoryRemission_Result resultJournalVoucher = _consignmentInventoryRemissionRepository.SP_GenerateJournalVoucherByConsignmentInventoryRemission(consignmentInventoryRemission.Id, audit.CodeUser);
                    if (resultJournalVoucher.CodeMessage > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, MessageResult = new List<string>() { resultJournalVoucher.Message } };
                    }

                    //Si es reposición actualizamos el detalle de la remisión de origen
                    if (consignmentInventoryRemission.MovementType == 3) //Reposición
                    {
                        foreach (var item in consignmentInventoryRemission.ConsignmentInventoryRemissionDetail)
                        {
                            var detail = _consignmentInventoryRemissionDetailRepository.GetConsignmentInventoryRemissionDetailById(Convert.ToInt32(item.ConsignmentInventoryRemissionDetailId));
                            foreach (var itemBatch in item.ConsignmentInventoryRemissionDetailBatchSerial)
                            {
                                var detailBatch = detail.ConsignmentInventoryRemissionDetailBatchSerial.Where(d => d.Id == itemBatch.ConsignmentInventoryRemissionDetailBatchSerialId).FirstOrDefault();
                                detailBatch.ReplacementQuantity += itemBatch.Quantity;
                                _consignmentInventoryRemissionDetailBatchSerialRepository.SaveEntity(detailBatch);
                            }
                        }
                    }

                    consignmentInventoryRemission.Status = 2;
                    consignmentInventoryRemission.ConfirmationDate = DateTime.Now;
                    consignmentInventoryRemission.ConfirmationUser = audit.CodeUser;
                    consignmentInventoryRemission.ModificationDate = DateTime.Now;
                    consignmentInventoryRemission.ModificationUser = audit.CodeUser;
                    consignmentInventoryRemission.MarkAsModified();
                    _consignmentInventoryRemissionRepository.SaveEntity(consignmentInventoryRemission);
                    consignmentInventoryRemissionUnitWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ConsignmentInventoryRemission>(consignmentInventoryRemission, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, consignmentInventoryRemission.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = true, ObjectEmbbeded = consignmentInventoryRemission, MessageResultAux = MessagesStock, Message = resultJournalVoucher.Message };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    consignmentInventoryRemissionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorConcurrence") }
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    consignmentInventoryRemissionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
                catch (Exception ex)
                {
                    //consignmentInventoryRemissionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission>
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
        /// <param name="consignmentInventoryRemission"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<Domain.Entities.ConsignmentInventoryRemission> SaveAndConfirmbConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false)
        {
            var result = SaveConsignmentInventoryRemission(consignmentInventoryRemission, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = ConfirmConsignmentInventoryRemission(result.ObjectEmbbeded, audit, controlCost);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };
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
                                return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { MessageResult = resultConfirm.MessageResult, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                            }
                        }
                        else
                        {
                            if (resultConfirm.MessageResult == null || resultConfirm.MessageResult.Count == 0)
                            {
                                return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { MessageResult = resultConfirm.MessageResult, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                            }
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
                else
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
            }
        }

        /// <summary>
        /// guardar y confirmar una remision al igual que su dispensación, esto para los tipos de remisión de gastos directos
        /// </summary>
        /// <param name="consignmentInventoryRemission"></param>
        /// <param name="pharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="sequenceC"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<string> SaveAndConfirmConsignmentInventoryRemissionAndPharmaceuticalDispensing(
            Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission,
            Domain.Entities.PharmaceuticalDispensing pharmaceuticalDispensing,
            AuditMessage audit,
            long idSequense,
            Domain.Entities.InventorySequence sequenceC,
            Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert
        )
        {
            ActionResult<string> actionResultReturn = new ActionResult<string>();
            actionResultReturn.Message = "OK";
            actionResultReturn.StateResult = true;
            actionResultReturn.StateResultAux = true;
            actionResultReturn.StatusCode = eStatusResult.SUCCESS;
            return actionResultReturn;
        }

        /// <summary>
        /// Valida la remisión de inventario en consignación antes de guardarla
        /// </summary>
        /// <param name="consignmentInventoryRemission">Remisión a validar</param>
        /// <returns>ActionResult con error si la validación falla, null si es válida</returns>
        private ActionResult<Domain.Entities.ConsignmentInventoryRemission> ValidateConsignmentInventoryRemission(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission)
        {
            //Se realizan las validaciones si no se va a anular
            if (consignmentInventoryRemission.Status != 3)
            {
                var warehouse = _warehouseRepository.GetWarehouseById(consignmentInventoryRemission.WarehouseId);
                if (warehouse == null)
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = "El almacén seleccionado no existe." };
                }
                if (warehouse.WarehouseConsignment == false)
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = "La remisión de inventario en consignación sólo se pueden realizar con un almacén de consignación." };
                }
                if (warehouse.SupplierId != consignmentInventoryRemission.SupplierId)
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = "El proveedor del almacén es diferente del proveedor de la remisión." };
                }

                var inventoryServices = new InventoryServices(_settingInventoryRepository);
                var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(consignmentInventoryRemission.RemissionDate, consignmentInventoryRemission.OperatingUnitId);
                if (resultValidatePeriod.StateResult == false)
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                }

                //Se valida que tenga registros
                if (consignmentInventoryRemission.ConsignmentInventoryRemissionDetail == null || consignmentInventoryRemission.ConsignmentInventoryRemissionDetail.Where(d => d.ChangeTracker.State != ObjectState.Deleted).Count() == 0)
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = "La remisión no tiene detalles" };
                }

                StringBuilder errors = new StringBuilder();
                //Validación por tipo de movimiento
                if (consignmentInventoryRemission.MovementType == 3)
                {
                    //Movimiento de reposición solo puede tener registros de inventario en consignación
                    if (consignmentInventoryRemission.ConsignmentInventoryRemissionDetail.Where(d => d.ChangeTracker.State != ObjectState.Deleted && d.RemissionSource != 4).Count() > 0)
                    {
                        errors.AppendLine("La remisión es de tipo reposición, no puede tener detalles que no sean de Remisiones de inventario en consignación.");
                    }

                    //Los registros deben estar enlazados a sus detalles
                    foreach (Domain.Entities.ConsignmentInventoryRemissionDetail detail in consignmentInventoryRemission.ConsignmentInventoryRemissionDetail.Where(d => d.ChangeTracker.State != ObjectState.Deleted))
                    {
                        //Movimiento de reposición solo puede tener registros de inventario en consignación
                        if (detail.ConsignmentInventoryRemissionDetailBatchSerial == null || detail.ConsignmentInventoryRemissionDetailBatchSerial.Where(d => d.ChangeTracker.State != ObjectState.Deleted).Count() == 0)
                        {
                            errors.AppendLine(String.Format("El producto {0} no posee detalle.", detail.CodeNameProduct));
                        }
                        else
                        {
                            foreach (Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial detailBatch in detail.ConsignmentInventoryRemissionDetailBatchSerial.Where(d => d.ChangeTracker.State != ObjectState.Deleted))
                            {
                                if (detail.ConsignmentInventoryRemissionDetailId == null || detailBatch.ConsignmentInventoryRemissionDetailBatchSerialId == null)
                                {
                                    errors.AppendLine(String.Format("El producto {0} no esta relacionado con ninguna remisión de inventario en consignación.", detail.CodeNameProduct));
                                }
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(errors.ToString()))
                {
                    return new ActionResult<Domain.Entities.ConsignmentInventoryRemission> { StateResult = false, StateResultAux = false, Message = errors.ToString() };
                }
            }

            return null;
        }

        /// <summary>
        /// metodo para validar que las cantidades existan
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <returns></returns>
        private string ValidateItemOutstandingQuantity(Domain.Entities.ConsignmentInventoryRemission consignmentInventoryRemission)
        {
            StringBuilder errors = new StringBuilder();
            if (consignmentInventoryRemission.MovementType == 3) //Reposición
            {
                foreach (var item in consignmentInventoryRemission.ConsignmentInventoryRemissionDetail)
                {
                    var detail = _consignmentInventoryRemissionDetailRepository.GetConsignmentInventoryRemissionDetailById(Convert.ToInt32(item.ConsignmentInventoryRemissionDetailId), true);
                    foreach (var itemBatch in item.ConsignmentInventoryRemissionDetailBatchSerial)
                    {
                        if (itemBatch.Quantity > 0)
                        {
                            var detailBatch = detail.ConsignmentInventoryRemissionDetailBatchSerial.Where(d => d.Id == itemBatch.ConsignmentInventoryRemissionDetailBatchSerialId).FirstOrDefault();
                            if (detailBatch == null)
                            {
                                errors.AppendLine(string.Format(ResourceManager.get_GetString("ProductNotFoundInRemision", "Inventory"), item.CodeNameProduct));
                            }
                            else if (itemBatch.Quantity > (detailBatch.UsedQuantity - detailBatch.ReplacementQuantity))
                            {
                                errors.AppendLine(string.Format(ResourceManager.get_GetString("ProductQuantity", "Inventory"), item.CodeNameProduct));
                            }
                        }
                        else
                        {
                            errors.AppendLine(string.Format(ResourceManager.get_GetString("QuantityReplacementRemission", "Inventory"), item.CodeNameProduct));
                        }
                    }
                }
            }
            return errors.ToString();
        }

        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _consignmentInventoryRemissionRepository.CascadeRollback(value);
            return await Task.FromResult(res);
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
                    _physicalInventoryAdminService.Dispose();
                    _inventoryServices.Dispose();
                }
                _consignmentInventoryRemissionRepository = null;
                _sequenseRepository = null;
                _inventoryContractDetailRepository = null;
                _purchaseOrderDetailRepository = null;
                _physicalInventoryAdminService = null;
                _settingInventoryRepository = null;
                _InventoryControlDocumentRepository = null;
                _inventoryServices = null;
                _physicalInventoryRepository = null;
                _warehouseRepository = null;
                _consignmentInventoryRemissionDetailRepository = null;
                _consignmentInventoryRemissionDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}