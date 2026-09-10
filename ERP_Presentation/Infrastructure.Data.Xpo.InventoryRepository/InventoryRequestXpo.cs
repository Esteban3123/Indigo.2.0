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
    [Persistent(@"Inventory.InventoryRequest")]
    public class InventoryRequestXpo : XPLiteObject
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

        private DateTime fDocumentDate;

        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        private byte fRequestType;

        public byte RequestType
        {
            get { return fRequestType; }
            set { SetPropertyValue<byte>("RequestType", ref fRequestType, value); }
        }

        [PersistentAlias("Iif(RequestType = 1, 'Unidad Funcional', Iif(RequestType = 2, 'Almacen', ''))")]
        public String RequestTypeName
        {
            get { return Convert.ToString(this.EvaluateAlias("RequestTypeName")); }
        }

        [PersistentAlias("IIF(MovementType = 1, 'Consumo', 'Traslado')")]
        public String MovementTypeName
        {
            get { return Convert.ToString(this.EvaluateAlias("MovementTypeName")); }
        }

        private FunctionalUnitXpo fTargetFunctionalUnitId;

        [Association(@"Inventory_InventoryRequestReferencesPayroll_FunctionalUnit")]
        public FunctionalUnitXpo TargetFunctionalUnitId
        {
            get { return fTargetFunctionalUnitId; }
            set { SetPropertyValue<FunctionalUnitXpo>("TargetFunctionalUnitId", ref fTargetFunctionalUnitId, value); }
        }

        private byte fMovementType;

        public byte MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<byte>("MovementType", ref fMovementType, value); }
        }

        private WarehouseXpo fSourceWarehouseId;

        [Association(@"Inventory_InventoryRequestReferencesInventory_Warehouse1")]
        public WarehouseXpo SourceWarehouseId
        {
            get { return fSourceWarehouseId; }
            set { SetPropertyValue<WarehouseXpo>("SourceWarehouseId", ref fSourceWarehouseId, value); }
        }

        private WarehouseXpo fTargetWarehouseId;

        [Association(@"Inventory_InventoryRequestReferencesInventory_Warehouse")]
        public WarehouseXpo TargetWarehouseId
        {
            get { return fTargetWarehouseId; }
            set { SetPropertyValue<WarehouseXpo>("TargetWarehouseId", ref fTargetWarehouseId, value); }
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

        [PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Confirmado', Iif(Status = 3, 'Anulado', '')))")]
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

        [Association(@"InventoryRequestDetailXpoReferencesInventoryRequestXpo", typeof(InventoryRequestDetailXpo))]
        public XPCollection<InventoryRequestDetailXpo> InventoryRequestDetailXpo { get { return GetCollection<InventoryRequestDetailXpo>("InventoryRequestDetailXpo"); } }

        public InventoryRequestXpo(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}