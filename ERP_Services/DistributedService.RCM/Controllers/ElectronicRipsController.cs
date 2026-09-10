using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Text.Json;
using Application.Glosas;
using Domain.Billing.POCO;
using Domain.Base.Entities;
using DistributedService.RCM.Utilities;
using DistributedService.Rest.Unity;
using Infrastructure.CrossCutting.Base;
using Unity;

namespace DistributedService.Rest.Controllers
{
    /// <summary>
    /// Controlador para RIPS electrónicos: envío, reenvío y consulta.
    /// </summary>
    [RoutePrefix("electronicRIPS")]
    public class ElectronicRipsController : ApiController
    {
        private const string CodeUserHeader = "CodeUser";
        private readonly bool _isMultiTenant;

        public ElectronicRipsController()
        {
            _isMultiTenant = bool.TryParse(
                ConfigurationManager.AppSettings["EnableMultiTenant"] as string,
                out bool flag) && flag;
        }

        #region Envío de RIPS

        /// <summary>
        /// Envía RIPS al servicio del gobierno (cola de procesamiento).
        /// Body: JSON array de números de documento.
        /// </summary>
        [Route("sendRIPS/{entityName}")]
        [HttpPost]
        public RequestResponse<string> SendElectronicRips(string entityName, [FromUri] int? entityId = null)
        {
            var documentNumbers = ReadBodyAsDocumentList();
            if (documentNumbers == null)
                return ErrorResponse("999", "Body vacío");

            InitializeSession();
            var audit = CreateAudit();

            var service = ResolveService();
            var response = service.GenerateElectronicRIPSToQueue(entityName, documentNumbers, audit, entityId);

            return MapToResponse(response);
        }

        #endregion

        #region Reenvío de RIPS

        /// <summary>
        /// Reenvía RIPS al servicio Prometheus.
        /// Body: JSON array de números de documento.
        /// </summary>
        [Route("ResendRIPS/{entityName}")]
        [HttpPost]
        public RequestResponse<string> ReSendElectronicRips(string entityName, [FromUri] int? entityId = null)
        {
            var documentNumbers = ReadBodyAsDocumentList();
            if (documentNumbers == null)
                return ErrorResponse("999", "Body vacío");

            InitializeSession();
            var audit = CreateAudit();

            var service = ResolveService();
            var response = service.ReSendElectronicRIPSToQueue(entityName, documentNumbers, audit, entityId);

            return MapToResponse(response);
        }

        /// <summary>
        /// Reenvío masivo según políticas configuradas.
        /// </summary>
        [Route("MassiveResendWithPolicies/")]
        [HttpPost]
        public RequestResponse<string> MassiveResendWithPolicies([FromBody] ResendTakeRequest request)
        {
            if (request == null || request.Take <= 0)
                return ErrorResponse("999", "El número de mensajes a reenviar debe ser mayor que 0.");

            InitializeSession();
            var audit = CreateAudit();

            using (var service = ResolveServiceWithBlob())
            {
                var response = service.MassiveResendWithPolicies(request.Take, audit);
                return MapToResponse(response);
            }
        }

        #endregion

        #region Consulta de RIPS

        /// <summary>
        /// Obtiene el JSON RIPS por ID de CosmosDB.
        /// </summary>
        [Route("GetJsonRIPSById/{IdItemCosmoDB}")]
        [HttpGet]
        public async Task<RequestResponse<string>> GetJsonRipsById(string IdItemCosmoDB)
        {
            InitializeSession();
            ConfigureBlobStorage();

            using (var service = ResolveServiceWithBlob())
            {
                var response = await service.GetJsonRIPSById(IdItemCosmoDB);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Obtiene el JSON RIPS por número de documento.
        /// </summary>
        [Route("GetJsonRIPSByDocNumber/{docNumber}")]
        [HttpGet]
        public async Task<RequestResponse<string>> GetJsonRipsByDocNumber(string docNumber)
        {
            InitializeSession();
            ConfigureBlobStorage();

            using (var service = ResolveServiceWithBlob())
            {
                var response = await service.GetJsonRIPSByDocNumber(docNumber);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Obtiene el objeto RIPS tipado por ID de CosmosDB.
        /// </summary>
        [Route("GetObjectJsonRIPSbyId/{IdItemCosmoDB}")]
        [HttpGet]
        public async Task<RequestResponse<ElectronicRIPSModel>> GetObjectJsonRipsById(string IdItemCosmoDB)
        {
            InitializeSession();
            ConfigureBlobStorage();

            using (var service = ResolveServiceWithBlob())
            {
                var response = await service.GetObjectJsonRIPSbyId(IdItemCosmoDB);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        #endregion

        #region Helpers privados

        private List<string> ReadBodyAsDocumentList()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyData = bodyStream.ReadToEnd();

            if (string.IsNullOrEmpty(bodyData))
                return null;

            return JsonSerializer.Deserialize<List<string>>(bodyData);
        }

        private void InitializeSession()
        {
            HeaderValueUtils.CleanSessionVariables();

            var headers = Request.Headers;
            var container = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER);
            var hisContainer = HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER_HIS);

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;
            ServerSessionValues.Current.CurrentContainer = container;
            SessionValues.Instance.SecurityContainer = HeaderValueUtils.GetOptionalValues(headers, ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME);
            SessionValues.Instance.CosmosDbContainer = HeaderValueUtils.GetOptionalValues(headers, ConfigurationFile.SESS_CONTAINER_AZCOS);
            SessionValues.Instance.CosmosDB = _isMultiTenant
                ? HeaderValueUtils.GetValues(headers, ConfigurationFile.SESS_CONTAINER)
                : HeaderValueUtils.GetOptionalValues(headers, ConfigurationFile.SESS_DATABASE_AZCOS);
        }

        private void ConfigureBlobStorage()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var headers = Request.Headers;

            ServerSessionValues.Current.CurrentBlobConnectionString =
                appSettings[ConfigurationFile.CONX_BLOB_STRING_AZ]
                ?? HeaderValueUtils.GetOptionalValues(headers, ConfigurationFile.CONX_BLOB_STRING_AZ);
            ServerSessionValues.Current.BlobContainerName =
                appSettings[ConfigurationFile.CONX_BLOB_CONTAINER_NAME_AZ]
                ?? HeaderValueUtils.GetOptionalValues(headers, ConfigurationFile.CONX_BLOB_CONTAINER_NAME_AZ);
        }

        private AuditMessage CreateAudit()
        {
            return new AuditMessage
            {
                CodeUser = HeaderValueUtils.GetValues(Request.Headers, CodeUserHeader),
                Company = SessionValues.Instance.TransactionalContainer
            };
        }

        private IRIPSPlaneAdminService ResolveService()
        {
            return ContainerRCM.Current(
                SessionValues.Instance.TransactionalContainer,
                SessionValues.Instance.HisContainer,
                SessionValues.Instance.SecurityContainer
            ).Resolve<IRIPSPlaneAdminService>();
        }

        private IRIPSPlaneAdminService ResolveServiceWithBlob()
        {
            return ContainerRCM.Current(
                SessionValues.Instance.TransactionalContainer,
                SessionValues.Instance.HisContainer,
                SessionValues.Instance.SecurityContainer,
                true
            ).Resolve<IRIPSPlaneAdminService>();
        }

        private static RequestResponse<string> MapToResponse(dynamic serviceResponse)
        {
            return new RequestResponse<string>
            {
                Status = serviceResponse.StateResult,
                Message = serviceResponse.Message,
                Code = serviceResponse.StatusCode?.ToString() ?? ((int)HttpStatusCode.OK).ToString()
            };
        }

        private static RequestResponse<T> MapToResponse<T>(T data, bool status, string message)
        {
            return new RequestResponse<T>
            {
                Data = data,
                Status = status,
                Message = message,
                Code = ((int)HttpStatusCode.OK).ToString()
            };
        }

        private static RequestResponse<string> ErrorResponse(string code, string message)
        {
            return new RequestResponse<string> { Code = code, Message = message, Status = false };
        }

        #endregion
    }
}
