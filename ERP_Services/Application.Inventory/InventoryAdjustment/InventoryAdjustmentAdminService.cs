//'************************************************************
//' Assembly         : Domain.Inventory.InventoryAdjustmentRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 15/01/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Accounting;
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
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Inventory.InventoryAdjustment
{
    public class InventoryAdjustmentAdminService : IInventoryAdjustmentAdminService
    {
        #region Variables
        private IInventoryAdjustmentRepository _InventoryAdjustmentRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IInventoryProductRepository _inventoryProductRepository;
        private IWarehouseRepository _warehouseRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IAdjustmentConceptRepository _adjustmentConceptRepository;
        private IAccountingDocumentAdminService _accountingDocumentAdminService;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IInventoryControlRepository _InventoryControlRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IInventoryService _inventoryServices;

        #endregion

        #region Builder
        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="InventoryAdjustmentRepository"></param>
        /// <param name="sequenseRepository"></param>IInventoryAdjustmentAdminService
        public InventoryAdjustmentAdminService(IInventoryAdjustmentRepository InventoryAdjustmentRepository, IInventorySequenceDetailRepository sequenseRepository,
               IInventoryProductRepository inventoryProductRepository, IWarehouseRepository warehouseRepository, IPhysicalInventoryAdminService physicalInventoryAdminService,
               IAdjustmentConceptRepository adjustmentConceptRepository, IAccountingDocumentAdminService accountingDocumentAdminService, ISettingInventoryRepository settingInventoryRepository,
               IInventoryControlRepository InventoryControlRepository, IInventoryControlDocumentRepository InventoryControlDocumentRepository, IInventoryService inventoryServices)
        {
            if (InventoryAdjustmentRepository == null)
            {
                throw new ArgumentNullException("Repositorio de InventoryAdjustmentRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if (inventoryProductRepository == null)
            {
                throw new ArgumentNullException("Repositorio de inventoryProductRepository vacio");
            }
            if (warehouseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de warehouseRepository vacio");
            }
            if (physicalInventoryAdminService == null)
            {
                throw new ArgumentNullException("Repositorio de physicalInventoryAdminService vacio");
            }
            if (adjustmentConceptRepository == null)
            {
                throw new ArgumentNullException("Repositorio de adjustmentConceptRepository vacio");
            }
            if (accountingDocumentAdminService == null)
            {
                throw new ArgumentNullException("Repositorio de accountingDocumentAdminService vacio");
            }
            if (settingInventoryRepository == null)
            {
                throw new ArgumentNullException("Repositorio de settingInventoryRepository vacio");
            }
            if (InventoryControlRepository == null)
            {
                throw new ArgumentNullException("Repositorio de InventoryControlRepository vacio");
            }
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            if (inventoryServices == null)
            {
                throw new ArgumentNullException("inventoryServices");
            }
            _InventoryAdjustmentRepository = InventoryAdjustmentRepository;
            _sequenseRepository = sequenseRepository;
            _inventoryProductRepository = inventoryProductRepository;
            _warehouseRepository = warehouseRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _adjustmentConceptRepository = adjustmentConceptRepository;
            _accountingDocumentAdminService = accountingDocumentAdminService;
            _settingInventoryRepository = settingInventoryRepository;
            _InventoryControlRepository = InventoryControlRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _inventoryServices = inventoryServices;
        }
        #endregion

        /// <summary>
        /// funcion que guarda o actualiza el adjustmentinventory
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public async Task<ActionResult<Domain.Entities.InventoryAdjustment>> SaveInventoryAdjustment(
            Domain.Entities.InventoryAdjustment InventoryAdjustment,
            AuditMessage audit,
            long idSecuence = 0,
            InventorySequence sequenceC = null)
        {
            if (InventoryAdjustment == null)
            {
                throw new ArgumentNullException(nameof(InventoryAdjustment));
            }

            IUnitWork unitOfWork = _InventoryAdjustmentRepository.UnitWork;

            var txSettings = new TransactionOptions
            {
                Timeout = TransactionManager.MaximumTimeout,
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Valido que el almacen del ajuste no sea un almacen en consignación
                    if (InventoryAdjustment.WarehouseId != null)
                    {
                        // Llamada a base de datos ahora es asíncrona.
                        var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(Convert.ToInt32(InventoryAdjustment.WarehouseId));
                        if (warehouse?.WarehouseConsignment == true)
                        {
                            return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, Message = "No se puede realizar ajustes de inventario con almacenes en consignación" };
                        }
                    }

                    // --- Lógica de generación de secuencia ---
                    if (string.IsNullOrWhiteSpace(InventoryAdjustment.Code))
                    {
                        var seq = (idSecuence == 0)
                            ? new InventorySequenceDetail()
                            : await this._sequenseRepository.GetSequenseDByIdAsync(Convert.ToInt32(idSecuence));

                        if (seq == null)
                        {
                            return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }

                        if (seq.Id == 0 && sequenceC != null)
                        {
                            seq.IdSequense = sequenceC.IdSequence.Value;
                            seq.InventorySequenceId = sequenceC.Id;
                            seq.Next = 1;
                            seq.Prefix = InventoryAdjustment.Prefix;
                        }

                        string pattern = seq.Id > 0 ? seq.Sequense?.Pattern : sequenceC?.Sequense?.Pattern;
                        if (pattern == null)
                        {
                            return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }

                        var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, pattern, seq.Next);

                        if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                        {
                            InventoryAdjustment.Code = res;
                            seq.Next += 1;
                            this._sequenseRepository.SaveEntity(seq); // Marca la entidad 'seq' para ser guardada.
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    else
                    {
                        var inventoryAdjustmentInDb = _InventoryAdjustmentRepository.GetInventoryAdjustmentByIdOptionalTracking(InventoryAdjustment.Id, false);

                        if (inventoryAdjustmentInDb != null && inventoryAdjustmentInDb.Status != 1)
                        {
                            string statusName;

                            switch (inventoryAdjustmentInDb.Status)
                            {
                                case 2:
                                    statusName = "Confirmado";
                                    break;
                                case 3:
                                    statusName = "Anulado";
                                    break;
                                default:
                                    statusName = "Desconocido";
                                    break;
                            }

                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.InventoryAdjustment>
                            {
                                StateResult = false,
                                StateResultAux = false,
                                MessageResult = new List<string> { $"El estado actual del ajuste de inventario es '{statusName}'" },
                                Message = $"El estado actual del ajuste de inventario es '{statusName}'"
                            };
                        }
                    }

                    Domain.Entities.InventoryAdjustment auxInventoryAdjustment = null;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (InventoryAdjustment.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        InventoryAdjustment.CreationUser = audit.CodeUser;
                        InventoryAdjustment.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;

                        var inventoryControlDocument = new InventoryControlDocument
                        {
                            DocumentNumber = InventoryAdjustment.Code,
                            DocumentType = (int)eTypeDocumentsControlInventory.Inventoryadjustment,
                            DocumentUser = audit.CodeUser,
                            DocumentDate = InventoryAdjustment.CreationDate
                        };
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else // Updated
                    {
                        auxInventoryAdjustment = InventoryAdjustment.OriginalValue;
                        InventoryAdjustment.ModificationUser = audit.CodeUser;
                        InventoryAdjustment.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;

                        if (InventoryAdjustment.Status == 3) // Annulled
                        {
                            InventoryAdjustment.AnnulmentUser = audit.CodeUser;
                            InventoryAdjustment.AnnulmentDate = DateTime.Now;

                            var documentControl = await _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumberAsync(InventoryAdjustment.Code, (int)eTypeDocumentsControlInventory.Inventoryadjustment);
                            if (documentControl?.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                            }
                        }
                    }

                    // La lógica para desacoplar entidades se mantiene, ya que alterarla podría tener efectos secundarios no deseados.
                    foreach (InventoryAdjustmentDetail detail in InventoryAdjustment.InventoryAdjustmentDetail)
                    {
                        if (detail.InventoryProduct != null)
                        {
                            if (detail.InventoryProduct.ProductGroup != null)
                            {
                                detail.InventoryProduct.ProductGroupId = detail.InventoryProduct.ProductGroup.Id;
                                detail.InventoryProduct.ProductGroup = null;
                            }
                            if (detail.InventoryProduct.ProductSubGroup != null)
                            {
                                detail.InventoryProduct.ProductSubGroupId = detail.InventoryProduct.ProductSubGroup.Id;
                                detail.InventoryProduct.ProductSubGroup = null;
                            }
                            detail.InventoryProduct = null;
                        }
                    }

                    _InventoryAdjustmentRepository.SaveEntity(InventoryAdjustment);
                    await unitOfWork.CommitAsync();

                    // La auditoría se ejecuta después de que los datos se han confirmado exitosamente en la BD.
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment>(InventoryAdjustment, audit, status, auxInventoryAdjustment);
                    auditProcess.Execute();

                    // Si todo fue exitoso, se completa la transacción.
                    transaction.Complete();

                    return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = true, ObjectEmbbeded = InventoryAdjustment };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    // El `Rollback` es opcional si el UoW se desecha, pero lo mantenemos por si tiene lógica adicional.
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, Message = ex.Message };
                }
            }
        }

        /// <summary>
        /// Funcion usada para borrar un registro de inventoryadjustment
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, AuditMessage audit)
        {
            if (InventoryAdjustment == null)
            {
                throw new ArgumentNullException("product");
            }

            IUnitWork unitOfWork = _InventoryAdjustmentRepository.UnitWork;
            try
            {
                InventoryAdjustment.MarkAsDeleted();
                IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment>(InventoryAdjustment, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _InventoryAdjustmentRepository.DeleteEntity(InventoryAdjustment);
                unitOfWork.Commit();
                auditProcess.Execute();

                return new ActionResult { StateResult = true };

            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (DbUpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Funcion utilizada para cambiar el estado de un registro de inventoryadjustment
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryAdjustment> ChangeStateInventoryAdjustment(string code, byte state, AuditMessage audit)
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
                Domain.Entities.InventoryAdjustment InventoryAdjustment = _InventoryAdjustmentRepository.GetInventoryAdjustment(code);
                if (InventoryAdjustment != null && InventoryAdjustment.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment>(InventoryAdjustment, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = true, ObjectEmbbeded = InventoryAdjustment };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Funcion utilizada pra consultar un registro de ajuste de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryAdjustment> GetInventoryAdjustment(string code, AuditMessage audit)
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
                Domain.Entities.InventoryAdjustment InventoryAdjustment = _InventoryAdjustmentRepository.GetInventoryAdjustment(code);
                if (InventoryAdjustment != null && InventoryAdjustment.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment>(InventoryAdjustment, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = true, ObjectEmbbeded = InventoryAdjustment };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Funcion utilizada pra consultar un registro de ajuste de inventario por id
        /// </summary>
        /// <param name="idInventoryAdjustment"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryAdjustment GetInventoryAdjustmentById(int idInventoryAdjustment)
        {
            if (idInventoryAdjustment == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _InventoryAdjustmentRepository.GetInventoryAdjustmentById(idInventoryAdjustment);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Método utilizado para Guardar/actualizar y confirmar el documento
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<ActionResult<Domain.Entities.InventoryAdjustment>> SaveAndConfirmbInventoryAdjustment(
        Domain.Entities.InventoryAdjustment inventoryAdjustment,
        AuditMessage audit,
        int operatingUnitId,
        long idSequence = 0,
        Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert,
        InventorySequence sequenceC = null,
        int type = 0)
        {
            var saveResult = await SaveInventoryAdjustment(inventoryAdjustment, audit, idSequence, sequenceC);
            if (!saveResult.StateResult)
            {
                String messageTemplate = ResourceManager.get_GetString("NoSaved", "Inventory");
                string message = string.Format(messageTemplate, Environment.NewLine + (saveResult.Message ?? ""));

                return new ActionResult<Domain.Entities.InventoryAdjustment>
                {
                    StateResult = false,
                    ObjectEmbbeded = saveResult.ObjectEmbbeded,
                    StateResultAux = false,
                    Message = message,
                    MessageResult = new List<string> {message}
                };
            }

            var confirmResult = await ConfirmInventoryAdjustment(saveResult.ObjectEmbbeded, audit, operatingUnitId, type);
            bool confirmed = confirmResult.StateResult && confirmResult.StateResultAux;

            string messageKey;
            if (confirmed)
            {
                messageKey = (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    ? "SaveAndConfirmDocument"
                    : "UpdateAndConfirmDocument";

                return new ActionResult<Domain.Entities.InventoryAdjustment>
                {
                    StateResult = true,
                    ObjectEmbbeded = saveResult.ObjectEmbbeded,
                    StateResultAux = true,
                    MessageResultAux = confirmResult.MessageResultAux,
                    Message = string.Format(ResourceManager.get_GetString(messageKey, "Inventory"),
                        saveResult.ObjectEmbbeded.Code, confirmResult.Message)
                };
            }
            else
            {
                bool isInsert = (action == Infrastructure.CrossCutting.Audit.Actions.Insert);
                string code = saveResult.ObjectEmbbeded.Code;
                string detailMessage = confirmResult.MessageResult != null
                    ? string.Join(Environment.NewLine, confirmResult.MessageResult)
                    : confirmResult.Message;

                string templateKey = isInsert ? "SavedNoConfirmed" : "UpdatedNoConfirmed";
                string template = ResourceManager.get_GetString(templateKey, "Inventory");

                string message = isInsert
                    ? string.Format(template, code, string.IsNullOrWhiteSpace(detailMessage) ? "motivo no especificado" : detailMessage)
                    : string.Format(template, string.IsNullOrWhiteSpace(detailMessage) ? "motivo no especificado" : detailMessage);

                return new ActionResult<Domain.Entities.InventoryAdjustment>
                {
                    StateResult = true,
                    ObjectEmbbeded = saveResult.ObjectEmbbeded,
                    StateResultAux = false,
                    Message = message
                };
            }
        }

        /// <summary>
        /// Confirma un ajuste de inventario y genera las afectaciones contables y físicas correspondientes.
        /// </summary>
        public async Task<ActionResult<Domain.Entities.InventoryAdjustment>> ConfirmInventoryAdjustment(
            Domain.Entities.InventoryAdjustment inventoryAdjustment,
            AuditMessage audit,
            int operatingUnitId,
            int type = 0)
        {
            var txOptions = new TransactionOptions
            {
                Timeout = TransactionManager.MaximumTimeout,
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var tx = new TransactionScope(TransactionScopeOption.Required, txOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                var unitOfWork = _InventoryAdjustmentRepository.UnitWork;
                var auditProcess = default(IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment>);
                var result = new ActionResult<Domain.Entities.InventoryAdjustment>();

                try
                {
                    
                    var generateJournal = true;
                    var errors = new List<string>();
                    var messagesStock = new List<string>();
                    var kardexList = new List<Kardex>();
                    JournalVouchers journalVouchers = new JournalVouchers();
                    var conceptDict = new Dictionary<int, Domain.Entities.AdjustmentConcept>();

                    inventoryAdjustment = _InventoryAdjustmentRepository.GetInventoryAdjustmentById(inventoryAdjustment.Id);
                    if (inventoryAdjustment == null) return ActionError("No se encontró el ajuste de inventario");

                    var validatePeriod = _inventoryServices.ValidateInventoryPeriod(inventoryAdjustment.DocumentDate, operatingUnitId);
                    if (!validatePeriod.StateResult) return ActionError(validatePeriod.Message);

                    var settings = validatePeriod.ObjectEmbbeded;

                    switch (inventoryAdjustment.AdjustmentType)
                    {
                        case 1:
                            var resultIn = await ProcessInputAdjustment(
                                inventoryAdjustment, settings, type, kardexList, conceptDict, journalVouchers, errors, messagesStock);
                            if (!resultIn.StateResult) return resultIn;
                            break;

                        case 2:
                            var resultOut = await ProcessOutputAdjustment(
                                inventoryAdjustment, settings, kardexList, conceptDict, journalVouchers, errors, messagesStock);
                            if (!resultOut.StateResult) return resultOut;
                            break;

                        case 3:
                            var resultPhysical = await ProcessPhysicalAdjustment(
                                inventoryAdjustment, audit, settings, kardexList, journalVouchers, errors, messagesStock);
                            if (!resultPhysical.StateResult) return resultPhysical;
                            break;

                        case 4:
                            var resultCustody = await ProcessCustodyAdjustment(
                                inventoryAdjustment, kardexList, errors);
                            if (!resultCustody.StateResult) return resultCustody;
                            generateJournal = false;
                            break;

                        default:
                            return ActionError("Tipo de ajuste de inventario no reconocido");
                    }

                    if (!journalVouchers.JournalVoucherDetails.Any())
                    {
                        generateJournal = false;
                    }

                    Domain.Base.Entities.ActionMessageResult<Domain.Entities.JournalVouchers> resultVoucher = new ActionMessageResult<JournalVouchers>();
                    if (generateJournal)
                    {
                        resultVoucher = await _accountingDocumentAdminService.SaveAccountingDocumentAsync(journalVouchers, audit);
                        if (!resultVoucher.StateResult)
                            return ActionError(resultVoucher.Message ?? string.Join(Environment.NewLine, resultVoucher.MessageResult));
                    }

                    var controlDoc = await _InventoryControlDocumentRepository
                        .GetInventoryControlDocumentByDocumentNumberAsync(inventoryAdjustment.Code, (int)eTypeDocumentsControlInventory.Inventoryadjustment);

                    if (controlDoc != null)
                        _InventoryControlDocumentRepository.DeleteEntity(controlDoc);

                    inventoryAdjustment.Status = 2;
                    inventoryAdjustment.ConfirmationDate = DateTime.Now;
                    inventoryAdjustment.ConfirmationUser = audit.CodeUser;
                    inventoryAdjustment.ModificationDate = DateTime.Now;
                    inventoryAdjustment.ModificationUser = audit.CodeUser;
                    inventoryAdjustment.MarkAsModified();

                    _InventoryAdjustmentRepository.SaveEntity(inventoryAdjustment);
                    await unitOfWork.CommitAsync();

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryAdjustment>(inventoryAdjustment, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, inventoryAdjustment.OriginalValue);
                    auditProcess.Execute();

                    tx.Complete();

                    result.StateResult = true;
                    result.StateResultAux = true;
                    result.ObjectEmbbeded = inventoryAdjustment;
                    result.MessageResultAux = messagesStock;
                    result.Message = generateJournal ? resultVoucher.ObjectEmbbeded.Consecutive.ToString() : "No genera comprobante contable";

                    return result;
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    tx.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");

                    return new ActionResult<Domain.Entities.InventoryAdjustment>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        Message = Utils.GetInnerExceptionMessageToString(ex)
                    };
                }
            }
        }

        /// <summary>
        /// Metodo que se encarga del manejo de errores
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private ActionResult<Domain.Entities.InventoryAdjustment> ActionError(string message)
        {
            return new ActionResult<Domain.Entities.InventoryAdjustment>
            {
                StateResult = false,
                StateResultAux = false,
                Message = message,
                MessageResult = new List<string> { message }
            };
        }

        /// <summary>
        /// Funcion que se encarga de la consulta de los conceptos de ajuste y del almacenamiento en diccionario temporal
        /// </summary>
        /// <param name="conceptId"></param>
        /// <param name="conceptDict"></param>
        /// <returns></returns>
        private Domain.Entities.AdjustmentConcept LoadConcept(int? conceptId, Dictionary<int, Domain.Entities.AdjustmentConcept> conceptDict)
        {
            if (conceptId == null) return new Domain.Entities.AdjustmentConcept();
            if (!conceptDict.TryGetValue(conceptId.Value, out var concept))
            {
                concept = _adjustmentConceptRepository.GetAdjustmentConceptById(conceptId.Value);
                conceptDict[conceptId.Value] = concept;
            }

            return concept;
        }

        /// <summary>
        /// Crea la cabecera del documento contable
        /// </summary>
        /// <param name="journal"></param>
        /// <param name="journalTypeId"></param>
        /// <param name="adj"></param>
        /// <param name="detail"></param>
        private void SetJournalHeader(JournalVouchers journal, int journalTypeId, Domain.Entities.InventoryAdjustment adj, string detail)
        {
            journal.IdJournalVoucher = journalTypeId;
            journal.VoucherDate = adj.DocumentDate;
            journal.Status = 2;
            journal.Detail = detail;
            journal.EntityCode = adj.Code;
            journal.EntityId = adj.Id;
            journal.EntityName = adj.GetType().Name;
            journal.IsClosedYear = 0;
        }

        /// <summary>
        /// Crea los detalles del comprobante contable
        /// </summary>
        /// <param name="product"></param>
        /// <param name="warehouse"></param>
        /// <param name="qty"></param>
        /// <param name="unitValue"></param>
        /// <param name="thirdPartyId"></param>
        /// <param name="isDebit"></param>
        /// <returns></returns>
        private JournalVoucherDetails CreateJournalDetail(Domain.Entities.InventoryProduct product, Domain.Entities.Warehouse warehouse, int qty, decimal unitValue, int thirdPartyId, bool isDebit)
        {
            return new JournalVoucherDetails
            {
                IdMainAccount = product.AccountInventoryId ?? 0,
                IdThirdParty = product.HandlesThirdPartyAccount ? thirdPartyId : (int?)null,
                IdCostCenter = product.AccountInventoryHandlessCostCenter ? warehouse.CostCenterId : (int?)null,
                DebitValue = isDebit ? qty * unitValue : 0,
                CreditValue = isDebit ? 0 : qty * unitValue,
                Detail = $"Detalle comprobante: {product.Code} - {product.Name}"
            };
        }

        /// <summary>
        /// Detalles del comprobante contable para la Contrapartida
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="costCenterId"></param>
        /// <param name="handleCostCenter"></param>
        /// <param name="thirdPartyId"></param>
        /// <param name="value"></param>
        /// <param name="isDebit"></param>
        /// <returns></returns>
        private JournalVoucherDetails CreateContraEntry(int accountId, bool handleThirdParty, int? costCenterId, bool handleCostCenter, int? thirdPartyId, decimal value, bool isDebit)
        {
            return new JournalVoucherDetails
            {
                IdMainAccount = accountId,
                IdThirdParty = handleThirdParty ? thirdPartyId : null,
                IdCostCenter = handleCostCenter ? costCenterId : null,
                DebitValue = isDebit ? value : 0,
                CreditValue = isDebit ? 0 : value,
                Detail = "Contrapartida generada automáticamente desde Ajuste de Inventario"
            };
        }

        /// <summary>
        /// Crea los registros de tipo Kardex
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="batchId"></param>
        /// <param name="movement"></param>
        /// <param name="qty"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Kardex CreateKardex(int productId, int warehouseId, int? batchId, byte movement, int qty, decimal value)
        {
            return new Kardex
            {
                ProductId = productId,
                WarehouseId = warehouseId,
                BatchSerialId = batchId,
                MovementType = movement,
                Quantity = qty,
                Value = value,
                AffectInventory = true
            };
        }

        /// <summary>
        /// Metodo que se encarga de validar el stock de los productos
        /// </summary>
        /// <param name="adjustment"></param>
        /// <param name="warehouse"></param>
        /// <param name="settings"></param>
        /// <param name="messages"></param>
        private void ValidateStock(Domain.Entities.InventoryAdjustment adjustment, Domain.Entities.Warehouse warehouse, Domain.Entities.SettingInventory settings, List<string> messages)
        {
            foreach (var detail in adjustment.InventoryAdjustmentDetail)
            {
                var result = _inventoryServices.ValidateStockProducts(detail.ProductId, adjustment.OperatingUnitId, warehouse.Id, 0, 0, settingInventory: settings);
                if (!result.StateResult)
                    messages.Add(result.Message);
            }
        }

        /// <summary>
        /// Tipo de ajuste de inventario: Entrada
        /// </summary>
        /// <param name="adjustment"></param>
        /// <param name="settings"></param>
        /// <param name="type"></param>
        /// <param name="kardexList"></param>
        /// <param name="conceptDict"></param>
        /// <param name="journal"></param>
        /// <param name="errors"></param>
        /// <param name="stockMessages"></param>
        /// <returns></returns>
        private async Task<ActionResult<Domain.Entities.InventoryAdjustment>> ProcessInputAdjustment(
        Domain.Entities.InventoryAdjustment adjustment,
        Domain.Entities.SettingInventory settings,
        int type,
        List<Kardex> kardexList,
        Dictionary<int, Domain.Entities.AdjustmentConcept> conceptDict,
        Domain.Entities.JournalVouchers journal,
        List<string> errors,
        List<string> stockMessages)
        {
            if (adjustment == null || settings == null)
                return ActionError("Los datos del ajuste o configuración son inválidos");

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(adjustment.WarehouseId ?? 0);
            if (warehouse == null || warehouse.VirtualStore || warehouse.WarehouseConsignment ||
                warehouse.CustodyStore || warehouse.TransitStore || warehouse.ControlStore)
            {
                return ActionError("El almacén no puede ser virtual, consignación, custodia, tránsito ni control");
            }

            var headerConcept = adjustment.AdjustmentConceptId != null
                ? LoadConcept(adjustment.AdjustmentConceptId, conceptDict)
                : null;

            SetJournalHeader(journal, settings.InventoryAdjustmentJournalVoucherTypeId, adjustment,
                "Comprobante de entrada, generado por Ajuste de Inventario. Tipo Entrada");

            foreach (var detail in adjustment.InventoryAdjustmentDetail)
            {
                var product = _inventoryProductRepository.GetInventoryProductById(detail.ProductId, false);
                if (product == null)
                    return ActionError($"Producto con ID {detail.ProductId} no encontrado");

                var detailConcept = detail.AdjustmentConceptId != null
                    ? LoadConcept(detail.AdjustmentConceptId, conceptDict)
                    : headerConcept;

                if (type == 0)
                {
                    if (product.ProductCost == 0)
                        return ActionError($"El producto {product.Code} - {product.Name} tiene costo promedio en 0");

                    detail.UnitValue = detailConcept.AffectsAverageCost ? detail.UnitValue : product.ProductCost;

                    if (detail.UnitValue == 0)
                        return ActionError($"El producto {product.Code} - {product.Name} no tiene valor unitario registrado en el detalle del ajuste");
                }

                foreach (var batch in detail.InventoryAdjustmentDetailBatchSerial)
                {
                    int quantity = batch.Quantity;
                    int? batchId = batch.BatchSerialId ;

                    kardexList.Add(CreateKardex(detail.ProductId, warehouse.Id, batchId, 1, quantity, detail.UnitValue));
                    journal.JournalVoucherDetails.Add(CreateJournalDetail(product, warehouse, quantity, detail.UnitValue,
                        adjustment.ThirdPartyId ?? 0, isDebit: true));
                }

                if (detailConcept == null)
                    return ActionError($"No se encontró el concepto contable para el detalle del producto {detail.ProductId}");

                decimal detailValue = detail.UnitValue * (detail.InventoryAdjustmentDetailBatchSerial?.Sum(b => b.Quantity) ?? 0);
                journal.JournalVoucherDetails.Add(CreateContraEntry(
                    detailConcept.AdjustmentAccountId,
                    detailConcept.MainAccounts?.HandlesThirdParty ?? false,
                    detailConcept.CostCenterId,
                    detailConcept.MainAccounts?.HandlesCostCenter ?? false,
                    adjustment.ThirdPartyId ?? 0,
                    detailValue,
                    isDebit: false));
            }

            var resultInventory = _physicalInventoryAdminService.SavePhysicalInventory(
                kardexList,
                adjustment.Id,
                adjustment.Code,
                adjustment.GetType().Name,
                adjustment.CreationUser);

            if (!resultInventory.StateResult)
                return ActionError(resultInventory.Message);

            ValidateStock(adjustment, warehouse, settings, stockMessages);

            return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = true };
        }

        /// <summary>
        /// Tipo de ajuste de inventario: Salida
        /// </summary>
        /// <param name="adjustment"></param>
        /// <param name="settings"></param>
        /// <param name="kardexList"></param>
        /// <param name="conceptDict"></param>
        /// <param name="journal"></param>
        /// <param name="errors"></param>
        /// <param name="stockMessages"></param>
        /// <returns></returns>
        private async Task<ActionResult<Domain.Entities.InventoryAdjustment>> ProcessOutputAdjustment(
        Domain.Entities.InventoryAdjustment adjustment,
        Domain.Entities.SettingInventory settings,
        List<Kardex> kardexList,
        Dictionary<int, Domain.Entities.AdjustmentConcept> conceptDict,
        JournalVouchers journal,
        List<string> errors,
        List<string> stockMessages)
        {
            if (adjustment == null || settings == null)
                return ActionError("Los datos del ajuste o configuración son inválidos");

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(adjustment.WarehouseId ?? 0);
            if (warehouse == null || warehouse.VirtualStore || warehouse.WarehouseConsignment ||
                warehouse.CustodyStore || warehouse.TransitStore)
            {
                return ActionError("El almacén no puede ser virtual, consignación, custodia, tránsito ni control");
            }
            var affectsAccounting = warehouse.WareHouseType == 0;

            var headerConcept = adjustment.AdjustmentConceptId != null
                ? LoadConcept(adjustment.AdjustmentConceptId, conceptDict)
                : null;

            SetJournalHeader(journal, settings.InventoryAdjustmentJournalVoucherTypeId, adjustment,
                "Comprobante de salida, generado por Ajuste de Inventario. Tipo Salida");

            foreach (var detail in adjustment.InventoryAdjustmentDetail)
            {
                var product = _inventoryProductRepository.GetInventoryProductById(detail.ProductId, false);
                if (product == null)
                    return ActionError($"Producto con ID {detail.ProductId} no encontrado");

                detail.UnitValue = product.ProductCost;

                if (detail.UnitValue == 0)
                    return ActionError($"El producto {product.Code} - {product.Name} tiene costo promedio en 0");

                var detailConcept = detail.AdjustmentConceptId != null
                    ? LoadConcept(detail.AdjustmentConceptId, conceptDict)
                    : headerConcept;

                foreach (var batch in detail.InventoryAdjustmentDetailBatchSerial)
                {
                    int quantity = batch.Quantity;
                    int? batchId = batch.BatchSerialId ;

                    kardexList.Add(CreateKardex(detail.ProductId, warehouse.Id, batchId, 2, quantity, detail.UnitValue));

                    if (affectsAccounting) { 
                        journal.JournalVoucherDetails.Add(CreateJournalDetail(product, warehouse, quantity, detail.UnitValue,
                        adjustment.ThirdPartyId ?? 0, isDebit: false));
                    }
                }
                
                if (affectsAccounting) 
                { 
                    if (detailConcept == null)
                        return ActionError($"No se encontró el concepto contable para el detalle del producto {detail.ProductId}");

                    journal.JournalVoucherDetails.Add(CreateContraEntry(
                        detailConcept.AdjustmentAccountId,
                        detailConcept.MainAccounts?.HandlesThirdParty ?? false,
                        detailConcept.CostCenterId,
                        detailConcept.MainAccounts?.HandlesCostCenter ?? false,
                        adjustment.ThirdPartyId ?? 0,
                        detail.UnitValue * (detail.InventoryAdjustmentDetailBatchSerial?.Sum(b => b.Quantity) ?? 0),
                        isDebit: true));
                }
            }

            var inventoryResult = _physicalInventoryAdminService.SavePhysicalInventory(
                kardexList,
                adjustment.Id,
                adjustment.Code,
                adjustment.GetType().Name,
                adjustment.CreationUser);

            if (!inventoryResult.StateResult)
                return ActionError(inventoryResult.Message);

            ValidateStock(adjustment, warehouse, settings, stockMessages);

            return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = true };
        }

        /// <summary>
        /// Tipo de ajuste de inventario: Inventario Fisico
        /// </summary>
        /// <param name="inventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="settings"></param>
        /// <param name="listKardex"></param>
        /// <param name="journalVouchers"></param>
        /// <param name="errors"></param>
        /// <param name="messagesStock"></param>
        /// <returns></returns>
        private async Task<ActionResult<Domain.Entities.InventoryAdjustment>> ProcessPhysicalAdjustment(
        Domain.Entities.InventoryAdjustment inventoryAdjustment,
        AuditMessage audit,
        Domain.Entities.SettingInventory settings,
        List<Kardex> listKardex,
        JournalVouchers journalVouchers,
        List<string> errors,
        List<string> messagesStock)
        {
            decimal totalValueIn = 0;
            decimal totalValueOut = 0;

            journalVouchers.IdJournalVoucher = settings.InventoryAdjustmentJournalVoucherTypeId;
            journalVouchers.VoucherDate = inventoryAdjustment.DocumentDate;
            journalVouchers.Status = 2;
            journalVouchers.Detail = "Comprobante generado por Ajuste de Inventario. Inventario Físico";
            journalVouchers.EntityCode = inventoryAdjustment.Code;
            journalVouchers.EntityId = inventoryAdjustment.Id;
            journalVouchers.EntityName = inventoryAdjustment.GetType().Name;
            journalVouchers.IsClosedYear = 0;

            foreach (var detailPhysical in inventoryAdjustment.InventoryAdjustmentControl)
            {
                var controlDetail = _InventoryControlRepository.GetInventoryControlDetailByInventoryControlDetailBatchSerialId(detailPhysical.InventoryControlDetailBatchSerialId);
                var product = _inventoryProductRepository.GetInventoryProductById(controlDetail.ProductId);
                var batch = controlDetail.InventoryControlDetailBatchSerial.FirstOrDefault(x => x.Id == detailPhysical.InventoryControlDetailBatchSerialId);

                if (batch == null)
                {
                    errors.Add($"No se encontró el batch con ID: {detailPhysical.InventoryControlDetailBatchSerialId}");
                    return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, StateResultAux = false, MessageResult = errors };
                }

                decimal movementValue = Math.Round(product.ProductCost * detailPhysical.QuantityAdjustment, 2);

                listKardex.Add(new Kardex
                {
                    ProductId = product.Id,
                    WarehouseId = controlDetail.WarehouseId,
                    BatchSerialId = batch.BatchSerialId,
                    MovementType = (byte)(detailPhysical.AdjustmentType == 1 ? 1 : 2),
                    Quantity = detailPhysical.QuantityAdjustment,
                    Value = product.ProductCost,
                    AffectInventory = true
                });

                var journalDetail = new JournalVoucherDetails
                {
                    IdMainAccount = Convert.ToInt32(product.AccountInventoryId),
                    IdThirdParty = inventoryAdjustment.ThirdPartyId,
                    IdCostCenter = product.AccountInventoryHandlessCostCenter ? controlDetail.WarehouseCostCenterId : null,
                    DebitValue = detailPhysical.AdjustmentType == 1 ? movementValue : 0,
                    CreditValue = detailPhysical.AdjustmentType == 1 ? 0 : movementValue,
                    Detail = $"Detalle comprobante, generado desde Ajuste Físico: {product.Code} - {product.Name}"
                };

                journalVouchers.JournalVoucherDetails.Add(journalDetail);
                totalValueIn += journalDetail.DebitValue;
                totalValueOut += journalDetail.CreditValue;

                batch.Status = 2;
                batch.MarkAsModified();
            }

            var resultPhysical = _physicalInventoryAdminService.SavePhysicalInventory(
                listKardex,
                inventoryAdjustment.Id,
                inventoryAdjustment.Code,
                inventoryAdjustment.GetType().Name,
                inventoryAdjustment.CreationUser);

            if (!resultPhysical.StateResult)
            {
                errors.Add(resultPhysical.Message);
                return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = false, StateResultAux = false, MessageResult = errors };
            }

            var grouped = listKardex.GroupBy(k => new { k.WarehouseId, k.ProductId });
            foreach (var detail in grouped)
            {
                var resultStock = _inventoryServices.ValidateStockProducts(detail.Key.ProductId, inventoryAdjustment.OperatingUnitId, detail.Key.WarehouseId, 0, 0, settingInventory: settings);
                if (!resultStock.StateResult)
                {
                    messagesStock.Add(resultStock.Message);
                }
            }

            if (totalValueIn > 0)
            {
                journalVouchers.JournalVoucherDetails.Add(new JournalVoucherDetails
                {
                    IdMainAccount = settings.AdjustmentInAccountId,
                    IdThirdParty = inventoryAdjustment.ThirdPartyId,
                    IdCostCenter = settings.AdjustmentInHandlessCostCenter ? settings.AdjustmentInCostCenterId : null,
                    DebitValue = 0,
                    CreditValue = totalValueIn,
                    Detail = "Contrapartida ajuste físico - entrada"
                });
            }

            if (totalValueOut > 0)
            {
                journalVouchers.JournalVoucherDetails.Add(new JournalVoucherDetails
                {
                    IdMainAccount = settings.AdjustmentOutAccountId,
                    IdThirdParty = inventoryAdjustment.ThirdPartyId,
                    IdCostCenter = settings.AdjustmentInHandlessCostCenter ? settings.AdjustmentInCostCenterId : null,
                    DebitValue = totalValueOut,
                    CreditValue = 0,
                    Detail = "Contrapartida ajuste físico - salida"
                });
            }

            return new ActionResult<Domain.Entities.InventoryAdjustment> { StateResult = true, StateResultAux = true };
        }

        /// <summary>
        /// Tipo de ajuste de inventario: Ajuste de inventario Custodia
        /// </summary>
        /// <param name="adjustment"></param>
        /// <param name="kardexList"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private Task<ActionResult<Domain.Entities.InventoryAdjustment>> ProcessCustodyAdjustment(
        Domain.Entities.InventoryAdjustment adjustment,
        List<Kardex> kardexList,
        List<string> errors)
        {
            foreach (var detail in adjustment.InventoryAdjustmentDetail)
            {
                var product = _inventoryProductRepository.GetInventoryProductById(detail.ProductId, false);
                detail.UnitValue = product.ProductCost;

                foreach (var batch in detail.InventoryAdjustmentDetailBatchSerial)
                {
                    var result = _physicalInventoryAdminService.SavePhysicalInventoryCustody(
                        adjustment.AdmissionNumber,
                        detail.ProductId,
                        MovementType.OutPut,
                        adjustment.WarehouseId.GetValueOrDefault(),
                        batch.BatchSerialId,
                        batch.Quantity,
                        detail.UnitValue,
                        adjustment.Id,
                        adjustment.Code,
                        adjustment.GetType().Name,
                        adjustment.CreationUser,
                        product,
                        false);

                    if (!result.StateResult)
                    {
                        errors.Add(result.Message);
                    }
                }
            }

            var actionResult = errors.Any()
                ? new ActionResult<Domain.Entities.InventoryAdjustment>
                {
                    StateResult = false,
                    StateResultAux = false,
                    MessageResult = errors
                }
                : new ActionResult<Domain.Entities.InventoryAdjustment>
                {
                    StateResult = true,
                    StateResultAux = true
                };

            return Task.FromResult(actionResult);
        }


        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _InventoryAdjustmentRepository.CascadeRollback(value);
            return await Task.FromResult(res);
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
                    _accountingDocumentAdminService.Dispose();
                    _physicalInventoryAdminService.Dispose();
                    _inventoryServices.Dispose();
                }
                _InventoryAdjustmentRepository = null;
                _sequenseRepository = null;
                _inventoryProductRepository = null;
                _warehouseRepository = null;
                _physicalInventoryAdminService = null;
                _adjustmentConceptRepository = null;
                _accountingDocumentAdminService = null;
                _settingInventoryRepository = null;
                _InventoryControlRepository = null;
                _InventoryControlDocumentRepository = null;
                _inventoryServices = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
