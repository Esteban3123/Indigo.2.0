namespace Domain.Billing.POCO.E_RIPS
{
    /// <summary>
    /// Información mínima de un doc Cosmos existente, usada para política de duplicados.
    /// </summary>
    public class RipsExistsInfo
    {
        public string Id { get; set; }
        public string NumFactura { get; set; }

        /// <summary>
        /// Timestamp Cosmos (_ts) en segundos epoch UTC.
        /// </summary>
        public long Ts { get; set; }
    }
}
