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
    [Persistent(@"Inventory.RemissionEntrance")]
    public class InventoryRemissionEntranceReportXpo : XPLiteObject
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
        InventoryCommonSupplierXpo fSupplierId;
        [Association(@"Inventory_RemissionEntranceReferencesCommon_Supplier")]
        public InventoryCommonSupplierXpo SupplierId
        {
            get { return fSupplierId; }
            set { SetPropertyValue<InventoryCommonSupplierXpo>("SupplierId", ref fSupplierId, value); }
        }
        int fSupplierDistributionLineId;
        public int SupplierDistributionLineId
        {
            get { return fSupplierDistributionLineId; }
            set { SetPropertyValue<int>("SupplierDistributionLineId", ref fSupplierDistributionLineId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"Inventory_RemissionEntraceReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        string fRemissionNumber;
        [Size(50)]
        public string RemissionNumber
        {
            get { return fRemissionNumber; }
            set { SetPropertyValue<string>("RemissionNumber", ref fRemissionNumber, value); }
        }
        string fDescription;
        [Size(200)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        decimal fValue;
        public decimal Value
        {
            get { return fValue; }
            set { SetPropertyValue<decimal>("Value", ref fValue, value); }
        }
        decimal fIvaValue;
        public decimal IvaValue
        {
            get { return fIvaValue; }
            set { SetPropertyValue<decimal>("IvaValue", ref fIvaValue, value); }
        }
        decimal fTotalValue;
        public decimal TotalValue
        {
            get { return fTotalValue; }
            set { SetPropertyValue<decimal>("TotalValue", ref fTotalValue, value); }
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

        CurrencyXpo fCurrency;
        [Association(@"Currency_References_InventoryRemissionEntranceReportXpo")]
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
            get { return Convert.ToString(EvaluateAlias("CurrencyName")); }

        }

        [Association(@"Inventory_RemissionEntranceDetailReferencesInventory_RemissionEntrance", typeof(InventoryRemissionEntranceDetailReportXpo))]
        public XPCollection<InventoryRemissionEntranceDetailReportXpo> Inventory_RemissionEntranceDetails { get { return GetCollection<InventoryRemissionEntranceDetailReportXpo>("Inventory_RemissionEntranceDetails"); } }
        [Association(@"Inventory_RemissionDevolutionReportXpoReferencesInventory_RemissionEntrance", typeof(InventoryRemissionDevolutionReportXpo))]
        public XPCollection<InventoryRemissionDevolutionReportXpo> Inventory_RemissionDevolutionReportXpo { get { return GetCollection<InventoryRemissionDevolutionReportXpo>("Inventory_RemissionDevolutionReportXpo"); } }

        public InventoryRemissionEntranceReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
