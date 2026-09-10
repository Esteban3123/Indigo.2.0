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
    [Persistent(@"Common.Address")]
    public class InventoryCommonAddressXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryCommonPersonXpo fIdPerson;
        [Association(@"Common_AddressReferencesCommon_Person")]
        public InventoryCommonPersonXpo IdPerson
        {
            get { return fIdPerson; }
            set { SetPropertyValue<InventoryCommonPersonXpo>("IdPerson", ref fIdPerson, value); }
        }
        string fAddresss;
        public string Addresss
        {
            get { return fAddresss; }
            set { SetPropertyValue<string>("Addresss", ref fAddresss, value); }
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

        public InventoryCommonAddressXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
