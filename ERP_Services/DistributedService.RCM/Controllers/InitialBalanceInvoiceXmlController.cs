using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Application.Portfolio;
using DistributedService.RCM.Utilities;
using DistributedService.Rest.Unity;
using Domain.Base.Entities;
using Domain.Billing.POCO.E_InvoiceXml;
using Infrastructure.CrossCutting.Base;
using Unity;

namespace DistributedService.Rest.Controllers
{
    /// <summary>
    /// Controlador para carga de XMLs DIAN de facturas saldo inicial.
    /// Almacena cada XML en blob storage respetando la convención de naming/path usada por
    /// ElectronicDocumentsAdminService, para que el worker DIAN herede el segmento Salud
    /// al generar notas crédito/débito tipo 6 sobre saldos iniciales.
    /// </summary>
    [RoutePrefix("initialBalanceInvoiceXml")]
    public class InitialBalanceInvoiceXmlController : ApiController
    {
        private const string CodeUserHeader = "CodeUser";
        private const int DefaultBulkThreshold = 10;
        private readonly bool _isMultiTenant;
        private readonly int _bulkThreshold;

        public InitialBalanceInvoiceXmlController()
        {
            _isMultiTenant = bool.TryParse(
                ConfigurationManager.AppSettings["EnableMultiTenant"] as string,
                out bool flag) && flag;

            _bulkThreshold = int.TryParse(
                ConfigurationManager.AppSettings["InvoiceXmlBulkThreshold"] as string,
                out int threshold) && threshold > 0
                    ? threshold
                    : DefaultBulkThreshold;
        }

        /// <summary>
        /// Cargue pequeño de XMLs (≤ InvoiceXmlBulkThreshold). Procesa síncronamente.
        /// Body: JSON array de InvoiceXmlUploadRequest.
        /// </summary>
        [Route("UploadSmall")]
        [HttpPost]
        public async Task<RequestResponse<InvoiceXmlBulkResponse>> UploadSmall([FromBody] List<InvoiceXmlUploadRequest> items)
        {
            if (items == null || items.Count == 0)
                return ErrorResponse<InvoiceXmlBulkResponse>("999", "Body vacío");

            if (items.Count > _bulkThreshold)
                return ErrorResponse<InvoiceXmlBulkResponse>("999",
                    $"UploadSmall acepta hasta {_bulkThreshold} items. Recibidos: {items.Count}. Use UploadBulk.");

            InitializeSession();
            ConfigureBlobStorage();
            var audit = CreateAudit();

            using (var service = ResolveService())
            {
                var response = await service.UploadInvoiceXmlSmallAsync(items, audit);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Cargue masivo de XMLs. Cliente envía batches de tamaño configurable.
        /// Body: InvoiceXmlBulkRequest con BatchId y items.
        /// </summary>
        [Route("UploadBulk")]
        [HttpPost]
        public async Task<RequestResponse<InvoiceXmlBulkResponse>> UploadBulk([FromBody] InvoiceXmlBulkRequest request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
                return ErrorResponse<InvoiceXmlBulkResponse>("999", "Body vacío");

            InitializeSession();
            ConfigureBlobStorage();
            var audit = CreateAudit();

            using (var service = ResolveService())
            {
                var response = await service.UploadInvoiceXmlBulkAsync(request.BatchId, request.Items, audit);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        /// <summary>
        /// Pre-check existencia XML en blob storage. Body: InvoiceXmlCheckExistRequest con InitialBalanceId
        /// + InvoiceNumbers. El InitialBalanceId permite derivar el FilePath canónico desde
        /// PortfolioInitialBalance.CreationDate antes de que existan los shadow Invoice/ElectronicDocument.
        /// Devuelve Existing/Missing. Usado por frontend SaveAndConfirm para bloquear confirmación si
        /// faltan XMLs para alguna factura saldo inicial con CUFE.
        /// </summary>
        [Route("CheckXmlExist")]
        [HttpPost]
        public async Task<RequestResponse<InvoiceXmlCheckExistResponse>> CheckXmlExist([FromBody] InvoiceXmlCheckExistRequest request)
        {
            if (request == null || request.InitialBalanceId <= 0 || request.InvoiceNumbers == null || request.InvoiceNumbers.Count == 0)
                return ErrorResponse<InvoiceXmlCheckExistResponse>("999", "Body vacío, sin InitialBalanceId o sin números de factura.");

            InitializeSession();
            ConfigureBlobStorage();
            var audit = CreateAudit();

            using (var service = ResolveService())
            {
                var response = await service.CheckInvoiceXmlExistAsync(request.InitialBalanceId, request.InvoiceNumbers, audit);
                return MapToResponse(response.ObjectEmbbeded, response.StateResult, response.Message);
            }
        }

        #region Helpers privados

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

        private IInvoiceXmlBulkAdminService ResolveService()
        {
            return ContainerRCM.Current(
                SessionValues.Instance.TransactionalContainer,
                SessionValues.Instance.HisContainer,
                SessionValues.Instance.SecurityContainer,
                true
            ).Resolve<IInvoiceXmlBulkAdminService>();
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

        private static RequestResponse<T> ErrorResponse<T>(string code, string message)
        {
            return new RequestResponse<T> { Code = code, Message = message, Status = false };
        }

        #endregion
    }
}
