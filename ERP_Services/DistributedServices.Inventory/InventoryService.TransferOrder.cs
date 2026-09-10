using Application.Inventory.TransferOrder;
using DistributedServices.Inventory.Unity;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {

        /// <summary>
        /// obtiene una orden de traslado por id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.TransferOrder GetTransferOrderById(int id)
        {
            using (var service = Container.Current.Resolve<ITransferOrderAdminService>())
            {
                return service.GetTransferOrderById(id);
            }
        }

        /// <summary>
        /// obtiene una orden de traslado por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.TransferOrder GetTranferOrderByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ITransferOrderAdminService>())
            {                
                return service.GetTranferOrderByCode(code, audit);
            }
        }
        
        /// <summary>
        /// guarda unan orden de traslado
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.TransferOrder> SaveTrasnferOrder(Domain.Entities.TransferOrder transferOrder, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<ITransferOrderAdminService>())
            {                
                return service.SaveTrasnferOrder(transferOrder, audit);
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.ViewListRequestDetailImport>> ChangeStateInventoryRequestDetail(List<ViewListRequestDetailImport> data)
        {
            using (var service = Container.Current.Resolve<ITransferOrderAdminService>())
            {
                return service.ChangeStateInventoryRequestDetail(data);
            }   
        }


    }
}
