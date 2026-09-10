using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ViewReportPharmaceuticalDispensingDevolution")]
    public class InventoryPharmaceuticalViewDispensingDevolutionReportXpo : XPLiteObject
    {
        #region Members

        string fRow;
        [Key(true)]
        public string Row
        {
            get { return fRow; }
            set { SetPropertyValue<string>("Row", ref fRow, value); }
        }

        int fPharmaceuticalDispensingDevolutionId;
        public int PharmaceuticalDispensingDevolutionId
        {
            get { return fPharmaceuticalDispensingDevolutionId; }
            set { SetPropertyValue<int>("PharmaceuticalDispensingDevolutionId", ref fPharmaceuticalDispensingDevolutionId, value); }
        }

        string fDevolutionCode;
        public string DevolutionCode
        {
            get { return fDevolutionCode; }
            set { SetPropertyValue<string>("DevolutionCode", ref fDevolutionCode, value); }
        }

        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        string fAdmissionNumber;
        [Size(10)]
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }

        string fCodepacient;
        public string Codepacient
        {
            get { return fCodepacient; }
            set { SetPropertyValue<string>("Codepacient", ref fCodepacient, value); }
        }

        string fNamePacient;
        public string NamePacient
        {
            get { return fNamePacient; }
            set { SetPropertyValue<string>("NamePacient", ref fNamePacient, value); }
        }

        string fBed;
        public string Bed
        {
            get { return fBed; }
            set { SetPropertyValue<string>("Bed", ref fBed, value); }
        }

        string fWareHouse;
        public string WareHouse
        {
            get { return fWareHouse; }
            set { SetPropertyValue<string>("WareHouse", ref fWareHouse, value); }
        }

        string fObservation;
        public string Observation
        {
            get { return fObservation; }
            set { SetPropertyValue<string>("Observation", ref fObservation, value); }
        }

        string fProductCode;
        public string ProductCode
        {
            get { return fProductCode; }
            set { SetPropertyValue<string>("ProductCode", ref fProductCode, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        string fBatchCode;
        public string BatchCode
        {
            get { return fBatchCode; }
            set { SetPropertyValue<string>("BatchCode", ref fBatchCode, value); }
        }

        DateTime? fExpirationDate;
        public DateTime? ExpirationDate
        {
            get { return fExpirationDate; }
            set { SetPropertyValue<DateTime?>("ExpirationDate", ref fExpirationDate, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        string fCreationUser;
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }

        #endregion

        #region Builder

        public InventoryPharmaceuticalViewDispensingDevolutionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion

    }
}