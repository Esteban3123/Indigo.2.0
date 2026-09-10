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
    [Persistent("Inventory.ViewReportTransferOrder")]
    public class InventoryViewTransferOrderReportXpo : XPLiteObject
    {
        int fid;
        [Key(true)]
        public int id
        {
            get { return fid; }
            set { SetPropertyValue<int>("id", ref fid, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fcodeAuxPharmacy;
        [Size(20)]
        public string codeAuxPharmacy
        {
            get { return fcodeAuxPharmacy; }
            set { SetPropertyValue<string>("codeAuxPharmacy", ref fcodeAuxPharmacy, value); }
        }
        string fFullname;
        [Size(250)]
        public string Fullname
        {
            get { return fFullname; }
            set { SetPropertyValue<string>("Fullname", ref fFullname, value); }
        }
        string fNit;
        [Size(15)]
        public string Nit
        {
            get { return fNit; }
            set { SetPropertyValue<string>("Nit", ref fNit, value); }
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
        string fNameProduct;
        [Size(200)]
        public string NameProduct
        {
            get { return fNameProduct; }
            set { SetPropertyValue<string>("NameProduct", ref fNameProduct, value); }
        }
        DateTime fExpirationDate;
        public DateTime ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime>("ExpirationDate", ref fExpirationDate, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        decimal fProductCost;
        public decimal ProductCost
        {
            get { return fProductCost; }
            set { SetPropertyValue<decimal>("ProductCost", ref fProductCost, value); }
        }
        string fcodeWarehouse;
        [Size(20)]
        public string codeWarehouse
        {
            get { return fcodeWarehouse; }
            set { SetPropertyValue<string>("codeWarehouse", ref fcodeWarehouse, value); }
        }
        string fBatchCode;
        [Size(50)]
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }
        string fConcentration;
        [Size(50)]
        public string Concentration
        {
            get { return fConcentration; }
            set { SetPropertyValue<string>("Concentration", ref fConcentration, value); }
        }
        string fthirdPartyName;
        [Size(300)]
        public string thirdPartyName
        {
            get { return fthirdPartyName; }
            set { SetPropertyValue<string>("thirdPartyName", ref fthirdPartyName, value); }
        }
        string fmeasurementUnit;
        [Size(123)]
        public string measurementUnit
        {
            get { return fmeasurementUnit; }
            set { SetPropertyValue<string>("measurementUnit", ref fmeasurementUnit, value); }
        }
        string fcodeUnitFunctional;
        [Size(20)]
        public string codeUnitFunctional
        {
            get { return fcodeUnitFunctional; }
            set { SetPropertyValue<string>("codeUnitFunctional", ref fcodeUnitFunctional, value); }
        }
        string fName;
        [Size(50)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fNameWarehouse;
        public string NameWarehouse
        {
            get { return fNameWarehouse; }
            set { SetPropertyValue<string>("NameWarehouse", ref fNameWarehouse, value); }
        }
        string fUserCodeNameAux;
        [Size(20)]
        public string UserCodeNameAux
        {
            get { return fUserCodeNameAux; }
            set { SetPropertyValue<string>("UserCodeNameAux", ref fUserCodeNameAux, value); }
        }

        public InventoryViewTransferOrderReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
