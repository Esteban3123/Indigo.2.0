using Application.Events.Models.Thirdparty;

namespace Application.Events.Models.Supplier
{
    public class PersonSupplier
    {
        public string IdentificationNumber;
        public MAddress[] Address;
        public Phones[] Phone;
        public Emails[] Email;

    }
}
