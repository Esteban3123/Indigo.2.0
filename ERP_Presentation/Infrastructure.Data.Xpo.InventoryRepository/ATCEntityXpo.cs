//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
//' Author           : Daniel Eduardo Arévalo
//' Created          : 11-04-2019
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
    [Persistent("Inventory.ATCEntity")]
    public class ATCEntityXpo: XPLiteObject
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
        [Size(50)]
        [Persistent("Name")]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        byte fState;
        [Persistent("State")]
        public byte State
        {
            get { return fState; }
            set { SetPropertyValue<byte>("State", ref fState, value); }
        }

        PharmacologicalGroupXpo fIdPharmacologicalGroup;
        [Association("ATCEntityReferencesPharmacologicalGroup")]
        public PharmacologicalGroupXpo IdPharmacologicalGroup
        {
            get { return fIdPharmacologicalGroup; }
            set { SetPropertyValue<PharmacologicalGroupXpo>("IdPharmacologicalGroup", ref fIdPharmacologicalGroup, value); }
        }

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        [Association("ATCReferencesATCEntity", typeof(ATCXpo))]
        public XPCollection<ATCXpo> ATCXpo
        {
            get { return GetCollection<ATCXpo>("ATCXpo"); }
        }

        #endregion

        #region Builders

        public ATCEntityXpo(Session session) : base(session)
        {
        }

        public ATCEntityXpo()
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
