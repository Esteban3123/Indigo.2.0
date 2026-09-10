using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ViewReportPharmaceuticalDispensingNeckBandPatient")]
    public class ViewReportPharmaceuticalDispensingNeckBandPatientXpo : XPLiteObject
    {
        #region Builder

        public ViewReportPharmaceuticalDispensingNeckBandPatientXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion

        #region Members
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fPharmaceuticalDispensingId;
        public int PharmaceuticalDispensingId
        {
            get { return fPharmaceuticalDispensingId; }
            set { SetPropertyValue<int>("PharmaceuticalDispensingId", ref fPharmaceuticalDispensingId, value); }
        }

        string fCodePharmaceuticalDispensing;
        public string CodePharmaceuticalDispensing
        {
            get { return fCodePharmaceuticalDispensing; }
            set { SetPropertyValue<string>("CodePharmaceuticalDispensing", ref fCodePharmaceuticalDispensing, value); }
        }

        string fPacientName;
        public string PacientName
        {
            get { return fPacientName; }
            set { SetPropertyValue<string>("PacientName", ref fPacientName, value); }
        }

        string fPacientDocumentNumber;
        public string PacientDocumentNumber
        {
            get { return fPacientDocumentNumber; }
            set { SetPropertyValue<string>("PacientDocumentNumber", ref fPacientDocumentNumber, value); }
        }

        string fPacientBirthDate;
        public string PacientBirthDate
        {
            get { return fPacientBirthDate; }
            set { SetPropertyValue<string>("PacientBirthDate", ref fPacientBirthDate, value);  }
        }

        string fAdmissionNumber;
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }

        string fProductCode;
        public string ProductCode
        {
            get { return fProductCode; }
            set { SetPropertyValue<string>("ProductCode", ref fProductCode, value); }
        }

        string fProductName;
        public string ProductName
        {
            get { return fProductName; }
            set { SetPropertyValue<string>("ProductName", ref fProductName, value); }
        }

        int fQuantityRequest;
        public int QuantityRequest
        {
            get { return fQuantityRequest; }
            set { SetPropertyValue<int>("QuantityRequest", ref fQuantityRequest, value); }
        }

        int fQuantityDispensing;
        public int QuantityDispensing
        {
            get { return fQuantityDispensing; }
            set { SetPropertyValue<int>("QuantityDispensing", ref fQuantityDispensing, value); }
        }

        string fPrescription;
        public string Prescription
        {
            get { return fPrescription; }
            set { SetPropertyValue<string>("Prescription", ref fPrescription, value); }
        }

        string fApplyInstructions;
        public string ApplyInstructions
        {
            get { return fApplyInstructions; }
            set { SetPropertyValue<string>("ApplyInstructions", ref fApplyInstructions, value); }
        }

        string fPharmaceuticalNotes;
        public string PharmaceuticalNotes
        {
            get { return fPharmaceuticalNotes; }
            set { SetPropertyValue<string>("PharmaceuticalNotes", ref fPharmaceuticalNotes, value); }
        }

        string fVia;
        public string Via
        {
            get { return fVia; }
            set { SetPropertyValue<string>("Via", ref fVia, value); }
        }
        #endregion

    }
}
