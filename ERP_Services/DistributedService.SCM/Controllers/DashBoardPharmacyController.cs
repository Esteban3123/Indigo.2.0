using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Base;
using DistributedServices.Crystal;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Crystal;
using Domain.Crystal.Entities;
using Domain.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/dashBoardPharmacy")]
    public class DashBoardPharmacyController : ApiController
    {
        [Route("pharmacyByConsecutive")]
        [HttpGet]
        // GET: HCFARMEPC
        public RequestResponse<HCFARMEPC> GetPharmacyByConsecutive(decimal consecutive)
        {
            var result = new RequestResponse<HCFARMEPC>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var dashboardPharmacyService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryDashBoardPharmacy>();
                var dashboardPharmacy = dashboardPharmacyService.PharmacyByConsecutive(consecutive);
                if (dashboardPharmacy.ObjectEmbbeded != null)
                {
                    if (dashboardPharmacy.ObjectEmbbeded.CODCONCEC == 0)
                    {
                        throw new Exception(String.Format("No existe una solicitud con el consecutivo {0}.", consecutive));
                    }
                }
                else
                {
                    throw new Exception(String.Format("No existe una solicitud con el consecutivo {0}.", consecutive));
                }
                    
                result.Status = dashboardPharmacy.StateResult;
                result.Data = dashboardPharmacy.ObjectEmbbeded;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("pharmacyDetail")]
        [HttpGet]
        // GET: ViewDashboardPharmacyDetail
        public RequestResponse<ViewDashboardPharmacyDetail> GetPharmacyDetail(int entityId)
        {
            var result = new RequestResponse<ViewDashboardPharmacyDetail>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var dashboardPharmacyDetailService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICrystalServiceDashboardPharmacyDetail>();
                var dashboardPharmacyDetail = dashboardPharmacyDetailService.DashboardPharmacyDetail(entityId);

                if (dashboardPharmacyDetail.EntityId == 0)
                {
                    result.Status = false;
                    result.Message = "No existe un detalle de farmacia disponible para dispensar.";
                    return result;
                }
                result.Status = true;
                result.Data = dashboardPharmacyDetail;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("getAdmissionInformation")]
        [HttpGet]
        // GET: AdmissionInformation
        public RequestResponse<AdmissionInformation> GetAdmissionInformation(int dispensingIntegration, int careGroupIdIntegrationMedilaser, string admissionNumber, string functionalUnitCode)
        {
            var result = new RequestResponse<AdmissionInformation>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var dashboardPharmacyDetailService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryDashBoardPharmacy>();
                var resultAdmissionInformation = dashboardPharmacyDetailService.GetAdmissionInformation(dispensingIntegration, careGroupIdIntegrationMedilaser, admissionNumber, functionalUnitCode, audit);

                result.Status = resultAdmissionInformation.StateResult;
                result.Message = (resultAdmissionInformation.StateResult == false) ? String.Join(", ", resultAdmissionInformation.MessageResult) : "";
                result.Data = resultAdmissionInformation.ObjectEmbbeded;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("listPhysicalInventoryByATCNumberWithAdditionalInformation")]
        [HttpGet]
        // GET: List<PhysicalInventory>
        public RequestResponse<List<PhysicalInventory>> ListPhysicalInventoryByATCNumberWithAdditionalInformation(string ATCNumber, int type, int userIndigoId, int careGroupId)
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
                var physicalInventory = physicalInventoryService.ListPhysicalInventoryByATCNumberWithAdditionalInformation(ATCNumber, type, userIndigoId, careGroupId);

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

        [Route("productRateDetailWithValue")]
        [HttpGet]
        // GET: ProductRateDetail
        public RequestResponse<ProductRateDetail> GetProductRateDetailWithValue(int CareGroupId, int ProductId)
        {
            var result = new RequestResponse<ProductRateDetail>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var commonService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICommonService>();
                var productRateDetailService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryProductRateDetail>();

                var productRateDetail = productRateDetailService.GetProductRateDetailWithValue(CareGroupId, ProductId, commonService.GetServerDate());
                
                result.Status = productRateDetail.StateResult;
                result.Message = productRateDetail.Message;
                result.Data = productRateDetail.ObjectEmbbeded;

                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("pharmacyDevolutionByConsecutive")]
        [HttpGet]
        // GET: GetDevolutionMedicationByConsecutive
        public RequestResponse<ViewDashBoardPharmacyDevolution> GetDevolutionMedicationByConsecutive(int consecutive)
        {
            var result = new RequestResponse<ViewDashBoardPharmacyDevolution>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var viewDashBoardPharmacyDevolutionRepository = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IDevolutionMedicationRepository>();
                var viewDashBoardPharmacyDevolution = viewDashBoardPharmacyDevolutionRepository.GetViewDashBoardPharmacyDevolutionByConsecutive(consecutive);
                
                if (viewDashBoardPharmacyDevolution.Row == 0)
                {
                    result.Status = false;
                    result.Message = String.Format("No existe una solicitud de devolutivo con número {0}.", consecutive);
                    return result;
                }

                result.Status = true;
                result.Data = viewDashBoardPharmacyDevolution;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("pharmacyDetailDevolution")]
        [HttpGet]
        // GET: ListDashboardPharmacyDetailDevolution
        public RequestResponse<List<ViewDashboardPharmacyDetailDevolution>> GetPharmacyDetailDevolution(int consecutive, string patientCode, string admission)
        {
            var result = new RequestResponse<List<ViewDashboardPharmacyDetailDevolution>>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var dashboardPharmacyDetailDevolutionService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICrystalServiceDashboardPharmacyDetailDevolution>();
                var dashboardPharmacyDetailDevolution = dashboardPharmacyDetailDevolutionService.ListDashboardPharmacyDetailDevolution(consecutive, patientCode, admission);

                if (dashboardPharmacyDetailDevolution.Count == 0)
                {
                    result.Status = false;
                    result.Message = String.Format("No existe un detalle para la solicitud de devolución {0}.", consecutive);
                    return result;
                }
                result.Status = true;
                result.Data = dashboardPharmacyDetailDevolution;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("surgicalPackageByConsecutive")]
        [HttpGet]
        // GET: ViewDashBoardPharmacy_SurgicalPackage
        public RequestResponse<ViewDashBoardPharmacy_SurgicalPackage> GetSurgicalPackageByConsecutive(decimal consecutive)
        {
            var result = new RequestResponse<ViewDashBoardPharmacy_SurgicalPackage>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var dashboardPharmacyService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryDashBoardPharmacy>();
                var dashboardPharmacy = dashboardPharmacyService.SurgicalPackageByConsecutive(consecutive);
                if (dashboardPharmacy.ObjectEmbbeded != null)
                {
                    if (dashboardPharmacy.ObjectEmbbeded.ConsecutivoFarmacia == 0)
                    {
                        throw new Exception(String.Format("No existe una solicitud de paquete qx con el consecutivo {0}.", consecutive));
                    }
                }
                else
                {
                    throw new Exception(String.Format("No existe una solicitud de paquete qx con el consecutivo {0}.", consecutive));
                }

                result.Status = dashboardPharmacy.StateResult;
                result.Data = dashboardPharmacy.ObjectEmbbeded;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }


        [Route("surgicalPackageDetail")]
        [HttpGet]
        // GET: ViewDashBoardPharmacy_SurgicalPackageDeatils
        public RequestResponse<ViewDashBoardPharmacy_SurgicalPackageDeatils> GetSurgicalPackageDeatils(decimal consecutive, string patientCode, string productCode)
        {
            var result = new RequestResponse<ViewDashBoardPharmacy_SurgicalPackageDeatils>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var dashboardPharmacyDetailService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICrystalServiceDashboardPharmacyDetail>();
                var dashboardPharmacyDetail = dashboardPharmacyDetailService.DashboardPharmacyDetailSurgicalPackage(consecutive, patientCode, productCode);

                if (dashboardPharmacyDetail.Id == null)
                {
                    result.Status = false;
                    result.Message = String.Format("No existe un detalle para el producto {0} de la solicitud de paquete qx con el consecutivo {1} del paciente {2}.", productCode, consecutive, patientCode);
                    return result;
                }
                result.Status = true;
                result.Data = dashboardPharmacyDetail;
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
