using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/product")]
    public class ProductController : ApiController
    {
        [Route("findByCode")]
        [HttpGet]
        // GET: ProductWithProducGroup
        public RequestResponse<InventoryProduct> GetInventoryProductByCodeWithProducGroup(string code)
        {
            var result = new RequestResponse<InventoryProduct>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var productService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryService>();
                var product = productService.GetInventoryProductByCodeWithProducGroup(code);
                if (product.Id == 0)
                {
                    throw new Exception(String.Format("El producto con código {0} no existe.", code));
                }
                result.Status = true;
                result.Data = product;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("findById")]
        [HttpGet]
        // GET: ProductByIdSimple
        public RequestResponse<InventoryProduct> GetInventoryProductByIdSimple(int id)
        {
            var result = new RequestResponse<InventoryProduct>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var productService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryService>();
                var product = productService.GetInventoryProductByIdSimple(id);
                if (product.Id == 0)
                {
                    throw new Exception(String.Format("El producto con id {0} no existe.", id));
                }
                result.Status = true;
                result.Data = product;
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
