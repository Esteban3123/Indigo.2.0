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
    [Persistent(@"Inventory.PharmaceuticalDispensingDetailBatchSerial")]
    public class InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo : XPLiteObject      
    {
        public InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryPharmaceuticalDispensingDetailReportXpo fPharmaceuticalDispensingDetailId;
        [Association(@"InventoryPharmaceuticalDispensingDetailBatchSerialReportXpoReferencesInventoryPharmaceuticalDispensingDetailReportXpo")]
        public InventoryPharmaceuticalDispensingDetailReportXpo PharmaceuticalDispensingDetailId
        {
            get { return fPharmaceuticalDispensingDetailId; }
            set { SetPropertyValue<InventoryPharmaceuticalDispensingDetailReportXpo>("PharmaceuticalDispensingDetailId", ref fPharmaceuticalDispensingDetailId, value); }
        }
        InventoryPhysicalInventoryReportXpo fPhysicalInventoryId;
        [Association(@"InventoryPharmaceuticalDispensingDetailBatchSerialReportXpoReferencesInventoryPhysicalInventoryReportXpo")]
        public InventoryPhysicalInventoryReportXpo PhysicalInventoryId
        {
            get { return fPhysicalInventoryId; }
            set { SetPropertyValue<InventoryPhysicalInventoryReportXpo>("PhysicalInventoryId", ref fPhysicalInventoryId, value); }
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

        [Association(@"InventoryPharmaceuticalDispensingDevolutionDetailReportXpoReferencesInventoryPharmaceuticalDispensingDetailBatchSerialReportXpo", typeof(InventoryPharmaceuticalDispensingDevolutionDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo> InventoryPharmaceuticalDispensingDevolutionDetailReportXpo { get { return GetCollection<InventoryPharmaceuticalDispensingDevolutionDetailReportXpo>("InventoryPharmaceuticalDispensingDevolutionDetailReportXpo"); } }

    }
}
