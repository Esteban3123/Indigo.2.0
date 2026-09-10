using DistributedService.SCM.Unity;
using DistributedService.SCM.Utilities;
using DistributedServices.Inventory.Contracts;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Unity;


namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("inventory/entranceVoucher")]
    public class EntranceVoucherController : ApiController
    {
        [Route("save")]
        [HttpPost]
        public RequestResponse<string> SaveEntranceVoucher()
        {
            var result = new RequestResponse<String>();
            try
            {
                EntranceVoucher entranceVoucher;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    entranceVoucher = Utils.DeserializeJsonToEntity<EntranceVoucher>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(entranceVoucher.Code, "1402", entranceVoucher.WarehouseId, entranceVoucher.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryService>();
                var response = service.SaveEntranceVoucher(entranceVoucher, idCurrentSequense, audit, inventorySequense);
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
        public async Task<RequestResponse<string>> SaveAndConfirmEntranceVoucher()
        {
            var result = new RequestResponse<String>();
            try
            {
                EntranceVoucher entranceVoucher;
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    entranceVoucher = Utils.DeserializeJsonToEntity<EntranceVoucher>(bodyData);
                }

                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var sequense = CurrentSequenseUtils.GetIdCurrentSequense(entranceVoucher.Code, "1402", entranceVoucher.WarehouseId, entranceVoucher.OperatingUnitId, audit);
                int idCurrentSequense = sequense.Item1;
                InventorySequence inventorySequense = sequense.Item2;

                var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryService>();
                var response = await service.SaveAndConfirmbEntranceVoucherAsync(entranceVoucher, SessionValues.Instance.HisContainer, audit, idCurrentSequense, inventorySequense, Infrastructure.CrossCutting.Audit.Actions.Insert, false);
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
        // GET: EntranceVoucherByCode
        public RequestResponse<EntranceVoucher> GetEntranceVoucher(string code)
        {
            var result = new RequestResponse<EntranceVoucher>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var entranceVoucherService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryService>();
                var entranceVoucher = entranceVoucherService.GetEntranceVoucher(code, audit);

                result.Status = entranceVoucher.StateResult;
                result.Message = entranceVoucher.Message;
                result.Data = entranceVoucher.ObjectEmbbeded;
                return result;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.Message;
                return result;
            }
        }

        [Route("findDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher")]
        [HttpGet]
        // GET: DetailEntranceVoucherWithBatchSerialByIdEntranceVoucher
        public RequestResponse<List<EntranceVoucherDetailBatchSerial>> GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(int entranceVoucherId)
        {
            var result = new RequestResponse<List<EntranceVoucherDetailBatchSerial>>();
            try
            {
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                var audit = new AuditMessage()
                {
                    CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser")
                };

                var entranceVoucherService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryService>();
                var entranceVoucherDetailBatchSerial = entranceVoucherService.GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(entranceVoucherId);

                result.Status = true;
                result.Data = entranceVoucherDetailBatchSerial;
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