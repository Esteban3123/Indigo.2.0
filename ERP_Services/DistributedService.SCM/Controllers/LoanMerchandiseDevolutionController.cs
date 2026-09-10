using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System.IO;
using System.Web;
using System;
using System.Web.Http;
using Unity;
using System.Threading.Tasks;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/loanMerchandiseDevolution")]
    public class LoanMerchandiseDevolutionController : ApiController
    {
        [Route("saveAndConfirm")]
        [HttpPost]
        public async Task<RequestResponse<string>> SaveAndConfirmLoadMerchadiseDevolution()
        {
            var result = new RequestResponse<String>();
            try
            {
                LoanMerchandiseDevolution loanMerchandiseDevolution;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    loanMerchandiseDevolution = Utils.DeserializeJsonToEntity<LoanMerchandiseDevolution>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(loanMerchandiseDevolution.Code, "1518", loanMerchandiseDevolution.WarehouseId, loanMerchandiseDevolution.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceLoanMerchandiseDevolution>();
                var response = await service.SaveAndConfirmLoadMerchadiseDevolution(loanMerchandiseDevolution, idCurrentSequense, audit, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert);
                result.Status = response.StateResult;
                result.Message = response.Message;
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
