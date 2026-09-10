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
    [Persistent(@"Inventory.Warehouse")]
    public class InventoryWarehouseReportXpo : XPLiteObject
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
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        InventoryPayrollCostCenterReportXpo fCostCenterId;
        [Association(@"InventoryWarehouseReportXpoReferencesInventoryPayrollCostCenterReportXpo")]
        public InventoryPayrollCostCenterReportXpo CostCenterId
        {
            get { return fCostCenterId; }
            set { SetPropertyValue<InventoryPayrollCostCenterReportXpo>("CostCenterId", ref fCostCenterId, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fLoanThirdPartyDebitAccountId;
        [Association(@"InventoryWarehouseReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpoDebit")]
        public InventoryGeneralLedgerMainAccountsReportXpo LoanThirdPartyDebitAccountId
        {
            get { return fLoanThirdPartyDebitAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("LoanThirdPartyDebitAccountId", ref fLoanThirdPartyDebitAccountId, value); }
        }
        InventoryGeneralLedgerMainAccountsReportXpo fLoanThirdPartyCreditAccountId;
        [Association(@"InventoryWarehouseReportXpoReferencesInventoryGeneralLedgerMainAccountsReportXpoCredit")]
        public InventoryGeneralLedgerMainAccountsReportXpo LoanThirdPartyCreditAccountId
        {
            get { return fLoanThirdPartyCreditAccountId; }
            set { SetPropertyValue<InventoryGeneralLedgerMainAccountsReportXpo>("LoanThirdPartyCreditAccountId", ref fLoanThirdPartyCreditAccountId, value); }
        }
        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
        }
        bool fWarehouseConsignment;
        public bool WarehouseConsignment
        {
            get { return fWarehouseConsignment; }
            set { SetPropertyValue<bool>("WarehouseConsignment", ref fWarehouseConsignment, value); }
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
        //Propiedad Añadida
        bool fSeleccionado = false;
        [NonPersistent()]
        public bool Seleccionado
        {
            get { return fSeleccionado; }
            set { this.fSeleccionado = value; }
        }

        #region "Navigation"

        [Association(@"InventoryKardexReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryKardexReportXpo))]
        public XPCollection<InventoryKardexReportXpo> InventoryKardexReportXpo { get { return GetCollection<InventoryKardexReportXpo>("InventoryKardexReportXpo"); } }

        [Association(@"Inventory_RemissionEntraceReferencesInventoryWarehouseReportXpo", typeof(InventoryRemissionEntranceReportXpo))]
        public XPCollection<InventoryRemissionEntranceReportXpo> InventoryRemissionEntraces { get { return GetCollection<InventoryRemissionEntranceReportXpo>("InventoryRemissionEntraces"); } }

        [Association(@"InventoryPhysicalInventoryReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryPhysicalInventoryReportXpo))]
        public XPCollection<InventoryPhysicalInventoryReportXpo> InventoryPhysicalInventoryReportXpo { get { return GetCollection<InventoryPhysicalInventoryReportXpo>("InventoryPhysicalInventoryReportXpo"); } }

        [Association(@"Inventory_RemissionDevolutionReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryRemissionDevolutionReportXpo))]
        public XPCollection<InventoryRemissionDevolutionReportXpo> Inventory_RemissionDevolutionReportXpo { get { return GetCollection<InventoryRemissionDevolutionReportXpo>("Inventory_RemissionDevolutionReportXpo"); } }

        [Association(@"InventoryRemissionOutputReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryRemissionOutputReportXpo))]
        public XPCollection<InventoryRemissionOutputReportXpo> InventoryRemissionOutputReportXpo { get { return GetCollection<InventoryRemissionOutputReportXpo>("InventoryRemissionOutputReportXpo"); } }

        [Association(@"InventoryPurchaseOrderReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryPurchaseOrderReportXpo))]
        public XPCollection<InventoryPurchaseOrderReportXpo> InventoryPurchaseOrderReportXpo { get { return GetCollection<InventoryPurchaseOrderReportXpo>("InventoryPurchaseOrderReportXpo"); } }

        [Association(@"InventoryEntranceVoucherReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryEntranceVoucherReportXpo))]
        public XPCollection<InventoryEntranceVoucherReportXpo> InventoryEntranceVoucherReportXpo { get { return GetCollection<InventoryEntranceVoucherReportXpo>("InventoryEntranceVoucherReportXpo"); } }

        [Association(@"InventoryAdjustmentReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryAdjustmentReportXpo))]
        public XPCollection<InventoryAdjustmentReportXpo> InventoryAdjustmentReportXpo { get { return GetCollection<InventoryAdjustmentReportXpo>("InventoryAdjustmentReportXpo"); } }

        [Association(@"InventoryLoanMerchandiseReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryLoanMerchandiseReportXpo))]
        public XPCollection<InventoryLoanMerchandiseReportXpo> InventoryLoanWarehouseReportXpo { get { return GetCollection<InventoryLoanMerchandiseReportXpo>("InventoryLoanWarehouseReportXpo"); } }

        [Association(@"InventoryTransferOrderReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryTransferOrderReportXpo))]
        public XPCollection<InventoryTransferOrderReportXpo> InventoryTransferOrderWarehouseReportXpo { get { return GetCollection<InventoryTransferOrderReportXpo>("InventoryTransferOrderWarehouseReportXpo"); } }

        [Association(@"InventoryTransferOrderReportXpo2ReferencesInventoryWarehouseReportXpo", typeof(InventoryTransferOrderReportXpo))]
        public XPCollection<InventoryTransferOrderReportXpo> InventoryTransferOrderWarehouseReportXpo2 { get { return GetCollection<InventoryTransferOrderReportXpo>("InventoryTransferOrderWarehouseReportXpo2"); } }

        [Association(@"InventoryLoanMerchandiseDevolutionReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryLoanMerchandiseDevolutionReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDevolutionReportXpo> InventoryLoanMerchandiseDvolutionWarehouseReportXpo { get { return GetCollection<InventoryLoanMerchandiseDevolutionReportXpo>("InventoryLoanMerchandiseDvolutionWarehouseReportXpo"); } }

        [Association(@"InventoryRequestReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryRequestReportXpo))]
        public XPCollection<InventoryRequestReportXpo> InventoryRequestWarehouseReportXpo { get { return GetCollection<InventoryRequestReportXpo>("InventoryRequestWarehouseReportXpo"); } }

        [Association(@"InventoryRequestReportXpoReferencesInventoryWarehouseReportXpo2", typeof(InventoryRequestReportXpo))]
        public XPCollection<InventoryRequestReportXpo> InventoryRequestWarehouseReportXpo2 { get { return GetCollection<InventoryRequestReportXpo>("InventoryRequestWarehouseReportXpo2"); } }

        [Association(@"Inventory_DocumentInvoiceProductSalesReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryDocumentInvoiceProductSalesReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesReportXpo> Inventory_DocumentInvoiceProductSalesReportXpo { get { return GetCollection<InventoryDocumentInvoiceProductSalesReportXpo>("Inventory_DocumentInvoiceProductSalesReportXpo"); } }

        [Association(@"InventoryPharmaceuticalDispensingDevolutionReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryPharmaceuticalDispensingDevolutionReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDevolutionReportXpo> InventoryPharmaceuticalDispensingDevolutionReportXpo { get { return GetCollection<InventoryPharmaceuticalDispensingDevolutionReportXpo>("InventoryPharmaceuticalDispensingDevolutionReportXpo"); } }

        [Association(@"InventoryWarehouseUserReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryWarehouseUserReportXpo))]
        public XPCollection<InventoryWarehouseUserReportXpo> InventoryWarehouseUserReportXpo { get { return GetCollection<InventoryWarehouseUserReportXpo>("InventoryWarehouseUserReportXpo"); } }

        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_Warehouse", typeof(InventoryPharmaceuticalDispensingDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailReportXpo> Inventory_PharmaceuticalDispensingDetails { get { return GetCollection<InventoryPharmaceuticalDispensingDetailReportXpo>("Inventory_PharmaceuticalDispensingDetails"); } }

        [Association(@"Inventory_ConsignmentInventoryRemissionReferencesInventoryWarehouseReportXpo", typeof(InventoryConsignmentInventoryRemissionReportXpo))]
        public XPCollection<InventoryConsignmentInventoryRemissionReportXpo> InventoryConsignmentInventoryRemissions { get { return GetCollection<InventoryConsignmentInventoryRemissionReportXpo>("InventoryConsignmentInventoryRemissions"); } }

        [Association(@"Inventory_InventoryControlReferencesInventory_Warehouse", typeof(InventoryControlReportXpo))]
        public XPCollection<InventoryControlReportXpo> InventoryControlReportXpo { get { return GetCollection<InventoryControlReportXpo>("InventoryControlReportXpo"); } }

        #endregion

        public InventoryWarehouseReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
