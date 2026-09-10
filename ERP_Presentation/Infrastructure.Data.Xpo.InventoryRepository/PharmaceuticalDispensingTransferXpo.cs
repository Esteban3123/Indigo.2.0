using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.PharmaceuticalDispensingTransfer")]
    public partial class PharmaceuticalDispensingTransferXpo : XPLiteObject
    {

        #region Members

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }

        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        WarehouseXpo fWarehouseId;
        [Association(@"Inventory_PharmaceuticalDispensingTransfer_References_Inventory_Warehouse")]
        public WarehouseXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<WarehouseXpo>("WarehouseId", ref fWarehouseId, value); }
        }

        string fAdmissionNumber;
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }

        string fAdmissionNumberDestination;
        public string AdmissionNumberDestination
        {
            get { return fAdmissionNumberDestination; }
            set { SetPropertyValue<string>("AdmissionNumberDestination", ref fAdmissionNumberDestination, value); }
        }

        string fObservation;
        public string Observation
        {
            get { return fObservation; }
            set { SetPropertyValue<string>("Observation", ref fObservation, value); }
        }

        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        string fCreationUser;
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

        #endregion

        #region Custom Members

        [PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', '')")]
        public string StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        #endregion

        #region Navigation Members

        [Association(@"Inventory_PharmaceuticalDispensingTransferDetail_References_Inventory_PharmaceuticalDispensingTransfer", typeof(PharmaceuticalDispensingTransferDetailXpo))]
        public XPCollection<PharmaceuticalDispensingTransferDetailXpo> PharmaceuticalDispensingTransferDetails { get { return GetCollection<PharmaceuticalDispensingTransferDetailXpo>("PharmaceuticalDispensingTransferDetails"); } }

        #endregion

        #region Builder

        public PharmaceuticalDispensingTransferXpo(Session session) : base(session) { }

        #endregion
    }
}
