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
    [RoutePrefix("inventory/remissionEntranceDetailBatchSerial")]
    public class RemissionEntranceDetailBatchSerialController : ApiController
    {
        [Route("listByRemissionEntranceCode")]
        [HttpGet]
        // GET: listByRemissionEntranceCode
        public RequestResponse<List<RemissionEntranceDetailBatchSerial>> ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(string code)
        {
            var result = new RequestResponse<List<RemissionEntranceDetailBatchSerial>>();
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

                var listByRemissionEntranceCodeService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceRemissionEntranceDetailBatchSerial>();
                var listByRemissionEntranceCode = listByRemissionEntranceCodeService.ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(code);
                if (listByRemissionEntranceCode.Count == 0)
                {
                    throw new Exception(String.Format("El código {0} no contiene productos para legalizar.", code));
                }
                result.Status = true;
                result.Data = listByRemissionEntranceCode;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("listByRemissionEntranceId")]
        [HttpGet]
        // GET: listByRemissionEntranceId
        public RequestResponse<List<RemissionEntranceDetailBatchSerial>> ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(int id)
        {
            var result = new RequestResponse<List<RemissionEntranceDetailBatchSerial>>();
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

                var listByRemissionEntranceCodeService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceRemissionEntranceDetailBatchSerial>();
                var listByRemissionEntranceCode = listByRemissionEntranceCodeService.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(id);
                if (listByRemissionEntranceCode.Count == 0)
                {
                    throw new Exception("No existen productos a legalizar en la remisión de entrada.");
                }
                result.Status = true;
                result.Data = listByRemissionEntranceCode;
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
