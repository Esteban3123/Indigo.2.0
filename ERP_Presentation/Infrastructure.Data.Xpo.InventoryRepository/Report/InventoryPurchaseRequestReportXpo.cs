using DevExpress.Xpo;
using System;


namespace Infrastructure.Data.Xpo.InventoryRepository.Report
{
    [Persistent(@"Inventory.PurchaseRequest")]
    public class InventoryPurchaseRequestReportXpo : XPLiteObject
    {

        #region "Properties"

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

        InventoryPayrollFunctionalUnitXpo fFunctionalUnitId;
        [Association(@"Inventory_PurchaseRequestReferencesPayroll_FunctionalUnit")]
        public InventoryPayrollFunctionalUnitXpo FunctionalUnitId
        {
            get { return fFunctionalUnitId; }
            set { SetPropertyValue<InventoryPayrollFunctionalUnitXpo>("FunctionalUnitId", ref fFunctionalUnitId, value); }
        }

        byte fRequestTypeId;
        public byte RequestTypeId
        {
            get { return fRequestTypeId; }
            set { SetPropertyValue<byte>("RequestTypeId", ref fRequestTypeId, value); }
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
        byte fOrdered;
        public byte Ordered
        {
            get { return fOrdered; }
            set { SetPropertyValue<byte>("Ordered", ref fOrdered, value); }
        }

        #endregion

        #region "Navigation Properties"

        [Association(@"Inventory_PurchaseRequestDetailReferencesInventory_PurchaseRequest", typeof(InventoryPurchaseRequestDetailReportXpo))]
        public XPCollection<InventoryPurchaseRequestDetailReportXpo> Inventory_PurchaseRequestDetails { get { return GetCollection<InventoryPurchaseRequestDetailReportXpo>("Inventory_PurchaseRequestDetails"); } }

        #endregion

        #region "Builder"

        public InventoryPurchaseRequestReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #endregion
    }
}
