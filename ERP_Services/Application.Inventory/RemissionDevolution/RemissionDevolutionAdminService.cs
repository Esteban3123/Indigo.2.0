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
using System.Linq;
using System.Threading.Tasks;

namespace Application.Inventory.RemissionDevolution
{
    public class RemissionDevolutionAdminService : IRemissionDevolutionAdminService
    {
        #region fields
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IRemissionDevolutionRepository _remissionDevolutionRepository;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IRemissionEntranceDetailBatchSerialRepository _remissionEntranceDetailBatchSerialRepository;
        private IRemissionOutputDetailPhysicalRepository _remissionOutputDetailPhysicalRepository;        
        private IRemissionEntranceRepository _remissionEntranceRepository;
        private IRemissionOutputRepository _remissionOutputRepository;        
        private IRemissionEntranceDetailRepository _remissionEntranceDetailRepository;
        private IRemissionOutputDetailRepository _remissionOutputDetailRepository;        
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IInventoryService _inventoryServices;
        private IConsignmentInventoryRemissionRepository _consignmentInventoryRemissionRepository;
        private IConsignmentInventoryRemissionDetailRepository _consignmentInventoryRemissionDetailRepository;
        private IConsignmentInventoryRemissionDetailControlRepository _consignmentInventoryRemissionDetailControlRepository;
        private IConsignmentInventoryRemissionDetailBatchSerialRepository _consignmentInventoryRemissionDetailBatchSerialRepository;
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        #endregion

        #region builder
        public RemissionDevolutionAdminService(IInventorySequenceDetailRepository sequenseRepository, IRemissionDevolutionRepository remissionDevolutionRepository,
            ISettingInventoryRepository settingInventoryRepository, IRemissionEntranceDetailBatchSerialRepository remissionEntranceDetailBatchSerialRepository,
            IRemissionOutputDetailPhysicalRepository remissionOutputDetailPhysicalRepository, IRemissionEntranceRepository remissionEntranceRepository,
            IRemissionOutputRepository remissionOutputRepository, IRemissionEntranceDetailRepository remissionEntranceDetailRepository,
            IRemissionOutputDetailRepository remissionOutputDetailRepository, IPhysicalInventoryRepository physicalInventoryRepository,
            IPhysicalInventoryAdminService physicalInventoryAdminService, IInventoryControlDocumentRepository InventoryControlDocumentRepository, IInventoryService inventoryServices,
            IConsignmentInventoryRemissionRepository consignmentInventoryRemissionRepository, IConsignmentInventoryRemissionDetailRepository consignmentInventoryRemissionDetailRepository,
            IConsignmentInventoryRemissionDetailControlRepository consignmentInventoryRemissionDetailControlRepository, IConsignmentInventoryRemissionDetailBatchSerialRepository consignmentInventoryRemissionDetailBatchSerialRepository, 
            IPurchaseOrderDetailRepository purchaseOrderDetailRepository)
        {
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("sequenseRepository");
            }
            if (remissionDevolutionRepository == null)
            {
                throw new ArgumentNullException("remissionDevolutionRepository");
            }
            if (settingInventoryRepository == null)
            {
                throw new ArgumentNullException("settingInventoryRepository");
            }
            if (remissionEntranceDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("remissionEntranceDetailBatchSerialRepository");
            }
            if (remissionOutputDetailPhysicalRepository == null)
            {
                throw new ArgumentNullException("remissionOutputDetailPhysicalRepository");
            }
            if (remissionEntranceRepository == null)
            {
                throw new ArgumentNullException("remissionEntranceRepository");
            }
            if (remissionOutputRepository == null)
            {
                throw new ArgumentNullException("remissionOutputRepository");
            }
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            if (inventoryServices == null)
            {
                throw new ArgumentNullException("inventoryServices");
            }
            if (consignmentInventoryRemissionRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionRepository");
            }
            if (consignmentInventoryRemissionDetailRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailRepository");
            }
            if (consignmentInventoryRemissionDetailControlRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailControlRepository");
            }
            if (consignmentInventoryRemissionDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailBatchSerialRepository");
            }

            _sequenseRepository = sequenseRepository;
            _remissionDevolutionRepository = remissionDevolutionRepository;
            _settingInventoryRepository = settingInventoryRepository;
            _remissionEntranceDetailBatchSerialRepository = remissionEntranceDetailBatchSerialRepository;
            _remissionOutputDetailPhysicalRepository = remissionOutputDetailPhysicalRepository;
            _remissionEntranceRepository = remissionEntranceRepository;
            _remissionOutputRepository = remissionOutputRepository;
            _remissionEntranceDetailRepository = remissionEntranceDetailRepository;
            _remissionOutputDetailRepository = remissionOutputDetailRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _inventoryServices = inventoryServices;
            _consignmentInventoryRemissionRepository = consignmentInventoryRemissionRepository;
            _consignmentInventoryRemissionDetailRepository = consignmentInventoryRemissionDetailRepository;
            _consignmentInventoryRemissionDetailControlRepository = consignmentInventoryRemissionDetailControlRepository;
            _consignmentInventoryRemissionDetailBatchSerialRepository = consignmentInventoryRemissionDetailBatchSerialRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        }
        #endregion

        #region methods
        /// <summary>
        /// obtiene una devolucion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Entities.RemissionDevolution GetRemissionDevolutionByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.RemissionDevolution RemissionDevolution = _remissionDevolutionRepository.GetRemissionDevolutionByCode(code.Trim());
                if (RemissionDevolution != null && RemissionDevolution.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.RemissionDevolution> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionDevolution>(RemissionDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return RemissionDevolution;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.RemissionDevolution();
            }
        }

        /// <summary>
        /// guarda una devolucion
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.RemissionDevolution> SaveRemissionDevolution(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _remissionDevolutionRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                try
                {
                    //Valido que el almacen de la devolucion sea el mismo de la remision
                    if (RemissionDevolution.DevolutionType == 1)
                    {
                        //Obtengo la remision de entrada
                        var remissionEntrance = _remissionEntranceRepository.GetRemissionEntranceById(Convert.ToInt32(RemissionDevolution.RemissionEntranceId));
                        if (remissionEntrance == null || remissionEntrance.Id == 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "No se encontró la remision de entrada asociada." };
                        }

                        if (RemissionDevolution.WarehouseId != remissionEntrance.WarehouseId)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "El almacen de la devolución no corresponde con el almacén de la remisión." };
                        }
                    }
                    else if (RemissionDevolution.DevolutionType == 2)
                    {
                        //Obtengo la remision de salida
                        var remissionOutput = _remissionOutputRepository.GetRemissionOutputById(Convert.ToInt32(RemissionDevolution.RemissionOutputId));
                        if (remissionOutput == null || remissionOutput.Id == 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "No se encontró la remision de salida asociada." };
                        }

                        if (RemissionDevolution.WarehouseId != remissionOutput.WarehouseId)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "El almacen de la devolución no corresponde con el almacén de la remisión." };
                        }
                    }
                    else if (RemissionDevolution.DevolutionType == 3)
                    {
                        //Obtengo la remision de inventario en consignacion
                        var consignmentInventoryRemission = _consignmentInventoryRemissionRepository.GetConsignmentInventoryRemissionById(Convert.ToInt32(RemissionDevolution.ConsignmentInventoryRemissionId));
                        if (consignmentInventoryRemission == null || consignmentInventoryRemission.Id == 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "No se encontró la remision de inventario en consignación asociada." };
                        }

                        if (RemissionDevolution.WarehouseId != consignmentInventoryRemission.WarehouseId)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "El almacen de la devolución no corresponde con el almacén de la remisión." };
                        }
                    }
                    else
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "Tipo de devolución inválida." };
                    }

                    //Se valida que el registro tenga al menos un detalle
                    if (RemissionDevolution.RemissionDevolutionDetail == null || RemissionDevolution.RemissionDevolutionDetail.Where(d => d.Quantity > 0).ToList().Count == 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = "La devolución no tiene ningún detalle." };
                    }

                    RemissionDevolution.RemissionDevolutionDetail.ToList().ForEach(d => { if (!(d.Quantity > 0)) { RemissionDevolution.RemissionDevolutionDetail.Remove(d); } });

                    var inventoryServices = new InventoryServices(_settingInventoryRepository);
                    var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(RemissionDevolution.RemissionDate, RemissionDevolution.OperatingUnitId);
                    if (resultValidatePeriod.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                    }

                    InventorySequenceDetail seq = (idSequence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSequence)));
                    if (RemissionDevolution.Code == null || RemissionDevolution.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = RemissionDevolution.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(RemissionDevolution.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                RemissionDevolution.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    Domain.Entities.RemissionDevolution auxRemissionDevolution = null;
                    IndigoAuditSimpleEntity<Domain.Entities.RemissionDevolution> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (RemissionDevolution.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        RemissionDevolution.CreationUser = audit.CodeUser;
                        RemissionDevolution.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        inventoryControlDocument.DocumentNumber = RemissionDevolution.Code;
                        inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.ReferralReturn;
                        inventoryControlDocument.DocumentUser = audit.CodeUser;
                        inventoryControlDocument.DocumentDate = RemissionDevolution.CreationDate;
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        if (RemissionDevolution.Status == 3)
                        {
                            auxRemissionDevolution = RemissionDevolution.OriginalValue;
                            RemissionDevolution.AnnulmentUser = audit.CodeUser;
                            RemissionDevolution.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                            var documentControl = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(RemissionDevolution.Code, (int)eTypeDocumentsControlInventory.ReferralReturn);
                            if (documentControl != null && documentControl.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                                _InventoryControlDocumentRepository.UnitWork.Commit();
                            }
                        }
                        else
                        {
                            auxRemissionDevolution = RemissionDevolution.OriginalValue;
                            RemissionDevolution.ModificationUser = audit.CodeUser;
                            RemissionDevolution.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }

                    }

                    foreach (Domain.Entities.RemissionDevolutionDetail detail in RemissionDevolution.RemissionDevolutionDetail)
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
                    _remissionDevolutionRepository.SaveEntity(RemissionDevolution);
                    unitOfWork.CommitAndRefreshChanges();
                    unitOfWorkSequense.Commit();
                    unitOfWorkControlDocuments.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionDevolution>(RemissionDevolution, audit, status, auxRemissionDevolution);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = true, ObjectEmbbeded = RemissionDevolution };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
            }
        }

        /// <summary>
        /// confirmar una devolucion
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        public async Task<ActionResult<Domain.Entities.RemissionDevolution>> ConfirmRemissionDevolutionAsync(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, Boolean controlCost = false)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled))
            {
                IndigoAuditSimpleEntity<Domain.Entities.RemissionDevolution> auditProcess;
                IUnitWork remissionDevolutionUnitWork = _remissionDevolutionRepository.UnitWork;
                IUnitWork detailBatchUnitWork = _remissionEntranceDetailBatchSerialRepository.UnitWork;
                IUnitWork detailPhysicalUnitWork = _remissionOutputDetailPhysicalRepository.UnitWork;
                IUnitWork consignmentInventoryRemissionDetailBatchSerialUnitWork = _consignmentInventoryRemissionDetailBatchSerialRepository.UnitWork;
                IUnitWork consignmentInventoryRemissionDetailControlUnitWork = _consignmentInventoryRemissionDetailControlRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                try
                {
                    var errorsValidateQuantity = ValidateItemOutstandingQuantity(RemissionDevolution);
                    if (errorsValidateQuantity.Length > 0)
                    {                        
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = errorsValidateQuantity };
                    }

                    List<Kardex> listKardex = new List<Kardex>();
					StringBuilder errors = new StringBuilder();
					var IdsPurchaseOrderDetails = new HashSet<int>();
					var purchaseOrderDetailQuantities = new Dictionary<int, (int outstandingQuantity, int cancelledQuantity, string CodeNameProduct)>();
					foreach (var item in RemissionDevolution.RemissionDevolutionDetail)
                    {
						if (RemissionDevolution.DevolutionType == 1)//entrada
                        {
                            var detailBatch = _remissionEntranceDetailBatchSerialRepository.GetRemissionEntranceDetailBatchSerialById(Convert.ToInt32(item.RemissionEntranceDetailBatchSerialId));
                            var detailEntrance = _remissionEntranceDetailRepository.GetRemissionEntranceDetailById(detailBatch.RemissionEntranceDetailId);
                            listKardex.Add(new Kardex()
                            {
                                ProductId = detailEntrance.ProductId,
                                WarehouseId = RemissionDevolution.WarehouseId,
                                BatchSerialId = detailBatch.BatchSerialId,
                                MovementType = 2,
                                Quantity = item.Quantity,
                                Value = detailEntrance.ValueInKardex,
                                AffectInventory = true
                            });
							int purchaseOrderDetailId = detailEntrance?.PurchaseOrderDetailId ?? default(int);
							if (purchaseOrderDetailId != default(int))
							{
								IdsPurchaseOrderDetails.Add(purchaseOrderDetailId);
								if (!purchaseOrderDetailQuantities.ContainsKey(purchaseOrderDetailId))
								{
									purchaseOrderDetailQuantities[purchaseOrderDetailId] = (0, 0, item.CodeNameProduct);
								}
								purchaseOrderDetailQuantities[purchaseOrderDetailId] = (
									outstandingQuantity: purchaseOrderDetailQuantities[purchaseOrderDetailId].outstandingQuantity + item.Quantity,
									cancelledQuantity: purchaseOrderDetailQuantities[purchaseOrderDetailId].cancelledQuantity + item.Quantity,
									CodeNameProduct: item.CodeNameProduct
								);
							}
						}
                        else if (RemissionDevolution.DevolutionType == 2) //salida
                        {
                            var detailPhysical = _remissionOutputDetailPhysicalRepository.GetRemissionOutputDetailPhysicalById(Convert.ToInt32(item.RemissionOutputDetailPhysicalId));
                            var detailOutput = _remissionOutputDetailRepository.GetRemissionOutputDetailById(detailPhysical.RemissionOutputDetailId);
                            var physical = _physicalInventoryRepository.GetPhysicalInventoryById(detailPhysical.PhysicalInventoryId);
                            listKardex.Add(new Kardex()
                            {
                                ProductId = detailOutput.ProductId,
                                WarehouseId = RemissionDevolution.WarehouseId,
                                BatchSerialId = physical.BatchSerialId,
                                MovementType = 1,
                                Quantity = item.Quantity,
                                Value = detailOutput.SalePriceWithDiscount,
                                AffectInventory = true
                            });
                        }
                        else if (RemissionDevolution.DevolutionType == 3) //inventario en consignacion
                        {
                            var detailBatch = _consignmentInventoryRemissionDetailBatchSerialRepository.GetConsignmentInventoryRemissionDetailBatchSerialById(Convert.ToInt32(item.ConsignmentInventoryRemissionDetailBatchSerialId));
                            var detailEntrance = _consignmentInventoryRemissionDetailRepository.GetConsignmentInventoryRemissionDetailById(detailBatch.ConsignmentInventoryRemissionDetailId);
                            listKardex.Add(new Kardex()
                            {
                                ProductId = detailEntrance.ProductId,
                                WarehouseId = RemissionDevolution.WarehouseId,
                                BatchSerialId = detailBatch.BatchSerialId,
                                MovementType = 2,
                                Quantity = item.Quantity,
                                Value = detailEntrance.ValueInKardex,
                                AffectInventory = true
                            });
                        }
                    }
					var ListPurchaseOrderDetails = new List<Domain.Entities.PurchaseOrderDetail>();
					if (purchaseOrderDetailQuantities.Any())
					{
						ListPurchaseOrderDetails = _purchaseOrderDetailRepository.GetByFilter(t => IdsPurchaseOrderDetails.Contains(t.Id), false).ToList();
						foreach (var kvp in purchaseOrderDetailQuantities)
						{
							var purchaseOrderDetail = ListPurchaseOrderDetails.FirstOrDefault(p => p.Id == kvp.Key);
							if (purchaseOrderDetail != null)
							{
								purchaseOrderDetail.OutstandingQuantity += kvp.Value.outstandingQuantity;
								purchaseOrderDetail.CancelledQuantity -= kvp.Value.cancelledQuantity;
								purchaseOrderDetail.MarkAsModified();
								if (purchaseOrderDetail.OutstandingQuantity < 0 || purchaseOrderDetail.OutstandingQuantity > purchaseOrderDetail.Quantity)
								{
									errors.AppendLine(string.Format(ResourceManager.get_GetString("ProductQuantityOrder", "Inventory"), kvp.Value.CodeNameProduct));
								}
							}
						}
					}
					errorsValidateQuantity = errors.ToString();
					if (errorsValidateQuantity.Length > 0)
					{
						transaction.Dispose();
						return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = errorsValidateQuantity };
					}
					else
					{
						if (ListPurchaseOrderDetails.Any()){
							await _purchaseOrderDetailRepository.SaveEntityMassiveAsync(ListPurchaseOrderDetails);
						}
					}
					var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, RemissionDevolution.Id, RemissionDevolution.Code, RemissionDevolution.GetType().Name, RemissionDevolution.CreationUser, controlCost);
                    if (result.StateResult == false)
                    {
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = result.Message };
                    }

                    List<string> MessagesStock = new List<string>();
                    foreach (var item in RemissionDevolution.RemissionDevolutionDetail)
                    {
                        if (RemissionDevolution.DevolutionType == 1)//entrada
                        {
                            var detailBatch = _remissionEntranceDetailBatchSerialRepository.GetRemissionEntranceDetailBatchSerialById(Convert.ToInt32(item.RemissionEntranceDetailBatchSerialId));
                            var detailEntrance = _remissionEntranceDetailRepository.GetRemissionEntranceDetailById(detailBatch.RemissionEntranceDetailId);
                            detailBatch.OutstandingQuantity -= item.Quantity;
                            _remissionEntranceDetailBatchSerialRepository.SaveEntity(detailBatch);
                            detailBatchUnitWork.Commit();
                            //Validaciones Stock
                            var resultStock = _inventoryServices.ValidateStockProducts(detailEntrance.ProductId, RemissionDevolution.OperatingUnitId, RemissionDevolution.WarehouseId, 0, 0);
                            if (resultStock.StateResult == false)
                            {
                                MessagesStock.Add(resultStock.Message);
                            }
                        }
                        else if (RemissionDevolution.DevolutionType == 2) //salida
                        {
                            var detailPhysical = _remissionOutputDetailPhysicalRepository.GetRemissionOutputDetailPhysicalById(Convert.ToInt32(item.RemissionOutputDetailPhysicalId));
                            var detailOutput = _remissionOutputDetailRepository.GetRemissionOutputDetailById(detailPhysical.RemissionOutputDetailId);
                            detailPhysical.OutstandingQuantity -= item.Quantity;
                            _remissionOutputDetailPhysicalRepository.SaveEntity(detailPhysical);
                            detailPhysicalUnitWork.Commit();
                            //Validaciones Stock
                            var resultStock = _inventoryServices.ValidateStockProducts(detailOutput.ProductId, RemissionDevolution.OperatingUnitId, RemissionDevolution.WarehouseId, 0, 0);
                            if (resultStock.StateResult == false)
                            {
                                MessagesStock.Add(resultStock.Message);
                            }
                        }
                        else if (RemissionDevolution.DevolutionType == 3) //inventario en consignacion
                        {
                            var detailBatch = _consignmentInventoryRemissionDetailBatchSerialRepository.GetConsignmentInventoryRemissionDetailBatchSerialById(Convert.ToInt32(item.ConsignmentInventoryRemissionDetailBatchSerialId));
                            var detailEntrance = _consignmentInventoryRemissionDetailRepository.GetConsignmentInventoryRemissionDetailById(detailBatch.ConsignmentInventoryRemissionDetailId);

                            ConsignmentInventoryRemissionDetailControl consignmentInventoryRemissionDetailControl = new ConsignmentInventoryRemissionDetailControl()
                            {
                                ConsignmentInventoryRemissionDetailId = detailEntrance.Id,
								BatchSerialId = detailBatch.BatchSerialId,
                                MovementType = 2,
                                Quantity = item.Quantity,
                                Value = detailEntrance.ValueInKardex,
                                EntityId = RemissionDevolution.Id,
                                EntityCode = RemissionDevolution.Code,
                                EntityName = RemissionDevolution.GetType().Name,
                                EntityDetailId = item.Id,
                                CreationUser = audit.CodeUser,
                                CreationDate = DateTime.Now
                            };
                            _consignmentInventoryRemissionDetailControlRepository.SaveEntity(consignmentInventoryRemissionDetailControl);
                            consignmentInventoryRemissionDetailControlUnitWork.Commit();

                            
                            detailBatch.ReturnedQuantity += item.Quantity;
                            detailBatch.OutstandingQuantity -= item.Quantity;
                            _consignmentInventoryRemissionDetailBatchSerialRepository.SaveEntity(detailBatch);
                            consignmentInventoryRemissionDetailBatchSerialUnitWork.Commit();
                            
                            //Validaciones Stock
                            var resultStock = _inventoryServices.ValidateStockProducts(detailEntrance.ProductId, RemissionDevolution.OperatingUnitId, RemissionDevolution.WarehouseId, 0, 0);
                            if (resultStock.StateResult == false)
                            {
                                MessagesStock.Add(resultStock.Message);
                            }
                        }
                    }

                    if (RemissionDevolution.DevolutionType == 1)//entrada
                    {
                        var listDetailBatch = _remissionEntranceDetailBatchSerialRepository.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(Convert.ToInt32(RemissionDevolution.RemissionEntranceId));
                        IUnitWork remissionEntranceUnitWork = _remissionEntranceRepository.UnitWork;
                        var remissionEntrance = _remissionEntranceRepository.GetRemissionEntranceById(Convert.ToInt32(RemissionDevolution.RemissionEntranceId));
                        if (listDetailBatch.Count == 0)
                        {
                            remissionEntrance.ProductStatus = 3;//total
                        }
                        else
                        {
                            remissionEntrance.ProductStatus = 2;//parcial
                        }
                        remissionEntrance.MarkAsModified();
                        _remissionEntranceRepository.SaveEntity(remissionEntrance);
                        remissionEntranceUnitWork.Commit();
                    }
                    else if (RemissionDevolution.DevolutionType == 2) //salida
                    {
                        var listDetailPhysical = _remissionOutputDetailPhysicalRepository.ListRemissionOutputDetailPhysicalByRemissionOutputId(Convert.ToInt32(RemissionDevolution.RemissionOutputId));
                        IUnitWork remissionOutputUnitWork = _remissionOutputDetailPhysicalRepository.UnitWork;
                        var remissionOutput = _remissionOutputRepository.GetRemissionOutputById(Convert.ToInt32(RemissionDevolution.RemissionOutputId));
                        if (listDetailPhysical.Count == 0)
                        {
                            remissionOutput.ProductStatus = 3;//total
                        }
                        else
                        {
                            remissionOutput.ProductStatus = 2;//parcial
                        }
                        remissionOutput.MarkAsModified();
                        _remissionOutputRepository.SaveEntity(remissionOutput);
                        remissionOutputUnitWork.Commit();
                    }
                    else
                    {
                        var listDetailBatch = _consignmentInventoryRemissionDetailBatchSerialRepository.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionIdWithOutstandingQuantity(Convert.ToInt32(RemissionDevolution.ConsignmentInventoryRemissionId));
                        IUnitWork consignmentInventoryRemissionUnitWork = _consignmentInventoryRemissionRepository.UnitWork;
                        var consignmentInventoryRemission = _consignmentInventoryRemissionRepository.GetConsignmentInventoryRemissionById(Convert.ToInt32(RemissionDevolution.ConsignmentInventoryRemissionId));
                        if (listDetailBatch.Count == 0)
                        {
                            consignmentInventoryRemission.ProductStatus = 3;//total
                        }
                        else
                        {
                            consignmentInventoryRemission.ProductStatus = 2;//parcial
                        }
                        consignmentInventoryRemission.MarkAsModified();
                        _consignmentInventoryRemissionRepository.SaveEntity(consignmentInventoryRemission);
                        consignmentInventoryRemissionUnitWork.Commit();
                    }

                    //eliminamos doc. de control de inventarios
                    InventoryControlDocument inventoryControlDocument = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(RemissionDevolution.Code, (int)eTypeDocumentsControlInventory.ReferralReturn);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }

                    //Se genera el comprobante contable
                    SP_GenerateJournalVoucherByRemissionDevolution_Result resultJournalVoucher = _remissionDevolutionRepository.SP_GenerateJournalVoucherByRemissionDevolution(RemissionDevolution.Id, audit.CodeUser);
                    if (resultJournalVoucher.CodeMessage > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = RemissionDevolution, Message = resultJournalVoucher.Message };
                    }

                    RemissionDevolution.Status = 2;//confirmado
                    RemissionDevolution.ConfirmationDate = DateTime.Now;
                    RemissionDevolution.ConfirmationUser = audit.CodeUser;
                    RemissionDevolution.ModificationDate = DateTime.Now;
                    RemissionDevolution.ModificationUser = audit.CodeUser;
                    RemissionDevolution.MarkAsModified();
                    _remissionDevolutionRepository.SaveEntity(RemissionDevolution);
                    remissionDevolutionUnitWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.RemissionDevolution>(RemissionDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, RemissionDevolution.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = true, ObjectEmbbeded = RemissionDevolution, MessageResultAux = MessagesStock, Message = resultJournalVoucher.Message };
                }
                catch (OptimisticConcurrencyException)
                {
                    remissionDevolutionUnitWork.RollbackChanges();
                    detailBatchUnitWork.RollbackChanges();
                    detailPhysicalUnitWork.RollbackChanges();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = false, MessageResult = { ResourceManager.get_GetString("ErrorConcurrence") } };
                }
                catch (DbEntityValidationException ex)
                {
                    remissionDevolutionUnitWork.RollbackChanges();
                    detailBatchUnitWork.RollbackChanges();
                    detailPhysicalUnitWork.RollbackChanges();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = false, MessageResult = { ResourceManager.get_GetString("ErrorUnknown") } };
                }
                catch (Exception ex)
                {
                    remissionDevolutionUnitWork.RollbackChanges();
                    detailBatchUnitWork.RollbackChanges();
                    detailPhysicalUnitWork.RollbackChanges();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, StateResultAux = false, MessageResult = { ResourceManager.get_GetString("ErrorUnknown") } };
                }
            }
        }

        /// <summary>
        /// guardar y confirmar una devolucion
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        public async Task<ActionResult<Domain.Entities.RemissionDevolution>> SaveAndConfirmRemissionDevolutionAsync(Domain.Entities.RemissionDevolution RemissionDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false)
        {
            var result = SaveRemissionDevolution(RemissionDevolution, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = await ConfirmRemissionDevolutionAsync(RemissionDevolution, audit, controlCost);
                string message = resultConfirm.Message;

                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        message = String.IsNullOrEmpty(message) ? string.Format(ResourceManager.get_GetString("SavedAndConfirmedWithCode", "Inventory"), resultConfirm.ObjectEmbbeded.Code) : message;
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = message };

                    }
                    else
                    {
                        message = String.IsNullOrEmpty(message) ? ResourceManager.get_GetString("UpdatedAndConfirmed", "Inventory") : message;
                        return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = message };
                    }

                }
                else
                {
                    if (resultConfirm.StateResult == false && resultConfirm.StateResultAux == false)
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.RemissionDevolution> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionDevolution> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    return new ActionResult<Domain.Entities.RemissionDevolution> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
                else
                {
                    return new ActionResult<Domain.Entities.RemissionDevolution> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }

            }
        }
        /// <summary>
        /// metodo para validar que las cantidades existan
        /// </summary>
        /// <param name="RemissionDevolution"></param>
        /// <returns></returns>
        private string ValidateItemOutstandingQuantity(Domain.Entities.RemissionDevolution RemissionDevolution)
        {
            StringBuilder errors = new StringBuilder();
            foreach (var item in RemissionDevolution.RemissionDevolutionDetail)
            {
                if (RemissionDevolution.DevolutionType == 1)//entrada
                {
                    var detailBatch = _remissionEntranceDetailBatchSerialRepository.GetRemissionEntranceDetailBatchSerialById(Convert.ToInt32(item.RemissionEntranceDetailBatchSerialId));
                    if (item.Quantity > detailBatch.OutstandingQuantity)
                    {
                        errors.AppendLine(string.Format(ResourceManager.get_GetString("ProductQuantity", "Inventory"), item.CodeNameProduct));
                    }
                }
                else if (RemissionDevolution.DevolutionType == 2) //salida
                {
                    var detailPhysical = _remissionOutputDetailPhysicalRepository.GetRemissionOutputDetailPhysicalById(Convert.ToInt32(item.RemissionOutputDetailPhysicalId));
                    if (item.Quantity > detailPhysical.OutstandingQuantity)
                    {
                        errors.AppendLine(string.Format(ResourceManager.get_GetString("ProductQuantity", "Inventory"), item.CodeNameProduct));
                    }
                }
                else if (RemissionDevolution.DevolutionType == 3) //inventario en consignación
                {
                    var detailBatch = _consignmentInventoryRemissionDetailBatchSerialRepository.GetConsignmentInventoryRemissionDetailBatchSerialById(Convert.ToInt32(item.ConsignmentInventoryRemissionDetailBatchSerialId));
                    if (item.Quantity > detailBatch.OutstandingQuantity)
                    {
                        errors.AppendLine(string.Format(ResourceManager.get_GetString("ProductQuantity", "Inventory"), item.CodeNameProduct));
                    }
                }
            }
            return errors.ToString();
        }


        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _remissionDevolutionRepository.CascadeRollback(value);
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
                _sequenseRepository = null;
                _remissionDevolutionRepository = null;
                _settingInventoryRepository = null;
                _remissionEntranceDetailBatchSerialRepository = null;
                _remissionOutputDetailPhysicalRepository = null;
                _remissionEntranceRepository = null;
                _remissionOutputRepository = null;
                _remissionEntranceDetailRepository = null;
                _remissionOutputDetailRepository = null;
                _physicalInventoryRepository = null;
                _physicalInventoryAdminService = null;
                _InventoryControlDocumentRepository = null;
                _inventoryServices = null;
                _consignmentInventoryRemissionRepository = null;
                _consignmentInventoryRemissionDetailRepository = null;
                _consignmentInventoryRemissionDetailControlRepository = null;
                _consignmentInventoryRemissionDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
