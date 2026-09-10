using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServicePharmaceuticalDispensingDevolutionDetail
    {
        /// <summary>
        /// lista los detalles de la devolucion
        /// </summary>
        /// <param name="IdPharmaceuticalDispensingDevolution"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PharmaceuticalDispensingDevolutionDetail> GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(int IdPharmaceuticalDispensingDevolution);
    }
}
