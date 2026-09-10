using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System.Collections.Generic;
using System;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/transferOrderDetailBatchSerial")]
    public class TransferOrderDetailBatchSerialController : ApiController
    {
        [Route("listTransferOrderDetailBatchSerialByTransferOrderId")]
        [HttpGet]
        // GET: ListTransferOrderDetailBatchSerialByTransferOrderId
        public RequestResponse<List<TransferOrderDetailBatchSerial>> ListTransferOrderDetailBatchSerialByTransferOrderId(int transferOrderId, bool flagQuantiyZero)
        {
            var result = new RequestResponse<List<TransferOrderDetailBatchSerial>>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var transferOrderService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryTrasnferOrderDetailBatchSerial>();
                var listTransferOrderDetai = transferOrderService.ListTransferOrderDetailBatchSerialByTransferOrderId(transferOrderId, flagQuantiyZero);

                if (listTransferOrderDetai.Count == 0)
                {
                    result.Status = false;
                    result.Message = String.Format("No existen productos a devolver.");
                    return result;
                }
                result.Status = true;
                result.Data = listTransferOrderDetai;
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
