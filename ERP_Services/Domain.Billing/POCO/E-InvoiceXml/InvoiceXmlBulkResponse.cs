using System.Collections.Generic;

namespace Domain.Billing.POCO.E_InvoiceXml
{
    /// <summary>
    /// Solicitud para cargue masivo (>threshold) de XMLs de factura DIAN.
    /// </summary>
    public class InvoiceXmlBulkRequest
    {
        public string BatchId { get; set; }
        public List<InvoiceXmlUploadRequest> Items { get; set; } = new List<InvoiceXmlUploadRequest>();
    }

    /// <summary>
    /// Respuesta agregada de un endpoint de carga (small o bulk).
    /// </summary>
    public class InvoiceXmlBulkResponse
    {
        public string BatchId { get; set; }
        public int TotalReceived { get; set; }
        public int Succeeded { get; set; }
        public int Skipped { get; set; }
        public int Failed { get; set; }
        public List<InvoiceXmlUploadResult> Results { get; set; } = new List<InvoiceXmlUploadResult>();
    }
}
