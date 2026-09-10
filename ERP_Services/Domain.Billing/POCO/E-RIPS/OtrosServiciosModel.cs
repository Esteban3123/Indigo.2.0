using Newtonsoft.Json;

namespace Domain.Billing.POCO.E_RIPS
{
    public class OtrosServiciosModel
    {
        [JsonProperty(Order = 1)]
        public string codPrestador { get; set; }
        [JsonProperty(Order = 2)]
        public string numAutorizacion { get; set; }
        [JsonProperty(Order = 3)]
        public string idMIPRES { get; set; }
        [JsonProperty(Order = 4)]
        public string fechaSuministroTecnologia { get; set; }
        [JsonProperty(Order = 5)]
        public string tipoOS { get; set; }
        [JsonProperty(Order = 6)]
        public string codTecnologiaSalud { get; set; }
        [JsonProperty(Order = 7)]
        public string nomTecnologiaSalud { get; set; }
        [JsonProperty(Order = 8)]
        public int? cantidadOS { get; set; }
        [JsonProperty(Order = 9)]
        public string tipoDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 10)]
        public string numDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 11)]
        public int? vrUnitOS { get; set; }
        [JsonProperty(Order = 12)]
        public int vrDispensacion { get; set; }
        [JsonProperty(Order = 13)]
        public int? vrServicio { get; set; }
        [JsonProperty(Order = 14)]
        public string conceptoRecaudo { get; set; }
        [JsonProperty(Order = 15)]
        public int? valorPagoModerador { get; set; }
        [JsonProperty(Order = 16)]
        public string numFEVPagoModerador { get; set; }
        [JsonProperty(Order = 17)]
        public string codigoVIDA { get; set; }
        [JsonProperty(Order = 18)]
        public int? consecutivo { get; set; }
    }
}
