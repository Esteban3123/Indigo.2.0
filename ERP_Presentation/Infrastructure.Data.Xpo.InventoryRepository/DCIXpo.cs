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
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.DCI")]
    public class DCIXpo : XPLiteObject
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

        bool fCombined;
        [Persistent("Combined")]
        public bool Combined
        {
            get { return fCombined; }
            set { SetPropertyValue<bool>("Combined", ref fCombined, value); }
        }

        [Size(2)]
        [PersistentAlias("Iif(Combined = true, 'SI', 'NO')")]
        public string CombinedName
        {
            get { return Convert.ToString(this.EvaluateAlias("CombinedName")); }
        }

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        [Association(@"ATCReferencesDCI", typeof(ATCXpo))]
        public XPCollection<ATCXpo> ATCs { get { return GetCollection<ATCXpo>("ATCs"); } }

        [Association(@"DrugInteractionReferencesParentDCI", typeof(DrugInteractionXpo))]
        public XPCollection<DrugInteractionXpo> DrugInteractionParents { get { return GetCollection<DrugInteractionXpo>("DrugInteractionParents"); } }

        [Association(@"DrugInteractionReferencesDCI", typeof(DrugInteractionXpo))]
        public XPCollection<DrugInteractionXpo> DrugInteractions { get { return GetCollection<DrugInteractionXpo>("DrugInteractions"); } }

        [Association(@"DrugActiveReferencesParentDCI", typeof(DrugActiveXpo))]
        public XPCollection<DrugActiveXpo> DrugActivesParents { get { return GetCollection<DrugActiveXpo>("DrugActivesParents"); } }

        [Association(@"DrugActiveReferencesDCI", typeof(DrugActiveXpo))]
        public XPCollection<DrugActiveXpo> DrugActives { get { return GetCollection<DrugActiveXpo>("DrugActives"); } }

        #endregion

        #region Builders

        public DCIXpo(Session session) : base(session)
        {
        }

        public DCIXpo()
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
