using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
    public class LoadRIPSResultModel
    {
        public Boolean ResultState { get; set; }
        public int? ProcesoId { get; set; }
        public string NumFactura { get; set; }
        public string CodigoUnicoValidacion { get; set; }
        public string FechaRadicacion { get; set; }
        public string RutaArchivos { get; set; }
        public List<ResultValidation> ResultadosValidacion { get; set; }
    }

    public class ResultValidation
    {
        public string Clase { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public string PathFuente { get; set; }
        public string Fuente { get; set; }
    }
}
