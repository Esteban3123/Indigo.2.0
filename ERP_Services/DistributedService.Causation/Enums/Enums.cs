namespace DistributedService.Causation.Enums
{
    public class Enums
    {
        public enum EventAction
        {
            Insert = 1,
            Update,
            Delete,
            Annulate
        }

        public enum EventType
        {
            INVOICE = 1
        }
    }
}
