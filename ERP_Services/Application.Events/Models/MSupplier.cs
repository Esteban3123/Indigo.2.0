using Application.Events.Models.Supplier;
using Application.Events.Models.Thirdparty;

namespace Application.Events.Models
{
    /// <summary>
    /// Proveedores
    /// </summary>
    public class MSupplier
    {
        public MThirdpartySupplier ThirdParty = new MThirdpartySupplier();
        public string Code;       
        public string Name;
        public string CodeCMMS;
        public string WebSite;
        public IdentificacionCity City;
        public int PermanentRetention;
        public int TimeLimitDays;
        public int NotIva;
        public int Declarant;
        public int IndependentEmployee;
        public int PrioritizeBankAccount;
        public int TaxCategory;
        public int Status;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
        public int WorkRent;
        public int Pensions;
        public int CapitalRent;
        public int NotLaborRent;
        public int DividendsAndParticipations;
        public int Manufacturer;
        public int Seller;
        public int SelfWithholding;
        public int SelfWithholdingICA;
        public SupplierBankAccount[] SupplierBankAccount;
        public SupplierType[] SupplierDetailType ;
        public DistributionLines[] SuppliersDistributionLines;

    }
}
