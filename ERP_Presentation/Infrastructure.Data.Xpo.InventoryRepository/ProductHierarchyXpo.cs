using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewProductHierarchy")]
    public class ProductHierarchyXpo : XPLiteObject
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

        int fHierarchyProductFinalId;
        [Persistent("HierarchyProductFinalId")]
        public int HierarchyProductFinalId
        {
            get { return fHierarchyProductFinalId; }
            set { SetPropertyValue<int>("HierarchyProductFinalId", ref fHierarchyProductFinalId, value); }
        }

        InventoryProductXpo fProduct;
        [Persistent("ProductId")]
        [Association(@"ProductHierarchyXpoReferencesPurchaseRequestXpo")]
        public InventoryProductXpo Product
        {
            get { return fProduct; }
            set { SetPropertyValue<InventoryProductXpo>("Product", ref fProduct, value); }
        }

        [PersistentAlias("Product.Id")]
        public int ProductId
        {
            get { return Convert.ToInt32(this.EvaluateAlias("ProductId")); }
        }

        int fParentProductId;
        [Persistent("ParentProductId")]
        public int ParentProductId
        {
            get { return fParentProductId; }
            set { SetPropertyValue<int>("ParentProductId", ref fParentProductId, value); }
        }
        string fName;
        [Persistent("Name")]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        int fQuantity;
        [Persistent("Quantity")]
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        #endregion
        #region "Navigation"


        [Association(@"Inventory_PurchaseOrderDetailReferencesInventory_ProductHierarchyId", typeof(InventoryPurchaseOrderDetailReportXpo))]
        public XPCollection<InventoryPurchaseOrderDetailReportXpo> Inventory_PurchaseOrderDetails { get { return GetCollection<InventoryPurchaseOrderDetailReportXpo>("Inventory_PurchaseOrderDetails"); } }

        #endregion
        #region Builders

        public ProductHierarchyXpo(Session session) : base(session)
        {
        }

        public ProductHierarchyXpo()
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
