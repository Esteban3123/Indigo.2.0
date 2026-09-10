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
    [Persistent("Inventory.ViewReportKardexForPharmacy")]
    public class InventoryViewKardexForPharmacyReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fEntityCode;
        [Size(20)]
        public string EntityCode
        {
            get { return fEntityCode; }
            set { SetPropertyValue<string>("EntityCode", ref fEntityCode, value); }
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
        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }
        decimal fAverageCost;
        public decimal AverageCost
        {
            get { return fAverageCost; }
            set { SetPropertyValue<decimal>("AverageCost", ref fAverageCost, value); }
        }
        string fincome;
        [Size(1)]
        public string income
        {
            get { return fincome; }
            set { SetPropertyValue<string>("income", ref fincome, value); }
        }
        string fexpenses;
        [Size(1)]
        public string expenses
        {
            get { return fexpenses; }
            set { SetPropertyValue<string>("expenses", ref fexpenses, value); }
        }
        int fopeningbalance;
        public int openingbalance
        {
            get { return fopeningbalance; }
            set { SetPropertyValue<int>("openingbalance", ref fopeningbalance, value); }
        }
        int ffinalBalance;
        public int finalBalance
        {
            get { return ffinalBalance; }
            set { SetPropertyValue<int>("finalBalance", ref ffinalBalance, value); }
        }
        string fnameWarehouse;
        public string nameWarehouse
        {
            get { return fnameWarehouse; }
            set { SetPropertyValue<string>("nameWarehouse", ref fnameWarehouse, value); }
        }
        string fnameThirdParty;
        [Size(300)]
        public string nameThirdParty
        {
            get { return fnameThirdParty; }
            set { SetPropertyValue<string>("nameThirdParty", ref fnameThirdParty, value); }
        }
        string fNameProduct;
        [Size(200)]
        public string NameProduct
        {
            get { return fNameProduct; }
            set { SetPropertyValue<string>("NameProduct", ref fNameProduct, value); }
        }
        string fConcentration;
        [Size(50)]
        public string Concentration
        {
            get { return fConcentration; }
            set { SetPropertyValue<string>("Concentration", ref fConcentration, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fUserCodeNameAux;
        [Size(20)]
        public string UserCodeNameAux
        {
            get { return fUserCodeNameAux; }
            set { SetPropertyValue<string>("UserCodeNameAux", ref fUserCodeNameAux, value); }
        }
        string fcodeWarehouse;
        [Size(20)]
        public string codeWarehouse
        {
            get { return fcodeWarehouse; }
            set { SetPropertyValue<string>("codeWarehouse", ref fcodeWarehouse, value); }
        }


        public InventoryViewKardexForPharmacyReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
