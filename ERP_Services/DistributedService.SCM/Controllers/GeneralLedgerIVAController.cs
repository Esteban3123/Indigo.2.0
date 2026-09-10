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
    [RoutePrefix("accounting/generalLedgerIVA")]
    public class GeneralLedgerIVAController : ApiController
    {
        [Route("findById")]
        [HttpGet]
        // GET: GeneralLedgerIVA
        public RequestResponse<GeneralLedgerIVA> GetGeneralLedgerIVAById(int id)
        {
            var result = new RequestResponse<GeneralLedgerIVA>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var generalLedgerIVAService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IAccountingGeneralLedgerIVA>();
                var generalLedgerIVA = generalLedgerIVAService.GetGeneralLedgerIVAById(id, audit);

                result.Status = generalLedgerIVA.StateResult;
                result.Data = generalLedgerIVA.ObjectEmbbeded;
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
