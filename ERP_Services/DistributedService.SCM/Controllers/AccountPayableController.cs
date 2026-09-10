using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Payments;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("payments/accountPayable")]
    public class AccountPayableController : ApiController
    {
        [Route("findById")]
        [HttpGet]
        // GET: AccountPayableById
        public RequestResponse<AccountPayable> GetAccountPayableById(int id)
        {
            var result = new RequestResponse<AccountPayable>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var accountPayableService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IPaymentsAccountPayable>();
                var accountPayable = accountPayableService.GetAccountPayableById(id, audit);

                result.Status = true;
                result.Data = accountPayable;
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
