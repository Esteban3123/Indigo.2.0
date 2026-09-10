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
    [Persistent(@"dbo.ViewDashBoardPharmacyDevolutionReport")]
    public class InventoryDashboardPharmacyDevolutionReportXpo : XPLiteObject
    {
        public InventoryDashboardPharmacyDevolutionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        public struct PharmacyKey
        {
            [Persistent("Row")]
            public int Row { get; set; }

            [Persistent("ProductCodeName")]
            public string ProductCodeName { get; set; }
        }

        [Key(), Persistent()]
        public PharmacyKey Key { get; set; }

        int fRow;
        public int Row
        {
            get { return fRow; }
            set { SetPropertyValue<int>("Row", ref fRow, value); }
        }

        string fPatientCode;
        public string PatientCode
        {
            get { return fPatientCode; }
            set { SetPropertyValue<string>("PatientCode", ref fPatientCode, value); }
        }

        string fPatientName;
        public string PatientName
        {
            get { return fPatientName; }
            set { SetPropertyValue<string>("PatientName", ref fPatientName, value); }
        }

        string fAdmissionNumber;
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }

        string fFunctionalUnitCodeName;
        public string FunctionalUnitCodeName
        {
            get { return fFunctionalUnitCodeName; }
            set { SetPropertyValue<string>("FunctionalUnitCodeName", ref fFunctionalUnitCodeName, value); }
        }

        string fBedNumber;
        public string BedNumber
        {
            get { return fBedNumber; }
            set { SetPropertyValue<string>("BedNumber", ref fBedNumber, value); }
        }

        DateTime fDevolutionDate;
        public DateTime DevolutionDate
        {
            get { return fDevolutionDate; }
            set { SetPropertyValue<DateTime>("DevolutionDate", ref fDevolutionDate, value); }
        }

        string fProductCodeName;
        public string ProductCodeName
        {
            get { return fProductCodeName; }
            set { SetPropertyValue<string>("ProductCodeName", ref fProductCodeName, value); }
        }

        int fDevolutionQuantity;
        public int DevolutionQuantity
        {
            get { return fDevolutionQuantity; }
            set { SetPropertyValue<int>("DevolutionQuantity", ref fDevolutionQuantity, value); }
        }

        int fPendingQuantity;
        public int PendingQuantity
        {
            get { return fPendingQuantity; }
            set { SetPropertyValue<int>("PendingQuantity", ref fPendingQuantity, value); }
        }

        string fProfessionalCodeName;
        public string ProfessionalCodeName
        {
            get { return fProfessionalCodeName; }
            set { SetPropertyValue<string>("ProfessionalCodeName", ref fProfessionalCodeName, value); }
        }
        
        string fProfessionalCard;
        public string ProfessionalCard
        {
            get { return fProfessionalCard; }
            set { SetPropertyValue<string>("ProfessionalCard", ref fProfessionalCard, value); }
        }

    }
}
