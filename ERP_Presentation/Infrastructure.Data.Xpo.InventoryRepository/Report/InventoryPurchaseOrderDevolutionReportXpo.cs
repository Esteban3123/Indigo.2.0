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
    [Persistent(@"Inventory.PurchaseOrderDevolution")]
    public class InventoryPurchaseOrderDevolutionReportXpo: XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Indexed(Name = @"IX_PurchaseOrderDevolution", Unique = true)]
        [Size(20)]
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
        InventoryCommonSupplierXpo fSupplierId;
        [Association(@"Inventory_PurchaseOrderDevolutionReferencesCommon_Supplier")]
        public InventoryCommonSupplierXpo SupplierId
        {
            get { return fSupplierId; }
            set { SetPropertyValue<InventoryCommonSupplierXpo>("SupplierId", ref fSupplierId, value); }
        }
        string fObservation;
        [Size(1000)]
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
        [Association(@"Inventory_PurchaseOrderDevolutionDetailReferencesInventory_PurchaseOrderDevolution", typeof(InventoryPurchaseOrderDevolutionDetailReportXpo))]
        public XPCollection<InventoryPurchaseOrderDevolutionDetailReportXpo> InventoryPurchaseOrderDevolutionDetailsReportXpo { get { return GetCollection<InventoryPurchaseOrderDevolutionDetailReportXpo>("InventoryPurchaseOrderDevolutionDetailsReportXpo"); } }




        public InventoryPurchaseOrderDevolutionReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
