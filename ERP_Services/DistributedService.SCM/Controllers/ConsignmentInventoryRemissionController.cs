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
    [RoutePrefix("inventory/consignmentInventoryRemission")]
    public class ConsignmentInventoryRemissionController : ApiController
    {
        [Route("save")]
        [HttpPost]
        public RequestResponse<string> SaveConsignmentInventoryRemission()
        {
            var result = new RequestResponse<String>();
            try
            {
                ConsignmentInventoryRemission consignmentInventoryRemission;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    consignmentInventoryRemission = Utils.DeserializeJsonToEntity<ConsignmentInventoryRemission>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };
                
                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(consignmentInventoryRemission.Code, "1977", consignmentInventoryRemission.WarehouseId, consignmentInventoryRemission.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceConsignmentInventoryRemission>();
                var response = service.SaveConsignmentInventoryRemission(consignmentInventoryRemission, idCurrentSequense, audit, inventorySequense);
                result.Status = response.StateResult;
                result.Message = (result.Status) ? String.Format("El registro se guardó con código {0}", response.ObjectEmbbeded.Code) : response.Message;
                return result;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }

        }

        [Route("saveAndConfirm")]
        [HttpPost]
        public RequestResponse<string> SaveAndConfirmbConsignmentInventoryRemission()
        {
            var result = new RequestResponse<String>();
            try
            {
                ConsignmentInventoryRemission consignmentInventoryRemission;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    consignmentInventoryRemission = Utils.DeserializeJsonToEntity<ConsignmentInventoryRemission>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(consignmentInventoryRemission.Code, "1977", consignmentInventoryRemission.WarehouseId, consignmentInventoryRemission.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceConsignmentInventoryRemission>();
                var response = service.SaveAndConfirmbConsignmentInventoryRemission(consignmentInventoryRemission, idCurrentSequense, audit, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert, false);
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
        // GET: getConsignmentInventoryRemissionByCode
        public RequestResponse<ConsignmentInventoryRemission> GetConsignmentInventoryRemissionByCode(string code)
        {
            var result = new RequestResponse<ConsignmentInventoryRemission>();
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

                var remissionEntranceService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceConsignmentInventoryRemission>();
                var listByConsignmentInventoryRemissionCode = remissionEntranceService.GetConsignmentInventoryRemissionByCode(code, audit);
                if (listByConsignmentInventoryRemissionCode.Id == 0)
                {
                    throw new Exception(String.Format("La remisión de inventario en consignación con codigo {0} no existe.", code));
                }
                result.Status = true;
                result.Data = listByConsignmentInventoryRemissionCode;
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
