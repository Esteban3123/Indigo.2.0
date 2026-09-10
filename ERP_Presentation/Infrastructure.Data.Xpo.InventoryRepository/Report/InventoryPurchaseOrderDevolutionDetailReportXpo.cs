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
    [Persistent(@"Inventory.PurchaseOrderDevolutionDetail")]
    public class InventoryPurchaseOrderDevolutionDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryPurchaseOrderDevolutionReportXpo fPurchaseOrderDevolutionId;
        [Association(@"Inventory_PurchaseOrderDevolutionDetailReferencesInventory_PurchaseOrderDevolution")]
        public InventoryPurchaseOrderDevolutionReportXpo PurchaseOrderDevolutionId
        {
            get { return fPurchaseOrderDevolutionId; }
            set { SetPropertyValue<InventoryPurchaseOrderDevolutionReportXpo>("PurchaseOrderDevolutionId", ref fPurchaseOrderDevolutionId, value); }
        }
        InventoryPurchaseOrderDetailReportXpo fPurchaseOrderDetailId;
        [Association(@"Inventory_PurchaseOrderDevolutionDetailReferencesInventory_PurchaseOrderDetail")]
        public InventoryPurchaseOrderDetailReportXpo PurchaseOrderDetailId
        {
            get { return fPurchaseOrderDetailId; }
            set { SetPropertyValue<InventoryPurchaseOrderDetailReportXpo>("PurchaseOrderDetailId", ref fPurchaseOrderDetailId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }


        public InventoryPurchaseOrderDevolutionDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
