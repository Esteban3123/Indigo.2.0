using Application.Inventory.PharmaceuticalDispensingDetailBatchSerial;
using DistributedServices.Inventory.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        public List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialDevolution(string admissionNumber, string functionalUnitCode, string productCode, int productType, int userId, string batchCode = "")
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDetailBatchSerialAdminService>())
            {
                return service.ListPharmaceuticalDispensingDetailBatchSerialDevolution(admissionNumber, functionalUnitCode, productCode, productType, userId, batchCode);
            }
        }

        public List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(string admissionNumber)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDetailBatchSerialAdminService>())
            {
                return service.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber);
            }
            //return _pharmaceuticalDispensingDetailBatchSerialAdminService.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber);
        }
        public Domain.Entities.PharmaceuticalDispensingDetailBatchSerial GetPharmaceuticalDispensingDetailBatchSerialById(int id)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDetailBatchSerialAdminService>())
            {
                return service.GetPharmaceuticalDispensingDetailBatchSerialById(id);
            }
            //return _pharmaceuticalDispensingDetailBatchSerialAdminService.GetPharmaceuticalDispensingDetailBatchSerialById(id);
        }

        public Domain.Base.Entities.ActionResult<List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>> GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(string admissionNumber, string productCode, string batchCode, int userId)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDetailBatchSerialAdminService>())
            {
                return service.GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(admissionNumber, productCode, batchCode, userId);
            }
            //return _pharmaceuticalDispensingDetailBatchSerialAdminService.GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(admissionNumber, productCode, batchCode, userId);
        }

        public List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(int pharmaceuticalDispensingId)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalDispensingDetailBatchSerialAdminService>())
            {
                return service.GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(pharmaceuticalDispensingId);
            }
            //return _pharmaceuticalDispensingDetailBatchSerialAdminService.GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(pharmaceuticalDispensingId);
        }
    }
}
