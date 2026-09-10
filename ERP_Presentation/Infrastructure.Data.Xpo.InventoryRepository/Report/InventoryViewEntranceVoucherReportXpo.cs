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
    [Persistent("Inventory.ViewReportEntranceVoucher")]
    public class InventoryViewEntranceVoucherReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        DateTime finvoiceVtoDate;
        public DateTime invoiceVtoDate
        {
            get { return finvoiceVtoDate; }
            set { SetPropertyValue<DateTime>("invoiceVtoDate", ref finvoiceVtoDate, value); }
        }
        string fInvoiceNumber;
        public string InvoiceNumber
        {
            get { return fInvoiceNumber; }
            set { SetPropertyValue<string>("InvoiceNumber", ref fInvoiceNumber, value); }
        }
        DateTime fInvoiceDate;
        public DateTime InvoiceDate
        {
            get { return fInvoiceDate; }
            set { SetPropertyValue<DateTime>("InvoiceDate", ref fInvoiceDate, value); }
        }
        string fNit;
        [Size(15)]
        public string Nit
        {
            get { return fNit; }
            set { SetPropertyValue<string>("Nit", ref fNit, value); }
        }
        string fNameThirdParty;
        [Size(300)]
        public string NameThirdParty
        {
            get { return fNameThirdParty; }
            set { SetPropertyValue<string>("NameThirdParty", ref fNameThirdParty, value); }
        }
        string fAddresss;
        public string Addresss
        {
            get { return fAddresss; }
            set { SetPropertyValue<string>("Addresss", ref fAddresss, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
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
        string fBatchCode;
        [Size(50)]
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }
        string fcodeWarehouse;
        [Size(20)]
        public string codeWarehouse
        {
            get { return fcodeWarehouse; }
            set { SetPropertyValue<string>("codeWarehouse", ref fcodeWarehouse, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        string fmeasurementUnit;
        [Size(123)]
        public string measurementUnit
        {
            get { return fmeasurementUnit; }
            set { SetPropertyValue<string>("measurementUnit", ref fmeasurementUnit, value); }
        }
        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
        }
        string fUserCodeNameAux;
        [Size(20)]
        public string UserCodeNameAux
        {
            get { return fUserCodeNameAux; }
            set { SetPropertyValue<string>("UserCodeNameAux", ref fUserCodeNameAux, value); }
        }
        string fConcentration;
        [Size(50)]
        public string Concentration
        {
            get { return fConcentration; }
            set { SetPropertyValue<string>("Concentration", ref fConcentration, value); }
        }
        decimal ftotalValuePurchase;
        public decimal totalValuePurchase
        {
            get { return ftotalValuePurchase; }
            set { SetPropertyValue<decimal>("totalValuePurchase", ref ftotalValuePurchase, value); }
        }
        string fNameProduct;
        [Size(200)]
        public string NameProduct
        {
            get { return fNameProduct; }
            set { SetPropertyValue<string>("NameProduct", ref fNameProduct, value); }
        }
        string fNameWarehouse;
        public string NameWarehouse
        {
            get { return fNameWarehouse; }
            set { SetPropertyValue<string>("NameWarehouse", ref fNameWarehouse, value); }
        }


        public InventoryViewEntranceVoucherReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
