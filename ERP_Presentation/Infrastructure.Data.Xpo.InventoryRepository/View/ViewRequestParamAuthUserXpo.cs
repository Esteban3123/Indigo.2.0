using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewRequestParamAuthUser")]
    public class ViewRequestParamAuthUserXpo : XPLiteObject
    {
         int fId;
        [Key(true)]
        //[Persistent("Id")]
        public  int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fIdRequestParamAuthUser;
       // [Persistent("IdRequestParamAuthUser")]
        public int IdRequestParamAuthUser
        {
            get { return fIdRequestParamAuthUser; }
            set { SetPropertyValue<int>("IdRequestParamAuthUser", ref fIdRequestParamAuthUser, value); }
        }


        int fIdRequestParam;
       // [Persistent("IdRequestParam")]
        public int IdRequestParam
        {
            get { return fIdRequestParam; }
            set { SetPropertyValue<int>("IdRequestParam", ref fIdRequestParam, value); }
        }

        int fIdType;
       // [Persistent("IdType")]
        public int IdType
        {
            get { return fIdType; }
            set { SetPropertyValue<int>("IdType", ref fIdType, value); }
        }


        string fCodeRequestParam;
       // [Persistent("CodeRequestParam")]
        public string CodeRequestParam
        {
            get { return fCodeRequestParam; }
            set { SetPropertyValue<string>("CodeRequestParam", ref fCodeRequestParam, value); }
        }

        string fCode;
      //  [Persistent("Code")]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fCodeName;
       // [Persistent("CodeName")]
        public string CodeName
        {
            get { return fCodeName; }
            set { SetPropertyValue<string>("CodeName", ref fCodeName, value); }
        }

        string fType;
       // [Persistent("Type")]
        public string Type
        {
            get { return fType; }
            set { SetPropertyValue<string>("Type", ref fType, value); }
        }

        int fCodeType;
        // [Persistent("IdType")]
        public int CodeType
        {
            get { return fCodeType; }
            set { SetPropertyValue<int>("CodeType", ref fCodeType, value); }
        }

        int? fUserPrincipalId;
        //[Persistent("UserPrincipalId")]
        public int? UserPrincipalId
        {
            get { return fUserPrincipalId; }
            set { SetPropertyValue<int?>("UserPrincipalId", ref fUserPrincipalId, value); }
        }

        int? fUserAlternativeId;
      ///  [Persistent("UserAlternativeId")]
        public int? UserAlternativeId
        {
            get { return fUserAlternativeId; }
            set { SetPropertyValue<int?>("UserAlternativeId", ref fUserAlternativeId, value); }
        }

        public ViewRequestParamAuthUserXpo(Session session) : base(session) { }
        public ViewRequestParamAuthUserXpo() : base(Session.DefaultSession) { }
    }
}
