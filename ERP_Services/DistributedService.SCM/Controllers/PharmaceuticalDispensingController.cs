using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Crystal.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/pharmaceuticalDispensing")]
    public class PharmaceuticalDispensingController : ApiController
    {
        [Route("saveDashboardPharmacy")]
        [HttpPost]
        public RequestResponse<string> SaveDashboardPharmacy()
        {
            var result = new RequestResponse<String>();
            try
            {
                List<PharmaceuticalDispensing> pharmaceuticalDispensing = null;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    pharmaceuticalDispensing = Utils.DeserializeJsonToEntity<List<PharmaceuticalDispensing>>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser"),
                    DispensingIntegration = 1
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(pharmaceuticalDispensing[0].Code, "322", pharmaceuticalDispensing[0].PharmaceuticalDispensingDetail[0].WarehouseId, pharmaceuticalDispensing[0].OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryPharmaceuticalDispensing>();
                var response = service.SaveDashboardPharmacy(pharmaceuticalDispensing, null, idCurrentSequense, audit);
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

        [Route("savePharmaceuticalDispensing")]
        [HttpPost]
        public RequestResponse<string> SavePharmaceuticalDispensing()
        {
            var result = new RequestResponse<String>();
            try
            {
                PharmaceuticalDispensing pharmaceuticalDispensing = null;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    pharmaceuticalDispensing = Utils.DeserializeJsonToEntity<PharmaceuticalDispensing>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser"),
                    DispensingIntegration = 1
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(pharmaceuticalDispensing.Code, "322", pharmaceuticalDispensing.PharmaceuticalDispensingDetail[0].WarehouseId, pharmaceuticalDispensing.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryPharmaceuticalDispensing>();
                var response = service.SavePharmaceuticalDispensing(pharmaceuticalDispensing, true, idCurrentSequense, audit, true);
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

        [Route("saveDashboardPharmacySurgicalPackage")]
        [HttpPost]
        public RequestResponse<string> SaveDashboardPharmacySurgicalPackage()
        {
            var result = new RequestResponse<String>();
            try
            {
                List<PharmaceuticalDispensing> pharmaceuticalDispensing = null;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    pharmaceuticalDispensing = Utils.DeserializeJsonToEntity<List<PharmaceuticalDispensing>>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser"),
                    DispensingIntegration = 1
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(pharmaceuticalDispensing[0].Code, "322", pharmaceuticalDispensing[0].PharmaceuticalDispensingDetail[0].WarehouseId, pharmaceuticalDispensing[0].OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryPharmaceuticalDispensing>();
                var listDetailAnnular = new List<ViewDashBoardPharmacy_SurgicalPackageDeatils>();
                var response = service.SaveDashboardPharmacySurgicalPackage(pharmaceuticalDispensing, listDetailAnnular, idCurrentSequense, audit);
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
