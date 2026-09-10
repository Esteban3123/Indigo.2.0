//'************************************************************
//' Assembly         : Domain.Inventory.EntranceVoucherRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 09/03/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

#region Imported Libraries
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
using static Domain.Payroll.Entities.HumanTalentParametrization;
#endregion

namespace Application.Inventory.AppEntranceVoucherDevolution
{
    public class EntranceVoucherDevolutionAdminService : IEntranceVoucherDevolutionAdminService
    {
        #region Variables
        private IEntranceVoucherDevolutionRepository _EntranceVoucherDevolutionRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private ISequensePaymentsCRepository _sequensePaymentsCRepository;
        private IEntranceVoucherRepository _entranceVoucherRepository;
        private IAccountPayableRepository _accountPayableRepository;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IWarehouseRepository _warehouseRepository;
        private ISupplierRepository _supplierRepository;
        private ISuppliersDistributionLinesRepository _suppliersDistributionLinesRepository;
        private IOtherWithholdingDeductionRepository _otherWithholdingDeductionRepository;
        private INotesDebitCreditAdminService _notesDebitCreditAdminService;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IInventoryService _inventoryServices;
        private IRetentionConceptRepository _retentionConceptRepository;
        private IProductGroupsRepository _productGroupsRepository;
        private IGeneralLedgerIVARepository _generalLedgerIVARepository;
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;

        #endregion

        #region Builder
        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="EntranceVoucherDetailRepository"></param>
        /// <param name="sequenseRepository"></param>IEntranceVoucherAdminService
        public EntranceVoucherDevolutionAdminService(IEntranceVoucherDevolutionRepository EntranceVoucherDevolutionRepository, IInventorySequenceDetailRepository sequenseRepository,
            ISequensePaymentsCRepository SequensePaymentsCRepository, IEntranceVoucherRepository entranceVoucherRepository,
            IAccountPayableRepository accountPayableRepository, ISettingInventoryRepository settingInventoryRepository, IWarehouseRepository WarehouseRepository, ISupplierRepository SupplierRepository,
            ISuppliersDistributionLinesRepository SuppliersDistributionLinesRepository, IOtherWithholdingDeductionRepository OtherWithholdingDeductionRepository, INotesDebitCreditAdminService NotesDebitCreditAdminService,
            IPhysicalInventoryAdminService PhysicalInventoryAdminService, IInventoryService inventoryServices, IRetentionConceptRepository retentionConceptRepository, IProductGroupsRepository productGroupsRepository,
            IGeneralLedgerIVARepository generalLedgerIVARepository,IPurchaseOrderDetailRepository purchaseOrderDetailRepository)
        {
            if (EntranceVoucherDevolutionRepository == null)
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if (SequensePaymentsCRepository == null)
            {
                throw new ArgumentNullException("Repositorio de SequensePaymentsCRepository vacio");
            }
            if (entranceVoucherRepository == null)
            {
                throw new ArgumentNullException("Repositorio de entranceVoucherRepository vacio");
            }
            if (accountPayableRepository == null)
            {
                throw new ArgumentNullException("Repositorio de accountPayableRepository vacio");
            }
            if (settingInventoryRepository == null)
            {
                throw new ArgumentNullException("Repositorio de settingInventoryRepository vacio");
            }
            if (WarehouseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de WarehouseRepository vacio");
            }
            if (SupplierRepository == null)
            {
                throw new ArgumentNullException("Repositorio de SupplierRepository vacio");
            }
            if (SuppliersDistributionLinesRepository == null)
            {
                throw new ArgumentNullException("Repositorio de SuppliersDistributionLinesRepository vacio");
            }
            if (OtherWithholdingDeductionRepository == null)
            {
                throw new ArgumentNullException("Repositorio de OtherWithholdingDeductionRepository vacio");
            }
            if (NotesDebitCreditAdminService == null)
            {
                throw new ArgumentNullException("Repositorio de NotesDebitCreditAdminService vacio");
            }
            if (PhysicalInventoryAdminService == null)
            {
                throw new ArgumentNullException("Repositorio de PhysicalInventoryAdminService vacio");
            }
            if (inventoryServices == null)
            {
                throw new ArgumentNullException("inventoryServices");
            }
            _EntranceVoucherDevolutionRepository = EntranceVoucherDevolutionRepository;
            _sequenseRepository = sequenseRepository;
            _sequensePaymentsCRepository = SequensePaymentsCRepository;
            _entranceVoucherRepository = entranceVoucherRepository;
            _accountPayableRepository = accountPayableRepository;
            _settingInventoryRepository = settingInventoryRepository;
            _warehouseRepository = WarehouseRepository;
            _supplierRepository = SupplierRepository;
            _suppliersDistributionLinesRepository = SuppliersDistributionLinesRepository;
            _otherWithholdingDeductionRepository = OtherWithholdingDeductionRepository;
            _notesDebitCreditAdminService = NotesDebitCreditAdminService;
            _physicalInventoryAdminService = PhysicalInventoryAdminService;
            _inventoryServices = inventoryServices;
            _retentionConceptRepository = retentionConceptRepository;
            _productGroupsRepository = productGroupsRepository;
            _generalLedgerIVARepository = generalLedgerIVARepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Funcion que guarda la entidad de entrancevoucherdevolvution
        /// </summary>
        /// <param name="entranceVoucherDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveEntranceVoucherDevolution(EntranceVoucherDevolution entranceVoucherDevolution, AuditMessage audit, long idSecuence = 0, InventorySequence sequenceC = null)
        {
            if (entranceVoucherDevolution == null)
            {
                throw new ArgumentNullException("EntranceVoucherDevolution");
            }

            //Validaciones
            StringBuilder errors = new StringBuilder();
            if (entranceVoucherDevolution.Status < 3)
            {
                if (entranceVoucherDevolution.EntranceVoucherDevolutionDetail == null || entranceVoucherDevolution.EntranceVoucherDevolutionDetail.Count() == 0)
                {
                    errors.AppendLine("La devolución no contiene ningun detalle");
                }

                var entranceVoucher = _entranceVoucherRepository.GetEntranceVoucherById(entranceVoucherDevolution.EntranceVoucherId);
                if (entranceVoucher == null || entranceVoucher.Id == 0)
                {
                    errors.AppendLine("No se encontró el comprobante de entrada asociado a la devolución.");
                }

                if (entranceVoucherDevolution.WarehouseId != entranceVoucher.WarehouseId)
                {
                    errors.AppendLine("El almacen de la devolución no corresponde con el almacén del comprobante de entrada.");
                }
            }
            else
            {
                if (entranceVoucherDevolution.Id == 0)
                {
                    errors.AppendLine("No se tiene la información necesaria de la devolución");
                }
            }

            if (errors.Length > 0)
            {
                return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, Message = errors.ToString() };
            }

            IUnitWork unitOfWork = _EntranceVoucherDevolutionRepository.UnitWork;
            IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    InventorySequenceDetail seq = (idSecuence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence)));
                    if (entranceVoucherDevolution.Code == null || entranceVoucherDevolution.Code.Trim().Equals(string.Empty))
                    {
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = entranceVoucherDevolution.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(entranceVoucherDevolution.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                entranceVoucherDevolution.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                transaction.Dispose();
                                return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }

                    Domain.Entities.EntranceVoucherDevolution auxEntranceVoucher = null;
                    IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (entranceVoucherDevolution.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        entranceVoucherDevolution.CreationUser = audit.CodeUser;
                        entranceVoucherDevolution.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        auxEntranceVoucher = entranceVoucherDevolution.OriginalValue;
                        entranceVoucherDevolution.ModificationUser = audit.CodeUser;
                        entranceVoucherDevolution.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    _EntranceVoucherDevolutionRepository.SaveEntity(entranceVoucherDevolution);
                    unitOfWork.Commit();
                    unitOfWorkSequense.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution>(entranceVoucherDevolution, audit, status, auxEntranceVoucher);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = entranceVoucherDevolution };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    String message = ex.Message;
                    if (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        message = ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.InnerException.Message))
                        {
                            message = ex.InnerException.InnerException.Message;
                        }
                    }

                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, MessageResult = new List<string> { message }  };
                }
            }
        }

        /// <summary>
        /// Funcion que Elimina la entidad de Entrance Voucher Detail
        /// </summary>
        /// <param name="EntranceVoucherDevolution"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution, AuditMessage audit)
        {
            if (EntranceVoucherDevolution == null)
            {
                throw new ArgumentNullException("EntranceVoucherDevolution");
            }

            IUnitWork unitOfWork = _EntranceVoucherDevolutionRepository.UnitWork;
            try
            {
                EntranceVoucherDevolution.MarkAsDeleted();
                IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution>(EntranceVoucherDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _EntranceVoucherDevolutionRepository.DeleteEntity(EntranceVoucherDevolution);
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
        /// Funcion utilizada pra modificar el estado del registro
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.EntranceVoucherDevolution> ChangeStateEntranceVoucherDevolution(string code, byte state, AuditMessage audit)
        {
            Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution = _EntranceVoucherDevolutionRepository.GetEntranceVoucherDevolution(code);
            EntranceVoucherDevolution.Status = state;
            return SaveEntranceVoucherDevolution(EntranceVoucherDevolution, audit);
        }

        /// <summary>
        /// Funcion que ejerce la consulta de EntranceVoucherDevolution por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.EntranceVoucherDevolution> GetEntranceVoucherDevolution(string code, AuditMessage audit)
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
                Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution = _EntranceVoucherDevolutionRepository.GetEntranceVoucherDevolution(code);
                if (EntranceVoucherDevolution != null && EntranceVoucherDevolution.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution>(EntranceVoucherDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = EntranceVoucherDevolution };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Funcion que ejerce la consulta de EntranceVoucherDevolution por id
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Entities.EntranceVoucherDevolution GetEntranceVoucherDevolutionById(int idEntranceVoucherDevolution)
        {
            if (idEntranceVoucherDevolution == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _EntranceVoucherDevolutionRepository.GetEntranceVoucherDevolutionById(idEntranceVoucherDevolution);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Guarda y confirma una devolucion de compra
        /// </summary>
        /// <param name="entranceDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveAndConfirmbEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution entranceDevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null)
        {
            var result = SaveEntranceVoucherDevolution(entranceDevolution, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = ConfirmEntranceVoucherDevolution(result.ObjectEmbbeded, audit);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = string.Format(ResourceManager.get_GetString("SaveAndConfirmEntranceVoucherDevolution", "Inventory"), result.ObjectEmbbeded.Code, resultConfirm.MessageResult.ElementAt(0)) };

                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = string.Format(ResourceManager.get_GetString("UpdateAndConfirmEntranceVoucherDevolution", "Inventory"), result.ObjectEmbbeded.Code, resultConfirm.MessageResult.ElementAt(0)) };
                    }
                }
                else
                {
                    if (resultConfirm.StateResult == false && resultConfirm.StateResultAux == false)
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, (resultConfirm.Message != null && !resultConfirm.Message.Trim().Equals("") ? resultConfirm.Message : string.Join(Environment.NewLine, resultConfirm.MessageResult))) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + (resultConfirm.Message != null && !resultConfirm.Message.Trim().Equals("") ? resultConfirm.Message : string.Join(Environment.NewLine, resultConfirm.MessageResult)))) };
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, string.Join(Environment.NewLine, resultConfirm.MessageResult)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + string.Join(Environment.NewLine, resultConfirm.MessageResult))) };
                        }
                    }
                }
            }
            else
            {
                return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
            }
        }


        public ActionResult<Domain.Entities.EntranceVoucherDevolution> ConfirmEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution entranceDevolution, AuditMessage audit)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                IUnitWork entranceVoucherDevolutionUnitWork = _EntranceVoucherDevolutionRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution> auditProcess;
                try
                {
                    //////-------------------------Genero la Nota-------------------------////////
                    Domain.Entities.SettingInventory settings = _settingInventoryRepository.GetSettingInventory(entranceDevolution.OperatingUnitId);
                    Domain.Entities.EntranceVoucher entranceVoucher = _entranceVoucherRepository.GetEntranceVoucherById(entranceDevolution.EntranceVoucherId);
                    Domain.Entities.AccountPayable accountPayable = _accountPayableRepository.GetAccountPayableById(Convert.ToInt32(entranceVoucher.AccountPayableId));
                    ActionResult<Domain.Entities.PaymentNotes> resultAcp = CreateNoteDebitCreditEntranceVoucherDevolution(settings, entranceDevolution, entranceVoucher, accountPayable);
                    if (!resultAcp.StateResult)
                    {
                        entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, StateResultAux = false, MessageResult = resultAcp.MessageResult };
                    }
                    PaymentNotes paymentNotes = resultAcp.ObjectEmbbeded;
                    if (paymentNotes == null)
                    {
                        entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "No se puede generar la Nota Debito." } };
                    }
                    // consulto la secuencia numerica
                    PaymentsSecuence sequensePayments = _sequensePaymentsCRepository.GetSequenseByIdForm("731");
                    if (sequensePayments.Id == 0)
                    {
                        entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "No se puede generar la Nota Debito porque no existe secuencia numérica para generar el consecutivo" } };
                    }
                    // Agrego la cuenta por pagar a un listado
                    List<AccountPayable> ListAccountPayable = new List<AccountPayable>();
                    accountPayable.Adjustment = entranceDevolution.TotalValue;
                    accountPayable.HandlesAddModifyDelete = 1;
                    accountPayable.AccountPayableShares.ElementAt(0).ValueNoteShare = entranceDevolution.TotalValue;

                    if(entranceDevolution.EntranceVoucherDevolutionObligationBudget != null && entranceDevolution.EntranceVoucherDevolutionObligationBudget.Count > 0)
                    {
                        foreach(var item in entranceDevolution.EntranceVoucherDevolutionObligationBudget)
                        {
                            AccountPayableCommitments accountPayableCommitments = new AccountPayableCommitments();
                            accountPayableCommitments.Id = item.ObligationDetailId;
                            accountPayableCommitments.AccountPayableId = accountPayable.Id;
                            accountPayableCommitments.CommitmentDetailId = item.CommitmentDetailId;
                            accountPayableCommitments.Value = item.Value;
                            accountPayable.AccountPayableCommitments.Add(accountPayableCommitments);
                        }
                    }

                    ListAccountPayable.Add(accountPayable);
                    List<AdvancePayments> ListAdvancePayments = new List<AdvancePayments>();

                    //Valida la secuencia para la unidad operativa actual.
                    PaymentsSecuenceDetail sequensePaymentsResult;
                    if (sequensePayments.Scope == "O")
                    {
                        sequensePaymentsResult = sequensePayments.PaymentsSecuenceDetail.FirstOrDefault();
                    }
                    else
                    {
                        sequensePaymentsResult = sequensePayments.PaymentsSecuenceDetail.Where(x => x.IdOperatingUnit == entranceDevolution.OperatingUnitId).FirstOrDefault();
                    }

                    if (sequensePaymentsResult == null ||sequensePaymentsResult.Id == 0  )
                    {
                        entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "No se puede generar la Nota Debito porque no existe secuencia numérica para la unidad operativa" } };
                    }

                    // guarda y confirma la nota debito y genera el comprobante contable
                    ActionResult<PaymentNotes> resultNotes = _notesDebitCreditAdminService.SavePaymentNotesComplete(paymentNotes, ListAccountPayable, ListAdvancePayments, true, audit, sequensePaymentsResult.Id);
                    if (resultNotes.StateResult == false)
                    {
                        entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, StateResultAux = false, MessageResult = resultNotes.MessageResult, Message = resultNotes.Message };
                    }
                    entranceDevolution.PaymentNoteId = resultNotes.ObjectEmbbeded.Id;

                    List<string> MessagesStock = new List<string>();
					//////---------Modifico las cantidades en el inventario físico---------////////
					var entranceVoucherDevolutionDetailSortedList = entranceDevolution.EntranceVoucherDevolutionDetail.OrderBy(detail => detail.Quantity);
					foreach (var detail in entranceVoucherDevolutionDetailSortedList)
					{
                        if (detail.Quantity > 0)
                        {
                            int? Batch = null;
                            if (detail.BatchSerialId != 0)
                            {
                                Batch = detail.BatchSerialId;
                            }
                            List<string> errors = new List<string>();

                            Domain.Entities.InventoryProduct product = _EntranceVoucherDevolutionRepository.GetInventoryProductByEntranceVoucherDetailBatchSerialId(detail.EntranceVoucherDetailBatchSerialId);

                            decimal valueInkardex = _entranceVoucherRepository.GetEntrancerVoucerValueInKardex(entranceVoucher.Id, product.Id);

                            //Agrego variable para controlar la afectación del inventario, ya que las devoluciones de remisiones no deben afectar el inventario
                            int? RemissionEntranceDetailBatchSerialId = detail?.EntranceVoucherDetailBatchSerial?.EntranceVoucherDetail?.RemissionEntranceDetailBatchSerialId;

                            List<Kardex> listKardex = new List<Kardex>()
                                {
                                    new Kardex()
                                    {
                                        ProductId = product.Id,
                                        WarehouseId = entranceDevolution.WarehouseId,
                                        BatchSerialId = Batch,
                                        MovementType = 2,
                                        Quantity = detail.Quantity,
                                        Value = valueInkardex,
                                        AffectInventory = RemissionEntranceDetailBatchSerialId == null
                                    }
                                };
                            var result = _physicalInventoryAdminService.SavePhysicalInventory(listKardex, entranceDevolution.Id, entranceDevolution.Code, entranceDevolution.GetType().Name, entranceDevolution.CreationUser);
                            if (result.StateResult == false)
                            {
                                entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                                transaction.Dispose();
                                return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { result.Message } };
                            }
                            //Validaciones Stock
                            var resultStock = _inventoryServices.ValidateStockProducts(product.Id, entranceDevolution.OperatingUnitId, entranceDevolution.WarehouseId, 0, 0);
                            if (resultStock.StateResult == false)
                            {
                                MessagesStock.Add(resultStock.Message);
                            }

                            var batchSerial = entranceVoucher.EntranceVoucherDetail.SelectMany(d => d.EntranceVoucherDetailBatchSerial).Where(bs => bs.Id == detail.EntranceVoucherDetailBatchSerialId).FirstOrDefault();
                            if (batchSerial == null || batchSerial.OutstandingQuantity < detail.Quantity)
                            {
                                entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                                transaction.Dispose();
                                return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "El item '" + product.Code + " - " + product.Name + "' no tiene la cantidad suficiente a devolver" } };
                            }
                            batchSerial.OutstandingQuantity = batchSerial.OutstandingQuantity - detail.Quantity;
                            var EntranceVoucherDetail = entranceVoucher.EntranceVoucherDetail.SelectMany(d => entranceVoucher.EntranceVoucherDetail).Where(ev => ev.Id == batchSerial.EntranceVoucherDetailId).FirstOrDefault();
							int? purchaseOrderDetailId = EntranceVoucherDetail?.PurchaseOrderDetailId;
							if (purchaseOrderDetailId != null)
							{
								var purchaseOrderdetail = _purchaseOrderDetailRepository.GetPurchaseOrderDetailById(purchaseOrderDetailId.Value);

								if (purchaseOrderdetail != null)
								{
									purchaseOrderdetail.OutstandingQuantity += detail.Quantity;
									purchaseOrderdetail.CancelledQuantity -= detail.Quantity;
									purchaseOrderdetail.MarkAsModified();
									_purchaseOrderDetailRepository.SaveEntity(purchaseOrderdetail);
									_purchaseOrderDetailRepository.UnitWork.Commit();
								}
							}
                        }
                    }
                    entranceDevolution.Status = 2;
                    entranceDevolution.ConfirmationDate = DateTime.Now;
                    entranceDevolution.ConfirmationUser = audit.CodeUser;
                    entranceDevolution.ModificationDate = DateTime.Now;
                    entranceDevolution.ModificationUser = audit.CodeUser;
                    entranceDevolution.MarkAsModified();
                    _EntranceVoucherDevolutionRepository.SaveEntity(entranceDevolution);

                    entranceVoucherDevolutionUnitWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucherDevolution>(entranceDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, entranceDevolution.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();

                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution> { StateResult = true, StateResultAux = true, ObjectEmbbeded = entranceDevolution, MessageResultAux = MessagesStock, MessageResult = new List<string> { resultNotes.MessageResult[0] } };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = new List<string>() { ResourceManager.get_GetString("ErrorConcurrence") }
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = new List<string>() { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
                catch (Exception ex)
                {
                    entranceVoucherDevolutionUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucherDevolution>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = new List<string>() { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
            }
        }

        private ActionResult<PaymentNotes> CreateNoteDebitCreditEntranceVoucherDevolution(Domain.Entities.SettingInventory settings, Domain.Entities.EntranceVoucherDevolution entranceDevolution, Domain.Entities.EntranceVoucher entranceVoucher, Domain.Entities.AccountPayable accountPayable)
        {
            List<string> errors = new List<string>();   // 731
            // diccionario subgrupo producto
            Dictionary<int, Domain.Entities.ProductGroup> dictionaryProductGroup = new Dictionary<int, Domain.Entities.ProductGroup>();

            Domain.Entities.Warehouse warehouse = _warehouseRepository.GetWarehouseById(entranceDevolution.WarehouseId);
            PaymentNotes paymentNotes = new PaymentNotes();
            paymentNotes.NoteDate = entranceDevolution.DocumentDate;
            paymentNotes.Status = 1;
            paymentNotes.IdSupplier = entranceVoucher.SupplierId;
            paymentNotes.IdSupplierDistributionLines = entranceVoucher.SupplierDistributionLineId;
            paymentNotes.IdCostCenter = null;
            paymentNotes.Comment = "Nota Debito generada por Devolución de Comprobante de Entrada " + entranceDevolution.Code;
            paymentNotes.Nature = 1; //Debito
            paymentNotes.Reinstatement = false;
            paymentNotes.IdVoucherTransaction = null;
            paymentNotes.IndicatesBillAdvance = 0;
            paymentNotes.CancelCheck = null;
            paymentNotes.BudgetInterface = false;
            paymentNotes.IdOperatingUnit = entranceDevolution.OperatingUnitId;
            paymentNotes.JournalVoucherId = settings.PurchaseReturnJournalVoucherTypeId;
            paymentNotes.EntityId = entranceDevolution.Id;
            paymentNotes.EntityCode = entranceDevolution.Code;
            paymentNotes.EntityName = entranceDevolution.GetType().Name;
            paymentNotes.AllowDiscountPromptPayment = true;
            paymentNotes.AffectBaseToDiscount = false;
            paymentNotes.CurrencyId = entranceVoucher.CurrencyId;

            byte? taxRegistration = (entranceVoucher?.TaxRegistration is null || entranceVoucher.TaxRegistration == 0) ? settings?.TaxRegistration : entranceVoucher.TaxRegistration;


            // Detalle de la nota
            Domain.Entities.PaymentsNoteDetails paymentNotesDetail;
            foreach (var item in entranceDevolution.EntranceVoucherDevolutionDetail)
            {
                if (item.Quantity > 0)
                {

                    Domain.Entities.InventoryProduct product = _EntranceVoucherDevolutionRepository.GetInventoryProductByEntranceVoucherDetailBatchSerialId(item.EntranceVoucherDetailBatchSerialId);
                    int? _costCenterId = GetEntranceVoucherCostCenterByAccountId(settings.AssociateCostCenter, warehouse, product.ProductGroup);
                    if (!dictionaryProductGroup.ContainsKey(product.ProductGroup.Id))
                    {
                        //Se obtiene el valor de si el proveedor es declarante o no para poder sacar el id del concepto de pagos del grupo para la retención
                        bool isDeclarant = _supplierRepository.GetSupplierById(entranceVoucher.SupplierId, false).Declarant;
                        Domain.Entities.ProductGroup prodguctGroupTemp = _productGroupsRepository.GetProductGroupByIdForAccounting(product.ProductGroup.Id, isDeclarant);
                        dictionaryProductGroup.Add(product.ProductGroup.Id, prodguctGroupTemp);
                    }
                    Domain.Entities.ProductGroup productGroup = dictionaryProductGroup[product.ProductGroup.Id];

                    if (item.SubTotalValue > 0)
                    {
                        paymentNotesDetail = new PaymentsNoteDetails();
                        paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                        paymentNotesDetail.IdAccount = Convert.ToInt32(productGroup.AccountInventoryId);
                        paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                        paymentNotesDetail.IdCostCenter = _costCenterId;
                        paymentNotesDetail.Nature = 2; //Credito
                        paymentNotesDetail.BaseValue = item.SubTotalValue - item.DiscountValue;
                        paymentNotesDetail.BillingValue = paymentNotesDetail.BaseValue;
                        paymentNotesDetail.Value = Convert.ToDecimal(paymentNotesDetail.BaseValue);
                        paymentNotesDetail.TotalConceptValue = Convert.ToDecimal(paymentNotesDetail.BaseValue);
                        paymentNotesDetail.IdRetentionConcept = null;
                        paymentNotesDetail.Percentage = null;
                        paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada 'Valor Bruto Producto'";
                        paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
                    }

                    if (item.IvaValue > 0)
                    {
                        paymentNotesDetail = new PaymentsNoteDetails();
                        switch (taxRegistration)
                        {
                            case 1:
                                paymentNotesDetail.IdAccount = Convert.ToInt32(productGroup.AccountInventoryId);
                                break;
                            case 2:
                                paymentNotesDetail.IdAccount = _generalLedgerIVARepository.FirstOrDefault(x => x.Id == product.IVAId).IdAccountPurchaseService.Value;
                                break;
                        }
                        paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                        paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                        paymentNotesDetail.IdCostCenter = _costCenterId;
                        paymentNotesDetail.Nature = 2; //Credito
                        paymentNotesDetail.BaseValue = item.SubTotalValue - item.DiscountValue;
                        paymentNotesDetail.BillingValue = paymentNotesDetail.BaseValue;
                        paymentNotesDetail.Value = item.IvaValue;
                        paymentNotesDetail.TotalConceptValue = item.IvaValue;
                        paymentNotesDetail.IdRetentionConcept = null;
                        paymentNotesDetail.Percentage = null;
                        paymentNotesDetail.IdGeneralLedgerIVA = product.IVAId;
                        paymentNotesDetail.IVAValue = item.IvaValue;
                        paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada 'Valor Bruto Producto'";
                        paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
                    }

                    if (item.RtfValue > 0)
                    {
						PaymentsNoteDetails detail = null;
						if (product.RetentionConceptsWithholdingSourceId.HasValue)
						{
							detail = paymentNotes.PaymentsNoteDetails.FirstOrDefault(x => x.IdRetentionConcept == product.RetentionConceptsWithholdingSourceId.Value);
						}
						if (detail != null)
                        {
                            detail.BaseValue += item.SubTotalValue - item.DiscountValue;
                            detail.BillingValue += item.SubTotalValue - item.DiscountValue + item.IvaValue;
                            detail.Value += item.RtfValue;
                            detail.TotalConceptValue += item.RtfValue;
                        }
                        else
                        {
                            paymentNotesDetail = new PaymentsNoteDetails();
                            paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                            paymentNotesDetail.IdAccount = product.AccountWithholdingSourceId.Value;
                            paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                            paymentNotesDetail.IdCostCenter = _costCenterId;
                            paymentNotesDetail.Nature = 1; //Debito
                            paymentNotesDetail.BaseValue = item.SubTotalValue - item.DiscountValue;
                            paymentNotesDetail.BillingValue = item.SubTotalValue - item.DiscountValue + item.IvaValue;
                            paymentNotesDetail.Value = item.RtfValue;
                            paymentNotesDetail.TotalConceptValue = item.RtfValue;
							paymentNotesDetail.IdRetentionConcept = product.RetentionConceptsWithholdingSourceId;
							paymentNotesDetail.Percentage = item.RtfPercentageProduct;
                            paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada 'RTF Productos'";
                            paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
                        }
                    }
                }
            }

            //Agrupación por conceptos y redondeo
            //var PaymentNoteDetailConcept = paymentNotes.PaymentsNoteDetails.Where(x => x.IdRetentionConcept != null).ToList();
            //if (PaymentNoteDetailConcept != null)
            //{
            //    foreach (var item in PaymentNoteDetailConcept)
            //    {
            //        int TypeRounding = _retentionConceptRepository.GetRetentionById(item.IdRetentionConcept ?? 0).TypeRounding;
            //        if (TypeRounding == 0) { break; }
            //        item.Value = (decimal)Utils.RoundValue((decimal)item.Value, TypeRounding);
            //    }
            //}

            //Retención Iva
            if (entranceDevolution.WithholdingTax > 0)
            {
                paymentNotesDetail = new PaymentsNoteDetails();
                paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                if (settings.IVARetention == 1)
                {
                    AccountPayableConcepts IvaConcept = _supplierRepository.GetIVARetentionAccountPayableConceptBySupplierId(entranceVoucher.SupplierId);
                    paymentNotesDetail.IdAccount = Convert.ToInt32(IvaConcept.IdAccount);
                    paymentNotesDetail.IdRetentionConcept = IvaConcept.RetentionConceptId;
                    paymentNotesDetail.Percentage = _supplierRepository.GetIVARetentionPercentageBySupplierId(entranceVoucher.SupplierId);
                }
                else
                {
                    paymentNotesDetail.IdAccount = Convert.ToInt32(settings.AccountIVARetentionId);
                    paymentNotesDetail.IdRetentionConcept = settings.RetentionConceptsIVARetentionId;
                    paymentNotesDetail.Percentage = Convert.ToDecimal(settings.WithholdingIvaPercentage);
                }
                paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                paymentNotesDetail.IdCostCenter = warehouse.CostCenterId;
                paymentNotesDetail.Nature = 1; //Debito
                paymentNotesDetail.BaseValue = entranceDevolution.ValueTax;
                paymentNotesDetail.BillingValue = entranceDevolution.TotalValue;
                paymentNotesDetail.Value = entranceDevolution.WithholdingTax;
                paymentNotesDetail.TotalConceptValue = entranceDevolution.WithholdingTax;
                paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada 'Retencion IVA'";
                paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
            }

            // Retencion ICA
            if (entranceDevolution.WithholdingICA > 0)
            {
                paymentNotesDetail = new PaymentsNoteDetails();
                AccountPayableConcepts apc = _suppliersDistributionLinesRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitId(entranceVoucher.SupplierDistributionLineId, entranceVoucher.OperatingUnitId);
                paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                paymentNotesDetail.IdAccount = Convert.ToInt32(apc.IdAccount);
                paymentNotesDetail.IdRetentionConcept = apc.RetentionConceptId;
                paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                paymentNotesDetail.IdCostCenter = warehouse.CostCenterId;
                paymentNotesDetail.Nature = 1; //Debito
                paymentNotesDetail.BaseValue = entranceDevolution.Value - entranceDevolution.ValueDiscount;
                paymentNotesDetail.BillingValue = entranceDevolution.TotalValue;
                paymentNotesDetail.Value = Convert.ToDecimal(entranceDevolution.WithholdingICA);
                paymentNotesDetail.TotalConceptValue = Convert.ToDecimal(entranceDevolution.WithholdingICA);
                paymentNotesDetail.Percentage = entranceDevolution.WithholdingIcaPercentage;
                paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada (Valor Retencion ICA) ";
                paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
            }

            // Si la devolucion es completa
            if (entranceDevolution.DevolutionType)
            {
                if (entranceDevolution.FreightValue > 0)
                {
                    paymentNotesDetail = new PaymentsNoteDetails();
                    paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                    paymentNotesDetail.IdAccount = settings.FreigthAcountId;
                    paymentNotesDetail.IdRetentionConcept = null;
                    paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                    if (settings.FreightHandlesCostCenter)
                    {
                        paymentNotesDetail.IdCostCenter = warehouse.CostCenterId;
                        //errors.Add(String.Format(ResourceManager.get_GetString("FreightMainAccountDontHandledCostCenter"), settings.FreightAccountCodeName));
                    }
                    else
                    {
                        paymentNotesDetail.IdCostCenter = null;
                    }
                    paymentNotesDetail.BaseValue = entranceDevolution.FreightValue;
                    paymentNotesDetail.BillingValue = entranceDevolution.FreightValue;
                    paymentNotesDetail.Value = Convert.ToDecimal(entranceDevolution.FreightValue);
                    paymentNotesDetail.TotalConceptValue = Convert.ToDecimal(entranceDevolution.FreightValue);
                    paymentNotesDetail.Percentage = entranceDevolution.WithholdingIcaPercentage;
                    paymentNotesDetail.Nature = 2; //Credito
                    paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada (Valor Flete) ";
                    paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
                    // IVA Flete
                    if (entranceVoucher.FreightIVAValue > 0)
                    {
                        paymentNotesDetail = new PaymentsNoteDetails();
                        paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                        paymentNotesDetail.IdAccount = Convert.ToInt32(settings.IvaFreigthAccountId);
                        paymentNotesDetail.IdRetentionConcept = settings.IvaFreigthRetentionConceptsId;
                        paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                        if (settings.IvaFreightHandlesCostCenter)
                        {
                            paymentNotesDetail.IdCostCenter = warehouse.CostCenterId;
                            //errors.Add(String.Format(ResourceManager.get_GetString("FreightIvaMainAccountDontHandledCostCenter"), settings.IvaFreightAccountCodeName));
                        }
                        else
                        {
                            paymentNotesDetail.IdCostCenter = null;
                        }
                        paymentNotesDetail.BaseValue = entranceDevolution.FreightIVAValue;
                        paymentNotesDetail.BillingValue = entranceDevolution.FreightIVAValue;
                        paymentNotesDetail.Value = Convert.ToDecimal(entranceDevolution.FreightIVAValue);
                        paymentNotesDetail.TotalConceptValue = Convert.ToDecimal(entranceDevolution.FreightIVAValue);
                        paymentNotesDetail.Percentage = entranceDevolution.FreightIVAPercentage;
                        paymentNotesDetail.Nature = 2; //Credito
                        paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada (Valor IVA Flete) ";
                        paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
                    }
                }

                // Otras retenciones y deducciones
                if (entranceDevolution.EntranceVoucherDevolutionOtherDeduction != null)
                {
                    if (entranceDevolution.EntranceVoucherDevolutionOtherDeduction.Count > 0)
                    {
                        List<Domain.Entities.OtherWithholdingDeduction> ListRetentionDeduction = _otherWithholdingDeductionRepository.ListOtherWithholdingDeduction();
                        foreach (var Dod in entranceDevolution.EntranceVoucherDevolutionOtherDeduction)
                        {
                            if (Dod.Type == 2 && !entranceDevolution.DevolutionType)
                            {
                                continue;
                            }
                            paymentNotesDetail = new PaymentsNoteDetails();
                            Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction = new Domain.Entities.OtherWithholdingDeduction();
                            foreach (var retentionDeduction in ListRetentionDeduction)
                            {
                                if (retentionDeduction.Id == Dod.OtherWithholdingDeductionId)
                                {
                                    otherWithholdingDeduction = retentionDeduction;
                                    break;
                                }
                            }
                            paymentNotesDetail.IdAccountPayableConceptNotes = settings.RefundAccountPayableConceptNoteId;
                            paymentNotesDetail.IdAccount = Convert.ToInt32(otherWithholdingDeduction.AccountPayableConcepts.IdAccount);
                            paymentNotesDetail.Nature = 1; //Debito
                            paymentNotesDetail.BaseValue = Convert.ToDecimal(Utils.RoundValue(Convert.ToDecimal(Dod.Value), entranceVoucher.RoundService));
                            paymentNotesDetail.BillingValue = paymentNotesDetail.BaseValue;
                            paymentNotesDetail.Value = Convert.ToDecimal(paymentNotesDetail.BaseValue);
                            paymentNotesDetail.TotalConceptValue = Convert.ToDecimal(paymentNotesDetail.BaseValue);
                            string var = string.Empty;
                            if (Dod.Type == 1) // Otras Retenciones
                            {
                                paymentNotesDetail.IdRetentionConcept = otherWithholdingDeduction.AccountPayableConcepts.RetentionConceptId;
                                paymentNotesDetail.Percentage = entranceDevolution.WithholdingIcaPercentage;
                                var = "'Otras Retenciones'";
                            }
                            else // Otras Deducciones
                            {
                                paymentNotesDetail.IdRetentionConcept = null;
                                paymentNotesDetail.Percentage = null;
                                var = "'Otras Deduciones'";
                            }
                            paymentNotesDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                            if (otherWithholdingDeduction.AccountPayableConcepts.MainAccounts.HandlesCostCenter)
                            {
                                paymentNotesDetail.IdCostCenter = warehouse.CostCenterId;
                            }
                            else
                            {
                                paymentNotesDetail.IdCostCenter = null;
                            }
                            paymentNotesDetail.Comments = "Detalle Nota Debito generada por Devolución de Comprobante de Entrada (" + var + ") ";

                            paymentNotes.PaymentsNoteDetails.Add(paymentNotesDetail);
                        }
                    }
                }
            }

            if (errors.Count > 0)
            {
                return new ActionResult<Domain.Entities.PaymentNotes> { StateResult = false, MessageResult = errors.Distinct().ToList() };
            }
            return new ActionResult<Domain.Entities.PaymentNotes> { StateResult = true, ObjectEmbbeded = paymentNotes };
        }

        public int? GetEntranceVoucherCostCenterByAccountId(int AssociateCostCenter, Domain.Entities.Warehouse warehouse, Domain.Entities.ProductGroup productGroup = null)
        {
            if (AssociateCostCenter == 1)
            {
                return null;
            }
            else if (AssociateCostCenter == 2)
            {
                if (productGroup == null)
                {
                    return null;
                }
                return productGroup.CostCenterId;
            }
            else
            {
                return warehouse.CostCenterId;
            }
        }

        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _EntranceVoucherDevolutionRepository.CascadeRollback(value);
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
                    _notesDebitCreditAdminService.Dispose();
                    _physicalInventoryAdminService.Dispose();
                    _inventoryServices.Dispose();
                }
                _EntranceVoucherDevolutionRepository = null;
                _sequenseRepository = null;
                _sequensePaymentsCRepository = null;
                _entranceVoucherRepository = null;
                _accountPayableRepository = null;
                _settingInventoryRepository = null;
                _warehouseRepository = null;
                _supplierRepository = null;
                _suppliersDistributionLinesRepository = null;
                _otherWithholdingDeductionRepository = null;
                _notesDebitCreditAdminService = null;
                _physicalInventoryAdminService = null;
                _inventoryServices = null;
                _productGroupsRepository = null;
                _generalLedgerIVARepository = null;

                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
