using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    /// <summary>
    /// Se usar para listar las vias de administracion de un medicamento
    /// </summary>
    [Persistent("Inventory.ViewAtcAdministrationRoute")]
    public class ViewATCAdministrationRouteXpo : XPLiteObject
    {
        #region "Members"
        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fATCId;
        [Persistent("ATCId")]
        public int ATCId
        {
            get { return fATCId; }
            set { SetPropertyValue<int>("ATCId", ref fATCId, value); }
        }

        string fAdministrationRouteCode;
        [Size(20)]
        [Persistent("AdministrationRouteCode")]
        public string AdministrationRouteCode
        {
            get { return fAdministrationRouteCode; }
            set { SetPropertyValue<string>("AdministrationRouteCode", ref fAdministrationRouteCode, value); }
        }

        string fAdministrationRouteName;
        [Size(100)]
        [Persistent("AdministrationRouteName")]
        public string AdministrationRouteName
        {
            get { return fAdministrationRouteName; }
            set { SetPropertyValue<string>("AdministrationRouteName", ref fAdministrationRouteName, value); }
        }

        int fAdministrationRouteId;
        [Persistent("AdministrationRouteId")]
        public int AdministrationRouteId
        {
            get { return fAdministrationRouteId; }
            set { SetPropertyValue<int>("AdministrationRouteId", ref fAdministrationRouteId, value); }
        }

        int fPharmaceuticalFormId;
        [Persistent("PharmaceuticalFormId")]
        public int PharmaceuticalFormId
        {
            get { return fPharmaceuticalFormId; }
            set { SetPropertyValue<int>("PharmaceuticalFormId", ref fPharmaceuticalFormId, value); }
        }

        [Size(120)]
        [PersistentAlias("concat(concat(AdministrationRouteCode,' - '),AdministrationRouteName)")]
        public string AdministrationRouteCodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("AdministrationRouteCodeName")); }
        }
        #endregion

        #region Builders

        public ViewATCAdministrationRouteXpo(Session session) : base(session)
        {
        }

        public ViewATCAdministrationRouteXpo()
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
