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
    [RoutePrefix("inventory/loanMerchandiseDetail")]
    public class LoanMerchandiseDetailController : ApiController
    {
        [Route("listByLoanMerchandiseId")]
        [HttpGet]
        // GET: listLoanMerchandiseDetail
        public RequestResponse<List<LoanMerchandiseDetail>> ListLoanMerchandiseDetailByIdLoanMerchandise(int idLoanMerchandise, bool isDevolution)
        {
            var result = new RequestResponse<List<LoanMerchandiseDetail>>();
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

                var listLoanMerchandiseDetailService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceLoanMerchandiseDetail>();
                var listLoanMerchandiseDetail = listLoanMerchandiseDetailService.ListLoanMerchandiseDetailByIdLoanMerchandise(idLoanMerchandise, isDevolution);
                if (listLoanMerchandiseDetail.Count == 0)
                {
                    throw new Exception("No existen productos para devolver.");
                }
                result.Status = true;
                result.Data = listLoanMerchandiseDetail;
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
