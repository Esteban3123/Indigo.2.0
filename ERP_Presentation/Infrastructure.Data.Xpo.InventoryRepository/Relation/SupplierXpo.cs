using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Common.Supplier")]
    public class SupplierXpo : XPLiteObject
    {

        #region Properties

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
        int fIdThirdParty;
        [Indexed(Name = @"IX_Supplier", Unique = true)]
        public int IdThirdParty
        {
            get { return fIdThirdParty; }
            set { SetPropertyValue<int>("IdThirdParty", ref fIdThirdParty, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
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
        int fIdCity;
        public int IdCity
        {
            get { return fIdCity; }
            set { SetPropertyValue<int>("IdCity", ref fIdCity, value); }
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
        [Indexed(Name = @"IX_Manufacturer_State")]
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
        #endregion

        #region Navigation Properties

        [Association(@"Inventory_PurchaseOrderReferencesCommon_Supplier", typeof(InventoryPurchaseOrderXpo))]
        public XPCollection<InventoryPurchaseOrderXpo> Inventory_PurchaserOrder { get { return GetCollection<InventoryPurchaseOrderXpo>("Inventory_PurchaserOrder"); } }

        [Association(@"Inventory_InventoryContractReferencesCommon_Supplier", typeof(InventoryContractXpo))]
        public XPCollection<InventoryContractXpo> Inventory_InventoryContracts { get { return GetCollection<InventoryContractXpo>("Inventory_InventoryContracts"); } }

        [Association(@"Inventory_EntranceVoucherReferencesCommon_Supplier", typeof(EntranceVoucherXpo))]
        public XPCollection<EntranceVoucherXpo> Inventory_EntranceVouchers { get { return GetCollection<EntranceVoucherXpo>("Inventory_EntranceVouchers"); } }

        [Association(@"Common_SuppliersDistributionLinesReferencesCommon_Supplier", typeof(SuppliersDistributionLinesXpo))]
        public XPCollection<SuppliersDistributionLinesXpo> Common_SuppliersDistributionLiness { get { return GetCollection<SuppliersDistributionLinesXpo>("Common_SuppliersDistributionLiness"); } }

        [Association(@"Inventory_RemissionEntranceReferencesCommon_Supplier", typeof(RemissionEntranceXpo))]
        public XPCollection<RemissionEntranceXpo> Inventory_RemissionEntrances { get { return GetCollection<RemissionEntranceXpo>("Inventory_RemissionEntrances"); } }

        [Association(@"Inventory_ConsignmentInventoryRemissionReferencesCommon_Supplier", typeof(ConsignmentInventoryRemissionXpo))]
        public XPCollection<ConsignmentInventoryRemissionXpo> Inventory_ConsignmentInventoryRemissions { get { return GetCollection<ConsignmentInventoryRemissionXpo>("Inventory_ConsignmentInventoryRemissions"); } }

        [Association(@"Inventory_PurchaseOrderDevolutionReferencesCommon_Supplier", typeof(InventoryPurchaseOrderDevolutionXpo))]
        public XPCollection<InventoryPurchaseOrderDevolutionXpo> Inventory_PurchaseOrderDevolutions { get { return GetCollection<InventoryPurchaseOrderDevolutionXpo>("Inventory_PurchaseOrderDevolutions"); } }

        [Association(@"Inventory_InventoryContractAssignment_References_SupplierTransferor", typeof(InventoryContractAssignmentXpo))]
        public XPCollection<InventoryContractAssignmentXpo> InventoryContractAssignmentTransferorXpo { get { return GetCollection<InventoryContractAssignmentXpo>("InventoryContractAssignmentTransferorXpo"); } }

        [Association(@"Inventory_InventoryContractAssignment_References_SupplierAssignee", typeof(InventoryContractAssignmentXpo))]
        public XPCollection<InventoryContractAssignmentXpo> InventoryContractAssignmentAssigneeXpo { get { return GetCollection<InventoryContractAssignmentXpo>("InventoryContractAssignmentAssigneeXpo"); } }

        [Association(@"Inventory_ProductInTransitReferencesCommon_Supplier", typeof(ProductInTransitXpo))]
        public XPCollection<ProductInTransitXpo> ProductInTransitXpo { get { return GetCollection<ProductInTransitXpo>("ProductInTransitXpo"); } }

        #endregion

        #region Builders

        public SupplierXpo(Session session) : base(session) { }

        #endregion

    }

}
