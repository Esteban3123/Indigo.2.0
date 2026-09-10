using Newtonsoft.Json;

namespace Domain.Billing.POCO.E_RIPS
{
    public class HospitalizacionModel
    {
        [JsonProperty(Order = 1)]
        public string codPrestador { get; set; }
        [JsonProperty(Order = 2)]
        public string viaIngresoServicioSalud { get; set; }
        [JsonProperty(Order = 3)]
        public string fechaInicioAtencion { get; set; }
        [JsonProperty(Order = 4)]
        public string numAutorizacion { get; set; }
        [JsonProperty(Order = 5)]
        public string causaMotivoAtencion { get; set; }
        [JsonProperty(Order = 6)]
        public string codDiagnosticoPrincipal { get; set; }
        [JsonProperty(Order = 7)]
        public string codDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 8)]
        public string nomCodDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 9)]
        public string codDiagnosticoPrincipalE { get; set; }
        [JsonProperty(Order = 10)]
        public string codDiagnosticoPrincipalECIE11 { get; set; }
        [JsonProperty(Order = 11)]
        public string nomCodDiagnosticoPrincipalECIE11 { get; set; }
        [JsonProperty(Order = 12)]
        public string codDiagnosticoRelacionadoE1 { get; set; }
        [JsonProperty(Order = 13)]
        public string codDiagnosticoRelacionadoE1CIE11 { get; set; }
        [JsonProperty(Order = 14)]
        public string nomCodDiagnosticoRelacionadoE1CIE11 { get; set; }
        [JsonProperty(Order = 15)]
        public string codDiagnosticoRelacionadoE2 { get; set; }
        [JsonProperty(Order = 16)]
        public string codDiagnosticoRelacionadoE2CIE11 { get; set; }
        [JsonProperty(Order = 17)]
        public string nomCodDiagnosticoRelacionadoE2CIE11 { get; set; }
        [JsonProperty(Order = 18)]
        public string codDiagnosticoRelacionadoE3 { get; set; }
        [JsonProperty(Order = 19)]
        public string codDiagnosticoRelacionadoE3CIE11 { get; set; }
        [JsonProperty(Order = 20)]
        public string nomCodDiagnosticoRelacionadoE3CIE11 { get; set; }
        [JsonProperty(Order = 21)]
        public string codComplicacion { get; set; }
        [JsonProperty(Order = 22)]
        public string codComplicacionCIE11 { get; set; }
        [JsonProperty(Order = 23)]
        public string nomCodComplicacionCIE11 { get; set; }
        [JsonProperty(Order = 24)]
        public string condicionDestinoUsuarioEgreso { get; set; }
        [JsonProperty(Order = 25)]
        public string codDiagnosticoCausaMuerte { get; set; }
        [JsonProperty(Order = 26)]
        public string codDiagnosticoCausaMuerteCIE11 { get; set; }
        [JsonProperty(Order = 27)]
        public string nomCodDiagnosticoCausaMuerteCIE11 { get; set; }
        [JsonProperty(Order = 28)]
        public string fechaEgreso { get; set; }
        [JsonProperty(Order = 29)]
        public string codigoVIDA { get; set; }
        [JsonProperty(Order = 30)]
        public int? consecutivo { get; set; }
    }
}
