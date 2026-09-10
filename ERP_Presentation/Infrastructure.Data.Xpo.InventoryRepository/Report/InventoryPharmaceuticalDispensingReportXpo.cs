using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using Infrastructure.Data.Xpo.InventoryRepository.View;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PharmaceuticalDispensing")]
    public class InventoryPharmaceuticalDispensingReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        CommonOperatingUnitReportXpo fOperatingUnitId;
        [Association(@"Inventory_PharmaceuticalDispensingReferencesCommon_OperatingUnit")]
        public CommonOperatingUnitReportXpo OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<CommonOperatingUnitReportXpo>("OperatingUnitId", ref fOperatingUnitId, value); }
        }
        string fAdmissionNumber;
        [Size(10)]
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }
        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }
        bool fAffectInventory;
        public bool AffectInventory
        {
            get { return fAffectInventory; }
            set { SetPropertyValue<bool>("AffectInventory", ref fAffectInventory, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }
        [NonPersistent()]
        public String StatusName
        {
            get
            {
                switch (fStatus)
                {
                    case 1:
                        return "Registrado";
                        break;
                    case 2:
                        return "Confirmado";
                        break;
                    case 3:
                        return "Anulado";
                        break;
                    default:
                        return String.Empty;
                        break;
                }
            }
        }
        string fCreationUser;
        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }
        DateTime fCreationDate;
        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }
        string fModificationUser;
        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }
        DateTime fModificationDate;
        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }
        string fConfirmationUser;
        [Size(20)]
        public string ConfirmationUser
        {
            get { return fConfirmationUser; }
            set { SetPropertyValue<string>("ConfirmationUser", ref fConfirmationUser, value); }
        }
        DateTime fConfirmationDate;
        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }
        string fAnnulmentUser;
        [Size(20)]
        public string AnnulmentUser
        {
            get { return fAnnulmentUser; }
            set { SetPropertyValue<string>("AnnulmentUser", ref fAnnulmentUser, value); }
        }
        DateTime fAnnulmentDate;
        public DateTime AnnulmentDate
        {
            get { return fAnnulmentDate; }
            set { SetPropertyValue<DateTime>("AnnulmentDate", ref fAnnulmentDate, value); }
        }
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_PharmaceuticalDispensing", typeof(InventoryPharmaceuticalDispensingDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailReportXpo> Inventory_PharmaceuticalDispensingDetails { get { return GetCollection<InventoryPharmaceuticalDispensingDetailReportXpo>("Inventory_PharmaceuticalDispensingDetails"); } }

        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesInventory_ViewPharmaceuticalDispensingAdmission", typeof(ViewPharmaceuticalDispensingAdmissionXpo))]
        public XPCollection<ViewPharmaceuticalDispensingAdmissionXpo> Inventory_ViewPharmaceuticalDispensingAdmission { get { return GetCollection<ViewPharmaceuticalDispensingAdmissionXpo>("Inventory_ViewPharmaceuticalDispensingAdmission"); } }

        public InventoryPharmaceuticalDispensingReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


     

    }
}
