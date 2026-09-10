


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
    [Persistent(@"Inventory.ViewListSuppliersByControl")]
    public class ViewListSuppliersByControlXpo : XPLiteObject
    {
        int fEntranceVoucherDetailId;
        [Key(true)]
        public int EntranceVoucherDetailId
        {
            get { return fEntranceVoucherDetailId; }
            set { SetPropertyValue<int>("EntranceVoucherDetailId", ref fEntranceVoucherDetailId, value); }
        }

        int fProductId;
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
        }

        string fSupplierCode;
        public string SupplierCode
        {
            get { return fSupplierCode; }
            set { SetPropertyValue<string>("SupplierCode", ref fSupplierCode, value); }
        }

        string fSupplierName;
        public string SupplierName
        {
            get { return fSupplierName; }
            set { SetPropertyValue<string>("SupplierName", ref fSupplierName, value); }
        }

        decimal fUnitValue;
        public decimal UnitValue
        {
            get { return fUnitValue; }
            set { SetPropertyValue<decimal>("UnitValue", ref fUnitValue, value); }
        }    

        public ViewListSuppliersByControlXpo(Session session) : base(session) { }

    }
}
