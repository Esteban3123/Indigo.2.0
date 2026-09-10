using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.TransferOrder")]
    public class InventoryTransferOrderXpo : XPLiteObject
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
        byte fOrderType;
        public byte OrderType
        {
            get { return fOrderType; }
            set { SetPropertyValue<byte>("OrderType", ref fOrderType, value); }
        }
        [PersistentAlias("Iif(OrderType = 1, 'Traslado', OrderType = 2, 'Consumo', OrderType = 3, 'Traslado en Transito', '')")]
        public string OrderTypeName { get { return Convert.ToString(EvaluateAlias("OrderTypeName")); } }

        byte fDispatchTo;
        public byte DispatchTo
        {
            get { return fDispatchTo; }
            set { SetPropertyValue<byte>("DispatchTo", ref fDispatchTo, value); }
        }

        [PersistentAlias("Iif(DispatchTo = 1, 'Almacén', 'Unidad Funcional')")]
        public string DispatchToDescription
        {
            get { return Convert.ToString(this.EvaluateAlias("DispatchToDescription")); }
        }

        [PersistentAlias("Iif(DispatchTo = 1, TargetWarehouseId.CodeName, TargetFunctionalUnitId.CodeName)")]
        public string TargetDescription
        {
            get { return Convert.ToString(this.EvaluateAlias("TargetDescription")); }
        }

        WarehouseXpo fSourceWarehouseId;
        [Association(@"TransferOrderReferenceSourceWarehouse")]
        public WarehouseXpo SourceWarehouseId
        {
            get { return fSourceWarehouseId; }
            set { SetPropertyValue<WarehouseXpo>("SourceWarehouseId", ref fSourceWarehouseId, value); }
        }

        WarehouseXpo fTargetWarehouseId;
        [Association(@"TransferOrderReferenceTargetWarehouse")]
        public WarehouseXpo TargetWarehouseId
        {
            get { return fTargetWarehouseId; }
            set { SetPropertyValue<WarehouseXpo>("TargetWarehouseId", ref fTargetWarehouseId, value); }
        }

        FunctionalUnitXpo fTargetFunctionalUnitId;
        [Association(@"TransferOrderReferenceFunctionalUnit")]
        public FunctionalUnitXpo TargetFunctionalUnitId
        {
            get { return fTargetFunctionalUnitId; }
            set { SetPropertyValue<FunctionalUnitXpo>("TargetFunctionalUnitId", ref fTargetFunctionalUnitId, value); }
        }

        int fAdjustmentConceptId;
        public int AdjustmentConceptId
        {
            get { return fAdjustmentConceptId; }
            set { SetPropertyValue<int>("AdjustmentConceptId", ref fAdjustmentConceptId, value); }
        }
        int fThirdPartyId;
        public int ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<int>("ThirdPartyId", ref fThirdPartyId, value); }
        }
        string fDescription;
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        byte fStatus;
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        [PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Entregado', Status = 3, 'Anulado', Status = 4, 'En Transito', '')")]
        public string StatusName { get { return Convert.ToString(EvaluateAlias("StatusName")); } }

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
        [Association(@"InventoryTransferOrderDetailXpoReferencesInventoryTransferOrderXpo", typeof(InventoryTransferOrderDetailXpo))]
        public XPCollection<InventoryTransferOrderDetailXpo> Inventory_TransferOrderDetails { get { return GetCollection<InventoryTransferOrderDetailXpo>("InventoryTransferOrderDetailXpo"); } }

        [Association(@"TransferOrderDevolutionXpoReferencesInventoryTransferOrderXpo", typeof(TransferOrderDevolutionXpo))]
        public XPCollection<TransferOrderDevolutionXpo> TransferOrderDevolutionXpo { get { return GetCollection<TransferOrderDevolutionXpo>("TransferOrderDevolutionXpo"); } }

        public InventoryTransferOrderXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
