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
    [Persistent(@"Common.DistributionLines")]
    public class InventoryCommonDistributionLineReportXpo : XPLiteObject
    {
        public InventoryCommonDistributionLineReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Indexed(Name = @"IX_DistributionLines", Unique = true)]
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
        string fDescription;
        [Size(500)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        int fIdMainAccount;
        public int IdMainAccount
        {
            get { return fIdMainAccount; }
            set { SetPropertyValue<int>("IdMainAccount", ref fIdMainAccount, value); }
        }
        int fExpensesConceptId;
        public int ExpensesConceptId
        {
            get { return fExpensesConceptId; }
            set { SetPropertyValue<int>("ExpensesConceptId", ref fExpensesConceptId, value); }
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
        [Association(@"Common_SuppliersDistributionLinesReferencesCommon_DistributionLines", typeof(InventoryCommonSupplierDistributionLineReportXpo))]
        public XPCollection<InventoryCommonSupplierDistributionLineReportXpo> Common_SuppliersDistributionLiness { get { return GetCollection<InventoryCommonSupplierDistributionLineReportXpo>("Common_SuppliersDistributionLiness"); } }
    }
}
