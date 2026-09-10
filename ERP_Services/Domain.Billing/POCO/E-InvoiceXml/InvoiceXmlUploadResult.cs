namespace Domain.Billing.POCO.E_InvoiceXml
{
    public enum InvoiceXmlUploadStatus
    {
        Created = 1,
        Skipped = 2,
        Failed = 3
    }

    /// <summary>
    /// Resultado de la subida de un XML de factura DIAN individual.
    /// </summary>
    public class InvoiceXmlUploadResult
    {
        public string NumFactura { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public InvoiceXmlUploadStatus Status { get; set; }
        public string Message { get; set; }
    }
}
