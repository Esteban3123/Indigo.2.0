using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{

    [Persistent(@"Inventory.InventoryControlDetail")]
    public partial class InventoryControlDetailReportXpo : XPLiteObject
    {

        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        InventoryControlReportXpo fInventoryControlId;
        [Association(@"Inventory_InventoryControlDetailReferencesInventory_InventoryControl")]
        public InventoryControlReportXpo InventoryControlId
        {
            get { return fInventoryControlId; }
            set { SetPropertyValue<InventoryControlReportXpo>("InventoryControlId", ref fInventoryControlId, value); }
        }

        InventoryProductReportXpo fProductId;
        [Association(@"Inventory_InventoryControlDetailReferencesInventory_InventoryProduct")]
        public InventoryProductReportXpo ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("ProductId", ref fProductId, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        #endregion

        #region "Navigation"

        [Association(@"Inventory_InventoryControlDetailBatchSerialReferencesInventory_InventoryControlDetail", typeof(InventoryControlDetailBatchSerialReportXpo))]
        public XPCollection<InventoryControlDetailBatchSerialReportXpo> InventoryControlDetailBatchSerials { get { return GetCollection<InventoryControlDetailBatchSerialReportXpo>("InventoryControlDetailBatchSerials"); } }

        #endregion

        #region "Builders"

        public InventoryControlDetailReportXpo(Session session) : base(session) { }

        #endregion
    }

}
