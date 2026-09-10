using System;
using System.Collections.Generic;

namespace DistributedService.Causation.Models
{
    /// <summary>
    /// Modelo de request para procesar actualizaciones de contratos
    /// </summary>
    public class ContractUpdateRequest
    {
        public string Action { get; set; }
        public List<string> HealthProfessionalsCodes { get; set; }
        public List<int> IpsServicesIds { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public string Database { get; set; }

        public ContractUpdateRequest()
        {
            HealthProfessionalsCodes = new List<string>();
            IpsServicesIds = new List<int>();
        }
    }
}
