using Domain.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Management;
using DistributedService.SCM.Utilities;
using Infrastructure.CrossCutting.Base;
using DistributedService.SCM.Unity;
using Unity;
using DistributedServices.Inventory.Contracts;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Inventory.POCO;
using DistributedServices.Base;

namespace DistributedService.SCM.Controllers
{
    [RoutePrefix("rollback")]
    public class RollbackController : ApiController
    {

        [Route("execute")]
        [HttpPost]
        public async Task<RequestResponse<Object>> ExecuteRollback()
        {
            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                var bodyObject = reader.ReadToEnd();
                var result = new RequestResponse<object>();
                var headers = Request.Headers;
                SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);
                try
                {
                    var entryObject = JsonConvert.DeserializeObject<RequestEntryObject>(bodyObject);

                    var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceRollbackStrategy>();
                    bool rollbackResponse = await service.ExecuteRollbackAsync(entryObject.Data.Code, entryObject.Source);
                    if (rollbackResponse)
                    {
                        result.Status = true;
                        result.Code = entryObject.Data.Code;
                        result.Message = $"Se ejecutó un rollback exitoso de los registros relacionados a la transacción de tipo: {entryObject.Source}, y con código: {entryObject.Data.Code}";
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = $"No se encontraron registros para hacer rollback en la transacción: {entryObject.Source} con código: {entryObject.Data.Code}";
                    }
                }
                catch (Exception ex)
                {
                    result.Status = false;
                    result.Message = $"Ocurrió un error inesperado. {ex.Message}";
                    return result;
                }
                return result;
            }
        }

        [Route("allSurgicalPackageTransaction")]
        [HttpPost]
        public RequestResponse<string> AllSurgicalPackageTransaction()
        {
            var result = new RequestResponse<String>();
            try
            {
                using (var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyData = bodyStream.ReadToEnd();
                    AllSurgicalPackageProcessWrapper entryObject = Utils.DeserializeJsonToEntity<AllSurgicalPackageProcessWrapper>(bodyData);


                    var headers = Request.Headers;
                    SessionValues.Instance.TransactionalContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
                    SessionValues.Instance.HisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

                    var audit = new AuditMessage()
                    {
                        CodeUser = HeaderValueUtils.GetValues(headers, "CodeUser"),
                        DispensingIntegration = 1,
                        IdUser = entryObject.UserId
                    };

                    var service = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IInventoryServiceSurgicalPackageProcess>();
                    var commonService = ContainerSCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<ICommonService>();
                    var response = service.AllSurgicalPackageTransaction(entryObject, audit, commonService.GetServerDate(), SessionValues.Instance.TransactionalContainer);
                    result.Status = response.StateResult;
                    result.Message = response.Message;
                    return result;
                }
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