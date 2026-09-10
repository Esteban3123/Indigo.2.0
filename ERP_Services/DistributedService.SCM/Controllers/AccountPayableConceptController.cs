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
    [RoutePrefix("payments/accountPayableConcept")]
    public class AccountPayableConceptController : ApiController
    {
        [Route("findById")]
        [HttpGet]
        // GET: PaymentConceptById
        public RequestResponse<AccountPayableConcepts> GetPaymentConceptById(int id)
        {
            var result = new RequestResponse<AccountPayableConcepts>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage() {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var accountPayableConceptsService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IPaymentsPaymentsConcept>();
                var accountPayableConcepts = accountPayableConceptsService.GetPaymentConceptById(id, audit);

                result.Status = accountPayableConcepts.StateResult;
                result.Data = accountPayableConcepts.ObjectEmbbeded;
                result.Message = accountPayableConcepts.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: "+ ex.Message;
                return result;
            }
        }
    }
}
