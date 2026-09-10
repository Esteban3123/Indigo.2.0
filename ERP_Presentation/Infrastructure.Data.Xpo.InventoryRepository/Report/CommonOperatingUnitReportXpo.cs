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
    [Persistent(@"Common.OperatingUnit")]
    public class CommonOperatingUnitReportXpo : XPLiteObject
    {


int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        CommonOperatingUnitReportXpo fIdUnit;
        [Association(@"Common_OperatingUnitReferencesCommon_OperatingUnit")]
        public CommonOperatingUnitReportXpo IdUnit
        {
            get { return fIdUnit; }
            set { SetPropertyValue<CommonOperatingUnitReportXpo>("IdUnit", ref fIdUnit, value); }
        }
        string fUnitName;
        public string UnitName
        {
            get { return fUnitName; }
            set { SetPropertyValue<string>("UnitName", ref fUnitName, value); }
        }
        string fUnitCode;
        [Indexed(Name = @"IX_OperatingUnit", Unique = true)]
        [Size(5)]
        public string UnitCode
        {
            get { return fUnitCode; }
            set { SetPropertyValue<string>("UnitCode", ref fUnitCode, value); }
        }
        string fIPSCode;
        [Size(20)]
        public string IPSCode
        {
            get { return fIPSCode; }
            set { SetPropertyValue<string>("IPSCode", ref fIPSCode, value); }
        }
        string fAddress;
        public string Address
        {
            get { return fAddress; }
            set { SetPropertyValue<string>("Address", ref fAddress, value); }
        }
        string fPhone;
        [Size(20)]
        public string Phone
        {
            get { return fPhone; }
            set { SetPropertyValue<string>("Phone", ref fPhone, value); }
        }
        string fEmail;
        public string Email
        {
            get { return fEmail; }
            set { SetPropertyValue<string>("Email", ref fEmail, value); }
        }
        string fEmailAudit;
        public string EmailAudit
        {
            get { return fEmailAudit; }
            set { SetPropertyValue<string>("EmailAudit", ref fEmailAudit, value); }
        }
        int fIdCity;
        public int IdCity
        {
            get { return fIdCity; }
            set { SetPropertyValue<int>("IdCity", ref fIdCity, value); }
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
        [Association(@"Common_OperatingUnitReferencesCommon_OperatingUnit", typeof(CommonOperatingUnitReportXpo))]
        public XPCollection<CommonOperatingUnitReportXpo> Common_OperatingUnitCollection { get { return GetCollection<CommonOperatingUnitReportXpo>("Common_OperatingUnitCollection"); } }
        [Association(@"Inventory_PharmaceuticalDispensingReferencesCommon_OperatingUnit", typeof(InventoryPharmaceuticalDispensingReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingReportXpo> Inventory_PharmaceuticalDispensings { get { return GetCollection<InventoryPharmaceuticalDispensingReportXpo>("Inventory_PharmaceuticalDispensings"); } }



        public CommonOperatingUnitReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }




    }
}
