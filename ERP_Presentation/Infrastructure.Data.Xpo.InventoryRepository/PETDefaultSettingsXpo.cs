using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.PETDefaultSettings")]
    public class PETDefaultSettingsXpo : XPLiteObject
    {

        public PETDefaultSettingsXpo(Session session) : base(session) { }

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fRiskLevelId;
        public int RiskLevelId
        {
            get { return fRiskLevelId; }
            set { SetPropertyValue<int>("RiskLevelId", ref fRiskLevelId, value); }
        }

        int fProductTypeId;
        public int ProductTypeId
        {
            get { return fProductTypeId; }
            set { SetPropertyValue<int>("ProductTypeId", ref fProductTypeId, value); }
        }

        string fCodeAlternative;
        public string CodeAlternative
        {
            get { return fCodeAlternative; }
            set { SetPropertyValue<string>("CodeAlternative", ref fCodeAlternative, value); }
        }

        int fProductGroupId;
        public int ProductGroupId
        {
            get { return fProductGroupId; }
            set { SetPropertyValue<int>("ProductGroupId", ref fProductGroupId, value); }
        }

        int fProductSubGroupId;
        public int ProductSubGroupId
        {
            get { return fProductSubGroupId; }
            set { SetPropertyValue<int>("ProductSubGroupId", ref fProductSubGroupId, value); }
        }

        int fMeasurementUnitId;
        public int MeasurementUnitId
        {
            get { return fMeasurementUnitId; }
            set { SetPropertyValue<int>("MeasurementUnitId", ref fMeasurementUnitId, value); }
        }

        int fPackagingUnitId;
        public int PackagingUnitId
        {
            get { return fPackagingUnitId; }
            set { SetPropertyValue<int>("PackagingUnitId", ref fPackagingUnitId, value); }
        }

        int fManufacturedId;
        public int ManufacturedId
        {
            get { return fManufacturedId; }
            set { SetPropertyValue<int>("ManufacturedId", ref fManufacturedId, value); }
        }

        int fBillingGroupId;
        public int BillingGroupId
        {
            get { return fBillingGroupId; }
            set { SetPropertyValue<int>("BillingGroupId", ref fBillingGroupId, value); }
        }

        string fCreationUser;
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
    }
}
