using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Text;
using System.Transactions;

namespace Application.Inventory.InventoryContractAssignment
{
    public class InventoryContractAssignmentAdminService : IInventoryContractAssignmentAdminService
    {

        #region Variables

        private IInventoryContractAssignmentRepository _InventoryContractAssignmentRepository;

        #endregion

        #region Builder

        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        public InventoryContractAssignmentAdminService(IInventoryContractAssignmentRepository InventoryContractAssignmentRepository)
        {
            _InventoryContractAssignmentRepository = InventoryContractAssignmentRepository;
        }

        #endregion

        #region Methods

        public ActionResult<Domain.Entities.InventoryContractAssignment> GetInventoryContractAssignment(string code, AuditMessage audit)
        {
            try
            {
                var entity = _InventoryContractAssignmentRepository.GetInventoryContractAssignment(code);
                return new ActionResult<Domain.Entities.InventoryContractAssignment>() { StateResult = true, ObjectEmbbeded = entity };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContractAssignment>() { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }

        public ActionResult<Domain.Entities.InventoryContractAssignment> GetInventoryContractAssignmentById(int id)
        {
            try
            {
                var entity = _InventoryContractAssignmentRepository.GetInventoryContractAssignmentById(id);
                return new ActionResult<Domain.Entities.InventoryContractAssignment>() { StateResult = true, ObjectEmbbeded = entity };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContractAssignment>() { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }

        public ActionResult<Domain.Entities.InventoryContractAssignment> SaveInventoryContractAssignment(Domain.Entities.InventoryContractAssignment InventoryContractAssignment, AuditMessage audit)
        {
            if (InventoryContractAssignment == null)
            {
                throw new ArgumentNullException("InventoryContractAssignment");
            }

            var unitOfWork = this._InventoryContractAssignmentRepository.UnitWork;

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    string InventoryContractAssignmentXML = ConvertInventoryContractAssignmentToXml(InventoryContractAssignment);
                    SP_SaveInventoryContractAssignment_Result result = this._InventoryContractAssignmentRepository.SP_SaveInventoryContractAssignment(InventoryContractAssignmentXML, audit.CodeUser);

                    if (result.CodeResult != 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.InventoryContractAssignment> { StateResult = false, Message = result.MessageResult };
                    }

                    InventoryContractAssignment.Id = result.Id.Value;
                    InventoryContractAssignment.Code = result.Code;
                    InventoryContractAssignment.MarkAsUnchanged();

                    scope.Complete();
                    return new ActionResult<Domain.Entities.InventoryContractAssignment> { StateResult = true, ObjectEmbbeded = InventoryContractAssignment, Message = result.MessageResult };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContractAssignment> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContractAssignment> { StateResult = false, Message = ex.Message };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContractAssignment> { StateResult = false, Message = ex.Message };
                }
            }
        }

        #endregion

        #region Private Methods

        private string ConvertInventoryContractAssignmentToXml(Domain.Entities.InventoryContractAssignment InventoryContractAssignment)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<InventoryContractAssignment>");

            result.Append("<Id>" + InventoryContractAssignment.Id + "</Id>");
            result.Append("<OperatingUnitId>" + InventoryContractAssignment.OperatingUnitId + "</OperatingUnitId>");
            result.Append("<Code>" + InventoryContractAssignment.Code + "</Code>");
            result.Append("<DocumentDate>" + InventoryContractAssignment.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") + "</DocumentDate>");
            result.Append("<ContractId>" + InventoryContractAssignment.ContractId + "</ContractId>");
            result.Append("<SupplierTransferorId>" + InventoryContractAssignment.SupplierTransferorId + "</SupplierTransferorId>");
            result.Append("<SupplierDistributionLineTransferorId>" + InventoryContractAssignment.SupplierDistributionLineTransferorId + "</SupplierDistributionLineTransferorId>");
            result.Append("<SupplierAssigneeId>" + InventoryContractAssignment.SupplierAssigneeId + "</SupplierAssigneeId>");
            result.Append("<SupplierDistributionLineAssigneeId>" + InventoryContractAssignment.SupplierDistributionLineAssigneeId + "</SupplierDistributionLineAssigneeId>");
            result.Append("<Description>" + InventoryContractAssignment.Description + "</Description>");
            result.Append("<Status>" + InventoryContractAssignment.Status + "</Status>");

            result.Append("</InventoryContractAssignment>");

            return result.ToString();
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

                _InventoryContractAssignmentRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion
    }
}
