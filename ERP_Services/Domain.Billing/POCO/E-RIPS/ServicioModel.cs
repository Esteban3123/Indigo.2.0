using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO.E_RIPS
{
    public class ServicioModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ConsultaModel> consultas { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProcedimientoModel> procedimientos { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<UrgenciaModel> urgencias { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RecienNacidosModel> recienNacidos { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MedicamentosModel> medicamentos { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OtrosServiciosModel> otrosServicios { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<HospitalizacionModel> hospitalizacion { get; set; }
    }
}
