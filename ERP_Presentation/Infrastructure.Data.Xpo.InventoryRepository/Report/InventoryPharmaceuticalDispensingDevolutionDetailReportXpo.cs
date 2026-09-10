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
    [Persistent(@"Inventory.PharmaceuticalDispensingDevolutionDetail")]
    public class InventoryPharmaceuticalDispensingDevolutionDetailReportXpo : XPLiteObject
    {
        public InventoryPharmaceuticalDispensingDevolutionDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryPharmaceuticalDispensingDevolutionReportXpo fPharmaceuticalDispensingDevolutionId;
        [Association(@"InventoryPharmaceuticalDispensingDevolutionDetailReportXpoReferencesInventoryPharmaceuticalDispensingDevolutionReportXpo")]
        public InventoryPharmaceuticalDispensingDevolutionReportXpo PharmaceuticalDispensingDevolutionId
        {
            get { return fPharmaceuticalDispensingDevolutionId; }
            set { SetPropertyValue<InventoryPharmaceuticalDispensingDevolutionReportXpo>("PharmaceuticalDispensingDevolutionId", ref fPharmaceuticalDispensingDevolutionId, value); }
        }        
        InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo fPharmaceuticalDispensingDetailBatchSerialId;
        [Association(@"InventoryPharmaceuticalDispensingDevolutionDetailReportXpoReferencesInventoryPharmaceuticalDispensingDetailBatchSerialReportXpo")]
        public InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo PharmaceuticalDispensingDetailBatchSerialId
        {
            get { return fPharmaceuticalDispensingDetailBatchSerialId; }
            set { SetPropertyValue<InventoryPharmaceuticalDispensingDetailBatchSerialReportXpo>("PharmaceuticalDispensingDetailBatchSerialId", ref fPharmaceuticalDispensingDetailBatchSerialId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

    }
}
