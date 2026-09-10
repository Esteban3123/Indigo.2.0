using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.InventoryContractAssignment")]
    public class InventoryContractAssignmentReportXpo : XPLiteObject
    {

        #region Properties

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fOperatingUnitId;
        public int OperatingUnitId
        {
            get { return fOperatingUnitId; }
            set { SetPropertyValue<int>("OperatingUnitId", ref fOperatingUnitId, value); }
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

        InventoryContractReportXpo fContractId;
        [Association(@"Inventory_InventoryContractAssignment_References_Inventory_InventoryContract")]
        public InventoryContractReportXpo ContractId
        {
            get { return fContractId; }
            set { SetPropertyValue<InventoryContractReportXpo>("ContractId", ref fContractId, value); }
        }

        InventoryCommonSupplierXpo fSupplierTransferorId;
        [Association(@"Inventory_InventoryContractAssignment_References_SupplierTransferor")]
        public InventoryCommonSupplierXpo SupplierTransferorId
        {
            get { return fSupplierTransferorId; }
            set { SetPropertyValue<InventoryCommonSupplierXpo>("SupplierTransferorId", ref fSupplierTransferorId, value); }
        }

        int fSupplierDistributionLineTransferorId;
        public int SupplierDistributionLineTransferorId
        {
            get { return fSupplierDistributionLineTransferorId; }
            set { SetPropertyValue<int>("SupplierDistributionLineTransferorId", ref fSupplierDistributionLineTransferorId, value); }
        }

        InventoryCommonSupplierXpo fSupplierAssigneeId;
        [Association(@"Inventory_InventoryContractAssignment_References_SupplierAssignee")]
        public InventoryCommonSupplierXpo SupplierAssigneeId
        {
            get { return fSupplierAssigneeId; }
            set { SetPropertyValue<InventoryCommonSupplierXpo>("SupplierAssigneeId", ref fSupplierAssigneeId, value); }
        }

        int fSupplierDistributionLineAssigneeId;
        public int SupplierDistributionLineAssigneeId
        {
            get { return fSupplierDistributionLineAssigneeId; }
            set { SetPropertyValue<int>("SupplierDistributionLineAssigneeId", ref fSupplierDistributionLineAssigneeId, value); }
        }

        string fDescription;
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
        public String StatusName
        {
            get { return Convert.ToString(this.EvaluateAlias("StatusName")); }
        }

        #endregion

        #region Builders

        public InventoryContractAssignmentReportXpo(Session session) : base(session) { }

        #endregion
    }
}

