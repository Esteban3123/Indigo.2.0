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
    [RoutePrefix("inventory/entranceVoucherDevolution")]
    public class EntranceVoucherDevolutionController : ApiController
    {
        [Route("saveAndConfirm")]
        [HttpPost]
        public RequestResponse<string> SaveAndConfirmbEntranceVoucherDevolution()
        {
            var result = new RequestResponse<String>();
            try
            {
                EntranceVoucherDevolution entranceVoucherDevolution;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    entranceVoucherDevolution = Utils.DeserializeJsonToEntity<EntranceVoucherDevolution>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(entranceVoucherDevolution.Code, "1403", entranceVoucherDevolution.WarehouseId, entranceVoucherDevolution.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryEntranceVoucherDevolution>();
                var response = service.SaveAndConfirmbEntranceVoucherDevolution(entranceVoucherDevolution, audit, idCurrentSequense, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert);
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
