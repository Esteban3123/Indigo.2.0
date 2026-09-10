using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.LoanMerchandiseDevolution")]
    public class InventoryLoanMerchandiseDevolutionReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Indexed(Name = @"IX_LoanMerchandiseDevolution", Unique = true)]
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
        InventoryWarehouseReportXpo fWarehouseId;
        [Association(@"InventoryLoanMerchandiseDevolutionReportXpoReferencesInventoryWarehouseReportXpo")]
        public InventoryWarehouseReportXpo WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<InventoryWarehouseReportXpo>("WarehouseId", ref fWarehouseId, value); }
        }
        InventoryLoanMerchandiseReportXpo fLoanMerchandiseId;
        [Association(@"Inventory_LoanMerchandiseDevolutionReferencesInventory_LoanMerchandise")]
        public InventoryLoanMerchandiseReportXpo LoanMerchandiseId
        {
            get { return fLoanMerchandiseId; }
            set { SetPropertyValue<InventoryLoanMerchandiseReportXpo>("LoanMerchandiseId", ref fLoanMerchandiseId, value); }
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
        [Association(@"Inventory_LoanMerchandiseDevolutionDetailReferencesInventory_LoanMerchandiseDevolution", typeof(InventoryLoanMerchandiseDevolutionDetailReportXpo))]
        public XPCollection<InventoryLoanMerchandiseDevolutionDetailReportXpo> Inventory_LoanMerchandiseDevolutionDetails { get { return GetCollection<InventoryLoanMerchandiseDevolutionDetailReportXpo>("Inventory_LoanMerchandiseDevolutionDetails"); } }

        public InventoryLoanMerchandiseDevolutionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
