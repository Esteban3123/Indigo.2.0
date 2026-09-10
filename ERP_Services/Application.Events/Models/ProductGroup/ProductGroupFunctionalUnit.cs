namespace Application.Events.Models.ProductGroup
{
    public class ProductGroupFunctionalUnit
    {        
        public FunctionalUnit FunctionalUnit { get; set; }
        public GeneralAccount CostAccount { get; set; }
        public GeneralAccount SalesAccount { get; set; }
        public int IsDelete { get; set; }
    }
}
