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
    [Persistent(@"Inventory.InventoryRequestDetail")]
    public class InventoryRequestDetailXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        InventoryRequestXpo fInventoryRequestId;
        [Association(@"InventoryRequestDetailXpoReferencesInventoryRequestXpo")]
        public InventoryRequestXpo InventoryRequestId
        {
            get { return fInventoryRequestId; }
            set { SetPropertyValue<InventoryRequestXpo>("InventoryRequestId", ref fInventoryRequestId, value); }
        }
        InventoryProductXpo fInventoryProductId;
        [Association(@"InventoryRequestDetailXpoReferencesInventoryProductXpo")]
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
        [Size(300)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        public InventoryRequestDetailXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
