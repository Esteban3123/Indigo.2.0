namespace Application.Events.Models.ATCEntity
{
    public class MATCEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public PharmacologicalGroup PharmacologicalGroup { get; set; }
        public int Status { get; set; }
        public string CreationUser { get; set; }
        public string CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ModificationDate { get; set; }

    }
}
