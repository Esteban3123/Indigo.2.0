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
    [Persistent(@"GeneralLedger.MainAccountLevels")]
     public class InventoryGeneralLedgerMainAccountsLevelsReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        int fLevel;
        //[Indexed(Name = @"IX_AccountLevel_Code", Unique = true)]
        public int Level
        {
            get { return fLevel; }
            set { SetPropertyValue<int>("Level", ref fLevel, value); }
        }
        int fLength;
        public int Length
        {
            get { return fLength; }
            set { SetPropertyValue<int>("Length", ref fLength, value); }
        }
        int fdigits;
        public int digits
        {
            get { return fdigits; }
            set { SetPropertyValue<int>("digits", ref fdigits, value); }
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
        [Association(@"InventoryGeneralLedgerMainAccountsReportXpoReferencesInventoryGeneralLedgerMainAccountsLevelsReportXpo", typeof(InventoryGeneralLedgerMainAccountsReportXpo))]
        public XPCollection<InventoryGeneralLedgerMainAccountsReportXpo> InventoryGeneralLedgerMainAccountsReportXpo { get { return GetCollection<InventoryGeneralLedgerMainAccountsReportXpo>("InventoryGeneralLedgerMainAccountsReportXpo"); } }

        public InventoryGeneralLedgerMainAccountsLevelsReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
