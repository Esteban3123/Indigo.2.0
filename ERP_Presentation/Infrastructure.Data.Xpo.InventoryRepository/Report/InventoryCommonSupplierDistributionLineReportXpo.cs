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
    [Persistent(@"Common.SuppliersDistributionLines")]
    public class InventoryCommonSupplierDistributionLineReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        int fIdSupplier;
        public int IdSupplier
        {
            get { return fIdSupplier; }
            set { SetPropertyValue<int>("IdSupplier", ref fIdSupplier, value); }
        }
        InventoryCommonDistributionLineReportXpo fIdDistributionLine;
        [Association(@"Common_SuppliersDistributionLinesReferencesCommon_DistributionLines")]
        public InventoryCommonDistributionLineReportXpo IdDistributionLine
        {
            get { return fIdDistributionLine; }
            set { SetPropertyValue<InventoryCommonDistributionLineReportXpo>("IdDistributionLine", ref fIdDistributionLine, value); }
        }
        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
        }

        public InventoryCommonSupplierDistributionLineReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association(@"Inventory_EntranceVoucherReferencesCommon_SuppliersDistributionLines", typeof(InventoryEntranceVoucherReportXpo))]
        public XPCollection<InventoryEntranceVoucherReportXpo> Inventory_EntranceVouchers { get { return GetCollection<InventoryEntranceVoucherReportXpo>("Inventory_EntranceVouchers"); } }
    }
}
