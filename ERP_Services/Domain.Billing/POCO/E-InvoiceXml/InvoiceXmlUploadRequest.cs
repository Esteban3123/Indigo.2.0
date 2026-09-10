namespace Domain.Billing.POCO.E_InvoiceXml
{
    /// <summary>
    /// Solicitud individual para subir un XML de factura DIAN al blob storage del saldo inicial.
    /// </summary>
    public class InvoiceXmlUploadRequest
    {
        /// <summary>
        /// Id del PortfolioInitialBalance al que pertenece la factura. Permite localizar la staging row
        /// (PortfolioInitialBalanceAccountReceivable) antes de que existan los registros shadow Invoice/ED
        /// (que solo nacen en el confirm).
        /// </summary>
        public int InitialBalanceId { get; set; }

        /// <summary>
        /// Número de factura (debe coincidir con el ParentDocumentID y el cbc:ID del Invoice embebido).
        /// </summary>
        public string NumFactura { get; set; }

        /// <summary>
        /// Contenido crudo del XML como AttachedDocument envelope DIAN con Invoice embebido.
        /// </summary>
        public string XmlContent { get; set; }
    }

    /// <summary>
    /// Solicitud para pre-chequear existencia de XMLs DIAN en blob storage de un saldo inicial.
    /// </summary>
    public class InvoiceXmlCheckExistRequest
    {
        /// <summary>
        /// Id del PortfolioInitialBalance al que pertenecen las facturas.
        /// </summary>
        public int InitialBalanceId { get; set; }

        /// <summary>
        /// Números de factura a verificar.
        /// </summary>
        public System.Collections.Generic.List<string> InvoiceNumbers { get; set; } = new System.Collections.Generic.List<string>();
    }
}
