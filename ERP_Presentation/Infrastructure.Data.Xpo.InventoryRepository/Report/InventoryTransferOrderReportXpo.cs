using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.TransferOrder")]
    public class InventoryTransferOrderReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Indexed(Name = @"IX_TransferOrder", Unique = true)]
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
        [NonPersistent()]
        public String OrderTypeName
        {
            get
            {
                switch (fOrderType)
                {
                    case 1:
                        return "Traslado";
                        break;
                    case 2:
                        return "Consumo";
                        break;
                    default:
                        return String.Empty;
                        break;
                }
            }
        }
        byte fDispatchTo;
        public byte DispatchTo
        {
            get { return fDispatchTo; }
            set { SetPropertyValue<byte>("DispatchTo", ref fDispatchTo, value); }
        }
        InventoryWarehouseReportXpo fSourceWarehouseId;
        [Association(@"InventoryTransferOrderReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo SourceWarehouseId
        {
            get { return fSourceWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("SourceWarehouseId", ref fSourceWarehouseId, value); }
        }
        InventoryWarehouseReportXpo fTargetWarehouseId;
        [Association(@"InventoryTransferOrderReportXpo2ReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo TargetWarehouseId
        {
            get { return fTargetWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("TargetWarehouseId", ref fTargetWarehouseId, value); }
        }
        InventoryPayrollFunctionalUnitXpo fTargetFunctionalUnitId;
        [Association(@"Inventory_TransferOrderReferencesPayroll_FunctionalUnit")]
        public InventoryPayrollFunctionalUnitXpo TargetFunctionalUnitId
        {
            get { return fTargetFunctionalUnitId; }
            set { SetPropertyValue<InventoryPayrollFunctionalUnitXpo>("TargetFunctionalUnitId", ref fTargetFunctionalUnitId, value); }
        }
        InventoryAdjustmentConceptReportXpo fAdjustmentConceptId;
        [Association(@"InventoryTransferOrderReportXpoReferencesInventoryAdjustmentConceptReportXpo")]
        public InventoryAdjustmentConceptReportXpo AdjustmentConceptId
        {
            get { return fAdjustmentConceptId; }
            set { SetPropertyValue<InventoryAdjustmentConceptReportXpo>("AdjustmentConceptId", ref fAdjustmentConceptId, value); }
        }
        InventoryCommonThirdPartyXpo fThirdPartyId;
        [Association(@"InventoryTransferOrderReportXpoReferencesInventoryCommonThirdPartyXpo")]
        public InventoryCommonThirdPartyXpo ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<InventoryCommonThirdPartyXpo>("ThirdPartyId", ref fThirdPartyId, value); }
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
        [Association(@"Inventory_TransferOrderDetailReferencesInventory_TransferOrder", typeof(InventoryTransferOrderDetailReportXpo))]
        public XPCollection<InventoryTransferOrderDetailReportXpo> Inventory_TransferOrderDetails { get { return GetCollection<InventoryTransferOrderDetailReportXpo>("Inventory_TransferOrderDetails"); } }
        [Association(@"Inventory_TransferOrderDevolutionReferencesInventory_TransferOrder", typeof(InventoryTransferOrderDevolutionReportXpo))]
        public XPCollection<InventoryTransferOrderDevolutionReportXpo> Inventory_TransferOrderDevolutions { get { return GetCollection<InventoryTransferOrderDevolutionReportXpo>("Inventory_TransferOrderDevolutions"); } }

        public InventoryTransferOrderReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
