using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/physicalInventory")]
    public class PhysicalInventoryController : ApiController
    {
        [Route("listPhysicalInventory")]
        [HttpGet]
        // GET: List<PhysicalInventory>
        public RequestResponse<List<PhysicalInventory>> GetListPhysicalInventory(int productId, int warehouseId)
        {
            var result = new RequestResponse<List<PhysicalInventory>>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var physicalInventoryService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServicePhysicalInventory>();
                var physicalInventory = physicalInventoryService.GetListPhysicalInventory(productId, warehouseId);

                result.Status = true;
                result.Data = physicalInventory;
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
