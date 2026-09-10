using DistributedService.Causation.Extensions;
using DistributedService.Causation.Models;
using DistributedService.Causation.Services;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DistributedService.Causation.Controllers
{
    [RoutePrefix("causation")]
    public class CausationController : ApiController
    {
        private const string AllowedServiceTypesHeader = "X-MedicalFees-Causation-Service-Types";

        [Route("causate/invoices")]
        [HttpPost]
        public async Task<RequestResponse<String>> CausateInvoices()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyData = bodyStream.ReadToEnd();
            var headers = Request.Headers;
            var response = new RequestResponse<String>();
            String container = String.Empty;
            String hisContainer = String.Empty;
            String codeUser = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
            {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
            {
                hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();
            }

            if (headers.Contains("CodeUser"))
            {
                codeUser = headers.GetValues("CodeUser").First();
            }

            if (!TryGetAllowedServiceTypes(headers, out var allowedServiceTypes, out var configurationError))
            {
                response.Status = false;
                response.Message = configurationError;
                return response;
            }

            var data = JsonConvert.DeserializeObject<object>(bodyData);
            var invoices = data.MapTo<List<InvoiceEvent>>();

            ICausationService _causationService = new CausationService();
            var result = await _causationService.CausateInvoicesAsync(invoices, container, codeUser, allowedServiceTypes);

            response.Status = result.StateResult;
            response.Message = result.Message;

            return response;
        }

        private static bool TryGetAllowedServiceTypes(
            System.Net.Http.Headers.HttpRequestHeaders headers,
            out List<byte> allowedServiceTypes,
            out string errorMessage)
        {
            allowedServiceTypes = null;
            errorMessage = null;

            if (!headers.Contains(AllowedServiceTypesHeader))
            {
                return true;
            }

            var configuredValue = string.Join(",", headers.GetValues(AllowedServiceTypesHeader));
            if (string.IsNullOrWhiteSpace(configuredValue))
            {
                return true;
            }

            var parsedServiceTypes = new SortedSet<byte>();
            foreach (var configuredServiceType in configuredValue.Split(','))
            {
                var normalizedServiceType = configuredServiceType.Trim();
                if (string.IsNullOrWhiteSpace(normalizedServiceType)
                    || !byte.TryParse(normalizedServiceType, NumberStyles.None, CultureInfo.InvariantCulture, out var serviceType))
                {
                    errorMessage = $"El encabezado {AllowedServiceTypesHeader} contiene un tipo de servicio no válido: '{configuredServiceType}'.";
                    return false;
                }

                parsedServiceTypes.Add(serviceType);
            }

            allowedServiceTypes = parsedServiceTypes.ToList();
            return true;
        }
        
        [Route("retryCausate/pending")]
        [HttpPost]
        public RequestResponse<String> RetryCausatePending()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyData = bodyStream.ReadToEnd();
            var headers = Request.Headers;
            String container = String.Empty;
            String hisContainer = String.Empty;
            String codeUser = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
            {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
            {
                hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();
            }

            if (headers.Contains("CodeUser"))
            {
                codeUser = headers.GetValues("CodeUser").First();
            }

            var _dataViewListNoSurgical = JsonConvert.DeserializeObject<ViewListNoSurgical>(bodyData);
            ICausationService _causationService = new CausationService();
            var result = _causationService.RetryCausateDetailInvoice(_dataViewListNoSurgical, container, codeUser);

            var response = new RequestResponse<String>();
            response.Status = result.StateResult;
            response.Message = result.Message;

            return response;
        }

        [Route("processContract/update")]
        [HttpPost]
        public async Task<RequestResponse<String>> ProcessContractUpdate()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyData = bodyStream.ReadToEnd();
            var headers = Request.Headers;
            String container = String.Empty;
            String hisContainer = String.Empty;
            String codeUser = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
            {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
            {
                hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();
            }

            if (headers.Contains("CodeUser"))
            {
                codeUser = headers.GetValues("CodeUser").First();
            }

            try
            {
                var contractUpdateData = JsonConvert.DeserializeObject<ContractUpdateRequest>(bodyData);
                ICausationService _causationService = new CausationService();
                var result = await _causationService.ProcessContractCausationsAsync(contractUpdateData, container, codeUser);

                var response = new RequestResponse<String>();
                response.Status = result.StateResult;
                response.Message = result.Message;

                return response;
            }
            catch (Exception ex)
            {
                var response = new RequestResponse<String>();
                response.Status = false;
                response.Message = $"Error procesando actualización de contrato: {ex.Message}";
                return response;
            }
        }

        /// <summary>
        /// Auto-causación de servicios no facturados (OS Registrada).
        /// Usado por el timer semanal de Stella y ejecución manual.
        /// Procesa todas las unidades operativas.
        /// </summary>
        [Route("autocausate/unbilled")]
        [HttpPost]
        public async Task<RequestResponse<String>> AutoCausateUnbilled()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyData = bodyStream.ReadToEnd();
            var headers = Request.Headers;
            var response = new RequestResponse<String>();
            String container = String.Empty;
            String codeUser = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
            {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains("CodeUser"))
            {
                codeUser = headers.GetValues("CodeUser").First();
            }

            try
            {
                var requestData = JsonConvert.DeserializeObject<AutoCausateUnbilledRequest>(bodyData);
                int batchSize = requestData?.BatchSize ?? 500;

                ICausationService _causationService = new CausationService();
                var result = await _causationService.AutoCausateUnbilledAsync(batchSize, container, codeUser);

                response.Status = result.StateResult;
                response.Message = result.Message;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error en auto-causación no facturado: {ex.Message}";
            }

            return response;
        }

    }

    /// <summary>
    /// Request model para auto-causación de no facturados
    /// </summary>
    public class AutoCausateUnbilledRequest
    {
        public int BatchSize { get; set; }
    }
}