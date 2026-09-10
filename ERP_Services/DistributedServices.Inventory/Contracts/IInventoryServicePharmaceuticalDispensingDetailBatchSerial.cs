using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServicePharmaceuticalDispensingDetailBatchSerial
    {
        /// <summary>
        /// obtiene los productos para la devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="productCode"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialDevolution(string admissionNumber, string functionalUnitCode, string productCode, int productType, int userId, string batchCode = "");

        /// <summary>
        /// lista los detalle de las dispensacion para hacer devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(string admissionNumber);

        /// <summary>
        /// obtiene un detalla por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PharmaceuticalDispensingDetailBatchSerial GetPharmaceuticalDispensingDetailBatchSerialById(int id);

        /// <summary>
        /// obtiene los detalle para hacer la devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Base.Entities.ActionResult<List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>> GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(string admissionNumber, string productCode, string batchCode, int userId);
    }
}
