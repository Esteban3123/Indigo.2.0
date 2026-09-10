//'************************************************************
//' Assembly         : Domain.Inventory.InventoryContractRepository
//' Author           : Henry Alejandro Vargas Polania
//' Created          : 07/01/2015
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

namespace Application.Inventory.InventoryContract
{
    public class InventoryContractAdminService : IInventoryContractAdminService
    {
        #region Variables
        private IInventoryContractRepository _InventoryContractRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        #endregion

        #region Builder
        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        /// <param name="inventoryContractRepository"></param>
        /// <param name="sequenseRepository"></param>IInventoryContractAdminService
        public InventoryContractAdminService(IInventoryContractRepository inventoryContractRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            if (inventoryContractRepository == null)
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _InventoryContractRepository = inventoryContractRepository;
            _sequenseRepository = sequenseRepository;
        }
        #endregion

        #region Methods
        public ActionResult<Domain.Entities.InventoryContract> SaveInventoryContract(Domain.Entities.InventoryContract inventoryContract, AuditMessage audit, long idSecuence = 0)
        {
            if (inventoryContract == null)
            {
                throw new ArgumentNullException("InventoryContract");
            }
            
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    string xml = ConvertToXml(inventoryContract);
                    SP_SaveInventoryContract_Result result = _InventoryContractRepository.SP_SaveInventoryContract(xml, audit.CodeUser);

                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.InventoryContract> { StateResult = false, Message = result.Message };
                    }

                    scope.Complete();
                    inventoryContract.Code = result.InventoryContractCode;
                    return new ActionResult<Domain.Entities.InventoryContract> { StateResult = true, ObjectEmbbeded = inventoryContract, Message = result.Message };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContract> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContract> { StateResult = false, Message = ex.Message };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContract> { StateResult = false, Message = ex.Message };
                }
            }
        }

        /// <summary>
        /// Método que convierte la entidad de atc a xml
        /// </summary>
        /// <returns></returns>
        private string ConvertToXml(Domain.Entities.InventoryContract inventoryContract)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<InventoryContract>");

            result.Append("<Id>" + inventoryContract.Id + "</Id>");
            result.Append("<Code>" + inventoryContract.Code + "</Code>");
            result.Append("<OperatingUnitId>" + inventoryContract.OperatingUnitId + "</OperatingUnitId>");
            result.Append("<ContractTypeId>" + inventoryContract.ContractTypeId + "</ContractTypeId>");
            if (inventoryContract.DocumentDate != null)
            {
                result.Append("<DocumentDate>" + inventoryContract.DocumentDate?.ToString("dd/MM/yyyy HH:mm:ss") + "</DocumentDate>");
            }
            result.Append("<InitialDate>" + inventoryContract.InitialDate.ToString("dd/MM/yyyy HH:mm:ss") + "</InitialDate>");
            result.Append("<EndDate>" + inventoryContract.EndDate.ToString("dd/MM/yyyy HH:mm:ss") + "</EndDate>");
            result.Append("<SupplierId>" + inventoryContract.SupplierId + "</SupplierId>");
            result.Append("<SupplierDistributionLineId>" + inventoryContract.SupplierDistributionLineId + "</SupplierDistributionLineId>");
            result.Append("<ContractNumber>" + inventoryContract.ContractNumber + "</ContractNumber>");
            result.Append("<Description>" + inventoryContract.Description + "</Description>");
            result.Append("<PaymentMethod>" + inventoryContract.PaymentMethod + "</PaymentMethod>");
            result.Append("<DeliveryMethod>" + inventoryContract.DeliveryMethod + "</DeliveryMethod>");
            result.Append("<DeliveryPlace>" + inventoryContract.DeliveryPlace + "</DeliveryPlace>");
            result.Append("<SourceOrder>" + inventoryContract.SourceOrder + "</SourceOrder>");
            result.Append("<PurchaseProcess>" + inventoryContract.PurchaseProcess + "</PurchaseProcess>");
            result.Append("<Exclusivity>" + inventoryContract.Exclusivity + "</Exclusivity>");
            result.Append("<ManageProducts>" + inventoryContract.ManageProducts + "</ManageProducts>");
            result.Append("<OnlyGuarantee>" + inventoryContract.OnlyGuarantee + "</OnlyGuarantee>");
            result.Append("<TechnicalSupervicion>" + inventoryContract.TechnicalSupervicion + "</TechnicalSupervicion>");
            result.Append("<SupervisionExecution>" + inventoryContract.SupervisionExecution + "</SupervisionExecution>");
            result.Append("<Clauses>" + inventoryContract.Clauses.ConvertToXmlText() + "</Clauses>");
            result.Append("<Attachments>" + inventoryContract.Attachments + "</Attachments>");
            result.Append("<Availability>" + inventoryContract.Availability + "</Availability>");
            result.Append("<Resolution>" + inventoryContract.Resolution + "</Resolution>");
            result.Append("<ResolutionDate>" + ((inventoryContract.ResolutionDate == null) ? "" : inventoryContract.ResolutionDate.Value.ToString("dd/MM/yyyy HH:mm:ss")) + "</ResolutionDate>");
            result.Append("<QuoteNumber>" + inventoryContract.QuoteNumber + "</QuoteNumber>");
            result.Append("<QuoteDate>" + ((inventoryContract.QuoteDate == null) ? "" : inventoryContract.QuoteDate.Value.ToString("dd/MM/yyyy HH:mm:ss")) + "</QuoteDate>");
            result.Append("<RecordNumber>" + inventoryContract.RecordNumber + "</RecordNumber>");
            result.Append("<RecordDate>" + ((inventoryContract.RecordDate == null) ? "" : inventoryContract.RecordDate.Value.ToString("dd/MM/yyyy HH:mm:ss")) + "</RecordDate>");
            result.Append("<NegotiationType>" + inventoryContract.NegotiationType + "</NegotiationType>");
            result.Append("<Approved>" + inventoryContract.Approved + "</Approved>");
            result.Append("<Deadline>" + ((inventoryContract.Deadline == null) ? "" : inventoryContract.Deadline.Value.ToString("dd/MM/yyyy HH:mm:ss")) + "</Deadline>");
            result.Append("<ValidityDate>" + ((inventoryContract.ValidityDate == null) ? "" : inventoryContract.ValidityDate.Value.ToString("dd/MM/yyyy HH:mm:ss")) + "</ValidityDate>");
            result.Append("<Value>" + inventoryContract.Value + "</Value>");
            result.Append("<DiscountValue>" + inventoryContract.DiscountValue + "</DiscountValue>");
            result.Append("<IvaValue>" + inventoryContract.IvaValue + "</IvaValue>");
            result.Append("<TotalValue>" + inventoryContract.TotalValue + "</TotalValue>");
            result.Append("<Status>" + inventoryContract.Status + "</Status>");
            result.Append("<BudgetaryValidityId>" + inventoryContract.BudgetaryValidityId + "</BudgetaryValidityId>");
            result.Append("<CurrencyId>" + inventoryContract.CurrencyId + "</CurrencyId>");

            if (inventoryContract.InventoryContractDetail != null && inventoryContract.InventoryContractDetail.Count() > 0)
            {
                foreach (var item in inventoryContract.InventoryContractDetail)
                {
                    result.Append("<InventoryContractDetail>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<InventoryContractId>" + item.InventoryContractId + "</InventoryContractId>");
                    result.Append("<ProductId>" + item.ProductId + "</ProductId>");
                    result.Append("<Quantity>" + item.Quantity + "</Quantity>");
                    result.Append("<OutstandingQuantity>" + item.OutstandingQuantity + "</OutstandingQuantity>");
                    result.Append("<CancelledQuantity>" + item.CancelledQuantity + "</CancelledQuantity>");
                    result.Append("<Value>" + item.Value + "</Value>");
                    result.Append("<SubTotalValue>" + item.SubTotalValue + "</SubTotalValue>");
                    result.Append("<IvaPercentage>" + item.IvaPercentage + "</IvaPercentage>");
                    result.Append("<IvaValue>" + item.IvaValue + "</IvaValue>");
                    result.Append("<DiscountPercentage>" + item.DiscountPercentage + "</DiscountPercentage>");
                    result.Append("<DiscountValue>" + item.DiscountValue + "</DiscountValue>");
                    result.Append("<TotalValue>" + item.TotalValue + "</TotalValue>");
                    result.Append("<IsDelete>" + ((item.ChangeTracker.State == ObjectState.Deleted) ? 1 : 0) + "</IsDelete>");
                    result.Append("</InventoryContractDetail>");
                }
            }

            if (inventoryContract.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("InventoryContractDetail"))
            {
                var listDelete = (from Domain.Entities.InventoryContractDetail e in inventoryContract.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "InventoryContractDetail").ToList()[0].Value select e).ToList();

                foreach (var item in listDelete)
                {
                    result.Append("<InventoryContractDetail>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<InventoryContractId>" + item.InventoryContractId + "</InventoryContractId>");
                    result.Append("<ProductId>" + item.ProductId + "</ProductId>");
                    result.Append("<Quantity>" + item.Quantity + "</Quantity>");
                    result.Append("<OutstandingQuantity>" + item.OutstandingQuantity + "</OutstandingQuantity>");
                    result.Append("<CancelledQuantity>" + item.CancelledQuantity + "</CancelledQuantity>");
                    result.Append("<Value>" + item.Value + "</Value>");
                    result.Append("<SubTotalValue>" + item.SubTotalValue + "</SubTotalValue>");
                    result.Append("<IvaPercentage>" + item.IvaPercentage + "</IvaPercentage>");
                    result.Append("<IvaValue>" + item.IvaValue + "</IvaValue>");
                    result.Append("<DiscountPercentage>" + item.DiscountPercentage + "</DiscountPercentage>");
                    result.Append("<DiscountValue>" + item.DiscountValue + "</DiscountValue>");
                    result.Append("<TotalValue>" + item.TotalValue + "</TotalValue>");
                    result.Append("<IsDelete>" + 1 + "</IsDelete>");
                    result.Append("</InventoryContractDetail>");
                }
            }

            if (inventoryContract.InventoryContractAvailability != null && inventoryContract.InventoryContractAvailability.Count() > 0)
            {
                foreach (var item in inventoryContract.InventoryContractAvailability)
                {
                    result.Append("<InventoryContractAvailability>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<InventoryContractId>" + item.InventoryContractId + "</InventoryContractId>");
                    result.Append("<AvailabilityDetailId>" + item.AvailabilityDetailId + "</AvailabilityDetailId>");
                    result.Append("<Value>" + item.Value + "</Value>");
                    result.Append("<IsDelete>" + ((item.ChangeTracker.State == ObjectState.Deleted) ? 1 : 0) + "</IsDelete>");
                    result.Append("</InventoryContractAvailability>");
                }
            }

            if (inventoryContract.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("InventoryContractAvailability"))
            {
                var listDelete = (from Domain.Entities.InventoryContractAvailability e in inventoryContract.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "InventoryContractAvailability").ToList()[0].Value select e).ToList();

                foreach (var item in listDelete)
                {
                    result.Append("<InventoryContractAvailability>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<InventoryContractId>" + item.InventoryContractId + "</InventoryContractId>");
                    result.Append("<AvailabilityDetailId>" + item.AvailabilityDetailId + "</AvailabilityDetailId>");
                    result.Append("<Value>" + item.Value + "</Value>");
                    result.Append("<IsDelete>" + 1 + "</IsDelete>");
                    result.Append("</InventoryContractAvailability>");
                }
            }

            result.Append("</InventoryContract>");

            return result.ToString();
        }

        public ActionResult DeleteInventoryContract(Domain.Entities.InventoryContract InventoryContract, AuditMessage audit)
        {
            if (InventoryContract == null)
            {
                throw new ArgumentNullException("product");
            }

            IUnitWork unitOfWork = _InventoryContractRepository.UnitWork;
            try
            {
                InventoryContract.MarkAsDeleted();
                IndigoAuditSimpleEntity<Domain.Entities.InventoryContract> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryContract>(InventoryContract, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _InventoryContractRepository.DeleteEntity(InventoryContract);
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

        public ActionResult<Domain.Entities.InventoryContract> ChangeStateInventoryContract(string code, byte state, AuditMessage audit)
        {
            Domain.Entities.InventoryContract InventoryContract = _InventoryContractRepository.GetInventoryContract(code);
            InventoryContract.Status = state;
            return SaveInventoryContract(InventoryContract, audit);
        }

        public ActionResult<Domain.Entities.InventoryContract> GetInventoryContract(string code, AuditMessage audit)
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
                Domain.Entities.InventoryContract InventoryContract = _InventoryContractRepository.GetInventoryContract(code);
                if (InventoryContract != null && InventoryContract.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryContract> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryContract>(InventoryContract, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryContract> { StateResult = true, ObjectEmbbeded = InventoryContract };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContract> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        public Domain.Entities.InventoryContract GetInventoryContractById(int idInventoryContract)
        {
            if (idInventoryContract == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _InventoryContractRepository.GetInventoryContractById(idInventoryContract);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
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
                    
                }
                _InventoryContractRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
