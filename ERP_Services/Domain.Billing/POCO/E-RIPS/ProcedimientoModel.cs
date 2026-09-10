using Newtonsoft.Json;

namespace Domain.Billing.POCO.E_RIPS
{
    public class ProcedimientoModel
    {
        [JsonProperty(Order = 1)]
        public string codPrestador { get; set; }
        [JsonProperty(Order = 2)]
        public string fechaInicioAtencion { get; set; }
        [JsonProperty(Order = 3)]
        public string idMIPRES { get; set; }
        [JsonProperty(Order = 4)]
        public string numAutorizacion { get; set; }
        [JsonProperty(Order = 5)]
        public string codProcedimiento { get; set; }
        [JsonProperty(Order = 6)]
        public string viaIngresoServicioSalud { get; set; }
        [JsonProperty(Order = 7)]
        public string modalidadGrupoServicioTecSal { get; set; }
        [JsonProperty(Order = 8)]
        public string grupoServicios { get; set; }
        [JsonProperty(Order = 9)]
        public int? codServicio { get; set; }
        [JsonProperty(Order = 10)]
        public string finalidadTecnologiaSalud { get; set; }
        [JsonProperty(Order = 11)]
        public string tipoDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 12)]
        public string numDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 13)]
        public string codDiagnosticoPrincipal { get; set; }
        [JsonProperty(Order = 14)]
        public string codDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 15)]
        public string nomCodDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 16)]
        public string codDiagnosticoRelacionado { get; set; }
        [JsonProperty(Order = 17)]
        public string codDiagnosticoRelacionadoCIE11 { get; set; }
        [JsonProperty(Order = 18)]
        public string nomCodDiagnosticoRelacionadoCIE11 { get; set; }
        [JsonProperty(Order = 19)]
        public string codComplicacion { get; set; }
        [JsonProperty(Order = 20)]
        public string codComplicacionCIE11 { get; set; }
        [JsonProperty(Order = 21)]
        public string nomCodComplicacionCIE11 { get; set; }
        [JsonProperty(Order = 22)]
        public int? vrServicio { get; set; }
        [JsonProperty(Order = 23)]
        public string conceptoRecaudo { get; set; }
        [JsonProperty(Order = 24)]
        public int? valorPagoModerador { get; set; }
        [JsonProperty(Order = 25)]
        public string numFEVPagoModerador { get; set; }
        [JsonProperty(Order = 26)]
        public string codigoVIDA { get; set; }
        [JsonProperty(Order = 27)]
        public int? consecutivo { get; set; }
    }
}
