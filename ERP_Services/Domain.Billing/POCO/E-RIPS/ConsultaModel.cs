using Newtonsoft.Json;

namespace Domain.Billing.POCO.E_RIPS
{
    public class ConsultaModel
    {
        [JsonProperty(Order = 1)]
        public string codPrestador { get; set; }
        [JsonProperty(Order = 2)]
        public string fechaInicioAtencion { get; set; }
        [JsonProperty(Order = 3)]
        public string numAutorizacion { get; set; }
        [JsonProperty(Order = 4)]
        public string codConsulta { get; set; }
        [JsonProperty(Order = 5)]
        public string modalidadGrupoServicioTecSal { get; set; }
        [JsonProperty(Order = 6)]
        public string grupoServicios { get; set; }
        [JsonProperty(Order = 7)]
        public int? codServicio { get; set; }
        [JsonProperty(Order = 8)]
        public string finalidadTecnologiaSalud { get; set; }
        [JsonProperty(Order = 9)]
        public string causaMotivoAtencion { get; set; }
        [JsonProperty(Order = 10)]
        public string codDiagnosticoPrincipal { get; set; }
        [JsonProperty(Order = 11)]
        public string codDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 12)]
        public string nomCodDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 13)]
        public string codDiagnosticoRelacionado1 { get; set; }
        [JsonProperty(Order = 14)]
        public string codDiagnosticoRelacionado1CIE11 { get; set; }
        [JsonProperty(Order = 15)]
        public string nomCodDiagnosticoRelacionado1CIE11 { get; set; }
        [JsonProperty(Order = 16)]
        public string codDiagnosticoRelacionado2 { get; set; }
        [JsonProperty(Order = 17)]
        public string codDiagnosticoRelacionado2CIE11 { get; set; }
        [JsonProperty(Order = 18)]
        public string nomCodDiagnosticoRelacionado2CIE11 { get; set; }
        [JsonProperty(Order = 19)]
        public string codDiagnosticoRelacionado3 { get; set; }
        [JsonProperty(Order = 20)]
        public string codDiagnosticoRelacionado3CIE11 { get; set; }
        [JsonProperty(Order = 21)]
        public string nomCodDiagnosticoRelacionado3CIE11 { get; set; }
        [JsonProperty(Order = 22)]
        public string tipoDiagnosticoPrincipal { get; set; }
        [JsonProperty(Order = 23)]
        public string tipoDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 24)]
        public string numDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 25)]
        public int? vrServicio { get; set; }
        [JsonProperty(Order = 26)]
        public string conceptoRecaudo { get; set; }
        [JsonProperty(Order = 27)]
        public int? valorPagoModerador { get; set; }
        [JsonProperty(Order = 28)]
        public string numFEVPagoModerador { get; set; }
        [JsonProperty(Order = 29)]
        public int? consecutivo { get; set; }
        [JsonProperty(Order = 30)]
        public string codigoVIDA { get; set; }
    }
}
