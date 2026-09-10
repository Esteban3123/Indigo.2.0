///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Carlos Ernesto cordoba
/// Created          : 21-09-2015
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
using Application.Portfolio;
using Application.Accounting;
using Application.Treasury;
using Domain.Payroll;
using System.Linq;
using Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial;
using Application.Inventory.RemissionOutput;

namespace Application.Inventory.DocumentInvoiceProductSales
{
    public class DocumentInvoiceProductSalesAdminService : IDocumentInvoiceProductSalesAdminService
    {
        private IDocumentInvoiceProductSalesRepository _documentInvoiceProductSalesRepository;
        private IDocumentInvoiceProductSalesDetailRepository _documentInvoiceProductSalesDetailRepository;
        private IBillingSequenceDetailRepository _sequenseDetailRepository;
        private IBillingSequenceRepository _sequenseRepository;
        private IInventoryService _inventoryServices;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IAccountReceivableAdminService _accountReceivableAdminService;
        private IAccountReceivableRepository _accountReceivableRepository;
        private IInvoiceRepository _invoiceRepository;
        private ISettingsBillingRepository _settingsBillingRepository;
        private IPUCRepository _mainAccountRepository;
        private IInventoryProductRepository _productRepository;
        private IAccountingDocumentAdminService _accountingAdminService;
        private IAccountingDocumentRepository _accountingRepository;
        private ICashReceiptsAdminService _cashReceiptsAdminService;
        private ISequenseTreasuryCRepository _sequenseTreasuryRepository;
        private IBillingAuthorizationRepository _billingAuthorizationRepository;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private ISequensePortfolioCRepository _sequensePortfolioCRepository;
        private IFunctionalUnitRepository _functionalUnitRepository;
        private IWarehouseRepository _warehouseRepository;
        private IPaymentsConceptRepository _paymentsConceptRepository;
        private ICashReceiptConceptRepository _cashReceiptConceptRepository;
        private IThirdPartyRepository _thirdPartyRepository;
        private IPortfolioSequenseAdminService _portFolioSequenceAdminService;
        private IPortfolioTransferRepository _portfolioTransferRepository;
        private IPortfolioTransfersAdminService _portfolioTransferAdminService;
        private IDocumentTypeRepository _documentTypeRepository;
        private ICashReceiptsRepository _cashReceipsRepository;
        private IRetentionConceptRepository _retentionRepository;
        private ISettingsAccountRepository _settingsAccountRepository;
        private IOperatingUnitRepository _operatingUnitRepository;
        private IElectronicDocumentRepository _electronicDocumentRepository;
        private IBillingNoteRepository _billingNoteRepository;
        private IBillingReversalReasonRepository _billingReversalReasonRepository;
        private IRemissionOutputDetailPhysicalRepository _remissionOutputDetailPhysicalRepository;
        private ICompanySettingsRepository _companySettingsRepository;
        private ICustomerRepository _customerRepository;

        //Presupuesto
        private IBudgetSequenceRepository _budgetSequenceRepository;

        private IBudgetRepository _budgetRepository;
        private IBudgetItemRepository _categoryRepository;
        private BudgetSequence sequenceBudget = null;
        private IRecognitionRepository _recognitionRepository;

        //Remisión de inventario en consignación
        private IConsignmentInventoryRemissionDetailBatchSerialAdminService _consignmentInventoryRemissionDetailBatchSerialAdminService;
        

        public DocumentInvoiceProductSalesAdminService(IDocumentInvoiceProductSalesRepository documentInvoiceProductSalesRepository, IBillingSequenceDetailRepository sequenseDetailRepository,
            IInventoryService inventoryServices, ISettingInventoryRepository settingInventoryRepository, IAccountReceivableAdminService accountReceivableAdminService, IInvoiceRepository invoiceRepository,
            IBillingSequenceRepository sequenseRepository, ISettingsBillingRepository settingsBillingRepository, IPUCRepository mainAccountRepository, IInventoryProductRepository productRepository,
            ICashReceiptsAdminService cashReceiptsAdminService, ISequenseTreasuryCRepository sequenseTreasuryRepository, IBillingAuthorizationRepository billingAuthorizationRepository,
            IPhysicalInventoryRepository physicalInventoryRepository, IPhysicalInventoryAdminService physicalInventoryAdminService, ISequensePortfolioCRepository sequensePortfolioCRepository,
            IAccountingDocumentAdminService accountingAdminService, IFunctionalUnitRepository functionalUnitRepository, IWarehouseRepository warehouseRepository, IPaymentsConceptRepository paymentsConceptRepository,
            IBudgetSequenceRepository budgetSequenceRepository, IBudgetRepository budgetRepository, IBudgetItemRepository categoryRepository, IRecognitionRepository recognitionRepository,
            ICashReceiptConceptRepository cashReceiptConceptRepository, IThirdPartyRepository thirdPartyRepository,
            IConsignmentInventoryRemissionDetailBatchSerialAdminService consignmentInventoryRemissionDetailBatchSerialAdminService,
            IAccountReceivableRepository accountReceivableRepository, IPortfolioSequenseAdminService portFolioSequenceAdminService,
            IPortfolioTransferRepository portfolioTransferRepository, IPortfolioTransfersAdminService portfolioTransferAdminService,
            IDocumentTypeRepository documentTypeRepository, IAccountingDocumentRepository accountingRepository,
            ICashReceiptsRepository cashReceipsRepository, IRetentionConceptRepository retentionRepository,
            IDocumentInvoiceProductSalesDetailRepository documentInvoiceProductSalesDetailRepository, ISettingsAccountRepository settingsAccountRepository,
            IOperatingUnitRepository operatingUnitRepository, IElectronicDocumentRepository electronicDocumentRepository, IBillingNoteRepository billingNoteRepository, 
            IBillingReversalReasonRepository billingReversalReasonRepository, IRemissionOutputDetailPhysicalRepository RemissionOutputDetailPhysicalRepository, ICompanySettingsRepository CompanySettingsRepository,
            ICustomerRepository customerRepository)
        {
            if (documentInvoiceProductSalesRepository == null)
            {
                throw new ArgumentNullException("documentInvoiceProductSalesRepository");
            }
            if (sequenseDetailRepository == null)
            {
                throw new ArgumentNullException("sequenseDetailRepository");
            }
            _cashReceipsRepository = cashReceipsRepository;
            _accountingRepository = accountingRepository;
            _documentTypeRepository = documentTypeRepository;
            _portfolioTransferAdminService = portfolioTransferAdminService;
            _portfolioTransferRepository = portfolioTransferRepository;
            _portFolioSequenceAdminService = portFolioSequenceAdminService;
            _accountReceivableRepository = accountReceivableRepository;
            _budgetSequenceRepository = budgetSequenceRepository;
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _recognitionRepository = recognitionRepository;
            _documentInvoiceProductSalesRepository = documentInvoiceProductSalesRepository;
            _sequenseDetailRepository = sequenseDetailRepository;
            _inventoryServices = inventoryServices;
            _settingInventoryRepository = settingInventoryRepository;
            _accountReceivableAdminService = accountReceivableAdminService;
            _invoiceRepository = invoiceRepository;
            _sequenseRepository = sequenseRepository;
            _settingsBillingRepository = settingsBillingRepository;
            _mainAccountRepository = mainAccountRepository;
            _productRepository = productRepository;
            _cashReceiptsAdminService = cashReceiptsAdminService;
            _sequenseTreasuryRepository = sequenseTreasuryRepository;
            _billingAuthorizationRepository = billingAuthorizationRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _sequensePortfolioCRepository = sequensePortfolioCRepository;
            _accountingAdminService = accountingAdminService;
            _functionalUnitRepository = functionalUnitRepository;
            _warehouseRepository = warehouseRepository;
            _paymentsConceptRepository = paymentsConceptRepository;
            _cashReceiptConceptRepository = cashReceiptConceptRepository;
            _retentionRepository = retentionRepository;
            _thirdPartyRepository = thirdPartyRepository;
            _consignmentInventoryRemissionDetailBatchSerialAdminService = consignmentInventoryRemissionDetailBatchSerialAdminService;
            _documentInvoiceProductSalesDetailRepository = documentInvoiceProductSalesDetailRepository;
            _settingsAccountRepository = settingsAccountRepository;
            _operatingUnitRepository = operatingUnitRepository;
            _electronicDocumentRepository = electronicDocumentRepository;
            _billingNoteRepository = billingNoteRepository;
            _billingReversalReasonRepository = billingReversalReasonRepository;
            _remissionOutputDetailPhysicalRepository =RemissionOutputDetailPhysicalRepository;
            _companySettingsRepository = CompanySettingsRepository;
            _customerRepository = customerRepository;
        }

        public Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesByCode(string code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales = _documentInvoiceProductSalesRepository.GetDocumentInvoiceProductSalesByCode(code.Trim());
                if (documentInvoiceProductSales != null && documentInvoiceProductSales.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.DocumentInvoiceProductSales> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.DocumentInvoiceProductSales>(documentInvoiceProductSales, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return documentInvoiceProductSales;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.DocumentInvoiceProductSales();
            }
        }

        public Domain.Entities.DocumentInvoiceProductSales GetDocumentInvoiceProductSalesById(int id)
        {
            try
            {
                return _documentInvoiceProductSalesRepository.GetDocumentInvoiceProductSalesById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.DocumentInvoiceProductSales();
            }
        }

        public ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, AuditMessage audit, long idSequence = 0, BillingSequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWorkSequense = _sequenseDetailRepository.UnitWork;
                IUnitWork unitOfWorkInvoiceProduct = _documentInvoiceProductSalesRepository.UnitWork;
                try
                {
                    var inventoryServices = new InventoryServices(_settingInventoryRepository);
                    var resultValidatePeriod = inventoryServices.ValidateInventoryPeriod(documentInvoiceProductSales.DocumentDate, documentInvoiceProductSales.OperatingUnitId);
                    if (resultValidatePeriod.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = true, Message = resultValidatePeriod.Message };
                    }
                    
                    if (documentInvoiceProductSales.Status != 3)
                    {
                        //Se valida que el documento tenga al menos un detalle
                        if (documentInvoiceProductSales.DocumentInvoiceProductSalesDetail == null || documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Count() == 0 || documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Where(d => d.ChangeTracker.State != ObjectState.Deleted).Count() == 0)
                        {
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = false, Message = "La factura no tiene detalles" };
                        }
                    }

                    var currentDetails = new List<Domain.Entities.DocumentInvoiceProductSalesDetail>();
                    if (documentInvoiceProductSales.Id > 0)
                    {
                        currentDetails = _documentInvoiceProductSalesDetailRepository.ListDocumentInvoiceProductSalesDetailByIdDocumentInvoiceProductSales(documentInvoiceProductSales.Id, false);
                    }
                    //Validamos los parametros del tenat 
                    var setting = _companySettingsRepository.GetCompanySettings();
                    
                    List<String> errors = new List<string>();
                    if (setting.TransactionEconomicActivity == true)
                    {
                        var economicActivityDetail = AddEconomicActivity(documentInvoiceProductSales);
                        if (!economicActivityDetail.StateResult)
                        {
                            errors = economicActivityDetail.MessageResult;
                        }                                                                                             
                    }

                    //Se valida que el producto solo se ha agregado una vez
                    foreach (var detail in documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Where(d => d.ChangeTracker.State == ObjectState.Added))
                    {                        
                        //validar en el actual detalle
                        if (currentDetails.Where(c => c.ProductId == detail.ProductId && (c.SourceCode == null || c.SourceCode == detail.SourceCode) && documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Where(dd => c.Id == dd.Id && dd.ChangeTracker.State == ObjectState.Deleted).Count() != 1).Count() > 0)
                        {
                            errors.Add("El producto '" + detail.CodeNameProduct + "' ya se encuentra agregado en el detalle de la factura.");
                        }

                        //validar en el detalle enviado
                        if (documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Where(d => d.ChangeTracker.State == ObjectState.Added && d.ProductId == detail.ProductId && (d.SourceCode == null || d.SourceCode == detail.SourceCode)).Count() > 1)
                        {
                            errors.Add("El producto '" + detail.CodeNameProduct + "' se encuentra duplicado dentro del detalle de la factura.");
                        }
                    }

                    if (errors.Count > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = false, MessageResult = errors };
                    }
                    
                    if (documentInvoiceProductSales.Code == null || documentInvoiceProductSales.Code.Trim().Equals(string.Empty))
                    {
                        BillingSequenceDetail seq = this._sequenseDetailRepository.GetSequenseDById(Convert.ToInt32(idSequence));
                        if (seq != null && seq.Id > 0 && seq.BillingSequence.Sequential)
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                documentInvoiceProductSales.Code = res;
                                seq.Next += 1;
                                this._sequenseDetailRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }

                    Domain.Entities.DocumentInvoiceProductSales auxDocumentInvoiceProductSales = null;
                    IndigoAuditSimpleEntity<Domain.Entities.DocumentInvoiceProductSales> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (documentInvoiceProductSales.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        // La fecha de la factura para facturación electrónica debe ser la fecha y hora actual
                        documentInvoiceProductSales.DocumentDate = DateTime.Now;
                        documentInvoiceProductSales.CreationUser = audit.CodeUser;
                        documentInvoiceProductSales.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        if (documentInvoiceProductSales.Status == 3)
                        {
                            auxDocumentInvoiceProductSales = documentInvoiceProductSales.OriginalValue;
                            documentInvoiceProductSales.AnnulmentUser = audit.CodeUser;
                            documentInvoiceProductSales.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }
                        else
                        {
                            auxDocumentInvoiceProductSales = documentInvoiceProductSales.OriginalValue;
                            // La fecha de la factura para facturación electrónica debe ser la fecha y hora actual
                            documentInvoiceProductSales.DocumentDate = DateTime.Now;
                            documentInvoiceProductSales.ModificationUser = audit.CodeUser;
                            documentInvoiceProductSales.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }
                    }

                    _documentInvoiceProductSalesRepository.SaveEntity(documentInvoiceProductSales);
                    unitOfWorkInvoiceProduct.CommitAndRefreshChanges();
                    unitOfWorkSequense.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.DocumentInvoiceProductSales>(documentInvoiceProductSales, audit, status, auxDocumentInvoiceProductSales);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = true, ObjectEmbbeded = documentInvoiceProductSales };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWorkInvoiceProduct.RollbackChanges();
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWorkInvoiceProduct.RollbackChanges();
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
            }
        }

        /// <summary>
        /// confirmar una factura
        /// </summary>
        /// <param name="documentInvoiceProductSales"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DocumentInvoiceProductSales> ConfirmDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, CashReceipts cashReceipts, AuditMessage audit, SessionValues session)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWorkInvoiceProduct = _documentInvoiceProductSalesRepository.UnitWork;
                IUnitWork unitOfWorkInvoice = _invoiceRepository.UnitWork;
                try
                {
                    //Consulto los parametros de Contabilidad definidos para la unidad operativa
                    var settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(documentInvoiceProductSales.OperatingUnitId);
                    if (settingsAccount == null || settingsAccount.Id == 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "No se encontro parametros de contabilidad para la unidad operativa seleccionada" };
                    }

                    //Consulto los parametros de Facturacion definidos para la unidad operativa
                    var billingSetting = _settingsBillingRepository.GetSettingsBillingByIdUnitOperative(documentInvoiceProductSales.OperatingUnitId, false);
                    if (billingSetting == null || billingSetting.Id == 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "No se encontro parametros de facturación para la unidad operativa seleccionada" };
                    }

                    var billingAuthorization = new BillingAuthorization();
                    bool IsElectronicTicket = false;
                    int JournalVoucherType = billingSetting.ProductInvoiceJournalVoucherTypeId;
                    var customerThirdParty = _thirdPartyRepository.GetThirdPartyById(documentInvoiceProductSales.ThirdPartyId, false);
                    if (billingSetting.ApplyElectronicSalesTicket == true && customerThirdParty.ElectronicBiller == false)
                    {
                        billingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(billingSetting.AccountingVoucherGenerationId.Value);
                        JournalVoucherType = billingSetting.AccountingVoucherGenerationId.Value;
                        IsElectronicTicket = true;
                    }
                    else
                    {
                        //Consulto la Autorizacion de Facturacion Asociada a la Factura de Producto
                        billingAuthorization = _billingAuthorizationRepository.GetBillingAuthorizationById(documentInvoiceProductSales.BillingAuthorizationId);
                    }

                    //Valido existencia de la autorizacion de facturacion y que esta se encuentre activa
                    if (billingAuthorization == null || billingAuthorization.Id == 0 || billingAuthorization.Status == false)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "No se encontró la autorización de facturación o esta se encuentra inactiva" };
                    }

                    //Valido existencia de consecutivos
                    if (billingAuthorization.Consecutive == billingAuthorization.FinalInvoice)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "La autorización de facturación " + billingAuthorization.Name + " llego al número maximo de consecutivos" };
                    }
                    //Valido que la Autorizacion de Facturacion se encuentre vigente
                    if (billingAuthorization.InitialDate != null && billingAuthorization.FinalDate != null && (documentInvoiceProductSales.DocumentDate < billingAuthorization.InitialDate || documentInvoiceProductSales.DocumentDate >= billingAuthorization.FinalDate))
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "La autorización de facturación " + billingAuthorization.Name + " no se encuentra vigente" };
                    }
                    //Valido que, si la facturacion electronica se encuentra habilitada, y la autorizacion es de tipo electronica, esta tenga asignado un codigo
                    if (settingsAccount.HandlesElectronicBilling == true && billingAuthorization.InvoiceType == 3 && String.IsNullOrEmpty(billingAuthorization.TechnicalKey))
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "No se ha parametrizado la clave tecnica en la autorización de facturación " + billingAuthorization.Name };
                    }

                    //Obtengo el almacén para determinar si es un almacén en consignación
                    var warehouse = _warehouseRepository.GetWarehouseById(documentInvoiceProductSales.WarehouseId);
                    var supplierThirdParty = _thirdPartyRepository.GetThirdPartyById(settingsAccount.IdDian, false);

                    //Suma de subtotales con el fin de compararlos con la base mínima de retención
                    Decimal SumSubTotalValues = documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Sum(d => d.SubTotalValue);
                    Decimal SumSubTotalValueWhitDiscount = documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Sum(d => (d.SubTotalValue - d.DiscountValue));

                    //afecto el inventario fisico
                    StringBuilder errors = new StringBuilder();
                    foreach (var item in documentInvoiceProductSales.DocumentInvoiceProductSalesDetail)
                    {
                        var product = _productRepository.GetInventoryProductByIdWithProducGroup(item.ProductId, false);
                        decimal productCost = product.ProductCost;

                        //Recálculo de la retención en la fuente - Para evitar errores (BUG 1197)
                        item.RTFPercentage = 0;
                        item.RTFValue = 0;
                        item.InventoryProductCost = productCost;

                        if (customerThirdParty?.RetentionType ==2)
                        {
                            if (billingSetting.ApplyBasicBilling)
                            {
                                if( product?.ProductGroup?.ReteFuenteConceptId != null)
                                {
                                    var conceptRetention = _retentionRepository.GetRetentionById(product.ProductGroup.ReteFuenteConceptId.Value);

                                    if (conceptRetention is null)
                                    {
                                        errors.AppendLine("El producto " + item.CodeNameProduct + " No tiene un concepto de retención valido");
                                        continue;
                                    }

                                    if (SumSubTotalValueWhitDiscount >= conceptRetention.MinBase)
                                    {
                                        item.RTFPercentage = conceptRetention.Rate;
                                        item.RTFValue = (decimal)Utils.RoundValue((decimal)((item.SubTotalValue - item.DiscountValue) * (conceptRetention.Rate / 100)), 6);
                                    }
                                }
                            }
                            else 
                            {
                                var conceptAccountPayable = _paymentsConceptRepository.GetPaymentConceptById(product?.ProductGroup?.NotDeclarantRetentionAccountPayableConceptId.ToString(), false);

                                if (conceptAccountPayable?.RetentionConceptId is null)
                                {
                                    errors.AppendLine("El producto " + item.CodeNameProduct + " No tiene un concepto de retención valido");
                                    continue;
                                }

                                var conceptRetention = _retentionRepository.GetRetentionById(conceptAccountPayable.RetentionConceptId.Value);

                                if (conceptRetention is null)
                                {
                                    errors.AppendLine("El producto " + item.CodeNameProduct + " No tiene un concepto de retención valido");
                                    continue;
                                }

                                if (SumSubTotalValueWhitDiscount >= conceptRetention.MinBase)
                                {
                                    item.RTFPercentage = conceptRetention.Rate;
                                    item.RTFValue = (decimal)Utils.RoundValue((decimal)((item.SubTotalValue - item.DiscountValue) * (conceptRetention.Rate / 100)), 6);
                                }
                                
                            }
                        }
                        
                        if (item.DocumentInvoiceProductSalesDetailBatchSerial == null || item.DocumentInvoiceProductSalesDetailBatchSerial.Count == 0)
                        {
                            errors.AppendLine("El producto " + item.CodeNameProduct + " no tiene asignado un detalle");
                            continue;
                        }
                        else
                        {
                            foreach (var itemBach in item.DocumentInvoiceProductSalesDetailBatchSerial)
                            {
                                itemBach.OutstandingQuantity = itemBach.Quantity;
                                Domain.Entities.PhysicalInventory physical = _physicalInventoryRepository.GetPhysicalInventoryById(itemBach.PhysicalInventoryId);
                                List<Kardex> listKardex = new List<Kardex>()
                                {
                                    new Kardex()
                                    {
                                        ProductId = item.ProductId,
                                        WarehouseId = documentInvoiceProductSales.WarehouseId,
                                        BatchSerialId = physical.BatchSerialId,
                                        MovementType = 2,
                                        Quantity = itemBach.Quantity,
                                        Value = productCost,
                                        AffectInventory = true
                                    }
                                };
                                var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, documentInvoiceProductSales.Id, documentInvoiceProductSales.Code, documentInvoiceProductSales.GetType().Name, documentInvoiceProductSales.CreationUser);
                                if (result.StateResult == false)
                                {
                                    errors.AppendLine(result.Message);
                                    continue;
                                }

                                //Si es un almacén en consignación se debe marcar el producto como usado
                                if (warehouse.WarehouseConsignment == true)
                                {
                                    //Actualizar el detalle de la remisión
                                    result = _consignmentInventoryRemissionDetailBatchSerialAdminService.UpdateTheQuantityProductUsedInConsignmentInventoryRemission(item.Id, documentInvoiceProductSales.OperatingUnitId, documentInvoiceProductSales.FunctionalUnitId, documentInvoiceProductSales.WarehouseId, item.ProductId, physical.BatchSerialId, MovementTypeRemissionUsed.OutPut, itemBach.Quantity, productCost, documentInvoiceProductSales.Id, documentInvoiceProductSales.Code, documentInvoiceProductSales.GetType().Name, documentInvoiceProductSales.CreationUser, null);
                                    if (result.StateResult == false)
                                    {
                                        errors.AppendLine(result.Message);
                                        continue;
                                    }
                                }

                                // afecto el documento de remision de salida si la factura producto ha sido importada
                                if (!string.IsNullOrEmpty(item.SourceCode)) {
                                    var RemissionOutputDetailPhysical = _remissionOutputDetailPhysicalRepository.GetByFilter(x => x.RemissionOutputDetail.RemissionOutput.Code == item.SourceCode
                                                                                                                                && x.PhysicalInventoryId == itemBach.PhysicalInventoryId
                                                                                                                                && x.Id== itemBach.RemissionOutputPhysicalId, true, new List<string> { "RemissionOutputDetail.RemissionOutput" }).ToList();
                                    if (RemissionOutputDetailPhysical ==null || RemissionOutputDetailPhysical.Count == 0) {
                                        
                                            errors.AppendLine($"Habido un problema con el detale Importado desde Remission de salida : Porducto {item.CodeNameProduct}");
                                            continue;
                                        
                                    };

                                    int _quantity = itemBach.OutstandingQuantity;
                                    RemissionOutputDetailPhysical.ForEach(d => {

                                        if (_quantity > 0)
                                        {
                                            d.OutstandingQuantity = d.OutstandingQuantity - _quantity;
                                            _quantity = d.OutstandingQuantity;
                                        }
                                        else {
                                            return;
                                        };
                                        _remissionOutputDetailPhysicalRepository.SaveEntity(d);
                                    });
                                };

                            }
                        }
                    }

                    if (errors.Length > 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = errors.ToString() };
                    }

                    //Actualizo el valor de la retencion en la cabecera
                    documentInvoiceProductSales.RetentionSource = documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Sum(d => d.RTFValue);
                    documentInvoiceProductSales.TotalValue = documentInvoiceProductSales.Value + documentInvoiceProductSales.ValueTax -
                                                             documentInvoiceProductSales.ValueDiscount - documentInvoiceProductSales.WithholdingTax - documentInvoiceProductSales.WithholdingICA -
                                                             documentInvoiceProductSales.RetentionSource - documentInvoiceProductSales.DistrictTax + documentInvoiceProductSales.FreightIVAValue + documentInvoiceProductSales.FreightValue;

                    
                    var invoice = new Invoice()
                    {
                        OperatingUnitId = documentInvoiceProductSales.OperatingUnitId,
                        DocumentType = 7, //Factura de Venta de Productos
                        InvoiceNumber = String.Concat(billingAuthorization.InvoicePrefix, billingAuthorization.Consecutive),
                        ThirdPartyId = documentInvoiceProductSales.ThirdPartyId,
                        InvoiceDate = documentInvoiceProductSales.DocumentDate,
                        InvoiceExpirationDate = documentInvoiceProductSales.DocumentDate,
                        TotalInvoice = documentInvoiceProductSales.TotalValue,
                        ThirdPartySalesValue = documentInvoiceProductSales.TotalValue,
                        ThirdPartyDiscountValue = documentInvoiceProductSales.ValueDiscount,
                        ThirdPartyAccountReceivableValue = documentInvoiceProductSales.TotalValue,
                        ResponsibleRecoveryFee = 1,
                        Status = 1,
                        InvoicedUser = audit.CodeUser,
                        InvoicedDate = DateTime.Now,
                        OutputDate = DateTime.Now,
                        InitialDate = DateTime.Now,
                        BillingAuthorizationId = billingAuthorization.Id,
                        Observation = documentInvoiceProductSales.Description,
                        InvoiceValue = documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Sum(d => d.Quantity * d.SalePrice) - documentInvoiceProductSales.ValueDiscount,
                        ValueTax = documentInvoiceProductSales.ValueTax,
                        TotalValue = (documentInvoiceProductSales.Value + documentInvoiceProductSales.ValueTax - documentInvoiceProductSales.ValueDiscount + documentInvoiceProductSales.FreightIVAValue + documentInvoiceProductSales.FreightValue),
                        IsElectronicTicket = IsElectronicTicket
                    };

                    billingAuthorization.Consecutive += 1;

                    //Si se maneja facturación electrónica y la autorización de facturación es de tipo electrónica, se agregan los datos que debe incluir la factura
                    if (settingsAccount.HandlesElectronicBilling == true && billingAuthorization.InvoiceType == 3)
                    {
                        invoice.DianVersion = settingsAccount.DianVersion;
                        invoice.NitFE = supplierThirdParty.Person.IdentificationNumber;
                        invoice.TipAdq = customerThirdParty.Person.getAcquirerType();
                        invoice.NumAdq = customerThirdParty.Person.IdentificationNumber;
                        invoice.ClTec = billingAuthorization.TechnicalKey;
                        invoice.Environment = settingsAccount.Environment;
                        
                        invoice.CUFE = invoice.getCUFE();
                        invoice.QR = invoice.GetQRCode();
                    }

                    foreach (var item in documentInvoiceProductSales.DocumentInvoiceProductSalesDetail)
                    {
                        var invoiceDetail = new InvoiceDetailProductSales()
                        {
                            DocumentInvoiceProductSalesDetailId = item.Id,
                            Quantity = item.Quantity,
                            UnitSalesPrice = item.SalePrice,
                            TotalSalesPrice = item.Quantity * item.SalePrice,
                            Balance = item.Quantity * item.SalePrice
                        };
                        invoice.InvoiceDetailProductSales.Add(invoiceDetail);
                    }

                    _invoiceRepository.SaveEntity(invoice);
                    unitOfWorkInvoice.Commit();

                    var unitOfWorkBillingAuthorization = _billingAuthorizationRepository.UnitWork;
                    billingAuthorization.MarkAsModified();
                    _billingAuthorizationRepository.SaveEntity(billingAuthorization);
                    unitOfWorkBillingAuthorization.Commit();

                    //Creo el objeto de AccountReceivable para guardar en cartera
                    var accountReceivable = new AccountReceivable();
                    var sequence = _sequensePortfolioCRepository.GetSequenseByIdForm("682");
                    if (sequence.Id == 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "La secuencia para cuentas por cobrar no esta parametrizada o no es secuencial" };
                    }
                    var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequence.PortfolioSequenceDetail[0].Sequense.Pattern, sequence.PortfolioSequenceDetail[0].Next);
                    if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                    {
                        accountReceivable.Code = res;
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "La secuencia para cuentas por cobrar alcanzo su valor maximo" };
                    }
                    sequence.PortfolioSequenceDetail[0].Next += 1;
                    _sequensePortfolioCRepository.SaveEntity(sequence);
                    var sequenceUnitWork = _sequensePortfolioCRepository.UnitWork;
                    sequenceUnitWork.Commit();

                    accountReceivable.OperatingUnitId = documentInvoiceProductSales.OperatingUnitId;
                    accountReceivable.AccountReceivableType = 7;//factura de producto
                    accountReceivable.ThirdPartyId = documentInvoiceProductSales.ThirdPartyId;
                    accountReceivable.CustomerId = _accountReceivableRepository.GetCustomerIdByThirdPartyId(documentInvoiceProductSales.ThirdPartyId);
                    accountReceivable.InvoiceId = invoice.Id;
                    accountReceivable.InvoiceNumber = invoice.InvoiceNumber;
                    accountReceivable.AccountReceivableDate = documentInvoiceProductSales.DocumentDate;
                    
                    //Obtener el plazo de la factura para el cliente del cliente
                    var customer = _customerRepository.GetCustomerByThirdPartyId(documentInvoiceProductSales.ThirdPartyId, false);
                    accountReceivable.Term = customer != null && customer.Id > 0 ? customer.Term : 0;
                    
                    accountReceivable.ExpiredDate = documentInvoiceProductSales.DocumentDate;
                    accountReceivable.Observations = $"Cuenta por cobrar generada desde factura de producto {documentInvoiceProductSales.Code}";
                    accountReceivable.PortfolioStatus = 1;//sin radicar
                    accountReceivable.MainAccountWithoutFilingId = billingSetting.ProductSalesMainAccountId;
                    accountReceivable.AccountWithoutRadicateId = billingSetting.ProductSalesMainAccountId;
                    accountReceivable.NumberShares = 1;
                    accountReceivable.Value = documentInvoiceProductSales.TotalValue;
                    accountReceivable.Balance = documentInvoiceProductSales.TotalValue;
                    accountReceivable.Status = 2;//confirmado
                    accountReceivable.CreationUser = audit.CodeUser;
                    accountReceivable.CreationDate = DateTime.Now;
                    accountReceivable.ConfirmationUser = audit.CodeUser;
                    accountReceivable.ConfirmationDate = DateTime.Now;
                    //creo el detalle de AccountReceivableAccounting
                    var accountReceivableAccounting = new AccountReceivableAccounting()
                    {
                        MainAccountId = billingSetting.ProductSalesMainAccountId,
                        ThirdPartyId = documentInvoiceProductSales.ThirdPartyId,
                        Value = documentInvoiceProductSales.TotalValue,
                        Balance = documentInvoiceProductSales.TotalValue
                    };
                    accountReceivable.AccountReceivableAccounting.Add(accountReceivableAccounting);
                    //creo el detalle de AccountReceivableShare
                    var accountReceivableShare = new AccountReceivableShare()
                    {
                        Number = 1,
                        ExpiredDate = documentInvoiceProductSales.DocumentDate,
                        Value = documentInvoiceProductSales.TotalValue,
                        Balance = documentInvoiceProductSales.TotalValue
                    };
                    accountReceivable.AccountReceivableShare.Add(accountReceivableShare);
                    //guardo la cuenta por cobrar
                    var resultAccountRecivable = _accountReceivableAdminService.SaveAccountReceivable(accountReceivable, audit);
                    if (resultAccountRecivable.StateResult == false)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = resultAccountRecivable.Message };
                    }
                    //creo el documento contable para la factura
                    long consecutive = 0;
                    var productSalesCostCenterId = (billingSetting.ProductSalesCostCenterId == null) ? 0 : Convert.ToInt32(billingSetting.ProductSalesCostCenterId);
                    var generateJournalVoucherResult = GenerateJournalVourcher(documentInvoiceProductSales, invoice, audit, billingSetting.ProductSalesMainAccountId, productSalesCostCenterId, JournalVoucherType, billingSetting);
                    if (generateJournalVoucherResult.StateResult == false)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = generateJournalVoucherResult.Message };
                    }

                    ActionMessageResult<JournalVouchers> resultSaveJournalVoucher = _accountingAdminService.SaveAccountingDocument(generateJournalVoucherResult.ObjectEmbbeded, audit, true);
                    if (resultSaveJournalVoucher.StateResult == false)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = resultSaveJournalVoucher.Message };
                    }
                    else
                    {
                        consecutive = resultSaveJournalVoucher.ObjectEmbbeded.Consecutive;
                    }

                    //Guardo el recibo de caja pero primero le asigno la factura que se genero
                    if (cashReceipts != null)
                    {
                        var sequenceTreasury = _sequenseTreasuryRepository.GetSequenseByIdForm("635");
                        if (sequenceTreasury.Id == 0)
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "La secuencia para recibos de caja no esta parametrizada o no es secuencial" };
                        }
                        var resCashReceipt = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequenceTreasury.TreasurySequenceDetail[0].Sequense.Pattern, sequenceTreasury.TreasurySequenceDetail[0].Next);
                        if (resCashReceipt != null && !resCashReceipt.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            cashReceipts.Code = resCashReceipt;
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = "La secuencia para recibos de caja alcanzo su valor maximo" };
                        }
                        cashReceipts.AllowBudgetInterface = false;
                        sequenceTreasury.TreasurySequenceDetail[0].Next += 1;
                        _sequenseTreasuryRepository.SaveEntity(sequenceTreasury);
                        var sequenceTreasuryUnitWork = _sequenseTreasuryRepository.UnitWork;
                        sequenceTreasuryUnitWork.Commit();
                        foreach (var item in cashReceipts.CashReceiptDetails)
                        {
                            var cashReceiptAccountReceivable = new CashReceiptAccountReceivable()
                            {
                                AccountReceivableId = accountReceivable.Id,
                                InvoiceNumber = accountReceivable.InvoiceNumber,
                                Value = item.Value
                            };
                            item.CashReceiptAccountReceivable.Add(cashReceiptAccountReceivable);
                        }
                        var resultCashReceipts = _cashReceiptsAdminService.SaveAndConfirm(cashReceipts, audit, null);
                        if (resultCashReceipts.StateResult == false)
                        {
                            scope.Dispose();
                            if (resultCashReceipts.MessageResult != null)
                                return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = resultCashReceipts.MessageResult[0] };
                            else
                                return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = false, Message = resultCashReceipts.Message };
                        }
                    }

                    //proceso para afectar presupuesto
                    var ProductSalesCashReceiptConcept = _cashReceiptConceptRepository.GetCashReceiptConceptById(billingSetting.ProductSalesCashReceiptConceptId);
                    var recognition = new Recognition();
                    if (ProductSalesCashReceiptConcept.AffectBudget)
                    {
                        var resultGenerateSecuence = GenerateSequence();
                        if (resultGenerateSecuence.StateResult == false)
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales>
                            {
                                StateResult = false,
                                Message = resultGenerateSecuence.Message
                            };
                        }

                        var budget = _budgetRepository.GetBudgetByIdAsNotTracking(34);
                        var category = _categoryRepository.GetCategoryById(budget.CategoryId);

                        recognition.Code = resultGenerateSecuence.Message;
                        recognition.BudgetaryValidityId = category.BudgetaryValidityId;
                        recognition.Document = documentInvoiceProductSales.Code;
                        recognition.DocumentDate = documentInvoiceProductSales.DocumentDate;
                        recognition.Observations = $"Reconocimiento creado desde factura de producto - {recognition.Document}";
                        recognition.RecognitonType = 2;//Cuenta por cobrar
                        recognition.ThirdPartyId = documentInvoiceProductSales.ThirdPartyId;
                        recognition.DependencyId = 1;// preguntar de donde se obtiene
                        recognition.AutomaticCollection = false;
                        recognition.Applicant = "";
                        recognition.Status = 2;//confirmado
                        recognition.CreationUser = audit.CodeUser;
                        recognition.CreationDate = DateTime.Now;
                        recognition.ConfirmationUser = audit.CodeUser;
                        recognition.ConfirmationDate = DateTime.Now;

                        var recognitionDetail = new RecognitionDetail();
                        recognitionDetail.CategoryId = budget.CategoryId;
                        recognitionDetail.RevenueTypeId = budget.RevenueTypeId;
                        recognitionDetail.InitialValue = documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Sum(x => x.TotalValue);
                        recognitionDetail.TotalRecognition = recognitionDetail.InitialValue;
                        recognitionDetail.Balance = recognitionDetail.InitialValue;

                        recognition.RecognitionDetail.Add(recognitionDetail);

                        _recognitionRepository.SaveEntity(recognition);
                        _recognitionRepository.UnitWork.Commit();
                        sequenceBudget.BudgetSequenceDetail[0].Next += 1;
                        sequenceBudget.BudgetSequenceDetail[0].MarkAsModified();
                        _budgetSequenceRepository.SaveEntity(sequenceBudget);
                        _budgetSequenceRepository.UnitWork.Commit();
                        documentInvoiceProductSales.RecognitionId = recognition.Id;
                    }

                    //Termina el proceso de presupuesto
                    documentInvoiceProductSales.InvoiceId = invoice.Id;
                    documentInvoiceProductSales.Status = 2;
                    documentInvoiceProductSales.ConfirmationDate = DateTime.Now;
                    documentInvoiceProductSales.ConfirmationUser = audit.CodeUser;
                    documentInvoiceProductSales.ModificationDate = DateTime.Now;
                    documentInvoiceProductSales.ModificationUser = audit.CodeUser;
                    documentInvoiceProductSales.MarkAsModified();
                    _documentInvoiceProductSalesRepository.SaveEntity(documentInvoiceProductSales);
                    unitOfWorkInvoiceProduct.Commit();
                    new IndigoAuditSimpleEntity<Domain.Entities.DocumentInvoiceProductSales>(documentInvoiceProductSales, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, documentInvoiceProductSales.OriginalValue).Execute();

                    //Si se maneja facturación electrónica y la autorización de facturación es de tipo electrónica, creo el registro necesario para el envio de la factura electronica
                    if (settingsAccount.HandlesElectronicBilling == true && billingAuthorization.InvoiceType == 3)
                    {
                        string documentNumber = invoice.InvoiceNumber;
                        if (! String.IsNullOrEmpty(billingAuthorization.InvoicePrefix) )
                        {
                            documentNumber = invoice.InvoiceNumber.Replace(billingAuthorization.InvoicePrefix, "");
                        }

                        var electronicDocument = new ElectronicDocument()
                        {
                            DianVersion = settingsAccount.DianVersion,
                            OperatingUnitId = invoice.OperatingUnitId,
                            CustomerPartyId = customerThirdParty.Id,
                            EntityId = invoice.Id,
                            EntityName = invoice.GetType().Name,
                            DocumentDate = invoice.InvoiceDate,
                            DocumentType = invoice.DocumentType,
                            Status = 1,
                            CreationDate = DateTime.Now,
                            Container = session.TransactionalContainer,
                            Prefix = billingAuthorization.InvoicePrefix,
                            DocumentNumber = documentNumber,
                            CUFE = invoice.CUFE,
                            Year = DateTime.Now.Year
                        };

                        var operatingUnit = _operatingUnitRepository.GetOperatingUnitById(documentInvoiceProductSales.OperatingUnitId);
                        electronicDocument.FilePath = System.IO.Path.Combine(
                            Utils.GetPathElectronicDocuments(),
                            electronicDocument.Container,
                            operatingUnit.UnitCode,
                            electronicDocument.DocumentDate.Year.ToString(),
                            electronicDocument.DocumentDate.Month.ToString(),
                            electronicDocument.getDocumentTypeName(),
                            String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber)
                        );

                        IUnitWork unitOfWorkElectronicDocument = _electronicDocumentRepository.UnitWork;
                        _electronicDocumentRepository.SaveEntity(electronicDocument);
                        unitOfWorkElectronicDocument.Commit();
                    }

                    var messageResult = new StringBuilder();
                    messageResult.AppendLine($"Factura de Producto: {documentInvoiceProductSales.Code}");
                    messageResult.AppendLine($"Cuenta Por Cobrar: {accountReceivable.InvoiceNumber}");
                    messageResult.AppendLine($"Comprobante Contable: {consecutive}");
                    if (cashReceipts != null)
                        messageResult.AppendLine($"Recibo de Caja: {cashReceipts.Code}");
                    if (recognition.Code != null)
                        messageResult.AppendLine($"Reconocimiento: {recognition.Code}");

                    scope.Complete();
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = documentInvoiceProductSales, StateResult = true, Message = messageResult.ToString() };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorConcurrence") }
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = Utils.GetInnerExceptionMessages(ex)
                    };
                }
            }
        }

        public ActionResult ReverseDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, int reversalReasonId, string reversalReasonDescription, AuditMessage audit, SessionValues session)
        {
            IUnitWork unitOfWorkInvoiceProduct = _documentInvoiceProductSalesRepository.UnitWork;
            IUnitWork unitOfWorkInvoice = _invoiceRepository.UnitWork;
            IUnitWork unitOfWorkSequence = _sequenseRepository.UnitWork;

            try
            {
                if (reversalReasonId == 0)
                    throw new ArgumentNullException("Razón de anulación");
                if (string.IsNullOrEmpty(reversalReasonDescription))
                    throw new ArgumentNullException("Descripción Razón de anulación");

                //IndigoAuditSimpleEntity<Domain.Entities.DocumentInvoiceProductSales> auditProcess;
                var billingSetting = _settingsBillingRepository.GetSettingsBillingByIdUnitOperative(documentInvoiceProductSales.OperatingUnitId, false);
                if (billingSetting.Id == 0)
                    return new ActionResult(false, "No se encontro parametros de facturación para la unidad operativa seleccionada");
                
                //Obtengo el almacén para determinar si es un almacén en consignación
                var warehouse = _warehouseRepository.GetWarehouseById(documentInvoiceProductSales.WarehouseId);
                if (warehouse == null || warehouse.Id == 0)
                    return new ActionResult(false, "No se encontró el Almacén seleccionado");

                if (documentInvoiceProductSales.DocumentInvoiceProductSalesDetail == null || !documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Any())
                    return new ActionResult(false, "No se encontraron detalles de la factura.");

                //consultamos la factura que se generó
                if (!documentInvoiceProductSales.InvoiceId.HasValue)
                {
                    return new ActionResult(false, "No se ha generado una factura a partir de éste documento.");
                }
                Invoice invoice = _invoiceRepository.GetInvoiceById(documentInvoiceProductSales.InvoiceId.Value);
                if (invoice == null || invoice.Id == 0)
                {
                    return new ActionResult(false, "Factura no encontrada.");
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions() { Timeout = TransactionManager.MaximumTimeout, IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    string codeNote = String.Empty;
                    //Aqui validamos, si es una factura electronica para realizar el registro de la nota Credito por la anulacion
                    if (String.IsNullOrEmpty(invoice.CUFE) == false)
                    {
                        //Secuencia de la Nota para los Documentos Electronicos
                        BillingSequence sequence = _sequenseRepository.GetSequenseByIdForm("2037");
                        if (sequence != null && sequence.Id > 0 && sequence.Sequential && sequence.BillingSequenceDetail != null && sequence.BillingSequenceDetail.Count > 0)
                        {
                            codeNote = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequence.BillingSequenceDetail.First().Sequense.Pattern, sequence.BillingSequenceDetail.First().Next);
                            if (codeNote == null || codeNote.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                return new ActionResult(false, "La secuencia para las Notas Crédito de Facturacion Electronica alcanzo su valor maximo.");
                            }

                            sequence.BillingSequenceDetail.First().Next += 1;
                            _sequenseRepository.SaveEntity(sequence);
                            unitOfWorkSequence.Commit();
                        }
                        else
                        {
                            return new ActionResult(false, "La secuencia para las Notas Crédito de Facturacion Electronica no esta parametrizada o no es secuencial.");
                        }
                    }

                    //afecto el inventario fisico
                    StringBuilder resultMessage = new StringBuilder();
                    foreach (var item in documentInvoiceProductSales.DocumentInvoiceProductSalesDetail)
                    {
                        //Obtenemos el producto por detalle
                        var product = _productRepository.GetInventoryProductByIdWithProducGroup(item.ProductId, false);
                        decimal productCost = (decimal)Utils.RoundValue((decimal)product.ProductCost, 1);

                        var batchesByPhysical = item.DocumentInvoiceProductSalesDetailBatchSerial.GroupBy(b => b.PhysicalInventoryId);

                        //Recorremos los lotes agrupados por PhysicalInventory
                        foreach (var groupBatch in batchesByPhysical)
                        {
                            var physicalInventoryId = groupBatch.Key;
                            int totalQuantity = groupBatch.Sum(g => g.Quantity);
                            Domain.Entities.PhysicalInventory physical = _physicalInventoryRepository.GetPhysicalInventoryById(physicalInventoryId);
                            List<Kardex> listKardex = new List<Kardex>()
                                {
                                    new Kardex()
                                    {
                                        ProductId = item.ProductId,
                                        WarehouseId = documentInvoiceProductSales.WarehouseId,
                                        BatchSerialId = physical.BatchSerialId,
                                        MovementType = 1,
                                        Quantity = totalQuantity,
                                        Value = productCost,
                                        AffectInventory = true
                                    }
                                };

                            var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, documentInvoiceProductSales.Id, documentInvoiceProductSales.Code, documentInvoiceProductSales.GetType().Name, documentInvoiceProductSales.CreationUser);

                            if (!result.StateResult)
                            {
                                resultMessage.AppendLine(result.Message);
                                continue;
                            }

                            //Si es consignación, marcamos el producto como usado una sola vez por PhysicalInventory
                            if (warehouse.WarehouseConsignment == true)
                            {
                                //Actualizar el detalle de la remisión
                                result = _consignmentInventoryRemissionDetailBatchSerialAdminService
                                    .UpdateTheQuantityProductUsedInConsignmentInventoryRemission(
                                        item.Id,
                                        documentInvoiceProductSales.OperatingUnitId,
                                        documentInvoiceProductSales.FunctionalUnitId,
                                        documentInvoiceProductSales.WarehouseId,
                                        item.ProductId,
                                        physical.BatchSerialId,
                                        MovementTypeRemissionUsed.Input,
                                        totalQuantity,
                                        productCost,
                                        documentInvoiceProductSales.Id,
                                        documentInvoiceProductSales.Code,
                                        documentInvoiceProductSales.GetType().Name,
                                        documentInvoiceProductSales.CreationUser,
                                        null
                                    );

                                if (!result.StateResult)
                                    resultMessage.AppendLine(result.Message);
                            }
                        }
                    }

                    if (resultMessage.Length > 0)
                    {
                        scope.Dispose();
                        return new ActionResult(false, resultMessage.ToString());
                    }
                    
                    invoice.Status = 2;
                    invoice.RevenueControlDetailId = null;
                    invoice.ReversalReasonId = reversalReasonId;
                    invoice.DescriptionReversal = reversalReasonDescription;
                    invoice.AnnulmentDate = DateTime.Now;
                    invoice.AnnulmentUser = audit.CodeUser;

                    _invoiceRepository.SaveEntity(invoice);
                    unitOfWorkInvoice.Commit();

                    //Consultamos las cuentas por cobrar generadas a partir de la factura
                    var accountReceivableList = _accountReceivableRepository.GetAccountReceivableByInvoiceId(invoice.Id);
                    AccountReceivable accountReceivable = null;
                    if (accountReceivableList != null && accountReceivableList.Any())
                    {
                        accountReceivable = accountReceivableList.FirstOrDefault();

                        //según adrianita este tipo de facturas no se radican, asi que no se hace la validacion
                        //validamos si la cuenta por cobrar ya tiene movimiento
                        if (accountReceivable.Balance != accountReceivable.Value)
                        {
                            scope.Dispose();
                            return new ActionResult(false, $"La factura ({invoice.InvoiceNumber}) no se puede anular porque la cuenta por cobrar ({accountReceivable.Code}) tiene movimiento.");
                        }
                        accountReceivable.Status = 3;
                        accountReceivable.Balance = 0;
                        foreach (AccountReceivableShare accountReceivableShare in accountReceivable.AccountReceivableShare)
                        {
                            accountReceivableShare.Balance = 0;
                        }
                        foreach (AccountReceivableAccounting accountReceivableAccounting in accountReceivable.AccountReceivableAccounting)
                        {
                            accountReceivableAccounting.Balance = 0;
                        }
                        var resultAccountRecivable = _accountReceivableAdminService.SaveAccountReceivable(accountReceivable, audit);
                        if (!resultAccountRecivable.StateResult)
                        {
                            scope.Dispose();
                            return new ActionResult(false, resultAccountRecivable.Message);
                        }
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult(false, $"No se encontró la cuenta por cobrar genedada por el documento ({documentInvoiceProductSales.Code}).");
                    }

                    //reversamos el comprobante contable (cosulta con estado )
                    JournalVouchers jvProduct = _accountingRepository.GetAccountingDocumenteByEntityIdAndEntityName(documentInvoiceProductSales.Id, documentInvoiceProductSales.GetType().Name);
                    if (jvProduct == null || jvProduct.Id == 0)
                    {
                        scope.Dispose();
                        return new ActionResult(false, $"No se encontró el comprobante contable generado por la factura de producto ({documentInvoiceProductSales.Code}).");
                    }

                    ThirdParty thirdParty = this._thirdPartyRepository.GetThirdPartyById(documentInvoiceProductSales.ThirdPartyId);
                    if (thirdParty == null || thirdParty.Id == 0)
                    {
                        scope.Dispose();
                        return new ActionResult(false, "No se encontró el tercero asociado a la factura.");
                    }

                    int JournalVoucherType = billingSetting.InvoiceAnnulmentJournalVoucherTypeId;
                    if (thirdParty.ElectronicBiller == false && billingSetting.ApplyElectronicSalesTicket == true)
                    {
                        JournalVoucherType = billingSetting.AccountingVoucherReversalId.Value;
                    }

                    JournalVouchers jvAnullate = new JournalVouchers()
                    {
                        IdJournalVoucher = JournalVoucherType,
                        VoucherDate = DateTime.Now,
                        Status = 2,
                        Detail = $"Factura No. {documentInvoiceProductSales.Code} - Tercero: ({thirdParty.Nit} - {thirdParty.Name.Trim()})",
                        EntityCode = documentInvoiceProductSales.Code,
                        EntityId = documentInvoiceProductSales.Id,
                        EntityName = documentInvoiceProductSales.GetType().Name,
                        CreationUser = audit.CodeUser,
                        CreationDate = DateTime.Now,
                        ConfirmationUser = audit.CodeUser,
                        ConfirmationDate = DateTime.Now
                    };

                    foreach (JournalVoucherDetails jvDetail in jvProduct.JournalVoucherDetails)
                    {
                        JournalVoucherDetails jvDetailAnullate = new JournalVoucherDetails()
                        {
                            IdMainAccount = jvDetail.IdMainAccount,
                            IdThirdParty = jvDetail.IdThirdParty,
                            IdCostCenter = jvDetail.IdCostCenter,
                            CreditValue = jvDetail.DebitValue,
                            DebitValue = jvDetail.CreditValue,
                            Detail = jvDetail.Detail,
                            IdRetention = jvDetail.IdRetention,
                            RetentionRate = jvDetail.RetentionRate,
                            BaseValue = jvDetail.BaseValue

                        };
                        jvAnullate.JournalVoucherDetails.Add(jvDetailAnullate);
                    }

                    string consecutive = "";
                    ActionMessageResult<JournalVouchers> resultSaveJournalVoucher = _accountingAdminService.SaveAccountingDocument(jvAnullate, audit);
                    if (!resultSaveJournalVoucher.StateResult)
                    {
                        scope.Dispose();
                        return new ActionResult(false, $"Ocurrieron errores al intentar Generar el comprobante contable: {resultSaveJournalVoucher.Message}.");
                    }
                    else
                    {
                        JournalVoucherTypes journalVoucherType = _documentTypeRepository.GetJournalVoucherById(JournalVoucherType);
                        if (journalVoucherType != null && journalVoucherType.Id > 0)
                        {
                            resultMessage.AppendLine($"Comprobante contable de tipo ({string.Concat(journalVoucherType.Code, " - ", journalVoucherType.Name)}) consecutivo: {resultSaveJournalVoucher.ObjectEmbbeded.Consecutive}");
                        }
                    }
                    consecutive = resultSaveJournalVoucher.ObjectEmbbeded.Consecutive.ToString();
                    
                    Recognition recognition = null;
                    if (documentInvoiceProductSales.RecognitionId != null && documentInvoiceProductSales.RecognitionId.Value > 0)
                    {
                        //Buscamos si se ha generado documento de reconocimiento
                        recognition = _recognitionRepository.GetRecognitionById(documentInvoiceProductSales.RecognitionId.Value);
                        //validamos que exista y que no tenga movimientos
                        if (recognition == null || recognition.Id == 0)
                        {
                            scope.Dispose();
                            return new ActionResult(false, $"No se encontró el documento de Reconocimiento para la Factura de Producto {documentInvoiceProductSales.Code}.");
                        }
                        if (recognition.RecognitionDetail != null && recognition.RecognitionDetail.Any())
                        {
                            if (recognition.RecognitionDetail.Any(m => m.Balance != m.InitialValue))
                            {
                                scope.Dispose();
                                return new ActionResult(false, $"El documento de Reconocimiento {recognition.Code} no se puede anular debido a que tiene movimiento.");
                            }
                        }
                        recognition.Status = 3;
                        recognition.AnnulmentDate = DateTime.Now;
                        recognition.AnnulmentUser = audit.CodeUser;
                        foreach (var item in recognition.RecognitionDetail)
                        {
                            item.Balance = 0;
                        }
                        _recognitionRepository.SaveEntity(recognition);
                        _recognitionRepository.UnitWork.Commit();
                    }

                    //Termina el proceso de presupuesto
                    documentInvoiceProductSales.Status = 3;
                    documentInvoiceProductSales.AnnulmentDate = DateTime.Now;
                    documentInvoiceProductSales.AnnulmentUser = audit.CodeUser;
                    documentInvoiceProductSales.ModificationDate = DateTime.Now;
                    documentInvoiceProductSales.ModificationUser = audit.CodeUser;
                    documentInvoiceProductSales.MarkAsModified();
                    _documentInvoiceProductSalesRepository.SaveEntity(documentInvoiceProductSales);
                    unitOfWorkInvoiceProduct.Commit();

                    new IndigoAuditSimpleEntity<Domain.Entities.DocumentInvoiceProductSales>(documentInvoiceProductSales, audit, Infrastructure.CrossCutting.Audit.Actions.Annular, documentInvoiceProductSales.OriginalValue).Execute();

                    resultMessage.AppendLine($"Anulación Factura de Producto: {documentInvoiceProductSales.Code}");
                    resultMessage.AppendLine($"Anulación Cuenta Por Cobrar: {accountReceivable.InvoiceNumber}");
                    //resultMessage.AppendLine($"Comprobante Contable: {consecutive}");
                    if (recognition != null)
                        resultMessage.AppendLine($"Anulación Reconocimiento: {recognition.Code}");

                    //Aqui validamos, si es una factura electronica realizamos el registro de la nota Credito para la anulacion
                    if (String.IsNullOrEmpty(invoice.CUFE) == false)
                    {
                        var reversalReason = _billingReversalReasonRepository.GetReversalReasonById(reversalReasonId, false);
                        string observations = reversalReason.Description + ": " + reversalReasonDescription;

                        BillingNote billingNote = new BillingNote()
                        {
                            Code = codeNote,
                            NoteDate = DateTime.Now,
                            CustomerPartyId = invoice.ThirdPartyId,
                            Observations = observations,
                            Nature = 2,
                            OperatingUnitId = invoice.OperatingUnitId,
                            EntityId = documentInvoiceProductSales.Id,
                            EntityName = documentInvoiceProductSales.GetType().Name
                        };

                        BillingNoteDetail billingNoteDetail = new BillingNoteDetail()
                        {
                            InvoiceId = invoice.Id,
                            InvoiceNumber = invoice.InvoiceNumber,
                            CUFE = invoice.CUFE,
                            DocumentDate = invoice.InvoiceDate,
                            AdjusmentValue = invoice.TotalValue,
                            BillingValue = invoice.InvoiceValue,
                            DiscountValue = invoice.ThirdPartyDiscountValue,
                            ConceptId = 2
                        };

                        if (invoice.ValueTax > 0)
                        {
                            foreach (var tax in documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.GroupBy(d => d.IvaPercentage))
                            {
                                billingNoteDetail.BillingNoteDetailTax.Add(
                                    new BillingNoteDetailTax()
                                    {
                                        TaxPercentage = tax.Key,
                                        TaxValue = documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Where(d => d.IvaPercentage == tax.Key).Sum(d => d.IvaValue),
                                        BaseValue = documentInvoiceProductSales.DocumentInvoiceProductSalesDetail.Where(d => d.IvaPercentage == tax.Key).Sum(d => d.Quantity * d.SalePrice - d.DiscountValue)
                                    }
                                );
                            }
                        }
                        billingNote.BillingNoteDetail.Add(billingNoteDetail);

                        IUnitWork unitOfWorkBillingNote = _billingNoteRepository.UnitWork;
                        billingNote.CUDE = billingNote.getCUDE();
                        _billingNoteRepository.SaveEntity(billingNote);
                        unitOfWorkBillingNote.Commit();

                        var settingsAccount = _settingsAccountRepository.GetSettingAccountSimple(documentInvoiceProductSales.OperatingUnitId);
                        var electronicDocument = new ElectronicDocument()
                        {
                            DianVersion = settingsAccount.DianVersion,
                            OperatingUnitId = billingNote.OperatingUnitId,
                            CustomerPartyId = billingNote.CustomerPartyId,
                            EntityId = billingNote.Id,
                            EntityName = billingNote.GetType().Name,
                            DocumentDate = billingNote.NoteDate,
                            DocumentType = billingNote.GetDocumentType(),
                            Status = 1,
                            CreationDate = DateTime.Now,
                            Container = session.TransactionalContainer,
                            Prefix = null,
                            DocumentNumber = billingNote.Code,
                            CUFE = billingNote.CUDE,
                            Year = DateTime.Now.Year
                        };

                        var operatingUnit = _operatingUnitRepository.GetOperatingUnitById(documentInvoiceProductSales.OperatingUnitId);
                        electronicDocument.FilePath = System.IO.Path.Combine(
                            Utils.GetPathElectronicDocuments(),
                            electronicDocument.Container,
                            operatingUnit.UnitCode,
                            electronicDocument.DocumentDate.Year.ToString(),
                            electronicDocument.DocumentDate.Month.ToString(),
                            electronicDocument.getDocumentTypeName(),
                            String.Concat(electronicDocument.Prefix, electronicDocument.DocumentNumber)
                        );

                        IUnitWork unitOfWorkElectronicDocument = _electronicDocumentRepository.UnitWork;
                        _electronicDocumentRepository.SaveEntity(electronicDocument);
                        unitOfWorkElectronicDocument.Commit();
                    }

                    scope.Complete();
                    return new ActionResult(true, resultMessage.ToString());
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult(false, ResourceManager.get_GetString("ErrorConcurrence"));
            }
            catch (DbEntityValidationException ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult(false, IndigoManagementExceptions.GetExceptionDetails(ex));
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult(false, IndigoManagementExceptions.GetExceptionDetails(ex));
            }
        }

        private ActionResult<string> GetPortfolioSequenceByTagForm(string tag, int idOperativeUnitId = 0)
        {
            try
            {
                int idCurrentSequence = 0;
                PortfolioSequence sequence = _portFolioSequenceAdminService.GetSequenseByIdForm(tag);
                if (sequence != null && sequence.Id > 0)
                {
                    if (sequence.Scope.Equals("O"))
                    {
                        idCurrentSequence = sequence.PortfolioSequenceDetail[0].Id;
                    }
                    else if (sequence.Scope.Equals("OU"))
                    {
                        var res = (from ou in sequence.PortfolioSequenceDetail where ou.OperatingUnit.Id == idOperativeUnitId select ou).ToList();
                        if (res != null && res.Count > 0)
                        {
                            idCurrentSequence = res[0].Id;
                        }
                    }
                    return new ActionResult<string>() { StateResult = true, ObjectEmbbeded = idCurrentSequence.ToString() };
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                return new ActionResult<string> { StateResult = false, Message = string.Format("No se encontró secuencia numérica para el Tag {0} de Cuentas por Cobrar", tag) };
            }
        }

        /// <summary>
        /// metodo para generar la secuencia del reconocimiento
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private ActionResult GenerateSequence()
        {
            sequenceBudget = _budgetSequenceRepository.GetSequenseByIdForm("213");

            if (sequenceBudget != null && sequenceBudget.Id > 0 && sequenceBudget.Sequential)
            {
                dynamic res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequenceBudget.BudgetSequenceDetail[0].Sequense.Pattern, sequenceBudget.BudgetSequenceDetail[0].Next);
                if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                {
                    return new ActionResult
                    {
                        Message = res,
                        StateResult = true
                    };
                }
                else
                {
                    return new ActionResult
                    {
                        StateResult = false,
                        Message = "La secuencia para el reconocimiento alcanzo su valor maximo"
                    };
                }
            }
            else
            {
                return new ActionResult
                {
                    StateResult = false,
                    Message = "La secuencia para reconocimentos no esta parametrizada o no es secuencial"
                };
            }
        }

        private ActionResult<Domain.Entities.JournalVouchers> GenerateJournalVourcher(Domain.Entities.DocumentInvoiceProductSales productInvoice, Domain.Entities.Invoice invoice, AuditMessage audit, int mainAccountDebit, int costCenterIdSettings, int ProductInvoiceJournalVoucherTypeId, SettingsBilling billingSettings)
        {
            if (billingSettings == null)
                return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se encontraron parametros de facturación para la unidad operativa seleccionada" };

            if (billingSettings.ApplyBasicBilling)
            {
                return GenerateJournalVoucherIsBillingBasicTrue(productInvoice, invoice, audit, mainAccountDebit, costCenterIdSettings, ProductInvoiceJournalVoucherTypeId, billingSettings);
            }
            else
            {
                return GenerateJournalVoucherIsBillingBasicFalse(productInvoice, audit, mainAccountDebit, costCenterIdSettings, ProductInvoiceJournalVoucherTypeId, billingSettings);
            }
        }

        /// <summary>
        /// Genera el comprobante contable para cuando el campo de facturación básica de parámetros de facturación este en true
        /// </summary>
        /// <returns></returns>
        private ActionResult<Domain.Entities.JournalVouchers> GenerateJournalVoucherIsBillingBasicTrue(Domain.Entities.DocumentInvoiceProductSales productInvoice, Domain.Entities.Invoice invoice, AuditMessage audit, int mainAccountDebit, int costCenterIdSettings, int ProductInvoiceJournalVoucherTypeId, SettingsBilling billingSettings)
        {
            var settingInventory = _settingInventoryRepository.GetSettingInventory(productInvoice.OperatingUnitId);
            if (settingInventory == null)
            {
                return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se encontraron parametros de inventario para la unidad operativa seleccionada" };
            }

            //Se valida que se haya parametrizado la cuenta contable para la contabilización de Retencion en la Fuente en el Campo Cuenta Contable RETEFUENTE del parametro de facturacion (Básica)
            if (billingSettings.ReteFuenteMainAccountId == null)
            {
                return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se ha parametrizado la Cuenta contable Retefuente en parámetros de facturación." };
            }

            //El Valor Neto de la factura contabiliza al Cliente y debe armar el asiento contable validando la cuenta contable asignada en el Campo Cuenta Contable Cliente del parametro de Facturacion
            if (billingSettings.ClientMainAccountId == null)
            {
                return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se ha parametrizado la Cuenta contable Cliente en parámetros de facturación." };
            }

            ThirdParty thirdParty = this._thirdPartyRepository.GetThirdPartyById(productInvoice.ThirdPartyId);
            var journalVoucher = new JournalVouchers()
            {
                IdJournalVoucher = ProductInvoiceJournalVoucherTypeId,
                VoucherDate = productInvoice.DocumentDate,
                Status = 2,
                Detail = $"Factura No. {invoice.InvoiceNumber} - Tercero: ({thirdParty.Nit} - {thirdParty.Name.Trim()})",
                EntityCode = productInvoice.Code,
                EntityId = productInvoice.Id,
                EntityName = productInvoice.GetType().Name,
                CreationUser = audit.CodeUser,
                CreationDate = DateTime.Now,
                ConfirmationUser = audit.CodeUser,
                ConfirmationDate = DateTime.Now
            };

            //Centro del costo de la unidad donde se realiza la factura del producto
            var fu = _functionalUnitRepository.GetFunctionalUnitById(productInvoice.FunctionalUnitId.ToString(), false);
            int CostCenterIdJv = fu.CostCenterId;

            //creo los detalles al credito por cada item del detalle de la factura de producto
            JournalVoucherDetails jvd;
            MainAccounts mainAccount;
            StringBuilder errors = new StringBuilder();
            foreach (var item in productInvoice.DocumentInvoiceProductSalesDetail)
            {
                var product = _productRepository.GetInventoryProductByIdWithProducGroup(item.ProductId, false);
                var warehouse = _warehouseRepository.GetWarehouseById(productInvoice.WarehouseId);

                //utilizo la cuenta de ingreso
                mainAccount = _mainAccountRepository.GetAccountById(product.ProductGroup.IncomeAccountId, false);
                jvd = new JournalVoucherDetails();
                jvd.IdMainAccount = mainAccount.Id;
                if (mainAccount.HandlesThirdParty)
                {
                    jvd.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    switch (billingSettings.AssociateCostCenter)
                    {
                        case 1://Costo(Unidad Funcional)
                            var functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(productInvoice.FunctionalUnitId.ToString(), false);
                            jvd.IdCostCenter = functionalUnit.CostCenterId;
                            break;

                        case 2://Costo(Grupo)
                            jvd.IdCostCenter = product.ProductGroup.CostCenterId;
                            break;
                    }
                }
                jvd.CreditValue = item.SubTotalValue - item.DiscountValue;
                journalVoucher.JournalVoucherDetails.Add(jvd);

                //cuenta de inventario
                var conceptAccountPayable = _paymentsConceptRepository.GetPaymentConceptById(product.ProductGroup.InventoryAccountPayableConceptId.ToString(), false);
                mainAccount = _mainAccountRepository.GetAccountById(conceptAccountPayable.IdAccount.Value, false);
                jvd = new JournalVoucherDetails();
                jvd.IdMainAccount = mainAccount.Id;
                if (mainAccount.HandlesThirdParty)
                {
                    jvd.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    switch (settingInventory.AssociateCostCenter)
                    {
                        case 1://Inventarios(Sin CC), Costo(Unidad Funcional)
                            jvd.IdCostCenter = null;
                            break;

                        case 2://Inventarios(Grupo), Costo(Grupo)
                            jvd.IdCostCenter = product.ProductGroup.CostCenterId;
                            break;

                        case 3://Inventarios(Almacen), Costo(Almacen)
                            jvd.IdCostCenter = warehouse.CostCenterId;
                            break;
                    }
                }
                jvd.CreditValue = (decimal)Utils.RoundValue((decimal)product.ProductCost * item.Quantity, 1);
                journalVoucher.JournalVoucherDetails.Add(jvd);

                //Debe estar parametrizada la cuenta del costo para el grupo del producto
                if (product.ProductGroup.InventoryCostMainAccountId == null)
                {
                    errors.AppendLine(String.Format("El Grupo {0} no tiene parametrizada la cuenta del costo", product.ProductGroup.Name));
                    continue;
                }

                //creo el detalle al debito por el costo
                mainAccount = _mainAccountRepository.GetAccountById(product.ProductGroup.InventoryCostMainAccountId.Value, false);
                jvd = new JournalVoucherDetails();
                jvd.IdMainAccount = mainAccount.Id;
                if (mainAccount.HandlesThirdParty)
                {
                    jvd.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    switch (billingSettings.AssociateCostCenter)
                    {
                        case 1://Costo(Unidad Funcional)
                            var functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(productInvoice.FunctionalUnitId.ToString(), false);
                            jvd.IdCostCenter = functionalUnit.CostCenterId;
                            break;

                        case 2://Costo(Grupo)
                            jvd.IdCostCenter = product.ProductGroup.CostCenterId;
                            break;
                    }
                }
                jvd.DebitValue = (decimal)Utils.RoundValue((decimal)product.ProductCost * item.Quantity, 1);
                journalVoucher.JournalVoucherDetails.Add(jvd);
            }

            if (errors.Length > 0)
            {
                return new ActionResult<JournalVouchers>() { StateResult = false, Message = errors.ToString() };
            }

            mainAccount = _mainAccountRepository.GetAccountById(billingSettings.ClientMainAccountId.Value, false);
            jvd = new JournalVoucherDetails();
            jvd.IdMainAccount = billingSettings.ClientMainAccountId.Value;
            if (mainAccount.HandlesThirdParty)
            {
                jvd.IdThirdParty = productInvoice.ThirdPartyId;
            }
            if (mainAccount.HandlesCostCenter)
            {
                jvd.IdCostCenter = CostCenterIdJv;
            }
            jvd.DebitValue = productInvoice.TotalValue;
            journalVoucher.JournalVoucherDetails.Add(jvd);

            //Se crea el detalle si la factura maneja retención iva
            if (productInvoice.WithholdingTax > 0)
            {
                //El campo Retencion IVA debe armar asiento validando la Cuenta Contable asignada en el Campo Cuenta Contable RETEIVA del parametro de facturacion
                if (billingSettings.ReteIVAMainAccountId == null)
                {
                    return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se ha parametrizado la Cuenta contable ReteIVA en parámetros de facturación." };
                }

                mainAccount = _mainAccountRepository.GetAccountById(billingSettings.ReteIVAMainAccountId.Value, false);
                jvd = new JournalVoucherDetails();
                jvd.IdMainAccount = billingSettings.ReteIVAMainAccountId.Value;
                if (mainAccount.HandlesThirdParty)
                {
                    jvd.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    jvd.IdCostCenter = CostCenterIdJv;
                }
                jvd.IdRetention = billingSettings.ReteIVAConceptId;
                if (jvd.IdRetention != null && jvd.IdRetention != 0)
                {                
                    jvd.RetentionRate = _retentionRepository.GetRetentionById(jvd.IdRetention.Value, true).Rate;
                }
                jvd.BaseValue = productInvoice.ValueTax;
                jvd.DebitValue = productInvoice.WithholdingTax;
                journalVoucher.JournalVoucherDetails.Add(jvd);
            }

            //Se crea el detalle si la factura maneja retencion ica
            if (productInvoice.WithholdingICA > 0)
            {
                //El campo Retencion ICA debe armar el asiento validando la Cuenta Contable asignada en el Campo Cuenta Contable RETEICA del parametro de facturacion
                if (billingSettings.ReteICAMainAccountId == null)
                {
                    return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se ha parametrizado la Cuenta contable ReteICA en parámetros de facturación." };
                }

                mainAccount = _mainAccountRepository.GetAccountById(billingSettings.ReteICAMainAccountId.Value, false);
                jvd = new JournalVoucherDetails();
                jvd.IdMainAccount = billingSettings.ReteICAMainAccountId.Value;
                if (mainAccount.HandlesThirdParty)
                {
                    jvd.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    jvd.IdCostCenter = CostCenterIdJv;
                }
                jvd.DebitValue = productInvoice.WithholdingICA;
                //La retencion esta parametrizada en la ciudad o en el tercero si este no maneja sucursales, por tal razón con uno de los registros es suficiente para obtener los datos adicionales
                jvd.RetentionRate = thirdParty.IcaPercentage;
                if (productInvoice.BranchOfficeId != null)
                {
                    var conceptRetention = _retentionRepository.GetRetentionConceptByIdBrachOfficeId((int)productInvoice.BranchOfficeId);

                    if(conceptRetention == null)
                    {
                        return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se ha parametrizado el concepto de retención ICA en la ciudad" };
                    }

                    jvd.IdRetention = conceptRetention.Id;
                    jvd.RetentionRate = conceptRetention.Rate;
                }
                jvd.BillingValue = productInvoice.DocumentInvoiceProductSalesDetail.Sum(d => d.SubTotalValue - d.DiscountValue);
                jvd.BaseValue = jvd.BillingValue;
                journalVoucher.JournalVoucherDetails.Add(jvd);
            }

            //Si la factura maneja retención en la fuente se crea el detalle
            if (productInvoice.RetentionSource > 0)
            {
                mainAccount = _mainAccountRepository.GetAccountById(billingSettings.ReteFuenteMainAccountId.Value, false);
                jvd = new JournalVoucherDetails();
                jvd.IdMainAccount = billingSettings.ReteFuenteMainAccountId.Value;
                if (mainAccount.HandlesThirdParty)
                {
                    jvd.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    jvd.IdCostCenter = CostCenterIdJv;
                }
                jvd.DebitValue = productInvoice.RetentionSource;
                //Actualmente se considera que la retención es la misma para todos los productos, por tal razón se obtiene el dato de alguno de ellos
                var documentInvoiceProductSalesDetail = productInvoice.DocumentInvoiceProductSalesDetail.Where(d => d.RTFValue > 0 && d.RTFPercentage > 0).FirstOrDefault();
                if (documentInvoiceProductSalesDetail != null && documentInvoiceProductSalesDetail.Id > 0)
                {
                    var product = _productRepository.GetInventoryProductByIdWithProducGroup(documentInvoiceProductSalesDetail.ProductId, false);
                    if (product.ProductGroup.ReteFuenteConceptId != null)
                    {
                        var conceptRetention = _retentionRepository.GetRetentionById(product.ProductGroup.ReteFuenteConceptId.Value);
                        jvd.IdRetention = conceptRetention.Id;
                        jvd.RetentionRate = conceptRetention.Rate;
                        jvd.BillingValue = productInvoice.DocumentInvoiceProductSalesDetail.Sum(d => d.SubTotalValue - d.DiscountValue);
                        jvd.BaseValue = jvd.BillingValue;
                    }
                }
                journalVoucher.JournalVoucherDetails.Add(jvd);
            }

            //Si la factura maneja iva se crea el detalle
            if (productInvoice.ValueTax > 0)
            {
                //El valor de la suma del IVA arma el asiento contable validando la Cuenta contable IVA por pagar del Formulario parametros
                if (billingSettings.IVAPaymentMainAccountId == null)
                {
                    return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se ha parametrizado la Cuenta contable Iva por Pagar en parámetros de facturación." };
                }

                mainAccount = _mainAccountRepository.GetAccountById(billingSettings.IVAPaymentMainAccountId.Value, false);
                jvd = new JournalVoucherDetails();
                jvd.IdMainAccount = billingSettings.IVAPaymentMainAccountId.Value;
                if (mainAccount.HandlesThirdParty)
                {
                    jvd.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    jvd.IdCostCenter = CostCenterIdJv;
                }
                jvd.CreditValue = productInvoice.ValueTax;
                journalVoucher.JournalVoucherDetails.Add(jvd);
            }

            return new ActionResult<JournalVouchers>() { ObjectEmbbeded = journalVoucher, StateResult = true };
        }

        /// <summary>
        /// Genera el comprobante contable para cuando el campo de facturación básica de parámetros de facturación este en false
        /// </summary>
        /// <returns></returns>
        private ActionResult<Domain.Entities.JournalVouchers> GenerateJournalVoucherIsBillingBasicFalse(Domain.Entities.DocumentInvoiceProductSales productInvoice, AuditMessage audit, int mainAccountDebit, int costCenterIdSettings, int ProductInvoiceJournalVoucherTypeId, SettingsBilling billingSettings)
        {
            var settingInventory = _settingInventoryRepository.GetSettingInventory(productInvoice.OperatingUnitId);
            if (settingInventory == null)
            {
                return new ActionResult<JournalVouchers>() { StateResult = false, Message = "No se encontraron parametros de inventario para la unidad operativa seleccionada" };
            }
            int CostAccountId = 0;
            if (settingInventory.AssociateCostMainAccount == 1)
            {
                var settingInventoryFunctionalUnit = settingInventory.SettingInventoryFunctionalUnit.Where(x => x.FunctionalUnitId == productInvoice.FunctionalUnitId).FirstOrDefault();
                if (settingInventoryFunctionalUnit == null)
                {
                    return new ActionResult<JournalVouchers>() { StateResult = false, Message = "La unidad funcional seleccionada no se encuentra en los parametros de inventario" };
                }

                CostAccountId = settingInventoryFunctionalUnit.CostAccountId;
            }

            ThirdParty thirdParty = this._thirdPartyRepository.GetThirdPartyById(productInvoice.ThirdPartyId);
            var journalVoucher = new JournalVouchers()
            {
                IdJournalVoucher = ProductInvoiceJournalVoucherTypeId,
                VoucherDate = productInvoice.DocumentDate,
                Status = 2,
                Detail = string.Format("Factura No. {0} - Tercero: ({1} - {2})", productInvoice.Code, thirdParty.Nit, thirdParty.Name.Trim()),
                EntityCode = productInvoice.Code,
                EntityId = productInvoice.Id,
                EntityName = productInvoice.GetType().Name,
                CreationUser = audit.CodeUser,
                CreationDate = DateTime.Now,
                ConfirmationUser = audit.CodeUser,
                ConfirmationDate = DateTime.Now
            };

            //creo los detalles al credito por cada item del detalle de la factura de producto
            JournalVoucherDetails journalVoucherDetails;
            MainAccounts mainAccount;
            foreach (var item in productInvoice.DocumentInvoiceProductSalesDetail)
            {
                var product = _productRepository.GetInventoryProductByIdWithProducGroup(item.ProductId, false);
                var warehouse = _warehouseRepository.GetWarehouseById(productInvoice.WarehouseId);

                //Validacion cuenta del costo
                if (settingInventory.AssociateCostMainAccount == 2)
                {
                    if (product.ProductGroup.ProductGroupFunctionalUnit == null || !product.ProductGroup.ProductGroupFunctionalUnit.Any(f => f.FunctionalUnitId == productInvoice.FunctionalUnitId))
                    {
                        return new ActionResult<JournalVouchers>() { StateResult = false, Message = String.Format("La unidad funcional seleccionada no se encuentra en el grupo de producto {0} - {1}", product.ProductGroup.Code, product.ProductGroup.Name)};
                    }
                    CostAccountId = product.ProductGroup.ProductGroupFunctionalUnit.Where(f => f.FunctionalUnitId == productInvoice.FunctionalUnitId).FirstOrDefault().CostAccountId;
                }

                //utilizo la cuenta de ingreso
                mainAccount = _mainAccountRepository.GetAccountById(product.ProductGroup.IncomeAccountId, false);
                journalVoucherDetails = new JournalVoucherDetails();
                journalVoucherDetails.IdMainAccount = mainAccount.Id;
                if (mainAccount.HandlesThirdParty)
                {
                    journalVoucherDetails.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    switch (billingSettings.AssociateCostCenter)
                    {
                        case 1://Costo(Unidad Funcional)
                            var functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(productInvoice.FunctionalUnitId.ToString(), false);
                            journalVoucherDetails.IdCostCenter = functionalUnit.CostCenterId;
                            break;

                        case 2://Costo(Grupo)
                            journalVoucherDetails.IdCostCenter = product.ProductGroup.CostCenterId;
                            break;
                    }
                }
                //si el item maneja iva creamos otro detalle
                if (item.IvaValue > 0)
                {
                    journalVoucherDetails.CreditValue = item.SubTotalValue - item.DiscountValue;

                    var journalVoucherDetailsIva = new JournalVoucherDetails();
                    journalVoucherDetailsIva.IdMainAccount = settingInventory.IVAGeneratedMainAccountId;
                    journalVoucherDetailsIva.IdThirdParty = productInvoice.ThirdPartyId;
                    journalVoucherDetailsIva.IdCostCenter = journalVoucherDetails.IdCostCenter;
                    journalVoucherDetailsIva.CreditValue = item.IvaValue;
                    journalVoucher.JournalVoucherDetails.Add(journalVoucherDetailsIva);
                }
                else
                {
                    journalVoucherDetails.CreditValue = item.TotalValue;
                }
                journalVoucher.JournalVoucherDetails.Add(journalVoucherDetails);

                //cuenta de inventario
                var conceptAccountPayable = _paymentsConceptRepository.GetPaymentConceptById(product.ProductGroup.InventoryAccountPayableConceptId.ToString(), false);
                mainAccount = _mainAccountRepository.GetAccountById(conceptAccountPayable.IdAccount.Value, false);
                journalVoucherDetails = new JournalVoucherDetails();
                journalVoucherDetails.IdMainAccount = mainAccount.Id;
                if (mainAccount.HandlesThirdParty)
                {
                    journalVoucherDetails.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    switch (settingInventory.AssociateCostCenter)
                    {
                        case 1://Inventarios(Sin CC), Costo(Unidad Funcional)
                            journalVoucherDetails.IdCostCenter = null;
                            break;

                        case 2://Inventarios(Grupo), Costo(Grupo)
                            journalVoucherDetails.IdCostCenter = product.ProductGroup.CostCenterId;
                            break;

                        case 3://Inventarios(Almacen), Costo(Almacen)
                            journalVoucherDetails.IdCostCenter = warehouse.CostCenterId;
                            break;
                    }
                }
                journalVoucherDetails.CreditValue = (decimal)Utils.RoundValue((decimal)product.ProductCost * item.Quantity, 1);
                journalVoucher.JournalVoucherDetails.Add(journalVoucherDetails);

                //creo el detalle al debito por el costo
                journalVoucherDetails = new JournalVoucherDetails();
                mainAccount = _mainAccountRepository.GetAccountById(CostAccountId, false);
                journalVoucherDetails.IdMainAccount = mainAccount.Id;
                if (mainAccount.HandlesThirdParty)
                {
                    journalVoucherDetails.IdThirdParty = productInvoice.ThirdPartyId;
                }
                if (mainAccount.HandlesCostCenter)
                {
                    switch (billingSettings.AssociateCostCenter)
                    {
                        case 1://Costo(Unidad Funcional)
                            var functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(productInvoice.FunctionalUnitId.ToString(), false);
                            journalVoucherDetails.IdCostCenter = functionalUnit.CostCenterId;
                            break;

                        case 2://Costo(Grupo)
                            journalVoucherDetails.IdCostCenter = product.ProductGroup.CostCenterId;
                            break;
                    }
                }
                journalVoucherDetails.DebitValue = (decimal)Utils.RoundValue((decimal)product.ProductCost * item.Quantity, 1);
                journalVoucher.JournalVoucherDetails.Add(journalVoucherDetails);
            }

            //creo el detalle al debito por el total de la factura - cliente
            journalVoucherDetails = new JournalVoucherDetails();
            mainAccount = _mainAccountRepository.GetAccountById(mainAccountDebit, false);
            journalVoucherDetails.IdMainAccount = mainAccount.Id;
            if (mainAccount.HandlesThirdParty)
            {
                journalVoucherDetails.IdThirdParty = productInvoice.ThirdPartyId;
            }
            if (mainAccount.HandlesCostCenter)
            {
                if (costCenterIdSettings == 0)
                {
                    return new ActionResult<JournalVouchers>() { StateResult = false, Message = "El centro de costo en los parametros de facturación para factura de producto esta vacio" };
                }
                journalVoucherDetails.IdCostCenter = costCenterIdSettings;
            }
            journalVoucherDetails.DebitValue = productInvoice.Value + productInvoice.ValueTax - productInvoice.ValueDiscount;
            journalVoucher.JournalVoucherDetails.Add(journalVoucherDetails);

            return new ActionResult<JournalVouchers>() { ObjectEmbbeded = journalVoucher, StateResult = true };
        }

        /// <summary>
        /// guardar y confirmar una factura
        /// </summary>
        /// <param name="documentInvoiceProductSales"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DocumentInvoiceProductSales> SaveAndConfirmDocumentInvoiceProductSales(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales, CashReceipts cashReceipts, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, BillingSequence sequenceC = null, SessionValues session = null)
        {
            var result = SaveDocumentInvoiceProductSales(documentInvoiceProductSales, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = ConfirmDocumentInvoiceProductSales(result.ObjectEmbbeded, cashReceipts, audit, session);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = true, Message = "Se guardó y se confirmó correctamente" + Environment.NewLine + resultConfirm.Message };
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = true, Message = "Se actualizó y se confirmó correctamente" + Environment.NewLine + resultConfirm.Message };
                    }
                }
                else
                {
                    if (resultConfirm.StateResult == false && resultConfirm.StateResultAux == false)
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = result.ObjectEmbbeded, MessageResult = new List<string> { "" }, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = result.ObjectEmbbeded, MessageResult = new List<string> { "" }, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    if (result.MessageResult == null || result.MessageResult.Count == 0)
                    {
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { MessageResult = new List<string> { "" }, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { MessageResult = result.MessageResult, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                    }
                }
                else
                {
                    return new ActionResult<Domain.Entities.DocumentInvoiceProductSales> { StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
            }
        }

        public ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> SetProductsProductInvoiceImportFile(List<ImportFileRow> data, int wareHouseId, int operatingUnitId, AuditMessage audit)
        {
            try
            {
                return _inventoryServices.SetProductsProductInvoiceImportFile(data, wareHouseId, operatingUnitId, audit);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.DocumentInvoiceProductSalesDetail>> { StateResult = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }
        /// <summary>
        /// Funcion que agrega la actividad economica.
        /// </summary>
        /// <param name="documentInvoiceProductSales"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DocumentInvoiceProductSales> AddEconomicActivity(Domain.Entities.DocumentInvoiceProductSales documentInvoiceProductSales)
        {
            List<string> errors = new List<string>();
            try
            {
                foreach (var detail in documentInvoiceProductSales.DocumentInvoiceProductSalesDetail)
                {
                        var documentsDetailEconomicActivity = _documentInvoiceProductSalesDetailRepository.DocumentInvoiceProductSalesEconomicActivity(detail.ProductId);

                        // Validar si no existe actividad económica asociada al producto
                        if (documentsDetailEconomicActivity is null || documentsDetailEconomicActivity.EconomicActivityId is null)
                    {
                                errors.Add($"No se tiene definida una Actividad Económica Generadora de Ingreso en el Grupo '{documentsDetailEconomicActivity.Name}' asociado al Producto '{detail.CodeNameProduct}'");
                        }
                        else
                        {
                                // Asignar la actividad económica al detalle
                                detail.EconomicActivityId = documentsDetailEconomicActivity.EconomicActivityId;
                        }
                }
            }
            catch (Exception ex)
            {
                // Capturar si el error viene de innerExeption
                errors.Add($"Ocurrió un error inesperado: {ex.Message}");
            }

            // Verificar si hubo errores
            if (errors.Any())
            {
                return new ActionResult<Domain.Entities.DocumentInvoiceProductSales>
                {
                    StateResult = false,
                    MessageResult = errors
                };
            }

            return new ActionResult<Domain.Entities.DocumentInvoiceProductSales>
            {
                StateResult = true,
                ObjectEmbbeded = documentInvoiceProductSales
            };
        }


        public System.Data.DataTable GetDatatable(string Comando, SessionValues session, string nameDt)
        {
            System.Data.DataTable functionReturnValue = default(System.Data.DataTable);
            System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection();

            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion = new System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, string.Empty, session.TransactionalContainer, false));
                    conexion.Open();
                }
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(Comando, conexion);
                da.SelectCommand.CommandTimeout = 30000;
                DataSet ds = new DataSet();
                da.Fill(ds, nameDt);
                functionReturnValue = ds.Tables[nameDt];
                da = null;
                ds = null;
                conexion.Close();
                return functionReturnValue;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
            finally
            {
                conexion.Close();
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
                    _portfolioTransferAdminService.Dispose();
                    _portFolioSequenceAdminService.Dispose();
                    _inventoryServices.Dispose();
                    _accountReceivableAdminService.Dispose();
                    _cashReceiptsAdminService.Dispose();
                    _physicalInventoryAdminService.Dispose();
                    _accountingAdminService.Dispose();
                    _consignmentInventoryRemissionDetailBatchSerialAdminService.Dispose();
                }
                _cashReceipsRepository = null;
                _accountingRepository = null;
                _documentTypeRepository = null;
                _portfolioTransferAdminService = null;
                _portfolioTransferRepository = null;
                _portFolioSequenceAdminService = null;
                _accountReceivableRepository = null;
                _budgetSequenceRepository = null;
                _budgetRepository = null;
                _categoryRepository = null;
                _recognitionRepository = null;
                _documentInvoiceProductSalesRepository = null;
                _sequenseDetailRepository = null;
                _inventoryServices = null;
                _settingInventoryRepository = null;
                _accountReceivableAdminService = null;
                _invoiceRepository = null;
                _sequenseRepository = null;
                _settingsBillingRepository = null;
                _mainAccountRepository = null;
                _productRepository = null;
                _cashReceiptsAdminService = null;
                _sequenseTreasuryRepository = null;
                _billingAuthorizationRepository = null;
                _physicalInventoryRepository = null;
                _physicalInventoryAdminService = null;
                _sequensePortfolioCRepository = null;
                _accountingAdminService = null;
                _functionalUnitRepository = null;
                _warehouseRepository = null;
                _paymentsConceptRepository = null;
                _cashReceiptConceptRepository = null;
                _retentionRepository = null;
                _thirdPartyRepository = null;
                _consignmentInventoryRemissionDetailBatchSerialAdminService = null;
                _documentInvoiceProductSalesDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion IDisposable Support
    }
}