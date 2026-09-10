using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Crystal;
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
    [RoutePrefix("inventory/pharmaceuticalDispensingDevolution")]
    public class PharmaceuticalDispensingDevolutionController : ApiController
    {
        [Route("saveDashboardPharmacyDevolution")]
        [HttpPost]
        public RequestResponse<string> SaveDashboardPharmacyDevolution()
        {
            var result = new RequestResponse<String>();
            try
            {
                List<PharmaceuticalDispensingDevolution> pharmaceuticalDispensingDevolution = null;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    pharmaceuticalDispensingDevolution = Utils.DeserializeJsonToEntity<List<PharmaceuticalDispensingDevolution>>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser"),
                    DispensingIntegration = 1
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(pharmaceuticalDispensingDevolution[0].Code, "1516", pharmaceuticalDispensingDevolution[0].WarehouseId, pharmaceuticalDispensingDevolution[0].OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServicePharmaceuticalDispensingDevolution>();
                var listDetailAnnular = new List<ViewDashboardPharmacyDetailDevolution>();
                var response = service.SaveDashboardPharmacyDevolution(pharmaceuticalDispensingDevolution, listDetailAnnular, idCurrentSequense, audit);
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

        [Route("saveAndConfirmPharmaceuticalDispensingDevolution")]
        [HttpPost]
        public RequestResponse<string> SaveAndConfirmPharmaceuticalDispensingDevolution()
        {
            var result = new RequestResponse<String>();
            try
            {
                PharmaceuticalDispensingDevolution pharmaceuticalDispensingDevolution = null;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    pharmaceuticalDispensingDevolution = Utils.DeserializeJsonToEntity<PharmaceuticalDispensingDevolution>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser"),
                    DispensingIntegration = 1
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(pharmaceuticalDispensingDevolution.Code, "1516", pharmaceuticalDispensingDevolution.WarehouseId, pharmaceuticalDispensingDevolution.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServicePharmaceuticalDispensingDevolution>();
                var response = service.SaveAndConfirmPharmaceuticalDispensingDevolution(pharmaceuticalDispensingDevolution, idCurrentSequense, audit, sequense.Item2);
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
