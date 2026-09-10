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
    [Persistent(@"Inventory.PharmaceuticalForm")]
    public class InventoryPharmaceuticalFormReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Indexed(Name = @"IX_PharmaceuticalForm", Unique = true)]
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        string fCrystalMedicalForm;
        [Size(3)]
        public string CrystalMedicalForm
        {
            get { return fCrystalMedicalForm; }
            set { SetPropertyValue<string>("CrystalMedicalForm", ref fCrystalMedicalForm, value); }
        }
        bool fStatus;
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
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
        [Association(@"Inventory_AdministrationRouteReferencesInventory_PharmaceuticalForm", typeof(InventoryAdministrationRouteReportXpo))]
        public XPCollection<InventoryAdministrationRouteReportXpo> Inventory_AdministrationRoutes { get { return GetCollection<InventoryAdministrationRouteReportXpo>("Inventory_AdministrationRoutes"); } }
        [Association(@"InventoryATCReportXpoReferencesInventory_PharmaceuticalForm", typeof(InventoryATCReportXpo))]
        public XPCollection<InventoryATCReportXpo> Inventory_ATC { get { return GetCollection<InventoryATCReportXpo>("Inventory_ATC"); } }




        public InventoryPharmaceuticalFormReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
