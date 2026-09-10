using Newtonsoft.Json;

namespace Domain.Billing.POCO.E_RIPS
{
    public class RecienNacidosModel
    {
        [JsonProperty(Order = 1)]
        public string codPrestador { get; set; }
        [JsonProperty(Order = 2)]
        public string tipoDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 3)]
        public string numDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 4)]
        public string fechaNacimiento { get; set; }
        [JsonProperty(Order = 5)]
        public int? edadGestacional { get; set; }
        [JsonProperty(Order = 6)]
        public int? numConsultasCPrenatal { get; set; }
        [JsonProperty(Order = 7)]
        public string codSexoBiologico { get; set; }
        [JsonProperty(Order = 8)]
        public decimal? peso { get; set; }
        [JsonProperty(Order = 9)]
        public string codDiagnosticoPrincipal { get; set; }
        [JsonProperty(Order = 10)]
        public string codDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 11)]
        public string nomCodDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 12)]
        public string condicionDestinoUsuarioEgreso { get; set; }
        [JsonProperty(Order = 13)]
        public string codDiagnosticoCausaMuerte { get; set; }
        [JsonProperty(Order = 14)]
        public string codDiagnosticoCausaMuerteCIE11 { get; set; }
        [JsonProperty(Order = 15)]
        public string nomCodDiagnosticoCausaMuerteCIE11 { get; set; }
        [JsonProperty(Order = 16)]
        public string fechaEgreso { get; set; }
        [JsonProperty(Order = 17)]
        public string codigoVIDA { get; set; }
        [JsonProperty(Order = 18)]
        public int? consecutivo { get; set; }
    }
}
