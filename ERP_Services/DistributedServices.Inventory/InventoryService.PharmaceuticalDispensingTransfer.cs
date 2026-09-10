using Application.Inventory.PharmaceuticalDispensingTransfer;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {

        /// <summary>
        /// obtiene una traslado de dispensacion por id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferById(int id)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingTransferAdminService>())
            {
                return service.GetPharmaceuticalDispensingTransferById(id);
            }
        }

        /// <summary>
        /// obtiene un traslado de dispensacion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingTransferAdminService>())
            {                
                return service.GetPharmaceuticalDispensingTransferByCode(code, audit);
            }
        }

        /// <summary>
        /// guarda, actualiza y confirma un traslado de dispensacion
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> SavePharmaceuticalDispensingTransfer(Domain.Entities.PharmaceuticalDispensingTransfer pharmaceuticalDispensingTransfer, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingTransferAdminService>())
            {                
                return service.SavePharmaceuticalDispensingTransfer(pharmaceuticalDispensingTransfer, audit);
            }
        }

    }
}
