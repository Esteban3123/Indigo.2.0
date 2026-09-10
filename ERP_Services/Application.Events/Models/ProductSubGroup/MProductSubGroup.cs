namespace Application.Events.Models.ProductSubGroup
{
    public class MProductSubGroup
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int HandlesBatch { get; set; }
        public int HandlesExpiry { get; set; }
        public int Status { get; set; }
        public string CreationUser { get; set; }
        public string CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ModificationDate { get; set; }
    }
}
