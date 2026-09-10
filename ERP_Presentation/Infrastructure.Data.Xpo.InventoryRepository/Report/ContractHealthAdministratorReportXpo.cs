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
    [Persistent(@"Contract.HealthAdministrator")]
    public class ContractHealthAdministratorReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Indexed(Name = @"IX_HealthAdministrator", Unique = true)]
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }
        string fName;
        [Size(300)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        int fThirdPartyId;
        public int ThirdPartyId
        {
            get { return fThirdPartyId; }
            set { SetPropertyValue<int>("ThirdPartyId", ref fThirdPartyId, value); }
        }
        byte fEntityType;
        public byte EntityType
        {
            get { return fEntityType; }
            set { SetPropertyValue<byte>("EntityType", ref fEntityType, value); }
        }
        string fHealthEntityCode;
        [Size(6)]
        public string HealthEntityCode
        {
            get { return fHealthEntityCode; }
            set { SetPropertyValue<string>("HealthEntityCode", ref fHealthEntityCode, value); }
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
        [Association(@"Inventory_PharmaceuticalDispensingDetailReferencesContract_HealthAdministrator", typeof(InventoryPharmaceuticalDispensingDetailReportXpo))]
        public XPCollection<InventoryPharmaceuticalDispensingDetailReportXpo> Inventory_PharmaceuticalDispensingDetails { get { return GetCollection<InventoryPharmaceuticalDispensingDetailReportXpo>("Inventory_PharmaceuticalDispensingDetails"); } }






        public ContractHealthAdministratorReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
