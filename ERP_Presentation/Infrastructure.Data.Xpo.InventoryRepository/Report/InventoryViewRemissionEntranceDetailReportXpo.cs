using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewReportRemissionEntranceDetail")]
    public class InventoryViewRemissionEntranceDetailReportXpo: XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCodeSupplier;
        [Size(20)]
        public string CodeSupplier
        {
            get { return fCodeSupplier; }
            set { SetPropertyValue<string>("CodeSupplier", ref fCodeSupplier, value); }
        }
        string fNameSupplier;
        public string NameSupplier
        {
            get { return fNameSupplier; }
            set { SetPropertyValue<string>("NameSupplier", ref fNameSupplier, value); }
        }
        string fCodeRemision;
        [Size(20)]
        public string CodeRemision
        {
            get { return fCodeRemision; }
            set { SetPropertyValue<string>("CodeRemision", ref fCodeRemision, value); }
        }
        DateTime fRemissionDate;
        public DateTime RemissionDate
        {
            get { return fRemissionDate; }
            set { SetPropertyValue<DateTime>("RemissionDate", ref fRemissionDate, value); }
        }
        string fDescription;
        [Size(200)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }
        int fIdWarehouse;
        public int IdWarehouse
        {
            get { return fIdWarehouse; }
            set { SetPropertyValue<int>("IdWarehouse", ref fIdWarehouse, value); }
        }
        string fCodeWarehouse;
        [Size(20)]
        public string CodeWarehouse
        {
            get { return fCodeWarehouse; }
            set { SetPropertyValue<string>("CodeWarehouse", ref fCodeWarehouse, value); }
        }
        string fNameWarehouse;
        public string NameWarehouse
        {
            get { return fNameWarehouse; }
            set { SetPropertyValue<string>("NameWarehouse", ref fNameWarehouse, value); }
        }
        string fCodeProduct;
        [Size(20)]
        public string CodeProduct
        {
            get { return fCodeProduct; }
            set { SetPropertyValue<string>("CodeProduct", ref fCodeProduct, value); }
        }
        string fNameProduct;
        [Size(200)]
        public string NameProduct
        {
            get { return fNameProduct; }
            set { SetPropertyValue<string>("NameProduct", ref fNameProduct, value); }
        }
        int fInitialAmmount;
        public int InitialAmmount
        {
            get { return fInitialAmmount; }
            set { SetPropertyValue<int>("InitialAmmount", ref fInitialAmmount, value); }
        }
        decimal fInitialValue;
        public decimal InitialValue
        {
            get { return fInitialValue; }
            set { SetPropertyValue<decimal>("InitialValue", ref fInitialValue, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        decimal fTotalOutstandingQuantity;
        public decimal TotalOutstandingQuantity
        {
            get { return fTotalOutstandingQuantity; }
            set { SetPropertyValue<decimal>("TotalOutstandingQuantity", ref fTotalOutstandingQuantity, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        string fCodeEntranceVoucher;
        public string CodeEntranceVoucher
        {
            get { return fCodeEntranceVoucher; }
            set { SetPropertyValue<string>("CodeEntranceVoucher", ref fCodeEntranceVoucher, value); }
        }

        CurrencyXpo fCurrency;
        [Association(@"Currency_References_InventoryViewRemissionEntranceDetailReportXpo")]
        [Persistent("CurrencyId")]
        public CurrencyXpo Currency
        {
            get { return fCurrency; }
            set { SetPropertyValue<CurrencyXpo>("Currency", ref fCurrency, value); }
        }

        [PersistentAlias("Currency.Id")]
        public int CurrencyId
        {
            get { return Convert.ToInt32(EvaluateAlias("CurrencyId")); }

        }
        
        [PersistentAlias("Currency.Abbreviation")]
        public string CurrencyAbbreviation
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyAbbreviation")); }

        }

        [PersistentAlias("Currency.Name")]
        public string CurrencyName
        {
            get { return Convert.ToString(EvaluateAlias("CurrencyName"));  }
        }

        [PersistentAlias("concat(CodeSupplier, ' - ',NameSupplier)")]
        public string CodeNameSupplier
        {
            get { return Convert.ToString(EvaluateAlias("CodeNameSupplier"));  }
        }

        [PersistentAlias("concat(CodeWarehouse, ' - ',NameWarehouse)")]
        public string CodeNameWarehouse
        {
            get {return Convert.ToString(EvaluateAlias("CodeNameWarehouse")); }
        }

        [PersistentAlias("concat(CodeProduct, ' - ',NameProduct)")]
        public string CodeNameProduct
        {
            get { return Convert.ToString(EvaluateAlias("CodeNameProduct")); }
        }

        public InventoryViewRemissionEntranceDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
