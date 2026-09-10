//'************************************************************
//' Assembly         : Domain.Inventory.EntranceVoucherRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 15/01/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Accounting;
using Application.Base;
using Application.Common;
using Application.Events.Serializers;
using Application.Inventory.PhysicalInventory;
using Application.Payments;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Domain.Entities.Service;
using Domain.Payroll;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Inventory.EntranceVoucher
{
    public class EntranceVoucherAdminService : IEntranceVoucherAdminService
    {
        #region Variables
        private IEntranceVoucherRepository _EntranceVoucherRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IPhysicalInventoryAdminService _physicalInventoryAdminService;
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        private IInventoryContractDetailRepository _inventoryContractDetailRepository;
        private IRemissionEntranceDetailBatchSerialRepository _remissionEntranceDetailBatchRepository;
        private IAccountPayableRepository _accountPayableRepository;
        private ISupplierRepository _supplierRepository;
        private IConsignmentCostListRepository _consignmentCostListRepository;
        private ISuppliersDistributionLinesRepository _suppliersDistributionLinesRepository;
        private ISettingInventoryRepository _settingInventoryRepository;
        private IInventoryProductRepository _inventoryProductRepository;
        private IOtherWithholdingDeductionRepository _otherWithholdingDeductionRepository;
        private IAccountPayableAdminService _accountPayableAdminService;
        private ISequensePaymentsCRepository _sequensePaymentsCRepository;
        private IWarehouseRepository _warehouseRepository;
        private IInventoryControlDocumentRepository _InventoryControlDocumentRepository;
        private IPaymentsConceptRepository _paymentConceptsRepository;
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IProductGroupsRepository _productGroupsRepository;
        private IConsignmentInventoryRemissionDetailControlRepository _consignmentInventoryRemissionDetailControlRepository;
        private IConsignmentInventoryRemissionDetailBatchSerialRepository _consignmentInventoryRemissionDetailBatchSerialRepository;
        private IFunctionalUnitRepository _functionalUnitRepository;
        private IGeneralLedgerIVARepository _generalLedgerIVARepository;
        private ICompanySettingsRepository _companySettingsRepository;
        private ICurrencyAdminService _currencyAdminService;
        private IPUCAdminService _pucAdminService;
        #endregion

        #region Builder
        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="EntranceVoucherRepository"></param>
        /// <param name="sequenseRepository"></param>IEntranceVoucherAdminService
        public EntranceVoucherAdminService(IEntranceVoucherRepository EntranceVoucherRepository, IInventorySequenceDetailRepository sequenseRepository, IPhysicalInventoryAdminService PhysicalInventoryAdminService,
            IPurchaseOrderDetailRepository PurchaseOrderDetailRepository, IInventoryContractDetailRepository InventoryContractDetailRepository, IRemissionEntranceDetailBatchSerialRepository RemissionEntranceDetailBatchSerialRepository,
            IAccountPayableRepository AccountPayableRepository, ISupplierRepository SupplierRepository, IConsignmentCostListRepository consignmentCostListRepository, ISuppliersDistributionLinesRepository SuppliersDistributionLinesRepository, ISettingInventoryRepository SettingInventoryRepository,
            IInventoryProductRepository InventoryProductRepository, IOtherWithholdingDeductionRepository OtherWithholdingDeductionRepository, IAccountPayableAdminService AccountPayableAdminService, ISequensePaymentsCRepository SequensePaymentsCRepository,
            IWarehouseRepository WarehouseRepository, IInventoryControlDocumentRepository InventoryControlDocumentRepository, IPaymentsConceptRepository paymentConceptsRepository,
            IPhysicalInventoryRepository physicalInventoryRepository, IProductGroupsRepository productGroupsRepository,
            IConsignmentInventoryRemissionDetailControlRepository consignmentInventoryRemissionDetailControlRepository, IConsignmentInventoryRemissionDetailBatchSerialRepository consignmentInventoryRemissionDetailBatchSerialRepository,
            IFunctionalUnitRepository functionalUnitRepository, IGeneralLedgerIVARepository GeneralLedgerIVARepository, ICompanySettingsRepository CompanySettingsRepository, ICurrencyAdminService CurrencyAdminService, IPUCAdminService PUCAdminService)
        {
            if (EntranceVoucherRepository == null)
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if (PhysicalInventoryAdminService == null)
            {
                throw new ArgumentNullException("Repositorio de PhysicalInventoryAdminService vacio");
            }
            if (PurchaseOrderDetailRepository == null)
            {
                throw new ArgumentNullException("Repositorio de PurchaseOrderDetailRepository vacio");
            }
            if (InventoryContractDetailRepository == null)
            {
                throw new ArgumentNullException("Repositorio de InventoryContractDetailRepository vacio");
            }
            if (RemissionEntranceDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("Repositorio de RemissionEntranceDetailBatchSerialRepository vacio");
            }
            if (AccountPayableRepository == null)
            {
                throw new ArgumentNullException("Repositorio de AccountPayableRepository vacio");
            }
            if (SupplierRepository == null)
            {
                throw new ArgumentNullException("Repositorio de SupplierRepository vacio");
            }
            if (SuppliersDistributionLinesRepository == null)
            {
                throw new ArgumentNullException("Repositorio de SuppliersDistributionLinesRepository vacio");
            }
            if (SettingInventoryRepository == null)
            {
                throw new ArgumentNullException("Repositorio de SettingInventoryRepository vacio");
            }
            if (InventoryProductRepository == null)
            {
                throw new ArgumentNullException("Repositorio de InventoryProductRepository vacio");
            }
            if (OtherWithholdingDeductionRepository == null)
            {
                throw new ArgumentNullException("Repositorio de OtherWithholdingDeductionRepository vacio");
            }
            if (AccountPayableAdminService == null)
            {
                throw new ArgumentNullException("Repositorio de AccountPayableAdminService vacio");
            }
            if (SequensePaymentsCRepository == null)
            {
                throw new ArgumentNullException("Repositorio de SequensePaymentsCRepository vacio");
            }
            if (WarehouseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de WarehouseRepository vacio");
            }
            if (InventoryControlDocumentRepository == null)
            {
                throw new ArgumentNullException("InventoryControlDocumentRepository");
            }
            if (paymentConceptsRepository == null)
            {
                throw new ArgumentNullException("paymentConceptsRepository");
            }
            if (physicalInventoryRepository == null)
            {
                throw new ArgumentNullException("physicalInventoryRepository");
            }
            if (productGroupsRepository == null)
            {
                throw new ArgumentNullException("productGroupsRepository");
            }
            if (consignmentInventoryRemissionDetailControlRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailControlRepository");
            }
            if (consignmentInventoryRemissionDetailBatchSerialRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailBatchSerialRepository");
            }
            if (functionalUnitRepository == null)
            {
                throw new ArgumentNullException("functionalUnitRepository");
            }

            _EntranceVoucherRepository = EntranceVoucherRepository;
            _sequenseRepository = sequenseRepository;
            _physicalInventoryAdminService = PhysicalInventoryAdminService;
            _purchaseOrderDetailRepository = PurchaseOrderDetailRepository;
            _inventoryContractDetailRepository = InventoryContractDetailRepository;
            _remissionEntranceDetailBatchRepository = RemissionEntranceDetailBatchSerialRepository;
            _accountPayableRepository = AccountPayableRepository;
            _supplierRepository = SupplierRepository;
            _consignmentCostListRepository = consignmentCostListRepository;
            _suppliersDistributionLinesRepository = SuppliersDistributionLinesRepository;
            _settingInventoryRepository = SettingInventoryRepository;
            _inventoryProductRepository = InventoryProductRepository;
            _otherWithholdingDeductionRepository = OtherWithholdingDeductionRepository;
            _accountPayableAdminService = AccountPayableAdminService;
            _sequensePaymentsCRepository = SequensePaymentsCRepository;
            _warehouseRepository = WarehouseRepository;
            _InventoryControlDocumentRepository = InventoryControlDocumentRepository;
            _paymentConceptsRepository = paymentConceptsRepository;
            _physicalInventoryRepository = physicalInventoryRepository;
            _productGroupsRepository = productGroupsRepository;
            _consignmentInventoryRemissionDetailControlRepository = consignmentInventoryRemissionDetailControlRepository;
            _consignmentInventoryRemissionDetailBatchSerialRepository = consignmentInventoryRemissionDetailBatchSerialRepository;
            _functionalUnitRepository = functionalUnitRepository;
            _generalLedgerIVARepository = GeneralLedgerIVARepository;
            _companySettingsRepository = CompanySettingsRepository;
            _currencyAdminService = CurrencyAdminService;
            _pucAdminService = PUCAdminService;
        }
        #endregion

        #region Methods
        public ActionResult<Domain.Entities.EntranceVoucher> SaveEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, AuditMessage audit, long idSecuence = 0, InventorySequence sequenceC = null)
        {
            if (EntranceVoucher == null)
            {
                throw new ArgumentNullException("EntranceVoucher");
            }
            IUnitWork unitOfWork = _EntranceVoucherRepository.UnitWork;
            IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;
            IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    bool validateStatus = false;
                    if(EntranceVoucher.Id > 0)
                    {
                        // Se verifica que el comprobante de entrada no haya sido confirmado previamente.
                        validateStatus = _EntranceVoucherRepository.Any(x => x.Id == EntranceVoucher.Id && x.Status == 2);
                    }

                    if (validateStatus)
                    {
                        unitOfWork.RollbackChanges();
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, Message =  "El comprobante de entrada ya ha sido confirmado previamente" };
                    }

                    if (EntranceVoucher.Code == null || EntranceVoucher.Code.Trim().Equals(string.Empty))
                    {
                        InventorySequenceDetail seq = (idSecuence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence)));
                        if (seq != null)
                        {
                            if (seq.Id == 0)
                            {
                                seq.IdSequense = sequenceC.IdSequence.Value;
                                seq.InventorySequenceId = sequenceC.Id;
                                seq.Next = 1;
                                seq.Prefix = EntranceVoucher.Prefix;
                            }
                            var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(EntranceVoucher.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                EntranceVoucher.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                transaction.Dispose();
                                return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                            }
                        }
                        else
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }

                    Domain.Entities.EntranceVoucher auxEntranceVoucher = null;
                    IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (EntranceVoucher.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        EntranceVoucher.CreationUser = audit.CodeUser;
                        EntranceVoucher.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                        InventoryControlDocument inventoryControlDocument = new InventoryControlDocument();
                        inventoryControlDocument.DocumentNumber = EntranceVoucher.Code;
                        inventoryControlDocument.DocumentType = (int)eTypeDocumentsControlInventory.VoucherofEntry;
                        inventoryControlDocument.DocumentUser = audit.CodeUser;
                        inventoryControlDocument.DocumentDate = EntranceVoucher.CreationDate;
                        _InventoryControlDocumentRepository.SaveEntity(inventoryControlDocument);
                    }
                    else
                    {
                        if (EntranceVoucher.Status == 3)
                        {
                            auxEntranceVoucher = EntranceVoucher.OriginalValue;
                            EntranceVoucher.AnnulmentUser = audit.CodeUser;
                            EntranceVoucher.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                            var documentControl = _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber(EntranceVoucher.Code, (int)eTypeDocumentsControlInventory.VoucherofEntry);
                            if (documentControl != null && documentControl.Id > 0)
                            {
                                documentControl.MarkAsDeleted();
                                _InventoryControlDocumentRepository.DeleteEntity(documentControl);
                                _InventoryControlDocumentRepository.UnitWork.Commit();
                            }

                        }
                        else
                        {
                            auxEntranceVoucher = EntranceVoucher.OriginalValue;
                            EntranceVoucher.ModificationUser = audit.CodeUser;
                            EntranceVoucher.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                        }

                    }
                    foreach (Domain.Entities.EntranceVoucherDetail detail in EntranceVoucher.EntranceVoucherDetail)
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

                        if(EntranceVoucher.RetentionSource == 0)
                        {
                            detail.RTFPercentage = 0;
                            detail.RTFValue = 0;
                        }
                    }
                    _EntranceVoucherRepository.SaveEntity(EntranceVoucher);
                    unitOfWork.Commit();
                    unitOfWorkSequense.Commit();
                    unitOfWorkControlDocuments.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher>(EntranceVoucher, audit, status, auxEntranceVoucher);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, ObjectEmbbeded = EntranceVoucher };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");

                    string message = IndigoManagementExceptions.GetExceptionDetails(ex);

                    if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        message = ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.InnerException.Message))
                        {
                            message = ex.InnerException.InnerException.Message;
                        }
                    }

                    return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, Message = message };
                }
            }
        }

        public ActionResult DeleteEntranceVoucher(Domain.Entities.EntranceVoucher EntranceVoucher, AuditMessage audit)
        {
            if (EntranceVoucher == null)
            {
                throw new ArgumentNullException("product");
            }

            IUnitWork unitOfWork = _EntranceVoucherRepository.UnitWork;
            try
            {
                EntranceVoucher.MarkAsDeleted();
                IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher>(EntranceVoucher, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _EntranceVoucherRepository.DeleteEntity(EntranceVoucher);
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

        public ActionResult<Domain.Entities.EntranceVoucher> ChangeStateEntranceVoucher(string code, byte state, AuditMessage audit)
        {
            Domain.Entities.EntranceVoucher EntranceVoucher = _EntranceVoucherRepository.GetEntranceVoucher(code);
            EntranceVoucher.Status = state;
            return SaveEntranceVoucher(EntranceVoucher, audit);
        }

        public T ConvertirDato<T>(object valor) 
        {
            if (valor == DBNull.Value)
                return default(T);
            else
                return (T)valor;
        }

        public ActionResult<Domain.Entities.EntranceVoucher> GetEntranceVoucher(string code, AuditMessage audit)
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
                Domain.Entities.EntranceVoucher ev = _EntranceVoucherRepository.GetEntranceVoucher(code);
                if (ev != null && ev.Id > 0)
                {
                    DataSet ds = SP_GetEntranceVoucher(string.Format("@EntranceVoucherCode = '{0}', @EntranceVoucherId = 0, @Operacion = 0", code));//, session);
                    DataRow dr = null;
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        dr = ds.Tables[0].Rows[0];
                        ev.DescriptionSupplier = dr["DescriptionSupplier"].ToString();
                        ev.DescriptionWarehouse = dr["DescriptionWarehouse"].ToString();
                        ev.DescriptionSupplierType = dr["DescriptionSupplierType"].ToString();
                        ev.DescriptionDocumentSupport = dr["DescriptionDocumentSupport"].ToString();

                        ds = SP_GetEntranceVoucher(string.Format("@EntranceVoucherCode = '', @EntranceVoucherId = '{0}', @Operacion = 1", ev.Id));//, session);
                        var dsBatch = SP_GetEntranceVoucher(string.Format("@EntranceVoucherCode = '', @EntranceVoucherId = '{0}', @Operacion = 2", ev.Id));//, session);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            EntranceVoucherDetail evd = null;
                            EntranceVoucherDetailBatchSerial evdbs = null;
                            foreach (DataRow l in ds.Tables[0].Rows)
                            {
                                evd = new EntranceVoucherDetail();
                                evd.StartTracking();
                                evd.ChangeTracker.State = ObjectState.Unchanged;
                                evd.StopTracking();
                                ev.EntranceVoucherDetail.Add(evd);
                                
                                evd.Id = Convert.ToInt32(l["Id"]);
                                evd.EntranceVoucherId = Convert.ToInt32(l["EntranceVoucherId"]);
                                evd.EntranceSource = Convert.ToByte(l["EntranceSource"]);
                                evd.SourceCode = Convert.ToString(l["SourceCode"]);
                                evd.PurchaseOrderDetailId = ConvertirDato<Int32?>(l["PurchaseOrderDetailId"]); // (Int32?)(l["PurchaseOrderDetailId"]);
                                evd.DeliveredDate = ConvertirDato<DateTime?>(l["DeliveredDate"]); // (DateTime?)l["DeliveredDate"];
                                evd.ContractDetailId = ConvertirDato<Int32?>(l["ContractDetailId"]);
                                evd.RemissionEntranceDetailBatchSerialId = ConvertirDato<Int32?>(l["RemissionEntranceDetailBatchSerialId"]);// (Int32?)(l["RemissionEntranceDetailBatchSerialId"]);
                                evd.ConsignmentInventoryRemissionDetailBatchSerialId = ConvertirDato<Int32?>(l["ConsignmentInventoryRemissionDetailBatchSerialId"]);// (Int32?)(l["ConsignmentInventoryRemissionDetailBatchSerialId"]);
                                evd.GroupCodeName = l["GroupCodeName"].ToString();                                
                                evd.ProductId = (Int32)(l["ProductId"]);
                                evd.ProductCode = l["ProductCode"].ToString();
                                evd.ProductName = l["ProductName"].ToString();
                                evd.ProductCodeName = String.Format("{0} - {1}", l["ProductCode"], l["ProductName"]);
                                evd.ManufacturerName = l["ManufacturerName"].ToString();
                                evd.HealthRegistration = l["HealthRegistration"].ToString();
                                evd.Presentation = l["Presentation"].ToString();
                                evd.HandlesBatch = Convert.ToBoolean(l["HandlesBatch"]);
                                evd.Quantity = Convert.ToInt32(l["Quantity"]);
                                evd.UnitValue = Convert.ToDecimal(l["UnitValue"]);
                                evd.LastValue = Convert.ToDecimal(l["LastValue"]);
                                evd.SubTotalValue = Convert.ToDecimal(l["SubTotalValue"]);
                                evd.IvaPercentage = Convert.ToDecimal(l["IvaPercentage"]);
                                evd.IvaValue = Convert.ToDecimal(l["IvaValue"]);
                                evd.DiscountPercentage = Convert.ToDecimal(l["DiscountPercentage"]);
                                evd.DiscountValue = Convert.ToDecimal(l["DiscountValue"]);
                                evd.TotalValue = Convert.ToDecimal(l["TotalValue"]);
                                evd.NetoValue = Convert.ToDecimal(l["NetoValue"]);
                                evd.RTFPercentage = Convert.ToDecimal(l["RTFPercentage"]);
                                evd.RTFValue = Convert.ToDecimal(l["RTFValue"]);
                                evd.MinBase = Convert.ToDecimal(l["MinBase"]);
                                evd.Rate = Convert.ToDecimal(l["Rate"]);
                                evd.TypeRounding = Convert.ToByte(l["TypeRounding"]);                                
                                evd.CostWithDescount = evd.UnitValue - (evd.UnitValue * (evd.DiscountPercentage/100));
                                if (dsBatch != null && dsBatch.Tables.Count > 0)
                                {
                                    foreach (DataRow b in dsBatch.Tables[0].Select(string.Format("EntranceVoucherDetailId = {0}",evd.Id)))
                                    {
                                        evdbs = new EntranceVoucherDetailBatchSerial();                                        
                                        evdbs.StartTracking();
                                        evdbs.ChangeTracker.State = ObjectState.Unchanged;
                                        evdbs.StopTracking();
                                        evd.EntranceVoucherDetailBatchSerial.Add(evdbs);
                                        evdbs.Id = (Int32)(b["Id"]);
	                                    evdbs.EntranceVoucherDetailId = (Int32)(b["EntranceVoucherDetailId"]);
                                        evdbs.BatchSerialId = ConvertirDato<Int32?>(b["BatchSerialId"]);// (Int32?)(b["BatchSerialId"]);
                                        evdbs.Quantity = (Int32)(b["Quantity"]);
                                        evdbs.OutstandingQuantity = (Int32)(b["OutstandingQuantity"]);
                                        evdbs.CodeBatchSerial = b["CodeBatchSerial"].ToString();
                                    }
                                }

                                
                                
                            }
                        }
                    }
                    EntranceVoucherCommitment evc = null;
                    
                    ds = SP_GetEntranceVoucher(string.Format("@EntranceVoucherCode = '', @EntranceVoucherId = '{0}', @Operacion = 3", ev.Id));//, session);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow l in ds.Tables[0].Rows)
                        {
                            evc = new EntranceVoucherCommitment();
                            evc.StartTracking();
                            evc.ChangeTracker.State = ObjectState.Unchanged;
                            evc.StopTracking();
                            ev.EntranceVoucherCommitment.Add(evc);
                            evc.Id = (Int32)l["Id"]; 
	                        evc.EntranceVoucherId = (Int32)l["EntranceVoucherId"]; 
	                        evc.CommitmentDetailId = (Int32)l["CommitmentDetailId"];
                            evc.Value = Convert.ToDecimal(l["Value"]);
                            evc.CommitmentCode = l["CommitmentCode"].ToString();
                            evc.CommitmentDocument = l["CommitmentDocument"].ToString();
                            evc.CategoryCodeName = l["CategoryCodeName"].ToString();
                            evc.FinancialSourceCodeName = l["FinancialSourceCodeName"].ToString();
                            evc.RevenueTypeCodeName = l["RevenueTypeCodeName"].ToString();
                            evc.Balance = Convert.ToDecimal(l["Balance"]);
                        }
                    }

                    EntranceVoucherOtherDeduction evod = null;
                    ds = SP_GetEntranceVoucher(string.Format("@EntranceVoucherCode = '', @EntranceVoucherId = '{0}', @Operacion = 4", ev.Id));//, session);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow l in ds.Tables[0].Rows)
                        {
                            evod = new EntranceVoucherOtherDeduction();
                            evod.StartTracking();
                            evod.ChangeTracker.State = ObjectState.Unchanged;
                            evod.StopTracking();
                            ev.EntranceVoucherOtherDeduction.Add(evod);
                            evod.Id = (Int32)l["Id"];
                            evod.EntranceVoucherId = (Int32)l["EntranceVoucherId"];
                            evod.OtherWithholdingDeductionId = (Int32)l["OtherWithholdingDeductionId"];
                            evod.Type = Convert.ToByte(l["Type"]);
                            evod.Value = Convert.ToDecimal(l["Value"]);
                            evod.ValueOutstanding = Convert.ToDecimal(l["ValueOutstanding"]);

                        }
                    }

                    IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher>(ev, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, ObjectEmbbeded = ev };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        //public ActionResult<Domain.Entities.EntranceVoucher> GetEntranceVoucher(string code, AuditMessage audit)
        //{
        //    if (code == string.Empty)
        //    {
        //        throw new ArgumentNullException("Code");
        //    }
        //    if (audit == null)
        //    {
        //        throw new ArgumentNullException("audit");
        //    }
        //    try
        //    {
        //        Domain.Entities.EntranceVoucher EntranceVoucher = _EntranceVoucherRepository.GetEntranceVoucher(code);
        //        if (EntranceVoucher != null && EntranceVoucher.Id > 0)
        //        {
        //            IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher> auditProcess;
        //            auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher>(EntranceVoucher, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
        //            auditProcess.Execute();
        //        }
        //        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, ObjectEmbbeded = EntranceVoucher };
        //    }
        //    catch (Exception ex)
        //    {
        //        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
        //        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, MessageResult = { ex.Message } };
        //    }
        //}

        public Domain.Entities.EntranceVoucher GetEntranceVoucherById(int idEntranceVoucher)
        {
            if (idEntranceVoucher == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _EntranceVoucherRepository.GetEntranceVoucherById(idEntranceVoucher);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        ///Obtiene un EntranceVoucherDetailBatchSerial por IdEntrancevoucher
        ///</summary>
        ///<param name="EntranceVoucherId"></param>
        ///<returns></returns>
        ///<remarks></remarks>
        public List<Domain.Entities.EntranceVoucherDetailBatchSerial> GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(int EntranceVoucherId)
        {
            if (EntranceVoucherId == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _EntranceVoucherRepository.GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(EntranceVoucherId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        public async Task<ActionResult<Domain.Entities.EntranceVoucher>> ConfirmEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher, AuditMessage audit, string ContainerNameCrystal, Boolean controlCost = false)
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled))
            {
                IUnitWork entranceVoucherUnitWork = _EntranceVoucherRepository.UnitWork;
                IUnitWork unitOfWorkControlDocuments = _InventoryControlDocumentRepository.UnitWork;
                IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher> auditProcess;
                try
                {                 
                    //Se valida que el comprobante de entrada tenga al menos un detalle
                    if (entranceVoucher.EntranceVoucherDetail == null || !entranceVoucher.EntranceVoucherDetail.Any())
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "El comprobante de entrada no tiene ningún detalle." } };
                    }

                    //Obtengo el almacén del comprobante de emtrada para determinar si es un almacén en consignación
                    var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(entranceVoucher.WarehouseId);

                    //Si el almacen es de consignación la única fuente de productos debe ser remisiones de inventario en consignación
                    if (warehouse.WarehouseConsignment == true && entranceVoucher.EntranceVoucherDetail.Where(e => e.EntranceSource != 5).Any())
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "Solo se pueden agregar productos a un almacén de consignación a través de remisiones de inventario en consignación." } };
                    }

                    /////-----------------------Valido cantidades a legalizar de la remisión de inventario en consignación-----------------------/////
                    ConsignmentRelationControl batchAndControl = null;
                    List<String> errors = new List<string>();
                    if (entranceVoucher.EntranceVoucherDetail.Where(e => e.EntranceSource == 5).Any())
                    {
                        List<EntranceVoucherDetail> listEntrancerDetail = entranceVoucher.EntranceVoucherDetail.Where(e => e.EntranceSource == 5).ToList();

                        List<int> IdsBatch = listEntrancerDetail.Select(x => x.ConsignmentInventoryRemissionDetailBatchSerialId.Value).ToList();

                        batchAndControl = _consignmentInventoryRemissionDetailControlRepository.GetConsignmentControlByListBatch(IdsBatch);

                        foreach (Domain.Entities.EntranceVoucherDetail detail in listEntrancerDetail)
                        {
                            if (detail.ConsignmentInventoryRemissionDetailBatchSerialId != null)
                            {
                                var consignmentInventoryRemissionDetailControls = batchAndControl?.ConsignmentInventoryRemissionDetailControl.FindAll(f => f.ConsignmentInventoryRemissionDetailBatchSerialId == detail.ConsignmentInventoryRemissionDetailBatchSerialId);

                                if (consignmentInventoryRemissionDetailControls != null && consignmentInventoryRemissionDetailControls.Any())
                                {
                                    int quantityPendingLegalization = consignmentInventoryRemissionDetailControls.Sum(cirdc => cirdc.QuantityPendingLegalization);
                                    if (detail.Quantity > quantityPendingLegalization)
                                    {
                                        errors.Add("El item '" + detail.ProductCodeName.Trim() + "' importado de la remisión de inventario en consignación (" + detail.SourceCode + ") no tienen la cantidad suficiente por legalizar (" + quantityPendingLegalization + ").");
                                    }
                                }
                            }
                            else
                            {
                                errors.Add("El item '" + detail.ProductCodeName.Trim() + "' importado de la remisión de inventario en consignación no posee una identificación válida.");
                            }
                        }
                    }

                    //Se valida que si hay compromisos agregados la sumatoria del valor de los productos que en el grupo tenga asociado un presupuesto sea igual a la sumatoria de los compromisos
                    if(entranceVoucher.EntranceVoucherCommitment != null && entranceVoucher.EntranceVoucherCommitment.Count() > 0 
                        && entranceVoucher.EntranceVoucherDetail != null && entranceVoucher.EntranceVoucherDetail.Count() > 0)
                    {
                        var commitmentsWithInsufficientBalance = entranceVoucher.EntranceVoucherCommitment.Where(c => c.Balance < c.Value);
                        foreach (EntranceVoucherCommitment commitment in commitmentsWithInsufficientBalance)
                        {
                            errors.Add("El compromiso con codigo " + commitment.CommitmentCode.Trim() + " no tiene saldo suficiente");
                        }
                        if (!commitmentsWithInsufficientBalance.Any() && _EntranceVoucherRepository.ValidateProductsAndCommitments(entranceVoucher.FreightValue, entranceVoucher.FreightIVAValue, entranceVoucher.EntranceVoucherDetail.ToList(), entranceVoucher.EntranceVoucherCommitment.ToList()) == false)
                        {
                            errors.Add("La sumatoria del valor de los productos que en su grupo tiene asociado un presupuesto no es igual a la sumatoria del valor de los compromisos");
                        }
                    }

                    if (errors.Count > 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = errors };
                    }

                    //////-------------------------Genero la Cuenta por pagar-------------------------////////                    
                    ActionResult<Domain.Entities.AccountPayable> resultAcp = await CreateAccountPayableEntranceVoucherAsync(entranceVoucher, batchAndControl);
                    if (!resultAcp.StateResult)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = resultAcp.MessageResult };
                    }
                    AccountPayable accountPayable = resultAcp.ObjectEmbbeded;
                    if (accountPayable == null)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "No se puede generar la cuenta por pagar." } };
                    }
                    // consulto la secuencia numerica
                    PaymentsSecuence sequensePayments = _sequensePaymentsCRepository.GetSequenseByIdForm("730");
                    if (sequensePayments.Id == 0)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "No se puede generar la cuenta por pagar porque no existe secuencia numérica para generar el consecutivo" } };
                    }

                    //Id de la secuencia numerica que se envia a los metodos de cxp
                    int PaymentSequenseDetailId = 0;

                    //Se valida que la secuencia numerica no sea manual
                    if (sequensePayments.IsManual)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "La secuencia numérica de CxP está parametrizada como manual y debe ser automática" } };
                    }

                    //Se obtiene el id de la secuencia numerica
                    if (sequensePayments.Scope == "O") //Si la secuencia es organizacional
                    {
                        PaymentSequenseDetailId = (from x in sequensePayments.PaymentsSecuenceDetail select x.Id).FirstOrDefault();
                    }
                    else //Si la secuencia es po unidad operativa
                    {
                        //Se valida que haya registros de secuencia con la unidad operativa que viene desde presentacion
                        if ((from x in sequensePayments.PaymentsSecuenceDetail where x.IdOperatingUnit == entranceVoucher.OperatingUnitId select x).Count() == 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string> { "No existe secuencia numérica para el form CxP con la unidad operativa escogida" } };
                        }
                        PaymentSequenseDetailId = (from x in sequensePayments.PaymentsSecuenceDetail where x.IdOperatingUnit == entranceVoucher.OperatingUnitId select x.Id).FirstOrDefault();
                    }

                    // Agrego la cuenta por pagar a un listado
                    List<AccountPayable> ListAccountPayable = new List<AccountPayable>();
                    ListAccountPayable.Add(accountPayable);
                    // Se consulta el id de la secuencia para el form de cxp para poder sacar el consecutivo de la misma
                    ActionResult<List<string>> resultAccountPayable = _accountPayableAdminService.SaveListAccountPayable(ListAccountPayable, null, true, audit, PaymentSequenseDetailId);
                    if (resultAccountPayable.StateResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = resultAccountPayable.MessageResult };
                    }

                    entranceVoucher.AccountPayableId = ListAccountPayable[0].Id;
                    List<string> MessagesStock = default(List<string>);
                    MessagesStock = new List<string>();
                    //////---------Modifico las cantidades en el inventario de los documentos---------////////

                    SP_PhysicalInventory_Result resultSavePhysicalInventory = _physicalInventoryRepository.SavePhysicalInventory(entranceVoucher.Id, entranceVoucher.GetType().Name, entranceVoucher.CreationUser, ContainerNameCrystal, controlCost);
                    if (resultSavePhysicalInventory.StatusResult == false)
                    {
                        transaction.Dispose();
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string>() { resultSavePhysicalInventory.MessageResult } };
                    }

                    if (resultSavePhysicalInventory.MessageResult != null)
                    {
                        string[] ResultStock = resultSavePhysicalInventory.MessageResult.Split(';');
                        foreach (var itemStock in ResultStock)
                        {
                            MessagesStock.Add(itemStock);
                        }
                    }
                    
                    //eliminamos doc. de control de inventarios
                    InventoryControlDocument inventoryControlDocument = await _InventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumberAsync(entranceVoucher.Code, (int)eTypeDocumentsControlInventory.VoucherofEntry);
                    if (inventoryControlDocument != null && inventoryControlDocument.Id > 0)
                    {
                        _InventoryControlDocumentRepository.DeleteEntity(inventoryControlDocument);
                        unitOfWorkControlDocuments.Commit();
                    }


                    //Se genera el comprobante contable de la reclasificacion de la remission de entrada
                    if ((from x in entranceVoucher.EntranceVoucherDetail where x.EntranceSource == 4 select x.EntranceVoucherId).Count() > 0)
                    {
                        SP_GenerateJournalVoucherByReclassificationRemissionEntrance_Result resultJournalVoucher = _EntranceVoucherRepository.SP_GenerateJournalVoucherByReclassificationRemissionEntrance(entranceVoucher.Id, audit.CodeUser);
                        if (resultJournalVoucher.CodeMessage > 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, StateResultAux = false, MessageResult = new List<string>() { resultJournalVoucher.Message } };
                        }
                    }

                    entranceVoucher.Status = 2;
                    entranceVoucher.ConfirmationDate = DateTime.Now;
                    entranceVoucher.ConfirmationUser = audit.CodeUser;
                    entranceVoucher.ModificationDate = DateTime.Now;
                    entranceVoucher.ModificationUser = audit.CodeUser;
                    entranceVoucher.MarkAsModified();
                    _EntranceVoucherRepository.SaveEntity(entranceVoucher);

                    entranceVoucherUnitWork.Commit();
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.EntranceVoucher>(entranceVoucher, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, entranceVoucher.OriginalValue);
                    auditProcess.Execute();
                    transaction.Complete();
                    return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, StateResultAux = true, ObjectEmbbeded = entranceVoucher, MessageResult = resultAccountPayable.MessageResult, MessageResultAux = MessagesStock };
                }
                catch (OptimisticConcurrencyException ex)
                {
                    entranceVoucherUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    return new ActionResult<Domain.Entities.EntranceVoucher>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = new List<string>() { ResourceManager.get_GetString("ErrorConcurrence") }
                    };
                }
                catch (DbEntityValidationException ex)
                {
                    entranceVoucherUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.EntranceVoucher>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = new List<string>() { ResourceManager.get_GetString("ErrorUnknown") }
                    };
                }
                catch (Exception ex)
                {
                    entranceVoucherUnitWork.RollbackChangesUnitOfWork();
                    transaction.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");

                    string message = ResourceManager.get_GetString("ErrorUnknown");
                    if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        message = ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.InnerException.Message))
                        {
                            message = ex.InnerException.InnerException.Message;
                        }
                    }

                    return new ActionResult<Domain.Entities.EntranceVoucher>
                    {
                        StateResult = false,
                        StateResultAux = false,
                        MessageResult = new List<string>() { message }
                    };
                }

            }
        }

        public async Task<ActionResult<AccountPayable>> CreateAccountPayableEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher,
                                                                                 ConsignmentRelationControl batchAndControl = null)
        {

            List<string> errors = new List<string>();
            IUnitWork accountPayableUnitWork = _accountPayableRepository.UnitWork;
            Domain.Entities.SettingInventory settings = _settingInventoryRepository.GetSettingInventory(entranceVoucher.OperatingUnitId);
            if (settings is null || settings?.Id==0) { return new ActionResult<AccountPayable> { StateResult = false, MessageResult = new List<string> { "No existe parámetros de inventario para la Unidad Operativa seleccionada" } }; };
            Domain.Entities.Warehouse warehouse = await _warehouseRepository.GetWarehouseByIdAsync(entranceVoucher.WarehouseId);
            Domain.Entities.Supplier Supplier = _supplierRepository.GetSupplierById(entranceVoucher.SupplierId, false);
            
            // Diccionario para los productos de la lista de costos 
            Dictionary<int, Domain.Entities.ConsignmentCostListDetail> dictionaryConsignmentCostListDetail = new Dictionary<int, Domain.Entities.ConsignmentCostListDetail>();
            Domain.Entities.ConsignmentCostList ConsignmentCostListTemp = null;
            Decimal ValueEntranceVoucherConsigmentList = 0;
            if (Supplier.ConsignmentInventoryCosting == 1)
            {
                ConsignmentCostListTemp = _consignmentCostListRepository.GetConsignmentCostListBySupplierId(Supplier.Id, settings.OperatingUnitId);

                if (ConsignmentCostListTemp == null)
                {
                    return new ActionResult<AccountPayable> { StateResult = false, MessageResult = new List<string> { "El proveedor no tiene ningún listado de costos activo" } };
                }

                foreach (var item in entranceVoucher.EntranceVoucherDetail)
                {
                    if (!dictionaryConsignmentCostListDetail.ContainsKey(item.ProductId))
                    {
                        if (!ConsignmentCostListTemp.ConsignmentCostListDetail.Any(x => x.ProductId == item.ProductId))
                        {
                            return new ActionResult<AccountPayable> { StateResult = false, Message = $"El producto {item.ProductCodeName} no existe en el listado de costos" };
                        }

                        ConsignmentCostListDetail consignmentCostListDetailTemp =
                        ConsignmentCostListTemp.ConsignmentCostListDetail.FirstOrDefault(x => x.ProductId == item.ProductId);

                        dictionaryConsignmentCostListDetail.Add(item.ProductId, consignmentCostListDetailTemp);
                        ValueEntranceVoucherConsigmentList += consignmentCostListDetailTemp.CostNew.Value;
                        item.SubTotalValue = consignmentCostListDetailTemp.CostNew.Value;
                    }
                    else
                    {
                        item.SubTotalValue = dictionaryConsignmentCostListDetail[item.ProductId].CostNew.Value;
                        ValueEntranceVoucherConsigmentList += dictionaryConsignmentCostListDetail[item.ProductId].CostNew.Value;
                    }
                }
            }
            
            // Calcular el valor total de la factura, usando el valor de consignación si aplica
            decimal totalInvoiceValue = Supplier.ConsignmentInventoryCosting == 1 ? ValueEntranceVoucherConsigmentList : entranceVoucher.Value;

            AccountPayable accountPayable = new AccountPayable();
            accountPayable.EntityId = entranceVoucher.Id;
            accountPayable.EntityCode = entranceVoucher.Code;
            accountPayable.EntityName = entranceVoucher.GetType().Name;
            accountPayable.IdSupplier = entranceVoucher.SupplierId;
            accountPayable.IdThirdParty = Supplier.IdThirdParty;
            accountPayable.IdAccount = _suppliersDistributionLinesRepository.GetSuppliersDistributionLinesById(entranceVoucher.SupplierDistributionLineId).DistributionLines.MainAccounts.Id;
            accountPayable.IdCostCenter = null;
            accountPayable.BillNumber = entranceVoucher.InvoiceNumber;
            accountPayable.BillDate = entranceVoucher.InvoiceDate;
            accountPayable.DocumentDate = entranceVoucher.DocumentDate;
            accountPayable.ServicePeriodDate = entranceVoucher.DocumentDate;
            accountPayable.FilingUnitId = settings.FilingUnitId;
            accountPayable.Term = entranceVoucher.DayPeriod;
            accountPayable.ExpirationDate = PaymentServices.AddDaysDate(entranceVoucher.DayPeriod, entranceVoucher.InvoiceDate);
            accountPayable.Coments = "Cuenta por pagar generada por el comprobante de entrada " + entranceVoucher.Code + " Con el numero de Factura " + entranceVoucher.InvoiceNumber;
            accountPayable.Status = 1;
            accountPayable.InitialBalance = false;
            accountPayable.PreviousBudget = false;
            accountPayable.Shares = 1;
            accountPayable.InvoiceValue = totalInvoiceValue - entranceVoucher.ValueDiscount + entranceVoucher.ValueTax;
            accountPayable.IdOperatingUnit = entranceVoucher.OperatingUnitId;
            accountPayable.IdSuppliersDistributionLines = entranceVoucher.SupplierDistributionLineId;
            accountPayable.SupplierTypeId = entranceVoucher.SupplierTypeId;
            accountPayable.JournalVoucherId = settings.PurchaseJournalVoucherTypeId;
            accountPayable.CommitmentDetailId = entranceVoucher.CommitmentDetailId;
            accountPayable.HandlesDocumentSupport = entranceVoucher.DocumentSupportId == null ? false : true;
            accountPayable.DocumentSupportId = entranceVoucher.DocumentSupportId;
            accountPayable.CurrencyId = entranceVoucher.CurrencyId;
            accountPayable.IdEconomicActivity = entranceVoucher.EconomicActivityId;

            //---------------------------IVA COSTO O IVA DESCONTABLE----------------------------------------------------------------------------------------------------------
            // TaxRegistration--: 1-IVA al costo, 2- IVA Descontable, 3-mixto pero esto es invalido porque significa que No lo escogio el usuario o no llego el dato a servicios
            byte? _taxRegistration = (entranceVoucher?.TaxRegistration is null || entranceVoucher?.TaxRegistration == 0) ? settings?.TaxRegistration : entranceVoucher?.TaxRegistration;
            switch (_taxRegistration)
            {
                case 1:
                    accountPayable.DeductibleIva = false;
                    accountPayable.TaxRegistration = 4;
                    break;
                case 2:
                    accountPayable.DeductibleIva = true;
                    accountPayable.TaxRegistration = 2;
                    break;
                case 3:
                    accountPayable.DeductibleIva = null;
                    accountPayable.TaxRegistration = null;
                    break;
                default:
                    accountPayable.DeductibleIva = null;
                    accountPayable.TaxRegistration = null;
                    break;
            }

            // Moneda
            int? officialCurrencyId = _companySettingsRepository.FirstOrDefault(x => true, false).OfficialCurrencyId;
            if (officialCurrencyId is null) { return new ActionResult<AccountPayable> { StateResult = false, Message = "No se logró establecer la moneda Oficial" }; }
            decimal tRM = 1;
            if (officialCurrencyId != entranceVoucher.CurrencyId)
            {
                SessionValues _sessionValues = SessionValues.Instance;
                _sessionValues.OfficialCurrencyId = officialCurrencyId.Value;
                ActionResult<TRM> _tRM = _currencyAdminService.GetTRMbyCurrencyId(entranceVoucher.CurrencyId, officialCurrencyId.Value, _sessionValues, accountPayable.DocumentDate);

                if (_tRM is null || !_tRM.StateResult)
                {
                    return new ActionResult<AccountPayable> { StateResult = false, Message = $"Error Obteniendo el TRM de la moneda Oficial a la del comprobante de entrada" };
                }

                tRM = _tRM.ObjectEmbbeded.Value;
            }

            List<Domain.Payroll.Entities.FunctionalUnit> functionalListConsigment = new List<Domain.Payroll.Entities.FunctionalUnit>();
            List<Domain.Entities.InventoryProduct> inventoryProducts = new List<Domain.Entities.InventoryProduct>();
            // diccionario para el subgrupo
            Dictionary<int, Domain.Entities.ProductGroup> dictionaryProductGroup = new Dictionary<int, Domain.Entities.ProductGroup>();

            // diccionario para los parametros por unidad opertiva
            Dictionary<int, Domain.Entities.SettingInventory> dictionarySettings = new Dictionary<int, Domain.Entities.SettingInventory>();
            dictionarySettings.Add(settings.OperatingUnitId, settings);

            List<int> ProductIds = entranceVoucher.EntranceVoucherDetail.Select(x => x.ProductId).ToList();
            inventoryProducts = _inventoryProductRepository.GetByFilter(f => ProductIds.Contains(f.Id), false, new List<string> { "ProductGroup" }).ToList();

            bool isConsigna = entranceVoucher?.EntranceVoucherDetail.Any(f => f.EntranceSource == 5) ?? false;

            if (isConsigna)
            {
                var IdsfunctionalListConsigment = batchAndControl.ConsignmentInventoryRemissionDetailControl.FindAll(t => t.FunctionalUnitId != null).Select(g => g.FunctionalUnitId.Value).ToList();
                functionalListConsigment = _functionalUnitRepository.GetByFilter(t => IdsfunctionalListConsigment.Contains(t.Id), false).ToList();
            }

            /// Detalle de la cuenta por pagar
            AccountPayableDetailConcept accountPayableDetail;
			foreach (var item in entranceVoucher.EntranceVoucherDetail)
			{
                Domain.Entities.InventoryProduct product = inventoryProducts.Find(f => f.Id == item.ProductId);

                if (!dictionaryProductGroup.ContainsKey(product.ProductGroup.Id))
                {
                    //Se obtiene el valor de si el proveedor es declarante o no para poder sacar el id del concepto de pagos del grupo para la retención
                    bool isDeclarant = _supplierRepository.GetSupplierById(entranceVoucher.SupplierId, false).Declarant;

                    Domain.Entities.ProductGroup prodguctGroupTemp = _productGroupsRepository.GetProductGroupByIdForAccounting(product.ProductGroup.Id, isDeclarant);
                    dictionaryProductGroup.Add(product.ProductGroup.Id, prodguctGroupTemp);
                }

                Domain.Entities.ProductGroup productGroup = dictionaryProductGroup[product.ProductGroup.Id];

                int? _costCenterId = GetEntranceVoucherCostCenterByAccountId(settings.AssociateCostCenter, warehouse, product.ProductGroup);
                               
                if (item.SubTotalValue > 0)
                {
                    if (item.EntranceSource != 5)
                    {
                        // Concepto de pago para agregar el avlor de los productos menos el descuento
                        accountPayableDetail = new AccountPayableDetailConcept();
                        accountPayableDetail.IdConceptAccountPayable = Convert.ToInt32(productGroup.ConceptAccountPayableInventory);
                        accountPayableDetail.IdAccount = Convert.ToInt32(productGroup.AccountInventoryId);
                        accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                        accountPayableDetail.IdCostCenter = productGroup.HandlesCostCenterWithholdigSourceAccount ? _costCenterId:null ;
                        accountPayableDetail.Nature = 1; //Debito
                        accountPayableDetail.BaseValue = ( item.IvaPercentage > 0)? 0 :(item.SubTotalValue - item.DiscountValue);
                        accountPayableDetail.BillingValue = totalInvoiceValue - entranceVoucher.ValueDiscount + entranceVoucher.ValueTax;
                        accountPayableDetail.Value = item.SubTotalValue - item.DiscountValue;
                        accountPayableDetail.TotalConcept = item.SubTotalValue - item.DiscountValue;
                        accountPayableDetail.IdRetentionConcept = null;
                        accountPayableDetail.Percentage = null;
                        accountPayableDetail.DeferredCausation = false;
                        accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'Valor Bruto Producto'";
                        accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
                    }
                    else if (item.EntranceSource == 5)
                    {


                        var result = this.RemissionConsignmentControlCxPDetail(item,
                                                                                ref batchAndControl,
                                                                                ref accountPayable,
                                                                                tRM, warehouse,
                                                                                settings,
                                                                                product,
                                                                                productGroup,
                                                                                ref dictionarySettings,
                                                                                functionalListConsigment,
                                                                                _costCenterId,
                                                                                Supplier.ConsignmentInventoryCosting,
                                                                                _taxRegistration);

                        if (result is null || !result.StateResult)
                        {
                            errors.Add(result?.Message ?? "Error Generando Legalización");
                            break;
                        }
                    }
                }

                if (! new List<byte?> {1,2 }.Contains(_taxRegistration)) { return new ActionResult<AccountPayable> { StateResult = false, MessageResult = new List<string> { "El Registro del IVA es Mixto y No se seleccionó desde el formulario ninguno en especifico" } }; }

                /// Concepto de IVA de los productos
                if (item.IvaValue > 0)
                {
                    if (product?.IVAId is null) { return new ActionResult<AccountPayable> { StateResult = false, MessageResult = new List<string> { "El producto no tiene asignado un IVA asociado" } }; };
                    accountPayableDetail = new AccountPayableDetailConcept();
                    accountPayableDetail.IdConceptAccountPayable = settings.IVAAccountPayableConceptId;

                    switch (_taxRegistration)
                    {
                        case 1:
                            int? AccountId;
                            if (item.EntranceSource == 5)
                            {
                                AccountId = productGroup.CounterpartCostConsignedInventoryId;
                            }
                            else
                            {
                                AccountId = productGroup.AccountInventoryId;
                            }
                            accountPayableDetail.IdAccount = Convert.ToInt32(AccountId);
                            break;
                        case 2:
                            int? _idAccount = _generalLedgerIVARepository.FirstOrDefault(x => x.Id == product.IVAId)?.IdAccountPurchaseService;
                            if (_idAccount is null)
                            {
                                return new ActionResult<AccountPayable> { StateResult = false, MessageResult = new List<string> { "No esta parametrizada la cuenta IVA Compra/Servicios" } };
                            }
                            accountPayableDetail.IdAccount = _idAccount.Value;
                            break;
                    }

                    accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                    accountPayableDetail.IdCostCenter = _pucAdminService.MainAccountHandlesCostCenter(accountPayableDetail.IdAccount) ? _costCenterId : (int?)null;
                    accountPayableDetail.Nature = 1; //Debito
                    accountPayableDetail.BaseValue = item.SubTotalValue - item.DiscountValue;
                    accountPayableDetail.BillingValue = totalInvoiceValue - entranceVoucher.ValueDiscount + entranceVoucher.ValueTax;
                    accountPayableDetail.Value = item.IvaValue;
                    accountPayableDetail.TotalConcept = item.IvaValue;
                    accountPayableDetail.IvaValue = item.IvaValue;
                    accountPayableDetail.RateIva = product?.IVAId;
                    accountPayableDetail.IdRetentionConcept = null;
                    accountPayableDetail.Percentage = item.IvaPercentage;
                    accountPayableDetail.DeferredCausation = false;
                    accountPayableDetail.Detail = $"IVA Tarifa {item.IvaPercentage} %";
                    accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------------

                if (item.RTFValue > 0)
                {
                    /// Concepto de RETENCION EN LA FUENTE de los productos
                    var detail = accountPayable.AccountPayableDetailConcept.Where(x => x.IdRetentionConcept == productGroup.RetentionConceptsWithholdingSourceId).FirstOrDefault();
                    if (detail != null)
                    {
                        detail.BaseValue += item.SubTotalValue - item.DiscountValue;
                        detail.Value += item.RTFValue;
                    }
                    else
                    {
                        accountPayableDetail = new AccountPayableDetailConcept();
                        accountPayableDetail.IdConceptAccountPayable = Convert.ToInt32(productGroup.ConceptAccountPayableWithholdingSourceId);
                        accountPayableDetail.IdAccount = Convert.ToInt32(productGroup.AccountWithholdingSourceId);
                        accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                        accountPayableDetail.IdCostCenter = _pucAdminService.MainAccountHandlesCostCenter(accountPayableDetail.IdAccount) ? _costCenterId : (int?)null;
                        accountPayableDetail.Nature = 2; //Credito
                        accountPayableDetail.BaseValue = item.SubTotalValue - item.DiscountValue;
                        accountPayableDetail.BillingValue = totalInvoiceValue - entranceVoucher.ValueDiscount + entranceVoucher.ValueTax;
                        accountPayableDetail.Value = item.RTFValue;
                        accountPayableDetail.TotalConcept = item.RTFValue;
                        accountPayableDetail.IdRetentionConcept = productGroup.RetentionConceptsWithholdingSourceId;
                        accountPayableDetail.Percentage = item.RTFPercentage;
                        accountPayableDetail.DeferredCausation = false;
                        accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'RTF Productos'";
                        accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
                    }
                }
            }

            // Calcular la retención en la fuente del flete aplicando el porcentaje de cada concepto al valor del flete
            if (entranceVoucher.FreightValue > 0)
            {
                // Obtener todos los conceptos de retención en la fuente que se crearon
                var rtfConcepts = accountPayable.AccountPayableDetailConcept
                    .Where(x => x.IdRetentionConcept != null && x.Detail != null && x.Detail.Contains("RTF") && x.Percentage.HasValue)
                    .ToList();
                
                if (rtfConcepts.Any())
                {
                    // Para cada concepto de RTF, calcular la retención sobre el flete usando su porcentaje
                    foreach (var concept in rtfConcepts)
                    {
                        // Calcular la retención del flete para este concepto: Flete * (Porcentaje / 100)
                        decimal rtfFleteConcepto = Math.Round(entranceVoucher.FreightValue * concept.Percentage.Value / 100, 2, MidpointRounding.AwayFromZero);
                        
                        // Agregar la retención del flete al concepto
                        concept.Value += rtfFleteConcepto;
                        concept.TotalConcept += rtfFleteConcepto;
                    }
                }
            }

            if (isConsigna && !errors.Any())
            {
                await _consignmentInventoryRemissionDetailControlRepository.SaveEntityMassiveAsync(batchAndControl?
                                                                                    .ConsignmentInventoryRemissionDetailControl);
                await _consignmentInventoryRemissionDetailBatchSerialRepository.SaveEntityMassiveAsync(batchAndControl?
                                                                                            .ConsignmentInventoryRemissionDetailBatchSerial);
            }

            if (entranceVoucher.WithholdingTax > 0)
            {
                // Retencion de IVA
                accountPayableDetail = new AccountPayableDetailConcept();
                if (settings.IVARetention == 1)
                {
                    AccountPayableConcepts IvaConcept = _supplierRepository.GetIVARetentionAccountPayableConceptBySupplierId(entranceVoucher.SupplierId);
                    accountPayableDetail.IdConceptAccountPayable = IvaConcept.Id;
                    accountPayableDetail.IdAccount = Convert.ToInt32(IvaConcept.IdAccount);
                    accountPayableDetail.IdRetentionConcept = IvaConcept.RetentionConceptId;
                    accountPayableDetail.Percentage = _supplierRepository.GetIVARetentionPercentageBySupplierId(entranceVoucher.SupplierId);                    
                    accountPayableDetail.IdCostCenter = IvaConcept.MainAccounts.HandlesCostCenter
                                                        ? warehouse.CostCenterId
                                                        : (int?)null;
                }
                else
                {
                    accountPayableDetail.IdConceptAccountPayable = Convert.ToInt32(settings.IVARetentionAccountPayableConceptId);
                    accountPayableDetail.IdAccount = Convert.ToInt32(settings.AccountIVARetentionId);
                    accountPayableDetail.IdRetentionConcept = settings.RetentionConceptsIVARetentionId;
                    accountPayableDetail.Percentage = Convert.ToDecimal(settings.WithholdingIvaPercentage);
                    accountPayableDetail.IdCostCenter = settings.HandlesCostCenterIVARetention
                                                        ? warehouse.CostCenterId
                                                        : (int?) null;
                }                
                accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);                
                accountPayableDetail.Nature = 2; //Credito
                accountPayableDetail.BaseValue = entranceVoucher.ValueTax + entranceVoucher.FreightIVAValue;
                accountPayableDetail.BillingValue = totalInvoiceValue - entranceVoucher.ValueDiscount + entranceVoucher.ValueTax;
                accountPayableDetail.Value = entranceVoucher.WithholdingTax;
                accountPayableDetail.TotalConcept = entranceVoucher.WithholdingTax;
                accountPayableDetail.DeferredCausation = false;
                accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'Retencion IVA'";
                accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
            }

            if (entranceVoucher.WithholdingICA > 0)
            {
                // Retencion de ICA
                accountPayableDetail = new AccountPayableDetailConcept();
                AccountPayableConcepts apc = _paymentConceptsRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitIdPay(entranceVoucher.SupplierDistributionLineId, entranceVoucher.OperatingUnitId);
                accountPayableDetail.IdConceptAccountPayable = apc.Id;
                accountPayableDetail.IdAccount = Convert.ToInt32(apc.IdAccount);
                accountPayableDetail.IdRetentionConcept = apc.RetentionConceptId;
                accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);                
                accountPayableDetail.IdCostCenter = apc.MainAccounts.HandlesCostCenter
                                                    ? warehouse.CostCenterId 
                                                    :(int?) null;
                accountPayableDetail.Nature = 2; //Credito
                accountPayableDetail.BaseValue = totalInvoiceValue - entranceVoucher.ValueDiscount;
                accountPayableDetail.BillingValue = totalInvoiceValue - entranceVoucher.ValueDiscount + entranceVoucher.ValueTax;
                accountPayableDetail.Value = entranceVoucher.WithholdingICA;
                accountPayableDetail.TotalConcept = entranceVoucher.WithholdingICA;
                accountPayableDetail.Percentage = entranceVoucher.IcaPercentage;
                accountPayableDetail.DeferredCausation = false;
                accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'Retencion ICA'";
                accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
            }

            if (entranceVoucher.FreightValue > 0)
            {
                // Flete
                accountPayableDetail = new AccountPayableDetailConcept();
                accountPayableDetail.IdConceptAccountPayable = settings.FreightAccountPayableConceptId;
                accountPayableDetail.IdAccount = settings.FreigthAcountId;
                accountPayableDetail.IdRetentionConcept = null;
                accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);               
                accountPayableDetail.IdCostCenter = settings.FreightHandlesCostCenter
                                                    ? warehouse.CostCenterId
                                                    : (int?) null;
                accountPayableDetail.Nature = 1; //Debito
                accountPayableDetail.BaseValue = entranceVoucher.FreightValue;
                accountPayableDetail.BillingValue = entranceVoucher.FreightValue;
                accountPayableDetail.Value = entranceVoucher.FreightValue;
                accountPayableDetail.TotalConcept = entranceVoucher.FreightValue;
                accountPayableDetail.Percentage = null;
                accountPayableDetail.DeferredCausation = false;
                accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'Flete'";
                accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
                if (entranceVoucher.FreightIVAValue > 0)
                {
                    // IVA Flete
                    accountPayableDetail = new AccountPayableDetailConcept();
                    accountPayableDetail.IdConceptAccountPayable = settings.IVAFreightAccountPayableConceptId;
                    accountPayableDetail.IdAccount = Convert.ToInt32(settings.IvaFreigthAccountId);
                    accountPayableDetail.IdRetentionConcept = settings.IvaFreigthRetentionConceptsId;
                    accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                    accountPayableDetail.IdCostCenter = settings.IvaFreightHandlesCostCenter
                                                        ? warehouse.CostCenterId 
                                                        : (int?) null;
                    accountPayableDetail.Nature = 1; //Debito
                    accountPayableDetail.BaseValue = entranceVoucher.FreightIVAValue;
                    accountPayableDetail.BillingValue = entranceVoucher.FreightIVAValue;
                    accountPayableDetail.Value = entranceVoucher.FreightIVAValue;
                    accountPayableDetail.TotalConcept = entranceVoucher.FreightIVAValue;
                    accountPayableDetail.Percentage = entranceVoucher.FreightIVAPercentage;
                    accountPayableDetail.DeferredCausation = false;
                    accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'IVA Flete'";
                    accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
                }
            }

            // Cargo las Otras Retenciones y Deducciones
            if (entranceVoucher.EntranceVoucherOtherDeduction != null)
            {
                if (entranceVoucher.EntranceVoucherOtherDeduction.Count > 0)
                {
                    List<Domain.Entities.OtherWithholdingDeduction> ListRetentionDeduction = _otherWithholdingDeductionRepository.ListOtherWithholdingDeduction();
                    foreach (var ord in entranceVoucher.EntranceVoucherOtherDeduction)
                    {
                        if (ord.Value > 0)
                        {
                            accountPayableDetail = new AccountPayableDetailConcept();
                            Domain.Entities.OtherWithholdingDeduction otherWithholdingDeduction = new Domain.Entities.OtherWithholdingDeduction();
                            foreach (var retentionDeduction in ListRetentionDeduction)
                            {
                                if (retentionDeduction.Id == ord.OtherWithholdingDeductionId)
                                {
                                    otherWithholdingDeduction = retentionDeduction;
                                    break;
                                }
                            }
                            accountPayableDetail.IdConceptAccountPayable = Convert.ToInt32(otherWithholdingDeduction.AccountPayableConceptId);
                            accountPayableDetail.IdAccount = Convert.ToInt32(otherWithholdingDeduction.AccountPayableConcepts.IdAccount);
                            accountPayableDetail.BaseValue = totalInvoiceValue;
                            accountPayableDetail.BillingValue = accountPayableDetail.BaseValue;
                            accountPayableDetail.Value = Convert.ToDecimal(Utils.RoundValue(Convert.ToDecimal(ord.Value), entranceVoucher.RoundService));
                            accountPayableDetail.TotalConcept = Convert.ToDecimal(Utils.RoundValue(Convert.ToDecimal(ord.Value), entranceVoucher.RoundService));
                            accountPayableDetail.Nature = 2; // Credito
                            string var = string.Empty;
                            if (otherWithholdingDeduction.Type == 1)  // Otras Retenciones
                            {
                                accountPayableDetail.IdRetentionConcept = otherWithholdingDeduction.AccountPayableConcepts.RetentionConceptId;
                                accountPayableDetail.Percentage = otherWithholdingDeduction.AccountPayableConcepts.RetentionConcepts.Rate;
                                var = " 'Otras Retenciones'";
                            }
                            else // Otras Deducciones
                            {
                                accountPayableDetail.IdRetentionConcept = null;
                                accountPayableDetail.Percentage = null;
                                var = " 'Otras Deduciones'";
                            }
                            accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                            accountPayableDetail.IdCostCenter = otherWithholdingDeduction.AccountPayableConcepts.MainAccounts.HandlesCostCenter ? warehouse.CostCenterId : (int?)null;
                            accountPayableDetail.DeferredCausation = false;
                            accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada" + var;
                            accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
                        }
                    }
                }
            }

            //Si hay detalles de compromisos los genero en la cxp
            if(entranceVoucher.EntranceVoucherCommitment != null && entranceVoucher.EntranceVoucherCommitment.Count() > 0)
            {
                foreach (var item in entranceVoucher.EntranceVoucherCommitment)
                {
                    AccountPayableCommitments apc = new AccountPayableCommitments();
                    apc.CommitmentDetailId = item.CommitmentDetailId;
                    apc.Value = item.Value;
                    accountPayable.AccountPayableCommitments.Add(apc);
                }
            }
            if (Supplier.ConsignmentInventoryCosting == 1)
            {
                accountPayable.Value = totalInvoiceValue + entranceVoucher.ValueTax + entranceVoucher.FreightValue + entranceVoucher.FreightIVAValue;
                accountPayable.Balance = accountPayable.Value;
            }
            else
            {
                accountPayable.Value = entranceVoucher.TotalValue;
                accountPayable.Balance = entranceVoucher.TotalValue;
            }

            ///creo las cuotas
            AccountPayableShares aps = new AccountPayableShares();
            aps.Share = 1;
            aps.DateExpires = accountPayable.ExpirationDate;
            aps.InitialValue = accountPayable.Value;
            aps.Balance = accountPayable.Value;
            accountPayable.AccountPayableShares.Add(aps);

            if (errors.Count > 0)
            {
                return new ActionResult<Domain.Entities.AccountPayable> { StateResult = false, MessageResult = errors.Distinct().ToList() };
            }
            return new ActionResult<Domain.Entities.AccountPayable> { StateResult = true, ObjectEmbbeded = accountPayable };
        }

        public async Task<ActionResult<Domain.Entities.EntranceVoucher>> SaveAndConfirmbEntranceVoucherAsync(Domain.Entities.EntranceVoucher entranceVoucher, string ContainerNameCrystal, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null, Boolean controlCost = false)
        {

            var result = SaveEntranceVoucher(entranceVoucher, audit, idSequence, sequenceC);
            if (result.StateResult == true)
            {
                var resultConfirm = await ConfirmEntranceVoucherAsync(result.ObjectEmbbeded, audit, ContainerNameCrystal, controlCost);
                if (resultConfirm.StateResult == true)
                {
                    if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                    {
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = string.Format(ResourceManager.get_GetString("SaveAndConfirmEntranceVoucher", "Inventory"), result.ObjectEmbbeded.Code, resultConfirm.MessageResult.ElementAt(0)) };
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = true, MessageResultAux = resultConfirm.MessageResultAux, Message = string.Format(ResourceManager.get_GetString("UpdateAndConfirmEntranceVoucher", "Inventory"), result.ObjectEmbbeded.Code, resultConfirm.MessageResult.ElementAt(0)) };
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
                                return new ActionResult<Domain.Entities.EntranceVoucher> { MessageResult = new List<string> { "" }, StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, string.Join(Environment.NewLine, resultConfirm.MessageResult)) };
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.EntranceVoucher> { MessageResult = resultConfirm.MessageResult, StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, string.Join(Environment.NewLine, resultConfirm.MessageResult)) };
                            }
                        }
                        else
                        {
                            if (resultConfirm.MessageResult == null || resultConfirm.MessageResult.Count == 0)
                            {
                                return new ActionResult<Domain.Entities.EntranceVoucher> { MessageResult = new List<string> { "" }, StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + string.Join(Environment.NewLine, resultConfirm.MessageResult))) };
                            }
                            else
                            {
                                return new ActionResult<Domain.Entities.EntranceVoucher> { MessageResult = resultConfirm.MessageResult, StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + string.Join(Environment.NewLine, resultConfirm.MessageResult))) };
                            }
                        }
                    }
                    else
                    {
                        if (action == Infrastructure.CrossCutting.Audit.Actions.Insert)
                        {
                            return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("SavedNoConfirmed", "Inventory"), result.ObjectEmbbeded.Code, string.Join(Environment.NewLine, resultConfirm.MessageResult)) };
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = true, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("UpdatedNoConfirmed", "Inventory"), (Environment.NewLine + string.Join(Environment.NewLine, resultConfirm.MessageResult))) };
                        }
                    }
                }
            }
            else
            {
                return new ActionResult<Domain.Entities.EntranceVoucher> { StateResult = false, ObjectEmbbeded = result.ObjectEmbbeded, StateResultAux = false, Message = string.Format(ResourceManager.get_GetString("NoSaved", "Inventory"), (Environment.NewLine + result.Message)) };
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public DataSet SP_GetEntranceVoucher(string parameters)//, SessionValues session)
        {
            if (string.IsNullOrEmpty(parameters))
                throw new ArgumentNullException("parameters");

            try
            {
                DataSet ds;
                string query1;
                query1 = string.Format("exec Inventory.SP_GetEntranceVoucher {0}", parameters);
                ds = this.GetDatatable(query1, "SP_GetEntranceVoucher"); //session, "SP_GetEntranceVoucher");
                return ds;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Comando"></param>
        /// <param name="session"></param>
        /// <param name="nameDt"></param>
        /// <returns></returns>
        private DataSet GetDatatable(string Comando, string nameDt)//SessionValues session, string nameDt)
        {
            SqlConnection conexion = new SqlConnection();
            DataSet _GetDatatable;

            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion = new SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, string.Empty, ServerSessionValues.Current.CurrentContainer, false)); //session.TransactionalContainer, false));
                    conexion.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(Comando, conexion);
                da.SelectCommand.CommandTimeout = 30000;
                DataSet ds = new DataSet();
                da.Fill(ds, nameDt);
                _GetDatatable = ds;
                da = null/* TODO Change to default(_) if this is not a reference type */;
                ds = null/* TODO Change to default(_) if this is not a reference type */;
                conexion.Close();
                return _GetDatatable;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
            finally
            {
                conexion.Close();
            }
        }

        private ActionResult RemissionConsignmentControlCxPDetail(EntranceVoucherDetail item,
                                                                      ref ConsignmentRelationControl consignmentRelationControl,
                                                                    ref AccountPayable accountPayable,
                                                                    decimal tRM,
                                                                    Domain.Entities.Warehouse warehouse,
                                                                    Domain.Entities.SettingInventory settings,
                                                                    Domain.Entities.InventoryProduct product,
                                                                    Domain.Entities.ProductGroup productGroup,
                                                                    ref Dictionary<int, Domain.Entities.SettingInventory> dictionarySettings,
                                                                    List<Domain.Payroll.Entities.FunctionalUnit> functionalListConsigment,
                                                                    int? costCenterId,
                                                                    byte ApplyConsignmentInventoryCosting,
                                                                    byte? taxRegistration)
        {

            var consignmentInventoryRemissionDetailBatchSerial = consignmentRelationControl?
                                                                .ConsignmentInventoryRemissionDetailBatchSerial?
                                                                .Find(g => g.Id == item.ConsignmentInventoryRemissionDetailBatchSerialId);
            var consignmentInventoryRemissionDetailControls = consignmentRelationControl?
                                                                .ConsignmentInventoryRemissionDetailControl?
                                                                .FindAll(g => g.ConsignmentInventoryRemissionDetailBatchSerialId == item.ConsignmentInventoryRemissionDetailBatchSerialId);

            if (consignmentInventoryRemissionDetailBatchSerial is null)
            {
                return new ActionResult { StateResult = false, Message = String.Format("No se encontro el detalle de la remisión del inventario en consignación del producto '{0}'", item.ProductCodeName) };
            }

            if (consignmentInventoryRemissionDetailControls is null || !consignmentInventoryRemissionDetailControls.Any())
            {
                return new ActionResult { StateResult = false, Message = $"No se encontró un registro de control para producto en consigna {item.ProductCodeName}" };
            }

            int quantity = item.Quantity;
            decimal baseValue = 0;
            decimal totalItemsValue = 0;
            decimal totalWithIva = 0;
            decimal itemValueIva = (item.IvaValue / item.Quantity);
            bool ivaAtCost = taxRegistration == 1;

            if ((consignmentInventoryRemissionDetailBatchSerial.UsedQuantity - consignmentInventoryRemissionDetailBatchSerial.LegalizedQuantity) < item.Quantity)
            {
                return new ActionResult { StateResult = false, Message = String.Format("El detalle de la remisión del producto {0} no posee la cantidad suficiente ({1}) a legalizar ({2})", item.ProductCodeName, (consignmentInventoryRemissionDetailBatchSerial.UsedQuantity - consignmentInventoryRemissionDetailBatchSerial.LegalizedQuantity), quantity) };
            }

            List<ConsignmentInventoryRemissionDetailControl> listConsignmentInventoryRemissionDetailControl = new List<ConsignmentInventoryRemissionDetailControl>();

            decimal ValueTemp = 0;
            foreach (var consignmentInventoryRemissionDetailControl in consignmentInventoryRemissionDetailControls)
            {
                // No se tienen cantidades que legalizar
                if (!(quantity > 0))
                {
                    break;
                }

                consignmentInventoryRemissionDetailControl.StartTracking();
                consignmentInventoryRemissionDetailControl.MarkAsModified();

                if (ApplyConsignmentInventoryCosting == 1)
                {
                    ValueTemp = item.SubTotalValue;
                }
                else
                {
                    ValueTemp = consignmentInventoryRemissionDetailControl.Value;
                }

                int quantityLegalized = consignmentInventoryRemissionDetailControl.QuantityPendingLegalization;
                quantityLegalized = (quantity > quantityLegalized) ? quantityLegalized : quantity;
                decimal value = Math.Round((ValueTemp / tRM) * quantityLegalized, MidpointRounding.AwayFromZero);
                decimal valueIva = Math.Round((itemValueIva * quantityLegalized), 2, MidpointRounding.AwayFromZero);
                decimal valueAccountPayable = ivaAtCost ? (value - valueIva) : value;

                //Valor inventario
                AccountPayableDetailConcept accountPayableDetail = new AccountPayableDetailConcept();
                accountPayableDetail.IdConceptAccountPayable = Convert.ToInt32(productGroup.ConceptAccountPayableInventory);
                accountPayableDetail.IdAccount = Convert.ToInt32(productGroup.CounterpartCostConsignedInventoryId);
                accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                accountPayableDetail.IdCostCenter = productGroup.CounterpartCostConsignedHandlessCostCenter ? costCenterId : null;
                accountPayableDetail.Nature = (byte)(valueAccountPayable > 0 ? 1 : 2);
                accountPayableDetail.BaseValue = item.SubTotalValue - item.DiscountValue + item.IvaValue;
                accountPayableDetail.BillingValue = accountPayableDetail.BaseValue;
                accountPayableDetail.Value = Math.Abs(valueAccountPayable);
                accountPayableDetail.IdRetentionConcept = null;
                accountPayableDetail.Percentage = null;
                accountPayableDetail.DeferredCausation = false;
                accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'Valor Bruto Producto En Consignación'";
                accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);

                consignmentInventoryRemissionDetailControl.QuantityLegalized = quantityLegalized;
                consignmentInventoryRemissionDetailControl.QuantityPendingLegalization = consignmentInventoryRemissionDetailControl.QuantityPendingLegalization - quantityLegalized;

                quantity = quantity - quantityLegalized;
                totalItemsValue += value;
                totalWithIva += Math.Abs(valueAccountPayable);
                //Agrego a la lista de control de la mercancia en consignacion
                listConsignmentInventoryRemissionDetailControl.Add(consignmentInventoryRemissionDetailControl);

            }

            baseValue = ivaAtCost
                ? ((item.SubTotalValue - item.DiscountValue + item.IvaValue) - totalItemsValue)
                : ((item.SubTotalValue - item.DiscountValue) - totalItemsValue);
            if (ivaAtCost)
            {
                totalWithIva += item.IvaValue;
            }
            if (totalWithIva != totalItemsValue)
            {
                var dif = totalItemsValue - totalWithIva;
                if (Math.Abs(dif) <= 3)
                {
                    var newValue = accountPayable.AccountPayableDetailConcept.LastOrDefault(x => (x.Value + dif) > 0);
                    newValue.Value += dif;
                }
            }

            //si quedan cantidades sin legalizar
            if (quantity != 0)
            {
                return new ActionResult { StateResult = false, Message = String.Format("El producto {0}, tiene cantidades pendientes de legalizar {1}", item.ProductCodeName, quantity) };
            }

            if (baseValue != 0)
            {
                Decimal distribuitedValue = 0;
                var groupedResults = listConsignmentInventoryRemissionDetailControl
                                        .GroupBy(x => new { x.OperatingUnitId, x.FunctionalUnitId })
                                        .Select(g => new
                                        {
                                            OperatingUnitId = g.Key.OperatingUnitId,
                                            FunctionalUnitId = g.Key.FunctionalUnitId,
                                            QuantityLegalized = g.Sum(x => x.QuantityLegalized)
                                        })
                                        .ToList();


                foreach (var consignmentInventoryRemissionDetailControl in groupedResults)
                {
                    Domain.Entities.SettingInventory settingsTemp;
                    int CostAccountId = 0;

                    if (!dictionarySettings.ContainsKey((int)consignmentInventoryRemissionDetailControl.OperatingUnitId))
                    {
                        settingsTemp = _settingInventoryRepository.GetSettingInventory((int)consignmentInventoryRemissionDetailControl.OperatingUnitId);
                        dictionarySettings.Add((int)consignmentInventoryRemissionDetailControl.OperatingUnitId, settingsTemp);
                    }

                    settingsTemp = dictionarySettings[(int)consignmentInventoryRemissionDetailControl.OperatingUnitId];

                    if (settingsTemp.AssociateCostMainAccount == 1)
                    {
                        var settingInventoryFunctionalUnit = settingsTemp.SettingInventoryFunctionalUnit.FirstOrDefault(x => x.FunctionalUnitId == consignmentInventoryRemissionDetailControl.FunctionalUnitId);
                        
                        if (settingInventoryFunctionalUnit == null)
                        {
                            return new ActionResult { StateResult = false, Message = String.Format("No se encuentró los parámetros de inventario necesarios para realizar la legalización del producto {0} con cantidad {1}", item.ProductCodeName, quantity) };
                        }

                        CostAccountId = settingInventoryFunctionalUnit.CostAccountId;
                    }
                    else if (settingsTemp.AssociateCostMainAccount == 2)
                    {
                        // CIMA-52985: movimientos como la devolucion de remision al proveedor
                        // (RemissionDevolution) no tienen Unidad Funcional de negocio real, ya que
                        // la mercancia nunca fue consumida por ningun servicio. En ese caso se usa
                        // la cuenta de contrapartida de inventario en consignacion del grupo en vez
                        // de exigir el cruce por Unidad Funcional.
                        if (consignmentInventoryRemissionDetailControl.FunctionalUnitId == null)
                        {
                            CostAccountId = Convert.ToInt32(productGroup.CounterpartCostConsignedInventoryId);
                        }
                        else if (productGroup.ProductGroupFunctionalUnit == null || !productGroup.ProductGroupFunctionalUnit.Any(f => f.FunctionalUnitId == consignmentInventoryRemissionDetailControl.FunctionalUnitId))
                        {
                            return new ActionResult { StateResult = false, Message = String.Format("No se encuentró la unidad funcional parametrizada en el grupo {0} - {1} para realizar la legalización del producto {2} con cantidad {3}", productGroup.Code, productGroup.Name, item.ProductCodeName, quantity) };
                        }
                        else
                        {
                            CostAccountId = productGroup.ProductGroupFunctionalUnit.Where(f => f.FunctionalUnitId == consignmentInventoryRemissionDetailControl.FunctionalUnitId).FirstOrDefault().CostAccountId;
                        }
                    }

                    var value = Math.Round(baseValue * consignmentInventoryRemissionDetailControl.QuantityLegalized / item.Quantity, 2, MidpointRounding.AwayFromZero);

                    var costCentertempId = costCenterId;
                    if (settingsTemp.AssociateCostCenter == 1)
                    {
                        var functionalUnit = functionalListConsigment.FirstOrDefault(t => t.Id == consignmentInventoryRemissionDetailControl.FunctionalUnitId);
                        costCenterId = functionalUnit.CostCenterId;
                    }

                    //Valor costo
                    AccountPayableDetailConcept accountPayableDetail = new AccountPayableDetailConcept();
                    accountPayableDetail.IdConceptAccountPayable = Convert.ToInt32(productGroup.ConceptAccountPayableInventory);
                    accountPayableDetail.IdAccount = CostAccountId;
                    accountPayableDetail.IdThirdParty = Convert.ToInt32(accountPayable.IdThirdParty);
                    accountPayableDetail.IdCostCenter = costCenterId;
                    accountPayableDetail.Nature = (byte)(value > 0 ? 1 : 2);
                    accountPayableDetail.BaseValue = item.SubTotalValue - item.DiscountValue + item.IvaValue;
                    accountPayableDetail.BillingValue = accountPayableDetail.BaseValue;
                    accountPayableDetail.Value = Math.Abs(value);
                    accountPayableDetail.IdRetentionConcept = null;
                    accountPayableDetail.Percentage = null;
                    accountPayableDetail.DeferredCausation = false;
                    accountPayableDetail.Detail = "Detalle de cuenta por pagar generada por Comprobante de Entrada 'Costo Producto En Consignación'";
                    accountPayable.AccountPayableDetailConcept.Add(accountPayableDetail);
                    distribuitedValue += accountPayableDetail.Value;

                }

                //Todo el valor debe ser distribuido, no debe quedar residuos
                var residuo = Math.Abs(baseValue) - distribuitedValue;
                if (residuo != 0)
                {
                    if (Math.Abs(baseValue) > distribuitedValue)
                    {
                        accountPayable.AccountPayableDetailConcept.LastOrDefault().Value += (Math.Abs(baseValue) - distribuitedValue);
                    }
                    else
                    {
                        //valor faltante y lo tomamos de un concepto que sea mayor al valor faltante
                        decimal missingValue = (distribuitedValue - Math.Abs(baseValue));
                        accountPayable.AccountPayableDetailConcept.Where(x => x.Value > missingValue).LastOrDefault().Value -= missingValue;
                    }
                }

            }

            consignmentInventoryRemissionDetailBatchSerial.StartTracking();
            consignmentInventoryRemissionDetailBatchSerial.LegalizedQuantity += item.Quantity;
            consignmentInventoryRemissionDetailBatchSerial.MarkAsModified();

            return new ActionResult { StateResult = true, Message = "Proceso de costeo inventario legalización CxP exitoso" };
        }

        public async Task<int> ExecuteRollbackAsync(string value)
        {
            var res = _EntranceVoucherRepository.CascadeRollback(value);
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
                    _accountPayableAdminService.Dispose();
                }
                _EntranceVoucherRepository = null;
                _sequenseRepository = null;
                _physicalInventoryAdminService = null;
                _purchaseOrderDetailRepository = null;
                _inventoryContractDetailRepository = null;
                _remissionEntranceDetailBatchRepository = null;
                _accountPayableRepository = null;
                _supplierRepository = null;
                _suppliersDistributionLinesRepository = null;
                _settingInventoryRepository = null;
                _inventoryProductRepository = null;
                _otherWithholdingDeductionRepository = null;
                _accountPayableAdminService = null;
                _sequensePaymentsCRepository = null;
                _warehouseRepository = null;
                _InventoryControlDocumentRepository = null;
                _paymentConceptsRepository = null;
                _physicalInventoryRepository = null;
                _productGroupsRepository = null;
                _consignmentInventoryRemissionDetailControlRepository = null;
                _consignmentInventoryRemissionDetailBatchSerialRepository = null;
                _functionalUnitRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion 
    }
}
