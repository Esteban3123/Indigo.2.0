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
    [Persistent(@"GeneralLedger.MainAccountClasses")]
    public class InventoryGeneralLedgerMainAccountClassesReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        //[Indexed(Name = @"IX_AccountClass_Code", Unique = true)]
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
        byte fNature;
        public byte Nature
        {
            get { return fNature; }
            set { SetPropertyValue<byte>("Nature", ref fNature, value); }
        }
        byte fType;
        public byte Type
        {
            get { return fType; }
            set { SetPropertyValue<byte>("Type", ref fType, value); }
        }
        bool fPatrimony;
        public bool Patrimony
        {
            get { return fPatrimony; }
            set { SetPropertyValue<bool>("Patrimony", ref fPatrimony, value); }
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
        [Association(@"InventoryGeneralLedgerMainAccountsReportXpoReferencesInventoryGeneralLedgerMainAccountClassesReportXpo", typeof(InventoryGeneralLedgerMainAccountsReportXpo))]
        public XPCollection<InventoryGeneralLedgerMainAccountsReportXpo> InventoryGeneralLedgerMainAccountsReportXpo { get { return GetCollection<InventoryGeneralLedgerMainAccountsReportXpo>("InventoryGeneralLedgerMainAccountsReportXpo"); } }

        public InventoryGeneralLedgerMainAccountClassesReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
