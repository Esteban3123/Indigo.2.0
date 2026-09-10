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
    [Persistent(@"Inventory.InventoryAdjustment")]
    public class InventoryAdjustmentReportXpo : XPLiteObject
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
        byte fAdjustmentType;
        public byte AdjustmentType
        {
            get { return fAdjustmentType; }
            set { SetPropertyValue<byte>("AdjustmentType", ref fAdjustmentType, value); }
        }
        InventoryAdjustmentConceptReportXpo fAdjustmentConceptId;
        [Association(@"InventoryAdjustmentReportXpoReferencesInventoryAdjustmentConceptReportXpo")]
        public InventoryAdjustmentConceptReportXpo AdjustmentConceptId
        {
            get { return fAdjustmentConceptId; }
            set { SetPropertyValue<InventoryAdjustmentConceptReportXpo>("AdjustmentConceptId", ref fAdjustmentConceptId, value); }
        }
        InventoryControlReportXpo fInventoryControlId;
        [Association(@"Inventory_InventoryAdjustmentReferencesInventory_InventoryControl")]
        public InventoryControlReportXpo InventoryControlId
        {
            get { return fInventoryControlId; }
            set { SetPropertyValue<InventoryControlReportXpo>("InventoryControlId", ref fInventoryControlId, value); }
        }
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryAdjustmentReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        InventoryCommonThirdPartyXpo fThirdPartyId;
        [Association(@"InventoryAdjustmentReportXpoReferencesInventoryCommonThirdPartyXpo")]
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
        [Association(@"InventoryAdjustmentControlReportXpoReferencesInventoryAdjustmentReportXpo", typeof(InventoryAdjustmentControlReportXpo))]
        public XPCollection<InventoryAdjustmentControlReportXpo> InventoryAdjustmentControlReportXpo { get { return GetCollection<InventoryAdjustmentControlReportXpo>("InventoryAdjustmentControlReportXpo"); } }
        [Association(@"InventoryAdjustmentDetailReportXpoReferencesInventoryAdjustmentReportXpo", typeof(InventoryAdjustmentDetailReportXpo))]
        public XPCollection<InventoryAdjustmentDetailReportXpo> InventoryAdjustmentDetailReportXpo { get { return GetCollection<InventoryAdjustmentDetailReportXpo>("InventoryAdjustmentDetailReportXpo"); } }

        public InventoryAdjustmentReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
