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
    [RoutePrefix("inventory/remissionEntrance")]
    public class RemissionEntranceController : ApiController
    {
        [Route("save")]
        [HttpPost]
        public RequestResponse<string> SaveRemissionEntrance()
        {
            var result = new RequestResponse<String>();
            try
            {
                RemissionEntrance remissionEntrance;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    remissionEntrance = Utils.DeserializeJsonToEntity<RemissionEntrance>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(remissionEntrance.Code, "317", remissionEntrance.WarehouseId, remissionEntrance.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceRemissionEntrance>();
                var response = service.SaveRemissionEntrance(remissionEntrance, audit, idCurrentSequense, inventorySequense);
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
        public RequestResponse<string> SaveAndConfirmbRemissionEntrance()
        {
            var result = new RequestResponse<String>();
            try
            {
                RemissionEntrance remissionEntrance;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    remissionEntrance = Utils.DeserializeJsonToEntity<RemissionEntrance>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(remissionEntrance.Code, "317", remissionEntrance.WarehouseId, remissionEntrance.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceRemissionEntrance>();
                var response = service.SaveAndConfirmbRemissionEntrance(remissionEntrance, idCurrentSequense, audit, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert, false);
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
        // GET: getRemissionEntranceByCode
        public RequestResponse<RemissionEntrance> GetRemissionEntranceByCode(string code)
        {
            var result = new RequestResponse<RemissionEntrance>();
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

                var remissionEntranceService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceRemissionEntrance>();
                var listByRemissionEntranceCode = remissionEntranceService.GetRemissionEntranceByCode(code, audit);
                if (listByRemissionEntranceCode.Id == 0)
                {
                    throw new Exception(String.Format("La remisión de entrada con codigo {0} no existe.", code));
                }
                result.Status = true;
                result.Data = listByRemissionEntranceCode;
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
