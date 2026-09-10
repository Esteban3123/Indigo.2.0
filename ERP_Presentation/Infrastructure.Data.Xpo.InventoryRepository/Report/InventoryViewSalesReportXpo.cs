using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
     [Persistent("Inventory.ViewSales")]
    public class InventoryViewSalesReportXpo : XPLiteObject
    {

        int fid;
        [Key(true)]
        public int id
        {
            get { return fid; }
            set { SetPropertyValue<int>("id", ref fid, value); }
        }
        string fCodeProductSales;
        [Size(20)]
        public string CodeProductSales
        {
            get { return fCodeProductSales; }
            set { SetPropertyValue<string>("CodeProductSales", ref fCodeProductSales, value); }
        }
        string fNit;
        [Size(15)]
        public string Nit
        {
            get { return fNit; }
            set { SetPropertyValue<string>("Nit", ref fNit, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fCodeProduct;
        [Size(20)]
        public string CodeProduct
        {
            get { return fCodeProduct; }
            set { SetPropertyValue<string>("CodeProduct", ref fCodeProduct, value); }
        }
        string fIdProduct;
        [Size(20)]
        public string IdProduct
        {
            get { return fIdProduct; }
            set { SetPropertyValue<string>("IdProduct", ref fIdProduct, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
        }
        decimal fSalePrice;
        public decimal SalePrice
        {
            get { return fSalePrice; }
            set { SetPropertyValue<decimal>("SalePrice", ref fSalePrice, value); }
        }
        DateTime fvtoProduct;
        public DateTime vtoProduct
        {
            get { return fvtoProduct; }
            set { SetPropertyValue<DateTime>("vtoProduct", ref fvtoProduct, value); }
        }
        string fFullname;
        [Size(250)]
        public string Fullname
        {
            get { return fFullname; }
            set { SetPropertyValue<string>("Fullname", ref fFullname, value); }
        }
        string fNameThirdParty;
        [Size(300)]
        public string NameThirdParty
        {
            get { return fNameThirdParty; }
            set { SetPropertyValue<string>("NameThirdParty", ref fNameThirdParty, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fIdAlmacen;
        [Size(20)]
        public string IdAlmacen
        {
            get { return fIdAlmacen; }
            set { SetPropertyValue<string>("IdAlmacen", ref fIdAlmacen, value); }
        }
        string fAlmacen;
        public string Almacen
        {
            get { return fAlmacen; }
            set { SetPropertyValue<string>("Almacen", ref fAlmacen, value); }
        }
        string fNameProduct;
        [Size(200)]
        public string NameProduct
        {
            get { return fNameProduct; }
            set { SetPropertyValue<string>("NameProduct", ref fNameProduct, value); }
        }
        string fUserCodeNameAux;
        [Size(20)]
        public string UserCodeNameAux
        {
            get { return fUserCodeNameAux; }
            set { SetPropertyValue<string>("UserCodeNameAux", ref fUserCodeNameAux, value); }
        }




        public InventoryViewSalesReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
