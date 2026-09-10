using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewFunctionalUnitUserRequestParam")]
    public class ViewFunctionalUnitUserRequestParamXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCodeFunctionalUnit;
        [Persistent("CodeFunctionalUnit")]
        public string CodeFunctionalUnit
        {
            get { return fCodeFunctionalUnit; }
            set { SetPropertyValue<string>("CodeFunctionalUnit", ref fCodeFunctionalUnit, value); }
        }

        string fCodeName;
        [Persistent("CodeName")]
        public string CodeName
        {
            get { return fCodeName; }
            set { SetPropertyValue<string>("CodeName", ref fCodeName, value); }
        }

        public ViewFunctionalUnitUserRequestParamXpo(Session session) : base(session) { }
        public ViewFunctionalUnitUserRequestParamXpo() : base(Session.DefaultSession) { }
    }
}
