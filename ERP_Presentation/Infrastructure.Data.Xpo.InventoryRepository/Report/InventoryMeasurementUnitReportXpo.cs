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
     [Persistent(@"Inventory.InventoryMeasurementUnit")]
    public class InventoryMeasurementUnitReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fCode;
        [Indexed(Name = @"IX_InventoryMeasurementUnit", Unique = true)]
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
        string fAbbreviation;
        [Size(10)]
        public string Abbreviation
        {
            get { return fAbbreviation; }
            set { SetPropertyValue<string>("Abbreviation", ref fAbbreviation, value); }
        }
        byte fUnitType;
        public byte UnitType
        {
            get { return fUnitType; }
            set { SetPropertyValue<byte>("UnitType", ref fUnitType, value); }
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

        [Association(@"InventoryATCReportXpo3ReferencesInventoryMeasurementUnitReportXpo", typeof(InventoryATCReportXpo))]
        public XPCollection<InventoryATCReportXpo> InventoryATCReportXpo3 { get { return GetCollection<InventoryATCReportXpo>("InventoryATCReportXpo3"); } }
                 
        [Association(@"InventoryATCReportXpo2ReferencesInventoryMeasurementUnitReportXpo", typeof(InventoryATCReportXpo))]
        public XPCollection<InventoryATCReportXpo> InventoryATCReportXpo2 { get { return GetCollection<InventoryATCReportXpo>("InventoryATCReportXpo2"); } }

        [Association(@"InventoryATCReportXpoReferencesInventoryMeasurementUnitReportXpo", typeof(InventoryATCReportXpo))]
        public XPCollection<InventoryATCReportXpo> InventoryATCReportXpo { get { return GetCollection<InventoryATCReportXpo>("InventoryATCReportXpo"); } }

        [Association(@"InventoryProductReportXpoReferencesInventoryMeasurementUnitReportXpo", typeof(InventoryProductReportXpo))]
        public XPCollection<InventoryProductReportXpo> InventoryProductReportXpo { get { return GetCollection<InventoryProductReportXpo>("InventoryProductReportXpo"); } }

        [Association(@"InventoryRequestDetailReferencesInventory_MeasurementUnit", typeof(Infrastructure.Data.Xpo.InventoryRepository.Report.InventoryPurchaseRequestDetailReportXpo))]
        public XPCollection<Infrastructure.Data.Xpo.InventoryRepository.Report.InventoryPurchaseRequestDetailReportXpo> PurchaseRequestDetails { get { return GetCollection<Infrastructure.Data.Xpo.InventoryRepository.Report.InventoryPurchaseRequestDetailReportXpo>("PurchaseRequestDetails"); } }

        public InventoryMeasurementUnitReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
