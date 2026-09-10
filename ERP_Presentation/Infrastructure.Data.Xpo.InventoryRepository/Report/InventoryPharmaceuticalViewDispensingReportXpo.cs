using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.ViewReportPharmaceuticalDispensing")]
    public class InventoryPharmaceuticalViewDispensingReportXpo : XPLiteObject
    {

        #region Members

        string fRow;
        [Key(true)]
        public string Row
        {
            get { return fRow; }
            set { SetPropertyValue<string>("Row", ref fRow, value); }
        }

        int fId;        
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fDispensacionCode;
        public string DispensacionCode
        {
            get { return fDispensacionCode; }
            set { SetPropertyValue<string>("DispensacionCode", ref fDispensacionCode, value); }
        }

        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        DateTime fServiceDate;
        public DateTime ServiceDate
        {
            get { return fServiceDate; }
            set { SetPropertyValue<DateTime>("ServiceDate", ref fServiceDate, value); }
        }

        string fAdmissionNumber;
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

        string fUnidadFuncional;
        public string UnidadFuncional
        {
            get { return fUnidadFuncional; }
            set { SetPropertyValue<string>("UnidadFuncional", ref fUnidadFuncional, value); }
        }

        string fBed;
        public string Bed
        {
            get { return fBed; }
            set { SetPropertyValue<string>("Bed", ref fBed, value); }
        }

        string fProfesional;
        public string Profesional
        {
            get { return fProfesional; }
            set { SetPropertyValue<string>("Profesional", ref fProfesional, value); }
        }

        string fWarehouseCodeName;
        public string WarehouseCodeName
        {
            get { return fWarehouseCodeName; }
            set { SetPropertyValue<string>("WarehouseCodeName", ref fWarehouseCodeName, value); }
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

        public InventoryPharmaceuticalViewDispensingReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion
    }
}