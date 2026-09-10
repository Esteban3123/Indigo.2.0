using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Common;
using DistributedServices.Maintenance;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/supplier")]
    public class SupplierController : ApiController
    {
        [Route("findByCode")]
        [HttpGet]
        // GET: Supplier
        public RequestResponse<Supplier> GetSupplier(String code)
        {
            var result = new RequestResponse<Supplier>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var supplierService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ISupplierService>();
                var supplier = supplierService.GetSupplier(code, SessionValues.Instance);
                if (supplier.Id == 0)
                {
                    throw new Exception(String.Format("El proveedor con código {0} no existe.", code));
                }
                result.Status = true;
                result.Data = supplier;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("distributionLinesByIdSupplierAndAccusationConcept")]
        [HttpGet]
        // GET: DistributionLinesByIdSupplierAndAccusationConcept
        public RequestResponse<List<SuppliersDistributionLines>> GetDistributionLinesByIdSupplierAndAccusationConcept(int idSupplier, int accusationConcept)
        {
            var result = new RequestResponse<List<SuppliersDistributionLines>>();
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

                var suppliersDistributionLinesService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICommonERPSuppliersDistributionLines>();
                var suppliersDistributionLines = suppliersDistributionLinesService.GetDistributionLinesByIdSupplierAndAccusationConcept(idSupplier, accusationConcept, SessionValues.Instance);
                if (suppliersDistributionLines.Count == 0)
                {
                    throw new Exception("El proveedor no tiene lineas de distribución parametrizadas.");
                }
                result.Status = true;
                result.Data = suppliersDistributionLines;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("retentionConcept")]
        [HttpGet]
        // GET: RetentionConcept
        public RequestResponse<RetentionConcepts> GetICARetentionConceptBySupplierDistributionLine(int idSupplierDistributionLine, int idOperatingUnit)
        {
            var result = new RequestResponse<RetentionConcepts>();
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

                var retentionConceptsService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICommonERPSuppliersDistributionLines>();
                var retentionConcepts = retentionConceptsService.GetICARetentionConceptBySupplierDistributionLine(idSupplierDistributionLine, idOperatingUnit, SessionValues.Instance);
                if (retentionConcepts.Id == 0)
                {
                    throw new Exception("El proveedor no tiene conceptos de retención parametrizados.");
                }
                result.Status = true;
                result.Data = retentionConcepts;
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