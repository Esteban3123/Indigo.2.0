using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/inventoryAdjustment")]
    public class InventoryAdjustmentController : ApiController
    {
        [Route("save")]
        [HttpPost]
        public async Task<RequestResponse<string>> SaveInventoryAdjustment()
        {
            var result = new RequestResponse<String>();
            try
            {
                InventoryAdjustment inventoryAdjustment;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    inventoryAdjustment = Utils.DeserializeJsonToEntity<InventoryAdjustment>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(inventoryAdjustment.Code, "318", Convert.ToInt32(inventoryAdjustment.WarehouseId), inventoryAdjustment.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceInventoryAdjustment>();
                var response = await service.SaveInventoryAdjustment(inventoryAdjustment, idCurrentSequense, audit, inventorySequense);
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
        public async Task<RequestResponse<string>> SaveAndConfirmInventoryAdjustment()
        {
            var result = new RequestResponse<String>();
            try
            {
                InventoryAdjustment inventoryAdjustment;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    inventoryAdjustment = Utils.DeserializeJsonToEntity<InventoryAdjustment>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(inventoryAdjustment.Code, "318", Convert.ToInt32(inventoryAdjustment.WarehouseId), inventoryAdjustment.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceInventoryAdjustment>();
                var response = await service.SaveAndConfirmbInventoryAdjustment(inventoryAdjustment, inventoryAdjustment.OperatingUnitId, audit, idCurrentSequense, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert);
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
