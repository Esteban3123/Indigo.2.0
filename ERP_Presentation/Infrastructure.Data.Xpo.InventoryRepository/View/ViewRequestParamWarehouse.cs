using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewRequestParamWarehouse")]
    public partial class ViewRequestParamWarehouseXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fRequestParamId;
        public int RequestParamId
        {
            get { return fRequestParamId; }
            set { SetPropertyValue<int>("RequestParamId", ref fRequestParamId, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }

        string fWarehouseCode;
        public string WarehouseCode
        {
            get { return fWarehouseCode; }
            set { SetPropertyValue<string>("WarehouseCode", ref fWarehouseCode, value); }
        }

        string fWarehouseName;
        public string WarehouseName
        {
            get { return fWarehouseName; }
            set { SetPropertyValue<string>("WarehouseName", ref fWarehouseName, value); }
        }

        int? fRequestParamWarehouseId;
        public int? RequestParamWarehouseId
        {
            get { return fRequestParamWarehouseId; }
            set { SetPropertyValue<int?>("RequestParamWarehouseId", ref fRequestParamWarehouseId, value); }
        }

        bool fRequiredAuthorization;
        public bool RequiredAuthorization
        {
            get { return fRequiredAuthorization; }
            set { SetPropertyValue<bool>("RequiredAuthorization", ref fRequiredAuthorization, value); }
        }

        bool fIsSelected;
        public bool IsSelected
        {
            get { return fIsSelected; }
            set { SetPropertyValue<bool>("IsSelected", ref fIsSelected, value); }
        }

        int fStatusWarehouse;
        public int StatusWarehouse
        {
            get { return fStatusWarehouse; }
            set { SetPropertyValue<int>("StatusWarehouse", ref fStatusWarehouse, value); }
        }

        [PersistentAlias("concat(WarehouseCode, ' - ', WarehouseName)")]
        public string WarehouseCodeName
        {
            get { return Convert.ToString(EvaluateAlias("WarehouseCodeName")); }
        }

        public ViewRequestParamWarehouseXpo(Session session) : base(session) { }

    }
}
