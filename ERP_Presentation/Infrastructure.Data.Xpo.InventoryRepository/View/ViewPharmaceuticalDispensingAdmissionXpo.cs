#region "Imports"

using DevExpress.Xpo;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewPharmaceuticalDispensingAdmission")]
    public class ViewPharmaceuticalDispensingAdmissionXpo : XPLiteObject
    {
        #region "Members"

        string fId;
        [Key(true)]
        public string Id
        {
            get { return fId; }
            set { SetPropertyValue<string>("Id", ref fId, value); }
        }

        InventoryPharmaceuticalDispensingReportXpo fPharmaceuticalDispensingId;
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_ViewPharmaceuticalDispensingAdmission")]
        public InventoryPharmaceuticalDispensingReportXpo PharmaceuticalDispensingId
        {
            get { return fPharmaceuticalDispensingId; }
            set { SetPropertyValue<InventoryPharmaceuticalDispensingReportXpo>("PharmaceuticalDispensingId", ref fPharmaceuticalDispensingId, value); }
        }

        string fPatientName;
        public string PatientName
        {
            get { return fPatientName; }
            set { SetPropertyValue<string>("PatientName", ref fPatientName, value); }
        }

        string fBed;
        public string Bed
        {
            get { return fBed; }
            set { SetPropertyValue<string>("Bed", ref fBed, value); }
        }

        #endregion

        #region Builders

        public ViewPharmaceuticalDispensingAdmissionXpo(Session session) : base(session)
        {
        }

        public ViewPharmaceuticalDispensingAdmissionXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
