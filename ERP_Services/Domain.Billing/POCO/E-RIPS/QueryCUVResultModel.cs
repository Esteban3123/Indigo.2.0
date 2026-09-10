using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
   public class QueryCUVResultModel
    {
        public int ProcesoId { get; set; }
        public bool EsValido { get; set; }
        public string CodigoUnicoValidacion { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public string NumDocumentoIdObligado { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime? FechaEmision { get; set; }
        public decimal TotalFactura { get; set; }
        public int CantidadUsuarios { get; set; }
        public int CantidadAtenciones { get; set; }
        public decimal TotalValorServicios { get; set; }
        public string IdentificacionAdquiriente { get; set; }
        public string CodigoPrestador { get; set; }
        public string ModalidadPago { get; set; }
        public string NumDocumentoReferenciado { get; set; }
        public string UrlJson { get; set; }
        public string UrlXml { get; set; }
        public string JsonFile { get; set; }
        public string XmlFileBase64 { get; set; }
        public List<ResultValidation> ResultadosValidacion { get; set; }
    }
}
