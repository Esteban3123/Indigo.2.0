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
    [Persistent(@"Payroll.CostCenter")]
    public class InventoryPayrollCostCenterReportXpo : XPLiteObject
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
        [Size(200)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
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
        [Association(@"InventoryProductGroupReportXpoReferencesInventoryPayrollCostCenterReportXpo", typeof(InventoryProductGroupReportXpo))]
        public XPCollection<InventoryProductGroupReportXpo> InventoryProductGroupReportXpo { get { return GetCollection<InventoryProductGroupReportXpo>("InventoryProductGroupReportXpo"); } }
        [Association(@"InventoryWarehouseReportXpoReferencesInventoryPayrollCostCenterReportXpo", typeof(InventoryWarehouseReportXpo))]
        public XPCollection<InventoryWarehouseReportXpo> InventoryWarehouseReportXpo { get { return GetCollection<InventoryWarehouseReportXpo>("InventoryWarehouseReportXpo"); } }
        [Association(@"InventoryAdjustmentConceptReportXpoReferencesInventoryPayrollCostCenterReportXpo", typeof(InventoryAdjustmentConceptReportXpo))]
        public XPCollection<InventoryAdjustmentConceptReportXpo> InventoryAdjustmentConceptReportXpo { get { return GetCollection<InventoryAdjustmentConceptReportXpo>("InventoryAdjustmentConceptReportXpo"); } }

        public InventoryPayrollCostCenterReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
