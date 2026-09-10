using Newtonsoft.Json;

namespace Domain.Billing.POCO.E_RIPS
{
    public class MedicamentosModel
    {
        [JsonProperty(Order = 1)]
        public string codPrestador { get; set; }
        [JsonProperty(Order = 2)]
        public string idMIPRES { get; set; }
        [JsonProperty(Order = 3)]
        public string fechaDispensAdmon { get; set; }
        [JsonProperty(Order = 4)]
        public string codDiagnosticoPrincipal { get; set; }
        [JsonProperty(Order = 5)]
        public string codDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 6)]
        public string nomCodDiagnosticoPrincipalCIE11 { get; set; }
        [JsonProperty(Order = 7)]
        public string codDiagnosticoRelacionado { get; set; }
        [JsonProperty(Order = 8)]
        public string codDiagnosticoRelacionadoCIE11 { get; set; }
        [JsonProperty(Order = 9)]
        public string nomCodDiagnosticoRelacionadoCIE11 { get; set; }
        [JsonProperty(Order = 10)]
        public string tipoMedicamento { get; set; }
        [JsonProperty(Order = 11)]
        public string codTecnologiaSalud { get; set; }
        [JsonProperty(Order = 12)]
        public string nomTecnologiaSalud { get; set; }
        [JsonProperty(Order = 13)]
        public int? concentracionMedicamento { get; set; }
        [JsonProperty(Order = 14)]
        public int? unidadMedida { get; set; }
        [JsonProperty(Order = 15)]
        public string formaFarmaceutica { get; set; }
        [JsonProperty(Order = 16)]
        public int? unidadMinDispensa { get; set; }
        [JsonProperty(Order = 17)]
        public int? cantidadMedicamento { get; set; }
        [JsonProperty(Order = 18)]
        public int? diasTratamiento { get; set; }
        [JsonProperty(Order = 19)]
        public string tipoDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 20)]
        public string numDocumentoIdentificacion { get; set; }
        [JsonProperty(Order = 21)]
        public int? vrUnitMedicamento { get; set; }
        [JsonProperty(Order = 22)]
        public int vrDispensacion { get; set; }
        [JsonProperty(Order = 23)]
        public int? vrServicio { get; set; }
        [JsonProperty(Order = 24)]
        public string conceptoRecaudo { get; set; }
        [JsonProperty(Order = 25)]
        public int? valorPagoModerador { get; set; }
        [JsonProperty(Order = 26)]
        public string numFEVPagoModerador { get; set; }
        [JsonProperty(Order = 27)]
        public int? consecutivo { get; set; }
        [JsonProperty(Order = 28)]
        public string codigoVIDA { get; set; }
    }
}
