using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
    public class OtrosServiciosModel
    {
        public string codPrestador { get; set; }
        public string numAutorizacion { get; set; }
        public string idMIPRES { get; set; }
        public string fechaSuministroTecnologia { get; set; }
        public string tipoOS { get; set; }
        public string codTecnologiaSalud { get; set; }
        public string nomTecnologiaSalud { get; set; }
        public int? cantidadOS { get; set; }
        public string tipoDocumentoIdentificacion { get; set; }
        public string numDocumentoIdentificacion { get; set; }
        public int? vrUnitOS { get; set; }
        public int? vrServicio { get; set; }
        public string conceptoRecaudo { get; set; }
        public int? valorPagoModerador { get; set; }
        public string numFEVPagoModerador { get; set; }
        public int? consecutivo { get; set; }

    }
}
