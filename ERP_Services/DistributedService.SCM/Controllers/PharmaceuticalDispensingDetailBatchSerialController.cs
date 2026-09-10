using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/pharmaceuticalDispensingDetailBatchSerial")]
    public class PharmaceuticalDispensingDetailBatchSerialController : ApiController
    {
        [Route("listPharmaceuticalDispensingDetailBatchSerialDevolution")]
        [HttpGet]
        // GET: listPharmaceuticalDispensingDetailBatchSerialDevolution
        public RequestResponse<List<PharmaceuticalDispensingDetailBatchSerial>> ListPharmaceuticalDispensingDetailBatchSerialDevolution(string admissionNumber, string functionalUnitCode, string productCode, int productType, int userId, string batchCode = "")
        {
            var result = new RequestResponse<List<PharmaceuticalDispensingDetailBatchSerial>>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };
                SessionValues.Instance.AuditMessageWcf = audit;

                var listDevolutionService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryService>();
                var listDevolution = listDevolutionService.ListPharmaceuticalDispensingDetailBatchSerialDevolution(admissionNumber, functionalUnitCode, productCode, productType, userId, batchCode);
                if (listDevolution.Count == 0)
                {
                    throw new Exception(String.Format("No existen productos a devolver para el ingreso número {0}.", admissionNumber));
                }
                result.Status = true;
                result.Data = listDevolution;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }
    }
}
