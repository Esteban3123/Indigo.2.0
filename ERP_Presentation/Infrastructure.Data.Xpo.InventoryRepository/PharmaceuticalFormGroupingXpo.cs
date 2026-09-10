using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.PharmaceuticalFormGrouping")]
    public class PharmaceuticalFormGroupingXpo : XPLiteObject
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

        bool fStatus;
        [Persistent("Status")]
        public bool Status
        {
            get { return fStatus; }
            set { SetPropertyValue<bool>("Status", ref fStatus, value); }
        }

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }


        #endregion

        #region Builders

        public PharmaceuticalFormGroupingXpo(Session session) : base(session)
        {
        }

        public PharmaceuticalFormGroupingXpo()
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
