using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.InventoryControl")]
    public partial class InventoryControlReportXpo : XPLiteObject
    {

        #region "Members"

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

        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        InventoryWarehouseReportXpo  fWarehouseId;
        [Association(@"Inventory_InventoryControlReferencesInventory_Warehouse")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }

        byte fDocumentType;
        public byte DocumentType
        {
            get { return fDocumentType; }
            set { SetPropertyValue<byte>("DocumentType", ref fDocumentType, value); }
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

        #region "Custom Members"

        [PersistentAlias("Iif(DocumentType = 1, 'Saldo Inicial', DocumentType = 2, 'Inventario Físico', '')")]
        public String DocumentTypeName
        {
            get { return Convert.ToString(this.EvaluateAlias("DocumentTypeName")); }
        }

        [PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', '')")]
        public String StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        #endregion

        #region "Navigation"

        [Association(@"Inventory_InventoryControlDetailReferencesInventory_InventoryControl", typeof(InventoryControlDetailReportXpo))]
        public XPCollection<InventoryControlDetailReportXpo> InventoryControlDetails { get { return GetCollection<InventoryControlDetailReportXpo>("InventoryControlDetails"); } }

        [Association(@"Inventory_InventoryAdjustmentReferencesInventory_InventoryControl", typeof(InventoryAdjustmentReportXpo))]
        public XPCollection<InventoryAdjustmentReportXpo> InventoryAdjustment { get { return GetCollection<InventoryAdjustmentReportXpo>("InventoryAdjustment"); } }

        #endregion

        #region "Builders"

        public InventoryControlReportXpo(Session session) : base(session) { }

        #endregion
    }

}
