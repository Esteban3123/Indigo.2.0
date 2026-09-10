///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Rafel Eduardo Patiño Cabrera
/// Created          : 24-04-2015
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
using System.Threading.Tasks;

namespace Application.Inventory.LoanMerchandise
{
    public class LoanMerchandiseAdminService : ILoanMerchandiseAdminService
    {

        #region fields
        private ILoanMerchandiseRepository _LoanMerchandiseRepository;
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
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        #endregion

        #region builder
        public LoanMerchandiseAdminService(ILoanMerchandiseRepository LoanMerchandiseRepository, IInventorySequenceDetailRepository sequenseRepository, IPhysicalInventoryAdminService physicalInventoryAdminService,
                                            IPhysicalInventoryRepository physicalInventoryRepository, IAccountingDocumentAdminService accountingRepository,
                                            ISettingInventoryRepository SettingInventoryRepository, IWarehouseRepository WarehouseRepository, IProductGroupsRepository ProductGroupsRepository,
                                            IPaymentsConceptRepository PaymentsConceptRepository, IPUCRepository PUCRepository, IInventoryProductRepository InventoryProductRepository,
                                            IInventoryControlDocumentRepository InventoryControlDocumentRepository)
        {
            if (LoanMerchandiseRepository == null)
            {
                throw new ArgumentNullException("LoanMerchandiseRepository");
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
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            _LoanMerchandiseRepository = LoanMerchandiseRepository;
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
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
        }
        #endregion

        #region Metodos


        /// <summary>
        /// obtiene una solicitud de prestamo
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Entities.LoanMerchandise GetLoanMerchadiseByCode(string Code, AuditMessage audit)
        {
            try
            {
                Domain.Entities.LoanMerchandise loadMerchandise = _LoanMerchandiseRepository.GetLoanMerchandiseByCode(Code.Trim());
                if (loadMerchandise != null && loadMerchandise.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandise> auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandise>(loadMerchandise, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return loadMerchandise;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.LoanMerchandise();
            }
        }

        /// <summary>
        /// obtiene una solicitud de prestamo por el ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.LoanMerchandise GetLoanMerchadiseById(int id)
        {
            try
            {
                return _LoanMerchandiseRepository.GetLoanMerchandiseById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.LoanMerchandise();
            }
        }

        /// <summary>
        /// Guarda una solicitud de prestamo
        /// </summary>
        /// <param name="loadmerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.LoanMerchandise> SaveLoanMerchadise(Domain.Entities.LoanMerchandise loadMerchadise, AuditMessage audit, long idSequence = 0, InventorySequence sequenceC = null)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork unitOfWork = _LoanMerchandiseRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
                try
                {
                    InventorySequenceDetail seq = (idSequence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSequence)));
                    if (loadMerchadise.Code == null || loadMerchadise.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = loadMerchadise.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(loadMerchadise.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                loadMerchadise.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = false, StateResultAux = true, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    Domain.Entities.LoanMerchandise auxRemissionEntrance = null;
                    IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandise> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (loadMerchadise.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        loadMerchadise.CreationUser = audit.CodeUser;
                        loadMerchadise.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        inventoryControlDocument.DocumentNumber = loadMerchadise.Code;
                        inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.loanMerchadinsing; //prestamo mercancia
                        inventoryControlDocument.DocumentUser = audit.CodeUser;
                        inventoryControlDocument.DocumentDate = loadMerchadise.CreationDate;
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        auxRemissionEntrance = loadMerchadise.OriginalValue;
                        loadMerchadise.ModificationUser = audit.CodeUser;
                        loadMerchadise.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;

                        if (loadMerchadise.Status == 3)
                        {
                            loadMerchadise.AnnulmentUser = audit.CodeUser;
                            loadMerchadise.AnnulmentDate = DateTime.Now;
                            var documentControl = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(loadMerchadise.Code, (int)eTypeDocumentsControlInventory.loanMerchadinsing);
                            if (documentControl != null && documentControl.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                                _InventoryControlDocumentRepository.UnitWork.Commit();
                            }
                        }
                    }
                    foreach (Domain.Entities.LoanMerchandiseDetail detail in loadMerchadise.LoanMerchandiseDetail)
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
                    _LoanMerchandiseRepository.SaveEntity(loadMerchadise);
                    unitOfWork.Commit();
                    unitOfWorkSequense.Commit();
                    unitOfWorkControlDocuments.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandise>(loadMerchadise, audit, status, auxRemissionEntrance);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = true, ObjectEmbbeded = loadMerchadise };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = false, StateResultAux = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
            }
        }


        /// <summary>
        /// confirmar un Solicitud de Prestamo
        /// </summary>
        /// <param name="RemissionOutput"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.LoanMerchandise> ConfirmLoandMerchadise(Domain.Entities.LoanMerchandise LoanMerchandise, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork LoanMerchandiseUnitWork = _LoanMerchandiseRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandise> auditProcess;
                try
                {
                    List<Kardex> listKardex = new List<Kardex>();
                    foreach (var item in LoanMerchandise.LoanMerchandiseDetail)
                    {   
                        foreach (var itemLoanMerchandiseDetailBatchSerial in item.LoanMerchandiseDetailBatchSerial)
                        {
                            listKardex.Add(new Kardex()
                            {
                                ProductId = item.ProductId,
                                WarehouseId = LoanMerchandise.WarehouseId,
                                BatchSerialId = itemLoanMerchandiseDetailBatchSerial.BatchSerialId,
                                MovementType = (byte)(LoanMerchandise.LoanType == 1 ? 1 : 2),
                                Quantity = itemLoanMerchandiseDetailBatchSerial.Quantity,
                                Value = item.UnitValue,
                                AffectInventory = true
                            });
                        }
                    }

                    var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, LoanMerchandise.Id, LoanMerchandise.Code, LoanMerchandise.GetType().Name, LoanMerchandise.CreationUser);
                    if (result.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = false, StateResultAux = false, ObjectEmbbeded = LoanMerchandise, Message = result.Message };
                    }

                    //generamos comprobante
                    ActionResult<Domain.Entities.JournalVouchers> accounting = CreateAccountingAccount(LoanMerchandise);
                    if (accounting.StateResult == false)
                    {
                        LoanMerchandiseUnitWork.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.LoanMerchandise>
                        {
                            StateResult = false,
                            StateResultAux = false,
                            ObjectEmbbeded = LoanMerchandise,
                            Message = accounting.Message
                        };
                    }
                    //guardamos y confirmamos comprobante
                    ActionMessageResult<JournalVouchers> resultAccounting;
                    resultAccounting = _accountingRepository.SaveAccountingDocument(accounting.ObjectEmbbeded, audit);
                    if (resultAccounting.StateResult == false)
                    {
                        LoanMerchandiseUnitWork.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.LoanMerchandise>
                        {
                            StateResult = false,
                            StateResultAux = false,
                            ObjectEmbbeded = LoanMerchandise,
                            Message = resultAccounting.Message
                        };
                    }
                    //eliminamos doc. de control de inventarios
                    InventoryControlDocument inventoryControlDocument = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(LoanMerchandise.Code, (int)eTypeDocumentsControlInventory.loanMerchadinsing);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }
                    LoanMerchandise.Status = 2;
                    LoanMerchandise.ConfirmationDate = DateTime.Now;
                    LoanMerchandise.ConfirmationUser = audit.CodeUser;
                    LoanMerchandise.ModificationDate = DateTime.Now;
                    LoanMerchandise.ModificationUser = audit.CodeUser;
                    LoanMerchandise.MarkAsModified();
                    _LoanMerchandiseRepository.SaveEntity(LoanMerchandise);
                    LoanMerchandiseUnitWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.LoanMerchandise>(LoanMerchandise, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, LoanMerchandise.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = true, ObjectEmbbeded = LoanMerchandise, Message = "- Se Genero Comprobante Contable " + resultAccounting.ObjectEmbbeded.Consecutive.ToString() };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    LoanMerchandiseUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.LoanMerchandise>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        ObjectEmbbeded = LoanMerchandise,
                        Message = ResourceManager.get_GetString("ErrorConcurrence")
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    LoanMerchandiseUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandise>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        ObjectEmbbeded = LoanMerchandise,
                        Message = Utils.GetInnerExceptionMessageToString(ex)
                    };
                }
                catch (Exception ex)
                {
                    LoanMerchandiseUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.LoanMerchandise>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        ObjectEmbbeded = LoanMerchandise,
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
        public ActionResult<Domain.Entities.JournalVouchers> CreateAccountingAccount(Domain.Entities.LoanMerchandise LoanMerchandise)
        {
            Domain.Entities.SettingInventory Settinginventory = _SettingInventoryRepository.GetInventorySettingsRegister(LoanMerchandise.OperatingUnitId);
            if (Settinginventory == null)
            {
                return new ActionResult<Domain.Entities.JournalVouchers>
                {
                    StateResult = false,
                    StateResultAux = false,
                    Message = "No se Encontro Parametros De Inventario para la Unidad Operativa del Consecutivo -" + LoanMerchandise.Code
                };
            }
            Domain.Entities.Warehouse warehouse = _WarehouseRepository.GetWarehouseById(LoanMerchandise.WarehouseId);
            if (warehouse == null)
            {
                return new ActionResult<Domain.Entities.JournalVouchers>
                {
                    StateResult = false,
                    StateResultAux = false,
                    Message = "No se Encontro Almacen"
                };
            }
            Domain.Entities.JournalVouchers accounting = new Domain.Entities.JournalVouchers();
            string _CommentVouchers;
            decimal _BalanceInvoice = 0;
            if (LoanMerchandise.LoanType == 1)
            {
                _CommentVouchers = "Comprobante de Entrada de Prestamo de Mercancía";
            }
            else
            {
                _CommentVouchers = "Comprobante de Salida de Prestamo de Mercancía";
            }

            //Se crea la cabecera
            accounting.IdJournalVoucher = Settinginventory.LoanJournalVoucherTypeId;
            accounting.VoucherDate = LoanMerchandise.DocumentDate;
            accounting.Status = 2;
            accounting.Detail = _CommentVouchers;
            accounting.EntityCode = LoanMerchandise.Code;
            accounting.EntityId = LoanMerchandise.Id;
            accounting.EntityName = typeof(Domain.Entities.LoanMerchandise).Name;

            //Se crean los detalles
            foreach (Domain.Entities.LoanMerchandiseDetail itemDetail in LoanMerchandise.LoanMerchandiseDetail)
            {
                Domain.Entities.InventoryProduct InventoryProduct = _InventoryProductRepository.GetInventoryProductById(itemDetail.ProductId);

                if (InventoryProduct?.ProductGroupId is null) { return new ActionResult<JournalVouchers> { StateResult = false, StateResultAux= false,Message=$"El producto {InventoryProduct?.Code} no maneja Grupo" }; }

                Domain.Entities.ProductGroup ProductGroup = _ProductGroupsRepository.GetProductGroupById(InventoryProduct.ProductGroupId.Value);
                if (ProductGroup == null)
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
                if (LoanMerchandise.LoanType == 1) //prestamo de entrada   (debitamos la cuenta contable  de inventario que tiene el grupo  del producto - si es prestamo salida, se acredita la cuenta de Inventario)
                {
                    accountDetail2.DebitValue = Math.Round(itemDetail.UnitValue * itemDetail.Quantity, 2);
                    accountDetail2.CreditValue = 0;
                }
                else
                {
                    accountDetail2.DebitValue = 0; //
                    accountDetail2.CreditValue = Math.Round(itemDetail.UnitValue * itemDetail.Quantity, 2);
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
                accountDetail1.DebitValue = 0;
                accountDetail1.CreditValue = Math.Round(_BalanceInvoice, 2);
            }
            else
            {
                accountDetail1.IdMainAccount = warehouse.LoanThirdPartyDebitAccountId;
                accountDetail1.DebitValue = Math.Round(_BalanceInvoice, 2);
                accountDetail1.CreditValue = 0;
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
        /// Guarda y confirma un documentos
        /// </summary>
        /// <param name="LoanMerchandise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.LoanMerchandise> SaveAndConfirmLoadMerchadise(Domain.Entities.LoanMerchandise LoanMerchandise, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null)
        {
            var result = SaveLoanMerchadise(LoanMerchandise, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = ConfirmLoandMerchadise(LoanMerchandise, audit);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, Message = string.Format(ResourceManager.get_GetString("SavedAndConfirmedWithCode", "Inventory"), resultConfirm.ObjectEmbbeded.Code) };

                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, Message = ResourceManager.get_GetString("UpdatedAndConfirmed", "Inventory") + resultConfirm.Message };
                    }
                }
                else
                {
                    if (resultConfirm.StateResult == false && resultConfirm.StateResultAux == false)
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandise> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandise> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = true, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), resultConfirm.ObjectEmbbeded.Code, (Environment.NewLine + resultConfirm.Message)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + resultConfirm.Message)) };
                        }
                    }
                }
            }
            else
            {
                if (result.StateResult == false && result.StateResultAux == false)
                {
                    return new ActionResult<Domain.Entities.LoanMerchandise> { MessageResult = new List<string> { "" }, ObjectEmbbeded = result.ObjectEmbbeded, StateResult = false, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }
                else
                {
                    return new ActionResult<Domain.Entities.LoanMerchandise> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
                }

            }
        }

        public Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _LoanMerchandiseRepository.CascadeRollback(value);
            return Task.FromResult(res);
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
                _LoanMerchandiseRepository = null;
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
                _InventoryControlDocumentRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
