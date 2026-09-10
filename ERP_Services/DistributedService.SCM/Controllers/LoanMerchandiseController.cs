using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/loanMerchandise")]
    public class LoanMerchandiseController : ApiController
    {
        [Route("save")]
        [HttpPost]
        public RequestResponse<string> SaveLoanMerchadise()
        {
            var result = new RequestResponse<String>();
            try
            {
                LoanMerchandise loanMerchandise;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    loanMerchandise = Utils.DeserializeJsonToEntity<LoanMerchandise>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(loanMerchandise.Code, "1514", loanMerchandise.WarehouseId, loanMerchandise.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceLoanMerchandise>();
                var response = service.SaveLoanMerchadise(loanMerchandise, audit, idCurrentSequense, inventorySequense);
                result.Status = response.StateResult;
                result.Message = (result.Status) ? String.Format("El registro se guardó con código {0}", response.ObjectEmbbeded.Code) : response.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("saveAndConfirm")]
        [HttpPost]
        public RequestResponse<string> SaveAndConfirmLoadMerchadise()
        {
            var result = new RequestResponse<String>();
            try
            {
                LoanMerchandise loanMerchandise;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    loanMerchandise = Utils.DeserializeJsonToEntity<LoanMerchandise>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(loanMerchandise.Code, "1514", loanMerchandise.WarehouseId, loanMerchandise.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceLoanMerchandise>();
                var response = service.SaveAndConfirmLoadMerchadise(loanMerchandise, idCurrentSequense, audit, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert);
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

        [Route("findByCode")]
        [HttpGet]
        // GET: getLoanMerchandiseByCode
        public RequestResponse<LoanMerchandise> GetLoanMerchandiseByCode(string code)
        {
            var result = new RequestResponse<LoanMerchandise>();
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

                var remissionEntranceService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceLoanMerchandise>();
                var listByLoanMerchandiseCode = remissionEntranceService.GetLoanMerchadiseByCode(code, audit);
                if (listByLoanMerchandiseCode.Id == 0)
                {
                    throw new Exception(String.Format("El préstamo con codigo {0} no existe.", code));
                }
                result.Status = true;
                result.Data = listByLoanMerchandiseCode;
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
