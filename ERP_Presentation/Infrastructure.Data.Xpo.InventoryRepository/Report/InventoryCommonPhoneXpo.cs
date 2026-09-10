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
    [Persistent(@"Common.Phone")]   
    public class InventoryCommonPhoneXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryCommonPersonXpo fIdPerson;
        [Association(@"Common_PhoneReferencesCommon_Person")]
        public InventoryCommonPersonXpo IdPerson
        {
            get { return fIdPerson; }
            set { SetPropertyValue<InventoryCommonPersonXpo>("IdPerson", ref fIdPerson, value); }
        }
        string fPhone;
        [Size(15)]
        public string Phone
        {
            get { return fPhone; }
            set { SetPropertyValue<string>("Phone", ref fPhone, value); }
        }
        bool fState;
        public bool State
        {
            get { return fState; }
            set { SetPropertyValue<bool>("State", ref fState, value); }
        }
        char fSynchronized;
        public char Synchronized
        {
            get { return fSynchronized; }
            set { SetPropertyValue<char>("Synchronized", ref fSynchronized, value); }
        }
        short fIdPhoneType;
        public short IdPhoneType
        {
            get { return fIdPhoneType; }
            set { SetPropertyValue<short>("IdPhoneType", ref fIdPhoneType, value); }
        }

        public InventoryCommonPhoneXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
