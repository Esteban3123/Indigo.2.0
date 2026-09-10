using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Text.Json;
using Application.Billing;
using Application.Glosas;
using Domain.Billing.POCO;
using Domain.Billing.POCO.E_RIPS;
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
        private const int DefaultBulkThreshold = 10;
        private readonly bool _isMultiTenant;
        private readonly int _bulkThreshold;

        public ElectronicRipsController()
        {
            _isMultiTenant = bool.TryParse(
                ConfigurationManager.AppSettings["EnableMultiTenant"] as string,
                out bool flag) && flag;

            _bulkThreshold = int.TryParse(
                ConfigurationManager.AppSettings["RipsBulkThreshold"] as string,
                out int threshold) && threshold > 0
                    ? threshold
                    : DefaultBulkThreshold;
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

        #region Reconstrucción facturas monto fijo

        /// <summary>
        /// Enqueues fixed amount service records to rebuild their RIPS JSON.
        /// </summary>
        [Route("RebuildFixedAmountRIPS")]
        [HttpPost]
        public RequestResponse<string> RebuildFixedAmountRips()
        {
            var documentNumbers = ReadBodyAsDocumentList();
            if (documentNumbers == null)
                return ErrorResponse("999", "Body vacÃ­o");

            InitializeSession();
            return MapToResponse(ResolveService().RebuildFixedAmountRIPSToQueue(documentNumbers, CreateAudit()));
        }

        /// <summary>
        /// Returns paged service records for a fixed amount invoice.
        /// </summary>
        [Route("fixedAmount/serviceRecords")]
        [HttpPost]
        public async Task<RequestResponse<PagedResult<FixedAmountServiceRecordDto>>> GetFixedAmountServiceRecords([FromBody] FixedAmountServiceRecordQuery query)
        {
            InitializeSession();
            using (var service = ResolveInvoiceEntityCapitatedService())
            {
                var response = await service.GetFixedAmountServiceRecordsAsync(query);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Returns service records that can be rebuilt for affected patients.
        /// </summary>
        [Route("fixedAmount/rebuildCandidates")]
        [HttpPost]
        public async Task<RequestResponse<List<FixedAmountRebuildCandidateDto>>> GetFixedAmountRebuildCandidates([FromBody] FixedAmountServiceRecordQuery query)
        {
            InitializeSession();
            using (var service = ResolveInvoiceEntityCapitatedService())
            {
                var response = await service.GetFixedAmountServiceRecordsToRebuildAsync(query);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Returns rebuild progress for fixed amount service records.
        /// </summary>
        [Route("fixedAmount/rebuildStatus")]
        [HttpPost]
        public async Task<RequestResponse<FixedAmountRebuildStatusDto>> GetFixedAmountRebuildStatus([FromBody] FixedAmountServiceRecordQuery query)
        {
            InitializeSession();
            using (var service = ResolveInvoiceEntityCapitatedService())
            {
                var response = await service.GetFixedAmountRIPSRebuildStatusAsync(query);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Returns the database server date used as rebuild boundary.
        /// </summary>
        [Route("serverDate")]
        [HttpGet]
        public RequestResponse<DateTime> GetServerDate()
        {
            InitializeSession();
            using (var service = ResolveInvoiceEntityCapitatedService())
            {
                return MapToResponse(service.GetDatabaseDate(), true, String.Empty);
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

        #region Carga masiva de RIPS (Saldos Iniciales)

        /// <summary>
        /// Cargue pequeño de RIPS (≤ RipsBulkThreshold). Procesa síncronamente.
        /// Body: JSON array de RipsUploadRequest.
        /// </summary>
        [Route("UploadSmall")]
        [HttpPost]
        public async Task<RequestResponse<RipsBulkResponse>> UploadSmall([FromBody] List<RipsUploadRequest> items)
        {
            if (items == null || items.Count == 0)
                return ErrorResponse<RipsBulkResponse>("999", "Body vacío");

            if (items.Count > _bulkThreshold)
                return ErrorResponse<RipsBulkResponse>("999",
                    $"UploadSmall acepta hasta {_bulkThreshold} items. Recibidos: {items.Count}. Use UploadBulk.");

            InitializeSession();
            ConfigureBlobStorage();
            var audit = CreateAudit();

            using (var service = ResolveServiceWithBlob())
            {
                var response = await service.UploadRipsSmallAsync(items, audit);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Cargue masivo de RIPS. Cliente envía batches de tamaño configurable (default 100).
        /// Body: RipsBulkRequest con BatchId y items.
        /// </summary>
        [Route("UploadBulk")]
        [HttpPost]
        public async Task<RequestResponse<RipsBulkResponse>> UploadBulk([FromBody] RipsBulkRequest request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
                return ErrorResponse<RipsBulkResponse>("999", "Body vacío");

            InitializeSession();
            ConfigureBlobStorage();
            var audit = CreateAudit();

            using (var service = ResolveServiceWithBlob())
            {
                var response = await service.UploadRipsBulkAsync(request.BatchId, request.Items, audit);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Pre-check existencia RIPS en CosmosDB. Body: JSON array de numFactura. Devuelve Existing/Missing.
        /// Usado por Portfolio Confirm + frontend SaveAndConfirm para bloquear confirmación si faltan
        /// JSON RIPS para alguna factura con CUV.
        /// </summary>
        [Route("CheckRipsExist/")]
        [HttpPost]
        public async Task<RequestResponse<RipsCheckExistResponse>> CheckRipsExist()
        {
            var invoiceNumbers = ReadBodyAsDocumentList();
            if (invoiceNumbers == null || invoiceNumbers.Count == 0)
                return ErrorResponse<RipsCheckExistResponse>("999", "Body vacío o sin números de factura.");

            InitializeSession();
            ConfigureBlobStorage();
            var audit = CreateAudit();

            using (var service = ResolveServiceWithBlob())
            {
                var response = await service.CheckRipsExistAsync(invoiceNumbers, audit);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        #endregion

        #region Carga InitialBalanceInvoiceDetail (Saldos Iniciales)

        /// <summary>
        /// Pobla InitialBalanceInvoiceDetail desde CosmosDB para una lista de facturas confirmadas como saldos
        /// iniciales con RIPS validado (CUV). Idempotente: DELETE detail rows existentes antes del INSERT.
        /// Body: JSON array de números de factura. Pre-requisito: el saldo inicial debe estar confirmado
        /// (InitialBalanceInvoice header existe con Status=1 = DetailPending).
        /// </summary>
        [Route("PopulateInitialBalanceDetail/")]
        [HttpPost]
        public async Task<RequestResponse<List<Domain.Billing.POCO.E_RIPS.RipsUploadResult>>> PopulateInitialBalanceDetail()
        {
            var invoiceNumbers = ReadBodyAsDocumentList();
            if (invoiceNumbers == null || invoiceNumbers.Count == 0)
                return new RequestResponse<List<Domain.Billing.POCO.E_RIPS.RipsUploadResult>>
                {
                    Code = "999",
                    Message = "Body vacío o sin números de factura.",
                    Status = false
                };

            InitializeSession();
            ConfigureBlobStorage();
            var audit = CreateAudit();

            using (var service = ResolveServiceWithBlob())
            {
                var response = await service.PopulateInitialBalanceDetail(invoiceNumbers, audit);
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

        private IInvoiceEntityCapitatedAdminService ResolveInvoiceEntityCapitatedService()
        {
            return ContainerRCM.Current(
                SessionValues.Instance.TransactionalContainer,
                SessionValues.Instance.HisContainer,
                SessionValues.Instance.SecurityContainer
            ).Resolve<IInvoiceEntityCapitatedAdminService>();
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

        private static RequestResponse<T> ErrorResponse<T>(string code, string message)
        {
            return new RequestResponse<T> { Code = code, Message = message, Status = false };
        }

        #endregion
    }
}
