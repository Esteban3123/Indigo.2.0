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

namespace Application.Inventory.RemissionOutput
{
    public class RemissionOutputAdminService : IRemissionOutputAdminService
    {
        #region fields
        private IRemissionOutputRepository _remissionOutputRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IInventoryService _inventoryServices;
        #endregion

        #region builder
        public RemissionOutputAdminService(IRemissionOutputRepository RemissionOutputRepository, IInventorySequenceDetailRepository sequenseRepository, IPhysicalInventoryAdminService physicalInventoryAdminService,
            ISettingInventoryRepository settingInventoryRepository, IPhysicalInventoryRepository physicalInventoryRepository, IInventoryService inventoryServices)
        {
            if (RemissionOutputRepository == null)
            {
                throw new ArgumentNullException("RemissionOutputRepository");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("sequenseRepository");
            }
            if (physicalInventoryAdminService == null)
            {
                throw new ArgumentNullException("physicalInventoryAdminService");
            }
            if (settingInventoryRepository == null)
            {
                throw new ArgumentNullException("settingInventoryRepository");
            }
            if (physicalInventoryRepository == null)
            {
                throw new ArgumentNullException("physicalInventoryRepository");
            }
            if (inventoryServices == null)
            {
                throw new ArgumentNullException("inventoryServices");
            }
            _remissionOutputRepository = RemissionOutputRepository;
            _sequenseRepository = sequenseRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _settingInventoryRepository = settingInventoryRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _inventoryServices = inventoryServices;
        }
        #endregion
        
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Entities.RemissionOutput GetRemissionOutputByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.RemissionOutput RemissionOutput = _remissionOutputRepository.GetRemissionOutputByCode(code.Trim());
                if (RemissionOutput != null && RemissionOutput.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.RemissionOutput> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionOutput>(RemissionOutput, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return RemissionOutput;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.RemissionOutput();
            }
        }

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.RemissionOutput GetRemissionOutputById(int id)
        {
            try
            {
                return _remissionOutputRepository.GetRemissionOutputById(id);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.RemissionOutput();
            }
        }

        /// <summary>
        /// Saves the remission output.
        /// </summary>
        /// <param name="RemissionOutput">The remission output.</param>
        /// <param name="audit">The audit.</param>
        /// <param name="idSequence">The identifier sequence.</param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.RemissionOutput> SaveRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _remissionOutputRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
                try
                {
                    var inventoryServices = new InventoryServices(_settingInventoryRepository);
                    var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(RemissionOutput.RemissionDate, RemissionOutput.OperatingUnitId);
                    if (resultValidatePeriod.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                    }

                    InventorySequenceDetail seq = (idSequence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSequence)));
                    if (RemissionOutput.Code == null || RemissionOutput.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = RemissionOutput.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(RemissionOutput.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                RemissionOutput.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    Domain.Entities.RemissionOutput auxRemissionOutput = null;
                    IndigoAuditSimpleEntity<Domain.Entities.RemissionOutput> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (RemissionOutput.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        RemissionOutput.CreationUser = audit.CodeUser;
                        RemissionOutput.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        if (RemissionOutput.Status == 3)
                        {
                            auxRemissionOutput = RemissionOutput.OriginalValue;
                            RemissionOutput.AnnulmentUser = audit.CodeUser;
                            RemissionOutput.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }
                        else
                        {
                            auxRemissionOutput = RemissionOutput.OriginalValue;
                            RemissionOutput.ModificationUser = audit.CodeUser;
                            RemissionOutput.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }

                    }
                    foreach (Domain.Entities.RemissionOutputDetail detail in RemissionOutput.RemissionOutputDetail)
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
                    _remissionOutputRepository.SaveEntity(RemissionOutput);
                    unitOfWork.CommitAndRefreshChanges();
                    unitOfWorkSequense.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionOutput>(RemissionOutput, audit, status, auxRemissionOutput);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = true, ObjectEmbbeded = RemissionOutput };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
            }
        }

        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="RemissionOutput"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.RemissionOutput> ConfirmRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork remissionOutputUnitWork = _remissionOutputRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.RemissionOutput> auditProcess;
                try
                {
                    List<string> MessagesStock = default(List<string>);
                    MessagesStock = new List<string>();
                    foreach (var item in RemissionOutput.RemissionOutputDetail)
                    {
                        StringBuilder errors = new StringBuilder();
                        foreach (var itemPhysicalInventory in item.RemissionOutputDetailPhysical)
                        {
                            var physical = _physicalInventoryRepository.GetPhysicalInventoryById(itemPhysicalInventory.PhysicalInventoryId);
                            List<Kardex> listKardex = new List<Kardex>()
                            {
                                new Kardex()
                                {
                                    ProductId = item.ProductId,
                                    WarehouseId = RemissionOutput.WarehouseId,
                                    BatchSerialId = physical.BatchSerialId,
                                    MovementType = 2,
                                    Quantity = itemPhysicalInventory.Quantity,
                                    Value = item.SalePriceWithDiscount,
                                    AffectInventory = true
                                }
                            };
                            //guardo el inventario fisico
                            var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, RemissionOutput.Id, RemissionOutput.Code, RemissionOutput.GetType().Name, RemissionOutput.CreationUser);
                            if (result.StateResult == false)
                            {
                                //agrego a un acumulador de errores
                                errors.AppendLine(result.Message);
                            }

                        }
                        //Validaciones Stock
                        var resultStock = _inventoryServices.ValidateStockProducts(item.ProductId, RemissionOutput.OperatingUnitId, RemissionOutput.WarehouseId, 0, 0);
                        if (resultStock.StateResult == false)
                        {
                            MessagesStock.Add(resultStock.Message);
                        }
                        //si hubo errores los retorno todos
                        if (errors.Length > 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionOutput, Message = errors.ToString() };
                        }
                    }

                    //Se genera el comprobante contable
                    SP_GenerateJournalVoucherByRemissionOutput_Result resultJournalVoucher = _remissionOutputRepository.SP_GenerateJournalVoucherByRemissionOutput(RemissionOutput.Id, audit.CodeUser);
                    if (resultJournalVoucher.CodeMessage > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, StateResultAux = false, Message = resultJournalVoucher.Message, ObjectEmbbeded = RemissionOutput };
                    }

                    RemissionOutput.Status = 2;
                    RemissionOutput.ConfirmationDate = DateTime.Now;
                    RemissionOutput.ConfirmationUser = audit.CodeUser;
                    RemissionOutput.ModificationDate = DateTime.Now;
                    RemissionOutput.ModificationUser = audit.CodeUser;
                    RemissionOutput.MarkAsModified();
                    _remissionOutputRepository.SaveEntity(RemissionOutput);
                    remissionOutputUnitWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionOutput>(RemissionOutput, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, RemissionOutput.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = true, ObjectEmbbeded = RemissionOutput, MessageResultAux = MessagesStock, Message = resultJournalVoucher.Message };

                }
                catch (OptimisticConcurrencyException ex)
                {
                    remissionOutputUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.RemissionOutput>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorConcurrence") }
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    remissionOutputUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.RemissionOutput>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
                catch (Exception ex)
                {
                    remissionOutputUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.RemissionOutput>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
            }
        }

        /// <summary>
        /// guardar y confirmar una remisio
        /// </summary>
        /// <param name="RemissionOutput"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.RemissionOutput> SaveAndConfirmRemissionOutput(Domain.Entities.RemissionOutput RemissionOutput, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null)
        {
            var result = SaveRemissionOutput(RemissionOutput, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = ConfirmRemissionOutput(RemissionOutput, audit);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };

                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };
                    }

                }
                else
                {
                    if (resultConfirm.StateResult == false && resultConfirm.StateResultAux == false)
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.RemissionOutput> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionOutput> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    return new ActionResult<Domain.Entities.RemissionOutput> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
                else
                {
                    return new ActionResult<Domain.Entities.RemissionOutput> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
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
                    _physicalInventoryAdminService.Dispose();
                    _inventoryServices.Dispose();
                }
                _remissionOutputRepository = null;
                _sequenseRepository = null;
                _physicalInventoryAdminService = null;
                _settingInventoryRepository = null;
                _physicalInventoryRepository = null;
                _inventoryServices = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
