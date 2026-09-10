using System.Collections.Generic;

namespace Domain.Billing.POCO.E_RIPS
{
    /// <summary>
    /// Resultado del pre-check de existencia de RIPS en CosmosDB previo a confirmar saldo inicial.
    /// Existing: numFacturas presentes en Cosmos. Missing: numFacturas no encontrados (cliente debe subir JSON).
    /// </summary>
    public class RipsCheckExistResponse
    {
        public List<string> Existing { get; set; } = new List<string>();
        public List<string> Missing { get; set; } = new List<string>();
    }
}
