using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Linq;
using System.Web.Http;
using Unity;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/adjustmentConcept")]
    public class AdjustmentConceptController : ApiController
    {
        [Route("findByCode")]
        [HttpGet]
        // GET: AdjustmentConcept
        public RequestResponse<AdjustmentConcept> GetAdjustmentConcept(String code)
        {
            var result = new RequestResponse<AdjustmentConcept>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var adjustmentConceptService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryAdjustmentConcept>();
                var adjustmentConcept = adjustmentConceptService.GetAdjustmentConcept(code, audit);
                if (adjustmentConcept.ObjectEmbbeded.Id == 0)
                {
                    throw new Exception(String.Format("El Concepto de Movimiento con código {0} no existe.", code));
                }
                result.Status = adjustmentConcept.StateResult;
                result.Data = adjustmentConcept.ObjectEmbbeded;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("findByAdjustmentAccountCostCenterId")]
        [HttpGet]
        // GET: List<AdjustmentConcept>
        public RequestResponse<AdjustmentConcept> GetListByAdjustmentAccountCostCenterId(Byte conceptType, int adjustmentAccountId, int costCenterId)
        {
            var result = new RequestResponse<AdjustmentConcept>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var adjustmentConceptService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryAdjustmentConcept>();
                var adjustmentConcept = adjustmentConceptService.GetListByAdjustmentAccountCostCenterId(conceptType, adjustmentAccountId, costCenterId, audit);
                if (adjustmentConcept.ObjectEmbbeded.Count == 0)
                {
                    throw new Exception("No existen Conceptos de Movimientos parametrizados.");
                }
                else if (adjustmentConcept.ObjectEmbbeded.Count > 1)
                {
                    throw new Exception("Existe más de un concepto de movimiento parametrizado.");
                }
                result.Status = adjustmentConcept.StateResult;
                result.Data = adjustmentConcept.ObjectEmbbeded.FirstOrDefault();
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
