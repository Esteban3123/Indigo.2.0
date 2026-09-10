using Application.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DistributedService.Rest.Unity;
using Unity;
using Domain.Entities;
using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using Domain.Billing.POCO;
using NewRelic.Api.Agent;
using DistribuitedServices.Billing;
using System.IO;
using System.Web;
using Application.Crystal;

namespace DistributedService.Rest.Controllers
{
    [RoutePrefix("billing")]
    public class BillingController : ApiController
    {
        [Route("folio/{idFolio:int}")]
        [Transaction]
        public RequestResponse<Folio> Get(int idFolio)
        {
            var headers = Request.Headers;
            String container = String.Empty;
            String hisContainer = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER)) {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
            {
                hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();
            }

            using (var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>())
            {
                return service.GetFolioDetails(idFolio);
            }
        }

        [Route("invoice/annullate/{invoiceId:int}")]
        public RequestResponse<AnnullateFolio> GetAnnullateFolio(int invoiceId)
        {
            var headers = Request.Headers;
            String container = String.Empty;
            String hisContainer = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
            {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
            {
                hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();
            }

            using (var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>())
            {
                return service.GetAnnullateFolio(invoiceId);
            }
        }

        [Route("serviceOrder/accountControl")]
        [HttpPost]
        public RequestResponse<String> GenerateServiceOrderAccountControl()
        {
            var headers = Request.Headers;
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyData = bodyStream.ReadToEnd();
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

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;

            var audit = new AuditMessage();
            audit.CodeUser = codeUser;
            var service = ContainerRCM.Current(container, hisContainer).Resolve<IBillingServiceAccountControl>();
            var response = service.GenerateServiceOrderMassive(bodyData, null, audit);

            var result = new RequestResponse<String>();
            result.Status = response.StateResult;
            if (response.StateResult == false) {
                result.Message = response.Message;
                if (response.ObjectEmbbeded != null && response.ObjectEmbbeded.Count > 0) {
                    result.Message = "No se logro liquidar ya que existen homologaciones";
                }
            }

            return result;
        }

        [Route("listOfStays/admissionToModel/{admissionCode}")]
        [HttpGet]
        public RequestResponse<String> GetListOfStaysByAdmissionToModel(String admissionCode)
        {
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

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;

            var audit = new AuditMessage();
            audit.CodeUser = codeUser;
            var service = ContainerRCM.Current(container, hisContainer).Resolve<IStayAdminService>();
            var response = service.ListOfStaysByAdmissionToModel(admissionCode, audit);

            var result = new RequestResponse<String>();
            result.Status = response.StateResult;
            result.Message = response.Message;
            if (response.ObjectEmbbeded != null && response.ObjectEmbbeded.Count > 0)
            {
                var data = response.ObjectEmbbeded.Where(x => x.Selected == false);

                result.Data = (data.Count() > 0) ? Utils.SerializeObjectToJson(data) : null;
            }

            return result;
        }


        [Route("RecalculateFolio/{admissionCode}/{revenueControlDetailId}")]
        [HttpGet]
        public RequestResponse<String> RecalculateFolio(String admissionCode, int revenueControlDetailId)
        {
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

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;

            var audit = new AuditMessage();
            audit.CodeUser = codeUser;
            var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>();
            var response = service.RecalculateFolio(admissionCode, revenueControlDetailId, audit);

            var result = new RequestResponse<String>();
            result.Status = response.StateResult;
            result.Message = response.Message;           
            return result;
        }

        [Route("SeparateAccount/{admissionCode}/{revenueControlDetailId}")]
        [HttpGet]
        public RequestResponse<String> SeparateAccount(String admissionCode, int revenueControlDetailId)
        {
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

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;

            var audit = new AuditMessage();
            audit.CodeUser = codeUser;
            var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>();
            var response = service.SeparateAccount(admissionCode, revenueControlDetailId, audit);

            var result = new RequestResponse<String>();
            result.Status = response.StateResult;
            result.Message = response.Message;
            return result;
        }


        [Route("UnifyAccount/{admissionCode}")]
        [HttpGet]
        public RequestResponse<String> UnifyAccount(String admissionCode)
        {
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

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;

            var audit = new AuditMessage();
            audit.CodeUser = codeUser;
            var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>();
            var response = service.UnifyAccount(admissionCode, audit);

            var result = new RequestResponse<String>();
            result.Status = response.StateResult;
            result.Message = response.Message;
            return result;
        }

        [Route("ValidateLiquidateFolio/{listRevenueControlDetailId}")]
        [HttpGet]
        public RequestResponse<string> ValidateLiquidateFolio( string listRevenueControlDetailId)
        {
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

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;

            var audit = new AuditMessage();
            audit.CodeUser = codeUser;
            var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>();
            var response = service.ValidateLiquidateFolio(listRevenueControlDetailId);
            return response;
        }


        [Route("CloseAccount/{revenueControlDetailId}")]
        [HttpGet]
        public RequestResponse<String> CloseFolioAccount(int? revenueControlDetailId)
        {
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

            SessionValues.Instance.HisContainer = hisContainer;
            SessionValues.Instance.TransactionalContainer = container;

            var audit = new AuditMessage();
            audit.CodeUser = codeUser;
            var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>();
            var response = service.CloseAccount( revenueControlDetailId, audit);

            var result = new RequestResponse<String>();
            result.Status = response.StateResult;
            result.Message = response.Message;
            return result;
        }

        [Route("GetVReportInvoicePartial/{folioId:int}/{CurrencyId?}/{date?}")]
        [HttpGet]
        public RequestResponse<InvoicePartialMasterAccount> GetVReportInvoicePartial(int folioId, int? CurrencyId = null, DateTime? date =null)
        {
            var headers = Request.Headers;
            String container = String.Empty;
            String hisContainer = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
            {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
            {
                hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();
            }

            using (var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>())
            {
                return service.GetVReportInvoicePartial(folioId, CurrencyId, date);
            }
        }

        [Route("GetTaxDevolutionByRevenueControlDetail/{folioId:int}")]
        [HttpGet]
        public RequestResponse<List<TaxDevolution> > GetTaxDevolutionByRevenueControlDetail(int folioId, [FromUri] int? CurrencyId = null, [FromUri] DateTime? date = null, [FromUri] int? paymentCurrencyId = null)
        {
            var headers = Request.Headers;
            String container = String.Empty;
            String hisContainer = String.Empty;

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
            {
                container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
            }

            if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
            {
                hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();
            }

            using (var service = ContainerRCM.Current(container, hisContainer).Resolve<IFolioAdminService>())
            {
                return service.GetTaxDevolutionByRevenueControlDetail(folioId, CurrencyId, date, paymentCurrencyId: paymentCurrencyId);
            }
        }

    }
}
