using System.Collections.Generic;

namespace Domain.Billing.POCO.E_RIPS
{
    /// <summary>
    /// Solicitud para cargue masivo (>threshold) de RIPS.
    /// </summary>
    public class RipsBulkRequest
    {
        public string BatchId { get; set; }
        public List<RipsUploadRequest> Items { get; set; } = new List<RipsUploadRequest>();
    }

    /// <summary>
    /// Respuesta agregada de un endpoint de carga (small o bulk).
    /// </summary>
    public class RipsBulkResponse
    {
        public string BatchId { get; set; }
        public int TotalReceived { get; set; }
        public int Succeeded { get; set; }
        public int Skipped { get; set; }
        public int Failed { get; set; }
        public List<RipsUploadResult> Results { get; set; } = new List<RipsUploadResult>();
    }
}
