namespace Domain.Billing.POCO.E_RIPS
{
    /// <summary>
    /// Solicitud individual para subir un JSON RIPS a CosmosDB.
    /// </summary>
    public class RipsUploadRequest
    {
        public int InitialBalanceId { get; set; }

        /// <summary>
        /// Número de factura (debe coincidir con JsonRIPS.rips.numFactura).
        /// </summary>
        public string NumFactura { get; set; }

        /// <summary>
        /// Contenido crudo del JSON RIPS. Debe ser deserializable a RIPSModel.
        /// </summary>
        public string JsonRipsRaw { get; set; }
    }
}
