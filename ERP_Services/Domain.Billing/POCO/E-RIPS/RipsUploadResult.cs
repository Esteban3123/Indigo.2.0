namespace Domain.Billing.POCO.E_RIPS
{
    public enum RipsUploadStatus
    {
        Created = 1,
        Overwritten = 2,
        Skipped = 3,
        Failed = 4
    }

    /// <summary>
    /// Resultado de la subida de un JSON RIPS individual.
    /// </summary>
    public class RipsUploadResult
    {
        public string NumFactura { get; set; }
        public string CosmosId { get; set; }
        public RipsUploadStatus Status { get; set; }
        public string Message { get; set; }
    }
}
