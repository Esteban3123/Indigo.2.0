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
    [RoutePrefix("inventory/consignmentInventoryRemissionDetailBatchSerial")]
    public class ConsignmentInventoryRemissionDetailBatchSerialController : ApiController
    {
        [Route("listConsignmentInventoryRemissionWithoutLegalize")]
        [HttpGet]
        // GET: listConsignmentInventoryRemissionWithoutLegalize
        public RequestResponse<List<ViewConsignmentInventoryRemissionWithoutLegalize>> ListConsignmentInventoryRemissionWithoutLegalize(string code, int warehouseId)
        {
            var result = new RequestResponse<List<ViewConsignmentInventoryRemissionWithoutLegalize>>();
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

                var listConsignmentInventoryRemissionWithoutLegalizeService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceConsignmentInventoryRemissionDetailBatchSerial>();
                var listConsignmentInventoryRemissionWithoutLegalize = listConsignmentInventoryRemissionWithoutLegalizeService.ListConsignmentInventoryRemissionWithoutLegalize(code, warehouseId);
                if (listConsignmentInventoryRemissionWithoutLegalize.Count == 0)
                {
                    throw new Exception(String.Format("No existen productos a legalizar para la Remisión de Consignación {0}.", code));
                }
                result.Status = true;
                result.Data = listConsignmentInventoryRemissionWithoutLegalize;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("listByConsignmentInventoryRemissionId")]
        [HttpGet]
        // GET: listConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId
        public RequestResponse<List<ConsignmentInventoryRemissionDetailBatchSerial>> ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(int id)
        {
            var result = new RequestResponse<List<ConsignmentInventoryRemissionDetailBatchSerial>>();
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

                var listConsignmentInventoryRemissionDetailBatchService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceConsignmentInventoryRemissionDetailBatchSerial>();
                var listConsignmentInventoryRemissionDetailBatch = listConsignmentInventoryRemissionDetailBatchService.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(id);
                if (listConsignmentInventoryRemissionDetailBatch.Count == 0)
                {
                    throw new Exception("No existen productos a legalizar en la Remisión de Consignación.");
                }
                result.Status = true;
                result.Data = listConsignmentInventoryRemissionDetailBatch;
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
