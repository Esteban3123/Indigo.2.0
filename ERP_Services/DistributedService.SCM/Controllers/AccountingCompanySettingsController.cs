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
    [RoutePrefix("accounting/companySettings")]
    public class AccountingCompanySettingsController : ApiController
    {
        [Route("getData")]
        [HttpGet]
        // GET: GetCompanySettings
        public RequestResponse<CompanySettings> GetCompanySettings()
        {
            var result = new RequestResponse<CompanySettings>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var accountPayableConceptsService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IAccountingCompanySettings>();
                var accountPayableConcepts = accountPayableConceptsService.GetCompanySettings();

                result.Status = true;
                result.Data = accountPayableConcepts;
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
