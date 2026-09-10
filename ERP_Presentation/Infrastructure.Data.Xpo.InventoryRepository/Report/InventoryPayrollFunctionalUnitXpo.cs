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
    [Persistent(@"Payroll.FunctionalUnit")]
    public class InventoryPayrollFunctionalUnitXpo : XPLiteObject
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
        [Size(50)]
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

        int fBranchOfficeId;
        public int BranchOfficeId
        {
            get { return fBranchOfficeId; }
            set { SetPropertyValue<int>("BranchOfficeId", ref fBranchOfficeId, value); }
        }
        int fCostCenterId;
        public int CostCenterId
        {
            get { return fCostCenterId; }
            set { SetPropertyValue<int>("CostCenterId", ref fCostCenterId, value); }
        }
        int fProductionCenterId;
        public int ProductionCenterId
        {
            get { return fProductionCenterId; }
            set { SetPropertyValue<int>("ProductionCenterId", ref fProductionCenterId, value); }
        }
        int fAccountingStructureId;
        public int AccountingStructureId
        {
            get { return fAccountingStructureId; }
            set { SetPropertyValue<int>("AccountingStructureId", ref fAccountingStructureId, value); }
        }
        byte fUnitType;
        public byte UnitType
        {
            get { return fUnitType; }
            set { SetPropertyValue<byte>("UnitType", ref fUnitType, value); }
        }
        bool fState;
        public bool State
        {
            get { return fState; }
            set { SetPropertyValue<bool>("State", ref fState, value); }
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

        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesPayroll_FunctionalUnit", typeof(InventoryPharmaceuticalDispensingDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailReportXpo> Inventory_PharmaceuticalDispensingDetails { get { return GetCollection<InventoryPharmaceuticalDispensingDetailReportXpo>("Inventory_PharmaceuticalDispensingDetails"); } }
        [Association(@"Inventory_TransferOrderReferencesPayroll_FunctionalUnit", typeof(InventoryTransferOrderReportXpo))]
        public XPCollection<InventoryTransferOrderReportXpo> Inventory_TransferOrderUnitFunctional { get { return GetCollection<InventoryTransferOrderReportXpo>("Inventory_TransferOrderUnitFunctional"); } }
        [Association(@"Inventory_RequestReferencesPayroll_FunctionalUnit", typeof(InventoryRequestReportXpo))]
        public XPCollection<InventoryRequestReportXpo> Inventory_RequestUnitFunctional { get { return GetCollection<InventoryRequestReportXpo>("Inventory_RequestUnitFunctional"); } }

        [Association(@"Inventory_PurchaseRequestReferencesPayroll_FunctionalUnit", typeof(Report.InventoryPurchaseRequestReportXpo))]
        public XPCollection<Report.InventoryPurchaseRequestReportXpo> Inventory_PurchaseRequestUnitFunctional { get { return GetCollection<Report.InventoryPurchaseRequestReportXpo>("Inventory_PurchaseRequestUnitFunctional"); } }

        [Association(@"Inventory_DocumentInvoiceProductSalesReportXpoReferencesPayroll_FunctionalUnit", typeof(InventoryDocumentInvoiceProductSalesReportXpo))]
        public XPCollection<InventoryDocumentInvoiceProductSalesReportXpo> Inventory_DocumentInvoiceProductSalesReportXpo { get { return GetCollection<InventoryDocumentInvoiceProductSalesReportXpo>("Inventory_DocumentInvoiceProductSalesReportXpo"); } }

        public InventoryPayrollFunctionalUnitXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
