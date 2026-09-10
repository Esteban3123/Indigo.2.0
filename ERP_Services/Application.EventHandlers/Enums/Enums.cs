namespace Application.EventHandlers.Enums
{
    public class Enums
    {
        public enum EventAction
        {
            added = 1,
            modified,
            delete,
            annulate
        }

        public enum EventType
        {
            Invoice = 1,
            BillingNote = 2,
            ResendInvoice = 3,
            ErrorHandler = 4,
            SupportDocument = 5,
            ResendElectronicDocument = 6
        }
    }
}
