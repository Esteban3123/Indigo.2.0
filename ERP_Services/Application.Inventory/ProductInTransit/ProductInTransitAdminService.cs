///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Angi Camila Duran Vargas
/// Created          : 27-07-2023
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
using Application.Inventory.InventorySupplie;
using Application.Inventory.InventoryProduct;
using Application.Inventory.RemissionEntrance;
using Application.Inventory.ProductTemplate;
using Application.Inventory.Sequense;
using Application.Common;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Text;
using Domain.Entities.Service;
using System.Threading.Tasks;

namespace Application.Inventory.ProductInTransit
{
    public class ProductInTransitAdminService : IProductInTransitAdminService
    {
        #region fields        
        private IProductInTransitRepository _ProductInTransitRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IInventoryContractDetailRepository _inventoryContractDetailRepository;
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IInventoryService _inventoryServices;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IWarehouseRepository _warehouseRepository;
        private IInventorySupplieAdminService _inventorySupplieAdminService;
        private IInventoryProductAdminService _inventoryProductAdminService;
        private IRemissionEntranceAdminService _remissionEntranceAdminService;
        private IInventorySequenceAdminService _inventorySequenceAdminService;
        private IInventorySequenceRepository _inventorySequenceRepository;
        private ICurrencyAdminService _currencyAdminService;
        private IProductTemplateRepository _productTemplateRepository;
        private IProductTemplateAdminService _productTemplateAdminService;
        #endregion

        #region Builder
        public ProductInTransitAdminService(IProductInTransitRepository ProductInTransitRepository, IInventorySequenceDetailRepository sequenseRepository, IInventoryContractDetailRepository inventoryContractDetailRepository,
            IPurchaseOrderDetailRepository purchaseOrderDetailRepository, IPhysicalInventoryAdminService physicalInventoryAdminService, ISettingInventoryRepository settingInventoryRepository,
            IInventoryControlDocumentRepository InventoryControlDocumentRepository, IInventoryService inventoryServices, IPhysicalInventoryRepository physicalInventoryRepository,
            IWarehouseRepository warehouseRepository, IInventorySupplieAdminService inventorySupplieAdminService, IInventoryProductAdminService inventoryProductAdminService,
            IRemissionEntranceAdminService remissionEntranceAdminService, IInventorySequenceAdminService inventorySequenceAdminService, ICurrencyAdminService currencyAdminService,
            IInventorySequenceRepository inventorySequenceRepository, IProductTemplateRepository productTemplateRepository, IProductTemplateAdminService productTemplateAdminService)
        {
            if (ProductInTransitRepository == null)
            {
                throw new ArgumentNullException("ProductInTransitRepository");
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
            if (inventorySupplieAdminService == null)
            {
                throw new ArgumentNullException("inventorySupplieAdminService");
            }
            if (inventoryProductAdminService == null)
            {
                throw new ArgumentNullException("inventoryProductAdminService");
            }
            if (remissionEntranceAdminService == null)
            {
                throw new ArgumentNullException("remissionEntranceAdminService");
            }
            if (inventorySequenceAdminService == null)
            {
                throw new ArgumentNullException("inventorySequenceAdminService");
            }
            if (currencyAdminService == null)
            {
                throw new ArgumentNullException("currencyAdminService");
            }
            if (inventorySequenceRepository == null)
            {
                throw new ArgumentNullException("inventorySequenceRepository");
            }
            if (productTemplateRepository == null)
            {
                throw new ArgumentNullException("productTemplateRepository");
            }
            if (productTemplateAdminService == null)
            {
                throw new ArgumentNullException("productTemplateRepository");
            }


            _ProductInTransitRepository = ProductInTransitRepository;
            _sequenseRepository = sequenseRepository;
            _inventoryContractDetailRepository = inventoryContractDetailRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _settingInventoryRepository = settingInventoryRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _inventoryServices = inventoryServices;
            _physicalInventoryRepository = physicalInventoryRepository;
            _warehouseRepository = warehouseRepository;
            _inventorySupplieAdminService = inventorySupplieAdminService;
            _inventoryProductAdminService = inventoryProductAdminService;
            _remissionEntranceAdminService = remissionEntranceAdminService;
            _inventorySequenceAdminService = inventorySequenceAdminService;
            _currencyAdminService = currencyAdminService;
            _inventorySequenceRepository = inventorySequenceRepository;
            _productTemplateRepository = productTemplateRepository;
            _productTemplateAdminService = productTemplateAdminService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// obtiene una remision por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Entities.ProductInTransit GetProductInTransitByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.ProductInTransit ProductInTransit = _ProductInTransitRepository.GetProductInTransitByCode(code.Trim());

                if (ProductInTransit != null && ProductInTransit.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.ProductInTransit> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductInTransit>(ProductInTransit, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return ProductInTransit;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.ProductInTransit();
            }
        }

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Entities.ProductInTransit GetProductInTransitById(int id)
        {
            try
            {
                return _ProductInTransitRepository.GetProductInTransitById(id);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.ProductInTransit();
            }
        }

        /// <summary>
        /// obtiene una remision por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Domain.Entities.PETDefaultSettings GetPETDefaultSettings()
        {
            try
            {
                return _ProductInTransitRepository.GetPETDefaultSettings();
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PETDefaultSettings();
            }
        }

        /// <summary>
        /// guarda una remision
        /// </summary>
        /// <param name="ProductInTransit"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        public async Task<ActionResult<Domain.Entities.ProductInTransit>> SaveProductInTransitAsync(Domain.Entities.ProductInTransit ProductInTransit, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled))
            {
                IUnitWork unitOfWork = _ProductInTransitRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                try
                {
                    var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(ProductInTransit.WarehouseId);
                    if (warehouse != null && warehouse.WarehouseConsignment == true)
                    {
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = "No es posible realizar una remisión de entrada con un almacén de consignación." };
                    }

                    var inventoryServices = new InventoryServices(_settingInventoryRepository);
                    var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(ProductInTransit.DocumentDate, ProductInTransit.OperatingUnitId);
                    if (resultValidatePeriod.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                    }

                    InventorySequenceDetail seq = (idSequence == 0 ? new InventorySequenceDetail() : await this._sequenseRepository.GetSequenseDByIdAsync(Convert.ToInt32(idSequence)));
                    if (ProductInTransit.Code == null || ProductInTransit.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = ProductInTransit.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(ProductInTransit.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                ProductInTransit.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    Domain.Entities.ProductInTransit auxProductInTransit = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ProductInTransit> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (ProductInTransit.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        ProductInTransit.CreationUser = audit.CodeUser;
                        ProductInTransit.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        inventoryControlDocument.DocumentNumber = ProductInTransit.Code;
                        inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.ProductIntransit;
                        inventoryControlDocument.DocumentUser = audit.CodeUser;
                        inventoryControlDocument.DocumentDate = ProductInTransit.CreationDate;
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        if (ProductInTransit.Status == 3)
                        {
                            auxProductInTransit = ProductInTransit.OriginalValue;
                            ProductInTransit.AnnulmentUser = audit.CodeUser;
                            ProductInTransit.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                            var documentControl = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(ProductInTransit.Code, (int)eTypeDocumentsControlInventory.ProductIntransit);
                            if (documentControl != null && documentControl.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                                _InventoryControlDocumentRepository.UnitWork.Commit();
                            }
                        }
                        else
                        {
                            auxProductInTransit = ProductInTransit.OriginalValue;
                            ProductInTransit.ModificationUser = audit.CodeUser;
                            ProductInTransit.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }

                    }
                    foreach (Domain.Entities.ProductInTransitDetail detail in ProductInTransit.ProductInTransitDetail)
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
                    _ProductInTransitRepository.SaveEntity(ProductInTransit);
                    unitOfWork.Commit();
                    unitOfWorkSequense.Commit();
                    unitOfWorkControlDocuments.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductInTransit>(ProductInTransit, audit, status, auxProductInTransit);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = true, ObjectEmbbeded = ProductInTransit };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }

        }

        /// <summary>
        /// confirmar una remision
        /// </summary>
        /// <param name="ProductInTransitId"></param>
        /// <param name="audit"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ActionResult<Domain.Entities.ProductInTransit>> ConfirmProductInTransitAsync(Domain.Entities.ProductInTransit ProductInTransit, AuditMessage audit, Boolean controlCost = false)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            StringBuilder errors = new StringBuilder();
            List<TRM> listTRM = new List<TRM>();
            Decimal? tRMValue = 1;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled))
            {

                IUnitWork ProductInTransitUnitWork = _ProductInTransitRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.ProductInTransit> auditProcess;
                try
                {
                    int currentItem = 0;
                    var productRates = _productTemplateRepository.GetProductTemplateAll();
                    
                    // Cache de condiciones por tarifa para evitar consultas repetitivas
                    var conditionsCache = new Dictionary<int, List<ProductRateGeneral>>();
                    var rateDetailsCache = new Dictionary<int, List<Domain.Entities.ProductRateDetail>>();
                    
                    foreach (var rate in productRates)
                    {
                        conditionsCache[rate.Id] = _productTemplateRepository.GetProductRateGeneralConditionByTemplateById(rate.Id);
                       
                        rateDetailsCache[rate.Id] = rate.ProductRateDetail.ToEntityList<Domain.Entities.ProductRateDetail>();
                    }
                   
                    
                    foreach (var item in ProductInTransit.ProductInTransitDetail)
                    {
                        currentItem++;
                        
                        //se valida que exista el producto
                        Domain.Entities.InventoryProduct product = _inventoryProductAdminService.GetInventoryProduct(item.ProductCode, audit)?.ObjectEmbbeded;

                        if (product.Id == 0)
                        {
                            //se consuktan los parametros
                            var parameters = _ProductInTransitRepository.GetPETDefaultSettings();
                            if (parameters.Id == 0)
                            {
                                transaction.Dispose();
                                return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = "No es posible confirmaar debido a que no se encontraron parámetros PET" };
                            }

                            //Se crea el insumo con los parametros
                            Domain.Entities.InventorySupplie newitem = new Domain.Entities.InventorySupplie();
                            newitem.Code = item.ProductCode;
                            newitem.SupplieName = item.ProductName;
                            newitem.RiskLevelId = parameters.RiskLevelId;
                            newitem.PBSProduct = true;
                            newitem.SupplieStatus = true;
                            newitem.CreationUser = parameters.CreationUser;
                            newitem.CreationDate = DateTime.Now;
                            newitem.JustificationOfInputs = false;
                            newitem.OsteosynthesisMaterial = false;
                            newitem.Consumption = false;
                            newitem.OptometryDevice = false;

                            var result = _inventorySupplieAdminService.SaveInventorySupplie(newitem, audit);
                            if (result?.StateResult is null || !result.StateResult)
                            {
                                transaction.Dispose();
                                errors.AppendLine($"El insumo {item.ProductCode} no se pudo crear porque {result.Message}");
                            }

                           //Se calcula el trm
                            tRMValue = listTRM?.Find(x => x.CurrencyId == ProductInTransit.OfficialCurrencyId && x.OfficialCurrencyId == ProductInTransit.CurrencyId && x.MeasurementDate == ProductInTransit.DocumentDate.Date)?.Value;
                            SessionValues session = new SessionValues();
                            session.OfficialCurrencyId = ProductInTransit.OfficialCurrencyId;
                            session.CurrencyISO4217 = ProductInTransit.CurrencyAbbreviation;
                            if (tRMValue == null)
                            {
                                var Currency = _currencyAdminService.GetTRMbyCurrencyId(ProductInTransit.OfficialCurrencyId, ProductInTransit.CurrencyId, session, ProductInTransit.DocumentDate.Date);
                                if (Currency == null)
                                {
                                    return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = "No se encontró tasa TRM para la conversion de valores" };

                                }
                                listTRM.Add(Currency.ObjectEmbbeded);
                                tRMValue = listTRM?.Find(x => x.CurrencyId == ProductInTransit.OfficialCurrencyId && x.OfficialCurrencyId == ProductInTransit.CurrencyId && x.MeasurementDate == ProductInTransit.DocumentDate.Date)?.Value;
                                if (tRMValue == null)
                                {
                                    return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = "No se encontró tasa TRM para la conversion de valores" };

                                }
                            }


                            //Se crea ahora el producto
                            var supplieId = _inventorySupplieAdminService.GetInventorySupplieByCode(result?.ObjectEmbbeded.Code, audit)?.ObjectEmbbeded?.Id;
                            if (supplieId == null || supplieId == 0)
                            {
                                transaction.Dispose();
                                errors.AppendLine($"El insumo {item.ProductCode} no se pudo crear");
                                continue;
                            }


                            if (item.ProductCode.Contains(" "))
                            {
                                transaction.Dispose();
                                errors.AppendLine($"El código {item.ProductCode} del producto no puede contener espacios en blanco");
                                continue;
                            }


                            //Se construye el producto con la info de los parametros
                            Domain.Entities.InventoryProduct inventoryProductItem = new Domain.Entities.InventoryProduct();
                            inventoryProductItem.Code = item.ProductCode;
                            inventoryProductItem.Name = item.ProductName;
                            inventoryProductItem.ProductTypeId = parameters.ProductTypeId;
                            inventoryProductItem.ATCId = null;
                            inventoryProductItem.CodeCUM = null;
                            inventoryProductItem.CodeAlternative =string.IsNullOrEmpty(item.CodeAlternative)? parameters.CodeAlternative : item.CodeAlternative  ;
                            inventoryProductItem.CodeAlternativeTwo = parameters.CodeAlternative;
                            inventoryProductItem.Description = item.ProductName;
                            inventoryProductItem.ProductGroupId = parameters.ProductGroupId;
                            inventoryProductItem.ProductSubGroupId = parameters.ProductSubGroupId;
                            inventoryProductItem.MeasurementUnitId = parameters.MeasurementUnitId;
                            inventoryProductItem.PackagingUnitId = parameters.PackagingUnitId;
                            inventoryProductItem.ManufacturerId = parameters.ManufacturerId;
                            inventoryProductItem.IVAId = null;
                            inventoryProductItem.Presentation = null;
                            inventoryProductItem.CodeSICE = null;
                            inventoryProductItem.HandlesSerial = false;
                            inventoryProductItem.HandlesHealthRegistration = false;
                            inventoryProductItem.HealthRegistration = null;
                            inventoryProductItem.ExpirationDate = null;
                            inventoryProductItem.BillingGroupId = parameters.BillingGroupId;
                            inventoryProductItem.ProductControl = false;
                            inventoryProductItem.ProductWithPriceControl = false;
                            inventoryProductItem.POSProduct = true;
                            inventoryProductItem.AuthorizationByOrderNumber = null;
                            inventoryProductItem.ExpirationDay = null;
                            inventoryProductItem.MaximumControlPeriod = false;
                            inventoryProductItem.ControlDays = null;
                            inventoryProductItem.ControlOrderQuantity = false;
                            inventoryProductItem.ProductOrderAmount = null;
                            inventoryProductItem.LastPurchase = null;
                            inventoryProductItem.LastSale = null;
                            inventoryProductItem.ProductOrigin = null;
                            inventoryProductItem.MinimumStock = 0;
                            inventoryProductItem.MaximumStock = 999;
                            inventoryProductItem.CommissionPercentage = null;
                            inventoryProductItem.RepositionPoint = null;
                            inventoryProductItem.ResetTime = null;
                            inventoryProductItem.CurrencyType = 1;
                            inventoryProductItem.ProductCost = Math.Round((item.UnitValue / tRMValue.Value),2);
                            inventoryProductItem.FinalProductCost = Math.Round((item.UnitValue / tRMValue.Value));
                            inventoryProductItem.SellingPrice = 0;
                            inventoryProductItem.AllPOSPathologies = null;
                            inventoryProductItem.Status = true;
                            inventoryProductItem.CreationUser = parameters.CreationUser;
                            inventoryProductItem.CreationDate = DateTime.Now;
                            inventoryProductItem.ModificationDate = null;
                            inventoryProductItem.ModificationUser = null;
                            inventoryProductItem.BillingGroupNoPosId = null;
                            inventoryProductItem.ControlCostPercentage = null;
                            inventoryProductItem.InventoryRiskLevelId = null;
                            inventoryProductItem.SerialNumber = null;
                            inventoryProductItem.DriveUnit = null;
                            inventoryProductItem.MinimumTemperature = null;
                            inventoryProductItem.MaximumTemperature = null;
                            inventoryProductItem.SanitaryRegistration = null;
                            inventoryProductItem.Consumption = null;
                            inventoryProductItem.JustificationSuppliesDispositives = null;
                            inventoryProductItem.Abbreviation = null;
                            inventoryProductItem.OsteosynthesisMaterial = null;
                            inventoryProductItem.SupplieId = supplieId;
                            inventoryProductItem.IUM = null;
                            inventoryProductItem.Storage = 0;
                            inventoryProductItem.TaxedProduct = false;
                            inventoryProductItem.Osmolarity = 0;

                            var resultProduct = await _inventoryProductAdminService.SaveInventoryProductAsync(inventoryProductItem, audit);
                            product = resultProduct.ObjectEmbbeded;
                            if (resultProduct?.StateResult is null || !resultProduct.StateResult)
                            {
                                transaction.Dispose();
                                errors.AppendLine($"El producto {item.ProductCode} no se pudo crear porque {resultProduct.Message}");
                                continue;
                            }
                            else
                            {
                                product = _inventoryProductAdminService.GetInventoryProduct(resultProduct?.ObjectEmbbeded.Code, audit)?.ObjectEmbbeded;
                                item.ProductId = product.Id;
                            }
                        }
                        else
                        {
                            item.ProductId = product.Id;
                        }

                    
                        
                        
                        int rateCount = 0;
                        if (productRates.Count > 0)
                        {
                            foreach (var itemProdu in productRates)
                            {
                                rateCount++;
                                Domain.Entities.ProductRateDetail _productRateDetail;

                                
                                var rateDetailsList = rateDetailsCache.ContainsKey(itemProdu.Id) ? rateDetailsCache[itemProdu.Id] : new List<Domain.Entities.ProductRateDetail>();
                                _productRateDetail = rateDetailsList.Find(x => x.ProductId == item.ProductId);
                                if(_productRateDetail == null)
                                {
                                    _productRateDetail = new Domain.Entities.ProductRateDetail();
                                }
                                //se consulta las tarifas con condiciones desde el cache
                                var conditionsRate = conditionsCache.ContainsKey(itemProdu.Id) ? conditionsCache[itemProdu.Id] : new List<ProductRateGeneral>();
                                var valueWithTRM = Math.Round((item.UnitValue / tRMValue.Value),2);
                                foreach(var conditions in conditionsRate)
                                {
                                    //en caso de que tenga condiciones o no
                                    if (conditions.ConditionType==1)
                                    {
                                        if (conditions.RateType ==1)
                                        {
                                            //se toma valor para aumentar al producto
                                            _productRateDetail.SalesValue = Math.Round(valueWithTRM + conditions.SalesValue,2, MidpointRounding.AwayFromZero);
                                            _productRateDetail.SalesValueWithSurcharge = Math.Round(valueWithTRM + conditions.SalesValue,2);
                                        }
                                        else
                                        {
                                            //se toma porcentaje para aumentar al producto
                                            _productRateDetail.SalesValue = Math.Round((valueWithTRM + (valueWithTRM *(conditions.Percentage.Value / 100))),2, MidpointRounding.AwayFromZero);
                                            _productRateDetail.SalesValueWithSurcharge = Math.Round((valueWithTRM + (valueWithTRM * (conditions.Percentage.Value / 100))),2, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                    else
                                    {
                                        var subtotal = valueWithTRM * item.Quantity;
                                        if (conditions.RateType == 1)
                                        {
                                            //se  valida que el costo unitario este dentro del rango de las condiciones
                                            foreach(var conditionDetail in conditions.ProductRateGeneralCondition)
                                            {
                                                if (subtotal >= conditionDetail.InitialValue && subtotal <= conditionDetail.EndValue)
                                                {
                                                    _productRateDetail.SalesValue = Math.Round(valueWithTRM + conditionDetail.SalesValue,2, MidpointRounding.AwayFromZero);
                                                    _productRateDetail.SalesValueWithSurcharge = Math.Round(valueWithTRM + conditionDetail.SalesValue,2, MidpointRounding.AwayFromZero);
                                                }
                                            }
                                        
                                        }
                                        else
                                        {
                                            //se  valida que el costo unitario este dentro del rango de las condiciones
                                            foreach (var conditionDetail in conditions.ProductRateGeneralCondition)
                                            {
                                                if (subtotal >= conditionDetail.InitialValue && subtotal <= conditionDetail.EndValue)
                                                {
                                                    _productRateDetail.SalesValue = Math.Round((valueWithTRM + (valueWithTRM * (conditionDetail.Percentage.Value / 100))),2, MidpointRounding.AwayFromZero);
                                                    _productRateDetail.SalesValueWithSurcharge = Math.Round((valueWithTRM + (valueWithTRM * (conditions.Percentage.Value / 100))),2, MidpointRounding.AwayFromZero);
                                                }
                                            }
                                        
                                        }
                                    }

                                }

                                if (_productRateDetail.SalesValue == null || _productRateDetail.SalesValue == 0)
                                {
                                    //se toma porcentaje para aumentar al producto
                                    _productRateDetail.SalesValue = valueWithTRM;
                                    _productRateDetail.SalesValueWithSurcharge = valueWithTRM;
                                }
                                _productRateDetail.ProductRateId = itemProdu.Id;
                                _productRateDetail.ProductId = item.ProductId;
                                _productRateDetail.InitialDate = ProductInTransit.CreationDate.AddDays(-1);
                                _productRateDetail.EndDate = ProductInTransit.CreationDate.AddMonths(12);
                                _productRateDetail.Contracted = true;
                                _productRateDetail.Quoted = false;
                                _productRateDetail.Observations = "";
                                _productRateDetail.LiquidationType = 1;
                                _productRateDetail.RateType = 1;
                                _productRateDetail.RateClass = 1;
                                _productRateDetail.PercentageBasedOn = 0;
                                _productRateDetail.Percentage = null;
                                _productRateDetail.CupsId = null;
                                _productRateDetail.ContractDescriptionId = null;
                                _productRateDetail.PackageId = null;
                                _productRateDetail.DoseType = null;
                                _productRateDetail.Status = 1;
                                
                                itemProdu.ProductRateDetail.Add(_productRateDetail);
                               
                            }
                        }
                    }

                   
                    foreach (var itemProdu in productRates)
                    {
                        var saveRate = await _productTemplateAdminService.SaveProductTemplate(itemProdu, audit);
                        if (saveRate?.StateResult is null || !saveRate.StateResult)
                        {
                            errors.AppendLine($"No se pudo guardar la tarifa {itemProdu.Code}");
                        }
                    }

                    if (errors.Length > 0)
                    {
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = errors.ToString() };
                    }

                    var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(ProductInTransit.WarehouseId);
                    if (warehouse != null && warehouse.WarehouseConsignment == true)
                    {
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = "No es posible realizar una remisión de entrada con un almacén de consignación." };
                    }

                    List<string> MessagesStock = default(List<string>);
                    MessagesStock = new List<string>();
                    //Se crea la remision
                    Domain.Entities.RemissionEntrance newRemission = new Domain.Entities.RemissionEntrance();
                    newRemission.RemissionDate = ProductInTransit.DocumentDate;
                    newRemission.OperatingUnitId = ProductInTransit.OperatingUnitId;
                    newRemission.SupplierId = ProductInTransit.SupplierId;
                    newRemission.SupplierDistributionLineId = ProductInTransit.SupplierDistributionLineId;
                    newRemission.WarehouseId = ProductInTransit.WarehouseId;
                    newRemission.RemissionNumber = ProductInTransit.ReferenceNumber;
                    newRemission.Description = ProductInTransit.Description;
                    newRemission.Value = ProductInTransit.Value;
                    newRemission.IvaValue = ProductInTransit.IvaValue;
                    newRemission.TotalValue = ProductInTransit.TotalValue;
                    newRemission.Status = ProductInTransit.Status;
                    newRemission.ProductStatus = ProductInTransit.ProductStatus;
                    newRemission.CreationUser = ProductInTransit.CreationUser;
                    newRemission.CreationDate = ProductInTransit.DocumentDate;
                    newRemission.CurrencyId = ProductInTransit.CurrencyId;
                    newRemission.Prefix = ProductInTransit.Prefix;
                    newRemission.RemissionEntranceDetail = new Domain.Entities.TrackableCollection<Domain.Entities.RemissionEntranceDetail>();

                    foreach (var item in ProductInTransit.ProductInTransitDetail)
                    {
                        Domain.Entities.RemissionEntranceDetail detail = new Domain.Entities.RemissionEntranceDetail();
                        detail.RemissionSource = 4;
                        detail.SourceCode = ProductInTransit.Code;
                        detail.PurchaseOrderDetailId = null;
                        detail.ContractDetailId = null;
                        detail.ProductId = item.ProductId.Value;
                        detail.Quantity = item.Quantity;
                        detail.UnitValue = item.UnitValue;
                        detail.LastValue = 0;
                        detail.SubTotalValue = item.SubTotalValue;
                        detail.IvaValue = item.IvaValue;
                        detail.TotalValue = item.TotalValue;
                        detail.ConsignmentInventoryRemissionDetailBatchSerialId = null;
                        detail.GrossUnitValue = item.GrossUnitValue;
                        detail.DiscountPercentage = item.DiscountPercentage;
                        detail.NetDiscount = item.NetDiscount;
                        newRemission.RemissionEntranceDetail.Add(detail);
                        detail.RemissionEntranceDetailBatchSerial = new Domain.Entities.TrackableCollection<Domain.Entities.RemissionEntranceDetailBatchSerial>();

                        Domain.Entities.RemissionEntranceDetailBatchSerial detailBatch = new Domain.Entities.RemissionEntranceDetailBatchSerial();
                        detailBatch.BatchSerialId = null;
                        detailBatch.Quantity = item.Quantity;
                        detailBatch.OutstandingQuantity = item.Quantity;
                        detail.RemissionEntranceDetailBatchSerial.Add(detailBatch);
                    }

                    //eliminamos doc. de control de inventarios
                    InventoryControlDocument inventoryControlDocument = await _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumberAsync(ProductInTransit.Code, (int)eTypeDocumentsControlInventory.ProductIntransit);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }

                    var sequenceRemission = _inventorySequenceAdminService.GetSequenseByIdForm("317");
                    var remissionDetailSequenceId = 0;
                    if (sequenceRemission.IdSequence == 0)
                    {
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = "No se encontró secuencia numerica para remisión de entrada" };
                    }
                    if (sequenceRemission.Scope != "OU")
                    {
                        var sequenceRemissionDetail = _inventorySequenceRepository.GetSequenseByPrefix(newRemission.Prefix, sequenceRemission.Id);
                        if (sequenceRemissionDetail.Id == 0)
                        {
                            return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = "No se encontró secuencia numerica para remisión de entrada" };
                        }
                        remissionDetailSequenceId = sequenceRemissionDetail.Id;
                    }
                    else
                    {
                        remissionDetailSequenceId = sequenceRemission.InventorySequenceDetail[0].Id;
                    }
                    
                    var remissionEntranceResult = _remissionEntranceAdminService.SaveAndConfirmbRemissionEntrance(newRemission, audit, remissionDetailSequenceId, Infrastructure.CrossCutting.Audit.Actions.Insert, sequenceRemission, false);

                    if (remissionEntranceResult?.StateResult is null || !remissionEntranceResult.StateResult)
                    {
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, StateResultAux = false, Message = remissionEntranceResult.MessageResult.ToString() };
                    }

                    ProductInTransit.Status = 2;
                    ProductInTransit.ConfirmationDate = DateTime.Now;
                    ProductInTransit.ConfirmationUser = audit.CodeUser;
                    ProductInTransit.ModificationDate = DateTime.Now;
                    ProductInTransit.ModificationUser = audit.CodeUser;
                    ProductInTransit.MarkAsModified();
                    _ProductInTransitRepository.SaveEntity(ProductInTransit);
                    ProductInTransitUnitWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ProductInTransit>(ProductInTransit, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, ProductInTransit.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = true, ObjectEmbbeded = ProductInTransit, Message = "Se confirmó correctamente el cargo de productos en transito" };
                }
                catch (Exception ex)
                {
                    //ProductInTransitUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.ProductInTransit>
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
        /// <param name="ProductInTransit"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ActionResult<Domain.Entities.ProductInTransit>> SaveAndConfirmbProductInTransitAsync(Domain.Entities.ProductInTransit ProductInTransit, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false)
        {
            var result = await SaveProductInTransitAsync(ProductInTransit, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = await ConfirmProductInTransitAsync(result.ObjectEmbbeded, audit, controlCost);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };

                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = resultConfirm.Message };
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
                                return new ActionResult<Domain.Entities.ProductInTransit> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.ProductInTransit> { MessageResult = resultConfirm.MessageResult, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                            }
                        }
                        else
                        {
                            if (resultConfirm.MessageResult == null || resultConfirm.MessageResult.Count == 0)
                            {
                                return new ActionResult<Domain.Entities.ProductInTransit> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.ProductInTransit> { MessageResult = resultConfirm.MessageResult, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                            }
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    return new ActionResult<Domain.Entities.ProductInTransit> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
                else
                {
                    return new ActionResult<Domain.Entities.ProductInTransit> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
            }
        }

        public ActionResult<List<Domain.Entities.ProductInTransitDetail>> SetCopyPasteOrImportFileProductInTransit(List<ImportFileRow> dataImportFile, List<List<string>> dataCopyPaste)
        {
            try
            {
                return _inventoryServices.SetCopyPasteOrImportFileProductInTransit(dataImportFile, dataCopyPaste);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.ProductInTransitDetail>> { StatusCode = eStatusResult.EXCEPTION, Message = ex.Message };
            }

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
                _ProductInTransitRepository = null;
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
