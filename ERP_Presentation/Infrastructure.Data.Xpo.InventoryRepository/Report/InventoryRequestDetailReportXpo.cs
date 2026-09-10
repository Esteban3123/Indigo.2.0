using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.InventoryRequestDetail")]
    public class InventoryRequestDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRequestReportXpo fInventoryRequestId;
        [Association(@"Inventory_InventoryRequestDetailReferencesInventory_InventoryRequest")]
        public InventoryRequestReportXpo InventoryRequestId
        {
            get { return fInventoryRequestId; }
            set { SetPropertyValue<InventoryRequestReportXpo>("InventoryRequestId", ref fInventoryRequestId, value); }
        }
        InventoryProductReportXpo fInventoryProductId;
        [Association(@"InventoryRequestDetailReportXpoReferencesInventoryProductReportXpo")]
        public InventoryProductReportXpo InventoryProductId
        {
            get { return fInventoryProductId; }
            set { SetPropertyValue<InventoryProductReportXpo>("InventoryProductId", ref fInventoryProductId, value); }
        }
        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }
        int fOutstandingQuantity;
        public int OutstandingQuantity
        {
            get { return fOutstandingQuantity; }
            set { SetPropertyValue<int>("OutstandingQuantity", ref fOutstandingQuantity, value); }
        }
        string fDescription;
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        public InventoryRequestDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
