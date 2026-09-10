//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 05-04-2014
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.Warehouse")]
    public class WarehouseXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        [Size(20)]
        [Persistent("Code")]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        [Size(100)]
        [Persistent("Name")]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        string fPrefix;
        [Size(4)]
        [Persistent("Prefix")]
        public string Prefix
        {
            get { return fPrefix; }
            set { SetPropertyValue<string>("Prefix", ref fPrefix, value); }
        }

        byte fStatus;
        [Persistent("Status")]
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        SupplierXpo fSupplierId;
        [Persistent("SupplierId")]
        public SupplierXpo SupplierId
        {
            get { return fSupplierId; }
            set { SetPropertyValue<SupplierXpo>("SupplierId", ref fSupplierId, value); }
        }

        bool fVirtualStore;
        [Persistent("VirtualStore")]
        public bool VirtualStore
        {
            get { return fVirtualStore; }
            set { SetPropertyValue<bool>("VirtualStore", ref fVirtualStore, value); }
        }

        bool fWarehouseConsignment;
        [Persistent("WarehouseConsignment")]
        public bool WarehouseConsignment
        {
            get { return fWarehouseConsignment; }
            set { SetPropertyValue<bool>("WarehouseConsignment", ref fWarehouseConsignment, value); }
        }

        bool fCustodyStore;
        [Persistent("CustodyStore")]
        public bool CustodyStore
        {
            get { return fCustodyStore; }
            set { SetPropertyValue<bool>("CustodyStore", ref fCustodyStore, value); }
        }

        bool fTransitStore;
        [Persistent("TransitStore")]
        public bool TransitStore
        {
            get { return fTransitStore; }
            set { SetPropertyValue<bool>("TransitStore", ref fTransitStore, value); }
        }

        bool fControlStore;
        [Persistent("ControlStore")]
        public bool ControlStore
        {
            get { return fControlStore; }
            set { SetPropertyValue<bool>("ControlStore", ref fControlStore, value); }
        }

        string fCodeCenterAttention;
        [Size(20)]
        [Persistent("CodeCenterAttention")]
        public string CodeCenterAttention
        {
            get { return fCodeCenterAttention; }
            set { SetPropertyValue<string>("CodeCenterAttention", ref fCodeCenterAttention, value); }
        }

        #endregion

        #region Custom Members

        [Size(50)]
        [PersistentAlias("concat(Code, ' - ' ,Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        byte fWareHouseType;
        [Persistent("WareHouseType")]
        public byte WareHouseType
        {
            get { return fWareHouseType; }
            set { SetPropertyValue<byte>("WareHouseType", ref fWareHouseType, value); }
        }
        #endregion

        #region Navigation Members

        [Association(@"Inventory_PharmaceuticalDispensingDevolutionReferencesInventory_Warehouse", typeof(PharmaceuticalDispensingDevolutionXpo))]
        public XPCollection<PharmaceuticalDispensingDevolutionXpo> Inventory_PharmaceuticalDispensingDevolutions { get { return GetCollection<PharmaceuticalDispensingDevolutionXpo>("Inventory_PharmaceuticalDispensingDevolutions"); } }
        [Association(@"Inventory_PharmaceuticalDispensingTransfer_References_Inventory_Warehouse", typeof(PharmaceuticalDispensingTransferXpo))]
        public XPCollection<PharmaceuticalDispensingTransferXpo> Inventory_PharmaceuticalDispensingTransfers { get { return GetCollection<PharmaceuticalDispensingTransferXpo>("Inventory_PharmaceuticalDispensingTransfers"); } }
        [Association(@"Inventory_PhysicalInventoryReferencesInventory_Warehouse", typeof(PhysicalInventoryXpo))]
        public XPCollection<PhysicalInventoryXpo> PhysicalInventoryXpo { get { return GetCollection<PhysicalInventoryXpo>("PhysicalInventoryXpo"); } }
        [Association(@"Inventory_EntranceVoucherDevolutionReferencesInventory_Warehouse", typeof(EntranceVoucherDevolutionXpo))]
        public XPCollection<EntranceVoucherDevolutionXpo> Inventory_EntranceVoucherDevolutions { get { return GetCollection<EntranceVoucherDevolutionXpo>("Inventory_EntranceVoucherDevolutions"); } }
        [Association(@"Inventory_WarehouseUserReferencesInventory_Warehouse", typeof(WarehouseUserXpo))]
        public XPCollection<WarehouseUserXpo> Inventory_WarehouseUsers { get { return GetCollection<WarehouseUserXpo>("Inventory_WarehouseUsers"); } }
        [Association(@"Inventory_RemissionEntranceReferencesInventory_Warehouse", typeof(RemissionEntranceXpo))]
        public XPCollection<RemissionEntranceXpo> Inventory_RemissionEntrances { get { return GetCollection<RemissionEntranceXpo>("Inventory_RemissionEntrances"); } }
        [Association(@"Inventory_ConsignmentInventoryRemissionReferencesInventory_Warehouse", typeof(ConsignmentInventoryRemissionXpo))]
        public XPCollection<ConsignmentInventoryRemissionXpo> Inventory_ConsignmentInventoryRemissions { get { return GetCollection<ConsignmentInventoryRemissionXpo>("Inventory_ConsignmentInventoryRemissions"); } }
        [Association(@"Inventory_RemissionOutputReferencesInventory_Warehouse", typeof(RemissionOutputXpo))]
        public XPCollection<RemissionOutputXpo> Inventory_RemissionOutputs { get { return GetCollection<RemissionOutputXpo>("Inventory_RemissionOutputs"); } }
        [Association(@"Inventory_RemissionDevolutionReferencesInventory_Warehouse", typeof(RemissionDevolutionXpo))]
        public XPCollection<RemissionDevolutionXpo> Inventory_RemissionDevolutions { get { return GetCollection<RemissionDevolutionXpo>("Inventory_RemissionDevolutions"); } }
        [Association(@"Inventory_InventoryControlReferencesInventory_Warehouse", typeof(InventoryControlXpo))]
        public XPCollection<InventoryControlXpo> Inventory_InventoryControls { get { return GetCollection<InventoryControlXpo>("Inventory_InventoryControls"); } }
        [Association(@"Inventory_LoanMerchandise_Warehouse", typeof(InventoryLoanMerchandiseXpo))]
        public XPCollection<InventoryLoanMerchandiseXpo> Inventory_LoanMerchandise { get { return GetCollection<InventoryLoanMerchandiseXpo>("Inventory_LoanMerchandise"); } }
        [Association(@"Inventory_LoanMerchandiseDevolution_Warehouse", typeof(InventoryLoanMerchandiseDevolutionXpo))]
        public XPCollection<InventoryLoanMerchandiseDevolutionXpo> Inventory_LoanMerchandiseDevolution { get { return GetCollection<InventoryLoanMerchandiseDevolutionXpo>("Inventory_LoanMerchandiseDevolution"); } }
        [Association(@"Inventory_DocumentInvoiceProductSalesReferencesInventory_Warehouse", typeof(InventoryDocumentInvoiceProductSalesXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesXpo> Inventory_DocumentInvoiceProductSaless { get { return GetCollection<InventoryDocumentInvoiceProductSalesXpo>("Inventory_DocumentInvoiceProductSaless"); } }
        [Association(@"Inventory_InventoryRequestReferencesInventory_Warehouse", typeof(InventoryRequestXpo))]
        public XPCollection<InventoryRequestXpo> Inventory_InventoryRequests { get { return GetCollection<InventoryRequestXpo>("Inventory_InventoryRequests"); } }
        [Association(@"Inventory_InventoryRequestReferencesInventory_Warehouse1", typeof(InventoryRequestXpo))]
        public XPCollection<InventoryRequestXpo> Inventory_InventoryRequests1 { get { return GetCollection<InventoryRequestXpo>("Inventory_InventoryRequests1"); } }
        [Association(@"Inventory_PhysicalInventoryCustodyReferencesInventory_Warehouse", typeof(PhysicalInventoryCustodyXpo))]
        public XPCollection<PhysicalInventoryCustodyXpo> PhysicalInventoryCustody { get { return GetCollection<PhysicalInventoryCustodyXpo>("PhysicalInventoryCustody"); } }
        [Association(@"TransferOrderReferenceSourceWarehouse", typeof(InventoryTransferOrderXpo))]
        public XPCollection<InventoryTransferOrderXpo> InventoryTransferOrderXpo { get { return GetCollection<InventoryTransferOrderXpo>("InventoryTransferOrderXpo"); } }
        [Association(@"TransferOrderReferenceTargetWarehouse", typeof(InventoryTransferOrderXpo))]
        public XPCollection<InventoryTransferOrderXpo> InventoryTransferOrderXpo2 { get { return GetCollection<InventoryTransferOrderXpo>("InventoryTransferOrderXpo2"); } }

        [Association(@"MedicalFormulaReferencesWarehouse", typeof(MedicalFormulaXpo))]
        public XPCollection<MedicalFormulaXpo> MedicalFormulaXpo { get { return GetCollection<MedicalFormulaXpo>("MedicalFormulaXpo"); } }

        [Association(@"Inventory_ProductInTransitReferencesInventory_Warehouse", typeof(ProductInTransitXpo))]
        public XPCollection<ProductInTransitXpo> ProductInTransitXpo { get { return GetCollection<ProductInTransitXpo>("ProductInTransitXpo"); } }

        [Association(@"ConsignmentTransfer_Reference_Warehouse", typeof(ConsignmentTransferXpo))]
        public XPCollection<ConsignmentTransferXpo> ConsignmentTransfers { get { return GetCollection<ConsignmentTransferXpo>("ConsignmentTransfers"); } }
        #endregion

        #region Builders

        public WarehouseXpo(Session session) : base(session)
        {
        }

        public WarehouseXpo()
            : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        [Association(@"Inventory_EntranceVoucherReferencesInventory_Warehouse", typeof(EntranceVoucherXpo))]
        public XPCollection<EntranceVoucherXpo> Inventory_EntranceVouchers { get { return GetCollection<EntranceVoucherXpo>("Inventory_EntranceVouchers"); } }


        [Association(@"InventoryConsigmentTransferDetailXpoReferencesWarehouse", typeof(InventoryConsigmentTransferDetailXpo))]
        public XPCollection<InventoryConsigmentTransferDetailXpo> InventoryConsigmentTransferDetailXpo
        {
            get { return GetCollection<InventoryConsigmentTransferDetailXpo>("InventoryConsigmentTransferDetailXpo"); }
        }

        #endregion
    }
}
