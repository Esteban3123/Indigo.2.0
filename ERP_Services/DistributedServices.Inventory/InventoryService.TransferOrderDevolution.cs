using Application.Inventory.TransferOrderDevolution;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {

        /// <summary>
        /// Gets the transfer order devolution by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ITransferOrderDevolutionAdminService>())
            {
                return service.GetTransferOrderDevolutionById(id, audit);
            }
        }

        /// <summary>
        /// Gets the transfer order devolution by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ITransferOrderDevolutionAdminService>())
            {               
                return service.GetTransferOrderDevolutionByCode(code, audit);
            }
        }

        /// <summary>
        /// Saves the transfer order devolution.
        /// </summary>
        /// <param name="transferOrderDevolution">The transfer order devolution.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.TransferOrderDevolution> SaveTransferOrderDevolution(Domain.Entities.TransferOrderDevolution transferOrderDevolution, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ITransferOrderDevolutionAdminService>())
            {                
                return service.SaveTransferOrderDevolution(transferOrderDevolution, audit);
            }
        }

    }
}
