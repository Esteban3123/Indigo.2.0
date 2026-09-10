///************************************************************
/// Assembly         : Application.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 2023-03-24
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using Application.Base;
using Application.Events.Models.Product;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Application.Inventory.RequestParam
{
    public class RequestParamAdminService : IRequestParamAdminService, Inject
    {
        private const string FORM_NAME = "FrmRequestParam";
        private readonly IRequestParamRepository _requestParamRepository;
        private readonly IInventoryProductRepository _inventoryProductRepository;
        private readonly IInventorySupplieRepository _inventorySupplieRepository;
        private readonly IRequestParamFunctionalUnitRepository _requestParamFunctionalUnitRepository;
        private readonly IRequestParamDetailPeriodicityRepository _requestParamDetailPeriodicityRepository;
        private readonly IRequestParamWarehouseRepository _requestParamWarehouseRepository;
        private readonly IRequestParamAuthUserRepository _requestParamAuthUserRepository;

        private readonly IInventorySequenceDetailRepository _sequenceRepository;

        public RequestParamAdminService(IRequestParamRepository requestParamRepository,
            IRequestParamFunctionalUnitRepository requestParamFunctionalUnitRepository,
            IInventoryProductRepository inventoryProductRepository,
            IInventorySupplieRepository inventorySupplieRepository,
            IInventorySequenceDetailRepository sequenceRepository,
            IRequestParamDetailPeriodicityRepository requestParamDetailPeriodicityRepository,
            IRequestParamWarehouseRepository requestParamWarehouseRepository,
            IRequestParamAuthUserRepository requestParamAuthUserRepository
            ) 
        {
            _sequenceRepository = sequenceRepository;
            _requestParamRepository = requestParamRepository;
            _inventorySupplieRepository = inventorySupplieRepository;
            _inventoryProductRepository = inventoryProductRepository;
            _requestParamFunctionalUnitRepository = requestParamFunctionalUnitRepository;
            _requestParamDetailPeriodicityRepository = requestParamDetailPeriodicityRepository;
            _requestParamWarehouseRepository = requestParamWarehouseRepository;
            _requestParamAuthUserRepository = requestParamAuthUserRepository;

        }

        /// <summary>
        /// Crea detalles de parametros de solicitud por imformación a importar
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ActionResult<List<RequestParamProduct>> LoadRequestParamProductByImportData(List<List<object>> data)
        {
            if (data == null) throw new ArgumentNullException("data");

            try
            {
                var result = ValidateInfoRequestParam(data);
                return new ActionResult<List<RequestParamProduct>> { StateResult = true, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = result.details, MessageResult = result.errors };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<RequestParamProduct>> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene los detalles de productos o insumos
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private (List<RequestParamProduct> details, List<string> errors) ValidateInfoRequestParam(List<List<object>> data)
        {
            var errors = new List<string>();
            var items = new List<RequestParamProduct>();

            foreach (var item in data)
            {
                var index = data.IndexOf(item) + 1;
                if (item.Count != 3)
                {
                    errors.Add($"Error en la posición {index}: no tiene la estructura requerida.");
                    continue;
                }

                var type = item[0].ToString();
                if (!byte.TryParse(type, out byte nType) || (nType < 1 && nType > 2))
                {
                    errors.Add($"Error en la posición {index}: no tiene un tipo válido.");
                    continue;
                }

                var quantity = item[2].ToString();
                if (!byte.TryParse(quantity, out byte nQuantity) || nQuantity < 0)
                {
                    errors.Add($"Error en la posición {index}: Cantidad no válida.");
                    continue;
                }

                var codeProduct = item[1].ToString().Trim();

                var requestParamProduct = new RequestParamProduct();
                requestParamProduct.Type = nType;
                requestParamProduct.Quantity = nQuantity;

                if (nType == 1)
                {
                    var supplie = _inventorySupplieRepository.Query(m => m.Code == codeProduct, false).FirstOrDefault();
                    if (supplie == null)
                    {
                        errors.Add($"Error en la posición {index}: El código de insumo ({codeProduct}) no existe.");
                        continue;
                    }

                    requestParamProduct.SupplieId = supplie.Id;
                    requestParamProduct.ProductCodeName = $"{supplie.Code} - {supplie.SupplieName}";
                }
                else
                {
                    var product = _inventoryProductRepository.Query(m => m.Code == codeProduct, false, includes: new[] { "ProductType" }).FirstOrDefault();
                    if (product == null)
                    {
                        errors.Add($"Error en la posición {index}: El código de producto ({codeProduct}) no existe.");
                        continue;
                    }

                    if (product.ProductType.Class != 4)
                    {
                        errors.Add($"Error en la posición {index}: El producto ({codeProduct}) no corresponde a un producto de clase Item otro.");
                        continue;
                    }

                    requestParamProduct.ProductId = product.Id;
                    requestParamProduct.ProductCodeName = $"{product.Code} - {product.Name}";
                }

                items.Add(requestParamProduct);
            }

            return (items, errors);
        }

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="requestParam"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionResult DeleteRequestParam(Domain.Entities.RequestParam requestParam, AuditMessage audit)
        {
            if (requestParam == null) throw new ArgumentNullException("requestParam");

            try
            {
                var txSettings = new TransactionOptions()
                {
                    Timeout = TransactionManager.MaximumTimeout,
                    IsolationLevel = IsolationLevel.ReadCommitted
                };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    while (requestParam.RequestParamFuncionalUnit.Any())
                    {
                        requestParam.RequestParamFuncionalUnit[0].MarkAsDeleted();
                    }

                    while (requestParam.RequestParamProduct.Any())
                    {
                        requestParam.RequestParamProduct[0].MarkAsDeleted();
                    }

                    while (requestParam.RequestParamAuthUser.Any())
                    {
                        requestParam.RequestParamAuthUser[0].MarkAsDeleted();
                    }

                    requestParam.MarkAsDeleted();
                    _requestParamRepository.SaveEntity(requestParam);
                    _requestParamRepository.UnitWork.Commit();

                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Get by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.RequestParam GetRequestParamByCode(string code)
        {
            return _requestParamRepository.GetRequestParamByCode(code);
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.RequestParam GetRequestParamById(int id)
        {
            return _requestParamRepository.FirstOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Save entity and save details to warehouse, funtionalUnit, requestParamAuthUser
        /// </summary>
        /// <param name="requestParam"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ActionResult<Domain.Entities.RequestParam> SaveRequestParam(Domain.Entities.RequestParam requestParam, AuditMessage audit, long idSecuence = 0)
        {
            if (requestParam == null) throw new ArgumentNullException("requestParam");

            try
            {
                var txSettings = new TransactionOptions()
                {
                    Timeout = TransactionManager.MaximumTimeout,
                    IsolationLevel = IsolationLevel.ReadCommitted
                };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(requestParam.Code))
                    {
                        InventorySequenceDetail seq = _sequenceRepository.GetSequenseDById((int)idSecuence);

                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                requestParam.Code = res;
                                seq.Next += 1;
                                _sequenceRepository.SaveEntity(seq);
                                _sequenceRepository.UnitWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.RequestParam> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), requestParam.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.RequestParam> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    Domain.Entities.RequestParam auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.RequestParam> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (requestParam.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        requestParam.CreationUser = audit.CodeUser;
                        requestParam.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = requestParam.OriginalValue;
                        requestParam.ModificationUser = audit.CodeUser;
                        requestParam.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    if (requestParam != null && requestParam.RequestParamProduct.Any())
                    {
                        foreach (var item in requestParam.RequestParamProduct.Where(m => m.Id == 0))
                        {
                            item.CreationUser = audit.CodeUser;
                            item.CreationDate = DateTime.Now;
                        }
                    }

                    if (requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("RequestParamFuncionalUnit"))
                        requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties["RequestParamFuncionalUnit"].Clear();

                     if(requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("RequestParamWharehouse"))
                     requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties["RequestParamWharehouse"].Clear();

                    if (requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("RequestParamDetailPeriodicity"))
                        requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties["RequestParamDetailPeriodicity"].Clear();


                    if (requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("RequestParamAuthUser"))
                        requestParam.ChangeTracker.ObjectsRemovedFromCollectionProperties["RequestParamAuthUser"].Clear();

                    if (requestParam.Id > 0)
                    {
                        var ids = requestParam.RequestParamFuncionalUnit.Where(m => m.Id > 0).Select(m => m.Id).ToList();
                        var toDelete = _requestParamFunctionalUnitRepository
                            .GetByFilter(m => m.RequestParamId == requestParam.Id && !ids.Contains(m.Id));

                        foreach (var item in toDelete)
                        {
                            _requestParamFunctionalUnitRepository.DeleteEntity(item);
                        }

                        var warehouseIds = requestParam.RequestParamWarehouse.Where(m => m.Id > 0).Select(m => m.Id).ToList();
                        var toDeleteWarehouse = _requestParamWarehouseRepository
                            .GetByFilter(m => m.RequestParamId == requestParam.Id && !warehouseIds.Contains(m.Id));
                        foreach (var itemDelete in toDeleteWarehouse)
                        {
                            _requestParamWarehouseRepository.DeleteEntity(itemDelete);
                        }

                        var idsDetails = requestParam.RequestParamDetailPeriodicity.Where(m => m.Id > 0).Select(m =>m.Id).ToList();
                        var toDeleteDetails = _requestParamDetailPeriodicityRepository.GetByFilter(m => m.IdRequestParam == requestParam.Id && !idsDetails.Contains(m.Id));
                        foreach (var itemDelete in toDeleteDetails)
                        {
                            _requestParamDetailPeriodicityRepository.DeleteEntity(itemDelete);
                        }
                        var listExistingAuthUsers = requestParam.RequestParamAuthUser;
                        var toDeleteAuthUser = _requestParamAuthUserRepository
                            .GetByFilter(m => m.RequestParamId == requestParam.Id);
                        foreach (var itemDelete in toDeleteAuthUser)
                        {
                            _requestParamAuthUserRepository.DeleteEntity(itemDelete);
                        }
                        requestParam.RequestParamAuthUser = listExistingAuthUsers;
                        

                        _requestParamFunctionalUnitRepository.UnitWork.Commit();
                        _requestParamWarehouseRepository.UnitWork.Commit();
                        _requestParamDetailPeriodicityRepository.UnitWork.Commit();
                        _requestParamAuthUserRepository.UnitWork.Commit();
                    }

                    _requestParamRepository.SaveEntity(requestParam);
                    _requestParamRepository.UnitWork.Commit();

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RequestParam>(requestParam, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    requestParam.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.RequestParam> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = requestParam, Message = MessageResult };
                }
            }
            catch (System.Data.OptimisticConcurrencyException)
            {
                _requestParamRepository.UnitWork.RollbackChanges();
                return new ActionResult<Domain.Entities.RequestParam> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                _requestParamRepository.UnitWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.RequestParam> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Change state
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionResult<Domain.Entities.RequestParam> UpdateStateRequestParam(string code, bool state, AuditMessage audit)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("code");

            if (audit == null) throw new ArgumentNullException("audit");

            try
            {
                var requestParam = _requestParamRepository.GetRequestParamByCode(code.Trim());

                if (requestParam != null && requestParam.Id > 0)
                {
                    requestParam.State = state;
                }

                var result = SaveRequestParam(requestParam, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.RequestParam> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
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

                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
