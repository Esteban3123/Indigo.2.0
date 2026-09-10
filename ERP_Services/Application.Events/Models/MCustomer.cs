using Application.Events.Models.Customer;

namespace Application.Events.Models
{
    /// <summary>
    /// Clientes
    /// </summary>
    public class MCustomer
    {
        public string Nit;
        public string Name;
        public string EPSCode;        
        public ThirdParty ThirdParty;
        public MainAccountReceivable MainAccountReceivable;
        public Retentions[] Retentions;
        public int Term;
        public int State;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
    }
}
