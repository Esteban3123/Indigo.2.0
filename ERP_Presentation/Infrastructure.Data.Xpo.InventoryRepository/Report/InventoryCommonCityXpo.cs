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
    [Persistent(@"Common.City")]
    public class InventoryCommonCityXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryCommonDepartmentXpo fDepartamentId;
        [Association(@"Common_CityReferencesCommon_Department")]
        public InventoryCommonDepartmentXpo DepartamentId
        {
            get { return fDepartamentId; }
            set { SetPropertyValue<InventoryCommonDepartmentXpo>("DepartamentId", ref fDepartamentId, value); }
        }
        string fCode;
        [Size(5)]
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
        bool fState;
        public bool State
        {
            get { return fState; }
            set { SetPropertyValue<bool>("State", ref fState, value); }
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
        [Association(@"Common_PersonReferencesCommon_City", typeof(InventoryCommonPersonXpo))]
        public XPCollection<InventoryCommonPersonXpo> Common_Persons { get { return GetCollection<InventoryCommonPersonXpo>("Common_Persons"); } }
        [Association(@"Common_PersonReferencesCommon_City1", typeof(InventoryCommonPersonXpo))]
        public XPCollection<InventoryCommonPersonXpo> Common_Persons1 { get { return GetCollection<InventoryCommonPersonXpo>("Common_Persons1"); } }
        [Association(@"Common_SupplierReferencesCommon_City", typeof(InventoryCommonSupplierXpo))]
        public XPCollection<InventoryCommonSupplierXpo> Common_Suppliers { get { return GetCollection<InventoryCommonSupplierXpo>("Common_Suppliers"); } }

        public InventoryCommonCityXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
