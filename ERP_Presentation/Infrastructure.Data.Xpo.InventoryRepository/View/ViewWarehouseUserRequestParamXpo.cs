using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewWarehouseUserRequestParam")]
    public class ViewWarehouseUserRequestParamXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fCodeWarehouse;
        [Persistent("CodeWarehouse")]
        public string CodeWarehouse
        {
            get { return fCodeWarehouse; }
            set { SetPropertyValue<string>("CodeWarehouse", ref fCodeWarehouse, value); }
        }

        string fCodeName;
        [Persistent("CodeName")]
        public string CodeName
        {
            get { return fCodeName; }
            set { SetPropertyValue<string>("CodeName", ref fCodeName, value); }
        }

        public ViewWarehouseUserRequestParamXpo(Session session) : base(session) { }
        public ViewWarehouseUserRequestParamXpo() : base(Session.DefaultSession) { }
    }
}
