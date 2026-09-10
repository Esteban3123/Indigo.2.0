using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Inventory.POCO
{
    public class ProductSalePrice
    {
        public decimal GrossValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TotalSalesPrice { get; set; }
        public decimal GrandTotalSalesPrice { get; set; }
    }
}
