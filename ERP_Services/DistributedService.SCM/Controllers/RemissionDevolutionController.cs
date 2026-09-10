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
    [RoutePrefix("inventory/remissionDevolution")]
    public class RemissionDevolutionController : ApiController
    {
        [Route("saveAndConfirm")]
        [HttpPost]
        public async Task<RequestResponse<RemissionDevolution>> SaveAndConfirmbRemissionDevolution()
        {
            var result = new RequestResponse<RemissionDevolution>();
            try
            {
                RemissionDevolution remissionDevolution;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    remissionDevolution = Utils.DeserializeJsonToEntity<RemissionDevolution>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(remissionDevolution.Code, "329", remissionDevolution.WarehouseId, remissionDevolution.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceRemissionDevolution>();
                var response = await service.SaveAndConfirmbRemissionDevolutionAsync(remissionDevolution, audit, idCurrentSequense, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert, false);
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
