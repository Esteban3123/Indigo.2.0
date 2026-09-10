//'************************************************************
//' Assembly         : Domain.Inventory.InventoryControlRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 15/01/2015
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
using Infrastructure.CrossCutting.Resources;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using System.Data.Entity.Validation;
using Application.Inventory.PhysicalInventory;
using Application.Payments;
using Domain.Entities.Service;

namespace Application.Inventory.InventoryControl
{
    public class InventoryControlAdminService : IInventoryControlAdminService
    {
        #region Variables
        private IInventoryControlRepository _InventoryControlRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IInventoryProductRepository _inventoryProductRepository;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IInventoryService _inventoryService;
        #endregion

        #region Builder
        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="InventoryControlRepository"></param>
        /// <param name="sequenseRepository"></param>IInventoryControlAdminService
        public InventoryControlAdminService(IInventoryControlRepository InventoryControlRepository, IInventorySequenceDetailRepository sequenseRepository,
            IPhysicalInventoryAdminService physicalInventoryAdminService, IInventoryProductRepository inventoryProductRepository, ISettingInventoryRepository settingInventoryRepository,
            IInventoryService inventoryService)
        {
            if (InventoryControlRepository == null)
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if (physicalInventoryAdminService == null)
            {
                throw new ArgumentNullException("Repositorio de physicalInventoryAdminService vacio");
            }
            if (inventoryProductRepository == null)
            {
                throw new ArgumentNullException("Repositorio de inventoryProductRepository vacio");
            }
            _InventoryControlRepository = InventoryControlRepository;
            _sequenseRepository = sequenseRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _inventoryProductRepository = inventoryProductRepository;
            _settingInventoryRepository = settingInventoryRepository;
            _inventoryService = inventoryService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// funcion utilizada para guardar InventoryControl
        /// </summary>
        /// <param name="InventoryControl"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryControl> SaveInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit, long idSecuence = 0)
        {
            if (InventoryControl == null)
            {
                throw new ArgumentNullException("InventoryControl");
            }

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _InventoryControlRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;

                try
                {
                    var inventoryServices = new InventoryServices(_settingInventoryRepository);
                    var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(InventoryControl.DocumentDate, InventoryControl.OperatingUnitId);
                    if (resultValidatePeriod.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                    }

                    InventorySequenceDetail seq = null;
                    if (InventoryControl.Code == null || InventoryControl.Code.Trim().Equals(string.Empty))
                    {
                        seq = this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence));
                        if (seq != null && seq.Id > 0 && seq.InventorySequence.Sequential)
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                InventoryControl.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }

                    Domain.Entities.InventoryControl auxInventoryControl = null;
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryControl> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (InventoryControl.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        InventoryControl.CreationUser = audit.CodeUser;
                        InventoryControl.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        auxInventoryControl = InventoryControl.OriginalValue;
                        InventoryControl.ModificationUser = audit.CodeUser;
                        InventoryControl.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    //Establecer en null la relación con los productos para que no se realicen actualizaciones en esa tabla
                    foreach (var detail in InventoryControl.InventoryControlDetail)
                    {
                        detail.InventoryProduct = null;
                    }

                    _InventoryControlRepository.SaveEntity(InventoryControl);
                    _InventoryControlRepository.UnitWork.Commit();
                    unitOfWork.Commit();
                    unitOfWorkSequense.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryControl>(InventoryControl, audit, status, auxInventoryControl);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.InventoryControl> { StateResult = true, ObjectEmbbeded = InventoryControl };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ex.Message };
                }
            }
        }

        /// <summary>
        /// Método utilziado para eliminar un registro
        /// </summary>
        /// <param name="InventoryControl"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit)
        {
            if (InventoryControl == null)
            {
                throw new ArgumentNullException("product");
            }

            IUnitWork unitOfWork = _InventoryControlRepository.UnitWork;
            try
            {
                InventoryControl.MarkAsDeleted();
                IndigoAuditSimpleEntity<Domain.Entities.InventoryControl> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryControl>(InventoryControl, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _InventoryControlRepository.DeleteEntity(InventoryControl);
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
        /// Modifica el estado del registro
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryControl> ChangeStateInventoryControl(string code, byte state, AuditMessage audit)
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
                Domain.Entities.InventoryControl InventoryControl = _InventoryControlRepository.GetInventoryControl(code);
                if (InventoryControl != null && InventoryControl.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryControl> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryControl>(InventoryControl, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = true, ObjectEmbbeded = InventoryControl };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Consulta el InventoryControl por código
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryControl> GetInventoryControl(string code, AuditMessage audit)
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
                Domain.Entities.InventoryControl InventoryControl = _InventoryControlRepository.GetInventoryControl(code);
                if (InventoryControl != null && InventoryControl.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryControl> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryControl>(InventoryControl, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = true, ObjectEmbbeded = InventoryControl };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Consulta idInventoryControl por id
        /// </summary>
        /// <param name="idInventoryControl"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryControl GetInventoryControlById(int idInventoryControl)
        {
            if (idInventoryControl == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _InventoryControlRepository.GetInventoryControlById(idInventoryControl);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene el inventoryControl por estado de la tabla InventoryControlDetailBatchSerial
        /// </summary>
        /// <param name="idInventoryControl"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryControl> GetInventoryControlByIdAndStatus(int idInventoryControl, byte status)
        {
            if (idInventoryControl == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                var query = _InventoryControlRepository.GetInventoryControlByIdTask(idInventoryControl, status);

                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = true, ObjectEmbbeded = query };
            }
            catch (Exception ex)
            {
                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InventoryAdjustmentId"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List< Domain.Entities.InventoryControlDetailBatchSerial>> GetInventoryAdjustmentControlByInventoryAdjustmentId(int InventoryAdjustmentId)
        {
            if (InventoryAdjustmentId == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                var query= _InventoryControlRepository.GetInventoryControlByInventoryAdjustmentId(InventoryAdjustmentId);

                return new ActionResult<List<InventoryControlDetailBatchSerial>> { StateResult = true, ObjectEmbbeded = query };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Base.Entities.ActionResult<List<Domain.Entities.InventoryControlDetailBatchSerial>> { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }

        /// <summary>
        /// funcion utilizada para guardar/Actualizar y confirmar el documento
        /// </summary>
        /// <param name="InventoryControl"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryControl> SaveAndConfirmbInventoryControl(Domain.Entities.InventoryControl InventoryControl, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            if (InventoryControl == null)
            {
                throw new ArgumentNullException("InventoryControl");
            }


            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _InventoryControlRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
                try
                {
                    var inventoryServices = new InventoryServices(_settingInventoryRepository);
                    var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(InventoryControl.DocumentDate, InventoryControl.OperatingUnitId);
                    if (resultValidatePeriod.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                    }

                    InventorySequenceDetail seq = null;
                    if (InventoryControl.Code == null || InventoryControl.Code.Trim().Equals(string.Empty))
                    {
                        seq = this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSequence));
                        if (seq != null && seq.Id > 0 && seq.InventorySequence.Sequential)
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                InventoryControl.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                unitOfWorkSequense.Commit();
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }

                    Domain.Entities.InventoryControl auxInventoryControl = null;
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryControl> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    String Mensaje = string.Empty;
                    if (InventoryControl.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        InventoryControl.CreationUser = audit.CodeUser;
                        InventoryControl.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        Mensaje = "Guardado y confirmado con exito." + Environment.NewLine + "Código: " + InventoryControl.Code;
                    }
                    else
                    {
                        auxInventoryControl = InventoryControl.OriginalValue;
                        InventoryControl.ModificationUser = audit.CodeUser;
                        InventoryControl.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        Mensaje = "Actualizado y confirmado con exito: " + InventoryControl.Code;
                    }

                    //Establecer en null la relación con los productos para que no se realicen actualizaciones en esa tabla
                    foreach (var detail in InventoryControl.InventoryControlDetail)
                    {
                        detail.InventoryProduct = null;
                    }

                    InventoryControl.Status = 2;
                    InventoryControl.ConfirmationDate = DateTime.Now;
                    InventoryControl.ConfirmationUser = audit.CodeUser;
                    InventoryControl.ModificationDate = DateTime.Now;
                    InventoryControl.ModificationUser = audit.CodeUser;
                    _InventoryControlRepository.SaveEntity(InventoryControl);
                    unitOfWork.Commit();

                    if (InventoryControl.DocumentType == 1 || InventoryControl.DocumentType == 4)
                    {
                        List<Kardex> listKardex = new List<Kardex>();
                        foreach (var item in InventoryControl.InventoryControlDetail)
                        {
                            if (item.InventoryControlDetailBatchSerial.Count > 0)
                            {
                                Domain.Entities.InventoryProduct product = _inventoryProductRepository.GetInventoryProductById(item.ProductId);
                                foreach (var itemBatch in item.InventoryControlDetailBatchSerial)
                                {
                                    listKardex.Add(new Kardex()
                                    {
                                        ProductId = item.ProductId,
                                        WarehouseId = InventoryControl.WarehouseId,
                                        BatchSerialId = itemBatch.BatchSerialId,
                                        MovementType = 1,
                                        Quantity = itemBatch.Quantity,
                                        Value = product.ProductCost,
                                        AffectInventory = true
                                    });
                                }
                            }
                        }
                        ///---- Agrego las cantidades al inventario físico
                        var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, InventoryControl.Id, InventoryControl.Code, InventoryControl.GetType().Name, InventoryControl.CreationUser);
                        if (result.StateResult == false)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = "No se pudo confirmar el documento " + result.Message };
                        }
                    }
                    else if (InventoryControl.DocumentType == 3)//custodia
                    {
                        ///---- Agrego las cantidades al inventario físico
                        foreach (var item in InventoryControl.InventoryControlDetail)
                        {
                            if (item.InventoryControlDetailBatchSerial.Count > 0)
                            {
                                Domain.Entities.InventoryProduct product = _inventoryProductRepository.GetInventoryProductById(item.ProductId);
                                foreach (var itemBatch in item.InventoryControlDetailBatchSerial)
                                {
                                    var result = _physicalInventoryAdminService.SavePhysicalInventoryCustody(InventoryControl.AdmissionNumber, item.ProductId, MovementType.Input, InventoryControl.WarehouseId, itemBatch.BatchSerialId, itemBatch.Quantity, (product.ProductCost != null) ? Convert.ToDecimal(product.ProductCost) : 0, InventoryControl.Id, InventoryControl.Code, InventoryControl.GetType().Name, InventoryControl.CreationUser);
                                    if (result.StateResult == false)
                                    {
                                        transaction.Dispose();
                                        return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = "No se pudo confirmar el documento " + result.Message };
                                    }
                                }
                            }
                        }
                    }

                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryControl>(InventoryControl, audit, status, auxInventoryControl);
                    auditProcess.Execute();
                    transaction.Complete();
                    if (InventoryControl.Import)
                    {
                        return new ActionResult<Domain.Entities.InventoryControl> { StateResult = true, Message = Mensaje, ObjectEmbbeded = new Domain.Entities.InventoryControl { Id = InventoryControl.Id } };
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.InventoryControl> { StateResult = true, Message = Mensaje, ObjectEmbbeded = InventoryControl };
                    }

                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryControl> { StateResult = false, Message = ex.Message };
                }
            }
        }


        /// <summary>
        /// metodo para validar los items que se estan importando en el archivo de excel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<List<InventoryControlDetail>> SetProductsInventoryControlImportFile(List<ImportFileRow> data, int warehouseId, int controlType, AuditMessage audit, DateTime documentDate, int operatingUnitId)
        {
            return _inventoryService.SetProductsInventoryControlImportFile(data, warehouseId, controlType, audit, documentDate, operatingUnitId);
        }
        #endregion

        /// <summary>
        /// metodo para validar los items pegados en la rejilla
        /// </summary>
        /// <param name="data"></param>
        /// <param name="warehouseId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ActionResult<List<InventoryControlDetail>> SetProductsInventoryControlCopyPaste(List<List<string>> data, int warehouseId, int controlType, AuditMessage audit)
        {
            try
            {
                return _inventoryService.SetProductsInventoryControlCopyPaste(data, warehouseId, controlType, audit);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<InventoryControlDetail>>();
            }

        }

        /// <summary>
        /// obtyiene el control de inventarios por id sin agregado solo con el original value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryControl GetInventoryControlByIdNoAdded(int id)
        {
            try
            {
                return _InventoryControlRepository.GetInventoryControlByIdNoAdded(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.InventoryControl();
            }
        }


        /// <summary>
        /// obtyiene el control de inventarios por codigo sin agregado solo con el original value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryControl GetInventoryControlByCodeNoAdded(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.InventoryControl InventoryControl = _InventoryControlRepository.GetInventoryControlByCodeNoAdded(code);
                if (InventoryControl != null && InventoryControl.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryControl> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryControl>(InventoryControl, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return InventoryControl;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.InventoryControl();
            }
        }

        /// <summary>
        /// lista los detalles del control de inventario
        /// </summary>
        /// <param name="inventoryControlId"></param>
        /// <returns></returns>
        public List<Domain.Entities.InventoryControlDetail> GetInventoryControlDetailByInventoryControlId(int inventoryControlId)
        {
            try
            {
                return _InventoryControlRepository.GetInventoryControlDetailByInventoryControlId(inventoryControlId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.InventoryControlDetail>();
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
                    _inventoryService.Dispose();
                }
                _InventoryControlRepository = null;
                _sequenseRepository = null;
                _physicalInventoryAdminService = null;
                _inventoryProductRepository = null;
                _settingInventoryRepository = null;
                _inventoryService = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
