namespace Application.Events.Models.ProductType
{
    public class MProductType
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Class { get; set; }
        public int Status { get; set; }
        public string CreationUser { get; set; }
        public string CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ModificationDate { get; set; }
        public int TemperatureRange { get; set; }
    }
}
