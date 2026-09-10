///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Rafael Eduardo Patiño Cabrera
/// Created          : 07-05-2015
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
using Application.Accounting;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Inventory.LoanMerchandiseDevolution
{
    public class LoanMerchandiseDevolutionAdminService : ILoanMerchandiseDevolutionAdminService
    {

        #region fields
        private ILoanMerchandiseDevolutionRepository _loanMerchandiseDevolutionRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IAccountingDocumentAdminService _accountingRepository;
        private ISettingInventoryRepository _SettingInventoryRepository;
        private IWarehouseRepository _WarehouseRepository;
        private IProductGroupsRepository _ProductGroupsRepository;
        private IPaymentsConceptRepository _PaymentsConceptRepository;
        private IPUCRepository _PUCRepository;
        private IInventoryProductRepository _InventoryProductRepository;
        private ILoanMerchandiseDetailRepository _LoanMerchandiseDetailRepository;
        private ILoanMerchandiseRepository _LoanMerchandiseRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        #endregion

        #region builder
        public LoanMerchandiseDevolutionAdminService(ILoanMerchandiseDevolutionRepository LoanMerchandiseDevolutionRepository, IInventorySequenceDetailRepository sequenseRepository, IPhysicalInventoryAdminService physicalInventoryAdminService,
                                            IPhysicalInventoryRepository physicalInventoryRepository, IAccountingDocumentAdminService accountingRepository,
                                            ISettingInventoryRepository SettingInventoryRepository, IWarehouseRepository WarehouseRepository, IProductGroupsRepository ProductGroupsRepository,
                                            IPaymentsConceptRepository PaymentsConceptRepository, IPUCRepository PUCRepository, IInventoryProductRepository InventoryProductRepository,
                                            ILoanMerchandiseDetailRepository LoanMerchandiseDetailRepository, ILoanMerchandiseRepository LoanMerchandiseRepository, IInventoryControlDocumentRepository InventoryControlDocumentRepository)
        {
            if (LoanMerchandiseDevolutionRepository == null)
            {
                throw new ArgumentNullException("LoanMerchandiseDevolutionRepository");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("sequenseRepository");
            }
            if (physicalInventoryRepository == null)
            {
                throw new ArgumentNullException("physicalInventoryRepository");
            }
            if (physicalInventoryAdminService == null)
            {
                throw new ArgumentNullException("physicalInventoryAdminService");
            }
            if (accountingRepository == null)
            {
                throw new ArgumentNullException("accountingRepository");
            }
            if (SettingInventoryRepository == null)
            {
                throw new ArgumentNullException("SettingInventoryRepository");
            }
            if (WarehouseRepository == null)
            {
                throw new ArgumentNullException("WarehouseRepository");
            }
            if (ProductGroupsRepository == null)
            {
                throw new ArgumentNullException("ProductGroupsRepository");
            }
            if (PaymentsConceptRepository == null)
            {
                throw new ArgumentNullException("PaymentsConceptRepository");
            }
            if (PUCRepository == null)
            {
                throw new ArgumentNullException("PUCRepository");
            }
            if (InventoryProductRepository == null)
            {
                throw new ArgumentNullException("InventoryProductRepository");
            }
            if (LoanMerchandiseDetailRepository == null)
            {
                throw new ArgumentNullException("LoanMerchandiseDetailRepository");
            }
            if (LoanMerchandiseRepository == null)
            {
                throw new ArgumentNullException("LoanMerchandiseRepository");
            }
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            _loanMerchandiseDevolutionRepository = LoanMerchandiseDevolutionRepository;
            _sequenseRepository = sequenseRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _physicalInventoryAdminService = physicalInventoryAdminService;
            _accountingRepository = accountingRepository;
            _SettingInventoryRepository = SettingInventoryRepository;
            _WarehouseRepository = WarehouseRepository;
            _ProductGroupsRepository = ProductGroupsRepository;
            _PaymentsConceptRepository = PaymentsConceptRepository;
            _PUCRepository = PUCRepository;
            _InventoryProductRepository = InventoryProductRepository;
            _LoanMerchandiseDetailRepository = LoanMerchandiseDetailRepository;
            _LoanMerchandiseRepository = LoanMerchandiseRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// obtiene por codigo un oficio de devolucion de prestamo
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Entities.LoanMerchandiseDevolution GetLoanMerchadiseDevolutionByCode(string Code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.LoanMerchandiseDevolution loadMerchandise = _loanMerchandiseDevolutionRepository.GetLoanMerchandiseDevolutionByCode(Code.Trim());
                if (loadMerchandise != null && loadMerchandise.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandiseDevolution> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandiseDevolution>(loadMerchandise, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return loadMerchandise;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.LoanMerchandiseDevolution();
            }
        }

        /// <summary>
        /// obtiene por id un oficio de devolucion de prestamo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.LoanMerchandiseDevolution GetLoanMerchadiseDevolutionById(int id)
        {
            try
            {
                return _loanMerchandiseDevolutionRepository.GetLoanMerchandiseDevolutionById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.LoanMerchandiseDevolution();
            }
        }

        /// <summary>
        /// Guarda un oficio de devolucion de prestamo
        /// </summary>
        /// <param name="loadmerchadiseDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.LoanMerchandiseDevolution> SaveLoanMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _loanMerchandiseDevolutionRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
                try
                {
                    InventorySequenceDetail seq = (idSequence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSequence)));
                    if (loadmerchadiseDevolution.Code == null || loadmerchadiseDevolution.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = loadmerchadiseDevolution.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(loadmerchadiseDevolution.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                loadmerchadiseDevolution.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    Domain.Entities.LoanMerchandiseDevolution auxLoanMerchandiseDevolution = null;
                    IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandiseDevolution> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (loadmerchadiseDevolution.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        loadmerchadiseDevolution.CreationUser = audit.CodeUser;
                        loadmerchadiseDevolution.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        inventoryControlDocument.DocumentNumber = loadmerchadiseDevolution.Code;
                        inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.Devolutionofloan;
                        inventoryControlDocument.DocumentUser = audit.CodeUser;
                        inventoryControlDocument.DocumentDate = loadmerchadiseDevolution.CreationDate;
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        auxLoanMerchandiseDevolution = loadmerchadiseDevolution.OriginalValue;
                        loadmerchadiseDevolution.ModificationUser = audit.CodeUser;
                        loadmerchadiseDevolution.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;

                        if (loadmerchadiseDevolution.Status == 3)
                        {
                            loadmerchadiseDevolution.AnnulmentUser = audit.CodeUser;
                            loadmerchadiseDevolution.AnnulmentDate = DateTime.Now;
                            var documentControl = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(loadmerchadiseDevolution.Code, (int)eTypeDocumentsControlInventory.Devolutionofloan);
                            if (documentControl != null && documentControl.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                                _InventoryControlDocumentRepository.UnitWork.Commit();
                            }
                        }
                    }
                    _loanMerchandiseDevolutionRepository.SaveEntity(loadmerchadiseDevolution);
                    unitOfWork.Commit();
                    unitOfWorkSequense.Commit();
                    unitOfWorkControlDocuments.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandiseDevolution>(loadmerchadiseDevolution, audit, status, auxLoanMerchandiseDevolution);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = true, ObjectEmbbeded = loadmerchadiseDevolution };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
            }
        }

        /// <summary>
        /// Confirma un oficio de devolucion de prestamo
        /// </summary>
        /// <param name="loadmerchadiseDevolution"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public async Task<ActionResult<Domain.Entities.LoanMerchandiseDevolution>> ConfirmLoandMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled))
            {
                IUnitWork loanMerchandiseDevolutionRepository = _loanMerchandiseDevolutionRepository.UnitWork;
                IUnitWork LoanMerchandiseDetailRepositoryUnitWork = _LoanMerchandiseDetailRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandiseDevolution> auditProcess;
                try
                {
                    Domain.Entities.LoanMerchandise loanMerchandise = _LoanMerchandiseRepository.GetLoanMerchandiseById(loadmerchadiseDevolution.LoanMerchandiseId, false);
                    var details = loadmerchadiseDevolution.LoanMerchandiseDevolutionDetail.Where(d => d.Quantity > 0).ToList();

                    StringBuilder errors = new StringBuilder();
                    foreach (var group in details.GroupBy(d => d.LoanMerchandiseDetaillId))
                    {
                        var quantity = details.Where(d => d.LoanMerchandiseDetaillId == group.Key).Sum(d => d.Quantity);                        
                        var LoanMerchandiseDetail = _LoanMerchandiseDetailRepository.GetLoanMerchandiseDetailById(group.Key);
                        if (LoanMerchandiseDetail.OutstandingQuantity < quantity)
                        {
                            var detail = details.Where(d => d.LoanMerchandiseDetaillId == group.Key).FirstOrDefault();
                            errors.AppendLine("La cantidad a devolver (" + quantity + ") del item '" + detail.CodeNameProduct.Trim() + "' es mayor que la cantidad disponible (" + LoanMerchandiseDetail.OutstandingQuantity + ").");
                            continue;
                        }

                        LoanMerchandiseDetail.OutstandingQuantity = LoanMerchandiseDetail.OutstandingQuantity - quantity;
                        _LoanMerchandiseDetailRepository.SaveEntity(LoanMerchandiseDetail);
                    }
                    //si hubo errores los retorno todos
                    if (errors.Length > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = false, StateResultAux = true, ObjectEmbbeded = loadmerchadiseDevolution, Message = errors.ToString() };
                    }

                    List<Kardex> listKardex = new List<Kardex>();
                    foreach (var detail in details)
                    {   
                        foreach (var itemLoanMerchandiseDetailBatchSerial in detail.LoanMerchandiseDevolutionDetailBatchSerial)
                        {
                            listKardex.Add(new Kardex()
                            {
                                ProductId = detail.ProductId,
                                WarehouseId = loadmerchadiseDevolution.WarehouseId,
                                BatchSerialId = itemLoanMerchandiseDetailBatchSerial.BatchSerialId,
                                MovementType = (byte)(loanMerchandise.LoanType == 1 ? 2 : 1),
                                Quantity = itemLoanMerchandiseDetailBatchSerial.Quantity,
                                Value = detail.UnitValue,
                                AffectInventory = true
                            });
                        }
                    }

                    var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, loadmerchadiseDevolution.Id, loadmerchadiseDevolution.Code, loadmerchadiseDevolution.GetType().Name, loadmerchadiseDevolution.CreationUser);
                    if (result.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = false, StateResultAux = false, ObjectEmbbeded = loadmerchadiseDevolution, Message = result.Message };
                    }

                    //generamos comprobante
                    ActionResult<Domain.Entities.JournalVouchers> accounting = CreateAccountingAccount(loadmerchadiseDevolution, details);
                    if (accounting.StateResult == false)
                    {
                        loanMerchandiseDevolutionRepository.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.LoanMerchandiseDevolution>
                        {   
                            StateResult = false,
                            StateResultAux = false,
                            ObjectEmbbeded = loadmerchadiseDevolution,
                            Message = accounting.Message
                        };
                    }
                    //guardamos y confirmamos comprobante
                    ActionMessageResult<JournalVouchers> resultAccounting;
                    resultAccounting = await _accountingRepository.SaveAccountingDocumentAsync(accounting.ObjectEmbbeded, audit);
                    if (resultAccounting.StateResult == false)
                    {
                        loanMerchandiseDevolutionRepository.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.LoanMerchandiseDevolution>
                        {   
                            StateResult = false,
                            StateResultAux = false,
                            ObjectEmbbeded = loadmerchadiseDevolution,
                            Message = resultAccounting.Message
                        };
                    }
                    //eliminamos doc. de control de inventarios
                    InventoryControlDocument inventoryControlDocument = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(loadmerchadiseDevolution.Code, (int)eTypeDocumentsControlInventory.Devolutionofloan);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }
                    loadmerchadiseDevolution.Status = 2;
                    loadmerchadiseDevolution.ConfirmationDate = DateTime.Now;
                    loadmerchadiseDevolution.ConfirmationUser = audit.CodeUser;
                    loadmerchadiseDevolution.ModificationDate = DateTime.Now;
                    loadmerchadiseDevolution.ModificationUser = audit.CodeUser;
                    loadmerchadiseDevolution.MarkAsModified();
                    _loanMerchandiseDevolutionRepository.SaveEntity(loadmerchadiseDevolution);
                    LoanMerchandiseDetailRepositoryUnitWork.Commit();
                    loanMerchandiseDevolutionRepository.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandiseDevolution>(loadmerchadiseDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, loadmerchadiseDevolution.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = true, ObjectEmbbeded = loadmerchadiseDevolution, Message = "- Se Genero Comprobante Contable " + resultAccounting.ObjectEmbbeded.Consecutive.ToString() };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    loanMerchandiseDevolutionRepository.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution>
                    {   
                        StateResult = false,
                        StateResultAux = false,
                        ObjectEmbbeded = loadmerchadiseDevolution,
                        Message = ResourceManager.get_GetString("ErrorConcurrence")
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    loanMerchandiseDevolutionRepository.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution>
                    {   
                        StateResult = false,
                        StateResultAux = false,
                        ObjectEmbbeded = loadmerchadiseDevolution,
                        Message = Utils.GetInnerExceptionMessageToString(ex)
                    };
                }
                catch (Exception ex)
                {
                    loanMerchandiseDevolutionRepository.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution>
                    {   
                        StateResult = false,
                        StateResultAux = false,
                        ObjectEmbbeded = loadmerchadiseDevolution,
                        Message = Utils.GetInnerExceptionMessageToString(ex)
                    };
                }
            }
        }

        /// <summary>
        /// Crea el comprobante contable para la cxp
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public ActionResult<Domain.Entities.JournalVouchers> CreateAccountingAccount(Domain.Entities.LoanMerchandiseDevolution LoanMerchandiseDevolution, List<Domain.Entities.LoanMerchandiseDevolutionDetail> details)
        {

            Domain.Entities.LoanMerchandise LoanMerchandise = _LoanMerchandiseRepository.GetLoanMerchandiseById(LoanMerchandiseDevolution.LoanMerchandiseId);
            if (LoanMerchandise == null)
            {
                return new ActionResult<Domain.Entities.JournalVouchers>
                {
                    StateResult = false,
                    StateResultAux = false,
                    Message = "No se encontro Solicitud de préstamo"
                };
            }
            Domain.Entities.SettingInventory Settinginventory = _SettingInventoryRepository.GetInventorySettingsRegister(LoanMerchandiseDevolution.OperatingUnitId);
            if (Settinginventory == null)
            {
                return new ActionResult<Domain.Entities.JournalVouchers>
                {
                    StateResult = false,
                    StateResultAux = false,
                    Message = "No se Encontro Parametros De Inventario para la Unidad Operativa del Consecutivo -" + LoanMerchandiseDevolution.Code
                };
            }
            Domain.Entities.Warehouse warehouse = _WarehouseRepository.GetWarehouseById(LoanMerchandiseDevolution.WarehouseId);
            if (warehouse == null)
            {
                return new ActionResult<Domain.Entities.JournalVouchers>
                {
                    StateResult = false,
                    StateResultAux = false,
                    Message = "No se encontró Almacen"
                };
            }

            Domain.Entities.JournalVouchers accounting = new Domain.Entities.JournalVouchers();
            string _CommentVouchers;
            decimal _BalanceInvoice = 0;
            if (LoanMerchandise.LoanType == 1)
            {
                _CommentVouchers = "Comprobante de Devolución de Préstamo de Entrada de Mercancía";
            }
            else
            {
                _CommentVouchers = "Comprobante de Devolución de Préstamo de Salida de Mercancía";
            }

            //Se crea la cabecera
            accounting.IdJournalVoucher = Settinginventory.LoanReturnJournalVoucherTypeId;
            accounting.VoucherDate = LoanMerchandiseDevolution.DocumentDate;
            accounting.Status = 2;
            accounting.Detail = _CommentVouchers;
            accounting.EntityCode = LoanMerchandiseDevolution.Code;
            accounting.EntityId = LoanMerchandiseDevolution.Id;
            accounting.EntityName = typeof(Domain.Entities.LoanMerchandiseDevolution).Name;
            
            //Se crean los detalles
            foreach (Domain.Entities.LoanMerchandiseDevolutionDetail itemDetail in details)
            {
                Domain.Entities.InventoryProduct InventoryProduct = _InventoryProductRepository.GetInventoryProductById(itemDetail.ProductId);

                if (InventoryProduct?.ProductGroupId is null) { return new ActionResult<JournalVouchers> {StateResult=false,StateResultAux=false,Message=$"El producto {InventoryProduct.Code} No maneja Grupo" }; }

                Domain.Entities.ProductGroup ProductGroup = _ProductGroupsRepository.GetProductGroupById(InventoryProduct.ProductGroupId.Value);

                if (ProductGroup == null || ProductGroup.Id == 0)
                {
                    return new ActionResult<Domain.Entities.JournalVouchers>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        Message = "No se Encontro el Grupo del Producto - " + InventoryProduct.Code
                    };
                }

                if (ProductGroup.InventoryAccountPayableConceptId == 0)
                {
                    return new ActionResult<Domain.Entities.JournalVouchers>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        Message = "El concepto de la cuenta por pagar para el Grupo de Producto " + ProductGroup.Code + " No es Específico, por lo tanto no se encontro cuenta contable para el grupo de producto " + InventoryProduct.Code
                    };
                }

                Domain.Entities.AccountPayableConcepts AccountPayableConcepts = _PaymentsConceptRepository.GetPaymentConceptById(ProductGroup.InventoryAccountPayableConceptId.ToString());
                JournalVoucherDetails accountDetail2 = new JournalVoucherDetails();
                accountDetail2.IdMainAccount = AccountPayableConcepts.IdAccount.Value;
                if (AccountPayableConcepts.MainAccounts.HandlesThirdParty == true)
                {
                    accountDetail2.IdThirdParty = LoanMerchandise.ThirdPartyId;
                }
                if (AccountPayableConcepts.MainAccounts.HandlesCostCenter == true)
                {
                    accountDetail2.IdCostCenter = warehouse.CostCenterId;
                }
                if (LoanMerchandise.LoanType == 1) //Si el prestamo fue de entrada
                {
                    accountDetail2.DebitValue = 0;
                    accountDetail2.CreditValue = Math.Round(itemDetail.UnitValue * itemDetail.Quantity, 2);
                }
                else //Si el prestamo fue de salida
                {
                    accountDetail2.DebitValue = Math.Round(itemDetail.UnitValue * itemDetail.Quantity, 2);
                    accountDetail2.CreditValue = 0;
                }
                accountDetail2.Detail = _CommentVouchers;
                accounting.JournalVoucherDetails.Add(accountDetail2);
                _BalanceInvoice += Math.Round(itemDetail.UnitValue * itemDetail.Quantity, 2);
            }
            JournalVoucherDetails accountDetail1 = new JournalVoucherDetails();
            accountDetail1.Detail = _CommentVouchers;
            //entrada
            if (LoanMerchandise.LoanType == 1)
            {
                accountDetail1.IdMainAccount = warehouse.LoanThirdPartyCreditAccountId;
                accountDetail1.DebitValue = Math.Round(_BalanceInvoice, 2);
                accountDetail1.CreditValue = 0;
            }
            else
            {
                accountDetail1.IdMainAccount = warehouse.LoanThirdPartyDebitAccountId;
                accountDetail1.DebitValue = 0;
                accountDetail1.CreditValue = Math.Round(_BalanceInvoice, 2);
            }
            Domain.Entities.MainAccounts MainAccounts = _PUCRepository.GetAccountById(accountDetail1.IdMainAccount, true);
            if (MainAccounts.HandlesThirdParty == true)
            {
                accountDetail1.IdThirdParty = LoanMerchandise.ThirdPartyId;
            }
            if (MainAccounts.HandlesCostCenter == true)
            {
                accountDetail1.IdCostCenter = warehouse.CostCenterId;
            }
            accounting.JournalVoucherDetails.Add(accountDetail1);
            return new ActionResult<Domain.Entities.JournalVouchers>
            {
                StateResult = true,
                StateResultAux = true,
                ObjectEmbbeded = accounting
            };
        }


        /// <summary>
        /// guarda y confirma una nueva devolcuion de prestamo
        /// </summary>
        /// <param name="loadmerchadiseDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<ActionResult<Domain.Entities.LoanMerchandiseDevolution>> SaveAndConfirmLoadMerchadiseDevolution(Domain.Entities.LoanMerchandiseDevolution loadmerchadiseDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null)
        {
            var result = SaveLoanMerchadiseDevolution(loadmerchadiseDevolution, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = await ConfirmLoandMerchadiseDevolution(loadmerchadiseDevolution, audit);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, Message = string.Format(ResourceManager.get_GetString("SavedAndConfirmedWithCode", "Inventory"), resultConfirm.ObjectEmbbeded.Code) };

                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, Message = ResourceManager.get_GetString("UpdatedAndConfirmed", "Inventory") + resultConfirm.Message };
                    }
                }
                else
                {
                    if (resultConfirm.StateResult == false && resultConfirm.StateResultAux == false)
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
                else
                {
                    return new ActionResult<Domain.Entities.LoanMerchandiseDevolution> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }

            }
        }


        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _loanMerchandiseDevolutionRepository.CascadeRollback(value);
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
                }
                _loanMerchandiseDevolutionRepository = null;
                _sequenseRepository = null;
                _physicalInventoryRepository = null;
                _physicalInventoryAdminService = null;
                _accountingRepository = null;
                _SettingInventoryRepository = null;
                _WarehouseRepository = null;
                _ProductGroupsRepository = null;
                _PaymentsConceptRepository = null;
                _PUCRepository = null;
                _InventoryProductRepository = null;
                _LoanMerchandiseDetailRepository = null;
                _LoanMerchandiseRepository = null;
                _InventoryControlDocumentRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
