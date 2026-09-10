using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/transferOrder")]
    public class TransferOrderController : ApiController
    {
        [Route("save")]
        [HttpPost]
        public RequestResponse<string> SaveTransferOrder()
        {
            var result = new RequestResponse<string>();
            try
            {
                TransferOrder transferOrder;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    transferOrder = Utils.DeserializeJsonToEntity<TransferOrder>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryTrasnferOrder>();
                var response = service.SaveTrasnferOrder(transferOrder, audit);
                result.Status = response.StateResult;
                result.Message = (result.Status) ? String.Format("El registro se guardó con código {0}", response.ObjectEmbbeded.Code) : response.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("getTranferOrderByCode")]
        [HttpGet]
        // GET: GetTranferOrderByCode
        public RequestResponse<TransferOrder> GetTranferOrderByCode(string code)
        {
            var result = new RequestResponse<TransferOrder>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var transferOrderService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryTrasnferOrder>();
                var transferOrder = transferOrderService.GetTranferOrderByCode(code, audit);

                if (transferOrder.Id == 0)
                {
                    throw new Exception(String.Format("La Orden de Traslado con código {0} no existe.", code));
                }
                result.Status = true;
                result.Data = transferOrder;
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
