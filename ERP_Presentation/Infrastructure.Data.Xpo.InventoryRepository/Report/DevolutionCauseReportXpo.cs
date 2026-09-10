using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.DevolutionCause")]
    public partial class DevolutionCauseReportXpo : XPLiteObject
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

        #endregion

        #region Custom Members

        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        #endregion

        #region Relationships

        [Association(@"Inventory_RemissionDevolutionDetail_References_Inventory_DevolutionCause", typeof(InventoryRemissionDevolutionDetailReportXpo))]
        public XPCollection<InventoryRemissionDevolutionDetailReportXpo> Inventory_RemissionDevolutionDetailReportXpo { get { return GetCollection<InventoryRemissionDevolutionDetailReportXpo>("Inventory_RemissionDevolutionDetailReportXpo"); } }

        [Association(@"Inventory_EntranceVoucherDevolutionDetail_References_Inventory_DevolutionCause", typeof(InventoryEntranceVoucherDevolutionDetailReportXpo))]
        public XPCollection<InventoryEntranceVoucherDevolutionDetailReportXpo> Inventory_EntranceVoucherDevolutionDetailBatchSerial { get { return GetCollection<InventoryEntranceVoucherDevolutionDetailReportXpo>("Inventory_EntranceVoucherDevolutionDetailBatchSerial"); } }

        #endregion

        #region Builders

        public DevolutionCauseReportXpo(Session session) : base(session) { }

        #endregion

    }

}