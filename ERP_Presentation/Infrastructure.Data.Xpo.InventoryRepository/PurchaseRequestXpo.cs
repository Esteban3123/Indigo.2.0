using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PurchaseRequest")]
    public class PurchaseRequestXpo : XPLiteObject
    {
        private int fId;

        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        private string fCode;

        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private int fOperatingUnitId;

        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
        }

        private byte fRequestTypeId;

        public byte RequestTypeId
        {
            get { return fRequestTypeId; }
            set { SetPropertyValue<byte>("RequestTypeId", ref fRequestTypeId, value); }
        }

        [PersistentAlias("Iif(RequestTypeId = 1, 'Producto', Iif(RequestTypeId = 2, 'Activo fijo', Iif(RequestTypeId = 3, 'Otro', '')))")]
        public String RequestTypeName
        {
            get { return Convert.ToString(this.EvaluateAlias("RequestTypeName")); }
        }

        private int fFunctionalUnitId;

        public int FunctionalUnitId
        {
            get { return fFunctionalUnitId; }
            set { SetPropertyValue<int>("FunctionalUnitId", ref fFunctionalUnitId, value); }
        }

        private string fObservation;

        [Size(300)]
        public string Observation
        {
            get { return fObservation; }
            set { SetPropertyValue<string>("Observation", ref fObservation, value); }
        }

        private byte fStatus;

        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        [PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Aprobado', Iif(Status = 3, 'Anulado', Iif(Status = 4, 'En tramite', ''))))")]
        public String StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        private string fCreationUser;

        [Size(20)]
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }

        private DateTime fCreationDate;

        public DateTime CreationDate
        {
            get { return fCreationDate; }
            set { SetPropertyValue<DateTime>("CreationDate", ref fCreationDate, value); }
        }

        private string fModificationUser;

        [Size(20)]
        public string ModificationUser
        {
            get { return fModificationUser; }
            set { SetPropertyValue<string>("ModificationUser", ref fModificationUser, value); }
        }

        private DateTime fModificationDate;

        public DateTime ModificationDate
        {
            get { return fModificationDate; }
            set { SetPropertyValue<DateTime>("ModificationDate", ref fModificationDate, value); }
        }

        private string fConfirmationUser;

        [Size(20)]
        public string ConfirmationUser
        {
            get { return fConfirmationUser; }
            set { SetPropertyValue<string>("ConfirmationUser", ref fConfirmationUser, value); }
        }

        private DateTime fConfirmationDate;

        public DateTime ConfirmationDate
        {
            get { return fConfirmationDate; }
            set { SetPropertyValue<DateTime>("ConfirmationDate", ref fConfirmationDate, value); }
        }

        private string fAnnulmentUser;

        [Size(20)]
        public string AnnulmentUser
        {
            get { return fAnnulmentUser; }
            set { SetPropertyValue<string>("AnnulmentUser", ref fAnnulmentUser, value); }
        }

        private DateTime fAnnulmentDate;

        public DateTime AnnulmentDate
        {
            get { return fAnnulmentDate; }
            set { SetPropertyValue<DateTime>("AnnulmentDate", ref fAnnulmentDate, value); }
        }

        [Association(@"PurchaseRequestDetailXpoReferencesPurchaseRequestXpo", typeof(PurchaseRequestDetailXpo))]
        public XPCollection<PurchaseRequestDetailXpo> PurchaseRequestDetailXpo { get { return GetCollection<PurchaseRequestDetailXpo>("PurchaseRequestDetailXpo"); } }

        public PurchaseRequestXpo(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}