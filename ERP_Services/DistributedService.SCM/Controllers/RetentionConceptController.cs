using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Accounting;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("payments/retentionConcept")]
    public class RetentionConceptController : ApiController
    {
        [Route("findById")]
        [HttpGet]
        // GET: GeneralLedgerIVA
        public RequestResponse<RetentionConcepts> GetPaymentConceptById(int id)
        {
            var result = new RequestResponse<RetentionConcepts>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var retentionConceptService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IAccountingRetentionConcept>();
                var retentionConcept = retentionConceptService.GetRetentionById(id, audit);
                if (retentionConcept.Id == 0)
                {
                    throw new Exception(String.Format("El concepto de retención con id {0} no existe.", id));
                }
                result.Status = true;
                result.Data = retentionConcept;
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
