using System.Collections.Generic;

namespace Domain.Billing.POCO.E_InvoiceXml
{
    /// <summary>
    /// Resultado del pre-check de existencia de XML factura en blob storage previo a confirmar saldo inicial.
    /// Existing: numFacturas con XML presente en blob. Missing: numFacturas sin XML (cliente debe subirlo).
    /// </summary>
    public class InvoiceXmlCheckExistResponse
    {
        public List<string> Existing { get; set; } = new List<string>();
        public List<string> Missing { get; set; } = new List<string>();
    }
}
