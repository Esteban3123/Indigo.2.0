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
    [Persistent(@"Common.Supplier")]
    public class InventoryCommonSupplierXpo : XPLiteObject
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
        InventoryCommonThirdPartyXpo fIdThirdParty;
        //[Indexed(Name = @"IX_Supplier", Unique = true)]
        [Association(@"Common_SupplierReferencesCommon_ThirdParty")]
        public InventoryCommonThirdPartyXpo IdThirdParty
        {
            get { return fIdThirdParty; }
            set { SetPropertyValue<InventoryCommonThirdPartyXpo>("IdThirdParty", ref fIdThirdParty, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fCodeCMMS;
        [Size(20)]
        public string CodeCMMS
        {
            get { return fCodeCMMS; }
            set { SetPropertyValue<string>("CodeCMMS", ref fCodeCMMS, value); }
        }
        string fWebSite;
        [Size(80)]
        public string WebSite
        {
            get { return fWebSite; }
            set { SetPropertyValue<string>("WebSite", ref fWebSite, value); }
        }
        InventoryCommonCityXpo fIdCity;
        [Association(@"Common_SupplierReferencesCommon_City")]
        public InventoryCommonCityXpo IdCity
        {
            get { return fIdCity; }
            set { SetPropertyValue<InventoryCommonCityXpo>("IdCity", ref fIdCity, value); }
        }
        int fIdManufacturer;
        public int IdManufacturer
        {
            get { return fIdManufacturer; }
            set { SetPropertyValue<int>("IdManufacturer", ref fIdManufacturer, value); }
        }
        bool fPermanentRetention;
        public bool PermanentRetention
        {
            get { return fPermanentRetention; }
            set { SetPropertyValue<bool>("PermanentRetention", ref fPermanentRetention, value); }
        }
        int fTimeLimitDays;
        public int TimeLimitDays
        {
            get { return fTimeLimitDays; }
            set { SetPropertyValue<int>("TimeLimitDays", ref fTimeLimitDays, value); }
        }
        bool fNotIva;
        public bool NotIva
        {
            get { return fNotIva; }
            set { SetPropertyValue<bool>("NotIva", ref fNotIva, value); }
        }
        bool fStatus;
        //[Indexed(Name = @"IX_Manufacturer_State")]
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
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

        [PersistentAlias("Iif([Status] = True, 'Activo', 'Inactivo')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }
        [PersistentAlias("IdThirdParty.Id")]
        public int ThirdPartyId
        {
            get { return Convert.ToInt32(this.EvaluateAlias("ThirdPartyId")); }
        }

        [PersistentAlias("IdThirdParty.Nit")]
        public string ThirdPartyNit
        {
            get { return Convert.ToString(this.EvaluateAlias("ThirdPartyNit")); }
        }

        [Association(@"Inventory_InventoryContractReferencesCommon_Supplier", typeof(InventoryContractReportXpo))]
        public XPCollection<InventoryContractReportXpo> Inventory_InventoryContracts { get { return GetCollection<InventoryContractReportXpo>("Inventory_InventoryContracts"); } }
        [Association(@"Inventory_PurchaseOrderReferencesCommon_Supplier", typeof(InventoryPurchaseOrderReportXpo))]
        public XPCollection<InventoryPurchaseOrderReportXpo> Inventory_PurchaseOrders { get { return GetCollection<InventoryPurchaseOrderReportXpo>("Inventory_PurchaseOrders"); } }
        [Association(@"Inventory_RemissionEntranceReferencesCommon_Supplier", typeof(InventoryRemissionEntranceReportXpo))]
        public XPCollection<InventoryRemissionEntranceReportXpo> Inventory_RemissionEntrances { get { return GetCollection<InventoryRemissionEntranceReportXpo>("Inventory_RemissionEntrances"); } }
        [Association(@"Inventory_EntranceVoucherReferencesCommon_Supplier", typeof(InventoryEntranceVoucherReportXpo))]
        public XPCollection<InventoryEntranceVoucherReportXpo> Inventory_EntranceVouchers { get { return GetCollection<InventoryEntranceVoucherReportXpo>("Inventory_EntranceVouchers"); } }
        [Association(@"Inventory_PurchaseOrderDevolutionReferencesCommon_Supplier", typeof(InventoryPurchaseOrderDevolutionReportXpo))]
        public XPCollection<InventoryPurchaseOrderDevolutionReportXpo> Inventory_PurchaseOrderDevolutions { get { return GetCollection<InventoryPurchaseOrderDevolutionReportXpo>("Inventory_PurchaseOrderDevolutions"); } }
        [Association(@"Inventory_ConsignmentInventoryRemissionReferencesCommon_Supplier", typeof(InventoryConsignmentInventoryRemissionReportXpo))]
        public XPCollection<InventoryConsignmentInventoryRemissionReportXpo> Inventory_ConsignmentInventoryRemissions { get { return GetCollection<InventoryConsignmentInventoryRemissionReportXpo>("Inventory_ConsignmentInventoryRemissions"); } }
        [Association(@"Inventory_InventoryContractAssignment_References_SupplierTransferor", typeof(InventoryContractAssignmentReportXpo))]
        public XPCollection<InventoryContractAssignmentReportXpo> InventoryContractAssignmentTransferorXpo { get { return GetCollection<InventoryContractAssignmentReportXpo>("InventoryContractAssignmentTransferorXpo"); } }
        [Association(@"Inventory_InventoryContractAssignment_References_SupplierAssignee", typeof(InventoryContractAssignmentReportXpo))]
        public XPCollection<InventoryContractAssignmentReportXpo> InventoryContractAssignmentAssigneeXpo { get { return GetCollection<InventoryContractAssignmentReportXpo>("InventoryContractAssignmentAssigneeXpo"); } }


        public InventoryCommonSupplierXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
