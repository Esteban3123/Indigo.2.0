//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 05-04-2014
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.PharmaceuticalForm")]
    public class PharmaceuticalFormXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCode;
        [Size(20)]
        [Persistent("Code")]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        [Size(100)]
        [Persistent("Name")]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        byte fStatus;
        [Persistent("Status")]
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        bool? fRequireStability;
        [Persistent("RequireStability")]
        public bool? RequireStability
        {
            get { return fRequireStability; }
            set { SetPropertyValue<bool?>("RequireStability", ref fRequireStability, value); }
        }

        [Association("AdministrationRouteReferencesPharmaceuticalForm", typeof(AdministrationRouteXpo))]
        public XPCollection<AdministrationRouteXpo> AdministrationRouteXpo
        {
            get { return GetCollection<AdministrationRouteXpo>("AdministrationRouteXpo"); }
        }


        [Association("ATCXpo_References_PharmaceuticalForm", typeof(ATCXpo))]
        public XPCollection<ATCXpo> ATCXpo
        {
            get { return GetCollection<ATCXpo>("ATCXpo"); }
        }

        #endregion

        #region Builders

        public PharmaceuticalFormXpo(Session session) : base(session)
        {
        }

        public PharmaceuticalFormXpo()
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
