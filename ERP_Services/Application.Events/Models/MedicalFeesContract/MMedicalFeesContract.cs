using System.Collections.Generic;

namespace Application.Events.Models.MedicalFeesContract
{
    public class MMedicalFeesContract
    {
        public string Code;
        public int Status;
        public string CreationUser;
        public string CreationDate;
        public string ModificationUser;
        public string ModificationDate;
        public List<string> HealthProfessionalsCodes;
        public List<int> IpsServicesIds;
    }
}
