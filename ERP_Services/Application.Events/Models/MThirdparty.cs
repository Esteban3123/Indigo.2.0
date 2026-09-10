using Application.Events.Models.Thirdparty;

namespace Application.Events.Models
{
    /// <summary>
    /// Propiedades Tercero
    /// </summary>
    public class MThirdparty
    {
        public Person Person;        
        public string Nit;
        public string DigitVerification;
        public string Name;
        public int PersonType;
        public int RetentionType;
        public int ContributionType;
        public int StateEnterpriseType;
        public IVARetentionAccountPayableConcept IVARetentionAccountPayableConcept;
        public int Ica;
        public decimal IcaPercentage;
        public int IcaTop;
        public decimal IcaTopValue;
        public string EntityCode;
        public EconomicActivity EconomicActivity;
        public int Class;
        public string DigitalSignature;
        public string CodeCIIU;
        public int State;
        public string CreationDate;
        public string CreationUser;
        public int HandlesBranchOffice;
        public BranchOffice[] BranchOffice;
        public string CodeDivipola;
        public IVARetentionConcept IVARetentionConcept;       
        public FiscalResponsibility[] FiscalResponsibility;
    }
}
