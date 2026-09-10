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
    [Persistent(@"Inventory.ConsignmentInventoryRemissionDetailBatchSerial")]
    public class InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryConsignmentInventoryRemissionDetailReportXpo fConsignmentInventoryRemissionDetailId;
        [Association(@"Inventory_ConsignmentInventoryRemissionDetailBatchSerialReferencesInventory_ConsignmentInventoryRemissionDetail")]
        public InventoryConsignmentInventoryRemissionDetailReportXpo ConsignmentInventoryRemissionDetailId
        {
            get { return fConsignmentInventoryRemissionDetailId; }
            set { SetPropertyValue<InventoryConsignmentInventoryRemissionDetailReportXpo>("ConsignmentInventoryRemissionDetailId", ref fConsignmentInventoryRemissionDetailId, value); }
        }
        InventoryBatchSerialReportXpo fBatchSerialId;
        [Association(@"Inventory_ConsignmentInventoryRemissionDetailBatchSerialReferencesInventoryBatchSerialReportXpo")]
        public InventoryBatchSerialReportXpo BatchSerialId
        {
            get { return fBatchSerialId; }
            set { SetPropertyValue<InventoryBatchSerialReportXpo>("BatchSerialId", ref fBatchSerialId, value); }
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
        int fUsedQuantity;
        public int UsedQuantity
        {
            get { return fUsedQuantity; }
            set { SetPropertyValue<int>("UsedQuantity", ref fUsedQuantity, value); }
        }
        int fLegalizedQuantity;
        public int LegalizedQuantity
        {
            get { return fLegalizedQuantity; }
            set { SetPropertyValue<int>("LegalizedQuantity", ref fLegalizedQuantity, value); }
        }
        int fReturnedQuantity;
        public int ReturnedQuantity
        {
            get { return fReturnedQuantity; }
            set { SetPropertyValue<int>("ReturnedQuantity", ref fReturnedQuantity, value); }
        }

        [NonPersistent()]
        public int fOutstandingLegalizedQuantity
        {
            get
            {
                return UsedQuantity - LegalizedQuantity;
            }
        }

        [PersistentAlias("concat(ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.SupplierId.IdThirdParty.Nit, ' - ',ConsignmentInventoryRemissionDetailId.ConsignmentInventoryRemissionId.SupplierId.IdThirdParty.Name)")]
        public string NitNameSupplier
        {
            get { return Convert.ToString(EvaluateAlias("NitNameSupplier")); }
        }

        


        [Association(@"Inventory_RemissionDevolutionDetailReportXpoReferencesInventory_ConsignmentInventoryRemissionDetailBatchSerial", typeof(InventoryRemissionDevolutionDetailReportXpo))]
        public XPCollection<InventoryRemissionDevolutionDetailReportXpo> Inventory_RemissionDevolutionDetailReportXpo { get { return GetCollection<InventoryRemissionDevolutionDetailReportXpo>("Inventory_RemissionDevolutionDetailReportXpo"); } }

        public InventoryConsignmentInventoryRemissionDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
