using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
    public class HospitalizacionModel
    {

        public string codPrestador { get; set; }

        public string viaIngresoServicioSalud { get; set; }

        public string fechaInicioAtencion { get; set; }

        public string numAutorizacion { get; set; }

        public string causaMotivoAtencion { get; set; }

        public string codDiagnosticoPrincipal { get; set; }

        public string codDiagnosticoPrincipalE { get; set; }

        public string codDiagnosticoRelacionadoE1 { get; set; }

        public string codDiagnosticoRelacionadoE2 { get; set; }

        public string codDiagnosticoRelacionadoE3 { get; set; }

        public string codComplicacion { get; set; }

        public string condicionDestinoUsuarioEgreso { get; set; }

        public string codDiagnosticoCausaMuerte { get; set; }

        public string fechaEgreso { get; set; }

        public int? consecutivo { get; set; }

    }
}
