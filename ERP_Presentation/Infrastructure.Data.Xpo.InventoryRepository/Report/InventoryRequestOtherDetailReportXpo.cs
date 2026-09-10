using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Inventory.InventoryRequestDetailOther")]
    public class InventoryRequestOtherDetailReportXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        InventoryRequestReportXpo fInventoryRequestId;
        [Association(@"Inventory_InventoryRequestDetailOtherReferencesInventory_InventoryRequest")]
        public InventoryRequestReportXpo InventoryRequestId
        {
            get { return fInventoryRequestId; }
            set { SetPropertyValue<InventoryRequestReportXpo>("InventoryRequestId", ref fInventoryRequestId, value); }
        }

        int fComponentType;
        public int ComponentType
        {
            get { return fComponentType; }
            set { SetPropertyValue<int>("ComponentType", ref fComponentType, value); }
        }

        ATCXpo fATCId;
        [Association(@"InventoryRequestOtherDetailReportXpo_References_Atc")]
        public ATCXpo ATCId
        {
            get { return fATCId; }
            set { SetPropertyValue<ATCXpo>("ATCId", ref fATCId, value); }
        }

        InventorySupplieXpo fSupplieId;
        [Association(@"InventoryRequestOtherDetailReportXpo_References_Supplied")]
        public InventorySupplieXpo SupplieId
        {
            get { return fSupplieId; }
            set { SetPropertyValue<InventorySupplieXpo>("SupplieId", ref fSupplieId, value); }
        }


        InventoryProductXpo fInventoryProductId;
        [Association(@"InventoryRequestOtherDetailReportXpo_Reference_Product")]
        public InventoryProductXpo InventoryProductId
        {
            get { return fInventoryProductId; }
            set { SetPropertyValue<InventoryProductXpo>("InventoryProductId", ref fInventoryProductId, value); }
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
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        int fStatus;
        public int Status
        {
            get { return fStatus; }
            set { SetPropertyValue<int>("Status", ref fStatus, value); }
        }


        public InventoryRequestOtherDetailReportXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
