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
     [Persistent(@"Inventory.AdministrationRoute")]
    public class InventoryAdministrationRouteReportXpo : XPLiteObject
    {


          int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryPharmaceuticalFormReportXpo fPharmaceuticalFormId;
        [Association(@"Inventory_AdministrationRouteReferencesInventory_PharmaceuticalForm")]
        public InventoryPharmaceuticalFormReportXpo PharmaceuticalFormId
        {
            get { return fPharmaceuticalFormId; }
            set { SetPropertyValue<InventoryPharmaceuticalFormReportXpo>("PharmaceuticalFormId", ref fPharmaceuticalFormId, value); }
        }
        string fCode;
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
        string fCrystalAdministrationRoute;
        [Size(2)]
        public string CrystalAdministrationRoute
        {
            get { return fCrystalAdministrationRoute; }
            set { SetPropertyValue<string>("CrystalAdministrationRoute", ref fCrystalAdministrationRoute, value); }
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
        [Association(@"Inventory_ATCReferencesInventory_AdministrationRoute", typeof(InventoryATCReportXpo))]
        public XPCollection<InventoryATCReportXpo> Inventory_ATCs { get { return GetCollection<InventoryATCReportXpo>("Inventory_ATCs"); } }





        public InventoryAdministrationRouteReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
