using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Application.Inventory.InventoryContractModification
{
    public class InventoryContractModificationAdminService : IInventoryContractModificationAdminService
    {
        #region Variables

        private IInventoryContractModificationRepository _InventoryContractModificationRepository;

        #endregion

        #region Builder

        /// <summary>
        /// Inicializa una instacia de la clase
        /// </summary>
        public InventoryContractModificationAdminService(IInventoryContractModificationRepository InventoryContractModificationRepository)
        {
            _InventoryContractModificationRepository = InventoryContractModificationRepository;
        }

        #endregion

        #region Methods

        public ActionResult<Domain.Entities.InventoryContractModification> GetInventoryContractModification(string code, AuditMessage audit)
        {
            try
            {
                var entity = _InventoryContractModificationRepository.GetInventoryContractModification(code);
                return new ActionResult<Domain.Entities.InventoryContractModification>() { StateResult = true, ObjectEmbbeded = entity };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContractModification>() { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }

        public ActionResult<Domain.Entities.InventoryContractModification> GetInventoryContractModificationById(int id)
        {
            try
            {
                var entity = _InventoryContractModificationRepository.GetInventoryContractModificationById(id);
                return new ActionResult<Domain.Entities.InventoryContractModification>() { StateResult = true, ObjectEmbbeded = entity };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryContractModification>() { StateResult = false, Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }

        public ActionResult<Domain.Entities.InventoryContractModification> SaveInventoryContractModification(Domain.Entities.InventoryContractModification InventoryContractModification, AuditMessage audit)
        {
            if (InventoryContractModification == null)
            {
                throw new ArgumentNullException("InventoryContractModification");
            }

            var unitOfWork = this._InventoryContractModificationRepository.UnitWork;

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    string InventoryContractModificationXML = ConvertInventoryContractModificationToXml(InventoryContractModification);
                    SP_SaveInventoryContractModification_Result result = this._InventoryContractModificationRepository.SP_SaveInventoryContractModification(InventoryContractModificationXML, audit.CodeUser);

                    if (result.CodeResult != 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.InventoryContractModification> { StateResult = false, Message = result.MessageResult };
                    }

                    InventoryContractModification.Id = result.Id.Value;
                    InventoryContractModification.Code = result.Code;
                    InventoryContractModification.MarkAsUnchanged();

                    scope.Complete();
                    return new ActionResult<Domain.Entities.InventoryContractModification> { StateResult = true, ObjectEmbbeded = InventoryContractModification, Message = result.MessageResult };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContractModification> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContractModification> { StateResult = false, Message = ex.Message };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryContractModification> { StateResult = false, Message = ex.Message };
                }
            }
        }

        #endregion

        #region Private Methods

        private string ConvertInventoryContractModificationToXml(Domain.Entities.InventoryContractModification InventoryContractModification)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<InventoryContractModification>");

            result.Append("<Id>" + InventoryContractModification.Id + "</Id>");
            result.Append("<OperatingUnitId>" + InventoryContractModification.OperatingUnitId + "</OperatingUnitId>");
            result.Append("<Code>" + InventoryContractModification.Code + "</Code>");
            result.Append("<DocumentDate>" + InventoryContractModification.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss") + "</DocumentDate>");
            result.Append("<ContractId>" + InventoryContractModification.ContractId + "</ContractId>");
            result.Append("<Description>" + InventoryContractModification.Description + "</Description>");
            result.Append("<ModificationType>" + InventoryContractModification.ModificationType + "</ModificationType>");
            result.Append("<EndDate>" + InventoryContractModification.EndDate?.ToString("dd/MM/yyyy HH:mm:ss") + "</EndDate>");
            result.Append("<Value>" + InventoryContractModification.Value.ToString().Replace(",", ".") + "</Value>");
            result.Append("<Status>" + InventoryContractModification.Status + "</Status>");
            result.Append("<BudgetaryValidityId>" + InventoryContractModification.BudgetaryValidityId + "</BudgetaryValidityId>");

            if (InventoryContractModification.InventoryContractModificationAvailability != null && InventoryContractModification.InventoryContractModificationAvailability.Count() > 0)
            {
                foreach (var item in InventoryContractModification.InventoryContractModificationAvailability)
                {
                    result.Append("<InventoryContractModificationAvailability>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<InventoryContractModificationId>" + item.InventoryContractModificationId + "</InventoryContractModificationId>");
                    result.Append("<AvailabilityDetailId>" + item.AvailabilityDetailId + "</AvailabilityDetailId>");
                    result.Append("<Value>" + item.Value.ToString().Replace(",", ".") + "</Value>");
                    result.Append("<IsDelete>" + ((item.ChangeTracker.State == ObjectState.Deleted) ? 1 : 0) + "</IsDelete>");
                    result.Append("</InventoryContractModificationAvailability>");
                }
            }

            if (InventoryContractModification.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("InventoryContractModificationAvailability"))
            {
                var listDelete = (from Domain.Entities.InventoryContractModificationAvailability e in InventoryContractModification.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "InventoryContractModificationAvailability").ToList()[0].Value select e).ToList();

                foreach (var item in listDelete)
                {
                    result.Append("<InventoryContractModificationAvailability>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<InventoryContractModificationId>" + item.InventoryContractModificationId + "</InventoryContractModificationId>");
                    result.Append("<AvailabilityDetailId>" + item.AvailabilityDetailId + "</AvailabilityDetailId>");
                    result.Append("<Value>" + item.Value + "</Value>");
                    result.Append("<IsDelete>" + 1 + "</IsDelete>");
                    result.Append("</InventoryContractModificationAvailability>");
                }
            }

            result.Append("</InventoryContractModification>");

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

                _InventoryContractModificationRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion
    }
}
