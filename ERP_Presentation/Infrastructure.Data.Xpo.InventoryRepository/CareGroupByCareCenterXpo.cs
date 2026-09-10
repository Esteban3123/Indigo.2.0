//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.InventoryRepository
//' Author           : Miguel Angel Fonseca Castro
//' Created          : 2018-04-16
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.CareGroupByCareCenter")]
    public class CareGroupByCareCenterXpo : XPLiteObject
    {
        #region Members

        private int fId;

        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        private int fCareGroupId;

        [Persistent("CareGroupId")]
        public int CareGroupId
        {
            get { return fCareGroupId; }
            set { SetPropertyValue<int>("CareGroupId", ref fCareGroupId, value); }
        }

        private string fCareCenterCode;

        [Size(10)]
        [Persistent("CareCenterCode")]
        public string CareCenterCode
        {
            get { return fCareCenterCode; }
            set { SetPropertyValue<string>("CareCenterCode", ref fCareCenterCode, value); }
        }

        #endregion Members

        #region Builders

        public CareGroupByCareCenterXpo(Session session) : base(session)
        {
        }

        public CareGroupByCareCenterXpo()
            : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion Builders
    }
}