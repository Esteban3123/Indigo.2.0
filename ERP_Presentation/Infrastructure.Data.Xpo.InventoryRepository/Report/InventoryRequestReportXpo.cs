using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.InventoryRequest")]
    public class InventoryRequestReportXpo : XPLiteObject
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
        byte fRequestType;
        public byte RequestType
        {
            get { return fRequestType; }
            set { SetPropertyValue<byte>("RequestType", ref fRequestType, value); }
        }
        InventoryPayrollFunctionalUnitXpo fTargetFunctionalUnitId;
        [Association(@"Inventory_RequestReferencesPayroll_FunctionalUnit")]
        public InventoryPayrollFunctionalUnitXpo TargetFunctionalUnitId
        {
            get { return fTargetFunctionalUnitId; }
            set { SetPropertyValue<InventoryPayrollFunctionalUnitXpo>("TargetFunctionalUnitId", ref fTargetFunctionalUnitId, value); }
        }
        byte fMovementType;
        public byte MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<byte>("MovementType", ref fMovementType, value); }
        }
        InventoryWarehouseReportXpo fSourceWarehouseId;
        [Association(@"InventoryRequestReportXpoReferencesInventoryWarehouseReportXpo", typeof(InventoryRequestReportXpo))]
        public InventoryWarehouseReportXpo SourceWarehouseId
        {
            get { return fSourceWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("SourceWarehouseId", ref fSourceWarehouseId, value); }
        }

        InventoryWarehouseReportXpo fTargetWarehouseId;
        [Association(@"InventoryRequestReportXpoReferencesInventoryWarehouseReportXpo2", typeof(InventoryRequestReportXpo))]
        public InventoryWarehouseReportXpo TargetWarehouseId
        {
            get { return fTargetWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("TargetWarehouseId", ref fTargetWarehouseId, value); }
        }

        string fObservation;
        [Size(300)]
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
        [Association(@"Inventory_InventoryRequestDetailReferencesInventory_InventoryRequest", typeof(InventoryRequestDetailReportXpo))]
        public XPCollection<InventoryRequestDetailReportXpo> Inventory_InventoryRequestDetails { get { return GetCollection<InventoryRequestDetailReportXpo>("Inventory_InventoryRequestDetails"); } }

        [Association(@"Inventory_InventoryRequestDetailOtherReferencesInventory_InventoryRequest", typeof(InventoryRequestOtherDetailReportXpo))]
        public XPCollection<InventoryRequestOtherDetailReportXpo> Inventory_InventoryRequestOther { get { return GetCollection<InventoryRequestOtherDetailReportXpo>("Inventory_InventoryRequestOther"); } }


        public InventoryRequestReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
