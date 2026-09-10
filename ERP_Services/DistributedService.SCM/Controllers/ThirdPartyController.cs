using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Common;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("common/thirdParty")]
    public class ThirdPartyController : ApiController
    {
        [Route("findByNit")]
        [HttpGet]
        // GET: ThirdParty
        public RequestResponse<ThirdParty> GetThirdParty(String nit)
        {
            var result = new RequestResponse<ThirdParty>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var thirdPartyService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICommonERPThirdParty>();
                var thirdParty = thirdPartyService.GetThirdPartyByNit(nit, SessionValues.Instance);
                if (thirdParty.Id == 0)
                {
                    throw new Exception(String.Format("El tercero con nit {0} no existe.", nit));
                }
                result.Status = true;
                result.Data = thirdParty;
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
