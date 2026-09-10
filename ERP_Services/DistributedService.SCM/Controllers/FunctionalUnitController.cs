using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Payroll;
using Domain.Base.Entities;
using Domain.Payroll.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("payroll/functionaUnit")]
    public class FunctionalUnitController : ApiController
    {
        [Route("findByCode")]
        [HttpGet]
        // GET: ProductWithProducGroup
        public RequestResponse<FunctionalUnit> GetFunctionalUnit(string code)
        {
            var result = new RequestResponse<FunctionalUnit>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var functionalUnitService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IPayrollFunctionalUnit>();
                var functionalUnit = functionalUnitService.GetFunctionalUnit(code, SessionValues.Instance);
                if (functionalUnit.Id == 0)
                {
                    throw new Exception(String.Format("La unidad funcional con código {0} no existe.", code));
                }
                result.Status = true;
                result.Data = functionalUnit;
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
