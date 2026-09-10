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
    [Persistent(@"Inventory.RemissionEntranceDetailBatchSerial")]
    public class InventoryRemissionEntranceDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRemissionEntranceDetailReportXpo fRemissionEntranceDetailId;
        [Association(@"Inventory_RemissionEntranceDetailBatchSerialReferencesInventory_RemissionEntranceDetail")]
        public InventoryRemissionEntranceDetailReportXpo RemissionEntranceDetailId
        {
            get { return fRemissionEntranceDetailId; }
            set { SetPropertyValue<InventoryRemissionEntranceDetailReportXpo>("RemissionEntranceDetailId", ref fRemissionEntranceDetailId, value); }
        }
        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"Inventory_RemissionEntranceDetailBatchSerialReferencesInventoryBatchSerialReportXpo")]
        public InventoryBatchSerialReportXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<InventoryBatchSerialReportXpo>("BatchSerialId", ref fBatchSerialId, value); }
        }


        [PersistentAlias("concat(RemissionEntranceDetailId.RemissionEntranceId.SupplierId.IdThirdParty.Nit, ' - ',RemissionEntranceDetailId.RemissionEntranceId.SupplierId.IdThirdParty.Name)")]
        public string NitNameSupplier
        {
            get { return Convert.ToString(EvaluateAlias("NitNameSupplier")); }
        }



        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
      
        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventory_RemissionEntranceDetailBatchSerial", typeof(InventoryRemissionDevolutionDetailReportXpo))]
        public XPCollection<InventoryRemissionDevolutionDetailReportXpo> Inventory_RemissionDevolutionDetailReportXpo { get { return GetCollection<InventoryRemissionDevolutionDetailReportXpo>("Inventory_RemissionDevolutionDetailReportXpo"); } }

        public InventoryRemissionEntranceDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
