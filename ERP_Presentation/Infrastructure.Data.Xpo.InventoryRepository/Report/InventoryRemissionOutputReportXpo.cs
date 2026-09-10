using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using Infrastructure.CrossCutting.Base;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.RemissionOutput")]
    public class InventoryRemissionOutputReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        DateTime fRemissionDate;
        public DateTime RemissionDate
        {
            get { return fRemissionDate; }
            set { SetPropertyValue<DateTime>("RemissionDate", ref fRemissionDate, value); }
        }
        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }
        InventoryCommonCustomerReportXpo fCustomerId;
        [Association(@"InventoryRemissionOutputReportXpoReferencesInventoryCommonCustomerReportXpo")]
        public InventoryCommonCustomerReportXpo CustomerId
        {
            get { return fCustomerId; }
            set { SetPropertyValue<InventoryCommonCustomerReportXpo>("CustomerId", ref fCustomerId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryRemissionOutputReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        string fDescription;
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }
        byte fProductStatus;
        public byte ProductStatus
        {
            get { return fProductStatus; }
            set { SetPropertyValue<byte>("ProductStatus", ref fProductStatus, value); }
        }
        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fModificationUser;
        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }
        DateTime fModificationDate;
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }
        string fConfirmationUser;
        [Size(20)]
        public string ConfirmationUser
        {
            get { return fConfirmationUser; }
            set { SetPropertyValue<string>("ConfirmationUser", ref fConfirmationUser, value); }
        }
        DateTime fConfirmationDate;
        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }
        string fAnnulmentUser;
        [Size(20)]
        public string AnnulmentUser
        {
            get { return fAnnulmentUser; }
            set { SetPropertyValue<string>("AnnulmentUser", ref fAnnulmentUser, value); }
        }
        DateTime fAnnulmentDate;
        public DateTime AnnulmentDate
        {
            get { return fAnnulmentDate; }
            set { SetPropertyValue<DateTime>("AnnulmentDate", ref fAnnulmentDate, value); }
        }

        [PersistentAlias("Inventory_RemissionOutputDetails.Sum(TotalPriceWithDiscount)")]
        public decimal Value
        {
            get { return Convert.ToDecimal(this.EvaluateAlias("Value")); }
        }

        string fCurrencyAbbreviation;
        [NonPersistent()]
        public  string CurrencyAbbreviation
        {
            get { return SessionValues.Instance.CurrencyISO4217; }
            set { fCurrencyAbbreviation = value; }
        }


        string fCurrencyName;
        [NonPersistent()]
        public string CurrencyName
        {
            get { return SessionValues.Instance.CurrencyName; }
            set { fCurrencyName = value; }
        }
        

        int fCurrencyId;
        [NonPersistent()]
        public int CurrencyId
        {
            get { return SessionValues.Instance.OfficialCurrencyId; }
            set { fCurrencyId = value; }
        }

        [Association(@"Inventory_RemissionOutputDetailReferencesInventory_RemissionOutput", typeof(InventoryRemissionOutputDetailReportXpo))]
        public XPCollection<InventoryRemissionOutputDetailReportXpo> Inventory_RemissionOutputDetails { get { return GetCollection<InventoryRemissionOutputDetailReportXpo>("Inventory_RemissionOutputDetails"); } }

        [Association(@"Inventory_RemissionDevolutionReferencesInventory_RemissionOutput", typeof(InventoryRemissionDevolutionReportXpo))]
        public XPCollection<InventoryRemissionDevolutionReportXpo> Inventory_RemissionDevolutions { get { return GetCollection<InventoryRemissionDevolutionReportXpo>("Inventory_RemissionDevolutions"); } }

        public InventoryRemissionOutputReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
