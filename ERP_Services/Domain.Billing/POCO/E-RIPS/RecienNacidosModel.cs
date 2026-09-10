using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
    public class RecienNacidosModel
    {
        public string codPrestador { get; set; }
        public string tipoDocumentoIdentificacion { get; set; }
        public string numDocumentoIdentificacion { get; set; }
        public string fechaNacimiento { get; set; }
        public int? edadGestacional { get; set; }
        public int? numConsultasCPrenatal { get; set; }
        public string codSexoBiologico { get; set; }
        public decimal? peso { get; set; }
        public string codDiagnosticoPrincipal { get; set; }
        public string condicionDestinoUsuarioEgreso { get; set; }
        public string codDiagnosticoCausaMuerte { get; set; }
        public string fechaEgreso { get; set; }
        public int? consecutivo { get; set; }
    }
}
