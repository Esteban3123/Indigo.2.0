using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
    public class MedicamentosModel
    {
        public string codPrestador { get; set; }
        public string numAutorizacion { get; set; }
        public string idMIPRES { get; set; }
        public string fechaDispensAdmon { get; set; }
        public string codDiagnosticoPrincipal { get; set; }
        public string codDiagnosticoRelacionado { get; set; }
        public string tipoMedicamento { get; set; }
        public string codTecnologiaSalud { get; set; }
        public string nomTecnologiaSalud { get; set; }
        public int? concentracionMedicamento { get; set; }
        public int? unidadMedida { get; set; }
        public string formaFarmaceutica { get; set; }
        public int? unidadMinDispensa { get; set; }
        public int? cantidadMedicamento { get; set; }
        public int? diasTratamiento { get; set; }
        public string tipoDocumentoIdentificacion { get; set; }
        public string numDocumentoIdentificacion { get; set; }
        public int? vrUnitMedicamento { get; set; }
        public int? vrServicio { get; set; }
        public string conceptoRecaudo { get; set; }
        public int? valorPagoModerador { get; set; }
        public string numFEVPagoModerador { get; set; }
        public int? consecutivo { get; set; }
    }
}
