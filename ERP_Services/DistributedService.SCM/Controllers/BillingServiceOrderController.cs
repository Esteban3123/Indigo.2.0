using DistribuitedServices.Billing;
using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("billing/serviceOrder")]
    public class BillingServiceOrderController : ApiController
    {
        [Route("getAdmissionByServiceOrder")]
        [HttpGet]
        // GET: listPharmaceuticalDispensingDetailBatchSerialDevolution
        public RequestResponse<object> GetAdmissionByServiceOrder(string admissionNumber)
        {
            var result = new RequestResponse<object>();
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

                var listDevolutionService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IBillingServiceServiceOrder>();
                var listDevolution = listDevolutionService.GetAdmissionByServiceOrder(admissionNumber);
                if (string.IsNullOrEmpty(listDevolution))
                {
                    throw new Exception(String.Format("El ingreso número {0} no existe.", admissionNumber));
                }
                result.Status = true;
                result.Data = Utils.DeserializeJsonToObject(listDevolution);
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
