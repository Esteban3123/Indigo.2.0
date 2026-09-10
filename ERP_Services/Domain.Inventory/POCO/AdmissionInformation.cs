using Domain.Crystal.Entities;
using Domain.Entities;
using System;

namespace Domain.Inventory.POCO
{
    public class AdmissionInformation
    {
        public ADINGRESO aDINGRESO { get; set; }
        public Domain.Payroll.Entities.FunctionalUnit functionalUnit { get; set; }
        public CareGroup careGroup { get; set; }
        public int thirdPartyId { get; set; }
        public DateTime? thirdPartyPatienDate { get; set; }
        public byte? genderThirdParty { get; set; }
        public int? healthAdministratorId { get; set; }
    }
}
