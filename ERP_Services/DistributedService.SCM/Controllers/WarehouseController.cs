using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/warehouse")]
    public class WarehouseController : ApiController
    {
        [Route("findByCode")]
        [HttpGet]
        // GET: Warehouse
        public RequestResponse<Warehouse> GetWarehouse(String code)
        {
            var result = new RequestResponse<Warehouse>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var warehouseService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryWarehouse>();
                var warehouse = warehouseService.GetWarehouse(code, audit).ObjectEmbbeded;
                if (warehouse.Id == 0 || warehouse.Status == false)
                {
                    throw new Exception(String.Format("El almacén con código {0} no existe.", code));
                }
                result.Status = true;
                result.Data = warehouse;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("findSupplierByType")]
        [HttpGet]
        // GET: Warehouse
        public RequestResponse<Warehouse> GetWarehouseSupplierByType(int supplierId, int type)
        {
            var result = new RequestResponse<Warehouse>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var warehouseService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryWarehouse>();
                var warehouse = warehouseService.GetWarehouseSupplierByType(supplierId, type, audit).ObjectEmbbeded;
                if (warehouse.Id == 0 || warehouse.Status == false)
                {
                    throw new Exception("El almacén del proveedor no existe.");
                }
                result.Status = true;
                result.Data = warehouse;
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
