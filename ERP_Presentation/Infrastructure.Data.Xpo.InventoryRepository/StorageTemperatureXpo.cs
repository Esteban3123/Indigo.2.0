using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.StorageTemperature")]
    public partial class StorageTemperatureXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
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

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        int fFrom;
        public int From
        {
            get { return fFrom; }
            set { SetPropertyValue<int>("From", ref fFrom, value); }
        }

        int fUntil;
        public int Until
        {
            get { return fUntil; }
            set { SetPropertyValue<int>("Until", ref fUntil, value); }
        }

        byte fTemperatureUnit;
        public byte TemperatureUnit
        {
            get { return fTemperatureUnit; }
            set { SetPropertyValue<byte>("TemperatureUnit", ref fTemperatureUnit, value); }
        } 

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        #endregion

        #region Builder

        public StorageTemperatureXpo(Session session) : base(session) { }
        public StorageTemperatureXpo()
          : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }

}
