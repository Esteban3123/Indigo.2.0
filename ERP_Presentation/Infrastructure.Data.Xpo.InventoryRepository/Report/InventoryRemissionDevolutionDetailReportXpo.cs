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
    [Persistent(@"Inventory.RemissionDevolutionDetail")]
    public class InventoryRemissionDevolutionDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRemissionDevolutionReportXpo fRemissionDevolutionId;
        [Association(@"Inventory_RemissionDevolutionDetailReferencesInventory_RemissionDevolution")]
        public InventoryRemissionDevolutionReportXpo RemissionDevolutionId
        {
            get { return fRemissionDevolutionId; }
            set { SetPropertyValue<InventoryRemissionDevolutionReportXpo>("RemissionDevolutionId", ref fRemissionDevolutionId, value); }
        }
        InventoryProductReportXpo fProductId;
        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        InventoryRemissionEntranceDetailBatchSerialReportXpo fRemissionEntranceDetailBatchSerialId;
        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventory_RemissionEntranceDetailBatchSerial")]
        public InventoryRemissionEntranceDetailBatchSerialReportXpo RemissionEntranceDetailBatchSerialId
        {
            get { return fRemissionEntranceDetailBatchSerialId; }
            set { SetPropertyValue<InventoryRemissionEntranceDetailBatchSerialReportXpo>("RemissionEntranceDetailBatchSerialId", ref fRemissionEntranceDetailBatchSerialId, value); }
        }
        InventoryRemissionOutputDetailPhysicalReportXpo fRemissionOutputDetailPhysicalId;
        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventory_RemissionOutputDetailPhysical")]
        public InventoryRemissionOutputDetailPhysicalReportXpo RemissionOutputDetailPhysicalId
        {
            get { return fRemissionOutputDetailPhysicalId; }
            set { SetPropertyValue<InventoryRemissionOutputDetailPhysicalReportXpo>("RemissionOutputDetailPhysicalId", ref fRemissionOutputDetailPhysicalId, value); }
        }
        InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo fConsignmentInventoryRemissionDetailBatchSerialId;
        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventory_ConsignmentInventoryRemissionDetailBatchSerial")]
        public InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo ConsignmentInventoryRemissionDetailBatchSerialId
        {
            get { return fConsignmentInventoryRemissionDetailBatchSerialId; }
            set { SetPropertyValue<InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo>("ConsignmentInventoryRemissionDetailBatchSerialId", ref fConsignmentInventoryRemissionDetailBatchSerialId, value); }
        }
        DevolutionCauseReportXpo fDevolutionCauseId;
        [Association(@"Inventory_RemissionDevolutionDetail_References_Inventory_DevolutionCause")]
        public DevolutionCauseReportXpo DevolutionCauseId
        {
            get { return fDevolutionCauseId; }
            set { SetPropertyValue<DevolutionCauseReportXpo>("DevolutionCauseId", ref fDevolutionCauseId, value); }
        }

        public InventoryRemissionDevolutionDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
