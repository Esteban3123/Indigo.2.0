using Application.MixingStation;
using DistributedService.Report.Models;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using Unity;

namespace DistributedService.MixingStation.Controllers
{
    /// <summary>
    /// Controlador de campañas
    /// </summary>
    [RoutePrefix("api/campaign")]
    public class CampaignController : ApiController
    {
        /// <summary>
        /// Lista los productos para la gestión de materia prima
        /// </summary>
        /// <param name="campaignDetailId"></param>
        /// <param name="stockId"></param>
        /// <param name="wareHouseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{campaignDetailId}/listProductRawMaterial")]
        public async Task<IHttpActionResult> listProductRawMaterial(
            int campaignDetailId,
            [FromUri] int stockId,
            [FromUri] int wareHouseId
        )
        {
            using (var service = Unity.Container.Instance.Resolve<IRawMaterialAdminService>())
            {
                return Ok(await service.ListProductRawMaterialAsync(campaignDetailId, stockId, wareHouseId, 2, null));
            }
        }

        /// <summary>
        /// procesar producto terminado
        /// </summary>
        /// <param name="campaignDetailId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{campaignDetailId}/processFinishedProduct")]
        public async Task<IHttpActionResult> ProcessFinishedProduct(
            int campaignDetailId,
            [FromBody] ProcessFinishedProductModel model
        )
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    Timeout = TransactionManager.MaximumTimeout,
                    IsolationLevel = IsolationLevel.ReadCommitted,
                }))
                {
                    var requestMixingStationDetailRepository = Unity.Container.Instance.Resolve<IRequestMixingStationDetailRepository>();
                    var requestMixingStationDetailIds = requestMixingStationDetailRepository
                        .Query(m => m.CampaignDetailId == campaignDetailId, tracking: false)
                        .Select(m => m.Id)
                        .ToList();
                    try
                    {
                        using (var campaignService = Unity.Container.Instance.Resolve<ICampaignAdminService>())
                        {
                            var res = await campaignService.ProcessFinishedProductAsync(requestMixingStationDetailIds, new Infrastructure.CrossCutting.Base.AuditMessage
                            {
                                CodeUser = model.UserCode
                            });
                            if (res == null || !res.StateResult)
                            {
                                scope.Dispose();
                                return BadRequest(res.Message);
                            }

                            string message = res.Message;
                            if (res.ObjectEmbbeded.Any())
                            {
                                message = string.Join(", ", res.ObjectEmbbeded.Select(m => m.Item1).ToArray());
                            }

                            res.Message = message;
                            scope.Complete();
                            return Ok(res);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        scope.Dispose();
                        throw ex;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        /// <summary>
        /// Generación de una órden de traslado
        /// </summary>
        /// <param name="campaignDetailId"></param>
        /// <param name="transferOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{campaignDetailId}/transferOrder")]
        public IHttpActionResult GenerateTransferOrderAsync(
            int campaignDetailId,
            [FromBody] TransferOrderModel transferOrder
        )
        {
            using (var service = Unity.Container.Instance.Resolve<IRawMaterialDevolutionAdminService>())
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    Timeout = TransactionManager.MaximumTimeout,
                    IsolationLevel = IsolationLevel.ReadCommitted,
                }))
                {
                    var validationRepo = Unity.Container.Instance.Resolve<ICampaignDetailValidationRepository>();
                    var operatingUnitRepo = Unity.Container.Instance.Resolve<IOperatingUnitRepository>();
                    var devolution = transferOrder.ToRawMaterialDevolution();
                    devolution.ProductionWarehouseId = transferOrder.ProductWarehouseId;
                    devolution.StockWarehouseId = transferOrder.StockWarehouseId;

                    foreach (var detail in devolution.RawMaterialDevolutionDetail)
                    {
                        var validationDB = validationRepo.FirstOrDefault(m => m.Id == detail.CampaignDetailValidationId, tracking: false);
                        if (validationDB != null)
                        {
                            detail.DeliveredQuantity = validationDB.DeliveredQuantity;
                            detail.DevolutionQuantity = validationDB.DevolutionQuantity;
                            detail.ProductId = validationDB.ProductId;
                            detail.BatchSerialId = validationDB.BatchSerialId;
                        }
                    }

                    var operativeUnitId = operatingUnitRepo.FirstOrDefault(m => true).Id;
                    using (var sequenceService = Unity.Container.Instance.Resolve<IMixingStationSequenceAdminService>())
                    {
                        var sequenceId = sequenceService.GetCurrentSequenceByIdForm("2701", operativeUnitId);
                        var res = service.SaveAndConfirmRawMaterialDevolution(
                            rawMaterialDevolution: devolution,
                            operatingUnitId: operativeUnitId,
                            audit: new Infrastructure.CrossCutting.Base.AuditMessage
                            {
                                CodeUser = transferOrder.UserCode,
                            },
                            idSecuence: sequenceId
                        );

                        if (res == null || res.StatusCode != Domain.Base.Entities.eStatusResult.SUCCESS)
                        {
                            scope.Dispose();
                            return BadRequest(res.Message);
                        }

                        scope.Complete();
                        return Ok(res);
                    }
                }
            }
        }

        /// <summary>
        /// Procesar ajuste
        /// </summary>
        /// <param name="campaignDetailId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{campaignDetailId}/processAdjusts")]
        public IHttpActionResult ProcessAdjust(int campaignDetailId)
        {
            return Ok();
        }
    }
}
