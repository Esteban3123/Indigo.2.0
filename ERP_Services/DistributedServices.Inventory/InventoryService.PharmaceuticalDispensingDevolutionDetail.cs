using Application.Inventory.PharmaceuticalDispensingDevolutionDetail;
using DistributedServices.Inventory.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        public List<Domain.Entities.PharmaceuticalDispensingDevolutionDetail> GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(int IdPharmaceuticalDispensingDevolution)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDevolutionDetailAdminService>())
            {
                return service.GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(IdPharmaceuticalDispensingDevolution);
            }
            //return _pharmaceuticalDispensingDevolutionDetailAdminService.GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(IdPharmaceuticalDispensingDevolution);
        }
    }
}
